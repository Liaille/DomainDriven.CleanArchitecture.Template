using BuildingBlocks.Logging.AuditLogging;
using BuildingBlocks.Logging.BusinessLogging;
using BuildingBlocks.Logging.Configuration;
using BuildingBlocks.Logging.Sinks;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace BuildingBlocks.Logging.Serilog;

/// <summary>
/// Serilog 全局配置器 (JSON结构化日志统一底座，整合全部持久化输出端)
/// <para>包含基础级别过滤、上下文增强、应用元数据、多输出链路统一组装</para>
/// </summary>
public static class SerilogConfigurator
{
    /// <summary>
    /// 完整初始化全局Serilog结构化日志
    /// </summary>
    /// <param name="configuration">应用配置根对象</param>
    /// <param name="applicationName">当前应用唯一名称</param>
    /// <returns>全局可用Serilog日志实例</returns>
    public static ILogger ConfigureGlobalSerilog(IConfiguration configuration, string applicationName)
    {
        ArgumentNullException.ThrowIfNull(configuration, nameof(configuration));
        ArgumentException.ThrowIfNullOrEmpty(applicationName, nameof(applicationName));

        // 读取日志配置
        var logOptions = configuration.GetSection("Logging").Get<SerilogSinkOptions>() ?? new SerilogSinkOptions();

        var loggerConfig = new LoggerConfiguration()
            // 全局日志最低级别
            .MinimumLevel.Debug()
            // 过滤系统框架冗余日志
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
            // 开启全局日志上下文
            .Enrich.FromLogContext()
            // 应用固定全局属性
            .Enrich.WithProperty("ApplicationName", applicationName)
            .Enrich.WithProperty("Environment", Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development")
            .Enrich.WithProperty("AppVersion", Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "1.0.0.0")
            // 配置所有输出端
            .ConfigureAllSinks(logOptions);

        // 构建全局日志实例并赋值静态全局日志
        var logger = loggerConfig.CreateLogger();
        Log.Logger = logger;

        return logger;
    }
}

/// <summary>
/// 日志模块 JSON 序列化上下文 (AOT兼容)
/// </summary>
[JsonSerializable(typeof(AuditOperationEvent))]
[JsonSerializable(typeof(BusinessLogEvent))]
[JsonSerializable(typeof(Dictionary<string, object>))]
public partial class LoggingJsonContext : JsonSerializerContext
{
    
}
