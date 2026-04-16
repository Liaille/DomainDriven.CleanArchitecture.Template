using MediatR;

namespace Template.Domain.Core.Abstractions;

/// <summary>
/// 领域事件接口 (仅描述领域事实，不区分本地/分布式)
/// </summary>
public interface IDomainEvent : INotification
{
    /// <summary>
    /// 事件唯一标识 (用于幂等、消息追踪、日志)
    /// </summary>
    Guid EventId { get; }

    /// <summary>
    /// 领域事件发生的时间 (UTC)
    /// </summary>
    DateTime OccurredOn { get; }

    /// <summary>
    /// 事件类型名称（便于监控、序列化、路由，默认使用类名）
    /// </summary>
    string EventType => GetType().Name;
}
