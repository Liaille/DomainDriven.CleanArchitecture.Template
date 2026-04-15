using MediatR;

namespace Template.Shared.Core.CQRS;

/// <summary>
/// 查询接口
/// </summary>
/// <typeparam name="TResponse">查询返回类型</typeparam>
public interface IQuery<out TResponse> : IRequest<TResponse>
    where TResponse : notnull
{
}
