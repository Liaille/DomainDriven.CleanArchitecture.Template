using BuildingBlocks.ExecutionContext.Abstractions.ContextData;
using BuildingBlocks.ExecutionContext.Abstractions.Resolvers;

namespace BuildingBlocks.ExecutionContext.Abstractions.NullObjects;

/// <summary>
/// 空租户解析器单例 (纯只读、不执行任何业务逻辑、无副作用)
/// </summary>
public sealed class NullTenantResolver : ITenantResolver
{
    public static NullTenantResolver Instance { get; } = new();
    private NullTenantResolver() { }
    public Task<TenantInfo> ResolveAsync(CancellationToken cancellationToken = default)
        => Task.FromResult(NullTenantInfo.Instance);
    public Task<TenantInfo?> TryResolveAsync(CancellationToken cancellationToken = default)
        => Task.FromResult<TenantInfo?>(null);
}
