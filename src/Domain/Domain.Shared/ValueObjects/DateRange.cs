using Domain.Shared.ErrorCodes;
using Domain.Shared.Exceptions;

namespace Domain.Shared.ValueObjects;

/// <summary>
/// 日期范围值对象
/// 核心职责: 封装开始与结束日期，保证开始日期 <= 结束日期
/// 设计说明: 
/// - 使用 DateTime 类型，统一使用 UTC 时间
/// - 支持包含时间的日期范围
/// - 提供常用的日期范围判断方法
/// </summary>
public record DateRange
{
    /// <summary>
    /// 开始日期时间 (UTC)
    /// </summary>
    public DateTime StartDate { get; }

    /// <summary>
    /// 结束日期时间 (UTC)
    /// </summary>
    public DateTime EndDate { get; }

    /// <summary>
    /// 私有构造函数
    /// </summary>
    private DateRange(DateTime startDate, DateTime endDate)
    {
        StartDate = startDate;
        EndDate = endDate;
    }

    /// <summary>
    /// 创建日期范围值对象的工厂方法
    /// </summary>
    /// <param name="startDate">开始日期时间</param>
    /// <param name="endDate">结束日期时间</param>
    /// <param name="enforceUtc">是否强制使用 UTC 时间 (默认 true)</param>
    /// <returns>验证通过的DateRange值对象</returns>
    /// <exception cref="DomainBusinessException">验证失败时抛出业务异常</exception>
    public static DateRange Create(DateTime startDate, DateTime endDate, bool enforceUtc = true)
    {
        if (enforceUtc)
        {
            if (startDate.Kind != DateTimeKind.Utc)
                throw new DomainBusinessException(BusinessErrorCodes.InvalidDataFormat, "开始日期必须使用 UTC 时间");

            if (endDate.Kind != DateTimeKind.Utc)
                throw new DomainBusinessException(BusinessErrorCodes.InvalidDataFormat, "结束日期必须使用 UTC 时间");
        }

        if (startDate > endDate)
            throw new DomainBusinessException(BusinessErrorCodes.InvalidDateRange, "开始日期不能晚于结束日期");

        return new DateRange(startDate, endDate);
    }

    /// <summary>
    /// 创建仅包含日期的范围 (时间部分为 00:00:00)
    /// </summary>
    /// <param name="startDate">开始日期</param>
    /// <param name="endDate">结束日期</param>
    /// <param name="enforceUtc">是否强制使用 UTC 时间 (默认 true)</param>
    /// <returns>验证通过的DateRange值对象</returns>
    public static DateRange CreateDateOnly(DateTime startDate, DateTime endDate, bool enforceUtc = true)
    {
        return Create(
            startDate.Date,
            endDate.Date.AddDays(1).AddTicks(-1), // 结束日期的最后一刻
            enforceUtc);
    }

    /// <summary>
    /// 判断指定日期时间是否在范围内
    /// </summary>
    /// <param name="date">指定日期时间</param>
    /// <returns>是否在范围内</returns>
    public bool Contains(DateTime date)
    {
        return date >= StartDate && date <= EndDate;
    }

    /// <summary>
    /// 判断另一个日期范围是否完全包含在当前范围内
    /// </summary>
    /// <param name="other">另一个日期范围</param>
    /// <returns>是否完全包含</returns>
    public bool Contains(DateRange other)
    {
        return other.StartDate >= StartDate && other.EndDate <= EndDate;
    }

    /// <summary>
    /// 判断是否与另一个日期范围重叠
    /// </summary>
    /// <param name="other">另一个日期范围</param>
    /// <returns>是否重叠</returns>
    public bool OverlapsWith(DateRange other)
    {
        return StartDate <= other.EndDate && other.StartDate <= EndDate;
    }

    /// <summary>
    /// 获取日期范围的总天数
    /// </summary>
    /// <returns>天数</returns>
    public int GetTotalDays()
    {
        return (EndDate - StartDate).Days;
    }

    /// <summary>
    /// 获取日期范围的总小时数
    /// </summary>
    /// <returns>小时数</returns>
    public double GetTotalHours()
    {
        return (EndDate - StartDate).TotalHours;
    }

    /// <summary>
    /// 获取日期范围的总分钟数
    /// </summary>
    /// <returns>分钟数</returns>
    public double GetTotalMinutes()
    {
        return (EndDate - StartDate).TotalMinutes;
    }

    /// <summary>
    /// 获取两个日期范围的交集
    /// </summary>
    /// <param name="other">另一个日期范围</param>
    /// <returns>交集范围，如果没有交集则返回 null</returns>
    public DateRange? Intersect(DateRange other)
    {
        if (!OverlapsWith(other))
            return null;

        var intersectStart = StartDate > other.StartDate ? StartDate : other.StartDate;
        var intersectEnd = EndDate < other.EndDate ? EndDate : other.EndDate;

        return new DateRange(intersectStart, intersectEnd);
    }

    /// <summary>
    /// 获取两个日期范围的并集
    /// </summary>
    /// <param name="other">另一个日期范围</param>
    /// <returns>并集范围</returns>
    public DateRange Union(DateRange other)
    {
        var unionStart = StartDate < other.StartDate ? StartDate : other.StartDate;
        var unionEnd = EndDate > other.EndDate ? EndDate : other.EndDate;

        return new DateRange(unionStart, unionEnd);
    }

    /// <summary>
    /// 重写ToString方法，返回格式化的日期范围字符串
    /// </summary>
    /// <returns>日期范围字符串</returns>
    public override string ToString()
    {
        return $"{StartDate:yyyy-MM-dd HH:mm:ss} ~ {EndDate:yyyy-MM-dd HH:mm:ss}";
    }
}
