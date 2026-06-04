namespace BuildingBlocks.Logging.Abstractions;

/// <summary>
/// 敏感数据过滤器抽象接口
/// 【核心职责】日志系统的敏感数据保护核心契约，负责识别和屏蔽日志中的个人敏感信息
/// 【设计目标】防止密码、身份证、手机号等敏感数据通过日志泄露，满足《个人信息保护法》、GDPR等合规要求
/// 【设计原则】可配置、可扩展、无侵入，默认实现基于LoggingConstants.DefaultSensitiveProperties
/// 【强制规范】所有日志输出前必须经过此过滤器处理，禁止直接输出原始敏感数据
/// </summary>
public interface ISensitiveDataFilter
{
    /// <summary>
    /// 检查指定属性名是否属于敏感数据字段
    /// 【判断依据】
    /// 1. 匹配LoggingConstants.DefaultSensitiveProperties中的默认敏感字段
    /// 2. 匹配用户在配置文件中自定义的敏感字段
    /// 3. 大小写不敏感匹配 (如"Password"、"password"、"PASSWORD"均会被识别)
    /// </summary>
    /// <param name="propertyName">待检查的属性名称 (日志结构化字段名)</param>
    /// <returns>true表示是敏感字段，需要脱敏；false表示非敏感字段</returns>
    bool IsSensitiveProperty(string propertyName);

    /// <summary>
    /// 对敏感数据进行脱敏处理
    /// 【默认脱敏规则】
    /// - 密码/密钥类: 直接替换为"***"
    /// - 手机号: 保留前3位和后4位，中间替换为"****" (如138****1234)
    /// - 身份证号: 保留前6位和后4位，中间替换为"********" (如110101********1234)
    /// - 邮箱: 保留用户名首字母和域名，中间替换为"***" (如a***@example.com)
    /// - 其他敏感字段: 根据长度动态脱敏，保留首尾各1-2位
    /// 【边界处理】
    /// - 若value为null或空字符串，直接返回原值
    /// - 若propertyName不是敏感字段，直接返回原值
    /// </summary>
    /// <param name="propertyName">敏感属性名称 (用于匹配对应的脱敏规则)</param>
    /// <param name="value">原始敏感数据值</param>
    /// <returns>脱敏后的安全字符串，可安全输出到日志</returns>
    string MaskSensitiveData(string propertyName, string value);
}
