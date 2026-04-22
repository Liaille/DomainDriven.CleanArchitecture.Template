namespace BuildingBlocks.Core.Common;

/// <summary>
/// 参数校验守卫类 (用于前置条件校验)
/// </summary>
public static class Guard
{
    /// <summary>
    /// 确保对象不为 null (为 null 时抛出 ArgumentNullException)
    /// </summary>
    /// <param name="value">要校验的值</param>
    /// <param name="paramName">参数名称</param>
    public static void NotNull<T>(T value, string paramName)
    {
        ArgumentNullException.ThrowIfNull(value, paramName);
    }

    /// <summary>
    /// 确保字符串不为 null 或空字符串
    /// </summary>
    public static void NotNullOrEmpty(string value, string paramName)
    {
        ArgumentException.ThrowIfNullOrEmpty(value, paramName);
    }

    /// <summary>
    /// 确保数值不为负数
    /// </summary>
    public static void NotNegative(decimal value, string paramName)
    {
        if (value < 0)
            throw new ArgumentOutOfRangeException(paramName, "Value cannot be negative.");
    }
}
