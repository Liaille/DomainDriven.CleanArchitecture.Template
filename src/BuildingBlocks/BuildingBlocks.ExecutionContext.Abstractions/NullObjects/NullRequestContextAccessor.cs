using BuildingBlocks.ExecutionContext.Abstractions.CoreInterfaces;
using BuildingBlocks.ExecutionContext.Abstractions.Resolvers;

namespace BuildingBlocks.ExecutionContext.Abstractions.NullObjects;

/// <summary>
/// 空请求上下文解析器单例 (纯只读、不执行任何业务逻辑、无副作用)
/// </summary>
public sealed class NullRequestContextAccessor : IRequestContextAccessor
{
    public static NullRequestContextAccessor Instance { get; } = new();
    private NullRequestContextAccessor() { }
    public Task<IRequestContext> GetRequestContextAsync(CancellationToken cancellationToken = default)
        => Task.FromResult<IRequestContext>(NullRequestContext.Instance);
}
