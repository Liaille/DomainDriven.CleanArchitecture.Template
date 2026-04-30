namespace BuildingBlocks.Core.Events;

/// <summary>
/// 集成事件接口 (用于跨微服务发布订阅的事件契约)
/// </summary>
public interface IIntegrationEvent
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
    /// 事件类型 (完整类名，用于反序列化、路由、审计)
    /// </summary>
    string EventType { get; }

    /// <summary>
    /// 事件版本号 (默认值为 "1"，需要版本控制时显式指定)
    /// </summary>
    IntegrationEventVersion Version { get; }
}
