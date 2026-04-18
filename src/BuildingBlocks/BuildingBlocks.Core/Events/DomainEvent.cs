namespace BuildingBlocks.Core.Events;

public abstract record DomainEvent : IDomainEvent
{
    /// <summary>
    /// 事件唯一标识 (默认自动生成)
    /// </summary>
    public Guid EventId { get; init; } = Guid.NewGuid();

    /// <summary>
    /// 事件发生时间 (UTC，默认自动生成)
    /// </summary>
    public DateTime OccurredOn { get; init; } = DateTime.UtcNow;

    /// <summary>
    /// 事件类型 (默认使用完整类名)
    /// </summary>
    public string EventType => GetType().FullName ?? GetType().Name;
}
