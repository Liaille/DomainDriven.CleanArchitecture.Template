using System.Text.Json.Serialization;

namespace BuildingBlocks.Monitoring.Configuration.WeChatWork;

/// <summary>
/// 企业微信文本内容
/// </summary>
public record WeChatWorkTextContent
{
    [JsonPropertyName("content")]
    public string Content { get; set; } = string.Empty;

    [JsonPropertyName("mentioned_list")]
    public string[]? MentionedList { get; set; }

    [JsonPropertyName("mentioned_mobile_list")]
    public string[]? MentionedMobileList { get; set; }
}
