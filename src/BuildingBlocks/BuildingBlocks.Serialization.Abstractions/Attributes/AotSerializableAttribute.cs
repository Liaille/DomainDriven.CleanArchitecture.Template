namespace BuildingBlocks.Serialization.Abstractions.Attributes;

/// <summary>
/// 标记可AOT序列化的类型/程序集
/// <para>【核心设计】</para>
/// <para>1. 无参数用法: 自动触发源生成器生成对应的 JsonSerializerContext</para>
/// <para>2. 手动指定 Context: 兼容现有代码，覆盖自动生成逻辑</para>
/// <para>3. 程序集级别: 批量标记指定命名空间下的所有类型</para>
/// <para>【无运行时逻辑】仅用于 Roslyn 分析器、源生成器和架构测试</para>
/// </summary>
/// <remarks>
/// 强制要求: 所有在 API 接口、事件总线、缓存、消息队列中传输的类型必须标记此特性
/// 架构测试会自动扫描所有序列化类型，未标记将导致构建失败
/// </remarks>
[AttributeUsage(
    AttributeTargets.Class |
    AttributeTargets.Struct |
    AttributeTargets.Enum |
    AttributeTargets.Assembly,
    AllowMultiple = false,
    Inherited = false)]
public sealed class AotSerializableAttribute : Attribute
{
    /// <summary>
    /// 手动指定的 JsonSerializerContext 类型
    /// <para>若设置此值，源生成器将跳过自动生成逻辑，直接使用指定的 Context</para>
    /// <para>适用于需要自定义序列化配置的特殊场景</para>
    /// </summary>
    public Type? ContextType { get; }

    /// <summary>
    /// 是否自动生成 JsonSerializerContext
    /// <para>默认值: true</para>
    /// <para>设置为 false 时，源生成器不会为该类型生成 Context，需手动提供</para>
    /// </summary>
    public bool AutoGenerateContext { get; set; } = true;

    /// <summary>
    /// 程序集级别标记时，指定要包含的命名空间前缀
    /// <para>多个命名空间用逗号分隔，如 "MyApp.Domain, MyApp.Application"</para>
    /// <para>若为空，将包含程序集中的所有类型</para>
    /// </summary>
    public string? IncludeNamespaces { get; set; }

    /// <summary>
    /// 程序集级别标记时，指定要排除的命名空间前缀
    /// <para>多个命名空间用逗号分隔</para>
    /// </summary>
    public string? ExcludeNamespaces { get; set; }

    /// <summary>
    /// 是否自动生成常用泛型集合的序列化支持
    /// <para>默认值: true</para>
    /// <para>自动生成 List&lt;T&gt;、Dictionary&lt;string, T&gt;、IEnumerable&lt;T&gt; 等</para>
    /// </summary>
    public bool IncludeCommonCollections { get; set; } = true;

    /// <summary>
    /// 极简用法: 自动生成 JsonSerializerContext
    /// <para>推荐绝大多数场景使用</para>
    /// </summary>
    public AotSerializableAttribute()
    {
    }

    /// <summary>
    /// 手动指定 Context 用法: 覆盖自动生成逻辑
    /// <para>适用于需要自定义序列化配置的特殊场景</para>
    /// </summary>
    /// <param name="contextType">手动实现的 JsonSerializerContext 类型</param>
    public AotSerializableAttribute(Type contextType)
    {
        ContextType = contextType;
        AutoGenerateContext = false; // 手动指定时自动禁用自动生成
    }
}
