namespace Domain.Shared.Enums;

/// <summary>
/// 是/否枚举
/// 避免使用 bool 类型的歧义，明确表达业务含义
/// </summary>
public enum YesNo
{
    /// <summary>
    /// 否
    /// </summary>
    No = 0,

    /// <summary>
    /// 是
    /// </summary>
    Yes = 1
}
