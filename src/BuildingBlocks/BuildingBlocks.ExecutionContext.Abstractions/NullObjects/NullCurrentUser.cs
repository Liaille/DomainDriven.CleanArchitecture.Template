using BuildingBlocks.ExecutionContext.Abstractions.ContextData;
using BuildingBlocks.ExecutionContext.Abstractions.CoreInterfaces;

namespace BuildingBlocks.ExecutionContext.Abstractions.NullObjects;

/// <summary>
/// 空当前用户访问器单例 (纯只读、不执行任何业务逻辑、无副作用)
/// </summary>
public sealed class NullCurrentUser : ICurrentUser
{
    public static NullCurrentUser Instance { get; } = new();
    private NullCurrentUser() { }
    public bool IsAuthenticated => false;
    public UserInfo User => NullUserInfo.Instance;
    public string UserId => User.UserId;
    public bool IsInRole(string roleName) => false;
    public bool HasPermission(string permissionName) => false;
}
