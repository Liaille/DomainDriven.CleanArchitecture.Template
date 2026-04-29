namespace BuildingBlocks.Core.Exceptions;

/// <summary>
/// 参数校验异常
/// 适用场景：请求参数格式、范围、必填项等校验失败
/// </summary>
public class ValidationException : Exception
{
    /// <summary>
    /// 参数字典，存储每个参数的错误信息
    /// </summary>
    public Dictionary<string, List<string>> Errors { get; }

    /// <summary>
    /// 初始化参数校验异常
    /// </summary>
    /// <param name="message">异常描述消息</param>
    public ValidationException(string message) : base(message)
    {
        Errors = [];
    }

    /// <summary>
    /// 初始化带参数字典的参数校验异常
    /// </summary>
    /// <param name="message">异常描述消息</param>
    /// <param name="errors">参数字典</param>
    public ValidationException(string message, Dictionary<string, List<string>> errors)
        : base(message)
    {
        Errors = errors ?? [];
    }

    /// <summary>
    /// 初始化带内部异常的参数校验异常
    /// </summary>
    /// <param name="message">异常描述消息</param>
    /// <param name="innerException">内部异常对象</param>
    public ValidationException(string message, Exception innerException)
        : base(message, innerException)
    {
        Errors = [];
    }
}
