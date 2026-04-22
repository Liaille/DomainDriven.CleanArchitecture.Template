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
    /// 全局配置软删除过滤
    /// </summary>
    /// <param name="modelBuilder">模型构建器</param>
    public static ModelBuilder ApplySoftDeleteFilter(this ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(ISoftDeletableEntity).IsAssignableFrom(entityType.ClrType))
            {
                var parameter = Expression.Parameter(entityType.ClrType, "e");
                var property = Expression.Property(parameter, nameof(ISoftDeletableEntity.IsDeleted));
                var condition = Expression.Equal(property, Expression.Constant(false));
                var lambda = Expression.Lambda(condition, parameter);

                entityType.SetQueryFilter(lambda);
            }
        }

        return modelBuilder;
    }

    /// <summary>
    /// 全局配置多租户标记
    /// </summary>
    /// <param name="modelBuilder">模型构建器</param>
    public static ModelBuilder ApplyMultiTenantFilter(this ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(IMultiTenantEntity).IsAssignableFrom(entityType.ClrType))
            {
                // 多租户过滤在拦截器中动态注入，此处仅标记实体支持多租户
                entityType.AddAnnotation("IsMultiTenantEntity", true);
            }
        }

        return modelBuilder;
    }
}
