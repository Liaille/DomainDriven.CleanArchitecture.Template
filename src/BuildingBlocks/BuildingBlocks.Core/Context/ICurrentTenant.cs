namespace BuildingBlocks.Core.Context;

/// <summary>
/// 当前租户上下文
/// </summary>
public interface ICurrentTenant
{
    /// <summary>
    /// 租户是否启用 (多租户功能是否生效)
    /// </summary>
    bool IsActive { get; }

    /// <summary>
    /// 租户唯一标识 (弱类型)
    /// </summary>
    object? Id { get; }

    /// <summary>
    /// 租户唯一名称 (用于路由/域名识别)
    /// </summary>
    string? Name { get; }

    /// <summary>
    /// 租户正常显示名称
    /// </summary>
    string? DisplayName { get; }

    /// <summary>
    /// 租户隔离模式 (共享数据库/独立架构/独立库)
    /// </summary>
    TenantIsolationMode IsolationMode { get; }

    /// <summary>
    /// 租户数据库连接字符串 (按需使用，支持动态切换)
    /// </summary>
    string? ConnectionString { get; }

    /// <summary>
    /// 是否为宿主管理员租户 (Host 租户)
    /// </summary>
    bool IsHost { get; }

    /// <summary>
    /// 租户是否启用
    /// </summary>
    bool IsEnabled { get; }

    /// <summary>
    /// 父租户 ID (支持多级租户结构)
    /// </summary>
    object? ParentId { get; }

    /// <summary>
    /// 租户默认语言
    /// </summary>
    string? DefaultLanguage { get; }

    /// <summary>
    /// 租户默认时区
    /// </summary>
    string? DefaultTimeZone { get; }

    /// <summary>
    /// 租户版本/套餐类型
    /// </summary>
    string? Edition { get; }

    /// <summary>
    /// 租户是否完成初始化 ( migrations / seed data )
    /// </summary>
    bool IsInitialized { get; }
}
