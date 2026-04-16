using MediatR;

namespace Template.Contracts.CQRS;

/// <summary>
/// 无返回值命令接口
/// </summary>
public interface ICommand : ICommand<Unit>
{
}

/// <summary>
/// 带返回值命令接口
/// </summary>
/// <typeparam name="TResponse">命令返回类型</typeparam>
public interface ICommand<out TResponse> : IRequest<TResponse>
{
}
