namespace BuildingBlocks.Monitoring.Abstractions;

/// <summary>
/// 告警服务抽象接口
/// <para>用于发送日志告警、指标告警、健康检查告警</para>
/// </summary>
public interface IAlertService
{
    /// <summary>
    /// 发送信息级别告警
    /// </summary>
    /// <param name="title">告警标题</param>
    /// <param name="message">告警消息</param>
    /// <param name="tags">标签 (可选)</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>表示异步操作的任务</returns>
    Task SendInfoAlertAsync(string title, string message, string[]? tags = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// 发送警告级别告警
    /// </summary>
    /// <param name="title">告警标题</param>
    /// <param name="message">告警消息</param>
    /// <param name="tags">标签 (可选)</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>表示异步操作的任务</returns>
    Task SendWarningAlertAsync(string title, string message, string[]? tags = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// 发送错误级别告警
    /// </summary>
    /// <param name="title">告警标题</param>
    /// <param name="message">告警消息</param>
    /// <param name="exception">异常对象 (可选)</param>
    /// <param name="tags">标签 (可选)</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>表示异步操作的任务</returns>
    Task SendErrorAlertAsync(string title, string message, Exception? exception = null, string[]? tags = null, CancellationToken cancellationToken = default);
}
