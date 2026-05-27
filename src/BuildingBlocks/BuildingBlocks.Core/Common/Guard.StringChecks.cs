using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace BuildingBlocks.Core.Common;

/// <summary>
/// Guard类的字符串基础校验部分
/// </summary>
public static partial class Guard
{
    /// <summary>
    /// 确保字符串不为null或空字符串
    /// </summary>
    /// <param name="value">要校验的字符串</param>
    /// <param name="paramName">参数名 (自动获取，无需手动传入)</param>
    /// <exception cref="ArgumentException">当value为null或空时抛出</exception>
    public static void NotNullOrEmpty([NotNull] string? value, [CallerArgumentExpression(nameof(value))] string paramName = "")
    {
        ArgumentException.ThrowIfNullOrEmpty(value, paramName);
    }

    /// <summary>
    /// 确保字符串不为null、空字符串或仅包含空白字符
    /// </summary>
    /// <param name="value">要校验的字符串</param>
    /// <param name="paramName">参数名 (自动获取，无需手动传入)</param>
    /// <exception cref="ArgumentException">当value为null、空或仅空白时抛出</exception>
    public static void NotNullOrWhiteSpace([NotNull] string? value, [CallerArgumentExpression(nameof(value))] string paramName = "")
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value, paramName);
    }
}
