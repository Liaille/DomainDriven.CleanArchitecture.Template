namespace Domain.Shared.Enums;

/// <summary>
/// 币种枚举
/// 遵循 ISO 4217 国际标准，包含全球完整主流货币
/// 枚举值 = ISO 4217 数字代码
/// 枚举名称 = ISO 4217 字母代码(大写)
/// </summary>
public enum Currency
{
    #region 亚洲货币
    /// <summary>
    /// 人民币 - 中国
    /// <para>ISO 4217: CNY</para>
    /// <para>数字代码: 156</para>
    /// </summary>
    CNY = 156,

    /// <summary>
    /// 港币 - 中国香港
    /// <para>ISO 4217: HKD</para>
    /// <para>数字代码: 344</para>
    /// </summary>
    HKD = 344,

    /// <summary>
    /// 澳门元 - 中国澳门
    /// <para>ISO 4217: MOP</para>
    /// <para>数字代码: 446</para>
    /// </summary>
    MOP = 446,

    /// <summary>
    /// 新台币 - 中国台湾
    /// <para>ISO 4217: TWD</para>
    /// <para>数字代码: 901</para>
    /// </summary>
    TWD = 901,

    /// <summary>
    /// 日元 - 日本
    /// <para>ISO 4217: JPY</para>
    /// <para>数字代码: 392</para>
    /// </summary>
    JPY = 392,

    /// <summary>
    /// 韩元 - 韩国
    /// <para>ISO 4217: KRW</para>
    /// <para>数字代码: 410</para>
    /// </summary>
    KRW = 410,

    /// <summary>
    /// 新加坡元 - 新加坡
    /// <para>ISO 4217: SGD</para>
    /// <para>数字代码: 702</para>
    /// </summary>
    SGD = 702,

    /// <summary>
    /// 印度卢比 - 印度
    /// <para>ISO 4217: INR</para>
    /// <para>数字代码: 356</para>
    /// </summary>
    INR = 356,

    /// <summary>
    /// 印尼卢比 - 印度尼西亚
    /// <para>ISO 4217: IDR</para>
    /// <para>数字代码: 360</para>
    /// </summary>
    IDR = 360,

    /// <summary>
    /// 马来西亚林吉特 - 马来西亚
    /// <para>ISO 4217: MYR</para>
    /// <para>数字代码: 458</para>
    /// </summary>
    MYR = 458,

    /// <summary>
    /// 泰国泰铢 - 泰国
    /// <para>ISO 4217: THB</para>
    /// <para>数字代码: 764</para>
    /// </summary>
    THB = 764,

    /// <summary>
    /// 菲律宾比索 - 菲律宾
    /// <para>ISO 4217: PHP</para>
    /// <para>数字代码: 608</para>
    /// </summary>
    PHP = 608,

    /// <summary>
    /// 越南盾 - 越南
    /// <para>ISO 4217: VND</para>
    /// <para>数字代码: 704</para>
    /// </summary>
    VND = 704,

    /// <summary>
    /// 孟加拉塔卡 - 孟加拉国
    /// <para>ISO 4217: BDT</para>
    /// <para>数字代码: 50</para>
    /// </summary>
    BDT = 50,

    /// <summary>
    /// 巴基斯坦卢比 - 巴基斯坦
    /// <para>ISO 4217: PKR</para>
    /// <para>数字代码: 586</para>
    /// </summary>
    PKR = 586,

    /// <summary>
    /// 斯里兰卡卢比 - 斯里兰卡
    /// <para>ISO 4217: LKR</para>
    /// <para>数字代码: 144</para>
    /// </summary>
    LKR = 144,

    /// <summary>
    /// 尼泊尔卢比 - 尼泊尔
    /// <para>ISO 4217: NPR</para>
    /// <para>数字代码: 524</para>
    /// </summary>
    NPR = 524,

    /// <summary>
    /// 哈萨克斯坦坚戈 - 哈萨克斯坦
    /// <para>ISO 4217: KZT</para>
    /// <para>数字代码: 398</para>
    /// </summary>
    KZT = 398,

    /// <summary>
    /// 乌兹别克斯坦索姆 - 乌兹别克斯坦
    /// <para>ISO 4217: UZS</para>
    /// <para>数字代码: 860</para>
    /// </summary>
    UZS = 860,

    /// <summary>
    /// 阿联酋迪拉姆 - 阿联酋
    /// <para>ISO 4217: AED</para>
    /// <para>数字代码: 784</para>
    /// </summary>
    AED = 784,

    /// <summary>
    /// 沙特里亚尔 - 沙特阿拉伯
    /// <para>ISO 4217: SAR</para>
    /// <para>数字代码: 682</para>
    /// </summary>
    SAR = 682,

    /// <summary>
    /// 卡塔尔里亚尔 - 卡塔尔
    /// <para>ISO 4217: QAR</para>
    /// <para>数字代码: 634</para>
    /// </summary>
    QAR = 634,

    /// <summary>
    /// 科威特第纳尔 - 科威特
    /// <para>ISO 4217: KWD</para>
    /// <para>数字代码: 414</para>
    /// </summary>
    KWD = 414,

    /// <summary>
    /// 阿曼里亚尔 - 阿曼
    /// <para>ISO 4217: OMR</para>
    /// <para>数字代码: 512</para>
    /// </summary>
    OMR = 512,

    /// <summary>
    /// 巴林第纳尔 - 巴林
    /// <para>ISO 4217: BHD</para>
    /// <para>数字代码: 48</para>
    /// </summary>
    BHD = 48,

    /// <summary>
    /// 约旦第纳尔 - 约旦
    /// <para>ISO 4217: JOD</para>
    /// <para>数字代码: 400</para>
    /// </summary>
    JOD = 400,

    /// <summary>
    /// 以色列新谢克尔 - 以色列
    /// <para>ISO 4217: ILS</para>
    /// <para>数字代码: 376</para>
    /// </summary>
    ILS = 376,
    #endregion

    #region 欧洲货币
    /// <summary>
    /// 欧元 - 欧盟
    /// <para>ISO 4217: EUR</para>
    /// <para>数字代码: 978</para>
    /// </summary>
    EUR = 978,

    /// <summary>
    /// 英镑 - 英国
    /// <para>ISO 4217: GBP</para>
    /// <para>数字代码: 826</para>
    /// </summary>
    GBP = 826,

    /// <summary>
    /// 瑞士法郎 - 瑞士
    /// <para>ISO 4217: CHF</para>
    /// <para>数字代码: 756</para>
    /// </summary>
    CHF = 756,

    /// <summary>
    /// 俄罗斯卢布 - 俄罗斯
    /// <para>ISO 4217: RUB</para>
    /// <para>数字代码: 643</para>
    /// </summary>
    RUB = 643,

    /// <summary>
    /// 瑞典克朗 - 瑞典
    /// <para>ISO 4217: SEK</para>
    /// <para>数字代码: 752</para>
    /// </summary>
    SEK = 752,

    /// <summary>
    /// 挪威克朗 - 挪威
    /// <para>ISO 4217: NOK</para>
    /// <para>数字代码: 578</para>
    /// </summary>
    NOK = 578,

    /// <summary>
    /// 丹麦克朗 - 丹麦
    /// <para>ISO 4217: DKK</para>
    /// <para>数字代码: 208</para>
    /// </summary>
    DKK = 208,

    /// <summary>
    /// 波兰兹罗提 - 波兰
    /// <para>ISO 4217: PLN</para>
    /// <para>数字代码: 985</para>
    /// </summary>
    PLN = 985,

    /// <summary>
    /// 捷克克朗 - 捷克
    /// <para>ISO 4217: CZK</para>
    /// <para>数字代码: 203</para>
    /// </summary>
    CZK = 203,

    /// <summary>
    /// 匈牙利福林 - 匈牙利
    /// <para>ISO 4217: HUF</para>
    /// <para>数字代码: 348</para>
    /// </summary>
    HUF = 348,

    /// <summary>
    /// 罗马尼亚列伊 - 罗马尼亚
    /// <para>ISO 4217: RON</para>
    /// <para>数字代码: 946</para>
    /// </summary>
    RON = 946,

    /// <summary>
    /// 保加利亚列弗 - 保加利亚
    /// <para>ISO 4217: BGN</para>
    /// <para>数字代码: 975</para>
    /// </summary>
    BGN = 975,

    /// <summary>
    /// 土耳其里拉 - 土耳其
    /// <para>ISO 4217: TRY</para>
    /// <para>数字代码: 949</para>
    /// </summary>
    TRY = 949,

    /// <summary>
    /// 乌克兰格里夫纳 - 乌克兰
    /// <para>ISO 4217: UAH</para>
    /// <para>数字代码: 980</para>
    /// </summary>
    UAH = 980,

    /// <summary>
    /// 冰岛克朗 - 冰岛
    /// <para>ISO 4217: ISK</para>
    /// <para>数字代码: 352</para>
    /// </summary>
    ISK = 352,
    #endregion

    #region 美洲货币
    /// <summary>
    /// 美元 - 美国
    /// <para>ISO 4217: USD</para>
    /// <para>数字代码: 840</para>
    /// </summary>
    USD = 840,

    /// <summary>
    /// 加元 - 加拿大
    /// <para>ISO 4217: CAD</para>
    /// <para>数字代码: 124</para>
    /// </summary>
    CAD = 124,

    /// <summary>
    /// 墨西哥比索 - 墨西哥
    /// <para>ISO 4217: MXN</para>
    /// <para>数字代码: 484</para>
    /// </summary>
    MXN = 484,

    /// <summary>
    /// 巴西雷亚尔 - 巴西
    /// <para>ISO 4217: BRL</para>
    /// <para>数字代码: 986</para>
    /// </summary>
    BRL = 986,

    /// <summary>
    /// 阿根廷比索 - 阿根廷
    /// <para>ISO 4217: ARS</para>
    /// <para>数字代码: 32</para>
    /// </summary>
    ARS = 32,

    /// <summary>
    /// 智利比索 - 智利
    /// <para>ISO 4217: CLP</para>
    /// <para>数字代码: 152</para>
    /// </summary>
    CLP = 152,

    /// <summary>
    /// 哥伦比亚比索 - 哥伦比亚
    /// <para>ISO 4217: COP</para>
    /// <para>数字代码: 170</para>
    /// </summary>
    COP = 170,

    /// <summary>
    /// 秘鲁索尔 - 秘鲁
    /// <para>ISO 4217: PEN</para>
    /// <para>数字代码: 604</para>
    /// </summary>
    PEN = 604,

    /// <summary>
    /// 委内瑞拉玻利瓦尔 - 委内瑞拉
    /// <para>ISO 4217: VES</para>
    /// <para>数字代码: 928</para>
    /// </summary>
    VES = 928,

    /// <summary>
    /// 古巴比索 - 古巴
    /// <para>ISO 4217: CUP</para>
    /// <para>数字代码: 192</para>
    /// </summary>
    CUP = 192,
    #endregion

    #region 大洋洲货币
    /// <summary>
    /// 澳元 - 澳大利亚
    /// <para>ISO 4217: AUD</para>
    /// <para>数字代码: 36</para>
    /// </summary>
    AUD = 36,

    /// <summary>
    /// 新西兰元 - 新西兰
    /// <para>ISO 4217: NZD</para>
    /// <para>数字代码: 554</para>
    /// </summary>
    NZD = 554,

    /// <summary>
    /// 斐济元 - 斐济
    /// <para>ISO 4217: FJD</para>
    /// <para>数字代码: 242</para>
    /// </summary>
    FJD = 242,

    /// <summary>
    /// 巴布亚新几内亚基那 - 巴布亚新几内亚
    /// <para>ISO 4217: PGK</para>
    /// <para>数字代码: 598</para>
    /// </summary>
    PGK = 598,
    #endregion

    #region 非洲货币
    /// <summary>
    /// 南非兰特 - 南非
    /// <para>ISO 4217: ZAR</para>
    /// <para>数字代码: 710</para>
    /// </summary>
    ZAR = 710,

    /// <summary>
    /// 埃及镑 - 埃及
    /// <para>ISO 4217: EGP</para>
    /// <para>数字代码: 818</para>
    /// </summary>
    EGP = 818,

    /// <summary>
    /// 尼日利亚奈拉 - 尼日利亚
    /// <para>ISO 4217: NGN</para>
    /// <para>数字代码: 566</para>
    /// </summary>
    NGN = 566,

    /// <summary>
    /// 肯尼亚先令 - 肯尼亚
    /// <para>ISO 4217: KES</para>
    /// <para>数字代码: 404</para>
    /// </summary>
    KES = 404,

    /// <summary>
    /// 摩洛哥迪拉姆 - 摩洛哥
    /// <para>ISO 4217: MAD</para>
    /// <para>数字代码: 504</para>
    /// </summary>
    MAD = 504,

    /// <summary>
    /// 突尼斯第纳尔 - 突尼斯
    /// <para>ISO 4217: TND</para>
    /// <para>数字代码: 788</para>
    /// </summary>
    TND = 788,

    /// <summary>
    /// 加纳塞地 - 加纳
    /// <para>ISO 4217: GHS</para>
    /// <para>数字代码: 936</para>
    /// </summary>
    GHS = 936,

    /// <summary>
    /// 坦桑尼亚先令 - 坦桑尼亚
    /// <para>ISO 4217: TZS</para>
    /// <para>数字代码: 834</para>
    /// </summary>
    TZS = 834,

    /// <summary>
    /// 乌干达先令 - 乌干达
    /// <para>ISO 4217: UGX</para>
    /// <para>数字代码: 800</para>
    /// </summary>
    UGX = 800,
    #endregion

    #region 通用/特殊货币
    /// <summary>
    /// 特别提款权 - IMF
    /// <para>ISO 4217: XDR</para>
    /// <para>数字代码: 960</para>
    /// </summary>
    XDR = 960,

    /// <summary>
    /// 贵金属 - 黄金
    /// <para>ISO 4217: XAU</para>
    /// <para>数字代码: 959</para>
    /// </summary>
    XAU = 959,

    /// <summary>
    /// 贵金属 - 白银
    /// <para>ISO 4217: XAG</para>
    /// <para>数字代码: 961</para>
    /// </summary>
    XAG = 961
    #endregion
}