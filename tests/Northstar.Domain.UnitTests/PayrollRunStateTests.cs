using Northstar.Domain.PayrollRuns;

namespace Northstar.Domain.UnitTests;

public sealed class PayrollRunStateTests
{
    [Fact]
    public void CompleteCalculation_MovesRunToNeedsReviewWhenBlockerExists()
    {
        var run = Run();
        Assert.Equal(PayrollRunActionStatus.Success, run.BeginCalculation().Status);
        var calculation = new PayrollCalculation(
            [],
            [Finding(run, FindingSeverity.Blocker)]);

        run.CompleteCalculation(calculation);

        Assert.Equal(PayrollRunStatus.NeedsReview, run.Status);
        Assert.Equal(PayrollRunActionStatus.Conflict, run.Approve(run.Version, DateTimeOffset.UtcNow).Status);
    }

    [Fact]
    public void CompleteCalculation_AllowsApprovalWhenOnlyWarningsExist()
    {
        var run = Run();
        Assert.Equal(PayrollRunActionStatus.Success, run.BeginCalculation().Status);
        run.CompleteCalculation(new PayrollCalculation([], [Finding(run, FindingSeverity.Warning)]));
        var versionBeforeApproval = run.Version;

        var result = run.Approve(versionBeforeApproval, DateTimeOffset.UtcNow);

        Assert.Equal(PayrollRunActionStatus.Success, result.Status);
        Assert.Equal(PayrollRunStatus.Approved, run.Status);
        Assert.Equal(versionBeforeApproval + 1, run.Version);
    }

    [Fact]
    public void Approve_RejectsAStaleVersion()
    {
        var run = Run();
        Assert.Equal(PayrollRunActionStatus.Success, run.BeginCalculation().Status);
        run.CompleteCalculation(new PayrollCalculation([], []));

        var result = run.Approve(run.Version - 1, DateTimeOffset.UtcNow);

        Assert.Equal(PayrollRunActionStatus.Conflict, result.Status);
        Assert.Equal("VERSION_CONFLICT", result.ErrorCode);
        Assert.Equal(PayrollRunStatus.ReadyForApproval, run.Status);
    }

    private static PayrollRun Run() =>
        new()
        {
            Id = Guid.NewGuid(),
            OrganizationId = Guid.NewGuid(),
            PeriodStart = new DateOnly(2026, 8, 24),
            PeriodEnd = new DateOnly(2026, 8, 30),
            CreatedAt = DateTimeOffset.UtcNow
        };

    private static ComplianceFinding Finding(PayrollRun run, FindingSeverity severity) =>
        new()
        {
            Id = Guid.NewGuid(),
            OrganizationId = run.OrganizationId,
            PayrollRunId = run.Id,
            Severity = severity,
            Code = "TEST",
            Message = "Test finding"
        };
}
