namespace Domain.Shared.Constants;

/// <summary>
/// 全局正则表达式常量
/// 集中管理所有业务验证用的正则表达式，避免硬编码
/// 注意: 模板仅提供基础通用正则，具体业务可在应用层扩展
/// </summary>
public static class RegexPatterns
{
    /// <summary>
    /// 邮箱正则表达式 (符合 RFC 5322 标准)
    /// </summary>
    public const string Email = @"^[a-zA-Z0-9_!#$%&'*+/=?`{|}~^.-]+@[a-zA-Z0-9.-]+$";

    /// <summary>
    /// 国际手机号正则表达式 (E.164 标准格式)
    /// 格式: +[国家码][手机号]，例如: +8613800138000
    /// 注意: 模板仅验证格式，不验证具体国家码的有效性
    /// </summary>
    public const string InternationalPhone = @"^\+[1-9]\d{1,14}$";

    /// <summary>
    /// 中国身份证号正则表达式 (18位)
    /// 仅作为可选验证，模板不强制使用
    /// </summary>
    public const string ChineseIdCard = @"^[1-9]\d{5}(18|19|20)\d{2}(0[1-9]|1[0-2])(0[1-9]|[12]\d|3[01])\d{3}[\dXx]$";

    /// <summary>
    /// 邮政编码正则表达式 (6位数字，通用格式)
    /// </summary>
    public const string PostalCode = @"^\d{4,10}$";

    /// <summary>
    /// 用户名正则表达式 (4-20位字母、数字、下划线)
    /// </summary>
    public const string UserName = @"^[a-zA-Z0-9_]{4,20}$";

    /// <summary>
    /// 密码正则表达式 (至少8位，包含字母和数字)
    /// </summary>
    public const string Password = @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d@$!%*#?&]{8,}$";
}
