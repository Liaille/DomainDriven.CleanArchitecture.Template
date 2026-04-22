namespace BuildingBlocks.Core.Exceptions;

/// <summary>
/// 资源未找到异常
/// </summary>
public class NotFoundException(string message) : AppServiceException(message)
{
}
