using BuildingBlocks.Core.Common;
using System.Text.Json.Serialization;

namespace BuildingBlocks.Core.Errors;

/// <summary>
/// 子错误详情
/// </summary>
public sealed record ErrorDetail
{
    /// <summary>
    /// 10位固定长度错误码
    /// </summary>
    [JsonPropertyOrder(1)]
    public string Code { get; init; }

    /// <summary>
    /// 错误类型
    /// </summary>
    [JsonPropertyOrder(2)]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ErrorType Type { get; init; }

    /// <summary>
    /// 用户友好提示信息
    /// </summary>
    [JsonPropertyOrder(3)]
    public string UserMessage { get; init; }

    /// <summary>
    /// 技术详情信息 (仅开发/测试环境可见)
    /// </summary>
    [JsonPropertyOrder(4)]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? TechnicalDetails { get; init; }

    /// <summary>
    /// 错误属性名/路径
    /// </summary>
    [JsonPropertyOrder(5)]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? PropertyName { get; init; }

    /// <summary>
    /// 用户尝试输入的错误值 (仅开发/测试环境可见)
    /// 禁止包含敏感信息
    /// </summary>
    [JsonPropertyOrder(6)]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object? AttemptedValue { get; init; }

    private ErrorDetail(
        string code,
        ErrorType type,
        string userMessage,
        string? technicalDetails = null,
        string? propertyName = null,
        object? attemptedValue = null)
    {
        Code = code;
        Type = type;
        UserMessage = userMessage;
        TechnicalDetails = technicalDetails;
        PropertyName = propertyName;
        AttemptedValue = attemptedValue;
    }

    /// <summary>
    /// 创建参数错误详情
    /// </summary>
    /// <param name="code"></param>
    /// <param name="userMessage"></param>
    /// <param name="technicalDetails"></param>
    /// <param name="propertyName"></param>
    /// <param name="attemptedValue"></param>
    /// <returns></returns>
    public static ErrorDetail Parameter(
        string code,
        string userMessage,
        string? technicalDetails = null,
        string? propertyName = null,
        object? attemptedValue = null)
    {
        ErrorCodeValidator.Validate(code, ErrorType.Parameter);
        return new ErrorDetail(code, ErrorType.Parameter, userMessage, technicalDetails, propertyName, attemptedValue);
    }

    /// <summary>
    /// 创建业务错误详情
    /// </summary>
    /// <param name="code"></param>
    /// <param name="userMessage"></param>
    /// <param name="technicalDetails"></param>
    /// <param name="propertyName"></param>
    /// <param name="attemptedValue"></param>
    /// <returns></returns>
    public static ErrorDetail Business(
        string code,
        string userMessage,
        string? technicalDetails = null,
        string? propertyName = null,
        object? attemptedValue = null)
    {
        ErrorCodeValidator.Validate(code, ErrorType.Business);
        return new ErrorDetail(code, ErrorType.Business, userMessage, technicalDetails, propertyName, attemptedValue);
    }

    /// <summary>
    /// 创建系统错误详情
    /// </summary>
    /// <param name="code"></param>
    /// <param name="userMessage"></param>
    /// <param name="technicalDetails"></param>
    /// <param name="propertyName"></param>
    /// <param name="attemptedValue"></param>
    /// <returns></returns>
    public static ErrorDetail System(
        string code,
        string userMessage,
        string? technicalDetails = null,
        string? propertyName = null,
        object? attemptedValue = null)
    {
        ErrorCodeValidator.Validate(code, ErrorType.System);
        return new ErrorDetail(code, ErrorType.System, userMessage, technicalDetails, propertyName, attemptedValue);
    }

    /// <summary>
    /// 创建第三方错误详情
    /// </summary>
    /// <param name="code"></param>
    /// <param name="userMessage"></param>
    /// <param name="technicalDetails"></param>
    /// <param name="propertyName"></param>
    /// <param name="attemptedValue"></param>
    /// <returns></returns>
    public static ErrorDetail ThirdParty(
        string code,
        string userMessage,
        string? technicalDetails = null,
        string? propertyName = null,
        object? attemptedValue = null)
    {
        ErrorCodeValidator.Validate(code, ErrorType.ThirdParty);
        return new ErrorDetail(code, ErrorType.ThirdParty, userMessage, technicalDetails, propertyName, attemptedValue);
    }

    /// <summary>
    /// 创建致命错误详情
    /// </summary>
    /// <param name="code"></param>
    /// <param name="userMessage"></param>
    /// <param name="technicalDetails"></param>
    /// <param name="propertyName"></param>
    /// <param name="attemptedValue"></param>
    /// <returns></returns>
    public static ErrorDetail Fatal(
        string code,
        string userMessage,
        string? technicalDetails = null,
        string? propertyName = null,
        object? attemptedValue = null)
    {
        ErrorCodeValidator.Validate(code, ErrorType.Fatal);
        return new ErrorDetail(code, ErrorType.Fatal, userMessage, technicalDetails, propertyName, attemptedValue);
    }

    public ErrorDetail WithMessage(string userMessage)
    {
        Guard.NotNullOrWhiteSpace(userMessage);
        return this with { UserMessage = userMessage };
    }
}
