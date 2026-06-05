namespace BuildingBlocks.ExecutionContext.Abstractions.NullObjects;

/// <summary>
/// 空Disposable单例 (纯只读、不执行任何业务逻辑、无副作用)
/// </summary>
/// <remarks>
/// 【用途】: 实现层返回临时设置上下文的空作用域 (如ICurrentTenant.ChangeTenant失败时)，避免null引用
/// 【复用场景】: 外部项目实现本层接口时，可直接复用此单例
/// </remarks>
public sealed class NullDisposable : IDisposable
{
    /// <summary>
    /// 全局唯一单例实例
    /// </summary>
    public static NullDisposable Instance { get; } = new();

    /// <summary>
    /// 私有构造函数 (防止外部实例化，确保单例)
    /// </summary>
    private NullDisposable()
    {
    }

    /// <summary>
    /// 不执行任何操作的Dispose方法
    /// </summary>
    public void Dispose()
    {
    }
}
