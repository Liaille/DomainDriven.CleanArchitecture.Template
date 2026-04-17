using BuildingBlocks.Core.Events;

namespace BuildingBlocks.Core.Entities;

/// <summary>
/// 聚合根基类 (支持领域事件有序记录、Outbox、事件溯源)
/// </summary>
/// <typeparam name="TKey">主键类型</typeparam>
public abstract class AggregateRoot<TKey> : Entity<TKey>, IAggregateRoot<TKey>, IGeneratesDomainEvents
{
    // 领域事件集合
    private readonly List<DomainEventRecord> _domainEvents = new();

    // 事件自增序号，保证同一个聚合内严格有序
    private long _eventOrder;

    protected AggregateRoot()
    {
    }

    protected AggregateRoot(TKey id) : base(id)
    {
    }

    /// <summary>
    /// 领域事件记录只读集合
    /// </summary>
    public IReadOnlyCollection<DomainEventRecord> DomainEvents => _domainEvents.AsReadOnly();

    /// <summary>
    /// 添加领域事件 (自动生成顺序号)
    /// </summary>
    /// <param name="domainEvent">领域事件</param>
    protected void AddDomainEvent(IDomainEvent domainEvent)
    {
        _eventOrder++;
        var eventRecord = new DomainEventRecord(domainEvent, _eventOrder);
        _domainEvents.Add(eventRecord);
    }

    /// <summary>
    /// 清空所有领域事件
    /// </summary>
    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}
