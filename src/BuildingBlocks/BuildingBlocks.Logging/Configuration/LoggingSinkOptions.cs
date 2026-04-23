namespace BuildingBlocks.Logging.Configuration;

/// <summary>
/// 日志输出端配置模型 (类型安全的日志配置)
/// <para>(Type-safe logging configuration)</para>
/// </summary>
public class LoggingSinkOptions
{
    /// <summary>
    /// 文件输出配置
    /// </summary>
    public FileLoggingSinkOptions File { get; set; } = new();

    /// <summary>
    /// Elasticsearch 输出配置
    /// </summary>
    public ElasticsearchLoggingSinkOptions? Elasticsearch { get; set; }

    /// <summary>
    /// Grafana Loki 输出配置
    /// </summary>
    public GrafanaLokiLoggingSinkOptions? GrafanaLoki { get; set; }
}
