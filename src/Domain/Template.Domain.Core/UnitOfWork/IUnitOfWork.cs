namespace Template.Domain.Core.UnitOfWork;

/// <summary>
/// 工作单元接口
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
    /// 保存变更并提交事务
    /// </summary>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// 回滚事务
    /// </summary>
    Task RollbackAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// 事务提交完成后执行的操作
    /// <para>用于发布领域事件，保证数据一致性</para>
    /// </summary>
    void OnCompleted(Func<Task> handler);
}
