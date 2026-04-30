namespace BuildingBlocks.Core.Common;

/// <summary>
/// Keyed Services 统一键名常量
/// 核心职责: 集中管理所有命名服务的键名，禁止魔法字符串
/// 使用场景: .NET 8+ Keyed Services 依赖注入时使用
/// </summary>
public static class KeyedServiceKeys
{
    #region 推送渠道键名
    /// <summary>
    /// 短信推送渠道
    /// </summary>
    public const string SmsChannel = "Sms";

    /// <summary>
    /// 邮件推送渠道
    /// </summary>
    public const string EmailChannel = "Email";

    /// <summary>
    /// 企业微信推送渠道
    /// </summary>
    public const string WeChatWorkChannel = "WeChatWork";

    /// <summary>
    /// 钉钉推送渠道
    /// </summary>
    public const string DingTalkChannel = "DingTalk";
    #endregion

    #region 存储服务键名
    /// <summary>
    /// 本地文件系统存储
    /// </summary>
    public const string LocalFileSystemStorage = "LocalFileSystem";

    /// <summary>
    /// MinIO 对象存储
    /// </summary>
    public const string MinioStorage = "Minio";

    /// <summary>
    /// 阿里云 OSS 存储
    /// </summary>
    public const string AliyunOssStorage = "AliyunOss";

    /// <summary>
    /// 腾讯云 COS 存储
    /// </summary>
    public const string TencentCosStorage = "TencentCos";
    #endregion

    #region 消息队列键名
    /// <summary>
    /// RabbitMQ 消息队列
    /// </summary>
    public const string RabbitMq = "RabbitMQ";

    /// <summary>
    /// Kafka 消息队列
    /// </summary>
    public const string Kafka = "Kafka";
    #endregion

    #region 缓存键名
    /// <summary>
    /// Redis 分布式缓存
    /// </summary>
    public const string RedisCache = "Redis";

    /// <summary>
    /// 内存缓存
    /// </summary>
    public const string MemoryCache = "Memory";
    #endregion
}
