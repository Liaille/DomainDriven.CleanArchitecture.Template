namespace BuildingBlocks.Core.Events;

/// <summary>
/// 集成事件版本号
/// </summary>
/// <remarks>
/// 核心特性:
/// <list type="number">
/// <item>值类型，高性能、零 GC、AOT 友好</item>
/// <item>支持 string/int/Enum 三种方式创建版本</item>
/// <item>支持隐式转换，使用简洁无侵入</item>
/// <item>内置语义化版本(SemVer)解析与智能比较</item>
/// <item>自带版本合法性验证</item>
/// <item>默认版本为 1，绝大多数事件无需手动配置</item>
/// </list>
/// </remarks>
public readonly record struct IntegrationEventVersion : IComparable<IntegrationEventVersion>, IEquatable<IntegrationEventVersion>
{
    /// <summary>
    /// 版本号原始值
    /// </summary>
    private readonly string _value;

    /// <summary>
    /// 语义化版本对象 (用于智能版本比较)
    /// </summary>
    private readonly Version? _semanticVersion;

    /// <summary>
    /// 私有构造函数
    /// </summary>
    /// <param name="value">版本字符串</param>
    /// <exception cref="ArgumentException"></exception>
    private IntegrationEventVersion(string value)
    {
        // 验证版本号不能为空或空白字符
        ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(value));

        _value = value.Trim();

        // 清理版本号前缀 (如 v1.0.0 → 1.0.0)
        var cleanVersion = _value.TrimStart('v', 'V');

        // 尝试解析为语义化版本，成功则存储解析结果，失败则保持 null
        bool isSemanticVersion = Version.TryParse(cleanVersion, out var semanticVersion);
        _semanticVersion = isSemanticVersion ? semanticVersion : null;
    }

    /// <summary>
    /// 默认版本 (版本 1)
    /// </summary>
    public static IntegrationEventVersion Default => new("1");

    #region 工厂方法

    /// <summary>
    /// 从字符串创建版本号
    /// </summary>
    /// <param name="version">版本字符串，如 1、2.0、v3.1</param>
    /// <exception cref="ArgumentException"></exception>
    public static IntegrationEventVersion From(string version) => new(version);

    /// <summary>
    /// 从整数创建版本号
    /// </summary>
    /// <param name="version">整数版本号</param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static IntegrationEventVersion From(int version)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(version, nameof(version));
        return new(version.ToString());
    }

    /// <summary>
    /// 从枚举创建版本号
    /// </summary>
    /// <typeparam name="TEnum">枚举类型</typeparam>
    /// <param name="version">枚举值</param>
    public static IntegrationEventVersion From<TEnum>(TEnum version) where TEnum : Enum
        => new(version.ToString()!);

    #endregion

    #region 隐式转换

    /// <summary>
    /// 字符串隐式转换为 IntegrationEventVersion
    /// </summary>
    public static implicit operator IntegrationEventVersion(string version) => From(version);

    /// <summary>
    /// 整数隐式转换为 IntegrationEventVersion
    /// </summary>
    public static implicit operator IntegrationEventVersion(int version) => From(version);

    /// <summary>
    /// 枚举隐式转换为 IntegrationEventVersion
    /// </summary>
    public static implicit operator IntegrationEventVersion(Enum version) => From(version);

    #endregion

    #region 版本比较方法

    /// <summary>
    /// 判断当前版本是否大于目标版本
    /// </summary>
    /// <param name="other">目标版本</param>
    /// <returns>比较结果</returns>
    public bool IsGreaterThan(IntegrationEventVersion other) => CompareTo(other) > 0;

    /// <summary>
    /// 判断当前版本是否小于目标版本
    /// </summary>
    /// <param name="other">目标版本</param>
    /// <returns>比较结果</returns>
    public bool IsLessThan(IntegrationEventVersion other) => CompareTo(other) < 0;

    /// <summary>
    /// 判断当前版本是否大于等于目标版本
    /// </summary>
    /// <param name="other">目标版本</param>
    /// <returns>比较结果</returns>
    public bool IsGreaterThanOrEqualTo(IntegrationEventVersion other) => CompareTo(other) >= 0;

    /// <summary>
    /// 判断当前版本是否小于等于目标版本
    /// </summary>
    /// <param name="other">目标版本</param>
    /// <returns>比较结果</returns>
    public bool IsLessThanOrEqualTo(IntegrationEventVersion other) => CompareTo(other) <= 0;

    #endregion

    #region 比较实现

    /// <summary>
    /// 版本号比较 (优先语义化版本比较，否则字符串比较)
    /// </summary>
    /// <param name="other">目标版本</param>
    /// <returns>比较结果</returns>
    public int CompareTo(IntegrationEventVersion other)
    {
        if (_semanticVersion is not null && other._semanticVersion is not null)
            return _semanticVersion.CompareTo(other._semanticVersion);

        return string.Compare(_value, other._value, StringComparison.Ordinal);
    }

    #endregion

    /// <summary>
    /// 返回版本号字符串
    /// </summary>
    /// <returns>版本字符串</returns>
    public override string ToString() => _value;
}
