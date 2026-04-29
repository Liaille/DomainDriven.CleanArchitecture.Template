namespace BuildingBlocks.Core.Common;

/// <summary>
/// 全局通用常量定义
/// 集中管理系统中所有固定不变的通用配置、默认值、格式字符串等常量
/// 统一维护，避免硬编码，提升代码可维护性与一致性
/// </summary>
public static class CommonConstants
{
    #region 分页相关
    /// <summary>
    /// 分页查询默认每页记录数
    /// 未指定分页大小时默认返回10条数据
    /// </summary>
    public const int DefaultPageSize = 10;

    /// <summary>
    /// 分页查询允许的最大每页记录数
    /// 防止一次性查询过多数据导致性能问题，上限为1000条
    /// </summary>
    public const int MaxPageSize = 1000;
    #endregion

    #region 日期时间相关
    /// <summary>
    /// 默认日期格式化字符串
    /// 标准格式：年-月-日 (示例：2025-12-25)
    /// </summary>
    public const string DefaultDateFormat = "yyyy-MM-dd";

    /// <summary>
    /// 默认日期时间格式化字符串
    /// 标准格式：年-月-日 时:分:秒 (示例：2025-12-25 14:30:00)
    /// </summary>
    public const string DefaultDateTimeFormat = "yyyy-MM-dd HH:mm:ss";

    /// <summary>
    /// 默认使用的时区ID
    /// 系统统一使用UTC时区，保证多服务器/多地区时间一致性
    /// </summary>
    public const string DefaultTimeZoneId = "UTC";

    /// <summary>
    /// 默认系统语言/区域文化
    /// 系统默认语言为简体中文，影响日期、数字、提示信息等展示格式
    /// </summary>
    public const string DefaultLanguage = "zh-CN";
    #endregion

    #region 安全相关
    /// <summary>
    /// 用户密码默认最小长度
    /// 保证密码复杂度，最低要求8位字符
    /// </summary>
    public const int DefaultPasswordMinLength = 8;

    /// <summary>
    /// 用户密码默认最大长度
    /// 限制密码长度，最高支持32位字符
    /// </summary>
    public const int DefaultPasswordMaxLength = 32;
    #endregion

    #region 网络相关
    /// <summary>
    /// HTTP请求默认超时时间 (秒)
    /// 接口调用超时时间默认30秒，防止请求长时间阻塞
    /// </summary>
    public const int DefaultHttpTimeoutSeconds = 30;

    /// <summary>
    /// 服务健康检查默认执行间隔时间 (秒)
    /// 每隔10秒执行一次服务健康状态检测
    /// </summary>
    public const int DefaultHealthCheckIntervalSeconds = 10;
    #endregion
}