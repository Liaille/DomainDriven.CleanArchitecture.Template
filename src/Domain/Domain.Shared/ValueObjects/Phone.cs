using Domain.Shared.Constants;
using Domain.Shared.ErrorCodes;
using Domain.Shared.Exceptions;
using Domain.Shared.Regexes;
using System.Text.RegularExpressions;

namespace Domain.Shared.ValueObjects;

/// <summary>
/// 电话号码值对象
/// 核心职责: 封装电话号码，支持国际格式
/// 验证规则: 遵循 E.164 国际标准格式 (+[国家码][手机号])
/// 设计说明: 模板仅验证格式，不验证具体国家码的有效性，具体业务可继承扩展
/// </summary>
public partial record Phone
{
    /// <summary>
    /// 电话号码字符串值 (E.164 格式)
    /// </summary>
    public string Number { get; }

    /// <summary>
    /// 国家码 (从 E.164 格式中提取)
    /// </summary>
    public string CountryCode { get; }

    /// <summary>
    /// 本地号码 (去除国家码后的部分)
    /// </summary>
    public string NationalNumber { get; }

    /// <summary>
    /// 国际手机号正则表达式 (E.164 标准格式)
    /// </summary>
    private static readonly Regex _internationalPhoneRegex = DomainRegexes.InternationalPhone();

    /// <summary>
    /// 私有构造函数
    /// </summary>
    /// <param name="number">经过验证的电话号码 (E.164 格式)</param>
    private Phone(string number)
    {
        Number = number;

        // 解析国家码和本地号码
        // E.164 格式: +[国家码][本地号码]
        // 国家码长度: 1-3位
        // 这里仅做简单解析，具体业务可根据国家码表精确解析
        CountryCode = ExtractCountryCode(number);
        NationalNumber = number[(CountryCode.Length + 1)..]; // 修复: 加括号
    }

    /// <summary int>
    /// 创建电话号码值对象的工厂方法
    /// 执行 E.164 格式验证
    /// </summary>
    /// <param name="number">待验证的电话号码</param>
    /// <returns>验证通过的Phone值对象</returns>
    /// <exception cref="DomainBusinessException">验证失败时抛出业务异常</exception>
    public static Phone Create(string number)
    {
        if (string.IsNullOrWhiteSpace(number))
            throw new DomainBusinessException(BusinessErrorCodes.RequiredValueNotEmpty, "电话号码不能为空");

        var cleanedNumber = number.Trim();

        // E.164 格式验证
        if (!_internationalPhoneRegex.IsMatch(cleanedNumber))
            throw new DomainBusinessException(
                BusinessErrorCodes.InvalidDataFormat,
                "电话号码格式不正确，请使用 E.164 国际标准格式 (例如: +8613800138000)");

        return new Phone(cleanedNumber);
    }

    /// <summary>
    /// 尝试从 E.164 格式号码中提取国家码
    /// 注意: 这是一个简化实现，具体业务应使用完整的国家码表
    /// </summary>
    /// <param name="e164Number">E.164 格式号码</param>
    /// <returns>国家码</returns>
    private static string ExtractCountryCode(string e164Number)
    {
        // 移除 + 号
        var digits = e164Number[1..];

        // 简化实现: 尝试常见的国家码长度
        // 具体业务应使用完整的国家码数据库
        // 1位国家码 (美国、加拿大等)
        if (digits.StartsWith('1'))
            return "1";

        // 2位国家码
        var twoDigitCode = digits[..2];
        if (IsKnownTwoDigitCountryCode(twoDigitCode))
            return twoDigitCode;

        // 3位国家码
        return digits[..3];
    }

    /// <summary>
    /// 两位国家码集合 (支持外部配置)
    /// </summary>
    private static readonly HashSet<string> KnownTwoDigitCountryCodes = InitializeKnownCountryCodes();

    private static HashSet<string> InitializeKnownCountryCodes()
    {
        // 生产环境可替换为: 从配置文件、环境变量、领域配置服务读取
        var defaultCodes = new[]
        {
            "20", "27", "30", "31", "32", "33", "34", "36", "39",
            "40", "41", "43", "44", "45", "46", "47", "48", "49",
            "51", "52", "53", "54", "55", "56", "57", "58", "60",
            "61", "62", "63", "64", "65", "66", "81", "82", "84",
            "86", "90", "91", "92", "93", "94", "95", "98"
        };

        return new HashSet<string>(defaultCodes, StringComparer.Ordinal);
    }

    /// <summary>
    /// 检查是否为已知的2位国家码 (生产级标准)
    /// </summary>
    private static bool IsKnownTwoDigitCountryCode(string code)
    {
        return !string.IsNullOrEmpty(code) && KnownTwoDigitCountryCodes.Contains(code);
    }

    /// <summary>
    /// 重写ToString方法，返回完整的 E.164 格式电话号码
    /// </summary>
    /// <returns>电话号码字符串</returns>
    public override string ToString() => Number;

    /// <summary>
    /// 隐式转换为string类型
    /// </summary>
    /// <param name="phone">Phone值对象</param>
    public static implicit operator string(Phone phone) => phone.Number;
}
