using BuildingBlocks.Core.Exceptions;
using System.ComponentModel;

namespace BuildingBlocks.Core.Context;

/// <summary>
/// 当前用户上下文扩展方法 (提供安全的强类型转换)
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
    /// 获取强类型租户 ID
    /// </summary>
    public static TTenantId GetTenantId<TTenantId>(this ICurrentUser currentUser)
    {
        if (currentUser.TenantId is null)
            throw new DomainException("TenantId is null.");

        return ConvertTo<TTenantId>(currentUser.TenantId);
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
    /// 安全的类型转换辅助方法
    /// </summary>
    private static T ConvertTo<T>(object value)
    {
        if (value is T typedValue)
        {
            return typedValue;
        }

        try
        {
            var converter = TypeDescriptor.GetConverter(typeof(T));
            if (converter.CanConvertFrom(value.GetType()))
            {
                return (T)converter.ConvertFrom(value)!;
            }

            return (T)Convert.ChangeType(value, typeof(T));
        }
        catch (Exception ex)
        {
            throw new InvalidCastException($"Cannot convert value '{value}' to type {typeof(T).Name}.", ex);
        }
    }
}
