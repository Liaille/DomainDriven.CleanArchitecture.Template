using Domain.Shared.ErrorCodes;
using Domain.Shared.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace Domain.Shared.ValueObjects;

/// <summary>
/// 电子邮箱值对象
/// 核心职责: 封装邮箱地址，保证格式有效性
/// 验证规则: 遵循 RFC 5322 简化规范
/// </summary>
public record Email
{
    /// <summary>
    /// 邮箱地址值
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// 私有构造函数
    /// </summary>
    /// <param name="value">经过验证的邮箱地址</param>
    private Email(string value) => Value = value;

    /// <summary>
    /// 创建电子邮箱值对象的工厂方法
    /// 执行基础格式验证
    /// </summary>
    /// <param name="value">待验证的邮箱地址</param>
    /// <returns>验证通过的Email值对象</returns>
    /// <exception cref="DomainBusinessException">验证失败时抛出业务异常</exception>
    public static Email Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainBusinessException(BusinessErrorCodes.RequiredValueNotEmpty, "邮箱地址不能为空");

        if (!new EmailAddressAttribute().IsValid(value))
            throw new DomainBusinessException(BusinessErrorCodes.InvalidDataFormat, "邮箱地址格式不正确");

        return new Email(value.Trim().ToLowerInvariant());
    }

    /// <summary>
    /// 重写ToString方法，返回邮箱地址
    /// </summary>
    /// <returns>邮箱地址字符串</returns>
    public override string ToString() => Value;

    /// <summary>
    /// 隐式转换为string类型
    /// </summary>
    /// <param name="email">Email值对象</param>
    public static implicit operator string(Email email) => email.Value;
}
