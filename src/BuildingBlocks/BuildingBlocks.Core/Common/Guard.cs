using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace BuildingBlocks.Core.Common;

/// <summary>
/// 核心参数校验守卫类
/// </summary>
/// <remarks>
/// 【设计原则】
/// <list type="number">
/// <item>所有方法都是.NET BCL原生静态方法的直接包装，无任何自定义逻辑</item>
/// <item>仅包含所有项目都必须使用的、最基础的校验能力</item>
/// <item>所有方法都标记[DebuggerStepThrough]，调试时自动跳过</item>
/// <item>所有方法都标记[ExcludeFromCodeCoverage]，不需要单元测试</item>
/// </list>
/// 【架构价值】
/// <list type="number">
/// <item>为整个系统建立统一的参数校验编程模型</item>
/// <item>提供唯一的全局控制点，支持未来全局逻辑注入</item>
/// <item>屏蔽.NET版本差异，保证跨版本兼容性</item>
/// <item>统一异常体系，与全局异常处理无缝集成</item>
/// </list>
/// </remarks>
[DebuggerStepThrough]
[ExcludeFromCodeCoverage]
public static partial class Guard
{
}
