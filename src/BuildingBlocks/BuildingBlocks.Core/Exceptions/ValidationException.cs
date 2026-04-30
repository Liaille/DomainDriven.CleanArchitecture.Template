using BuildingBlocks.Core.ErrorCodes;

namespace BuildingBlocks.Core.Exceptions;

/// <summary>
/// 参数校验异常
/// <para>适用场景: 请求参数格式、范围、必填项等校验失败</para>
/// <para>强制约束: 不触发告警，仅记录 Warn 级别日志</para>
/// </summary>
public class ValidationException : BusinessException
{
    /// <summary>
    /// 参数字典，存储每个参数的错误信息
    /// </summary>
    public Dictionary<string, List<string>> Errors { get; }

    /// <summary>
    /// 初始化参数校验异常
    /// </summary>
    /// <param name="message">异常描述消息</param>
    public ValidationException(string message)
        : base(SystemErrorCodes.InvalidParameter, message)
    {
        Errors = [];
    }

    /// <summary>
    /// 初始化带参数字典的参数校验异常
    /// </summary>
    /// <param name="message">异常描述消息</param>
    /// <param name="errors">参数字典</param>
    public ValidationException(string message, Dictionary<string, List<string>> errors)
        : base(SystemErrorCodes.InvalidParameter, message)
    {
        Errors = errors ?? [];
    }

    /// <summary>
    /// 初始化带内部异常的参数校验异常
    /// </summary>
    /// <param name="message">异常描述消息</param>
    /// <param name="innerException">内部异常对象</param>
    public ValidationException(string message, Exception innerException)
        : base(SystemErrorCodes.InvalidParameter, message, innerException)
    {
        Errors = [];
    }
}
