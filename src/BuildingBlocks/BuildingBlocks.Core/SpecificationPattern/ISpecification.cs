using System.Linq.Expressions;

namespace BuildingBlocks.Core.SpecificationPattern;

/// <summary>
/// 规约接口 (DDD 核心模式，用于封装查询条件)
/// </summary>
/// <typeparam name="T">实体类型</typeparam>
public interface ISpecification<T>
{
    /// <summary>
    /// 判断实体是否满足规约条件
    /// </summary>
    bool IsSatisfiedBy(T entity);

    /// <summary>
    /// 将规约转换为表达式树 (用于 EF Core 查询)
    /// </summary>
    Expression<Func<T, bool>> ToExpression();
}
