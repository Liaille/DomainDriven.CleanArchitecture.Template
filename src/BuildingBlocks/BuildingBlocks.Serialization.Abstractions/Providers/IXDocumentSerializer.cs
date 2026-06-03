using System.Xml.Linq;

namespace BuildingBlocks.Serialization.Abstractions.Providers;

/// <summary>
/// XDocument/XML序列化器接口
/// 【适用场景】传统系统对接、第三方XML接口集成
/// 【强制约束】
/// 1. 仅使用System.Xml.Linq/XDocument实现
/// 2. 禁止使用Newtonsoft.Json.XmlSerializer
/// 3. 所有异常统一抛出InternalException，使用SerializationErrorCodes中定义的错误码
/// 【设计说明】
/// 作为可选能力提供，仅用于无法避免的XML对接场景，不推荐在新系统中使用
/// </summary>
public interface IXDocumentSerializer : ISerializer
{
    #region 序列化 (同步方法)

    /// <summary>
    /// 序列化对象为XDocument
    /// </summary>
    /// <typeparam name="T">可序列化类型</typeparam>
    /// <param name="value">待序列化对象</param>
    /// <returns>XDocument对象</returns>
    /// <exception cref="Core.Exceptions.InternalException">序列化失败时抛出，错误码：XmlSerializationFailed</exception>
    XDocument SerializeToXDocument<T>(T? value);

    /// <summary>
    /// 序列化对象为XML字符串
    /// </summary>
    /// <typeparam name="T">可序列化类型</typeparam>
    /// <param name="value">待序列化对象</param>
    /// <returns>格式化后的XML字符串</returns>
    /// <exception cref="Core.Exceptions.InternalException">序列化失败时抛出，错误码：XmlSerializationFailed</exception>
    string Serialize<T>(T? value);

    /// <summary>
    /// 序列化对象到指定流
    /// </summary>
    /// <typeparam name="T">可序列化类型</typeparam>
    /// <param name="value">待序列化对象</param>
    /// <param name="stream">目标输出流</param>
    /// <exception cref="Core.Exceptions.InternalException">序列化失败时抛出，错误码：XmlSerializationFailed</exception>
    void SerializeToStream<T>(T? value, Stream stream);

    #endregion

    #region 序列化 (异步方法)

    /// <summary>
    /// 异步序列化对象为XML字符串
    /// </summary>
    /// <typeparam name="T">可序列化类型</typeparam>
    /// <param name="value">待序列化对象</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>格式化后的XML字符串</returns>
    /// <exception cref="Core.Exceptions.InternalException">序列化失败时抛出，错误码：XmlSerializationFailed</exception>
    Task<string> SerializeAsync<T>(T? value, CancellationToken cancellationToken = default);

    /// <summary>
    /// 异步序列化对象到指定流
    /// </summary>
    /// <typeparam name="T">可序列化类型</typeparam>
    /// <param name="value">待序列化对象</param>
    /// <param name="stream">目标输出流</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <exception cref="Core.Exceptions.InternalException">序列化失败时抛出，错误码：XmlSerializationFailed</exception>
    Task SerializeToStreamAsync<T>(T? value, Stream stream, CancellationToken cancellationToken = default);

    #endregion

    #region 反序列化 (同步方法)

    /// <summary>
    /// 反序列化XDocument为对象
    /// </summary>
    /// <typeparam name="T">目标类型</typeparam>
    /// <param name="document">XDocument对象</param>
    /// <returns>反序列化后的对象</returns>
    /// <exception cref="Core.Exceptions.InternalException">反序列化失败时抛出，错误码：XmlDeserializationFailed</exception>
    T? DeserializeFromXDocument<T>(XDocument document);

    /// <summary>
    /// 反序列化XML字符串为对象
    /// </summary>
    /// <typeparam name="T">目标类型</typeparam>
    /// <param name="xml">XML字符串</param>
    /// <returns>反序列化后的对象</returns>
    /// <exception cref="Core.Exceptions.InternalException">反序列化失败时抛出，错误码：XmlDeserializationFailed</exception>
    T? Deserialize<T>(string xml);

    /// <summary>
    /// 从指定流反序列化为对象
    /// </summary>
    /// <typeparam name="T">目标类型</typeparam>
    /// <param name="stream">输入流</param>
    /// <returns>反序列化后的对象</returns>
    /// <exception cref="Core.Exceptions.InternalException">反序列化失败时抛出，错误码：XmlDeserializationFailed</exception>
    T? DeserializeFromStream<T>(Stream stream);

    #endregion

    #region 反序列化 (异步方法)

    /// <summary>
    /// 异步反序列化XML字符串为对象
    /// </summary>
    /// <typeparam name="T">目标类型</typeparam>
    /// <param name="xml">XML字符串</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>反序列化后的对象</returns>
    /// <exception cref="Core.Exceptions.InternalException">反序列化失败时抛出，错误码：XmlDeserializationFailed</exception>
    Task<T?> DeserializeAsync<T>(string xml, CancellationToken cancellationToken = default);

    /// <summary>
    /// 异步从指定流反序列化为对象
    /// </summary>
    /// <typeparam name="T">目标类型</typeparam>
    /// <param name="stream">输入流</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>反序列化后的对象</returns>
    /// <exception cref="Core.Exceptions.InternalException">反序列化失败时抛出，错误码：XmlDeserializationFailed</exception>
    Task<T?> DeserializeFromStreamAsync<T>(Stream stream, CancellationToken cancellationToken = default);

    #endregion
}
