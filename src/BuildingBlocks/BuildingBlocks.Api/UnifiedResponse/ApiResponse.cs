namespace BuildingBlocks.Api.UnifiedResponse;

/// <summary>
/// 统一API响应结果(无数据类型)
/// </summary>
public class ApiResponse
{
    /// <summary>
    /// 是否成功
    /// </summary>
    public bool IsSuccess { get; set; }

    /// <summary>
    /// 响应码
    /// </summary>
    public int Code { get; set; }

    /// <summary>
    /// 响应消息
    /// </summary>
    public string? Message { get; set; }

    /// <summary>
    /// 请求追踪ID
    /// </summary>
    public string? TraceId { get; set; }

    /// <summary>
    /// 请求路径
    /// </summary>
    public string? Instance { get; set; }

    /// <summary>
    /// 验证错误详情集合
    /// </summary>
    public object? Errors { get; set; }

    /// <summary>
    /// 创建成功响应
    /// </summary>
    public static ApiResponse Success(int code = 200, string? message = "Operation succeeded")
    {
        return new ApiResponse { IsSuccess = true, Code = code, Message = message };
    }

    /// <summary>
    /// 创建失败响应
    /// </summary>
    public static ApiResponse Fail(int code = 500, string? message = "Operation failed")
    {
        return new ApiResponse { IsSuccess = false, Code = code, Message = message };
    }
}

/// <summary>
/// 统一API响应结果(带数据类型)
/// </summary>
/// <typeparam name="T">数据类型</typeparam>
public class ApiResult<T> : ApiResponse
{
    /// <summary>
    /// 响应数据
    /// </summary>
    public T? Data { get; set; }

    /// <summary>
    /// 创建成功响应(带数据)
    /// </summary>
    public static ApiResult<T> Success(T? data, int code = 200, string? message = "Operation succeeded")
    {
        return new ApiResult<T>
        {
            IsSuccess = true,
            Code = code,
            Message = message,
            Data = data
        };
    }

    /// <summary>
    /// 创建失败响应(带数据)
    /// </summary>
    public static new ApiResult<T> Fail(int code = 500, string? message = "Operation failed")
    {
        return new ApiResult<T>
        {
            IsSuccess = false,
            Code = code,
            Message = message,
            Data = default
        };
    }
}
