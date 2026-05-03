using BuildingBlocks.Core.ErrorCodes;
using BuildingBlocks.Core.Exceptions;

namespace Domain.Shared.Exceptions;

/// <summary>
/// 领域层验证异常
/// <para>适用场景：领域实体、值对象、聚合根的业务规则校验失败</para>
/// <para>错误码：10000 InvalidParameter</para>
/// <para>设计原则：纯数据载体，仅用于领域规则不满足时抛出</para>
/// </summary>
public class DomainValidationException : BusinessException
{
    /// <summary>
    /// 初始化领域验证异常
    /// </summary>
    /// <param name="technicalMessage">技术描述信息（仅用于日志/调试）</param>
    public DomainValidationException(string technicalMessage)
        : base(SystemErrorCodes.InvalidParameter, technicalMessage)
    {
    }

    /// <summary>
    /// 初始化领域验证异常（包含内部异常）
    /// </summary>
    /// <param name="technicalMessage">技术描述信息</param>
    /// <param name="innerException">内部异常</param>
    public DomainValidationException(string technicalMessage, Exception innerException)
        : base(SystemErrorCodes.InvalidParameter, technicalMessage, innerException)
    {
    }
}
