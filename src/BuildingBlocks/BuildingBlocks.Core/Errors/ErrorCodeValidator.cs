using BuildingBlocks.Core.Common;

namespace BuildingBlocks.Core.Errors;

/// <summary>
/// 错误码格式校验器
/// </summary>
internal static class ErrorCodeValidator
{
    public const int ErrorCodeLength = 10;

    public static void ValidateCodeFormat(string code)
    {
        Guard.NotNullOrWhiteSpace(code);
        Guard.Requires(code.Length == ErrorCodeLength, $"错误码必须为{ErrorCodeLength}位固定长度，当前长度: {code.Length}");
        Guard.Requires(Enum.IsDefined(typeof(ErrorType), (ErrorType)code[0]), $"无效的错误类型码: {code[0]}，必须为P/B/S/T/F中的一个");
    }

    public static void ValidateCodeAndTypeMatch(string code, ErrorType type)
    {
        Guard.NotNullOrWhiteSpace(code);

        var expectedTypeCode = (char)type;
        var actualTypeCode = code[0];

        Guard.Requires(actualTypeCode == expectedTypeCode, $"错误码前缀与类型不匹配: 错误码以'{actualTypeCode}'开头，但指定的类型是{type}(应为'{expectedTypeCode}')");
    }

    public static void Validate(string code, ErrorType type)
    {
        ValidateCodeFormat(code);
        ValidateCodeAndTypeMatch(code, type);
    }
}
