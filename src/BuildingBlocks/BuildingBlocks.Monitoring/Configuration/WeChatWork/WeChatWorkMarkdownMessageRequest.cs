using System.Text.Json.Serialization;

namespace BuildingBlocks.Monitoring.Configuration.WeChatWork;

/// <summary>
/// 企业微信 Markdown 消息请求模型
/// </summary>
public record WeChatWorkMarkdownMessageRequest
{
    [JsonPropertyName("msgtype")]
    public string MsgType => "markdown";

    [JsonPropertyName("markdown")]
    public WeChatWorkMarkdownContent Markdown { get; set; } = null!;
}
