namespace Template.Domain.Core.Abstractions;

/// <summary>
/// 实体接口
/// 不指定主键类型，支持复合主键、自定义主键
/// </summary>
public interface IEntity
{
    /// <summary>
    /// 获取主键数组 (支持复合主键、自定义主键)
    /// </summary>
    object[] GetKeys();
}

/// <summary>
/// 泛型主键实体接口
/// 支持 int/long/Guid/string 等主键类型
/// </summary>
/// <typeparam name="TKey">主键类型</typeparam>
public interface IEntity<out TKey> : IEntity
{
    /// <summary>
    /// 实体唯一标识
    /// </summary>
    TKey Id { get; }
}
