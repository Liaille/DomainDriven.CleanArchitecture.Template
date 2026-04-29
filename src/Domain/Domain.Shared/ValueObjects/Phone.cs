using Domain.Shared.Errors;
using Domain.Shared.Exceptions;

namespace Domain.Shared.ValueObjects;

/// <summary>
/// 电话号码值对象
/// </summary>
public record Phone
{
    /// <summary>
    /// 电话号码字符串值
    /// </summary>
    public string Number { get; }

    /// <summary>
    /// 私有构造函数，仅通过Create工厂方法创建实例
    /// </summary>
    /// <param name="number">经过验证的电话号码</param>
    private Phone(string number) => Number = number;

    /// <summary>
    /// 创建电话号码值对象的工厂方法
    /// 执行完整的业务验证逻辑
    /// </summary>
    /// <param name="number">待验证的电话号码字符串</param>
    /// <returns>验证通过的Phone值对象</returns>
    /// <exception cref="BusinessException">验证失败时抛出业务异常</exception>
    public static Phone Create(string number)
    {
        if (string.IsNullOrWhiteSpace(number))
            throw new BusinessException(ErrorCode.ValueIsRequired);

        return new Phone(number);
    }

    /// <summary>
    /// 重写ToString方法，返回电话号码字符串
    /// </summary>
    /// <returns>电话号码字符串</returns>
    public override string ToString() => Number;

    /// <summary>
    /// 隐式转换为string类型
    /// </summary>
    /// <param name="phone">Phone值对象</param>
    public static implicit operator string(Phone phone) => phone.Number;
}
