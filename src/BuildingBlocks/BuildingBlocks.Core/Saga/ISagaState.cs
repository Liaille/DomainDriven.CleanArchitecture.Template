namespace BuildingBlocks.Core.Saga;

/// <summary>
/// Saga 状态抽象接口
/// <para>核心定位: 定义 Saga 状态机的核心字段，保证 Saga 执行的可追踪性与可恢复性</para>
/// <para>设计背景: Saga 执行过程中可能会崩溃，必须持久化状态以便恢复执行</para>
/// <para>强制约束: 所有 Saga 状态实现必须包含这些核心字段</para>
/// </summary>
public interface ISagaState
{
    /// <summary>
    /// Saga 唯一标识 (用于追踪整个 Saga 的执行过程，也是补偿操作的关联标识)
    /// </summary>
    Guid SagaId { get; }

    /// <summary>
    /// Saga 类型名称 (完整类名，用于反序列化时确定具体的 Saga 类型) 
    /// </summary>
    string SagaType { get; }

    /// <summary>
    /// 当前执行步骤 (记录 Saga 执行到了哪一步，用于崩溃后恢复执行)
    /// </summary>
    string CurrentStep { get; set; }

    /// <summary>
    /// 当前 Saga 的整体状态
    /// </summary>
    SagaStatus Status { get; set; }

    /// <summary>
    /// 创建时间 (UTC 用于超时判断和审计) 
    /// </summary>
    DateTime CreatedAt { get; }

    /// <summary>
    /// 最后更新时间 (UTC)
    /// </summary>
    DateTime? LastUpdatedAt { get; set; }

    /// <summary>
    /// 补偿日志 (JSON 格式，记录每个步骤的补偿信息)
    /// 详细说明: 
    /// 记录每个步骤执行后需要的补偿信息，比如：
    /// - 扣减库存后记录扣减的数量
    /// - 扣款后记录扣款的金额
    /// 补偿操作时需要这些信息来恢复数据
    /// </summary>
    string? CompensationLog { get; set; }

    /// <summary>
    /// 错误信息 (仅在Saga失败时填充)
    /// </summary>
    string? ErrorMessage { get; set; }
}
