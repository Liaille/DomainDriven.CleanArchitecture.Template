using BuildingBlocks.ExecutionContext.Abstractions.ContextData;
using System.Collections.Immutable;

namespace BuildingBlocks.ExecutionContext.Abstractions.NullObjects;

/// <summary>
/// 空链路追踪信息单例 (纯只读、不执行任何业务逻辑、无副作用)
/// </summary>
/// <remarks>
/// 【用途】: IRequestContext.TraceInfo 的默认值，明确标识为「未初始化/无请求上下文」
/// 【禁止使用】: 业务层禁止将此实例作为真实的链路追踪信息存储/传输
/// </remarks>
public static class NullRequestTraceInfo
{
    /// <summary>
    /// 全局唯一单例实例
    /// </summary>
    public static RequestTraceInfo Instance { get; } = new(NullConstants.NullTraceId)
    {
        ExtraProperties = ImmutableDictionary<string, object?>.Empty
    };
}
