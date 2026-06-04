using Serilog.Core;

namespace BuildingBlocks.Logging.Abstractions;

/// <summary>
/// 日志丰富器统一抽象接口
/// 【设计背景】扩展Serilog原生ILogEventEnricher接口，增加唯一标识属性，实现日志丰富器的统一管理、动态配置与可观测性
/// 【核心职责】为日志事件添加上下文属性 (如TraceId、UserId、TenantId、请求信息等)，增强日志的可追溯性与诊断能力
/// 【设计原则】单一职责 (每个丰富器仅负责添加一类上下文属性)、可插拔 (通过DI容器动态注册/移除)、无侵入 (不修改业务代码)
/// 【强制规范】所有自定义日志丰富器必须实现此接口，禁止直接实现Serilog.ILogEventEnricher
/// 【使用方式】通过DI容器注册后，自动集成到Serilog日志管道，所有日志事件都会经过已注册的丰富器处理
/// </summary>
public interface ILogEnricher : ILogEventEnricher
{
    /// <summary>
    /// 日志丰富器的唯一标识名称
    /// 【核心作用】
    /// 1. 配置管理: 在appsettings.json中通过名称启用/禁用指定丰富器
    /// 2. 可观测性: 在系统日志和监控中标记哪些丰富器已应用
    /// 3. 动态管理: 支持运行时动态注册/移除指定名称的丰富器
    /// 4. 调试排查: 快速定位日志属性缺失或异常的来源
    /// 【命名规范】采用帕斯卡命名法，清晰描述功能，如"TraceIdEnricher"、"UserContextEnricher"、"TenantContextEnricher"
    /// </summary>
    /// <value>丰富器的唯一标识字符串，不可为null或空白</value>
    string Name { get; }
}
