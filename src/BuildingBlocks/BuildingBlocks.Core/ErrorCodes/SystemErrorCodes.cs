namespace BuildingBlocks.Core.ErrorCodes;

/// <summary>
/// 全局系统错误码定义 (10000+ 号段)
/// <para>核心职责: 统一管理所有系统级、框架级、基础设施级错误码</para>
/// <para>强制约束: 禁止与业务/第三方错误码号段冲突，每个错误码必须带中文说明</para>
/// </summary>
public static class SystemErrorCodes
{
    #region 通用参数错误 (10000-10099)
    /// <summary>
    /// 请求参数无效
    /// </summary>
    public const string InvalidParameter = "10000";

    /// <summary>
    /// 必填参数缺失
    /// </summary>
    public const string RequiredParameterMissing = "10001";

    /// <summary>
    /// 参数格式错误
    /// </summary>
    public const string ParameterFormatError = "10002";

    /// <summary>
    /// 参数范围超出限制
    /// </summary>
    public const string ParameterOutOfRange = "10003";
    #endregion

    #region 认证授权错误 (10100-10199)
    /// <summary>
    /// 未提供身份认证信息
    /// </summary>
    public const string AuthenticationMissing = "10100";

    /// <summary>
    /// 身份认证失败 (令牌无效、过期、签名错误)
    /// </summary>
    public const string AuthenticationFailed = "10101";

    /// <summary>
    /// 权限不足，无法执行操作
    /// </summary>
    public const string AuthorizationFailed = "10102";

    /// <summary>
    /// 访问令牌已过期
    /// </summary>
    public const string TokenExpired = "10103";

    /// <summary>
    /// 访问令牌被吊销
    /// </summary>
    public const string TokenRevoked = "10104";
    #endregion

    #region 资源错误 (10200-10299)
    /// <summary>
    /// 请求的资源不存在
    /// </summary>
    public const string ResourceNotFound = "10200";

    /// <summary>
    /// 资源已存在
    /// </summary>
    public const string ResourceAlreadyExists = "10201";

    /// <summary>
    /// 资源被锁定，无法操作
    /// </summary>
    public const string ResourceLocked = "10202";
    #endregion

    #region 服务器内部错误 (10300-10399)
    /// <summary>
    /// 服务器内部未知错误
    /// </summary>
    public const string InternalServerError = "10300";

    /// <summary>
    /// 配置错误
    /// </summary>
    public const string ConfigurationError = "10301";

    /// <summary>
    /// 依赖服务不可用
    /// </summary>
    public const string DependencyUnavailable = "10302";
    #endregion

    #region 请求限流错误 (10400-10499)
    /// <summary>
    /// 请求频率超出限制
    /// </summary>
    public const string RequestRateLimitExceeded = "10400";

    /// <summary>
    /// 并发请求数超出限制
    /// </summary>
    public const string ConcurrencyLimitExceeded = "10401";
    #endregion

    #region 服务不可用错误 (10500-10599)
    /// <summary>
    /// 服务正在启动中
    /// </summary>
    public const string ServiceStarting = "10500";

    /// <summary>
    /// 服务已停止
    /// </summary>
    public const string ServiceStopped = "10501";

    /// <summary>
    /// 服务维护中
    /// </summary>
    public const string ServiceUnderMaintenance = "10502";
    #endregion
}
