using Microsoft.AspNetCore.Http;

namespace BuildingBlocks.Api.Middleware;

/// <summary>
/// 安全响应头中间件
/// 增加Web安全防护，防止XSS、点击劫持等攻击
/// </summary>
public class SecurityHeadersMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        // 禁止MIME类型嗅探
        context.Response.Headers.TryAdd("X-Content-Type-Options", "nosniff");
        // 禁止页面嵌套(防点击劫持)
        context.Response.Headers.TryAdd("X-Frame-Options", "DENY");
        // 开启XSS防护
        context.Response.Headers.TryAdd("X-XSS-Protection", "1; mode=block");
        // 严格来源策略
        context.Response.Headers.TryAdd("Referrer-Policy", "strict-origin-when-cross-origin");
        // 基础内容安全策略
        context.Response.Headers.TryAdd("Content-Security-Policy", "default-src 'self'; script-src 'self'; style-src 'self'; img-src 'self' data:;");

        await next(context);
    }
}
