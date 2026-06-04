using BuildingBlocks.Core.Common;
using BuildingBlocks.Logging.Core;
using Serilog;

namespace BuildingBlocks.Logging.Extensions;

/// <summary>
/// Serilog LoggerConfiguration 链式扩展方法
/// 【设计背景】补充原生Serilog API的不足，提供更简洁的标准化属性添加方式，避免重复编写.Enrich.WithProperty()
/// 【核心职责】为LoggerConfiguration提供直接的链式扩展，快速添加日志类型、模块等标准化分类属性
/// 【与LoggerEnrichmentConfigurationExtensions的区别】
/// - 本类直接扩展LoggerConfiguration，支持更流畅的链式调用: logger.WithLogType("Request")
/// - 另一类扩展LoggerEnrichmentConfiguration，用于Enrich()方法内部: enrich => enrich.WithLogType("Request")
/// 【设计目标】统一日志属性命名，避免硬编码，提升开发体验，保证全系统日志分类标准一致
/// 【使用场景】在构建LoggerConfiguration时，快速添加全局或局部的标准化分类属性
/// </summary>
public static class LoggerConfigurationEnrichmentExtensions
{
    /// <summary>
    /// 为整个日志配置添加全局标准化的"日志类型"属性
    /// 【属性名】LoggingConstants.PropertyNames.LogType
    /// 【核心作用】全局统一标记所有日志的类型，用于日志分类、过滤和统计分析
    /// 【推荐值】优先使用LoggingConstants.LogTypes中定义的常量 (Request/Exception/Audit/Business/Performance/System)
    /// 【链式示例】Log.Logger = new LoggerConfiguration().WithLogType(LoggingConstants.LogTypes.System).Build();
    /// </summary>
    /// <param name="loggerConfiguration">Serilog日志配置对象，不可为null</param>
    /// <param name="logType">日志类型标识，不可为null或空白字符串</param>
    /// <returns>当前LoggerConfiguration实例，支持链式调用</returns>
    /// <exception cref="ArgumentNullException">当loggerConfiguration为null时抛出</exception>
    /// <exception cref="ArgumentException">当logType为null或空白字符串时抛出</exception>
    public static LoggerConfiguration WithLogType(
        this LoggerConfiguration loggerConfiguration, string logType)
    {
        Guard.NotNull(loggerConfiguration);
        Guard.NotNullOrWhiteSpace(logType);

        return loggerConfiguration.Enrich.WithProperty(LoggingConstants.PropertyNames.LogType, logType);
    }

    /// <summary>
    /// 为整个日志配置添加全局标准化的"业务模块"属性
    /// 【属性名】LoggingConstants.PropertyNames.Module
    /// 【核心作用】全局统一标记所有日志所属的业务模块，用于按模块排查问题、统计各模块日志量和错误率
    /// 【推荐值】使用业务模块的英文名称 (如"Orders"、"Users"、"Payments"、"Inventory")
    /// 【链式示例】Log.Logger = new LoggerConfiguration().WithModule("Orders").Build();
    /// </summary>
    /// <param name="loggerConfiguration">Serilog日志配置对象，不可为null</param>
    /// <param name="moduleName">业务模块名称，不可为null或空白字符串</param>
    /// <returns>当前LoggerConfiguration实例，支持链式调用</returns>
    /// <exception cref="ArgumentNullException">当loggerConfiguration为null时抛出</exception>
    /// <exception cref="ArgumentException">当moduleName为null或空白字符串时抛出</exception>
    public static LoggerConfiguration WithModule(
        this LoggerConfiguration loggerConfiguration, string moduleName)
    {
        Guard.NotNull(loggerConfiguration);
        Guard.NotNullOrWhiteSpace(moduleName);

        return loggerConfiguration.Enrich.WithProperty(LoggingConstants.PropertyNames.Module, moduleName);
    }
}
