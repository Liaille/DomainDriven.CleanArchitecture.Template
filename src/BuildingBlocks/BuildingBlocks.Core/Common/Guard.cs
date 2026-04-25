using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace BuildingBlocks.Core.Common;

/// <summary>
/// 参数校验守卫类 (用于前置条件校验)
/// </summary>
[DebuggerStepThrough] // 调试时自动跳过此类，不进入
public static class Guard
{
    #region 对象 Null 校验
    /// <summary>
    /// 确保对象不为 null
    /// </summary>
    /// <param name="value">校验值</param>
    /// <param name="paramName">参数名</param>
    /// <exception cref="ArgumentNullException" />
    public static void NotNull<T>(T? value, [CallerArgumentExpression(nameof(value))] string paramName = "")
    {
        ArgumentNullException.ThrowIfNull(value, paramName);
    }
    #endregion

    #region 字符串校验
    /// <summary>
    /// 确保字符串不为 null 或空
    /// </summary>
    /// <exception cref="ArgumentException" />
    public static void NotNullOrEmpty(string? value, [CallerArgumentExpression(nameof(value))] string paramName = "")
    {
        ArgumentException.ThrowIfNullOrEmpty(value, paramName);
    }

    /// <summary>
    /// 确保字符串不为 null、空或仅空白字符
    /// </summary>
    /// <exception cref="ArgumentException" />
    public static void NotNullOrWhiteSpace(string? value, [CallerArgumentExpression(nameof(value))] string paramName = "")
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value, paramName);
    }
    #endregion

    #region 集合校验
    /// <summary>
    /// 确保集合不为 null 且至少包含一个元素
    /// </summary>
    /// <exception cref="ArgumentNullException" />
    /// <exception cref="ArgumentException" />
    public static void NotNullOrEmpty<T>(IEnumerable<T>? value, [CallerArgumentExpression(nameof(value))] string paramName = "")
    {
        NotNull(value, paramName);

        if (!value!.Any())
        {
            throw new ArgumentException("Collection cannot be empty.", paramName);
        }
    }
    #endregion

    #region 数值范围校验
    /// <summary>
    /// 确保数值 >= 0 (非负数)
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException" />
    public static void NotNegative(int value, [CallerArgumentExpression(nameof(value))] string paramName = "")
    {
        if (value < 0)
        {
            throw new ArgumentOutOfRangeException(paramName, value, "Value cannot be negative.");
        }
    }

    /// <summary>
    /// 确保数值 >= 0 (非负数)
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException" />
    public static void NotNegative(long value, [CallerArgumentExpression(nameof(value))] string paramName = "")
    {
        if (value < 0)
        {
            throw new ArgumentOutOfRangeException(paramName, value, "Value cannot be negative.");
        }
    }

    /// <summary>
    /// 确保数值 >= 0 (非负数)
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException" />
    public static void NotNegative(decimal value, [CallerArgumentExpression(nameof(value))] string paramName = "")
    {
        if (value < 0)
        {
            throw new ArgumentOutOfRangeException(paramName, value, "Value cannot be negative.");
        }
    }

    /// <summary>
    /// 确保数值 > 0 (正数)
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException" />
    public static void Positive(int value, [CallerArgumentExpression(nameof(value))] string paramName = "")
    {
        if (value <= 0)
        {
            throw new ArgumentOutOfRangeException(paramName, value, "Value must be greater than zero.");
        }
    }

    /// <summary>
    /// 确保数值在指定范围内 [min, max]
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException" />
    public static void InRange(int value, int min, int max, [CallerArgumentExpression(nameof(value))] string paramName = "")
    {
        if (value < min || value > max)
        {
            throw new ArgumentOutOfRangeException(paramName, value, $"Value must be between {min} and {max}.");
        }
    }
    #endregion

    #region 布尔条件校验
    /// <summary>
    /// 确保条件为 true
    /// </summary>
    /// <exception cref="ArgumentException" />
    public static void Against(bool condition, string message, [CallerArgumentExpression(nameof(condition))] string paramName = "")
    {
        if (condition)
        {
            throw new ArgumentException(message, paramName);
        }
    }

    /// <summary>
    /// 确保条件为 true (通用断言)
    /// </summary>
    /// <exception cref="InvalidOperationException" />
    public static void Requires(bool condition, string message)
    {
        if (!condition)
        {
            throw new InvalidOperationException(message);
        }
    }
    #endregion

    #region 格式校验
    /// <summary>
    /// 确保字符串是有效的 Guid
    /// </summary>
    /// <exception cref="ArgumentException" />
    public static void ValidGuid(string? value, [CallerArgumentExpression(nameof(value))] string paramName = "")
    {
        NotNullOrWhiteSpace(value, paramName);

        if (!Guid.TryParse(value, out _))
        {
            throw new ArgumentException("String is not a valid Guid format.", paramName);
        }
    }

    /// <summary>
    /// 确保 Guid 不是空值
    /// </summary>
    /// <exception cref="ArgumentException" />
    public static void NotEmpty(Guid value, [CallerArgumentExpression(nameof(value))] string paramName = "")
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("Guid cannot be empty.", paramName);
        }
    }
    #endregion

    #region 高级：枚举校验
    /// <summary>
    /// 确保枚举值是定义的有效值
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException" />
    public static void ValidEnum<TEnum>(TEnum value, [CallerArgumentExpression(nameof(value))] string paramName = "")
        where TEnum : struct, Enum
    {
        if (!Enum.IsDefined(value))
        {
            throw new ArgumentOutOfRangeException(
                paramName,
                value,
                $"Enum value {value} is not defined in {typeof(TEnum).Name}.");
        }
    }
    #endregion
}
