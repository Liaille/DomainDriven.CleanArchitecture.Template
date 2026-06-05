using BuildingBlocks.ExecutionContext.Abstractions.ContextData;
using BuildingBlocks.ExecutionContext.Abstractions.CoreInterfaces;

namespace BuildingBlocks.ExecutionContext.Abstractions.NullObjects;

/// <summary>
/// 空当前租户访问器单例 (纯只读、不执行任何业务逻辑、无副作用)
/// </summary>
public sealed class NullCurrentTenant : ICurrentTenant
{
    public static NullCurrentTenant Instance { get; } = new();
    private NullCurrentTenant() { }
    public bool IsAvailable => false;
    public TenantInfo Tenant => NullTenantInfo.Instance;
    public string TenantId => Tenant.TenantId;
    public IDisposable ChangeTenant(TenantInfo tenantInfo) => NullDisposable.Instance;
    public IDisposable ChangeTenant(string tenantId) => NullDisposable.Instance;
}
