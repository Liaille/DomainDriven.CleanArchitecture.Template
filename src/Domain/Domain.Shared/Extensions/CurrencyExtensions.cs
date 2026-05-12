using Domain.Shared.Enums;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Reflection;

namespace Domain.Shared.Extensions;

/// <summary>
/// 货币枚举(Currency)的扩展方法类
/// 提供获取货币ISO编码、货币符号、小数位数的通用扩展方法
/// 使用静态缓存优化反射性能
/// </summary>
public static class CurrencyExtensions
{
    /// <summary>
    /// 货币特性缓存字典
    /// 键：货币枚举值
    /// 值：元组(ISO代码, 符号, 小数位数)
    /// </summary>
    private static readonly ConcurrentDictionary<Currency, (string IsoCode, string Symbol, int? DecimalPlaces)> _currencyCache = new();

    static CurrencyExtensions()
    {
        // 预加载所有货币特性到缓存
        foreach (var currency in Enum.GetValues<Currency>())
        {
            _ = GetCurrencyInfo(currency);
        }
    }

    /// <summary>
    /// 获取货币对应的ISO 4217标准编码
    /// </summary>
    /// <param name="currency">货币枚举值</param>
    /// <returns>ISO编码字符串</returns>
    public static string GetIsoCode(this Currency currency)
    {
        return GetCurrencyInfo(currency).IsoCode;
    }

    /// <summary>
    /// 获取货币对应的符号
    /// </summary>
    /// <param name="currency">货币枚举值</param>
    /// <returns>货币符号字符串</returns>
    public static string GetSymbol(this Currency currency)
    {
        return GetCurrencyInfo(currency).Symbol;
    }

    /// <summary>
    /// 获取货币金额的保留小数位数
    /// </summary>
    /// <param name="currency">货币枚举值</param>
    /// <returns>小数位数整数，特殊货币返回0</returns>
    public static int GetDecimalPlaces(this Currency currency)
    {
        return GetCurrencyInfo(currency).DecimalPlaces ?? 0;
    }

    /// <summary>
    /// 判断货币是否为特殊货币 (贵金属、SDR等)
    /// </summary>
    /// <param name="currency">货币枚举值</param>
    /// <returns>是否为特殊货币</returns>
    public static bool IsSpecialCurrency(this Currency currency)
    {
        return GetCurrencyInfo(currency).DecimalPlaces is null;
    }

    /// <summary>
    /// 从缓存获取货币信息，不存在则反射加载
    /// </summary>
    private static (string IsoCode, string Symbol, int? DecimalPlaces) GetCurrencyInfo(Currency currency)
    {
        return _currencyCache.GetOrAdd(currency, c =>
        {
            var memberInfo = c.GetType().GetMember(c.ToString())[0];
            var attr = memberInfo.GetCustomAttribute<DescriptionAttribute>();

            if (attr is null || string.IsNullOrWhiteSpace(attr.Description))
            {
                return (c.ToString(), c.ToString(), 2);
            }

            var parts = attr.Description.Split('|');
            var isoCode = parts.Length > 0 ? parts[0].Trim() : c.ToString();
            var symbol = parts.Length > 1 ? parts[1].Trim() : c.ToString();
            int? decimalPlaces = parts.Length > 2 && int.TryParse(parts[2].Trim(), out int dec) ? dec : null;

            return (isoCode, symbol, decimalPlaces);
        });
    }
}
