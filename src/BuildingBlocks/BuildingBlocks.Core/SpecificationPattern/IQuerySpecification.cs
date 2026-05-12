using System.Linq.Expressions;

namespace BuildingBlocks.Core.SpecificationPattern;

/// <summary>
/// 查询规约接口
/// <para>唯一职责: 生成数据库查询条件与查询配置，不包含任何业务规则</para>
/// </summary>
/// <typeparam name="T">实体类型</typeparam>
public interface IQuerySpecification<T>
{
    /// <summary>
    /// 导航属性预加载列表 (表达式树形式)
    /// </summary>
    List<Expression<Func<T, object>>> Includes { get; }

    /// <summary>
    /// 导航属性预加载列表 (字符串形式，用于多级嵌套)
    /// </summary>
    List<string> IncludeStrings { get; }

    /// <summary>
    /// 升序排序表达式
    /// </summary>
    Expression<Func<T, object>>? OrderBy { get; }

    /// <summary>
    /// 降序排序表达式
    /// </summary>
    Expression<Func<T, object>>? OrderByDescending { get; }

    /// <summary>
    /// 跳过的记录数 (分页用)
    /// </summary>
    int Skip { get; }

    /// <summary>
    /// 取的记录数 (分页用)
    /// </summary>
    int Take { get; }

    /// <summary>
    /// 转换为EF Core可执行的查询条件表达式树
    /// </summary>
    /// <returns>实体筛选条件表达式树</returns>
    Expression<Func<T, bool>> ToExpression();
}
