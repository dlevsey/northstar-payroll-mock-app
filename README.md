# Northstar Payroll Compliance Lab

Northstar is a mock app for my own practice. It is not a real product or company, and all data and rules are made up. I had it created with known bugs for me to try to solve: they are in the code-review exercises in `training/`, which are never merged into `main`.

The current repository contains the calculation slice plus the Phase 3 provider-import study slice:

```text
Angular workbench
  -> ASP.NET Core controller
  -> application use case
  -> persisted import batch + idempotency reservation
  -> recoverable in-process worker
  -> typed fake-provider HTTP client with bounded retries
  -> staged row validation + atomic promotion
  -> imported time entries / row-level diagnostics
```

It also retains the complete payroll path: calculate the seeded weekly run, surface a missing-rate blocker and fringe warning, add the missing carpenter rate, recalculate without duplicate lines, and approve with an optimistic version check.

## Prerequisites

- .NET SDK 10.0.301 or a compatible 10.0 feature band
- Node.js 22 and npm 11
- Chromium only when running Playwright
- Docker Desktop with WSL integration only when using the optional SQL Server/Azurite services

## Run locally

Start the synthetic payroll provider in one terminal:

```bash
dotnet run --project tools/Northstar.FakePayrollProvider --urls http://localhost:5130
```

Start the API in a second terminal:

```bash
dotnet restore
dotnet run --project src/Northstar.Api
```

The default development database is a zero-setup SQLite file. It is created and seeded automatically.

Start Angular in a third terminal:

```bash
cd src/Northstar.Web
npm install
npm start
```

Open `http://localhost:4200`. The import panel can run valid, mixed, and transient-retry provider scenarios and polls until the batch reaches a truthful terminal state. The payroll panel supports the calculate/remediate/recalculate/approve path.

To reset the demo, stop the API and remove `src/Northstar.Api/northstar.db`; the next start recreates the synthetic seed data. This reset is required when moving an older local database to this slice because the lab intentionally still uses `EnsureCreated` rather than migrations.

### Import lifecycle in this increment

- `POST /api/import-batches` requires an organization-scoped `Idempotency-Key` and returns `202 Accepted`.
- A local worker retrieves paged provider data outside a database transaction, validates each staging row, then promotes accepted rows and the terminal status atomically.
- Duplicate external time-entry IDs are retained as diagnosable row issues and cannot create duplicate payroll input.
- Provider timeouts, throttling, connection failures, and eligible server failures are retried with a bound; exhausted transient work can be retried through the API.
- A persisted `Pending` state, recurring recovery scan, optimistic claim, and expiring processing lease make a lost in-memory notification or interrupted worker recoverable.

The `Channel` notification is deliberately an incremental local seam, not an Azure queue or transactional outbox. Phase 4 replaces it with durable outbox dispatch while preserving the application contracts.

## Test

```bash
dotnet test Northstar.PayrollCompliance.sln

cd src/Northstar.Web
npm ci
npm run build
npm test

cd ../../tests/e2e
npm ci
npx playwright install chromium
npm test
```

The API integration tests use a fresh SQLite in-memory database for every test. Playwright starts both real development servers and uses a unique temporary database.

## Optional SQL Server

The application includes the SQL Server provider and a Compose service for the SQL-specific exercises:

```bash
docker compose up -d sqlserver

Database__Provider=SqlServer \
ConnectionStrings__Northstar='Server=localhost,1433;Database=NorthstarPayroll;User Id=sa;Password=Northstar_Local_Only_42!;TrustServerCertificate=True' \
dotnet run --project src/Northstar.Api
```

The checked-in password is local-only sample configuration. Do not reuse it outside the lab.

## Local study authentication

The first slice deliberately uses explicit headers instead of pretending to provide production authentication:

```text
X-Organization-Id: 11111111-1111-1111-1111-111111111111
X-User-Role: PayrollManager
```

Angular supplies those headers through an interceptor. Tenant-scoped queries return `404` when a resource belongs to another organization. Replace this seam with JWT/Entra claims in the security phase.

## Repository map

- `src/Northstar.Domain` — framework-free entities, selector, calculator, and run state machine
- `src/Northstar.Application` — use cases, explicit results, idempotency, and persistence contracts
- `src/Northstar.Infrastructure` — EF Core, typed provider client, recoverable local worker, SQLite/SQL Server providers, and seed data
- `src/Northstar.Api` — REST endpoints, problem details, local header context, and OpenAPI
- `src/Northstar.Web` — Angular import and payroll review workbench
- `tools/Northstar.FakePayrollProvider` — deterministic success, mixed, throttling, malformed, permanent-failure, and timeout scenarios
- `tests` — xUnit domain/API tests and a Playwright critical path
- `training` — PR-style review exercises that are not applied to `main`
- `docs` — decisions and testing notes

See [architecture.md](docs/architecture.md) for the boundaries and deliberate trade-offs. Durable queue/outbox dispatch, reports, Blob Storage, and legacy adapters remain later phases rather than empty scaffolding.
