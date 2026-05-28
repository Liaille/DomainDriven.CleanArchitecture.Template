using BuildingBlocks.Core.Common;
using System.Text.RegularExpressions;

namespace BuildingBlocks.Core.Errors;

/// <summary>
/// 错误码格式校验器
/// <para>【格式规范】</para>
/// <para>[错误类型码(1位)][严重级别(1位)][业务线编码(2位)][模块编码(3位)][错误编码(3位)]</para>
/// <para>总长度: 10位</para>
/// </summary>
internal static class ErrorCodeValidator
{
    /// <summary>
    /// 错误码正则表达式
    /// </summary>
    private static readonly Regex ErrorCodeRegex = new(
        @"^[PABSTF][0123]\d{2}\d{3}\d{3}$",
        RegexOptions.Compiled | RegexOptions.Singleline,
        TimeSpan.FromSeconds(1));

    /// <summary>
    /// 验证错误码格式是否正确
    /// </summary>
    /// <param name="code"></param>
    public static void ValidateCodeFormat(string code)
    {
        Guard.NotNullOrWhiteSpace(code);
        Guard.Requires(ErrorCodeRegex.IsMatch(code), $"错误码格式不符合规范，应为：[P/A/B/S/T/F][0/1/2/3][01-99][001-999][001-999]，实际值: {code}");
    }

    /// <summary>
    /// 验证错误码与类型是否匹配
    /// </summary>
    /// <param name="code"></param>
    /// <param name="type"></param>
    public static void ValidateCodeAndTypeMatch(string code, ErrorType type)
    {
        Guard.NotNullOrWhiteSpace(code);

        var expectedTypeCode = (char)type;
        var actualTypeCode = code[0];

        Guard.Requires(actualTypeCode == expectedTypeCode, $"错误码前缀与类型不匹配: 错误码以'{actualTypeCode}'开头，但指定的类型是{type}(应为'{expectedTypeCode}')");
    }

    /// <summary>
    /// 从错误码中解析错误类型
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    public static ErrorType ParseErrorType(string code)
    {
        ValidateCodeFormat(code);
        return (ErrorType)code[0];
    }

    /// <summary>
    /// 从错误码中解析严重级别
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    public static SeverityLevel ParseSeverityLevel(string code)
    {
        ValidateCodeFormat(code);
        return (SeverityLevel)code[1];
    }

    /// <summary>
    /// 验证错误码格式 + 类型匹配
    /// </summary>
    /// <param name="code"></param>
    /// <param name="type"></param>
    public static void Validate(string code, ErrorType type)
    {
        ValidateCodeFormat(code);
        ValidateCodeAndTypeMatch(code, type);
    }
}
