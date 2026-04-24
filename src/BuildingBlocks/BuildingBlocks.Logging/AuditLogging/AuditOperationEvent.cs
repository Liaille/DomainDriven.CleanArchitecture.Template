using System.Text.Json.Serialization;

namespace BuildingBlocks.Logging.AuditLogging;

/// <summary>
/// 通用审计事件完整模型（全字段上下文）
/// </summary>
[JsonSerializable(typeof(AuditOperationEvent))]
public partial record AuditOperationEvent
{
    /// <summary>
    /// 操作类型（Create/Update/Delete/Custom）
    /// </summary>
    public string OperationType { get; init; } = string.Empty;

    /// <summary>
    /// 操作实体类型
    /// </summary>
    public string? EntityType { get; init; }

    /// <summary>
    /// 实体唯一标识 ID
    /// </summary>
    public string? EntityId { get; init; }

    /// <summary>
    /// 修改前数据
    /// </summary>
    public object? OldValue { get; init; }

    /// <summary>
    /// 修改后数据
    /// </summary>
    public object? NewValue { get; init; }

    /// <summary>
    /// 操作备注描述
    /// </summary>
    public string? Description { get; init; }

    /// <summary>
    /// 操作人唯一 ID
    /// </summary>
    public object? OperatorId { get; init; }

    /// <summary>
    /// 操作人账号名称
    /// </summary>
    public string? OperatorName { get; init; }

    /// <summary>
    /// 操作 UTC 时间
    /// </summary>
    public DateTime OperationUtcTime { get; init; } = DateTime.UtcNow;

    /// <summary>
    /// 请求客户端 IP 地址
    /// </summary>
    public string? ClientIp { get; init; }

    /// <summary>
    /// 请求唯一追踪 ID
    /// </summary>
    public string? RequestTraceId { get; init; }

    /// <summary>
    /// 租户唯一标识 ID
    /// </summary>
    public string? TenantId { get; init; }

    /// <summary>
    /// 租户名称
    /// </summary>
    public string? TenantName { get; init; }
}
