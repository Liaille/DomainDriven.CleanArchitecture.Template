using BuildingBlocks.ExecutionContext.Abstractions.ContextData;
using System.Collections.Immutable;

namespace BuildingBlocks.ExecutionContext.Abstractions.NullObjects;

/// <summary>
/// 空用户信息单例 (纯只读、不执行任何业务逻辑、无副作用)
/// </summary>
/// <remarks>
/// 【用途】: ICurrentUser.User 的默认值，明确标识为「未初始化/无用户上下文」
/// 【禁止使用】: 业务层禁止将此实例作为真实的用户信息存储/传输
/// </remarks>
public static class NullUserInfo
{
    /// <summary>
    /// 全局唯一单例实例
    /// </summary>
    public static UserInfo Instance { get; } = new(NullConstants.NullUserId)
    {
        IsAuthenticated = false,
        UserName = "Null User",
        Roles = ImmutableList<string>.Empty,
        Permissions = ImmutableList<string>.Empty,
        ExtraProperties = ImmutableDictionary<string, object?>.Empty
    };
}
