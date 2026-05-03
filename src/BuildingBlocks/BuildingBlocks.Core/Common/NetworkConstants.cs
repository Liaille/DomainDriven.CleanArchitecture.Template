namespace BuildingBlocks.Core.Common;

/// <summary>
/// 网络通信相关默认配置
/// 定义HTTP请求、服务调用的默认行为参数
/// 统一网络通信行为，避免零散配置
/// </summary>
public static class NetworkConstants
{
    /// <summary>
    /// HTTP请求默认超时时间（单位：秒）
    /// 接口调用默认30秒超时，防止请求长时间阻塞
    /// 可根据具体业务场景在调用端覆盖此配置
    /// </summary>
    public const int DefaultHttpTimeoutSeconds = 30;

    /// <summary>
    /// HTTP客户端默认连接超时时间（单位：秒）
    /// 建立TCP连接的默认超时时间为10秒
    /// 避免连接尝试长时间等待
    /// </summary>
    public const int DefaultHttpConnectTimeoutSeconds = 10;

    /// <summary>
    /// HTTP请求默认重试次数
    /// 网络异常、超时异常、限流异常的默认重试次数为3次
    /// 仅对幂等请求执行重试
    /// </summary>
    public const int DefaultHttpRetryCount = 3;

    /// <summary>
    /// HTTP客户端默认最大连接数
    /// 单个HTTP客户端实例允许的最大并发连接数为100
    /// 避免连接数耗尽导致性能问题
    /// </summary>
    public const int DefaultHttpMaxConnectionsPerServer = 100;
}
