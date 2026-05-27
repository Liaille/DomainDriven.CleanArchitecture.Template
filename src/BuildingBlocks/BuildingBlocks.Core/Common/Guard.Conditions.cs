using System.Diagnostics.CodeAnalysis;

namespace BuildingBlocks.Core.Common;

/// <summary>
/// Guard类的通用条件校验部分
/// </summary>
public static partial class Guard
{
    /// <summary>
    /// 确保条件为true (通用前置条件断言)
    /// </summary>
    /// <param name="condition">要校验的条件</param>
    /// <param name="message">条件不满足时的异常消息</param>
    /// <exception cref="InvalidOperationException">当condition为false时抛出</exception>
    public static void Requires([DoesNotReturnIf(false)] bool condition, string message)
    {
        if (!condition)
        {
            throw new InvalidOperationException(message);
        }
    }

    /// <summary>
    /// 确保条件为false (通用反条件断言)
    /// </summary>
    /// <param name="condition">要校验的条件</param>
    /// <param name="message">条件满足时的异常消息</param>
    /// <exception cref="InvalidOperationException">当condition为true时抛出</exception>
    public static void Against([DoesNotReturnIf(true)] bool condition, string message)
    {
        if (condition)
        {
            throw new InvalidOperationException(message);
        }
    }
}
