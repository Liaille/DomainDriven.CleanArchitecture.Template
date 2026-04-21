using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace BuildingBlocks.Api.Behaviors;

/// <summary>
/// 请求日志管道(IPipelineBehavior实现)
/// 记录所有Command/Query的入参、耗时、执行状态
/// </summary>
/// <typeparam name="TRequest">请求类型</typeparam>
/// <typeparam name="TResponse">响应类型</typeparam>
public class RequestLoggingBehavior<TRequest, TResponse>(
    ILogger<RequestLoggingBehavior<TRequest, TResponse>> logger,
    IHttpContextAccessor httpContextAccessor)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        // 获取请求链路追踪ID
        var traceId = httpContextAccessor.HttpContext?.TraceIdentifier ?? "NoTraceId";
        var requestName = typeof(TRequest).Name;

        if (logger.IsEnabled(LogLevel.Information))
            logger.LogInformation(
                "Request Start | Name: {RequestName} | TraceId: {TraceId} | Data: {RequestData}",
                requestName, traceId, request);

        // 启动请求耗时计时器
        var stopwatch = Stopwatch.StartNew();
        try
        {
            // 执行下一个管道/处理器
            var response = await next(cancellationToken);
            stopwatch.Stop();

            if (logger.IsEnabled(LogLevel.Information))
                logger.LogInformation(
                    "Request Success | Name: {RequestName} | TraceId: {TraceId} | Elapsed: {ElapsedMs}ms",
                    requestName, traceId, stopwatch.ElapsedMilliseconds);

            return response;
        }
        catch (Exception ex)
        {
            stopwatch.Stop();
            logger.LogError(
                ex,
                "Request Failed | Name: {RequestName} | TraceId: {TraceId} | Elapsed: {ElapsedMs}ms", 
                requestName, traceId, stopwatch.ElapsedMilliseconds);
            // 抛出异常，由全局异常处理器处理
            throw;
        }
    }
}
