using BuildingBlocks.Core.Context;
using System.Reflection;

namespace BuildingBlocks.Logging.Helpers;

internal static class LogContextEnricher
{
    public static void EnrichContext<TEvent>(
        TEvent logEvent,
        ICurrentUser? currentUser,
        IRequestContext? requestContext,
        ICurrentTenant? currentTenant)
        where TEvent : notnull
    {
        var properties = typeof(TEvent).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        // 用户上下文
        TrySetProperty(properties, logEvent, nameof(ICurrentUser.Id), currentUser?.Id?.ToString());
        TrySetProperty(properties, logEvent, "OperatorId", currentUser?.Id?.ToString());
        TrySetProperty(properties, logEvent, "OperatorName", currentUser?.UserName);

        // 请求上下文
        TrySetProperty(properties, logEvent, "RequestId", requestContext?.RequestId.ToString());
        TrySetProperty(properties, logEvent, "TraceId", requestContext?.TraceId ?? requestContext?.RequestId.ToString());
        TrySetProperty(properties, logEvent, "ClientIp", requestContext?.ClientIp);
        TrySetProperty(properties, logEvent, "RequestTraceId", requestContext?.TraceId ?? requestContext?.RequestId.ToString());

        // 租户上下文
        if (currentTenant is not null && currentTenant.IsActive)
        {
            TrySetProperty(properties, logEvent, "TenantId", currentTenant.Id?.ToString());
            TrySetProperty(properties, logEvent, "TenantName", currentTenant.Name);
        }

        // 时间戳
        TrySetProperty(properties, logEvent, "UtcTimestamp", DateTime.UtcNow);
        TrySetProperty(properties, logEvent, "OperationUtcTime", DateTime.UtcNow);
    }

    private static void TrySetProperty<T>(PropertyInfo[] properties, T obj, string propName, object? value)
    {
        var prop = properties.FirstOrDefault(p => p.Name == propName && p.CanWrite);
        prop?.SetValue(obj, value);
    }
}
