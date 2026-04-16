namespace Template.Domain.Core.Abstractions;

/// <summary>
/// 聚合根接口
/// </summary>
public interface IAggregateRoot : IEntity, IGeneratesDomainEvents
{
}

/// <summary>
/// 泛型主键聚合根接口
/// </summary>
/// <typeparam name="TKey">主键类型</typeparam>
public interface IAggregateRoot<out TKey> : IAggregateRoot, IEntity<TKey>
{
}
