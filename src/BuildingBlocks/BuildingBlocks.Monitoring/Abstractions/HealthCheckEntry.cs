namespace BuildingBlocks.Monitoring.Abstractions;

/// <summary>
/// 健康检查条目
/// </summary>
public record HealthCheckEntry
{
    /// <summary>
    /// 健康状态
    /// </summary>
    public HealthStatus Status { get; init; }

    /// <summary>
    /// 描述
    /// </summary>
    public string? Description { get; init; }

    /// <summary>
    /// 持续时间
    /// </summary>
    public TimeSpan Duration { get; init; }

    /// <summary>
    /// 异常信息
    /// </summary>
    public Exception? Exception { get; init; }

    /// <summary>
    /// 附加数据
    /// </summary>
    public Dictionary<string, object?>? Data { get; init; }
}
