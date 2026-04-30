namespace BuildingBlocks.Core.Saga;

/// <summary>
/// Saga 补偿步骤接口
/// 核心定位: 强制补偿逻辑实现幂等性与日志记录，保证补偿操作的安全性与可追溯性
/// 强制约束: 所有补偿步骤必须实现此接口
/// </summary>
public interface ISagaCompensationStep
{
    /// <summary>
    /// 步骤名称 (用于日志记录和状态追踪)
    /// </summary>
    string StepName { get; }

    /// <summary>
    /// 执行补偿操作
    /// 核心职责: 撤销原操作的影响，恢复数据到原操作执行前的状态
    /// 强制约束: 
    /// 1. 必须实现幂等性：多次执行补偿操作的结果与执行一次相同
    /// 2. 执行前必须调用 <see cref="HasOriginalOperationExecutedAsync"/> 检查原操作是否已执行
    /// 3. 必须记录详细的执行日志
    /// 4. 补偿失败时必须抛出 <see cref="InvalidOperationException"/>
    /// </summary>
    /// <param name="sagaState">Saga 状态，包含补偿需要的信息</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <exception cref="InvalidOperationException">补偿操作失败时抛出</exception>
    Task CompensateAsync(ISagaState sagaState, CancellationToken cancellationToken = default);

    /// <summary>
    /// 检查原操作是否已执行 (用于幂等性校验)
    /// 核心职责: 避免对未执行的操作进行补偿，防止数据错误
    /// </summary>
    /// <param name="sagaState">Saga 状态</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>原操作是否已执行</returns>
    Task<bool> HasOriginalOperationExecutedAsync(ISagaState sagaState, CancellationToken cancellationToken = default);
}
