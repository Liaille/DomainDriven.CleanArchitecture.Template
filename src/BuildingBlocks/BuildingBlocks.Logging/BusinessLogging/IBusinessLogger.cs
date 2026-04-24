namespace BuildingBlocks.Logging.BusinessLogging;

/// <summary>
/// 业务事件标准化日志接口（独立于系统日志，仅记录领域业务流程）
/// <para>用于记录业务事件、业务告警、业务异常等，支持租户、用户、请求全量上下文</para>
/// </summary>
public interface IBusinessLogger
{
    /// <summary>
    /// 记录正常业务事件
    /// </summary>
    /// <param name="eventName">业务事件唯一标识名称</param>
    /// <param name="eventData">事件附加业务数据（可选）</param>
    /// <param name="desc">事件文字描述（可选）</param>
    void LogBusinessEvent(string eventName, object? eventData = null, string? desc = null);

    /// <summary>
    /// 记录业务流程告警（非异常、业务边界风险）
    /// </summary>
    /// <param name="eventName">业务事件唯一标识名称</param>
    /// <param name="eventData">事件附加业务数据（可选）</param>
    /// <param name="desc">事件文字描述（可选）</param>
    void LogBusinessWarning(string eventName, object? eventData = null, string? desc = null);

    /// <summary>
    /// 记录业务执行异常（携带异常堆栈）
    /// </summary>
    /// <param name="eventName">业务事件唯一标识名称</param>
    /// <param name="exception">异常对象（可选）</param>
    /// <param name="eventData">事件附加业务数据（可选）</param>
    /// <param name="desc">事件文字描述（可选）</param>
    void LogBusinessError(string eventName, Exception? exception = null, object? eventData = null, string? desc = null);

    /// <summary>
    /// 异步记录正常业务事件
    /// </summary>
    /// <param name="eventName">业务事件唯一标识名称</param>
    /// <param name="eventData">事件附加业务数据（可选）</param>
    /// <param name="desc">事件文字描述（可选）</param>
    /// <param name="cancellationToken">取消令牌</param>
    Task LogBusinessEventAsync(string eventName, object? eventData = null, string? desc = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// 异步记录业务流程告警（非异常、业务边界风险）
    /// </summary>
    /// <param name="eventName">业务事件唯一标识名称</param>
    /// <param name="eventData">事件附加业务数据（可选）</param>
    /// <param name="desc">事件文字描述（可选）</param>
    /// <param name="cancellationToken">取消令牌</param>
    Task LogBusinessWarningAsync(string eventName, object? eventData = null, string? desc = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// 异步记录业务执行异常（携带异常堆栈）
    /// </summary>
    /// <param name="eventName">业务事件唯一标识名称</param>
    /// <param name="exception">异常对象（可选）</param>
    /// <param name="eventData">事件附加业务数据（可选）</param>
    /// <param name="desc">事件文字描述（可选）</param>
    /// <param name="cancellationToken">取消令牌</param>
    Task LogBusinessErrorAsync(string eventName, Exception? exception = null, object? eventData = null, string? desc = null, CancellationToken cancellationToken = default);
}
