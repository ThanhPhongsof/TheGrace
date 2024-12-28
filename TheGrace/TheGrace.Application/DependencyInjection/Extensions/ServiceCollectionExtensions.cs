using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TheGrace.Application.Mapper;
using TheGrace.Application.Services.ProductCategory;
using TheGrace.Application.Services.V1.TimeZone;
using TheGrace.Domain.Exceptions;

namespace TheGrace.Application.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    //public static IServiceCollection AddConfigurationMediatR(this IServiceCollection services)
    //    => services
    //    .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(AssemblyReference.Assembly))
    //    .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>))
    //    .AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformancePipelineBehavior<,>))
    //    .AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionPipelineBehavior<,>))
    //    .AddTransient(typeof(IPipelineBehavior<,>), typeof(TracingPipelineBehavior<,>))
    //    .AddValidatorsFromAssembly(Contract.AssemblyReference.Assembly, includeInternalTypes: true);

    public static IServiceCollection AddConfigurationAutoMapper(this IServiceCollection services)
    => services.AddAutoMapper(typeof(ServiceProfile));

    public static IServiceCollection AddConfigurationTimeZone(this IServiceCollection services, IConfiguration configuration)
    => services.AddSingleton<ITimeZoneService>(p => new TimeZoneService(configuration["TIMEZONE"] ?? throw new TimeZoneException.TimeZoneNotFoundException()));

    public static IServiceCollection AddConfigurationServiceCommon(this IServiceCollection services)
        => services
        .AddScoped<IProductCategoryService, ProductCategoryService>();
        //.AddScoped<IProductCategoryService, Prod>();
}