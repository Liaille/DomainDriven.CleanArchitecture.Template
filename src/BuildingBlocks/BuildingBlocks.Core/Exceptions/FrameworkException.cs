namespace BuildingBlocks.Core.Exceptions;

/// <summary>
/// 系统异常
/// <para>适用场景: 框架级、基础设施级错误，配置错误、资源不可用等</para>
/// </summary>
public class FrameworkException : Exception
{
    /// <summary>
    /// 初始化系统异常
    /// </summary>
    /// <param name="message">异常描述消息</param>
    public FrameworkException(string message) : base(message)
    {
    }

    /// <summary>
    /// 初始化带内部异常的系统异常
    /// </summary>
    /// <param name="message">异常描述消息</param>
    /// <param name="innerException">内部异常对象</param>
    public FrameworkException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
