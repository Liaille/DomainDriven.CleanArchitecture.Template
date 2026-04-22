using BuildingBlocks.Core.Context;
using BuildingBlocks.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace BuildingBlocks.Persistence.Interceptors;

/// <summary>
/// 多租户拦截器 (自动填充租户 ID)
/// </summary>
public class MultiTenantInterceptor(ICurrentTenant currentTenant) : SaveChangesInterceptor
{
    /// <summary>
    /// 保存变更前同步拦截
    /// </summary>
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        if (eventData.Context is not null) SetTenantId(eventData.Context);

        return base.SavingChanges(eventData, result);
    }

    /// <summary>
    /// 保存变更前异步拦截
    /// </summary>
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        if (eventData.Context is not null) SetTenantId(eventData.Context);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    /// <summary>
    /// 设置租户 ID
    /// </summary>
    private void SetTenantId(DbContext dbContext)
    {
        if (!currentTenant.IsActive || currentTenant.Id is null) return;

        foreach (var entry in dbContext.ChangeTracker.Entries<IMultiTenantEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.TenantId = currentTenant.Id;
            }
        }
    }
}
