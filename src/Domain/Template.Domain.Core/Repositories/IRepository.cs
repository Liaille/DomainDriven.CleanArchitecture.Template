using Template.Domain.Core.Abstractions;

namespace Template.Domain.Core.Repositories;

/// <summary>
/// 仓储接口
/// </summary>
/// <typeparam name="TEntity">聚合根</typeparam>
/// <typeparam name="TKey">主键类型</typeparam>
public interface IRepository<TEntity, TKey> : IBasicRepository<TEntity, TKey>
    where TEntity : class, IAggregateRoot<TKey>
{
}
