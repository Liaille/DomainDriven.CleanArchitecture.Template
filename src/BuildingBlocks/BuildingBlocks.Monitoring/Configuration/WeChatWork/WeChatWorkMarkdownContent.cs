using System.Text.Json.Serialization;

namespace BuildingBlocks.Monitoring.Configuration.WeChatWork;

/// <summary>
/// 企业微信 Markdown 内容
/// </summary>
public record WeChatWorkMarkdownContent
{
    [JsonPropertyName("content")]
    public string Content { get; set; } = string.Empty;
}
