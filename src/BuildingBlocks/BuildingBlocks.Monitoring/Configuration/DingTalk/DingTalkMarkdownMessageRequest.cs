using System.Text.Json.Serialization;

namespace BuildingBlocks.Monitoring.Configuration.DingTalk;

/// <summary>
/// 钉钉 Markdown 消息请求模型
/// </summary>
public record DingTalkMarkdownMessageRequest
{
    [JsonPropertyName("msgtype")]
    public string MsgType => "markdown";

    [JsonPropertyName("markdown")]
    public DingTalkMarkdownContent Markdown { get; set; } = null!;

    [JsonPropertyName("at")]
    public DingTalkAtContent? At { get; set; }
}
