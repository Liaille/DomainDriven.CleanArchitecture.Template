using BuildingBlocks.Core.Context;
using BuildingBlocks.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace BuildingBlocks.Persistence.Interceptors;

/// <summary>
/// EF Core 软删除拦截器 (将删除操作转换为更新 IsDeleted 字段，并自动填充删除人、删除时间)
/// </summary>
/// <param name="currentUser">当前用户上下文</param>
public class SoftDeleteInterceptor(ICurrentUser currentUser) : SaveChangesInterceptor
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
        var now = DateTime.UtcNow;
        var userId = currentUser.IsAuthenticated ? currentUser.Id : null;

        foreach (var entry in dbContext.ChangeTracker.Entries())
        {
            // 仅处理删除状态且实现了软删除接口的实体
            if (entry.State is not EntityState.Deleted) continue;
            if (entry.Entity is not ISoftDeletableEntity softDeletable) continue;

            // 将物理删除转换为逻辑删除（更新状态）
            entry.State = EntityState.Modified;
            softDeletable.IsDeleted = true;

            // 填充审计字段：删除时间、删除人
            if (entry.Entity is IFullAuditableEntity fullAuditable)
            {
                fullAuditable.DeletionTime = now;
                fullAuditable.DeleterId = userId;
            }
        }
    }
}
