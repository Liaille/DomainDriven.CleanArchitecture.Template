using Domain.Shared.Enums;
using Domain.Shared.ErrorCodes;
using Domain.Shared.Exceptions;

namespace Domain.Shared.ValueObjects;

/// <summary>
/// 金额值对象
/// 核心职责: 封装金额数值与币种，避免币种混淆与精度丢失
/// 验证规则: 金额数值必须 >= 0，币种必须为有效枚举值
/// 设计说明: 
/// - 使用 decimal 类型保证精度
/// - 支持主要币种 (ISO 4217 标准)
/// - 提供基础的金额运算
/// - 具体业务可继承扩展更多币种和运算规则
/// </summary>
public record Money
{
    /// <summary>
    /// 金额数值 (精确到最小货币单位，如分、便士等)
    /// 使用 decimal 类型保证精度，避免浮点数精度丢失
    /// </summary>
    public decimal Amount { get; }

    /// <summary>
    /// 币种
    /// 遵循 ISO 4217 标准
    /// </summary>
    public Currency Currency { get; }

    /// <summary>
    /// 私有构造函数
    /// </summary>
    /// <param name="amount">金额数值</param>
    /// <param name="currency">币种</param>
    private Money(decimal amount, Currency currency)
    {
        Amount = amount;
        Currency = currency;
    }

    /// <summary>
    /// 创建金额值对象的工厂方法
    /// </summary>
    /// <param name="amount">金额数值</param>
    /// <param name="currency">币种</param>
    /// <returns>验证通过的Money值对象</returns>
    /// <exception cref="DomainBusinessException">验证失败时抛出业务异常</exception>
    public static Money Create(decimal amount, Currency currency)
    {
        if (amount < 0)
            throw new DomainBusinessException(BusinessErrorCode.ValueOutOfRange, "金额不能为负数");

        if (!Enum.IsDefined(currency))
            throw new DomainBusinessException(BusinessErrorCode.InvalidDataFormat, "无效的币种");

        return new Money(amount, currency);
    }

    /// <summary>
    /// 创建默认币种 (人民币)的金额值对象
    /// </summary>
    /// <param name="amount">金额数值</param>
    /// <returns>验证通过的Money值对象</returns>
    public static Money Cny(decimal amount) => Create(amount, Currency.CNY);

    /// <summary>
    /// 创建美元金额值对象
    /// </summary>
    /// <param name="amount">金额数值</param>
    /// <returns>验证通过的Money值对象</returns>
    public static Money Usd(decimal amount) => Create(amount, Currency.USD);

    /// <summary>
    /// 创建欧元金额值对象
    /// </summary>
    /// <param name="amount">金额数值</param>
    /// <returns>验证通过的Money值对象</returns>
    public static Money Eur(decimal amount) => Create(amount, Currency.EUR);

    /// <summary>
    /// 金额加法 (仅支持相同币种)
    /// </summary>
    /// <param name="other">要相加的金额</param>
    /// <returns>相加后的新金额值对象</returns>
    /// <exception cref="DomainBusinessException">币种不匹配时抛出业务异常</exception>
    public Money Add(Money other)
    {
        if (Currency != other.Currency)
            throw new DomainBusinessException(BusinessErrorCode.CurrencyMismatch, "币种不匹配，无法进行加法操作");

        return new Money(Amount + other.Amount, Currency);
    }

    /// <summary>
    /// 金额减法 (仅支持相同币种)
    /// </summary>
    /// <param name="other">要减去的金额</param>
    /// <returns>相减后的新金额值对象</returns>
    /// <exception cref="DomainBusinessException">币种不匹配或结果为负时抛出业务异常</exception>
    public Money Subtract(Money other)
    {
        if (Currency != other.Currency)
            throw new DomainBusinessException(BusinessErrorCode.CurrencyMismatch, "币种不匹配，无法进行减法操作");

        decimal newAmount = Amount - other.Amount;
        if (newAmount < 0)
            throw new DomainBusinessException(BusinessErrorCode.ValueOutOfRange, "金额不足");

        return new Money(newAmount, Currency);
    }

    /// <summary>
    /// 金额乘法 (仅支持相同币种)
    /// </summary>
    /// <param name="multiplier">乘数</param>
    /// <returns>相乘后的新金额值对象</returns>
    public Money Multiply(decimal multiplier)
    {
        if (multiplier < 0)
            throw new DomainBusinessException(BusinessErrorCode.ValueOutOfRange, "乘数不能为负数");

        return new Money(Amount * multiplier, Currency);
    }

    /// <summary>
    /// 金额除法 (仅支持相同币种)
    /// </summary>
    /// <param name="divisor">除数</param>
    /// <returns>相除后的新金额值对象</returns>
    public Money Divide(decimal divisor)
    {
        if (divisor <= 0)
            throw new DomainBusinessException(BusinessErrorCode.ValueOutOfRange, "除数必须大于0");

        return new Money(Amount / divisor, Currency);
    }

    /// <summary>
    /// 判断金额是否为零
    /// </summary>
    /// <returns>是否为零</returns>
    public bool IsZero() => Amount == 0;

    /// <summary>
    /// 判断金额是否大于另一个金额
    /// </summary>
    /// <param name="other">要比较的金额</param>
    /// <returns>比较结果</returns>
    /// <exception cref="DomainBusinessException">币种不匹配时抛出业务异常</exception>
    public bool IsGreaterThan(Money other)
    {
        if (Currency != other.Currency)
            throw new DomainBusinessException(BusinessErrorCode.CurrencyMismatch, "币种不匹配，无法进行比较");

        return Amount > other.Amount;
    }

    /// <summary>
    /// 判断金额是否小于另一个金额
    /// </summary>
    /// <param name="other">要比较的金额</param>
    /// <returns>比较结果</returns>
    /// <exception cref="DomainBusinessException">币种不匹配时抛出业务异常</exception>
    public bool IsLessThan(Money other)
    {
        if (Currency != other.Currency)
            throw new DomainBusinessException(BusinessErrorCode.CurrencyMismatch, "币种不匹配，无法进行比较");

        return Amount < other.Amount;
    }

    /// <summary>
    /// 获取币种的 ISO 4217 代码
    /// </summary>
    /// <returns>ISO 4217 代码</returns>
    public string GetCurrencyCode()
    {
        return Currency switch
        {
            Currency.CNY => "CNY",
            Currency.USD => "USD",
            Currency.EUR => "EUR",
            Currency.HKD => "HKD",
            Currency.JPY => "JPY",
            Currency.GBP => "GBP",
            Currency.AUD => "AUD",
            Currency.CAD => "CAD",
            Currency.CHF => "CHF",
            Currency.SGD => "SGD",
            Currency.TWD => "TWD",
            Currency.KRW => "KRW",
            Currency.RUB => "RUB",
            Currency.INR => "INR",
            Currency.BRL => "BRL",
            Currency.AED => "AED",
            Currency.SAR => "SAR",
            _ => Currency.ToString().ToUpperInvariant()
        };
    }

    /// <summary>
    /// 获取币种符号
    /// </summary>
    /// <returns>币种符号</returns>
    public string GetCurrencySymbol()
    {
        return Currency switch
        {
            Currency.CNY => "¥",
            Currency.USD => "$",
            Currency.EUR => "€",
            Currency.HKD => "HK$",
            Currency.JPY => "¥",
            Currency.GBP => "£",
            Currency.AUD => "A$",
            Currency.CAD => "C$",
            Currency.CHF => "CHF",
            Currency.SGD => "S$",
            Currency.TWD => "NT$",
            Currency.KRW => "₩",
            Currency.RUB => "₽",
            Currency.INR => "₹",
            Currency.BRL => "R$",
            Currency.AED => "د.إ",
            Currency.SAR => "﷼",
            _ => GetCurrencyCode()
        };
    }

    /// <summary>
    /// 获取币种的小数位数
    /// </summary>
    /// <returns>小数位数</returns>
    public int GetDecimalPlaces()
    {
        return Currency switch
        {
            Currency.JPY => 0, // 日元没有小数
            Currency.KRW => 0, // 韩元没有小数
            _ => 2
        };
    }

    /// <summary>
    /// 重写ToString方法，返回格式化的金额字符串
    /// </summary>
    /// <returns>格式化的金额字符串 (如: ¥100.00)</returns>
    public override string ToString()
    {
        var symbol = GetCurrencySymbol();
        var decimalPlaces = GetDecimalPlaces();
        return $"{symbol}{Amount.ToString($"F{decimalPlaces}")}";
    }

    /// <summary>
    /// 加法运算符重载
    /// </summary>
    public static Money operator +(Money left, Money right) => left.Add(right);

    /// <summary>
    /// 减法运算符重载
    /// </summary>
    public static Money operator -(Money left, Money right) => left.Subtract(right);

    /// <summary>
    /// 乘法运算符重载
    /// </summary>
    public static Money operator *(Money money, decimal multiplier) => money.Multiply(multiplier);

    /// <summary>
    /// 除法运算符重载
    /// </summary>
    public static Money operator /(Money money, decimal divisor) => money.Divide(divisor);

    /// <summary>
    /// 大于运算符重载
    /// </summary>
    public static bool operator >(Money left, Money right) => left.IsGreaterThan(right);

    /// <summary>
    /// 小于运算符重载
    /// </summary>
    public static bool operator <(Money left, Money right) => left.IsLessThan(right);

    /// <summary>
    /// 大于等于运算符重载
    /// </summary>
    public static bool operator >=(Money left, Money right) => left.IsGreaterThan(right) || left == right;

    /// <summary>
    /// 小于等于运算符重载
    /// </summary>
    public static bool operator <=(Money left, Money right) => left.IsLessThan(right) || left == right;
}