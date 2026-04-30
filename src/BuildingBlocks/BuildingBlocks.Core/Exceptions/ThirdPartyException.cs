namespace BuildingBlocks.Core.Exceptions;

/// <summary>
/// 第三方服务异常
/// <para>适用场景: 外部服务调用、推送渠道、第三方接口调用失败</para>
/// </summary>
public class ThirdPartyException : Exception
{
    /// <summary>
    /// 第三方服务名称
    /// </summary>
    public string ServiceName { get; }

    /// <summary>
    /// 初始化第三方服务异常
    /// </summary>
    /// <param name="message">异常描述消息</param>
    /// <param name="serviceName">第三方服务名称</param>
    public ThirdPartyException(string message, string serviceName) : base(message)
    {
        ServiceName = serviceName;
    }

    /// <summary>
    /// 初始化带内部异常的第三方服务异常
    /// </summary>
    /// <param name="message">异常描述消息</param>
    /// <param name="serviceName">第三方服务名称</param>
    /// <param name="innerException">内部异常对象</param>
    public ThirdPartyException(string message, string serviceName, Exception innerException)
        : base(message, innerException)
    {
        ServiceName = serviceName;
    }
}
