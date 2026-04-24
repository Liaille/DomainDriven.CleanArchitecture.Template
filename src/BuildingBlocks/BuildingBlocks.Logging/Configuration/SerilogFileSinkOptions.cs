using System.ComponentModel.DataAnnotations;

namespace BuildingBlocks.Logging.Configuration;

/// <summary>
/// 文件输出配置
/// </summary>
public class SerilogFileSinkOptions
{
    /// <summary>
    /// 日志文件路径
    /// </summary>
    [Required(ErrorMessage = "日志文件路径不能为空")]
    public string Path { get; set; } = "logs/app-log-.log";

    /// <summary>
    /// 文件保留天数
    /// </summary>
    [Range(1, 365, ErrorMessage = "文件保留天数必须在1-365之间")]
    public int RetainDays { get; set; } = 30;
}
