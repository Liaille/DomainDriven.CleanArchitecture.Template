namespace BuildingBlocks.Logging.Core;

/// <summary>
/// 日志系统全局常量与约定
/// 【设计原则】所有日志系统的全局约定集中在此处，统一管理，避免魔法值
/// 【强制规范】所有日志相关的属性名、类型名、模板必须使用此处定义的常量
/// </summary>
public static class LoggingConstants
{
    /// <summary>
    /// 全局统一的日志输出模板
    /// 【强制规范】所有日志Sink必须使用此模板，保证日志格式的一致性
    /// 包含字段: 时间戳、日志级别、链路ID、用户ID、租户ID、环境、源上下文、消息、异常
    /// </summary>
    public const string DefaultOutputTemplate =
        "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz}] [{Level:u4}] [{TraceId}] [{UserId}] [{TenantId}] [{Environment}] [{SourceContext}] {Message:lj}{NewLine}{Exception}";

    /// <summary>
    /// 日志结构化属性名常量
    /// 【强制规范】所有日志属性必须使用此处定义的名称，保证全链路日志字段统一
    /// </summary>
    public static class PropertyNames
    {
        /// <summary>
        /// 分布式链路追踪ID
        /// </summary>
        public const string TraceId = "TraceId";

        /// <summary>
        /// 当前用户ID
        /// </summary>
        public const string UserId = "UserId";

        /// <summary>
        /// 当前租户ID
        /// </summary>
        public const string TenantId = "TenantId";

        /// <summary>
        /// 运行环境名称
        /// </summary>
        public const string Environment = "Environment";

        /// <summary>
        /// 请求唯一标识
        /// </summary>
        public const string RequestId = "RequestId";

        /// <summary>
        /// 业务模块名称
        /// </summary>
        public const string Module = "Module";

        /// <summary>
        /// 客户端IP地址
        /// </summary>
        public const string Ip = "Ip";

        /// <summary>
        /// 客户端User-Agent
        /// </summary>
        public const string UserAgent = "UserAgent";

        /// <summary>
        /// 请求路径
        /// </summary>
        public const string RequestPath = "RequestPath";

        /// <summary>
        /// 请求HTTP方法
        /// </summary>
        public const string RequestMethod = "RequestMethod";

        /// <summary>
        /// 响应状态码
        /// </summary>
        public const string ResponseStatusCode = "ResponseStatusCode";

        /// <summary>
        /// 请求响应时间(毫秒)
        /// </summary>
        public const string ResponseTime = "ResponseTime";

        /// <summary>
        /// 是否包含敏感数据
        /// </summary>
        public const string IsSensitive = "IsSensitive";

        /// <summary>
        /// 日志类型
        /// </summary>
        public const string LogType = "LogType";
    }

    /// <summary>
    /// 日志类型常量
    /// 【强制规范】所有日志必须明确指定类型，用于日志分类和过滤
    /// </summary>
    public static class LogTypes
    {
        /// <summary>
        /// HTTP请求日志
        /// </summary>
        public const string Request = "Request";

        /// <summary>
        /// 异常错误日志
        /// </summary>
        public const string Exception = "Exception";

        /// <summary>
        /// 审计操作日志
        /// </summary>
        public const string Audit = "Audit";

        /// <summary>
        /// 业务事件日志
        /// </summary>
        public const string Business = "Business";

        /// <summary>
        /// 性能监控日志
        /// </summary>
        public const string Performance = "Performance";

        /// <summary>
        /// 系统运行日志
        /// </summary>
        public const string System = "System";
    }

    /// <summary>
    /// 默认敏感字段列表
    /// 【自动脱敏】日志系统会自动过滤这些字段的值，替换为***
    /// 【扩展方式】可以在配置文件中添加额外的敏感字段
    /// </summary>
    public static readonly string[] DefaultSensitiveProperties =
    [
        "password", "pwd", "secret", "token", "auth", "authorization",
        "creditcard", "ssn", "idcard", "phone", "mobile", "email",
        "address", "privatekey", "private_key", "apikey", "api_key"
    ];
}
