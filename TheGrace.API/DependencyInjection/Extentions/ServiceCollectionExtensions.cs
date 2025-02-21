using Serilog;
using TheGrace.Contract.JsonConvert;

namespace TheGrace.API.DependencyInjection.Extentions;

public static class ServiceCollectionExtensions
{
    //public static void AddConfigurationAutentication(this IServiceCollection services, IConfiguration configuration)
    //{
    //    services.Configure<UrlSetting>(configuration.GetSection("UrlSetting"));
    //    services.Configure<PrompterSetting>(configuration.GetSection("PrompterSetting"));

    //    var urlSetting = new UrlSetting();
    //    configuration.GetSection("UrlSetting").Bind(urlSetting);

    //    if (string.IsNullOrEmpty(urlSetting.IdentityHost))
    //    {
    //        throw new IdentityHostException.IdentityHostNotFoundException();
    //    }

    //    services
    //   .AddAuthentication(options =>
    //   {
    //       options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    //   })
    //   .AddJwtBearer(options =>
    //   {
    //       options.Authority = urlSetting.IdentityHost;
    //       options.TokenValidationParameters = new TokenValidationParameters
    //       {
    //           ValidateAudience = false
    //       };
    //   });
    //}

    public static void AddConfigurationController(this IServiceCollection services)
    {
        services
            .AddControllers()
            .AddApplicationPart(AssemblyReference.Assembly)
            .AddNewtonsoftJson(opt =>
            {
                opt.SerializerSettings.Converters.Add(new StatusEnumJsonConvert());
            });
    }

    public static void AddConfigurationSeriLog(this WebApplicationBuilder builder, IConfiguration configuration)
    {
        Log.Logger = new LoggerConfiguration().ReadFrom
            .Configuration(configuration)
            .CreateLogger();

        builder.Logging
            .ClearProviders()
            .AddSerilog();

        builder.Host.UseSerilog();
    }
}
