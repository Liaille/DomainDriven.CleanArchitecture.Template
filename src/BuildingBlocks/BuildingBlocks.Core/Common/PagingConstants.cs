namespace BuildingBlocks.Core.Common;

/// <summary>
/// 分页查询相关常量
/// 定义分页行为的默认参数与边界约束
/// 统一分页查询行为，防止一次性查询过多数据
/// </summary>
public static class PagingConstants
{
    /// <summary>
    /// 分页查询默认页码
    /// 未指定页码时默认从第1页开始查询
    /// </summary>
    public const int DefaultPageNumber = 1;

    /// <summary>
    /// 分页查询默认每页记录数
    /// 未指定分页大小时默认返回10条数据
    /// </summary>
    public const int DefaultPageSize = 10;

    /// <summary>
    /// 分页查询允许的最小每页记录数
    /// 防止分页大小设置过小导致查询次数过多
    /// </summary>
    public const int MinPageSize = 1;

    /// <summary>
    /// 分页查询允许的最大每页记录数
    /// 防止一次性查询过多数据导致性能问题，上限为1000条
    /// </summary>
    public const int MaxPageSize = 1000;

    /// <summary>
    /// 数据导出时允许的最大每页记录数
    /// 导出场景下允许更大的分页大小，上限为10000条
    /// </summary>
    public const int MaxExportPageSize = 10000;
}
