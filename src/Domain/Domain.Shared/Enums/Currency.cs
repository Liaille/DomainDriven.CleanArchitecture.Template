using System.ComponentModel;

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
    /// 巴林第纳尔 - 巴林
    /// <para>ISO 4217: BHD</para>
    /// <para>数字代码: 048</para>
    /// <para>符号: .د.ب</para>
    /// <para>小数位数: 3</para>
    /// </summary>
    [Description("BHD|BD|3")]
    BHD = 48,

    /// <summary>
    /// 孟加拉塔卡 - 孟加拉国
    /// <para>ISO 4217: BDT</para>
    /// <para>数字代码: 050</para>
    /// <para>符号: ৳</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("BDT|৳|2")]
    BDT = 50,

    /// <summary>
    /// 亚美尼亚德拉姆 - 亚美尼亚
    /// <para>ISO 4217: AMD</para>
    /// <para>数字代码: 051</para>
    /// <para>符号: ֏</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("AMD|֏|2")]
    AMD = 51,

    /// <summary>
    /// 不丹努尔特鲁姆 - 不丹
    /// <para>ISO 4217: BTN</para>
    /// <para>数字代码: 064</para>
    /// <para>符号: Nu.</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("BTN|Nu.|2")]
    BTN = 64,

    /// <summary>
    /// 文莱元 - 文莱
    /// <para>ISO 4217: BND</para>
    /// <para>数字代码: 096</para>
    /// <para>符号: B$</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("BND|B$|2")]
    BND = 96,

    /// <summary>
    /// 柬埔寨瑞尔 - 柬埔寨
    /// <para>ISO 4217: KHR</para>
    /// <para>数字代码: 116</para>
    /// <para>符号: ៛</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("KHR|៛|2")]
    KHR = 116,

    /// <summary>
    /// 斯里兰卡卢比 - 斯里兰卡
    /// <para>ISO 4217: LKR</para>
    /// <para>数字代码: 144</para>
    /// <para>符号: රු</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("LKR|රු|2")]
    LKR = 144,

    /// <summary>
    /// 人民币 - 中国
    /// <para>ISO 4217: CNY</para>
    /// <para>数字代码: 156</para>
    /// <para>符号: ¥</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("CNY|¥|2")]
    CNY = 156,

    /// <summary>
    /// 厄立特里亚纳克法 - 厄立特里亚
    /// <para>ISO 4217: ERN</para>
    /// <para>数字代码: 232</para>
    /// <para>符号: Nfk</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("ERN|Nfk|2")]
    ERN = 232,

    /// <summary>
    /// 港币 - 中国香港
    /// <para>ISO 4217: HKD</para>
    /// <para>数字代码: 344</para>
    /// <para>符号: HK$</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("HKD|HK$|2")]
    HKD = 344,

    /// <summary>
    /// 印度卢比 - 印度
    /// <para>ISO 4217: INR</para>
    /// <para>数字代码: 356</para>
    /// <para>符号: ₹</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("INR|₹|2")]
    INR = 356,

    /// <summary>
    /// 印尼卢比 - 印度尼西亚
    /// <para>ISO 4217: IDR</para>
    /// <para>数字代码: 360</para>
    /// <para>符号: Rp</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("IDR|Rp|2")]
    IDR = 360,

    /// <summary>
    /// 伊朗里亚尔 - 伊朗
    /// <para>ISO 4217: IRR</para>
    /// <para>数字代码: 364</para>
    /// <para>符号: ﷼</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("IRR|﷼|2")]
    IRR = 364,

    /// <summary>
    /// 伊拉克第纳尔 - 伊拉克
    /// <para>ISO 4217: IQD</para>
    /// <para>数字代码: 368</para>
    /// <para>符号: ع.د</para>
    /// <para>小数位数: 3</para>
    /// </summary>
    [Description("IQD|dinar|3")]
    IQD = 368,

    /// <summary>
    /// 以色列新谢克尔 - 以色列
    /// <para>ISO 4217: ILS</para>
    /// <para>数字代码: 376</para>
    /// <para>符号: ₪</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("ILS|₪|2")]
    ILS = 376,

    /// <summary>
    /// 日元 - 日本
    /// <para>ISO 4217: JPY</para>
    /// <para>数字代码: 392</para>
    /// <para>符号: ¥</para>
    /// <para>小数位数: 0</para>
    /// </summary>
    [Description("JPY|¥|0")]
    JPY = 392,

    /// <summary>
    /// 哈萨克斯坦坚戈 - 哈萨克斯坦
    /// <para>ISO 4217: KZT</para>
    /// <para>数字代码: 398</para>
    /// <para>符号: ₸</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("KZT|₸|2")]
    KZT = 398,

    /// <summary>
    /// 约旦第纳尔 - 约旦
    /// <para>ISO 4217: JOD</para>
    /// <para>数字代码: 400</para>
    /// <para>符号: د.ا</para>
    /// <para>小数位数: 3</para>
    /// </summary>
    [Description("JOD|د.ا|3")]
    JOD = 400,

    /// <summary>
    /// 朝鲜元 - 朝鲜
    /// <para>ISO 4217: KPW</para>
    /// <para>数字代码: 408</para>
    /// <para>符号: ₩</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("KPW|₩|2")]
    KPW = 408,

    /// <summary>
    /// 韩元 - 韩国
    /// <para>ISO 4217: KRW</para>
    /// <para>数字代码: 410</para>
    /// <para>符号: ₩</para>
    /// <para>小数位数: 0</para>
    /// </summary>
    [Description("KRW|₩|0")]
    KRW = 410,

    /// <summary>
    /// 科威特第纳尔 - 科威特
    /// <para>ISO 4217: KWD</para>
    /// <para>数字代码: 414</para>
    /// <para>符号: د.ك</para>
    /// <para>小数位数: 3</para>
    /// </summary>
    [Description("KWD|د.ك|3")]
    KWD = 414,

    /// <summary>
    /// 吉尔吉斯斯坦索姆 - 吉尔吉斯斯坦
    /// <para>ISO 4217: KGS</para>
    /// <para>数字代码: 417</para>
    /// <para>符号: с</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("KGS|с|2")]
    KGS = 417,

    /// <summary>
    /// 老挝基普 - 老挝
    /// <para>ISO 4217: LAK</para>
    /// <para>数字代码: 418</para>
    /// <para>符号: ₭</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("LAK|₭|2")]
    LAK = 418,

    /// <summary>
    /// 黎巴嫩镑 - 黎巴嫩
    /// <para>ISO 4217: LBP</para>
    /// <para>数字代码: 422</para>
    /// <para>符号: ل.ل</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("LBP|ل.ل|2")]
    LBP = 422,

    /// <summary>
    /// 澳门元 - 中国澳门
    /// <para>ISO 4217: MOP</para>
    /// <para>数字代码: 446</para>
    /// <para>符号: MOP$</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("MOP|MOP$|2")]
    MOP = 446,

    /// <summary>
    /// 马来西亚林吉特 - 马来西亚
    /// <para>ISO 4217: MYR</para>
    /// <para>数字代码: 458</para>
    /// <para>符号: RM</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("MYR|RM|2")]
    MYR = 458,

    /// <summary>
    /// 马尔代夫拉菲亚 - 马尔代夫
    /// <para>ISO 4217: MVR</para>
    /// <para>数字代码: 462</para>
    /// <para>符号: Rf</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("MVR|Rf|2")]
    MVR = 462,

    /// <summary>
    /// 蒙古图格里克 - 蒙古
    /// <para>ISO 4217: MNT</para>
    /// <para>数字代码: 496</para>
    /// <para>符号: ₮</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("MNT|₮|2")]
    MNT = 496,

    /// <summary>
    /// 阿曼里亚尔 - 阿曼
    /// <para>ISO 4217: OMR</para>
    /// <para>数字代码: 512</para>
    /// <para>符号: ر.ع.</para>
    /// <para>小数位数: 3</para>
    /// </summary>
    [Description("OMR|ر.ع.|3")]
    OMR = 512,

    /// <summary>
    /// 尼泊尔卢比 - 尼泊尔
    /// <para>ISO 4217: NPR</para>
    /// <para>数字代码: 524</para>
    /// <para>符号: रू</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("NPR|रू|2")]
    NPR = 524,

    /// <summary>
    /// 巴基斯坦卢比 - 巴基斯坦
    /// <para>ISO 4217: PKR</para>
    /// <para>数字代码: 586</para>
    /// <para>符号: ₨</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("PKR|₨|2")]
    PKR = 586,

    /// <summary>
    /// 菲律宾比索 - 菲律宾
    /// <para>ISO 4217: PHP</para>
    /// <para>数字代码: 608</para>
    /// <para>符号: ₱</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("PHP|₱|2")]
    PHP = 608,

    /// <summary>
    /// 卡塔尔里亚尔 - 卡塔尔
    /// <para>ISO 4217: QAR</para>
    /// <para>数字代码: 634</para>
    /// <para>符号: ر.ق</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("QAR|ر.ق|2")]
    QAR = 634,

    /// <summary>
    /// 沙特里亚尔 - 沙特阿拉伯
    /// <para>ISO 4217: SAR</para>
    /// <para>数字代码: 682</para>
    /// <para>符号: ﷼</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("SAR|﷼|2")]
    SAR = 682,

    /// <summary>
    /// 新加坡元 - 新加坡
    /// <para>ISO 4217: SGD</para>
    /// <para>数字代码: 702</para>
    /// <para>符号: S$</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("SGD|S$|2")]
    SGD = 702,

    /// <summary>
    /// 叙利亚镑 - 叙利亚
    /// <para>ISO 4217: SYP</para>
    /// <para>数字代码: 760</para>
    /// <para>符号: £S</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("SYP|£S|2")]
    SYP = 760,

    /// <summary>
    /// 泰铢 - 泰国
    /// <para>ISO 4217: THB</para>
    /// <para>数字代码: 764</para>
    /// <para>符号: ฿</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("THB|฿|2")]
    THB = 764,

    /// <summary>
    /// 阿联酋迪拉姆 - 阿联酋
    /// <para>ISO 4217: AED</para>
    /// <para>数字代码: 784</para>
    /// <para>符号: د.إ</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("AED|د.إ|2")]
    AED = 784,

    /// <summary>
    /// 土库曼斯坦马纳特 - 土库曼斯坦
    /// <para>ISO 4217: TMT</para>
    /// <para>数字代码: 934</para>
    /// <para>符号: m</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("TMT|m|2")]
    TMT = 934,

    /// <summary>
    /// 乌兹别克斯坦索姆 - 乌兹别克斯坦
    /// <para>ISO 4217: UZS</para>
    /// <para>数字代码: 860</para>
    /// <para>符号: сўм</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("UZS|сўм|2")]
    UZS = 860,

    /// <summary>
    /// 也门里亚尔 - 也门
    /// <para>ISO 4217: YER</para>
    /// <para>数字代码: 886</para>
    /// <para>符号: ﷼</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("YER|﷼|2")]
    YER = 886,

    /// <summary>
    /// 新台币 - 中国台湾
    /// <para>ISO 4217: TWD</para>
    /// <para>数字代码: 901</para>
    /// <para>符号: NT$</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("TWD|NT$|2")]
    TWD = 901,

    /// <summary>
    /// 阿塞拜疆马纳特 - 阿塞拜疆
    /// <para>ISO 4217: AZN</para>
    /// <para>数字代码: 944</para>
    /// <para>符号: ₼</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("AZN|₼|2")]
    AZN = 944,

    /// <summary>
    /// 土耳其里拉 - 土耳其
    /// <para>ISO 4217: TRY</para>
    /// <para>数字代码: 949</para>
    /// <para>符号: ₺</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("TRY|₺|2")]
    TRY = 949,

    /// <summary>
    /// 阿富汗尼 - 阿富汗
    /// <para>ISO 4217: AFN</para>
    /// <para>数字代码: 971</para>
    /// <para>符号: ؋</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("AFN|؋|2")]
    AFN = 971,

    /// <summary>
    /// 塔吉克斯坦索莫尼 - 塔吉克斯坦
    /// <para>ISO 4217: TJS</para>
    /// <para>数字代码: 972</para>
    /// <para>符号: ЅМ</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("TJS|ЅМ|2")]
    TJS = 972,

    /// <summary>
    /// 格鲁吉亚拉里 - 格鲁吉亚
    /// <para>ISO 4217: GEL</para>
    /// <para>数字代码: 981</para>
    /// <para>符号: ლ</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("GEL|ლ|2")]
    GEL = 981,
    #endregion

    #region 欧洲货币
    /// <summary>
    /// 阿尔巴尼亚列克 - 阿尔巴尼亚
    /// <para>ISO 4217: ALL</para>
    /// <para>数字代码: 008</para>
    /// <para>符号: L</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("ALL|L|2")]
    ALL = 8,

    /// <summary>
    /// 捷克克朗 - 捷克
    /// <para>ISO 4217: CZK</para>
    /// <para>数字代码: 203</para>
    /// <para>符号: Kč</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("CZK|Kč|2")]
    CZK = 203,

    /// <summary>
    /// 丹麦克朗 - 丹麦
    /// <para>ISO 4217: DKK</para>
    /// <para>数字代码: 208</para>
    /// <para>符号: kr</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("DKK|kr|2")]
    DKK = 208,

    /// <summary>
    /// 直布罗陀镑 - 直布罗陀
    /// <para>ISO 4217: GIP</para>
    /// <para>数字代码: 292</para>
    /// <para>符号: £</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("GIP|£|2")]
    GIP = 292,

    /// <summary>
    /// 匈牙利福林 - 匈牙利
    /// <para>ISO 4217: HUF</para>
    /// <para>数字代码: 348</para>
    /// <para>符号: Ft</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("HUF|Ft|2")]
    HUF = 348,

    /// <summary>
    /// 冰岛克朗 - 冰岛
    /// <para>ISO 4217: ISK</para>
    /// <para>数字代码: 352</para>
    /// <para>符号: kr</para>
    /// <para>小数位数: 0</para>
    /// </summary>
    [Description("ISK|kr|0")]
    ISK = 352,

    /// <summary>
    /// 摩尔多瓦列伊 - 摩尔多瓦
    /// <para>ISO 4217: MDL</para>
    /// <para>数字代码: 498</para>
    /// <para>符号: L</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("MDL|L|2")]
    MDL = 498,

    /// <summary>
    /// 挪威克朗 - 挪威
    /// <para>ISO 4217: NOK</para>
    /// <para>数字代码: 578</para>
    /// <para>符号: kr</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("NOK|kr|2")]
    NOK = 578,

    /// <summary>
    /// 俄罗斯卢布 - 俄罗斯
    /// <para>ISO 4217: RUB</para>
    /// <para>数字代码: 643</para>
    /// <para>符号: ₽</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("RUB|₽|2")]
    RUB = 643,

    /// <summary>
    /// 圣赫勒拿镑 - 圣赫勒拿
    /// <para>ISO 4217: SHP</para>
    /// <para>数字代码: 654</para>
    /// <para>符号: £</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("SHP|£|2")]
    SHP = 654,

    /// <summary>
    /// 瑞典克朗 - 瑞典
    /// <para>ISO 4217: SEK</para>
    /// <para>数字代码: 752</para>
    /// <para>符号: kr</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("SEK|kr|2")]
    SEK = 752,

    /// <summary>
    /// 瑞士法郎 - 瑞士
    /// <para>ISO 4217: CHF</para>
    /// <para>数字代码: 756</para>
    /// <para>符号: Fr</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("CHF|Fr|2")]
    CHF = 756,

    /// <summary>
    /// 英镑 - 英国
    /// <para>ISO 4217: GBP</para>
    /// <para>数字代码: 826</para>
    /// <para>符号: £</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("GBP|£|2")]
    GBP = 826,

    /// <summary>
    /// 塞尔维亚第纳尔 - 塞尔维亚
    /// <para>ISO 4217: RSD</para>
    /// <para>数字代码: 941</para>
    /// <para>符号: дин</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("RSD|дин|2")]
    RSD = 941,

    /// <summary>
    /// 罗马尼亚列伊 - 罗马尼亚
    /// <para>ISO 4217: RON</para>
    /// <para>数字代码: 946</para>
    /// <para>符号: lei</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("RON|lei|2")]
    RON = 946,

    /// <summary>
    /// 瑞士WIR欧元 - 瑞士
    /// <para>ISO 4217: CHE</para>
    /// <para>数字代码: 947</para>
    /// <para>符号: CHE</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("CHE|CHE|2")]
    CHE = 947,

    /// <summary>
    /// 瑞士WIR法郎 - 瑞士
    /// <para>ISO 4217: CHW</para>
    /// <para>数字代码: 948</para>
    /// <para>符号: CHW</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("CHW|CHW|2")]
    CHW = 948,

    /// <summary>
    /// 保加利亚列弗 - 保加利亚
    /// <para>ISO 4217: BGN</para>
    /// <para>数字代码: 975</para>
    /// <para>符号: лв</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("BGN|лв|2")]
    BGN = 975,

    /// <summary>
    /// 波黑可兑换马克 - 波斯尼亚和黑塞哥维那
    /// <para>ISO 4217: BAM</para>
    /// <para>数字代码: 977</para>
    /// <para>符号: KM</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("BAM|KM|2")]
    BAM = 977,

    /// <summary>
    /// 欧元 - 欧盟
    /// <para>ISO 4217: EUR</para>
    /// <para>数字代码: 978</para>
    /// <para>符号: €</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("EUR|€|2")]
    EUR = 978,

    /// <summary>
    /// 乌克兰格里夫纳 - 乌克兰
    /// <para>ISO 4217: UAH</para>
    /// <para>数字代码: 980</para>
    /// <para>符号: ₴</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("UAH|₴|2")]
    UAH = 980,

    /// <summary>
    /// 波兰兹罗提 - 波兰
    /// <para>ISO 4217: PLN</para>
    /// <para>数字代码: 985</para>
    /// <para>符号: zł</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("PLN|zł|2")]
    PLN = 985,
    #endregion

    #region 美洲货币
    /// <summary>
    /// 阿根廷比索 - 阿根廷
    /// <para>ISO 4217: ARS</para>
    /// <para>数字代码: 032</para>
    /// <para>符号: $</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("ARS|$|2")]
    ARS = 32,

    /// <summary>
    /// 巴哈马元 - 巴哈马
    /// <para>ISO 4217: BSD</para>
    /// <para>数字代码: 044</para>
    /// <para>符号: B$</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("BSD|B$|2")]
    BSD = 44,

    /// <summary>
    /// 巴巴多斯元 - 巴巴多斯
    /// <para>ISO 4217: BBD</para>
    /// <para>数字代码: 052</para>
    /// <para>符号: Bds$</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("BBD|Bds$|2")]
    BBD = 52,

    /// <summary>
    /// 百慕大元 - 百慕大
    /// <para>ISO 4217: BMD</para>
    /// <para>数字代码: 060</para>
    /// <para>符号: $</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("BMD|$|2")]
    BMD = 60,

    /// <summary>
    /// 玻利维亚诺 - 玻利维亚
    /// <para>ISO 4217: BOB</para>
    /// <para>数字代码: 068</para>
    /// <para>符号: Bs.</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("BOB|Bs.|2")]
    BOB = 68,

    /// <summary>
    /// 伯利兹元 - 伯利兹
    /// <para>ISO 4217: BZD</para>
    /// <para>数字代码: 084</para>
    /// <para>符号: BZ$</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("BZD|BZ$|2")]
    BZD = 84,

    /// <summary>
    /// 加元 - 加拿大
    /// <para>ISO 4217: CAD</para>
    /// <para>数字代码: 124</para>
    /// <para>符号: C$</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("CAD|C$|2")]
    CAD = 124,

    /// <summary>
    /// 开曼群岛元 - 开曼群岛
    /// <para>ISO 4217: KYD</para>
    /// <para>数字代码: 136</para>
    /// <para>符号: CI$</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("KYD|CI$|2")]
    KYD = 136,

    /// <summary>
    /// 智利比索 - 智利
    /// <para>ISO 4217: CLP</para>
    /// <para>数字代码: 152</para>
    /// <para>符号: $</para>
    /// <para>小数位数: 0</para>
    /// </summary>
    [Description("CLP|$|0")]
    CLP = 152,

    /// <summary>
    /// 哥伦比亚比索 - 哥伦比亚
    /// <para>ISO 4217: COP</para>
    /// <para>数字代码: 170</para>
    /// <para>符号: $</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("COP|$|2")]
    COP = 170,

    /// <summary>
    /// 哥斯达黎加科朗 - 哥斯达黎加
    /// <para>ISO 4217: CRC</para>
    /// <para>数字代码: 188</para>
    /// <para>符号: ₡</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("CRC|₡|2")]
    CRC = 188,

    /// <summary>
    /// 古巴比索 - 古巴
    /// <para>ISO 4217: CUP</para>
    /// <para>数字代码: 192</para>
    /// <para>符号: $MN</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("CUP|$MN|2")]
    CUP = 192,

    /// <summary>
    /// 多米尼加比索 - 多米尼加
    /// <para>ISO 4217: DOP</para>
    /// <para>数字代码: 214</para>
    /// <para>符号: RD$</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("DOP|RD$|2")]
    DOP = 214,

    /// <summary>
    /// 福克兰群岛镑 - 福克兰群岛
    /// <para>ISO 4217: FKP</para>
    /// <para>数字代码: 238</para>
    /// <para>符号: £</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("FKP|£|2")]
    FKP = 238,

    /// <summary>
    /// 危地马拉格查尔 - 危地马拉
    /// <para>ISO 4217: GTQ</para>
    /// <para>数字代码: 320</para>
    /// <para>符号: Q</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("GTQ|Q|2")]
    GTQ = 320,

    /// <summary>
    /// 圭亚那元 - 圭亚那
    /// <para>ISO 4217: GYD</para>
    /// <para>数字代码: 328</para>
    /// <para>符号: G$</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("GYD|G$|2")]
    GYD = 328,

    /// <summary>
    /// 海地古德 - 海地
    /// <para>ISO 4217: HTG</para>
    /// <para>数字代码: 332</para>
    /// <para>符号: G</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("HTG|G|2")]
    HTG = 332,

    /// <summary>
    /// 洪都拉斯伦皮拉 - 洪都拉斯
    /// <para>ISO 4217: HNL</para>
    /// <para>数字代码: 340</para>
    /// <para>符号: L</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("HNL|L|2")]
    HNL = 340,

    /// <summary>
    /// 牙买加元 - 牙买加
    /// <para>ISO 4217: JMD</para>
    /// <para>数字代码: 388</para>
    /// <para>符号: J$</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("JMD|J$|2")]
    JMD = 388,

    /// <summary>
    /// 墨西哥比索 - 墨西哥
    /// <para>ISO 4217: MXN</para>
    /// <para>数字代码: 484</para>
    /// <para>符号: $</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("MXN|$|2")]
    MXN = 484,

    /// <summary>
    /// 尼加拉瓜科多巴 - 尼加拉瓜
    /// <para>ISO 4217: NIO</para>
    /// <para>数字代码: 558</para>
    /// <para>符号: C$</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("NIO|C$|2")]
    NIO = 558,

    /// <summary>
    /// 巴拿马巴波亚 - 巴拿马
    /// <para>ISO 4217: PAB</para>
    /// <para>数字代码: 590</para>
    /// <para>符号: B/.</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("PAB|B/.|2")]
    PAB = 590,

    /// <summary>
    /// 巴拉圭瓜拉尼 - 巴拉圭
    /// <para>ISO 4217: PYG</para>
    /// <para>数字代码: 600</para>
    /// <para>符号: ₲</para>
    /// <para>小数位数: 0</para>
    /// </summary>
    [Description("PYG|₲|0")]
    PYG = 600,

    /// <summary>
    /// 秘鲁索尔 - 秘鲁
    /// <para>ISO 4217: PEN</para>
    /// <para>数字代码: 604</para>
    /// <para>符号: S/</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("PEN|S/|2")]
    PEN = 604,

    /// <summary>
    /// 加勒比盾 - 库拉索、荷属圣马丁
    /// <para>ISO 4217: XCG</para>
    /// <para>数字代码: 532</para>
    /// <para>符号: ƒ</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("XCG|ƒ|2")]
    XCG = 532,

    /// <summary>
    /// 特立尼达和多巴哥元 - 特立尼达和多巴哥
    /// <para>ISO 4217: TTD</para>
    /// <para>数字代码: 780</para>
    /// <para>符号: TT$</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("TTD|TT$|2")]
    TTD = 780,

    /// <summary>
    /// 美元 - 美国
    /// <para>ISO 4217: USD</para>
    /// <para>数字代码: 840</para>
    /// <para>符号: $</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("USD|$|2")]
    USD = 840,

    /// <summary>
    /// 乌拉圭比索 - 乌拉圭
    /// <para>ISO 4217: UYU</para>
    /// <para>数字代码: 858</para>
    /// <para>符号: $U</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("UYU|$U|2")]
    UYU = 858,

    /// <summary>
    /// 委内瑞拉数字玻利瓦尔 - 委内瑞拉
    /// <para>ISO 4217: VED</para>
    /// <para>数字代码: 926</para>
    /// <para>符号: Bs.D</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("VED|Bs.D|2")]
    VED = 926,

    /// <summary>
    /// 委内瑞拉玻利瓦尔 - 委内瑞拉
    /// <para>ISO 4217: VES</para>
    /// <para>数字代码: 928</para>
    /// <para>符号: Bs.S</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("VES|Bs.S|2")]
    VES = 928,

    /// <summary>
    /// 苏里南元 - 苏里南
    /// <para>ISO 4217: SRD</para>
    /// <para>数字代码: 968</para>
    /// <para>符号: $</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("SRD|$|2")]
    SRD = 968,

    /// <summary>
    /// 哥伦比亚实际价值单位 - 哥伦比亚
    /// <para>ISO 4217: COU</para>
    /// <para>数字代码: 970</para>
    /// <para>符号: COU</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("COU|COU|2")]
    COU = 970,

    /// <summary>
    /// 墨西哥投资单位 - 墨西哥
    /// <para>ISO 4217: MXV</para>
    /// <para>数字代码: 979</para>
    /// <para>符号: UDI</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("MXV|UDI|2")]
    MXV = 979,

    /// <summary>
    /// 玻利维亚Mvdol - 玻利维亚
    /// <para>ISO 4217: BOV</para>
    /// <para>数字代码: 984</para>
    /// <para>符号: BOV</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("BOV|BOV|2")]
    BOV = 984,

    /// <summary>
    /// 巴西雷亚尔 - 巴西
    /// <para>ISO 4217: BRL</para>
    /// <para>数字代码: 986</para>
    /// <para>符号: R$</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("BRL|R$|2")]
    BRL = 986,

    /// <summary>
    /// 智利发展单位 - 智利
    /// <para>ISO 4217: CLF</para>
    /// <para>数字代码: 990</para>
    /// <para>符号: UF</para>
    /// <para>小数位数: 4</para>
    /// </summary>
    [Description("CLF|UF|4")]
    CLF = 990,

    /// <summary>
    /// 美元(次日) - 美国
    /// <para>ISO 4217: USN</para>
    /// <para>数字代码: 997</para>
    /// <para>符号: $</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("USN|$|2")]
    USN = 997,
    #endregion

    #region 非洲货币
    /// <summary>
    /// 阿尔及利亚第纳尔 - 阿尔及利亚
    /// <para>ISO 4217: DZD</para>
    /// <para>数字代码: 012</para>
    /// <para>符号: دج</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("DZD|دج|2")]
    DZD = 12,

    /// <summary>
    /// 博茨瓦纳普拉 - 博茨瓦纳
    /// <para>ISO 4217: BWP</para>
    /// <para>数字代码: 072</para>
    /// <para>符号: P</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("BWP|P|2")]
    BWP = 72,

    /// <summary>
    /// 布隆迪法郎 - 布隆迪
    /// <para>ISO 4217: BIF</para>
    /// <para>数字代码: 108</para>
    /// <para>符号: FBu</para>
    /// <para>小数位数: 0</para>
    /// </summary>
    [Description("BIF|FBu|0")]
    BIF = 108,

    /// <summary>
    /// 佛得角埃斯库多 - 佛得角
    /// <para>ISO 4217: CVE</para>
    /// <para>数字代码: 132</para>
    /// <para>符号: Esc</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("CVE|Esc|2")]
    CVE = 132,

    /// <summary>
    /// 科摩罗法郎 - 科摩罗
    /// <para>ISO 4217: KMF</para>
    /// <para>数字代码: 174</para>
    /// <para>符号: CF</para>
    /// <para>小数位数: 0</para>
    /// </summary>
    [Description("KMF|CF|0")]
    KMF = 174,

    /// <summary>
    /// 埃塞俄比亚比尔 - 埃塞俄比亚
    /// <para>ISO 4217: ETB</para>
    /// <para>数字代码: 230</para>
    /// <para>符号: Br</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("ETB|Br|2")]
    ETB = 230,

    /// <summary>
    /// 冈比亚达拉西 - 冈比亚
    /// <para>ISO 4217: GMD</para>
    /// <para>数字代码: 270</para>
    /// <para>符号: D</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("GMD|D|2")]
    GMD = 270,

    /// <summary>
    /// 几内亚法郎 - 几内亚
    /// <para>ISO 4217: GNF</para>
    /// <para>数字代码: 324</para>
    /// <para>符号: FG</para>
    /// <para>小数位数: 0</para>
    /// </summary>
    [Description("GNF|FG|0")]
    GNF = 324,

    /// <summary>
    /// 肯尼亚先令 - 肯尼亚
    /// <para>ISO 4217: KES</para>
    /// <para>数字代码: 404</para>
    /// <para>符号: Ksh</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("KES|Ksh|2")]
    KES = 404,

    /// <summary>
    /// 莱索托洛蒂 - 莱索托
    /// <para>ISO 4217: LSL</para>
    /// <para>数字代码: 426</para>
    /// <para>符号: L</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("LSL|L|2")]
    LSL = 426,

    /// <summary>
    /// 利比里亚元 - 利比里亚
    /// <para>ISO 4217: LRD</para>
    /// <para>数字代码: 430</para>
    /// <para>符号: $</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("LRD|$|2")]
    LRD = 430,

    /// <summary>
    /// 利比亚第纳尔 - 利比亚
    /// <para>ISO 4217: LYD</para>
    /// <para>数字代码: 434</para>
    /// <para>符号: LD</para>
    /// <para>小数位数: 3</para>
    /// </summary>
    [Description("LYD|LD|3")]
    LYD = 434,

    /// <summary>
    /// 马拉维克瓦查 - 马拉维
    /// <para>ISO 4217: MWK</para>
    /// <para>数字代码: 454</para>
    /// <para>符号: MK</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("MWK|MK|2")]
    MWK = 454,

    /// <summary>
    /// 毛里求斯卢比 - 毛里求斯
    /// <para>ISO 4217: MUR</para>
    /// <para>数字代码: 480</para>
    /// <para>符号: ₨</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("MUR|₨|2")]
    MUR = 480,

    /// <summary>
    /// 摩洛哥迪拉姆 - 摩洛哥
    /// <para>ISO 4217: MAD</para>
    /// <para>数字代码: 504</para>
    /// <para>符号: MAD</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("MAD|MAD|2")]
    MAD = 504,

    /// <summary>
    /// 纳米比亚元 - 纳米比亚
    /// <para>ISO 4217: NAD</para>
    /// <para>数字代码: 516</para>
    /// <para>符号: N$</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("NAD|N$|2")]
    NAD = 516,

    /// <summary>
    /// 尼日利亚奈拉 - 尼日利亚
    /// <para>ISO 4217: NGN</para>
    /// <para>数字代码: 566</para>
    /// <para>符号: ₦</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("NGN|₦|2")]
    NGN = 566,

    /// <summary>
    /// 卢旺达法郎 - 卢旺达
    /// <para>ISO 4217: RWF</para>
    /// <para>数字代码: 646</para>
    /// <para>符号: FRw</para>
    /// <para>小数位数: 0</para>
    /// </summary>
    [Description("RWF|FRw|0")]
    RWF = 646,

    /// <summary>
    /// 塞舌尔卢比 - 塞舌尔
    /// <para>ISO 4217: SCR</para>
    /// <para>数字代码: 690</para>
    /// <para>符号: SR</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("SCR|SR|2")]
    SCR = 690,

    /// <summary>
    /// 塞拉利昂利昂 - 塞拉利昂
    /// <para>ISO 4217: SLE</para>
    /// <para>数字代码: 925</para>
    /// <para>符号: Le</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("SLE|Le|2")]
    SLE = 925,

    /// <summary>
    /// 索马里先令 - 索马里
    /// <para>ISO 4217: SOS</para>
    /// <para>数字代码: 706</para>
    /// <para>符号: Sh.so.</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("SOS|Sh.so.|2")]
    SOS = 706,

    /// <summary>
    /// 南非兰特 - 南非
    /// <para>ISO 4217: ZAR</para>
    /// <para>数字代码: 710</para>
    /// <para>符号: R</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("ZAR|R|2")]
    ZAR = 710,

    /// <summary>
    /// 南苏丹镑 - 南苏丹
    /// <para>ISO 4217: SSP</para>
    /// <para>数字代码: 728</para>
    /// <para>符号: £</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("SSP|£|2")]
    SSP = 728,

    /// <summary>
    /// 斯威士兰里兰吉尼 - 斯威士兰
    /// <para>ISO 4217: SZL</para>
    /// <para>数字代码: 748</para>
    /// <para>符号: E</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("SZL|E|2")]
    SZL = 748,

    /// <summary>
    /// 突尼斯第纳尔 - 突尼斯
    /// <para>ISO 4217: TND</para>
    /// <para>数字代码: 788</para>
    /// <para>符号: DT</para>
    /// <para>小数位数: 3</para>
    /// </summary>
    [Description("TND|DT|3")]
    TND = 788,

    /// <summary>
    /// 乌干达先令 - 乌干达
    /// <para>ISO 4217: UGX</para>
    /// <para>数字代码: 800</para>
    /// <para>符号: USh</para>
    /// <para>小数位数: 0</para>
    /// </summary>
    [Description("UGX|USh|0")]
    UGX = 800,

    /// <summary>
    /// 埃及镑 - 埃及
    /// <para>ISO 4217: EGP</para>
    /// <para>数字代码: 818</para>
    /// <para>符号: ج.م</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("EGP|ج.م|2")]
    EGP = 818,

    /// <summary>
    /// 坦桑尼亚先令 - 坦桑尼亚
    /// <para>ISO 4217: TZS</para>
    /// <para>数字代码: 834</para>
    /// <para>符号: TSh</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("TZS|TSh|2")]
    TZS = 834,

    /// <summary>
    /// 毛里塔尼亚乌吉亚 - 毛里塔尼亚
    /// <para>ISO 4217: MRU</para>
    /// <para>数字代码: 929</para>
    /// <para>符号: UM</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("MRU|UM|2")]
    MRU = 929,

    /// <summary>
    /// 圣多美和普林西比多布拉 - 圣多美和普林西比
    /// <para>ISO 4217: STN</para>
    /// <para>数字代码: 930</para>
    /// <para>符号: Db</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("STN|Db|2")]
    STN = 930,

    /// <summary>
    /// 加纳塞地 - 加纳
    /// <para>ISO 4217: GHS</para>
    /// <para>数字代码: 936</para>
    /// <para>符号: GH₵</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("GHS|GH₵|2")]
    GHS = 936,

    /// <summary>
    /// 苏丹镑 - 苏丹
    /// <para>ISO 4217: SDG</para>
    /// <para>数字代码: 938</para>
    /// <para>符号: £S</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("SDG|£S|2")]
    SDG = 938,

    /// <summary>
    /// 莫桑比克梅蒂卡尔 - 莫桑比克
    /// <para>ISO 4217: MZN</para>
    /// <para>数字代码: 943</para>
    /// <para>符号: MT</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("MZN|MT|2")]
    MZN = 943,

    /// <summary>
    /// 安哥拉宽扎 - 安哥拉
    /// <para>ISO 4217: AOA</para>
    /// <para>数字代码: 973</para>
    /// <para>符号: Kz</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("AOA|Kz|2")]
    AOA = 973,

    /// <summary>
    /// 刚果法郎 - 刚果(金)
    /// <para>ISO 4217: CDF</para>
    /// <para>数字代码: 976</para>
    /// <para>符号: FC</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("CDF|FC|2")]
    CDF = 976,

    /// <summary>
    /// 中非法郎 - 中非经济共同体
    /// <para>ISO 4217: XAF</para>
    /// <para>数字代码: 950</para>
    /// <para>符号: FCFA</para>
    /// <para>小数位数: 0</para>
    /// </summary>
    [Description("XAF|FCFA|0")]
    XAF = 950,

    /// <summary>
    /// 西非法郎 - 非洲金融共同体
    /// <para>ISO 4217: XOF</para>
    /// <para>数字代码: 952</para>
    /// <para>符号: CFA</para>
    /// <para>小数位数: 0</para>
    /// </summary>
    [Description("XOF|CFA|0")]
    XOF = 952,

    /// <summary>
    /// 赞比亚克瓦查 - 赞比亚
    /// <para>ISO 4217: ZMW</para>
    /// <para>数字代码: 967</para>
    /// <para>符号: K</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("ZMW|K|2")]
    ZMW = 967,

    /// <summary>
    /// 津巴布韦黄金 - 津巴布韦
    /// <para>ISO 4217: ZWG</para>
    /// <para>数字代码: 924</para>
    /// <para>符号: ZWG</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("ZWG|ZWG|2")]
    ZWG = 924,
    #endregion

    #region 大洋洲货币
    /// <summary>
    /// 澳元 - 澳大利亚
    /// <para>ISO 4217: AUD</para>
    /// <para>数字代码: 036</para>
    /// <para>符号: A$</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("AUD|A$|2")]
    AUD = 36,

    /// <summary>
    /// 所罗门群岛元 - 所罗门群岛
    /// <para>ISO 4217: SBD</para>
    /// <para>数字代码: 090</para>
    /// <para>符号: SI$</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("SBD|SI$|2")]
    SBD = 90,

    /// <summary>
    /// 斐济元 - 斐济
    /// <para>ISO 4217: FJD</para>
    /// <para>数字代码: 242</para>
    /// <para>符号: FJ$</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("FJD|FJ$|2")]
    FJD = 242,

    /// <summary>
    /// 瓦努阿图瓦图 - 瓦努阿图
    /// <para>ISO 4217: VUV</para>
    /// <para>数字代码: 548</para>
    /// <para>符号: VT</para>
    /// <para>小数位数: 0</para>
    /// </summary>
    [Description("VUV|VT|0")]
    VUV = 548,

    /// <summary>
    /// 新西兰元 - 新西兰
    /// <para>ISO 4217: NZD</para>
    /// <para>数字代码: 554</para>
    /// <para>符号: NZ$</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("NZD|NZ$|2")]
    NZD = 554,

    /// <summary>
    /// 巴布亚新几内亚基那 - 巴布亚新几内亚
    /// <para>ISO 4217: PGK</para>
    /// <para>数字代码: 598</para>
    /// <para>符号: K</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("PGK|K|2")]
    PGK = 598,

    /// <summary>
    /// 汤加潘加 - 汤加
    /// <para>ISO 4217: TOP</para>
    /// <para>数字代码: 776</para>
    /// <para>符号: T$</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("TOP|T$|2")]
    TOP = 776,

    /// <summary>
    /// 萨摩亚塔拉 - 萨摩亚
    /// <para>ISO 4217: WST</para>
    /// <para>数字代码: 882</para>
    /// <para>符号: WS$</para>
    /// <para>小数位数: 2</para>
    /// </summary>
    [Description("WST|WS$|2")]
    WST = 882,

    /// <summary>
    /// 太平洋法郎 - 法属太平洋地区
    /// <para>ISO 4217: XPF</para>
    /// <para>数字代码: 953</para>
    /// <para>符号: ₣</para>
    /// <para>小数位数: 0</para>
    /// </summary>
    [Description("XPF|₣|0")]
    XPF = 953,
    #endregion

    #region 特殊/通用货币
    /// <summary>
    /// 黄金 - 贵金属
    /// <para>ISO 4217: XAU</para>
    /// <para>数字代码: 959</para>
    /// <para>符号: Au</para>
    /// <para>小数位数: N/A</para>
    /// </summary>
    [Description("XAU|Au|N/A")]
    XAU = 959,

    /// <summary>
    /// 特别提款权 - 国际货币基金组织
    /// <para>ISO 4217: XDR</para>
    /// <para>数字代码: 960</para>
    /// <para>符号: SDR</para>
    /// <para>小数位数: N/A</para>
    /// </summary>
    [Description("XDR|SDR|N/A")]
    XDR = 960,

    /// <summary>
    /// 白银 - 贵金属
    /// <para>ISO 4217: XAG</para>
    /// <para>数字代码: 961</para>
    /// <para>符号: Ag</para>
    /// <para>小数位数: N/A</para>
    /// </summary>
    [Description("XAG|Ag|N/A")]
    XAG = 961,

    /// <summary>
    /// 铂 - 贵金属
    /// <para>ISO 4217: XPT</para>
    /// <para>数字代码: 962</para>
    /// <para>符号: Pt</para>
    /// <para>小数位数: N/A</para>
    /// </summary>
    [Description("XPT|Pt|N/A")]
    XPT = 962,

    /// <summary>
    /// 钯 - 贵金属
    /// <para>ISO 4217: XPD</para>
    /// <para>数字代码: 964</para>
    /// <para>符号: Pd</para>
    /// <para>小数位数: N/A</para>
    /// </summary>
    [Description("XPD|Pd|N/A")]
    XPD = 964,

    /// <summary>
    /// 测试代码 - 测试用途
    /// <para>ISO 4217: XTS</para>
    /// <para>数字代码: 963</para>
    /// <para>符号: XTS</para>
    /// <para>小数位数: N/A</para>
    /// </summary>
    [Description("XTS|XTS|N/A")]
    XTS = 963,

    /// <summary>
    /// 无货币 - 无货币交易
    /// <para>ISO 4217: XXX</para>
    /// <para>数字代码: 999</para>
    /// <para>符号: XXX</para>
    /// <para>小数位数: N/A</para>
    /// </summary>
    [Description("XXX|XXX|N/A")]
    XXX = 999,
    #endregion
}
