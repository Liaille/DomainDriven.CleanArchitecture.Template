using BuildingBlocks.Core.Common;
using BuildingBlocks.Core.Errors;
using BuildingBlocks.Core.Exceptions;

namespace BuildingBlocks.ExecutionContext.Abstractions.Exceptions;

/// <summary>
/// 上下文解析失败异常 (继承自Core的BusinessException，符合统一错误码规范)
/// </summary>
/// <remarks>
/// 【默认错误码】: B001010001 (业务级-公共业务线-上下文模块-默认解析失败)
/// 【使用场景】: 租户解析失败、用户信息转换失败、请求上下文获取失败等
/// </remarks>
public sealed class ContextResolutionException : BusinessException
{
    /// <summary>
    /// 默认错误码
    /// </summary>
    public const string DefaultErrorCode = "B001010001";

    /// <summary>
    /// 使用预构建的Error对象创建异常
    /// </summary>
    /// <param name="error">预构建的Error对象 (包含错误码、子错误详情等)</param>
    /// <param name="innerException">引发当前异常的内部异常 (可选，用于异常链路追踪)</param>
    public ContextResolutionException(Error error, Exception? innerException = null)
        : base(error, innerException)
    {
    }

    /// <summary>
    /// 使用默认错误码创建异常，并添加上下文信息
    /// </summary>
    /// <param name="contextType">解析失败的上下文类型 (如"Tenant"、"User"、"Request"，不可为null/空/空白)</param>
    /// <param name="message">详细错误信息 (仅用于开发诊断，可选)</param>
    /// <param name="details">子错误详情列表 (可选)</param>
    /// <param name="innerException">引发当前异常的内部异常 (可选，用于异常链路追踪)</param>
    /// <exception cref="ArgumentNullException">当contextType为null/空/空白时抛出</exception>
    public ContextResolutionException(
        string contextType,
        string? message = null,
        IReadOnlyList<ErrorDetail>? details = null,
        Exception? innerException = null)
        : base(Error.Business(DefaultErrorCode, details), innerException)
    {
        Guard.NotNullOrWhiteSpace(contextType);
        AddContext("ContextType", contextType);
        if (!string.IsNullOrWhiteSpace(message))
        {
            AddContext("ResolutionMessage", message);
        }
    }

    /// <summary>
    /// 使用自定义错误码创建异常，并添加上下文信息
    /// </summary>
    /// <param name="code">自定义的10位统一错误码 (必须为Business类型，B开头)</param>
    /// <param name="contextType">解析失败的上下文类型 (如"Tenant"、"User"、"Request"，不可为null/空/空白)</param>
    /// <param name="message">详细错误信息 (仅用于开发诊断，可选)</param>
    /// <param name="details">子错误详情列表 (可选)</param>
    /// <param name="innerException">引发当前异常的内部异常 (可选，用于异常链路追踪)</param>
    /// <exception cref="ArgumentNullException">当contextType为null/空/空白时抛出</exception>
    public ContextResolutionException(
        string code,
        string contextType,
        string? message = null,
        IReadOnlyList<ErrorDetail>? details = null,
        Exception? innerException = null)
        : base(Error.Business(code, details), innerException)
    {
        Guard.NotNullOrWhiteSpace(contextType);
        AddContext("ContextType", contextType);
        if (!string.IsNullOrWhiteSpace(message))
        {
            AddContext("ResolutionMessage", message);
        }
    }
}
