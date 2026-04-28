using System.Text.Json.Serialization;

namespace BuildingBlocks.Monitoring.Configuration.WeChatWork;

/// <summary>
/// 企业微信 Webhook 响应模型
/// </summary>
public record WeChatWorkWebhookResponse
{
    [JsonPropertyName("errcode")]
    public int ErrorCode { get; set; }

    [JsonPropertyName("errmsg")]
    public string ErrorMessage { get; set; } = string.Empty;
}
