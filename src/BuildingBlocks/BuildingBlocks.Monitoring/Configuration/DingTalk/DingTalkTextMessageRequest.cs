using System.Text.Json.Serialization;

namespace BuildingBlocks.Monitoring.Configuration.DingTalk;

/// <summary>
/// 钉钉文本消息请求模型
/// </summary>
public record DingTalkTextMessageRequest
{
    [JsonPropertyName("msgtype")]
    public static string MsgType => "text";

    [JsonPropertyName("text")]
    public DingTalkTextContent Text { get; set; } = null!;

    [JsonPropertyName("at")]
    public DingTalkAtContent? At { get; set; }
}
