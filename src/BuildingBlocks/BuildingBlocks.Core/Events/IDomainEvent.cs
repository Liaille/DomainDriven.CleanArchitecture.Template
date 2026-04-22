using MediatR;

namespace BuildingBlocks.Core.Events;

/// <summary>
/// 领域事件接口 (实现 INotification 以支持 MediatR 事件总线)
/// </summary>
public interface IDomainEvent : INotification
{
    /// <summary>
    /// 事件唯一标识 (用于幂等、去重、事件溯源)
    /// </summary>
    Guid EventId { get; }

    /// <summary>
    /// 事件发生时间 (UTC)
    /// </summary>
    DateTime OccurredOn { get; }

    /// <summary>
    /// 事件类型 (用于反序列化、路由、审计，通常为完整类名)
    /// </summary>
    string EventType { get; }
}
