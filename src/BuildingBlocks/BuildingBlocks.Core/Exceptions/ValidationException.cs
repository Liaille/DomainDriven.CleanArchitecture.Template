using BuildingBlocks.Core.ErrorCodes;

namespace BuildingBlocks.Core.Exceptions;

/// <summary>
/// 参数校验异常
/// <para>使用场景: 请求参数无效、格式错误、必填项缺失、范围越界</para>
/// <para>错误码: 10000 InvalidParameter</para>
/// </summary>
public class ValidationException : BusinessException
{
    /// <summary>
    /// 校验错误集合
    /// <para>Key：参数名</para>
    /// <para>Value：该参数的所有错误信息列表</para>
    /// </summary>
    public Dictionary<string, List<string>> Errors { get; }

    /// <summary>
    /// 初始化参数校验异常
    /// </summary>
    /// <param name="message">通用提示</param>
    public ValidationException(string message)
        : base(SystemErrorCodes.InvalidParameter, message)
    {
        Errors = [];
    }

    /// <summary>
    /// 初始化参数校验异常（带详细错误字典）
    /// </summary>
    /// <param name="message">通用提示</param>
    /// <param name="errors">详细错误集合</param>
    public ValidationException(string message, Dictionary<string, List<string>> errors)
        : base(SystemErrorCodes.InvalidParameter, message)
    {
        Errors = errors ?? [];
    }

    /// <summary>
    /// 初始化参数校验异常（带内部异常）
    /// </summary>
    /// <param name="message">通用提示</param>
    /// <param name="innerException">内部异常</param>
    public ValidationException(string message, Exception innerException)
        : base(SystemErrorCodes.InvalidParameter, message, innerException)
    {
        Errors = [];
    }
}
