namespace BuildingBlocks.Core.Exceptions;

/// <summary>
/// 应用服务异常
/// </summary>
public class ApplicationException : Exception
{
    public ApplicationException(string message) : base(message)
    {
    }

    public ApplicationException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
