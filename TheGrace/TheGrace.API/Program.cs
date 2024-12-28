using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Serilog;
using TheGrace.API.DependencyInjection.Extentions;
using TheGrace.API.Middleware;
using TheGrace.Application.DependencyInjection.Extensions;
using TheGrace.Persistence.DependencyInjection.Extentions;
using TheGrace.Persistence.DependencyInjection.Options;

var builder = WebApplication.CreateBuilder(args);

// Add configuration
builder.AddConfigurationSeriLog(builder.Configuration);

builder.Services.AddHttpContextAccessor();
builder.Services.AddCors();
builder.Services.AddConfigurationTimeZone(builder.Configuration);

// Configure Middleware
builder.Services.AddTransient<ExceptionHandlingMiddleware>();

builder.Services.ConfigurationSqlServerOptions(builder.Configuration.GetSection(nameof(SqlServerRetryOptions)));
builder.Services.AddConfigurationSql();
builder.Services.AddRepositoryBaseConfiguration();
builder.Services.AddConfigurationAutoMapper();
builder.Services.AddConfigurationServiceCommon();

// Configure Swagger
builder.Services
    .AddSwaggerGenNewtonsoftSupport()
    .AddFluentValidationRulesToSwagger()
    .AddEndpointsApiExplorer()
    .AddSwagger();

builder.Services
    .AddApiVersioning(options => options.ReportApiVersions = true)
    .AddApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

//app.UseAuthentication();
//app.UseAuthorization();

app.MapControllers();

if (builder.Environment.IsDevelopment() || builder.Environment.IsStaging())
    app.ConfigureSwagger();

try
{
    await app.RunAsync();
    Log.Information("Stopped cleanly");
}
catch (Exception ex)
{
    Log.Fatal(ex, "An unhandled exception occured during boostrapping");
    await app.StopAsync();
}
finally
{
    Log.CloseAndFlush();
    await app.DisposeAsync();
}