using BuildingBlocks.Core.Common;
using BuildingBlocks.Logging.Abstractions;
using BuildingBlocks.Logging.Builders;
using BuildingBlocks.Logging.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Serilog;

namespace BuildingBlocks.Logging.Extensions;

/// <summary>
/// 日志模块依赖注入扩展方法
/// 【核心职责】统一注册日志系统所有服务，提供标准化的DI集成入口
/// 【设计原则】
/// 1. 遵循.NET官方扩展方法规范，使用IServiceCollection作为扩展目标
/// 2. 提供两种注册模式: 基础注册 (支持自定义配置)和一键初始化 (开箱即用)
/// 3. 使用TryAddSingleton保证服务幂等注册，支持用户自定义替换实现
/// 4. 完全封装Serilog初始化细节，业务层无需直接依赖Serilog
/// 【强制规范】所有日志服务必须通过此类注册，禁止手动注册日志相关组件
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// 注册BuildingBlocks.Logging基础服务 (不初始化Serilog)
    /// 【适用场景】需要自定义Serilog配置、手动控制日志初始化时机的场景
    /// 【注册内容】
    /// 1. 绑定LoggingOptions配置到配置文件的"Logging"节
    /// 2. 注册敏感数据过滤器ISensitiveDataFilter的默认实现
    /// 3. 注册日志构建器ILoggerBuilder的Serilog实现
    /// 【注意事项】调用此方法后需要手动构建并设置Serilog.Log.Logger
    /// </summary>
    /// <param name="services">IServiceCollection服务集合</param>
    /// <param name="configuration">IConfiguration配置对象，用于绑定日志配置</param>
    /// <returns>IServiceCollection服务集合，支持链式调用</returns>
    /// <exception cref="ArgumentNullException">当services或configuration为null时抛出</exception>
    public static IServiceCollection AddBuildingBlocksLogging(this IServiceCollection services, IConfiguration configuration)
    {
        Guard.NotNull(services);
        Guard.NotNull(configuration);

        // 绑定日志配置到配置文件的"Logging"节
        services.Configure<LoggingOptions>(configuration.GetSection(LoggingOptions.ConfigurationSectionName));

        // 幂等注册核心服务，支持用户自定义替换实现
        services.TryAddSingleton<ISensitiveDataFilter, SensitiveDataFilter>();
        services.TryAddSingleton<ILoggerBuilder, SerilogLoggerBuilder>();

        return services;
    }

    /// <summary>
    /// 注册BuildingBlocks.Logging服务并一键初始化Serilog (推荐生产环境使用)
    /// 【适用场景】绝大多数标准业务场景，开箱即用，无需自定义配置
    /// 【执行流程】
    /// 1. 调用AddBuildingBlocksLogging注册所有基础服务
    /// 2. 临时构建服务提供者以获取已注册的服务
    /// 3. 从配置文件加载LoggingOptions配置
    /// 4. 使用ILoggerBuilder构建Serilog.ILogger实例
    /// 5. 自动注册敏感数据过滤器
    /// 6. 设置全局静态日志实例Serilog.Log.Logger
    /// 7. 释放临时服务提供者
    /// 【强制要求】生产环境必须使用此方法，确保敏感数据过滤自动生效
    /// 【注意事项】
    /// 1. 此方法会临时构建服务提供者，仅用于日志初始化，不会影响主容器
    /// 2. 所有配置均来自appsettings.json的"Logging"节
    /// 3. 自动启用默认丰富器和敏感数据过滤
    /// </summary>
    /// <param name="services">IServiceCollection服务集合</param>
    /// <param name="configuration">IConfiguration配置对象</param>
    /// <returns>IServiceCollection服务集合，支持链式调用</returns>
    /// <exception cref="ArgumentNullException">当services或configuration为null时抛出</exception>
    /// <exception cref="InvalidOperationException">当无法获取必要的日志服务时抛出</exception>
    public static IServiceCollection AddBuildingBlocksLoggingWithSerilog(this IServiceCollection services, IConfiguration configuration)
    {
        Guard.NotNull(services);
        Guard.NotNull(configuration);

        // 第一步: 注册所有基础日志服务
        services.AddBuildingBlocksLogging(configuration);

        // 第二步: 临时构建服务提供者以获取已注册的服务
        // 注意: 这里使用临时容器是为了在应用启动早期完成日志初始化
        // 不会影响主应用容器的生命周期
        using var serviceProvider = services.BuildServiceProvider();

        // 第三步: 加载配置并构建日志实例
        var options = configuration.GetSection(LoggingOptions.ConfigurationSectionName).Get<LoggingOptions>() ?? new LoggingOptions();
        var loggerBuilder = serviceProvider.GetRequiredService<ILoggerBuilder>();
        var sensitiveDataFilter = serviceProvider.GetRequiredService<ISensitiveDataFilter>();

        // 第四步: 构建并设置全局静态日志实例
        Log.Logger = loggerBuilder
            .ConfigureFromOptions(options)
            .AddSensitiveDataFilter(sensitiveDataFilter)
            .Build();

        return services;
    }
}
