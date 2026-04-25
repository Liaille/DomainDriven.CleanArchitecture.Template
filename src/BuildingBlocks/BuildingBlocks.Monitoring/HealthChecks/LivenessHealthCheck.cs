using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace BuildingBlocks.Monitoring.HealthChecks;

/// <summary>
/// 自定义存活健康检查
/// </summary>
internal class LivenessHealthCheck : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(HealthCheckResult.Healthy("Application is alive."));
    }
}
