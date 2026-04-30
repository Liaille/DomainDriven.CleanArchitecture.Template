using BuildingBlocks.Core.Exceptions;
using Domain.Shared.Errors;

namespace Domain.Shared.Exceptions;

/// <summary>
/// 业务规则异常
/// 适用场景：领域层业务规则校验失败，属于正常业务异常，不触发告警
/// 对应错误码：20000+ 业务错误码段
/// 强制约束：仅能在Domain层抛出，禁止在其他层抛出此异常
/// </summary>
public class DomainBusinessException : DomainException
{
    /// <summary>
    /// 错误码
    /// </summary>
    public ErrorCode ErrorCode { get; }

    /// <summary>
    /// 初始化业务规则异常
    /// </summary>
    /// <param name="errorCode">错误码枚举</param>
    public DomainBusinessException(ErrorCode errorCode)
        : base(ErrorMessage.GetMessage(errorCode))
    {
        ErrorCode = errorCode;
    }

    /// <summary>
    /// 初始化带自定义消息的业务规则异常
    /// </summary>
    /// <param name="errorCode">错误码枚举</param>
    /// <param name="message">自定义异常消息</param>
    public DomainBusinessException(ErrorCode errorCode, string message)
        : base(message)
    {
        ErrorCode = errorCode;
    }

    /// <summary>
    /// 初始化带内部异常的业务规则异常
    /// </summary>
    /// <param name="errorCode">错误码枚举</param>
    /// <param name="message">自定义异常消息</param>
    /// <param name="innerException">内部异常对象</param>
    public DomainBusinessException(ErrorCode errorCode, string message, Exception innerException)
        : base(message, innerException)
    {
        ErrorCode = errorCode;
    }
}
