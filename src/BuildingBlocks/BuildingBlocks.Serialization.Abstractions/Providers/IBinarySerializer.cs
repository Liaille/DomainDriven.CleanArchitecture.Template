namespace BuildingBlocks.Serialization.Abstractions.Providers;

/// <summary>
/// 二进制序列化器通用接口
/// 【适用场景】Protobuf、MessagePack等高性能二进制序列化格式
/// 【强制约束】
/// 1. 严格遵循.NET 10 AOT要求，必须使用对应格式的源生成器
/// 2. 禁止使用任何基于反射的序列化实现
/// 3. 所有异常统一抛出InternalException，使用SerializationErrorCodes中定义的错误码
/// 【设计原则】
/// 提供字节数组和流两种序列化/反序列化形式，覆盖所有高性能场景
/// </summary>
public interface IBinarySerializer : ISerializer
{
    #region 序列化 (同步方法)

    /// <summary>
    /// 序列化对象为二进制字节数组
    /// </summary>
    /// <typeparam name="T">可序列化类型 (必须标记对应格式的源生成器特性)</typeparam>
    /// <param name="value">待序列化对象</param>
    /// <returns>二进制字节数组</returns>
    /// <exception cref="Core.Exceptions.InternalException">序列化失败时抛出，错误码：BinarySerializationFailed</exception>
    byte[] Serialize<T>(T? value);

    /// <summary>
    /// 序列化对象到指定流
    /// 【高性能推荐】直接写入流，避免内存拷贝，适合大对象序列化
    /// </summary>
    /// <typeparam name="T">可序列化类型 (必须标记对应格式的源生成器特性)</typeparam>
    /// <param name="value">待序列化对象</param>
    /// <param name="stream">目标输出流</param>
    /// <exception cref="Core.Exceptions.InternalException">序列化失败时抛出，错误码：BinarySerializationFailed</exception>
    void SerializeToStream<T>(T? value, Stream stream);

    #endregion

    #region 序列化 (异步方法)

    /// <summary>
    /// 异步序列化对象为二进制字节数组
    /// </summary>
    /// <typeparam name="T">可序列化类型 (必须标记对应格式的源生成器特性)</typeparam>
    /// <param name="value">待序列化对象</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>二进制字节数组</returns>
    /// <exception cref="Core.Exceptions.InternalException">序列化失败时抛出，错误码：BinarySerializationFailed</exception>
    Task<byte[]> SerializeAsync<T>(T? value, CancellationToken cancellationToken = default);

    /// <summary>
    /// 异步序列化对象到指定流
    /// 【高性能推荐】直接写入流，避免内存拷贝，适合大对象序列化
    /// </summary>
    /// <typeparam name="T">可序列化类型 (必须标记对应格式的源生成器特性)</typeparam>
    /// <param name="value">待序列化对象</param>
    /// <param name="stream">目标输出流</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <exception cref="Core.Exceptions.InternalException">序列化失败时抛出，错误码：BinarySerializationFailed</exception>
    Task SerializeToStreamAsync<T>(T? value, Stream stream, CancellationToken cancellationToken = default);

    #endregion

    #region 反序列化 (同步方法)

    /// <summary>
    /// 反序列化二进制字节数组为对象
    /// </summary>
    /// <typeparam name="T">目标类型 (必须标记对应格式的源生成器特性)</typeparam>
    /// <param name="bytes">二进制字节数组</param>
    /// <returns>反序列化后的对象</returns>
    /// <exception cref="Core.Exceptions.InternalException">反序列化失败时抛出，错误码：BinaryDeserializationFailed</exception>
    T? Deserialize<T>(byte[] bytes);

    /// <summary>
    /// 从指定流反序列化为对象
    /// 【高性能推荐】直接从流读取，避免内存拷贝，适合大对象反序列化
    /// </summary>
    /// <typeparam name="T">目标类型 (必须标记对应格式的源生成器特性)</typeparam>
    /// <param name="stream">输入流</param>
    /// <returns>反序列化后的对象</returns>
    /// <exception cref="Core.Exceptions.InternalException">反序列化失败时抛出，错误码：BinaryDeserializationFailed</exception>
    T? DeserializeFromStream<T>(Stream stream);

    #endregion

    #region 反序列化 (异步方法)

    /// <summary>
    /// 异步反序列化二进制字节数组为对象
    /// </summary>
    /// <typeparam name="T">目标类型 (必须标记对应格式的源生成器特性)</typeparam>
    /// <param name="bytes">二进制字节数组</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>反序列化后的对象</returns>
    /// <exception cref="Core.Exceptions.InternalException">反序列化失败时抛出，错误码：BinaryDeserializationFailed</exception>
    Task<T?> DeserializeAsync<T>(byte[] bytes, CancellationToken cancellationToken = default);

    /// <summary>
    /// 异步从指定流反序列化为对象
    /// 【高性能推荐】直接从流读取，避免内存拷贝，适合大对象反序列化
    /// </summary>
    /// <typeparam name="T">目标类型 (必须标记对应格式的源生成器特性)</typeparam>
    /// <param name="stream">输入流</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>反序列化后的对象</returns>
    /// <exception cref="Core.Exceptions.InternalException">反序列化失败时抛出，错误码：BinaryDeserializationFailed</exception>
    Task<T?> DeserializeFromStreamAsync<T>(Stream stream, CancellationToken cancellationToken = default);

    #endregion
}

