using Template.Domain.Core.Abstractions;

namespace Template.Domain.Core.Events;

/// <summary>
/// 领域事件记录
/// 用于保证事件有序、可靠发布、支持 OutBox 模式
/// </summary>
public class DomainEventRecord(IDomainEvent eventData, long eventOrder)
{
    /// <summary>
    /// 领域事件内容
    /// </summary>
    public IDomainEvent EventData { get; } = eventData ?? throw new ArgumentNullException(nameof(eventData));

    /// <summary>
    /// 事件顺序号（保证同一个聚合内严格有序）
    /// </summary>
    public long EventOrder { get; } = eventOrder;
}
