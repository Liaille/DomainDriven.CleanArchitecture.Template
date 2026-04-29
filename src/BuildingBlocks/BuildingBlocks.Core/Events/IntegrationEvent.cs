namespace BuildingBlocks.Core.Events;

/// <summary>
/// 集成事件基类 (不可变 record 类型，仅记录业务事实)
/// </summary>
public abstract record IntegrationEvent : IIntegrationEvent
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
