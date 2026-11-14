namespace Northstar.Domain.PayrollRuns;

public enum PayrollRunActionStatus
{
    Success,
    Conflict,
    Blocked
}

public sealed record PayrollRunActionResult(PayrollRunActionStatus Status, string? ErrorCode = null)
{
    public static PayrollRunActionResult Success() => new(PayrollRunActionStatus.Success);
    public static PayrollRunActionResult Conflict(string code) => new(PayrollRunActionStatus.Conflict, code);
    public static PayrollRunActionResult Blocked(string code) => new(PayrollRunActionStatus.Blocked, code);
}

public sealed class PayrollRun
{
    public Guid Id { get; set; }
    public Guid OrganizationId { get; set; }
    public DateOnly PeriodStart { get; set; }
    public DateOnly PeriodEnd { get; set; }
    public PayrollRunStatus Status { get; private set; } = PayrollRunStatus.Draft;
    public int CalculationVersion { get; private set; }
    public long Version { get; private set; } = 1;
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? ApprovedAt { get; private set; }
    public List<PayrollLine> Lines { get; } = [];
    public List<ComplianceFinding> Findings { get; } = [];

    public PayrollRunActionResult BeginCalculation()
    {
        if (Status is PayrollRunStatus.Approved or PayrollRunStatus.ReportQueued or
            PayrollRunStatus.ReportGenerated or PayrollRunStatus.Submitted)
        {
            return PayrollRunActionResult.Conflict("APPROVED_RUN_REQUIRES_REOPEN");
        }

        if (Status == PayrollRunStatus.Calculating)
        {
            return PayrollRunActionResult.Conflict("CALCULATION_IN_PROGRESS");
        }

        Status = PayrollRunStatus.Calculating;
        Version++;
        return PayrollRunActionResult.Success();
    }

    public void CompleteCalculation(PayrollCalculation calculation)
    {
        ArgumentNullException.ThrowIfNull(calculation);

        if (Status != PayrollRunStatus.Calculating)
        {
            throw new InvalidOperationException("A payroll run must be calculating before results are applied.");
        }

        Lines.Clear();
        Findings.Clear();
        CalculationVersion++;

        foreach (var line in calculation.Lines)
        {
            line.PayrollRunId = Id;
            line.OrganizationId = OrganizationId;
            line.CalculationVersion = CalculationVersion;
            Lines.Add(line);
        }

        foreach (var finding in calculation.Findings)
        {
            finding.PayrollRunId = Id;
            finding.OrganizationId = OrganizationId;
            Findings.Add(finding);
        }

        Status = Findings.Any(finding => finding.Severity == FindingSeverity.Blocker)
            ? PayrollRunStatus.NeedsReview
            : PayrollRunStatus.ReadyForApproval;
        Version++;
    }

    public PayrollRunActionResult Approve(long expectedVersion, DateTimeOffset approvedAt)
    {
        if (Version != expectedVersion)
        {
            return PayrollRunActionResult.Conflict("VERSION_CONFLICT");
        }

        if (Status != PayrollRunStatus.ReadyForApproval)
        {
            return PayrollRunActionResult.Conflict("RUN_NOT_READY_FOR_APPROVAL");
        }

        if (Findings.Any(finding => finding.Severity == FindingSeverity.Blocker))
        {
            return PayrollRunActionResult.Blocked("BLOCKER_FINDINGS_EXIST");
        }

        Status = PayrollRunStatus.Approved;
        ApprovedAt = approvedAt;
        Version++;
        return PayrollRunActionResult.Success();
    }
}
