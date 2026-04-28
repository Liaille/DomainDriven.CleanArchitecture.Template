using System.Text.Json.Serialization;

namespace BuildingBlocks.Monitoring.Configuration.DingTalk;

/// <summary>
/// 钉钉文本内容
/// </summary>
public record DingTalkTextContent
{
    [JsonPropertyName("content")]
    public string Content { get; set; } = string.Empty;
}
