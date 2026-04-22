using BuildingBlocks.Core.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace BuildingBlocks.Persistence.UnitOfWork;

/// <summary>
/// EF Core 工作单元实现 (维护事务一致性，统一提交变更)
/// </summary>
/// <typeparam name="TDbContext">数据库上下文类型</typeparam>
/// <param name="dbContext">数据库上下文实例</param>
public class EfCoreUnitOfWork<TDbContext>(TDbContext dbContext)
    where TDbContext : DbContext
{
    
}
