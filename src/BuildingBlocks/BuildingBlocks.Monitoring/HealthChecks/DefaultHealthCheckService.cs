using BuildingBlocks.Core.Common;
using BuildingBlocks.Monitoring.Abstractions;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using HealthStatus = Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus;

namespace BuildingBlocks.Monitoring.HealthChecks;

public class DefaultHealthCheckService(HealthCheckService healthCheckService) : IHealthCheckService
{
    /// <summary>
    /// 执行存活检查 (仅检查应用自身状态)
    /// </summary>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>健康检查结果</returns>
    public async Task<Abstractions.HealthCheckResult> CheckLivenessAsync(CancellationToken cancellationToken = default)
    {
        var startTime = DateTime.UtcNow;
        try
        {
            // 仅检查 "liveness" 标签的健康检查项
            var result = await healthCheckService.CheckHealthAsync(context => context.Tags.Contains("liveness"), cancellationToken);

            return ConvertToHealthCheckResult(result, DateTime.UtcNow - startTime);
        }
        catch (Exception ex)
        {
            return CreateUnhealthyResult("Liveness check failed with exception.", DateTime.UtcNow - startTime, ex);
        }
    }

    /// <summary>
    /// 执行就绪检查 (检查所有外部依赖可用性)
    /// </summary>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>健康检查结果</returns>
    public async Task<Abstractions.HealthCheckResult> CheckReadinessAsync(CancellationToken cancellationToken = default)
    {
        var startTime = DateTime.UtcNow;
        try
        {
            // 检查所有健康检查项
            var result = await healthCheckService.CheckHealthAsync(cancellationToken);

            return ConvertToHealthCheckResult(result, DateTime.UtcNow - startTime);
        }
        catch (Exception ex)
        {
            return CreateUnhealthyResult("Readiness check failed with exception.", DateTime.UtcNow - startTime, ex);
        }
    }

    /// <summary>
    /// 执行自定义健康检查
    /// </summary>
    /// <param name="checkName">检查名称</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>健康检查结果</returns>
    public async Task<Abstractions.HealthCheckResult> CheckCustomAsync(string checkName, CancellationToken cancellationToken = default)
    {
        Guard.NotNullOrEmpty(checkName);

        var startTime = DateTime.UtcNow;
        try
        {
            // 仅检查指定名称的健康检查项
            var result = await healthCheckService.CheckHealthAsync(
                context => context.Name == checkName,
                cancellationToken);

            return ConvertToHealthCheckResult(result, DateTime.UtcNow - startTime);
        }
        catch (Exception ex)
        {
            return CreateUnhealthyResult($"Custom check '{checkName}' failed with exception.", DateTime.UtcNow - startTime, ex);
        }
    }

    /// <summary>
    /// 将 Microsoft.Extensions.Diagnostics.HealthChecks.HealthReport 转换为自定义模型
    /// </summary>
    /// <param name="report">健康报告</param>
    /// <param name="duration">检查持续时间</param>
    /// <returns>自定义健康检查结果</returns>
    private static Abstractions.HealthCheckResult ConvertToHealthCheckResult(HealthReport report, TimeSpan duration)
    {
        var status = report.Status switch
        {
            HealthStatus.Healthy => Abstractions.HealthStatus.Healthy,
            HealthStatus.Degraded => Abstractions.HealthStatus.Degraded,
            _ => Abstractions.HealthStatus.Unhealthy
        };

        var entries = report.Entries.ToDictionary(
            kvp => kvp.Key,
            kvp => new HealthCheckEntry
            {
                Status = kvp.Value.Status switch
                {
                    HealthStatus.Healthy => Abstractions.HealthStatus.Healthy,
                    HealthStatus.Degraded => Abstractions.HealthStatus.Degraded,
                    _ => Abstractions.HealthStatus.Unhealthy
                },
                Description = kvp.Value.Description,
                Duration = kvp.Value.Duration,
                Exception = kvp.Value.Exception,
                Data = kvp.Value.Data.ToDictionary(d => d.Key, d => (object?)d.Value)
            });

        return new Abstractions.HealthCheckResult
        {
            Status = status,
            Description = status == Abstractions.HealthStatus.Healthy ? "All checks passed." : "Some checks failed.",
            Duration = duration,
            Entries = entries
        };
    }

    private static Abstractions.HealthCheckResult CreateUnhealthyResult(string description, TimeSpan duration, Exception exception)
    {
        return new Abstractions.HealthCheckResult
        {
            Status = Abstractions.HealthStatus.Unhealthy,
            Description = description,
            Duration = duration,
            Exception = exception
        };
    }
}
