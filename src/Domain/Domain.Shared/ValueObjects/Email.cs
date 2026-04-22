using BuildingBlocks.Core.ValueObjects;
using Domain.Shared.Errors;
using Domain.Shared.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace Domain.Shared.ValueObjects;

/// <summary>
/// 邮箱值对象
/// </summary>
public record Email : ValueObject
{
    public string Value { get; } = string.Empty;

    private Email() { }

    private Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new BusinessException(ErrorCode.ValueIsRequired);

        if (!new EmailAddressAttribute().IsValid(value))
            throw new BusinessException(ErrorCode.InvalidFormat);

        Value = value;
    }

    public static Email Create(string value) => new(value);
}
