using BuildingBlocks.Monitoring.Abstractions;
using BuildingBlocks.Monitoring.Alerts;
using BuildingBlocks.Monitoring.Configuration;
using BuildingBlocks.Monitoring.HealthChecks;
using BuildingBlocks.Monitoring.OpenTelemetry;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System.Text.Json;

// 解决命名冲突
using HealthStatus = Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus;

namespace BuildingBlocks.Monitoring.Extensions;

/// <summary>
/// 监控基础设施服务依赖注入注册扩展
/// </summary>
public static class MonitoringServiceCollectionExtensions
{
    /// <summary>
    /// 注册完整监控基础设施
    /// <para>包含 OpenTelemetry 链路追踪、健康检查、告警</para>
    /// </summary>
    /// <param name="services">服务集合</param>
    /// <param name="configureOptions">配置选项委托</param>
    /// <returns>链式返回服务集合</returns>
    public static IServiceCollection AddMonitoringInfrastructure(
        this IServiceCollection services,
        Action<MonitoringOptions> configureOptions)
    {
        // 配置选项
        var options = new MonitoringOptions();
        configureOptions(options);
        services.AddSingleton(options);

        // 注册核心服务
        services.AddScoped<ITracer, OpenTelemetryTracer>();
        services.AddScoped<IHealthCheckService, DefaultHealthCheckService>();
        services.AddScoped<IAlertService, DefaultAlertService>();

        // 注册 OpenTelemetry
        if (options.OpenTelemetry.Enabled)
        {
            services.AddOpenTelemetryInfrastructure(options.OpenTelemetry);
        }

        // 注册健康检查
        if (options.HealthChecks.Enabled)
        {
            services.AddHealthChecksInfrastructure(options.HealthChecks);
        }

        return services;
    }

    /// <summary>
    /// 注册 OpenTelemetry 基础设施
    /// </summary>
    private static IServiceCollection AddOpenTelemetryInfrastructure(
        this IServiceCollection services,
        OpenTelemetryOptions options)
    {
        services.AddOpenTelemetry()
            .ConfigureResource(resource => resource
                .AddService(options.ServiceName, serviceVersion: options.ServiceVersion)
                .AddEnvironmentVariableDetector())
            .WithTracing(tracing =>
            {
                tracing
                    .SetSampler(new AlwaysOnSampler())
                    .AddSource(options.ServiceName);

                if (options.EnableAspNetCoreInstrumentation)
                    tracing.AddAspNetCoreInstrumentation();

                if (options.EnableHttpClientInstrumentation)
                    tracing.AddHttpClientInstrumentation();

                if (!string.IsNullOrEmpty(options.OtlpEndpoint))
                    tracing.AddOtlpExporter(otlp => otlp.Endpoint = new Uri(options.OtlpEndpoint));

                if (options.ExportToConsole)
                    tracing.AddConsoleExporter();
            })
            .WithMetrics(metrics =>
            {
                metrics
                    .AddMeter(options.ServiceName)
                    .AddRuntimeInstrumentation();

                // 正确的 Metrics 扩展方法
                metrics.AddAspNetCoreInstrumentation();
                metrics.AddHttpClientInstrumentation();

                if (!string.IsNullOrEmpty(options.OtlpEndpoint))
                    metrics.AddOtlpExporter(otlp => otlp.Endpoint = new Uri(options.OtlpEndpoint));

                if (options.ExportToConsole)
                    metrics.AddConsoleExporter();
            });

        return services;
    }

    /// <summary>
    /// 注册健康检查基础设施
    /// </summary>
    private static IServiceCollection AddHealthChecksInfrastructure(
        this IServiceCollection services,
        HealthCheckOptions options)
    {
        _ = options;
        var healthChecksBuilder = services.AddHealthChecks();

        // 添加自定义存活检查
        healthChecksBuilder.AddCheck<LivenessHealthCheck>(
            "self",
            failureStatus: HealthStatus.Unhealthy,
            tags: ["liveness"]);

        return services;
    }

    /// <summary>
    /// 配置监控中间件管道
    /// <para>应在 UseRouting 之后、UseEndpoints 之前调用</para>
    /// </summary>
    /// <param name="app">应用构建器</param>
    /// <param name="options">监控配置</param>
    /// <returns>链式返回应用构建器</returns>
    public static IApplicationBuilder UseMonitoringInfrastructure(
        this IApplicationBuilder app,
        MonitoringOptions options)
    {
        // 使用健康检查中间件
        if (options.HealthChecks.Enabled)
        {
            app.UseHealthChecks(options.HealthChecks.LivenessPath, new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
            {
                Predicate = check => check.Tags.Contains("liveness"),
                ResponseWriter = WriteHealthCheckResponse
            });

            app.UseHealthChecks(options.HealthChecks.ReadinessPath, new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
            {
                Predicate = _ => true,
                ResponseWriter = WriteHealthCheckResponse
            });
        }

        return app;
    }

    /// <summary>
    /// 自定义健康检查响应写入器
    /// </summary>
    private static Task WriteHealthCheckResponse(HttpContext context, HealthReport report)
    {
        context.Response.ContentType = "application/json";
        var result = new
        {
            status = report.Status.ToString(),
            duration = report.TotalDuration.TotalMilliseconds,
            entries = report.Entries.ToDictionary(
                e => e.Key,
                e => new
                {
                    status = e.Value.Status.ToString(),
                    description = e.Value.Description,
                    duration = e.Value.Duration.TotalMilliseconds,
                    data = e.Value.Data
                })
        };

        return JsonSerializer.SerializeAsync(context.Response.Body, result);
    }
}