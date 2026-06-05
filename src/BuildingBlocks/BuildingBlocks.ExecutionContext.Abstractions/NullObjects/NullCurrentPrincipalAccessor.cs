using BuildingBlocks.ExecutionContext.Abstractions.CoreInterfaces;
using System.Security.Claims;

namespace BuildingBlocks.ExecutionContext.Abstractions.NullObjects;

/// <summary>
/// 空当前ClaimsPrincipal访问器单例 (纯只读、不执行任何业务逻辑、无副作用)
/// </summary>
public sealed class NullCurrentPrincipalAccessor : ICurrentPrincipalAccessor
{
    public static NullCurrentPrincipalAccessor Instance { get; } = new();
    private NullCurrentPrincipalAccessor() { }
    public ClaimsPrincipal? Principal => null;
    public IDisposable SetPrincipal(ClaimsPrincipal? principal) => NullDisposable.Instance;
}
