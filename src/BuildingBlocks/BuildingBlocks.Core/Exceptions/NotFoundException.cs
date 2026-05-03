using BuildingBlocks.Core.ErrorCodes;

namespace BuildingBlocks.Core.Exceptions;

/// <summary>
/// 资源未找到异常
/// <para>使用场景: 查询的数据、记录、文件、资源不存在</para>
/// <para>错误码: 10200 ResourceNotFound</para>
/// </summary>
public class NotFoundException : BusinessException
{
    /// <summary>
    /// 资源类型（如：Order、User、Product）
    /// </summary>
    public string? ResourceType { get; }

    /// <summary>
    /// 资源ID
    /// </summary>
    public object? ResourceId { get; }

    /// <summary>
    /// 初始化资源不存在异常
    /// </summary>
    /// <param name="technicalMessage">技术描述</param>
    public NotFoundException(string technicalMessage)
        : base(SystemErrorCodes.ResourceNotFound, technicalMessage)
    {
    }

    /// <summary>
    /// 初始化资源不存在异常（带资源信息）
    /// </summary>
    /// <param name="resourceType">资源类型</param>
    /// <param name="resourceId">资源ID</param>
    /// <param name="technicalMessage">技术描述</param>
    public NotFoundException(string resourceType, object? resourceId, string technicalMessage)
        : base(SystemErrorCodes.ResourceNotFound, technicalMessage)
    {
        ResourceType = resourceType;
        ResourceId = resourceId;
    }

    /// <summary>
    /// 初始化资源不存在异常（带内部异常）
    /// </summary>
    /// <param name="technicalMessage">技术描述</param>
    /// <param name="innerException">内部异常</param>
    public NotFoundException(string technicalMessage, Exception innerException)
        : base(SystemErrorCodes.ResourceNotFound, technicalMessage, innerException)
    {
    }
}
