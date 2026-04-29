namespace Domain.Shared.Errors;

/// <summary>
/// 全局错误消息提供器
/// 用于根据错误码获取对应的用户友好提示信息
/// </summary>
public static class ErrorMessage
{
    /// <summary>
    /// 根据错误码获取错误消息
    /// </summary>
    /// <param name="code">错误码枚举</param>
    /// <returns>对应的错误消息字符串</returns>
    public static string GetMessage(ErrorCode code)
    {
        return code switch
        {
            ErrorCode.Success => "操作成功",
            ErrorCode.UnknownError => "系统发生未知错误，请稍后重试",
            ErrorCode.ValidationFailed => "参数验证失败，请检查输入信息",
            ErrorCode.Unauthorized => "未登录或登录已过期，请重新登录",
            ErrorCode.Forbidden => "权限不足，无法执行该操作",
            ErrorCode.NotFound => "请求的资源不存在",
            ErrorCode.AlreadyExists => "资源已存在，不可重复创建",
            ErrorCode.ConcurrencyConflict => "数据已被其他用户修改，请刷新后重试",
            ErrorCode.InvalidOperation => "当前状态不允许执行此操作",
            ErrorCode.ValueIsRequired => "必填项不能为空",
            ErrorCode.InvalidFormat => "格式不正确",
            ErrorCode.OutOfRange => "数值超出允许范围",
            ErrorCode.StringTooLong => "文本长度超出限制",
            ErrorCode.ThirdPartyServiceFailed => "第三方服务调用失败，请稍后重试",
            ErrorCode.ThirdPartyTimeout => "第三方服务响应超时，请稍后重试",
            _ => "系统异常，请稍后重试"
        };
    }
}