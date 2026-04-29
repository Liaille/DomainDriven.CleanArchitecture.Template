using BuildingBlocks.Core.Entities;
using BuildingBlocks.Core.Repositories;
using BuildingBlocks.Core.SpecificationPattern;
using BuildingBlocks.Core.UnitOfWork;
using BuildingBlocks.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace BuildingBlocks.Persistence.UnitOfWork;

/// <summary>
/// EF Core 工作单元实现 (统一事务管理、仓储获取、事务后置操作)
/// </summary>
/// <typeparam name="TDbContext">业务 DbContext 类型</typeparam>
public class EfCoreUnitOfWork<TDbContext>(
    TDbContext dbContext,
    ISpecificationEvaluator specificationEvaluator)
    : IUnitOfWork
    where TDbContext : DbContext
{
    private readonly TDbContext _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    private readonly ISpecificationEvaluator _specificationEvaluator = specificationEvaluator ?? throw new ArgumentNullException(nameof(specificationEvaluator));
    private readonly Dictionary<Type, object> _repositoryCache = [];
    private readonly List<Func<Task>> _completedHandlers = [];
    private IDbContextTransaction? _currentTransaction;
    private bool _disposed;

    /// <summary>
    /// 判断当前是否存在活跃事务
    /// </summary>
    public bool HasActiveTransaction => _currentTransaction is not null;

    /// <summary>
    /// 开启数据库事务
    /// </summary>
    /// <param name="isolationLevel">事务隔离级别 (可选)</param>
    /// <param name="cancellationToken">取消令牌</param>
    public async Task BeginAsync(IsolationLevel? isolationLevel = null, CancellationToken cancellationToken = default)
    {
        if (_currentTransaction is not null) return;

        _currentTransaction = isolationLevel.HasValue
            ? await _dbContext.Database.BeginTransactionAsync(isolationLevel.Value, cancellationToken)
            : await _dbContext.Database.BeginTransactionAsync(cancellationToken);
    }

    /// <summary>
    /// 提交事务并保存所有变更
    /// </summary>
    /// <param name="cancellationToken">取消令牌</param>
    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        if (_currentTransaction is null)
            throw new InvalidOperationException("Cannot perform commit operation: no active transaction exists.");

        try
        {
            // 先保存所有实体变更
            await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            // 提交数据库事务
            await _currentTransaction.CommitAsync(cancellationToken).ConfigureAwait(false);
            // 执行所有注册的事务成功后置操作
            foreach (var handler in _completedHandlers)
            {
                await handler().ConfigureAwait(false);
            }
        }
        catch
        {
            // 异常时自动回滚
            await RollbackAsync(cancellationToken).ConfigureAwait(false);
            throw;
        }
        finally
        {
            // 清理资源
            if (_currentTransaction is not null)
            {
                await _currentTransaction.DisposeAsync().ConfigureAwait(false);
                _currentTransaction = null;
            }
            _completedHandlers.Clear();
        }
    }

    /// <summary>
    /// 回滚当前事务
    /// </summary>
    /// <param name="cancellationToken">取消令牌</param>
    public async Task RollbackAsync(CancellationToken cancellationToken = default)
    {
        if (_currentTransaction is not null)
        {
            await _currentTransaction.RollbackAsync(cancellationToken).ConfigureAwait(false);
            await _currentTransaction.DisposeAsync().ConfigureAwait(false);
            _currentTransaction = null;
        }
        _completedHandlers.Clear();
    }

    /// <summary>
    /// 保存实体变更到数据库 (不开启事务)
    /// </summary>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>受影响的行数</returns>
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// 注册事务成功后执行的后置操作
    /// </summary>
    /// <param name="handler">异步操作委托</param>
    public void OnCompleted(Func<Task> handler)
    {
        _completedHandlers.Add(handler);
    }

    /// <summary>
    /// 获取只读仓储实例
    /// </summary>
    /// <typeparam name="TEntity">聚合根类型</typeparam>
    /// <typeparam name="TKey">主键类型</typeparam>
    public IReadOnlyRepository<TEntity, TKey> GetReadOnlyRepository<TEntity, TKey>()
        where TEntity : class, IAggregateRoot<TKey>
    {
        return GetOrCreateRepository<TEntity, TKey, EfCoreReadOnlyRepository<TEntity, TKey, TDbContext>>();
    }

    /// <summary>
    /// 获取基础写仓储实例
    /// </summary>
    /// <typeparam name="TEntity">聚合根类型</typeparam>
    /// <typeparam name="TKey">主键类型</typeparam>
    public IBasicRepository<TEntity, TKey> GetBasicRepository<TEntity, TKey>()
        where TEntity : class, IAggregateRoot<TKey>
    {
        return GetOrCreateRepository<TEntity, TKey, EfCoreBasicRepository<TEntity, TKey, TDbContext>>();
    }

    /// <summary>
    /// 获取完整仓储实例
    /// </summary>
    /// <typeparam name="TEntity">聚合根类型</typeparam>
    /// <typeparam name="TKey">主键类型</typeparam>
    public IRepository<TEntity, TKey> GetRepository<TEntity, TKey>()
        where TEntity : class, IAggregateRoot<TKey>
    {
        return GetOrCreateRepository<TEntity, TKey, EfCoreRepository<TEntity, TKey, TDbContext>>();
    }

    /// <summary>
    /// 内部仓储缓存创建方法 (避免重复实例化)
    /// </summary>
    private TRepository GetOrCreateRepository<TEntity, TKey, TRepository>()
        where TEntity : class, IAggregateRoot<TKey>
        where TRepository : class
    {
        var entityType = typeof(TEntity);
        if (_repositoryCache.TryGetValue(entityType, out var cachedRepository))
            return (TRepository)cachedRepository;

        // 实例化仓储
        var repository = Activator.CreateInstance(typeof(TRepository), _dbContext, _specificationEvaluator)
            ?? throw new InvalidOperationException($"Could not create repository instance: {typeof(TRepository).FullName}");

        _repositoryCache[entityType] = repository;
        return (TRepository)repository;
    }

    #region 资源释放
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public async ValueTask DisposeAsync()
    {
        if (_currentTransaction is not null)
            await _currentTransaction.DisposeAsync().ConfigureAwait(false);

        await _dbContext.DisposeAsync().ConfigureAwait(false);
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed) return;
        if (disposing)
        {
            _currentTransaction?.Dispose();
            _dbContext.Dispose();
        }
        _disposed = true;
    }
    #endregion
}