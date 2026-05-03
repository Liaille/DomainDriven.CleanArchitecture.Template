using System.Text.RegularExpressions;
using Domain.Shared.Constants;

namespace Domain.Shared.Regexes;

/// <summary>
/// 领域层全局共享正则表达式
/// 源生成正则，高性能、无GC、全局单例复用
/// </summary>
public static partial class DomainRegexes
{
    /// <summary>
    /// 邮箱正则 (RFC 5322)
    /// </summary>
    [GeneratedRegex(RegexPatterns.Email, RegexOptions.Compiled | RegexOptions.IgnoreCase)]
    public static partial Regex Email();

    /// <summary>
    /// 国际手机号正则 (E.164 标准)
    /// </summary>
    [GeneratedRegex(RegexPatterns.InternationalPhone, RegexOptions.Compiled)]
    public static partial Regex InternationalPhone();

    /// <summary>
    /// 中国身份证号正则 (18位)
    /// </summary>
    [GeneratedRegex(RegexPatterns.ChineseIdCard, RegexOptions.Compiled)]
    public static partial Regex ChineseIdCard();

    /// <summary>
    /// 邮政编码正则
    /// </summary>
    [GeneratedRegex(RegexPatterns.PostalCode, RegexOptions.Compiled)]
    public static partial Regex PostalCode();

    /// <summary>
    /// 用户名正则
    /// </summary>
    [GeneratedRegex(RegexPatterns.UserName, RegexOptions.Compiled)]
    public static partial Regex UserName();

    /// <summary>
    /// 密码正则
    /// </summary>
    [GeneratedRegex(RegexPatterns.Password, RegexOptions.Compiled)]
    public static partial Regex Password();
}