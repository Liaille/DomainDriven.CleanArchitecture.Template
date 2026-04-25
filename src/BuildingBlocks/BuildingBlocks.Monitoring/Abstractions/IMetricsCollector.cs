namespace BuildingBlocks.Monitoring.Abstractions;

/// <summary>
/// 指标采集抽象接口
/// <para>用于记录业务指标、系统指标、依赖服务指标</para>
/// </summary>
public interface IMetricsCollector
{
    /// <summary>
    /// 增加计数器指标
    /// </summary>
    /// <param name="name">指标名称</param>
    /// <param name="value">增加的值 (默认 1)</param>
    /// <param name="labels">标签字典 (可选)</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>表示异步操作的任务</returns>
    Task IncrementCounterAsync(string name, double value = 1, Dictionary<string, string>? labels = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// 记录直方图指标 (用于分布统计，如延迟、大小)
    /// </summary>
    /// <param name="name">指标名称</param>
    /// <param name="value">记录的值</param>
    /// <param name="labels">标签字典 (可选)</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>表示异步操作的任务</returns>
    Task RecordHistogramAsync(string name, double value, Dictionary<string, string>? labels = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// 设置仪表盘指标 (用于瞬时值，如内存占用、连接数)
    /// </summary>
    /// <param name="name">指标名称</param>
    /// <param name="value">设置的值</param>
    /// <param name="labelsj">标签字典 (可选)</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>表示异步操作的任务</returns>
    Task SetGaugeAsync(string name, double value, Dictionary<string, string>? labelsj = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// 记录请求延迟 (便捷方法)
    /// </summary>
    /// <param name="endpoint">请求端点</param>
    /// <param name="method">HTTP 方法</param>
    /// <param name="statusCode">状态码</param>
    /// <param name="durationMs">延迟毫秒数</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>表示异步操作的任务</returns>
    Task RecordRequestDurationAsync(string endpoint, string method, int statusCode, double durationMs, CancellationToken cancellationToken = default);
}
