namespace BuildingBlocks.Core.Events;

/// <summary>
/// 领域事件总线接口 (用于服务内领域事件的发布与处理)
/// </summary>
public interface IDomainEventBus
{
    /// <summary>
    /// 发布单个领域事件
    /// </summary>
    /// <param name="domainEvent">领域事件实例</param>
    /// <param name="cancellationToken">取消令牌</param>
    Task PublishAsync(IDomainEvent domainEvent, CancellationToken cancellationToken = default);

    /// <summary>
    /// 批量发布带顺序的领域事件记录 (保证事件处理顺序)
    /// </summary>
    /// <param name="eventRecords">领域事件记录列表</param>
    /// <param name="cancellationToken">取消令牌</param>
    Task PublishAsync(IReadOnlyList<DomainEventRecord> eventRecords, CancellationToken cancellationToken = default);
}
