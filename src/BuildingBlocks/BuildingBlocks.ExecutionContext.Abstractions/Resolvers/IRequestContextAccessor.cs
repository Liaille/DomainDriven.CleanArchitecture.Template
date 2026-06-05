using BuildingBlocks.ExecutionContext.Abstractions.CoreInterfaces;

namespace BuildingBlocks.ExecutionContext.Abstractions.Resolvers;

/// <summary>
/// 当前请求上下文访问器抽象接口 (业务层不直接依赖，由实现层通过DI注入给IRequestContext)
/// </summary>
/// <remarks>
/// 【底层适配】: 可适配HttpContext、MessageContext、后台任务AsyncLocal等
/// </remarks>
public interface IRequestContextAccessor
{
    /// <summary>
    /// 获取当前请求上下文
    /// </summary>
    /// <param name="cancellationToken">用于取消操作的取消令牌 (由调用方传递)</param>
    /// <returns>当前请求上下文 (永远不会为null，未初始化时返回NullRequestContext.Instance)</returns>
    Task<IRequestContext> GetRequestContextAsync(CancellationToken cancellationToken = default);
}
