using Template.Domain.Core.Abstractions;

namespace Template.Domain.Core.Repositories;

/// <summary>
/// 只读仓储
/// </summary>
/// <typeparam name="TEntity">聚合根</typeparam>
/// <typeparam name="TKey">主键类型</typeparam>
public interface IReadOnlyRepository<TEntity, TKey>
    where TEntity : class, IAggregateRoot<TKey>
{
    /// <summary>
    /// 根据ID查询
    /// </summary>
    Task<TEntity?> GetByIdAsync(
        TKey id,
        CancellationToken cancellationToken = default);
}