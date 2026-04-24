using BuildingBlocks.Logging.AuditLogging;
using BuildingBlocks.Logging.BusinessLogging;
using System.Text.Json.Serialization;

namespace BuildingBlocks.Logging.Serilog;

/// <summary>
/// 日志模块 JSON 序列化上下文 (AOT兼容)
/// </summary>
[JsonSerializable(typeof(AuditOperationEvent))]
[JsonSerializable(typeof(BusinessLogEvent))]
[JsonSerializable(typeof(Dictionary<string, object>))]
public partial class LoggingJsonContext : JsonSerializerContext
{

}
