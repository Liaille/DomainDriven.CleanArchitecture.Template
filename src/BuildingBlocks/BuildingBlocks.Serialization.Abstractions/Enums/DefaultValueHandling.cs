namespace BuildingBlocks.Serialization.Abstractions.Enums;

/// <summary>
/// 默认值处理方式
/// </summary>
public enum DefaultValueHandling
{
    /// <summary>
    /// 包含所有值
    /// </summary>
    IncludeAll = 0,

    /// <summary>
    /// 忽略默认值
    /// </summary>
    IgnoreDefaults = 1,

    /// <summary>
    /// 忽略空值
    /// </summary>
    IgnoreNulls = 2
}
