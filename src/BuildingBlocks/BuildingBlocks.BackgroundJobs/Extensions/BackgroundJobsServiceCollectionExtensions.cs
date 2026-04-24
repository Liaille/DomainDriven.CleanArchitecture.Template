using BuildingBlocks.BackgroundJobs.BackgroundServices;
using BuildingBlocks.BackgroundJobs.TaskQueue;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.BackgroundJobs.Extensions;

/// <summary>
/// 后台任务模块服务注册扩展
/// </summary>
public static class BackgroundJobsServiceCollectionExtensions
{
    /// <summary>
    /// 注册后台任务基础设施
    /// <para>包含通用异步任务队列和处理器</para>
    /// </summary>
    /// <param name="services">服务集合/param>
    /// <returns>链式返回服务集合</returns>
    public static IServiceCollection AddBackgroundJobsInfrastructure(this IServiceCollection services)
    {
        // 注册通用的异步任务队列工厂
        services.TryAddSingleton(typeof(IAsyncTaskQueue<>), typeof(ChannelAsyncTaskQueue<>));

        return services;
    }

    /// <summary>
    /// 注册特定类型的任务队列及处理器
    /// </summary>
    /// <typeparam name="T">任务项类型</typeparam>
    /// <param name="services">服务集合</param>
    /// <param name="processItemAsync">任务处理委托</param>
    /// <param name="capacity">队列容量 (0 表示无界队列)</param>
    /// <returns>链式返回服务集合</returns>
    public static IServiceCollection AddTaskQueueProcessor<T>(
        this IServiceCollection services,
        Func<T, CancellationToken, Task> processItemAsync,
        int capacity = 0)
    {
        // 注册特定类型的队列（覆盖泛型注册）
        services.AddSingleton<IAsyncTaskQueue<T>>(sp => new ChannelAsyncTaskQueue<T>(capacity));

        // 注册队列处理器为 BackgroundService
        services.AddHostedService(sp =>
            new TaskQueueProcessor<T>(
                sp.GetRequiredService<IAsyncTaskQueue<T>>(),
                sp.GetRequiredService<ILogger<TaskQueueProcessor<T>>>(),
                processItemAsync));

        return services;
    }

    /// <summary>
    /// 注册BuildingBlocks.Logging专用任务队列及处理器
    /// <para>处理 Func&lt;Task&gt; 类型的日志任务</para>
    /// </summary>
    /// <param name="services">服务集合</param>
    /// <param name="capacity">队列容量 (0 表示无界队列)</param>
    /// <returns>链式返回服务集合</returns>
    public static IServiceCollection AddLoggingTaskQueueProcessor(this IServiceCollection services, int capacity = 0)
    {
        // 注册 Func<Task> 类型的队列
        services.AddSingleton<IAsyncTaskQueue<Func<Task>>>(sp => new ChannelAsyncTaskQueue<Func<Task>>(capacity));

        // 注册日志队列处理器
        services.AddHostedService(sp =>
            new TaskQueueProcessor<Func<Task>>(
                sp.GetRequiredService<IAsyncTaskQueue<Func<Task>>>(),
                sp.GetRequiredService<ILogger<TaskQueueProcessor<Func<Task>>>>(),
                async (taskFunc, ct) => await taskFunc.Invoke()));

        return services;
    }
}
