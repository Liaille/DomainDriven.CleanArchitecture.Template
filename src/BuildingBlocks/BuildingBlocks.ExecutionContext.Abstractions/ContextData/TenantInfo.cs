using static BuildingBlocks.ExecutionContext.Abstractions.Common.Guard;
using static BuildingBlocks.Core.Common.Guard;
using System.Collections.Immutable;

namespace BuildingBlocks.ExecutionContext.Abstractions.ContextData;

/// <summary>
/// 租户基础信息纯契约记录 (全不可变、线程安全、无技术实现细节依赖)
/// </summary>
public sealed record TenantInfo
{
    /// <summary>
    /// 租户唯一标识 (不可为null/空字符串/空白字符)
    /// </summary>
    public string TenantId { get; init; }

    /// <summary>
    /// 租户业务名称 (可选)
    /// </summary>
    public string? TenantName { get; init; }

    /// <summary>
    /// 租户是否启用 (默认true)
    /// </summary>
    public bool IsEnabled { get; init; } = true;

    /// <summary>
    /// 租户过期时间 (UTC，null表示永不过期；必须为DateTimeKind.Utc)
    /// </summary>
    public DateTime? ExpirationTime { get; init; }

    /// <summary>
    /// 独立Schema模式下的数据库Schema名称 (仅适用于数据库层，可选)
    /// </summary>
    public string? TenantSchema { get; init; }

    /// <summary>
    /// 独立库模式下的加密数据库连接串 (仅适用于数据库层，实现层负责加解密，可选)
    /// </summary>
    public string? TenantConnectionString { get; init; }

    /// <summary>
    /// 不可变扩展属性字典 (Key为string，Value为object?，可选)
    /// </summary>
    public IImmutableDictionary<string, object?>? ExtraProperties { get; init; }

    /// <summary>
    /// 不可变扩展属性字典 (默认值为Empty，避免外部判空)
    /// </summary>
    public IImmutableDictionary<string, object?> SafeExtraProperties
        => ExtraProperties ?? ImmutableDictionary<string, object?>.Empty;

    /// <summary>
    /// 简化构造函数 (仅需租户ID)
    /// </summary>
    /// <param name="tenantId">租户唯一标识 (不可为null/空字符串/空白字符)</param>
    public TenantInfo(string tenantId) : this(tenantId, null)
    {
    }

    /// <summary>
    /// 完整构造函数
    /// </summary>
    /// <param name="tenantId">租户唯一标识 (不可为null/空字符串/空白字符)</param>
    /// <param name="tenantName">租户业务名称 (可选)</param>
    /// <param name="isEnabled">租户是否启用 (默认true)</param>
    /// <param name="expirationTime">租户过期时间 (UTC，null表示永不过期；必须为DateTimeKind.Utc)</param>
    /// <param name="tenantSchema">独立Schema模式下的数据库Schema名称 (仅适用于数据库层，可选)</param>
    /// <param name="tenantConnectionString">独立库模式下的加密数据库连接串 (仅适用于数据库层，实现层负责加解密，可选)</param>
    /// <param name="extraProperties">不可变扩展属性字典 (Key为string，Value为object?，可选)</param>
    /// <exception cref="ArgumentNullException">当tenantId为null/空字符串/空白字符时抛出</exception>
    /// <exception cref="ArgumentException">当expirationTime不为null且Kind不为Utc时抛出</exception>
    public TenantInfo(
        string tenantId,
        string? tenantName = null,
        bool isEnabled = true,
        DateTime? expirationTime = null,
        string? tenantSchema = null,
        string? tenantConnectionString = null,
        IImmutableDictionary<string, object?>? extraProperties = null)
    {
        NotNullOrWhiteSpace(tenantId);
        IsUtc(expirationTime);

        TenantId = tenantId;
        TenantName = tenantName;
        IsEnabled = isEnabled;
        ExpirationTime = expirationTime;
        TenantSchema = tenantSchema;
        TenantConnectionString = tenantConnectionString;
        ExtraProperties = extraProperties;
    }

    /// <summary>
    /// 安全合并新扩展属性，返回新的TenantInfo实例 (不会修改原实例)
    /// </summary>
    /// <param name="newProperties">要合并的新扩展属性字典 (不可为null)</param>
    /// <returns>合并后的新TenantInfo实例</returns>
    /// <exception cref="ArgumentNullException">当newProperties为null时抛出</exception>
    public TenantInfo WithMergedExtraProperties(IDictionary<string, object?> newProperties)
    {
        NotNull(newProperties);
        return this with
        {
            ExtraProperties = SafeExtraProperties.SetItems(newProperties)
        };
    }

    /// <summary>
    /// 安全添加单个新扩展属性，返回新的TenantInfo实例 (不会修改原实例)
    /// </summary>
    /// <param name="key">扩展属性Key (不可为null/空字符串/空白字符)</param>
    /// <param name="value">扩展属性Value (可选)</param>
    /// <returns>添加后的新TenantInfo实例</returns>
    /// <exception cref="ArgumentNullException">当key为null/空字符串/空白字符时抛出</exception>
    public TenantInfo WithExtraProperty(string key, object? value)
    {
        NotNullOrWhiteSpace(key);
        return this with
        {
            ExtraProperties = SafeExtraProperties.SetItem(key, value)
        };
    }

    /// <summary>
    /// 安全更新租户过期时间，返回新的TenantInfo实例 (不会修改原实例；强制UTC)
    /// </summary>
    /// <param name="newExpirationTime">新的租户过期时间 (UTC，null表示永不过期；必须为DateTimeKind.Utc)</param>
    /// <returns>更新后的新TenantInfo实例</returns>
    /// <exception cref="ArgumentException">当newExpirationTime不为null且Kind不为Utc时抛出</exception>
    public TenantInfo WithExpirationTime(DateTime? newExpirationTime)
    {
        IsUtc(newExpirationTime);
        return this with
        {
            ExpirationTime = newExpirationTime
        };
    }
}
