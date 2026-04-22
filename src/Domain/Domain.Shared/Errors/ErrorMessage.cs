namespace Domain.Shared.Errors;

/// <summary>
/// 全局错误消息
/// </summary>
public static class ErrorMessage
{
    public static string GetMessage(ErrorCode code)
    {
        return code switch
        {
            ErrorCode.Success => "操作成功",
            ErrorCode.UnknownError => "系统发生未知错误",
            ErrorCode.ValidationFailed => "参数验证失败",
            ErrorCode.Unauthorized => "未登录或登录已过期",
            ErrorCode.Forbidden => "权限不足，无法执行该操作",
            ErrorCode.NotFound => "请求的资源不存在",
            ErrorCode.AlreadyExists => "资源已存在，不可重复创建",
            ErrorCode.ConcurrencyConflict => "数据并发冲突，请刷新后重试",
            ErrorCode.InvalidOperation => "当前状态不允许执行此操作",
            ErrorCode.ValueIsRequired => "必填项不能为空",
            ErrorCode.InvalidFormat => "格式不正确",
            ErrorCode.OutOfRange => "数值超出允许范围",
            ErrorCode.StringTooLong => "文本长度超出限制",
            _ => "系统异常，请稍后重试"
        };
    }
}
