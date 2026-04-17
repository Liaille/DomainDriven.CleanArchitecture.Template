namespace BuildingBlocks.Core.Entities;

/// <summary>
/// 实体接口 (无主键类型约束)
/// </summary>
public interface IEntity
{
    /// <summary>
    /// 获取主键数组 (支持复合主键)
    /// </summary>
    object[] GetKeys();
}

/// <summary>
/// 泛型实体接口 (单一主键约束)
/// </summary>
/// <typeparam name="TKey">主键类型</typeparam>
public interface IEntity<out TKey> : IEntity
{
    /// <summary>
    /// 实体主键
    /// </summary>
    TKey Id { get; }
}
