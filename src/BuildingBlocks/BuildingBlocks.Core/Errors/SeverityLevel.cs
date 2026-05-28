namespace BuildingBlocks.Core.Errors;

/// <summary>
/// 严重级别枚举
/// <para>对应错误码第2位</para>
/// </summary>
public enum SeverityLevel
{
    /// <summary>
    /// 普通级
    /// 影响范围：正常业务校验拦截，无系统影响
    /// </summary>
    Info = '0',

    /// <summary>
    /// 警告级
    /// 影响范围：非核心流程异常，不影响主业务
    /// </summary>
    Warning = '1',

    /// <summary>
    /// 错误级
    /// 影响范围：核心业务阻断，单服务功能不可用
    /// </summary>
    Error = '2',

    /// <summary>
    /// 致命级
    /// 影响范围：系统级故障，全局服务不可用
    /// </summary>
    Critical = '3'
}
