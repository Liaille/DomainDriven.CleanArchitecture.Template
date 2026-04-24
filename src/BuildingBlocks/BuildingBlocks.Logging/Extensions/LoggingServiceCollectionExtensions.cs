using BuildingBlocks.Logging.AuditLogging;
using BuildingBlocks.Logging.BusinessLogging;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Logging.Extensions;

/// <summary>
/// 日志模块服务依赖注入注册扩展
/// </summary>
public static class LoggingServiceCollectionExtensions
{
    /// <summary>
    /// 注册核心日志基础设施 (审计日志、业务日志)
    /// <para>自动支持用户、请求、租户全量上下文注入</para>
    /// </summary>
    /// <param name="services">服务集合</param>
    /// <returns>链式返回服务集合</returns>
    public static IServiceCollection AddCoreLoggingInfrastructure(this IServiceCollection services)
    {
        // 注册业务日志服务 (自动注入 ICurrentUser、IRequestContext、ICurrentTenant)
        services.AddScoped<IBusinessLogger, BusinessLogger>();
        // 注册审计日志服务 (自动注入 ICurrentUser、IRequestContext、ICurrentTenant)
        services.AddScoped<IAuditLogger, AuditLogger>();
        
        return services;
    }
}
