namespace BuildingBlocks.Logging.Configuration;

/// <summary>
/// 文件输出配置
/// </summary>
public class FileLoggingSinkOptions
{
    /// <summary>
    /// 日志文件路径
    /// </summary>
    public string Path { get; set; } = "logs/app-log-.log";

    /// <summary>
    /// 文件保留天数
    /// </summary>
    public int RetainDays { get; set; } = 30;
}
