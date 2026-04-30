namespace BuildingBlocks.Core.Saga;

/// <summary>
/// Saga 状态枚举
/// <para>核心定位: 定义 Saga 状态机的所有可能状态，保证 Saga 执行的可追踪性</para>
/// <list type="table">
/// 状态流转规则
/// <item>Pending → Running → Completed</item>
/// <item>Pending → Running → Failed → Compensating → Compensated</item>
/// <item>Pending → Running → Failed → Compensating → CompensationFailed (需人工干预)</item>
/// </list>
/// </summary>
public enum SagaStatus
{
    /// <summary>
    /// 待执行
    /// <para>Saga 已创建但尚未开始执行</para>
    /// </summary>
    Pending = 0,

    /// <summary>
    /// 执行中
    /// <para>Saga 正在执行正向操作</para>
    /// </summary>
    Running = 1,

    /// <summary>
    /// 执行成功
    /// <para>所有正向操作已成功完成，Saga 结束</para>
    /// </summary>
    Completed = 2,

    /// <summary>
    /// 执行失败，待补偿
    /// <para>某个正向操作失败，需要执行补偿操作</para>
    /// </summary>
    Failed = 3,

    /// <summary>
    /// 补偿中
    /// <para>正在执行补偿操作</para>
    /// </summary>
    Compensating = 4,

    /// <summary>
    /// 补偿成功
    /// <para>所有补偿操作已成功完成，Saga 结束</para>
    /// </summary>
    Compensated = 5,

    /// <summary>
    /// 补偿失败，需人工干预
    /// <para>补偿操作也失败了，需要人工介入处理</para>
    /// </summary>
    CompensationFailed = 6
}