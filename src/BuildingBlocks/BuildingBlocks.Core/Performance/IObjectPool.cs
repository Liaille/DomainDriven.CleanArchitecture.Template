namespace BuildingBlocks.Core.Performance;

/// <summary>
/// 对象池抽象接口 (用于高频使用对象的复用，减少 GC 压力)
/// </summary>
/// <typeparam name="T">对象类型</typeparam>
public interface IObjectPool<T>
{
    /// <summary>
    /// 从池中获取对象
    /// </summary>
    T Get();

    /// <summary>
    /// 将对象归还到池中
    /// </summary>
    void Return(T obj);
}
