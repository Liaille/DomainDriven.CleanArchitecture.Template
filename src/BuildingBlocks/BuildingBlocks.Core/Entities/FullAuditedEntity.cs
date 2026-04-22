namespace BuildingBlocks.Core.Entities;

/// <summary>
/// 完整审计实体 (包含软删除，用于聚合根内的子实体)
/// </summary>
/// <typeparam name="TKey">主键类型</typeparam>
public abstract class FullAuditedEntity<TKey> : AuditedEntity<TKey>, IFullAuditableEntity
{
    /// <summary>
    /// 是否删除
    /// </summary>
    public bool IsDeleted { get; set; }

    /// <summary>
    /// 删除人 ID
    /// </summary>
    public object? DeleterId { get; set; }

    /// <summary>
    /// 删除时间 (UTC)
    /// </summary>
    public DateTime? DeletionTime { get; set; }
}
