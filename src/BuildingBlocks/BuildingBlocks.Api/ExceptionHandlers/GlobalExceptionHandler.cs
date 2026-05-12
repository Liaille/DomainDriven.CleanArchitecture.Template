using BuildingBlocks.Api.UnifiedResponse;
using BuildingBlocks.Core.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;
using ValidationException = BuildingBlocks.Core.Exceptions.ValidationException;

namespace BuildingBlocks.Api.ExceptionHandlers;

/// <summary>
/// 全局异常统一处理器
/// 实现 IExceptionHandler 官方全局异常拦截接口
/// 统一捕获系统所有未处理异常，标准化输出 API 统一响应格式
/// </summary>
public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    /// <summary>
    /// 全局静态缓存 JSON 序列化配置
    /// 避免每次异常处理都新建配置实例，提升性能
    /// 采用驼峰命名、不格式化缩进，适配前端标准 JSON 格式
    /// </summary>
    private static readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = false
    };

    /// <summary>
    /// 全局异常处理入口核心方法
    /// </summary>
    /// <param name="httpContext">当前 HTTP 请求上下文</param>
    /// <param name="exception">捕获到的原始异常对象</param>
    /// <param name="cancellationToken">异步取消令牌</param>
    /// <returns>bool：true 表示异常已自行处理，不再进入默认异常管道</returns>
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        // 拆解聚合异常，取内部真实异常作为处理对象
        if (exception is AggregateException aggEx && aggEx.InnerException is not null)
            exception = aggEx.InnerException;

        // 记录错误日志：包含请求路径、追踪标识、异常堆栈信息
        logger.LogError(
            exception,
            "全局异常捕获 | 请求路径: {RequestPath} | 追踪ID: {TraceId} | 异常信息: {Message}",
            httpContext.Request.Path,
            httpContext.TraceIdentifier,
            exception.Message);

        // 将异常映射为 HTTP 状态码、提示消息、详细错误集合
        var (statusCode, message, errors) = MapExceptionToError(exception);

        // 构造标准化失败响应实体
        var response = ApiResponse.Fail(statusCode, message);
        // 链路追踪唯一标识
        response.TraceId = httpContext.TraceIdentifier;
        // 请求接口路径
        response.Instance = httpContext.Request.Path;
        // 详细校验错误/业务错误明细
        response.Errors = errors;

        // 设置响应状态码与内容类型
        httpContext.Response.StatusCode = statusCode;
        httpContext.Response.ContentType = "application/json";

        // 序列化统一响应并写入响应流返回前端
        await JsonSerializer.SerializeAsync(httpContext.Response.Body, response, _jsonSerializerOptions, cancellationToken);

        // 标记异常已完成处理，终止后续默认异常处理流程
        return true;
    }

    /// <summary>
    /// 异常类型映射器
    /// 根据不同自定义异常类型，匹配对应的 Http 状态码、友好提示、错误明细
    /// 遵循架构设计：区分业务可预期异常、系统不可预期异常
    /// </summary>
    private static (int Code, string Message, object? Errors) MapExceptionToError(Exception ex)
    {
        return ex switch
        {
            // 身份认证异常：未登录、Token 无效/过期
            AuthenticationException => ((int)HttpStatusCode.Unauthorized, "身份认证失败，请重新登录", null),

            // 权限授权异常：已登录但无操作权限
            AuthorizationException => ((int)HttpStatusCode.Forbidden, "权限不足，无法访问该资源", null),

            // 资源不存在异常：数据/聚合根未找到
            NotFoundException notFoundEx => ((int)HttpStatusCode.NotFound, notFoundEx.Message, null),

            // 参数校验异常 (自定义)
            ValidationException validationEx => ((int)HttpStatusCode.UnprocessableEntity, "参数校验失败", validationEx.Errors),

            // FluentValidation 参数校验异常
            FluentValidation.ValidationException fluentEx => ((int)HttpStatusCode.UnprocessableEntity, "参数校验失败", fluentEx.Errors),

            // 框架系统异常：基础设施、配置、外部依赖等不可预期错误
            FrameworkException => ((int)HttpStatusCode.InternalServerError, "系统内部异常，请稍后重试", null),

            // 第三方服务调用异常：支付、短信、外部接口调用失败
            ThirdPartyException => ((int)HttpStatusCode.BadGateway, "第三方服务调用失败", null),

            // 所有业务异常基类 (最后匹配，避免覆盖子类)
            BusinessException businessEx => ((int)HttpStatusCode.BadRequest, businessEx.Message, null),

            // 兜底未知系统异常
            _ => ((int)HttpStatusCode.InternalServerError, "服务器未知异常，请联系管理员", null)
        };
    }
}
