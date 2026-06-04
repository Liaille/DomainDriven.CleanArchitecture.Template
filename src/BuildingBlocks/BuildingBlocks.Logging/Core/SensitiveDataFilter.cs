using BuildingBlocks.Core.Common;
using BuildingBlocks.Logging.Abstractions;
using Microsoft.Extensions.Options;

namespace BuildingBlocks.Logging.Core;

/// <summary>
/// 敏感数据过滤器默认实现
/// 【核心职责】实现日志系统的敏感数据自动识别与脱敏，防止个人隐私和机密信息通过日志泄露
/// 【设计特点】
/// 1. 可配置: 支持通过配置文件添加自定义敏感字段、调整脱敏规则
/// 2. 高性能: 使用HashSet实现O(1)时间复杂度的敏感字段检查
/// 3. 大小写不敏感: 自动统一转换为小写进行匹配，避免漏检
/// 4. 灵活脱敏: 支持完全屏蔽和部分保留两种脱敏模式
/// 【合规性】满足《个人信息保护法》、GDPR等数据隐私保护法规要求
/// 【强制规范】所有日志输出前必须经过此过滤器处理
/// </summary>
public class SensitiveDataFilter : ISensitiveDataFilter
{
    /// <summary>
    /// 合并后的敏感字段集合 (全部转为小写，大小写不敏感匹配)
    /// </summary>
    private readonly HashSet<string> _sensitiveProperties;

    /// <summary>
    /// 敏感数据过滤配置选项
    /// </summary>
    private readonly SensitiveDataOptions _options;

    /// <summary>
    /// 初始化敏感数据过滤器
    /// 【初始化逻辑】
    /// 1. 合并系统默认敏感字段(LoggingConstants.DefaultSensitiveProperties)和用户自定义敏感字段
    /// 2. 统一转换为小写，实现大小写不敏感匹配
    /// 3. 自动去重，避免重复字段
    /// </summary>
    /// <param name="options">敏感数据过滤配置选项，不可为null</param>
    /// <exception cref="ArgumentNullException">当options为null时抛出</exception>
    public SensitiveDataFilter(IOptions<SensitiveDataOptions> options)
    {
        Guard.NotNull(options);
        _options = options.Value;

        // 合并默认敏感属性和自定义敏感属性，统一转为小写并去重
        var allSensitiveProperties = LoggingConstants.DefaultSensitiveProperties
            .Concat(_options.AdditionalSensitiveProperties ?? [])
            .Select(p => p.ToLowerInvariant())
            .Distinct();

        _sensitiveProperties = [.. allSensitiveProperties];
    }

    /// <summary>
    /// 检查指定属性名是否属于敏感数据字段
    /// 【判断逻辑】
    /// 1. 自动将属性名转为小写，实现大小写不敏感匹配
    /// 2. 在合并后的敏感字段HashSet中进行O(1)时间复杂度查找
    /// </summary>
    /// <param name="propertyName">待检查的日志结构化字段名，不可为null或空白</param>
    /// <returns>true表示是敏感字段需要脱敏；false表示非敏感字段</returns>
    /// <exception cref="ArgumentException">当propertyName为null或空白字符串时抛出</exception>
    public bool IsSensitiveProperty(string propertyName)
    {
        Guard.NotNullOrWhiteSpace(propertyName);
        return _sensitiveProperties.Contains(propertyName.ToLowerInvariant());
    }

    /// <summary>
    /// 对敏感数据进行脱敏处理
    /// 【处理流程】
    /// 1. 若全局过滤开关未启用，直接返回原始值
    /// 2. 若原始值为null或空白字符串，直接返回原值
    /// 3. 触发完全屏蔽模式的条件: 
    ///    - 配置为完全屏蔽(PartialMaskKeepChars ≤ 0)
    ///    - 原始值长度过短，无法保留首尾字符(长度 ≤ 2*PartialMaskKeepChars)
    /// 4. 部分屏蔽模式: 保留首尾各N位字符，中间替换为掩码文本
    /// </summary>
    /// <param name="propertyName">敏感属性名称 (用于匹配脱敏规则，本实现暂未使用，预留扩展)</param>
    /// <param name="value">原始敏感数据值</param>
    /// <returns>脱敏后的安全字符串，可安全输出到日志</returns>
    public string MaskSensitiveData(string propertyName, string value)
    {
        // 全局过滤开关未启用，直接返回原始值
        if (!_options.EnableFilter)
            return value;

        // 空值或空白字符串无需脱敏
        if (string.IsNullOrWhiteSpace(value))
            return value;

        // 完全屏蔽模式: 直接替换为掩码文本
        if (_options.PartialMaskKeepChars <= 0 || value.Length <= _options.PartialMaskKeepChars * 2)
            return _options.MaskText;

        // 部分屏蔽模式: 保留首尾各N位字符，中间替换为掩码
        var keep = _options.PartialMaskKeepChars;
        return $"{value[..keep]}{_options.MaskText}{value[^keep..]}";
    }
}
