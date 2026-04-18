namespace BuildingBlocks.Core.Events;

/// <summary>
/// 领域事件生成接口
/// 表示对象可以产生、存储、清空领域事件 (用于聚合根)
/// </summary>
public interface IGeneratesDomainEvents
{
    /// <summary>
    /// 纯净领域事件只读列表
    /// </summary>
    IReadOnlyList<IDomainEvent> DomainEvents { get; }

    /// <summary>
    /// 获取带顺序的事件记录只读列表 (供基础设施层分发时排序)
    /// </summary>
    IReadOnlyList<DomainEventRecord> GetDomainEventRecords();

    /// <summary>
    /// 清空所有领域事件
    /// </summary>
    void ClearDomainEvents();
}
