namespace Domain.Shared.Constants;

/// <summary>
/// 全局日期时间格式常量
/// 集中管理所有业务用的日期时间格式，保证格式一致性
/// 遵循 ISO 8601 标准
/// </summary>
public static class DateTimeFormats
{
    /// <summary>
    /// 日期格式 (年-月-日)
    /// ISO 8601 标准
    /// </summary>
    public const string Date = "yyyy-MM-dd";

    /// <summary>
    /// 日期时间格式 (年-月-日 时:分:秒)
    /// ISO 8601 标准
    /// </summary>
    public const string DateTime = "yyyy-MM-dd HH:mm:ss";

    /// <summary>
    /// 日期时间格式 (年-月-日 时:分:秒.毫秒)
    /// ISO 8601 标准
    /// </summary>
    public const string DateTimeWithMilliseconds = "yyyy-MM-dd HH:mm:ss.fff";

    /// <summary>
    /// UTC 日期时间格式 (带时区标识)
    /// ISO 8601 标准
    /// </summary>
    public const string DateTimeUtc = "yyyy-MM-ddTHH:mm:ss.fffZ";

    /// <summary>
    /// 时间格式 (时:分:秒)
    /// </summary>
    public const string Time = "HH:mm:ss";

    /// <summary>
    /// 年月格式 (年-月)
    /// </summary>
    public const string YearMonth = "yyyy-MM";
}
