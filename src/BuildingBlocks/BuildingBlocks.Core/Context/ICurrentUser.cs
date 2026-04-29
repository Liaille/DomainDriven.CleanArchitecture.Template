namespace BuildingBlocks.Core.Context;

/// <summary>
/// 当前用户上下文
/// 支持 RBAC / ABAC / 数据级权限 / 审计
/// </summary>
public interface ICurrentUser
{
    /// <summary>
    /// 是否已通过身份认证
    /// </summary>
    bool IsAuthenticated { get; }

    /// <summary>
    /// 用户唯一标识（弱类型通用兼容）
    /// </summary>
    object? Id { get; }

    /// <summary>
    /// 用户名 / 登录账号
    /// </summary>
    string? UserName { get; }

    /// <summary>
    /// 组织 ID（用于部门/组织范围的数据级授权）
    /// </summary>
    object? OrgId { get; }

    /// <summary>
    /// 角色列表（RBAC 授权使用）
    /// </summary>
    IReadOnlyList<string> Roles { get; }

    /// <summary>
    /// 权限列表（细粒度功能授权）
    /// </summary>
    IReadOnlyList<string> Permissions { get; }

    /// <summary>
    /// 是否为宿主平台管理员
    /// </summary>
    bool IsHostAdmin { get; }

    /// <summary>
    /// 模拟操作者 ID（管理员代用户操作时用于审计）
    /// </summary>
    object? ImpersonatedBy { get; }
}