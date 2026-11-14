using Microsoft.Extensions.DependencyInjection;
using Northstar.Application.Abstractions;
using Northstar.Application.Imports;
using Northstar.Application.PayrollRuns;
using Northstar.Application.WageRates;
using Northstar.Domain.PayrollRuns;
using Northstar.Domain.WageRates;

namespace Northstar.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services) =>
        services
            .AddSingleton<IClock, SystemClock>()
            .AddSingleton<WageRateSelector>()
            .AddSingleton<PayrollCalculator>()
            .AddScoped<ImportBatchService>()
            .AddScoped<ImportBatchProcessor>()
            .AddScoped<PayrollRunService>()
            .AddScoped<WageRateService>();
}
