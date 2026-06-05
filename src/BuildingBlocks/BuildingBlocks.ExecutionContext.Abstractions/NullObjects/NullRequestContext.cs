using BuildingBlocks.ExecutionContext.Abstractions.ContextData;
using BuildingBlocks.ExecutionContext.Abstractions.CoreInterfaces;
using System.Collections.Immutable;

namespace BuildingBlocks.ExecutionContext.Abstractions.NullObjects;

/// <summary>
/// 空当前请求上下文访问器单例 (纯只读、不执行任何业务逻辑、无副作用)
/// </summary>
public sealed class NullRequestContext : IRequestContext
{
    public static NullRequestContext Instance { get; } = new();
    private NullRequestContext() { }
    public bool IsAvailable => false;
    public RequestTraceInfo TraceInfo => NullRequestTraceInfo.Instance;
    public string TraceId => TraceInfo.TraceId;
    public string? RequestId => null;
    public string? EnvironmentName => null;
    public string? ClientIp => null;
    public string? UserAgent => null;
    public IImmutableDictionary<string, object?> ExtraProperties => ImmutableDictionary<string, object?>.Empty;
}
