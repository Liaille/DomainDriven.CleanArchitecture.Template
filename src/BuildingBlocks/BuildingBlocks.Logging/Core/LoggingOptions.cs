using Serilog.Events;

namespace BuildingBlocks.Logging.Core;

/// <summary>
/// 日志配置选项
/// </summary>
public class LoggingOptions
{
    /// <summary>
    /// 配置节名称
    /// </summary>
    public const string ConfigurationSectionName = "Logging";

    /// <summary>
    /// 应用程序名称
    /// </summary>
    public string ApplicationName { get; set; } = "DefaultApplication";

    /// <summary>
    /// 环境名称
    /// </summary>
    public string Environment { get; set; } = "Production";

    /// <summary>
    /// 默认日志级别
    /// </summary>
    public LogEventLevel MinimumLevel { get; set; } = LogEventLevel.Information;

    /// <summary>
    /// 是否启用控制台输出
    /// </summary>
    public bool EnableConsole { get; set; } = true;

    /// <summary>
    /// 是否启用Debug输出
    /// </summary>
    public bool EnableDebug { get; set; } = false;

    /// <summary>
    /// 文件日志配置
    /// </summary>
    public FileLoggingOptions FileLogging { get; set; } = new();

    /// <summary>
    /// 敏感数据过滤配置
    /// </summary>
    public SensitiveDataOptions SensitiveData { get; set; } = new();

    /// <summary>
    /// 日志级别覆盖配置
    /// </summary>
    public Dictionary<string, LogEventLevel> Overrides { get; set; } = [];
}

/// <summary>
/// 文件日志配置选项
/// </summary>
public class FileLoggingOptions
{
    /// <summary>
    /// 是否启用文件输出
    /// </summary>
    public bool Enable { get; set; } = true;

    /// <summary>
    /// 日志文件路径
    /// </summary>
    public string Path { get; set; } = "logs/log-.txt";

    /// <summary>
    /// 滚动间隔
    /// </summary>
    public Serilog.RollingInterval RollingInterval { get; set; } = Serilog.RollingInterval.Day;

    /// <summary>
    /// 单个文件最大大小 (字节)
    /// </summary>
    public long FileSizeLimitBytes { get; set; } = 100 * 1024 * 1024; // 100MB

    /// <summary>
    /// 保留文件个数
    /// </summary>
    public int RetainedFileCountLimit { get; set; } = 31;

    /// <summary>
    /// 是否输出到单独的错误日志文件
    /// </summary>
    public bool SeparateErrorLogs { get; set; } = true;

    /// <summary>
    /// 错误日志文件路径
    /// </summary>
    public string ErrorPath { get; set; } = "logs/error-.txt";
}

/// <summary>
/// 敏感数据过滤配置选项
/// </summary>
public class SensitiveDataOptions
{
    /// <summary>
    /// 是否启用敏感数据过滤
    /// </summary>
    public bool EnableFilter { get; set; } = true;

    /// <summary>
    /// 自定义敏感属性名
    /// </summary>
    public string[] AdditionalSensitiveProperties { get; set; } = [];

    /// <summary>
    /// 过滤后替换文本
    /// </summary>
    public string MaskText { get; set; } = "***MASKED***";

    /// <summary>
    /// 部分屏蔽的字符数 (保留前X后X位)
    /// </summary>
    public int PartialMaskKeepChars { get; set; } = 4;
}
