namespace BuildingBlocks.Monitoring.Configuration;

/// <summary>
/// 告警配置
/// </summary>
public class AlertOptions
{
    /// <summary>
    /// 是否启用告警
    /// </summary>
    public bool Enabled { get; set; } = true;

    /// <summary>
    /// 告警级别阈值 (Info/Warning/Error)
    /// </summary>
    public string MinimumAlertLevel { get; set; } = "Warning";
}
