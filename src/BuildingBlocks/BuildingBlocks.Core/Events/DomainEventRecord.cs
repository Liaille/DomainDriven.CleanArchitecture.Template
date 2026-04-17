namespace BuildingBlocks.Core.Events;

/// <summary>
/// 领域事件记录
/// 用于保证事件有序、可靠发布、支持 OutBox 模式与事件溯源
/// </summary>
/// <remarks>
/// 构造领域事件记录
/// </remarks>
/// <param name="eventData">领域事件实例</param>
/// <param name="eventOrder">事件顺序号</param>
/// <exception cref="ArgumentNullException"></exception>
public class DomainEventRecord(IDomainEvent eventData, long eventOrder)
{
    /// <summary>
    /// 领域事件内容
    /// </summary>
    public IDomainEvent EventData { get; } = eventData ?? throw new ArgumentNullException(nameof(eventData));

    /// <summary>
    /// 事件顺序号 (保证同一个聚合内事件严格有序)
    /// </summary>
    public long EventOrder { get; } = eventOrder;
}
