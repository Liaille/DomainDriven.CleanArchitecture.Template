using BuildingBlocks.BackgroundJobs.TaskQueue;
using BuildingBlocks.Core.Context;
using BuildingBlocks.Logging.Helpers;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Logging.BusinessLogging;

/// <summary>
/// 业务事件日志默认实现类
/// 提供统一的结构化日志格式，自动填充用户、请求、租户全量上下文信息
/// 与 AuditLogger 保持完全一致的上下文处理逻辑和代码风格
/// </summary>
/// <param name="logger">日志记录器实例</param>
/// <param name="currentUser">当前用户上下文 (可选自动注入)</param>
/// <param name="requestContext">当前请求上下文 (可选自动注入)</param>
/// <param name="currentTenant">当前租户上下文 (可选自动注入)</param>
/// <param name="taskQueue">异步任务队列</param>
public class BusinessLogger(
    ILogger<BusinessLogger> logger,
    ICurrentUser? currentUser = null,
    IRequestContext? requestContext = null,
    ICurrentTenant? currentTenant = null,
    IAsyncTaskQueue<Func<Task>>? taskQueue = null)
    : IBusinessLogger
{
    #region 同步方法
    /// <summary>
    /// 记录正常业务事件
    /// 用于记录业务流程关键节点，如订单创建、支付成功等
    /// </summary>
    /// <param name="eventName">业务事件唯一标识名称</param>
    /// <param name="eventData">事件附加业务数据 (可选)</param>
    /// <param name="desc">事件文字描述 (可选)</param>
    /// <exception cref="ArgumentNullException">事件名称为空时抛出</exception>
    public void LogBusinessEvent(string eventName, object? eventData = null, string? desc = null)
    {
        // 校验事件名称不能为空
        ArgumentNullException.ThrowIfNull(eventName);

        if (!logger.IsEnabled(LogLevel.Information)) return;

        // 构建统一业务日志事件对象
        var logModel = BuildBaseBusinessLog(eventName, eventData, desc);

        // 结构化日志输出，@符号表示序列化为JSON
        logger.LogInformation("BusinessEventLog: {@BusinessEvent}", logModel);
    }

    /// <summary>
    /// 记录业务流程告警
    /// 用于记录非异常业务风险，如库存不足、额度接近上限等
    /// </summary>
    /// <param name="eventName">业务事件唯一标识名称</param>
    /// <param name="eventData">事件附加业务数据 (可选)</param>
    /// <param name="desc">事件文字描述 (可选)</param>
    /// <exception cref="ArgumentNullException">事件名称为空时抛出</exception>
    public void LogBusinessWarning(string eventName, object? eventData = null, string? desc = null)
    {
        // 校验事件名称不能为空
        ArgumentNullException.ThrowIfNull(eventName);

        if (!logger.IsEnabled(LogLevel.Warning)) return;

        // 构建统一业务日志事件对象
        var logModel = BuildBaseBusinessLog(eventName, eventData, desc);

        // 结构化警告日志输出
        logger.LogWarning("BusinessWarningLog: {@BusinessEvent}", logModel);
    }

    /// <summary>
    /// 记录业务执行异常
    /// 用于记录业务流程异常，如支付失败、订单取消等，携带完整异常堆栈
    /// </summary>
    /// <param name="eventName">业务事件唯一标识名称</param>
    /// <param name="exception">异常对象 (可选)</param>
    /// <param name="eventData">事件附加业务数据 (可选)</param>
    /// <param name="desc">事件文字描述 (可选)</param>
    /// <exception cref="ArgumentNullException">事件名称为空时抛出</exception>
    public void LogBusinessError(string eventName, Exception? exception = null, object? eventData = null, string? desc = null)
    {
        // 校验事件名称不能为空
        ArgumentNullException.ThrowIfNull(eventName);

        if (!logger.IsEnabled(LogLevel.Error)) return;

        // 构建统一业务日志事件对象
        var logModel = BuildBaseBusinessLog(eventName, eventData, desc);

        // 结构化错误日志输出，携带异常信息
        logger.LogError(exception, "BusinessErrorLog: {@BusinessEvent}", logModel);
    }
    #endregion

    #region 异步方法
    /// <summary>
    /// 异步记录正常业务事件
    /// </summary>
    /// <param name="eventName">业务事件唯一标识名称</param>
    /// <param name="eventData">事件附加业务数据 (可选)</param>
    /// <param name="desc">事件文字描述 (可选)</param>
    /// <param name="cancellationToken">取消令牌</param>
    public async Task LogBusinessEventAsync(string eventName, object? eventData = null, string? desc = null, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(eventName);

        if (!logger.IsEnabled(LogLevel.Information)) return;

        // 降级策略
        if (taskQueue is null)
        {
            LogBusinessEvent(eventName, eventData, desc);
            return;
        }

        var logModel = BuildBaseBusinessLog(eventName, eventData, desc);
        await taskQueue.QueueAsync(() =>
        {
            logger.LogInformation("BusinessEventLog: {@BusinessEvent}", logModel);
            return Task.CompletedTask;
        }, cancellationToken);
    }

    /// <summary>
    /// 异步记录业务流程告警 (非异常、业务边界风险)
    /// </summary>
    /// <param name="eventName">业务事件唯一标识名称</param>
    /// <param name="eventData">事件附加业务数据 (可选)</param>
    /// <param name="desc">事件文字描述 (可选)</param>
    /// <param name="cancellationToken">取消令牌</param>
    public async Task LogBusinessWarningAsync(string eventName, object? eventData = null, string? desc = null, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(eventName);

        if (!logger.IsEnabled(LogLevel.Warning)) return;

        if (taskQueue is null)
        {
            LogBusinessWarning(eventName, eventData, desc);
            return;
        }

        var logModel = BuildBaseBusinessLog(eventName, eventData, desc);
        await taskQueue.QueueAsync(() =>
        {
            logger.LogWarning("BusinessWarningLog: {@BusinessEvent}", logModel);
            return Task.CompletedTask;
        }, cancellationToken);
    }

    /// <summary>
    /// 异步记录业务执行异常 (携带异常堆栈)
    /// </summary>
    /// <param name="eventName">业务事件唯一标识名称</param>
    /// <param name="exception">异常对象 (可选)</param>
    /// <param name="eventData">事件附加业务数据 (可选)</param>
    /// <param name="desc">事件文字描述 (可选)</param>
    /// <param name="cancellationToken">取消令牌</param>
    public async Task LogBusinessErrorAsync(string eventName, Exception? exception = null, object? eventData = null, string? desc = null, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(eventName);

        if (!logger.IsEnabled(LogLevel.Error)) return;

        if (taskQueue is null)
        {
            LogBusinessError(eventName, exception, eventData, desc);
            return;
        }

        var logModel = BuildBaseBusinessLog(eventName, eventData, desc);
        await taskQueue.QueueAsync(() =>
        {
            logger.LogError(exception, "BusinessErrorLog: {@BusinessEvent}", logModel);
            return Task.CompletedTask;
        }, cancellationToken);
    }
    #endregion

    #region 私有方法
    /// <summary>
    /// 构建统一业务日志事件模型
    /// 自动填充用户、请求、租户上下文，与AuditLogger保持一致
    /// </summary>
    /// <param name="eventName">业务事件名称</param>
    /// <param name="eventData">业务数据</param>
    /// <param name="desc">描述信息</param>
    /// <returns>业务日志实体对象</returns>
    private BusinessLogEvent BuildBaseBusinessLog(string eventName, object? eventData, string? desc)
    {
        var logModel = new BusinessLogEvent
        {
            // 业务核心信息
            EventName = eventName,
            Description = desc,
            BusinessData = eventData,
        };

        LogContextEnricher.EnrichContext(logModel, currentUser, requestContext, currentTenant);

        return logModel;
    }
    #endregion
}
