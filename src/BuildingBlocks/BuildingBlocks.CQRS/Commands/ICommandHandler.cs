using MediatR;

namespace BuildingBlocks.CQRS.Commands;

/// <summary>
/// 通用无返回值命令处理器接口
/// <para>处理对应无返回值命令请求，执行完成后默认返回Unit空结果</para>
/// <para>所有应用层无返回业务命令处理器统一继承此接口</para>
/// </summary>
/// <typeparam name="TCommand">待处理的命令请求对象</typeparam>
public interface ICommandHandler<in TCommand>
    : IRequestHandler<TCommand, Unit>
    where TCommand : ICommand
{
}

/// <summary>
/// 通用带返回值命令处理器接口
/// <para>接收指定命令请求并完成业务处理，返回对应类型业务结果</para>
/// <para>应用层所有带结果返回的写操作处理器统一基接口</para>
/// </summary>
/// <typeparam name="TCommand">待处理的命令请求对象</typeparam>
/// <typeparam name="TResponse">命令处理完成后返回的结果数据类型</typeparam>
public interface ICommandHandler<in TCommand, TResponse>
    : IRequestHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>
    where TResponse : notnull
{
}
