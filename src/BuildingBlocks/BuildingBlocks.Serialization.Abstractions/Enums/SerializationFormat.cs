namespace BuildingBlocks.Serialization.Abstractions.Enums;

/// <summary>
/// 支持的序列化格式枚举
/// <para>JSON、XML、Protobuf、MessagePack</para>
/// </summary>
public enum SerializationFormat
{
    /// <summary>
    /// 原生JSON (唯一强制支持)
    /// 使用 System.Text.Json
    /// </summary>
    Json = 0,

    /// <summary>
    /// Protocol Buffers (gRPC/MQ高并发场景)
    /// 使用 Google.Protobuf + 源生成器
    /// </summary>
    Protobuf = 1,

    /// <summary>
    /// MessagePack (高性能压缩缓存场景)
    /// 使用 MessagePack + 源生成器
    /// </summary>
    MessagePack = 2,

    /// <summary>
    /// XML (传统系统对接场景，可选支持)
    /// 使用 System.Xml.Linq/XDocument
    /// </summary>
    Xml = 3
}
