using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace BuildingBlocks.Core.Common;

/// <summary>
/// Guard类的Null校验部分
/// </summary>
public static partial class Guard
{
    /// <summary>
    /// 确保对象不为null
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    /// <param name="value">要校验的值</param>
    /// <param name="paramName">参数名 (自动获取，无需手动传入)</param>
    /// <exception cref="ArgumentNullException">当value为null时抛出</exception>
    public static void NotNull<T>([NotNull] T? value, [CallerArgumentExpression(nameof(value))] string paramName = "")
    {
        ArgumentNullException.ThrowIfNull(value, paramName);
    }

    /// <summary>
    /// 确保可空值类型有值
    /// </summary>
    /// <typeparam name="T">值类型</typeparam>
    /// <param name="value">要校验的可空值</param>
    /// <param name="paramName">参数名 (自动获取，无需手动传入)</param>
    /// <exception cref="ArgumentNullException">当value为null时抛出</exception>
    public static void HasValue<T>([NotNull] T? value, [CallerArgumentExpression(nameof(value))] string paramName = "")
        where T : struct
    {
        if (!value.HasValue)
        {
            throw new ArgumentNullException(paramName, "Nullable value must have a value.");
        }
    }
}
