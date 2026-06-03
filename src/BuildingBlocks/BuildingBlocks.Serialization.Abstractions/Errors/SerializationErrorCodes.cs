namespace BuildingBlocks.Serialization.Abstractions.Errors;

/// <summary>
/// 序列化模块公共错误码常量
/// 严格遵循10位格式: [错误类型(1)][严重级别(1)][业务线(2)][模块(3)][错误编码(3)]
/// </summary>
public static class SerializationErrorCodes
{
    /// <summary>
    /// 序列化失败
    /// </summary>
    public const string SerializationFailed = "S101002001";

    /// <summary>
    /// 反序列化失败
    /// </summary>
    public const string DeserializationFailed = "S101002002";

    /// <summary>
    /// 不支持的序列化格式
    /// </summary>
    public const string UnsupportedFormat = "S101002003";

    /// <summary>
    /// JSON上下文缺失 (必须使用源生成器)
    /// </summary>
    public const string MissingJsonSerializerContext = "S201002101";

    /// <summary>
    /// JSON解析失败
    /// </summary>
    public const string JsonParseError = "S101002102";

    /// <summary>
    /// 二进制序列化失败
    /// </summary>
    public const string BinarySerializationFailed = "S101002201";

    /// <summary>
    /// 二进制反序列化失败
    /// </summary>
    public const string BinaryDeserializationFailed = "S101002202";

    /// <summary>
    /// XML序列化失败
    /// </summary>
    public const string XmlSerializationFailed = "S101002301";

    /// <summary>
    /// XML反序列化失败
    /// </summary>
    public const string XmlDeserializationFailed = "S101002302";

    /// <summary>
    /// 非AOT安全的序列化调用 (触发强制约束)
    /// </summary>
    public const string AotUnsafeCallDetected = "S201002901";
}
