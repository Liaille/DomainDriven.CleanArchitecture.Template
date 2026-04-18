using BuildingBlocks.Core.Events;

namespace BuildingBlocks.Core.Entities;

/// <summary>
/// 聚合根基类 (支持领域事件有序记录、Outbox、事件溯源)
/// </summary>
/// <typeparam name="TKey">主键类型</typeparam>
public abstract class AggregateRoot<TKey> : Entity<TKey>, IAggregateRoot<TKey>, IGeneratesDomainEvents
{
    /// <summary>
    /// 领域事件记录列表 (包含顺序)
    /// </summary>
    private readonly List<DomainEventRecord> _domainEvents = [];

    /// <summary>
    /// 事件顺序计数器 (保证事件处理的先后顺序)
    /// </summary>
    private long _eventOrderCounter;

    /// <summary>
    /// 领域事件只读列表
    /// </summary>
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents
        .Select(record => record.EventData)
        .ToList()
        .AsReadOnly();

    /// <summary>
    /// 获取带执行顺序的领域事件记录只读列表 (供基础设施层分发时排序)
    /// </summary>
    /// <returns></returns>
    public IReadOnlyList<DomainEventRecord> GetDomainEventRecords() => _domainEvents.AsReadOnly();

    protected AggregateRoot()
    {
    }

    protected AggregateRoot(TKey id) : base(id)
    {
    }

    /// <summary>
    /// 添加领域事件 (自动生成顺序号)
    /// </summary>
    /// <param name="domainEvent">领域事件</param>
    protected void AddDomainEvent(IDomainEvent domainEvent)
    {
        ArgumentNullException.ThrowIfNull(domainEvent, nameof(domainEvent));

        var eventRecord = new DomainEventRecord(domainEvent, Interlocked.Increment(ref _eventOrderCounter));
        _domainEvents.Add(eventRecord);
    }

    /// <summary>
    /// 清空所有领域事件
    /// </summary>
    public void ClearDomainEvents() =>  _domainEvents.Clear();
}
