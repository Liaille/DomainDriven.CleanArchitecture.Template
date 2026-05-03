namespace BuildingBlocks.Core.Exceptions;

/// <summary>
/// 业务异常基类 (所有可预期异常的统一基类)
/// <para>核心职责: 标识这是一个可预期的业务/领域/参数/第三方/认证授权异常，并承载唯一错误标识</para>
/// <para>设计原则: 纯数据载体，不包含任何 UI 文案逻辑、日志逻辑</para>
/// <para>强制约束: 不触发告警，仅记录 Info/Warn 级别日志</para>
/// </summary>
public class BusinessException : Exception
{
    /// <summary>
    /// 业务错误码 (唯一错误标识，int 类型，统一错误码体系)
    /// </summary>
    public int ErrorCode { get; }

    /// <summary>
    /// 初始化业务异常
    /// </summary>
    /// <param name="errorCode">业务错误码 (int 类型)</param>
    /// <param name="technicalMessage">技术描述消息 (仅用于基类 Exception.Message，满足调试需求)</param>
    public BusinessException(int errorCode, string technicalMessage)
        : base(technicalMessage)
    {
        ErrorCode = errorCode;
    }

    /// <summary>
    /// 初始化带内部异常的业务异常
    /// </summary>
    /// <param name="errorCode">业务错误码 (int 类型)</param>
    /// <param name="technicalMessage">技术描述消息 (仅用于基类 Exception.Message，满足调试需求)</param>
    /// <param name="innerException">内部异常</param>
    public BusinessException(int errorCode, string technicalMessage, Exception innerException)
        : base(technicalMessage, innerException)
    {
        ErrorCode = errorCode;
    }
}
