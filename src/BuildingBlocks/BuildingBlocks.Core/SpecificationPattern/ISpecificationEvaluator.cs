using BuildingBlocks.Core.Entities;

namespace BuildingBlocks.Core.SpecificationPattern;

/// <summary>
/// 规约执行器接口
/// <para>唯一职责: 将查询规约应用到IQueryable对象，生成最终可执行的查询</para>
/// <para>设计原则: 抽象与实现分离，支持多种ORM无缝替换</para>
/// </summary>
public interface ISpecificationEvaluator
{
    /// <summary>
    /// 将查询规约应用到输入查询
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <param name="inputQuery">原始查询对象</param>
    /// <param name="specification">查询规约</param>
    /// <param name="evaluateCriteria">是否应用Where条件</param>
    /// <returns>应用规约后的最终查询对象</returns>
    IQueryable<TEntity> GetQuery<TEntity>(
        IQueryable<TEntity> inputQuery,
        IQuerySpecification<TEntity> specification,
        bool evaluateCriteria = true)
        where TEntity : class, IEntity;
}
