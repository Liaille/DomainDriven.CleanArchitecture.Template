using BuildingBlocks.Core.Common;
using BuildingBlocks.Core.Errors;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BuildingBlocks.Core.Exceptions;

/// <summary>
/// 应用程序异常基类
/// 所有自定义异常必须继承自此基类，是整个异常体系的根
/// </summary>
/// <remarks>
/// 【设计原则】
/// 1. 抽象类: 不能直接实例化，必须通过具体异常类使用
/// 2. 强类型关联: 与Error对象强绑定，所有异常必须包含完整的错误元数据
/// 3. 序列化支持: 实现标准ISerializable接口，支持跨服务传递
/// 4. 不可变设计: 所有属性均为只读，创建后无法修改
/// 
/// 【强制约束】
/// 1. 严禁直接抛出Exception或System.Exception，必须使用派生自此类的异常
/// 2. 所有异常必须包含有效的Error对象，严禁使用null
/// 3. 异常消息必须使用Error.UserMessage，严禁硬编码字符串
/// </remarks>
public abstract class AppException : Exception
{
    /// <summary>
    /// 强类型错误对象
    /// 包含错误处理所需的所有元数据
    /// </summary>
    [JsonPropertyOrder(1)]
    public Error ErrorInfo { get; }

    /// <summary>
    /// 10位固定长度错误码
    /// 快捷访问 Error.Code
    /// </summary>
    [JsonIgnore]
    public string Code => ErrorInfo.Code;

    /// <summary>
    /// 错误类型
    /// 快捷访问 Error.Type
    /// </summary>
    [JsonIgnore]
    public ErrorType Type => ErrorInfo.Type;

    /// <summary>
    /// 用户友好提示信息 (生产环境可见)
    /// 快捷访问 Error.UserMessage
    /// </summary>
    [JsonIgnore]
    public string UserMessage => ErrorInfo.UserMessage;

    /// <summary>
    /// 技术详情信息 (仅开发/测试环境可见)
    /// 快捷访问 Error.TechnicalDetails
    /// </summary>
    [JsonIgnore]
    public string? TechnicalDetails => ErrorInfo.TechnicalDetails;

    /// <summary>
    /// 日志级别
    /// 快捷访问 Error.LogLevel
    /// </summary>
    [JsonIgnore]
    public LogLevel LogLevel => ErrorInfo.LogLevel;

    /// <summary>
    /// 是否触发告警
    /// 快捷访问 Error.ShouldAlert
    /// </summary>
    [JsonIgnore]
    public bool ShouldAlert => ErrorInfo.ShouldAlert;

    /// <summary>
    /// 全局 JSON 序列化配置
    /// 确保所有异常和错误对象序列化行为一致
    /// </summary>
    public static readonly JsonSerializerOptions SerializerOptions = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Converters =
        {
            new JsonStringEnumConverter()
        },
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    /// <summary>
    /// 主构造函数
    /// </summary>
    /// <param name="error">强类型错误对象</param>
    /// <param name="innerException">内部异常 (可选)</param>
    protected AppException(Error error, Exception? innerException = null)
        : base(error.UserMessage, innerException)
    {
        Guard.NotNull(error);
        ErrorInfo = error;
    }

    /// <summary>
    /// 将异常序列化为 JSON 字符串
    /// 用于跨服务传递、日志记录和响应输出
    /// </summary>
    /// <returns>格式化的 JSON 字符串</returns>
    public virtual string ToJson()
    {
        return JsonSerializer.Serialize(this, SerializerOptions);
    }

    /// <summary>
    /// 从 JSON 字符串反序列化为异常对象
    /// </summary>
    /// <typeparam name="TException">具体异常类型</typeparam>
    /// <param name="json">JSON 字符串</param>
    /// <returns>反序列化后的异常对象</returns>
    public static TException? FromJson<TException>(string json)
        where TException : AppException
    {
        return JsonSerializer.Deserialize<TException>(json, SerializerOptions);
    }

    /// <summary>
    /// 重写 ToString 方法
    /// 输出包含完整错误信息的格式化字符串，便于日志记录
    /// </summary>
    /// <returns>格式化的错误信息字符串</returns>
    public override string ToString()
    {
        return $"{ErrorInfo}{Environment.NewLine}{base.ToString()}";
    }
}
