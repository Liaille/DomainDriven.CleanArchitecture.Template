using BuildingBlocks.Core.Exceptions;
using Domain.Shared.Errors;

namespace Domain.Shared.Exceptions;

/// <summary>
/// 业务规则异常
/// </summary>
public class BusinessException : AppServiceException
{
    /// <summary>
    /// 错误码
    /// </summary>
    public ErrorCode ErrorCode { get; }

    public BusinessException(ErrorCode errorCode)
        : base(ErrorMessage.GetMessage(errorCode))
    {
        ErrorCode = errorCode;
    }

    public BusinessException(ErrorCode errorCode, string message)
        : base(message)
    {
        ErrorCode = errorCode;
    }
}
