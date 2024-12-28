using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace TheGrace.API.DependencyInjection.Options;

public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _provider;
    private const string _Scheme = "Bearer";

    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) => _provider = provider;

    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in _provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(description.GroupName, new()
            {
                Title = AppDomain.CurrentDomain.FriendlyName,
                Version = description.ApiVersion.ToString()
            });

            //options.AddSecurityDefinition(_Scheme, new OpenApiSecurityScheme
            //{
            //    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\nExample: 'Bearer 123456'",
            //    Name = "Authorization",
            //    In = ParameterLocation.Header,
            //    Type = SecuritySchemeType.ApiKey,
            //    Scheme = _Scheme
            //});

            //options.AddSecurityRequirement(new OpenApiSecurityRequirement()
            //{
            //    {
            //        new OpenApiSecurityScheme()
            //        {
            //            Reference = new OpenApiReference()
            //            {
            //                Type = ReferenceType.SecurityScheme,
            //                Id = _Scheme
            //            },
            //            Scheme = "oauth2",
            //            Name = _Scheme,
            //            In = ParameterLocation.Header,
            //        },
            //        new List<string>()
            //    }
            //});
        }

        options.MapType<DateOnly>(() => new()
        {
            Format = "date",
            Example = new OpenApiString(DateOnly.MinValue.ToString())
        });

        options.CustomSchemaIds(type => type.ToString().Replace("+", "."));
    }
}
