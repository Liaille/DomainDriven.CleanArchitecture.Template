namespace BuildingBlocks.Core.Common;

/// <summary>
/// 全球化与区域文化相关常量
/// 统一管理系统默认语言、时区、区域格式、文化编码等全局配置
/// 确保系统在多地区、多语言环境下展示格式保持一致
/// </summary>
public static class GlobalizationConstants
{
    /// <summary>
    /// 系统默认语言/区域文化编码
    /// 系统全局默认使用简体中文 (zh-CN)
    /// 影响日期格式、数字格式、货币格式、提示信息语言等展示效果
    /// </summary>
    public const string DefaultLanguage = "zh-CN";

    /// <summary>
    /// 系统默认时区标识
    /// 系统全局统一使用UTC时区
    /// 保证多服务器、多地区部署时时间处理的一致性
    /// </summary>
    public const string DefaultTimeZoneId = "UTC";

    /// <summary>
    /// 默认日期格式化字符串
    /// 标准格式：年-月-日 (示例：2026-05-03)
    /// 用于技术层面未指定格式时的默认日期展示
    /// </summary>
    public const string DefaultDateFormat = "yyyy-MM-dd";

    /// <summary>
    /// 默认日期时间格式化字符串
    /// 标准格式：年-月-日 时:分:秒 (示例：2026-05-03 14:30:00)
    /// 用于技术层面未指定格式时的默认日期时间展示
    /// </summary>
    public const string DefaultDateTimeFormat = "yyyy-MM-dd HH:mm:ss";
}
