namespace BuildingBlocks.Core.Context;

/// <summary>
/// 多租户隔离模式
/// </summary>
public enum TenantIsolationMode
{
    /// <summary>
    /// 共享数据库，共享架构 (TenantId 区分)
    /// </summary>
    Shared = 0,

    /// <summary>
    /// 共享数据库，独立 Schema (db schema 隔离)
    /// </summary>
    SchemaSeparate = 1,

    /// <summary>
    /// 独立数据库 (完全物理隔离)
    /// </summary>
    DatabaseSeparate = 2
}
