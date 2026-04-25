namespace BuildingBlocks.Monitoring.Abstractions;

/// <summary>
/// 健康检查服务抽象接口
/// <para>用于检查应用自身及外部依赖的健康状态</para>
/// </summary>
public interface IHealthCheckService
{
    /// <summary>
    /// 执行存活检查 (仅检查应用自身状态)
    /// </summary>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>健康检查结果</returns>
    Task<HealthCheckResult> CheckLivenessAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// 执行就绪检查 (检查所有外部依赖可用性)
    /// </summary>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>健康检查结果</returns>
    Task<HealthCheckResult> CheckReadinessAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// 执行自定义健康检查
    /// </summary>
    /// <param name="checkName">检查名称</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>健康检查结果</returns>
    Task<HealthCheckResult> CheckCustomAsync(string checkName, CancellationToken cancellationToken = default);
}
