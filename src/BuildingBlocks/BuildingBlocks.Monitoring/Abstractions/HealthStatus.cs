namespace BuildingBlocks.Monitoring.Abstractions;

/// <summary>
/// 健康状态枚举
/// </summary>
public enum HealthStatus
{
    /// <summary>
    /// 健康
    /// </summary>
    Healthy = 0,

    /// <summary>
    /// 降级
    /// </summary>
    Degraded = 1,

    /// <summary>
    /// 不健康
    /// </summary>
    Unhealthy = 2
}
