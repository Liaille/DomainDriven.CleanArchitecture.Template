namespace Domain.Shared.Enums;

/// <summary>
/// 通用实体状态
/// </summary>
public enum EntityStatus
{
    /// <summary>
    /// 草稿/未激活
    /// </summary>
    Draft = 0,

    /// <summary>
    /// 已激活/正常
    /// </summary>
    Active = 1,

    /// <summary>
    /// 已禁用/锁定
    /// </summary>
    Disabled = 2,

    /// <summary>
    /// 已删除/归档
    /// </summary>
    Deleted = 99
}
