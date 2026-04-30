namespace BuildingBlocks.Core.HealthChecks;

/// <summary>
/// 存活探针接口 (适配 K8s Liveness Probe)
/// <para>核心职责: 仅检查应用自身进程状态，严禁包含任何外部依赖检查</para>
/// <para>强制约束: 失败则重启 Pod，必须轻量、无外部 IO</para>
/// </summary>
public interface ILivenessHealthCheck
{
    /// <summary>
    /// 执行存活探针检测
    /// </summary>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>健康检查结果</returns>
    Task<HealthCheckResult> CheckAsync(CancellationToken cancellationToken = default);
}
