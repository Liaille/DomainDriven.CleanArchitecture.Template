namespace BuildingBlocks.Core.Exceptions;

/// <summary>
/// 认证异常
/// <para>适用场景: 未登录、登录已过期、令牌无效等身份认证失败场景</para>
/// </summary>
public class AuthenticationException : Exception
{
    /// <summary>
    /// 初始化认证异常
    /// </summary>
    /// <param name="message">异常描述消息</param>
    public AuthenticationException(string message) : base(message)
    {
    }

    /// <summary>
    /// 初始化带内部异常的认证异常
    /// </summary>
    /// <param name="message">异常描述消息</param>
    /// <param name="innerException">内部异常对象</param>
    public AuthenticationException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
