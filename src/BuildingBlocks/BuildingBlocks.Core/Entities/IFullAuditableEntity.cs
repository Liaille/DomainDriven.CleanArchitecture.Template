namespace BuildingBlocks.Core.Entities;

/// <summary>
/// 具备完整审计功能的实体接口，包含创建、修改、删除信息
/// </summary>
public interface IFullAuditableEntity : IAuditableEntity, ISoftDeletableEntity
{
}
