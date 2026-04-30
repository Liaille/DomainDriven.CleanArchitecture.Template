using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace BuildingBlocks.Core.Common;

/// <summary>
/// 参数校验守卫类 (用于前置条件校验)
/// 设计目的: 消除重复的if判断，提供统一的参数校验入口
/// 使用场景: 方法入口处对参数进行前置校验
/// </summary>
[DebuggerStepThrough] // 调试时自动跳过此类，不进入
public static partial class Guard
{
    #region 对象 Null 校验
    /// <summary>
    /// 确保对象不为 null
    /// </summary>
    /// <param name="value">校验值</param>
    /// <param name="paramName">参数名 (自动通过CallerArgumentExpression获取)</param>
    /// <exception cref="ArgumentNullException">当value为null时抛出</exception>
    public static void NotNull<T>(T? value, [CallerArgumentExpression(nameof(value))] string paramName = "")
    {
        ArgumentNullException.ThrowIfNull(value, paramName);
    }
    #endregion

    #region 字符串校验
    /// <summary>
    /// 确保字符串不为 null 或空
    /// </summary>
    /// <param name="value">校验值</param>
    /// <param name="paramName">参数名</param>
    /// <exception cref="ArgumentException">当value为null或空时抛出</exception>
    public static void NotNullOrEmpty(string? value, [CallerArgumentExpression(nameof(value))] string paramName = "")
    {
        ArgumentException.ThrowIfNullOrEmpty(value, paramName);
    }

    /// <summary>
    /// 确保字符串不为 null、空或仅空白字符
    /// </summary>
    /// <param name="value">校验值</param>
    /// <param name="paramName">参数名</param>
    /// <exception cref="ArgumentException">当value为null、空或仅空白字符时抛出</exception>
    public static void NotNullOrWhiteSpace(string? value, [CallerArgumentExpression(nameof(value))] string paramName = "")
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value, paramName);
    }

    /// <summary>
    /// 确保字符串长度在 [minLength, maxLength] 范围内
    /// 前置条件: 字符串不能为null、空或仅空白字符
    /// </summary>
    /// <param name="value">校验值</param>
    /// <param name="minLength">最小允许长度 (包含)</param>
    /// <param name="maxLength">最大允许长度 (包含)</param>
    /// <param name="paramName">参数名</param>
    /// <exception cref="ArgumentOutOfRangeException">当字符串长度超出范围时抛出</exception>
    public static void StringLength(string? value, int minLength, int maxLength,
        [CallerArgumentExpression(nameof(value))] string paramName = "")
    {
        // 先确保字符串不为空
        NotNullOrWhiteSpace(value, paramName);

        // 校验长度范围
        if (value!.Length < minLength || value.Length > maxLength)
        {
            throw new ArgumentOutOfRangeException(
                paramName,
                value.Length,
                $"String length must be between {minLength} and {maxLength}, but was {value.Length}.");
        }
    }

    /// <summary>
    /// 确保字符串长度不超过最大长度
    /// 前置条件: 字符串不能为null、空或仅空白字符
    /// </summary>
    /// <param name="value">校验值</param>
    /// <param name="maxLength">最大允许长度 (包含)</param>
    /// <param name="paramName">参数名</param>
    /// <exception cref="ArgumentOutOfRangeException">当字符串长度超出最大长度时抛出</exception>
    public static void StringMaxLength(string? value, int maxLength,
        [CallerArgumentExpression(nameof(value))] string paramName = "")
    {
        // 先确保字符串不为空
        NotNullOrWhiteSpace(value, paramName);

        // 校验最大长度
        if (value!.Length > maxLength)
        {
            throw new ArgumentOutOfRangeException(
                paramName,
                value.Length,
                $"String length cannot exceed {maxLength}, but was {value.Length}.");
        }
    }
    #endregion

    #region 集合校验
    /// <summary>
    /// 确保集合不为 null 且至少包含一个元素
    /// </summary>
    /// <param name="value">校验值</param>
    /// <param name="paramName">参数名</param>
    /// <exception cref="ArgumentNullException">当value为null时抛出</exception>
    /// <exception cref="ArgumentException">当集合为空时抛出</exception>
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
    /// <param name="value">校验值</param>
    /// <param name="paramName">参数名</param>
    /// <exception cref="ArgumentOutOfRangeException">当value为负数时抛出</exception>
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
    /// <param name="value">校验值</param>
    /// <param name="paramName">参数名</param>
    /// <exception cref="ArgumentOutOfRangeException">当value为负数时抛出</exception>
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
    /// <param name="value">校验值</param>
    /// <param name="paramName">参数名</param>
    /// <exception cref="ArgumentOutOfRangeException">当value为负数时抛出</exception>
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
    /// <param name="value">校验值</param>
    /// <param name="paramName">参数名</param>
    /// <exception cref="ArgumentOutOfRangeException">当value小于等于0时抛出</exception>
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
    /// <param name="value">校验值</param>
    /// <param name="min">最小值 (包含)</param>
    /// <param name="max">最大值 (包含)</param>
    /// <param name="paramName">参数名</param>
    /// <exception cref="ArgumentOutOfRangeException">当value超出范围时抛出</exception>
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
    /// <param name="condition">校验条件</param>
    /// <param name="message">异常消息</param>
    /// <param name="paramName">参数名</param>
    /// <exception cref="ArgumentException">当condition为true时抛出</exception>
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
    /// <param name="condition">校验条件</param>
    /// <param name="message">异常消息</param>
    /// <exception cref="InvalidOperationException">当condition为false时抛出</exception>
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
    /// <param name="value">校验值</param>
    /// <param name="paramName">参数名</param>
    /// <exception cref="ArgumentException">当value不是有效的Guid格式时抛出</exception>
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
    /// <param name="value">校验值</param>
    /// <param name="paramName">参数名</param>
    /// <exception cref="ArgumentException">当value为Guid.Empty时抛出</exception>
    public static void NotEmpty(Guid value, [CallerArgumentExpression(nameof(value))] string paramName = "")
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("Guid cannot be empty.", paramName);
        }
    }
    #endregion

    #region 高级: 枚举校验
    /// <summary>
    /// 确保枚举值是定义的有效值
    /// </summary>
    /// <param name="value">校验值</param>
    /// <param name="paramName">参数名</param>
    /// <typeparam name="TEnum">枚举类型</typeparam>
    /// <exception cref="ArgumentOutOfRangeException">当value不是有效的枚举值时抛出</exception>
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
