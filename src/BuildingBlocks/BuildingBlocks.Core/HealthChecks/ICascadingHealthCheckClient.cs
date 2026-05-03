namespace BuildingBlocks.Core.HealthChecks;

/// <summary>
/// 跨服务级联健康检查客户端接口
/// <para>核心职责: 检查依赖服务的健康状态，用于就绪探针级联检查</para>
/// </summary>
public interface ICascadingHealthCheckClient
{
    /// <summary>
    /// 检查目标服务的健康状态
    /// </summary>
    /// <param name="serviceName">目标服务名称</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>目标服务的健康检查结果</returns>
    Task<HealthCheckResult> CheckServiceHealthAsync(string serviceName, CancellationToken cancellationToken = default);
}
