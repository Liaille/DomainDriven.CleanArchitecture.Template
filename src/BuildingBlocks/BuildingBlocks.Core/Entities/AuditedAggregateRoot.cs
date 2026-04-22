namespace BuildingBlocks.Core.Entities;

/// <summary>
/// 带审计字段的聚合根基类 (包含创建、修改审计信息)
/// </summary>
/// <typeparam name="TKey">聚合根主键类型</typeparam>
public abstract class AuditedAggregateRoot<TKey> : AggregateRoot<TKey>, IAuditableEntity
{
    /// <summary>
    /// 创建时间 (由基础设施层拦截器统一赋值)
    /// </summary>
    public DateTime CreationTime { get; set; }

    /// <summary>
    /// 创建人ID
    /// </summary>
    public object? CreatorId { get; set; }

    /// <summary>
    /// 最后修改时间
    /// </summary>
    public DateTime? LastModificationTime { get; set; }

    /// <summary>
    /// 最后修改人ID
    /// </summary>
    public object? LastModifierId { get; set; }

    protected AuditedAggregateRoot()
    {
    }

    protected AuditedAggregateRoot(TKey id) : base(id)
    {
    }
}
