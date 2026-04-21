using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using System.Data.Common;

namespace BuildingBlocks.Persistence.Interceptors;

/// <summary>
/// EF Core 慢查询拦截器 (记录执行超时的SQL语句)
/// </summary>
/// <param name="logger">日志实例</param>
/// <param name="slowQueryThresholdMs">慢查询阈值 (单位:毫秒,默认500ms)</param>
public class SlowQueryInterceptor(ILogger<SlowQueryInterceptor> logger, int slowQueryThresholdMs = 500) : DbCommandInterceptor
{
    /// <summary>
    /// 同步执行查询后拦截
    /// </summary>
    public override DbDataReader ReaderExecuted(DbCommand command, CommandExecutedEventData eventData, DbDataReader result)
    {
        CheckSlowQuery(command, eventData.Duration);
        return base.ReaderExecuted(command, eventData, result);
    }

    /// <summary>
    /// 异步执行查询后拦截
    /// </summary>
    public override ValueTask<DbDataReader> ReaderExecutedAsync(DbCommand command, CommandExecutedEventData eventData, DbDataReader result, CancellationToken cancellationToken = default)
    {
        CheckSlowQuery(command, eventData.Duration);
        return base.ReaderExecutedAsync(command, eventData, result, cancellationToken);
    }

    /// <summary>
    /// 检查是否为慢查询并记录日志
    /// </summary>
    /// <param name="command">数据库命令</param>
    /// <param name="duration">执行耗时</param>
    private void CheckSlowQuery(DbCommand command, TimeSpan duration)
    {
        var elapsedMs = duration.TotalMilliseconds;
        if (elapsedMs >= slowQueryThresholdMs)
        {
            logger.LogWarning("Slow query detected (Execution Time: {ElapsedMs}ms, Threshold: {Threshold}ms)\nSQL: {CommandText}",
                elapsedMs,
                slowQueryThresholdMs,
                command.CommandText);
        }
    }
}
