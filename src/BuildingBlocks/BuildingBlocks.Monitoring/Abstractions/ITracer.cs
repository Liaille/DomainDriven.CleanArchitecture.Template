namespace BuildingBlocks.Monitoring.Abstractions;

/// <summary>
/// 分布式链路追踪抽象接口
/// <para>用于创建和管理 Trace、Span、Baggage 全链路追踪能力</para>
/// </summary>
public interface ITracer
{
    /// <summary>
    /// 开始一个新的追踪 Span
    /// </summary>
    /// <param name="name">Span 名称 (通常为操作名)</param>
    /// <param name="kind">Span 类型 (客户端/服务端/生产者/消费者)</param>
    /// <returns>可释放的 Span 上下文</returns>
    IDisposable StartSpan(string name, SpanKind kind = SpanKind.Internal);

    /// <summary>
    /// 向当前 Span 添加标签
    /// </summary>
    /// <param name="key">标签键</param>
    /// <param name="value">标签值</param>
    void SetTag(string key, string? value);

    /// <summary>
    /// 向当前 Span 添加事件
    /// </summary>
    /// <param name="name">事件名称</param>
    /// <param name="attributes">事件属性</param>
    void AddEvent(string name, Dictionary<string, object?>? attributes = null);

    /// <summary>
    /// 记录异常到当前 Span
    /// </summary>
    /// <param name="exception">异常对象</param>
    void RecordException(Exception exception);

    /// <summary>
    /// 获取当前 Trace ID
    /// </summary>
    /// <returns>当前 Trace ID，如果不存在则返回 null</returns>
    string? GetCurrentTraceId();

    /// <summary>
    /// 获取当前 Span ID
    /// </summary>
    /// <returns>当前 Span ID，如果不存在则返回null</returns>
    string? GetCurrentSpanId();
}
