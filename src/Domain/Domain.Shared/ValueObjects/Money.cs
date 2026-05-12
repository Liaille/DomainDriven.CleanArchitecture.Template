using BuildingBlocks.Core.Common;
using Domain.Shared.Enums;
using Domain.Shared.ErrorCodes;
using Domain.Shared.Exceptions;
using Domain.Shared.Extensions;
using Domain.Shared.Resources.Errors;
using System.Globalization;

namespace Domain.Shared.ValueObjects;

/// <summary>
/// 金额值对象
/// 核心职责: 封装金额数值与币种，避免币种混淆与精度丢失
/// 验证规则: 金额数值必须 >= 0，币种必须为有效枚举值
/// 设计说明: 
/// - 使用 decimal 类型保证精度，避免浮点数精度丢失
/// - 自动标准化金额到对应币种的小数位数
/// - 支持主要币种 (ISO 4217 标准)
/// - 提供基础的金额运算
/// </summary>
public record Money : IComparable<Money>
{
    #region 静态零值常量
    /// <summary>
    /// 零元人民币
    /// </summary>
    public static readonly Money ZeroCny = new(0m, Currency.CNY);

    /// <summary>
    /// 零美元
    /// </summary>
    public static readonly Money ZeroUsd = new(0m, Currency.USD);

    /// <summary>
    /// 零欧元
    /// </summary>
    public static readonly Money ZeroEur = new(0m, Currency.EUR);
    #endregion

    /// <summary>
    /// 金额数值 (已标准化到对应币种的小数位数)
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
    /// 仅内部使用，外部必须通过工厂方法创建
    /// </summary>
    /// <param name="amount">已标准化的金额数值</param>
    /// <param name="currency">币种</param>
    private Money(decimal amount, Currency currency)
    {
        Amount = amount;
        Currency = currency;
    }

    #region 工厂方法
    /// <summary>
    /// 创建金额值对象的工厂方法
    /// 自动将金额标准化到对应币种的小数位数
    /// </summary>
    /// <param name="amount">金额数值</param>
    /// <param name="currency">币种</param>
    /// <returns>验证通过并标准化的Money值对象</returns>
    /// <exception cref="DomainBusinessException">验证失败时抛出业务异常</exception>
    public static Money Create(decimal amount, Currency currency)
    {
        if (amount < 0)
            throw new DomainBusinessException(BusinessErrorCodes.ValueOutOfRange, ErrorMessages.AmountInvalid);

        if (!Enum.IsDefined(currency))
            throw new DomainBusinessException(BusinessErrorCodes.InvalidDataFormat, ErrorMessages.InvalidDataFormat);

        // 自动标准化金额到对应币种的小数位数
        var decimalPlaces = currency.GetDecimalPlaces();
        var standardizedAmount = Math.Round(amount, decimalPlaces, MidpointRounding.AwayFromZero);

        return new Money(standardizedAmount, currency);
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
    /// 创建指定币种的零金额
    /// </summary>
    /// <param name="currency">币种</param>
    /// <returns>零金额值对象</returns>
    public static Money Zero(Currency currency) => Create(0m, currency);
    #endregion

    #region 运算方法
    /// <summary>
    /// 金额加法 (仅支持相同币种)
    /// </summary>
    /// <param name="other">要相加的金额</param>
    /// <returns>相加后的新金额值对象</returns>
    /// <exception cref="ArgumentNullException">参数为null时抛出</exception>
    /// <exception cref="DomainBusinessException">币种不匹配时抛出业务异常</exception>
    public Money Add(Money other)
    {
        Guard.NotNull(other);

        if (Currency != other.Currency)
            throw new DomainBusinessException(BusinessErrorCodes.CurrencyMismatch, ErrorMessages.CurrencyMismatch);

        return Create(Amount + other.Amount, Currency);
    }

    /// <summary>
    /// 金额减法 (仅支持相同币种)
    /// </summary>
    /// <param name="other">要减去的金额</param>
    /// <returns>相减后的新金额值对象</returns>
    /// <exception cref="ArgumentNullException">参数为null时抛出</exception>
    /// <exception cref="DomainBusinessException">币种不匹配或结果为负时抛出业务异常</exception>
    public Money Subtract(Money other)
    {
        Guard.NotNull(other);

        if (Currency != other.Currency)
            throw new DomainBusinessException(BusinessErrorCodes.CurrencyMismatch, ErrorMessages.CurrencyMismatch);

        decimal newAmount = Amount - other.Amount;
        if (newAmount < 0)
            throw new DomainBusinessException(BusinessErrorCodes.ValueOutOfRange, ErrorMessages.InsufficientFunds);

        return Create(newAmount, Currency);
    }

    /// <summary>
    /// 金额乘法
    /// </summary>
    /// <param name="multiplier">乘数</param>
    /// <returns>相乘后的新金额值对象</returns>
    /// <exception cref="DomainBusinessException">乘数为负时抛出业务异常</exception>
    public Money Multiply(decimal multiplier)
    {
        if (multiplier < 0)
            throw new DomainBusinessException(BusinessErrorCodes.ValueOutOfRange, ErrorMessages.MultiplierCannotBeNegative);

        return Create(Amount * multiplier, Currency);
    }

    /// <summary>
    /// 金额除法
    /// 使用银行家舍入 (四舍六入五成双)
    /// </summary>
    /// <param name="divisor">除数</param>
    /// <returns>相除后的新金额值对象</returns>
    /// <exception cref="DomainBusinessException">除数小于等于0时抛出业务异常</exception>
    public Money Divide(decimal divisor)
    {
        return Divide(divisor, MidpointRounding.ToEven);
    }

    /// <summary>
    /// 金额除法 (指定舍入方式)
    /// </summary>
    /// <param name="divisor">除数</param>
    /// <param name="rounding">舍入方式</param>
    /// <returns>相除后的新金额值对象</returns>
    /// <exception cref="DomainBusinessException">除数小于等于0时抛出业务异常</exception>
    public Money Divide(decimal divisor, MidpointRounding rounding)
    {
        if (divisor <= 0)
            throw new DomainBusinessException(BusinessErrorCodes.ValueOutOfRange, ErrorMessages.DivisorMustBePositive);

        var result = Amount / divisor;
        var decimalPlaces = Currency.GetDecimalPlaces();
        var roundedResult = Math.Round(result, decimalPlaces, rounding);

        return Create(roundedResult, Currency);
    }
    #endregion

    #region 比较方法
    /// <summary>
    /// 判断金额是否为零
    /// </summary>
    /// <returns>是否为零</returns>
    public bool IsZero() => Amount == 0;

    /// <summary>
    /// 判断金额是否为正数
    /// </summary>
    /// <returns>是否为正数</returns>
    public bool IsPositive() => Amount > 0;

    /// <summary>
    /// 判断金额是否大于另一个金额
    /// </summary>
    /// <param name="other">要比较的金额</param>
    /// <returns>比较结果</returns>
    /// <exception cref="ArgumentNullException">参数为null时抛出</exception>
    /// <exception cref="DomainBusinessException">币种不匹配时抛出业务异常</exception>
    public bool IsGreaterThan(Money other)
    {
        Guard.NotNull(other);

        if (Currency != other.Currency)
            throw new DomainBusinessException(BusinessErrorCodes.CurrencyMismatch, ErrorMessages.CurrencyMismatch);

        return Amount > other.Amount;
    }

    /// <summary>
    /// 判断金额是否小于另一个金额
    /// </summary>
    /// <param name="other">要比较的金额</param>
    /// <returns>比较结果</returns>
    /// <exception cref="ArgumentNullException">参数为null时抛出</exception>
    /// <exception cref="DomainBusinessException">币种不匹配时抛出业务异常</exception>
    public bool IsLessThan(Money other)
    {
        Guard.NotNull(other);

        if (Currency != other.Currency)
            throw new DomainBusinessException(BusinessErrorCodes.CurrencyMismatch, ErrorMessages.CurrencyMismatch);

        return Amount < other.Amount;
    }

    /// <summary>
    /// 实现IComparable<Money>接口
    /// </summary>
    /// <param name="other">要比较的金额</param>
    /// <returns>比较结果：-1小于，0等于，1大于</returns>
    /// <exception cref="ArgumentNullException">参数为null时抛出</exception>
    /// <exception cref="DomainBusinessException">币种不匹配时抛出业务异常</exception>
    public int CompareTo(Money? other)
    {
        if (other is null)
            return 1;

        if (Currency != other.Currency)
            throw new DomainBusinessException(BusinessErrorCodes.CurrencyMismatch, ErrorMessages.CurrencyMismatch);

        return Amount.CompareTo(other.Amount);
    }
    #endregion

    #region 辅助方法
    /// <summary>
    /// 获取币种的 ISO 4217 代码
    /// </summary>
    public string GetCurrencyCode() => Currency.GetIsoCode();

    /// <summary>
    /// 获取币种符号
    /// </summary>
    public string GetCurrencySymbol() => Currency.GetSymbol();

    /// <summary>
    /// 获取币种的小数位数
    /// </summary>
    public int GetDecimalPlaces() => Currency.GetDecimalPlaces();

    /// <summary>
    /// 元组解构方法
    /// </summary>
    /// <param name="amount">金额数值</param>
    /// <param name="currency">币种</param>
    public void Deconstruct(out decimal amount, out Currency currency)
    {
        amount = Amount;
        currency = Currency;
    }

    /// <summary>
    /// 重写ToString方法，返回格式化的金额字符串
    /// 使用不变文化确保格式一致
    /// </summary>
    /// <returns>格式化的金额字符串 (如: ¥100.00)</returns>
    public override string ToString()
    {
        var symbol = GetCurrencySymbol();
        var decimalPlaces = GetDecimalPlaces();
        return $"{symbol}{Amount.ToString($"F{decimalPlaces}", CultureInfo.InvariantCulture)}";
    }

    /// <summary>
    /// 转换为数据传输对象
    /// 用于API响应序列化
    /// </summary>
    /// <returns>包含金额和币种信息的DTO</returns>
    public object ToDto()
    {
        return new
        {
            Amount,
            CurrencyCode = GetCurrencyCode(),
            CurrencySymbol = GetCurrencySymbol(),
            Formatted = ToString()
        };
    }
    #endregion

    #region 运算符重载
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
    public static bool operator >(Money left, Money right) => left.CompareTo(right) > 0;

    /// <summary>
    /// 小于运算符重载
    /// </summary>
    public static bool operator <(Money left, Money right) => left.CompareTo(right) < 0;

    /// <summary>
    /// 大于等于运算符重载
    /// </summary>
    public static bool operator >=(Money left, Money right) => left.CompareTo(right) >= 0;

    /// <summary>
    /// 小于等于运算符重载
    /// </summary>
    public static bool operator <=(Money left, Money right) => left.CompareTo(right) <= 0;
    #endregion
}
