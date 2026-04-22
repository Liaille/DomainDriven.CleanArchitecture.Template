using BuildingBlocks.Core.ValueObjects;
using Domain.Shared.Errors;
using Domain.Shared.Exceptions;

namespace Domain.Shared.ValueObjects;

/// <summary>
/// 电话号码值对象
/// </summary>
public record Phone : ValueObject
{
    public string Number { get; }

    private Phone(string number) => Number = number;

    public static Phone Create(string number)
    {
        if (string.IsNullOrWhiteSpace(number))
            throw new BusinessException(ErrorCode.ValueIsRequired);

        return new Phone(number);
    }
}
