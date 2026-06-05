using BuildingBlocks.Core.Common;
using System.Collections.Immutable;

namespace BuildingBlocks.ExecutionContext.Abstractions.ContextData;

public sealed record RequestTraceInfo
{
    /// <summary>
    /// 全局唯一链路ID (不可为null/空字符串/空白字符；W3C TraceContext标准要求: 16字节十六进制字符串)
    /// </summary>
    public string TraceId { get; init; }

    /// <summary>
    /// 当前操作的SpanId (可选；W3C TraceContext标准要求: 8字节十六进制字符串)
    /// </summary>
    public string? SpanId { get; init; }

    /// <summary>
    /// 父操作的SpanId (可选；W3C TraceContext标准要求: 8字节十六进制字符串)
    /// </summary>
    public string? ParentSpanId { get; init; }

    /// <summary>
    /// 是否被采样记录 (可选；OpenTelemetry标准要求)
    /// </summary>
    public bool? Sampled { get; init; }

    /// <summary>
    /// 不可变扩展属性字典 (Key为string，Value为object?，可选)
    /// </summary>
    public IImmutableDictionary<string, object?>? ExtraProperties { get; init; }

    /// <summary>
    /// 不可变扩展属性字典 (默认值为Empty，避免外部判空)
    /// </summary>
    public IImmutableDictionary<string, object?> SafeExtraProperties
        => ExtraProperties ?? ImmutableDictionary<string, object?>.Empty;

    /// <summary>
    /// 简化构造函数 (仅需TraceId)
    /// </summary>
    /// <param name="traceId">全局唯一链路ID</param>
    public RequestTraceInfo(string traceId) : this(traceId, null)
    {
    }

    /// <summary>
    /// 完整构造函数
    /// </summary>
    /// <param name="traceId">全局唯一链路ID</param>
    /// <param name="spanId">当前操作的SpanId</param>
    /// <param name="parentSpanId">父操作的SpanId</param>
    /// <param name="sampled">是否被采样记录</param>
    /// <param name="extraProperties">不可变扩展属性字典</param>
    /// <exception cref="ArgumentNullException">当traceId为null/空字符串/空白字符时抛出</exception>
    public RequestTraceInfo(
        string traceId,
        string? spanId = null,
        string? parentSpanId = null,
        bool? sampled = null,
        IImmutableDictionary<string, object?>? extraProperties = null)
    {
        Guard.NotNullOrWhiteSpace(traceId);

        TraceId = traceId;
        SpanId = spanId;
        ParentSpanId = parentSpanId;
        Sampled = sampled;
        ExtraProperties = extraProperties;
    }

    /// <summary>
    /// 安全更新TraceId，返回新的RequestTraceInfo实例 (不会修改原实例)
    /// </summary>
    /// <param name="newTraceId">新的全局唯一链路ID</param>
    /// <returns>更新后的新实例</returns>
    /// <exception cref="ArgumentNullException">当newTraceId为null/空字符串/空白字符时抛出</exception>
    public RequestTraceInfo WithTraceId(string newTraceId)
    {
        Guard.NotNullOrWhiteSpace(newTraceId);
        return this with { TraceId = newTraceId };
    }

    /// <summary>
    /// 安全更新SpanId，返回新的RequestTraceInfo实例 (不会修改原实例)
    /// </summary>
    /// <param name="newSpanId">新的当前操作SpanId</param>
    /// <returns>更新后的新实例</returns>
    public RequestTraceInfo WithSpanId(string? newSpanId)
    {
        return this with { SpanId = newSpanId };
    }

    /// <summary>
    /// 安全更新ParentSpanId，返回新的RequestTraceInfo实例 (不会修改原实例)
    /// </summary>
    /// <param name="newParentSpanId">新的父操作SpanId</param>
    /// <returns>更新后的新实例</returns>
    public RequestTraceInfo WithParentSpanId(string? newParentSpanId)
    {
        return this with { ParentSpanId = newParentSpanId };
    }

    /// <summary>
    /// 安全更新采样标记，返回新的RequestTraceInfo实例 (不会修改原实例)
    /// </summary>
    /// <param name="newSampled">新的采样标记</param>
    /// <returns>更新后的新实例</returns>
    public RequestTraceInfo WithSampled(bool? newSampled)
    {
        return this with { Sampled = newSampled };
    }

    /// <summary>
    /// 安全合并新扩展属性，返回新的RequestTraceInfo实例 (不会修改原实例)
    /// </summary>
    /// <param name="newProperties">要合并的新扩展属性字典 (不可为null)</param>
    /// <returns>合并后的新实例</returns>
    /// <exception cref="ArgumentNullException">当newProperties为null时抛出</exception>
    public RequestTraceInfo WithMergedExtraProperties(IDictionary<string, object?> newProperties)
    {
        Guard.NotNull(newProperties);
        return this with
        {
            ExtraProperties = SafeExtraProperties.SetItems(newProperties)
        };
    }

    /// <summary>
    /// 安全添加单个新扩展属性，返回新的RequestTraceInfo实例 (不会修改原实例)
    /// </summary>
    /// <param name="key">扩展属性Key (不可为null/空字符串/空白字符)</param>
    /// <param name="value">扩展属性Value (可选)</param>
    /// <returns>添加后的新实例</returns>
    /// <exception cref="ArgumentNullException">当key为null/空字符串/空白字符时抛出</exception>
    public RequestTraceInfo WithExtraProperty(string key, object? value)
    {
        Guard.NotNullOrWhiteSpace(key);
        return this with
        {
            ExtraProperties = SafeExtraProperties.SetItem(key, value)
        };
    }
}
