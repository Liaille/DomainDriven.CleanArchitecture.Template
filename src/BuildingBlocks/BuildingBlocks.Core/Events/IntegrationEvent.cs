using System.Text.Json.Serialization;

namespace BuildingBlocks.Core.Events;

/// <summary>
/// 集成事件抽象基类
/// <para>所有跨微服务通信的集成事件必须直接继承此类</para>
/// <remark>
/// 强制约束:
/// <list type="number">
/// <item>所有业务属性必须使用 init-only 赋值</item>
/// <item>新增字段必须提供默认值，保证向下兼容</item>
/// <item>禁止直接删除已有字段 (可标记 [Obsolete]，至少保留2个版本的兼容期)</item>
/// <item>禁止修改已有字段类型 (可新增字段替代)</item>
/// </list>
/// </remark>
/// </summary>
public abstract record IntegrationEvent : IIntegrationEvent
{
    /// <summary>
    /// 事件唯一标识
    /// </summary>
    /// <remarks>用于消息幂等、追踪、日志、去重</remarks>
    public Guid EventId { get; init; }

    /// <summary>
    /// 事件发生时间 (UTC)
    /// </summary>
    /// <remarks>统一使用UTC时间，避免跨时区问题</remarks>
    public DateTime OccurredOn { get; init; }

    /// <summary>
    /// 事件类型名称 (自动获取当前类型完整名)
    /// </summary>
    /// <remarks>用于消息路由、反序列化、日志标记</remarks>
    public string EventType => GetType().FullName ?? GetType().Name;

    /// <summary>
    /// 事件版本号
    /// </summary>
    /// <remarks>用于事件契约演进、多版本兼容</remarks>
    public IntegrationEventVersion Version { get; init; }

    #region 构造函数

    /// <summary>
    /// 【序列化专用】核心构造函数
    /// </summary>
    /// <param name="eventId">事件ID</param>
    /// <param name="occurredOn">事件发生时间</param>
    /// <param name="version">事件版本</param>
    /// <remarks>
    /// 1. 唯一被 Json 序列化器使用的构造函数
    /// 2. 所有其他构造函数最终都会调用此构造函数
    /// 3. 确保初始化路径统一，避免属性未赋值
    /// </remarks>
    [JsonConstructor]
    protected IntegrationEvent(Guid eventId, DateTime occurredOn, IntegrationEventVersion version)
    {
        EventId = eventId;
        OccurredOn = occurredOn;
        Version = version;
    }

    /// <summary>
    /// 【默认构造】无参构造函数 (90% 场景使用)
    /// </summary>
    /// <remarks>
    /// 自动生成：
    /// <para>- EventId = Guid.NewGuid()</para>
    /// <para>- OccurredOn = DateTime.UtcNow</para>
    /// <para>- Version = IntegrationEventVersion.Default (版本1)</para>
    /// </remarks>
    protected IntegrationEvent()
        : this(Guid.NewGuid(), DateTime.UtcNow, IntegrationEventVersion.Default)
    {
    }

    /// <summary>
    /// 【自定义版本】指定版本号的构造函数
    /// </summary>
    /// <param name="version">事件版本</param>
    /// <remarks>
    /// 支持隐式转换：可直接传入 int / string / 枚举
    /// </remarks>
    protected IntegrationEvent(IntegrationEventVersion version)
        : this(Guid.NewGuid(), DateTime.UtcNow, version)
    {
    }

    #endregion
}
