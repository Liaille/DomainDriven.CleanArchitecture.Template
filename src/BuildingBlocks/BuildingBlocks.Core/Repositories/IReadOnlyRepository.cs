using BuildingBlocks.Core.Entities;
using BuildingBlocks.Core.SpecificationPattern;

namespace BuildingBlocks.Core.Repositories;

/// <summary>
/// 只读仓储接口 (CQRS 查询专用，支持规约模式)
/// </summary>
/// <typeparam name="TEntity">聚合根类型</typeparam>
/// <typeparam name="TKey">主键类型</typeparam>
public interface IReadOnlyRepository<TEntity, TKey> where TEntity : class, IAggregateRoot<TKey>
{
    /// <summary>
    /// 根据 ID 查询实体
    /// </summary>
    Task<TEntity?> GetByIdAsync(TKey id, CancellationToken cancellationToken = default);

    /// <summary>
    /// 根据规约查询单个实体
    /// </summary>
    Task<TEntity?> GetAsync(IQuerySpecification<TEntity> specification, CancellationToken cancellationToken = default);

    /// <summary>
    /// 根据规约查询实体列表
    /// </summary>
    Task<List<TEntity>> GetListAsync(IQuerySpecification<TEntity> specification, CancellationToken cancellationToken = default);

    /// <summary>
    /// 判断是否存在满足规约的实体
    /// </summary>
    Task<bool> AnyAsync(IQuerySpecification<TEntity> specification, CancellationToken cancellationToken = default);

    /// <summary>
    /// 统计满足规约的实体数量
    /// </summary>
    Task<int> CountAsync(IQuerySpecification<TEntity> specification, CancellationToken cancellationToken = default);
}
