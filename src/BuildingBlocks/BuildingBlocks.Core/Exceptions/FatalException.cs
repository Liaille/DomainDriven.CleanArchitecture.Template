using BuildingBlocks.Core.Errors;

namespace BuildingBlocks.Core.Exceptions;

/// <summary>
/// 致命异常
/// </summary>
/// <remarks>
/// <para>适用于应用系统致命级故障的错误场景，遵循统一错误码规范 (前缀: F)</para>
/// <para>适用场景: 核心数据库宕机、集群崩溃、主进程异常退出、核心服务不可用、系统级灾难性故障等</para>
/// <para>补充说明: 该异常属于最高级别系统错误，默认触发紧急告警(电话+短信)，需立即干预处理</para>
/// </remarks>
public class FatalException : AppException
{
    /// <summary>
    /// 使用预构建的错误信息创建致命异常
    /// </summary>
    /// <param name="error">错误核心对象 (包含错误码、类型、详情等信息)</param>
    /// <param name="innerException">引发当前异常的内部异常 (可选，用于异常链路追踪)</param>
    public FatalException(Error error, Exception? innerException = null) 
        : base(error, innerException)
    {
    }

    /// <summary>
    /// 通过错误码直接创建致命异常
    /// </summary>
    /// <param name="code">10位统一错误码 (必须以F开头)</param>
    /// <param name="details">错误详情列表 (可选，用于多字段校验、全量错误返回)</param>
    /// <param name="innerException">引发当前异常的内部异常 (可选)</param>
    public FatalException(string code, IReadOnlyList<ErrorDetail>? details = null, Exception? innerException = null) 
        : base(Error.Fatal(code, details), innerException)
    {
    }
}
