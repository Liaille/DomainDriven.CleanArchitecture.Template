using BuildingBlocks.ExecutionContext.Abstractions.ContextData;
using BuildingBlocks.ExecutionContext.Abstractions.NullObjects;

namespace BuildingBlocks.ExecutionContext.Abstractions.CoreInterfaces;

/// <summary>
/// 当前业务用户访问器抽象接口 (业务层可直接依赖)
/// </summary>
/// <remarks>
/// 【线程安全】: 默认承诺异步线程安全
/// 【职责】: 提供当前业务用户的只读访问、角色/权限检查功能
/// 【默认实现规则】: IsAuthenticated = User.IsAuthenticated && User.UserId != NullConstants.NullUserId
/// </remarks>
public interface ICurrentUser
{
    /// <summary>
    /// 当前用户是否已认证
    /// </summary>
    /// <value>
    /// true表示用户已通过身份认证、用户ID非NullUserId
    /// false表示其他情况
    /// </value>
    bool IsAuthenticated
    {
        get
        {
            var user = User;
            return user.IsAuthenticated && user.UserId != NullConstants.NullUserId;
        }
    }

    /// <summary>
    /// 当前用户信息 (永远不会为null，未初始化或未认证时返回NullUserInfo.Instance)
    /// </summary>
    UserInfo User { get; }

    /// <summary>
    /// 当前用户ID (永远不会为null/空字符串/空白字符，未初始化时返回NullConstants.NullUserId)
    /// </summary>
    string UserId => User.UserId;

    /// <summary>
    /// 检查当前用户是否拥有指定角色
    /// </summary>
    /// <param name="roleName">要检查的角色名称 (不可为null/空字符串/空白字符；忽略大小写)</param>
    /// <returns>true表示用户拥有该角色，false表示未拥有</returns>
    /// <exception cref="ArgumentNullException">当roleName为null/空字符串/空白字符时抛出</exception>
    bool IsInRole(string roleName);

    /// <summary>
    /// 检查当前用户是否拥有指定权限
    /// </summary>
    /// <param name="permissionName">要检查的权限名称 (不可为null/空字符串/空白字符；忽略大小写)</param>
    /// <returns>true表示用户拥有该权限，false表示未拥有</returns>
    /// <exception cref="ArgumentNullException">当permissionName为null/空字符串/空白字符时抛出</exception>
    bool HasPermission(string permissionName);
}
