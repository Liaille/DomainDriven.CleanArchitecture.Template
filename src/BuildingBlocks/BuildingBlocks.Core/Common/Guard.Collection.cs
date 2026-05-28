using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace BuildingBlocks.Core.Common;

/// <summary>
/// Guard类的集合校验部分
/// </summary>
public static partial class Guard
{
    /// <summary>
    /// 确保集合不为null且不为空
    /// </summary>
    /// <typeparam name="T">集合元素类型</typeparam>
    /// <param name="value">要校验的集合</param>
    /// <param name="paramName">参数名 (自动获取)</param>
    /// <exception cref="ArgumentNullException">当集合为null时抛出</exception>
    /// <exception cref="ArgumentException">当集合为空时抛出</exception>
    public static void NotNullOrEmpty<T>(
        [NotNull] IEnumerable<T>? value,
        [CallerArgumentExpression(nameof(value))] string paramName = "")
    {
        NotNull(value, paramName);

        if (value is ICollection<T> collection)
        {
            if (collection.Count == 0)
            {
                throw new ArgumentException("Collection cannot be empty", paramName);
            }
        }
        else if (!value.Any())
        {
            throw new ArgumentException("Collection cannot be empty", paramName);
        }
    }

    /// <summary>
    /// 确保集合不包含null元素
    /// </summary>
    /// <typeparam name="T">集合元素类型</typeparam>
    /// <param name="value">要校验的集合</param>
    /// <param name="paramName">参数名 (自动获取)</param>
    /// <exception cref="ArgumentNullException">当集合为null时抛出</exception>
    /// <exception cref="ArgumentException">当集合包含null元素时抛出</exception>
    public static void NoNullElements<T>(
        [NotNull] IEnumerable<T>? value,
        [CallerArgumentExpression(nameof(value))] string paramName = "")
    {
        NotNull(value, paramName);

        if (value.Any(item => item == null))
        {
            throw new ArgumentException("Collection cannot contain null elements", paramName);
        }
    }

    /// <summary>
    /// 确保集合包含指定数量的元素
    /// </summary>
    /// <typeparam name="T">集合元素类型</typeparam>
    /// <param name="value">要校验的集合</param>
    /// <param name="count">期望的元素数量</param>
    /// <param name="paramName">参数名 (自动获取)</param>
    /// <exception cref="ArgumentNullException">当集合为null时抛出</exception>
    /// <exception cref="ArgumentException">当集合元素数量不符时抛出</exception>
    public static void CountEquals<T>(
        [NotNull] IEnumerable<T>? value,
        int count,
        [CallerArgumentExpression(nameof(value))] string paramName = "")
    {
        NotNull(value, paramName);

        if (value.Count() != count)
        {
            throw new ArgumentException($"Collection must contain exactly {count} elements", paramName);
        }
    }
}
