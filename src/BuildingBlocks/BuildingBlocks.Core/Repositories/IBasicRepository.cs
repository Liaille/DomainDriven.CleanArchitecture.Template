using BuildingBlocks.Core.Entities;

namespace BuildingBlocks.Core.Repositories;

/// <summary>
/// 基础写仓储 (CQRS 命令专用)
/// </summary>
/// <typeparam name="TEntity">聚合根类型</typeparam>
/// <typeparam name="TKey">主键类型</typeparam>
public interface IBasicRepository<TEntity, TKey> : IReadOnlyRepository<TEntity, TKey>
    where TEntity : class, IAggregateRoot<TKey>
{
    /// <summary>
    /// 添加实体
    /// </summary>
    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// 更新实体
    /// </summary>
    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// 删除实体 (根据实体对象)
    /// </summary>
    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// 删除实体 (根据主键 ID)
    /// </summary>
    Task DeleteAsync(TKey id, CancellationToken cancellationToken = default);
}
