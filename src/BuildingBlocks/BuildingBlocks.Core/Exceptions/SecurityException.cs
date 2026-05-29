using BuildingBlocks.Core.Errors;

namespace BuildingBlocks.Core.Exceptions;

/// <summary>
/// 安全异常
/// </summary>
/// <remarks>
/// <para>适用于身份认证、权限校验相关的错误场景，遵循统一错误码规范 (前缀：A)</para>
/// <para>适用场景: 未登录、Token无效、权限不足、越权访问、恶意请求等</para>
/// </remarks>
public class SecurityException : AppException
{
    /// <summary>
    /// 使用预构建的错误信息创建安全异常
    /// </summary>
    /// <param name="error">错误核心对象 (包含错误码、类型、详情等信息)</param>
    /// <param name="innerException">引发当前异常的内部异常 (可选，用于异常链路追踪)</param>
    public SecurityException(Error error, Exception? innerException = null) 
        : base(error, innerException)
    {
    }

    /// <summary>
    /// 通过错误码直接创建安全异常
    /// </summary>
    /// <param name="code">10位统一错误码 (必须以A开头)</param>
    /// <param name="details">错误详情列表 (可选，用于多字段校验、全量错误返回)</param>
    /// <param name="innerException">引发当前异常的内部异常 (可选)</param>
    public SecurityException(string code, IReadOnlyList<ErrorDetail>? details = null, Exception? innerException = null) 
        : base(Error.Security(code, details), innerException)
    {
    }
}
