namespace BuildingBlocks.Serialization.Abstractions.Providers;

/// <summary>
/// 所有序列化器的通用根接口
/// </summary>
public interface ISerializer
{
    /// <summary>
    /// 序列化器支持的格式
    /// </summary>
    Enums.SerializationFormat Format { get; }

    /// <summary>
    /// 是否支持原生数据压缩
    /// </summary>
    bool SupportsCompression { get; }

    /// <summary>
    /// 是否支持版本兼容序列化
    /// </summary>
    bool SupportsVersioning { get; }

    /// <summary>
    /// 是否完全支持.NET Native AOT编译
    /// </summary>
    bool SupportsAot { get; }

    /// <summary>
    /// 是否线程安全
    /// </summary>
    bool IsThreadSafe { get; }
}
