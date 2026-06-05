using BuildingBlocks.ExecutionContext.Abstractions.ContextData;
using BuildingBlocks.ExecutionContext.Abstractions.NullObjects;
using System.Collections.Immutable;

namespace BuildingBlocks.ExecutionContext.Abstractions.CoreInterfaces;

/// <summary>
/// 当前请求上下文访问器抽象接口 (业务层可直接依赖)
/// </summary>
/// <remarks>
/// 【线程安全】: 默认承诺异步线程安全
/// 【职责】: 提供当前请求的链路追踪、请求ID、环境信息等只读访问
/// 【只读规则】: 所有属性均为不可变，禁止外部直接修改
/// </remarks>
public interface IRequestContext
{
    /// <summary>
    /// 当前请求是否可用
    /// </summary>
    /// <value>
    /// true表示链路ID非NullTraceId
    /// false表示其他情况
    /// </value>
    bool IsAvailable
    {
        get
        {
            return TraceInfo.TraceId != NullConstants.NullTraceId;
        }
    }

    /// <summary>
    /// 当前链路追踪信息 (永远不会为null，未初始化时返回NullRequestTraceInfo.Instance)
    /// </summary>
    RequestTraceInfo TraceInfo { get; }

    /// <summary>
    /// 当前链路ID (永远不会为null/空字符串/空白字符，未初始化时返回NullConstants.NullTraceId)
    /// </summary>
    string TraceId => TraceInfo.TraceId;

    /// <summary>
    /// 当前请求ID (可选，由请求ID生成器生成，通常为GUID或雪花ID)
    /// </summary>
    string? RequestId { get; }

    /// <summary>
    /// 当前环境名称 (如Development、Test、Staging、Production，可选)
    /// </summary>
    string? EnvironmentName { get; }

    /// <summary>
    /// 当前请求的客户端IP (可选，注意: 如果是代理/负载均衡，需要由实现层正确获取真实IP)
    /// </summary>
    string? ClientIp { get; }

    /// <summary>
    /// 当前请求的用户代理 (User-Agent，可选)
    /// </summary>
    string? UserAgent { get; }

    /// <summary>
    /// 不可变扩展属性字典 (用于存放自定义请求上下文数据，永远不会为null)
    /// </summary>
    IImmutableDictionary<string, object?> ExtraProperties { get; }
}
