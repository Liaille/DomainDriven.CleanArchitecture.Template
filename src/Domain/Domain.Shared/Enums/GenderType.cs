namespace Domain.Shared.Enums;

/// <summary>
/// 性别类型
/// <para>遵循 ISO 5218 标准</para>
/// </summary>
public enum GenderType
{
    /// <summary>
    /// 未知
    /// </summary>
    Unknown = 0,

    /// <summary>
    /// 男性
    /// </summary>
    Male = 1,

    /// <summary>
    /// 女性
    /// </summary>
    Female = 2,

    /// <summary>
    /// 不适用
    /// </summary>
    NotApplicable = 9
}
