using MediatR;

namespace BuildingBlocks.CQRS.Queries;

/// <summary>
/// 通用查询请求处理器接口
/// <para>接收指定只读查询请求，完成数据查询并返回结果数据</para>
/// <para>所有应用层数据查询业务处理器统一继承此接口</para>
/// </summary>
/// <typeparam name="TQuery">待处理的查询请求对象</typeparam>
/// <typeparam name="TResponse">查询完成后返回的结果数据类型</typeparam>
public interface IQueryHandler<in TQuery, TResponse>
    : IRequestHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>
    where TResponse : notnull
{
}
