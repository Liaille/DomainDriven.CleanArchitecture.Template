namespace BuildingBlocks.Core.Entities;

/// <summary>
/// 带审计字段的实体基类 (用于聚合根内的子实体)
/// </summary>
/// <typeparam name="TKey">主键类型</typeparam>
public abstract class AuditedEntity<TKey> : Entity<TKey>, IAuditableEntity
{
    /// <summary>
    /// 创建时间 (由基础设施层拦截器统一赋值)
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

    protected AuditedEntity()
    {
    }
}
