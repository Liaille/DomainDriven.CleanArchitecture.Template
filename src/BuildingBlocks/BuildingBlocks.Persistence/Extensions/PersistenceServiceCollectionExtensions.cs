using BuildingBlocks.Core.Repositories;
using BuildingBlocks.Core.UnitOfWork;
using BuildingBlocks.Persistence.Interceptors;
using BuildingBlocks.Persistence.Repository;
using BuildingBlocks.Persistence.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Persistence.Extensions;

/// <summary>
/// 持久化模块服务注册扩展
/// </summary>
public static class PersistenceServiceCollectionExtensions
{
    public static IServiceCollection AddEfCorePersistence<TDbContext>(
        this IServiceCollection services,
        Action<DbContextOptionsBuilder> optionsAction)
        where TDbContext : DbContext
    {
        // 注册拦截器
        services.AddScoped<AuditingInterceptor>();
        services.AddScoped<SoftDeleteInterceptor>();
        services.AddScoped<SlowQueryInterceptor>();

        // 注册DbContext
        services.AddDbContext<TDbContext>((sp, options) =>
        {
            optionsAction(options);
            // 添加拦截器
            options.AddInterceptors(
                sp.GetRequiredService<AuditingInterceptor>(),
                sp.GetRequiredService<SoftDeleteInterceptor>(),
                sp.GetRequiredService<SlowQueryInterceptor>());
        });

        // 注册仓储
        services.AddScoped(typeof(IReadOnlyRepository<,>), typeof(EfCoreReadOnlyRepository<,,>));
        services.AddScoped(typeof(IBasicRepository<,>), typeof(EfCoreBasicRepository<,,>));
        services.AddScoped(typeof(IRepository<,>), typeof(EfCoreRepository<,,>));

        // 注册工作单元
        services.AddScoped(typeof(IUnitOfWork), typeof(EfCoreUnitOfWork<TDbContext>));

        return services;
    }
}
