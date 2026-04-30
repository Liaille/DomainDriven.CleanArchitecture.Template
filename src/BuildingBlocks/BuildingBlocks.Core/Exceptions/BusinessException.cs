namespace BuildingBlocks.Core.Exceptions;

/// <summary>
/// 业务异常基类
/// <para>适用场景: 业务规则校验失败、业务流程不满足等正常业务异常</para>
/// <para>强制约束: 不触发告警，仅记录 Info 级别日志</para>
/// </summary>
public class BusinessException : Exception
{
    /// <summary>
    /// 业务错误码
    /// </summary>
    public string ErrorCode { get; }

    /// <summary>
    /// 用户友好提示信息 (生产环境返回给前端)
    /// </summary>
    public string UserFriendlyMessage { get; }

    /// <summary>
    /// 内部技术详情 (仅开发环境可见)
    /// </summary>
    public string? InternalDetails { get; }

    /// <summary>
    /// 初始化业务异常
    /// </summary>
    /// <param name="errorCode">业务错误码</param>
    /// <param name="userFriendlyMessage">用户友好提示</param>
    public BusinessException(string errorCode, string userFriendlyMessage)
        : base(userFriendlyMessage)
    {
        ErrorCode = errorCode;
        UserFriendlyMessage = userFriendlyMessage;
    }

    /// <summary>
    /// 初始化带内部详情的业务异常
    /// </summary>
    /// <param name="errorCode">业务错误码</param>
    /// <param name="userFriendlyMessage">用户友好提示</param>
    /// <param name="internalDetails">内部技术详情</param>
    public BusinessException(string errorCode, string userFriendlyMessage, string internalDetails)
        : base($"{userFriendlyMessage} | Internal Details: {internalDetails}")
    {
        ErrorCode = errorCode;
        UserFriendlyMessage = userFriendlyMessage;
        InternalDetails = internalDetails;
    }

    /// <summary>
    /// 初始化带内部异常的业务异常
    /// </summary>
    /// <param name="errorCode">业务错误码</param>
    /// <param name="userFriendlyMessage">用户友好提示</param>
    /// <param name="innerException">内部异常</param>
    public BusinessException(string errorCode, string userFriendlyMessage, Exception innerException)
        : base(userFriendlyMessage, innerException)
    {
        ErrorCode = errorCode;
        UserFriendlyMessage = userFriendlyMessage;
        InternalDetails = innerException.Message;
    }
}
