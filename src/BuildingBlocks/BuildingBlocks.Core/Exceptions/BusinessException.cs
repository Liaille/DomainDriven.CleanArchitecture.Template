using BuildingBlocks.Core.Errors;

namespace BuildingBlocks.Core.Exceptions;

/// <summary>
/// 业务异常
/// </summary>
/// <remarks>
/// <para>适用于业务逻辑校验、业务状态异常相关场景，遵循统一错误码规范 (前缀: B)</para>
/// <para>适用场景: 业务规则校验失败、资源不存在、操作受限、数据状态异常、正常业务流程拦截等</para>
/// <para>补充说明: 该异常属于常规业务流程范畴，默认不触发系统级别告警</para>
/// </remarks>
public class BusinessException : AppException
{
    /// <summary>
    /// 使用预构建的错误信息创建业务异常
    /// </summary>
    /// <param name="error">错误核心对象 (包含错误码、类型、详情等信息)</param>
    /// <param name="innerException">引发当前异常的内部异常 (可选，用于异常链路追踪)</param>
    public BusinessException(Error error, Exception? innerException = null) 
        : base(error, innerException)
    {
    }

    /// <summary>
    /// 通过错误码直接创建业务异常
    /// </summary>
    /// <param name="code">10位统一错误码 (必须以B开头)</param>
    /// <param name="details">错误详情列表 (可选，用于多字段校验、全量错误返回)</param>
    /// <param name="innerException">引发当前异常的内部异常 (可选)</param>
    public BusinessException(string code, IReadOnlyList<ErrorDetail>? details = null, Exception? innerException = null) 
        : base(Error.Business(code, details), innerException)
    {
    }
}
