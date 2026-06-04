using BuildingBlocks.Core.Common;
using BuildingBlocks.Logging.Abstractions;
using Serilog.Core;
using Serilog.Events;

namespace BuildingBlocks.Logging.Enrichers;

/// <summary>
/// 基础设施环境信息丰富器
/// 【核心职责】为日志事件添加服务器、进程、运行时等基础设施层面的上下文属性
/// 【设计特点】
/// 1. 基础设施视角: 提供问题定位所需的机器和进程级信息
/// 2. 可扩展: 支持添加自定义环境属性，满足不同部署环境的需求
/// 3. 高性能: 所有属性值在构造时一次性获取，避免每次日志输出重复计算
/// 【关键价值】当出现服务器或进程级别的问题时，可以快速定位到具体的机器和进程
/// 【使用场景】全局注册，生产环境建议启用，开发环境可选择性禁用
/// </summary>
/// <remarks>
/// 初始化基础设施环境丰富器
/// </remarks>
/// <param name="environmentProperties">自定义环境属性，为null时使用空字典</param>
public class EnvironmentEnricher(Dictionary<string, string>? environmentProperties = null) : ILogEnricher
{
    /// <summary>
    /// 自定义环境属性字典 (键: 属性名，值: 属性值)
    /// </summary>
    private readonly Dictionary<string, string> _environmentProperties = environmentProperties ?? [];

    /// <summary>
    /// 丰富器唯一标识名称
    /// </summary>
    public string Name => "EnvironmentEnricher";

    /// <summary>
    /// 为日志事件添加基础设施环境属性
    /// 【默认添加属性】
    /// 1. MachineName: 服务器机器名
    /// 2. ProcessId: 当前进程ID
    /// 【自定义属性】添加构造函数传入的所有自定义环境属性
    /// </summary>
    /// <param name="logEvent">待丰富的日志事件，不可为null</param>
    /// <param name="propertyFactory">日志属性工厂，不可为null</param>
    /// <exception cref="ArgumentNullException">当logEvent或propertyFactory为null时抛出</exception>
    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        Guard.NotNull(logEvent);
        Guard.NotNull(propertyFactory);

        // 添加服务器机器名 (用于定位问题发生的具体服务器)
        var machineName = Environment.MachineName;
        logEvent.AddOrUpdateProperty(
            propertyFactory.CreateProperty("MachineName", machineName));

        // 添加当前进程ID (用于定位问题发生的具体进程)
        logEvent.AddOrUpdateProperty(
            propertyFactory.CreateProperty("ProcessId", Environment.ProcessId));

        // 添加所有自定义环境属性 (如版本号、集群名称、区域等)
        foreach (var property in _environmentProperties)
        {
            logEvent.AddOrUpdateProperty(
                propertyFactory.CreateProperty(property.Key, property.Value));
        }
    }
}

