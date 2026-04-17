using BuildingBlocks.Core.Exceptions;

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
        if (!currentUser.IsAuthenticated || currentUser.Id == null)
            throw new UnauthorizedAccessException("User is not authenticated.");

        return (TUserId)currentUser.Id;
    }

    /// <summary>
    /// 获取强类型租户 ID
    /// </summary>
    public static TTenantId GetTenantId<TTenantId>(this ICurrentUser currentUser)
    {
        if (currentUser.TenantId == null)
            throw new DomainException("TenantId is null.");

        return (TTenantId)currentUser.TenantId;
    }

    /// <summary>
    /// 尝试获取用户 ID
    /// </summary>
    public static bool TryGetUserId<TUserId>(this ICurrentUser currentUser, out TUserId userId)
    {
        userId = default!;
        if (currentUser.IsAuthenticated && currentUser.Id is TUserId id)
        {
            userId = id;
            return true;
        }
        return false;
    }
}
