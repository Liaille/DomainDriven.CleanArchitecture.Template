namespace Domain.Shared.Errors;

/// <summary>
/// 全局错误码
/// </summary>
public enum ErrorCode
{
    #region 系统级别 1000~1999
    /// <summary>
    /// 成功
    /// </summary>
    Success = 0,

    /// <summary>
    /// 未知错误
    /// </summary>
    UnknownError = 1000,

    /// <summary>
    /// 参数验证失败
    /// </summary>
    ValidationFailed = 1001,

    /// <summary>
    /// 未授权
    /// </summary>
    Unauthorized = 1002,

    /// <summary>
    /// 权限不足
    /// </summary>
    Forbidden = 1003,

    /// <summary>
    /// 资源不存在
    /// </summary>
    NotFound = 1004,

    /// <summary>
    /// 资源已存在
    /// </summary>
    AlreadyExists = 1005,

    /// <summary>
    /// 并发冲突
    /// </summary>
    ConcurrencyConflict = 1006,

    /// <summary>
    /// 无效操作
    /// </summary>
    InvalidOperation = 1007,
    #endregion

    #region 通用业务
    /// <summary>
    /// 值不能为空
    /// </summary>
    ValueIsRequired = 2000,

    /// <summary>
    /// 值格式无效
    /// </summary>
    InvalidFormat = 2001,

    /// <summary>
    /// 值超出范围
    /// </summary>
    OutOfRange = 2002,

    /// <summary>
    /// 字符串长度超出限制
    /// </summary>
    StringTooLong = 2003,
    #endregion

    
}
