using BuildingBlocks.ExecutionContext.Abstractions.ContextData;
using System.Security.Claims;

namespace BuildingBlocks.ExecutionContext.Abstractions.Resolvers;

/// <summary>
/// 当前业务用户访问器抽象接口 (业务层不直接依赖，由实现层通过DI注入给ICurrentUser)
/// </summary>
/// <remarks>
/// 【职责】: 将原始ClaimsPrincipal转换为业务UserInfo
/// 【Claim映射规则】: 由实现层自行定义，或通过配置中心动态配置
/// </remarks>
public interface ICurrentUserAccessor
{
    /// <summary>
    /// 从原始ClaimsPrincipal转换为业务UserInfo
    /// </summary>
    /// <param name="principal">原始ClaimsPrincipal (可选，null时返回NullUserInfo.Instance)</param>
    /// <param name="cancellationToken">用于取消操作的取消令牌 (由调用方传递)</param>
    /// <returns>转换后的业务UserInfo (永远不会为null，转换失败时返回NullUserInfo.Instance)</returns>
    Task<UserInfo> GetUserInfoAsync(ClaimsPrincipal? principal, CancellationToken cancellationToken = default);
}
