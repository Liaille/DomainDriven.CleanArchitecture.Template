namespace BuildingBlocks.Core.Entities;

/// <summary>
/// 实体基类
/// </summary>
public abstract class Entity : IEntity
{
    /// <summary>
    /// 获取主键数组 (由子类实现)
    /// </summary>
    public abstract object[] GetKeys();

    /// <summary>
    /// 实体相等性比较 (按业务标识比较)
    /// </summary>
    public bool Equals(IEntity? other)
    {
        if (other == null || other.GetType() != GetType())
            return false;

        return GetKeys().SequenceEqual(other.GetKeys());
    }
}

/// <summary>
/// 泛型实体基类
/// </summary>
/// <typeparam name="TKey">主键类型</typeparam>
public abstract class Entity<TKey> : Entity, IEntity<TKey>
{
    /// <summary>
    /// 主键
    /// </summary>
    public TKey Id { get; protected set; } = default!;

    protected Entity()
    {
    }

    protected Entity(TKey id)
    {
        Id = id;
    }

    /// <summary>
    /// 返回主键数组 (满足 IEntity 接口约束)
    /// </summary>
    public override object[] GetKeys()
    {
        return new object[] { Id! };
    }
}
