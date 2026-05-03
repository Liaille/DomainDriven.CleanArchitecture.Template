using BuildingBlocks.Core.ErrorCodes;
using BuildingBlocks.Core.Exceptions;

namespace Domain.Shared.Exceptions;

/// <summary>
/// 领域层 - 资源未找到异常
/// <para>适用场景：聚合根、实体、值对象未找到（领域层专用）</para>
/// <para>错误码：10200 - ResourceNotFound</para>
/// <para>设计原则：纯数据载体，不包含任何前端展示文案</para>
/// </summary>
public class DomainNotFoundException : BusinessException
{
    /// <summary>
    /// 资源类型（例如：Order、User、Product）
    /// </summary>
    public string ResourceType { get; }

    /// <summary>
    /// 资源唯一标识
    /// </summary>
    public object? ResourceId { get; }

    /// <summary>
    /// 初始化领域资源未找到异常
    /// </summary>
    /// <param name="resourceType">资源类型</param>
    /// <param name="resourceId">资源ID（可选）</param>
    public DomainNotFoundException(string resourceType, object? resourceId = null)
        : base(
            errorCode: SystemErrorCodes.ResourceNotFound,
            technicalMessage: GenerateTechnicalMessage(resourceType, resourceId))
    {
        ResourceType = resourceType;
        ResourceId = resourceId;
    }

    /// <summary>
    /// 初始化领域资源未找到异常（带内部异常）
    /// </summary>
    /// <param name="resourceType">资源类型</param>
    /// <param name="resourceId">资源ID</param>
    /// <param name="innerException">内部异常</param>
    public DomainNotFoundException(string resourceType, object? resourceId, Exception innerException)
        : base(
            errorCode: SystemErrorCodes.ResourceNotFound,
            technicalMessage: GenerateTechnicalMessage(resourceType, resourceId),
            innerException: innerException
          )
    {
        ResourceType = resourceType;
        ResourceId = resourceId;
    }

    /// <summary>
    /// 生成技术日志消息（不返回前端，仅用于日志/调试）
    /// </summary>
    private static string GenerateTechnicalMessage(string resourceType, object? resourceId)
    {
        return resourceId == null
            ? $"Resource not found: {resourceType}"
            : $"Resource not found: {resourceType} (Id: {resourceId})";
    }
}
