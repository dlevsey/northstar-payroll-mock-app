using Microsoft.EntityFrameworkCore;
using Northstar.Domain.Employees;
using Northstar.Domain.Organizations;
using Northstar.Domain.PayrollRuns;
using Northstar.Domain.Projects;
using Northstar.Domain.TimeEntries;
using Northstar.Domain.WageRates;

namespace Northstar.Infrastructure.Persistence;

public static class DatabaseInitializer
{
    private static readonly DateOnly PeriodStart = new(2026, 8, 24);

    public static async Task InitializeAsync(
        ApplicationDbContext dbContext,
        CancellationToken cancellationToken = default)
    {
        await dbContext.Database.EnsureCreatedAsync(cancellationToken);

        if (await dbContext.Organizations.AnyAsync(cancellationToken))
        {
            return;
        }

        var createdAt = new DateTimeOffset(2026, 8, 24, 12, 0, 0, TimeSpan.Zero);

        dbContext.Organizations.AddRange(
            new Organization
            {
                Id = DemoDataIds.OrganizationA,
                Name = "Northstar Demo Construction",
                ExternalReference = "DEMO-A",
                CreatedAt = createdAt
            },
            new Organization
            {
                Id = DemoDataIds.OrganizationB,
                Name = "Boundary Test Builders",
                ExternalReference = "DEMO-B",
                CreatedAt = createdAt
            });

        dbContext.Employees.AddRange(
            new Employee
            {
                Id = DemoDataIds.EmployeeAvery,
                OrganizationId = DemoDataIds.OrganizationA,
                ExternalEmployeeId = "A-EMP-001",
                DisplayName = "Avery Rivera",
                HomeJurisdiction = "MN",
                BaseHourlyRate = 45m,
                ActualFringeRate = 15m,
                ActiveFrom = PeriodStart
            },
            new Employee
            {
                Id = DemoDataIds.EmployeeJordan,
                OrganizationId = DemoDataIds.OrganizationA,
                ExternalEmployeeId = "A-EMP-002",
                DisplayName = "Jordan Lee",
                HomeJurisdiction = "MN",
                BaseHourlyRate = 30m,
                ActualFringeRate = 5m,
                ActiveFrom = PeriodStart
            },
            new Employee
            {
                Id = DemoDataIds.EmployeeMorgan,
                OrganizationId = DemoDataIds.OrganizationA,
                ExternalEmployeeId = "A-EMP-003",
                DisplayName = "Morgan Ellis",
                HomeJurisdiction = "MN",
                BaseHourlyRate = 32m,
                ActualFringeRate = 7m,
                ActiveFrom = PeriodStart
            },
            new Employee
            {
                Id = DemoDataIds.EmployeeB,
                OrganizationId = DemoDataIds.OrganizationB,
                ExternalEmployeeId = "B-EMP-001",
                DisplayName = "Casey Boundary",
                HomeJurisdiction = "WI",
                BaseHourlyRate = 38m,
                ActualFringeRate = 12m,
                ActiveFrom = PeriodStart
            });

        dbContext.Projects.AddRange(
            new Project
            {
                Id = DemoDataIds.ProjectA,
                OrganizationId = DemoDataIds.OrganizationA,
                ExternalProjectId = "A-PRJ-001",
                ContractNumber = "NS-2026-101",
                Name = "River Crossing Retrofit",
                Location = "Hennepin County, MN",
                JurisdictionCode = "MN",
                GoverningAuthority = "Synthetic State Authority",
                WageDeterminationNumber = "MN-DEMO-2026-01",
                StartsOn = PeriodStart
            },
            new Project
            {
                Id = DemoDataIds.ProjectASecond,
                OrganizationId = DemoDataIds.OrganizationA,
                ExternalProjectId = "A-PRJ-002",
                ContractNumber = "NS-2026-102",
                Name = "Civic Annex",
                Location = "Ramsey County, MN",
                JurisdictionCode = "MN",
                GoverningAuthority = "Synthetic Federal Authority",
                WageDeterminationNumber = "US-DEMO-2026-01",
                StartsOn = PeriodStart
            },
            new Project
            {
                Id = DemoDataIds.ProjectB,
                OrganizationId = DemoDataIds.OrganizationB,
                ExternalProjectId = "B-PRJ-001",
                ContractNumber = "BT-2026-201",
                Name = "Tenant Boundary Project",
                Location = "Dane County, WI",
                JurisdictionCode = "WI",
                GoverningAuthority = "Synthetic State Authority",
                WageDeterminationNumber = "WI-DEMO-2026-01",
                StartsOn = PeriodStart
            });

        dbContext.WorkClassifications.AddRange(
            Classification(DemoDataIds.ElectricianA, DemoDataIds.OrganizationA, "ELEC", "Electrician"),
            Classification(DemoDataIds.LaborerA, DemoDataIds.OrganizationA, "LAB", "Laborer"),
            Classification(DemoDataIds.CarpenterA, DemoDataIds.OrganizationA, "CARP", "Carpenter"),
            Classification(DemoDataIds.ElectricianB, DemoDataIds.OrganizationB, "ELEC", "Electrician"));

        dbContext.WageRates.AddRange(
            Rate(
                Guid.Parse("a4000000-0000-0000-0000-000000000001"),
                DemoDataIds.OrganizationA,
                DemoDataIds.ElectricianA,
                WageRateScope.Federal,
                "US",
                35m,
                12m,
                PeriodStart,
                null,
                null,
                "US-DEMO-BASE"),
            Rate(
                Guid.Parse("a4000000-0000-0000-0000-000000000002"),
                DemoDataIds.OrganizationA,
                DemoDataIds.ElectricianA,
                WageRateScope.ProjectClassification,
                "MN",
                40m,
                15m,
                PeriodStart,
                new DateOnly(2026, 8, 25),
                DemoDataIds.ProjectA,
                "PROJECT-PREMIUM-V1"),
            Rate(
                Guid.Parse("a4000000-0000-0000-0000-000000000003"),
                DemoDataIds.OrganizationA,
                DemoDataIds.ElectricianA,
                WageRateScope.ProjectClassification,
                "MN",
                42m,
                15m,
                new DateOnly(2026, 8, 26),
                null,
                DemoDataIds.ProjectA,
                "PROJECT-PREMIUM-V2"),
            Rate(
                Guid.Parse("a4000000-0000-0000-0000-000000000004"),
                DemoDataIds.OrganizationA,
                DemoDataIds.LaborerA,
                WageRateScope.State,
                "MN",
                28m,
                10m,
                PeriodStart,
                null,
                null,
                "MN-DEMO-LAB"),
            Rate(
                Guid.Parse("b4000000-0000-0000-0000-000000000001"),
                DemoDataIds.OrganizationB,
                DemoDataIds.ElectricianB,
                WageRateScope.State,
                "WI",
                36m,
                11m,
                PeriodStart,
                null,
                null,
                "WI-DEMO-ELEC"));

        dbContext.TimeEntries.AddRange(
            Entry("A-TIME-001", DemoDataIds.OrganizationA, DemoDataIds.EmployeeAvery, DemoDataIds.ProjectA, DemoDataIds.ElectricianA, PeriodStart, 8m, 0m),
            Entry("A-TIME-002", DemoDataIds.OrganizationA, DemoDataIds.EmployeeAvery, DemoDataIds.ProjectA, DemoDataIds.ElectricianA, new DateOnly(2026, 8, 26), 6m, 2m),
            Entry("A-TIME-003", DemoDataIds.OrganizationA, DemoDataIds.EmployeeJordan, DemoDataIds.ProjectA, DemoDataIds.LaborerA, new DateOnly(2026, 8, 25), 8m, 0m),
            Entry("A-TIME-004", DemoDataIds.OrganizationA, DemoDataIds.EmployeeMorgan, DemoDataIds.ProjectA, DemoDataIds.CarpenterA, new DateOnly(2026, 8, 27), 8m, 0m),
            Entry("B-TIME-001", DemoDataIds.OrganizationB, DemoDataIds.EmployeeB, DemoDataIds.ProjectB, DemoDataIds.ElectricianB, PeriodStart, 8m, 0m));

        dbContext.PayrollRuns.AddRange(
            new PayrollRun
            {
                Id = DemoDataIds.RunA,
                OrganizationId = DemoDataIds.OrganizationA,
                PeriodStart = PeriodStart,
                PeriodEnd = new DateOnly(2026, 8, 30),
                CreatedAt = createdAt
            },
            new PayrollRun
            {
                Id = DemoDataIds.RunB,
                OrganizationId = DemoDataIds.OrganizationB,
                PeriodStart = PeriodStart,
                PeriodEnd = new DateOnly(2026, 8, 30),
                CreatedAt = createdAt
            });

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private static WorkClassification Classification(Guid id, Guid organizationId, string code, string name) =>
        new() { Id = id, OrganizationId = organizationId, Code = code, Name = name };

    private static WageRate Rate(
        Guid id,
        Guid organizationId,
        Guid classificationId,
        WageRateScope scope,
        string jurisdiction,
        decimal baseRate,
        decimal fringeRate,
        DateOnly effectiveFrom,
        DateOnly? effectiveTo,
        Guid? projectId,
        string source) =>
        new()
        {
            Id = id,
            OrganizationId = organizationId,
            ClassificationId = classificationId,
            Scope = scope,
            JurisdictionCode = jurisdiction,
            BaseRate = baseRate,
            FringeRate = fringeRate,
            OvertimeMultiplier = 1.5m,
            EffectiveFrom = effectiveFrom,
            EffectiveTo = effectiveTo,
            ProjectId = projectId,
            Priority = 0,
            SourceReference = source
        };

    private static TimeEntry Entry(
        string externalId,
        Guid organizationId,
        Guid employeeId,
        Guid projectId,
        Guid classificationId,
        DateOnly workDate,
        decimal regularHours,
        decimal overtimeHours) =>
        new()
        {
            Id = Guid.NewGuid(),
            OrganizationId = organizationId,
            ExternalTimeEntryId = externalId,
            EmployeeId = employeeId,
            ProjectId = projectId,
            ClassificationId = classificationId,
            WorkDate = workDate,
            RegularHours = regularHours,
            OvertimeHours = overtimeHours
        };
}
