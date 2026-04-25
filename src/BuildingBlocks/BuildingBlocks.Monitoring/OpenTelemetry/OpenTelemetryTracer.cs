using BuildingBlocks.Core.Common;
using BuildingBlocks.Monitoring.Abstractions;
using BuildingBlocks.Monitoring.Configuration;
using System.Diagnostics;

namespace BuildingBlocks.Monitoring.OpenTelemetry;

/// <summary>
/// OpenTelemetry 链路追踪默认实现
/// <para>基于 System.Diagnostics.Activity 实现，兼容 OpenTelemetry 标准</para>
/// </summary>
/// <param name="options">OpenTelemetry 配置</param>
public class OpenTelemetryTracer(OpenTelemetryOptions options) : ITracer
{
    /// <summary>
    /// ActivitySource 实例 (用于创建 Activity)
    /// </summary>
    private readonly ActivitySource _activitySource = new(options.ServiceName, options.ServiceVersion);

    /// <summary>
    /// 开始一个新的追踪 Span
    /// </summary>
    /// <param name="name">Span 名称 (通常为操作名)</param>
    /// <param name="kind">Span 类型 (客户端/服务端/生产者/消费者)</param>
    /// <returns>可释放的 Span 上下文</returns>
    public IDisposable StartSpan(string name, SpanKind kind = SpanKind.Internal)
    {
        Guard.NotNullOrEmpty(name);

        // 转换为 System.Diagnostics.ActivityKind
        var activityKind = kind switch
        {
            SpanKind.Client => ActivityKind.Client,
            SpanKind.Server => ActivityKind.Server,
            SpanKind.Producer => ActivityKind.Producer,
            SpanKind.Consumer => ActivityKind.Consumer,
            _ => ActivityKind.Internal
        };

        // 启动 Activity
        var activity = _activitySource.StartActivity(name, activityKind);

        return (IDisposable?)activity ?? NullDisposable.Instance;
    }

    /// <summary>
    /// 向当前 Span 添加标签
    /// </summary>
    /// <param name="key">标签键</param>
    /// <param name="value">标签值</param>
    public void SetTag(string key, string? value)
    {
        Guard.NotNullOrEmpty(key);

        var activity = Activity.Current;
        activity?.SetTag(key, value);
    }

    /// <summary>
    /// 向当前 Span 添加事件
    /// </summary>
    /// <param name="name">事件名称</param>
    /// <param name="attributes">事件属性</param>
    public void AddEvent(string name, Dictionary<string, object?>? attributes = null)
    {
        Guard.NotNullOrEmpty(name);

        var activity = Activity.Current;
        if (activity is not null)
        {
            var tags = attributes?.Select(kvp => new KeyValuePair<string, object?>(kvp.Key, kvp.Value)).ToList();
            activity.AddEvent(new ActivityEvent(name, DateTimeOffset.UtcNow, tags is null ? default : [.. tags]));
        }
    }

    /// <summary>
    /// 记录异常到当前 Span
    /// </summary>
    /// <param name="exception">异常对象</param>
    public void RecordException(Exception exception)
    {
        Guard.NotNull(exception);

        var activity = Activity.Current;
        if (activity is not null)
        {
            activity.AddException(exception);
            activity.SetStatus(ActivityStatusCode.Error, exception.Message);
        }
    }

    /// <summary>
    /// 获取当前 Trace ID
    /// </summary>
    /// <returns>当前 Trace ID，如果不存在则返回 null</returns>
    public string? GetCurrentTraceId()
    {
        return Activity.Current?.TraceId.ToString();
    }

    /// <summary>
    /// 获取当前 Span ID
    /// </summary>
    /// <returns>当前 Span ID，如果不存在则返回null</returns>
    public string? GetCurrentSpanId()
    {
        return Activity.Current?.SpanId.ToString();
    }

    /// <summary>
    /// 空可释放对象 (用于 Activity 为 null 时的降级)
    /// </summary>
    private class NullDisposable : IDisposable
    {
        public static NullDisposable Instance { get; } = new();
        public void Dispose() { }
    }
}
