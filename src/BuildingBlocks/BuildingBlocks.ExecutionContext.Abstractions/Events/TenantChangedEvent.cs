using BuildingBlocks.Core.Events;
using BuildingBlocks.ExecutionContext.Abstractions.ContextData;

namespace BuildingBlocks.ExecutionContext.Abstractions.Events;

/// <summary>
/// 租户上下文变化事件 (继承自Core的DomainEvent，实现INotification，支持MediatR事件总线)
/// </summary>
/// <remarks>
/// 【继承特性】: Core的DomainEvent已包含唯一事件ID (EventId)、发生时间 (OccurredOn，UTC)、事件类型 (EventType)
/// 【线程安全】: 事件为不可变record，支持多线程安全发布
/// 【使用场景】: 租户切换时触发 (如手动切换、请求初始化切换)，可用于清空租户缓存、更新租户配置等
/// </remarks>
/// <param name="OldTenantId">原租户ID (不可为null/空/空白)</param>
/// <param name="NewTenantInfo">新租户信息 (不可为null)</param>
/// <param name="ChangeType">变化类型 (如"Manual"、"Auto"、"RequestScope"，不可为null/空/空白)</param>
public sealed record TenantChangedEvent(
    string OldTenantId,
    TenantInfo NewTenantInfo,
    string ChangeType) : DomainEvent;
