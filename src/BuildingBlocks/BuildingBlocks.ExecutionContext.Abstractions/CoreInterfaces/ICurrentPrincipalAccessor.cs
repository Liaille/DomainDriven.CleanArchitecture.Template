using System.Security.Claims;

namespace BuildingBlocks.ExecutionContext.Abstractions.CoreInterfaces;

/// <summary>
/// 当前.NET Identity ClaimsPrincipal访问器抽象接口 (业务层可直接依赖，但仅用于获取原始身份信息)
/// </summary>
/// <remarks>
/// 【线程安全】: 默认承诺异步线程安全
/// 【职责单一】: 仅用于获取/设置原始ClaimsPrincipal，不包含任何业务逻辑
/// 【底层适配】: 可适配HttpContext、MessageContext、后台任务AsyncLocal等
/// </remarks>
public interface ICurrentPrincipalAccessor
{
    /// <summary>
    /// 当前ClaimsPrincipal (null表示未初始化或未认证)
    /// </summary>
    ClaimsPrincipal? Principal { get; }

    /// <summary>
    /// 临时设置ClaimsPrincipal (作用域生命周期结束后自动恢复)
    /// </summary>
    /// <param name="principal">要设置的ClaimsPrincipal (可选，null时表示清除当前身份)</param>
    /// <returns>可释放的作用域对象 (Dispose后自动恢复原Principal)</returns>
    IDisposable SetPrincipal(ClaimsPrincipal? principal);
}
