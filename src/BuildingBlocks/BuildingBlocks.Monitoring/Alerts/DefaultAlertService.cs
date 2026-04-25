using BuildingBlocks.Core.Common;
using BuildingBlocks.Logging.BusinessLogging;
using BuildingBlocks.Monitoring.Abstractions;
using BuildingBlocks.Monitoring.Configuration;

namespace BuildingBlocks.Monitoring.Alerts;

/// <summary>
/// 默认告警服务实现
/// <para>支持日志告警、企业微信、钉钉、邮件多渠道告警</para>
/// <para>集成 BuildingBlocks.Logging 的 IBusinessLogger 记录告警日志</para>
/// </summary>
/// <param name="options">告警配置</param>
/// <param name="businessLogger">业务日志记录器</param>
public class DefaultAlertService(AlertOptions options, IBusinessLogger businessLogger) : IAlertService
{
    /// <summary>
    /// 发送信息级别告警
    /// </summary>
    /// <param name="title">告警标题</param>
    /// <param name="message">告警消息</param>
    /// <param name="tags">标签 (可选)</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>表示异步操作的任务</returns>
    public async Task SendInfoAlertAsync(string title, string message, string[]? tags = null, CancellationToken cancellationToken = default)
    {
        if (!ShouldAlert("Info")) return;

        Guard.NotNullOrEmpty(title, nameof(title));
        Guard.NotNullOrEmpty(message, nameof(message));

        // 记录业务日志（集成 BuildingBlocks.Logging）
        businessLogger.LogBusinessEvent("InfoAlert", new { Title = title, Message = message, Tags = tags });

        // 发送到各渠道（占位实现，实际项目中需集成 Webhook）
        await SendToAllChannelsAsync("Info", title, message, tags, null, cancellationToken);
    }

    /// <summary>
    /// 发送警告级别告警
    /// </summary>
    /// <param name="title">告警标题</param>
    /// <param name="message">告警消息</param>
    /// <param name="tags">标签 (可选)</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>表示异步操作的任务</returns>
    public async Task SendWarningAlertAsync(string title, string message, string[]? tags = null, CancellationToken cancellationToken = default)
    {
        if (!ShouldAlert("Warning")) return;

        Guard.NotNullOrEmpty(title, nameof(title));
        Guard.NotNullOrEmpty(message, nameof(message));

        // 记录业务警告日志
        businessLogger.LogBusinessWarning("WarningAlert", new { Title = title, Message = message, Tags = tags });

        await SendToAllChannelsAsync("Warning", title, message, tags, null, cancellationToken);
    }

    /// <summary>
    /// 发送错误级别告警
    /// </summary>
    /// <param name="title">告警标题</param>
    /// <param name="message">告警消息</param>
    /// <param name="exception">异常对象 (可选)</param>
    /// <param name="tags">标签 (可选)</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>表示异步操作的任务</returns>
    public async Task SendErrorAlertAsync(string title, string message, Exception? exception = null, string[]? tags = null, CancellationToken cancellationToken = default)
    {
        if (!ShouldAlert("Error")) return;

        Guard.NotNullOrEmpty(title, nameof(title));
        Guard.NotNullOrEmpty(message, nameof(message));

        // 记录业务错误日志
        businessLogger.LogBusinessError("ErrorAlert", exception, new { Title = title, Message = message, Tags = tags });

        await SendToAllChannelsAsync("Error", title, message, tags, exception, cancellationToken);
    }

    /// <summary>
    /// 判断是否应该发送告警 (基于级别阈值)
    /// </summary>
    /// <param name="level">告警级别</param>
    /// <returns>是否应该发送告警</returns>
    private bool ShouldAlert(string level)
    {
        if (!options.Enabled) return false;

        var levelOrder = new Dictionary<string, int>
        {
            { "Info", 0 },
            { "Warning", 1 },
            { "Error", 2 }
        };

        var minimumLevel = options.MinimumAlertLevel;
        if (!levelOrder.TryGetValue(minimumLevel, out var minimumOrder))
            minimumOrder = 1; // 默认 Warning

        if (!levelOrder.TryGetValue(level, out var currentOrder))
            currentOrder = 0;

        return currentOrder >= minimumOrder;
    }

    /// <summary>
    /// 发送到所有配置的告警渠道
    /// </summary>
    /// <param name="level">告警级别</param>
    /// <param name="title">告警标题</param>
    /// <param name="message">告警消息</param>
    /// <param name="tags">标签 (可选)</param>
    /// <param name="exception">异常对象 (可选)</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>表示异步操作的任务</returns>
    private static async Task SendToAllChannelsAsync(
        string level,
        string title,
        string message,
        string[]? tags,
        Exception? exception,
        CancellationToken cancellationToken)
    {
        // 标记未使用参数，消除 IDE0060
        _ = tags;
        _ = exception;
        _ = cancellationToken;

        // 这里仅做控制台输出，实际项目中需实现 Webhook/邮件调用
        Console.WriteLine($"[{level.ToUpperInvariant()} ALERT] {title}: {message}");
        await Task.CompletedTask;
    }
}
