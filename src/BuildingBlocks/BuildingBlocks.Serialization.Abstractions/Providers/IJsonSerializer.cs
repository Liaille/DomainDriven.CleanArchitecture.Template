using System.Text.Json.Serialization;

namespace BuildingBlocks.Serialization.Abstractions.Providers;

/// <summary>
/// JSON序列化器核心接口
/// 【强制约束】
/// 1. 严格遵循.NET 10 AOT要求，所有方法必须使用源生成器生成的JsonSerializerContext
/// 2. 禁止使用任何无JsonSerializerContext参数的重载
/// 3. 所有异常统一抛出InternalException，使用SerializationErrorCodes中定义的错误码
/// 4. 仅依赖System.Text.Json，禁止使用Newtonsoft.Json
/// 【设计原则】
/// 提供字符串、字节数组、流三种序列化/反序列化形式，覆盖所有性能场景
/// </summary>
public interface IJsonSerializer : ISerializer
{
    #region 序列化 (同步方法)

    /// <summary>
    /// 序列化对象为JSON字符串
    /// </summary>
    /// <typeparam name="T">可序列化类型 (必须标记[AotSerializable])</typeparam>
    /// <param name="value">待序列化对象</param>
    /// <param name="context">源生成器生成的JsonSerializerContext</param>
    /// <returns>格式化后的JSON字符串</returns>
    /// <exception cref="Core.Exceptions.InternalException">序列化失败时抛出，错误码：SerializationFailed</exception>
    string Serialize<T>(T? value, JsonSerializerContext context);

    /// <summary>
    /// 序列化对象为UTF-8字节数组
    /// 【高性能推荐】避免字符串中间分配，适合网络传输和缓存场景
    /// </summary>
    /// <typeparam name="T">可序列化类型 (必须标记[AotSerializable])</typeparam>
    /// <param name="value">待序列化对象</param>
    /// <param name="context">源生成器生成的JsonSerializerContext</param>
    /// <returns>UTF-8编码的字节数组</returns>
    /// <exception cref="Core.Exceptions.InternalException">序列化失败时抛出，错误码：SerializationFailed</exception>
    byte[] SerializeToBytes<T>(T? value, JsonSerializerContext context);

    /// <summary>
    /// 序列化对象到指定流
    /// 【高性能推荐】直接写入流，避免内存拷贝，适合大对象序列化
    /// </summary>
    /// <typeparam name="T">可序列化类型 (必须标记[AotSerializable])</typeparam>
    /// <param name="value">待序列化对象</param>
    /// <param name="context">源生成器生成的JsonSerializerContext</param>
    /// <param name="stream">目标输出流</param>
    /// <exception cref="Core.Exceptions.InternalException">序列化失败时抛出，错误码：SerializationFailed</exception>
    void SerializeToStream<T>(T? value, JsonSerializerContext context, Stream stream);

    #endregion

    #region 序列化 (异步方法)

    /// <summary>
    /// 异步序列化对象为JSON字符串
    /// </summary>
    /// <typeparam name="T">可序列化类型 (必须标记[AotSerializable])</typeparam>
    /// <param name="value">待序列化对象</param>
    /// <param name="context">源生成器生成的JsonSerializerContext</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>格式化后的JSON字符串</returns>
    /// <exception cref="Core.Exceptions.InternalException">序列化失败时抛出，错误码：SerializationFailed</exception>
    Task<string> SerializeAsync<T>(T? value, JsonSerializerContext context, CancellationToken cancellationToken = default);

    /// <summary>
    /// 异步序列化对象为UTF-8字节数组
    /// 【高性能推荐】避免字符串中间分配，适合网络传输和缓存场景
    /// </summary>
    /// <typeparam name="T">可序列化类型 (必须标记[AotSerializable])</typeparam>
    /// <param name="value">待序列化对象</param>
    /// <param name="context">源生成器生成的JsonSerializerContext</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>UTF-8编码的字节数组</returns>
    /// <exception cref="Core.Exceptions.InternalException">序列化失败时抛出，错误码：SerializationFailed</exception>
    Task<byte[]> SerializeToBytesAsync<T>(T? value, JsonSerializerContext context, CancellationToken cancellationToken = default);

    /// <summary>
    /// 异步序列化对象到指定流
    /// 【高性能推荐】直接写入流，避免内存拷贝，适合大对象序列化
    /// </summary>
    /// <typeparam name="T">可序列化类型 (必须标记[AotSerializable])</typeparam>
    /// <param name="value">待序列化对象</param>
    /// <param name="context">源生成器生成的JsonSerializerContext</param>
    /// <param name="stream">目标输出流</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <exception cref="Core.Exceptions.InternalException">序列化失败时抛出，错误码：SerializationFailed</exception>
    Task SerializeToStreamAsync<T>(T? value, JsonSerializerContext context, Stream stream, CancellationToken cancellationToken = default);

    #endregion

    #region 反序列化 (同步方法)

    /// <summary>
    /// 反序列化JSON字符串为对象
    /// </summary>
    /// <typeparam name="T">目标类型 (必须标记[AotSerializable])</typeparam>
    /// <param name="json">JSON字符串</param>
    /// <param name="context">源生成器生成的JsonSerializerContext</param>
    /// <returns>反序列化后的对象</returns>
    /// <exception cref="Core.Exceptions.InternalException">反序列化失败时抛出，错误码：DeserializationFailed</exception>
    T? Deserialize<T>(string json, JsonSerializerContext context);

    /// <summary>
    /// 反序列化UTF-8字节数组为对象
    /// 【高性能推荐】避免字符串中间分配，适合网络传输和缓存场景
    /// </summary>
    /// <typeparam name="T">目标类型 (必须标记[AotSerializable])</typeparam>
    /// <param name="bytes">UTF-8编码的字节数组</param>
    /// <param name="context">源生成器生成的JsonSerializerContext</param>
    /// <returns>反序列化后的对象</returns>
    /// <exception cref="Core.Exceptions.InternalException">反序列化失败时抛出，错误码：DeserializationFailed</exception>
    T? DeserializeFromBytes<T>(byte[] bytes, JsonSerializerContext context);

    /// <summary>
    /// 从指定流反序列化为对象
    /// 【高性能推荐】直接从流读取，避免内存拷贝，适合大对象反序列化
    /// </summary>
    /// <typeparam name="T">目标类型 (必须标记[AotSerializable])</typeparam>
    /// <param name="stream">输入流</param>
    /// <param name="context">源生成器生成的JsonSerializerContext</param>
    /// <returns>反序列化后的对象</returns>
    /// <exception cref="Core.Exceptions.InternalException">反序列化失败时抛出，错误码：DeserializationFailed</exception>
    T? DeserializeFromStream<T>(Stream stream, JsonSerializerContext context);

    #endregion

    #region 反序列化 (异步方法)

    /// <summary>
    /// 异步反序列化JSON字符串为对象
    /// </summary>
    /// <typeparam name="T">目标类型 (必须标记[AotSerializable])</typeparam>
    /// <param name="json">JSON字符串</param>
    /// <param name="context">源生成器生成的JsonSerializerContext</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>反序列化后的对象</returns>
    /// <exception cref="Core.Exceptions.InternalException">反序列化失败时抛出，错误码：DeserializationFailed</exception>
    Task<T?> DeserializeAsync<T>(string json, JsonSerializerContext context, CancellationToken cancellationToken = default);

    /// <summary>
    /// 异步反序列化UTF-8字节数组为对象
    /// 【高性能推荐】避免字符串中间分配，适合网络传输和缓存场景
    /// </summary>
    /// <typeparam name="T">目标类型 (必须标记[AotSerializable])</typeparam>
    /// <param name="bytes">UTF-8编码的字节数组</param>
    /// <param name="context">源生成器生成的JsonSerializerContext</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>反序列化后的对象</returns>
    /// <exception cref="Core.Exceptions.InternalException">反序列化失败时抛出，错误码：DeserializationFailed</exception>
    Task<T?> DeserializeFromBytesAsync<T>(byte[] bytes, JsonSerializerContext context, CancellationToken cancellationToken = default);

    /// <summary>
    /// 异步从指定流反序列化为对象
    /// 【高性能推荐】直接从流读取，避免内存拷贝，适合大对象反序列化
    /// </summary>
    /// <typeparam name="T">目标类型 (必须标记[AotSerializable])</typeparam>
    /// <param name="stream">输入流</param>
    /// <param name="context">源生成器生成的JsonSerializerContext</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>反序列化后的对象</returns>
    /// <exception cref="Core.Exceptions.InternalException">反序列化失败时抛出，错误码：DeserializationFailed</exception>
    Task<T?> DeserializeFromStreamAsync<T>(Stream stream, JsonSerializerContext context, CancellationToken cancellationToken = default);

    #endregion
}
