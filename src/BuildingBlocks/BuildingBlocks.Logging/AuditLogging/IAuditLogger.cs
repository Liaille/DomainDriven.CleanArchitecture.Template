namespace BuildingBlocks.Logging.AuditLogging;

/// <summary>
/// 审计日志接口（记录领域实体全生命周期操作）
/// <para>用于记录实体的创建、更新、删除等操作</para>
/// </summary>
public interface IAuditLogger
{
    /// <summary>
    /// 记录实体创建审计日志
    /// </summary>
    /// <param name="entityType">实体类型名称</param>
    /// <param name="entityId">实体唯一主键</param>
    /// <param name="newValue">实体完整新数据</param>
    /// <param name="operationDesc">自定义操作描述（可选）</param>
    void LogEntityCreate(string entityType, string entityId, object newValue, string? operationDesc = null);

    /// <summary>
    /// 记录实体更新审计日志（携带新旧值对比）
    /// </summary>
    /// <param name="entityType">实体类型名称</param>
    /// <param name="entityId">实体唯一主键</param>
    /// <param name="oldValue">修改前原始数据</param>
    /// <param name="newValue">修改后最新数据</param>
    /// <param name="operationDesc">自定义操作描述（可选）</param>
    void LogEntityUpdate(string entityType, string entityId, object? oldValue, object newValue, string? operationDesc = null);

    /// <summary>
    /// 记录实体删除审计日志
    /// </summary>
    /// <param name="entityType">实体类型名称</param>
    /// <param name="entityId">实体唯一主键</param>
    /// <param name="oldValue">删除前实体原始数据</param>
    /// <param name="operationDesc">自定义操作描述（可选）</param>
    void LogEntityDelete(string entityType, string entityId, object? oldValue, string? operationDesc = null);

    /// <summary>
    /// 自定义扩展审计事件记录
    /// </summary>
    /// <param name="auditEvent">完整审计事件模型</param>
    void LogCustomAuditEvent(AuditOperationEvent auditEvent);

    /// <summary>
    /// 异步记录实体创建审计日志
    /// </summary>
    /// <param name="entityType">实体类型名称</param>
    /// <param name="entityId">实体唯一主键</param>
    /// <param name="newValue">实体完整新数据</param>
    /// <param name="operationDesc">自定义操作描述（可选）</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns></returns>
    Task LogEntityCreateAsync(string entityType, string entityId, object newValue, string? operationDesc = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// 异步记录实体更新审计日志（携带新旧值对比）
    /// </summary>
    /// <param name="entityType">实体类型名称</param>
    /// <param name="entityId">实体唯一主键</param>
    /// <param name="oldValue">修改前原始数据</param>
    /// <param name="newValue">修改后最新数据</param>
    /// <param name="operationDesc">自定义操作描述（可选）</param>
    /// <param name="cancellationToken">取消令牌</param>
    Task LogEntityUpdateAsync(string entityType, string entityId, object? oldValue, object newValue, string? operationDesc = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// 异步记录实体删除审计日志
    /// </summary>
    /// <param name="entityType">实体类型名称</param>
    /// <param name="entityId">实体唯一主键</param>
    /// <param name="oldValue">删除前实体原始数据</param>
    /// <param name="operationDesc">自定义操作描述（可选）</param>
    /// <param name="cancellationToken">取消令牌</param>
    Task LogEntityDeleteAsync(string entityType, string entityId, object? oldValue, string? operationDesc = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// 异步自定义扩展审计事件记录
    /// </summary>
    /// <param name="auditEvent">完整审计事件模型</param>
    /// <param name="cancellationToken">取消令牌</param>
    Task LogCustomAuditEventAsync(AuditOperationEvent auditEvent, CancellationToken cancellationToken = default);
}
