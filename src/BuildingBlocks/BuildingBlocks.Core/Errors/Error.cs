using System.Text.Json.Serialization;

namespace BuildingBlocks.Core.Errors;

public sealed record Error
{
    /// <summary>
    /// 固定长度格式错误码
    /// <para>格式: [错误类型码(1位)][业务线编码(3位)][模块编码(2位)][错误编码(4位)]</para>
    /// </summary>
    [JsonPropertyOrder(1)]
    public string Code { get; init; }

    /// <summary>
    /// 错误类型
    /// 从错误码第1位自动解析
    /// </summary>
    [JsonIgnore]
    public ErrorType Type { get; init; }

    /// <summary>
    /// 严重级别
    /// 从错误码第2位自动解析
    /// </summary>
    [JsonIgnore]
    public SeverityLevel Severity { get; init; }

    /// <summary>
    /// 子错误详情列表
    /// </summary>
    [JsonPropertyOrder(2)]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IReadOnlyList<ErrorDetail> Details { get; init; }

    /// <summary>
    /// 错误发生时间
    /// </summary>
    [JsonIgnore]
    public DateTimeOffset Timestamp { get; init; }

    /// <summary>
    /// 私有构造函数
    /// 强制通过工厂方法创建错误
    /// </summary>
    /// <param name="code">10位错误码</param>
    /// <param name="details">子错误详情列表 (可选)</param>
    /// <returns>错误对象</returns>
    private Error(string code, IReadOnlyList<ErrorDetail> details)
    {
        Code = code;
        Type = ErrorCodeValidator.ParseErrorType(code);
        Severity = ErrorCodeValidator.ParseSeverityLevel(code);
        Details = details;
        Timestamp = DateTimeOffset.UtcNow;
    }

    /// <summary>
    /// 创建任意类型的错误
    /// </summary>
    /// <param name="code">10位错误码</param>
    /// <param name="details">子错误详情列表 (可选)</param>
    /// <returns>错误对象</returns>
    public static Error Create(string code, IEnumerable<ErrorDetail>? details = null)
    {
        ErrorCodeValidator.ValidateCodeFormat(code);
        return new Error(code, details?.ToList().AsReadOnly() ?? []);
    }

    /// <summary>
    /// 创建参数错误
    /// </summary>
    /// <param name="code">10位错误码 (P开头)</param>
    /// <param name="details">子错误详情列表 (可选)</param>
    /// <returns>参数错误对象</returns>
    public static Error Parameter(string code, IEnumerable<ErrorDetail>? details = null)
    {
        ErrorCodeValidator.Validate(code, ErrorType.Parameter);
        return new Error(code, details?.ToList().AsReadOnly() ?? []);
    }

    /// <summary>
    /// 创建安全错误
    /// </summary>
    /// <param name="code">10位错误码 (A开头)</param>
    /// <param name="details">子错误详情列表 (可选)</param>
    /// <returns>参数错误对象</returns>
    public static Error Security(string code, IEnumerable<ErrorDetail>? details = null)
    {
        ErrorCodeValidator.Validate(code, ErrorType.Security);
        return new Error(code, details?.ToList().AsReadOnly() ?? []);
    }

    /// <summary>
    /// 创建业务错误
    /// </summary>
    /// <param name="code">10位错误码 (B开头)</param>
    /// <param name="details">子错误详情列表 (可选)</param>
    /// <returns>业务错误对象</returns>
    public static Error Business(string code, IEnumerable<ErrorDetail>? details = null)
    {
        ErrorCodeValidator.Validate(code, ErrorType.Business);
        return new Error(code, details?.ToList().AsReadOnly() ?? []);
    }

    /// <summary>
    /// 创建系统错误
    /// </summary>
    /// <param name="code">10位错误码 (S开头)</param>
    /// <param name="details">子错误详情列表 (可选)</param>
    /// <returns>系统错误对象</returns>
    public static Error System(string code, IEnumerable<ErrorDetail>? details = null)
    {
        ErrorCodeValidator.Validate(code, ErrorType.System);
        return new Error(code, details?.ToList().AsReadOnly() ?? []);
    }

    /// <summary>
    /// 创建第三方错误
    /// </summary>
    /// <param name="code">10位错误码 (T开头)</param>
    /// <param name="details">子错误详情列表 (可选)</param>
    /// <returns>第三方错误对象</returns>
    public static Error ThirdParty(string code, IEnumerable<ErrorDetail>? details = null)
    {
        ErrorCodeValidator.Validate(code, ErrorType.ThirdParty);
        return new Error(code, details?.ToList().AsReadOnly() ?? []);
    }

    /// <summary>
    /// 创建致命错误
    /// </summary>
    /// <param name="code">10位错误码 (F开头)</param>
    /// <param name="details">子错误详情列表 (可选)</param>
    /// <returns>致命错误对象</returns>
    public static Error Fatal(string code, IEnumerable<ErrorDetail>? details = null)
    {
        ErrorCodeValidator.Validate(code, ErrorType.Fatal);
        return new Error(code, details?.ToList().AsReadOnly() ?? []);
    }

    /// <summary>
    /// 设置子错误详情
    /// </summary>
    /// <param name="details"></param>
    /// <returns></returns>
    public Error WithDetails(IEnumerable<ErrorDetail> details)
    {
        return this with { Details = details.ToList().AsReadOnly() };
    }

    /// <summary>
    /// 隐式转换为字符串 (返回错误码)
    /// 方便在日志、响应等场景直接使用
    /// </summary>
    /// <param name="error">错误对象</param>
    public static implicit operator string(Error error) => error.Code;

    /// <summary>
    /// 隐式转换为布尔值
    /// 规则: 任何非空错误对象都表示有错误，返回true
    /// 用于简化条件判断: if (error) { ... }
    /// </summary>
    /// <param name="error">错误对象</param>
    public static implicit operator bool(Error? error) => error != null;

    /// <summary>
    /// 重写ToString方法
    /// 输出格式化的错误信息，方便日志打印和调试
    /// </summary>
    /// <returns>格式化错误字符串</returns>
    public override string ToString()
    {
        var detailCount = Details?.Count ?? 0;
        return detailCount > 0
            ? $"[{Code}] {Type} (Severity: {Severity}, {detailCount} error detail)"
            : $"[{Code}] {Type} (Severity: {Severity})";
    }
}
