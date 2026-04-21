using BuildingBlocks.Core.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace BuildingBlocks.Persistence.UnitOfWork;

/// <summary>
/// EF Core 工作单元实现 (维护事务一致性，统一提交变更)
/// </summary>
/// <typeparam name="TDbContext">数据库上下文类型</typeparam>
/// <param name="dbContext">数据库上下文实例</param>
public class EfCoreUnitOfWork<TDbContext>(TDbContext dbContext) : IUnitOfWork
    where TDbContext : DbContext
{
    private readonly List<Func<Task>> _completedHandlers = [];

    /// <summary>
    /// 提交事务，保存所有变更到数据库
    /// </summary>
    /// <param name="cancellationToken">取消令牌</param>
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var result = await dbContext.SaveChangesAsync(cancellationToken);
        
        // 执行事务完成后的回调
        foreach (var handler in _completedHandlers)
        {
            await handler();
        }

        return result;
    }

    /// <summary>
    /// 回滚当前事务
    /// </summary>
    /// <param name="cancellationToken">取消令牌</param>
    public Task RollbackAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// 注册事务提交完成后的回调操作
    /// </summary>
    public void OnCompleted(Func<Task> handler)
    {
        _completedHandlers.Add(handler);
    }

    /// <summary>
    /// 释放资源
    /// </summary>
    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// 异步释放资源
    /// </summary>
    /// <returns></returns>
    public ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        return ValueTask.CompletedTask;
    }
}
