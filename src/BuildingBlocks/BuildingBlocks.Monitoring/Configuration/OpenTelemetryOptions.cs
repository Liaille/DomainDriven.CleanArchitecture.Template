using System.ComponentModel.DataAnnotations;

namespace BuildingBlocks.Monitoring.Configuration;

/// <summary>
/// OpenTelemetry 配置
/// </summary>
public class OpenTelemetryOptions
{
    /// <summary>
    /// 是否启用 OpenTelemetry
    /// </summary>
    public bool Enabled { get; set; } = true;

    /// <summary>
    /// 是否启用 ASP.NET Core 链路追踪
    /// </summary>
    public bool EnableAspNetCoreInstrumentation { get; set; }

    /// <summary>
    /// 是否启用 HttpClient 链路追踪
    /// </summary>
    public bool EnableHttpClientInstrumentation { get; set; }

    /// <summary>
    /// 服务名称
    /// </summary>
    [Required(ErrorMessage = "Service name is required.")]
    public string ServiceName { get; set; } = "UnknownService";

    /// <summary>
    /// 服务版本
    /// </summary>
    public string ServiceVersion { get; set; } = "1.0.0";

    /// <summary>
    /// 环境名称
    /// </summary>
    public string Environment { get; set; } = "Development";

    /// <summary>
    /// OTLP 导出端点 (如 Jaeger、Grafana、OTel Collector)
    /// </summary>
    public string? OtlpEndpoint { get; set; }

    /// <summary>
    /// 是否导出到控制台 (开发环境用)
    /// </summary>
    public bool ExportToConsole { get; set; } = true;
}
