namespace BuildingBlocks.Core.Entities;

/// <summary>
/// 具备软删除功能的实体接口
/// </summary>
public interface ISoftDeletableEntity
{
    /// <summary>
    /// 标识数据是否已被软删除
    /// </summary>
    bool IsDeleted { get; set; }

    /// <summary>
    /// 数据删除人ID
    /// </summary>
    object? DeleterId { get; set; }

    /// <summary>
    /// 数据删除时间，UTC格式
    /// </summary>
    DateTime? DeletionTime { get; set; }
}
