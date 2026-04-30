namespace BuildingBlocks.Core.HealthChecks;

/// <summary>
/// 健康检查结果
/// </summary>
public record HealthCheckResult
{
    /// <summary>
    /// 健康状态
    /// </summary>
    public HealthStatus Status { get; init; }

    /// <summary>
    /// 检查描述信息
    /// </summary>
    public string? Description { get; init; }

    /// <summary>
    /// 异常信息 (仅在失败时填充)
    /// </summary>
    public Exception? Exception { get; init; }

    /// <summary>
    /// 检查耗时 (毫秒)
    /// </summary>
    public long DurationMs { get; init; }

    /// <summary>
    /// 私有构造函数，强制通过静态方法创建
    /// </summary>
    private HealthCheckResult()
    {
    }

    /// <summary>
    /// 创建健康状态结果
    /// </summary>
    /// <param name="description">描述信息</param>
    /// <param name="durationMs">耗时毫秒</param>
    public static HealthCheckResult Healthy(string? description = null, long durationMs = 0)
    {
        return new HealthCheckResult
        {
            Status = HealthStatus.Healthy,
            Description = description,
            DurationMs = durationMs
        };
    }

    /// <summary>
    /// 创建降级状态结果
    /// </summary>
    /// <param name="description">描述信息</param>
    /// <param name="durationMs">耗时毫秒</param>
    public static HealthCheckResult Degraded(string? description = null, long durationMs = 0)
    {
        return new HealthCheckResult
        {
            Status = HealthStatus.Degraded,
            Description = description,
            DurationMs = durationMs
        };
    }

    /// <summary>
    /// 创建不健康状态结果
    /// </summary>
    /// <param name="description">描述信息</param>
    /// <param name="exception">异常对象</param>
    /// <param name="durationMs">耗时毫秒</param>
    public static HealthCheckResult Unhealthy(string? description = null, Exception? exception = null, long durationMs = 0)
    {
        return new HealthCheckResult
        {
            Status = HealthStatus.Unhealthy,
            Description = description,
            Exception = exception,
            DurationMs = durationMs
        };
    }
}
