namespace BuildingBlocks.Core.Entities;

/// <summary>
/// 带审计字段的聚合根基类 (包含创建、修改审计信息)
/// </summary>
/// <typeparam name="TKey">聚合根主键类型</typeparam>
public abstract class AuditedAggregateRoot<TKey> : AggregateRoot<TKey>
{
    /// <summary>
    /// 创建时间 (UTC)
    /// </summary>
    public DateTime CreationTime { get; set; }

    /// <summary>
    /// 创建人 ID
    /// </summary>
    public object? CreatorId { get; set; }

    /// <summary>
    /// 最后修改时间 (UTC)
    /// </summary>
    public DateTime? LastModificationTime { get; set; }

    /// <summary>
    /// 最后修改人 ID
    /// </summary>
    public object? LastModifierId { get; set; }

    protected AuditedAggregateRoot()
    {
        CreationTime = DateTime.UtcNow;
    }

    protected AuditedAggregateRoot(TKey id) : base(id)
    {
        CreationTime = DateTime.UtcNow;
    }
}
