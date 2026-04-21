using Asp.Versioning;
using BuildingBlocks.Api.Behaviors;
using BuildingBlocks.Api.ExceptionHandlers;
using BuildingBlocks.Api.Middleware;
using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi;

namespace BuildingBlocks.Api.Extensions;

/// <summary>
/// API基础设施构建扩展
/// 提供服务注册、中间件配置统一入口
/// </summary>
public static class ApiInfrastructureExtensions
{
    /// <summary>
    /// 注册API核心基础设施
    /// </summary>
    /// <param name="services">服务集合</param>
    public static IServiceCollection AddApiInfrastructure(this IServiceCollection services, string? xmlCommentsPath = null)
    {
        // 注册Http上下文访问器(管道内部获取请求上下文依赖)
        services.AddHttpContextAccessor();
        // 注册API端点探索器(自动生成API文档和客户端代码依赖)
        services.AddEndpointsApiExplorer();
        // 注册Swagger生成器(自动生成API文档和测试界面依赖)
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "DDD Clean Architecture API",
                Version = "v1",
                Description = "DDD整洁架构模板API文档"
            });

            // 添加XML注释
            if (!string.IsNullOrEmpty(xmlCommentsPath) && File.Exists(xmlCommentsPath))
            {
                options.IncludeXmlComments(xmlCommentsPath, true);
            }

            // 添加JWT认证配置
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\""
            });

            options.AddSecurityRequirement(_ => new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecuritySchemeReference("Bearer"),
                    new List<string>()
                }
            });
        });

        // 全局异常处理器
        services.AddExceptionHandler<GlobalExceptionHandler>();
        // 注册ProblemDetails中间件(统一错误响应格式依赖)
        services.AddProblemDetails();

        // 注册MediatR通用管道
        services.AddMediatR(cfg =>
        {
            // 注册通用行为(请求日志、验证、事务、性能监控等)
            cfg.AddOpenBehavior(typeof(RequestLoggingBehavior<,>));
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
            cfg.AddOpenBehavior(typeof(TransactionBehavior<,>));
            cfg.AddOpenBehavior(typeof(PerformanceBehavior<,>));
        });

        // 注册Carter
        services.AddCarter();

        // 跨域配置
        services.AddCors(options =>
        {
            options.AddPolicy("Default", policy =>
            {
                policy.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });

        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;

            // 支持3种版本传递方式
            options.ApiVersionReader = ApiVersionReader.Combine(
                new UrlSegmentApiVersionReader(),
                new QueryStringApiVersionReader("api-version"),
                new HeaderApiVersionReader("X-API-Version")
            );
        });

        return services;
    }

    /// <summary>
    /// 使用API基础设施中间件
    /// </summary>
    /// <param name="app">Web应用构建器</param>
    public static WebApplication UseApiInfrastructure(this WebApplication app)
    {
        // 异常处理
        app.UseExceptionHandler();

        // 开发环境启用Swagger文档
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "DDD Clean Architecture API V1");
                c.RoutePrefix = string.Empty; // 设置Swagger UI根路径
            });
        }

        // 使用HTTPS重定向
        app.UseHttpsRedirection();
        // 使用CORS
        app.UseCors("Default");
        // 使用安全头中间件
        app.UseMiddleware<SecurityHeadersMiddleware>();
        // 使用相关ID中间件(请求跟踪和日志关联依赖)
        app.UseMiddleware<CorrelationIdMiddleware>();

        // 使用认证和授权中间件
        app.UseAuthentication();
        app.UseAuthorization();

        // 启用Carter路由
        app.MapCarter().AutoResponseWrapper();

        return app;
    }
}
