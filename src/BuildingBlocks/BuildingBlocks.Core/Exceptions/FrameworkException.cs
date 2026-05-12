namespace BuildingBlocks.Core.Exceptions;

/// <summary>
/// 框架/系统异常基类
/// <para>使用场景: 不可预期的系统级错误：配置错误、数据库连接失败、基础设施故障、未处理异常</para>
/// <para>监控规则: 触发告警，记录 Error 级别日志</para>
/// </summary>
public class FrameworkException : Exception
{
    /// <summary>
    /// 初始化框架异常
    /// </summary>
    /// <param name="message">异常描述</param>
    public FrameworkException(string message) : base(message)
    {
    }

    /// <summary>
    /// 初始化框架异常 (包含内部异常)
    /// </summary>
    /// <param name="message">异常描述</param>
    /// <param name="innerException">内部异常</param>
    public FrameworkException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
