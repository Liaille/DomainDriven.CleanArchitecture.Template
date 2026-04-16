namespace Template.Domain.Core.Entities.Auditing;

/// <summary>
/// 审计聚合根基类
/// 支持审计字段，具备领域事件能力，适用于核心业务根实体
/// </summary>
/// <typeparam name="TKey">实体主键类型</typeparam>
/// <typeparam name="TUserId">操作用户ID类型</typeparam>
[Serializable]
public abstract class AuditedAggregateRoot<TKey, TUserId> : AggregateRoot<TKey>
{
    /// <summary>
    /// 创建时间 (UTC)
    /// </summary>
    public DateTime CreationTime { get; set; }

    /// <summary>
    /// 创建人ID
    /// </summary>
    public TUserId? CreatorId { get; set; }

    /// <summary>
    /// 最后修改时间 (UTC)
    /// </summary>
    public DateTime? LastModificationTime { get; set; }

    /// <summary>
    /// 最后修改人ID
    /// </summary>
    public TUserId? LastModifierId { get; set; }
}

/// <summary>
/// 简化版审计聚合根
/// 用户ID类型默认使用 Guid?
/// </summary>
/// <typeparam name="TKey">实体主键类型</typeparam>
[Serializable]
public abstract class AuditedAggregateRoot<TKey> : AuditedAggregateRoot<TKey, Guid?>
{
}
