using BuildingBlocks.Core.Exceptions;

namespace Domain.Shared.Exceptions;

/// <summary>
/// 领域验证异常
/// </summary>
/// <param name="message">异常消息</param>
public class DomainValidationException(string message) : DomainException(message)
{
}
