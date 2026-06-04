using Microsoft.Extensions.Logging;
using Serilog.Events;

namespace BuildingBlocks.Logging.Core;

/// <summary>
/// 日志级别统一映射器
/// 【核心职责】解决.NET原生日志(Microsoft.Extensions.Logging)与Serilog日志系统的级别定义不兼容问题
/// 提供三大核心能力: 配置字符串转枚举、跨框架级别双向转换、环境差异化默认级别
/// 【强制规范】所有日志级别转换必须通过此类完成，禁止业务代码中硬编码级别映射逻辑
/// </summary>
public static class LogLevelMapper
{
    /// <summary>
    /// 将配置文件中的字符串值转换为Serilog日志级别
    /// 【使用场景】从appsettings.json读取日志级别配置时使用，自动处理大小写不敏感
    /// </summary>
    /// <param name="logLevelString">日志级别字符串，支持: Verbose/Debug/Information/Warning/Error/Fatal</param>
    /// <returns>对应的Serilog日志级别；转换失败时默认返回Information，保证系统可用性</returns>
    public static LogEventLevel ToSerilogLevel(string logLevelString)
    {
        if (Enum.TryParse<LogEventLevel>(logLevelString, true, out var level))
            return level;

        // 转换失败时降级到Information，避免因配置错误导致日志完全不输出
        return LogEventLevel.Information;
    }

    /// <summary>
    /// 将.NET原生日志级别转换为Serilog日志级别
    /// 【映射说明】
    /// Trace → Verbose (最详细的跟踪日志)
    /// Debug → Debug (调试信息)
    /// Information → Information (常规业务日志)
    /// Warning → Warning (警告信息)
    /// Error → Error (业务错误)
    /// Critical → Fatal (系统致命错误)
    /// None → Fatal (最高级别，不输出任何日志，与.NET行为保持一致)
    /// </summary>
    /// <param name="logLevel">.NET原生Microsoft.Extensions.Logging日志级别</param>
    /// <returns>对应的Serilog日志级别；未知级别默认返回Information</returns>
    public static LogEventLevel ToSerilogLevel(LogLevel logLevel)
    {
        return logLevel switch
        {
            LogLevel.Trace => LogEventLevel.Verbose,
            LogLevel.Debug => LogEventLevel.Debug,
            LogLevel.Information => LogEventLevel.Information,
            LogLevel.Warning => LogEventLevel.Warning,
            LogLevel.Error => LogEventLevel.Error,
            LogLevel.Critical => LogEventLevel.Fatal,
            LogLevel.None => LogEventLevel.Fatal,
            _ => LogEventLevel.Information
        };
    }

    /// <summary>
    /// 将Serilog日志级别转换为.NET原生日志级别
    /// 【映射说明】
    /// Verbose → Trace (最详细的跟踪日志)
    /// Debug → Debug (调试信息)
    /// Information → Information (常规业务日志)
    /// Warning → Warning (警告信息)
    /// Error → Error (业务错误)
    /// Fatal → Critical (系统致命错误)
    /// </summary>
    /// <param name="logEventLevel">Serilog日志级别</param>
    /// <returns>对应的.NET原生Microsoft.Extensions.Logging日志级别；未知级别默认返回Information</returns>
    public static LogLevel ToMicrosoftLevel(LogEventLevel logEventLevel)
    {
        return logEventLevel switch
        {
            LogEventLevel.Verbose => LogLevel.Trace,
            LogEventLevel.Debug => LogLevel.Debug,
            LogEventLevel.Information => LogLevel.Information,
            LogEventLevel.Warning => LogLevel.Warning,
            LogEventLevel.Error => LogLevel.Error,
            LogEventLevel.Fatal => LogLevel.Critical,
            _ => LogLevel.Information
        };
    }

    /// <summary>
    /// 根据运行环境获取推荐的默认日志级别
    /// 【环境规则】
    /// - 开发环境(development/dev): Debug级别，输出完整调试信息，方便问题排查
    /// - 测试/预发布环境(test/staging): Information级别，输出核心业务日志，兼顾性能与可观测性
    /// - 生产环境及其他: Information级别，保证性能同时保留必要的问题定位信息
    /// </summary>
    /// <param name="environmentName">运行环境名称 (不区分大小写)</param>
    /// <returns>对应环境的推荐Serilog默认日志级别</returns>
    public static LogEventLevel GetDefaultLevelForEnvironment(string environmentName)
    {
        return environmentName.ToLowerInvariant() switch
        {
            "development" or "dev" => LogEventLevel.Debug,
            "test" or "staging" => LogEventLevel.Information,
            _ => LogEventLevel.Information
        };
    }
}
