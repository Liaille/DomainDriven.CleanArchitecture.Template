using BuildingBlocks.Api.UnifiedResponse;
using BuildingBlocks.Core.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;
using ApplicationException = BuildingBlocks.Core.Exceptions.ApplicationException;

namespace BuildingBlocks.Api.ExceptionHandlers;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    /// <summary>
    /// 全局静态缓存 JsonSerializerOptions
    /// </summary>
    private static readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = false
    };

    /// <summary>
    /// 异常处理核心方法
    /// </summary>
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        // 展开聚合异常
        if (exception is AggregateException aggEx && aggEx.InnerException is not null)
            exception = aggEx.InnerException;

        // 记录异常日志
        logger.LogError(
            exception,
            "Global exception handled | Path: {RequestPath} | TraceId: {TraceId} | Error: {Message}",
            httpContext.Request.Path,
            httpContext.TraceIdentifier,
            exception.Message);

        // 构建统一错误响应
        var (statusCode, message, errors) = MapExceptionToError(exception);
        var response = ApiResponse.Fail(statusCode, message);
        response.TraceId = httpContext.TraceIdentifier;
        response.Instance = httpContext.Request.Path;
        response.Errors = errors;

        // 设置响应头并JSON序列化输出
        httpContext.Response.StatusCode = statusCode;
        httpContext.Response.ContentType = "application/json";
        await JsonSerializer.SerializeAsync(httpContext.Response.Body, response, _jsonSerializerOptions, cancellationToken);

        // 标记异常已处理完成
        return true;
    }

    /// <summary>
    /// 异常类型与错误信息映射
    /// </summary>
    private static (int Code, string Message, object? Errors) MapExceptionToError(Exception ex)
    {
        return ex switch
        {
            NotFoundException notFoundEx => ((int)HttpStatusCode.NotFound, notFoundEx.Message, null),
            DomainException domainEx => ((int)HttpStatusCode.BadRequest, domainEx.Message, null),
            ApplicationException appEx => ((int)HttpStatusCode.BadRequest, appEx.Message, null),
            UnauthorizedAccessException => ((int)HttpStatusCode.Unauthorized, "Unauthorized access", null),
            ValidationException validationEx => ((int)HttpStatusCode.UnprocessableEntity, "Parameter validation failed", validationEx.Message),
            FluentValidation.ValidationException fluentEx => ((int)HttpStatusCode.UnprocessableEntity, "Parameter validation failed", fluentEx.Errors),
            _ => ((int)HttpStatusCode.InternalServerError, "Server internal error (Please contact system administrator)", null)
        };
    }
}
