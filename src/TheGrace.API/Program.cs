using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.Extensions.Options;
using Serilog;
using TheGrace.API.DependencyInjection.Extentions;
using TheGrace.API.Middleware;
using TheGrace.Application.DependencyInjection.Extensions;
using TheGrace.Contract.Hubs;
using TheGrace.Persistence.DependencyInjection.Extentions;
using TheGrace.Persistence.DependencyInjection.Options;

var builder = WebApplication.CreateBuilder(args);

// Add configuration
builder.AddConfigurationSeriLog(builder.Configuration);

builder.Services.AddConfigureMediatR();
builder.Services.AddHttpContextAccessor();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", policy =>
    {
        policy.WithOrigins("http://localhost:4200") // Add the Angular app's URL
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});
builder.Services.AddConfigurationTimeZone(builder.Configuration);

// Configure Middleware
builder.Services.AddTransient<ExceptionHandlingMiddleware>();

builder.Services.ConfigurationSqlServerOptions(builder.Configuration.GetSection(nameof(SqlServerRetryOptions)));
builder.Services.AddConfigurationSql();
builder.Services.AddRepositoryBaseConfiguration();
builder.Services.AddConfigurationAutoMapper();
builder.Services.AddConfigurationServiceCommon();
builder.Services.AddConfigurationController();

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

builder.Services.AddSignalR(options =>
{
    options.KeepAliveInterval = TimeSpan.FromSeconds(10);
});

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapHub<ProductHub>("/productHub");

//app.UseAuthentication();
//app.UseAuthorization();

app.MapControllers();
app.UseCors("AllowSpecificOrigins");

//if (builder.Environment.IsDevelopment() || builder.Environment.IsStaging())
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
