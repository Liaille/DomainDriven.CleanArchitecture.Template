using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using System.Reflection;

namespace BuildingBlocks.Logging.Serilog;

/// <summary>
/// Serilog 全局配置器 (JSON结构化日志统一底座，整合全部持久化输出端)
/// <para>包含基础级别过滤、上下文增强、应用元数据、多输出链路统一组装</para>
/// </summary>
public static class SerilogConfigurator
{
    //public static ILogger ConfigureGlobalSerilog(IConfiguration configuration, string applicationName)
    //{
    //    ArgumentNullException.ThrowIfNull(configuration, nameof(configuration));
    //    ArgumentException.ThrowIfNullOrEmpty(applicationName, nameof(applicationName));

    //    var loggerConfig = new LoggerConfiguration()
    //        .MinimumLevel.Debug()
    //        .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    //        .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
    //        .Enrich.FromLogContext()
    //        .Enrich.WithProperty("ApplicationName", applicationName)
    //        .Enrich.WithProperty("Environment", Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development")
    //        .Enrich.WithProperty("AppVersion", Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "1.0.0.0");

    //    //loggerConfig = LogSinkConfigurator
        
    //}
}
