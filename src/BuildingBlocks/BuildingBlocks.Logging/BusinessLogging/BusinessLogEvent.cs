using System.Text.Json.Serialization;

namespace BuildingBlocks.Logging.BusinessLogging;

/// <summary>
/// 业务日志事件记录
/// </summary>
[JsonSerializable(typeof(BusinessLogEvent))]
public partial record BusinessLogEvent
{
    /// <summary>
    /// 业务事件唯一标识名称
    /// </summary>
    public string EventName { get; init; } = string.Empty;

    /// <summary>
    /// 事件文字描述
    /// </summary>
    public string? Description { get; init; }

    /// <summary>
    /// 事件附加业务数据
    /// </summary>
    public object? BusinessData { get; init; }

    /// <summary>
    /// 操作人唯一 ID
    /// </summary>
    public string? OperatorId { get; init; }

    /// <summary>
    /// 操作人账号名称
    /// </summary>
    public string? OperatorName { get; init; }

    /// <summary>
    /// 当前请求唯一 ID
    /// </summary>
    public string? RequestId { get; init; }

    /// <summary>
    /// 分布式追踪 Trace ID
    /// </summary>
    public string? TraceId { get; init; }

    /// <summary>
    /// 客户端 IP 地址
    /// </summary>
    public string? ClientIp { get; init; }

    /// <summary>
    /// 租户唯一标识 ID
    /// </summary>
    public string? TenantId { get; init; }

    /// <summary>
    /// 租户名称
    /// </summary>
    public string? TenantName { get; init; }

    /// <summary>
    /// 事件 UTC 时间戳
    /// </summary>
    public DateTime UtcTimestamp { get; init; }
}
