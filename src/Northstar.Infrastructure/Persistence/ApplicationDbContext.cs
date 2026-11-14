using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Northstar.Application.Abstractions;
using Northstar.Application.Idempotency;
using Northstar.Domain.Employees;
using Northstar.Domain.Imports;
using Northstar.Domain.Organizations;
using Northstar.Domain.PayrollRuns;
using Northstar.Domain.Projects;
using Northstar.Domain.TimeEntries;
using Northstar.Domain.WageRates;

namespace Northstar.Infrastructure.Persistence;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : DbContext(options), IApplicationDbContext
{
    public DbSet<Organization> Organizations => Set<Organization>();
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<WorkClassification> WorkClassifications => Set<WorkClassification>();
    public DbSet<WageRate> WageRates => Set<WageRate>();
    public DbSet<TimeEntry> TimeEntries => Set<TimeEntry>();
    public DbSet<PayrollRun> PayrollRuns => Set<PayrollRun>();
    public DbSet<PayrollLine> PayrollLines => Set<PayrollLine>();
    public DbSet<ComplianceFinding> ComplianceFindings => Set<ComplianceFinding>();
    public DbSet<IdempotencyRecord> IdempotencyRecords => Set<IdempotencyRecord>();
    public DbSet<ImportBatch> ImportBatches => Set<ImportBatch>();
    public DbSet<ImportRow> ImportRows => Set<ImportRow>();

    IQueryable<Organization> IApplicationDbContext.Organizations => Organizations;
    IQueryable<Employee> IApplicationDbContext.Employees => Employees;
    IQueryable<Project> IApplicationDbContext.Projects => Projects;
    IQueryable<WorkClassification> IApplicationDbContext.WorkClassifications => WorkClassifications;
    IQueryable<WageRate> IApplicationDbContext.WageRates => WageRates;
    IQueryable<TimeEntry> IApplicationDbContext.TimeEntries => TimeEntries;
    IQueryable<PayrollRun> IApplicationDbContext.PayrollRuns => PayrollRuns;
    IQueryable<PayrollLine> IApplicationDbContext.PayrollLines => PayrollLines;
    IQueryable<ComplianceFinding> IApplicationDbContext.ComplianceFindings => ComplianceFindings;
    IQueryable<IdempotencyRecord> IApplicationDbContext.IdempotencyRecords => IdempotencyRecords;
    IQueryable<ImportBatch> IApplicationDbContext.ImportBatches => ImportBatches;
    IQueryable<ImportRow> IApplicationDbContext.ImportRows => ImportRows;

    public new void Add<TEntity>(TEntity entity) where TEntity : class => Set<TEntity>().Add(entity);

    public void ClearTracking() => ChangeTracker.Clear();

    public async Task<IApplicationTransaction> BeginTransactionAsync(CancellationToken cancellationToken) =>
        new EfApplicationTransaction(await Database.BeginTransactionAsync(cancellationToken));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ConfigureOrganization(modelBuilder);
        ConfigureEmployee(modelBuilder);
        ConfigureProject(modelBuilder);
        ConfigureClassification(modelBuilder);
        ConfigureWageRate(modelBuilder);
        ConfigureTimeEntry(modelBuilder);
        ConfigurePayrollRun(modelBuilder);
        ConfigurePayrollLine(modelBuilder);
        ConfigureFinding(modelBuilder);
        ConfigureIdempotency(modelBuilder);
        ConfigureImportBatch(modelBuilder);
        ConfigureImportRow(modelBuilder);
    }

    private static void ConfigureOrganization(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<Organization>();
        entity.HasKey(value => value.Id);
        entity.Property(value => value.Name).HasMaxLength(200).IsRequired();
        entity.Property(value => value.ExternalReference).HasMaxLength(100).IsRequired();
        entity.HasIndex(value => value.ExternalReference).IsUnique();
        entity.Property(value => value.Version).IsConcurrencyToken();
    }

    private static void ConfigureEmployee(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<Employee>();
        entity.HasKey(value => value.Id);
        entity.Property(value => value.ExternalEmployeeId).HasMaxLength(100).IsRequired();
        entity.Property(value => value.DisplayName).HasMaxLength(200).IsRequired();
        entity.Property(value => value.HomeJurisdiction).HasMaxLength(20).IsRequired();
        entity.Property(value => value.BaseHourlyRate).HasPrecision(18, 4);
        entity.Property(value => value.ActualFringeRate).HasPrecision(18, 4);
        entity.Property(value => value.Version).IsConcurrencyToken();
        entity.HasIndex(value => new { value.OrganizationId, value.ExternalEmployeeId }).IsUnique();
    }

    private static void ConfigureProject(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<Project>();
        entity.HasKey(value => value.Id);
        entity.Property(value => value.ExternalProjectId).HasMaxLength(100).IsRequired();
        entity.Property(value => value.ContractNumber).HasMaxLength(100).IsRequired();
        entity.Property(value => value.Name).HasMaxLength(200).IsRequired();
        entity.Property(value => value.Location).HasMaxLength(200).IsRequired();
        entity.Property(value => value.JurisdictionCode).HasMaxLength(20).IsRequired();
        entity.Property(value => value.GoverningAuthority).HasMaxLength(100).IsRequired();
        entity.Property(value => value.WageDeterminationNumber).HasMaxLength(100).IsRequired();
        entity.Property(value => value.Version).IsConcurrencyToken();
        entity.HasIndex(value => new { value.OrganizationId, value.ExternalProjectId }).IsUnique();
    }

    private static void ConfigureClassification(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<WorkClassification>();
        entity.HasKey(value => value.Id);
        entity.Property(value => value.Code).HasMaxLength(50).IsRequired();
        entity.Property(value => value.Name).HasMaxLength(100).IsRequired();
        entity.HasIndex(value => new { value.OrganizationId, value.Code }).IsUnique();
    }

    private static void ConfigureWageRate(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<WageRate>();
        entity.HasKey(value => value.Id);
        entity.Property(value => value.Scope).HasConversion<string>().HasMaxLength(40);
        entity.Property(value => value.JurisdictionCode).HasMaxLength(20).IsRequired();
        entity.Property(value => value.BaseRate).HasPrecision(18, 4);
        entity.Property(value => value.FringeRate).HasPrecision(18, 4);
        entity.Property(value => value.OvertimeMultiplier).HasPrecision(8, 4);
        entity.Property(value => value.SourceReference).HasMaxLength(200).IsRequired();
        entity.Property(value => value.Version).IsConcurrencyToken();
        entity.ToTable(table =>
        {
            table.HasCheckConstraint("CK_WageRates_EffectiveDates", "EffectiveTo IS NULL OR EffectiveTo >= EffectiveFrom");
            table.HasCheckConstraint("CK_WageRates_Amounts", "BaseRate >= 0 AND FringeRate >= 0 AND OvertimeMultiplier >= 1");
        });
        entity.HasIndex(value => new
        {
            value.OrganizationId,
            value.ClassificationId,
            value.ProjectId,
            value.EffectiveFrom,
            value.EffectiveTo
        });
    }

    private static void ConfigureTimeEntry(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<TimeEntry>();
        entity.HasKey(value => value.Id);
        entity.Property(value => value.ExternalTimeEntryId)
            .HasConversion(
                value => value.Trim().ToUpperInvariant(),
                value => value.Trim().ToUpperInvariant())
            .HasMaxLength(100)
            .IsRequired();
        entity.Property(value => value.RegularHours).HasPrecision(9, 2);
        entity.Property(value => value.OvertimeHours).HasPrecision(9, 2);
        entity.Property(value => value.ShiftCode).HasMaxLength(20);
        entity.ToTable(table =>
            table.HasCheckConstraint("CK_TimeEntries_Hours", "RegularHours >= 0 AND OvertimeHours >= 0"));
        entity.HasIndex(value => new { value.OrganizationId, value.ExternalTimeEntryId }).IsUnique();
        entity.HasIndex(value => new { value.OrganizationId, value.WorkDate, value.EmployeeId, value.ProjectId });
        entity.HasOne<ImportBatch>()
            .WithMany()
            .HasForeignKey(value => value.ImportedInBatchId)
            .OnDelete(DeleteBehavior.SetNull);
    }

    private static void ConfigurePayrollRun(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<PayrollRun>();
        entity.HasKey(value => value.Id);
        entity.Property(value => value.Status).HasConversion<string>().HasMaxLength(40);
        entity.Property(value => value.Version).IsConcurrencyToken();
        entity.HasMany(value => value.Lines)
            .WithOne()
            .HasForeignKey(value => value.PayrollRunId)
            .OnDelete(DeleteBehavior.Cascade);
        entity.HasMany(value => value.Findings)
            .WithOne()
            .HasForeignKey(value => value.PayrollRunId)
            .OnDelete(DeleteBehavior.Cascade);
        entity.HasIndex(value => new { value.OrganizationId, value.PeriodStart, value.PeriodEnd });
    }

    private static void ConfigurePayrollLine(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<PayrollLine>();
        entity.HasKey(value => value.Id);
        entity.Property(value => value.EmployeeName).HasMaxLength(200).IsRequired();
        entity.Property(value => value.ProjectName).HasMaxLength(200).IsRequired();
        entity.Property(value => value.ClassificationName).HasMaxLength(100).IsRequired();
        entity.Property(value => value.RegularHours).HasPrecision(9, 2);
        entity.Property(value => value.OvertimeHours).HasPrecision(9, 2);
        entity.Property(value => value.RequiredBaseWages).HasPrecision(18, 2);
        entity.Property(value => value.RequiredFringe).HasPrecision(18, 2);
        entity.Property(value => value.ActualBaseWages).HasPrecision(18, 2);
        entity.Property(value => value.ActualFringe).HasPrecision(18, 2);
        entity.Property(value => value.Variance).HasPrecision(18, 2);
        entity.HasIndex(value => new { value.OrganizationId, value.PayrollRunId, value.EmployeeId });
    }

    private static void ConfigureFinding(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<ComplianceFinding>();
        entity.HasKey(value => value.Id);
        entity.Property(value => value.Severity).HasConversion<string>().HasMaxLength(20);
        entity.Property(value => value.Code).HasMaxLength(80).IsRequired();
        entity.Property(value => value.Message).HasMaxLength(500).IsRequired();
        entity.HasIndex(value => new { value.OrganizationId, value.PayrollRunId, value.Severity });
    }

    private static void ConfigureIdempotency(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<IdempotencyRecord>();
        entity.HasKey(value => value.Id);
        entity.Property(value => value.OperationName).HasMaxLength(80).IsRequired();
        entity.Property(value => value.Key).HasMaxLength(100).IsRequired();
        entity.Property(value => value.RequestHash).HasMaxLength(64).IsRequired();
        entity.HasIndex(value => new { value.OrganizationId, value.OperationName, value.Key }).IsUnique();
    }

    private static void ConfigureImportBatch(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<ImportBatch>();
        entity.HasKey(value => value.Id);
        entity.Property(value => value.ProviderScenario).HasMaxLength(40).IsRequired();
        entity.Property(value => value.Status).HasConversion<string>().HasMaxLength(40);
        entity.Property(value => value.FailureCode).HasMaxLength(100);
        entity.Property(value => value.FailureMessage).HasMaxLength(500);
        entity.Property(value => value.Version).IsConcurrencyToken();
        entity.HasAlternateKey(value => new { value.OrganizationId, value.Id });
        entity.HasMany(value => value.Rows)
            .WithOne()
            .HasForeignKey(value => new { value.OrganizationId, value.ImportBatchId })
            .HasPrincipalKey(value => new { value.OrganizationId, value.Id })
            .OnDelete(DeleteBehavior.Cascade);
        entity.HasIndex(value => new { value.OrganizationId, value.CreatedAt });
        entity.ToTable(table =>
            table.HasCheckConstraint(
                "CK_ImportBatches_RowCounts",
                "TotalRows >= 0 AND ImportedRows >= 0 AND ErrorRows >= 0 AND DuplicateRows >= 0"));
    }

    private static void ConfigureImportRow(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<ImportRow>();
        entity.HasKey(value => value.Id);
        entity.Property(value => value.ExternalTimeEntryId).HasMaxLength(100);
        entity.Property(value => value.ExternalEmployeeId).HasMaxLength(100);
        entity.Property(value => value.ExternalProjectId).HasMaxLength(100);
        entity.Property(value => value.ClassificationCode).HasMaxLength(50);
        entity.Property(value => value.RegularHours).HasPrecision(9, 2);
        entity.Property(value => value.OvertimeHours).HasPrecision(9, 2);
        entity.Property(value => value.ShiftCode).HasMaxLength(20);
        entity.Property(value => value.Status).HasConversion<string>().HasMaxLength(30);
        entity.Property(value => value.ErrorCode).HasMaxLength(100);
        entity.Property(value => value.ErrorMessage).HasMaxLength(500);
        entity.Property(value => value.SourcePayload).HasMaxLength(2_000).IsRequired();
        entity.HasIndex(value => new { value.OrganizationId, value.ImportBatchId, value.RowNumber }).IsUnique();
    }

    private sealed class EfApplicationTransaction(IDbContextTransaction transaction) : IApplicationTransaction
    {
        public Task CommitAsync(CancellationToken cancellationToken) => transaction.CommitAsync(cancellationToken);

        public Task RollbackAsync(CancellationToken cancellationToken) => transaction.RollbackAsync(cancellationToken);

        public ValueTask DisposeAsync() => transaction.DisposeAsync();
    }
}
