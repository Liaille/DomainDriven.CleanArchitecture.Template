using BuildingBlocks.Core.Events;
using BuildingBlocks.ExecutionContext.Abstractions.ContextData;

namespace BuildingBlocks.ExecutionContext.Abstractions.Events;

/// <summary>
/// 用户上下文变化事件 (继承自Core的DomainEvent，实现INotification，支持MediatR事件总线)
/// </summary>
/// <remarks>
/// 【继承特性】: Core的DomainEvent已包含唯一事件ID (EventId)、发生时间 (OccurredOn，UTC)、事件类型 (EventType)
/// 【线程安全】: 事件为不可变record，支持多线程安全发布
/// 【使用场景】: 用户登录/登出/身份切换时触发，可用于清空用户缓存、更新用户状态等
/// </remarks>
/// <param name="OldUserId">原用户ID (不可为null/空/空白)</param>
/// <param name="NewUserInfo">新用户信息 (不可为null)</param>
/// <param name="ChangeType">变化类型 (如"Login"、"Logout"、"Impersonation"、"RequestScope"，不可为null/空/空白)</param>
public sealed record UserChangedEvent(
    string OldUserId,
    UserInfo NewUserInfo,
    string ChangeType) : DomainEvent;
