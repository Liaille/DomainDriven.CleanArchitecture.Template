namespace BuildingBlocks.Core.Errors;

/// <summary>
/// 公共错误码常量
/// <remark>
/// <para>【规范约定】</para>
/// <list type="number">
/// <item>业务线01预留为公共业务线</item>
/// <item>各业务模块应在此基础上扩展自己的业务错误码</item>
/// </list>
/// </remark>
/// </summary>
public static class ErrorCodes
{
    #region 参数错误 (Px01001xxx)

    /// <summary>
    /// 参数不能为空
    /// </summary>
    public const string ParameterCannotBeEmpty = "P001001001";

    /// <summary>
    /// 参数格式不正确
    /// </summary>
    public const string ParameterFormatInvalid = "P001001002";

    /// <summary>
    /// 参数值超出范围
    /// </summary>
    public const string ParameterValueOutOfRange = "P001001003";

    /// <summary>
    /// 请求参数校验失败
    /// </summary>
    public const string ParameterValidationFailed = "P001001004";

    /// <summary>
    /// 参数类型不匹配
    /// </summary>
    public const string ParameterTypeMismatch = "P001001005";

    #endregion

    #region 安全错误 - 认证模块 (Ax01002xxx)

    /// <summary>
    /// 未认证
    /// </summary>
    public const string Unauthorized = "A001002001";

    /// <summary>
    /// Token格式/签名无效
    /// </summary>
    public const string TokenInvalid = "A101002002";

    /// <summary>
    /// Token过期
    /// </summary>
    public const string TokenExpired = "A101002003";

    /// <summary>
    /// Token已吊销
    /// </summary>
    public const string TokenRevoked = "A101002004";

    /// <summary>
    /// 身份验证失败
    /// </summary>
    public const string AuthenticationFailed = "A101002005";

    #endregion

    #region 安全错误- 授权模块 (Ax01003xxx)

    /// <summary>
    /// 权限不足
    /// </summary>
    public const string Forbidden = "A201003001";

    /// <summary>
    /// 越权访问资源
    /// </summary>
    public const string IllegalAccess = "A201003002";

    /// <summary>
    /// 角色无权限
    /// </summary>
    public const string RoleNotAllowed = "A201003003";

    /// <summary>
    /// 作用域权限不足
    /// </summary>
    public const string ScopeUnauthorized = "A201003004";

    /// <summary>
    /// 访问被拒绝
    /// </summary>
    public const string AccessDenied = "A201003005";

    #endregion

    #region 系统错误 - 资源模块 (Sx01004xxx)

    /// <summary>
    /// 资源不存在
    /// </summary>
    public const string ResourceNotFound = "S001004001";

    /// <summary>
    /// 资源已存在
    /// </summary>
    public const string ResourceAlreadyExists = "S001004002";

    /// <summary>
    /// 资源已被占用
    /// </summary>
    public const string ResourceOccupied = "S001004003";

    /// <summary>
    /// 资源已被删除
    /// </summary>
    public const string ResourceDeleted = "S001004004";

    #endregion

    #region 系统错误 - 限流模块 (Sx01005xxx)

    /// <summary>
    /// 请求被限流
    /// </summary>
    public const string RequestThrottled = "S101005001";

    /// <summary>
    /// 请求过于频繁
    /// </summary>
    public const string RequestTooFrequent = "S101005002";

    /// <summary>
    /// API速率限制已超出
    /// </summary>
    public const string ApiRateLimitExceeded = "S101005003";

    #endregion

    #region 系统错误 - 配置模块 (Sx01006xxx)

    /// <summary>
    /// 配置错误
    /// </summary>
    public const string ConfigurationError = "S201006001";

    /// <summary>
    /// 配置缺失
    /// </summary>
    public const string ConfigurationMissing = "S201006002";

    /// <summary>
    /// 配置无效
    /// </summary>
    public const string ConfigurationInvalid = "S201006003";

    #endregion

    #region 系统错误 - 通用模块 (Sx01999xxx)

    /// <summary>
    /// 服务器内部错误
    /// </summary>
    public const string InternalServerError = "S201999001";

    /// <summary>
    /// 服务不可用
    /// </summary>
    public const string ServiceUnavailable = "S301999002";

    /// <summary>
    /// 服务暂时不可用，请稍后重试
    /// </summary>
    public const string ServiceTemporarilyUnavailable = "S201999003";

    #endregion
}
