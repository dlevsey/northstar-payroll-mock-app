# Testing strategy

Use the cheapest boundary that proves the risk:

- Domain tests prove inclusive date boundaries, precedence, ambiguity, rounding, and run transitions without I/O.
- API integration tests use the real ASP.NET Core pipeline, a background worker, a deterministic provider fake, and SQLite relational constraints. They prove `202` polling, row promotion, failures, command idempotency, retries, and tenant-safe `404` responses in addition to payroll behavior.
- Typed-client tests control HTTP status, bodies, and cancellation to prove retry and exception classification without real waits.
- Angular unit tests verify polling, mixed row diagnostics, duplicate counts, and retry requests with a controlled HTTP backend.
- Playwright proves the real import path and the complete calculate, remediate, recalculate, and approve path through the UI and API.

SQL Server container tests are the next addition for provider-specific query translation, precision, row-version semantics, migrations, and locking. The in-memory EF provider is intentionally not used.

Fixed one-shot delays are not part of the asynchronous test strategy. API tests poll to a bounded terminal condition; Angular advances a virtual timer; Playwright waits on user-visible state. SQL Server concurrency tests and a durable-queue Function suite remain later additions and are not implied by the fast SQLite coverage.
