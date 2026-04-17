using MediatR;

namespace BuildingBlocks.Core.Events;

/// <summary>
/// 领域事件接口 (实现 INotification 以支持 MediatR 事件总线)
/// </summary>
public interface IDomainEvent : INotification
{
    /// <summary>
    /// 事件发生时间 (UTC)
    /// </summary>
    DateTime OccurredOn { get; }
}

