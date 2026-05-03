using Domain.Shared.ErrorCodes;
using Domain.Shared.Exceptions;

namespace Domain.Shared.ValueObjects;

/// <summary>
/// 地址值对象
/// 核心职责: 封装完整的地址信息，保证地址的完整性与一致性
/// 设计说明: 
/// - 采用通用的地址结构，不绑定特定国家
/// - 支持可选字段，适应不同地区的地址格式
/// - 具体业务可继承扩展更详细的地址结构
/// </summary>
public record Address
{
    /// <summary>
    /// 国家/地区
    /// 建议使用 ISO 3166-1 alpha-2 标准 (如: CN、US、GB)
    /// </summary>
    public string Country { get; }

    /// <summary>
    /// 省/州/郡
    /// </summary>
    public string StateProvince { get; }

    /// <summary>
    /// 城市
    /// </summary>
    public string City { get; }

    /// <summary>
    /// 区/县
    /// </summary>
    public string? District { get; }

    /// <summary>
    /// 街道地址
    /// </summary>
    public string StreetAddress { get; }

    /// <summary>
    /// 详细地址 (门牌号、房间号等)
    /// </summary>
    public string? Detail { get; }

    /// <summary>
    /// 邮政编码 (可选)
    /// </summary>
    public string? PostalCode { get; }

    /// <summary>
    /// 私有构造函数
    /// </summary>
    private Address(
        string country,
        string stateProvince,
        string city,
        string? district,
        string streetAddress,
        string? detail,
        string? postalCode)
    {
        Country = country;
        StateProvince = stateProvince;
        City = city;
        District = district;
        StreetAddress = streetAddress;
        Detail = detail;
        PostalCode = postalCode;
    }

    /// <summary>
    /// 创建地址值对象的工厂方法
    /// </summary>
    /// <param name="country">国家/地区</param>
    /// <param name="stateProvince">省/州/郡</param>
    /// <param name="city">城市</param>
    /// <param name="streetAddress">街道地址</param>
    /// <param name="district">区/县 (可选)</param>
    /// <param name="detail">详细地址 (可选)</param>
    /// <param name="postalCode">邮政编码 (可选)</param>
    /// <returns>验证通过的Address值对象</returns>
    /// <exception cref="DomainBusinessException">验证失败时抛出业务异常</exception>
    public static Address Create(
        string country,
        string stateProvince,
        string city,
        string streetAddress,
        string? district = null,
        string? detail = null,
        string? postalCode = null)
    {
        if (string.IsNullOrWhiteSpace(country))
            throw new DomainBusinessException(BusinessErrorCode.RequiredValueNotEmpty, "国家/地区不能为空");

        if (string.IsNullOrWhiteSpace(stateProvince))
            throw new DomainBusinessException(BusinessErrorCode.RequiredValueNotEmpty, "省/州/郡不能为空");

        if (string.IsNullOrWhiteSpace(city))
            throw new DomainBusinessException(BusinessErrorCode.RequiredValueNotEmpty, "城市不能为空");

        if (string.IsNullOrWhiteSpace(streetAddress))
            throw new DomainBusinessException(BusinessErrorCode.RequiredValueNotEmpty, "街道地址不能为空");

        return new Address(
            country.Trim(),
            stateProvince.Trim(),
            city.Trim(),
            district?.Trim(),
            streetAddress.Trim(),
            detail?.Trim(),
            postalCode?.Trim());
    }

    /// <summary>
    /// 获取单行地址字符串 (不包含国家)
    /// </summary>
    /// <returns>单行地址字符串</returns>
    public string GetAddressLine()
    {
        var parts = new List<string>();

        if (!string.IsNullOrWhiteSpace(StateProvince))
            parts.Add(StateProvince);

        if (!string.IsNullOrWhiteSpace(City))
            parts.Add(City);

        if (!string.IsNullOrWhiteSpace(District))
            parts.Add(District);

        if (!string.IsNullOrWhiteSpace(StreetAddress))
            parts.Add(StreetAddress);

        if (!string.IsNullOrWhiteSpace(Detail))
            parts.Add(Detail);

        return string.Join(" ", parts);
    }

    /// <summary>
    /// 获取完整地址字符串 (包含国家)
    /// </summary>
    /// <returns>完整地址字符串</returns>
    public string GetFullAddress()
    {
        var parts = new List<string>
        {
            Country,
            GetAddressLine()
        };

        if (!string.IsNullOrWhiteSpace(PostalCode))
            parts.Add($"(邮编: {PostalCode})");

        return string.Join(" ", parts);
    }

    /// <summary>
    /// 重写ToString方法，返回完整地址
    /// </summary>
    /// <returns>完整地址字符串</returns>
    public override string ToString() => GetFullAddress();
}