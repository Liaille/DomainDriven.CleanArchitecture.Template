using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace BuildingBlocks.ExecutionContext.Abstractions.Common;

/// <summary>
/// Guard类的DateTime相关扩展方法
/// </summary>
[DebuggerStepThrough]
[ExcludeFromCodeCoverage]
public static class Guard
{
    /// <summary>
    /// 确保DateTime为UTC时间
    /// </summary>
    /// <param name="value">要校验的DateTime值</param>
    /// <param name="paramName">参数名 (自动获取)</param>
    /// <exception cref="ArgumentException">当value.Kind不为Utc时抛出</exception>
    public static void IsUtc(
        DateTime value,
        [CallerArgumentExpression(nameof(value))] string paramName = "")
    {
        if (value.Kind != DateTimeKind.Utc)
        {
            throw new ArgumentException(
                $"DateTime must be in UTC kind. Actual kind: {value.Kind}",
                paramName);
        }
    }

    /// <summary>
    /// 确保可空DateTime为UTC时间 (如果有值)
    /// </summary>
    /// <param name="value">要校验的可空DateTime值</param>
    /// <param name="paramName">参数名 (自动获取)</param>
    /// <exception cref="ArgumentException">当value有值且Kind不为Utc时抛出</exception>
    public static void IsUtc(
        DateTime? value,
        [CallerArgumentExpression(nameof(value))] string paramName = "")
    {
        if (value.HasValue && value.Value.Kind != DateTimeKind.Utc)
        {
            throw new ArgumentException(
                $"DateTime must be in UTC kind. Actual kind: {value.Value.Kind}",
                paramName);
        }
    }
}
