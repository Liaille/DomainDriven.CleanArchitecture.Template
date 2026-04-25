namespace BuildingBlocks.Monitoring.Abstractions;

/// <summary>
/// Span 类型枚举
/// </summary>
public enum SpanKind
{
    /// <summary>
    /// 内部操作
    /// </summary>
    Internal = 0,

    /// <summary>
    /// 客户端调用
    /// </summary>
    Client = 1,

    /// <summary>
    /// 服务端处理
    /// </summary>
    Server = 2,

    /// <summary>
    /// 消息生产者
    /// </summary>
    Producer = 3,

    /// <summary>
    /// 消息消费者
    /// </summary>
    Consumer = 4
}
