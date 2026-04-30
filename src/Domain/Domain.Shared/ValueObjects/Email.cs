using Domain.Shared.Errors;
using Domain.Shared.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace Domain.Shared.ValueObjects;

/// <summary>
/// 电子邮箱值对象
/// </summary>
public record Email
{
    public string Value { get; }

    private Email(string value) => Value = value;

    /// <summary>
    /// 创建带有基础验证功能的电子邮箱值对象
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <exception cref="DomainBusinessException"></exception>
    public static Email Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainBusinessException(ErrorCode.ValueIsRequired);

        if (!new EmailAddressAttribute().IsValid(value))
            throw new DomainBusinessException(ErrorCode.InvalidFormat);

        return new Email(value);
    }
}
