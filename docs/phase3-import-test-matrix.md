# Phase 3 import risk and test matrix

This matrix covers the first asynchronous import slice: accept a tenant-scoped request, create an import batch, retrieve synthetic provider data, validate it, and atomically promote valid time entries. Queue delivery and provider paging must be treated as at-least-once operations.

## Required invariants

- A successful `POST /api/import-batches` commits the batch and its work request together and returns `202 Accepted` with a stable batch location.
- Reusing an idempotency key with the same normalized request returns the original batch; using it with a different request returns `409 Conflict`.
- Concurrent requests with the same key create exactly one batch. The database unique constraint is the final arbiter; a read-before-insert check alone is insufficient.
- A queue message identifies a batch, but is not an authorization source. The worker derives tenant scope from the persisted batch and scopes every employee, project, classification, and time-entry lookup to it.
- Delivery of the same message, page, or provider record more than once cannot create another time entry or duplicate audit side effects.
- A repeated external ID with identical normalized content is a replay/no-op. The same external ID with conflicting content is an explicit row error; it must not silently overwrite payroll input.
- Staging and promotion have explicit boundaries. Row-level validation may produce `CompletedWithErrors`, but promotion of the accepted rows and the final batch status are one transaction.
- Cancellation before request acceptance leaves no batch or work item. Once acceptance is committed, client disconnect does not undo it. Worker shutdown rolls back an in-flight promotion, does not acknowledge the message, and does not translate cancellation into `Failed` or `Completed`.
- Provider timeouts, connection failures, `408`, `429`, and eligible `5xx` responses are transient. Invalid credentials/configuration, rejected requests, invalid JSON, and missing required contract fields are permanent unless the documented provider contract says otherwise.
- A failed attempt records a sanitized error code and correlation data, never the complete provider payload or credentials.

## Implementation risk checklist

### Idempotency and duplicate delivery

- [x] Scope request keys by organization and operation; hash all behavior-affecting request fields using a canonical representation.
- [x] Represent `Pending`, `Processing`, `Completed`, `CompletedWithErrors`, and `Failed`; do not use success-shaped results for failed work.
- [x] Claim work with an optimistic concurrency token and expiring lease; a losing local claim exits as duplicate delivery.
- [x] Enforce unique `(OrganizationId, ExternalTimeEntryId)` and reconcile request-key insert races by reloading the winning reservation. SQL Server race evidence remains pending.
- [x] Deduplicate within and across provider pages before promotion.
- [x] Make explicit retry commands idempotent, including replay after the retry has completed.

### Transactions and state

- [ ] Transaction 1 accepts the command: import batch + idempotency reservation + outbox/work record.
- [x] Provider HTTP is outside database transactions; never hold locks while waiting on the provider.
- [x] Transaction 2 promotes all accepted rows and changes the final batch state atomically.
- [x] A crash between provider read and SQL promotion is recoverable through the lease/retry state.
- [x] A duplicate notification after SQL commit observes `Completed*` and exits without re-promoting.
- [x] Use one scoped `DbContext` sequentially. Do not run EF operations concurrently or share it across worker scopes.

### Cancellation and worker lifecycle

- [x] Pass the request/host token to EF and HTTP calls.
- [x] Allow `OperationCanceledException` to represent shutdown; broad exception handling does not classify caller cancellation as a provider failure.
- [x] Dispose `HttpResponseMessage` and worker DI scopes.
- [x] Recover an abandoned `Processing` claim with a two-minute lease and recurring scan.
- [ ] Bound retries and move permanent/exhausted work to a diagnosable terminal state (and poison/dead-letter path once a real queue is added).

### Provider contract and tenant safety

- [x] Use a typed client with a configured timeout; avoid stacked retries at both HTTP and worker layers.
- [x] Honor cancellation and capped `Retry-After`/exponential delays. Injected delay/jitter remains a production-hardening exercise.
- [x] Validate required fields, field lengths, dates, nonnegative hours, and page cursors before domain mapping.
- [x] Detect repeated/non-advancing page cursors and cap imports at 100 pages.
- [x] Resolve every external employee/project/classification ID using organization-scoped maps.
- [x] Return `404` for foreign batches and their row-error resources.

## Proposed automated tests

| Priority | Boundary | Scenario | Required evidence |
|---|---|---|---|
| P0 | API + database | Same key and same request, sequentially | Both responses reference one batch; one idempotency row and one work item exist. |
| P0 | API + database | Same key and different request | `409`; original batch/request is unchanged. |
| P0 | SQL/concurrency | Two simultaneous requests use the same key | Exactly one batch is committed; the loser returns/reloads the same result rather than `500`. Run against SQL Server because SQLite locking is not equivalent. |
| P0 | Worker + database | The same message is delivered twice, sequentially and concurrently | One terminal batch, one time entry per external ID, one logical completion/audit side effect. |
| P0 | Worker + database | Provider repeats a record in one page and on a later page | Identical repeats are counted/skipped deterministically; no duplicate time entry. |
| P0 | Worker + database | Existing external ID has different hours/project/classification | Explicit row conflict; existing payroll input is not silently changed. |
| P0 | Worker + database | Failure occurs after two accepted rows but before promotion completes | No accepted time entries or terminal success status remain after rollback. Retry promotes all accepted rows once. |
| P0 | Worker lifecycle | Host cancellation during provider download | HTTP token is canceled, message remains retryable, batch is not marked completed or permanently failed. |
| P0 | Worker lifecycle | Host cancellation during promotion | SQL transaction rolls back and the next delivery completes once. |
| P0 | Tenant isolation | Tenant B gets Tenant A's batch/status/errors/retry endpoint | `404` and no data mutation. |
| P0 | Tenant isolation | A forged message combines Tenant A batch ID with Tenant B organization/external IDs | Persisted batch tenant wins; cross-tenant references are rejected and neither tenant's rows are mutated. |
| P0 | Contract | Provider returns malformed JSON or a missing required field | Permanent, sanitized batch/row error; zero promoted rows; no raw sensitive payload in response/log capture. |
| P0 | Failure mapping | Timeout, connection reset, `408`, `429`, then `503` | Remains retryable; attempt metadata advances; provider delay policy is used without real sleeps in tests. |
| P0 | Failure mapping | `400`, `401`, or `403` | No automatic retry storm; terminal/actionable error classification, subject to the documented fake-provider contract. |
| P1 | Worker lifecycle | SQL commit succeeds, acknowledgement is lost, message is redelivered | Worker detects terminal batch and makes no duplicate writes. |
| P1 | Worker lifecycle | Worker crashes after claiming and the lease expires | Another worker can reclaim; attempts are bounded and observable. |
| P1 | Contract | Provider repeats the next-page cursor | Import terminates with an explicit provider-contract error instead of looping. |
| P1 | Validation | Mixed valid rows, unknown mapping, negative hours, and duplicate conflict | Valid subset is promoted atomically; rejected rows remain queryable; status is `CompletedWithErrors` with stable counts. |
| P1 | API | Client cancels before acceptance commit versus immediately after commit | Before: no records. After: committed batch remains processable and can be retrieved. |
| P1 | Resource lifecycle | Large streamed payload and canceled read | Payload is not fully buffered, streams/responses are disposed, and the worker scope is released. |

## Test-shape guidance

- Keep provider parsing/mapping tests deterministic and table-driven; include unknown enum values, null required values, invalid dates/decimals, and duplicate IDs.
- Use the real ASP.NET Core pipeline for header/tenant behavior, `202`/`404`/`409` contracts, idempotency, and persisted batch status.
- Use a controllable fake HTTP handler/server for timeout, cancellation, status-code, body, pagination, and call-count assertions. Do not use fixed delays.
- Use SQL Server integration tests for simultaneous claims, unique-key races, execution-strategy transactions, decimal/date behavior, and rollback. SQLite remains useful for fast request-path tests but is not concurrency proof.
- Add one Playwright path only after the API/worker tests are stable: request import, observe polling move through a nonterminal state, inspect a rejected row, then verify imported time data. Most failure cases belong below E2E.

## Constraints in the current repository

- `IApplicationDbContext` gained only the transaction rollback and tracker-clear operations needed to reconcile database races; it was not widened into a generic repository.
- SQL Server connection retries are intentionally disabled while application services own manual transactions. A future SQL Server slice must wrap complete transactional units in an execution strategy before enabling retry-on-failure.
- API tests use a unique shared-memory SQLite database per factory, one anchor connection, and separate scoped connections for HTTP and worker work. This is fast relational evidence, not SQL Server locking evidence.
- The project uses `EnsureCreatedAsync`, not migrations. New tables appear in fresh test databases, but an existing local database will not evolve; migrations or a documented reset are required.
- Tenant identity is currently a trusted request header. Tests can prove object-level scoping, but this is not production authentication.
- Import rows use a composite `(OrganizationId, ImportBatchId)` foreign key. Older entities still use entity IDs, so mapping lookups remain explicitly tenant-scoped.
- There is not yet a queue/outbox or Functions/host project. If Phase 3 uses an in-process worker as an incremental seam, document that accepted work is not durable across a process crash until a transactional outbox/real queue is added.

## Review snapshot after the hardening pass

The `Channel<Guid>` plus `BackgroundService` remains a useful study stepping-stone, not a durable queue or transactional outbox. The hardening pass closed the stranded-processing, timeout classification, request-key race reconciliation, tenant-errors `404`, unsafe SQL retry configuration, optimistic claim, duplicate-only UI, and process-global provider-state findings. A persisted pending state plus recurring scanner also makes a lost local notification recoverable without claiming atomic queue durability.

The intentionally deferred risks are now narrower:

- **Major — production delivery:** there is still no transactional outbox, Azure queue acknowledgement, poison/dead-letter policy, or multi-instance lease owner. Those belong to the asynchronous-report/outbox phase.
- **Major — SQL Server evidence and migrations:** fast SQLite tests do not prove simultaneous unique-key/claim races, and `EnsureCreated` cannot evolve a prior local database. Add real migrations and SQL Server container tests before treating the path as deployment-ready.
- **Improvement — duplicate meaning:** existing and repeated external IDs are safely rejected but still conflate identical replay with conflicting payroll content. A later exercise should compare normalized content and use distinct codes.
- **Improvement — retry timing:** production code uses concrete `Task.Delay`; typed-client tests avoid real waits with zero backoff, but an injected delay policy would make `Retry-After` assertions and jitter deterministic.
- **Improvement — large payloads:** responses use streaming headers but pages are deserialized into memory and the aggregate result is retained through promotion. Add record/page limits or streaming staging before large-file testing.
