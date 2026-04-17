namespace BuildingBlocks.Core.Context;

/// <summary>
/// 请求上下文 (用于记录一次请求的全局标识信息)
/// </summary>
public interface IRequestContext
{
    /// <summary>
    /// 当前请求唯一 ID
    /// </summary>
    Guid RequestId { get; }

    /// <summary>
    /// 分布式追踪 Trace ID
    /// </summary>
    string? TraceId { get; }
}
