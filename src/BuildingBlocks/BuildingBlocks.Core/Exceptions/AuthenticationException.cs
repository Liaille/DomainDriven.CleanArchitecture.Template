using BuildingBlocks.Core.ErrorCodes;

namespace BuildingBlocks.Core.Exceptions;

/// <summary>
/// 身份认证异常
/// <para>使用场景: 未登录、Token 无效/过期/伪造、身份验证失败</para>
/// <para>错误码: 10101 AuthenticationFailed</para>
/// </summary>
public class AuthenticationException : BusinessException
{
    /// <summary>
    /// 初始化认证异常
    /// </summary>
    /// <param name="technicalMessage">技术描述</param>
    public AuthenticationException(string technicalMessage)
        : base(SystemErrorCodes.AuthenticationFailed, technicalMessage)
    {
    }

    /// <summary>
    /// 初始化认证异常 (带内部异常)
    /// </summary>
    /// <param name="technicalMessage">技术描述</param>
    /// <param name="innerException">内部异常</param>
    public AuthenticationException(string technicalMessage, Exception innerException)
        : base(SystemErrorCodes.AuthenticationFailed, technicalMessage, innerException)
    {
    }
}
