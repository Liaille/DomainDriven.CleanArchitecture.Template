namespace BuildingBlocks.Serialization.Abstractions.Attributes;

/// <summary>
/// 通用序列化属性名映射
/// 适配所有支持的格式
/// 避免重复标记同一属性
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public class SerializationPropertyNameAttribute : Attribute
{
    /// <summary>
    /// 指定格式的属性名映射 (键: 格式，值: 名称)
    /// 若为空，所有格式统一使用Name
    /// </summary>
    public Dictionary<Enums.SerializationFormat, string>? FormatSpecificNames { get; set; }

    /// <summary>
    /// 统一属性名 (所有未指定格式使用)
    /// </summary>
    public string? Name { get; set; }

    public SerializationPropertyNameAttribute() { }

    public SerializationPropertyNameAttribute(string name)
    {
        Name = name;
    }

    public SerializationPropertyNameAttribute(Dictionary<Enums.SerializationFormat, string> formatSpecificNames)
    {
        FormatSpecificNames = formatSpecificNames;
    }
}
