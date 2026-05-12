using System.Linq.Expressions;

namespace BuildingBlocks.Core.SpecificationPattern;

/// <summary>
/// 查询规约抽象基类
/// </summary>
/// <remarks>
/// 实现 IQuerySpecification 接口，提供查询条件、排序、分页、导航属性加载、实体跟踪等查询配置的统一封装
/// </remarks>
/// <typeparam name="T">领域实体类型</typeparam>
public abstract class QuerySpecification<T> : IQuerySpecification<T>
{
    /// <summary>
    /// 导航属性预加载列表 (表达式树形式)
    /// </summary>
    public List<Expression<Func<T, object>>> Includes { get; } = [];

    /// <summary>
    /// 导航属性预加载列表 (字符串形式，用于多级嵌套)
    /// </summary>
    public List<string> IncludeStrings { get; } = [];

    /// <summary>
    /// 升序排序表达式
    /// </summary>
    public Expression<Func<T, object>>? OrderBy { get; private set; }

    /// <summary>
    /// 降序排序表达式
    /// </summary>
    public Expression<Func<T, object>>? OrderByDescending { get; private set; }

    /// <summary>
    /// 跳过的记录数 (分页用)
    /// </summary>
    public int Skip { get; private set; }

    /// <summary>
    /// 取的记录数 (分页用)
    /// </summary>
    public int Take { get; private set; }

    /// <summary>
    /// 转换为EF Core可执行的查询条件表达式树
    /// </summary>
    /// <returns>实体筛选条件表达式树</returns>
    public abstract Expression<Func<T, bool>> ToExpression();

    #region 流畅API
    /// <summary>
    /// 添加导航属性预加载 (强类型表达式)
    /// </summary>
    protected QuerySpecification<T> Include(Expression<Func<T, object>> includeExpression)
    {
        Includes.Add(includeExpression);
        return this;
    }

    /// <summary>
    /// 添加导航属性预加载 (字符串形式，支持多级嵌套)
    /// </summary>
    protected QuerySpecification<T> Include(string includeString)
    {
        IncludeStrings.Add(includeString);
        return this;
    }

    /// <summary>
    /// 设置升序排序规则
    /// </summary>
    protected QuerySpecification<T> OrderByAsc(Expression<Func<T, object>> orderByExpression)
    {
        OrderBy = orderByExpression;
        return this;
    }

    /// <summary>
    /// 设置降序排序规则
    /// </summary>
    protected QuerySpecification<T> OrderByDesc(Expression<Func<T, object>> orderByDescendingExpression)
    {
        OrderByDescending = orderByDescendingExpression;
        return this;
    }

    /// <summary>
    /// 设置分页参数
    /// </summary>
    protected QuerySpecification<T> Paginate(int skip, int take)
    {
        Skip = skip;
        Take = take;
        return this;
    }
    #endregion
}
