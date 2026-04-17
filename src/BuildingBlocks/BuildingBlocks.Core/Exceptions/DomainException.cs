namespace BuildingBlocks.Core.Exceptions;

/// <summary>
/// 领域异常 (用于业务规则不满足时抛出)
/// </summary>
public class DomainException : Exception
{
    public DomainException(string message) : base(message)
    {
    }

    public DomainException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
