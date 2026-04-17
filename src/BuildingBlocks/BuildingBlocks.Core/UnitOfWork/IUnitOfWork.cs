namespace BuildingBlocks.Core.UnitOfWork;

/// <summary>
/// 工作单元接口 (用于维护事务一致性，统一提交变更)
/// </summary>
public interface IUnitOfWork : IDisposable
{
    /// <summary>
    /// 是否已提交
    /// </summary>
    bool IsCompleted { get; }

    /// <summary>
    /// 是否已回滚
    /// </summary>
    bool IsRolledback { get; }

    /// <summary>
    /// 提交当前事务，保存所有变更
    /// </summary>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// 回滚事务
    /// </summary>
    Task RollbackAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// 事务提交完成后执行的操作 (用于发布领域事件，保证数据一致性)
    /// </summary>
    void OnCompleted(Func<Task> handler);
}
