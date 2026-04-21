using BuildingBlocks.Api.UnifiedResponse;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace BuildingBlocks.Api.Extensions;

public static class CarterResponseExtensions
{
    /// <summary>
    /// 全局自动统一响应包装
    /// </summary>
    public static IEndpointRouteBuilder AutoResponseWrapper(this IEndpointRouteBuilder app)
    {
        // 给所有 Carter 接口添加全局过滤器
        app.MapGroup("/api").AddEndpointFilter(async (context, next) =>
        {
            var result = await next(context);
            var httpContext = context.HttpContext;
            var statusCode = httpContext.Response.StatusCode;

            // 只处理成功请求
            if (statusCode is < 200 or >= 300)
                return result;

            // 204 不返回内容
            if (statusCode == StatusCodes.Status204NoContent)
                return result;

            // 获取返回数据
            object? data = null;
            if (result is IResult httpResult)
            {
                var valueProperty = httpResult.GetType().GetProperty("Value");
                data = valueProperty?.GetValue(httpResult);
            }
            else
            {
                data = result;
            }

            // 获取自动消息
            var message = GetDefaultMessage(statusCode);

            // 包装统一响应
            if (data is null)
            {
                var apiResponse = ApiResponse.Success(statusCode, message);
                apiResponse.TraceId = httpContext.TraceIdentifier;
                apiResponse.Instance = httpContext.Request.Path;
                return Results.Json(apiResponse);
            }
            else
            {
                var apiResponse = CreateGenericResult(data, statusCode, message);
                ((ApiResponse)apiResponse).TraceId = httpContext.TraceIdentifier;
                ((ApiResponse)apiResponse).Instance = httpContext.Request.Path;
                return Results.Json(apiResponse);
            }
        });

        return app;
    }

    /// <summary>
    /// 根据状态码自动生成消息
    /// </summary>
    private static string GetDefaultMessage(int statusCode)
    {
        return statusCode switch
        {
            200 => "Request succeeded",
            201 => "Resource created successfully",
            204 => "Operation executed successfully",
            _ => "Operation succeeded"
        };
    }

    /// <summary>
    /// 创建泛型 ApiResult<T>
    /// </summary>
    private static object CreateGenericResult(object data, int code, string message)
    {
        var dataType = data.GetType();
        var resultType = typeof(ApiResult<>).MakeGenericType(dataType);
        var instance = Activator.CreateInstance(resultType)!;

        var baseResult = (ApiResponse)instance;
        baseResult.IsSuccess = true;
        baseResult.Code = code;
        baseResult.Message = message;
        resultType.GetProperty("Data")!.SetValue(instance, data);

        return instance;
    }
}