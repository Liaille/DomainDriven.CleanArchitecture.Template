using Template.Domain.Core.Abstractions;

namespace Template.Domain.Core.Repositories;

/// <summary>
/// 基础仓储 (增删改 + 只读)
/// </summary>
/// <typeparam name="TEntity">聚合根</typeparam>
/// <typeparam name="TKey">主键类型</typeparam>
public interface IBasicRepository<TEntity, TKey> : IReadOnlyRepository<TEntity, TKey>
    where TEntity : class, IAggregateRoot<TKey>
{
    /// <summary>
    /// 添加
    /// </summary>
    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// 更新
    /// </summary>
    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// 删除 (根据实体)
    /// </summary>
    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// 删除 (根据ID)
    /// </summary>
    Task DeleteAsync(TKey id, CancellationToken cancellationToken = default);
}
