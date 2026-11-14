# Architecture

Northstar is a modular monolith. The payroll request path remains deliberately short enough to trace:

```text
Angular page
  -> PayrollRunsController
  -> PayrollRunService
  -> EF Core / ApplicationDbContext
  -> WageRateSelector + PayrollCalculator
  -> one database transaction
```

## Dependency direction

- `Northstar.Domain` has no project or framework dependencies.
- `Northstar.Application` depends on Domain and owns use cases and persistence interfaces.
- `Northstar.Infrastructure` implements persistence and seed data.
- `Northstar.Api` is the HTTP composition root.
- `Northstar.Web` communicates with the API only.

SQLite is the zero-setup development and fast integration-test provider. SQL Server is selectable with configuration and remains the required evidence boundary for provider-specific locking, precision, and concurrency exercises.

## Provider import boundary

```text
POST /api/import-batches + Idempotency-Key
  -> ImportBatchService commits Pending batch + idempotency record
  -> local Channel notification
  -> ImportBatchWorker claims with optimistic Version + expiring lease
  -> FakePayrollProviderClient (HTTP, bounded retry, cancellation)
  -> ImportBatchProcessor stages and validates every provider row
  -> one transaction inserts accepted TimeEntries + rows + terminal status
  -> Angular polls GET /api/import-batches/{id}
```

The database state, not the in-memory notification, is the recoverable source of local work. A recurring scan republishes `Pending` batches and expired `Processing` leases. This closes the useful study failure modes for a single-process lab but is not presented as production durability: there is no atomic SQL-to-queue handoff, cross-instance lease owner, poison queue, or Azure acknowledgement yet. The next async phase introduces the transactional outbox and Storage Queue behind the existing application-owned queue interface.

Provider payloads stay outside the domain. External employee, project, and classification identifiers are resolved through organization-scoped maps. The database uniquely constrains `(OrganizationId, ExternalTimeEntryId)`, while staging keeps duplicate and invalid rows visible without silently coercing missing fields or hours.

## First-slice decisions

- Dates are inclusive at both effective-date boundaries.
- Rate selection is pure and never turns a missing rate into zero dollars.
- Lower `WageRateScope` rank has higher precedence; priority breaks ties within a scope.
- Multiple active candidates at the same scope and priority are ambiguous.
- Payroll lines are replaced in one transaction during recalculation.
- A missing or ambiguous rate is a blocker. A fringe shortfall is a warning in this fictional lab.
- Tenant ownership is checked before a resource is returned, producing `404` for cross-tenant IDs.
- Repeated calculate requests use an organization-scoped idempotency record.
- Repeated import and retry commands use organization/operation-scoped idempotency records; the database unique key arbitrates a request race.
- Provider I/O never occurs while the promotion transaction is open.
- Duplicate-only batches are `CompletedWithErrors`, and imported/error/duplicate counts must add to the provider total.
- Unexpected worker failures become sanitized retryable failures; host cancellation is allowed to propagate and the lease enables later recovery.
