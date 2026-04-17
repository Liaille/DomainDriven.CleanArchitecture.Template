using BuildingBlocks.Core.Entities;

namespace BuildingBlocks.Core.Repositories;

/// <summary>
/// 完整仓储接口 (包含读写操作，兼容传统 CRUD)
/// </summary>
/// <typeparam name="TEntity">聚合根类型</typeparam>
/// <typeparam name="TKey">主键类型</typeparam>
public interface IRepository<TEntity, TKey> : IBasicRepository<TEntity, TKey>
    where TEntity : class, IAggregateRoot<TKey>
{
}
