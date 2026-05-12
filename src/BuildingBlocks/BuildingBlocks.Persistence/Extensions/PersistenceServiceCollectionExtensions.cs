using BuildingBlocks.Core.SpecificationPattern;
using BuildingBlocks.Core.UnitOfWork;
using BuildingBlocks.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Persistence.Extensions;

/// <summary>
/// 持久化模块服务注册扩展
/// </summary>
public static class PersistenceServiceCollectionExtensions
{
    /// <summary>
    /// 添加 EF Core 持久化基础设施
    /// </summary>
    /// <typeparam name="TDbContext">业务 DbContext 类型</typeparam>
    /// <param name="services">服务集合</param>
    /// <param name="optionsAction">DbContext 配置委托</param>
    /// <returns>服务集合</returns>
    public static IServiceCollection AddEfCorePersistence<TDbContext>(
        this IServiceCollection services,
        Action<DbContextOptionsBuilder> optionsAction)
        where TDbContext : DbContext
    {
        // 注册EF Core拦截器
        services.AddScoped<AuditingInterceptor>();
        services.AddScoped<SoftDeleteInterceptor>();
        services.AddScoped<MultiTenantInterceptor>();
        services.AddScoped<SlowQueryInterceptor>();

        // 注册DbContext
        services.AddDbContext<TDbContext>((sp, options) =>
        {
            optionsAction(options);
            // 添加拦截器
            options.AddInterceptors(
                sp.GetRequiredService<AuditingInterceptor>(),
                sp.GetRequiredService<SoftDeleteInterceptor>(),
                sp.GetRequiredService<MultiTenantInterceptor>(),
                sp.GetRequiredService<SlowQueryInterceptor>());
        });

        // 注册规约执行器
        

        // 注册工作单元
        

        return services;
    }
}
