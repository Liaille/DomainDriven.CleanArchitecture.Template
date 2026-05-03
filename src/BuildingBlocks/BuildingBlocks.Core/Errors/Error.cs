namespace BuildingBlocks.Core.Errors;

/// <summary>
/// 全局统一强类型错误对象
/// </summary>
/// <param name="Code">错误码</param>
/// <param name="Message">用户友好提示信息</param>
/// <param name="Type">错误类型</param>
/// <param name="Module">所属模块</param>
/// <param name="TraceId">链路追踪ID</param>
public record Error(
    int Code,
    string Message,
    ErrorType Type = ErrorType.Failure,
    string Module = "Unknown",
    string? TraceId = null)
{
    /// <summary>
    /// 无错误占位符
    /// </summary>
    public static readonly Error None = new(0, string.Empty, ErrorType.None);

    /// <summary>
    /// 操作成功
    /// </summary>
    public static readonly Error Success = new(0, "Success", ErrorType.None);
}
