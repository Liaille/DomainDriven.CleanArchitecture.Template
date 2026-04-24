namespace BuildingBlocks.Logging.Configuration;

/// <summary>
/// 日志输出端配置模型 (类型安全的日志配置)
/// <para>(Type-safe logging configuration)</para>
/// </summary>
public class SerilogSinkOptions
{
    /// <summary>
    /// 文件输出配置
    /// </summary>
    public SerilogFileSinkOptions File { get; set; } = new();

    /// <summary>
    /// Elasticsearch 输出配置
    /// </summary>
    public SerilogElasticsearchSinkOptions? Elasticsearch { get; set; }

    /// <summary>
    /// Grafana Loki 输出配置
    /// </summary>
    public SerilogLokiSinkOptions? GrafanaLoki { get; set; }
}
