using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace BuildingBlocks.Core.Common;

/// <summary>
/// Guard类的数值校验部分
/// </summary>
public static partial class Guard
{
    /// <summary>
    /// 确保数值在指定范围内
    /// </summary>
    /// <typeparam name="T">数值类型</typeparam>
    /// <param name="value">要校验的值</param>
    /// <param name="minValue">最小值 (包含)</param>
    /// <param name="maxValue">最大值 (包含)</param>
    /// <param name="paramName">参数名 (自动获取)</param>
    /// <exception cref="ArgumentOutOfRangeException">当值超出范围时抛出</exception>
    public static void InRange<T>(
        [NotNull] T value,
        T minValue,
        T maxValue,
        [CallerArgumentExpression(nameof(value))] string paramName = "")
        where T : IComparable<T>, IComparable
    {
        if (value.CompareTo(minValue) < 0 || value.CompareTo(maxValue) > 0)
        {
            throw new ArgumentOutOfRangeException(paramName, value, $"Value must be in the range [{minValue}, {maxValue}]");
        }
    }

    /// <summary>
    /// 确保数值大于指定值
    /// </summary>
    /// <typeparam name="T">数值类型</typeparam>
    /// <param name="value">要校验的值</param>
    /// <param name="minValue">最小值 (不包含)</param>
    /// <param name="paramName">参数名 (自动获取)</param>
    /// <exception cref="ArgumentOutOfRangeException">当值小于等于最小值时抛出</exception>
    public static void GreaterThan<T>(
        [NotNull] T value,
        T minValue,
        [CallerArgumentExpression(nameof(value))] string paramName = "")
        where T : IComparable<T>, IComparable
    {
        if (value.CompareTo(minValue) <= 0)
        {
            throw new ArgumentOutOfRangeException(paramName, value, $"Value must be greater than {minValue}");
        }
    }

    /// <summary>
    /// 确保数值大于等于指定值
    /// </summary>
    /// <typeparam name="T">数值类型</typeparam>
    /// <param name="value">要校验的值</param>
    /// <param name="minValue">最小值 (包含)</param>
    /// <param name="paramName">参数名 (自动获取)</param>
    /// <exception cref="ArgumentOutOfRangeException">当值小于最小值时抛出</exception>
    public static void GreaterThanOrEqual<T>(
        [NotNull] T value,
        T minValue,
        [CallerArgumentExpression(nameof(value))] string paramName = "")
        where T : IComparable<T>, IComparable
    {
        if (value.CompareTo(minValue) < 0)
        {
            throw new ArgumentOutOfRangeException(paramName, value, $"Value must be greater than or equal to {minValue}");
        }
    }

    /// <summary>
    /// 确保数值小于指定值
    /// </summary>
    /// <typeparam name="T">数值类型</typeparam>
    /// <param name="value">要校验的值</param>
    /// <param name="maxValue">最大值 (不包含)</param>
    /// <param name="paramName">参数名 (自动获取)</param>
    /// <exception cref="ArgumentOutOfRangeException">当值大于等于最大值时抛出</exception>
    public static void LessThan<T>(
        [NotNull] T value,
        T maxValue,
        [CallerArgumentExpression(nameof(value))] string paramName = "")
        where T : IComparable<T>, IComparable
    {
        if (value.CompareTo(maxValue) >= 0)
        {
            throw new ArgumentOutOfRangeException(paramName, value, $"Value must be less than {maxValue}");
        }
    }

    /// <summary>
    /// 确保数值小于等于指定值
    /// </summary>
    /// <typeparam name="T">数值类型</typeparam>
    /// <param name="value">要校验的值</param>
    /// <param name="maxValue">最大值 (包含)</param>
    /// <param name="paramName">参数名 (自动获取)</param>
    /// <exception cref="ArgumentOutOfRangeException">当值大于最大值时抛出</exception>
    public static void LessThanOrEqual<T>(
        [NotNull] T value,
        T maxValue,
        [CallerArgumentExpression(nameof(value))] string paramName = "")
        where T : IComparable<T>, IComparable
    {
        if (value.CompareTo(maxValue) > 0)
        {
            throw new ArgumentOutOfRangeException(paramName, value, $"Value must be less than or equal to {maxValue}");
        }
    }

    /// <summary>
    /// 确保数值为正数
    /// </summary>
    /// <typeparam name="T">数值类型</typeparam>
    /// <param name="value">要校验的值</param>
    /// <param name="paramName">参数名 (自动获取)</param>
    /// <exception cref="ArgumentOutOfRangeException">当值小于等于0时抛出</exception>
    public static void Positive<T>(
        [NotNull] T value,
        [CallerArgumentExpression(nameof(value))] string paramName = "")
        where T : IComparable<T>, IComparable, INumber<T>
    {
        if (value.CompareTo(T.Zero) <= 0)
        {
            throw new ArgumentOutOfRangeException(paramName, value, "Value must be a positive number");
        }
    }

    /// <summary>
    /// 确保数值为非负数
    /// </summary>
    /// <typeparam name="T">数值类型</typeparam>
    /// <param name="value">要校验的值</param>
    /// <param name="paramName">参数名 (自动获取)</param>
    /// <exception cref="ArgumentOutOfRangeException">当值小于0时抛出</exception>
    public static void NonNegative<T>(
        [NotNull] T value,
        [CallerArgumentExpression(nameof(value))] string paramName = "")
        where T : IComparable<T>, IComparable, INumber<T>
    {
        if (value.CompareTo(T.Zero) < 0)
        {
            throw new ArgumentOutOfRangeException(paramName, value, "Value must be a non-negative number");
        }
    }
}
