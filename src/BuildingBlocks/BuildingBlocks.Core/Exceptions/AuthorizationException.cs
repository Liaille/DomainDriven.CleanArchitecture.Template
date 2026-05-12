using BuildingBlocks.Core.ErrorCodes;

namespace BuildingBlocks.Core.Exceptions;

/// <summary>
/// 权限授权异常
/// <para>使用场景: 已登录，但权限不足、禁止访问、禁止操作</para>
/// <para>错误码: 10102 AuthorizationFailed</para>
/// </summary>
public class AuthorizationException : BusinessException
{
    /// <summary>
    /// 初始化授权异常
    /// </summary>
    /// <param name="technicalMessage">技术描述</param>
    public AuthorizationException(string technicalMessage)
        : base(SystemErrorCodes.AuthorizationFailed, technicalMessage)
    {
    }

    /// <summary>
    /// 初始化授权异常 (带内部异常)
    /// </summary>
    /// <param name="technicalMessage">技术描述</param>
    /// <param name="innerException">内部异常</param>
    public AuthorizationException(string technicalMessage, Exception innerException)
        : base(SystemErrorCodes.AuthorizationFailed, technicalMessage, innerException)
    {
    }
}
