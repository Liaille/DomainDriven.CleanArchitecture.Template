using Elastic.Channels;
using Elastic.Ingest.Elasticsearch;
using System.ComponentModel.DataAnnotations;

namespace BuildingBlocks.Logging.Configuration;

/// <summary>
/// Elasticsearch 输出配置
/// </summary>
public class SerilogElasticsearchSinkOptions
{
    /// <summary>
    /// ES集群节点地址数组（支持单节点/多节点集群）
    /// </summary>
    [Required(ErrorMessage = "Elasticsearch节点地址不能为空")]
    public string[] NodeUrls { get; set; } = [];

    /// <summary>
    /// 数据流前缀（官方标准DataStream命名：{prefix}-{type}-{dataset}）
    /// </summary>
    public string DataStreamPrefix { get; set; } = "logs";

    /// <summary>
    /// 数据流类型
    /// </summary>
    public string DataStreamType { get; set; } = "dotnet";

    /// <summary>
    /// 数据流数据集（一般为应用/服务名称）
    /// </summary>
    public string DataStreamDataset { get; set; } = "app";

    /// <summary>
    /// 绑定ES ILM索引生命周期策略名称
    /// </summary>
    public string? IlmPolicy { get; set; }

    /// <summary>
    /// 模板自动安装引导策略
    /// </summary>
    public BootstrapMethod BootstrapMethod { get; set; } = BootstrapMethod.Failure;

    /// <summary>
    /// 是否启用集群嗅探（多节点集群推荐开启）
    /// </summary>
    public bool UseSniffing { get; set; } = false;

    /// <summary>
    /// Basic用户名密码认证配置
    /// </summary>
    public SerilogElasticsearchAuthOptions? BasicAuth { get; set; }

    /// <summary>
    /// API Key认证配置（ES8+生产推荐）
    /// </summary>
    public string? ApiKey { get; set; }

    /// <summary>
    /// 服务端证书指纹（自签名证书场景使用）
    /// </summary>
    public string? CertificateFingerprint { get; set; }

    /// <summary>
    /// HTTP请求超时时间（秒）
    /// </summary>
    public int RequestTimeoutSeconds { get; set; } = 5;

    /// <summary>
    /// 请求失败最大重试次数
    /// </summary>
    public int MaxRetries { get; set; } = 2;

    /// <summary>
    /// 通道缓冲区配置
    /// </summary>
    public BufferOptions? BufferOptions { get; set; }
}

/// <summary>
/// Basic认证配置
/// </summary>
public class SerilogElasticsearchAuthOptions
{
    /// <summary>
    /// 用户名
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// 密码
    /// </summary>
    public string Password { get; set; } = string.Empty;
}
