using BuildingBlocks.Core.Common;
using System.Text.Json.Serialization;

namespace BuildingBlocks.Core.Errors;

/// <summary>
/// 子错误详情
/// </summary>
public sealed record ErrorDetail
{
    /// <summary>
    /// 错误码
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
    public SeverityLevel Severity { get; }

    /// <summary>
    /// 错误属性名/路径
    /// </summary>
    [JsonPropertyOrder(2)]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? PropertyName { get; init; }

    /// <summary>
    /// 用户尝试输入的错误值 (仅开发/测试环境可见)
    /// 禁止包含敏感信息
    /// </summary>
    [JsonPropertyOrder(3)]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object? AttemptedValue { get; init; }

    /// <summary>
    /// 私有构造函数
    /// 强制通过工厂方法创建子错误详情
    /// </summary>
    /// <param name="code">10位错误码</param>
    /// <param name="propertyName">错误属性名/路径</param>
    /// <param name="attemptedValue">用户尝试输入的错误值 (仅开发/测试环境可见)</param>
    /// <returns>子错误详情对象</returns>
    private ErrorDetail(string code, string? propertyName = null, object? attemptedValue = null)
    {
        Code = code;
        Type = ErrorCodeValidator.ParseErrorType(code);
        Severity = ErrorCodeValidator.ParseSeverityLevel(code);
        PropertyName = propertyName;
        AttemptedValue = attemptedValue;
    }

    /// <summary>
    /// 创建任意类型的子错误详情
    /// </summary>
    /// <param name="code">10位错误码</param>
    /// <param name="propertyName">错误属性名/路径</param>
    /// <param name="attemptedValue">用户尝试输入的错误值 (仅开发/测试环境可见)</param>
    /// <returns>子错误详情对象</returns>
    public static ErrorDetail Create(string code, string? propertyName = null, object? attemptedValue = null)
    {
        ErrorCodeValidator.ValidateCodeFormat(code);
        return new ErrorDetail(code, propertyName, attemptedValue);
    }

    /// <summary>
    /// 创建参数错误详情
    /// </summary>
    /// <param name="code">10位错误码 (P开头)</param>
    /// <param name="propertyName">错误属性名/路径</param>
    /// <param name="attemptedValue">用户尝试输入的错误值 (仅开发/测试环境可见)</param>
    /// <returns>子错误详情对象</returns>
    public static ErrorDetail Parameter(string code, string? propertyName = null, object? attemptedValue = null)
    {
        ErrorCodeValidator.Validate(code, ErrorType.Parameter);
        return new ErrorDetail(code, propertyName, attemptedValue);
    }

    /// <summary>
    /// 创建安全错误详情
    /// </summary>
    /// <param name="code">10位错误码 (A开头)</param>
    /// <param name="propertyName">错误属性名/路径</param>
    /// <param name="attemptedValue">用户尝试输入的错误值 (仅开发/测试环境可见)</param>
    /// <returns>子错误详情对象</returns>
    public static ErrorDetail Security(string code, string? propertyName = null, object? attemptedValue = null)
    {
        ErrorCodeValidator.Validate(code, ErrorType.Security);
        return new ErrorDetail(code, propertyName, attemptedValue);
    }

    /// <summary>
    /// 创建业务错误详情
    /// </summary>
    /// <param name="code">10位错误码 (B开头)</param>
    /// <param name="propertyName">错误属性名/路径</param>
    /// <param name="attemptedValue">用户尝试输入的错误值 (仅开发/测试环境可见)</param>
    /// <returns>子错误详情对象</returns>
    public static ErrorDetail Business(string code, string? propertyName = null, object? attemptedValue = null)
    {
        ErrorCodeValidator.Validate(code, ErrorType.Business);
        return new ErrorDetail(code, propertyName, attemptedValue);
    }

    /// <summary>
    /// 创建系统错误详情
    /// </summary>
    /// <param name="code">10位错误码 (S开头)</param>
    /// <param name="propertyName">错误属性名/路径</param>
    /// <param name="attemptedValue">用户尝试输入的错误值 (仅开发/测试环境可见)</param>
    /// <returns>子错误详情对象</returns>
    public static ErrorDetail System(string code, string? propertyName = null, object? attemptedValue = null)
    {
        ErrorCodeValidator.Validate(code, ErrorType.System);
        return new ErrorDetail(code, propertyName, attemptedValue);
    }

    /// <summary>
    /// 创建第三方错误详情
    /// </summary>
    /// <param name="code">10位错误码 (T开头)</param>
    /// <param name="propertyName">错误属性名/路径</param>
    /// <param name="attemptedValue">用户尝试输入的错误值 (仅开发/测试环境可见)</param>
    /// <returns>子错误详情对象</returns>
    public static ErrorDetail ThirdParty(string code, string? propertyName = null, object? attemptedValue = null)
    {
        ErrorCodeValidator.Validate(code, ErrorType.ThirdParty);
        return new ErrorDetail(code, propertyName, attemptedValue);
    }

    /// <summary>
    /// 创建致命错误详情
    /// </summary>
    /// <param name="code">10位错误码 (F开头)</param>
    /// <param name="propertyName">错误属性名/路径</param>
    /// <param name="attemptedValue">用户尝试输入的错误值 (仅开发/测试环境可见)</param>
    /// <returns>子错误详情对象</returns>
    public static ErrorDetail Fatal(string code, string? propertyName = null, object? attemptedValue = null)
    {
        ErrorCodeValidator.Validate(code, ErrorType.Fatal);
        return new ErrorDetail(code, propertyName, attemptedValue);
    }

    /// <summary>
    /// 设置错误属性名
    /// </summary>
    /// <param name="propertyName">错误属性名</param>
    /// <returns>新的错误详情对象</returns>
    public ErrorDetail WithPropertyName(string propertyName)
    {
        Guard.NotNullOrWhiteSpace(propertyName);
        return this with { PropertyName = propertyName };
    }

    /// <summary>
    /// 设置用户尝试输入的错误值
    /// </summary>
    /// <param name="attemptedValue">用户尝试输入的错误值</param>
    /// <returns>新的错误详情对象</returns>
    public ErrorDetail WithAttemptedValue(object attemptedValue)
    {
        Guard.NotNull(attemptedValue);
        return this with { AttemptedValue = attemptedValue };
    }

    /// <summary>
    /// 生产环境脱敏，移除敏感输入值
    /// </summary>
    /// <returns></returns>
    public ErrorDetail SanitizeForProduction()
    {
        return this with { AttemptedValue = null };
    }
}
