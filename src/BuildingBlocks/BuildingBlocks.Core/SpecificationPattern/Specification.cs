using System.Linq.Expressions;

namespace BuildingBlocks.Core.SpecificationPattern;

/// <summary>
/// 规约基类 (提供规约组合逻辑)
/// </summary>
/// <typeparam name="T">实体类型</typeparam>
public abstract class Specification<T> : ISpecification<T>
{
    /// <summary>
    /// 判断实体是否满足规约条件
    /// </summary>
    public abstract bool IsSatisfiedBy(T entity);

    /// <summary>
    /// 将规约转换为表达式树
    /// </summary>
    public abstract Expression<Func<T, bool>> ToExpression();

    /// <summary>
    /// 与操作 (两个规约都必须满足)
    /// </summary>
    public static Specification<T> operator &(Specification<T> left, Specification<T> right)
    {
        return new AndSpecification<T>(left, right);
    }

    /// <summary>
    /// 或操作 (满足任意一个规约即可)
    /// </summary>
    public static Specification<T> operator |(Specification<T> left, Specification<T> right)
    {
        return new OrSpecification<T>(left, right);
    }

    /// <summary>
    /// 非操作 (取反)
    /// </summary>
    public static Specification<T> operator !(Specification<T> specification)
    {
        return new NotSpecification<T>(specification);
    }

    /// <summary>
    /// 隐式转换为表达式树
    /// </summary>
    public static implicit operator Expression<Func<T, bool>>(Specification<T> specification)
    {
        return specification.ToExpression();
    }
}

/// <summary>
/// 与规约
/// </summary>
internal class AndSpecification<T> : Specification<T>
{
    private readonly Specification<T> _left;
    private readonly Specification<T> _right;

    public AndSpecification(Specification<T> left, Specification<T> right)
    {
        _left = left;
        _right = right;
    }

    public override bool IsSatisfiedBy(T entity)
    {
        return _left.IsSatisfiedBy(entity) && _right.IsSatisfiedBy(entity);
    }

    public override Expression<Func<T, bool>> ToExpression()
    {
        var leftExpr = _left.ToExpression();
        var rightExpr = _right.ToExpression();
        var parameterExpr = Expression.Parameter(typeof(T));

        var body = Expression.AndAlso(
            Expression.Invoke(leftExpr, parameterExpr),
            Expression.Invoke(rightExpr, parameterExpr)
        );

        return Expression.Lambda<Func<T, bool>>(body, parameterExpr);
    }
}

/// <summary>
/// 或规约
/// </summary>
internal class OrSpecification<T> : Specification<T>
{
    private readonly Specification<T> _left;
    private readonly Specification<T> _right;

    public OrSpecification(Specification<T> left, Specification<T> right)
    {
        _left = left;
        _right = right;
    }

    public override bool IsSatisfiedBy(T entity)
    {
        return _left.IsSatisfiedBy(entity) || _right.IsSatisfiedBy(entity);
    }

    public override Expression<Func<T, bool>> ToExpression()
    {
        var leftExpr = _left.ToExpression();
        var rightExpr = _right.ToExpression();
        var parameterExpr = Expression.Parameter(typeof(T));

        var body = Expression.OrElse(
            Expression.Invoke(leftExpr, parameterExpr),
            Expression.Invoke(rightExpr, parameterExpr)
        );

        return Expression.Lambda<Func<T, bool>>(body, parameterExpr);
    }
}

/// <summary>
/// 非规约
/// </summary>
internal class NotSpecification<T> : Specification<T>
{
    private readonly Specification<T> _specification;

    public NotSpecification(Specification<T> specification)
    {
        _specification = specification;
    }

    public override bool IsSatisfiedBy(T entity)
    {
        return !_specification.IsSatisfiedBy(entity);
    }

    public override Expression<Func<T, bool>> ToExpression()
    {
        var expr = _specification.ToExpression();
        var parameterExpr = Expression.Parameter(typeof(T));

        var body = Expression.Not(Expression.Invoke(expr, parameterExpr));

        return Expression.Lambda<Func<T, bool>>(body, parameterExpr);
    }
}
