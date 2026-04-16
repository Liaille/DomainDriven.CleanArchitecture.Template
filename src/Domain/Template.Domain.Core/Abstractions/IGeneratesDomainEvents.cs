using Template.Domain.Core.Events;

namespace Template.Domain.Core.Abstractions;

/// <summary>
/// 领域事件生成接口
/// 表示对象可以产生、存储、清空领域事件
/// </summary>
public interface IGeneratesDomainEvents
{
    /// <summary>
    /// 领域事件记录只读集合 (带事件顺序排列)
    /// </summary>
    IReadOnlyCollection<DomainEventRecord> DomainEvents { get; }

    /// <summary>
    /// 清空领域事件
    /// </summary>
    void ClearDomainEvents();
}
