using BuildingBlocks.Core.Entities;
using BuildingBlocks.Core.Repositories;
using BuildingBlocks.Core.SpecificationPattern;
using Microsoft.EntityFrameworkCore;

namespace BuildingBlocks.Persistence.Repository;

/// <summary>
/// EF Core 基础写仓储实现 (CQRS 命令端专用，仅提供增删改能力)
/// </summary>
/// <typeparam name="TEntity">聚合根类型</typeparam>
/// <typeparam name="TKey">主键类型</typeparam>
/// <typeparam name="TDbContext">数据库上下文类型</typeparam>
public class EfCoreBasicRepository<TEntity, TKey, TDbContext>(
    TDbContext dbContext,
    ISpecificationEvaluator specificationEvaluator)
    : EfCoreReadOnlyRepository<TEntity, TKey, TDbContext>(dbContext, specificationEvaluator), IBasicRepository<TEntity, TKey>
    where TEntity : class, IAggregateRoot<TKey>
    where TDbContext : DbContext
{
    /// <summary>
    /// 添加实体到数据库
    /// </summary>
    /// <param name="entity">聚合根实体</param>
    /// <param name="cancellationToken">取消令牌</param>
    public virtual async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));
        await DbSet.AddAsync(entity, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// 更新实体到数据库
    /// </summary>
    /// <param name="entity">聚合根实体</param>
    /// <param name="cancellationToken">取消令牌</param>
    public virtual Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));
        DbSet.Update(entity);
        return Task.CompletedTask;
    }

    /// <summary>
    /// 根据实体对象删除数据
    /// </summary>
    /// <param name="entity">聚合根实体</param>
    /// <param name="cancellationToken">取消令牌</param>
    public virtual Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));
        DbSet.Remove(entity);
        return Task.CompletedTask;
    }

    /// <summary>
    /// 根据主键 ID 删除数据
    /// </summary>
    /// <param name="id">实体主键</param>
    /// <param name="cancellationToken">取消令牌</param>
    public virtual async Task DeleteAsync(TKey id, CancellationToken cancellationToken = default)
    {
        var entity = await GetByIdAsync(id, cancellationToken).ConfigureAwait(false);
        if (entity is not null) DbSet.Remove(entity);
    }
}
