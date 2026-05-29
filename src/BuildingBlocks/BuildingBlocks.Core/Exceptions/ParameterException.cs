using BuildingBlocks.Core.Errors;

namespace BuildingBlocks.Core.Exceptions;

/// <summary>
/// 参数异常
/// </summary>
/// <remarks>
/// <para>适用于请求参数相关的错误场景，遵循统一错误码规范 (前缀：P)</para>
/// <para>适用场景: 参数缺失、参数格式非法、参数值超出范围、数据校验不通过等</para>
/// </remarks>
public class ParameterException : AppException
{
    /// <summary>
    /// 使用预构建的错误信息创建参数异常
    /// </summary>
    /// <param name="error">错误核心对象 (包含错误码、类型、详情等信息)</param>
    /// <param name="innerException">引发当前异常的内部异常 (可选，用于异常链路追踪)</param>
    public ParameterException(Error error, Exception? innerException = null)
        : base(error, innerException)
    {
    }

    /// <summary>
    /// 通过错误码直接创建参数异常
    /// </summary>
    /// <param name="code">10位统一错误码 (必须以P开头)</param>
    /// <param name="details">子错误详情列表 (可选，用于多字段校验、全量错误返回)</param>
    /// <param name="innerException">引发当前异常的内部异常 (可选)</param>
    public ParameterException(string code, IReadOnlyList<ErrorDetail>? details = null, Exception? innerException = null)
        : base(Error.Parameter(code, details), innerException)
    {
    }
}
