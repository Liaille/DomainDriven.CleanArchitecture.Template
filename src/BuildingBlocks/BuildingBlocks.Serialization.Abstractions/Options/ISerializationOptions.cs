using BuildingBlocks.Serialization.Abstractions.Enums;

namespace BuildingBlocks.Serialization.Abstractions.Options;

/// <summary>
/// 全局序列化选项接口
/// <para>所有实现必须支持源生成器、AOT适配</para>
/// <para>不含具体框架的配置 (如 System.Text.Json.JsonSerializerOptions)</para>
/// </summary>
public interface ISerializationOptions
{
    /// <summary>
    /// 默认序列化格式
    /// <para>默认: Json</para>
    /// </summary>
    SerializationFormat DefaultFormat { get; }

    /// <summary>
    /// 命名策略
    /// <para>默认: CamelCase</para>
    /// </summary>
    NamingPolicy NamingPolicy { get; }

    /// <summary>
    /// 默认值处理方式
    /// <para>默认: IgnoreNulls</para>
    /// </summary>
    DefaultValueHandling DefaultValueHandling { get; }

    /// <summary>
    /// 枚举序列化方式
    /// <para>默认: String</para>
    /// </summary>
    EnumSerializationMode EnumSerializationMode { get; }

    /// <summary>
    /// 最大深度 (防止循环引用/恶意数据)
    /// <para>默认: 100</para>
    /// </summary>
    int MaxDepth { get; }

    /// <summary>
    /// 是否启用压缩 (仅Binary、MessagePack、Protobuf有效)
    /// <para>默认: true (生产环境)</para>
    /// </summary>
    bool EnableCompression { get; }

    /// <summary>
    /// 是否强制使用AOT模式
    /// <para>默认: true (.NET10+默认AOT编译)</para>
    /// <para>禁用会导致AOT模式下运行时错误</para>
    /// </summary>
    bool EnforceAotMode { get; }

    /// <summary>
    /// 是否允许循环引用
    /// <para>默认: false (禁止循环引用，避免性能问题和序列化异常)</para>
    /// </summary>
    bool AllowCircularReferences { get; }
}

/// <summary>
/// 默认序列化配置 (符合企业级最佳实践)
/// </summary>
public static class DefaultSerializationOptions
{
    /// <summary>
    /// 生产环境推荐默认配置
    /// </summary>
    public static readonly ISerializationOptions Production = new DefaultSerializationOptionsImpl
    {
        DefaultFormat = SerializationFormat.Json,
        NamingPolicy = NamingPolicy.CamelCase,
        DefaultValueHandling = DefaultValueHandling.IgnoreNulls,
        EnumSerializationMode = EnumSerializationMode.String,
        MaxDepth = 100,
        EnableCompression = true,
        EnforceAotMode = true,
        AllowCircularReferences = false
    };

    /// <summary>
    /// 开发环境推荐默认配置
    /// </summary>
    public static readonly ISerializationOptions Development = new DefaultSerializationOptionsImpl
    {
        DefaultFormat = SerializationFormat.Json,
        NamingPolicy = NamingPolicy.CamelCase,
        DefaultValueHandling = DefaultValueHandling.IncludeAll,
        EnumSerializationMode = EnumSerializationMode.String,
        MaxDepth = 100,
        EnableCompression = false,
        EnforceAotMode = false,
        AllowCircularReferences = true
    };

    private sealed class DefaultSerializationOptionsImpl : ISerializationOptions
    {
        public SerializationFormat DefaultFormat { get; init; }
        public NamingPolicy NamingPolicy { get; init; }
        public DefaultValueHandling DefaultValueHandling { get; init; }
        public EnumSerializationMode EnumSerializationMode { get; init; }
        public int MaxDepth { get; init; }
        public bool EnableCompression { get; init; }
        public bool EnforceAotMode { get; init; }
        public bool AllowCircularReferences { get; init; }
    }
}
