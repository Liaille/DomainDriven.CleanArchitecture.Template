namespace BuildingBlocks.Core.Entities;

/// <summary>
/// 具备审计字段的实体接口，包含创建与修改信息
/// </summary>
public interface IAuditableEntity
{
    /// <summary>
    /// 数据创建时间，UTC格式
    /// </summary>
    DateTime CreationTime { get; set; }

    /// <summary>
    /// 数据创建人ID
    /// </summary>
    object? CreatorId { get; set; }

    /// <summary>
    /// 最后一次修改时间，UTC格式
    /// </summary>
    DateTime? LastModificationTime { get; set; }

    /// <summary>
    /// 最后一次修改人ID
    /// </summary>
    object? LastModifierId { get; set; }
}
