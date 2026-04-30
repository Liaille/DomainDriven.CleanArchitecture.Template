namespace BuildingBlocks.Core.Context;

/// <summary>
/// 当前用户上下文扩展方法
/// 提供安全的强类型转换
/// </summary>
public static class CurrentUserExtensions
{
    /// <summary>
    /// 获取强类型用户 ID
    /// </summary>
    public static TUserId GetUserId<TUserId>(this ICurrentUser currentUser)
    {
        if (!currentUser.IsAuthenticated || currentUser.Id is null)
            throw new UnauthorizedAccessException("User is not authenticated.");

        return ConvertTo<TUserId>(currentUser.Id);
    }

    /// <summary>
    /// 从 ICurrentTenant 获取强类型租户 ID
    /// 统一租户来源，避免上下文不一致
    /// </summary>
    public static TTenantId GetTenantId<TTenantId>(this ICurrentTenant currentTenant)
    {
        if (!currentTenant.IsActive)
            throw new UnauthorizedAccessException("Multi-tenancy is not active.");

        if (currentTenant.Id is null)
            throw new UnauthorizedAccessException("TenantId is missing.");

        if (!currentTenant.IsEnabled)
            throw new UnauthorizedAccessException("Tenant is disabled.");

        return ConvertTo<TTenantId>(currentTenant.Id);
    }

    /// <summary>
    /// 尝试获取用户 ID
    /// </summary>
    public static bool TryGetUserId<TUserId>(this ICurrentUser currentUser, out TUserId userId)
    {
        userId = default!;
        if (currentUser.IsAuthenticated && currentUser.Id is not null)
        {
            try
            {
                userId = ConvertTo<TUserId>(currentUser.Id);
                return true;
            }
            catch
            {
                return false;
            }
        }
        return false;
    }

    /// <summary>
    /// 高性能类型转换
    /// 支持 Guid / int / long / string 快速路径
    /// </summary>
    private static T ConvertTo<T>(object value)
    {
        // 直接类型匹配，快速返回
        if (value is T typedValue)
            return typedValue;

        // 处理数据库 DBNull 常见场景
        if (value == DBNull.Value)
            throw new InvalidCastException($"Cannot convert DBNull to type {typeof(T).Name}.");

        try
        {
            // 常用类型快速转换 (性能优化)
            return typeof(T) switch
            {
                Type t when t == typeof(Guid) => (T)(object)Guid.Parse(value.ToString()!),
                Type t when t == typeof(int) => (T)(object)Convert.ToInt32(value),
                Type t when t == typeof(long) => (T)(object)Convert.ToInt64(value),
                Type t when t == typeof(string) => (T)(object)value.ToString()!,
                _ => (T)Convert.ChangeType(value, typeof(T))
            };
        }
        catch (Exception ex)
        {
            throw new InvalidCastException($"Cannot convert value '{value}' to type {typeof(T).Name}.", ex);
        }
    }
}
