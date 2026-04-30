using BuildingBlocks.Core.Entities;
using BuildingBlocks.Core.Repositories;
using System.Data;

namespace BuildingBlocks.Core.UnitOfWork;

/// <summary>
/// 工作单元抽象接口，统一管理数据库事务生命周期、数据变更、事务后置操作
/// </summary>
/// <remarks>
/// 核心职责: 保证跨仓储数据操作原子性、事务成功后统一执行后置业务、严格遵循整洁架构依赖原则
/// </remarks>
public interface IUnitOfWork : IAsyncDisposable, IDisposable
{
    /// <summary>
    /// 判断当前上下文是否存在已开启的活跃数据库事务
    /// </summary>
    bool HasActiveTransaction { get; }

    /// <summary>
    /// 开启全新数据库事务，支持自定义事务隔离级别
    /// </summary>
    /// <remarks>
    /// 若当前上下文已存在活跃事务，则直接返回现有事务，不会重复创建底层数据库事务
    /// </remarks>
    /// <param name="cancellationToken">异步操作取消令牌</param>
    /// <param name="isolationLevel">数据库事务隔离级别，为空则使用数据库默认级别</param>
    Task BeginAsync(IsolationLevel? isolationLevel = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// 提交当前所有数据变更与活跃事务，事务完全提交成功后执行全部注册的后置操作
    /// </summary>
    /// <remarks>
    /// 执行流程: 持久化数据变更 -> 提交数据库事务 -> 串行执行所有OnCompleted注册任务
    /// <para>异常处理: 事务提交过程发生异常时，自动回滚全部数据变更，并清空所有后置操作任务</para>
    /// </remarks>
    /// <param name="cancellationToken">异步操作取消令牌</param>
    Task CommitAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// 回滚当前所有未提交的数据变更与活跃事务
    /// </summary>
    /// <remarks>
    /// 事务回滚完成后，自动清空全部已注册的事务后置操作任务
    /// </remarks>
    /// <param name="cancellationToken">异步操作取消令牌</param>
    Task RollbackAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// 持久化当前上下文所有实体变更到数据库
    /// </summary>
    /// <remarks>
    /// 特性: 不开启、不提交任何数据库事务，适用于无事务场景数据保存、事务内部中间数据持久化场景
    /// <para>自动触发逻辑: 审计字段填充、软删除状态转换、多租户字段赋值</para>
    /// </remarks>
    /// <param name="cancellationToken">异步操作取消令牌</param>
    /// <returns>数据库受影响的数据行数</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// 注册事务**完全提交成功**后才会执行的异步后置操作
    /// </summary>
    /// <remarks>
    /// 执行规则: 仅数据库事务全部提交成功后执行；事务回滚则全部任务自动丢弃
    /// <para>适用场景: 领域事件发布、消息队列发送、审计日志记录、业务通知等事务后置解耦操作</para>
    /// <para>执行顺序: 内部按照任务注册顺序，依次串行执行多个注册任务</para>
    /// </remarks>
    /// <param name="handler">事务成功后执行的异步任务委托</param>
    void OnCompleted(Func<Task> handler);

    /// <summary>
    /// 获取指定聚合根的只读仓储实例
    /// </summary>
    /// <remarks>
    /// 设计用途: CQRS查询端专用，仅提供数据查询能力，无任何新增/修改/删除写入操作接口
    /// <para>上下文共享: 所有从当前工作单元获取的仓储，共享同一个数据库上下文与事务环境</para>
    /// </remarks>
    /// <typeparam name="TEntity">业务聚合根实体类型，必须继承聚合根基类</typeparam>
    /// <typeparam name="TKey">聚合根主键数据类型</typeparam>
    /// <returns>对应实体的只读仓储实例</returns>
    IReadOnlyRepository<TEntity, TKey> GetReadOnlyRepository<TEntity, TKey>()
        where TEntity : class, IAggregateRoot<TKey>;

    /// <summary>
    /// 获取指定聚合根的基础写入仓储实例
    /// </summary>
    /// <remarks>
    /// 设计用途: CQRS命令端专用，仅提供基础增删改写入能力
    /// <para>能力约束: 无批量操作、原生SQL等高级数据操作能力，适配绝大多数常规业务变更场景</para>
    /// <para>上下文共享: 所有从当前工作单元获取的仓储，共享同一个数据库上下文与事务环境</para>
    /// </remarks>
    /// <typeparam name="TEntity">业务聚合根实体类型，必须继承聚合根基类</typeparam>
    /// <typeparam name="TKey">聚合根主键数据类型</typeparam>
    /// <returns>对应实体的基础写入仓储实例</returns>
    IBasicRepository<TEntity, TKey> GetBasicRepository<TEntity, TKey>()
        where TEntity : class, IAggregateRoot<TKey>;

    /// <summary>
    /// 获取指定聚合根的完整功能仓储实例
    /// </summary>
    /// <remarks>
    /// 设计用途: 兼容传统CRUD遗留业务场景，集成查询、基础写入、批量操作、高级查询全部能力
    /// <para>使用约束: 非必要不优先使用，防止业务层滥用高级操作破坏架构分层约束</para>
    /// <para>上下文共享: 所有从当前工作单元获取的仓储，共享同一个数据库上下文与事务环境</para>
    /// </remarks>
    /// <typeparam name="TEntity">业务聚合根实体类型，必须继承聚合根基类</typeparam>
    /// <typeparam name="TKey">聚合根主键数据类型</typeparam>
    /// <returns>对应实体的全功能仓储实例</returns>
    IRepository<TEntity, TKey> GetRepository<TEntity, TKey>()
        where TEntity : class, IAggregateRoot<TKey>;
}
