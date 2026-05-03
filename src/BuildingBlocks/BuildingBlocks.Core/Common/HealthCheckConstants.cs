namespace BuildingBlocks.Core.Common;

/// <summary>
/// 服务健康检查相关常量
/// 集中管理系统健康检查的默认配置、时间间隔、超时等固定参数
/// 保证服务监控行为的一致性与可维护性
/// </summary>
public static class HealthCheckConstants
{
    /// <summary>
    /// 服务健康检查默认执行间隔时间（单位：秒）
    /// 系统默认每隔10秒对各服务节点执行一次健康状态探测
    /// 用于监控服务是否正常运行、资源是否可用
    /// </summary>
    public const int DefaultCheckIntervalSeconds = 10;

    /// <summary>
    /// 健康检查请求默认超时时间（单位：秒）
    /// 单次健康检查请求超过5秒未响应则判定为检查失败
    /// 避免健康检查请求长时间阻塞
    /// </summary>
    public const int DefaultCheckTimeoutSeconds = 5;

    /// <summary>
    /// 启动探针默认失败阈值（单位：次）
    /// 启动探针连续失败30次后判定应用启动失败
    /// 用于Kubernetes Startup Probe配置
    /// </summary>
    public const int StartupProbeFailureThreshold = 30;

    /// <summary>
    /// 存活探针默认失败阈值（单位：次）
    /// 存活探针连续失败3次后判定应用不可用
    /// 用于Kubernetes Liveness Probe配置
    /// </summary>
    public const int LivenessProbeFailureThreshold = 3;

    /// <summary>
    /// 就绪探针默认失败阈值（单位：次）
    /// 就绪探针连续失败2次后判定应用未就绪
    /// 用于Kubernetes Readiness Probe配置
    /// </summary>
    public const int ReadinessProbeFailureThreshold = 2;
}
