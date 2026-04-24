using BuildingBlocks.BackgroundJobs.TaskQueue;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.BackgroundJobs.BackgroundServices;

/// <summary>
/// 异步任务队列处理器
/// <para>基于 IAsyncTaskQueue 实现的通用后台服务，用于持续消费和处理队列中的任务项</para>
/// </summary>
/// <typeparam name="T">任务项类型</typeparam>
/// <param name="taskQueue">异步任务队列</param>
/// <param name="logger">日志记录器</param>
/// <param name="processItemAsync">处理任务项的异步方法</param>
public class TaskQueueProcessor<T>(
    IAsyncTaskQueue<T> taskQueue,
    ILogger<TaskQueueProcessor<T>> logger,
    Func<T, CancellationToken, Task> processItemAsync) : BackgroundService
{
    /// <summary>
    /// 执行后台任务
    /// </summary>
    /// <param name="stoppingToken">用于取消后台任务的取消令牌</param>
    /// <returns>表示后台任务的任务</returns>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        if (logger.IsEnabled(LogLevel.Information))
            logger.LogInformation("TaskQueueProcessor<{TaskType}> started", typeof(T).Name);

        await foreach (var item in taskQueue.ReadAllAsync(stoppingToken))
        {
            if (stoppingToken.IsCancellationRequested) break;

            try
            {
                await processItemAsync(item, stoppingToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error processing task item of type {TaskType}", typeof(T).Name);
            }

            if (logger.IsEnabled(LogLevel.Information))
                logger.LogInformation("TaskQueueProcessor<{TaskType}> stopped", typeof(T).Name);
        }
    }
}
