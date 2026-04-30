namespace BuildingBlocks.Core.HealthChecks;

/// <summary>
/// 启动探针接口 (适配 K8s Startup Probe)
/// <para>核心职责: 仅在应用初始化阶段执行，检测核心依赖可用性，启动成功后停止检测</para>
/// <para>强制约束: 仅在应用启动时执行一次，成功后不再调用</para>
/// </summary>
public interface IStartupHealthCheck
{
    /// <summary>
    /// 执行启动探针检测
    /// </summary>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>健康检查结果</returns>
    Task<HealthCheckResult> CheckAsync(CancellationToken cancellationToken = default);
}
