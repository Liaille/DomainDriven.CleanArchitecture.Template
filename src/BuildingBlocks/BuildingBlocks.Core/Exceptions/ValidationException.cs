using BuildingBlocks.Core.Common;
using BuildingBlocks.Core.Errors;
using System.Text.Json.Serialization;

namespace BuildingBlocks.Core.Exceptions;

/// <summary>
/// 参数验证异常
/// <para>用户请求参数校验失败、参数格式错误、参数值非法等场景</para>
/// <remarks>
/// <list type="table">
/// <item>[默认行为]</item>
/// <item>错误类型: Parameter</item>
/// <item>日志级别: Warning</item>
/// <item>HTTP状态码: 400 Bad Request</item>
/// <item>告警策略: 高频触发时告警</item>
/// </list>
/// </remarks>
/// </summary>
public class ValidationException
{
    
}
