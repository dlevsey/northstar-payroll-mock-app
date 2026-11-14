using Northstar.Application.Idempotency;
using Northstar.Domain.Employees;
using Northstar.Domain.Imports;
using Northstar.Domain.Organizations;
using Northstar.Domain.PayrollRuns;
using Northstar.Domain.Projects;
using Northstar.Domain.TimeEntries;
using Northstar.Domain.WageRates;

namespace Northstar.Application.Abstractions;

public interface IApplicationTransaction : IAsyncDisposable
{
    Task CommitAsync(CancellationToken cancellationToken);
    Task RollbackAsync(CancellationToken cancellationToken);
}

public interface IApplicationDbContext
{
    IQueryable<Organization> Organizations { get; }
    IQueryable<Employee> Employees { get; }
    IQueryable<Project> Projects { get; }
    IQueryable<WorkClassification> WorkClassifications { get; }
    IQueryable<WageRate> WageRates { get; }
    IQueryable<TimeEntry> TimeEntries { get; }
    IQueryable<PayrollRun> PayrollRuns { get; }
    IQueryable<PayrollLine> PayrollLines { get; }
    IQueryable<ComplianceFinding> ComplianceFindings { get; }
    IQueryable<IdempotencyRecord> IdempotencyRecords { get; }
    IQueryable<ImportBatch> ImportBatches { get; }
    IQueryable<ImportRow> ImportRows { get; }

    void Add<TEntity>(TEntity entity) where TEntity : class;
    void ClearTracking();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    Task<IApplicationTransaction> BeginTransactionAsync(CancellationToken cancellationToken);
}
