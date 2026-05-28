using BuildingBlocks.Core.Common;
using BuildingBlocks.Core.Errors;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BuildingBlocks.Core.Exceptions;


public abstract class AppException : Exception
{
    /// <summary>
    /// 错误信息
    /// </summary>
    [JsonPropertyOrder(1)]
    public Error ErrorInfo { get; }

    /// <summary>
    /// 错误码
    /// </summary>
    [JsonIgnore]
    public string Code => ErrorInfo.Code;

    /// <summary>
    /// 错误类型
    /// </summary>
    [JsonIgnore]
    public ErrorType Type => ErrorInfo.Type;

    /// <summary>
    /// 严重级别
    /// </summary>
    [JsonIgnore]
    public SeverityLevel Severity => ErrorInfo.Severity;

    /// <summary>
    /// 子错误详情
    /// </summary>
    [JsonIgnore]
    public IReadOnlyList<ErrorDetail>? Details => ErrorInfo.Details;

    /// <summary>
    /// 分布式上下文
    /// 用于存放 TraceId、UserId、RequestId 等临时上下文，不对外序列化
    /// </summary>
    [JsonIgnore]
    public Dictionary<string, object> Context { get; }

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
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        // 支持派生类多态反序列化
        AllowTrailingCommas = true
    };

    /// <summary>
    /// 主构造函数
    /// </summary>
    /// <param name="error"></param>
    /// <param name="innerException"></param>
    protected AppException(Error error, Exception? innerException = null)
        : base(error.ToString(), innerException)
    {
        Guard.NotNull(error);
        ErrorInfo = error;
        Context = [];
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
        Guard.NotNullOrWhiteSpace(json);
        return JsonSerializer.Deserialize<TException>(json, SerializerOptions);
    }

    public void AddContext(string key, object value)
    {
        Guard.NotNullOrWhiteSpace(key);
        Guard.NotNull(value);
        Context[key] = value;
    }

    public object? GetContext(string key)
    {
        Context.TryGetValue(key, out var value);
        return value;
    }
}
