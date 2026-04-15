using Template.Domain.Core.Abstractions;
using Template.Domain.Core.Events;

namespace Template.Domain.Core.Entities;

/// <summary>
/// 聚合根基类 (复合主键/自定义主键)
/// </summary>
public abstract class AggregateRoot : Entity, IAggregateRoot
{
    private readonly List<DomainEventRecord> _domainEvents = [];
    private long _eventOrderCounter;

    public IReadOnlyCollection<DomainEventRecord> DomainEvents => _domainEvents.AsReadOnly();

    protected void AddDomainEvent(IDomainEvent eventData)
    {
        ArgumentNullException.ThrowIfNull(eventData);

        _domainEvents.Add(new DomainEventRecord(eventData, Interlocked.Increment(ref _eventOrderCounter)));
    }

    public void ClearDomainEvents() => _domainEvents.Clear();
}

/// <summary>
/// 泛型主键聚合根基类
/// 适用于单一主键场景（如 int/long/Guid/string 等），简化主键访问
/// </summary>
public abstract class AggregateRoot<TKey> : Entity<TKey>, IAggregateRoot<TKey>
{
    private readonly List<DomainEventRecord> _domainEvents = [];
    private long _eventOrderCounter;

    public IReadOnlyCollection<DomainEventRecord> DomainEvents => _domainEvents.AsReadOnly();

    protected AggregateRoot() { }
    protected AggregateRoot(TKey id) : base(id) { }

    protected void AddDomainEvent(IDomainEvent eventData)
    {
        ArgumentNullException.ThrowIfNull(eventData);

        _domainEvents.Add(new DomainEventRecord(eventData, Interlocked.Increment(ref _eventOrderCounter)));
    }

    public void ClearDomainEvents() => _domainEvents.Clear();
}
