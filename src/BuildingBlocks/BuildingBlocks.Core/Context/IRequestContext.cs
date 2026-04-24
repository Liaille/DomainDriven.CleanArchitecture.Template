namespace BuildingBlocks.Core.Context;

/// <summary>
/// 请求上下文 (用于记录一次请求的全局标识信息)
/// </summary>
public interface IRequestContext
{
    /// <summary>
    /// 当前请求唯一 ID (内部分发 ID，用于应用内日志关联)
    /// </summary>
    Guid RequestId { get; }

    /// <summary>
    /// 分布式追踪 Trace ID (全链路追踪 ID，通常来自 W3C Trace Context)
    /// </summary>
    string? TraceId { get; }

    /// <summary>
    /// 客户端 IP 地址 (支持代理场景，优先读取 X-Forwarded-For)
    /// </summary>
    string? ClientIp { get; }

    /// <summary>
    /// 请求开始UTC时间戳 (用于性能监控、慢请求追踪与超时分析)
    /// </summary>
    DateTime StartTime { get; }
}
