using System.Text.Json.Serialization;

namespace BuildingBlocks.Monitoring.Configuration.DingTalk;

/// <summary>
/// 钉钉 Markdown 内容
/// </summary>
public record DingTalkMarkdownContent
{
    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("text")]
    public string Text { get; set; } = string.Empty;
}
