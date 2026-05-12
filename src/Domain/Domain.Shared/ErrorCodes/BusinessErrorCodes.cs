namespace Domain.Shared.ErrorCodes;

/// <summary>
/// 业务错误码
/// <para>号段划分规则:</para>
/// <list type="bullet">
/// <item>20000 ~ 29999 = 业务级错误 (参数、状态、金额、日期、业务规则)</item>
/// </list>
/// <para>强制约束：仅用于领域层业务规则校验失败</para>
/// </summary>
public static class BusinessErrorCodes
{
    #region 通用业务错误 20000 ~ 20999
    /// <summary>
    /// 必填项不能为空
    /// </summary>
    public const int RequiredValueNotEmpty = 20000;

    /// <summary>
    /// 数据格式无效
    /// </summary>
    public const int InvalidDataFormat = 20001;

    /// <summary>
    /// 数值超出允许范围
    /// </summary>
    public const int ValueOutOfRange = 20002;

    /// <summary>
    /// 字符串长度超出限制
    /// </summary>
    public const int StringLengthExceeded = 20003;
    #endregion

    #region 状态转换错误 20100 ~ 20199
    /// <summary>
    /// 不允许的状态转换
    /// </summary>
    public const int InvalidStateTransition = 20100;
    #endregion

    #region 金额与币种错误 20200 ~ 20299
    /// <summary>
    /// 币种不匹配
    /// </summary>
    public const int CurrencyMismatch = 20200;

    /// <summary>
    /// 金额不能小于等于 0
    /// </summary>
    public const int AmountInvalid = 20201;
    #endregion

    #region 日期范围错误 20300 ~ 20399
    /// <summary>
    /// 日期范围无效 (开始时间 > 结束时间)
    /// </summary>
    public const int InvalidDateRange = 20300;
    #endregion
}
