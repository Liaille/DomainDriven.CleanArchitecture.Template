using BuildingBlocks.Core.Context;
using BuildingBlocks.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace BuildingBlocks.Persistence.Interceptors;

/// <summary>
/// EF Core 软删除拦截器 (将删除操作转换为更新IsDeleted字段)
/// </summary>
public class SoftDeleteInterceptor : SaveChangesInterceptor
{
    /// <summary>
    /// 同步保存时拦截
    /// </summary>
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        if (eventData.Context is not null) ApplySoftDelete(eventData.Context);

        return base.SavingChanges(eventData, result);
    }

    /// <summary>
    /// 异步保存时拦截
    /// </summary>
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        if (eventData.Context is not null) ApplySoftDelete(eventData.Context);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    /// <summary>
    /// 应用软删除逻辑
    /// </summary>
    /// <param name="dbContext">数据库上下文</param>
    private void ApplySoftDelete(DbContext dbContext)
    {
        foreach (var entry in dbContext.ChangeTracker.Entries())
        {
            // 仅处理删除状态的完整审计实体
            if (entry.State is not EntityState.Deleted) continue;

            if (entry.Entity is FullAuditedEntity<object> fullAuditedEntity)
            {
                entry.State = EntityState.Modified;
                fullAuditedEntity.IsDeleted = true;
            }

            if (entry.Entity is FullAuditedAggregateRoot<object> fullAuditedAggregateRoot)
            {
                entry.State = EntityState.Modified;
                fullAuditedAggregateRoot.IsDeleted = true;
            }
        }
    }
}
