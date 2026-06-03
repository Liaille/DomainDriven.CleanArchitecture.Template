using BuildingBlocks.Serialization.Abstractions.Enums;

namespace BuildingBlocks.Serialization.Abstractions.Attributes;

/// <summary>
/// 通用序列化忽略属性
/// 适配所有支持的格式
/// 避免重复标记同一属性
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public class SerializationIgnoreAttribute : Attribute
{
    /// <summary>
    /// 指定要忽略的格式字典 (键: 格式，值: 是否忽略)
    /// 若为空，忽略所有格式
    /// </summary>
    public Dictionary<SerializationFormat, bool> IgnoreForFormats { get; set; } = [];

    public SerializationIgnoreAttribute() { }

    /// <summary>
    /// 忽略指定的格式
    /// </summary>
    /// <param name="ignoreForFormats">要忽略的格式列表</param>
    public SerializationIgnoreAttribute(params SerializationFormat[] ignoreForFormats)
    {
        foreach (var format in ignoreForFormats)
        {
            IgnoreForFormats[format] = true;
        }
    }

    /// <summary>
    /// 检查是否需要忽略指定格式
    /// </summary>
    public bool ShouldIgnore(SerializationFormat format)
    {
        if (IgnoreForFormats.Count == 0)
            return true;

        return IgnoreForFormats.TryGetValue(format, out var ignore) && ignore;
    }
}
