using System.ComponentModel.DataAnnotations;

namespace BuildingBlocks.Monitoring.Configuration;

/// <summary>
/// 告警配置
/// </summary>
public class AlertOptions
{
    /// <summary>
    /// 是否启用告警
    /// </summary>
    public bool Enabled { get; set; } = true;

    /// <summary>
    /// 告警级别阈值 (Info/Warning/Error)
    /// </summary>
    [Required(ErrorMessage = "Minimum alert level is required.")]
    public string MinimumAlertLevel { get; set; } = "Warning";

    /// <summary>
    /// Webhook 发送最大重试次数
    /// </summary>
    [Range(1, 10, ErrorMessage = "Max retry count must be between 1 and 10.")]
    public int MaxRetryCount { get; set; } = 3;

    /// <summary>
    /// Webhook 重试间隔 (毫秒)
    /// </summary>
    [Range(100, 30000, ErrorMessage = "Retry delay must be between 100ms and 30000ms.")]
    public int RetryDelayMs { get; set; } = 1000;

    /// <summary>
    /// HTTP 请求超时时间 (秒)
    /// </summary>
    [Range(1, 120, ErrorMessage = "HTTP timeout must be between 1 and 120 seconds.")]
    public int HttpClientTimeoutSeconds { get; set; } = 10;

    /// <summary>
    /// 邮件告警配置 (可选)
    /// </summary>
    public EmailAlertOptions? Email { get; set; }

    /// <summary>
    /// 企业微信 Webhook 配置 (可选)
    /// </summary>
    public WeChatWorkAlertOptions? WeChatWork { get; set; }

    /// <summary>
    /// 钉钉 Webhook 配置 (可选)
    /// </summary>
    public DingTalkAlertOptions? DingTalk { get; set; }
}

/// <summary>
/// 邮件告警配置
/// </summary>
public class EmailAlertOptions
{
    /// <summary>
    /// SMTP 服务器地址
    /// </summary>
    [Required(ErrorMessage = "SMTP host is required.")]
    public string SmtpHost { get; set; } = string.Empty;

    /// <summary>
    /// SMTP 端口
    /// </summary>
    [Range(1, 65535, ErrorMessage = "SMTP port must be between 1 and 65535.")]
    public int SmtpPort { get; set; } = 587;

    /// <summary>
    /// SMTP 用户名
    /// </summary>
    public string? SmtpUsername { get; set; }

    /// <summary>
    /// SMTP 密码
    /// </summary>
    public string? SmtpPassword { get; set; }

    /// <summary>
    /// 发件人地址
    /// </summary>
    [Required(ErrorMessage = "From address is required.")]
    [EmailAddress(ErrorMessage = "Invalid from email address.")]
    public string FromAddress { get; set; } = string.Empty;

    /// <summary>
    /// 发件人显示名称 (可选)
    /// </summary>
    public string? FromDisplayName { get; set; }

    /// <summary>
    /// 收件人地址列表
    /// </summary>
    [Required(ErrorMessage = "To addresses are required.")]
    [MinLength(1, ErrorMessage = "At least one to address is required.")]
    public string[] ToAddresses { get; set; } = [];

    /// <summary>
    /// 抄送地址列表 (可选)
    /// </summary>
    public string[]? CcAddresses { get; set; }

    /// <summary>
    /// 密送地址列表 (可选)
    /// </summary>
    public string[]? BccAddresses { get; set; }

    /// <summary>
    /// 是否启用 SSL/TLS
    /// </summary>
    public bool EnableSsl { get; set; } = true;

    /// <summary>
    /// 邮件主题前缀 (可选)
    /// </summary>
    public string? SubjectPrefix { get; set; }
}

/// <summary>
/// 企业微信告警配置
/// </summary>
public class WeChatWorkAlertOptions
{
    /// <summary>
    /// Webhook 地址
    /// </summary>
    [Required(ErrorMessage = "WeChat Work webhook URL is required.")]
    public string WebhookUrl { get; set; } = string.Empty;

    /// <summary>
    /// 密钥 (可选，用于签名验证)
    /// </summary>
    public string? Secret { get; set; }

    /// <summary>
    /// @ 指定用户的 UserId 列表 (可选)
    /// </summary>
    public string[]? MentionedUserIds { get; set; }

    /// <summary>
    /// 是否 @ 所有人
    /// </summary>
    public bool MentionAll { get; set; }
}

/// <summary>
/// 钉钉告警配置
/// </summary>
public class DingTalkAlertOptions
{
    /// <summary>
    /// Webhook 地址
    /// </summary>
    [Required(ErrorMessage = "DingTalk webhook URL is required.")]
    public string WebhookUrl { get; set; } = string.Empty;

    /// <summary>
    /// 加签密钥 (可选)
    /// </summary>
    public string? Secret { get; set; }

    /// <summary>
    /// @ 指定用户的手机号列表 (可选)
    /// </summary>
    public string[]? MentionedMobiles { get; set; }

    /// <summary>
    /// 是否 @ 所有人
    /// </summary>
    public bool MentionAll { get; set; }
}
