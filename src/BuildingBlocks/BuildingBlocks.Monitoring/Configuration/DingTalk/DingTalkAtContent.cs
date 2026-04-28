using System.Text.Json.Serialization;

namespace BuildingBlocks.Monitoring.Configuration.DingTalk;

/// <summary>
/// 钉钉 @ 内容
/// </summary>
public record DingTalkAtContent
{
    [JsonPropertyName("atMobiles")]
    public string[]? AtMobiles { get; set; }

    [JsonPropertyName("atUserIds")]
    public string[]? AtUserIds { get; set; }

    [JsonPropertyName("isAtAll")]
    public bool IsAtAll { get; set; }
}
