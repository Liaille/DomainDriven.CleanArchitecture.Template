using Domain.Shared.ErrorCodes;
using Domain.Shared.Exceptions;
using Domain.Shared.Resources.Errors;

namespace Domain.Shared.ValueObjects;

/// <summary>
/// 百分比值对象
/// 核心职责: 封装百分比数值，强制范围 0-100
/// 设计说明: 避免百分比数值超出范围的常见错误
/// </summary>
public record Percentage
{
    /// <summary>
    /// 百分比数值 (0-100)
    /// </summary>
    public decimal Value { get; }

    private Percentage(decimal value) => Value = value;

    public static Percentage Create(decimal value)
    {
        if (value < 0 || value > 100)
            throw new DomainBusinessException(BusinessErrorCodes.ValueOutOfRange, ErrorMessages.ValueOutOfRange);

        return new Percentage(value);
    }

    /// <summary>
    /// 转换为小数 (如 50% → 0.5)
    /// </summary>
    public decimal ToDecimal() => Value / 100;

    public override string ToString() => $"{Value:F2}%";

    public static implicit operator decimal(Percentage percentage) => percentage.Value;
}
