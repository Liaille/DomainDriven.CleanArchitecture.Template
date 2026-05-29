using BuildingBlocks.Core.Common;
using BuildingBlocks.Core.Errors;

namespace BuildingBlocks.Core.Exceptions;

/// <summary>
/// 第三方服务异常
/// </summary>
/// <remarks>
/// <para>适用于调用外部第三方服务相关的错误场景，遵循统一错误码规范 (前缀: T)</para>
/// <para>适用场景: 外部服务调用失败、第三方接口超时、云服务异常、推送渠道异常、依赖服务不可用等</para>
/// <para>扩展属性: ServiceName 用于快速定位故障的第三方服务名称</para>
/// </remarks>
public class ThirdPartyException : AppException
{
    /// <summary>
    /// 第三方服务名称
    /// </summary>
    /// <remarks>用于快速定位故障的外部服务，便于日志诊断和问题排查</remarks>
    public string ServiceName { get; }

    /// <summary>
    /// 使用预构建的错误信息创建第三方服务异常
    /// </summary>
    /// <param name="error">错误核心对象 (包含错误码、类型、详情等信息)</param>
    /// <param name="serviceName">第三方服务名称 (非空)</param>
    /// <param name="innerException">引发当前异常的内部异常 (可选，用于异常链路追踪)</param>
    public ThirdPartyException(Error error, string serviceName, Exception? innerException = null)
        : base(error, innerException)
    {
        Guard.NotNullOrWhiteSpace(serviceName);
        ServiceName = serviceName;
    }

    /// <summary>
    /// 通过错误码直接创建第三方服务异常
    /// </summary>
    /// <param name="code">10位统一错误码 (必须以T开头)</param>
    /// <param name="serviceName">第三方服务名称 (非空)</param>
    /// <param name="details">错误详情列表 (可选，用于多字段校验、全量错误返回)</param>
    /// <param name="innerException">引发当前异常的内部异常 (可选)</param>
    public ThirdPartyException(
        string code,
        string serviceName,
        IReadOnlyList<ErrorDetail>? details = null,
        Exception? innerException = null)
        : base(Error.ThirdParty(code, details), innerException)
    {
        Guard.NotNullOrWhiteSpace(serviceName);
        ServiceName = serviceName;
    }

    /// <summary>
    /// 重写字符串输出，追加第三方服务名称，提升日志可读性
    /// </summary>
    public override string ToString()
    {
        return $"{base.ToString()} | Service: {ServiceName}";
    }
}
