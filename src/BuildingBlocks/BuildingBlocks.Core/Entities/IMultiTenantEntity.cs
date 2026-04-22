namespace BuildingBlocks.Core.Entities;

/// <summary>
/// 具备多租户隔离的数据实体接口
/// </summary>
public interface IMultiTenantEntity
{
    /// <summary>
    /// 租户唯一标识
    /// </summary>
    object TenantId { get; set; }
}
