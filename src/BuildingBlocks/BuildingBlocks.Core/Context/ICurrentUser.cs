namespace BuildingBlocks.Core.Context;

/// <summary>
/// 当前用户上下文
/// </summary>
public interface ICurrentUser
{
    /// <summary>
    /// 是否已通过身份认证
    /// </summary>
    bool IsAuthenticated { get; }

    /// <summary>
    /// 用户唯一标识 (弱类型，保证通用)
    /// </summary>
    object? Id { get; }

    /// <summary>
    /// 用户名/登录账号
    /// </summary>
    string? UserName { get; }

    /// <summary>
    /// 租户 ID (多租户必需，弱类型)
    /// </summary>
    object? TenantId { get; }

    /// <summary>
    /// 是否为宿主平台管理员 (用于跨租户功能)
    /// </summary>
    bool IsHostAdmin { get; }
}
