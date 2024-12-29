using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TheGrace.Domain.Abstractions;
using TheGrace.Domain.Abstractions.Repositories;
using TheGrace.Domain.Exceptions;
using TheGrace.Persistence.DependencyInjection.Options;
using TheGrace.Persistence.Repositories;

namespace TheGrace.Persistence.DependencyInjection.Extentions;

public static class ServiceCollectionExtentions
{
    public static void AddConfigurationSql(this IServiceCollection services)
    {
        services.AddDbContextPool<DbContext, ApplicationDbContext>((provider, builder) =>
        {
            var configuration = provider.GetRequiredService<IConfiguration>();
            var options = provider.GetRequiredService<IOptionsMonitor<SqlServerRetryOptions>>();

            var connectionStringOne = configuration.GetConnectionString("ConnectionStrings");
            if (string.IsNullOrEmpty(connectionStringOne))
            {
                throw new DatabaseException.ConnectionStringNotFound();
            }

            try
            {
                builder
                .EnableDetailedErrors(true)
                .EnableSensitiveDataLogging(true)
                .UseLazyLoadingProxies(true) // if useLazyLoadingProxies, all of the navigation fields should be VIRTUAL
                .UseSqlServer(
                    connectionString: connectionStringOne,
                    sqlServerOptionsAction: optionsBuilder => optionsBuilder
                        .ExecutionStrategy(
                            dependencies => new SqlServerRetryingExecutionStrategy(
                                dependencies: dependencies,
                                maxRetryCount: options.CurrentValue.MaxRetryCount,
                                maxRetryDelay: options.CurrentValue.MaxRetryDelay,
                                errorNumbersToAdd: options.CurrentValue.ErrorNumbersToAdd))
                        .MigrationsAssembly(typeof(ApplicationDbContext).Assembly.GetName().Name));
            }
            catch (Exception ex)
            {
                string ss = ex.Message;
                throw new Exception(ss);
                //throw new DatabaseException.ConnectionDatabaseFail();
            }
        });
    }

    public static OptionsBuilder<SqlServerRetryOptions> ConfigurationSqlServerOptions(this IServiceCollection services, IConfigurationSection section)
        => services
        .AddOptions<SqlServerRetryOptions>()
        .Bind(section)
        .ValidateDataAnnotations()
        .ValidateOnStart();

    public static void AddRepositoryBaseConfiguration(this IServiceCollection services)
        => services
        .AddTransient(typeof(IUnitOfWork), typeof(EFUnitOfWork))
        .AddTransient(typeof(IRepositoryBase<,>), typeof(RepositoryBase<,>));
}