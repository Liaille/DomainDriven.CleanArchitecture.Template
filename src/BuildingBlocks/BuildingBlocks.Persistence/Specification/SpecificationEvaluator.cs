using BuildingBlocks.Core.Entities;
using BuildingBlocks.Core.SpecificationPattern;
using Microsoft.EntityFrameworkCore;

namespace BuildingBlocks.Persistence.Specification;

/// <summary>
/// 规约求值器 (将规约转换为EF Core可执行的查询)
/// </summary>
public static class SpecificationEvaluator
{
    /// <summary>
    /// 将规约应用到IQueryable查询对象
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <param name="inputQuery">原始查询对象</param>
    /// <param name="specification">查询规约</param>
    public static IQueryable<TEntity> GetQuery<TEntity>(IQueryable<TEntity> inputQuery, ISpecification<TEntity> specification)
        where TEntity : class, IEntity
    {
        var query = inputQuery;

        // 应用规约的过滤条件
        if (specification.ToExpression() is not null) 
            query = query.Where(specification.ToExpression());

        // 应用跟踪设置 (默认不跟踪查询)
        query = query.AsNoTracking();

        return query;
    }
}
