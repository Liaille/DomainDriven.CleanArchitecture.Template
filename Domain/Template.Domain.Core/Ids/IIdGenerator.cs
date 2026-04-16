namespace Template.Domain.Core.Ids;

/// <summary>
/// ID 生成器接口，支持任意 ID 类型
/// </summary>
/// <typeparam name="TKey">ID 类型</typeparam>
public interface IIdGenerator<out TKey>
{
    /// <summary>
    /// 创建新 ID
    /// </summary>
    /// <returns>新创建的 ID</returns>
    TKey CreateId();
}
