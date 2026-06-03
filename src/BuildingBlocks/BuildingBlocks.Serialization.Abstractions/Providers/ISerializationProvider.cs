using BuildingBlocks.Serialization.Abstractions.Enums;

namespace BuildingBlocks.Serialization.Abstractions.Providers;

/// <summary>
/// 统一序列化提供者接口
/// 【设计模式】工厂模式，根据格式获取对应的序列化器
/// 【核心价值】
/// 1. 业务层仅依赖此接口，与具体序列化库完全解耦
/// 2. 统一管理所有序列化器，支持动态添加/替换实现
/// 3. 提供统一的错误处理和能力检查
/// 【强制约束】
/// 所有业务代码必须通过此接口获取序列化器，禁止直接引用具体序列化库
/// </summary>
public interface ISerializationProvider
{
    /// <summary>
    /// 获取JSON序列化器 (唯一强制支持的格式)
    /// 【快捷访问】避免每次都调用GetSerializer
    /// </summary>
    IJsonSerializer JsonSerializer { get; }

    /// <summary>
    /// 获取默认序列化格式
    /// </summary>
    SerializationFormat DefaultFormat { get; }

    /// <summary>
    /// 获取指定格式的序列化器
    /// </summary>
    /// <param name="format">目标序列化格式</param>
    /// <returns>对应格式的序列化器</returns>
    /// <exception cref="Core.Exceptions.InternalException">未实现指定格式时抛出，错误码：UnsupportedFormat</exception>
    ISerializer GetSerializer(SerializationFormat format);

    /// <summary>
    /// 获取指定格式的强类型序列化器
    /// 【类型安全】避免类型转换异常
    /// </summary>
    /// <typeparam name="TSerializer">序列化器接口类型</typeparam>
    /// <param name="format">目标序列化格式</param>
    /// <returns>对应格式的强类型序列化器</returns>
    /// <exception cref="Core.Exceptions.InternalException">未实现指定格式或类型不匹配时抛出</exception>
    TSerializer GetSerializer<TSerializer>(SerializationFormat format)
        where TSerializer : class, ISerializer;

    /// <summary>
    /// 检查是否支持指定格式
    /// </summary>
    /// <param name="format">要检查的序列化格式</param>
    /// <returns>true表示支持，false表示不支持</returns>
    bool SupportsFormat(SerializationFormat format);

    /// <summary>
    /// 获取所有支持的序列化格式
    /// </summary>
    /// <returns>支持的格式列表</returns>
    IReadOnlyList<SerializationFormat> GetSupportedFormats();
}
