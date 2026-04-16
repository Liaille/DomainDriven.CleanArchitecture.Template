namespace Template.Domain.Core.Entities.Auditing;

/// <summary>
/// 完整审计实体基类，包含审计字段与软删除功能
/// 适用于需要记录操作日志并支持软删除的子实体
/// </summary>
/// <typeparam name="TKey">实体主键类型</typeparam>
/// <typeparam name="TUserId">操作用户ID类型</typeparam>
[Serializable]
public abstract class FullAuditedEntity<TKey, TUserId> : AuditedEntity<TKey, TUserId>
{
    /// <summary>
    /// 软删除标记，true 表示已删除
    /// </summary>
    public bool IsDeleted { get; set; }

    /// <summary>
    /// 删除时间 (UTC)
    /// </summary>
    public DateTime? DeletionTime { get; set; }

    /// <summary>
    /// 删除人ID
    /// </summary>
    public TUserId? DeleterId { get; set; }
}

/// <summary>
/// 简化版完整审计实体
/// 用户ID类型默认使用 Guid?
/// </summary>
/// <typeparam name="TKey">实体主键类型</typeparam>
[Serializable]
public abstract class FullAuditedEntity<TKey> : FullAuditedEntity<TKey, Guid?>
{
}
