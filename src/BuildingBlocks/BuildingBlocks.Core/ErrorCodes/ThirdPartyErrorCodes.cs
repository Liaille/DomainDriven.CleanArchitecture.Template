namespace BuildingBlocks.Core.ErrorCodes;

/// <summary>
/// 全局第三方错误码定义 (30000+ 号段)
/// <para>核心职责: 统一管理所有外部服务、第三方接口、推送渠道、存储服务错误码</para>
/// <para>强制约束: 禁止与系统/业务错误码号段冲突</para>
/// </summary>
public static class ThirdPartyErrorCodes
{
    #region 通用第三方错误 (30000-30099)
    /// <summary>
    /// 第三方服务调用失败
    /// </summary>
    public const int ThirdPartyServiceInvokeFailed = 30000;

    /// <summary>
    /// 第三方服务超时
    /// </summary>
    public const int ThirdPartyServiceTimeout = 30001;

    /// <summary>
    /// 第三方服务限流
    /// </summary>
    public const int ThirdPartyServiceRateLimited = 30002;

    /// <summary>
    /// 第三方服务认证失败
    /// </summary>
    public const int ThirdPartyAuthenticationFailed = 30003;
    #endregion

    #region 消息队列错误 (30100-30199)
    /// <summary>
    /// 消息队列连接失败
    /// </summary>
    public const int MessageQueueConnectionFailed = 30100;

    /// <summary>
    /// 消息发送失败
    /// </summary>
    public const int MessageSendFailed = 30101;

    /// <summary>
    /// 消息消费失败
    /// </summary>
    public const int MessageConsumeFailed = 30102;
    #endregion

    #region 缓存错误 (30200-30299)
    /// <summary>
    /// 缓存连接失败
    /// </summary>
    public const int CacheConnectionFailed = 30200;

    /// <summary>
    /// 缓存操作失败
    /// </summary>
    public const int CacheOperationFailed = 30201;
    #endregion

    #region 存储服务错误 (30300-30399)
    /// <summary>
    /// 对象存储连接失败
    /// </summary>
    public const int StorageConnectionFailed = 30300;

    /// <summary>
    /// 文件上传失败
    /// </summary>
    public const int FileUploadFailed = 30301;

    /// <summary>
    /// 文件下载失败
    /// </summary>
    public const int FileDownloadFailed = 30302;

    /// <summary>
    /// 文件删除失败
    /// </summary>
    public const int FileDeleteFailed = 30303;
    #endregion

    #region 推送渠道错误 (30400-30499)
    /// <summary>
    /// 短信推送失败
    /// </summary>
    public const int SmsPushFailed = 30400;

    /// <summary>
    /// 邮件推送失败
    /// </summary>
    public const int EmailPushFailed = 30401;

    /// <summary>
    /// 企业微信推送失败
    /// </summary>
    public const int WeWorkPushFailed = 30402;

    /// <summary>
    /// 钉钉推送失败
    /// </summary>
    public const int DingTalkPushFailed = 30403;
    #endregion

    #region 支付服务错误 (30500-30599)
    /// <summary>
    /// 支付服务调用失败
    /// </summary>
    public const int PaymentServiceFailed = 30500;

    /// <summary>
    /// 支付订单创建失败
    /// </summary>
    public const int PaymentOrderCreateFailed = 30501;

    /// <summary>
    /// 支付回调验证失败
    /// </summary>
    public const int PaymentCallbackVerifyFailed = 30502;
    #endregion
}