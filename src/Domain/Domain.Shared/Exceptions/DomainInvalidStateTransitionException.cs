using Domain.Shared.ErrorCodes;

namespace Domain.Shared.Exceptions;

/// <summary>
/// 领域状态转换无效异常
/// 适用场景: 实体状态流转不符合业务规则 (如订单从已支付不能回到待支付)
/// 对应错误码: 20100 InvalidStateTransition
/// </summary>
public class DomainInvalidStateTransitionException : DomainBusinessException
{
    /// <summary>
    /// 当前状态
    /// </summary>
    public string CurrentState { get; }

    /// <summary>
    /// 目标状态
    /// </summary>
    public string TargetState { get; }

    /// <summary>
    /// 初始化领域状态转换无效异常
    /// </summary>
    /// <param name="currentState">当前状态</param>
    /// <param name="targetState">目标状态</param>
    public DomainInvalidStateTransitionException(string currentState, string targetState)
        : base(BusinessErrorCode.InvalidStateTransition, "Invalid state transition")
    {
        CurrentState = currentState;
        TargetState = targetState;
    }

    /// <summary>
    /// 初始化带自定义消息的领域状态转换无效异常
    /// </summary>
    /// <param name="currentState">当前状态</param>
    /// <param name="targetState">目标状态</param>
    /// <param name="message">自定义异常消息</param>
    public DomainInvalidStateTransitionException(string currentState, string targetState, string message)
        : base(BusinessErrorCode.InvalidStateTransition, message)
    {
        CurrentState = currentState;
        TargetState = targetState;
    }
}