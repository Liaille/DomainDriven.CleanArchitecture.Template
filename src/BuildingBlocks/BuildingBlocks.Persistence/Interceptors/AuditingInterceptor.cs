using BuildingBlocks.Core.Context;
using BuildingBlocks.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace BuildingBlocks.Persistence.Interceptors;

/// <summary>
/// EF Core 审计字段自动填充拦截器 (自动填充创建/修改人、时间)
/// </summary>
/// <param name="currentUser">当前用户上下文</param>
public class AuditingInterceptor(ICurrentUser currentUser) : SaveChangesInterceptor
{
    /// <summary>
    /// 同步保存时拦截
    /// </summary>
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        if (eventData.Context is not null) ApplyAuditProperties(eventData.Context);

        return base.SavingChanges(eventData, result);
    }

    /// <summary>
    /// 异步保存时拦截
    /// </summary>
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        if (eventData.Context is not null) ApplyAuditProperties(eventData.Context);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    /// <summary>
    /// 应用审计字段填充
    /// </summary>
    /// <param name="dbContext">数据库上下文</param>
    private void ApplyAuditProperties(DbContext dbContext)
    {
        var now = DateTime.UtcNow;
        var userId = currentUser.IsAuthenticated ? currentUser.Id : null;

        foreach (var entry in dbContext.ChangeTracker.Entries())
        {
            // 处理新增实体
            if (entry.State is EntityState.Added)
            {
                if (entry.Entity is IAuditableEntity auditable)
                {
                    auditable.CreationTime = now;
                    auditable.CreatorId = userId;
                }
            }

            // 处理修改实体
            if (entry.State is EntityState.Modified)
            {
                if (entry.Entity is IAuditableEntity auditable)
                {
                    auditable.LastModificationTime = now;
                    auditable.LastModifierId = userId;
                }
            }
        }
    }
}
