namespace BuildingBlocks.Core.Performance;

/// <summary>
/// 异步任务队列接口
/// <para>用于异步解耦耗时任务、削峰、背压处理、后台作业处理</para>
/// <para>支持优雅关闭、批量操作、阻塞/非阻塞消费、异步流式消费，满足微服务后台任务核心需求</para>
/// </summary>
/// <typeparam name="T">队列中存储的任务项类型</typeparam>
/// <remarks>实现类必须保证线程安全，支持多生产者、多消费者并发使用</remarks>
public interface IAsyncTaskQueue<T>
{
    /// <summary>
    /// 获取队列中当前等待处理的任务项数量
    /// </summary>
    /// <remarks>用于监控、告警、限流、负载统计等生产环境运维场景</remarks>
    int Count { get; }

    /// <summary>
    /// 获取队列是否已标记为完成状态
    /// <para>已完成的队列将不再接受新的入队操作，但会继续消费剩余任务</para>
    /// </summary>
    bool IsCompleted { get; }

    /// <summary>
    /// 异步将单个任务项加入队列
    /// </summary>
    /// <param name="item">需要入队的任务项</param>
    /// <param name="cancellationToken">取消操作的令牌</param>
    /// <returns>表示异步入队操作的值任务</returns>
    /// <exception cref="System.InvalidOperationException">当队列已标记完成时，调用此方法抛出该异常</exception>
    /// <exception cref="System.OperationCanceledException">操作被取消时抛出</exception>
    ValueTask EnqueueAsync(T item, CancellationToken cancellationToken = default);

    /// <summary>
    /// 异步批量将多个任务项加入队列
    /// <para>批量操作可减少并发竞争，提升高吞吐场景下的性能</para>
    /// </summary>
    /// <param name="items">需要批量入队的任务项集合</param>
    /// <param name="cancellationToken">取消操作的令牌</param>
    /// <returns>表示异步批量入队操作的值任务</returns>
    /// <exception cref="System.InvalidOperationException">当队列已标记完成时，调用此方法抛出该异常</exception>
    /// <exception cref="System.OperationCanceledException">操作被取消时抛出</exception>
    ValueTask EnqueueManyAsync(IEnumerable<T> items, CancellationToken cancellationToken = default);

    /// <summary>
    /// 异步从队列中取出一个任务项 (阻塞读取)
    /// <para>队列为空时会异步等待，直到有任务可用或队列关闭/操作取消</para>
    /// </summary>
    /// <param name="cancellationToken">取消操作的令牌</param>
    /// <returns>获取到的任务项</returns>
    /// <exception cref="System.InvalidOperationException">队列已完成且无剩余任务时抛出</exception>
    /// <exception cref="System.OperationCanceledException">操作被取消时抛出</exception>
    ValueTask<T> DequeueAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// 异步尝试从队列中取出一个任务项 (非阻塞读取)
    /// <para>无论队列是否为空，都会立即返回结果，不会异步等待</para>
    /// </summary>
    /// <param name="item">如果读取成功，返回获取的任务项；否则返回类型默认值</param>
    /// <param name="cancellationToken">取消操作的令牌</param>
    /// <returns>读取成功返回 true，读取失败 (队列为空/已关闭)返回 false</returns>
    /// <exception cref="System.OperationCanceledException">操作被取消时抛出</exception>
    ValueTask<bool> TryDequeueAsync(out T item, CancellationToken cancellationToken = default);

    /// <summary>
    /// 标记队列完成，停止接收新任务
    /// <para>调用后，所有入队方法将抛出异常，已入队的任务会被正常消费完毕</para>
    /// <para>用于服务优雅停机、资源释放、任务流程终止等场景</para>
    /// </summary>
    void Complete();

    /// <summary>
    /// 获取队列的异步流式枚举器，持续消费队列中的所有任务
    /// <para>支持无限循环消费，直到队列完成或取消操作</para>
    /// <para>简化消费者编写逻辑，适合后台长期运行的作业处理器</para>
    /// </summary>
    /// <param name="cancellationToken">取消操作的令牌</param>
    /// <returns>异步可枚举的任务项流</returns>
    /// <remarks>枚举会在队列完成且无剩余任务时自动结束</remarks>
    IAsyncEnumerable<T> ReadAllAsync(CancellationToken cancellationToken = default);
}
