namespace BuildingBlocks.Core.Entities;

/// <summary>
/// 完整审计聚合根基类 (包含创建、修改、软删除全流程审计信息)
/// </summary>
/// <typeparam name="TKey">聚合根主键类型</typeparam>
public abstract class FullAuditedAggregateRoot<TKey> : AuditedAggregateRoot<TKey>
{
    /// <summary>
    /// 是否已软删除
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

    protected FullAuditedAggregateRoot()
    {
    }

    protected FullAuditedAggregateRoot(TKey id) : base(id)
    {
    }
}
