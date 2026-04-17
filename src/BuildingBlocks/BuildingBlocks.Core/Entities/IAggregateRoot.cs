namespace BuildingBlocks.Core.Entities;

/// <summary>
/// 聚合根标记接口
/// </summary>
public interface IAggregateRoot
{
}

/// <summary>
/// 泛型聚合根接口
/// </summary>
/// <typeparam name="TKey">聚合根主键类型</typeparam>
public interface IAggregateRoot<out TKey> : IEntity<TKey>, IAggregateRoot
{
}
