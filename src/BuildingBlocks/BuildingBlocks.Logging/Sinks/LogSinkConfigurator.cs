using BuildingBlocks.Logging.Configuration;
using Elastic.Ingest.Elasticsearch.DataStreams;
using Elastic.Serilog.Sinks;
using Elastic.Transport;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Grafana.Loki;

namespace BuildingBlocks.Logging.Sinks;

/// <summary>
/// 日志持久化输出端配置器
/// <para>支持：本地文件、Elasticsearch、Grafana Loki</para>
/// </summary>
public static class LogSinkConfigurator
{
    /// <summary>
    /// 统一配置所有日志输出持久化端
    /// <list type="bullet">
    /// <item>控制台</item>
    /// <item>文件</item>
    /// <item>Elasticsearch</item>
    /// <item>Grafana Loki</item>
    /// </list>
    /// </summary>
    /// <param name="loggerConfig">Serilog配置构建器</param>
    /// <param name="options">日志输出配置选项</param>
    /// <returns>配置了所有日志输出持久化端的日志构建器</returns>
    public static LoggerConfiguration ConfigureAllSinks(this LoggerConfiguration loggerConfig, LoggingSinkOptions options)
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));

        return loggerConfig
            .WriteToConsole()
            .WriteToFile(options.File)
            .WriteToElasticsearch(options.Elasticsearch)
            .WriteToGrafanaLoki(options.GrafanaLoki);
    }

    #region 独立输出端配置
    /// <summary>
    /// 配置控制台输出
    /// </summary>
    /// <param name="loggerConfig">Serilog配置构建器</param>
    /// <returns>配置了控制台输出的日志构建器</returns>
    public static LoggerConfiguration WriteToConsole(this LoggerConfiguration loggerConfig)
        => loggerConfig.WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}");
    
    /// <summary>
    /// 配置文件输出
    /// </summary>
    /// <param name="loggerConfig">Serilog配置构建器</param>
    /// <param name="options">文件输出配置选项</param>
    /// <returns>配置了文件输出的日志构建器</returns>
    public static LoggerConfiguration WriteToFile(this LoggerConfiguration loggerConfig, FileLoggingSinkOptions options)
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));

        return loggerConfig.WriteTo.File(
            path: options.Path,
            rollingInterval: RollingInterval.Day,
            outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} {Level:u3}] {Message:lj}{NewLine}{Exception}",
            retainedFileCountLimit: options.RetainDays);
    }

    /// <summary>
    /// 配置Elasticsearch日志输出
    /// </summary>
    /// <param name="loggerConfig">Serilog配置构建器</param>
    /// <param name="options">Elasticsearch日志输出配置选项</param>
    /// <returns>配置了Elasticsearch输出的日志构建器</returns>
    public static LoggerConfiguration WriteToElasticsearch(this LoggerConfiguration loggerConfig, ElasticsearchLoggingSinkOptions? options)
    {
        if (options is null || options.NodeUrls.Length == 0) return loggerConfig;

        // 安全转换节点地址数组
        var nodeUris = options.NodeUrls
            .Select(url => Uri.TryCreate(url, UriKind.Absolute, out var uri) ? uri : null)
            .Where(uri => uri is not null)
            .Cast<Uri>()
            .ToArray();

        if (nodeUris.Length == 0) return loggerConfig;

        return loggerConfig.WriteTo.Elasticsearch(
            // 参数1: ES集群节点地址集合
            nodes: nodeUris,
            // 参数2: Sink输出端配置
            configureOptions: sinkOpts =>
            {
                // DataStream命名规范{prefix}-{type}-{dataset}
                sinkOpts.DataStream = new DataStreamName(
                    options.DataStreamPrefix, 
                    options.DataStreamType, 
                    options.DataStreamDataset);
                
                // 绑定ILM生命周期策略
                if (!string.IsNullOrEmpty(options.IlmPolicy))
                    sinkOpts.IlmPolicy = options.IlmPolicy;
                
                // 模板自动安装策略
                sinkOpts.BootstrapMethod = options.BootstrapMethod;

                // 缓冲区配置
                if (options.BufferOptions is not null)
                {
                    sinkOpts.ConfigureChannel = channelOpts =>
                    {
                        channelOpts.BufferOptions = options.BufferOptions;
                    };
                }

                // 日志最低输出级别
                sinkOpts.MinimumLevel = LogEventLevel.Information;
            },
            // 参数3: TransportConfigurationDescriptor配置
            configureTransport: transportOpts =>
            {
                // Basic用户名密码认证
                if (options.BasicAuth is not null &&
                !string.IsNullOrEmpty(options.BasicAuth.Username) &&
                !string.IsNullOrEmpty(options.BasicAuth.Password))
                    transportOpts.Authentication(new BasicAuthentication(options.BasicAuth.Username,  options.BasicAuth.Password));

                // API Key认证
                if (!string.IsNullOrEmpty(options.ApiKey))
                    transportOpts.Authentication(new ApiKey(options.ApiKey));

                // 证书指纹配置
                if (!string.IsNullOrEmpty(options.CertificateFingerprint))
                    transportOpts.CertificateFingerprint(options.CertificateFingerprint);

                // 请求超时配置
                transportOpts.RequestTimeout(TimeSpan.FromSeconds(options.RequestTimeoutSeconds));

                // 最大重试次数
                transportOpts.MaximumRetries(options.MaxRetries);

                // 生产优化: 关闭直流传输
                transportOpts.DisableDirectStreaming();
            },
            // 参数4: 是否启用集群嗅探
            useSniffing: options.UseSniffing,
            // 参数5: 日志级别开关
            levelSwitch: null,
            // 参数6: 最低日志级别
            restrictedToMinimumLevel: LogEventLevel.Information);
    }

    /// <summary>
    /// 配置Grafana Loki日志输出
    /// </summary>
    /// <param name="loggerConfig">Serilog配置构建器</param>
    /// <param name="options">Grafana Loki日志输出配置选项</param>
    /// <returns>配置了Grafana Loki日志输出的日志构建器</returns>
    public static LoggerConfiguration WriteToGrafanaLoki(this LoggerConfiguration loggerConfig, GrafanaLokiLoggingSinkOptions? options)
    {
        if (options is null || string.IsNullOrEmpty(options.Url)) return loggerConfig;

        var labels = new List<LokiLabel>
        {
            new() { Key = "Application", Value = options.ApplicationName },
            new() { Key = "Environment", Value = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development"}
        };

        return loggerConfig.WriteTo.GrafanaLoki(
            // 必需参数：Loki 服务地址
            uri: options.Url,
            // 全局标签
            labels: labels,
            // 最低日志级别
            restrictedToMinimumLevel: LogEventLevel.Information,
            // 批量配置（可选，使用官方默认值）
            batchPostingLimit: options.BatchPostingLimit ?? 1000,
            queueLimit: options.QueueLimit,
            period: options.Period.HasValue ? TimeSpan.FromSeconds(options.Period.Value) : null,
            // 认证配置（可选）
            credentials: options.Credentials,
            // Tenant ID（可选）
            tenant: options.Tenant
        );
    }
    #endregion
}
