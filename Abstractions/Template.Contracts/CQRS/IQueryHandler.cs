using MediatR;

namespace Template.Contracts.CQRS;

/// <summary>
/// 查询处理器接口
/// </summary>
/// <typeparam name="TQuery">要处理的查询类型</typeparam>
/// <typeparam name="TResponse">查询返回类型</typeparam>
public interface IQueryHandler<in TQuery, TResponse>
    : IRequestHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>
    where TResponse : notnull
{
}
