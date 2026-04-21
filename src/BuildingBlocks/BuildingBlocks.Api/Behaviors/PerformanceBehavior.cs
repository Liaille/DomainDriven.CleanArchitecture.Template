using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace BuildingBlocks.Api.Behaviors;

/// <summary>
/// 性能监控管道(IPipelineBehavior实现)
/// 监控慢请求并记录警告日志
/// </summary>
/// <typeparam name="TRequest">请求类型</typeparam>
/// <typeparam name="TResponse">响应类型</typeparam>
/// <param name="logger"></param>
/// <param name="slowRequestThreshold">慢请求阈值 (单位:毫秒)</param>
public class PerformanceBehavior<TRequest, TResponse>(
    ILogger<PerformanceBehavior<TRequest, TResponse>> logger,
    long slowRequestThreshold = 500)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var stopwatch = Stopwatch.StartNew();
        var response = await next(cancellationToken);
        stopwatch.Stop();

        var elapsedMs = stopwatch.ElapsedMilliseconds;
        var requestName = typeof(TRequest).Name;

        // 超过阈值，记录慢请求警告
        if (elapsedMs >= slowRequestThreshold)
        {
            logger.LogWarning(
                "Slow Request Detected | Name: {RequestName} | Elapsed: {ElapsedMs}ms | Threshold: {Threshold}ms",
                requestName, elapsedMs, slowRequestThreshold);
        }

        return response;
    }
}
