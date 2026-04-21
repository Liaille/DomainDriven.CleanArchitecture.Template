using Microsoft.AspNetCore.Http;

namespace BuildingBlocks.Api.Middleware;

/// <summary>
/// 链路追踪ID中间件
/// 将TraceId写入响应头，便于前后端联调排查
/// </summary>
/// <param name="next"></param>
public class CorrelationIdMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        // 向响应头添加追踪ID
        context.Response.Headers["X-Trace-Id"] = context.TraceIdentifier;
        await next(context);
    }
}
