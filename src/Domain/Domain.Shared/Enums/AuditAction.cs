namespace Domain.Shared.Enums;

/// <summary>
/// 审计操作类型枚举
/// 用于记录实体的关键操作日志
/// </summary>
public enum AuditAction
{
    /// <summary>
    /// 创建
    /// </summary>
    Create = 0,

    /// <summary>
    /// 更新
    /// </summary>
    Update = 1,

    /// <summary>
    /// 删除
    /// </summary>
    Delete = 2,

    /// <summary>
    /// 读取
    /// </summary>
    Read = 3,

    /// <summary>
    /// 导入
    /// </summary>
    Import = 4,

    /// <summary>
    /// 导出
    /// </summary>
    Export = 5
}
