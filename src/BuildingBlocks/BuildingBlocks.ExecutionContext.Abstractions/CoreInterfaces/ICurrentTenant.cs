using BuildingBlocks.ExecutionContext.Abstractions.ContextData;
using BuildingBlocks.ExecutionContext.Abstractions.NullObjects;

namespace BuildingBlocks.ExecutionContext.Abstractions.CoreInterfaces;

/// <summary>
/// 当前租户访问器抽象接口 (业务层可直接依赖)
/// </summary>
/// <remarks>
/// 【线程安全】: 默认承诺异步线程安全
/// 【职责】: 提供当前租户的只读访问、临时切换租户功能
/// 【默认实现规则】: IsAvailable = 非NullTenantId && IsEnabled && (ExpirationTime == null || ExpirationTime > DateTime.UtcNow)
/// </remarks>
public interface ICurrentTenant
{
    /// <summary>
    /// 当前租户是否可用
    /// </summary>
    /// <value>
    /// true表示租户ID非NullTenantId、租户已启用、未过期 (或永不过期)
    /// false表示其他情况
    /// </value>
    bool IsAvailable
    {
        get
        {
            var tenant = Tenant;
            if (tenant.TenantId == NullConstants.NullTenantId || !tenant.IsEnabled)
            {
                return false;
            }
            if (tenant.ExpirationTime.HasValue && tenant.ExpirationTime.Value <= DateTime.UtcNow)
            {
                return false;
            }
            return true;
        }
    }

    /// <summary>
    /// 当前租户信息 (永远不会为null，未初始化或未解析时返回NullTenantInfo.Instance)
    /// </summary>
    TenantInfo Tenant { get; }

    /// <summary>
    /// 当前租户ID (永远不会为null/空字符串/空白字符，未初始化时返回NullConstants.NullTenantId)
    /// </summary>
    string TenantId => Tenant.TenantId;

    /// <summary>
    /// 临时切换租户 (作用域生命周期结束后自动恢复原租户)
    /// </summary>
    /// <param name="tenantInfo">要切换到的租户信息 (不可为null)</param>
    /// <returns>可释放的作用域对象 (Dispose后自动恢复原租户)</returns>
    /// <exception cref="ArgumentNullException">当tenantInfo为null时抛出</exception>
    IDisposable ChangeTenant(TenantInfo tenantInfo);

    /// <summary>
    /// 临时切换租户 (简化版，仅需TenantId)
    /// </summary>
    /// <param name="tenantId">要切换到的租户ID (不可为null/空字符串/空白字符)</param>
    /// <returns>可释放的作用域对象 (Dispose后自动恢复原租户)</returns>
    /// <exception cref="ArgumentNullException">当tenantId为null/空字符串/空白字符时抛出</exception>
    IDisposable ChangeTenant(string tenantId);
}
