namespace BuildingBlocks.Core.HealthChecks;

/// <summary>
/// 就绪探针接口 (适配 K8s Readiness Probe)
/// <para>核心职责: 检查所有外部依赖可用性，失败则从 Service 中剔除 Pod</para>
/// <para>强制约束: 必须包含所有外部依赖检查 (数据库、Redis、MQ、配置中心等)</para>
/// </summary>
public interface IReadinessHealthCheck
{
    /// <summary>
    /// 执行就绪探针检测
    /// </summary>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>健康检查结果</returns>
    Task<HealthCheckResult> CheckAsync(CancellationToken cancellationToken = default);
}
