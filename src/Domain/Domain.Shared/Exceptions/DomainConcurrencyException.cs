using BuildingBlocks.Core.Exceptions;
using BuildingBlocks.Core.ErrorCodes;

namespace Domain.Shared.Exceptions;

/// <summary>
/// 领域并发冲突异常
/// 适用场景: 乐观锁冲突，数据已被其他用户修改
/// 对应错误码: 10006 ConcurrencyConflict
/// </summary>
public class DomainConcurrencyException : BusinessException
{
    /// <summary>
    /// 资源类型名称
    /// </summary>
    public string ResourceType { get; }

    /// <summary>
    /// 资源标识
    /// </summary>
    public object? ResourceId { get; }

    /// <summary>
    /// 初始化领域并发冲突异常
    /// </summary>
    /// <param name="resourceType">资源类型名称</param>
    /// <param name="resourceId">资源标识 (可选)</param>
    public DomainConcurrencyException(string resourceType, object? resourceId = null)
        : base(SystemErrorCodes.ConcurrencyConflict, "Concurrency conflict occurred")
    {
        ResourceType = resourceType;
        ResourceId = resourceId;
    }

    /// <summary>
    /// 初始化带内部异常的领域并发冲突异常
    /// </summary>
    /// <param name="resourceType">资源类型名称</param>
    /// <param name="resourceId">资源标识</param>
    /// <param name="innerException">内部异常</param>
    public DomainConcurrencyException(string resourceType, object? resourceId, Exception innerException)
        : base(SystemErrorCodes.ConcurrencyConflict, "Concurrency conflict occurred", innerException)
    {
        ResourceType = resourceType;
        ResourceId = resourceId;
    }
}
