using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Northstar.Application.Abstractions;
using Northstar.Infrastructure.Integrations;
using Northstar.Infrastructure.Messaging;
using Northstar.Infrastructure.Persistence;

namespace Northstar.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var provider = configuration["Database:Provider"] ?? "Sqlite";
        var connectionString = configuration.GetConnectionString("Northstar")
            ?? "Data Source=northstar.db";

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            if (string.Equals(provider, "SqlServer", StringComparison.OrdinalIgnoreCase))
            {
                options.UseSqlServer(connectionString, sql =>
                {
                    sql.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                });
            }
            else
            {
                options.UseSqlite(connectionString, sqlite =>
                    sqlite.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
            }
        });

        services.AddScoped<IApplicationDbContext>(provider =>
            provider.GetRequiredService<ApplicationDbContext>());
        services.AddOptions<PayrollProviderOptions>()
            .Bind(configuration.GetSection(PayrollProviderOptions.SectionName))
            .Validate(options => options.BaseUrl.IsAbsoluteUri, "PayrollProvider:BaseUrl must be absolute.")
            .Validate(options => options.MaxAttempts is >= 1 and <= 10, "PayrollProvider:MaxAttempts must be between 1 and 10.")
            .Validate(options => options.BaseRetryDelayMilliseconds is >= 0 and <= 30_000,
                "PayrollProvider:BaseRetryDelayMilliseconds must be between 0 and 30000.")
            .Validate(options => options.AttemptTimeoutMilliseconds is >= 1 and <= 300_000,
                "PayrollProvider:AttemptTimeoutMilliseconds must be between 1 and 300000.")
            .ValidateOnStart();
        services.AddHttpClient<IPayrollProviderClient, FakePayrollProviderClient>((provider, client) =>
        {
            var options = provider
                .GetRequiredService<Microsoft.Extensions.Options.IOptions<PayrollProviderOptions>>()
                .Value;
            client.BaseAddress = options.BaseUrl;
            // The provider client owns a per-attempt timeout that spans both headers
            // and the streamed JSON body. A second HttpClient timeout would make
            // cancellation classification ambiguous.
            client.Timeout = Timeout.InfiniteTimeSpan;
        });
        services.AddSingleton<ImportBatchQueue>();
        services.AddSingleton<IImportBatchQueue>(provider =>
            provider.GetRequiredService<ImportBatchQueue>());
        services.AddHostedService<ImportBatchWorker>();

        return services;
    }
}
