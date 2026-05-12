using Domain.Shared.ErrorCodes;
using Domain.Shared.Exceptions;
using Domain.Shared.Resources.Errors;

namespace Domain.Shared.ValueObjects;

/// <summary>
/// 姓名值对象
/// 核心职责: 封装姓名信息，支持不同文化的姓名格式
/// 设计说明: 支持姓/名/中间名拆分，自动处理空格和大小写
/// </summary>
public record FullName
{
    /// <summary>
    /// 姓氏
    /// </summary>
    public string LastName { get; }

    /// <summary>
    /// 名字
    /// </summary>
    public string FirstName { get; }

    /// <summary>
    /// 中间名 (可选)
    /// </summary>
    public string? MiddleName { get; }

    private FullName(string lastName, string firstName, string? middleName)
    {
        LastName = lastName;
        FirstName = firstName;
        MiddleName = middleName;
    }

    public static FullName Create(string lastName, string firstName, string? middleName = null)
    {
        if (string.IsNullOrWhiteSpace(lastName))
            throw new DomainBusinessException(BusinessErrorCodes.RequiredValueNotEmpty, ErrorMessages.RequiredValueNotEmpty);

        if (string.IsNullOrWhiteSpace(firstName))
            throw new DomainBusinessException(BusinessErrorCodes.RequiredValueNotEmpty, ErrorMessages.RequiredValueNotEmpty);

        return new FullName(
            lastName.Trim(),
            firstName.Trim(),
            middleName?.Trim());
    }

    /// <summary>
    /// 获取中文格式姓名 (姓+名)
    /// </summary>
    public string ToChineseFormat() => $"{LastName}{FirstName}";

    /// <summary>
    /// 获取英文格式姓名 (名+姓)
    /// </summary>
    public string ToEnglishFormat() => string.IsNullOrWhiteSpace(MiddleName)
        ? $"{FirstName} {LastName}"
        : $"{FirstName} {MiddleName} {LastName}";

    public override string ToString() => ToChineseFormat();
}
