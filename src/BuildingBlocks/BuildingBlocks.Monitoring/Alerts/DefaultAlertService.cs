using BuildingBlocks.Core.Common;
using BuildingBlocks.Logging.BusinessLogging;
using BuildingBlocks.Monitoring.Abstractions;
using BuildingBlocks.Monitoring.Configuration;
using BuildingBlocks.Monitoring.Configuration.DingTalk;
using BuildingBlocks.Monitoring.Configuration.WeChatWork;
using MailKit.Net.Smtp;
using MimeKit;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BuildingBlocks.Monitoring.Alerts;

/// <summary>
/// 默认告警服务实现
/// <para>支持日志告警、企业微信、钉钉、邮件多渠道告警</para>
/// <para>集成 BuildingBlocks.Logging 的 IBusinessLogger 记录告警日志</para>
/// <para>提供完整的渠道扩展点，实际项目中可根据需要实现具体的 Webhook/邮件调用</para>
/// </summary>
/// <param name="options">告警配置</param>
/// <param name="businessLogger">业务日志记录器</param>
public class DefaultAlertService(
    AlertOptions options, 
    IBusinessLogger businessLogger, 
    IHttpClientFactory httpClientFactory) : IAlertService
{
    /// <summary>
    /// 告警级别顺序映射 (用于阈值判断)
    /// </summary>
    private static readonly Dictionary<string, int> _levelOrder = new()
    {
        { "Info", 0 },
        { "Warning", 1 },
        { "Error", 2 }
    };

    /// <summary>
    /// JSON 序列化选项
    /// </summary>
    private static readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

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

        // 记录业务日志 (集成 BuildingBlocks.Logging)
        businessLogger.LogBusinessEvent("InfoAlert", new { Title = title, Message = message, Tags = tags });

        // 发送到所有已配置的渠道
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
        // 如果告警未启用，直接返回 false
        if (!options.Enabled) return false;

        // 获取配置的最低告警级别，默认为 Warning
        if (!_levelOrder.TryGetValue(options.MinimumAlertLevel, out var minimumOrder))
            minimumOrder = 1;

        // 获取当前告警级别的顺序值
        if (!_levelOrder.TryGetValue(level, out var currentOrder))
            currentOrder = 0;

        // 只有当前级别大于等于阈值时才发送告警
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
    private async Task SendToAllChannelsAsync(
        string level,
        string title,
        string message,
        string[]? tags,
        Exception? exception,
        CancellationToken cancellationToken)
    {
        // 1. 始终输出到控制台 (开发调试用)
        SendToConsole(level, title, message, tags, exception);

        // 2. 如果配置了企业微信，则发送到企业微信
        if (options.WeChatWork is not null && !string.IsNullOrEmpty(options.WeChatWork.WebhookUrl))
        {
            try
            {
                await SendToWeChatWorkAsync(level, title, message, tags, exception, cancellationToken);
            }
            catch (Exception ex)
            {
                // 渠道发送失败时记录日志，但不影响其他渠道
                businessLogger.LogBusinessWarning(
                    "WeChatWorkAlertFailed", 
                    new { Level = level, Title = title, Message = message, Tags = tags, Exception = ex?.ToString() }, 
                    "Failed to send alert to WeChat Work.");
            }
        }

        // 3. 如果配置了钉钉，则发送到钉钉
        if (options.DingTalk is not null && !string.IsNullOrEmpty(options.DingTalk.WebhookUrl))
        {
            try
            {
                await SendToDingTalkAsync(level, title, message, tags, exception, cancellationToken);
            }
            catch (Exception ex)
            {
                businessLogger.LogBusinessWarning(
                    "DingTalkAlertFailed",
                    new { Level = level, Title = title, Message = message, Tags = tags, Exception = ex?.ToString() }, 
                    "Failed to send alert to DingTalk.");
            }
        }

        // 4. 如果配置了邮件，则发送邮件
        if (options.Email is not null && !string.IsNullOrEmpty(options.Email.SmtpHost))
        {
            try
            {
                await SendToEmailAsync(level, title, message, tags, exception, cancellationToken);
            }
            catch (Exception ex)
            {
                businessLogger.LogBusinessWarning(
                    "EmailAlertFailed", 
                    new { Level = level, Title = title, Message = message, Tags = tags, Exception = ex?.ToString() }, 
                    "Failed to send alert via Email.");
            }
        }
    }

    /// <summary>
    /// 发送告警到控制台
    /// </summary>
    /// <param name="level">告警级别</param>
    /// <param name="title">告警标题</param>
    /// <param name="message">告警消息</param>
    /// <param name="exception">异常对象 (可选)</param>
    /// <returns>表示异步操作的任务</returns>
    private static void SendToConsole(
        string level,
        string title,
        string message,
        string[]? tags,
        Exception? exception)
    {
        // 根据级别设置控制台颜色
        var originalColor = Console.ForegroundColor;
        Console.ForegroundColor = level switch
        {
            "Info" => ConsoleColor.Cyan,
            "Warning" => ConsoleColor.Yellow,
            "Error" => ConsoleColor.Red,
            _ => ConsoleColor.White
        };

        // 构建控制台输出内容
        StringBuilder sb = new();
        sb.AppendLine($"[{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss.fff UTC}] [{level.ToUpperInvariant()} ALERT]");
        sb.AppendLine($"Title: {title}");
        sb.AppendLine($"Message: {message}");
        
        if (tags?.Length > 0)
            sb.AppendLine($"Tags: {string.Join(", ", tags)}");

        if (exception is not null)
        {
            sb.AppendLine($"Exception: {exception.GetType().Name}");
            sb.AppendLine($"Exception Message: {exception.Message}");
            sb.AppendLine($"Stack Trace: {exception.StackTrace}");
        }
        
        // 输出到控制台
        Console.WriteLine(sb.ToString());

        // 恢复原始颜色
        Console.ForegroundColor = originalColor;
    }

    /// <summary>
    /// 发送告警邮件
    /// </summary>
    /// <param name="level">告警级别</param>
    /// <param name="title">告警标题</param>
    /// <param name="message">告警消息</param>
    /// <param name="tags">标签 (可选)</param>
    /// <param name="exception">异常对象 (可选)</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>表示异步操作的任务</returns>
    private async Task SendToEmailAsync(
        string level,
        string title,
        string message,
        string[]? tags,
        Exception? exception,
        CancellationToken cancellationToken)
    {
        if (options.Email is null) return;

        var emailOptions = options.Email;

        // 构建邮件消息
        using var mimeMessage = new MimeMessage();

        // 设置发件人
        var fromAddress = new MailboxAddress(emailOptions.FromDisplayName ?? "System Alert", emailOptions.FromAddress);
        mimeMessage.From.Add(fromAddress);

        // 设置收件人
        foreach (var toAddress in emailOptions.ToAddresses)
        {
            mimeMessage.To.Add(MailboxAddress.Parse(toAddress));
        }

        // 设置抄送
        if (emailOptions.CcAddresses?.Length > 0)
        {
            foreach (var ccAddress in emailOptions.CcAddresses)
            {
                mimeMessage.Cc.Add(MailboxAddress.Parse(ccAddress));
            }
        }

        // 设置密送
        if (emailOptions.BccAddresses?.Length > 0)
        {
            foreach (var bccAddress in emailOptions.BccAddresses)
            {
                mimeMessage.Bcc.Add(MailboxAddress.Parse(bccAddress));
            }
        }

        // 设置邮件主题
        var subjectPrefix = string.IsNullOrEmpty(emailOptions.SubjectPrefix) ? "" : $"[{emailOptions.SubjectPrefix}]";
        var levelTag = level switch
        {
            "Info" => "[INFO]",
            "Warning" => "[WARNING]",
            "Error" => "[ERROR]",
            _ => "[ALERT]"
        };
        mimeMessage.Subject = $"{subjectPrefix} {levelTag} {title}";

        // 构建邮件正文 (HTML格式)
        var bodyBuilder = new BodyBuilder
        {
            HtmlBody = BuildEmailHtmlBody(level, title, message, tags, exception)
        };

        // 如果有异常，可以添加异常详情作为附件 (可选)
        if (exception is not null)
        {
            var exceptionContent = $"Exception Type: {exception.GetType().Name}\n" +
                                   $"Exception Message: {exception.Message}\n" +
                                   $"Stack Trace: {exception.StackTrace}";
            bodyBuilder.Attachments.Add("exception-details.txt", Encoding.UTF8.GetBytes(exceptionContent));
        }

        // 使用 MailKit 发送邮件
        using var smtpClient = new SmtpClient();
        try
        {
            // 连接 SMTP 服务器
            await smtpClient.ConnectAsync(
                emailOptions.SmtpHost,
                emailOptions.SmtpPort,
                emailOptions.EnableSsl,
                cancellationToken);

            // 如果配置了用户名和密码，则进行认证
            if (!string.IsNullOrEmpty(emailOptions.SmtpUsername) && !string.IsNullOrEmpty(emailOptions.SmtpPassword))
            {
                await smtpClient.AuthenticateAsync(
                    emailOptions.SmtpUsername,
                    emailOptions.SmtpPassword,
                    cancellationToken);
            }

            // 发送邮件
            await smtpClient.SendAsync(mimeMessage, cancellationToken);
        }
        finally
        {
            // 断开连接并释放资源
            await smtpClient.DisconnectAsync(true, cancellationToken);
        }
    }

    /// <summary>
    /// 构建邮件 HTML 正文
    /// </summary>
    /// <param name="level">告警级别</param>
    /// <param name="title">告警标题</param>
    /// <param name="message">告警消息</param>
    /// <param name="tags">标签（可选）</param>
    /// <param name="exception">异常对象（可选）</param>
    /// <returns>HTML 格式的邮件正文</returns>
    private static string BuildEmailHtmlBody(
        string level,
        string title,
        string message,
        string[]? tags,
        Exception? exception)
    {
        // 根据级别设置颜色
        var levelColor = level switch
        {
            "Info" => "#17a2b8",
            "Warning" => "#ffc107",
            "Error" => "#dc3545",
            _ => "#6c757d"
        };

        StringBuilder sb = new();

        // HTML 头部
        sb.AppendLine("<!DOCTYPE html>");
        sb.AppendLine("<html lang=\"en\">");
        sb.AppendLine("<head>");
        sb.AppendLine("    <meta charset=\"UTF-8\">");
        sb.AppendLine("    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">");
        sb.AppendLine("    <title>Alert Notification</title>");
        sb.AppendLine("    <style>");
        sb.AppendLine("        body { font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; line-height: 1.6; color: #333; max-width: 800px; margin: 0 auto; padding: 20px; }");
        sb.AppendLine("        .alert-header { background-color: {{levelColor}}; color: white; padding: 15px; border-radius: 5px; margin-bottom: 20px; }");
        sb.AppendLine("        .alert-title { font-size: 24px; font-weight: bold; margin: 0; }");
        sb.AppendLine("        .alert-level { font-size: 14px; opacity: 0.9; margin-top: 5px; }");
        sb.AppendLine("        .alert-content { background-color: #f8f9fa; padding: 20px; border-radius: 5px; margin-bottom: 20px; }");
        sb.AppendLine("        .alert-message { font-size: 16px; margin-bottom: 15px; }");
        sb.AppendLine("        .alert-tags { margin-bottom: 15px; }");
        sb.AppendLine("        .tag { display: inline-block; background-color: #e9ecef; color: #495057; padding: 3px 8px; border-radius: 3px; font-size: 12px; margin-right: 5px; margin-bottom: 5px; }");
        sb.AppendLine("        .alert-time { color: #6c757d; font-size: 14px; }");
        sb.AppendLine("        .exception-section { background-color: #fff3cd; border: 1px solid #ffeaa7; padding: 15px; border-radius: 5px; margin-top: 20px; }");
        sb.AppendLine("        .exception-title { font-weight: bold; color: #856404; margin-top: 0; }");
        sb.AppendLine("        .exception-details { font-family: 'Courier New', Courier, monospace; font-size: 12px; background-color: #fff; padding: 10px; border-radius: 3px; overflow-x: auto; }");
        sb.AppendLine("    </style>");
        sb.AppendLine("</head>");
        sb.AppendLine("<body>");

        // 替换颜色占位符
        sb.Replace("{{levelColor}}", levelColor);

        // 告警头部
        sb.AppendLine("    <div class=\"alert-header\">");
        sb.AppendLine($"        <div class=\"alert-title\">{title}</div>");
        sb.AppendLine($"        <div class=\"alert-level\">{level.ToUpperInvariant()} ALERT</div>");
        sb.AppendLine("    </div>");

        // 告警内容
        sb.AppendLine("    <div class=\"alert-content\">");
        sb.AppendLine($"        <div class=\"alert-message\">{message}</div>");

        // 标签
        if (tags?.Length > 0)
        {
            sb.AppendLine("        <div class=\"alert-tags\">");
            foreach (var tag in tags)
            {
                sb.AppendLine($"            <span class=\"tag\">{tag}</span>");
            }
            sb.AppendLine("        </div>");
        }

        // 时间
        sb.AppendLine($"        <div class=\"alert-time\">Time: {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss.fff UTC}</div>");
        sb.AppendLine("    </div>");

        // 异常信息
        if (exception is not null)
        {
            sb.AppendLine("    <div class=\"exception-section\">");
            sb.AppendLine("        <h3 class=\"exception-title\">Exception Details</h3>");
            sb.AppendLine("        <div class=\"exception-details\">");
            sb.AppendLine($"            <strong>Type:</strong> {exception.GetType().Name}<br>");
            sb.AppendLine($"            <strong>Message:</strong> {exception.Message}<br>");
            sb.AppendLine($"            <strong>Stack Trace:</strong><br>");
            sb.AppendLine($"            <pre>{exception.StackTrace}</pre>");
            sb.AppendLine("        </div>");
            sb.AppendLine("    </div>");
        }

        // HTML 尾部
        sb.AppendLine("</body>");
        sb.AppendLine("</html>");

        return sb.ToString();
    }

    /// <summary>
    /// 发送告警到企业微信
    /// </summary>
    /// <param name="level">告警级别</param>
    /// <param name="title">告警标题</param>
    /// <param name="message">告警消息</param>
    /// <param name="tags">标签 (可选)</param>
    /// <param name="exception">异常对象 (可选)</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>表示异步操作的任务</returns>
    private async Task SendToWeChatWorkAsync(
        string level,
        string title,
        string message,
        string[]? tags,
        Exception? exception,
        CancellationToken cancellationToken)
    {
        var client = httpClientFactory.CreateClient();
        client.Timeout = TimeSpan.FromSeconds(options.HttpClientTimeoutSeconds);

        // 构建 Markdown 消息内容
        var markdownContent = BuildWeChatWorkMarkdownContent(level, title, message, tags, exception);
        var request = new WeChatWorkMarkdownMessageRequest
        {
            Markdown = new WeChatWorkMarkdownContent { Content = markdownContent }
        };

        // 带重试发送
        // 参考文档：https://developer.work.weixin.qq.com/document/path/91770
        await SendWebhookWithRetryAsync(
            client,
            options.WeChatWork!.WebhookUrl,
            request,
            options.MaxRetryCount,
            options.RetryDelayMs,
            async (response) =>
            {
                var result = await response.Content.ReadFromJsonAsync<WeChatWorkWebhookResponse>(_jsonSerializerOptions, cancellationToken);
                return result is not null && result.ErrorCode == 0;
            },
            cancellationToken); 
    }

    /// <summary>
    /// 构建企业微信 Markdown 内容
    /// </summary>
    /// <param name="level">告警级别</param>
    /// <param name="title">告警标题</param>
    /// <param name="message">告警消息</param>
    /// <param name="tags">标签 (可选)</param>
    /// <param name="exception">异常对象 (可选)</param>
    /// <returns>企业微信 Markdown 内容</returns>
    private static string BuildWeChatWorkMarkdownContent(
        string level,
        string title,
        string message,
        string[]? tags,
        Exception? exception)
    {
        StringBuilder sb = new();

        // 标题（带颜色）
        var levelColor = level switch
        {
            "Info" => "info",
            "Warning" => "warning",
            "Error" => "warning",
            _ => "comment"
        };
        sb.AppendLine($"## <font color=\"{levelColor}\">{level.ToUpperInvariant()} ALERT</font>");
        sb.AppendLine();

        // 告警标题
        sb.AppendLine($"**{title}**");
        sb.AppendLine();

        // 告警消息
        sb.AppendLine($"> {message}");
        sb.AppendLine();

        // 标签
        if (tags?.Length > 0)
        {
            sb.AppendLine($"**Tags**: {string.Join(", ", tags)}");
            sb.AppendLine();
        }

        // 时间
        sb.AppendLine($"**Time**: {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss.fff UTC}");

        // 异常信息
        if (exception is not null)
        {
            sb.AppendLine();
            sb.AppendLine("**Exception Details**:");
            sb.AppendLine($"> Type: {exception.GetType().Name}");
            sb.AppendLine($"> Message: {exception.Message}");
        }

        return sb.ToString();
    }

    /// <summary>
    /// 发送告警到钉钉 (扩展点，实际项目中需实现 Webhook 调用)
    /// </summary>
    /// <param name="level">告警级别</param>
    /// <param name="title">告警标题</param>
    /// <param name="message">告警消息</param>
    /// <param name="exception">异常对象 (可选)</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>表示异步操作的任务</returns>
    private async Task SendToDingTalkAsync(
        string level,
        string title,
        string message,
        string[]? tags,
        Exception? exception,
        CancellationToken cancellationToken)
    {
        var client = httpClientFactory.CreateClient();
        client.Timeout = TimeSpan.FromSeconds(options.HttpClientTimeoutSeconds);

        // 构建 Markdown 消息内容
        var markdownText = BuildDingTalkMarkdownText(level, title, message, tags, exception);
        var request = new DingTalkMarkdownMessageRequest
        {
            Markdown = new DingTalkMarkdownContent
            {
                Title = $"{level.ToUpperInvariant()} ALERT: {title}",
                Text = markdownText
            },
            At = new DingTalkAtContent
            {
                AtMobiles = options.DingTalk!.MentionedMobiles,
                AtUserIds = null,
                IsAtAll = options.DingTalk.MentionAll
            }
        };

        // 带重试发送
        // 参考文档：https://open.dingtalk.com/document/robots/custom-robot-access
        await SendWebhookWithRetryAsync(
            client,
            options.DingTalk!.WebhookUrl,
            request,
            options.MaxRetryCount,
            options.RetryDelayMs,
            async (response) =>
            {
                var result = await response.Content.ReadFromJsonAsync<DingTalkWebhookResponse>(_jsonSerializerOptions, cancellationToken);
                return result is not null && result.ErrorCode == 0;
            },
            cancellationToken);
    }

    /// <summary>
    /// 构建钉钉 Markdown 文本内容
    /// </summary>
    /// <param name="level">告警级别</param>
    /// <param name="title">告警标题</param>
    /// <param name="message">告警消息</param>
    /// <param name="tags">标签 (可选)</param>
    /// <param name="exception">异常对象 (可选)</param>
    /// <returns>钉钉 Markdown 文本内容</returns>
    private static string BuildDingTalkMarkdownText(
        string level,
        string title,
        string message,
        string[]? tags,
        Exception? exception)
    {
        StringBuilder sb = new();

        // 标题
        var levelEmoji = level switch
        {
            "Info" => "ℹ️",
            "Warning" => "⚠️",
            "Error" => "🔴",
            _ => "📢"
        };
        sb.AppendLine($"# {levelEmoji} {level.ToUpperInvariant()} ALERT");
        sb.AppendLine();

        // 告警标题
        sb.AppendLine($"## {title}");
        sb.AppendLine();

        // 告警消息
        sb.AppendLine($"**{message}**");
        sb.AppendLine();

        // 标签
        if (tags?.Length > 0)
        {
            sb.AppendLine($"**Tags**: {string.Join(", ", tags)}");
            sb.AppendLine();
        }

        // 时间
        sb.AppendLine($"**Time**: {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss.fff UTC}");

        // 异常信息
        if (exception is not null)
        {
            sb.AppendLine();
            sb.AppendLine("---");
            sb.AppendLine("## Exception Details");
            sb.AppendLine($"**Type**: {exception.GetType().Name}");
            sb.AppendLine($"**Message**: {exception.Message}");
            sb.AppendLine($"**Stack Trace**:");
            sb.AppendLine("```");
            sb.AppendLine(exception.StackTrace);
            sb.AppendLine("```");
        }

        return sb.ToString();
    }

    /// <summary>
    /// 带重试机制的 Webhook 发送
    /// </summary>
    /// <typeparam name="TRequest">请求类型</typeparam>
    /// <param name="client">HTTP 客户端</param>
    /// <param name="url">Webhook URL</param>
    /// <param name="request">请求对象</param>
    /// <param name="maxRetryCount">最大重试次数</param>
    /// <param name="retryDelayMs">重试间隔 (毫秒)</param>
    /// <param name="validateResponse">验证响应的委托</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>表示异步操作的任务</returns>
    private static async Task SendWebhookWithRetryAsync<TRequest>(
        HttpClient client,
        string url,
        TRequest request,
        int maxRetryCount,
        int retryDelayMs,
        Func<HttpResponseMessage, Task<bool>> validateResponse,
        CancellationToken cancellationToken)
    {
        int retryCount = 0;
        Exception? lastException = null;

        while (retryCount < maxRetryCount)
        {
            try
            {
                using var httpRequest = new HttpRequestMessage(HttpMethod.Post, url);
                httpRequest.Content = JsonContent.Create(request, options: _jsonSerializerOptions);

                using var response = await client.SendAsync(httpRequest, cancellationToken);
                
                if (await validateResponse(response)) return;

                // 响应验证失败，记录并重试
                var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
                lastException = new InvalidOperationException($"Webhook request failed with status code {response.StatusCode}. Response: {errorContent}");
            }
            catch (HttpRequestException ex)
            {
                // HTTP 请求异常，重试
                lastException = ex;
            }
            catch (JsonException ex)
            {
                // JSON 序列化异常，不重试
                throw new InvalidOperationException("Failed to serialize webhook request.", ex);
            }

            retryCount++;
            if (retryCount < maxRetryCount)
                await Task.Delay(retryDelayMs, cancellationToken);
        }

        // 所有重试都失败
        throw new InvalidOperationException($"Failed to send webhook after {maxRetryCount} retries.", lastException);
    }
}
