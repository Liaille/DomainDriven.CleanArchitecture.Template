namespace BuildingBlocks.Core.Errors;

/// <summary>
/// 错误类型枚举
/// </summary>
public enum ErrorType
{
    /// <summary>
    /// 无错误
    /// </summary>
    None = 0,

    /// <summary>
    /// 通用业务失败
    /// </summary>
    Failure = 1,

    /// <summary>
    /// 参数验证失败
    /// </summary>
    Validation = 2,

    /// <summary>
    /// 未授权
    /// </summary>
    Unauthorized = 3,

    /// <summary>
    /// 权限不足
    /// </summary>
    Forbidden = 4,

    /// <summary>
    /// 资源不存在
    /// </summary>
    NotFound = 5,

    /// <summary>
    /// 并发冲突
    /// </summary>
    Conflict = 6,

    /// <summary>
    /// 第三方服务错误
    /// </summary>
    ThirdParty = 7
}
