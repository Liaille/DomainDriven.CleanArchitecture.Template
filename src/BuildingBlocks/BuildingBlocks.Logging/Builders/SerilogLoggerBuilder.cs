using BuildingBlocks.Core.Common;
using BuildingBlocks.Logging.Abstractions;
using BuildingBlocks.Logging.Core;
using BuildingBlocks.Logging.Enrichers;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

namespace BuildingBlocks.Logging.Builders;

/// <summary>
/// Serilog日志构建器默认实现
/// 【核心职责】基于Serilog框架实现ILoggerBuilder接口，提供标准化的日志系统构建能力
/// 【设计特点】
/// 1. 流畅链式API: 支持链式调用配置，开发体验友好
/// 2. 配置优先: 支持从配置对象加载所有参数，实现配置与代码分离
/// 3. 生产级优化: 默认启用异步写入、滚动日志、文件大小限制等生产级特性
/// 4. 内置企业级能力: 自动集成上下文丰富器、敏感数据过滤、错误日志分离
/// 5. 完全隔离: 封装Serilog原生API，业务层无需直接依赖Serilog
/// 【强制规范】所有日志系统构建必须通过此类完成，禁止直接使用Serilog.LoggerConfiguration
/// </summary>
public class SerilogLoggerBuilder : ILoggerBuilder
{
    /// <summary>
    /// Serilog原生日志配置对象
    /// </summary>
    private readonly LoggerConfiguration _loggerConfiguration;

    /// <summary>
    /// 已注册的日志丰富器列表
    /// </summary>
    private readonly List<ILogEnricher> _enrichers = [];

    /// <summary>
    /// 命名空间级别覆盖字典 (键: 命名空间前缀，值: 最小日志级别)
    /// </summary>
    private readonly Dictionary<string, LogEventLevel> _overrides = [];

    /// <summary>
    /// 全局最小日志级别
    /// </summary>
    private LogEventLevel _minimumLevel = LogEventLevel.Information;

    /// <summary>
    /// 敏感数据过滤器实例
    /// </summary>
    private ISensitiveDataFilter? _sensitiveDataFilter;

    /// <summary>
    /// 应用程序名称 (用于区分不同微服务)
    /// </summary>
    private string _applicationName = "DefaultApplication";

    /// <summary>
    /// 运行环境名称
    /// </summary>
    private string _environment = "Production";

    /// <summary>
    /// 初始化Serilog日志构建器
    /// </summary>
    public SerilogLoggerBuilder()
    {
        _loggerConfiguration = new LoggerConfiguration();
    }

    /// <summary>
    /// 从配置对象批量加载所有日志配置
    /// 【自动配置内容】
    /// 1. 应用名称和运行环境
    /// 2. 全局最小日志级别和命名空间覆盖
    /// 3. 自动注册默认丰富器(ContextEnricher、EnvironmentEnricher)
    /// 4. 根据配置启用/禁用控制台、调试、文件输出
    /// 5. 自动配置错误日志分离
    /// </summary>
    /// <param name="options">日志系统全局配置对象，不可为null</param>
    /// <returns>当前构建器实例，支持链式调用</returns>
    /// <exception cref="ArgumentNullException">当options为null时抛出</exception>
    public ILoggerBuilder ConfigureFromOptions(LoggingOptions options)
    {
        Guard.NotNull(options);

        _applicationName = options.ApplicationName;
        _environment = options.Environment;

        // 设置全局最小日志级别
        _minimumLevel = options.MinimumLevel;

        // 加载命名空间级别覆盖配置
        foreach (var overrideConfig in options.Overrides)
        {
            _overrides[overrideConfig.Key] = overrideConfig.Value;
        }

        // 自动注册默认核心丰富器
        AddEnricher(new ContextEnricher(options.Environment, options.ApplicationName));
        AddEnricher(new EnvironmentEnricher());

        // 根据配置启用输出渠道
        if (options.EnableConsole)
            AddConsole();

        if (options.EnableDebug)
            AddDebug();

        if (options.FileLogging.Enable)
        {
            AddFile(options.FileLogging.Path, options.FileLogging);

            // 启用错误日志分离时，单独输出Error及以上级别日志
            if (options.FileLogging.SeparateErrorLogs)
                AddErrorFile(options.FileLogging.ErrorPath, options.FileLogging);
        }

        return this;
    }

    /// <summary>
    /// 设置全局最小日志级别
    /// 【优先级】低于此级别的所有日志事件都会被丢弃，命名空间覆盖优先级高于此设置
    /// </summary>
    /// <param name="level">全局最小日志级别</param>
    /// <returns>当前构建器实例，支持链式调用</returns>
    public ILoggerBuilder MinimumLevel(LogEventLevel level)
    {
        _minimumLevel = level;
        return this;
    }

    /// <summary>
    /// 为指定命名空间覆盖最小日志级别
    /// 【使用场景】针对特定模块或第三方库调整日志详细程度，减少不必要的日志输出
    /// </summary>
    /// <param name="source">命名空间前缀 (如"Microsoft.AspNetCore")，不可为null或空白</param>
    /// <param name="level">该命名空间的最小日志级别</param>
    /// <returns>当前构建器实例，支持链式调用</returns>
    /// <exception cref="ArgumentException">当source为null或空白字符串时抛出</exception>
    public ILoggerBuilder MinimumLevelOverride(string source, LogEventLevel level)
    {
        Guard.NotNullOrWhiteSpace(source);
        _overrides[source] = level;
        return this;
    }

    /// <summary>
    /// 添加自定义日志丰富器
    /// 【核心作用】为所有日志事件添加上下文属性，增强日志的可追溯性
    /// 【注意】默认已自动添加ContextEnricher和EnvironmentEnricher，无需重复注册
    /// </summary>
    /// <param name="enricher">日志丰富器实例，不可为null</param>
    /// <returns>当前构建器实例，支持链式调用</returns>
    /// <exception cref="ArgumentNullException">当enricher为null时抛出</exception>
    public ILoggerBuilder AddEnricher(ILogEnricher enricher)
    {
        Guard.NotNull(enricher);
        _enrichers.Add(enricher);
        return this;
    }

    /// <summary>
    /// 添加控制台输出目标
    /// 【输出特性】
    /// 1. 使用AnsiConsoleTheme.Code主题，支持彩色输出
    /// 2. 采用全局统一的日志输出模板
    /// 3. 受全局最小日志级别控制
    /// 【适用场景】开发环境调试、容器化部署标准输出
    /// </summary>
    /// <returns>当前构建器实例，支持链式调用</returns>
    public ILoggerBuilder AddConsole()
    {
        _loggerConfiguration.WriteTo.Console(
            theme: AnsiConsoleTheme.Code,
            outputTemplate: LoggingConstants.DefaultOutputTemplate,
            restrictedToMinimumLevel: _minimumLevel);
        return this;
    }

    /// <summary>
    /// 添加调试输出目标
    /// 【输出特性】输出到Visual Studio调试窗口，采用全局统一模板
    /// 【注意事项】仅在开发环境生效，生产环境建议禁用以避免性能损耗
    /// </summary>
    /// <returns>当前构建器实例，支持链式调用</returns>
    public ILoggerBuilder AddDebug()
    {
        _loggerConfiguration.WriteTo.Debug(
            outputTemplate: LoggingConstants.DefaultOutputTemplate,
            restrictedToMinimumLevel: _minimumLevel);
        return this;
    }

    /// <summary>
    /// 添加文件输出目标
    /// 【生产级优化】
    /// 1. 默认启用异步写入，避免日志IO阻塞主线程
    /// 2. 支持按时间/大小自动滚动日志文件
    /// 3. 自动限制单个文件大小和保留文件数量
    /// 4. 使用UTF-8编码，支持中文
    /// 【输出内容】所有级别≥全局最小级别的日志
    /// </summary>
    /// <param name="path">日志文件路径，支持相对路径和绝对路径，不可为null或空白</param>
    /// <param name="options">文件日志配置选项，不可为null</param>
    /// <returns>当前构建器实例，支持链式调用</returns>
    /// <exception cref="ArgumentException">当path为null或空白时抛出</exception>
    /// <exception cref="ArgumentNullException">当options为null时抛出</exception>
    public ILoggerBuilder AddFile(string path, FileLoggingOptions options)
    {
        Guard.NotNullOrWhiteSpace(path);
        Guard.NotNull(options);

        // 使用Async包装器实现异步写入，提升性能
        _loggerConfiguration.WriteTo.Async(a =>
            a.File(
                path: path,
                rollingInterval: options.RollingInterval,
                fileSizeLimitBytes: options.FileSizeLimitBytes,
                retainedFileCountLimit: options.RetainedFileCountLimit,
                outputTemplate: LoggingConstants.DefaultOutputTemplate,
                restrictedToMinimumLevel: _minimumLevel,
                encoding: System.Text.Encoding.UTF8));

        return this;
    }

    /// <summary>
    /// 添加错误日志单独文件输出目标
    /// 【核心价值】将严重错误日志与普通日志分离，方便快速定位生产环境问题
    /// 【过滤规则】仅输出LogEventLevel.Error和LogEventLevel.Fatal级别的日志
    /// 【特性】与普通文件日志共享相同的滚动、大小限制和编码配置
    /// </summary>
    /// <param name="path">错误日志文件路径，不可为null或空白</param>
    /// <param name="options">文件日志配置选项，不可为null</param>
    /// <returns>当前构建器实例，支持链式调用</returns>
    /// <exception cref="ArgumentException">当path为null或空白时抛出</exception>
    /// <exception cref="ArgumentNullException">当options为null时抛出</exception>
    public ILoggerBuilder AddErrorFile(string path, FileLoggingOptions options)
    {
        Guard.NotNullOrWhiteSpace(path);
        Guard.NotNull(options);

        _loggerConfiguration.WriteTo.Async(a =>
            a.File(
                path: path,
                rollingInterval: options.RollingInterval,
                fileSizeLimitBytes: options.FileSizeLimitBytes,
                retainedFileCountLimit: options.RetainedFileCountLimit,
                outputTemplate: LoggingConstants.DefaultOutputTemplate,
                restrictedToMinimumLevel: LogEventLevel.Error,
                encoding: System.Text.Encoding.UTF8));

        return this;
    }

    /// <summary>
    /// 注册敏感数据过滤器
    /// 【核心作用】所有日志输出前自动扫描并脱敏敏感字段，防止隐私信息泄露
    /// 【强制要求】生产环境必须注册此过滤器，满足《个人信息保护法》等合规要求
    /// </summary>
    /// <param name="filter">敏感数据过滤器实例，不可为null</param>
    /// <returns>当前构建器实例，支持链式调用</returns>
    /// <exception cref="ArgumentNullException">当filter为null时抛出</exception>
    public ILoggerBuilder AddSensitiveDataFilter(ISensitiveDataFilter filter)
    {
        Guard.NotNull(filter);
        _sensitiveDataFilter = filter;
        return this;
    }

    /// <summary>
    /// 构建最终的Serilog.ILogger实例
    /// 【构建流程】
    /// 1. 应用全局最小日志级别
    /// 2. 应用所有命名空间级别覆盖
    /// 3. 启用日志上下文并注册所有丰富器
    /// 4. 注册敏感数据过滤器 (如果已配置)
    /// 5. 创建最终的ILogger实例
    /// 【注意事项】调用后所有配置将被冻结，无法再修改
    /// </summary>
    /// <returns>配置完成的Serilog.ILogger实例</returns>
    public Serilog.ILogger Build()
    {
        // 应用全局最小日志级别
        var config = _loggerConfiguration.MinimumLevel.Is(_minimumLevel);

        // 应用所有命名空间级别覆盖
        foreach (var overrideConfig in _overrides)
        {
            config.MinimumLevel.Override(overrideConfig.Key, overrideConfig.Value);
        }

        // 启用日志上下文并注册所有丰富器
        config.Enrich.FromLogContext();
        foreach (var enricher in _enrichers)
        {
            config.Enrich.With(enricher);
        }

        // 注册敏感数据过滤器 (如果已配置)
        if (_sensitiveDataFilter != null)
        {
            config.Filter.With(new SensitiveDataFilterSinkWrapper(_sensitiveDataFilter));
        }

        return config.CreateLogger();
    }
}

/// <summary>
/// 敏感数据过滤器Serilog Sink包装器
/// 【设计背景】Serilog没有提供专门的日志内容修改扩展点，因此使用ILogEventFilter接口实现
/// 【核心职责】在日志事件输出到Sink之前，自动扫描并脱敏所有敏感字段
/// 【重要说明】此类虽然实现了ILogEventFilter接口，但**永远不会过滤掉任何日志**，仅修改日志内容
/// 【工作原理】遍历日志事件的所有属性，识别敏感字段并替换为脱敏后的值，同时添加IsSensitive标记
/// </summary>
public class SensitiveDataFilterSinkWrapper : ILogEventFilter
{
    /// <summary>
    /// 敏感数据过滤器实例
    /// </summary>
    private readonly ISensitiveDataFilter _filter;

    /// <summary>
    /// 初始化敏感数据过滤器包装器
    /// </summary>
    /// <param name="filter">敏感数据过滤器实例，不可为null</param>
    /// <exception cref="ArgumentNullException">当filter为null时抛出</exception>
    public SensitiveDataFilterSinkWrapper(ISensitiveDataFilter filter)
    {
        Guard.NotNull(filter);
        _filter = filter;
    }

    /// <summary>
    /// 处理日志事件，执行敏感数据脱敏
    /// 【处理逻辑】
    /// 1. 遍历日志事件的所有属性
    /// 2. 检查属性名是否为敏感字段
    /// 3. 仅处理字符串类型的标量值 (避免处理复杂对象导致的性能问题)
    /// 4. 对敏感值进行脱敏处理
    /// 5. 更新日志属性并添加IsSensitive标记
    /// 【返回值】永远返回true，不会过滤任何日志
    /// </summary>
    /// <param name="logEvent">待处理的日志事件，不可为null</param>
    /// <returns>始终返回true，表示允许日志输出</returns>
    public bool IsEnabled(LogEvent logEvent)
    {
        // 遍历所有日志属性，检查并脱敏敏感数据
        foreach (var property in logEvent.Properties)
        {
            if (_filter.IsSensitiveProperty(property.Key) &&
                property.Value is ScalarValue scalarValue &&
                scalarValue.Value is string stringValue)
            {
                // 执行脱敏处理
                var maskedValue = _filter.MaskSensitiveData(property.Key, stringValue);
                // 更新为脱敏后的值
                logEvent.AddOrUpdateProperty(new LogEventProperty(property.Key, new ScalarValue(maskedValue)));
                // 添加敏感标记，方便后续日志过滤和统计
                logEvent.AddOrUpdateProperty(new LogEventProperty(LoggingConstants.PropertyNames.IsSensitive, new ScalarValue(true)));
            }
        }

        // 永远返回true，不过滤任何日志，仅修改内容
        return true;
    }
}
