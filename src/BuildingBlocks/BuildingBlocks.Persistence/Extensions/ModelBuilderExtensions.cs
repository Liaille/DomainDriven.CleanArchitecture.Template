using BuildingBlocks.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BuildingBlocks.Persistence.Extensions;

/// <summary>
/// EF Core ModelBuilder 扩展方法
/// </summary>
public static class ModelBuilderExtensions
{
    /// <summary>
    /// 全局配置软删除查询过滤器
    /// </summary>
    /// <param name="modelBuilder">模型构建器</param>
    public static void ApplySoftDeleteFilter(this ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            // 仅对完整审计实体应用软删除过滤器
            if (typeof(FullAuditedEntity<object>).IsAssignableFrom(entityType.ClrType)
                || typeof(FullAuditedAggregateRoot<object>).IsAssignableFrom(entityType.ClrType))
            {
                var parameter = Expression.Parameter(entityType.ClrType, "e");
                var property = Expression.Property(parameter, nameof(FullAuditedEntity<>.IsDeleted));
                var condition = Expression.Equal(property, Expression.Constant(false));
                var lambda = Expression.Lambda(condition, parameter);

                modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
            }
        }
    }
}
