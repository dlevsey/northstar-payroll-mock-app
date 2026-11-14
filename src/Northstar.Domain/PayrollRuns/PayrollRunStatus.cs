namespace Northstar.Domain.PayrollRuns;

public enum PayrollRunStatus
{
    Draft,
    Calculating,
    NeedsReview,
    ReadyForApproval,
    Approved,
    ReportQueued,
    ReportGenerated,
    Submitted,
    Failed,
    ReportFailed
}
