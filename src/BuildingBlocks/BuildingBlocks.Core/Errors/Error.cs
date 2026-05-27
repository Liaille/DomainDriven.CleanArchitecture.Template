using Microsoft.Extensions.Logging;
using System.Text.Json.Serialization;

namespace BuildingBlocks.Core.Errors;

public sealed record Error
{
    /// <summary>
    /// 10位固定长度错误码
    /// <para>格式: [错误类型码(1位)][业务线编码(3位)][模块编码(2位)][错误编码(4位)]</para>
    /// </summary>
    [JsonPropertyOrder(1)]
    public string Code { get; init; }

    /// <summary>
    /// 错误类型
    /// 从错误码第1位自动解析
    /// </summary>
    [JsonPropertyOrder(2)]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ErrorType Type { get; init; }

    /// <summary>
    /// 用户友好提示信息 (生产环境可见)
    /// </summary>
    [JsonPropertyOrder(3)]
    public string UserMessage { get; init; }

    /// <summary>
    /// 技术详情信息 (仅开发/测试环境可见)
    /// 包含详细的错误上下文、参数值、调试信息
    /// </summary>
    [JsonPropertyOrder(4)]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? TechnicalDetails { get; init; }

    /// <summary>
    /// 子错误详情列表
    /// </summary>
    [JsonPropertyOrder(5)]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IReadOnlyList<ErrorDetail>? Details { get; init; }

    /// <summary>
    /// 日志级别
    /// 决定日志记录的级别和是否触发告警
    /// </summary>
    [JsonPropertyOrder(6)]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public LogLevel LogLevel { get; init; }

    /// <summary>
    /// 是否触发告警
    /// 由错误类型和日志级别共同决定，可手动覆盖
    /// </summary>
    [JsonPropertyOrder(7)]
    public bool ShouldAlert { get; init; }

    /// <summary>
    /// 私有构造函数
    /// 强制通过工厂方法创建错误对象
    /// </summary>
    /// <param name="code">10位错误码</param>
    /// <param name="type">错误类型</param>
    /// <param name="userMessage">用户友好提示</param>
    /// <param name="technicalDetails">技术详情</param>
    /// <param name="logLevel">日志级别</param>
    /// <param name="shouldAlert">是否触发告警</param>
    private Error(
        string code,
        ErrorType type,
        string userMessage,
        string? technicalDetails = null,
        IReadOnlyList<ErrorDetail>? details = null,
        LogLevel logLevel = LogLevel.Error,
        bool shouldAlert = false)
    {
        Code = code;
        Type = type;
        UserMessage = userMessage;
        TechnicalDetails = technicalDetails;
        Details = details;
        LogLevel = logLevel;
        ShouldAlert = shouldAlert;
    }

    /// <summary>
    /// 创建参数错误
    /// </summary>
    /// <param name="code">10位错误码 (P开头)</param>
    /// <param name="userMessage">用户友好提示</param>
    /// <param name="technicalDetails">技术详情</param>
    /// <param name="logLevel">日志级别 (默认Warning)</param>
    /// <param name="shouldAlert">是否触发告警 (默认false)</param>
    /// <returns>参数错误对象</returns>
    public static Error Parameter(
        string code,
        string? userMessage = null,
        string? technicalDetails = null,
        IEnumerable<ErrorDetail>? details = null,
        LogLevel logLevel = LogLevel.Warning,
        bool shouldAlert = false)
    {
        ErrorCodeValidator.Validate(code, ErrorType.Parameter);
        return new Error(code, ErrorType.Parameter, userMessage ?? "Invalid request parameter", technicalDetails, details?.ToList().AsReadOnly(), logLevel, shouldAlert);
    }

    /// <summary>
    /// 创建业务错误
    /// </summary>
    /// <param name="code">10位错误码 (B开头)</param>
    /// <param name="userMessage">用户友好提示</param>
    /// <param name="technicalDetails">技术详情</param>
    /// <param name="logLevel">日志级别 (默认Information)</param>
    /// <param name="shouldAlert">是否触发告警 (默认false)</param>
    /// <returns>业务错误对象</returns>
    public static Error Business(
        string code,
        string? userMessage = null,
        string? technicalDetails = null,
        IEnumerable<ErrorDetail>? details = null,
        LogLevel logLevel = LogLevel.Information,
        bool shouldAlert = false)
    {
        ErrorCodeValidator.Validate(code, ErrorType.Business);
        return new Error(code, ErrorType.Business, userMessage ?? "Business processing failed", technicalDetails, details?.ToList().AsReadOnly(), logLevel, shouldAlert);
    }

    /// <summary>
    /// 创建系统错误
    /// </summary>
    /// <param name="code">10位错误码 (S开头)</param>
    /// <param name="userMessage">用户友好提示</param>
    /// <param name="technicalDetails">技术详情</param>
    /// <param name="logLevel">日志级别 (默认Error)</param>
    /// <param name="shouldAlert">是否触发告警 (默认true)</param>
    /// <returns>系统错误对象</returns>
    public static Error System(
        string code,
        string? userMessage = null,
        string? technicalDetails = null,
        IEnumerable<ErrorDetail>? details = null,
        LogLevel logLevel = LogLevel.Error,
        bool shouldAlert = true)
    {
        ErrorCodeValidator.Validate(code, ErrorType.System);
        return new Error(code, ErrorType.System, userMessage ?? "Internal system error", technicalDetails, details?.ToList().AsReadOnly(), logLevel, shouldAlert);
    }

    /// <summary>
    /// 创建第三方错误
    /// </summary>
    /// <param name="code">10位错误码 (T开头)</param>
    /// <param name="userMessage">用户友好提示</param>
    /// <param name="technicalDetails">技术详情</param>
    /// <param name="logLevel">日志级别 (默认Error)</param>
    /// <param name="shouldAlert">是否触发告警 (默认true)</param>
    /// <returns>第三方错误对象</returns>
    public static Error ThirdParty(
        string code,
        string? userMessage = null,
        string? technicalDetails = null,
        IEnumerable<ErrorDetail>? details = null,
        LogLevel logLevel = LogLevel.Error,
        bool shouldAlert = true)
    {
        ErrorCodeValidator.Validate(code, ErrorType.ThirdParty);
        return new Error(code, ErrorType.ThirdParty, userMessage ?? "Third-party service error", technicalDetails, details?.ToList().AsReadOnly(), logLevel, shouldAlert);
    }

    /// <summary>
    /// 创建致命错误
    /// </summary>
    /// <param name="code">10位错误码 (F开头)</param>
    /// <param name="userMessage">用户友好提示</param>
    /// <param name="technicalDetails">技术详情</param>
    /// <param name="logLevel">日志级别 (默认Critical)</param>
    /// <param name="shouldAlert">是否触发告警 (默认true)</param>
    /// <returns>致命错误对象</returns>
    public static Error Fatal(
        string code,
        string? userMessage = null,
        string? technicalDetails = null,
        IEnumerable<ErrorDetail>? details = null,
        LogLevel logLevel = LogLevel.Critical,
        bool shouldAlert = true)
    {
        ErrorCodeValidator.Validate(code, ErrorType.Fatal);
        return new Error(code, ErrorType.Fatal, userMessage ?? "Fatal system error", technicalDetails, details?.ToList().AsReadOnly(), logLevel, shouldAlert);
    }

    /// <summary>
    /// 创建一个新的Error对象，替换用户消息
    /// </summary>
    /// <param name="userMessage"></param>
    /// <returns></returns>
    public Error WithMessage(string userMessage)
    {
        return this with { UserMessage = userMessage };
    }

    /// <summary>
    /// 创建一个新的Error对象，替换技术详情
    /// </summary>
    /// <param name="technicalDetails"></param>
    /// <returns></returns>
    public Error WithTechnicalDetails(string technicalDetails)
    {
        return this with { TechnicalDetails = technicalDetails };
    }

    /// <summary>
    /// 创建一个新的Error对象，替换子错误详情
    /// </summary>
    /// <param name="details"></param>
    /// <returns></returns>
    public Error WithDetails(IEnumerable<ErrorDetail> details)
    {
        return this with { Details = details.ToList().AsReadOnly() };
    }

    /// <summary>
    /// 创建一个新的Error对象，替换日志级别
    /// </summary>
    /// <param name="logLevel"></param>
    /// <returns></returns>
    public Error WithLogLevel(LogLevel logLevel)
    {
        return this with { LogLevel = logLevel };
    }

    /// <summary>
    /// 创建一个新的Error对象，替换告警设置
    /// </summary>
    /// <param name="shouldAlert"></param>
    /// <returns></returns>
    public Error WithAlert(bool shouldAlert)
    {
        return this with { ShouldAlert = shouldAlert };
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
        return $"[{Code}] {UserMessage} (Type: {Type}, Level: {LogLevel}, Alert: {ShouldAlert})";
    }
}
