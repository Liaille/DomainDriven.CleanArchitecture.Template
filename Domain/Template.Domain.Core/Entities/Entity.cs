using Template.Domain.Core.Abstractions;

namespace Template.Domain.Core.Entities;

/// <summary>
/// 实体基类 (支持复合主键 / 自定义主键)
/// </summary>
public abstract class Entity : IEntity
{
    /// <summary>
    /// 获取主键数组 (支持复合主键 / 自定义主键，由子类实现)
    /// </summary>
    public abstract object[] GetKeys();

    /// <summary>
    /// 相等性比较
    /// </summary>
    public bool Equals(IEntity? other)
    {
        if (other == null || other.GetType() != GetType())
            return false;

        return GetKeys().SequenceEqual(other.GetKeys());
    }
}

/// <summary>
/// 泛型主键实体基类
/// 支持单一主键场景（如 int/long/Guid/string 等），简化主键访问
/// </summary>
/// <typeparam name="TKey">主键类型</typeparam>
public abstract class Entity<TKey> : Entity, IEntity<TKey>
{
    /// <summary>
    /// 主键
    /// </summary>
    public TKey Id { get; protected set; } = default!;

    /// <summary>
    /// 无参构造函数（ORM/序列化使用）
    /// </summary>
    protected Entity()
    {
    }

    /// <summary>
    /// 指定主键构造
    /// </summary>
    protected Entity(TKey id)
    {
        Id = id;
    }

    /// <summary>
    /// 返回主键数组
    /// </summary>
    public override object[] GetKeys()
    {
        return [Id!];
    }
}
