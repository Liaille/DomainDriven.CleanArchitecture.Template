namespace BuildingBlocks.Monitoring.Abstractions;

/// <summary>
/// 健康检查结果模型
/// </summary>
public record HealthCheckResult
{
    /// <summary>
    /// 健康状态
    /// </summary>
    public HealthStatus Status { get; init; } = HealthStatus.Unhealthy;

    /// <summary>
    /// 检查描述
    /// </summary>
    public string? Description { get; init; }

    /// <summary>
    /// 检查持续时间
    /// </summary>
    public TimeSpan Duration { get; init; }

    /// <summary>
    /// 详细检查结果 (键为检查项名称)
    /// </summary>
    public Dictionary<string, HealthCheckEntry>? Entries { get; init; }

    /// <summary>
    /// 异常信息 (如有)
    /// </summary>
    public Exception? Exception { get; init; }
}
