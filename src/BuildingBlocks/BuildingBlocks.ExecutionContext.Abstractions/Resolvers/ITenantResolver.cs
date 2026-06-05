using BuildingBlocks.ExecutionContext.Abstractions.ContextData;

namespace BuildingBlocks.ExecutionContext.Abstractions.Resolvers;

/// <summary>
/// 租户解析器抽象接口 (业务层不直接依赖，由实现层通过DI注入给ICurrentTenant)
/// </summary>
/// <remarks>
/// 【解析源优先级可配】: 实现层可配置解析源优先级 (如Header > 域名 > Claim > Cookie > 默认租户)
/// 【隔离模式可配】: 实现层可适配共享库共享表、共享库独立Schema、独立库三种隔离模式
/// </remarks>
public interface ITenantResolver
{
    /// <summary>
    /// 解析当前租户信息 (必须返回非null值)
    /// </summary>
    /// <param name="cancellationToken">用于取消操作的取消令牌 (由调用方传递)</param>
    /// <returns>解析到的租户信息 (永远不会为null，解析失败时抛出ContextResolutionException或返回默认租户)</returns>
    /// <exception cref="ContextResolutionException">当解析失败且无默认租户配置时抛出</exception>
    Task<TenantInfo> ResolveAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// 尝试解析当前租户信息 (不抛出任何异常)
    /// </summary>
    /// <param name="cancellationToken">用于取消操作的取消令牌 (由调用方传递)</param>
    /// <returns>解析到的租户信息 (解析失败时返回null)</returns>
    Task<TenantInfo?> TryResolveAsync(CancellationToken cancellationToken = default);
}
