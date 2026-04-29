namespace BuildingBlocks.Core.Exceptions;

/// <summary>
/// 授权异常
/// 适用场景：已通过身份认证，但权限不足无法执行操作
/// </summary>
public class AuthorizationException : Exception
{
    /// <summary>
    /// 初始化授权异常
    /// </summary>
    /// <param name="message">异常描述消息</param>
    public AuthorizationException(string message) : base(message)
    {
    }

    /// <summary>
    /// 初始化带内部异常的授权异常
    /// </summary>
    /// <param name="message">异常描述消息</param>
    /// <param name="innerException">内部异常对象</param>
    public AuthorizationException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
