using BuildingBlocks.Core.ValueObjects;
using Domain.Shared.Errors;
using Domain.Shared.Exceptions;

namespace Domain.Shared.ValueObjects;

/// <summary>
/// 电话号码值对象
/// </summary>
public record Phone : ValueObject
{
    public string Number { get; } = string.Empty;

    private Phone() { }

    private Phone(string number)
    {
        if (string.IsNullOrWhiteSpace(number))
            throw new BusinessException(ErrorCode.ValueIsRequired);

        Number = number;
    }

    public static Phone Create(string number) => new(number);
}
