using System.Threading.Channels;

namespace BuildingBlocks.BackgroundJobs.TaskQueue;

/// <summary>
/// 基于 Channel 实现的异步任务队列 (高性能、线程安全、支持多生产者多消费者)
/// </summary>
/// <typeparam name="T">任务项类型</typeparam>
/// <param name="capacity">队列容量 (可选，0 表示无界队列)</param>
/// <param name="singleReader">是否单一消费者 (默认为 true，适用于大多数场景)</param>
public class ChannelAsyncTaskQueue<T>(int capacity = 0, bool singleReader = true) : IAsyncTaskQueue<T>
{
    private readonly Channel<T> _channel = capacity > 0
            ? Channel.CreateBounded<T>(new BoundedChannelOptions(capacity)
            {
                SingleReader = singleReader,
                SingleWriter = false,
                FullMode = BoundedChannelFullMode.Wait // 队列满时等待
            })
            : Channel.CreateUnbounded<T>(new UnboundedChannelOptions
            {
                SingleReader = singleReader,
                SingleWriter = false
            });

    /// <summary>
    /// 将指定项异步写入队列
    /// <remarks>如果队列已满 (对于有界队列)，则等待直到有空间可用或操作被取消</remarks>
    /// </summary>
    /// <param name="item">要写入队列的项</param>
    /// <param name="cancellationToken">用于取消异步写入操作的取消令牌</param>
    /// <returns>表示异步操作的任务</returns>
    public ValueTask QueueAsync(T item, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(item, nameof(item));

        return _channel.Writer.WriteAsync(item, cancellationToken);
    }
        

    /// <summary>
    /// 异步地从队列中读取并返回下一个项
    /// </summary>
    /// <remarks>如果队列为空，返回的任务将在有项可用或操作被取消时完成</remarks>
    /// <param name="cancellationToken">用于取消出队操作的取消令牌</param>
    /// <returns>表示异步操作的值任务，结果包含队列中的下一个项</returns>
    public ValueTask<T> DequeueAsync(CancellationToken cancellationToken = default) =>
        _channel.Reader.ReadAsync(cancellationToken);

    /// <summary>
    /// 以异步枚举器的形式从队列中读取所有项，直到队列完成或取消
    /// </summary>
    /// <param name="cancellationToken">用于取消异步读取操作的取消令牌</param>
    /// <returns>从队列中读取的项的异步枚举器，当没有更多项可用时，枚举器完成</returns>
    public IAsyncEnumerable<T> ReadAllAsync(CancellationToken cancellationToken = default) =>
        _channel.Reader.ReadAllAsync(cancellationToken);
}
