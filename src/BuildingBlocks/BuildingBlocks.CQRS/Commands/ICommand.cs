using MediatR;

namespace BuildingBlocks.CQRS.Commands;

/// <summary>
/// 通用无返回值命令接口 (进程内CQRS命令基类)
/// <para>继承自带返回值命令接口，默认返回MediatR内置Unit类型 (代表无业务返回结果)</para>
/// <para>归属底层通用积木组件，供应用层所有业务命令统一继承实现</para>
/// </summary>
public interface ICommand : ICommand<Unit>
{
}

/// <summary>
/// 通用带返回值命令接口 (进程内CQRS命令基类)
/// <para>封装MediatR请求抽象，用于应用层修改状态类写操作请求</para>
/// <para>归属底层通用积木组件，全架构应用层命令统一基接口</para>
/// </summary>
/// <typeparam name="TResponse">命令执行完成后的响应结果类型</typeparam>
public interface ICommand<out TResponse> : IRequest<TResponse>
{
}
