using MediatR;

namespace BuildingBlocks.CQRS.Queries;

/// <summary>
/// 通用查询请求接口 (进程内CQRS查询基类)
/// <para>封装MediatR请求抽象，用于应用层无状态只读数据查询操作</para>
/// <para>严格区分命令写操作，查询不产生领域状态变更</para>
/// <para>归属底层通用积木组件，全架构所有查询请求统一基接口</para>
/// </summary>
/// <typeparam name="TResponse">查询请求对应的结果数据类型</typeparam>
public interface IQuery<out TResponse> : IRequest<TResponse>
    where TResponse : notnull
{
}
