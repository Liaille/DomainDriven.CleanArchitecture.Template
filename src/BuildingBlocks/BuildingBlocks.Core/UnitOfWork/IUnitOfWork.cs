namespace BuildingBlocks.Core.UnitOfWork;

/// <summary>
/// 工作单元接口 (用于维护事务一致性，统一提交变更)
/// </summary>
public interface IUnitOfWork : IDisposable, IAsyncDisposable
{
    /// <summary>
    /// 提交当前工作单元的所有变更
    /// 然后执行OnCompleted注册的操作
    /// </summary>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>受影响的记录行数</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// 回滚当前工作单元的所有变更
    /// 清空变更跟踪并回滚事务
    /// </summary>
    /// <param name="cancellationToken">取消令牌</param>
    Task RollbackAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// 注册在CommitAsync成功完成后执行的操作
    /// 通常用于发布领域事件、消息队列、日志记录等
    /// </summary>
    /// <param name="handler">成功提交后执行的任务</param>
    void OnCompleted(Func<Task> handler);
}
