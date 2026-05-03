namespace Domain.Shared.Enums;

/// <summary>
/// 通知渠道枚举
/// 业务层面的通知渠道定义，与BuildingBlocks.Core的 KeyedServiceKeys 对应
/// </summary>
public enum NotificationChannel
{
    /// <summary>
    /// 短信
    /// </summary>
    Sms = 0,

    /// <summary>
    /// 邮件
    /// </summary>
    Email = 1,

    /// <summary>
    /// 企业微信
    /// </summary>
    WeChatWork = 2,

    /// <summary>
    /// 钉钉
    /// </summary>
    DingTalk = 3,

    /// <summary>
    /// 系统内通知
    /// </summary>
    System = 4,

    /// <summary>
    /// 推送通知
    /// </summary>
    Push = 5
}
