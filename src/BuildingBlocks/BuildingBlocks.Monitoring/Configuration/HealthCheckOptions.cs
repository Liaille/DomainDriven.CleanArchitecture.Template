namespace BuildingBlocks.Monitoring.Configuration;

/// <summary>
/// 健康检查配置
/// </summary>
public class HealthCheckOptions
{
    /// <summary>
    /// 是否启用健康检查
    /// </summary>
    public bool Enabled { get; set; } = true;

    /// <summary>
    /// 存活检查路径 (默认 /health/live)
    /// </summary>
    public string LivenessPath { get; set; } = "/health/live";

    /// <summary>
    /// 就绪检查路径 (默认 /health/ready)
    /// </summary>
    public string ReadinessPath { get; set; } = "/health/ready";

    /// <summary>
    /// 自定义健康检查超时时间 (秒)
    /// </summary>
    public int TimeoutSeconds { get; set; } = 5;
}
