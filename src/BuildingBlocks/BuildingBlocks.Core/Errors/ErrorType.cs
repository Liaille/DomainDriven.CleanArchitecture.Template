namespace BuildingBlocks.Core.Errors;

/// <summary>
/// 错误类型枚举
/// 严格对应10位错误码的第1位字符
/// 全局统一语义分类，所有业务线必须遵循
/// </summary>
/// <remarks>
/// 【强制约束】
/// 1. 枚举值的字符必须与错误码第1位完全一致
/// 2. 新增错误类型必须同步更新错误码规范文档
/// 3. 错误类型决定了默认的HTTP状态码、日志级别和告警策略
/// </remarks>
public enum ErrorType
{
    /// <summary>
    /// 参数错误 (Parameter)
    /// 错误码前缀: P
    /// 适用场景: 请求参数格式错误、参数缺失、参数值非法、数据校验失败
    /// 默认日志级别: Warning
    /// 默认HTTP状态码: 400 Bad Request
    /// 默认告警策略: 高频触发(1分钟>100次)时告警
    /// </summary>
    Parameter = 'P',

    /// <summary>
    /// 业务错误 (Business)
    /// 错误码前缀: B
    /// 适用场景: 业务规则校验失败、业务状态异常、操作权限不足(业务级)
    /// 默认日志级别: Information
    /// 默认HTTP状态码: 400 Bad Request
    /// 默认告警策略: 不触发告警(属于正常业务流程)
    /// </summary>
    Business = 'B',

    /// <summary>
    /// 系统错误 (System)
    /// 错误码前缀: S
    /// 适用场景: 服务器内部错误、数据库异常、缓存异常、配置错误、认证授权失败、资源不存在、请求限流
    /// 默认日志级别: Error
    /// 默认HTTP状态码: 500 Internal Server Error
    /// 默认告警策略: 立即触发告警
    /// </summary>
    System = 'S',

    /// <summary>
    /// 第三方错误 (Third-party)
    /// 错误码前缀: T
    /// 适用场景: 外部服务调用失败、第三方接口超时、推送渠道异常、云服务异常
    /// 默认日志级别: Error
    /// 默认HTTP状态码: 503 Service Unavailable
    /// 默认告警策略: 按依赖重要性分级告警
    /// </summary>
    ThirdParty = 'T',

    /// <summary>
    /// 致命错误 (Fatal)
    /// 错误码前缀: F
    /// 适用场景: 系统崩溃、数据库宕机、核心服务不可用、进程异常退出
    /// 默认日志级别: Critical
    /// 默认HTTP状态码: 503 Service Unavailable
    /// 默认告警策略: 立即触发最高级别告警(电话+短信)
    /// </summary>
    Fatal = 'F'
}
