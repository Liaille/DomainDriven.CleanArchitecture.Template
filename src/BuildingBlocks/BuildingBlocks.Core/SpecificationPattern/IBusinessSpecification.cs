namespace BuildingBlocks.Core.SpecificationPattern;

/// <summary>
/// 规约接口
/// <para>唯一职责: 判断内存中的领域对象是否满足业务规则</para>
/// </summary>
/// <typeparam name="T">领域实体类型</typeparam>
public interface IBusinessSpecification<in T>
{
    /// <summary>
    /// 判断实体是否满足当前业务规则
    /// </summary>
    /// <param name="entity">已加载到内存的领域实体</param>
    /// <returns>满足规则返回true，否则返回false</returns>
    bool IsSatisfiedBy(T entity);
}

/// <summary>
/// 业务规约抽象基类
/// <para>提供规约组合运算符重载 (And/Or/Not)</para>
/// </summary>
/// <typeparam name="T">领域实体类型</typeparam>
public abstract class BusinessSpecification<T> : IBusinessSpecification<T>
{
    public abstract bool IsSatisfiedBy(T entity);

    /// <summary>
    /// 与操作: 两个业务规则必须同时满足
    /// </summary>
    /// <param name="left">左规约</param>
    /// <param name="right">右规约</param>
    /// <returns>返回一个新的规约，表示两个规约的与操作</returns>
    public static BusinessSpecification<T> operator &(BusinessSpecification<T> left, BusinessSpecification<T> right)
    {
        return new AndBusinessSpecification<T>(left, right);
    }

    /// <summary>
    /// 或操作: 满足任意一个业务规则即可
    /// </summary>
    /// <param name="left">左规约</param>
    /// <param name="right">右规约</param>
    /// <returns>返回一个新的规约，表示两个规约的或操作</returns>
    public static BusinessSpecification<T> operator |(
        BusinessSpecification<T> left,
        BusinessSpecification<T> right)
    {
        return new OrBusinessSpecification<T>(left, right);
    }

    /// <summary>
    /// 非操作: 业务规则取反
    /// </summary>
    /// <param name="specification">要取反的规约</param>
    /// <returns>返回一个新的规约，表示规约的非操作</returns>
    public static BusinessSpecification<T> operator !(BusinessSpecification<T> specification)
    {
        return new NotBusinessSpecification<T>(specification);
    }
}

/// <summary>
/// 与组合规约
/// </summary>
/// <typeparam name="T">领域实体类型</typeparam>
/// <param name="left">左规约</param>
/// <param name="right">右规约</param>
internal class AndBusinessSpecification<T>(
    BusinessSpecification<T> left,
    BusinessSpecification<T> right)
    : BusinessSpecification<T>
{
    /// <summary>
    /// 判断实体是否同时满足两个规约条件
    /// </summary>
    /// <param name="entity">领域实体</param>
    /// <returns>满足规则返回true，否则返回false</returns>
    public override bool IsSatisfiedBy(T entity)
    {
        return left.IsSatisfiedBy(entity) && right.IsSatisfiedBy(entity);
    }
}

/// <summary>
/// 或组合规约
/// </summary>
/// <typeparam name="T">领域实体类型</typeparam>
/// <param name="left">左规约</param>
/// <param name="right">右规约</param>
internal class OrBusinessSpecification<T>(
    BusinessSpecification<T> left,
    BusinessSpecification<T> right)
    : BusinessSpecification<T>
{
    /// <summary>
    /// 判断实体是否满足至少一个规约条件
    /// </summary>
    /// <param name="entity">领域实体</param>
    /// <returns>满足规则返回true，否则返回false</returns>
    public override bool IsSatisfiedBy(T entity)
    {
        return left.IsSatisfiedBy(entity) || right.IsSatisfiedBy(entity);
    }
}

/// <summary>
/// 非组合规约
/// </summary>
/// <typeparam name="T">领域实体</typeparam>
/// <param name="specification">要取反的规约</param>
internal class NotBusinessSpecification<T>(BusinessSpecification<T> specification)
    : BusinessSpecification<T>
{
    /// <summary>
    /// 判断实体是否不满足指定规约条件
    /// </summary>
    /// <param name="entity">领域实体</param>
    /// <returns>不满足规则返回true，否则返回false</returns>
    public override bool IsSatisfiedBy(T entity)
    {
        return !specification.IsSatisfiedBy(entity);
    }
}
