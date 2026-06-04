using BuildingBlocks.Serialization.Abstractions.Enums;
using System.Diagnostics.CodeAnalysis;

namespace BuildingBlocks.Serialization.Abstractions.Providers;

/// <summary>
/// 统一序列化提供者接口
/// 【核心职责】根据格式提供对应的序列化器实例
/// 【设计原则】
/// 1. 单一职责: 仅负责提供序列化器，不暴露任何配置信息
/// 2. 强类型优先: 静态已知格式使用对应的强类型快捷属性
/// 3. 约定大于配置: 提供全局默认序列化器，减少开发人员决策成本
/// 4. 异常安全: 提供TryGet方法避免不必要的异常开销
/// 【强制约束】
/// 所有业务代码必须通过此接口获取序列化器，禁止直接引用具体序列化库
/// </summary>
public interface ISerializationProvider
{
    #region 强类型快捷属性 (静态已知格式优先使用)

    /// <summary>
    /// 获取JSON序列化器 (唯一强制支持的格式)
    /// 【保证非空】所有实现必须提供JSON序列化器
    /// 【推荐使用】绝大多数业务场景优先使用JSON格式
    /// </summary>
    IJsonSerializer JsonSerializer { get; }

    /// <summary>
    /// 获取Protobuf序列化器 (可选支持)
    /// 【可能为null】如果未注册Protobuf序列化器则返回null
    /// 【适用场景】gRPC通信、高性能消息队列、集成事件
    /// </summary>
    IProtobufSerializer? ProtobufSerializer { get; }

    /// <summary>
    /// 获取MessagePack序列化器 (可选支持)
    /// 【可能为null】如果未注册MessagePack序列化器则返回null
    /// 【适用场景】分布式缓存、高性能二进制传输
    /// </summary>
    IMessagePackSerializer? MessagePackSerializer { get; }

    /// <summary>
    /// 获取XML序列化器 (可选支持)
    /// 【可能为null】如果未注册XML序列化器则返回null
    /// 【适用场景】传统系统对接、第三方XML接口集成
    /// </summary>
    IXDocumentSerializer? XmlSerializer { get; }

    #endregion

    #region 默认序列化器 (约定大于配置)

    /// <summary>
    /// 获取全局默认格式的序列化器
    /// 【设计目的】实现"约定大于配置"，消除硬编码，统一全局序列化行为
    /// 【默认格式来源】内部依赖 ISerializationOptions.DefaultFormat 配置
    /// 【异常】如果默认格式未实现，抛出InternalException，错误码: UnsupportedFormat
    /// </summary>
    /// <returns>全局默认格式的序列化器实例</returns>
    /// <exception cref="Core.Exceptions.InternalException">默认格式未注册或不支持时抛出</exception>
    ISerializer GetDefaultSerializer();

    /// <summary>
    /// 获取全局默认格式的强类型序列化器
    /// 【适用场景】需要强类型访问默认序列化器的场景
    /// 【异常】如果默认格式未实现或类型不匹配，抛出InternalException
    /// </summary>
    /// <typeparam name="TSerializer">目标序列化器接口类型</typeparam>
    /// <returns>全局默认格式的强类型序列化器实例</returns>
    /// <exception cref="Core.Exceptions.InternalException">默认格式未注册或类型不匹配时抛出</exception>
    TSerializer GetDefaultSerializer<TSerializer>()
        where TSerializer : class, ISerializer;

    #endregion

    #region 通用工厂方法 (动态选择格式使用)

    /// <summary>
    /// 获取指定格式的序列化器
    /// 【适用场景】需要动态选择序列化格式的场景
    /// 【异常】如果指定格式未实现，抛出InternalException，错误码: UnsupportedFormat
    /// </summary>
    /// <param name="format">目标序列化格式</param>
    /// <returns>对应格式的序列化器实例</returns>
    /// <exception cref="Core.Exceptions.InternalException">指定格式未注册或不支持时抛出</exception>
    ISerializer GetSerializer(SerializationFormat format);

    /// <summary>
    /// 获取指定格式的强类型序列化器
    /// 【适用场景】需要动态选择格式且需要强类型访问的场景
    /// 【异常】如果指定格式未实现或类型不匹配，抛出InternalException
    /// </summary>
    /// <typeparam name="TSerializer">目标序列化器接口类型</typeparam>
    /// <param name="format">目标序列化格式</param>
    /// <returns>对应格式的强类型序列化器实例</returns>
    /// <exception cref="Core.Exceptions.InternalException">指定格式未注册或类型不匹配时抛出</exception>
    TSerializer GetSerializer<TSerializer>(SerializationFormat format)
        where TSerializer : class, ISerializer;

    #endregion

    #region 安全获取方法 (不确定格式是否支持时使用)

    /// <summary>
    /// 尝试获取指定格式的序列化器
    /// 【适用场景】不确定格式是否支持，希望优雅处理失败的场景
    /// 【无异常】不会抛出任何异常，失败时返回false
    /// </summary>
    /// <param name="format">目标序列化格式</param>
    /// <param name="serializer">输出参数，成功时为对应序列化器，失败时为null</param>
    /// <returns>true表示获取成功，false表示格式不支持</returns>
    bool TryGetSerializer(SerializationFormat format, [NotNullWhen(true)] out ISerializer? serializer);

    /// <summary>
    /// 尝试获取指定格式的强类型序列化器
    /// 【适用场景】不确定格式是否支持且需要强类型访问的场景
    /// 【无异常】不会抛出任何异常，失败时返回false
    /// </summary>
    /// <typeparam name="TSerializer">目标序列化器接口类型</typeparam>
    /// <param name="format">目标序列化格式</param>
    /// <param name="serializer">输出参数，成功时为对应序列化器，失败时为null</param>
    /// <returns>true表示获取成功，false表示格式不支持或类型不匹配</returns>
    bool TryGetSerializer<TSerializer>(SerializationFormat format, [NotNullWhen(true)] out TSerializer? serializer)
        where TSerializer : class, ISerializer;

    #endregion

    #region 能力检查方法

    /// <summary>
    /// 检查是否支持指定的序列化格式
    /// </summary>
    /// <param name="format">要检查的序列化格式</param>
    /// <returns>true表示支持，false表示不支持</returns>
    bool SupportsFormat(SerializationFormat format);

    /// <summary>
    /// 获取当前已注册的所有支持的序列化格式
    /// </summary>
    /// <returns>支持的格式列表</returns>
    IReadOnlyList<SerializationFormat> GetSupportedFormats();

    #endregion
}
