using BuildingBlocks.Core.Entities;

namespace BuildingBlocks.Core.SpecificationPattern;

/// <summary>
/// 规约执行器接口 (将规约应用到查询对象)
/// </summary>
/// <remarks>
/// 职责：将 ISpecification 规约转换为可执行查询
/// 实现：由具体持久化层 (EF Core/SqlSugar)提供
/// </remarks>
public interface ISpecificationEvaluator
{
    /// <summary>
    /// 将规约应用到输入查询上
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <param name="inputQuery">输入查询对象</param>
    /// <param name="specification">规约条件</param>
    /// <param name="evaluateCriteria">是否评估 Where 条件</param>
    /// <returns>应用规约后的查询对象</returns>
    IQueryable<TEntity> GetQuery<TEntity>(
        IQueryable<TEntity> inputQuery,
        ISpecification<TEntity> specification,
        bool evaluateCriteria = true)
        where TEntity : class, IEntity;
}
