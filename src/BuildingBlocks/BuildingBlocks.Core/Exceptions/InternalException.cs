using BuildingBlocks.Core.Errors;

namespace BuildingBlocks.Core.Exceptions;

/// <summary>
/// 系统内部异常
/// </summary>
/// <remarks>
/// <para>适用于应用系统内部运行时相关的错误场景，遵循统一错误码规范 (前缀: S)</para>
/// <para>适用场景: 服务器内部错误、数据库异常、缓存异常、配置读取失败、框架运行错误、请求限流、资源耗尽等</para>
/// <para>补充说明: 该异常属于系统级错误，默认触发错误级别日志与自动化告警</para>
/// </remarks>
public class InternalException : AppException
{
    /// <summary>
    /// 使用预构建的错误信息创建应用内部异常
    /// </summary>
    /// <param name="error">错误核心对象 (包含错误码、类型、详情等信息)</param>
    /// <param name="innerException">引发当前异常的内部异常 (可选，用于异常链路追踪)</param>
    public InternalException(Error error, Exception? innerException = null) 
        : base(error, innerException)
    {
    }

    /// <summary>
    /// 通过错误码直接创建应用内部异常
    /// </summary>
    /// <param name="code">10位统一错误码 (必须以S开头)</param>
    /// <param name="details">错误详情列表 (可选，用于多字段校验、全量错误返回)</param>
    /// <param name="innerException">引发当前异常的内部异常 (可选)</param>
    public InternalException(string code, IReadOnlyList<ErrorDetail>? details = null, Exception? innerException = null) 
        : base(Error.System(code, details), innerException)
    {
    }
}
