namespace BuildingBlocks.BackgroundJobs.TaskQueue;

/// <summary>
/// 异步任务队列接口
/// <para>用于解耦耗时操作，支持后台异步处理</para>
/// </summary>
/// <typeparam name="T">任务项类型</typeparam>
public interface IAsyncTaskQueue<T>
{
    /// <summary>
    /// 将指定项异步写入队列
    /// <remarks>如果队列已满 (对于有界队列)，则等待直到有空间可用或操作被取消</remarks>
    /// </summary>
    /// <param name="item">要写入队列的项</param>
    /// <param name="cancellationToken">用于取消异步写入操作的取消令牌</param>
    /// <returns>表示异步操作的任务</returns>
    ValueTask QueueAsync(T item, CancellationToken cancellationToken = default);

    /// <summary>
    /// 异步地从队列中读取并返回下一个项
    /// </summary>
    /// <remarks>如果队列为空，返回的任务将在有项可用或操作被取消时完成</remarks>
    /// <param name="cancellationToken">用于取消出队操作的取消令牌</param>
    /// <returns>表示异步操作的值任务，结果包含队列中的下一个项</returns>
    ValueTask<T> DequeueAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// 以异步枚举器的形式从队列中读取所有项，直到队列完成或取消
    /// </summary>
    /// <param name="cancellationToken">用于取消异步读取操作的取消令牌</param>
    /// <returns>从队列中读取的项的异步枚举器，当没有更多项可用时，枚举器完成</returns>
    IAsyncEnumerable<T> ReadAllAsync(CancellationToken cancellationToken = default);
}
