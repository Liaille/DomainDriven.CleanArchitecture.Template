namespace BuildingBlocks.Serialization.Abstractions.Enums;

/// <summary>
/// 命名策略枚举
/// </summary>
public enum NamingPolicy
{
    /// <summary>
    /// 驼峰命名法 (camelCase)
    /// </summary>
    CamelCase = 0,

    /// <summary>
    /// 帕斯卡命名法 (PascalCase)
    /// </summary>
    PascalCase = 1,

    /// <summary>
    /// 蛇形命名法 (snake_case)
    /// </summary>
    SnakeCase = 2,

    /// <summary>
    /// 短横线命名法 (kebab-case)
    /// </summary>
    KebabCase = 3
}
