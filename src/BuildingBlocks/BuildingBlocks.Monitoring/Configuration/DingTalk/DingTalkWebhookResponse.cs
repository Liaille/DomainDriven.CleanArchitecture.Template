using System.Text.Json.Serialization;

namespace BuildingBlocks.Monitoring.Configuration.DingTalk;

/// <summary>
/// 钉钉 Webhook 响应模型
/// </summary>
public record DingTalkWebhookResponse
{
    [JsonPropertyName("errcode")]
    public int ErrorCode { get; set; }

    [JsonPropertyName("errmsg")]
    public string ErrorMessage { get; set; } = string.Empty;
}
