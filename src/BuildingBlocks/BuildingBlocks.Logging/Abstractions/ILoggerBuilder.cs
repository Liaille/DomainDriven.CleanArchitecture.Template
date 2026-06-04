using BuildingBlocks.Logging.Core;
using Serilog.Events;

namespace BuildingBlocks.Logging.Abstractions;

/// <summary>
/// 日志系统统一构建器接口
/// 【设计模式】采用建造者模式，提供流畅的链式配置API，统一管理所有日志配置项
/// 【核心职责】日志系统的统一配置入口，负责整合日志级别、输出目标、丰富器、过滤器等所有配置，最终构建出Serilog.ILogger实例
/// 【设计目标】
/// 1. 封装Serilog原生API，隔离业务层与具体日志框架的耦合
/// 2. 提供标准化的配置接口，保证所有微服务的日志配置风格一致
/// 3. 支持从配置文件加载配置，同时保留代码级配置能力
/// 4. 内置敏感数据过滤、结构化日志等企业级能力
/// 【强制规范】所有日志配置必须通过此接口完成，禁止业务代码直接使用Serilog原生LoggerConfiguration
/// 【使用方式】采用链式调用，最后调用Build()方法生成最终的ILogger实例
/// </summary>
public interface ILoggerBuilder
{
    /// <summary>
    /// 从配置对象加载所有日志配置
    /// 【核心作用】实现配置与代码分离，支持通过appsettings.json配置所有日志参数
    /// 【加载内容】全局最小级别、覆盖级别、输出目标、敏感过滤规则、丰富器配置等
    /// </summary>
    /// <param name="options">日志系统全局配置对象，不可为null</param>
    /// <returns>当前构建器实例，支持链式调用</returns>
    ILoggerBuilder ConfigureFromOptions(LoggingOptions options);

    /// <summary>
    /// 设置全局最小日志级别
    /// 【过滤规则】低于此级别的所有日志事件都会被直接丢弃，不会输出到任何Sink
    /// 【默认值】开发环境为Debug，生产环境为Information
    /// </summary>
    /// <param name="level">全局最小日志级别</param>
    /// <returns>当前构建器实例，支持链式调用</returns>
    ILoggerBuilder MinimumLevel(LogEventLevel level);

    /// <summary>
    /// 为指定命名空间覆盖最小日志级别
    /// 【使用场景】针对特定模块或第三方库调整日志级别，例如将Microsoft.AspNetCore的日志级别设置为Warning以减少噪音
    /// 【优先级】覆盖级别优先级高于全局最小级别
    /// </summary>
    /// <param name="source">命名空间前缀 (如"Microsoft.AspNetCore")</param>
    /// <param name="level">该命名空间的最小日志级别</param>
    /// <returns>当前构建器实例，支持链式调用</returns>
    ILoggerBuilder MinimumLevelOverride(string source, LogEventLevel level);

    /// <summary>
    /// 添加日志丰富器
    /// 【核心作用】为所有日志事件添加上下文属性，增强日志的可追溯性
    /// 【内置丰富器】ContextEnricher、EnvironmentEnricher、UserContextEnricher等
    /// </summary>
    /// <param name="enricher">日志丰富器实例，不可为null</param>
    /// <returns>当前构建器实例，支持链式调用</returns>
    ILoggerBuilder AddEnricher(ILogEnricher enricher);

    /// <summary>
    /// 添加控制台输出目标
    /// 【使用场景】开发环境调试、容器化部署标准输出
    /// 【输出格式】使用LoggingConstants.DefaultOutputTemplate统一模板
    /// </summary>
    /// <returns>当前构建器实例，支持链式调用</returns>
    ILoggerBuilder AddConsole();

    /// <summary>
    /// 添加调试输出目标
    /// 【使用场景】Visual Studio调试窗口输出
    /// 【注意事项】仅在开发环境生效，生产环境建议禁用
    /// </summary>
    /// <returns>当前构建器实例，支持链式调用</returns>
    ILoggerBuilder AddDebug();

    /// <summary>
    /// 添加文件输出目标
    /// 【核心特性】支持按时间/大小自动滚动、文件压缩、自动清理过期文件
    /// 【输出内容】所有级别≥全局最小级别的日志
    /// </summary>
    /// <param name="path">日志文件路径，支持相对路径和绝对路径</param>
    /// <param name="options">文件日志配置选项，不可为null</param>
    /// <returns>当前构建器实例，支持链式调用</returns>
    ILoggerBuilder AddFile(string path, FileLoggingOptions options);

    /// <summary>
    /// 添加错误日志单独文件输出目标
    /// 【核心作用】将Error及以上级别的日志输出到单独的文件，方便快速定位严重问题
    /// 【过滤规则】仅输出LogEventLevel.Error和LogEventLevel.Fatal级别的日志
    /// </summary>
    /// <param name="path">错误日志文件路径</param>
    /// <param name="options">文件日志配置选项，不可为null</param>
    /// <returns>当前构建器实例，支持链式调用</returns>
    ILoggerBuilder AddErrorFile(string path, FileLoggingOptions options);

    /// <summary>
    /// 注册敏感数据过滤器
    /// 【核心作用】所有日志输出前自动过滤敏感数据，防止隐私信息泄露
    /// 【强制要求】生产环境必须注册此过滤器，满足数据合规要求
    /// </summary>
    /// <param name="filter">敏感数据过滤器实例，不可为null</param>
    /// <returns>当前构建器实例，支持链式调用</returns>
    ILoggerBuilder AddSensitiveDataFilter(ISensitiveDataFilter filter);

    /// <summary>
    /// 构建最终的Serilog.ILogger实例
    /// 【调用时机】所有配置完成后调用，是构建流程的最后一步
    /// 【注意事项】调用后所有配置将被冻结，无法再修改
    /// </summary>
    /// <returns>配置完成的Serilog.ILogger实例</returns>
    Serilog.ILogger Build();
}
