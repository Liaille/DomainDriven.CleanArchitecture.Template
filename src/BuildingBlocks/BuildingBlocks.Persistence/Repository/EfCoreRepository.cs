using BuildingBlocks.Core.Entities;
using BuildingBlocks.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BuildingBlocks.Persistence.Repository;

/// <summary>
/// EF Core 完整仓储实现 (兼容传统CRUD场景)
/// </summary>
/// <typeparam name="TEntity">聚合根类型</typeparam>
/// <typeparam name="TKey">主键类型</typeparam>
/// <typeparam name="TDbContext">数据库上下文类型</typeparam>
public class EfCoreRepository<TEntity, TKey, TDbContext>(TDbContext dbContext)
    : EfCoreBasicRepository<TEntity, TKey, TDbContext>(dbContext), IRepository<TEntity, TKey>
    where TEntity : class, IAggregateRoot<TKey>
    where TDbContext : DbContext
{
}
