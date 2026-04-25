namespace BuildingBlocks.Monitoring.Configuration;

/// <summary>
/// 监控基础设施全局配置
/// </summary>
public class MonitoringOptions
{
    /// <summary>
    /// OpenTelemetry 配置
    /// </summary>
    public OpenTelemetryOptions OpenTelemetry { get; set; } = new();

    /// <summary>
    /// 健康检查配置
    /// </summary>
    public HealthCheckOptions HealthChecks { get; set; } = new();

    /// <summary>
    /// 告警配置
    /// </summary>
    public AlertOptions Alerting { get; set; } = new();
}
