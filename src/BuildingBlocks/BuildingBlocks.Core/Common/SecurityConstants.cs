namespace BuildingBlocks.Core.Common;

/// <summary>
/// 安全相关默认配置
/// 定义密码策略、认证授权的默认参数
/// 统一安全标准，满足企业级安全合规要求
/// </summary>
public static class SecurityConstants
{
    /// <summary>
    /// 用户密码默认最小长度
    /// 保证密码复杂度，最低要求8位字符
    /// </summary>
    public const int DefaultPasswordMinLength = 8;

    /// <summary>
    /// 用户密码默认最大长度
    /// 限制密码长度，最高支持32位字符
    /// </summary>
    public const int DefaultPasswordMaxLength = 32;

    /// <summary>
    /// 密码哈希算法默认迭代次数
    /// PBKDF2/Argon2id算法的迭代次数，默认10000次
    /// 增加暴力破解难度，平衡安全性与性能
    /// </summary>
    public const int DefaultPasswordHashIterations = 10000;

    /// <summary>
    /// JWT访问令牌默认有效期（单位：分钟）
    /// 用户登录后访问令牌默认有效期为120分钟
    /// 超过有效期需使用刷新令牌重新获取
    /// </summary>
    public const int DefaultJwtAccessTokenExpirationMinutes = 120;

    /// <summary>
    /// JWT刷新令牌默认有效期（单位：天）
    /// 刷新令牌默认有效期为7天
    /// 超过有效期需重新登录
    /// </summary>
    public const int DefaultJwtRefreshTokenExpirationDays = 7;

    /// <summary>
    /// 登录失败默认锁定阈值（单位：次）
    /// 连续登录失败5次后临时锁定账户
    /// 防止暴力破解密码
    /// </summary>
    public const int DefaultLoginFailureLockoutThreshold = 5;

    /// <summary>
    /// 登录失败默认锁定时长（单位：分钟）
    /// 登录失败锁定后默认锁定30分钟
    /// 锁定期间禁止登录尝试
    /// </summary>
    public const int DefaultLoginFailureLockoutMinutes = 30;
}
