using BuildingBlocks.Core.Exceptions;
using Domain.Shared.ErrorCodes;

namespace Domain.Shared.Exceptions;

/// <summary>
/// 业务规则异常
/// 适用场景: 领域层业务规则校验失败，属于正常业务异常，不触发告警
/// 对应错误码: 20000+ 业务错误码段
/// 强制约束: 仅能在Domain层抛出，禁止在其他层抛出此异常
/// </summary>
public class DomainBusinessException : BusinessException
{
    /// <summary>
    /// 初始化领域业务异常
    /// </summary>
    /// <param name="errorCode">业务错误码</param>
    /// <param name="userFriendlyMessage">用户友好提示</param>
    public DomainBusinessException(int errorCode, string userFriendlyMessage)
        : base(errorCode, userFriendlyMessage)
    {
    }

    /// <summary>
    /// 初始化带内部异常的领域业务异常
    /// </summary>
    /// <param name="errorCode">业务错误码</param>
    /// <param name="userFriendlyMessage">用户友好提示</param>
    /// <param name="innerException">内部异常</param>
    public DomainBusinessException(int errorCode, string userFriendlyMessage, Exception innerException)
        : base(errorCode, userFriendlyMessage, innerException)
    {
    }
}
