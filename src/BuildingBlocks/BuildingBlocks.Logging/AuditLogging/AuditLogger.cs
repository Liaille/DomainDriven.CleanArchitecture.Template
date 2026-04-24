using BuildingBlocks.BackgroundJobs.TaskQueue;
using BuildingBlocks.Core.Context;
using BuildingBlocks.Logging.Helpers;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Logging.AuditLogging;

/// <summary>
/// 审计日志默认实现类
/// <para>提供结构化 JSON 输出、自动填充用户/请求/租户全量上下文信息</para>
/// </summary>
/// <param name="logger">日志记录器</param>
/// <param name="currentUser">当前用户上下文</param>
/// <param name="requestContext">请求上下文</param>
/// <param name="currentTenant">当前租户上下文</param>
/// <param name="taskQueue">异步任务队列</param>
public class AuditLogger(
    ILogger<AuditLogger> logger,
    ICurrentUser? currentUser = null,
    IRequestContext? requestContext = null,
    ICurrentTenant? currentTenant = null,
    IAsyncTaskQueue<Func<Task>>? taskQueue = null)
    : IAuditLogger
{
    #region 同步方法
    /// <summary>
    /// 记录实体创建操作审计日志
    /// </summary>
    /// <param name="entityType">实体类型名称</param>
    /// <param name="entityId">实体唯一标识</param>
    /// <param name="newValue">创建后的实体数据</param>
    /// <param name="operationDesc">操作描述（可选）</param>
    public void LogEntityCreate(string entityType, string entityId, object newValue, string? operationDesc = null)
    {
        // 构建审计事件对象并调用通用记录方法
        LogCustomAuditEvent(new AuditOperationEvent
        {
            OperationType = "Create",
            EntityType = entityType,
            EntityId = entityId,
            NewValue = newValue,
            Description = operationDesc ?? $"Entity {entityType} data created"
        });
    }

    /// <summary>
    /// 记录实体更新操作审计日志
    /// </summary>
    /// <param name="entityType">实体类型名称</param>
    /// <param name="entityId">实体唯一标识</param>
    /// <param name="oldValue">更新前的实体数据</param>
    /// <param name="newValue">更新后的实体数据</param>
    /// <param name="operationDesc">操作描述（可选）</param>
    public void LogEntityUpdate(string entityType, string entityId, object? oldValue, object newValue, string? operationDesc = null)
    {
        // 构建审计事件对象并调用通用记录方法
        LogCustomAuditEvent(new AuditOperationEvent
        {
            OperationType = "Update",
            EntityType = entityType,
            EntityId = entityId,
            OldValue = oldValue,
            NewValue = newValue,
            Description = operationDesc ?? $"Entity {entityType} data updated"
        });
    }

    /// <summary>
    /// 记录实体删除操作审计日志
    /// </summary>
    /// <param name="entityType">实体类型名称</param>
    /// <param name="entityId">实体唯一标识</param>
    /// <param name="oldValue">删除前的实体数据</param>
    /// <param name="operationDesc">操作描述（可选）</param>
    public void LogEntityDelete(string entityType, string entityId, object? oldValue, string? operationDesc = null)
    {
        // 构建审计事件对象并调用通用记录方法
        LogCustomAuditEvent(new AuditOperationEvent
        {
            OperationType = "Delete",
            EntityType = entityType,
            EntityId = entityId,
            OldValue = oldValue,
            Description = operationDesc ?? $"Entity {entityType} data deleted"
        });
    }

    /// <summary>
    /// 记录自定义审计事件
    /// <para>这是核心方法，会自动填充所有上下文元数据</para>
    /// </summary>
    /// <param name="auditEvent">审计事件对象</param>
    /// <exception cref="ArgumentNullException">当 auditEvent 为 null 时抛出</exception>
    public void LogCustomAuditEvent(AuditOperationEvent auditEvent)
    {
        // 校验审计事件对象不为空
        ArgumentNullException.ThrowIfNull(auditEvent);

        LogContextEnricher.EnrichContext(auditEvent, currentUser, requestContext, currentTenant);

        // 注意：使用 @ 符号告诉日志框架将对象序列化为结构化 JSON
        if (logger.IsEnabled(LogLevel.Information))
            logger.LogInformation("EntityAuditLog: {@AuditEvent}", auditEvent);
    }
    #endregion

    #region 异步方法
    /// <summary>
    /// 异步记录实体创建审计日志
    /// </summary>
    /// <param name="entityType">实体类型名称</param>
    /// <param name="entityId">实体唯一主键</param>
    /// <param name="newValue">实体完整新数据</param>
    /// <param name="operationDesc">自定义操作描述（可选）</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns></returns>
    public async Task LogEntityCreateAsync(string entityType, string entityId, object newValue, string? operationDesc = null, CancellationToken cancellationToken = default)
    {
        if (!logger.IsEnabled(LogLevel.Information)) return;

        var auditEvent = new AuditOperationEvent
        {
            OperationType = "Create",
            EntityType = entityType,
            EntityId = entityId,
            NewValue = newValue,
            Description = operationDesc ?? $"Entity {entityType} data created"
        };

        await LogCustomAuditEventAsync(auditEvent, cancellationToken);
    }

    /// <summary>
    /// 异步记录实体更新审计日志（携带新旧值对比）
    /// </summary>
    /// <param name="entityType">实体类型名称</param>
    /// <param name="entityId">实体唯一主键</param>
    /// <param name="oldValue">修改前原始数据</param>
    /// <param name="newValue">修改后最新数据</param>
    /// <param name="operationDesc">自定义操作描述（可选）</param>
    /// <param name="cancellationToken">取消令牌</param>
    public async Task LogEntityUpdateAsync(string entityType, string entityId, object? oldValue, object newValue, string? operationDesc = null, CancellationToken cancellationToken = default)
    {
        if (!logger.IsEnabled(LogLevel.Information)) return;

        var auditEvent = new AuditOperationEvent
        {
            OperationType = "Update",
            EntityType = entityType,
            EntityId = entityId,
            OldValue = oldValue,
            NewValue = newValue,
            Description = operationDesc ?? $"Entity {entityType} data updated"
        };

        await LogCustomAuditEventAsync(auditEvent, cancellationToken);
    }

    /// <summary>
    /// 异步记录实体删除审计日志
    /// </summary>
    /// <param name="entityType">实体类型名称</param>
    /// <param name="entityId">实体唯一主键</param>
    /// <param name="oldValue">删除前实体原始数据</param>
    /// <param name="operationDesc">自定义操作描述（可选）</param>
    /// <param name="cancellationToken">取消令牌</param>
    public async Task LogEntityDeleteAsync(string entityType, string entityId, object? oldValue, string? operationDesc = null, CancellationToken cancellationToken = default)
    {
        if (!logger.IsEnabled(LogLevel.Information)) return;

        var auditEvent = new AuditOperationEvent
        {
            OperationType = "Delete",
            EntityType = entityType,
            EntityId = entityId,
            OldValue = oldValue,
            Description = operationDesc ?? $"Entity {entityType} data deleted"
        };

        await LogCustomAuditEventAsync(auditEvent, cancellationToken);
    }

    /// <summary>
    /// 异步自定义扩展审计事件记录
    /// </summary>
    /// <param name="auditEvent">完整审计事件模型</param>
    /// <param name="cancellationToken">取消令牌</param>
    public async Task LogCustomAuditEventAsync(AuditOperationEvent auditEvent, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(auditEvent);
        if (!logger.IsEnabled(LogLevel.Information)) return;

        // 降级策略：如果队列未注册，直接同步写入
        if (taskQueue is null)
        {
            LogCustomAuditEvent(auditEvent);
            return;
        }

        LogContextEnricher.EnrichContext(auditEvent, currentUser, requestContext, currentTenant);

        // 入队异步处理（await ValueTask，自动转 Task）
        await taskQueue.QueueAsync(() =>
        {
            logger.LogInformation("EntityAuditLog: {@AuditEvent}", auditEvent);
            return Task.CompletedTask;
        }, cancellationToken);
    }
    #endregion
}
