using BuildingBlocks.Core.Entities;
using BuildingBlocks.Core.Repositories;
using BuildingBlocks.Core.SpecificationPattern;
using Microsoft.EntityFrameworkCore;

namespace BuildingBlocks.Persistence.Repository;

/// <summary>
/// EF Core 完整仓储实现 (兼容传统 CRUD 场景，非 CQRS 场景使用)
/// </summary>
/// <typeparam name="TEntity">聚合根类型</typeparam>
/// <typeparam name="TKey">主键类型</typeparam>
/// <typeparam name="TDbContext">数据库上下文类型</typeparam>
public class EfCoreRepository<TEntity, TKey, TDbContext>(
    TDbContext dbContext,
    ISpecificationEvaluator specificationEvaluator)
    : EfCoreBasicRepository<TEntity, TKey, TDbContext>(dbContext, specificationEvaluator), IRepository<TEntity, TKey>
    where TEntity : class, IAggregateRoot<TKey>
    where TDbContext : DbContext
{
}
