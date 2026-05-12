using BuildingBlocks.Core.ErrorCodes;

namespace BuildingBlocks.Core.Exceptions;

/// <summary>
/// 第三方服务调用异常
/// <para>使用场景: 调用外部接口、HTTP、支付、短信、推送等第三方服务失败</para>
/// <para>错误码: 30000 ThirdPartyServiceFailed</para>
/// </summary>
public class ThirdPartyException : BusinessException
{
    /// <summary>
    /// 第三方服务名称
    /// </summary>
    public string ServiceName { get; }

    /// <summary>
    /// 初始化第三方服务异常
    /// </summary>
    /// <param name="serviceName">服务名</param>
    /// <param name="technicalMessage">技术描述</param>
    public ThirdPartyException(string serviceName, string technicalMessage)
        : base(ThirdPartyErrorCodes.ThirdPartyServiceInvokeFailed, technicalMessage)
    {
        ServiceName = serviceName;
    }

    /// <summary>
    /// 初始化第三方服务异常 (带内部异常)
    /// </summary>
    /// <param name="serviceName">服务名</param>
    /// <param name="technicalMessage">技术描述</param>
    /// <param name="innerException">内部异常</param>
    public ThirdPartyException(string serviceName, string technicalMessage, Exception innerException)
        : base(ThirdPartyErrorCodes.ThirdPartyServiceInvokeFailed, technicalMessage, innerException)
    {
        ServiceName = serviceName;
    }
}
