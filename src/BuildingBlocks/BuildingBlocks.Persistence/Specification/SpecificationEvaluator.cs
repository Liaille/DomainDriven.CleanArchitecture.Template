using BuildingBlocks.Core.Entities;
using BuildingBlocks.Core.SpecificationPattern;

namespace BuildingBlocks.Persistence.Specification;

/// <summary>
/// 规约执行器 EF Core 实现 (将规约表达式树转换为可执行查询)
/// </summary>
public class SpecificationEvaluator : ISpecificationEvaluator
{
    /// <summary>
    /// 将规约应用到输入查询对象
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <param name="inputQuery">输入查询对象</param>
    /// <param name="specification">规约条件</param>
    /// <param name="evaluateCriteria">是否评估 Where 条件</param>
    public IQueryable<TEntity> GetQuery<TEntity>(
        IQueryable<TEntity> inputQuery,
        ISpecification<TEntity> specification,
        bool evaluateCriteria = true)
        where TEntity : class, IEntity
    {
        ArgumentNullException.ThrowIfNull(inputQuery, nameof(inputQuery));
        if (specification is null) return inputQuery;

        var query = inputQuery;

        // 应用 Where 条件
        if (evaluateCriteria && specification.ToExpression() is not null)
        {
            query = query.Where(specification.ToExpression());
        }

        return query;
    }
}
