namespace Domain.Shared.Errors;

/// <summary>
/// 全局错误码枚举
/// <list type="bullet">
/// <item>0 = 成功</item>
/// <item>10000~19999 = 系统错误码</item>
/// <item>20000~29999 = 业务错误码</item>
/// <item>30000~39999 = 第三方错误码</item>
/// </list>
/// </summary>
public enum ErrorCode
{
    /// <summary>
    /// 操作成功
    /// </summary>
    Success = 0,

    #region 系统级别 10000~19999
    /// <summary>
    /// 未知系统错误
    /// </summary>
    UnknownError = 10000,

    /// <summary>
    /// 参数验证失败
    /// </summary>
    ValidationFailed = 10001,

    /// <summary>
    /// 未授权访问 (未登录或登录已过期)
    /// </summary>
    Unauthorized = 10002,

    /// <summary>
    /// 权限不足 (已登录但无权限执行操作)
    /// </summary>
    Forbidden = 10003,

    /// <summary>
    /// 请求的资源不存在
    /// </summary>
    NotFound = 10004,

    /// <summary>
    /// 资源已存在，不可重复创建
    /// </summary>
    AlreadyExists = 10005,

    /// <summary>
    /// 数据并发冲突
    /// </summary>
    ConcurrencyConflict = 10006,

    /// <summary>
    /// 当前状态不允许执行此操作
    /// </summary>
    InvalidOperation = 10007,
    #endregion

    #region 通用业务 20000~20999
    /// <summary>
    /// 必填项不能为空
    /// </summary>
    ValueIsRequired = 20000,

    /// <summary>
    /// 值格式无效
    /// </summary>
    InvalidFormat = 20001,

    /// <summary>
    /// 数值超出允许范围
    /// </summary>
    OutOfRange = 20002,

    /// <summary>
    /// 字符串长度超出限制
    /// </summary>
    StringTooLong = 20003,
    #endregion

    #region 第三方错误 30000~39999
    /// <summary>
    /// 第三方服务调用失败
    /// </summary>
    ThirdPartyServiceFailed = 30000,

    /// <summary>
    /// 第三方服务调用超时
    /// </summary>
    ThirdPartyTimeout = 30001,
    #endregion
}