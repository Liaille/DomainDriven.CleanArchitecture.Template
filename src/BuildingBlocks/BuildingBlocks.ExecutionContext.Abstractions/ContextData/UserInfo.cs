using BuildingBlocks.Core.Common;
using System.Collections.Immutable;

namespace BuildingBlocks.ExecutionContext.Abstractions.ContextData;

/// <summary>
/// 业务化用户信息纯契约记录
/// </summary>
public sealed record UserInfo
{
    /// <summary>
    /// 业务用户唯一标识 (不可为null/空字符串/空白字符)
    /// </summary>
    public string UserId { get; init; }

    /// <summary>
    /// 用户登录名/业务显示名 (可选)
    /// </summary>
    public string? UserName { get; init; }

    /// <summary>
    /// 用户邮箱 (可选)
    /// </summary>
    public string? Email { get; init; }

    /// <summary>
    /// 用户手机号 (可选)
    /// </summary>
    public string? Phone { get; init; }

    /// <summary>
    /// 不可变用户角色列表 (可选)
    /// </summary>
    public IImmutableList<string>? Roles { get; init; }

    /// <summary>
    /// 不可变用户权限列表 (可选)
    /// </summary>
    public IImmutableList<string>? Permissions { get; init; }

    /// <summary>
    /// 用户是否已通过身份认证 (默认false)
    /// </summary>
    public bool IsAuthenticated { get; init; } = false;

    /// <summary>
    /// 不可变扩展属性字典 (Key为string，Value为object?，可选)
    /// </summary>
    public IImmutableDictionary<string, object?>? ExtraProperties { get; init; }

    /// <summary>
    /// 不可变用户角色列表 (默认值为Empty，避免外部判空)
    /// </summary>
    public IImmutableList<string> SafeRoles => Roles ?? ImmutableList<string>.Empty;

    /// <summary>
    /// 不可变用户权限列表 (默认值为Empty，避免外部判空)
    /// </summary>
    public IImmutableList<string> SafePermissions => Permissions ?? ImmutableList<string>.Empty;

    /// <summary>
    /// 不可变扩展属性字典 (默认值为Empty，避免外部判空)
    /// </summary>
    public IImmutableDictionary<string, object?> SafeExtraProperties => ExtraProperties ?? ImmutableDictionary<string, object?>.Empty;

    /// <summary>
    /// 简化构造函数 (仅需用户ID)
    /// </summary>
    /// <param name="userId">业务用户唯一标识 (不可为null/空字符串/空白字符)</param>
    public UserInfo(string userId) : this(userId, null)
    {
    }

    /// <summary>
    /// 完整构造函数
    /// </summary>
    /// <param name="userId">业务用户唯一标识 (不可为null/空字符串/空白字符)</param>
    /// <param name="userName">用户登录名/业务显示名 (可选)</param>
    /// <param name="email">用户邮箱 (可选)</param>
    /// <param name="phone">用户手机号 (可选)</param>
    /// <param name="roles">不可变用户角色列表 (可选)</param>
    /// <param name="permissions">不可变用户权限列表 (可选)</param>
    /// <param name="isAuthenticated">用户是否已通过身份认证 (默认false)</param>
    /// <param name="extraProperties">不可变扩展属性字典 (Key为string，Value为object?，可选)</param>
    /// <exception cref="ArgumentNullException">当userId为null/空字符串/空白字符时抛出</exception>
    public UserInfo(
        string userId,
        string? userName = null,
        string? email = null,
        string? phone = null,
        IImmutableList<string>? roles = null,
        IImmutableList<string>? permissions = null,
        bool isAuthenticated = false,
        IImmutableDictionary<string, object?>? extraProperties = null)
    {
        Guard.NotNullOrWhiteSpace(userId);

        UserId = userId;
        UserName = userName;
        Email = email;
        Phone = phone;
        Roles = roles;
        Permissions = permissions;
        IsAuthenticated = isAuthenticated;
        ExtraProperties = extraProperties;
    }

    /// <summary>
    /// 检查用户是否拥有指定角色 (已包含空参数校验)
    /// </summary>
    /// <param name="roleName">要检查的角色名称 (不可为null/空字符串/空白字符；忽略大小写)</param>
    /// <returns>true表示用户拥有该角色，false表示未拥有</returns>
    /// <exception cref="ArgumentNullException">当roleName为null/空字符串/空白字符时抛出</exception>
    public bool IsInRole(string roleName)
    {
        Guard.NotNullOrWhiteSpace(roleName);
        return SafeRoles.Contains(roleName, StringComparer.OrdinalIgnoreCase);
    }

    /// <summary>
    /// 检查用户是否拥有指定权限 (已包含空参数校验)
    /// </summary>
    /// <param name="permissionName">要检查的权限名称 (不可为null/空字符串/空白字符；忽略大小写)</param>
    /// <returns>true表示用户拥有该权限，false表示未拥有</returns>
    /// <exception cref="ArgumentNullException">当permissionName为null/空字符串/空白字符时抛出</exception>
    public bool HasPermission(string permissionName)
    {
        Guard.NotNullOrWhiteSpace(permissionName);
        return SafePermissions.Contains(permissionName, StringComparer.OrdinalIgnoreCase);
    }

    /// <summary>
    /// 安全合并新扩展属性，返回新的UserInfo实例 (不会修改原实例)
    /// </summary>
    /// <param name="newProperties">要合并的新扩展属性字典 (不可为null)</param>
    /// <returns>合并后的新UserInfo实例</returns>
    /// <exception cref="ArgumentNullException">当newProperties为null时抛出</exception>
    public UserInfo WithMergedExtraProperties(IDictionary<string, object?> newProperties)
    {
        Guard.NotNull(newProperties);
        return this with
        {
            ExtraProperties = SafeExtraProperties.SetItems(newProperties)
        };
    }

    /// <summary>
    /// 安全添加单个新扩展属性，返回新的UserInfo实例 (不会修改原实例)
    /// </summary>
    /// <param name="key">扩展属性Key (不可为null/空字符串/空白字符)</param>
    /// <param name="value">扩展属性Value (可选)</param>
    /// <returns>添加后的新UserInfo实例</returns>
    /// <exception cref="ArgumentNullException">当key为null/空字符串/空白字符时抛出</exception>
    public UserInfo WithExtraProperty(string key, object? value)
    {
        Guard.NotNullOrWhiteSpace(key);
        return this with
        {
            ExtraProperties = SafeExtraProperties.SetItem(key, value)
        };
    }

    /// <summary>
    /// 安全更新用户角色，返回新的UserInfo实例 (不会修改原实例)
    /// </summary>
    /// <param name="newRoles">新的角色列表 (不可为null)</param>
    /// <returns>更新后的新UserInfo实例</returns>
    /// <exception cref="ArgumentNullException">当newRoles为null时抛出</exception>
    public UserInfo WithRoles(IImmutableList<string> newRoles)
    {
        Guard.NotNull(newRoles);
        return this with
        {
            Roles = newRoles
        };
    }

    /// <summary>
    /// 安全更新用户权限，返回新的UserInfo实例 (不会修改原实例)
    /// </summary>
    /// <param name="newPermissions">新的权限列表 (不可为null)</param>
    /// <returns>更新后的新UserInfo实例</returns>
    /// <exception cref="ArgumentNullException">当newPermissions为null时抛出</exception>
    public UserInfo WithPermissions(IImmutableList<string> newPermissions)
    {
        Guard.NotNull(newPermissions);
        return this with
        {
            Permissions = newPermissions
        };
    }
}
