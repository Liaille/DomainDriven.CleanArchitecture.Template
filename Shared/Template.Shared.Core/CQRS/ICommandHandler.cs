using MediatR;

namespace Template.Shared.Core.CQRS;

/// <summary>
/// 无返回值命令处理器接口
/// </summary>
/// <typeparam name="TCommand">要处理的命令类型</typeparam>
public interface ICommandHandler<in TCommand>
    : IRequestHandler<TCommand, Unit>
    where TCommand : ICommand
{
}

/// <summary>
/// 带返回值命令处理器接口
/// </summary>
/// <typeparam name="TCommand">要处理的命令类型</typeparam>
/// <typeparam name="TResponse">处理器返回类型</typeparam>
public interface ICommandHandler<in TCommand, TResponse>
    : IRequestHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>
    where TResponse : notnull
{
}
