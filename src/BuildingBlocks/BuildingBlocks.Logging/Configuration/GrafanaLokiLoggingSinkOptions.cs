using Serilog.Sinks.Grafana.Loki;

namespace BuildingBlocks.Logging.Configuration;

/// <summary>
/// Loki 输出配置
/// </summary>
public class GrafanaLokiLoggingSinkOptions
{
    /// <summary>
    /// Loki 服务地址
    /// </summary>
    public string Url { get; set; } = string.Empty;

    /// <summary>
    /// 应用名称（用于Loki标签过滤）
    /// </summary>
    public string ApplicationName { get; set; } = "Unknown";

    /// <summary>
    /// 批量发送最大条目数（默认：1000）
    /// </summary>
    public int? BatchPostingLimit { get; set; }

    /// <summary>
    /// 内存队列最大上限（默认：无限）
    /// </summary>
    public int? QueueLimit { get; set; }

    /// <summary>
    /// 批量发送间隔（秒，默认：2秒）
    /// </summary>
    public int? Period { get; set; }

    /// <summary>
    /// Loki 认证配置
    /// </summary>
    public LokiCredentials? Credentials { get; set; }

    /// <summary>
    /// Tenant ID（多租户场景使用）
    /// </summary>
    public string? Tenant { get; set; }
}
