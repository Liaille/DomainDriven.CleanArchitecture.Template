using BuildingBlocks.Core.UnitOfWork;
using BuildingBlocks.CQRS.Commands;
using MediatR;

namespace BuildingBlocks.Api.Behaviors;

/// <summary>
/// 事务管道(IPipelineBehavior实现)
/// 仅为Command请求提供事务管理，Query不开启事务
/// </summary>
/// <typeparam name="TRequest">请求类型</typeparam>
/// <typeparam name="TResponse">响应类型</typeparam>
/// <param name="unitOfWork">工作单元实例</param>
public class TransactionBehavior<TRequest, TResponse>(
    IUnitOfWork unitOfWork)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        // 非Command请求，直接跳过事务流程
        if (request is not ICommand<TResponse>) return await next(cancellationToken);

        try
        {
            // 执行业务逻辑
            var response = await next(cancellationToken);

            // 提交事务并保存变更
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return response;
        }
        catch (Exception)
        {
            // 业务异常，回滚所有变更
            await unitOfWork.RollbackAsync(cancellationToken);
            throw;
        }
    }
}
