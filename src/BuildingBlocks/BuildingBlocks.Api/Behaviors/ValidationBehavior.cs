using FluentValidation;
using MediatR;

namespace BuildingBlocks.Api.Behaviors;

/// <summary>
/// 参数验证管道(IPipelineBehavior实现)
/// 自动验证所有Command/Query请求参数
/// </summary>
/// <typeparam name="TRequest">请求类型</typeparam>
/// <typeparam name="TResponse">响应类型</typeparam>
public class ValidationBehavior<TRequest, TResponse>(
    IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        // 无验证器直接跳过验证流程
        if (!validators.Any())
            return await next(cancellationToken);

        // 创建验证上下文
        var validationContext = new ValidationContext<TRequest>(request);

        // 并行执行所有验证规则
        var validationResults = await Task.WhenAll(
            validators.Select(v => v.ValidateAsync(validationContext, cancellationToken)));

        // 收集所有验证失败项
        var validationFailures = validationResults
            .SelectMany(r => r.Errors)
            .Where(f => f != null)
            .ToList();

        // 存在验证失败，抛出FluentValidation异常
        if (validationFailures.Count != 0)
            throw new ValidationException(validationFailures);

        // 验证通过，执行下一个管道/处理器
        return await next(cancellationToken);
    }
}
