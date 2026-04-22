using BuildingBlocks.Core.Entities;
using BuildingBlocks.Core.Repositories;
using BuildingBlocks.Core.SpecificationPattern;
using Microsoft.EntityFrameworkCore;

namespace BuildingBlocks.Persistence.Repository;

/// <summary>
/// EF Core 只读仓储实现 (CQRS 查询端专用，仅提供查询能力)
/// </summary>
/// <typeparam name="TEntity">聚合根类型</typeparam>
/// <typeparam name="TKey">主键类型</typeparam>
/// <typeparam name="TDbContext">数据库上下文类型</typeparam>
public class EfCoreReadOnlyRepository<TEntity, TKey, TDbContext>(
    TDbContext dbContext,
    ISpecificationEvaluator specificationEvaluator)
    : IReadOnlyRepository<TEntity, TKey>
    where TEntity : class, IAggregateRoot<TKey>
    where TDbContext : DbContext
{
    protected readonly TDbContext DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    protected readonly DbSet<TEntity> DbSet = dbContext.Set<TEntity>();
    protected readonly ISpecificationEvaluator SpecificationEvaluator = specificationEvaluator ?? throw new ArgumentNullException(nameof(specificationEvaluator));

    /// <summary>
    /// 根据 ID 查询实体
    /// </summary>
    /// <param name="id">实体主键</param>
    /// <param name="cancellationToken">取消令牌</param>
    public virtual async Task<TEntity?> GetByIdAsync(TKey id, CancellationToken cancellationToken = default)
    {
        return await DbSet.FindAsync([id!], cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// 根据规约查询单个实体
    /// </summary>
    /// <param name="specification">查询规约</param>
    /// <param name="cancellationToken">取消令牌</param>
    public virtual async Task<TEntity?> GetAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(specification, nameof(specification));
        return await ApplySpecification(specification).AsNoTracking().FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// 根据规约查询实体列表
    /// </summary>
    /// <param name="specification">查询规约</param>
    /// <param name="cancellationToken">取消令牌</param>
    public virtual async Task<List<TEntity>> GetListAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(specification, nameof(specification));
        return await ApplySpecification(specification).AsNoTracking().ToListAsync(cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// 判断是否存在满足规约的实体
    /// </summary>
    /// <param name="specification">查询规约</param>
    /// <param name="cancellationToken">取消令牌</param>
    public virtual async Task<bool> AnyAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(specification, nameof(specification));
        return await ApplySpecification(specification).AsNoTracking().AnyAsync(cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// 统计满足规约的实体数量
    /// </summary>
    /// <param name="specification">查询规约</param>
    /// <param name="cancellationToken">取消令牌</param>
    public virtual async Task<int> CountAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(specification, nameof(specification));
        return await ApplySpecification(specification).AsNoTracking().CountAsync(cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// 应用规约到查询对象
    /// </summary>
    /// <param name="specification">查询规约</param>
    protected virtual IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> specification)
    {
        return SpecificationEvaluator.GetQuery(DbSet.AsQueryable(), specification);
    }
}
