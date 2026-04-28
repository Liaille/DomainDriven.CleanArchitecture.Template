using System.Text.Json.Serialization;

namespace BuildingBlocks.Monitoring.Configuration.WeChatWork;

/// <summary>
/// 企业微信文本消息请求模型
/// </summary>
public record WeChatWorkTextMessageRequest
{
    [JsonPropertyName("msgtype")]
    public static string MsgType  => "text";

    [JsonPropertyName("text")]
    public WeChatWorkTextContent Text { get; set; } = null!;
}
