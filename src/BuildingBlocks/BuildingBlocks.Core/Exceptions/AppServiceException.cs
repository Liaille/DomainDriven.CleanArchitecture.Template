namespace BuildingBlocks.Core.Exceptions;

/// <summary>
/// 应用服务层异常 (用于应用层业务逻辑错误)
/// </summary>
public class AppServiceException : Exception
{
    public AppServiceException(string message) : base(message)
    {
    }

    public AppServiceException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
