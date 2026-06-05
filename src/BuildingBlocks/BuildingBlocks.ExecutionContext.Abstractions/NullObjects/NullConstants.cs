namespace BuildingBlocks.ExecutionContext.Abstractions.NullObjects;

/// <summary>
/// 空对象模式专用常量
/// </summary>
/// <remarks>
/// 【命名规则】: 使用 `__` 前缀和后缀，避免与真实业务ID冲突
/// 【适用场景】: NullObject 实现返回的默认ID，明确标识为「未初始化/无上下文」
/// 【禁止使用】: 业务层禁止将这些常量作为真实的业务ID存储/传输
/// </remarks>
public static class NullConstants
{
    /// <summary>
    /// 空租户ID (值: __NULL_TENANT__)
    /// </summary>
    /// <value>
    /// 用于 NullTenantInfo.Instance.TenantId、ICurrentTenant.TenantId 默认值
    /// </value>
    public const string NullTenantId = "__NULL_TENANT__";

    /// <summary>
    /// 空用户ID (值: __NULL_USER__)
    /// </summary>
    /// <value>
    /// 用于 NullUserInfo.Instance.UserId、ICurrentUser.UserId 默认值
    /// </value>
    public const string NullUserId = "__NULL_USER__";

    /// <summary>
    /// 空链路ID (值: __NULL_TRACE__)
    /// </summary>
    /// <value>
    /// 用于 NullRequestTraceInfo.Instance.TraceId、IRequestContext.TraceId 默认值
    /// </value>
    public const string NullTraceId = "__NULL_TRACE__";
}
