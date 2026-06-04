using BuildingBlocks.Core.Common;
using BuildingBlocks.Logging.Abstractions;
using BuildingBlocks.Logging.Core;
using Serilog.Core;
using Serilog.Events;
using System.Diagnostics;

namespace BuildingBlocks.Logging.Enrichers;

/// <summary>
/// 核心上下文信息丰富器
/// 【核心职责】为所有日志事件添加分布式链路追踪、运行环境、应用标识等全局核心上下文属性
/// 【设计特点】
/// 1. 全链路强制: 所有日志事件都会自动添加这些属性，保证日志的可追溯性
/// 2. 标准化字段: 所有属性名统一使用LoggingConstants中定义的常量，保证全系统字段一致
/// 3. 容错处理: 所有属性值获取失败时默认返回"Unknown"，避免日志输出异常
/// 【关键价值】是分布式系统问题定位的基础，通过TraceId可以串联起跨服务的全链路日志
/// 【使用场景】全局注册，所有日志事件自动应用
/// </summary>
/// <remarks>
/// 初始化核心上下文丰富器
/// </remarks>
/// <param name="environment">运行环境名称，为null时默认使用"Unknown"</param>
/// <param name="applicationName">应用程序名称，为null时默认使用"Unknown"</param>
public class ContextEnricher(string environment, string applicationName) : ILogEnricher
{
    /// <summary>
    /// 当前运行环境名称 (如Development/Production)
    /// </summary>
    private readonly string _environment = environment ?? "Unknown";

    /// <summary>
    /// 当前应用程序名称 (用于区分不同微服务)
    /// </summary>
    private readonly string _applicationName = applicationName ?? "Unknown";

    /// <summary>
    /// 丰富器唯一标识名称
    /// </summary>
    public string Name => "ContextEnricher";

    /// <summary>
    /// 为日志事件添加核心上下文属性
    /// 【添加属性】
    /// 1. TraceId: 分布式链路追踪ID，来自.NET内置Activity.Current
    /// 2. Environment: 运行环境名称
    /// 3. ApplicationName: 应用程序名称
    /// </summary>
    /// <param name="logEvent">待丰富的日志事件，不可为null</param>
    /// <param name="propertyFactory">日志属性工厂，不可为null</param>
    /// <exception cref="ArgumentNullException">当logEvent或propertyFactory为null时抛出</exception>
    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        Guard.NotNull(logEvent);
        Guard.NotNull(propertyFactory);

        // 添加分布式链路追踪ID (全链路问题定位的核心)
        var traceId = Activity.Current?.Id ?? "Unknown";
        logEvent.AddOrUpdateProperty(
            propertyFactory.CreateProperty(LoggingConstants.PropertyNames.TraceId, traceId));

        // 添加运行环境信息 (用于区分不同环境的日志)
        logEvent.AddOrUpdateProperty(
            propertyFactory.CreateProperty(LoggingConstants.PropertyNames.Environment, _environment));

        // 添加应用程序名称 (用于区分不同微服务的日志)
        logEvent.AddOrUpdateProperty(
            propertyFactory.CreateProperty("ApplicationName", _applicationName));
    }
}
