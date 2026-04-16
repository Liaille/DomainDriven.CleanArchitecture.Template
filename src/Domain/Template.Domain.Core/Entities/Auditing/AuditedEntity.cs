namespace Template.Domain.Core.Entities.Auditing;

/// <summary>
/// 审计实体基类 (支持自定义【实体ID】和【用户ID】类型)
/// <para>包含：创建时间、创建人、最后修改时间、最后修改人</para>
/// </summary>
/// <typeparam name="TKey">实体主键类型 (long/Guid/string/int)</typeparam>
/// <typeparam name="TUserId">操作用户ID类型 (创建/修改人ID)</typeparam>
[Serializable]
public abstract class AuditedEntity<TKey, TUserId> : Entity<TKey>
{
    /// <summary>
    /// 创建时间（UTC）
    /// </summary>
    public DateTime CreationTime { get; set; }

    /// <summary>
    /// 创建人ID
    /// </summary>
    public TUserId? CreatorId { get; set; }

    /// <summary>
    /// 最后修改时间（UTC）
    /// </summary>
    public DateTime? LastModificationTime { get; set; }

    /// <summary>
    /// 最后修改人ID
    /// </summary>
    public TUserId? LastModifierId { get; set; }
}

/// <summary>
/// 简化版审计实体基类
/// <para>实体ID：自定义</para>
/// <para>用户ID：默认使用 Guid?</para>
/// </summary>
[Serializable]
public abstract class AuditedEntity<TKey> : AuditedEntity<TKey, Guid?>
{
}
