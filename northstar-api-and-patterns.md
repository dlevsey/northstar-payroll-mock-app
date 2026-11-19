# Northstar API reference and patterns

A study companion for the Northstar Payroll Compliance Lab. It answers three questions about the code in this repository.

1. **Which outside APIs does the code call?** Every .NET, ASP.NET Core, EF Core, xUnit, Angular, RxJS, Vitest and Playwright API in use, each linked to its official documentation page and, for .NET, to its source code.
2. **Which language features does it rely on?** Every C# feature found in the code, with a count, a description in plain words and a link to the C# reference. The same for Angular template syntax and TypeScript.
3. **Which patterns is it built from?** The 26 patterns that shape the code, each shown with real excerpts, read line by line, and linked to the documentation that explains it.

| What was read | Count |
|---|---:|
| C# source files, in 7 projects | 54 |
| External .NET APIs (types and members), in 37 namespaces | 568 |
| Places where those APIs are used | 3,570 |
| C# language features | 81 |
| Types declared in this codebase | 90 |
| Angular, RxJS, Vitest, Playwright and browser APIs | 84 |
| Code excerpts shown | 97 |
| Different documentation and source pages linked, each one checked | 1,617 |

*Snapshot: commit `7fff3f4` on `main` plus the uncommitted edits in the working tree, read on 16 November 2026.*

## Contents

- [1. How to read this document](#1-how-to-read-this-document)
  - [What each part is for](#what-each-part-is-for)
  - [The three kinds of links](#the-three-kinds-of-links)
  - [How locations are written](#how-locations-are-written)
  - [How the counts were made](#how-the-counts-were-made)
  - [How code excerpts are shown](#how-code-excerpts-are-shown)
  - [Things to keep in mind](#things-to-keep-in-mind)
- [2. Solution map](#2-solution-map)
  - [Projects](#projects)
  - [Dependency direction](#dependency-direction)
  - [How a request travels](#how-a-request-travels)
  - [HTTP endpoints](#http-endpoints)
  - [NuGet packages](#nuget-packages)
  - [npm packages](#npm-packages)
- [3. Patterns](#3-patterns)
  - [Patterns at a glance](#patterns-at-a-glance)
  - [Pattern 1: Layered modular monolith](#pattern-1-layered-modular-monolith)
  - [Pattern 2: Composition root and service registration](#pattern-2-composition-root-and-service-registration)
  - [Pattern 3: Constructor injection with primary constructors](#pattern-3-constructor-injection-with-primary-constructors)
  - [Pattern 4: Request pipeline and host startup](#pattern-4-request-pipeline-and-host-startup)
  - [Pattern 5: Thin attribute-routed controllers](#pattern-5-thin-attribute-routed-controllers)
  - [Pattern 6: Result objects mapped to HTTP responses](#pattern-6-result-objects-mapped-to-http-responses)
  - [Pattern 7: Tenant scoping from header to query](#pattern-7-tenant-scoping-from-header-to-query)
  - [Pattern 8: Idempotent commands with an Idempotency-Key](#pattern-8-idempotent-commands-with-an-idempotency-key)
  - [Pattern 9: Accept now, process later](#pattern-9-accept-now-process-later)
  - [Pattern 10: Background worker with a queue and a recovery scan](#pattern-10-background-worker-with-a-queue-and-a-recovery-scan)
  - [Pattern 11: Typed HttpClient with validated options](#pattern-11-typed-httpclient-with-validated-options)
  - [Pattern 12: Bounded retries, timeouts and exception translation](#pattern-12-bounded-retries-timeouts-and-exception-translation)
  - [Pattern 13: Persistence behind an interface](#pattern-13-persistence-behind-an-interface)
  - [Pattern 14: EF Core model configuration with the Fluent API](#pattern-14-ef-core-model-configuration-with-the-fluent-api)
  - [Pattern 15: Read queries with no tracking, a tenant filter and projection](#pattern-15-read-queries-with-no-tracking-a-tenant-filter-and-projection)
  - [Pattern 16: Optimistic concurrency with a version number](#pattern-16-optimistic-concurrency-with-a-version-number)
  - [Pattern 17: Entities as state machines and pure domain services](#pattern-17-entities-as-state-machines-and-pure-domain-services)
  - [Pattern 18: Async all the way with cancellation](#pattern-18-async-all-the-way-with-cancellation)
  - [Pattern 19: Small conventions used everywhere](#pattern-19-small-conventions-used-everywhere)
  - [Pattern 20: Tests at three levels in .NET](#pattern-20-tests-at-three-levels-in-net)
  - [Pattern 21: Minimal API endpoints in the fake provider](#pattern-21-minimal-api-endpoints-in-the-fake-provider)
  - [Pattern 22: Angular standalone bootstrap and an HTTP interceptor](#pattern-22-angular-standalone-bootstrap-and-an-http-interceptor)
  - [Pattern 23: Angular services that return Observables](#pattern-23-angular-services-that-return-observables)
  - [Pattern 24: Signals, computed values and control-flow templates](#pattern-24-signals-computed-values-and-control-flow-templates)
  - [Pattern 25: Polling with RxJS and automatic cleanup](#pattern-25-polling-with-rxjs-and-automatic-cleanup)
  - [Pattern 26: Frontend tests with Vitest and Playwright](#pattern-26-frontend-tests-with-vitest-and-playwright)
- [4. C# language features](#4-c-language-features)
  - [How to read the feature tables](#how-to-read-the-feature-tables)
  - [The language version](#the-language-version)
  - [What the counts show](#what-the-counts-show)
  - [The twelve most used features](#the-twelve-most-used-features)
  - [Types and members](#types-and-members)
  - [Async and resource handling](#async-and-resource-handling)
  - [Pattern matching](#pattern-matching)
  - [Null handling](#null-handling)
  - [Expressions, literals and operators](#expressions-literals-and-operators)
  - [Statements and exceptions](#statements-and-exceptions)
  - [File organization](#file-organization)
- [5. .NET API inventory](#5-net-api-inventory)
  - [How to read the API tables](#how-to-read-the-api-tables)
  - [The twenty most used APIs](#the-twenty-most-used-apis)
  - [Namespaces at a glance](#namespaces-at-a-glance)
  - [The .NET base class library](#the-net-base-class-library)
  - [ASP.NET Core](#aspnet-core)
  - [Microsoft.Extensions](#microsoftextensions)
  - [Entity Framework Core and SQLite](#entity-framework-core-and-sqlite)
  - [xUnit test framework](#xunit-test-framework)
- [6. Frontend inventory](#6-frontend-inventory)
  - [How the frontend numbers were made](#how-the-frontend-numbers-were-made)
  - [How to read the frontend tables](#how-to-read-the-frontend-tables)
  - [The files of the frontend](#the-files-of-the-frontend)
  - [Angular](#angular)
  - [Angular testing](#angular-testing)
  - [RxJS](#rxjs)
  - [Vitest](#vitest)
  - [Playwright](#playwright)
  - [Browser and JavaScript](#browser-and-javascript)
  - [Angular template syntax](#angular-template-syntax)
  - [TypeScript features](#typescript-features)
- [7. Tooling and configuration](#7-tooling-and-configuration)
  - [How to read this section](#how-to-read-this-section)
  - [The files at a glance](#the-files-at-a-glance)
  - [The SDK version: `global.json`](#the-sdk-version-globaljson)
  - [Settings for every project: `Directory.Build.props`](#settings-for-every-project-directorybuildprops)
  - [Project files and the solution file](#project-files-and-the-solution-file)
  - [Package versions: `Directory.Packages.props`](#package-versions-directorypackagesprops)
  - [Code style: `.editorconfig` and `dotnet format`](#code-style-editorconfig-and-dotnet-format)
  - [Application settings: `appsettings.json`](#application-settings-appsettingsjson)
  - [Launch profiles: `launchSettings.json`](#launch-profiles-launchsettingsjson)
  - [Request files: `.http`](#request-files-http)
  - [The CI workflow: `.github/workflows/ci.yml`](#the-ci-workflow-githubworkflowsciyml)
  - [Optional services: `docker-compose.yml`](#optional-services-docker-composeyml)
  - [The Angular workspace: `angular.json`](#the-angular-workspace-angularjson)
  - [npm packages and scripts: `package.json`](#npm-packages-and-scripts-packagejson)
  - [TypeScript configuration: the `tsconfig` files](#typescript-configuration-the-tsconfig-files)
  - [Formatting and the development proxy: `.prettierrc` and `proxy.conf.json`](#formatting-and-the-development-proxy-prettierrc-and-proxyconfjson)
  - [Unit test runner settings: `angular.json` and `vitest.config.ts`](#unit-test-runner-settings-angularjson-and-vitestconfigts)
  - [End-to-end test runner settings: `playwright.config.ts`](#end-to-end-test-runner-settings-playwrightconfigts)
  - [Files that git leaves out: `.gitignore`](#files-that-git-leaves-out-gitignore)
- [Appendix A: Types declared in this codebase](#appendix-a-types-declared-in-this-codebase)
  - [Northstar.Domain](#northstardomain)
  - [Northstar.Application](#northstarapplication)
  - [Northstar.Infrastructure](#northstarinfrastructure)
  - [Northstar.Api](#northstarapi)
  - [Northstar.FakePayrollProvider](#northstarfakepayrollprovider)
  - [Northstar.Domain.UnitTests](#northstardomainunittests)
  - [Northstar.Api.IntegrationTests](#northstarapiintegrationtests)
- [Appendix B: File index](#appendix-b-file-index)
- [Appendix C: How the links were made and checked](#appendix-c-how-the-links-were-made-and-checked)
  - [Finding the APIs](#finding-the-apis)
  - [Documentation links for .NET APIs](#documentation-links-for-net-apis)
  - [Source links to the .NET Source Browser](#source-links-to-the-net-source-browser)
  - [Source links to GitHub](#source-links-to-github)
  - [Links to guides and reference pages](#links-to-guides-and-reference-pages)
  - [File links and code excerpts](#file-links-and-code-excerpts)
  - [The summaries in the API tables](#the-summaries-in-the-api-tables)
  - [Totals and limits](#totals-and-limits)

## 1. How to read this document

### What each part is for

| Part | Use it when you want to |
|---|---|
| [2. Solution map](#2-solution-map) | Find which project a file belongs to, follow a request from the browser to the database, or see which HTTP endpoints exist. |
| [3. Patterns](#3-patterns) | Understand *why the code is shaped the way it is*. Start here if you are reading the codebase for the first time. |
| [4. C# language features](#4-c-language-features) | Look up a piece of syntax you met in the code, such as `is not null`, `=>` members or `await using`. |
| [5. .NET API inventory](#5-net-api-inventory) | Look up a type or method the code calls, see where it is called, and jump to its documentation or source. |
| [6. Frontend inventory](#6-frontend-inventory) | Do the same for the Angular app and its tests. |
| [7. Tooling and configuration](#7-tooling-and-configuration) | Understand the files that are not code: project files, `global.json`, `.editorconfig`, the CI workflow, the Angular and test configuration. |
| Appendices | See every type this codebase declares, every file this document points into, and how the links were produced and checked. |

### The three kinds of links

- **Documentation links** go to the official page for an API or a concept: Microsoft Learn (the [.NET API browser](https://learn.microsoft.com/en-us/dotnet/api/?view=net-10.0), the ASP.NET Core documentation and the EF Core documentation), angular.dev, rxjs.dev, vitest.dev, playwright.dev, MDN and the TypeScript handbook. Each .NET API link asks for the .NET 10 version of its page, for example with `view=net-10.0`, `view=aspnetcore-10.0` or `view=efcore-10.0`. [Appendix C](#appendix-c-how-the-links-were-made-and-checked) lists the five views that are used.
- **Source links** go to the code behind a .NET API. Most go to the [.NET Source Browser (source.dot.net)](https://source.dot.net/). EF Core, Microsoft.Data.Sqlite and xUnit are not indexed there, so those go to the matching release tag on GitHub ([dotnet/efcore on GitHub (tag v10.0.15)](https://github.com/dotnet/efcore/tree/v10.0.15), [xunit/assert.xunit on GitHub (tag v2-2.9.3)](https://github.com/xunit/assert.xunit/tree/v2-2.9.3), [xunit/xunit on GitHub (tag v2-2.9.3)](https://github.com/xunit/xunit/tree/v2-2.9.3)).
- **File links** such as [Api/Program.cs:6](src/Northstar.Api/Program.cs#L6) point into this repository. They are paths from the repository root, so they open the file when this Markdown file is read from the repository root in VS Code or on GitHub. In the PDF they are plain text.

### How locations are written

A location is a short project name, a file name and a line number. `Api/Program.cs:6` is line 6 of `src/Northstar.Api/Program.cs`, and `:6-21` is a range of lines.

| Shown as | Folder |
|---|---|
| `Api/` | `src/Northstar.Api/` |
| `Application/` | `src/Northstar.Application/` |
| `Domain/` | `src/Northstar.Domain/` |
| `Infrastructure/` | `src/Northstar.Infrastructure/` |
| `Web/` | `src/Northstar.Web/` |
| `IntegrationTests/` | `tests/Northstar.Api.IntegrationTests/` |
| `UnitTests/` | `tests/Northstar.Domain.UnitTests/` |
| `e2e/` | `tests/e2e/` |
| `FakeProvider/` | `tools/Northstar.FakePayrollProvider/` |

Sub-folders are left out to keep the tables narrow. When two files in one project share a name, the parent folder is added. [Appendix B](#appendix-b-file-index) maps every short name to its full path.

### How the counts were made

The C# numbers come from the C# compiler (Roslyn), not from text search. All 54 C# files in the 7 projects were loaded into the compiler with their real references, and every name in the code was resolved to the API it refers to. That is why overloads are told apart, why calls through `var` and extension methods are resolved, and why APIs that never appear by name are still listed. `Task<TResult>.GetAwaiter()` is the clearest example: nobody writes it, but every `await` calls it.

A *use* is one place in the code where a name refers to that API. The count on a type's own row covers direct mentions of the type's name only. Calls to its members are counted on the member rows below it.

The frontend numbers in section 6 come from pattern matching over the TypeScript and HTML files, so they count matching lines rather than resolved symbols.

### How code excerpts are shown

Every excerpt is copied from the snapshot without changes, apart from removing the shared indentation. The caption gives the file and the line range, and in the PDF each line carries its real line number. Where lines are left out, a comment says which ones, for example `// ... lines 39–63 omitted ...`. The comment `// ... continues in the file ...` means the type or method goes on after the excerpt ends.

In the explanations, a quote such as `var builder = WebApplication.CreateBuilder(args);` (line 6) is copied from the excerpt above it, and the number is the line it sits on in the file.

### Things to keep in mind

- **The Source Browser shows the current main branch** of the .NET repositories, not the .NET 10 release. The code you see there can be newer than the code that runs here. When the two disagree, the documentation link is the one that matches this solution's version.
- **EF Core, Microsoft.Data.Sqlite and xUnit source links** point at the release tags that match the package versions in [Directory.Packages.props](Directory.Packages.props) (`v10.0.15` and `v2-2.9.3`).
- **The "What it does" text** in the .NET tables is the first sentence of the summary that each library ships in its XML documentation file ([Appendix C](#appendix-c-how-the-links-were-made-and-checked) says more). The words are Microsoft's, and those of the xUnit project on the `Xunit` rows. Four APIs have no summary there. Those rows are marked † and described in this document's own words.
- **Line numbers follow the snapshot.** Four files had uncommitted edits in the working tree: [Directory.Packages.props](Directory.Packages.props), [Application/IImportBatchQueue.cs](src/Northstar.Application/Abstractions/IImportBatchQueue.cs), [Application/IPayrollProviderClient.cs](src/Northstar.Application/Abstractions/IPayrollProviderClient.cs) and [Application/ImportBatchProcessor.cs](src/Northstar.Application/Imports/ImportBatchProcessor.cs). They are shown as they were in the working tree. Line numbers drift as the code changes.
- **This document describes; it does not review.** It explains what the code does and which documented technique each part follows. It does not judge the code, and it does not draw on the review exercises in `training/`.

## 2. Solution map

Northstar is a *modular monolith*: one application that is deployed as a whole, split into projects that may only depend on each other in one direction. The repository describes itself in [README.md](README.md) and [docs/architecture.md](docs/architecture.md), and the decision is recorded in [docs/adr/0001-use-modular-monolith.md](docs/adr/0001-use-modular-monolith.md).

### Projects

| Project | Kind | What lives there | C# files | Types |
|---|---|---|---:|---:|
| `Northstar.Domain`<br>`src/Northstar.Domain` | Class library | Entities, enums, the wage-rate selector, the payroll calculator, and the state rules for payroll runs and import batches. It references no other project and no package. | 15 | 25 |
| `Northstar.Application`<br>`src/Northstar.Application` | Class library | Use cases (the `...Service` classes and the import processor), result types, the records sent to the client, the idempotency record, and the interfaces that Infrastructure implements. | 15 | 29 |
| `Northstar.Infrastructure`<br>`src/Northstar.Infrastructure` | Class library | The EF Core `DbContext` and its model configuration, seed data, the typed HTTP client for the payroll provider, the in-memory queue and the background worker. | 8 | 11 |
| `Northstar.Api`<br>`src/Northstar.Api` | ASP.NET Core web app | Controllers, the header-based user context, and `Program.cs`, which wires everything together and starts the host. | 6 | 7 |
| `Northstar.FakePayrollProvider`<br>`tools/Northstar.FakePayrollProvider` | ASP.NET Core web app (minimal API) | A stand-in for an outside payroll provider. It serves scripted scenarios: success, mixed, throttling, malformed, permanent failure and timeout. | 1 | 6 |
| `Northstar.Domain.UnitTests`<br>`tests/Northstar.Domain.UnitTests` | xUnit test project | Tests of the domain types, with no database and no HTTP. | 4 | 5 |
| `Northstar.Api.IntegrationTests`<br>`tests/Northstar.Api.IntegrationTests` | xUnit test project | Tests that run the real API in memory against SQLite, plus tests of the typed HTTP client. | 5 | 7 |
| `northstar-web`<br>`src/Northstar.Web` | Angular 22 app | The import and payroll review workbench that runs in the browser. | – | – |
| `northstar-e2e`<br>`tests/e2e` | Playwright project | Two browser tests that drive the real UI, API and fake provider together. | – | – |

All seven .NET projects target `net10.0` and are listed in [Northstar.PayrollCompliance.sln](Northstar.PayrollCompliance.sln). [Appendix A](#appendix-a-types-declared-in-this-codebase) lists every type they declare.

### Dependency direction

An arrow means "has a project reference to".

```text
Northstar.Api ----------------> Northstar.Application ----> Northstar.Domain
      |                                  ^                         ^
      +----> Northstar.Infrastructure ---+-------------------------+
```

The rules, in the repository's own words ([docs/architecture.md:16-20](docs/architecture.md#L16-L20)):

> - `Northstar.Domain` has no project or framework dependencies.
> - `Northstar.Application` depends on Domain and owns use cases and persistence interfaces.
> - `Northstar.Infrastructure` implements persistence and seed data.
> - `Northstar.Api` is the HTTP composition root.
> - `Northstar.Web` communicates with the API only.

[Pattern 1](#pattern-1-layered-modular-monolith) shows where the project files enforce this.

### How a request travels

The codebase has two paths worth tracing. The first is the import path, as the README draws it ([README.md:7-16](README.md#L7-L16)):

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

| Step | Where it happens | Pattern |
|---|---|---|
| Angular workbench | [Web/import-panel.component.ts:46](src/Northstar.Web/src/app/imports/import-panel.component.ts#L46) | [24](#pattern-24-signals-computed-values-and-control-flow-templates), [25](#pattern-25-polling-with-rxjs-and-automatic-cleanup) |
| ASP.NET Core controller | [Api/ImportBatchesController.cs:67](src/Northstar.Api/Controllers/ImportBatchesController.cs#L67) | [5](#pattern-5-thin-attribute-routed-controllers), [9](#pattern-9-accept-now-process-later) |
| Application use case | [Application/ImportBatchService.cs:107](src/Northstar.Application/Imports/ImportBatchService.cs#L107) | [6](#pattern-6-result-objects-mapped-to-http-responses), [7](#pattern-7-tenant-scoping-from-header-to-query) |
| Persisted import batch and idempotency reservation | [Application/ImportBatchService.cs:156-167](src/Northstar.Application/Imports/ImportBatchService.cs#L156-L167) | [8](#pattern-8-idempotent-commands-with-an-idempotency-key) |
| Recoverable in-process worker | [Infrastructure/ImportBatchWorker.cs:16](src/Northstar.Infrastructure/Messaging/ImportBatchWorker.cs#L16) | [10](#pattern-10-background-worker-with-a-queue-and-a-recovery-scan) |
| Typed HTTP client with bounded retries | [Infrastructure/FakePayrollProviderClient.cs:70](src/Northstar.Infrastructure/Integrations/FakePayrollProviderClient.cs#L70) | [11](#pattern-11-typed-httpclient-with-validated-options), [12](#pattern-12-bounded-retries-timeouts-and-exception-translation) |
| Staged row validation and atomic promotion | [Application/ImportBatchProcessor.cs:120-170](src/Northstar.Application/Imports/ImportBatchProcessor.cs#L120-L170) | [13](#pattern-13-persistence-behind-an-interface), [16](#pattern-16-optimistic-concurrency-with-a-version-number) |

The second is the payroll path ([docs/architecture.md:5-12](docs/architecture.md#L5-L12)):

```text
Angular page
  -> PayrollRunsController
  -> PayrollRunService
  -> EF Core / ApplicationDbContext
  -> WageRateSelector + PayrollCalculator
  -> one database transaction
```

| Step | Where it happens | Pattern |
|---|---|---|
| Angular page | [Web/app.ts:32](src/Northstar.Web/src/app/app.ts#L32) | [23](#pattern-23-angular-services-that-return-observables), [24](#pattern-24-signals-computed-values-and-control-flow-templates) |
| `PayrollRunsController` | [Api/PayrollRunsController.cs:47](src/Northstar.Api/Controllers/PayrollRunsController.cs#L47) | [5](#pattern-5-thin-attribute-routed-controllers) |
| `PayrollRunService` | [Application/PayrollRunService.cs:43](src/Northstar.Application/PayrollRuns/PayrollRunService.cs#L43) | [6](#pattern-6-result-objects-mapped-to-http-responses), [8](#pattern-8-idempotent-commands-with-an-idempotency-key) |
| EF Core and `ApplicationDbContext` | [Application/PayrollRunService.cs:104-131](src/Northstar.Application/PayrollRuns/PayrollRunService.cs#L104-L131) | [13](#pattern-13-persistence-behind-an-interface), [15](#pattern-15-read-queries-with-no-tracking-a-tenant-filter-and-projection) |
| `WageRateSelector` and `PayrollCalculator` | [Domain/WageRateSelector.cs:13](src/Northstar.Domain/WageRates/WageRateSelector.cs#L13), [Domain/PayrollCalculator.cs:27](src/Northstar.Domain/PayrollRuns/PayrollCalculator.cs#L27) | [17](#pattern-17-entities-as-state-machines-and-pure-domain-services) |

### HTTP endpoints

The API has 3 controllers with 11 actions, plus one health endpoint. The response columns list what each action declares with `[ProducesResponseType]`.

| Request | Handled by | Success | Other documented responses | Where |
| --- | --- | --- | --- | --- |
| `GET /api/import-batches` | `ImportBatchesController.List` | 200 `IReadOnlyList<ImportBatchView>` | – | [Api/ImportBatchesController.cs:15](src/Northstar.Api/Controllers/ImportBatchesController.cs#L15) |
| `GET /api/import-batches/{batchId:guid}` | `ImportBatchesController.Get` | 200 `ImportBatchView` | 404 (problem details) | [Api/ImportBatchesController.cs:29](src/Northstar.Api/Controllers/ImportBatchesController.cs#L29) |
| `GET /api/import-batches/{batchId:guid}/errors` | `ImportBatchesController.GetErrors` | 200 `IReadOnlyList<ImportRowView>` | 404 (problem details) | [Api/ImportBatchesController.cs:47](src/Northstar.Api/Controllers/ImportBatchesController.cs#L47) |
| `POST /api/import-batches` | `ImportBatchesController.Create` | 202 `ImportBatchView` | 403 · 409 (problem details) · 422 (problem details) | [Api/ImportBatchesController.cs:67](src/Northstar.Api/Controllers/ImportBatchesController.cs#L67) |
| `POST /api/import-batches/{batchId:guid}/retry` | `ImportBatchesController.Retry` | 202 `ImportBatchView` | 403 · 409 (problem details) · 422 (problem details) | [Api/ImportBatchesController.cs:95](src/Northstar.Api/Controllers/ImportBatchesController.cs#L95) |
| `GET /api/payroll-runs` | `PayrollRunsController.List` | 200 `IReadOnlyList<PayrollRunView>` | – | [Api/PayrollRunsController.cs:14](src/Northstar.Api/Controllers/PayrollRunsController.cs#L14) |
| `GET /api/payroll-runs/{runId:guid}` | `PayrollRunsController.Get` | 200 `PayrollRunView` | 404 (problem details) | [Api/PayrollRunsController.cs:28](src/Northstar.Api/Controllers/PayrollRunsController.cs#L28) |
| `POST /api/payroll-runs/{runId:guid}/calculate` | `PayrollRunsController.Calculate` | 200 `PayrollRunView` | 409 (problem details) · 422 (problem details) | [Api/PayrollRunsController.cs:47](src/Northstar.Api/Controllers/PayrollRunsController.cs#L47) |
| `POST /api/payroll-runs/{runId:guid}/approve` | `PayrollRunsController.Approve` | 200 `PayrollRunView` | 403 · 409 (problem details) | [Api/PayrollRunsController.cs:68](src/Northstar.Api/Controllers/PayrollRunsController.cs#L68) |
| `GET /api/wage-rates` | `WageRatesController.List` | 200 `IReadOnlyList<WageRateView>` | – | [Api/WageRatesController.cs:14](src/Northstar.Api/Controllers/WageRatesController.cs#L14) |
| `POST /api/wage-rates` | `WageRatesController.Create` | 201 `WageRateView` | 422 (problem details) | [Api/WageRatesController.cs:28](src/Northstar.Api/Controllers/WageRatesController.cs#L28) |
| `GET /health` | minimal API lambda in `Program.cs` | 200 | – | [Api/Program.cs:34](src/Northstar.Api/Program.cs#L34) |

- Every controller action starts by reading the organization from the `X-Organization-Id` header, and returns `401` with a problem-details body when the header is missing or is not a GUID. There are 11 such checks, one per action. See [Pattern 7](#pattern-7-tenant-scoping-from-header-to-query).
- `{runId:guid}` and `{batchId:guid}` are route constraints. The route only matches when that part of the URL parses as a GUID ([Route constraints](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/routing?view=aspnetcore-10.0#route-constraints)).
- The three `POST` actions that start work (`Calculate`, `Create` and `Retry`) read an `Idempotency-Key` header. See [Pattern 8](#pattern-8-idempotent-commands-with-an-idempotency-key).
- The fake provider has two endpoints of its own, `GET /health` ([FakeProvider/Program.cs:14](tools/Northstar.FakePayrollProvider/Program.cs#L14)) and `GET /api/time-entries` ([FakeProvider/Program.cs:16](tools/Northstar.FakePayrollProvider/Program.cs#L16)). See [Pattern 21](#pattern-21-minimal-api-endpoints-in-the-fake-provider).
- Ready-made requests are in [docs/api/example-requests.http](docs/api/example-requests.http) and [Api/Northstar.Api.http](src/Northstar.Api/Northstar.Api.http). Visual Studio can send them straight from the editor ([Use .http files in Visual Studio](https://learn.microsoft.com/en-us/aspnet/core/test/http-files?view=aspnetcore-10.0)).

### NuGet packages

Package versions are set once, in [Directory.Packages.props](Directory.Packages.props). The project files name a package without a version ([Central Package Management](https://learn.microsoft.com/en-us/nuget/consume-packages/central-package-management)).

| Package | Version | What it brings | Referenced by |
| --- | --- | --- | --- |
| [`coverlet.collector`](https://www.nuget.org/packages/coverlet.collector/6.0.4) | 6.0.4 | Collects code-coverage data while `dotnet test` runs. | [Api.IntegrationTests:11](tests/Northstar.Api.IntegrationTests/Northstar.Api.IntegrationTests.csproj#L11)<br>[Domain.UnitTests:11](tests/Northstar.Domain.UnitTests/Northstar.Domain.UnitTests.csproj#L11) |
| [`Microsoft.AspNetCore.Mvc.Testing`](https://www.nuget.org/packages/Microsoft.AspNetCore.Mvc.Testing/10.0.11) | 10.0.11 | `WebApplicationFactory<TEntryPoint>`: starts the API in memory so integration tests can call it over HTTP. | [Api.IntegrationTests:12](tests/Northstar.Api.IntegrationTests/Northstar.Api.IntegrationTests.csproj#L12) |
| [`Microsoft.AspNetCore.OpenApi`](https://www.nuget.org/packages/Microsoft.AspNetCore.OpenApi/10.0.11) | 10.0.11 | `AddOpenApi` and `MapOpenApi`: generates the OpenAPI description of the endpoints. | [Api:10](src/Northstar.Api/Northstar.Api.csproj#L10) |
| [`Microsoft.EntityFrameworkCore`](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore/10.0.15) | 10.0.15 | EF Core itself: `DbContext`, `DbSet<T>`, LINQ queries, change tracking. | [Application:8](src/Northstar.Application/Northstar.Application.csproj#L8) |
| [`Microsoft.EntityFrameworkCore.Design`](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Design/10.0.15) | 10.0.15 | Design-time support for the `dotnet ef` tools. Marked `PrivateAssets` = `all`, so it does not flow to projects that reference Infrastructure. | [Infrastructure:9](src/Northstar.Infrastructure/Northstar.Infrastructure.csproj#L9) |
| [`Microsoft.EntityFrameworkCore.Sqlite`](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Sqlite/10.0.15) | 10.0.15 | The SQLite database provider (`UseSqlite`). | [Infrastructure:13](src/Northstar.Infrastructure/Northstar.Infrastructure.csproj#L13)<br>[Api.IntegrationTests:13](tests/Northstar.Api.IntegrationTests/Northstar.Api.IntegrationTests.csproj#L13) |
| [`Microsoft.EntityFrameworkCore.SqlServer`](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.SqlServer/10.0.15) | 10.0.15 | The SQL Server database provider (`UseSqlServer`). | [Infrastructure:14](src/Northstar.Infrastructure/Northstar.Infrastructure.csproj#L14) |
| [`Microsoft.Extensions.Hosting.Abstractions`](https://www.nuget.org/packages/Microsoft.Extensions.Hosting.Abstractions/10.0.11) | 10.0.11 | `BackgroundService` and the other hosting abstractions. | [Infrastructure:15](src/Northstar.Infrastructure/Northstar.Infrastructure.csproj#L15) |
| [`Microsoft.Extensions.Http`](https://www.nuget.org/packages/Microsoft.Extensions.Http/10.0.11) | 10.0.11 | `IHttpClientFactory` and `AddHttpClient`. | [Infrastructure:16](src/Northstar.Infrastructure/Northstar.Infrastructure.csproj#L16) |
| [`Microsoft.NET.Test.Sdk`](https://www.nuget.org/packages/Microsoft.NET.Test.Sdk/17.14.1) | 17.14.1 | The test host that lets `dotnet test` and Visual Studio run a test project. | [Api.IntegrationTests:14](tests/Northstar.Api.IntegrationTests/Northstar.Api.IntegrationTests.csproj#L14)<br>[Domain.UnitTests:12](tests/Northstar.Domain.UnitTests/Northstar.Domain.UnitTests.csproj#L12) |
| [`Microsoft.OpenApi`](https://www.nuget.org/packages/Microsoft.OpenApi/2.12.2) | 2.12.2 | The OpenAPI document object model that `Microsoft.AspNetCore.OpenApi` builds on. | [Api:11](src/Northstar.Api/Northstar.Api.csproj#L11) |
| [`xunit`](https://www.nuget.org/packages/xunit/2.9.3) | 2.9.3 | The test framework: `[Fact]`, `[Theory]`, `Assert`. | [Api.IntegrationTests:15](tests/Northstar.Api.IntegrationTests/Northstar.Api.IntegrationTests.csproj#L15)<br>[Domain.UnitTests:13](tests/Northstar.Domain.UnitTests/Northstar.Domain.UnitTests.csproj#L13) |
| [`xunit.runner.visualstudio`](https://www.nuget.org/packages/xunit.runner.visualstudio/3.1.4) | 3.1.4 | The adapter that lets `dotnet test` and Test Explorer discover and run xUnit tests. | [Api.IntegrationTests:16](tests/Northstar.Api.IntegrationTests/Northstar.Api.IntegrationTests.csproj#L16)<br>[Domain.UnitTests:14](tests/Northstar.Domain.UnitTests/Northstar.Domain.UnitTests.csproj#L14) |

### npm packages

The web app's packages are listed in [Web/package.json](src/Northstar.Web/package.json), and the browser tests' packages in [e2e/package.json](tests/e2e/package.json).

| Package | Version range | Used for | Documentation |
|---|---|---|---|
| `@angular/core`, `@angular/common`, `@angular/compiler`, `@angular/forms`, `@angular/platform-browser`, `@angular/router` | `^22.1.0` | The Angular framework. | [What is Angular?](https://angular.dev/overview), [Angular API reference](https://angular.dev/api) |
| `rxjs` | `~7.8.0` | Observables and the operators used for polling. | [RxJS overview](https://rxjs.dev/guide/overview) |
| `tslib` | `^2.3.0` | Helper functions the TypeScript compiler calls instead of repeating them in every file (`importHelpers` in `tsconfig.json`). | [TSConfig reference](https://www.typescriptlang.org/tsconfig/) |
| `@angular/build`, `@angular/cli`, `@angular/compiler-cli` | `^22.1.4`, `^22.1.4`, `^22.1.0` | Build, development server and test runner integration (`ng build`, `ng serve`, `ng test`). | [The Angular CLI](https://angular.dev/tools/cli), [Angular application build system](https://angular.dev/tools/cli/build-system-migration) |
| `typescript` | `~6.0.2` | The TypeScript compiler. | [The TypeScript Handbook](https://www.typescriptlang.org/docs/handbook/intro.html) |
| `vitest` | `^4.0.8` | The unit test runner behind `ng test`. | [Vitest: getting started](https://vitest.dev/guide/) |
| `jsdom` | `^28.0.0` | A browser-like page for unit tests, which run in Node rather than in a browser. | [jsdom](https://github.com/jsdom/jsdom) |
| `prettier` | `^3.8.1` | Code formatter for TypeScript, HTML and SCSS. | [Prettier configuration file](https://prettier.io/docs/configuration) |
| `@playwright/test` | `1.62.1` | The browser test runner in `tests/e2e`. | [Playwright: installation and first test](https://playwright.dev/docs/intro) |

## 3. Patterns

A *pattern* is a way of solving a problem that the code uses again and again. Learning the patterns is the quickest way into this codebase, because most files are one of them applied to a new case.

The 26 patterns below are ordered from the outside in.

| Patterns | Topic |
|---|---|
| 1–4 | How the solution is divided and how the application starts. |
| 5–9 | The HTTP surface: controllers, results, tenants, repeated requests and delayed work. |
| 10–12 | Background work and calls to the outside payroll provider. |
| 13–16 | Data: the persistence interface, the EF Core model, queries and concurrent updates. |
| 17–19 | The domain model, and habits that run through every C# file. |
| 20–21 | The .NET tests and the fake provider. |
| 22–26 | The Angular app and its tests. |

Every pattern is written up in the same order.

- **What it is.** The idea in plain words, with its terms defined.
- **Where to find it.** The files to open.
- The code itself, copied from the repository.
- **How to read the code.** What each important line does. A quoted line carries its line number.
- **Why it is done this way.** The reason, taken from the repository's own documents where they give one and from the official documentation otherwise.
- **Look up.** The APIs involved, each linked to its reference page.
- **Read more.** Articles that explain the technique in depth.

### Patterns at a glance

| # | Pattern | Where to look first | How often it shows up |
|---:|---|---|---|
| 1 | [Layered modular monolith](#pattern-1-layered-modular-monolith) | [Application/Northstar.Application.csproj:4](src/Northstar.Application/Northstar.Application.csproj#L4) | Four projects under `src`, with references in one direction only. |
| 2 | [Composition root and service registration](#pattern-2-composition-root-and-service-registration) | [Api/Program.cs:6-21](src/Northstar.Api/Program.cs#L6-L21) | 15 `AddScoped` and `AddSingleton` calls in the solution. |
| 3 | [Constructor injection with primary constructors](#pattern-3-constructor-injection-with-primary-constructors) | [Application/ImportBatchService.cs:11-14](src/Northstar.Application/Imports/ImportBatchService.cs#L11-L14) | 18 classes take their dependencies this way. |
| 4 | [Request pipeline and host startup](#pattern-4-request-pipeline-and-host-startup) | [Api/Program.cs:23-46](src/Northstar.Api/Program.cs#L23-L46) | 2 hosts: the API and the fake provider. |
| 5 | [Thin attribute-routed controllers](#pattern-5-thin-attribute-routed-controllers) | [Api/ImportBatchesController.cs:26-42](src/Northstar.Api/Controllers/ImportBatchesController.cs#L26-L42) | 3 controllers, 11 actions. |
| 6 | [Result objects mapped to HTTP responses](#pattern-6-result-objects-mapped-to-http-responses) | [Api/ApiControllerBase.cs:15-32](src/Northstar.Api/Controllers/ApiControllerBase.cs#L15-L32) | `OperationResult<` is written 58 times in 6 files. |
| 7 | [Tenant scoping from header to query](#pattern-7-tenant-scoping-from-header-to-query) | [Api/HeaderUserContext.cs:5-9](src/Northstar.Api/Security/HeaderUserContext.cs#L5-L9) | 11 header checks in the controllers and 20 `OrganizationId ==` comparisons in 3 services. |
| 8 | [Idempotent commands with an Idempotency-Key](#pattern-8-idempotent-commands-with-an-idempotency-key) | [Application/ImportBatchService.cs:119-191](src/Northstar.Application/Imports/ImportBatchService.cs#L119-L191) | 3 actions read the header. |
| 9 | [Accept now, process later](#pattern-9-accept-now-process-later) | [Api/ImportBatchesController.cs:118-122](src/Northstar.Api/Controllers/ImportBatchesController.cs#L118-L122) | 2 actions answer `202`. |
| 10 | [Background worker with a queue and a recovery scan](#pattern-10-background-worker-with-a-queue-and-a-recovery-scan) | [Infrastructure/ImportBatchWorker.cs:16-55](src/Northstar.Infrastructure/Messaging/ImportBatchWorker.cs#L16-L55) | One background service, fed by one channel and one periodic timer. |
| 11 | [Typed HttpClient with validated options](#pattern-11-typed-httpclient-with-validated-options) | [Infrastructure/DependencyInjection.cs:39-58](src/Northstar.Infrastructure/DependencyInjection.cs#L39-L58) | One typed client with 4 validation rules. |
| 12 | [Bounded retries, timeouts and exception translation](#pattern-12-bounded-retries-timeouts-and-exception-translation) | [Infrastructure/FakePayrollProviderClient.cs:70-152](src/Northstar.Infrastructure/Integrations/FakePayrollProviderClient.cs#L70-L152) | One retry loop and three exception types. |
| 13 | [Persistence behind an interface](#pattern-13-persistence-behind-an-interface) | [Application/IApplicationDbContext.cs:18-37](src/Northstar.Application/Abstractions/IApplicationDbContext.cs#L18-L37) | 12 `IQueryable` properties, one per table. |
| 14 | [EF Core model configuration with the Fluent API](#pattern-14-ef-core-model-configuration-with-the-fluent-api) | [Infrastructure/ApplicationDbContext.cs:51-65](src/Northstar.Infrastructure/Persistence/ApplicationDbContext.cs#L51-L65) | 12 `Configure…` methods; `Property` is called 64 times. |
| 15 | [Read queries with no tracking, a tenant filter and projection](#pattern-15-read-queries-with-no-tracking-a-tenant-filter-and-projection) | [Application/ImportBatchService.cs:26-56](src/Northstar.Application/Imports/ImportBatchService.cs#L26-L56) | `AsNoTracking` is called 23 times in the solution. |
| 16 | [Optimistic concurrency with a version number](#pattern-16-optimistic-concurrency-with-a-version-number) | [Domain/ImportBatch.cs:35-60](src/Northstar.Domain/Imports/ImportBatch.cs#L35-L60) | 6 concurrency tokens and 7 `Version++` statements. |
| 17 | [Entities as state machines and pure domain services](#pattern-17-entities-as-state-machines-and-pure-domain-services) | [Domain/PayrollRun.cs:31-47](src/Northstar.Domain/PayrollRuns/PayrollRun.cs#L31-L47) | 17 properties with a private setter. |
| 18 | [Async all the way with cancellation](#pattern-18-async-all-the-way-with-cancellation) | [Infrastructure/ImportBatchWorker.cs:101-117](src/Northstar.Infrastructure/Messaging/ImportBatchWorker.cs#L101-L117) | 72 `async` methods, 204 `await` expressions and 42 `CancellationToken` parameters. |
| 19 | [Small conventions used everywhere](#pattern-19-small-conventions-used-everywhere) | [Application/ImportBatchView.cs:3-6](src/Northstar.Application/Imports/ImportBatchView.cs#L3-L6) | 66 of 68 classes and records are `sealed`; 22 types are records. |
| 20 | [Tests at three levels in .NET](#pattern-20-tests-at-three-levels-in-net) | [IntegrationTests/CustomWebApplicationFactory.cs:13-63](tests/Northstar.Api.IntegrationTests/CustomWebApplicationFactory.cs#L13-L63) | 41 `[Fact]` tests and 2 `[Theory]` tests with 7 data rows. |
| 21 | [Minimal API endpoints in the fake provider](#pattern-21-minimal-api-endpoints-in-the-fake-provider) | [FakeProvider/Program.cs:16-25](tools/Northstar.FakePayrollProvider/Program.cs#L16-L25) | 3 `MapGet` calls. |
| 22 | [Angular standalone bootstrap and an HTTP interceptor](#pattern-22-angular-standalone-bootstrap-and-an-http-interceptor) | [Web/app.config.ts:8-14](src/Northstar.Web/src/app/app.config.ts#L8-L14) | One bootstrap call and one interceptor. |
| 23 | [Angular services that return Observables](#pattern-23-angular-services-that-return-observables) | [Web/import-api.service.ts:6-22](src/Northstar.Web/src/app/imports/import-api.service.ts#L6-L22) | Two services; `http.get` on 4 lines and `http.post` on 5. |
| 24 | [Signals, computed values and control-flow templates](#pattern-24-signals-computed-values-and-control-flow-templates) | [Web/import-panel.component.ts:31-40](src/Northstar.Web/src/app/imports/import-panel.component.ts#L31-L40) | `signal(` on 10 lines and `computed(` on 3. |
| 25 | [Polling with RxJS and automatic cleanup](#pattern-25-polling-with-rxjs-and-automatic-cleanup) | [Web/import-panel.component.ts:150-167](src/Northstar.Web/src/app/imports/import-panel.component.ts#L150-L167) | One polling pipeline; `takeUntilDestroyed` on 5 lines. |
| 26 | [Frontend tests with Vitest and Playwright](#pattern-26-frontend-tests-with-vitest-and-playwright) | [Web/import-panel.component.spec.ts:19-61](src/Northstar.Web/src/app/imports/import-panel.component.spec.ts#L19-L61) | 3 Vitest tests and 2 Playwright tests. |

### Pattern 1: Layered modular monolith

**What it is.** One application, built and deployed as a whole, but cut into four projects called *layers*. A layer may only use the layers beneath it, and every reference points towards `Northstar.Domain`. The rule is not something a developer has to remember. It is the list of `<ProjectReference>` lines in each project file, so code that breaks the rule does not compile.

**Where to find it.** The four project files under `src`, and the "Dependency direction" list in [docs/architecture.md:14-20](docs/architecture.md#L14-L20).

**[`src/Northstar.Application/Northstar.Application.csproj`](src/Northstar.Application/Northstar.Application.csproj#L3-L9)** · lines 3–9

<!-- x:csproj-app -->
```xml
<ItemGroup>
  <ProjectReference Include="..\Northstar.Domain\Northstar.Domain.csproj" />
</ItemGroup>

<ItemGroup>
  <PackageReference Include="Microsoft.EntityFrameworkCore" />
</ItemGroup>
```

**[`src/Northstar.Infrastructure/Northstar.Infrastructure.csproj`](src/Northstar.Infrastructure/Northstar.Infrastructure.csproj#L3-L17)** · lines 3–17

<!-- x:csproj-infra -->
```xml
<ItemGroup>
  <ProjectReference Include="..\Northstar.Application\Northstar.Application.csproj" />
  <ProjectReference Include="..\Northstar.Domain\Northstar.Domain.csproj" />
</ItemGroup>

<ItemGroup>
  <PackageReference Include="Microsoft.EntityFrameworkCore.Design">
    <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
    <PrivateAssets>all</PrivateAssets>
  </PackageReference>
  <PackageReference Include="Microsoft.EntityFrameworkCore.Sqlite" />
  <PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" />
  <PackageReference Include="Microsoft.Extensions.Hosting.Abstractions" />
  <PackageReference Include="Microsoft.Extensions.Http" />
</ItemGroup>
```

**How to read the code.**

1. `<ProjectReference Include="..\Northstar.Domain\Northstar.Domain.csproj" />` (line 4) is the only project reference in Application. Application can use Domain types such as `ImportBatch`, but it cannot name `ApplicationDbContext` (Infrastructure) or a controller (Api), because the compiler is never shown those projects.
2. `<PackageReference Include="Microsoft.EntityFrameworkCore" />` (line 8) is Application's only NuGet package. No version is written because versions are set in one central file ([section 7](#7-tooling-and-configuration)). The package gives Application the asynchronous query methods such as [`ToListAsync`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.entityframeworkqueryableextensions.tolistasync?view=efcore-10.0#microsoft-entityframeworkcore-entityframeworkqueryableextensions-tolistasync-1(system-linq-iqueryable((-0))-system-threading-cancellationtoken)) and the exception types [`DbUpdateException`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.dbupdateexception?view=efcore-10.0) and [`DbUpdateConcurrencyException`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.dbupdateconcurrencyexception?view=efcore-10.0). Four Application files import its namespace.
3. In Infrastructure, lines 4 and 5 reference both Application and Domain. Infrastructure needs Application because it *implements* the interfaces that Application declares ([Pattern 13](#pattern-13-persistence-behind-an-interface)), and Domain because it maps the entities to database tables ([Pattern 14](#pattern-14-ef-core-model-configuration-with-the-fluent-api)).
4. Lines 13 to 16 are packages that Application and Domain do not have: the two database providers, `Microsoft.Extensions.Hosting.Abstractions` (the home of [`BackgroundService`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.hosting.backgroundservice?view=net-10.0-pp), [Pattern 10](#pattern-10-background-worker-with-a-queue-and-a-recovery-scan)) and `Microsoft.Extensions.Http` (the home of [`AddHttpClient`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.httpclientfactoryservicecollectionextensions.addhttpclient?view=net-10.0-pp#microsoft-extensions-dependencyinjection-httpclientfactoryservicecollectionextensions-addhttpclient-2(microsoft-extensions-dependencyinjection-iservicecollection-system-action((system-iserviceprovider-system-net-http-httpclient)))), [Pattern 11](#pattern-11-typed-httpclient-with-validated-options)).
5. `<PrivateAssets>all</PrivateAssets>` (line 11) belongs to the `Microsoft.EntityFrameworkCore.Design` package, which holds design-time tooling. `PrivateAssets` set to `all` keeps the package inside this project, so projects that reference Infrastructure do not receive it ([Controlling dependency assets](https://learn.microsoft.com/en-us/nuget/consume-packages/package-references-in-project-files#controlling-dependency-assets)).

The same rule shows up in four other places.

- [Domain/Northstar.Domain.csproj](src/Northstar.Domain/Northstar.Domain.csproj) has no `<ProjectReference>` and no `<PackageReference>` at all.
- The Domain source files contain one `using` directive between them ([Domain/PayrollCalculator.cs:1](src/Northstar.Domain/PayrollRuns/PayrollCalculator.cs#L1)), and it names another Domain namespace. Everything else Domain uses comes from the .NET base class library.
- In the Api project only `Program.cs` names an Infrastructure namespace ([Api/Program.cs:3-4](src/Northstar.Api/Program.cs#L3-L4)). The controllers use Application services.
- The unit-test project references Domain and nothing else ([UnitTests/Northstar.Domain.UnitTests.csproj:22](tests/Northstar.Domain.UnitTests/Northstar.Domain.UnitTests.csproj#L22)), so the domain rules are tested without a database or a web server.

**Why it is done this way.** The decision record gives the goal: "The lab must make one business operation easy to trace while exposing realistic database, authorization, and background-work seams." It also says when the shape may change: "Modules may be extracted only after a concrete scaling or ownership boundary appears." ([docs/adr/0001-use-modular-monolith.md:7-15](docs/adr/0001-use-modular-monolith.md#L7-L15)). Microsoft's architecture guide describes the same arrangement under the name *Clean Architecture*: the business rules sit in the centre, and the infrastructure depends on them instead of the other way round.

**Read more.** [Common web application architectures](https://learn.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures) · [Architectural principles](https://learn.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/architectural-principles) · [Dependency inversion principle](https://learn.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/architectural-principles#dependency-inversion)

### Pattern 2: Composition root and service registration

**What it is.** *Dependency injection* means that a class does not create the objects it needs. It lists them as constructor parameters, and something else supplies them. That something is the *service container*. The *composition root* is the one place where the application tells the container which types exist and how long each instance lives. Here it is `Program.cs` in the Api project, helped by one extension method per layer.

**Where to find it.** [Api/Program.cs:6-21](src/Northstar.Api/Program.cs#L6-L21), [Application/DependencyInjection.cs](src/Northstar.Application/DependencyInjection.cs) and [Infrastructure/DependencyInjection.cs](src/Northstar.Infrastructure/DependencyInjection.cs).

**[`src/Northstar.Api/Program.cs`](src/Northstar.Api/Program.cs#L6-L21)** · lines 6–21

<!-- x:program-services -->
```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddProblemDetails();
builder.Services.AddOpenApi();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<Northstar.Api.Security.HeaderUserContext>();
builder.Services.AddCors(options => options.AddPolicy("DevelopmentSpa", policy =>
    policy.WithOrigins("http://localhost:4200")
        .AllowAnyHeader()
        .AllowAnyMethod()));
```

**[`src/Northstar.Application/DependencyInjection.cs`](src/Northstar.Application/DependencyInjection.cs#L11-L22)** · lines 11–22

<!-- x:app-di -->
```csharp
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services) =>
        services
            .AddSingleton<IClock, SystemClock>()
            .AddSingleton<WageRateSelector>()
            .AddSingleton<PayrollCalculator>()
            .AddScoped<ImportBatchService>()
            .AddScoped<ImportBatchProcessor>()
            .AddScoped<PayrollRunService>()
            .AddScoped<WageRateService>();
}
```

**[`src/Northstar.Infrastructure/DependencyInjection.cs`](src/Northstar.Infrastructure/DependencyInjection.cs#L13-L38)** · lines 13–38 and 64–65

<!-- x:infra-di-db -->
```csharp
public static IServiceCollection AddInfrastructure(
    this IServiceCollection services,
    IConfiguration configuration)
{
    var provider = configuration["Database:Provider"] ?? "Sqlite";
    var connectionString = configuration.GetConnectionString("Northstar")
        ?? "Data Source=northstar.db";

    services.AddDbContext<ApplicationDbContext>(options =>
    {
        if (string.Equals(provider, "SqlServer", StringComparison.OrdinalIgnoreCase))
        {
            options.UseSqlServer(connectionString, sql =>
            {
                sql.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
            });
        }
        else
        {
            options.UseSqlite(connectionString, sqlite =>
                sqlite.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
        }
    });

    services.AddScoped<IApplicationDbContext>(provider =>
        provider.GetRequiredService<ApplicationDbContext>());
    // ... lines 39–63 omitted ...
    return services;
}
```

**How to read the code.** In `Program.cs`:

1. `var builder = WebApplication.CreateBuilder(args);` (line 6) creates the builder. It loads configuration (`appsettings.json`, environment variables, command-line arguments), sets up logging, and exposes `builder.Services`, the list of registrations.
2. `.AddControllers()` (line 9) registers what MVC controllers need. The call chained onto it adds a [`JsonStringEnumConverter`](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.serialization.jsonstringenumconverter?view=net-10.0), so enums travel as their names (`"Pending"`) and not as numbers.
3. `builder.Services.AddProblemDetails();` (line 12) and `builder.Services.AddOpenApi();` (line 13) switch on the standard error body ([Pattern 6](#pattern-6-result-objects-mapped-to-http-responses)) and the OpenAPI document ([Pattern 4](#pattern-4-request-pipeline-and-host-startup)).
4. `builder.Services.AddApplication();` (line 14) and `builder.Services.AddInfrastructure(builder.Configuration);` (line 15) are this codebase's own methods. Each layer registers its own types, so `Program.cs` does not need to know them.
5. `builder.Services.AddHttpContextAccessor();` (line 16) and the line after it register the two pieces that read the tenant from the request ([Pattern 7](#pattern-7-tenant-scoping-from-header-to-query)).
6. `builder.Services.AddCors(options => options.AddPolicy("DevelopmentSpa", policy =>` (line 18) defines a named CORS policy for the Angular development server at `http://localhost:4200`. Defining it does nothing yet. It is switched on in [Pattern 4](#pattern-4-request-pipeline-and-host-startup).

In `AddApplication`:

7. `public static IServiceCollection AddApplication(this IServiceCollection services) =>` (line 13) is an *extension method*: the `this` in front of the first parameter lets it be called as `builder.Services.AddApplication()`. It returns the collection, which is what allows the calls below it to be chained.
8. `.AddSingleton<IClock, SystemClock>()` (line 15) has two type arguments: whoever asks for `IClock` receives a `SystemClock`. `.AddSingleton<WageRateSelector>()` (line 16) has one, so the class is registered under its own type.
9. The method name sets the *lifetime*. `AddSingleton` means one instance for as long as the application runs. `AddScoped` means one instance per scope, and ASP.NET Core opens a scope for every HTTP request. The clock, the selector and the calculator are singletons. The four classes that receive the database context are scoped, starting with `.AddScoped<ImportBatchService>()` (line 18).

In `AddInfrastructure`:

10. `var provider = configuration["Database:Provider"] ?? "Sqlite";` (line 17) reads one configuration value. The colon steps into nested JSON (`"Database": { "Provider": … }` in [Api/appsettings.json:2-4](src/Northstar.Api/appsettings.json#L2-L4)), and `??` supplies a default when the key is missing.
11. `var connectionString = configuration.GetConnectionString("Northstar")` (line 18) is shorthand for reading `ConnectionStrings:Northstar`.
12. `services.AddDbContext<ApplicationDbContext>(options =>` (line 21) registers the EF Core context, which is scoped by default, and picks the database: `options.UseSqlServer(connectionString, sql =>` (line 25) or `options.UseSqlite(connectionString, sqlite =>` (line 32).
13. `services.AddScoped<IApplicationDbContext>(provider =>` (line 37) is a *factory registration*. Asking for the interface runs the lambda, which returns the `ApplicationDbContext` the current scope already has. Both names therefore lead to the same object within one request. The `provider` in this lambda is its own parameter (the service provider). It hides the string variable of the same name from line 17.

**Why it is done this way.** The architecture note states the role in one line: "`Northstar.Api` is the HTTP composition root." ([docs/architecture.md:19](docs/architecture.md#L19)). One `Add…` method per layer is the convention ASP.NET Core uses for its own features, and the documentation recommends it for application code as well. The lifetimes follow from the database context: it is scoped, and the documentation warns against a singleton holding on to a scoped service, so each class that receives the context is scoped too.

**Look up.** [`WebApplication.CreateBuilder`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.builder.webapplication.createbuilder?view=aspnetcore-10.0#microsoft-aspnetcore-builder-webapplication-createbuilder(system-string())) · [`WebApplicationBuilder.Services`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.builder.webapplicationbuilder.services?view=aspnetcore-10.0#microsoft-aspnetcore-builder-webapplicationbuilder-services) · [`MvcServiceCollectionExtensions.AddControllers`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.mvcservicecollectionextensions.addcontrollers?view=aspnetcore-10.0#microsoft-extensions-dependencyinjection-mvcservicecollectionextensions-addcontrollers(microsoft-extensions-dependencyinjection-iservicecollection)) · [`MvcCoreMvcBuilderExtensions.AddJsonOptions`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.mvccoremvcbuilderextensions.addjsonoptions?view=aspnetcore-10.0#microsoft-extensions-dependencyinjection-mvccoremvcbuilderextensions-addjsonoptions(microsoft-extensions-dependencyinjection-imvcbuilder-system-action((microsoft-aspnetcore-mvc-jsonoptions)))) · [`IServiceCollection`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.iservicecollection?view=net-10.0-pp) · [`ServiceCollectionServiceExtensions.AddSingleton`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.servicecollectionserviceextensions.addsingleton?view=net-10.0-pp#microsoft-extensions-dependencyinjection-servicecollectionserviceextensions-addsingleton-1(microsoft-extensions-dependencyinjection-iservicecollection)) · [`ServiceCollectionServiceExtensions.AddScoped`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.servicecollectionserviceextensions.addscoped?view=net-10.0-pp#microsoft-extensions-dependencyinjection-servicecollectionserviceextensions-addscoped-1(microsoft-extensions-dependencyinjection-iservicecollection)) · [`IConfiguration`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.configuration.iconfiguration?view=net-10.0-pp) · [`ConfigurationExtensions.GetConnectionString`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.configuration.configurationextensions.getconnectionstring?view=net-10.0-pp#microsoft-extensions-configuration-configurationextensions-getconnectionstring(microsoft-extensions-configuration-iconfiguration-system-string)) · [`EntityFrameworkServiceCollectionExtensions.AddDbContext`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.entityframeworkservicecollectionextensions.adddbcontext?view=efcore-10.0#microsoft-extensions-dependencyinjection-entityframeworkservicecollectionextensions-adddbcontext-1(microsoft-extensions-dependencyinjection-iservicecollection-system-action((microsoft-entityframeworkcore-dbcontextoptionsbuilder))-microsoft-extensions-dependencyinjection-servicelifetime-microsoft-extensions-dependencyinjection-servicelifetime)) · [`SqliteDbContextOptionsBuilderExtensions.UseSqlite`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.sqlitedbcontextoptionsbuilderextensions.usesqlite?view=efcore-10.0#microsoft-entityframeworkcore-sqlitedbcontextoptionsbuilderextensions-usesqlite(microsoft-entityframeworkcore-dbcontextoptionsbuilder-system-string-system-action((microsoft-entityframeworkcore-infrastructure-sqlitedbcontextoptionsbuilder)))) · [`SqlServerDbContextOptionsExtensions.UseSqlServer`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.sqlserverdbcontextoptionsextensions.usesqlserver?view=efcore-10.0#microsoft-entityframeworkcore-sqlserverdbcontextoptionsextensions-usesqlserver(microsoft-entityframeworkcore-dbcontextoptionsbuilder-system-string-system-action((microsoft-entityframeworkcore-infrastructure-sqlserverdbcontextoptionsbuilder)))) · [`ServiceProviderServiceExtensions.GetRequiredService`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.serviceproviderserviceextensions.getrequiredservice?view=net-10.0-pp#microsoft-extensions-dependencyinjection-serviceproviderserviceextensions-getrequiredservice-1(system-iserviceprovider))

**Read more.** [Dependency injection in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-10.0) · [Register groups of services with extension methods](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-10.0#register-groups-of-services-with-extension-methods) · [Service lifetimes in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-10.0#service-lifetimes) · [Configuration in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-10.0) · [DbContext lifetime, configuration and initialization](https://learn.microsoft.com/en-us/ef/core/dbcontext-configuration/)

### Pattern 3: Constructor injection with primary constructors

**What it is.** A class states what it needs by listing it in its constructor, and the container supplies the arguments. This codebase writes that list on the class line itself. A parameter list directly after the type name is a *primary constructor*, and its parameters can be used anywhere in the class body.

**Where to find it.** Every service, controller and worker. Three examples:

**[`src/Northstar.Application/Imports/ImportBatchService.cs`](src/Northstar.Application/Imports/ImportBatchService.cs#L11-L14)** · lines 11–14

<!-- x:pc-service -->
```csharp
public sealed class ImportBatchService(
    IApplicationDbContext dbContext,
    IImportBatchQueue queue,
    IClock clock)
```

**[`src/Northstar.Infrastructure/Messaging/ImportBatchWorker.cs`](src/Northstar.Infrastructure/Messaging/ImportBatchWorker.cs#L11-L14)** · lines 11–14

<!-- x:pc-worker -->
```csharp
public sealed class ImportBatchWorker(
    ImportBatchQueue queue,
    IServiceScopeFactory scopeFactory,
    ILogger<ImportBatchWorker> logger) : BackgroundService
```

**[`src/Northstar.Api/Controllers/ImportBatchesController.cs`](src/Northstar.Api/Controllers/ImportBatchesController.cs#L8-L11)** · lines 8–11

<!-- x:pc-controller -->
```csharp
[Route("api/import-batches")]
public sealed class ImportBatchesController(
    HeaderUserContext userContext,
    ImportBatchService importBatchService) : ApiControllerBase(userContext)
```

**How to read the code.**

1. `public sealed class ImportBatchService(` (line 11) opens the parameter list. The three parameters that follow are the class's dependencies, and methods further down simply use `dbContext`, `queue` and `clock`.
2. `IApplicationDbContext dbContext,` (line 12) asks for an interface, not a concrete class. Which class arrives was decided in [Pattern 2](#pattern-2-composition-root-and-service-registration).
3. `ILogger<ImportBatchWorker> logger) : BackgroundService` (line 14) shows where a base class goes: after the closing parenthesis.
4. `ImportBatchService importBatchService) : ApiControllerBase(userContext)` (line 11) passes one of the parameters on to the base class, which has a primary constructor of its own ([Api/ApiControllerBase.cs:8](src/Northstar.Api/Controllers/ApiControllerBase.cs#L8)).
5. A field is still declared where a value should be worked out once and kept. [Infrastructure/FakePayrollProviderClient.cs:13](src/Northstar.Infrastructure/Integrations/FakePayrollProviderClient.cs#L13) stores `options.Value` in a `readonly` field.

Nobody writes `new ImportBatchesController(…)`. ASP.NET Core creates the controller for each request and asks the container for the arguments. 18 classes in the solution have a primary constructor, the 22 records use the same syntax for their data, and there is exactly 1 constructor written the long way ([IntegrationTests/CustomWebApplicationFactory.cs:19](tests/Northstar.Api.IntegrationTests/CustomWebApplicationFactory.cs#L19)).

One language rule is worth knowing. A primary constructor parameter is a parameter, not a field. It is not `readonly`, and it does not become a property, except in a record.

**Why it is done this way.** It removes the field-plus-assignment lines that a hand-written constructor needs for every dependency. The C# documentation names dependency injection as a common use of primary constructors.

**Look up.** [`IServiceScopeFactory`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.iservicescopefactory?view=net-10.0-pp) · [`ILogger`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.logging.ilogger-1?view=net-10.0-pp) · [`BackgroundService`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.hosting.backgroundservice?view=net-10.0-pp) · [Primary constructor `class C(Dep dep)`](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/instance-constructors#primary-constructors) · [Positional record `record R(int A)`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/record#positional-syntax-for-property-and-field-definition)

**Read more.** [Tutorial: explore primary constructors](https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/tutorials/primary-constructors) · [Dependency injection in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-10.0)

### Pattern 4: Request pipeline and host startup

**What it is.** After the services are registered, `Program.cs` builds the application and describes what happens to each request. *Middleware* is a piece of code that every request passes through, in the order it was added. An *endpoint* is the code that finally answers one route. The same file also prepares the database before the application starts listening.

**Where to find it.** [Api/Program.cs:23-46](src/Northstar.Api/Program.cs#L23-L46). The fake provider has a shorter version of the same thing ([FakeProvider/Program.cs:5-14](tools/Northstar.FakePayrollProvider/Program.cs#L5-L14)).

**[`src/Northstar.Api/Program.cs`](src/Northstar.Api/Program.cs#L23-L46)** · lines 23–46

<!-- x:program-pipeline -->
```csharp
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseCors("DevelopmentSpa");
}

app.UseExceptionHandler();
app.UseAuthorization();

app.MapGet("/health", () => Results.Ok(new { status = "Healthy" }));
app.MapControllers();

if (app.Configuration.GetValue("SeedData", true))
{
    await using var scope = app.Services.CreateAsyncScope();
    await DatabaseInitializer.InitializeAsync(
        scope.ServiceProvider.GetRequiredService<ApplicationDbContext>());
}

app.Run();

public partial class Program;
```

**How to read the code.**

1. `var app = builder.Build();` (line 23) turns the registrations into a running container. From here on the code works with `app`.
2. `if (app.Environment.IsDevelopment())` (line 25) is true when the environment name is `Development`. Locally the name comes from `ASPNETCORE_ENVIRONMENT` in [Api/launchSettings.json](src/Northstar.Api/Properties/launchSettings.json).
3. `app.MapOpenApi();` (line 27) adds an endpoint that serves the generated OpenAPI document, by default at `/openapi/v1.json`.
4. `app.UseCors("DevelopmentSpa");` (line 28) switches on the policy that [Pattern 2](#pattern-2-composition-root-and-service-registration) defined, in development only.
5. `app.UseExceptionHandler();` (line 31) catches exceptions that nothing else handled. Because `AddProblemDetails()` was registered, the response is a problem-details body with status `500`.
6. `app.UseAuthorization();` (line 32) adds the middleware that enforces `[Authorize]` attributes. No controller or endpoint in this codebase carries one. Tenant and role are checked inside the controllers from request headers ([Pattern 7](#pattern-7-tenant-scoping-from-header-to-query)), which the README describes as a stand-in: "The first slice deliberately uses explicit headers instead of pretending to provide production authentication:"
7. `app.MapGet("/health", () => Results.Ok(new { status = "Healthy" }));` (line 34) is a minimal API endpoint. `new { status = "Healthy" }` is an anonymous type, serialized as `{"status":"Healthy"}`.
8. `app.MapControllers();` (line 35) adds one endpoint for every controller action ([Pattern 5](#pattern-5-thin-attribute-routed-controllers)).
9. `if (app.Configuration.GetValue("SeedData", true))` (line 37) reads a setting with a default of `true` ([Api/appsettings.json:8](src/Northstar.Api/appsettings.json#L8)).
10. `await using var scope = app.Services.CreateAsyncScope();` (line 39) opens a scope by hand. The database context is scoped, and at startup there is no HTTP request to supply a scope. `await using` disposes the scope, and the context with it, at the end of the block.
11. `await DatabaseInitializer.InitializeAsync(` (line 40) creates the database if it does not exist ([Infrastructure/DatabaseInitializer.cs:19](src/Northstar.Infrastructure/Persistence/DatabaseInitializer.cs#L19)) and inserts the demo data unless an organization is already there ([Infrastructure/DatabaseInitializer.cs:21](src/Northstar.Infrastructure/Persistence/DatabaseInitializer.cs#L21)).
12. `app.Run();` (line 44) starts the web server and blocks until the application shuts down.
13. `public partial class Program;` (line 46) is for the tests. A `Program.cs` made of top-level statements compiles to a class named `Program` that is `internal` by default. This line makes it `public`, so the test project can write `WebApplicationFactory<Program>` ([Pattern 20](#pattern-20-tests-at-three-levels-in-net)). A type declaration may end in `;` when its body is empty.

**Why it is done this way.** Middleware runs in the order it is added, so the order of these lines is part of the behaviour. The database setup follows the README: "The default development database is a zero-setup SQLite file. It is created and seeded automatically." The README also says that the lab "intentionally still uses `EnsureCreated` rather than migrations" ([README.md:54](README.md#L54)).

**Look up.** [`WebApplicationBuilder.Build`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.builder.webapplicationbuilder.build?view=aspnetcore-10.0#microsoft-aspnetcore-builder-webapplicationbuilder-build) · [`HostEnvironmentEnvExtensions.IsDevelopment`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.hosting.hostenvironmentenvextensions.isdevelopment?view=net-10.0-pp#microsoft-extensions-hosting-hostenvironmentenvextensions-isdevelopment(microsoft-extensions-hosting-ihostenvironment)) · [`OpenApiEndpointRouteBuilderExtensions.MapOpenApi`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.builder.openapiendpointroutebuilderextensions.mapopenapi?view=aspnetcore-10.0#microsoft-aspnetcore-builder-openapiendpointroutebuilderextensions-mapopenapi(microsoft-aspnetcore-routing-iendpointroutebuilder-system-string)) · [`CorsMiddlewareExtensions.UseCors`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.builder.corsmiddlewareextensions.usecors?view=aspnetcore-10.0#microsoft-aspnetcore-builder-corsmiddlewareextensions-usecors(microsoft-aspnetcore-builder-iapplicationbuilder-system-string)) · [`ExceptionHandlerExtensions.UseExceptionHandler`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.builder.exceptionhandlerextensions.useexceptionhandler?view=aspnetcore-10.0#microsoft-aspnetcore-builder-exceptionhandlerextensions-useexceptionhandler(microsoft-aspnetcore-builder-iapplicationbuilder)) · [`AuthorizationAppBuilderExtensions.UseAuthorization`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.builder.authorizationappbuilderextensions.useauthorization?view=aspnetcore-10.0#microsoft-aspnetcore-builder-authorizationappbuilderextensions-useauthorization(microsoft-aspnetcore-builder-iapplicationbuilder)) · [`EndpointRouteBuilderExtensions.MapGet`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.builder.endpointroutebuilderextensions.mapget?view=aspnetcore-10.0#microsoft-aspnetcore-builder-endpointroutebuilderextensions-mapget(microsoft-aspnetcore-routing-iendpointroutebuilder-system-string-system-delegate)) · [`ControllerEndpointRouteBuilderExtensions.MapControllers`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.builder.controllerendpointroutebuilderextensions.mapcontrollers?view=aspnetcore-10.0#microsoft-aspnetcore-builder-controllerendpointroutebuilderextensions-mapcontrollers(microsoft-aspnetcore-routing-iendpointroutebuilder)) · [`ConfigurationBinder.GetValue`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.configuration.configurationbinder.getvalue?view=net-10.0-pp#microsoft-extensions-configuration-configurationbinder-getvalue-1(microsoft-extensions-configuration-iconfiguration-system-string-0)) · [`ServiceProviderServiceExtensions.CreateAsyncScope`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.serviceproviderserviceextensions.createasyncscope?view=net-10.0-pp#microsoft-extensions-dependencyinjection-serviceproviderserviceextensions-createasyncscope(system-iserviceprovider)) · [`DatabaseFacade.EnsureCreatedAsync`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.infrastructure.databasefacade.ensurecreatedasync?view=efcore-10.0#microsoft-entityframeworkcore-infrastructure-databasefacade-ensurecreatedasync(system-threading-cancellationtoken)) · [`WebApplication.Run`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.builder.webapplication.run?view=aspnetcore-10.0#microsoft-aspnetcore-builder-webapplication-run(system-string))

**Read more.** [ASP.NET Core middleware](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/middleware/?view=aspnetcore-10.0) · [App startup in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/startup?view=aspnetcore-10.0) · [Use multiple environments in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/environments?view=aspnetcore-10.0) · [Generate OpenAPI documents](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/openapi/aspnetcore-openapi?view=aspnetcore-10.0) · [Enable Cross-Origin Requests (CORS)](https://learn.microsoft.com/en-us/aspnet/core/security/cors?view=aspnetcore-10.0) · [Handle errors in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/error-handling?view=aspnetcore-10.0) · [Create and drop APIs (EnsureCreated)](https://learn.microsoft.com/en-us/ef/core/managing-schemas/ensure-created) · [Integration tests in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-10.0)

### Pattern 5: Thin attribute-routed controllers

**What it is.** A *controller* is a class whose public methods, called *actions*, answer HTTP requests. *Attribute routing* means the URL and the HTTP method are written in attributes above the action. *Thin* means the action holds no business rules. It reads the request, makes one call into the Application layer and turns the answer into an HTTP response.

**Where to find it.** The three controllers in `src/Northstar.Api/Controllers` and their base class [Api/ApiControllerBase.cs](src/Northstar.Api/Controllers/ApiControllerBase.cs).

**[`src/Northstar.Api/Controllers/ImportBatchesController.cs`](src/Northstar.Api/Controllers/ImportBatchesController.cs#L26-L42)** · lines 26–42

<!-- x:ctrl-get -->
```csharp
[HttpGet("{batchId:guid}")]
[ProducesResponseType<ImportBatchView>(StatusCodes.Status200OK)]
[ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
public async Task<ActionResult<ImportBatchView>> Get(
    Guid batchId,
    CancellationToken cancellationToken)
{
    if (!TryGetOrganizationId(out var organizationId))
    {
        return MissingTenantHeader();
    }

    return ToActionResult(await importBatchService.GetAsync(
        organizationId,
        batchId,
        cancellationToken));
}
```

**How to read the code.**

1. The class carries `[Route("api/import-batches")]` (shown in [Pattern 3](#pattern-3-constructor-injection-with-primary-constructors)), and its base class carries `[ApiController]` ([Api/ApiControllerBase.cs:7](src/Northstar.Api/Controllers/ApiControllerBase.cs#L7)). The action's own route is added to the class route.
2. `[HttpGet("{batchId:guid}")]` (line 26) makes this action answer `GET api/import-batches/{batchId}`. The `:guid` part is a *route constraint*: the route only matches when that segment parses as a GUID.
3. `[ProducesResponseType<ImportBatchView>(StatusCodes.Status200OK)]` (line 27) documents one possible response for the OpenAPI document. It does not change what the action returns.
4. `public async Task<ActionResult<ImportBatchView>> Get(` (line 29) declares the return type. `ActionResult<T>` lets an action return either a `T` or any other result such as a `404`. The conversion is implicit, and the compiler uses it 25 times in the three controllers and their base class.
5. The parameter `Guid batchId` is filled from the route, because the names match. `CancellationToken cancellationToken)` (line 31) is supplied by ASP.NET Core and is cancelled when the client gives up on the request ([Pattern 18](#pattern-18-async-all-the-way-with-cancellation)).
6. `if (!TryGetOrganizationId(out var organizationId))` (line 33) reads the tenant from a header, and `return MissingTenantHeader();` (line 35) answers `401` when it is missing ([Pattern 7](#pattern-7-tenant-scoping-from-header-to-query)).
7. `return ToActionResult(await importBatchService.GetAsync(` (line 38) is the whole of the work: one service call, and one conversion from the service's result to an HTTP response ([Pattern 6](#pattern-6-result-objects-mapped-to-http-responses)).

All 11 actions have this shape: check the tenant, check the role (in four of them), make one service call, convert the result.

`[ApiController]` changes how a controller behaves. The documentation lists what it adds: attribute routing becomes a requirement, invalid input is answered with `400` automatically, the source of each parameter is inferred (a complex type such as `CreateImportBatchRequest` is read from the JSON body, a simple type from the route or the query string), and error status codes get a problem-details body.

**Why it is done this way.** The architecture note opens with it: "The payroll request path remains deliberately short enough to trace:" ([docs/architecture.md:3-12](docs/architecture.md#L3-L12)). The rules live in the Domain and Application layers, and the domain rules have tests of their own that involve no HTTP at all ([Pattern 20](#pattern-20-tests-at-three-levels-in-net)).

**Look up.** [`ApiControllerAttribute`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.apicontrollerattribute?view=aspnetcore-10.0) · [`RouteAttribute`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.routeattribute?view=aspnetcore-10.0) · [`HttpGetAttribute`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.httpgetattribute?view=aspnetcore-10.0) · [`HttpPostAttribute`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.httppostattribute?view=aspnetcore-10.0) · [`ProducesResponseTypeAttribute`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.producesresponsetypeattribute-1?view=aspnetcore-10.0) · [`ControllerBase`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.controllerbase?view=aspnetcore-10.0) · [`ActionResult`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.actionresult-1?view=aspnetcore-10.0) · [`HttpContext.RequestAborted`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.httpcontext.requestaborted?view=aspnetcore-10.0#microsoft-aspnetcore-http-httpcontext-requestaborted)

**Read more.** [Create web APIs with ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/web-api/?view=aspnetcore-10.0) · [The ApiController attribute](https://learn.microsoft.com/en-us/aspnet/core/web-api/?view=aspnetcore-10.0#apicontroller-attribute) · [Binding source parameter inference](https://learn.microsoft.com/en-us/aspnet/core/web-api/?view=aspnetcore-10.0#binding-source-parameter-inference) · [Routing to controller actions](https://learn.microsoft.com/en-us/aspnet/core/mvc/controllers/routing?view=aspnetcore-10.0) · [Route constraints](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/routing?view=aspnetcore-10.0#route-constraints) · [Controller action return types](https://learn.microsoft.com/en-us/aspnet/core/web-api/action-return-types?view=aspnetcore-10.0)

### Pattern 6: Result objects mapped to HTTP responses

**What it is.** An Application service does not throw an exception when a request cannot be carried out for an ordinary reason, and it knows nothing about HTTP. It returns an object that says what happened. One method in the controllers' base class turns that object into a status code and a body.

**Where to find it.** [Application/OperationResult.cs](src/Northstar.Application/Common/OperationResult.cs) and [Api/ApiControllerBase.cs:15-49](src/Northstar.Api/Controllers/ApiControllerBase.cs#L15-L49).

**[`src/Northstar.Application/Common/OperationResult.cs`](src/Northstar.Application/Common/OperationResult.cs#L3-L29)** · lines 3–29

<!-- x:result-type -->
```csharp
public enum OperationOutcome
{
    Success,
    NotFound,
    Conflict,
    Invalid,
    Forbidden
}

public sealed record OperationResult<T>(
    OperationOutcome Outcome,
    T? Value = default,
    string? ErrorCode = null,
    string? ErrorMessage = null)
{
    public static OperationResult<T> Success(T value) => new(OperationOutcome.Success, value);

    public static OperationResult<T> NotFound() => new(OperationOutcome.NotFound);

    public static OperationResult<T> Conflict(string code, string message) =>
        new(OperationOutcome.Conflict, default, code, message);

    public static OperationResult<T> Invalid(string code, string message) =>
        new(OperationOutcome.Invalid, default, code, message);

    public static OperationResult<T> Forbidden() => new(OperationOutcome.Forbidden);
}
```

**[`src/Northstar.Api/Controllers/ApiControllerBase.cs`](src/Northstar.Api/Controllers/ApiControllerBase.cs#L15-L49)** · lines 15–49

<!-- x:result-map -->
```csharp
protected ActionResult<T> ToActionResult<T>(OperationResult<T> result) =>
    result.Outcome switch
    {
        OperationOutcome.Success => Ok(result.Value),
        OperationOutcome.NotFound => NotFound(CreateProblem(
            StatusCodes.Status404NotFound,
            "Resource not found")),
        OperationOutcome.Conflict => Conflict(CreateProblem(
            StatusCodes.Status409Conflict,
            result.ErrorCode ?? "Conflict",
            result.ErrorMessage)),
        OperationOutcome.Invalid => UnprocessableEntity(CreateProblem(
            StatusCodes.Status422UnprocessableEntity,
            result.ErrorCode ?? "Invalid request",
            result.ErrorMessage)),
        OperationOutcome.Forbidden => StatusCode(StatusCodes.Status403Forbidden),
        _ => throw new InvalidOperationException($"Unsupported outcome {result.Outcome}.")
    };

protected ActionResult MissingTenantHeader() => Unauthorized(CreateProblem(
    StatusCodes.Status401Unauthorized,
    "Organization context required",
    "Provide a valid X-Organization-Id header for the local study authentication scheme."));

protected ActionResult ForbiddenForLocalStudyUser() =>
    StatusCode(StatusCodes.Status403Forbidden);

private ProblemDetails CreateProblem(int status, string title, string? detail = null) =>
    new()
    {
        Status = status,
        Title = title,
        Detail = detail,
        Instance = HttpContext.Request.Path
    };
```

**How to read the code.**

1. `OperationOutcome` (lines 3 to 10) lists the five things that can happen.
2. `public sealed record OperationResult<T>(` (line 12) is a generic record: an outcome, an optional value of type `T`, and an optional error code and message. `T? Value = default,` (line 14) gives the value a default, so a failure needs no value.
3. `public static OperationResult<T> Success(T value) => new(OperationOutcome.Success, value);` (line 18) is one of five small factory methods. They make the calling code read like a sentence: `OperationResult<ImportBatchView>.NotFound()`. `new(…)` without a type name is a target-typed `new`: the return type already says what to create.
4. In the base class, `result.Outcome switch` (line 16) is a `switch` expression. Each arm has the form `pattern => result`.
5. `OperationOutcome.Success => Ok(result.Value),` (line 18) answers `200` with the value as JSON.
6. `OperationOutcome.NotFound => NotFound(CreateProblem(` (line 19) answers `404` with a problem-details body. `Conflict` and `Invalid` do the same with `409` and `422`, and `result.ErrorCode ?? "Conflict",` (line 24) falls back to a general title when the service gave no code.
7. `OperationOutcome.Forbidden => StatusCode(StatusCodes.Status403Forbidden),` (line 30) answers `403`. The code supplies no body here. `[ApiController]` fills one in: the documentation says that MVC turns an error result (status 400 or higher) into a result with a default problem-details body.
8. `_ => throw new InvalidOperationException($"Unsupported outcome {result.Outcome}.")` (line 31) is the discard arm. It catches any value the arms above did not name.
9. `private ProblemDetails CreateProblem(int status, string title, string? detail = null) =>` (line 42) builds the error body. The service's error code becomes `Title`, its message becomes `Detail`, and `Instance = HttpContext.Request.Path` (line 48) records which URL was asked for.

| Outcome | Status | Body | Error codes that use it, for example |
|---|---|---|---|
| `Success` | `200` | The value. Two controllers choose `201` or `202` themselves before this method is reached ([Api/WageRatesController.cs:48](src/Northstar.Api/Controllers/WageRatesController.cs#L48), [Pattern 9](#pattern-9-accept-now-process-later)). | – |
| `NotFound` | `404` | Problem details, title "Resource not found". | – |
| `Conflict` | `409` | Problem details. | `VERSION_CONFLICT`, `IDEMPOTENCY_KEY_REUSED`, `IMPORT_NOT_RETRYABLE` |
| `Invalid` | `422` | Problem details. | `IDEMPOTENCY_KEY_REQUIRED`, `INVALID_IMPORT_PERIOD` |
| `Forbidden` | `403` | The default problem details that `[ApiController]` adds. | – |

The Domain layer uses the same idea with its own small types: `PayrollRunActionResult` ([Domain/PayrollRun.cs:10-15](src/Northstar.Domain/PayrollRuns/PayrollRun.cs#L10-L15)), `RateSelectionResult` ([Domain/RateSelectionResult.cs:10-23](src/Northstar.Domain/WageRates/RateSelectionResult.cs#L10-L23)), and plain `bool` results from `BeginProcessing` and `Retry` ([Pattern 17](#pattern-17-entities-as-state-machines-and-pure-domain-services)).

Exceptions are still used, for a different kind of problem. A method called in a state that the calling code should never have allowed throws ([Domain/ImportBatch.cs:95-98](src/Northstar.Domain/Imports/ImportBatch.cs#L95-L98)), and so does a failed call to the provider ([Pattern 12](#pattern-12-bounded-retries-timeouts-and-exception-translation)).

**Why it is done this way.** The README lists "explicit results" among the things the Application layer owns ([README.md:112](README.md#L112)). An expected outcome such as "not found" is part of the method's normal answer, and the caller cannot forget to deal with it, because it is in the return type. The .NET guidance draws the same line: conditions that are likely to occur are handled without exceptions.

**Look up.** [`ControllerBase.Ok`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.controllerbase.ok?view=aspnetcore-10.0#microsoft-aspnetcore-mvc-controllerbase-ok(system-object)) · [`ControllerBase.NotFound`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.controllerbase.notfound?view=aspnetcore-10.0#microsoft-aspnetcore-mvc-controllerbase-notfound(system-object)) · [`ControllerBase.Conflict`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.controllerbase.conflict?view=aspnetcore-10.0#microsoft-aspnetcore-mvc-controllerbase-conflict(system-object)) · [`ControllerBase.UnprocessableEntity`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.controllerbase.unprocessableentity?view=aspnetcore-10.0#microsoft-aspnetcore-mvc-controllerbase-unprocessableentity(system-object)) · [`ControllerBase.StatusCode`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.controllerbase.statuscode?view=aspnetcore-10.0#microsoft-aspnetcore-mvc-controllerbase-statuscode(system-int32)) · [`ProblemDetails`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.problemdetails?view=aspnetcore-10.0) · [`StatusCodes`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.statuscodes?view=aspnetcore-10.0) · [`switch` expression](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/switch-expression) · [Target-typed `new()`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/new-operator#target-typed-new) · [`record`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/record)

**Read more.** [Problem details](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/error-handling?view=aspnetcore-10.0#problem-details) · [Handle errors in ASP.NET Core APIs](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/error-handling-api?view=aspnetcore-10.0) · [RFC 9457: Problem Details for HTTP APIs](https://www.rfc-editor.org/rfc/rfc9457.html) · [Best practices for exceptions](https://learn.microsoft.com/en-us/dotnet/standard/exceptions/best-practices-for-exceptions) · [409 Conflict (MDN)](https://developer.mozilla.org/en-US/docs/Web/HTTP/Reference/Status/409) · [422 Unprocessable Content (MDN)](https://developer.mozilla.org/en-US/docs/Web/HTTP/Reference/Status/422) · [HTTP response status codes (MDN)](https://developer.mozilla.org/en-US/docs/Web/HTTP/Reference/Status)

### Pattern 7: Tenant scoping from header to query

**What it is.** A *tenant* is one customer of a system that serves several. Here the tenant is an organization. All organizations share the same tables, and each row carries an `OrganizationId`. The organization of the caller is read from a request header, handed to the service as its first argument, and written into the `WHERE` clause of the query.

**Where to find it.** [Api/HeaderUserContext.cs](src/Northstar.Api/Security/HeaderUserContext.cs), the first lines of every controller action, and every public method of the three services.

**[`src/Northstar.Api/Security/HeaderUserContext.cs`](src/Northstar.Api/Security/HeaderUserContext.cs#L3-L15)** · lines 3–15

<!-- x:tenant-header -->
```csharp
public sealed class HeaderUserContext(IHttpContextAccessor httpContextAccessor)
{
    public bool TryGetOrganizationId(out Guid organizationId)
    {
        var value = httpContextAccessor.HttpContext?.Request.Headers["X-Organization-Id"].ToString();
        return Guid.TryParse(value, out organizationId);
    }

    public bool IsPayrollManager => string.Equals(
        httpContextAccessor.HttpContext?.Request.Headers["X-User-Role"].ToString(),
        "PayrollManager",
        StringComparison.OrdinalIgnoreCase);
}
```

**[`src/Northstar.Application/Imports/ImportBatchService.cs`](src/Northstar.Application/Imports/ImportBatchService.cs#L58-L72)** · lines 58–72

<!-- x:tenant-query -->
```csharp
public async Task<OperationResult<ImportBatchView>> GetAsync(
    Guid organizationId,
    Guid importBatchId,
    CancellationToken cancellationToken)
{
    var batch = await dbContext.ImportBatches
        .AsNoTracking()
        .SingleOrDefaultAsync(
            value => value.OrganizationId == organizationId && value.Id == importBatchId,
            cancellationToken);

    return batch is null
        ? OperationResult<ImportBatchView>.NotFound()
        : OperationResult<ImportBatchView>.Success(batch.ToView());
}
```

**How to read the code.**

1. `public sealed class HeaderUserContext(IHttpContextAccessor httpContextAccessor)` (line 3) receives an accessor, which gives a class that is not a controller a way to reach the current request.
2. `var value = httpContextAccessor.HttpContext?.Request.Headers["X-Organization-Id"].ToString();` (line 7) reads the header. The `?.` yields `null` when there is no current request. A header that is absent yields an empty string.
3. `return Guid.TryParse(value, out organizationId);` (line 8) is the *Try pattern*: the method returns `true` or `false`, and hands back the parsed value through the `out` parameter. Nothing is thrown for bad input.
4. `public bool IsPayrollManager => string.Equals(` (line 11) compares the `X-User-Role` header with `"PayrollManager"`. `StringComparison.OrdinalIgnoreCase);` (line 14) makes the comparison ignore upper and lower case and ignore the machine's language settings.
5. In the service, `Guid organizationId,` (line 59) is the first parameter. That holds for all 11 public methods of the three services, so none of them can be called without saying whose data is wanted.
6. `value => value.OrganizationId == organizationId && value.Id == importBatchId,` (line 66) puts both conditions into the SQL `WHERE` clause. A batch that belongs to another organization is not found, and `? OperationResult<ImportBatchView>.NotFound()` (line 70) gives the same `404` as an id that does not exist.

The pieces around it:

- Each of the 11 actions starts with `TryGetOrganizationId` and answers `401` when it fails ([Api/ApiControllerBase.cs:34-37](src/Northstar.Api/Controllers/ApiControllerBase.cs#L34-L37)). Four actions also check `IsPayrollManager` and answer `403`: creating a wage rate, approving a payroll run, and creating or retrying an import.
- The comparison `OrganizationId == organizationId` is written 20 times in the 3 service files. EF Core can also add such a condition to every query of an entity automatically, through a *global query filter*. This codebase writes the condition out by hand in the service queries.
- The database repeats the rule. An import row points at its batch with a two-column foreign key that includes the organization ([Infrastructure/ApplicationDbContext.cs:226-231](src/Northstar.Infrastructure/Persistence/ApplicationDbContext.cs#L226-L231)), and six of the seven unique indexes start with `OrganizationId` ([Pattern 14](#pattern-14-ef-core-model-configuration-with-the-fluent-api)). The seventh is on the organization table itself.
- The Angular app adds both headers to every request in an interceptor ([Pattern 22](#pattern-22-angular-standalone-bootstrap-and-an-http-interceptor)), and the API tests create a client per organization ([Pattern 20](#pattern-20-tests-at-three-levels-in-net)).

**Why it is done this way.** The README is direct about the headers: "The first slice deliberately uses explicit headers instead of pretending to provide production authentication:" and "Replace this seam with JWT/Entra claims in the security phase." ([README.md:98-107](README.md#L98-L107)). The `404` is a decision recorded in the architecture note: "Tenant ownership is checked before a resource is returned, producing `404` for cross-tenant IDs."

**Look up.** [`IHttpContextAccessor`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.ihttpcontextaccessor?view=aspnetcore-10.0) · [`IHttpContextAccessor.HttpContext`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.ihttpcontextaccessor.httpcontext?view=aspnetcore-10.0#microsoft-aspnetcore-http-ihttpcontextaccessor-httpcontext) · [`HttpRequest.Headers`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.httprequest.headers?view=aspnetcore-10.0#microsoft-aspnetcore-http-httprequest-headers) · [`IHeaderDictionary`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.iheaderdictionary?view=aspnetcore-10.0) · [`Guid.TryParse`](https://learn.microsoft.com/en-us/dotnet/api/system.guid.tryparse?view=net-10.0#system-guid-tryparse(system-string-system-guid@)) · [`string.Equals`](https://learn.microsoft.com/en-us/dotnet/api/system.string.equals?view=net-10.0#system-string-equals(system-string-system-string-system-stringcomparison)) · [`StringComparison.OrdinalIgnoreCase`](https://learn.microsoft.com/en-us/dotnet/api/system.stringcomparison?view=net-10.0) · [`EntityFrameworkQueryableExtensions.SingleOrDefaultAsync`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.entityframeworkqueryableextensions.singleordefaultasync?view=efcore-10.0#microsoft-entityframeworkcore-entityframeworkqueryableextensions-singleordefaultasync-1(system-linq-iqueryable((-0))-system-linq-expressions-expression((system-func((-0-system-boolean))))-system-threading-cancellationtoken)) · [`out var` declaration](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/method-parameters#out-parameter-modifier) · [Null-conditional `?.`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/member-access-operators#null-conditional-operators--and-)

**Read more.** [Access HttpContext in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/http-context?view=aspnetcore-10.0) · [Multi-tenancy](https://learn.microsoft.com/en-us/ef/core/miscellaneous/multitenancy) · [Global query filters](https://learn.microsoft.com/en-us/ef/core/querying/filters) · [Architect multitenant solutions](https://learn.microsoft.com/en-us/azure/architecture/guide/multitenant/overview) · [Best practices for comparing strings in .NET](https://learn.microsoft.com/en-us/dotnet/standard/base-types/best-practices-strings)

### Pattern 8: Idempotent commands with an Idempotency-Key

**What it is.** An operation is *idempotent* when doing it twice has the same effect as doing it once. `GET` is idempotent by nature. A `POST` that creates something is not: a client that sent the request and lost the answer cannot know whether the work was done, and sending it again could create a second copy. The fix used here is a key. The client invents a unique value, sends it in an `Idempotency-Key` header, and repeats the same value when it retries. The server stores the key next to what it created, and answers a repeat with the original result.

**Where to find it.** The three actions that start work: creating an import, retrying an import and calculating a payroll run. The fullest version is `ImportBatchService.CreateAsync`.

**[`src/Northstar.Api/Controllers/ImportBatchesController.cs`](src/Northstar.Api/Controllers/ImportBatchesController.cs#L62-L88)** · lines 62–88

<!-- x:ctrl-create -->
```csharp
[HttpPost]
[ProducesResponseType<ImportBatchView>(StatusCodes.Status202Accepted)]
[ProducesResponseType(StatusCodes.Status403Forbidden)]
[ProducesResponseType<ProblemDetails>(StatusCodes.Status409Conflict)]
[ProducesResponseType<ProblemDetails>(StatusCodes.Status422UnprocessableEntity)]
public async Task<ActionResult<ImportBatchView>> Create(
    CreateImportBatchRequest request,
    [FromHeader(Name = "Idempotency-Key")] string? idempotencyKey,
    CancellationToken cancellationToken)
{
    if (!TryGetOrganizationId(out var organizationId))
    {
        return MissingTenantHeader();
    }

    if (!IsPayrollManager)
    {
        return ForbiddenForLocalStudyUser();
    }

    var result = await importBatchService.CreateAsync(
        organizationId,
        request,
        idempotencyKey ?? string.Empty,
        cancellationToken);
    return ToAcceptedResult(result);
}
```

**[`src/Northstar.Application/Imports/ImportBatchService.cs`](src/Northstar.Application/Imports/ImportBatchService.cs#L119-L136)** · lines 119–136 and 156–191

<!-- x:idem-create -->
```csharp
var scenario = request.ProviderScenario.Trim().ToLowerInvariant();
var operation = "CreateImportBatch";
var requestHash = Hash(
    $"{organizationId:N}:{request.PeriodStart:yyyy-MM-dd}:{request.PeriodEnd:yyyy-MM-dd}:{scenario}");

ImportBatch batch;
await using (var transaction = await dbContext.BeginTransactionAsync(cancellationToken))
{
    var priorResult = await ResolvePriorRequestAsync(
        organizationId,
        operation,
        idempotencyKey,
        requestHash,
        cancellationToken);
    if (priorResult is not null)
    {
        return priorResult;
    }
    // ... lines 137–155 omitted ...
    dbContext.Add(batch);
    dbContext.Add(CreateIdempotencyRecord(
        organizationId,
        operation,
        idempotencyKey,
        requestHash,
        batch.Id));

    try
    {
        await dbContext.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);
    }
    catch (DbUpdateException)
    {
        await transaction.RollbackAsync(CancellationToken.None);
        dbContext.ClearTracking();
        var concurrentResult = await ResolvePriorRequestAsync(
            organizationId,
            operation,
            idempotencyKey,
            requestHash,
            cancellationToken);
        if (concurrentResult is not null)
        {
            return concurrentResult;
        }

        throw;
    }
}

// SQL is the durable source of pending work. The recurring worker scan closes
// the process-crash gap if this in-memory notification is ever lost.
await queue.QueueAsync(batch.Id, CancellationToken.None);
return OperationResult<ImportBatchView>.Success(batch.ToView());
```

**[`src/Northstar.Application/Imports/ImportBatchService.cs`](src/Northstar.Application/Imports/ImportBatchService.cs#L311-L346)** · lines 311–346

<!-- x:idem-resolve -->
```csharp
private async Task<OperationResult<ImportBatchView>?> ResolvePriorRequestAsync(
    Guid organizationId,
    string operation,
    string idempotencyKey,
    string requestHash,
    CancellationToken cancellationToken)
{
    var priorRequest = await dbContext.IdempotencyRecords
        .AsNoTracking()
        .SingleOrDefaultAsync(
            record => record.OrganizationId == organizationId &&
                record.OperationName == operation &&
                record.Key == idempotencyKey,
            cancellationToken);
    if (priorRequest is null)
    {
        return null;
    }

    if (!string.Equals(priorRequest.RequestHash, requestHash, StringComparison.Ordinal))
    {
        return OperationResult<ImportBatchView>.Conflict(
            "IDEMPOTENCY_KEY_REUSED",
            "This idempotency key was already used with a different import request.");
    }

    var priorBatch = await dbContext.ImportBatches
        .AsNoTracking()
        .SingleOrDefaultAsync(
            batch => batch.OrganizationId == organizationId &&
                batch.Id == priorRequest.ResultResourceId,
            cancellationToken);
    return priorBatch is null
        ? OperationResult<ImportBatchView>.NotFound()
        : OperationResult<ImportBatchView>.Success(priorBatch.ToView());
}
```

**[`src/Northstar.Application/Imports/ImportBatchService.cs`](src/Northstar.Application/Imports/ImportBatchService.cs#L381-L382)** · lines 381–382

<!-- x:idem-hash -->
```csharp
private static string Hash(string value) =>
    Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(value)));
```

**[`src/Northstar.Application/Idempotency/IdempotencyRecord.cs`](src/Northstar.Application/Idempotency/IdempotencyRecord.cs#L3-L12)** · lines 3–12

<!-- x:idem-record -->
```csharp
public sealed class IdempotencyRecord
{
    public Guid Id { get; set; }
    public Guid OrganizationId { get; set; }
    public string OperationName { get; set; } = string.Empty;
    public string Key { get; set; } = string.Empty;
    public string RequestHash { get; set; } = string.Empty;
    public Guid ResultResourceId { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}
```

**[`src/Northstar.Infrastructure/Persistence/ApplicationDbContext.cs`](src/Northstar.Infrastructure/Persistence/ApplicationDbContext.cs#L207-L215)** · lines 207–215

<!-- x:idem-index -->
```csharp
private static void ConfigureIdempotency(ModelBuilder modelBuilder)
{
    var entity = modelBuilder.Entity<IdempotencyRecord>();
    entity.HasKey(value => value.Id);
    entity.Property(value => value.OperationName).HasMaxLength(80).IsRequired();
    entity.Property(value => value.Key).HasMaxLength(100).IsRequired();
    entity.Property(value => value.RequestHash).HasMaxLength(64).IsRequired();
    entity.HasIndex(value => new { value.OrganizationId, value.OperationName, value.Key }).IsUnique();
}
```

**How to read the code.** In the controller:

1. `[FromHeader(Name = "Idempotency-Key")] string? idempotencyKey,` (line 69) binds the parameter to the header. It is `string?` because the header may be absent.
2. `idempotencyKey ?? string.Empty,` (line 85) passes an empty string in that case. The service then answers `422` with `IDEMPOTENCY_KEY_REQUIRED`, or with `IDEMPOTENCY_KEY_TOO_LONG` when the key has more than 100 characters ([Application/ImportBatchService.cs:365-379](src/Northstar.Application/Imports/ImportBatchService.cs#L365-L379)).

In `CreateAsync`:

3. `var requestHash = Hash(` (line 121) makes a fingerprint of what was asked for: organization, period and scenario. The fingerprint lets the server tell a true repeat from a different request that reuses a key.
4. `await using (var transaction = await dbContext.BeginTransactionAsync(cancellationToken))` (line 125) opens a database transaction. Everything in the block is saved together or not at all.
5. `var priorResult = await ResolvePriorRequestAsync(` (line 127) asks whether this key has been seen. If so, the method returns straight away with the earlier answer.
6. `dbContext.Add(batch);` (line 156) and `dbContext.Add(CreateIdempotencyRecord(` (line 157) queue two inserts: the new batch, and the record that ties the key to it.
7. `await dbContext.SaveChangesAsync(cancellationToken);` (line 166) writes both, and `await transaction.CommitAsync(cancellationToken);` (line 167) makes them permanent.
8. `catch (DbUpdateException)` (line 169) handles the race. Two requests with the same key can both pass step 5 before either has saved. The unique index (step 13) lets only one insert succeed. The other lands here.
9. `await transaction.RollbackAsync(CancellationToken.None);` (line 171) undoes the loser's work. It is given `CancellationToken.None` so that the rollback still runs if the request was cancelled. `dbContext.ClearTracking();` (line 172) makes the context forget the two objects that could not be saved.
10. The loser then looks the key up again and returns the winner's batch. `throw;` (line 184) re-raises the exception when the lookup finds nothing, because then the failure had another cause.
11. `await queue.QueueAsync(batch.Id, CancellationToken.None);` (line 190) tells the worker about the new batch, after the commit and never before it ([Pattern 10](#pattern-10-background-worker-with-a-queue-and-a-recovery-scan)).

In `ResolvePriorRequestAsync`:

12. `record => record.OrganizationId == organizationId &&` (line 321) starts a lookup by organization, operation name and key. `if (priorRequest is null)` (line 325) means the key is new. `if (!string.Equals(priorRequest.RequestHash, requestHash, StringComparison.Ordinal))` (line 330) means the key is known but the request is different, which is answered with `409` and `"IDEMPOTENCY_KEY_REUSED",` (line 333). Otherwise the original batch is loaded and returned.

In the storage:

13. `entity.HasIndex(value => new { value.OrganizationId, value.OperationName, value.Key }).IsUnique();` (line 214) is the rule that the database enforces: one record per organization, operation and key.
14. `Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(value)));` (line 382) turns the text into bytes, hashes them with SHA-256 and writes the 32 bytes as 64 hexadecimal characters, which is why `entity.Property(value => value.RequestHash).HasMaxLength(64).IsRequired();` (line 213) allows exactly 64.

The other two commands follow the same steps with their own operation names: `RetryImportBatch:{id}` ([Application/ImportBatchService.cs:206](src/Northstar.Application/Imports/ImportBatchService.cs#L206)) and `CalculatePayrollRun` ([Application/PayrollRunService.cs:63-81](src/Northstar.Application/PayrollRuns/PayrollRunService.cs#L63-L81)).

On the client, the key is a fresh UUID for each button press: `crypto.randomUUID()` in [Web/import-panel.component.ts:54](src/Northstar.Web/src/app/imports/import-panel.component.ts#L54) and [Web/app.ts:37](src/Northstar.Web/src/app/app.ts#L37). It is sent as a header in [Web/import-api.service.ts:16](src/Northstar.Web/src/app/imports/import-api.service.ts#L16). The test `Create_WithSameIdempotencyKey_ReturnsTheOriginalBatch` shows the behaviour from outside ([IntegrationTests/ImportBatchesApiTests.cs:45-64](tests/Northstar.Api.IntegrationTests/ImportBatchesApiTests.cs#L45-L64)).

**Why it is done this way.** Two lines in the architecture note: "Repeated calculate requests use an organization-scoped idempotency record." and "Repeated import and retry commands use organization/operation-scoped idempotency records; the database unique key arbitrates a request race." The second half of that sentence is steps 8 to 10: the check in step 5 alone cannot stop two simultaneous requests, and the unique index can.

**Look up.** [`FromHeaderAttribute`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.fromheaderattribute?view=aspnetcore-10.0) · [`SHA256.HashData`](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.sha256.hashdata?view=net-10.0#system-security-cryptography-sha256-hashdata(system-byte())) · [`Convert.ToHexString`](https://learn.microsoft.com/en-us/dotnet/api/system.convert.tohexstring?view=net-10.0#system-convert-tohexstring(system-byte())) · [`Encoding.GetBytes`](https://learn.microsoft.com/en-us/dotnet/api/system.text.encoding.getbytes?view=net-10.0#system-text-encoding-getbytes(system-string)) · [`DbUpdateException`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.dbupdateexception?view=efcore-10.0) · [`DatabaseFacade.BeginTransactionAsync`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.infrastructure.databasefacade.begintransactionasync?view=efcore-10.0#microsoft-entityframeworkcore-infrastructure-databasefacade-begintransactionasync(system-threading-cancellationtoken)) · [`IDbContextTransaction.CommitAsync`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.storage.idbcontexttransaction.commitasync?view=efcore-10.0#microsoft-entityframeworkcore-storage-idbcontexttransaction-commitasync(system-threading-cancellationtoken)) · [`IDbContextTransaction.RollbackAsync`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.storage.idbcontexttransaction.rollbackasync?view=efcore-10.0#microsoft-entityframeworkcore-storage-idbcontexttransaction-rollbackasync(system-threading-cancellationtoken)) · [`ChangeTracker.Clear`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.changetracking.changetracker.clear?view=efcore-10.0#microsoft-entityframeworkcore-changetracking-changetracker-clear) · [`IndexBuilder.IsUnique`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.metadata.builders.indexbuilder-1.isunique?view=efcore-10.0#microsoft-entityframeworkcore-metadata-builders-indexbuilder-1-isunique(system-boolean)) · [`CancellationToken.None`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken.none?view=net-10.0#system-threading-cancellationtoken-none) · [`crypto.randomUUID`](https://developer.mozilla.org/en-US/docs/Web/API/Crypto/randomUUID)

**Read more.** [Idempotent (MDN glossary)](https://developer.mozilla.org/en-US/docs/Glossary/Idempotent) · [RFC 9110: Idempotent methods](https://www.rfc-editor.org/rfc/rfc9110.html#name-idempotent-methods) · [IETF draft: The Idempotency-Key HTTP Header Field](https://datatracker.ietf.org/doc/draft-ietf-httpapi-idempotency-key-header/) · [Using transactions](https://learn.microsoft.com/en-us/ef/core/saving/transactions) · [Indexes: index uniqueness](https://learn.microsoft.com/en-us/ef/core/modeling/indexes#index-uniqueness) · [Model binding in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/mvc/models/model-binding?view=aspnetcore-10.0)

### Pattern 9: Accept now, process later

**What it is.** An import takes time, because it calls another system. The API does not make the client wait. It records the request, answers `202 Accepted` at once, and does the work in the background. `202` means "received and not finished yet". The response says where to ask for progress.

**Where to find it.** The `Create` and `Retry` actions of [Api/ImportBatchesController.cs](src/Northstar.Api/Controllers/ImportBatchesController.cs).

**[`src/Northstar.Api/Controllers/ImportBatchesController.cs`](src/Northstar.Api/Controllers/ImportBatchesController.cs#L118-L122)** · lines 118–122

<!-- x:ctrl-accepted -->
```csharp
private ActionResult<ImportBatchView> ToAcceptedResult(
    OperationResult<ImportBatchView> result) =>
    result.Outcome == OperationOutcome.Success
        ? AcceptedAtAction(nameof(Get), new { batchId = result.Value!.Id }, result.Value)
        : ToActionResult(result);
```

**How to read the code.**

1. The action declares the status it returns on success: `[ProducesResponseType<ImportBatchView>(StatusCodes.Status202Accepted)]` (line 63) in the excerpt of [Pattern 8](#pattern-8-idempotent-commands-with-an-idempotency-key).
2. `? AcceptedAtAction(nameof(Get), new { batchId = result.Value!.Id }, result.Value)` (line 121) builds the `202` response. The first argument names the action that can be asked for progress, and the second gives its route value. ASP.NET Core turns the two into a `Location` header that holds the URL of `GET /api/import-batches/{id}` for this batch. The third argument is the body: the batch as it is now, with status `Pending`.
3. `nameof(Get)` produces the string `"Get"`, checked by the compiler. If the method is renamed, this line stops compiling instead of pointing at nothing.
4. The `!` after `result.Value` is the null-forgiving operator. `Value` is declared as nullable, and on the success branch the code knows it is set.
5. `: ToActionResult(result);` (line 122) sends every other outcome through the shared mapping of [Pattern 6](#pattern-6-result-objects-mapped-to-http-responses).

What happens next is in other patterns. The worker picks the batch up and moves it to a final status ([Pattern 10](#pattern-10-background-worker-with-a-queue-and-a-recovery-scan)). The Angular app asks `GET /api/import-batches/{id}` once a second until the status is no longer `Pending` or `Processing` ([Pattern 25](#pattern-25-polling-with-rxjs-and-automatic-cleanup)). The API tests do the same in a loop ([IntegrationTests/ImportBatchesApiTests.cs:376-400](tests/Northstar.Api.IntegrationTests/ImportBatchesApiTests.cs#L376-L400)) after checking the status code and the `Location` header ([IntegrationTests/ImportBatchesApiTests.cs:24-25](tests/Northstar.Api.IntegrationTests/ImportBatchesApiTests.cs#L24-L25)).

**Why it is done this way.** The README states the contract: "`POST /api/import-batches` requires an organization-scoped `Idempotency-Key` and returns `202 Accepted`." It then describes the work that follows: "A local worker retrieves paged provider data outside a database transaction, validates each staging row, then promotes accepted rows and the terminal status atomically." Fetching pages from another system, with retries, can take longer than a client should wait for one response. Microsoft's cloud design guide calls this arrangement the Asynchronous Request-Reply pattern.

**Look up.** [`ControllerBase.AcceptedAtAction`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.controllerbase.acceptedataction?view=aspnetcore-10.0#microsoft-aspnetcore-mvc-controllerbase-acceptedataction(system-string-system-object-system-object)) · [`StatusCodes.Status202Accepted`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.statuscodes.status202accepted?view=aspnetcore-10.0#microsoft-aspnetcore-http-statuscodes-status202accepted) · [`HttpResponseHeaders.Location`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.headers.httpresponseheaders.location?view=net-10.0#system-net-http-headers-httpresponseheaders-location) · [`nameof`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/nameof) · [Null-forgiving `!`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/null-forgiving) · [Anonymous type `new { ... }`](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/anonymous-types)

**Read more.** [202 Accepted (MDN)](https://developer.mozilla.org/en-US/docs/Web/HTTP/Reference/Status/202) · [RFC 9110: 202 Accepted](https://www.rfc-editor.org/rfc/rfc9110.html#name-202-accepted) · [Asynchronous Request-Reply pattern](https://learn.microsoft.com/en-us/azure/architecture/patterns/asynchronous-request-reply)

### Pattern 10: Background worker with a queue and a recovery scan

**What it is.** The work an import needs is done by a *hosted service*: a class that the host starts with the application and stops with it. It waits for batch ids on an in-memory queue and processes one batch at a time. An in-memory queue forgets everything when the process stops. So a second loop looks in the database every five seconds for batches that still need work and puts their ids on the queue again.

**Where to find it.** [Infrastructure/ImportBatchQueue.cs](src/Northstar.Infrastructure/Messaging/ImportBatchQueue.cs), [Infrastructure/ImportBatchWorker.cs](src/Northstar.Infrastructure/Messaging/ImportBatchWorker.cs) and the last lines of [Infrastructure/DependencyInjection.cs](src/Northstar.Infrastructure/DependencyInjection.cs).

**[`src/Northstar.Infrastructure/DependencyInjection.cs`](src/Northstar.Infrastructure/DependencyInjection.cs#L59-L62)** · lines 59–62

<!-- x:infra-di-worker -->
```csharp
services.AddSingleton<ImportBatchQueue>();
services.AddSingleton<IImportBatchQueue>(provider =>
    provider.GetRequiredService<ImportBatchQueue>());
services.AddHostedService<ImportBatchWorker>();
```

**[`src/Northstar.Infrastructure/Messaging/ImportBatchQueue.cs`](src/Northstar.Infrastructure/Messaging/ImportBatchQueue.cs#L6-L20)** · lines 6–20

<!-- x:bg-queue -->
```csharp
public sealed class ImportBatchQueue : IImportBatchQueue
{
    private readonly Channel<Guid> _channel = Channel.CreateUnbounded<Guid>(
        new UnboundedChannelOptions
        {
            SingleReader = true,
            SingleWriter = false
        });

    public ValueTask QueueAsync(Guid importBatchId, CancellationToken cancellationToken) =>
        _channel.Writer.WriteAsync(importBatchId, cancellationToken);

    public IAsyncEnumerable<Guid> ReadAllAsync(CancellationToken cancellationToken) =>
        _channel.Reader.ReadAllAsync(cancellationToken);
}
```

**[`src/Northstar.Infrastructure/Messaging/ImportBatchWorker.cs`](src/Northstar.Infrastructure/Messaging/ImportBatchWorker.cs#L16-L55)** · lines 16–55

<!-- x:bg-worker -->
```csharp
protected override async Task ExecuteAsync(CancellationToken stoppingToken)
{
    var scanner = ScanForRecoverableBatchesAsync(stoppingToken);

    try
    {
        await foreach (var importBatchId in queue.ReadAllAsync(stoppingToken))
        {
            try
            {
                await using var scope = scopeFactory.CreateAsyncScope();
                var processor = scope.ServiceProvider.GetRequiredService<ImportBatchProcessor>();
                await processor.ProcessAsync(importBatchId, stoppingToken);
            }
            catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
            {
                return;
            }
            catch (Exception exception)
            {
                logger.LogError(
                    exception,
                    "Import batch {ImportBatchId} failed with an unexpected worker exception.",
                    importBatchId);
                await MarkUnexpectedFailureAsync(importBatchId);
            }
        }
    }
    finally
    {
        try
        {
            await scanner;
        }
        catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
        {
            // Expected host shutdown.
        }
    }
}
```

**[`src/Northstar.Infrastructure/Messaging/ImportBatchWorker.cs`](src/Northstar.Infrastructure/Messaging/ImportBatchWorker.cs#L57-L99)** · lines 57–99

<!-- x:bg-scan -->
```csharp
private async Task ScanForRecoverableBatchesAsync(CancellationToken cancellationToken)
{
    using var timer = new PeriodicTimer(TimeSpan.FromSeconds(5));
    do
    {
        try
        {
            await using var scope = scopeFactory.CreateAsyncScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();
            var now = DateTimeOffset.UtcNow;
            var candidates = await dbContext.ImportBatches
                .AsNoTracking()
                .Where(batch => batch.Status == ImportBatchStatus.Pending ||
                    batch.Status == ImportBatchStatus.Processing)
                .Select(batch => new
                {
                    batch.Id,
                    batch.Status,
                    batch.ProcessingLeaseExpiresAt
                })
                .ToListAsync(cancellationToken);
            var pendingIds = candidates
                .Where(batch => batch.Status == ImportBatchStatus.Pending ||
                    batch.ProcessingLeaseExpiresAt is null ||
                    batch.ProcessingLeaseExpiresAt <= now)
                .Select(batch => batch.Id);

            foreach (var pendingId in pendingIds)
            {
                await queue.QueueAsync(pendingId, cancellationToken);
            }
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "The import recovery scan failed and will be retried.");
        }
    }
    while (await timer.WaitForNextTickAsync(cancellationToken));
}
```

**How to read the code.** The registration and the queue:

1. `services.AddSingleton<ImportBatchQueue>();` (line 59) registers one queue object for the whole application. The next statement registers the interface `IImportBatchQueue` with a small function that returns that same object. The services of the Application project ask for the interface, which only lets them write to the queue. The worker asks for the class, which also lets it read.
2. `services.AddHostedService<ImportBatchWorker>();` (line 62) tells the host to create the worker at startup and to call its `ExecuteAsync` method.
3. The queue wraps a *channel*: `private readonly Channel<Guid> _channel = Channel.CreateUnbounded<Guid>(` (line 8). A channel is a thread-safe first-in, first-out queue with an asynchronous way to wait for the next item. *Unbounded* means that it has no size limit, so a write never has to wait for room.
4. `SingleReader = true,` (line 11) and `SingleWriter = false` (line 12) describe how the channel will be used: one reader (the worker) and several writers (the requests and the scan). The documentation calls such options promises. They let the channel choose a more efficient implementation.
5. `QueueAsync` writes one id. `ReadAllAsync` hands out the reading side as an `IAsyncEnumerable<Guid>`: a sequence in which the next item is awaited.

The worker:

6. `var scanner = ScanForRecoverableBatchesAsync(stoppingToken);` (line 18) starts the recovery scan. There is no `await` in front of the call. The call starts the scan and hands back a task that represents it. The method keeps that task in `scanner` and goes on to the main loop.
7. `await foreach (var importBatchId in queue.ReadAllAsync(stoppingToken))` (line 22) is the main loop. It waits for the next id without holding a thread. The channel is never closed, so the loop only ends when the host cancels `stoppingToken` at shutdown. The wait then throws an `OperationCanceledException`, the `finally` block runs, and the exception leaves `ExecuteAsync`.
8. `await using var scope = scopeFactory.CreateAsyncScope();` (line 26) creates a new dependency-injection scope for each batch. The worker lives as long as the application, and the processor and its database context are scoped services ([Pattern 2](#pattern-2-composition-root-and-service-registration)). A scope per batch gives each batch its own database context. `await using` disposes the scope at the end of the block.
9. `var processor = scope.ServiceProvider.GetRequiredService<ImportBatchProcessor>();` (line 27) asks the scope for the processor. The next line runs it.
10. `catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)` (line 30) has a `when` part. That is an *exception filter*: the clause only applies when the condition is true. Here it means "the work was cancelled because the host is shutting down". The method returns. The batch stays `Processing` with its lease, so it can be picked up again later.
11. `catch (Exception exception)` (line 34) takes every other failure. It writes a log entry and calls `MarkUnexpectedFailureAsync`, which sets the batch to `Failed` when it is still `Processing` ([Pattern 18](#pattern-18-async-all-the-way-with-cancellation)). The loop then continues with the next id, so one failing batch does not stop the worker.
12. The log call passes the text `"Import batch {ImportBatchId} failed with an unexpected worker exception."` and the id as a separate argument. `{ImportBatchId}` is a named placeholder in a *message template*. It is not string interpolation. Logging providers can store the id as a field of its own.
13. The `finally` block runs however the loop ends. `await scanner;` (line 48) waits for the scan to finish as well. The filter around it swallows the cancellation that is expected at shutdown.

The scan:

14. `using var timer = new PeriodicTimer(TimeSpan.FromSeconds(5));` (line 59) creates a timer that ticks every five seconds.
15. The loop is a `do … while` loop that ends in `while (await timer.WaitForNextTickAsync(cancellationToken));` (line 98). The body runs once at startup and then once after every tick. `WaitForNextTickAsync` gives `true` on a tick. At shutdown the token cancels the wait, the `OperationCanceledException` leaves the method, and the `using` disposes the timer.
16. The query asks the database only for batches whose status is `Pending` or `Processing`. `.Select(batch => new` (line 71) reads just three columns into an *anonymous type*, a small unnamed class that the compiler creates.
17. The second `Where` runs in memory, on the list that came back. It keeps the `Pending` batches and the `Processing` batches whose *lease* is missing or has run out: `batch.ProcessingLeaseExpiresAt <= now)` (line 81). The lease is the time until which a worker has claimed a batch ([Pattern 16](#pattern-16-optimistic-concurrency-with-a-version-number)). It is a `DateTimeOffset`. The SQLite provider's documentation lists `DateTimeOffset` among the types whose comparisons are not translated to SQL, and SQLite is the default database here.
18. `await queue.QueueAsync(pendingId, cancellationToken);` (line 86) puts each id on the same queue that the main loop reads.
19. Two `catch` clauses end the body. The first rethrows a cancellation at shutdown with `throw;`. The second logs any other error and lets the loop go on to the next tick.

The pieces around it:

- The same id can be in the channel more than once. The request queues it after its transaction has committed ([Application/ImportBatchService.cs:190](src/Northstar.Application/Imports/ImportBatchService.cs#L190)), and the scan may queue it again. That does no harm, because the processor first tries to claim the batch ([Pattern 16](#pattern-16-optimistic-concurrency-with-a-version-number)) and returns at once when the batch is already claimed or finished ([Application/ImportBatchProcessor.cs:23-26](src/Northstar.Application/Imports/ImportBatchProcessor.cs#L23-L26)).
- Neither the scan nor the processor's lookup filters by organization. The worker has no caller, and it handles the batches of every organization.
- The request side is [Pattern 9](#pattern-9-accept-now-process-later). The call that the processor makes to the provider is [Pattern 11](#pattern-11-typed-httpclient-with-validated-options) and [Pattern 12](#pattern-12-bounded-retries-timeouts-and-exception-translation).

**Why it is done this way.** The README names the parts: "A persisted `Pending` state, recurring recovery scan, optimistic claim, and expiring processing lease make a lost in-memory notification or interrupted worker recoverable." It also says what the channel is not: "The `Channel` notification is deliberately an incremental local seam, not an Azure queue or transactional outbox." The architecture note gives the reason for the scan: "The database state, not the in-memory notification, is the recoverable source of local work. A recurring scan republishes `Pending` batches and expired `Processing` leases." ([docs/architecture.md:37](docs/architecture.md#L37)). It also covers the two `catch` clauses of the worker: "Unexpected worker failures become sanitized retryable failures; host cancellation is allowed to propagate and the lease enables later recovery." ([docs/architecture.md:54](docs/architecture.md#L54)). The glossary defines the processing lease as "the bounded claim window after which an interrupted local worker may safely attempt to reclaim a batch using optimistic concurrency." ([docs/domain-glossary.md:8](docs/domain-glossary.md#L8)). The scope per batch follows the documentation for hosted services: no scope is created for a hosted service by default, so the service creates one and asks it for the scoped services it needs.

**Look up.** [`BackgroundService`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.hosting.backgroundservice?view=net-10.0-pp) · [`BackgroundService.ExecuteAsync`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.hosting.backgroundservice.executeasync?view=net-10.0-pp#microsoft-extensions-hosting-backgroundservice-executeasync(system-threading-cancellationtoken)) · [`ServiceCollectionHostedServiceExtensions.AddHostedService`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.servicecollectionhostedserviceextensions.addhostedservice?view=net-10.0-pp#microsoft-extensions-dependencyinjection-servicecollectionhostedserviceextensions-addhostedservice-1(microsoft-extensions-dependencyinjection-iservicecollection)) · [`Channel.CreateUnbounded`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.channels.channel.createunbounded?view=net-10.0#system-threading-channels-channel-createunbounded-1(system-threading-channels-unboundedchanneloptions)) · [`ChannelReader.ReadAllAsync`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.channels.channelreader-1.readallasync?view=net-10.0#system-threading-channels-channelreader-1-readallasync(system-threading-cancellationtoken)) · [`ChannelWriter.WriteAsync`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.channels.channelwriter-1.writeasync?view=net-10.0#system-threading-channels-channelwriter-1-writeasync(-0-system-threading-cancellationtoken)) · [`PeriodicTimer`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.periodictimer?view=net-10.0) · [`PeriodicTimer.WaitForNextTickAsync`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.periodictimer.waitfornexttickasync?view=net-10.0#system-threading-periodictimer-waitfornexttickasync(system-threading-cancellationtoken)) · [`IServiceScopeFactory`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.iservicescopefactory?view=net-10.0-pp) · [`ServiceProviderServiceExtensions.CreateAsyncScope`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.serviceproviderserviceextensions.createasyncscope?view=net-10.0-pp#microsoft-extensions-dependencyinjection-serviceproviderserviceextensions-createasyncscope(system-iserviceprovider)) · [`LoggerExtensions.LogError`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.logging.loggerextensions.logerror?view=net-10.0-pp#microsoft-extensions-logging-loggerextensions-logerror(microsoft-extensions-logging-ilogger-system-exception-system-string-system-object())) · [`await foreach`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/iteration-statements#await-foreach) · [`await using`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/using) · [Exception filter `catch (X) when (...)`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/exception-handling-statements#a-when-exception-filter) · [`try` / `finally`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/exception-handling-statements#the-try-finally-statement) · [Anonymous type `new { ... }`](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/anonymous-types)

**Read more.** [Background tasks with hosted services](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/host/hosted-services?view=aspnetcore-10.0) · [Use scoped services within a BackgroundService](https://learn.microsoft.com/en-us/dotnet/core/extensions/scoped-service) · [System.Threading.Channels library](https://learn.microsoft.com/en-us/dotnet/core/extensions/channels) · [Create a queue service](https://learn.microsoft.com/en-us/dotnet/core/extensions/queue-service) · [.NET: Timers](https://learn.microsoft.com/en-us/dotnet/standard/threading/timers) · [SQLite provider limitations](https://learn.microsoft.com/en-us/ef/core/providers/sqlite/limitations) · [Log message template](https://learn.microsoft.com/en-us/dotnet/core/extensions/logging/overview#log-message-template) · [Competing Consumers pattern](https://learn.microsoft.com/en-us/azure/architecture/patterns/competing-consumers) · [Transactional Outbox pattern (shown with Azure Cosmos DB)](https://learn.microsoft.com/en-us/azure/architecture/databases/guide/transactional-out-box-cosmos)

### Pattern 11: Typed HttpClient with validated options

**What it is.** The application calls one outside system, the payroll provider. Two things are set up for that call. An *options class* holds the settings (address, number of attempts, delay, timeout) as typed properties. They are filled from configuration and checked when the application starts. A *typed client* is a class that receives a ready-configured `HttpClient` in its constructor and offers methods in the application's own terms. The rest of the application sees only the interface `IPayrollProviderClient`.

**Where to find it.** [Infrastructure/PayrollProviderOptions.cs](src/Northstar.Infrastructure/Integrations/PayrollProviderOptions.cs), [Infrastructure/DependencyInjection.cs:39-58](src/Northstar.Infrastructure/DependencyInjection.cs#L39-L58), [Infrastructure/FakePayrollProviderClient.cs:9-13](src/Northstar.Infrastructure/Integrations/FakePayrollProviderClient.cs#L9-L13) and the `PayrollProvider` section of [Api/appsettings.json:9-14](src/Northstar.Api/appsettings.json#L9-L14).

**[`src/Northstar.Infrastructure/Integrations/PayrollProviderOptions.cs`](src/Northstar.Infrastructure/Integrations/PayrollProviderOptions.cs#L3-L11)** · lines 3–11

<!-- x:http-options -->
```csharp
public sealed class PayrollProviderOptions
{
    public const string SectionName = "PayrollProvider";

    public Uri BaseUrl { get; set; } = new("http://localhost:5130");
    public int MaxAttempts { get; set; } = 3;
    public int BaseRetryDelayMilliseconds { get; set; } = 100;
    public int AttemptTimeoutMilliseconds { get; set; } = 10_000;
}
```

**[`src/Northstar.Infrastructure/DependencyInjection.cs`](src/Northstar.Infrastructure/DependencyInjection.cs#L39-L58)** · lines 39–58

<!-- x:infra-di-http -->
```csharp
services.AddOptions<PayrollProviderOptions>()
    .Bind(configuration.GetSection(PayrollProviderOptions.SectionName))
    .Validate(options => options.BaseUrl.IsAbsoluteUri, "PayrollProvider:BaseUrl must be absolute.")
    .Validate(options => options.MaxAttempts is >= 1 and <= 10, "PayrollProvider:MaxAttempts must be between 1 and 10.")
    .Validate(options => options.BaseRetryDelayMilliseconds is >= 0 and <= 30_000,
        "PayrollProvider:BaseRetryDelayMilliseconds must be between 0 and 30000.")
    .Validate(options => options.AttemptTimeoutMilliseconds is >= 1 and <= 300_000,
        "PayrollProvider:AttemptTimeoutMilliseconds must be between 1 and 300000.")
    .ValidateOnStart();
services.AddHttpClient<IPayrollProviderClient, FakePayrollProviderClient>((provider, client) =>
{
    var options = provider
        .GetRequiredService<Microsoft.Extensions.Options.IOptions<PayrollProviderOptions>>()
        .Value;
    client.BaseAddress = options.BaseUrl;
    // The provider client owns a per-attempt timeout that spans both headers
    // and the streamed JSON body. A second HttpClient timeout would make
    // cancellation classification ambiguous.
    client.Timeout = Timeout.InfiniteTimeSpan;
});
```

**How to read the code.** The options:

1. `public const string SectionName = "PayrollProvider";` (line 5) keeps the name of the configuration section in a constant, next to the properties that the section fills.
2. Each of the four properties has a public setter and a default value, as in `public int MaxAttempts { get; set; } = 3;` (line 8). The underscore in `10_000` is a *digit separator*. It only makes the number easier to read.
3. `services.AddOptions<PayrollProviderOptions>()` (line 39) starts the registration. It returns a builder, and the next calls are chained onto it.
4. `.Bind(configuration.GetSection(PayrollProviderOptions.SectionName))` (line 40) copies the values of the `PayrollProvider` section onto the properties with the same names. The binder converts each text value to the type of its property: one `Uri` and three `int`s. A key that is missing leaves the default value of the class in place.
5. Each of the four `Validate` calls takes a test and the message to report when the test fails. `options.MaxAttempts is >= 1 and <= 10` (line 42) is a pattern. `>= 1` and `<= 10` are *relational patterns*, and `and` requires both of them.
6. `.ValidateOnStart();` (line 47) moves the check to startup. Without it, the rules would run the first time something asks for the options, which here is the moment the worker processes its first import. With it, a wrong setting stops the application as it starts, with the message of the rule that failed.

The typed client:

7. `services.AddHttpClient<IPayrollProviderClient, FakePayrollProviderClient>((provider, client) =>` (line 48) registers the typed client. Whoever asks for `IPayrollProviderClient` receives a new `FakePayrollProviderClient`, and the container passes it an `HttpClient` that the lambda has prepared. The documentation states that a typed client is registered as transient, which means that a new one is created every time it is asked for.
8. `.GetRequiredService<Microsoft.Extensions.Options.IOptions<PayrollProviderOptions>>()` (line 51) asks the container for the options. `IOptions<T>` is the wrapper in which options are handed out, and `.Value` on the next line is the options object itself. The type name is written out with its namespace because this file has no `using Microsoft.Extensions.Options;` directive.
9. `client.BaseAddress = options.BaseUrl;` (line 53) sets the address that relative request URIs are combined with. The client class only ever builds the relative part, `api/time-entries?…` ([Infrastructure/FakePayrollProviderClient.cs:154-170](src/Northstar.Infrastructure/Integrations/FakePayrollProviderClient.cs#L154-L170)).
10. `client.Timeout = Timeout.InfiniteTimeSpan;` (line 57) switches off the timeout of `HttpClient` itself, which is 100 seconds by default. The comment above the line gives the reason. The client class has its own timeout for each attempt, and a second timeout would make it unclear which of the two cancelled a request ([Pattern 12](#pattern-12-bounded-retries-timeouts-and-exception-translation)).

The pieces around it:

- The client class asks for both in its primary constructor ([Pattern 3](#pattern-3-constructor-injection-with-primary-constructors)): an `HttpClient` and an `IOptions<PayrollProviderOptions>`. It keeps `options.Value` in the field `_options`.
- The name needs a word of explanation. `FakePayrollProviderClient` is a real HTTP client. "Fake" belongs to the provider that it calls, the small web application in `tools/Northstar.FakePayrollProvider` ([Pattern 21](#pattern-21-minimal-api-endpoints-in-the-fake-provider)).
- `BuildUri` puts the query string together from a dictionary. It leaves out the entries that have no value, such as the cursor when the first page is requested. It passes every name and value through `Uri.EscapeDataString`, so a character such as `&` or a space inside a value cannot change the meaning of the URI.
- The only class that asks for `IPayrollProviderClient` is `ImportBatchProcessor` ([Application/ImportBatchProcessor.cs:11](src/Northstar.Application/Imports/ImportBatchProcessor.cs#L11)). It is in the Application project, which knows the interface and its exceptions ([Pattern 12](#pattern-12-bounded-retries-timeouts-and-exception-translation)) and nothing about HTTP.
- The values in `appsettings.json` are the same as the defaults in the class.
- The tests use the two halves separately ([Pattern 20](#pattern-20-tests-at-three-levels-in-net)). The API tests replace the registration of `IPayrollProviderClient` with a stand-in that makes no HTTP call. The client tests create the real class by hand, with a test `HttpClient` and `Options.Create(new PayrollProviderOptions { … })` ([IntegrationTests/FakePayrollProviderClientTests.cs:156-168](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L156-L168)).

**Why it is done this way.** The README lists a "typed provider client" among the things that the Infrastructure project owns ([README.md:113](README.md#L113)). The decision record says where such code belongs: "Provider-specific and asynchronous boundaries will be added behind application-owned interfaces." ([docs/adr/0001-use-modular-monolith.md:11](docs/adr/0001-use-modular-monolith.md#L11)). The documentation for `IHttpClientFactory` gives the general reasons. A typed client keeps what is known about one remote service in one class. The factory behind it pools the underlying message handlers and replaces them after a lifetime of two minutes by default, which the documentation describes as the way to avoid the socket and DNS problems of managing `HttpClient` objects by hand. The options half follows the shape of the documentation's own sample: `AddOptions`, `Bind`, `Validate` with a message, `ValidateOnStart`.

**Look up.** [`OptionsServiceCollectionExtensions.AddOptions`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.optionsservicecollectionextensions.addoptions?view=net-10.0-pp#microsoft-extensions-dependencyinjection-optionsservicecollectionextensions-addoptions-1(microsoft-extensions-dependencyinjection-iservicecollection)) · [`OptionsBuilderConfigurationExtensions.Bind`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.optionsbuilderconfigurationextensions.bind?view=net-10.0-pp#microsoft-extensions-dependencyinjection-optionsbuilderconfigurationextensions-bind-1(microsoft-extensions-options-optionsbuilder((-0))-microsoft-extensions-configuration-iconfiguration)) · [`IConfiguration.GetSection`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.configuration.iconfiguration.getsection?view=net-10.0-pp#microsoft-extensions-configuration-iconfiguration-getsection(system-string)) · [`OptionsBuilder.Validate`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.options.optionsbuilder-1.validate?view=net-10.0-pp#microsoft-extensions-options-optionsbuilder-1-validate(system-func((-0-system-boolean))-system-string)) · [`OptionsBuilderExtensions.ValidateOnStart`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.optionsbuilderextensions.validateonstart?view=net-10.0-pp#microsoft-extensions-dependencyinjection-optionsbuilderextensions-validateonstart-1(microsoft-extensions-options-optionsbuilder((-0)))) · [`IOptions`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.options.ioptions-1?view=net-10.0-pp) · [`IOptions.Value`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.options.ioptions-1.value?view=net-10.0-pp#microsoft-extensions-options-ioptions-1-value) · [`Options.Create`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.options.options.create?view=net-10.0-pp#microsoft-extensions-options-options-create-1(-0)) · [`HttpClientFactoryServiceCollectionExtensions.AddHttpClient`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.httpclientfactoryservicecollectionextensions.addhttpclient?view=net-10.0-pp#microsoft-extensions-dependencyinjection-httpclientfactoryservicecollectionextensions-addhttpclient-2(microsoft-extensions-dependencyinjection-iservicecollection-system-action((system-iserviceprovider-system-net-http-httpclient)))) · [`HttpClient.BaseAddress`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient.baseaddress?view=net-10.0#system-net-http-httpclient-baseaddress) · [`HttpClient.Timeout`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient.timeout?view=net-10.0#system-net-http-httpclient-timeout) · [`Timeout.InfiniteTimeSpan`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.timeout.infinitetimespan?view=net-10.0#system-threading-timeout-infinitetimespan) · [`Uri.IsAbsoluteUri`](https://learn.microsoft.com/en-us/dotnet/api/system.uri.isabsoluteuri?view=net-10.0#system-uri-isabsoluteuri) · [`Uri.EscapeDataString`](https://learn.microsoft.com/en-us/dotnet/api/system.uri.escapedatastring?view=net-10.0#system-uri-escapedatastring(system-string)) · [Relational pattern `is >= 1`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/patterns#relational-patterns) · [`and` pattern](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/patterns#logical-patterns) · [Digit separator `10_000`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/integral-numeric-types#integer-literals) · [`const`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/const)

**Read more.** [Typed clients](https://learn.microsoft.com/en-us/dotnet/core/extensions/httpclient-factory#typed-clients) · [Make HTTP requests using IHttpClientFactory](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/http-requests?view=aspnetcore-10.0) · [Guidelines for using HttpClient](https://learn.microsoft.com/en-us/dotnet/fundamentals/networking/http/httpclient-guidelines) · [Options pattern in .NET](https://learn.microsoft.com/en-us/dotnet/core/extensions/options) · [Options validation in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/configuration/options?view=aspnetcore-10.0#options-validation) · [Configuration in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-10.0)

### Pattern 12: Bounded retries, timeouts and exception translation

**What it is.** A call to another system can fail in two different ways. A *transient* failure may go away by itself: a timeout, a dropped connection, "too many requests", a server error. Trying again makes sense, but only a limited number of times and with a pause in between. A *permanent* failure will be the same the next time: a rejected request, or an answer that cannot be read. The client class tries each request up to `MaxAttempts` times and gives every attempt its own time limit. It reports the final outcome with one of two exception types that the Application project defines, so the caller never sees an HTTP status code.

**Where to find it.** [Infrastructure/FakePayrollProviderClient.cs:70-185](src/Northstar.Infrastructure/Integrations/FakePayrollProviderClient.cs#L70-L185), the exception types at the end of [Application/IPayrollProviderClient.cs:28-48](src/Northstar.Application/Abstractions/IPayrollProviderClient.cs#L28-L48), and the caller in [Application/ImportBatchProcessor.cs:52-82](src/Northstar.Application/Imports/ImportBatchProcessor.cs#L52-L82).

**[`src/Northstar.Infrastructure/Integrations/FakePayrollProviderClient.cs`](src/Northstar.Infrastructure/Integrations/FakePayrollProviderClient.cs#L70-L152)** · lines 70–152

<!-- x:http-retry -->
```csharp
private async Task<ProviderTimeEntryPage?> GetPageWithRetryAsync(
    ProviderImportRequest request,
    string? cursor,
    CancellationToken cancellationToken)
{
    Exception? lastException = null;

    for (var attempt = 1; attempt <= _options.MaxAttempts; attempt++)
    {
        TimeSpan? retryDelay = null;
        using var attemptTimeout = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        attemptTimeout.CancelAfter(TimeSpan.FromMilliseconds(_options.AttemptTimeoutMilliseconds));

        try
        {
            var uri = BuildUri(request, cursor);
            using var response = await httpClient.GetAsync(
                uri,
                HttpCompletionOption.ResponseHeadersRead,
                attemptTimeout.Token);

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    return await response.Content.ReadFromJsonAsync<ProviderTimeEntryPage>(
                        cancellationToken: attemptTimeout.Token);
                }
                catch (JsonException exception)
                {
                    throw new PayrollProviderContractException(
                        "The payroll provider returned malformed JSON.",
                        exception);
                }
            }

            if (!IsTransient(response.StatusCode))
            {
                var statusCode = (int)response.StatusCode;
                throw new PayrollProviderContractException(
                    $"The payroll provider rejected the import request with HTTP {statusCode}.");
            }

            if (attempt == _options.MaxAttempts)
            {
                var statusCode = (int)response.StatusCode;
                throw new PayrollProviderTransientException(
                    $"The payroll provider remained unavailable after {_options.MaxAttempts} attempts (HTTP {statusCode}).");
            }

            retryDelay = GetRetryDelay(response, attempt);
        }
        catch (HttpRequestException exception) when (attempt < _options.MaxAttempts)
        {
            lastException = exception;
            retryDelay = GetBackoff(attempt);
        }
        catch (HttpRequestException exception)
        {
            throw new PayrollProviderTransientException(
                $"The payroll provider could not be reached after {_options.MaxAttempts} attempts.",
                exception);
        }
        catch (OperationCanceledException exception)
            when (!cancellationToken.IsCancellationRequested && attempt < _options.MaxAttempts)
        {
            lastException = exception;
            retryDelay = GetBackoff(attempt);
        }
        catch (OperationCanceledException exception) when (!cancellationToken.IsCancellationRequested)
        {
            throw new PayrollProviderTransientException(
                $"The payroll provider timed out after {_options.MaxAttempts} attempts.",
                exception);
        }

        await Task.Delay(retryDelay!.Value, cancellationToken);
    }

    throw new PayrollProviderTransientException(
        "The payroll provider could not be reached.",
        lastException);
}
```

**[`src/Northstar.Infrastructure/Integrations/FakePayrollProviderClient.cs`](src/Northstar.Infrastructure/Integrations/FakePayrollProviderClient.cs#L172-L185)** · lines 172–185

<!-- x:http-helpers -->
```csharp
private TimeSpan GetRetryDelay(HttpResponseMessage response, int attempt)
{
    var retryAfter = response.Headers.RetryAfter?.Delta;
    return retryAfter is not null
        ? TimeSpan.FromMilliseconds(Math.Min(retryAfter.Value.TotalMilliseconds, 1_000))
        : GetBackoff(attempt);
}

private TimeSpan GetBackoff(int attempt) =>
    TimeSpan.FromMilliseconds(_options.BaseRetryDelayMilliseconds * Math.Pow(2, attempt - 1));

private static bool IsTransient(HttpStatusCode statusCode) =>
    statusCode is HttpStatusCode.RequestTimeout or HttpStatusCode.TooManyRequests ||
    (int)statusCode >= 500;
```

**[`src/Northstar.Application/Abstractions/IPayrollProviderClient.cs`](src/Northstar.Application/Abstractions/IPayrollProviderClient.cs#L28-L48)** · lines 28–48

<!-- x:http-exceptions -->
```csharp
public abstract class PayrollProviderException(
    string message,
    Exception? innerException = null
    )
    : Exception(message, innerException);

public sealed class PayrollProviderTransientException(
    string message,
    Exception? innerException = null
    )
    : PayrollProviderException(
        message,
        innerException
        );

public sealed class PayrollProviderContractException(
    string message,
    Exception? innerException = null)
    : PayrollProviderException(
        message,
        innerException);
```

**[`src/Northstar.Application/Imports/ImportBatchProcessor.cs`](src/Northstar.Application/Imports/ImportBatchProcessor.cs#L52-L82)** · lines 52–82

<!-- x:proc-provider -->
```csharp
IReadOnlyList<ProviderTimeEntryRecord> records;
try
{
    records = await providerClient.GetTimeEntriesAsync(
        new ProviderImportRequest(
            organization.ExternalReference,
            batch.PeriodStart,
            batch.PeriodEnd,
            batch.ProviderScenario),
        cancellationToken);
}
catch (PayrollProviderTransientException exception)
{
    batch.Fail(
        "PROVIDER_TRANSIENT_FAILURE",
        exception.Message,
        canRetry: true,
        clock.UtcNow);
    await dbContext.SaveChangesAsync(cancellationToken);
    return;
}
catch (PayrollProviderContractException exception)
{
    batch.Fail(
        "PROVIDER_CONTRACT_FAILURE",
        exception.Message,
        canRetry: false,
        clock.UtcNow);
    await dbContext.SaveChangesAsync(cancellationToken);
    return;
}
```

**How to read the code.** One attempt:

1. `for (var attempt = 1; attempt <= _options.MaxAttempts; attempt++)` (line 77) is the bound. Each pass through the loop is one attempt.
2. `using var attemptTimeout = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);` (line 80) creates a cancellation source that is *linked* to the caller's token: when the caller cancels, this source is cancelled too. The next line, `attemptTimeout.CancelAfter(…)`, adds a second trigger, the time limit of the attempt (10 seconds with the default settings). The token of this source is cancelled by whichever comes first. `using var` disposes the source at the end of each pass.
3. `using var response = await httpClient.GetAsync(` (line 86) sends the request. `HttpCompletionOption.ResponseHeadersRead,` (line 88) makes the call complete as soon as the response headers have arrived, before the body has been read.
4. The body is read on line 95 by `ReadFromJsonAsync<ProviderTimeEntryPage>`, with the same token: `cancellationToken: attemptTimeout.Token);` (line 96). One time limit therefore covers the headers and the body together.

What happens with an answer:

5. `if (response.IsSuccessStatusCode)` (line 91) is the good case, a status from 200 to 299. The page is read and returned. A body that is not valid JSON makes the read throw a `JsonException`, and the inner `catch` wraps it in a `PayrollProviderContractException`.
6. `if (!IsTransient(response.StatusCode))` (line 106) is the permanent case. `IsTransient` is true for `408`, `429` and every status from `500` up. Any other status ends here with a `PayrollProviderContractException`. `var statusCode = (int)response.StatusCode;` (line 108) is a *cast*, which turns the enum value into its number for the message.
7. `if (attempt == _options.MaxAttempts)` (line 113) is a transient status with no attempts left. It ends with a `PayrollProviderTransientException`.
8. `retryDelay = GetRetryDelay(response, attempt);` (line 120) is what remains: a transient status with attempts left. `GetRetryDelay` first looks at the `Retry-After` response header, in which a server says how long the client should wait. When the header holds a number of seconds, the method uses it, up to a limit of one second: `? TimeSpan.FromMilliseconds(Math.Min(retryAfter.Value.TotalMilliseconds, 1_000))` (line 176). Otherwise it calls `GetBackoff`.
9. `TimeSpan.FromMilliseconds(_options.BaseRetryDelayMilliseconds * Math.Pow(2, attempt - 1));` (line 181) is *exponential backoff*: the wait doubles with every attempt. With the default base of 100 milliseconds it is 100 ms after the first attempt and 200 ms after the second.

What happens without an answer:

10. `catch (HttpRequestException exception) when (attempt < _options.MaxAttempts)` (line 122) handles a request that failed without a response, for example a refused connection, while attempts are left. It remembers the exception and sets the backoff delay. The `when` part is an exception filter ([Pattern 10](#pattern-10-background-worker-with-a-queue-and-a-recovery-scan)). When the filter is false, the clause is skipped and the next one is tried. The next one is the same `catch` without a filter, and it throws a `PayrollProviderTransientException`.
11. `when (!cancellationToken.IsCancellationRequested && attempt < _options.MaxAttempts)` (line 134) belongs to the `catch (OperationCanceledException exception)` on the line before it. `OperationCanceledException` is what a cancelled operation throws. `HttpClient` throws the derived type `TaskCanceledException`, which this clause catches as well. The filter looks at the caller's token. When that token is not cancelled, the cancellation came from the attempt's own time limit, because the timeout of `HttpClient` itself is switched off ([Pattern 11](#pattern-11-typed-httpclient-with-validated-options)). So this is a timeout with attempts left, and it is retried.
12. `catch (OperationCanceledException exception) when (!cancellationToken.IsCancellationRequested)` (line 139) is a timeout on the last attempt. It becomes a `PayrollProviderTransientException`.
13. When the caller's token is cancelled, neither filter is true and no clause catches the exception. It leaves the method unchanged. That is how a shutdown of the host stops an import ([Pattern 10](#pattern-10-background-worker-with-a-queue-and-a-recovery-scan)).
14. `await Task.Delay(retryDelay!.Value, cancellationToken);` (line 146) is the pause before the next attempt. It is outside the `try`, and only the paths that set `retryDelay` reach it. The `!` tells the compiler that the nullable variable has a value at this point. The pause takes the caller's token, so a shutdown also ends the waiting.
15. The `throw` after the loop is there for the compiler. With a `MaxAttempts` of 1 or more, the last attempt always ends in a `return` or a `throw`, so this line is not reached. The compiler cannot work that out, and C# does not accept a method that returns a value when its end can be reached. The validation of the options keeps `MaxAttempts` between 1 and 10 ([Pattern 11](#pattern-11-typed-httpclient-with-validated-options)).

The exceptions and the caller:

16. `public abstract class PayrollProviderException(` (line 28) is the common base. `abstract` means that no object of this exact type can be created. The two `sealed` classes below it are the two outcomes. All three have a primary constructor that passes the message and the optional *inner exception*, the original error, on to the base class.
17. The three classes are declared in the Application project, in the same file as the interface. That lets the processor write `catch (PayrollProviderTransientException exception)` (line 63) and `catch (PayrollProviderContractException exception)` (line 73) without knowing anything about HTTP.
18. Both clauses call `batch.Fail(…)` ([Pattern 17](#pattern-17-entities-as-state-machines-and-pure-domain-services)), save and return. They differ in the error code and in one named argument: `canRetry: true,` (line 68) for the transient failure and `canRetry: false,` (line 78) for the contract failure. A batch that failed with `canRetry: true` can be started again with `POST /api/import-batches/{batchId}/retry`.

The pieces around it:

- `GetTimeEntriesAsync` calls this method once for every page ([Infrastructure/FakePayrollProviderClient.cs:15-68](src/Northstar.Infrastructure/Integrations/FakePayrollProviderClient.cs#L15-L68)). It follows the `NextCursor` of each page and returns when a page has none. Four things are contract failures there: a response without a records collection, a null record, a cursor that was seen before, and more than 100 pages.
- The fake provider has a scenario for this code ([Pattern 21](#pattern-21-minimal-api-endpoints-in-the-fake-provider)). For the scenario `transient` it answers the first two attempts of a request with `429` and the header `Retry-After: 1`, and the third with the data ([FakeProvider/Program.cs:109-133](tools/Northstar.FakePayrollProvider/Program.cs#L109-L133)). With the default of three attempts, the client waits one second twice and then succeeds.
- The processor makes the provider call before it opens its transaction ([Application/ImportBatchProcessor.cs:120](src/Northstar.Application/Imports/ImportBatchProcessor.cs#L120)). No database transaction is open while the attempts and pauses run.
- The loop is written by hand. No retry library is referenced by any project.
- The client tests replace the network with a small message handler of their own, `StubHttpMessageHandler`. It returns the status codes and bodies that a test chooses and counts the calls ([Pattern 20](#pattern-20-tests-at-three-levels-in-net)).

**Why it is done this way.** The README states the rule: "Provider timeouts, throttling, connection failures, and eligible server failures are retried with a bound; exhausted transient work can be retried through the API." The architecture note records two decisions that shape this code: "Provider I/O never occurs while the promotion transaction is open." ([docs/architecture.md:52](docs/architecture.md#L52)) and "Provider payloads stay outside the domain." ([docs/architecture.md:39](docs/architecture.md#L39)). The testing note says what the client tests do with the settings: they "control HTTP status, bodies, and cancellation to prove retry and exception classification without real waits." ([docs/testing-strategy.md:7](docs/testing-strategy.md#L7)). The test helper sets the base delay to 0 for that reason. Microsoft's cloud design guide describes the general idea as the Retry pattern: retry a failure that is likely to pass, wait longer each time, and give up after a fixed number of attempts. Its guidance on transient faults draws the same line between failures that are worth retrying and failures that are not, which is the line between the two exception types here.

**Look up.** [`CancellationTokenSource.CreateLinkedTokenSource`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtokensource.createlinkedtokensource?view=net-10.0) · [`CancellationTokenSource.CancelAfter`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtokensource.cancelafter?view=net-10.0#system-threading-cancellationtokensource-cancelafter(system-timespan)) · [`HttpClient.GetAsync`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient.getasync?view=net-10.0#system-net-http-httpclient-getasync(system-uri)) · [`HttpCompletionOption.ResponseHeadersRead`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpcompletionoption?view=net-10.0) · [`HttpContentJsonExtensions.ReadFromJsonAsync`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.json.httpcontentjsonextensions.readfromjsonasync?view=net-10.0#system-net-http-json-httpcontentjsonextensions-readfromjsonasync-1(system-net-http-httpcontent-system-threading-cancellationtoken)) · [`HttpResponseMessage.IsSuccessStatusCode`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpresponsemessage.issuccessstatuscode?view=net-10.0#system-net-http-httpresponsemessage-issuccessstatuscode) · [`HttpResponseHeaders.RetryAfter`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.headers.httpresponseheaders.retryafter?view=net-10.0#system-net-http-headers-httpresponseheaders-retryafter) · [`RetryConditionHeaderValue.Delta`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.headers.retryconditionheadervalue.delta?view=net-10.0#system-net-http-headers-retryconditionheadervalue-delta) · [`HttpRequestException`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httprequestexception?view=net-10.0) · [`OperationCanceledException`](https://learn.microsoft.com/en-us/dotnet/api/system.operationcanceledexception?view=net-10.0) · [`JsonException`](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.jsonexception?view=net-10.0) · [`Task.Delay`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.delay?view=net-10.0#system-threading-tasks-task-delay(system-timespan-system-threading-cancellationtoken)) · [`Math.Pow`](https://learn.microsoft.com/en-us/dotnet/api/system.math.pow?view=net-10.0#system-math-pow(system-double-system-double)) · [`Math.Min`](https://learn.microsoft.com/en-us/dotnet/api/system.math.min?view=net-10.0#system-math-min(system-int32-system-int32)) · [`HttpStatusCode.TooManyRequests`](https://learn.microsoft.com/en-us/dotnet/api/system.net.httpstatuscode?view=net-10.0) · [Exception filter `catch (X) when (...)`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/exception-handling-statements#a-when-exception-filter) · [`catch (SpecificException)`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/exception-handling-statements#the-try-catch-statement) · [`using` declaration](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/using) · [`or` pattern](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/patterns#logical-patterns) · [Cast `(T)value`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/type-testing-and-cast#cast-expression) · [Null-forgiving `!`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/null-forgiving) · [Named argument `name: value`](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/named-and-optional-arguments#named-arguments) · [`abstract` class](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/abstract)

**Read more.** [Retry pattern](https://learn.microsoft.com/en-us/azure/architecture/patterns/retry) · [Transient fault handling](https://learn.microsoft.com/en-us/azure/architecture/best-practices/transient-faults) · [Listen for multiple cancellation requests](https://learn.microsoft.com/en-us/dotnet/standard/threading/how-to-listen-for-multiple-cancellation-requests) · [Create user-defined exceptions](https://learn.microsoft.com/en-us/dotnet/standard/exceptions/how-to-create-user-defined-exceptions) · [Build resilient HTTP apps](https://learn.microsoft.com/en-us/dotnet/core/resilience/http-resilience) · [429 Too Many Requests (MDN)](https://developer.mozilla.org/en-US/docs/Web/HTTP/Reference/Status/429) · [Retry-After header (MDN)](https://developer.mozilla.org/en-US/docs/Web/HTTP/Reference/Headers/Retry-After)

### Pattern 13: Persistence behind an interface

**What it is.** The Application project needs a database, but it may not reference the Infrastructure project, where the database code lives ([Pattern 1](#pattern-1-layered-modular-monolith)). So the Application project declares an interface with exactly the operations its services use, and the Infrastructure project implements that interface with the EF Core context. The layer with the rules owns the contract, and the layer with the technology fulfils it. This is called *dependency inversion*.

**Where to find it.** [Application/IApplicationDbContext.cs](src/Northstar.Application/Abstractions/IApplicationDbContext.cs) and [Infrastructure/ApplicationDbContext.cs:15-49](src/Northstar.Infrastructure/Persistence/ApplicationDbContext.cs#L15-L49), with the transaction wrapper at [Infrastructure/ApplicationDbContext.cs:257-264](src/Northstar.Infrastructure/Persistence/ApplicationDbContext.cs#L257-L264).

**[`src/Northstar.Application/Abstractions/IApplicationDbContext.cs`](src/Northstar.Application/Abstractions/IApplicationDbContext.cs#L12-L37)** · lines 12–37

<!-- x:dbctx-interface -->
```csharp
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
```

**[`src/Northstar.Infrastructure/Persistence/ApplicationDbContext.cs`](src/Northstar.Infrastructure/Persistence/ApplicationDbContext.cs#L15-L19)** · lines 15–19, 31–32 and 44–49

<!-- x:dbctx-impl -->
```csharp
public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : DbContext(options), IApplicationDbContext
{
    public DbSet<Organization> Organizations => Set<Organization>();
    public DbSet<Employee> Employees => Set<Employee>();
    // ... lines 20–30 omitted ...
    IQueryable<Organization> IApplicationDbContext.Organizations => Organizations;
    IQueryable<Employee> IApplicationDbContext.Employees => Employees;
    // ... lines 33–43 omitted ...
    public new void Add<TEntity>(TEntity entity) where TEntity : class => Set<TEntity>().Add(entity);

    public void ClearTracking() => ChangeTracker.Clear();

    public async Task<IApplicationTransaction> BeginTransactionAsync(CancellationToken cancellationToken) =>
        new EfApplicationTransaction(await Database.BeginTransactionAsync(cancellationToken));
        // ... continues in the file ...
```

**[`src/Northstar.Infrastructure/Persistence/ApplicationDbContext.cs`](src/Northstar.Infrastructure/Persistence/ApplicationDbContext.cs#L257-L264)** · lines 257–264

<!-- x:dbctx-tx -->
```csharp
private sealed class EfApplicationTransaction(IDbContextTransaction transaction) : IApplicationTransaction
{
    public Task CommitAsync(CancellationToken cancellationToken) => transaction.CommitAsync(cancellationToken);

    public Task RollbackAsync(CancellationToken cancellationToken) => transaction.RollbackAsync(cancellationToken);

    public ValueTask DisposeAsync() => transaction.DisposeAsync();
}
```

**How to read the code.** The interface:

1. `IQueryable<Organization> Organizations` (line 20) is the first of twelve read-only properties, one for each entity type. An `IQueryable<T>` is a query that has not run yet. A service adds `Where`, `Select` and other operators to it and then runs it ([Pattern 15](#pattern-15-read-queries-with-no-tracking-a-tenant-filter-and-projection)).
2. `void Add<TEntity>(TEntity entity) where TEntity : class;` (line 33) is a *generic method*: one method that serves every entity type. `where TEntity : class` is a *constraint* that only allows reference types. The method tells the context about a new object, and the object is inserted at the next save.
3. `Task<int> SaveChangesAsync(CancellationToken cancellationToken);` (line 35) writes all pending changes to the database. The number it returns is the number of entries written.
4. `Task<IApplicationTransaction> BeginTransactionAsync(CancellationToken cancellationToken);` (line 36) starts a database transaction. It returns the small interface at the top of the file, `public interface IApplicationTransaction : IAsyncDisposable` (line 12), which can be committed, rolled back, and disposed with `await using`.
5. No member deletes or updates. Existing rows are changed through *change tracking*. A query that is not marked `AsNoTracking` returns objects that the context remembers. The code changes those objects through their own methods ([Pattern 17](#pattern-17-entities-as-state-machines-and-pure-domain-services)), and `SaveChangesAsync` compares them with what was loaded and writes the differences.

The implementation:

6. `: DbContext(options), IApplicationDbContext` (line 16) says two things. The class derives from EF Core's `DbContext` and passes its options on to it, and it implements the application's interface.
7. `public DbSet<Organization> Organizations => Set<Organization>();` (line 18) is the usual EF Core property for one entity type, written as an expression-bodied property that asks the base class for the set. A `DbSet<T>` stands for all rows of that type. It can be queried, and it also has members for adding and removing.
8. `IQueryable<Organization> IApplicationDbContext.Organizations => Organizations;` (line 31) is an *explicit interface implementation*: the name carries the interface as a prefix, and there is no access modifier. It is needed because the interface asks for a property `Organizations` of type `IQueryable<Organization>`, and the public property of that name has the type `DbSet<Organization>`. A public member only implements an interface member when its type is exactly the same, and a class cannot have two properties with one name. The explicit member is visible only through the interface. It returns the public property, which works because `DbSet<T>` implements `IQueryable<T>`.
9. `public new void Add<TEntity>(TEntity entity) where TEntity : class => Set<TEntity>().Add(entity);` (line 44) implements the interface's `Add`. `DbContext` already has a public `Add` method with the same parameter, but that one returns an `EntityEntry<TEntity>` and so does not match the `void` of the interface. The class declares its own method, and the `new` modifier states that hiding the inherited one is intended.
10. `SaveChangesAsync` does not appear in the class at all. The interface member is implemented by the public method that the class inherits from `DbContext`. The only member the class overrides is `OnModelCreating` ([Pattern 14](#pattern-14-ef-core-model-configuration-with-the-fluent-api)).
11. `public void ClearTracking() => ChangeTracker.Clear();` (line 46) makes the context forget every object it is tracking. The import service calls it after a save has failed and before it queries again ([Application/ImportBatchService.cs:172](src/Northstar.Application/Imports/ImportBatchService.cs#L172) and [Application/ImportBatchService.cs:262](src/Northstar.Application/Imports/ImportBatchService.cs#L262)).
12. `new EfApplicationTransaction(await Database.BeginTransactionAsync(cancellationToken));` (line 49) starts an EF Core transaction and wraps it. `private sealed class EfApplicationTransaction(IDbContextTransaction transaction) : IApplicationTransaction` (line 257) is a *nested class*. It is declared inside the context class, and because it is `private`, nothing outside that class can name it. Its three one-line methods pass each call on to the EF Core transaction.

The pieces around it:

- Both names lead to the same object within one scope, because the interface is registered with a function that returns the scope's `ApplicationDbContext` ([Pattern 2](#pattern-2-composition-root-and-service-registration), step 13).
- Code that holds the interface sees `IQueryable<T>` and the four methods. Code that holds the class sees the full `DbSet<T>` properties. The class is asked for at startup ([Api/Program.cs:41](src/Northstar.Api/Program.cs#L41)), by the seeding class `DatabaseInitializer`, and by the API tests.
- The interface does not hide EF Core completely. The Application project references the `Microsoft.EntityFrameworkCore` package, and four of its files import that namespace for the asynchronous query operators such as `ToListAsync`. What the Application project cannot see is the context class, the mapping ([Pattern 14](#pattern-14-ef-core-model-configuration-with-the-fluent-api)) and the choice of database provider.
- There are no repository classes, the common alternative in which each entity type gets a class with methods such as `GetByIdAsync`. The services build their queries directly on the `IQueryable<T>` properties.
- Four methods open a transaction, each with `await using var transaction = await dbContext.BeginTransactionAsync(cancellationToken);`. They are creating an import ([Application/ImportBatchService.cs:125](src/Northstar.Application/Imports/ImportBatchService.cs#L125)), retrying one ([Application/ImportBatchService.cs:209](src/Northstar.Application/Imports/ImportBatchService.cs#L209)), calculating a payroll run ([Application/PayrollRunService.cs:66](src/Northstar.Application/PayrollRuns/PayrollRunService.cs#L66)) and promoting imported rows ([Application/ImportBatchProcessor.cs:120](src/Northstar.Application/Imports/ImportBatchProcessor.cs#L120)). Each saves and then calls `CommitAsync`. When a method leaves before the commit, `await using` disposes the transaction. The EF Core documentation notes that a transaction that is disposed without a commit is rolled back.
- Child rows are replaced without a delete method. When a payroll run is recalculated, the entity empties its two collections with `Lines.Clear()` and `Findings.Clear()` and fills them again ([Domain/PayrollRun.cs:58-59](src/Northstar.Domain/PayrollRuns/PayrollRun.cs#L58-L59)). The run was loaded together with its lines and findings and is tracked, so EF Core sees that the old objects no longer belong to it. Both relationships are required and are configured with cascade delete ([Infrastructure/ApplicationDbContext.cs:169-176](src/Northstar.Infrastructure/Persistence/ApplicationDbContext.cs#L169-L176)). For that case the documentation says that the removed children, which it calls *orphans*, are deleted at the next save.

**Why it is done this way.** The architecture note gives each project its part: "`Northstar.Application` depends on Domain and owns use cases and persistence interfaces." and "`Northstar.Infrastructure` implements persistence and seed data." ([docs/architecture.md:17-18](docs/architecture.md#L17-L18)). The README says the same from the other side: the Application project owns "persistence contracts" ([README.md:112](README.md#L112)). Microsoft's architecture guide explains the principle behind it. The application core defines the abstractions, infrastructure code implements them, and the project references therefore point toward the core. The interface is also a short list of what the application does with its database: query, add, save, and open a transaction.

**Look up.** [`DbContext`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.dbcontext?view=efcore-10.0) · [`DbSet`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.dbset-1?view=efcore-10.0) · [`DbContext.Set`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.dbcontext.set?view=efcore-10.0#microsoft-entityframeworkcore-dbcontext-set-1) · [`IQueryable`](https://learn.microsoft.com/en-us/dotnet/api/system.linq.iqueryable-1?view=net-10.0) · [`DbSet.Add`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.dbset-1.add?view=efcore-10.0#microsoft-entityframeworkcore-dbset-1-add(-0)) · [`DbContext.SaveChangesAsync`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.dbcontext.savechangesasync?view=efcore-10.0#microsoft-entityframeworkcore-dbcontext-savechangesasync(system-threading-cancellationtoken)) · [`DbContext.ChangeTracker`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.dbcontext.changetracker?view=efcore-10.0#microsoft-entityframeworkcore-dbcontext-changetracker) · [`ChangeTracker.Clear`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.changetracking.changetracker.clear?view=efcore-10.0#microsoft-entityframeworkcore-changetracking-changetracker-clear) · [`DbContext.Database`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.dbcontext.database?view=efcore-10.0#microsoft-entityframeworkcore-dbcontext-database) · [`DatabaseFacade.BeginTransactionAsync`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.infrastructure.databasefacade.begintransactionasync?view=efcore-10.0#microsoft-entityframeworkcore-infrastructure-databasefacade-begintransactionasync(system-threading-cancellationtoken)) · [`IDbContextTransaction`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.storage.idbcontexttransaction?view=efcore-10.0) · [`IDbContextTransaction.CommitAsync`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.storage.idbcontexttransaction.commitasync?view=efcore-10.0#microsoft-entityframeworkcore-storage-idbcontexttransaction-commitasync(system-threading-cancellationtoken)) · [`IDbContextTransaction.RollbackAsync`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.storage.idbcontexttransaction.rollbackasync?view=efcore-10.0#microsoft-entityframeworkcore-storage-idbcontexttransaction-rollbackasync(system-threading-cancellationtoken)) · [`IAsyncDisposable`](https://learn.microsoft.com/en-us/dotnet/api/system.iasyncdisposable?view=net-10.0) · [`DbContextOptions`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.dbcontextoptions-1?view=efcore-10.0) · [`interface`](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/types/interfaces) · [Generic method `M<T>()`](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/generics/generic-methods) · [Generic constraint `where T : class`](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/generics/constraints-on-type-parameters) · [Expression-bodied member `=>`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/lambda-operator#expression-body-definition) · [Primary constructor `class C(Dep dep)`](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/instance-constructors#primary-constructors) · [`await using`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/using)

**Read more.** [Dependency inversion principle](https://learn.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/architectural-principles#dependency-inversion) · [Design the infrastructure persistence layer](https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/infrastructure-persistence-layer-design) · [Explicit interface implementation](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/interfaces/explicit-interface-implementation) · [The new modifier (hiding an inherited member)](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/new-modifier) · [DbContext lifetime, configuration and initialization](https://learn.microsoft.com/en-us/ef/core/dbcontext-configuration/) · [Change tracking in EF Core](https://learn.microsoft.com/en-us/ef/core/change-tracking/) · [Using transactions](https://learn.microsoft.com/en-us/ef/core/saving/transactions) · [Cascade delete](https://learn.microsoft.com/en-us/ef/core/saving/cascade-delete)

### Pattern 14: EF Core model configuration with the Fluent API

**What it is.** EF Core needs a *model*: a description of which class belongs to which table, which property is the key, how long a text column is, which values must be unique, and how the tables refer to each other. The model has three sources. *Conventions* are what EF Core works out from the classes by itself. Attributes can be put on the entity classes. The *Fluent API* is a set of methods that are called in chains inside the context. This codebase uses the Fluent API for everything it wants to state, and the entity classes in the Domain project carry no attributes at all.

**Where to find it.** [Infrastructure/ApplicationDbContext.cs:51-255](src/Northstar.Infrastructure/Persistence/ApplicationDbContext.cs#L51-L255): `OnModelCreating` and the twelve `Configure…` methods that it calls, one for each entity type.

**[`src/Northstar.Infrastructure/Persistence/ApplicationDbContext.cs`](src/Northstar.Infrastructure/Persistence/ApplicationDbContext.cs#L114-L138)** · lines 114–138

<!-- x:ef-wagerate -->
```csharp
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
```

**[`src/Northstar.Infrastructure/Persistence/ApplicationDbContext.cs`](src/Northstar.Infrastructure/Persistence/ApplicationDbContext.cs#L140-L161)** · lines 140–161

<!-- x:ef-timeentry -->
```csharp
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
```

**[`src/Northstar.Infrastructure/Persistence/ApplicationDbContext.cs`](src/Northstar.Infrastructure/Persistence/ApplicationDbContext.cs#L217-L237)** · lines 217–237

<!-- x:ef-importbatch -->
```csharp
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
```

**How to read the code.** Properties and columns, in `ConfigureWageRate`:

1. `var entity = modelBuilder.Entity<WageRate>();` (line 116) asks for the builder of one entity type. Every call after it adds a fact about that type.
2. `entity.HasKey(value => value.Id);` (line 117) names the primary key. The lambda `value => value.Id` is not run. EF Core reads it to find out which property is meant, and the compiler checks that the property exists.
3. `entity.Property(value => value.Scope).HasConversion<string>().HasMaxLength(40);` (line 118) stores an enum as text. `Scope` is an enum in C#. `HasConversion<string>()` writes its name to the database and not its number, and `HasMaxLength(40)` limits the column to 40 characters.
4. `entity.Property(value => value.JurisdictionCode).HasMaxLength(20).IsRequired();` (line 119) is the usual line for a text property: a maximum length, and `IsRequired()`, which makes the column refuse null.
5. `entity.Property(value => value.BaseRate).HasPrecision(18, 4);` (line 120) sets the size of a `decimal` column: 18 digits in total, 4 of them after the decimal point.
6. `entity.Property(value => value.Version).IsConcurrencyToken();` (line 124) marks the property that protects a row against lost updates ([Pattern 16](#pattern-16-optimistic-concurrency-with-a-version-number)).
7. `table.HasCheckConstraint("CK_WageRates_Amounts", "BaseRate >= 0 AND FringeRate >= 0 AND OvertimeMultiplier >= 1");` (line 128) adds a *check constraint*: a condition, written in SQL, that the database tests for every row it stores. The first argument is the name of the constraint. The call sits inside `ToTable(table => …)` because a check constraint is a setting of the table.
8. `entity.HasIndex(value => new` (line 130) creates an index over five columns. The anonymous type lists them, and their order is the order of the columns in the index.

A value converter and a unique index, in `ConfigureTimeEntry`:

9. `.HasConversion(` (line 145) with two lambdas is a *value converter* written by hand. The first lambda runs when a value is saved, and the second when it is read. Both are `value => value.Trim().ToUpperInvariant(),` (line 146), so an identifier is stored in upper case and without spaces around it.
10. `entity.HasIndex(value => new { value.OrganizationId, value.ExternalTimeEntryId }).IsUnique();` (line 155) makes the pair unique. The database refuses a second time entry with the same external identifier in the same organization. The index works on the stored values, so two identifiers that differ only in letter case or in surrounding spaces count as the same one.
11. `entity.HasOne<ImportBatch>()` (line 157) starts a relationship: a time entry can point to the import batch that created it. `WithMany()` adds that one batch can have many time entries. Neither class has a property that leads to the other, which is why both calls are written without a lambda. Only the foreign key property exists: `.HasForeignKey(value => value.ImportedInBatchId)` (line 159). Its type is `Guid?`, so a time entry without a batch is allowed.
12. `.OnDelete(DeleteBehavior.SetNull);` (line 160) decides what happens to the time entries when their batch is deleted. The foreign key is set to null and the time entries stay.

Keys and a relationship, in `ConfigureImportBatch`:

13. `entity.HasAlternateKey(value => new { value.OrganizationId, value.Id });` (line 226) declares an *alternate key*: a second set of columns that identifies a row, next to the primary key. A foreign key has to point at a key, and the next step needs a key that includes the organization.
14. `entity.HasMany(value => value.Rows)` (line 227) is the relationship from a batch to its rows. The foreign key has two columns, `.HasForeignKey(value => new { value.OrganizationId, value.ImportBatchId })` (line 229), and it points at the alternate key: `.HasPrincipalKey(value => new { value.OrganizationId, value.Id })` (line 230). The pair of values in a row must exist as a pair in the batch table. A row can therefore not belong to a batch of another organization, and it is the database that refuses it ([Pattern 7](#pattern-7-tenant-scoping-from-header-to-query)).
15. `.OnDelete(DeleteBehavior.Cascade);` (line 231) deletes the rows together with their batch.

The pieces around it:

- `OnModelCreating` is the only method that the context class overrides ([Infrastructure/ApplicationDbContext.cs:51-65](src/Northstar.Infrastructure/Persistence/ApplicationDbContext.cs#L51-L65)). EF Core calls it when it builds the model. The method does nothing but call the twelve private static methods. The codebase does not use `IEntityTypeConfiguration<T>`, EF Core's interface for putting the configuration of each entity into a class of its own.
- In numbers, the twelve methods hold 64 `Property` calls, 42 `HasMaxLength` calls, 27 `IsRequired` calls, 16 `HasPrecision` calls, 13 indexes and 4 check constraints.
- The precision follows the kind of number. Rates are `(18, 4)`, the overtime multiplier is `(8, 4)`, hours are `(9, 2)` and money amounts are `(18, 2)`.
- `HasConversion<string>()` appears five times: for the `Scope` of a wage rate, the `Status` of payroll runs, import batches and import rows, and the `Severity` of findings.
- 7 of the indexes are unique. Six of them begin with `OrganizationId`, so the uniqueness holds inside one organization ([Pattern 7](#pattern-7-tenant-scoping-from-header-to-query)). One of the six is the idempotency key ([Pattern 8](#pattern-8-idempotent-commands-with-an-idempotency-key)). The seventh is the `ExternalReference` of the organization itself.
- The import processor uses the same expression in memory, in `NormalizeExternalTimeEntryId` ([Application/ImportBatchProcessor.cs:326-327](src/Northstar.Application/Imports/ImportBatchProcessor.cs#L326-L327)), to find duplicates before it saves.
- `ConfigurePayrollRun` sets up the two relationships with cascade delete that [Pattern 13](#pattern-13-persistence-behind-an-interface) relies on when it replaces lines and findings.
- The database schema is created from this model at startup by `EnsureCreatedAsync` ([Pattern 4](#pattern-4-request-pipeline-and-host-startup)). The repository has no migration files.

**Why it is done this way.** The architecture note requires that "`Northstar.Domain` has no project or framework dependencies." ([docs/architecture.md:16](docs/architecture.md#L16)). The EF Core documentation describes the Fluent API as the way to configure a model without changing the entity classes, and as the source with the highest precedence. The mapping can therefore live entirely in the Infrastructure project. The unique indexes and check constraints put rules into the database itself, where they hold for every code path that writes a row. The repository's documents lean on that. The README says that duplicate identifiers "cannot create duplicate payroll input" ([README.md:60](README.md#L60)). The architecture note adds: "The database uniquely constrains `(OrganizationId, ExternalTimeEntryId)`, while staging keeps duplicate and invalid rows visible without silently coercing missing fields or hours." ([docs/architecture.md:39](docs/architecture.md#L39)). How a setting such as precision is carried out depends on the database provider, and the same note says which database counts for that: "SQL Server is selectable with configuration and remains the required evidence boundary for provider-specific locking, precision, and concurrency exercises." ([docs/architecture.md:22](docs/architecture.md#L22)).

**Look up.** [`DbContext.OnModelCreating`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.dbcontext.onmodelcreating?view=efcore-10.0#microsoft-entityframeworkcore-dbcontext-onmodelcreating(microsoft-entityframeworkcore-modelbuilder)) · [`ModelBuilder.Entity`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.modelbuilder.entity?view=efcore-10.0#microsoft-entityframeworkcore-modelbuilder-entity-1) · [`EntityTypeBuilder.HasKey`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.metadata.builders.entitytypebuilder-1.haskey?view=efcore-10.0#microsoft-entityframeworkcore-metadata-builders-entitytypebuilder-1-haskey(system-linq-expressions-expression((system-func((-0-system-object)))))) · [`EntityTypeBuilder.Property`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.metadata.builders.entitytypebuilder-1.property?view=efcore-10.0#microsoft-entityframeworkcore-metadata-builders-entitytypebuilder-1-property-1(system-linq-expressions-expression((system-func((-0-0)))))) · [`PropertyBuilder.HasMaxLength`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.metadata.builders.propertybuilder-1.hasmaxlength?view=efcore-10.0#microsoft-entityframeworkcore-metadata-builders-propertybuilder-1-hasmaxlength(system-int32)) · [`PropertyBuilder.IsRequired`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.metadata.builders.propertybuilder-1.isrequired?view=efcore-10.0#microsoft-entityframeworkcore-metadata-builders-propertybuilder-1-isrequired(system-boolean)) · [`PropertyBuilder.HasPrecision`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.metadata.builders.propertybuilder-1.hasprecision?view=efcore-10.0#microsoft-entityframeworkcore-metadata-builders-propertybuilder-1-hasprecision(system-int32-system-int32)) · [`PropertyBuilder.HasConversion`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.metadata.builders.propertybuilder-1.hasconversion?view=efcore-10.0#microsoft-entityframeworkcore-metadata-builders-propertybuilder-1-hasconversion-1) · [`PropertyBuilder.IsConcurrencyToken`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.metadata.builders.propertybuilder-1.isconcurrencytoken?view=efcore-10.0#microsoft-entityframeworkcore-metadata-builders-propertybuilder-1-isconcurrencytoken(system-boolean)) · [`EntityTypeBuilder.HasIndex`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.metadata.builders.entitytypebuilder-1.hasindex?view=efcore-10.0#microsoft-entityframeworkcore-metadata-builders-entitytypebuilder-1-hasindex(system-linq-expressions-expression((system-func((-0-system-object)))))) · [`IndexBuilder.IsUnique`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.metadata.builders.indexbuilder-1.isunique?view=efcore-10.0#microsoft-entityframeworkcore-metadata-builders-indexbuilder-1-isunique(system-boolean)) · [`RelationalEntityTypeBuilderExtensions.ToTable`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.relationalentitytypebuilderextensions.totable?view=efcore-10.0#microsoft-entityframeworkcore-relationalentitytypebuilderextensions-totable-1(microsoft-entityframeworkcore-metadata-builders-entitytypebuilder((-0))-system-action((microsoft-entityframeworkcore-metadata-builders-tablebuilder((-0)))))) · [`TableBuilder.HasCheckConstraint`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.metadata.builders.tablebuilder.hascheckconstraint?view=efcore-10.0#microsoft-entityframeworkcore-metadata-builders-tablebuilder-hascheckconstraint(system-string-system-string)) · [`EntityTypeBuilder.HasAlternateKey`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.metadata.builders.entitytypebuilder-1.hasalternatekey?view=efcore-10.0#microsoft-entityframeworkcore-metadata-builders-entitytypebuilder-1-hasalternatekey(system-linq-expressions-expression((system-func((-0-system-object)))))) · [`EntityTypeBuilder.HasMany`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.metadata.builders.entitytypebuilder-1.hasmany?view=efcore-10.0#microsoft-entityframeworkcore-metadata-builders-entitytypebuilder-1-hasmany-1(system-linq-expressions-expression((system-func((-0-system-collections-generic-ienumerable((-0)))))))) · [`CollectionNavigationBuilder.WithOne`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.metadata.builders.collectionnavigationbuilder-2.withone?view=efcore-10.0#microsoft-entityframeworkcore-metadata-builders-collectionnavigationbuilder-2-withone(system-string)) · [`EntityTypeBuilder.HasOne`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.metadata.builders.entitytypebuilder-1.hasone?view=efcore-10.0#microsoft-entityframeworkcore-metadata-builders-entitytypebuilder-1-hasone-1(system-linq-expressions-expression((system-func((-0-0)))))) · [`ReferenceNavigationBuilder.WithMany`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.metadata.builders.referencenavigationbuilder-2.withmany?view=efcore-10.0#microsoft-entityframeworkcore-metadata-builders-referencenavigationbuilder-2-withmany(system-string)) · [`ReferenceCollectionBuilder.HasForeignKey`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.metadata.builders.referencecollectionbuilder-2.hasforeignkey?view=efcore-10.0#microsoft-entityframeworkcore-metadata-builders-referencecollectionbuilder-2-hasforeignkey(system-linq-expressions-expression((system-func((-1-system-object)))))) · [`ReferenceCollectionBuilder.HasPrincipalKey`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.metadata.builders.referencecollectionbuilder-2.hasprincipalkey?view=efcore-10.0#microsoft-entityframeworkcore-metadata-builders-referencecollectionbuilder-2-hasprincipalkey(system-linq-expressions-expression((system-func((-0-system-object)))))) · [`ReferenceCollectionBuilder.OnDelete`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.metadata.builders.referencecollectionbuilder-2.ondelete?view=efcore-10.0#microsoft-entityframeworkcore-metadata-builders-referencecollectionbuilder-2-ondelete(microsoft-entityframeworkcore-deletebehavior)) · [`DeleteBehavior.Cascade`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.deletebehavior?view=efcore-10.0) · [`DeleteBehavior.SetNull`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.deletebehavior?view=efcore-10.0) · [Lambda `x => ...`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/lambda-expressions) · [Anonymous type `new { ... }`](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/anonymous-types) · [`override`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/override)

**Read more.** [Creating and configuring a model](https://learn.microsoft.com/en-us/ef/core/modeling/) · [Use fluent API to configure a model](https://learn.microsoft.com/en-us/ef/core/modeling/#use-fluent-api-to-configure-a-model) · [Keys](https://learn.microsoft.com/en-us/ef/core/modeling/keys) · [Entity properties: maximum length](https://learn.microsoft.com/en-us/ef/core/modeling/entity-properties#maximum-length) · [Entity properties: required and optional properties](https://learn.microsoft.com/en-us/ef/core/modeling/entity-properties#required-and-optional-properties) · [Entity properties: precision and scale](https://learn.microsoft.com/en-us/ef/core/modeling/entity-properties#precision-and-scale) · [Value conversions](https://learn.microsoft.com/en-us/ef/core/modeling/value-conversions) · [Value conversions: pre-defined conversions](https://learn.microsoft.com/en-us/ef/core/modeling/value-conversions#pre-defined-conversions) · [Indexes: index uniqueness](https://learn.microsoft.com/en-us/ef/core/modeling/indexes#index-uniqueness) · [Indexes: composite index](https://learn.microsoft.com/en-us/ef/core/modeling/indexes#composite-index) · [Check constraints](https://learn.microsoft.com/en-us/ef/core/modeling/indexes#check-constraints) · [Alternate keys](https://learn.microsoft.com/en-us/ef/core/modeling/keys#alternate-keys) · [One-to-many relationships](https://learn.microsoft.com/en-us/ef/core/modeling/relationships/one-to-many) · [Cascade delete](https://learn.microsoft.com/en-us/ef/core/saving/cascade-delete)

### Pattern 15: Read queries with no tracking, a tenant filter and projection

**What it is.** The queries that only read data share one shape. They start from a property of the context, switch off change tracking, filter by organization, pick the values that the caller needs (a *projection*), and run asynchronously. They are written in *LINQ*, the query operators of C#, and EF Core turns them into SQL. Nothing is sent to the database while the query is being put together. It runs when a method such as `ToListAsync` is awaited.

**Where to find it.** `ListAsync` in [Application/ImportBatchService.cs:26-56](src/Northstar.Application/Imports/ImportBatchService.cs#L26-L56), the query for the calculation in [Application/PayrollRunService.cs:104-131](src/Northstar.Application/PayrollRuns/PayrollRunService.cs#L104-L131) and `LoadAsync` in [Application/PayrollRunService.cs:216-233](src/Northstar.Application/PayrollRuns/PayrollRunService.cs#L216-L233).

**[`src/Northstar.Application/Imports/ImportBatchService.cs`](src/Northstar.Application/Imports/ImportBatchService.cs#L26-L56)** · lines 26–56

<!-- x:query-list -->
```csharp
public async Task<IReadOnlyList<ImportBatchView>> ListAsync(
    Guid organizationId,
    CancellationToken cancellationToken)
{
    var batches = await dbContext.ImportBatches
        .AsNoTracking()
        .Where(batch => batch.OrganizationId == organizationId)
        .Select(batch => new ImportBatchView(
            batch.Id,
            batch.OrganizationId,
            batch.PeriodStart,
            batch.PeriodEnd,
            batch.ProviderScenario,
            batch.Status.ToString(),
            batch.TotalRows,
            batch.ImportedRows,
            batch.ErrorRows,
            batch.DuplicateRows,
            batch.AttemptCount,
            batch.FailureCode,
            batch.FailureMessage,
            batch.CanRetry,
            batch.CreatedAt,
            batch.CompletedAt,
            batch.Version))
        .ToListAsync(cancellationToken);

    // SQLite cannot order DateTimeOffset values. Keep tenant filtering and
    // projection in SQL, then order this intentionally small study history.
    return batches.OrderByDescending(batch => batch.CreatedAt).ToList();
}
```

**[`src/Northstar.Application/PayrollRuns/PayrollRunService.cs`](src/Northstar.Application/PayrollRuns/PayrollRunService.cs#L104-L131)** · lines 104–131

<!-- x:query-join -->
```csharp
var segments = await (
    from entry in dbContext.TimeEntries.AsNoTracking()
    join employee in dbContext.Employees.AsNoTracking() on entry.EmployeeId equals employee.Id
    join project in dbContext.Projects.AsNoTracking() on entry.ProjectId equals project.Id
    join classification in dbContext.WorkClassifications.AsNoTracking() on entry.ClassificationId equals classification.Id
    where entry.OrganizationId == organizationId &&
        employee.OrganizationId == organizationId &&
        project.OrganizationId == organizationId &&
        classification.OrganizationId == organizationId &&
        entry.WorkDate >= run.PeriodStart &&
        entry.WorkDate <= run.PeriodEnd
    orderby entry.WorkDate, employee.DisplayName, classification.Name
    select new PayrollWorkSegment(
        entry.Id,
        entry.OrganizationId,
        employee.Id,
        employee.DisplayName,
        employee.BaseHourlyRate,
        employee.ActualFringeRate,
        project.Id,
        project.Name,
        project.JurisdictionCode,
        classification.Id,
        classification.Name,
        entry.WorkDate,
        entry.RegularHours,
        entry.OvertimeHours))
    .ToListAsync(cancellationToken);
```

**[`src/Northstar.Application/PayrollRuns/PayrollRunService.cs`](src/Northstar.Application/PayrollRuns/PayrollRunService.cs#L216-L233)** · lines 216–233

<!-- x:query-load -->
```csharp
private async Task<PayrollRun?> LoadAsync(
    Guid organizationId,
    Guid runId,
    bool track,
    CancellationToken cancellationToken)
{
    IQueryable<PayrollRun> query = dbContext.PayrollRuns
        .Where(run => run.OrganizationId == organizationId && run.Id == runId)
        .Include(run => run.Lines)
        .Include(run => run.Findings);

    if (!track)
    {
        query = query.AsNoTracking();
    }

    return await query.SingleOrDefaultAsync(cancellationToken);
}
```

**How to read the code.** A list query in method syntax:

1. `var batches = await dbContext.ImportBatches` (line 30) starts from the `IQueryable<ImportBatch>` of the context ([Pattern 13](#pattern-13-persistence-behind-an-interface)). Each method that follows returns a new query with one more step.
2. `.AsNoTracking()` (line 31) tells EF Core not to remember the objects that this query returns. Tracking is what makes updates possible ([Pattern 13](#pattern-13-persistence-behind-an-interface)), and a query that only reads has no use for it.
3. `.Where(batch => batch.OrganizationId == organizationId)` (line 32) is the tenant filter ([Pattern 7](#pattern-7-tenant-scoping-from-header-to-query)). The lambda is handed to `Queryable.Where` as an *expression tree*: a description of the code, which EF Core reads and translates into a SQL `WHERE`.
4. `.Select(batch => new ImportBatchView(` (line 33) is the projection. It builds the view record straight from the query. No `ImportBatch` object is created, and EF Core only reads the columns that the projection mentions.
5. `.ToListAsync(cancellationToken);` (line 51) is where the query runs. The SQL is sent, and the rows come back as a `List<ImportBatchView>`.
6. `return batches.OrderByDescending(batch => batch.CreatedAt).ToList();` (line 55) sorts afterwards, in memory. `batches` is a list by now, so this is `Enumerable.OrderByDescending`, which takes an ordinary delegate and does the sorting itself. The comment above the line gives the reason: "SQLite cannot order DateTimeOffset values." The documentation of the SQLite provider says the same: comparison and ordering of `DateTimeOffset` values are not translated to SQL.

A join in query syntax:

7. `from entry in dbContext.TimeEntries.AsNoTracking()` (line 105) begins the only query in the codebase that is written in *query syntax*, the form that reads like SQL in a different order. The compiler rewrites it into the same kind of method calls as above.
8. `join employee in dbContext.Employees.AsNoTracking() on entry.EmployeeId equals employee.Id` (line 106) joins a second table. `on … equals …` names the two values that must match. Two more `join` lines add the project and the classification.
9. The `where` part has one tenant comparison for each of the four tables, such as `employee.OrganizationId == organizationId &&` (line 110). After those it limits the work date to the period of the payroll run.
10. `orderby entry.WorkDate, employee.DisplayName, classification.Name` (line 115) sorts by three values. This sort is part of the query, so the database does it.
11. `select new PayrollWorkSegment(` (line 116) is a projection again. Fourteen values from four tables go into one record for each time entry. The whole query stands in parentheses so that `.ToListAsync(cancellationToken)` can be called on it.

Loading one entity with its children:

12. `IQueryable<PayrollRun> query = dbContext.PayrollRuns` (line 222) writes out the type of the variable where most of the code uses `var`. With `var`, the variable would get the type that the last `Include` returns, an `IIncludableQueryable<…>`, and line 229 could not assign the `IQueryable<PayrollRun>` that `AsNoTracking` returns.
13. `.Where(run => run.OrganizationId == organizationId && run.Id == runId)` (line 223) filters by tenant and by id in one condition. A run of another organization is not found at all ([Pattern 7](#pattern-7-tenant-scoping-from-header-to-query)).
14. `.Include(run => run.Lines)` (line 224) and the line after it load the lines and the findings together with the run. This is called *eager loading*.
15. `if (!track)` (line 227) lets one method serve two kinds of caller. The reads pass `track: false` and get objects that are not tracked. The two operations that change a run, calculate and approve, pass `track: true` ([Application/PayrollRunService.cs:90](src/Northstar.Application/PayrollRuns/PayrollRunService.cs#L90) and [Application/PayrollRunService.cs:181](src/Northstar.Application/PayrollRuns/PayrollRunService.cs#L181)).
16. `return await query.SingleOrDefaultAsync(cancellationToken);` (line 232) runs the query. It returns the one run that matches, or null when there is none. The callers turn null into a `NotFound` result ([Pattern 6](#pattern-6-result-objects-mapped-to-http-responses)).

The pieces around it:

- Both database providers are set to split queries with `UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)` ([Infrastructure/DependencyInjection.cs:27](src/Northstar.Infrastructure/DependencyInjection.cs#L27) and [Infrastructure/DependencyInjection.cs:33](src/Northstar.Infrastructure/DependencyInjection.cs#L33)). EF Core's default is to load an entity and its included collections with one SQL query that joins the tables. With split queries it sends one more query for each included collection. `LoadAsync` therefore sends three: one for the run, one for its lines and one for its findings.
- The same method name can stand for two different methods. `Where` on an `IQueryable<T>` is `Queryable.Where` (12 uses), which is translated to SQL. `Where` on a list or an array is `Enumerable.Where` (6 uses), which runs in memory. The type in front of the dot decides.
- The other list queries sort in the database: payroll runs by `PeriodStart` ([Application/PayrollRunService.cs:26](src/Northstar.Application/PayrollRuns/PayrollRunService.cs#L26)), wage rates by classification, scope and start date ([Application/WageRateService.cs:16-18](src/Northstar.Application/WageRates/WageRateService.cs#L16-L18)), and the rows of an import by `RowNumber` ([Application/ImportBatchService.cs:94](src/Northstar.Application/Imports/ImportBatchService.cs#L94)).
- `PayrollRunService.ListAsync` does not project inside the query. It loads the runs with their lines and findings and maps them afterwards, in memory, with `runs.Select(Map).ToArray()` ([Application/PayrollRunService.cs:17-30](src/Northstar.Application/PayrollRuns/PayrollRunService.cs#L17-L30)).
- The query for the wage rates of a calculation passes a list of ids: `classificationIds.Contains(rate.ClassificationId)` keeps the rates whose classification is in the array ([Application/PayrollRunService.cs:133-140](src/Northstar.Application/PayrollRuns/PayrollRunService.cs#L133-L140)). C# 14 binds this call to `MemoryExtensions.Contains`, a method for spans, where earlier versions bound it to `Enumerable.Contains`. That is why [section 5](#5-net-api-inventory) lists the first method and not the second.
- Besides `ToListAsync` (12 uses), the methods that run a query here are `SingleOrDefaultAsync` (9), `AnyAsync` (7), `SingleAsync` (3) and `CountAsync` (2). `AsNoTracking` is called 23 times.

**Why it is done this way.** Each part of the shape has its reason in the EF Core documentation. No-tracking queries are recommended when the results are only read, because EF Core then does not have to set up the information for change tracking. Projecting only the properties that are needed keeps the amount of data small. For included collections the documentation describes what split queries are for: a single joined query repeats the columns of the parent in every child row, and with two collections the rows multiply, which it calls *cartesian explosion*. It also names the price: one roundtrip for each query, and results that can be inconsistent when the data changes between the queries. The tenant filter is the decision from the architecture note that [Pattern 7](#pattern-7-tenant-scoping-from-header-to-query) quotes ([docs/architecture.md:49](docs/architecture.md#L49)). The sort in memory is limited by its own comment to what it calls an "intentionally small study history".

**Look up.** [`IQueryable`](https://learn.microsoft.com/en-us/dotnet/api/system.linq.iqueryable-1?view=net-10.0) · [`EntityFrameworkQueryableExtensions.AsNoTracking`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.entityframeworkqueryableextensions.asnotracking?view=efcore-10.0#microsoft-entityframeworkcore-entityframeworkqueryableextensions-asnotracking-1(system-linq-iqueryable((-0)))) · [`Queryable.Where`](https://learn.microsoft.com/en-us/dotnet/api/system.linq.queryable.where?view=net-10.0#system-linq-queryable-where-1(system-linq-iqueryable((-0))-system-linq-expressions-expression((system-func((-0-system-boolean)))))) · [`Queryable.Select`](https://learn.microsoft.com/en-us/dotnet/api/system.linq.queryable.select?view=net-10.0#system-linq-queryable-select-2(system-linq-iqueryable((-0))-system-linq-expressions-expression((system-func((-0-1)))))) · [`Queryable.OrderBy`](https://learn.microsoft.com/en-us/dotnet/api/system.linq.queryable.orderby?view=net-10.0#system-linq-queryable-orderby-2(system-linq-iqueryable((-0))-system-linq-expressions-expression((system-func((-0-1)))))) · [`EntityFrameworkQueryableExtensions.ToListAsync`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.entityframeworkqueryableextensions.tolistasync?view=efcore-10.0#microsoft-entityframeworkcore-entityframeworkqueryableextensions-tolistasync-1(system-linq-iqueryable((-0))-system-threading-cancellationtoken)) · [`EntityFrameworkQueryableExtensions.SingleOrDefaultAsync`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.entityframeworkqueryableextensions.singleordefaultasync?view=efcore-10.0#microsoft-entityframeworkcore-entityframeworkqueryableextensions-singleordefaultasync-1(system-linq-iqueryable((-0))-system-linq-expressions-expression((system-func((-0-system-boolean))))-system-threading-cancellationtoken)) · [`EntityFrameworkQueryableExtensions.AnyAsync`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.entityframeworkqueryableextensions.anyasync?view=efcore-10.0#microsoft-entityframeworkcore-entityframeworkqueryableextensions-anyasync-1(system-linq-iqueryable((-0))-system-linq-expressions-expression((system-func((-0-system-boolean))))-system-threading-cancellationtoken)) · [`EntityFrameworkQueryableExtensions.Include`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.entityframeworkqueryableextensions.include?view=efcore-10.0#microsoft-entityframeworkcore-entityframeworkqueryableextensions-include-2(system-linq-iqueryable((-0))-system-linq-expressions-expression((system-func((-0-1)))))) · [`Enumerable.Where`](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.where?view=net-10.0#system-linq-enumerable-where-1(system-collections-generic-ienumerable((-0))-system-func((-0-system-boolean)))) · [`Enumerable.OrderByDescending`](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.orderbydescending?view=net-10.0#system-linq-enumerable-orderbydescending-2(system-collections-generic-ienumerable((-0))-system-func((-0-1)))) · [`Enumerable.ToList`](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.tolist?view=net-10.0#system-linq-enumerable-tolist-1(system-collections-generic-ienumerable((-0)))) · [`RelationalDbContextOptionsBuilder.UseQuerySplittingBehavior`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.infrastructure.relationaldbcontextoptionsbuilder-2.usequerysplittingbehavior?view=efcore-10.0#microsoft-entityframeworkcore-infrastructure-relationaldbcontextoptionsbuilder-2-usequerysplittingbehavior(microsoft-entityframeworkcore-querysplittingbehavior)) · [`QuerySplittingBehavior.SplitQuery`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.querysplittingbehavior?view=efcore-10.0) · [`MemoryExtensions.Contains`](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.contains?view=net-10.0#system-memoryextensions-contains-1(system-readonlyspan((-0))-0)) · [LINQ query syntax `from ... join ... select`](https://learn.microsoft.com/en-us/dotnet/csharp/linq/get-started/query-expression-basics) · [Lambda `x => ...`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/lambda-expressions) · [Named argument `name: value`](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/named-and-optional-arguments#named-arguments) · [`var`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/declarations#implicitly-typed-local-variables)

**Read more.** [Querying data](https://learn.microsoft.com/en-us/ef/core/querying/) · [Tracking vs. no-tracking queries](https://learn.microsoft.com/en-us/ef/core/querying/tracking) · [Project only properties you need](https://learn.microsoft.com/en-us/ef/core/performance/efficient-querying#project-only-properties-you-need) · [Eager loading of related data](https://learn.microsoft.com/en-us/ef/core/querying/related-data/eager) · [Single vs. split queries](https://learn.microsoft.com/en-us/ef/core/querying/single-split-queries) · [Client vs. server evaluation](https://learn.microsoft.com/en-us/ef/core/querying/client-eval) · [SQLite provider limitations](https://learn.microsoft.com/en-us/ef/core/providers/sqlite/limitations) · [Efficient querying](https://learn.microsoft.com/en-us/ef/core/performance/efficient-querying) · [Write C# LINQ queries to query data](https://learn.microsoft.com/en-us/dotnet/csharp/linq/get-started/write-linq-queries) · [.NET 10 breaking change: C# 14 overload resolution with span parameters](https://learn.microsoft.com/en-us/dotnet/core/compatibility/core-libraries/10.0/csharp-overload-resolution)

### Pattern 16: Optimistic concurrency with a version number

**What it is.** Two requests, or two workers, can load the same row, change it and save it. Without protection the second save overwrites the first, and nobody notices. That is called a *lost update*. *Optimistic concurrency* detects it without locking anything. Each row carries a version number. A save succeeds only when the row still has the version that was loaded, and it raises the number by one. A second save that started from the old version finds a different number and fails. The name comes from the assumption behind it: conflicts are rare, so it is enough to check at the moment of saving.

**Where to find it.** The worker's claim: `BeginProcessing` in [Domain/ImportBatch.cs:35-60](src/Northstar.Domain/Imports/ImportBatch.cs#L35-L60) and its caller in [Application/ImportBatchProcessor.cs:16-36](src/Northstar.Application/Imports/ImportBatchProcessor.cs#L16-L36). The user's approval: `Approve` in [Domain/PayrollRun.cs:83-104](src/Northstar.Domain/PayrollRuns/PayrollRun.cs#L83-L104) and `ApproveAsync` in [Application/PayrollRunService.cs:175-214](src/Northstar.Application/PayrollRuns/PayrollRunService.cs#L175-L214).

**[`src/Northstar.Domain/Imports/ImportBatch.cs`](src/Northstar.Domain/Imports/ImportBatch.cs#L35-L60)** · lines 35–60

<!-- x:conc-claim -->
```csharp
public bool BeginProcessing(DateTimeOffset startedAt, TimeSpan leaseDuration)
{
    var canClaim = Status == ImportBatchStatus.Pending ||
        (Status == ImportBatchStatus.Processing &&
            (ProcessingLeaseExpiresAt is null || ProcessingLeaseExpiresAt <= startedAt));
    if (!canClaim)
    {
        return false;
    }

    if (leaseDuration <= TimeSpan.Zero)
    {
        throw new ArgumentOutOfRangeException(nameof(leaseDuration));
    }

    Status = ImportBatchStatus.Processing;
    StartedAt = startedAt;
    ProcessingLeaseExpiresAt = startedAt.Add(leaseDuration);
    CompletedAt = null;
    FailureCode = null;
    FailureMessage = null;
    CanRetry = false;
    AttemptCount++;
    Version++;
    return true;
}
```

**[`src/Northstar.Application/Imports/ImportBatchProcessor.cs`](src/Northstar.Application/Imports/ImportBatchProcessor.cs#L16-L36)** · lines 16–36

<!-- x:conc-processor -->
```csharp
public async Task ProcessAsync(
    Guid importBatchId,
    CancellationToken cancellationToken)
{
    var batch = await dbContext.ImportBatches.SingleOrDefaultAsync(
        value => value.Id == importBatchId,
        cancellationToken);
    if (batch is null || !batch.BeginProcessing(clock.UtcNow, ProcessingLease))
    {
        return;
    }

    try
    {
        await dbContext.SaveChangesAsync(cancellationToken);
    }
    catch (DbUpdateConcurrencyException)
    {
        // Another worker won the optimistic claim for this batch.
        return;
    }
    // ... continues in the file ...
```

**[`src/Northstar.Domain/PayrollRuns/PayrollRun.cs`](src/Northstar.Domain/PayrollRuns/PayrollRun.cs#L83-L104)** · lines 83–104

<!-- x:conc-approve -->
```csharp
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
```

**[`src/Northstar.Application/PayrollRuns/PayrollRunService.cs`](src/Northstar.Application/PayrollRuns/PayrollRunService.cs#L175-L214)** · lines 175–214

<!-- x:conc-approve-service -->
```csharp
public async Task<OperationResult<PayrollRunView>> ApproveAsync(
    Guid organizationId,
    Guid runId,
    long expectedVersion,
    CancellationToken cancellationToken)
{
    var run = await LoadAsync(organizationId, runId, track: true, cancellationToken);
    if (run is null)
    {
        return OperationResult<PayrollRunView>.NotFound();
    }

    var approval = run.Approve(expectedVersion, clock.UtcNow);
    if (approval.Status == PayrollRunActionStatus.Blocked)
    {
        return OperationResult<PayrollRunView>.Invalid(
            approval.ErrorCode ?? "APPROVAL_BLOCKED",
            "The payroll run has blocker findings.");
    }

    if (approval.Status == PayrollRunActionStatus.Conflict)
    {
        return OperationResult<PayrollRunView>.Conflict(
            approval.ErrorCode ?? "INVALID_STATE",
            "The payroll run changed or is not ready for approval.");
    }

    try
    {
        await dbContext.SaveChangesAsync(cancellationToken);
    }
    catch (DbUpdateConcurrencyException)
    {
        return OperationResult<PayrollRunView>.Conflict(
            "VERSION_CONFLICT",
            "The payroll run was updated by another request.");
    }

    return OperationResult<PayrollRunView>.Success(Map(run));
}
```

**How to read the code.** The worker claims a batch:

1. `var canClaim = Status == ImportBatchStatus.Pending ||` (line 37) begins a condition that runs over three lines. A batch can be claimed when it is `Pending`. It can also be claimed when it is `Processing` and its *lease* is missing or has run out. The lease is the time until which an earlier claim holds.
2. `return false;` (line 42) refuses every other batch. Nothing has been changed at that point.
3. `ProcessingLeaseExpiresAt = startedAt.Add(leaseDuration);` (line 52) sets a new lease. The processor passes two minutes, its constant `ProcessingLease` ([Application/ImportBatchProcessor.cs:14](src/Northstar.Application/Imports/ImportBatchProcessor.cs#L14)).
4. `Version++;` (line 58) raises the version number by one. The rest of the pattern depends on this statement.
5. In the processor, `if (batch is null || !batch.BeginProcessing(clock.UtcNow, ProcessingLease))` (line 23) ends the method when the batch does not exist or when the claim was refused. The batch was loaded three lines earlier with a tracked query, so the changes of steps 3 and 4 are now waiting to be saved ([Pattern 13](#pattern-13-persistence-behind-an-interface)).
6. `await dbContext.SaveChangesAsync(cancellationToken);` (line 30) writes the claim. EF Core knows from the model that `Version` is a *concurrency token* ([Pattern 14](#pattern-14-ef-core-model-configuration-with-the-fluent-api)). The `UPDATE` that it sends therefore has a `WHERE` with the id of the row and with the version that was loaded. When another worker has saved its own claim in the meantime, the row has a higher version, the `UPDATE` matches no row, and EF Core throws `DbUpdateConcurrencyException`.
7. `catch (DbUpdateConcurrencyException)` (line 32) treats that exception as a normal outcome. The comment gives the reason: "Another worker won the optimistic claim for this batch." The method returns and leaves the batch to the other worker.

The user approves a payroll run:

8. `public PayrollRunActionResult Approve(long expectedVersion, DateTimeOffset approvedAt)` (line 83) takes the version that the caller last saw. That value has travelled to the browser and back. The view of a run carries `Version` ([Application/PayrollRunView.cs:33](src/Northstar.Application/PayrollRuns/PayrollRunView.cs#L33)), the Angular service sends it as `expectedVersion` ([Web/payroll-api.service.ts:42-46](src/Northstar.Web/src/app/payroll-runs/payroll-api.service.ts#L42-L46)), and the controller receives it in the record `ApprovePayrollRunRequest` ([Api/PayrollRunsController.cs:91](src/Northstar.Api/Controllers/PayrollRunsController.cs#L91)).
9. `if (Version != expectedVersion)` (line 85) compares it with the version of the run as it was just loaded. A difference means that the run has changed since the user's copy was read, and the method returns the code `VERSION_CONFLICT`.
10. Two more checks look at the state and not at the version. `if (Status != PayrollRunStatus.ReadyForApproval)` (line 90) refuses a run in any other status, and `if (Findings.Any(finding => finding.Severity == FindingSeverity.Blocker))` (line 95) refuses a run that still has a blocker finding.
11. `Version++;` (line 102) raises the version together with the change of status.
12. In the service, `var approval = run.Approve(expectedVersion, clock.UtcNow);` (line 187) calls the method on a tracked run. The service turns `Blocked` into an `Invalid` result and `Conflict` into a `Conflict` result, and the mapping of [Pattern 6](#pattern-6-result-objects-mapped-to-http-responses) answers `422` and `409`.
13. `catch (DbUpdateConcurrencyException)` (line 206) covers the case that step 9 cannot see. Two requests can load the same run at the same moment, and then both pass the comparison in memory. Both try to save, and only the first `UPDATE` still finds the row with the old version. The second request gets the exception, and the service answers with the same code, `VERSION_CONFLICT`.

The pieces around it:

- Six entity classes have a property `Version` of type `long` that starts at 1, and the model marks all six with `IsConcurrencyToken()` ([Pattern 14](#pattern-14-ef-core-model-configuration-with-the-fluent-api)).
- The codebase has 7 `Version++` statements: four in `ImportBatch` (`BeginProcessing`, `Complete`, `Fail`, `Retry`) and three in `PayrollRun` (`BeginCalculation`, `CompleteCalculation`, `Approve`). In these two classes the property has a private setter ([Domain/ImportBatch.cs:32](src/Northstar.Domain/Imports/ImportBatch.cs#L32), [Domain/PayrollRun.cs:25](src/Northstar.Domain/PayrollRuns/PayrollRun.cs#L25)), so only the entity's own methods can raise it ([Pattern 17](#pattern-17-entities-as-state-machines-and-pure-domain-services)).
- The number is assigned by the application and not by the database. The EF Core documentation calls this an *application-managed concurrency token*. Some databases have a column type that changes by itself on every update, such as `rowversion` in SQL Server. The codebase does not use one.
- `DbUpdateConcurrencyException` is caught in four places. Two are shown above. `MarkUnexpectedFailureAsync` ignores it, with the comment "The batch was recovered or completed by another worker." ([Application/ImportBatchProcessor.cs:194-197](src/Northstar.Application/Imports/ImportBatchProcessor.cs#L194-L197)). The retry request answers `409` with the code `IMPORT_RETRY_CONFLICT` ([Application/ImportBatchService.cs:252-258](src/Northstar.Application/Imports/ImportBatchService.cs#L252-L258)).
- `DbUpdateConcurrencyException` derives from `DbUpdateException`. The retry request catches both, and the clause for the derived type stands first ([Application/ImportBatchService.cs:252](src/Northstar.Application/Imports/ImportBatchService.cs#L252) and [Application/ImportBatchService.cs:259](src/Northstar.Application/Imports/ImportBatchService.cs#L259)). C# tries the `catch` clauses from top to bottom. The second clause handles the other failures of the save, such as a second idempotency record with the same key ([Pattern 8](#pattern-8-idempotent-commands-with-an-idempotency-key)).
- `Complete`, `Fail` and `Retry` set the lease back to null ([Domain/ImportBatch.cs:88](src/Northstar.Domain/Imports/ImportBatch.cs#L88), [Domain/ImportBatch.cs:105](src/Northstar.Domain/Imports/ImportBatch.cs#L105), [Domain/ImportBatch.cs:121](src/Northstar.Domain/Imports/ImportBatch.cs#L121)). A batch that is still `Processing` after its lease has run out is found by the recovery scan ([Pattern 10](#pattern-10-background-worker-with-a-queue-and-a-recovery-scan)) and can be claimed again through step 1.
- Two tests show the approval from both sides. A unit test approves with `run.Version - 1` and expects `VERSION_CONFLICT` ([UnitTests/PayrollRunStateTests.cs:44-48](tests/Northstar.Domain.UnitTests/PayrollRunStateTests.cs#L44-L48)). An API test sends the version that the calculation returned ([IntegrationTests/PayrollRunsApiTests.cs:111-113](tests/Northstar.Api.IntegrationTests/PayrollRunsApiTests.cs#L111-L113)).

**Why it is done this way.** The README describes the end of the payroll path as "approve with an optimistic version check", and it lists an "optimistic claim" and an "expiring processing lease" among the parts that make the worker recoverable ([Pattern 10](#pattern-10-background-worker-with-a-queue-and-a-recovery-scan) quotes the whole sentence). The EF Core documentation contrasts two approaches. A pessimistic one locks the data first and changes it afterwards. The optimistic one takes no lock and makes the save fail when the data has changed since it was read. For the approval, reading and saving happen in two separate HTTP requests, which is why the version is sent to the browser and comes back with the request. The same documentation says that a token managed by the application also works on databases that have no self-updating column type, and it names SQLite as an example. That matters here, because the architecture note begins its paragraph on databases with: "SQLite is the zero-setup development and fast integration-test provider." ([docs/architecture.md:22](docs/architecture.md#L22)).

**Look up.** [`PropertyBuilder.IsConcurrencyToken`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.metadata.builders.propertybuilder-1.isconcurrencytoken?view=efcore-10.0#microsoft-entityframeworkcore-metadata-builders-propertybuilder-1-isconcurrencytoken(system-boolean)) · [`DbUpdateConcurrencyException`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.dbupdateconcurrencyexception?view=efcore-10.0) · [`DbUpdateException`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.dbupdateexception?view=efcore-10.0) · [`DbContext.SaveChangesAsync`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.dbcontext.savechangesasync?view=efcore-10.0#microsoft-entityframeworkcore-dbcontext-savechangesasync(system-threading-cancellationtoken)) · [`DateTimeOffset.Add`](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.add?view=net-10.0#system-datetimeoffset-add(system-timespan)) · [`TimeSpan.FromMinutes`](https://learn.microsoft.com/en-us/dotnet/api/system.timespan.fromminutes?view=net-10.0#system-timespan-fromminutes(system-int64)) · [`TimeSpan.Zero`](https://learn.microsoft.com/en-us/dotnet/api/system.timespan.zero?view=net-10.0#system-timespan-zero) · [`Enumerable.Any`](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.any?view=net-10.0#system-linq-enumerable-any-1(system-collections-generic-ienumerable((-0))-system-func((-0-system-boolean)))) · [`ArgumentOutOfRangeException`](https://learn.microsoft.com/en-us/dotnet/api/system.argumentoutofrangeexception?view=net-10.0) · [`catch (SpecificException)`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/exception-handling-statements#the-try-catch-statement) · [`private set`](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/restricting-accessor-accessibility) · [`is` pattern](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/is) · [Null-coalescing `??`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/null-coalescing-operator) · [`nameof`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/nameof)

**Read more.** [Handling concurrency conflicts](https://learn.microsoft.com/en-us/ef/core/saving/concurrency) · [Optimistic concurrency](https://learn.microsoft.com/en-us/ef/core/saving/concurrency#optimistic-concurrency) · [Application-managed concurrency tokens](https://learn.microsoft.com/en-us/ef/core/saving/concurrency#application-managed-concurrency-tokens) · [409 Conflict (MDN)](https://developer.mozilla.org/en-US/docs/Web/HTTP/Reference/Status/409) · [422 Unprocessable Content (MDN)](https://developer.mozilla.org/en-US/docs/Web/HTTP/Reference/Status/422)

### Pattern 17: Entities as state machines and pure domain services

**What it is.** Two ideas keep the business rules inside the Domain project. The first concerns entities whose status may only change in certain ways. Such an entity does not let other code assign its status. It offers methods, and each method checks the current status before it changes anything. An object like this is called a *state machine*: a fixed set of states, and a fixed set of allowed moves between them. The second idea concerns rules that need more than one entity, such as choosing a wage rate. They live in small classes without any data of their own, called *domain services*. Their methods are *pure*: the result depends only on the arguments, and calling them changes nothing else.

**Where to find it.** `ImportBatch` in [Domain/ImportBatch.cs:12-126](src/Northstar.Domain/Imports/ImportBatch.cs#L12-L126), `PayrollRun` in [Domain/PayrollRun.cs:17-105](src/Northstar.Domain/PayrollRuns/PayrollRun.cs#L17-L105), `WageRateSelector` in [Domain/WageRateSelector.cs:11-70](src/Northstar.Domain/WageRates/WageRateSelector.cs#L11-L70) and its result type in [Domain/RateSelectionResult.cs:3-23](src/Northstar.Domain/WageRates/RateSelectionResult.cs#L3-L23).

**[`src/Northstar.Domain/Imports/ImportBatch.cs`](src/Northstar.Domain/Imports/ImportBatch.cs#L12-L33)** · lines 12–33

<!-- x:state-props -->
```csharp
public sealed class ImportBatch
{
    public Guid Id { get; set; }
    public Guid OrganizationId { get; set; }
    public DateOnly PeriodStart { get; set; }
    public DateOnly PeriodEnd { get; set; }
    public string ProviderScenario { get; set; } = "success";
    public ImportBatchStatus Status { get; private set; } = ImportBatchStatus.Pending;
    public int TotalRows { get; private set; }
    public int ImportedRows { get; private set; }
    public int ErrorRows { get; private set; }
    public int DuplicateRows { get; private set; }
    public int AttemptCount { get; private set; }
    public string? FailureCode { get; private set; }
    public string? FailureMessage { get; private set; }
    public bool CanRetry { get; private set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? StartedAt { get; private set; }
    public DateTimeOffset? ProcessingLeaseExpiresAt { get; private set; }
    public DateTimeOffset? CompletedAt { get; private set; }
    public long Version { get; private set; } = 1;
    public List<ImportRow> Rows { get; } = [];
    // ... continues in the file ...
```

**[`src/Northstar.Domain/Imports/ImportBatch.cs`](src/Northstar.Domain/Imports/ImportBatch.cs#L93-L125)** · lines 93–125

<!-- x:state-fail-retry -->
```csharp
public void Fail(string code, string message, bool canRetry, DateTimeOffset failedAt)
{
    if (Status != ImportBatchStatus.Processing)
    {
        throw new InvalidOperationException("Only a processing import batch can fail.");
    }

    Status = ImportBatchStatus.Failed;
    FailureCode = code;
    FailureMessage = message;
    CanRetry = canRetry;
    CompletedAt = failedAt;
    ProcessingLeaseExpiresAt = null;
    Version++;
}

public bool Retry()
{
    if (Status != ImportBatchStatus.Failed || !CanRetry)
    {
        return false;
    }

    Status = ImportBatchStatus.Pending;
    FailureCode = null;
    FailureMessage = null;
    CanRetry = false;
    StartedAt = null;
    ProcessingLeaseExpiresAt = null;
    CompletedAt = null;
    Version++;
    return true;
}
```

**[`src/Northstar.Domain/PayrollRuns/PayrollRun.cs`](src/Northstar.Domain/PayrollRuns/PayrollRun.cs#L31-L47)** · lines 31–47

<!-- x:state-run-begin -->
```csharp
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
```

**[`src/Northstar.Domain/WageRates/WageRateSelector.cs`](src/Northstar.Domain/WageRates/WageRateSelector.cs#L13-L38)** · lines 13–38

<!-- x:selector -->
```csharp
public RateSelectionResult Select(WageRateQuery query, IEnumerable<WageRate> candidates)
{
    ArgumentNullException.ThrowIfNull(candidates);

    var matching = candidates
        .Where(rate => Matches(query, rate))
        .ToArray();

    if (matching.Length == 0)
    {
        return RateSelectionResult.Missing();
    }

    var bestScope = matching.Min(rate => rate.Scope);
    var scoped = matching.Where(rate => rate.Scope == bestScope).ToArray();
    var bestPriority = scoped.Max(rate => rate.Priority);
    var finalists = scoped
        .Where(rate => rate.Priority == bestPriority)
        .OrderBy(rate => rate.EffectiveFrom)
        .ThenBy(rate => rate.Id)
        .ToArray();

    return finalists.Length == 1
        ? RateSelectionResult.Selected(finalists[0])
        : RateSelectionResult.Ambiguous(finalists);
}
```

**[`src/Northstar.Domain/WageRates/RateSelectionResult.cs`](src/Northstar.Domain/WageRates/RateSelectionResult.cs#L3-L23)** · lines 3–23

<!-- x:selection-result -->
```csharp
public enum RateSelectionStatus
{
    Selected,
    Missing,
    Ambiguous
}

public sealed record RateSelectionResult(
    RateSelectionStatus Status,
    WageRate? Rate,
    IReadOnlyList<Guid> CandidateIds)
{
    public static RateSelectionResult Selected(WageRate rate) =>
        new(RateSelectionStatus.Selected, rate, [rate.Id]);

    public static RateSelectionResult Missing() =>
        new(RateSelectionStatus.Missing, null, []);

    public static RateSelectionResult Ambiguous(IEnumerable<WageRate> rates) =>
        new(RateSelectionStatus.Ambiguous, null, rates.Select(rate => rate.Id).ToArray());
}
```

**How to read the code.** An entity that guards its status:

1. `public ImportBatchStatus Status { get; private set; } = ImportBatchStatus.Pending;` (line 19) has a *private setter*. Every class can read the status, and only `ImportBatch` itself can assign it. The initializer at the end makes a new batch `Pending`.
2. Thirteen properties of the class are declared with `private set`. Six have a public setter: the id, the organization, the two dates of the period, the scenario and the creation time. The service fills them in when it creates the batch ([Application/ImportBatchService.cs:146-154](src/Northstar.Application/Imports/ImportBatchService.cs#L146-L154)).
3. `public List<ImportRow> Rows { get; } = [];` (line 33) has no setter at all. The list is created together with the batch. Rows can be added to it, and it cannot be replaced by another list.
4. `if (Status != ImportBatchStatus.Processing)` (line 95) is the check at the top of `Fail`. Only a batch that is being processed can fail. In any other status the method throws: `throw new InvalidOperationException("Only a processing import batch can fail.");` (line 97)
5. After the check, the method sets everything that belongs to the new state in one place: the status, the failure code and message, whether a retry is allowed, the time, the lease and the version ([Pattern 16](#pattern-16-optimistic-concurrency-with-a-version-number)). The time arrives as the parameter `failedAt`. The entity does not read a clock ([Pattern 19](#pattern-19-small-conventions-used-everywhere)).
6. `if (Status != ImportBatchStatus.Failed || !CanRetry)` (line 111) is the check of `Retry`, and this method refuses in a different way: `return false;` (line 113). A request to retry a batch that cannot be retried is something that happens in normal use. The service turns the `false` into a `409` answer with the code `IMPORT_NOT_RETRYABLE` ([Application/ImportBatchService.cs:233-238](src/Northstar.Application/Imports/ImportBatchService.cs#L233-L238)).
7. `Status = ImportBatchStatus.Pending;` (line 116) puts the batch back at the start. The lines after it clear what the failed attempt left behind.

A payroll run:

8. `if (Status is PayrollRunStatus.Approved or PayrollRunStatus.ReportQueued or` (line 33) uses a *pattern*. `is` with `or` compares one value with several constants, and the condition continues on the next line with two more. A run in one of these four statuses is not calculated again. The method reports that with a result object: `return PayrollRunActionResult.Conflict("APPROVED_RUN_REQUIRES_REOPEN");` (line 36)
9. `PayrollRunActionResult` is a small record with a status and an error code ([Domain/PayrollRun.cs:10-15](src/Northstar.Domain/PayrollRuns/PayrollRun.cs#L10-L15)). It does the job of the `bool` in step 6 and can also say why. The service turns it into a `Conflict` result ([Application/PayrollRunService.cs:96-102](src/Northstar.Application/PayrollRuns/PayrollRunService.cs#L96-L102), [Pattern 6](#pattern-6-result-objects-mapped-to-http-responses)).
10. `Status = PayrollRunStatus.Calculating;` (line 44) is the move itself, followed by the new version number.

A pure domain service:

11. `ArgumentNullException.ThrowIfNull(candidates);` (line 15) is a *guard clause*. It throws at once when the argument is null.
12. `.Where(rate => Matches(query, rate))` (line 18) keeps the rates that apply to the query. `Matches` is a private static method further down ([Domain/WageRateSelector.cs:40-66](src/Northstar.Domain/WageRates/WageRateSelector.cs#L40-L66)). It asks for the same organization, the same classification and a rate that is effective on the work date, and then a `switch` expression has one condition for each scope.
13. `return RateSelectionResult.Missing();` (line 23) is the answer when nothing applies. The method does not return null, and it does not make up a rate. The architecture note states the rule: "Rate selection is pure and never turns a missing rate into zero dollars."
14. `var bestScope = matching.Min(rate => rate.Scope);` (line 26) finds the most specific scope among the matches. `Scope` is an enum whose values are numbered from 0 for `EmployeeProjectOverride` to 4 for `Federal` ([Domain/WageRate.cs:3-10](src/Northstar.Domain/WageRates/WageRate.cs#L3-L10)), and `Min` compares enum values by their numbers. The note says: "Lower `WageRateScope` rank has higher precedence; priority breaks ties within a scope."
15. `var bestPriority = scoped.Max(rate => rate.Priority);` (line 28) is that tie-break. Inside the best scope, the highest priority wins.
16. The rates that are left are sorted, first with `.OrderBy(rate => rate.EffectiveFrom)` (line 31) and then by id. The same input therefore always gives the same order.
17. `return finalists.Length == 1` (line 35) chooses between two results with the conditional operator `? :`. One finalist is the selected rate. Several are reported, and none is picked: "Multiple active candidates at the same scope and priority are ambiguous."
18. `public sealed record RateSelectionResult(` (line 10) is the type of the answer: a status, the rate when one was selected, and the ids of the candidates. Three static methods create the three kinds of result, so the selector reads `RateSelectionResult.Missing()` and does not spell out the three values each time.
19. `new(RateSelectionStatus.Selected, rate, [rate.Id]);` (line 16) shows two short forms. `new(…)` without a type name creates the type that the method returns, which is called *target-typed `new`*. `[rate.Id]` is a *collection expression*, here a list with one element. The `[]` in the next method is an empty one.

The pieces around it:

- The moves that the two entities allow:

| Method | Allowed when the status is | New status | How it refuses |
|---|---|---|---|
| `ImportBatch.BeginProcessing` | `Pending`, or `Processing` with a lease that is missing or has run out | `Processing` | returns `false` |
| `ImportBatch.Complete` | `Processing` | `Completed` or `CompletedWithErrors` | throws `InvalidOperationException` |
| `ImportBatch.Fail` | `Processing` | `Failed` | throws `InvalidOperationException` |
| `ImportBatch.Retry` | `Failed` with `CanRetry` set | `Pending` | returns `false` |
| `PayrollRun.BeginCalculation` | anything except `Calculating`, `Approved`, `ReportQueued`, `ReportGenerated` and `Submitted` | `Calculating` | returns a `Conflict` result |
| `PayrollRun.CompleteCalculation` | `Calculating` | `NeedsReview` or `ReadyForApproval` | throws `InvalidOperationException` |
| `PayrollRun.Approve` | `ReadyForApproval`, with the expected version and no blocker finding | `Approved` | returns a `Conflict` or `Blocked` result |

- The table shows the rule behind the two ways of refusing. A method returns `false` or a result object when the refusal can happen in normal operation: a second worker, a repeated request, a run that is not ready. It throws when the calling code itself is wrong. The processor calls `Complete` and `Fail` only for a batch that it has claimed or has just found in `Processing`, so another status at that point would be a mistake in the calling code.
- `Complete` also checks its numbers. It throws `ArgumentOutOfRangeException` when a count is negative or when the imported, error and duplicate rows do not add up to the total ([Domain/ImportBatch.cs:74-78](src/Northstar.Domain/Imports/ImportBatch.cs#L74-L78)). The architecture note has the rule that it enforces: "imported/error/duplicate counts must add to the provider total".
- The enum `PayrollRunStatus` has ten values ([Domain/PayrollRunStatus.cs:3-15](src/Northstar.Domain/PayrollRuns/PayrollRunStatus.cs#L3-L15)). A new run starts as `Draft`, and the three methods assign four more. No code in the repository assigns the remaining five.
- All 17 private setters of the codebase are in these two classes. EF Core can still fill such properties when it loads a row. Its documentation says that it treats a property with a private setter as one that can be read and written.
- `WageRateSelector` has no fields and no constructor parameters. `PayrollCalculator` has one, the selector ([Domain/PayrollCalculator.cs:25](src/Northstar.Domain/PayrollRuns/PayrollCalculator.cs#L25)). Neither touches the database, the clock or the network. Both are registered as singletons ([Application/DependencyInjection.cs:16-17](src/Northstar.Application/DependencyInjection.cs#L16-L17), [Pattern 2](#pattern-2-composition-root-and-service-registration)), and the unit tests create them with `new` ([UnitTests/WageRateSelectorTests.cs:7](tests/Northstar.Domain.UnitTests/WageRateSelectorTests.cs#L7)).
- Not every entity is a state machine. `TimeEntry` is eleven properties with public setters and no method ([Domain/TimeEntry.cs:3-16](src/Northstar.Domain/TimeEntries/TimeEntry.cs#L3-L16)). `WageRate` adds a single method, `IsEffectiveOn` ([Domain/WageRate.cs:30-31](src/Northstar.Domain/WageRates/WageRate.cs#L30-L31)), which carries the rule "Dates are inclusive at both effective-date boundaries."
- The glossary gives the order of the scopes in words: "employee override, project, union, state, then federal" ([docs/domain-glossary.md:10](docs/domain-glossary.md#L10)).

**Why it is done this way.** The README describes the Domain project as "framework-free entities, selector, calculator, and run state machine" ([README.md:111](README.md#L111)), and its project file has no package and no project reference ([Domain/Northstar.Domain.csproj](src/Northstar.Domain/Northstar.Domain.csproj)). Because a status can only change inside the entity, every rule about a status has one place where it can be read and tested. Because the selector and the calculator are pure, a test needs nothing but objects in memory. The testing notes say what follows from that: "Domain tests prove inclusive date boundaries, precedence, ambiguity, rounding, and run transitions without I/O." ([docs/testing-strategy.md:5](docs/testing-strategy.md#L5); the tests are [Pattern 20](#pattern-20-tests-at-three-levels-in-net)). Microsoft's architecture guide for .NET describes the same kind of entity. It contrasts a model whose entities have behaviour as well as data with an *anemic* model, where the entities only hold data and the rules sit in other classes. Its chapter on validation says that an entity should always be valid, and that the entity itself enforces its rules, which it calls *invariants*.

**Look up.** [`InvalidOperationException`](https://learn.microsoft.com/en-us/dotnet/api/system.invalidoperationexception?view=net-10.0) · [`ArgumentOutOfRangeException`](https://learn.microsoft.com/en-us/dotnet/api/system.argumentoutofrangeexception?view=net-10.0) · [`ArgumentNullException.ThrowIfNull`](https://learn.microsoft.com/en-us/dotnet/api/system.argumentnullexception.throwifnull?view=net-10.0#system-argumentnullexception-throwifnull(system-object-system-string)) · [`Enumerable.Where`](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.where?view=net-10.0#system-linq-enumerable-where-1(system-collections-generic-ienumerable((-0))-system-func((-0-system-boolean)))) · [`Enumerable.Min`](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.min?view=net-10.0#system-linq-enumerable-min-2(system-collections-generic-ienumerable((-0))-system-func((-0-1)))) · [`Enumerable.Max`](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.max?view=net-10.0#system-linq-enumerable-max-1(system-collections-generic-ienumerable((-0))-system-func((-0-system-int32)))) · [`Enumerable.OrderBy`](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.orderby?view=net-10.0#system-linq-enumerable-orderby-2(system-collections-generic-ienumerable((-0))-system-func((-0-1)))) · [`Enumerable.ThenBy`](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.thenby?view=net-10.0#system-linq-enumerable-thenby-2(system-linq-iorderedenumerable((-0))-system-func((-0-1)))) · [`Enumerable.ToArray`](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.toarray?view=net-10.0#system-linq-enumerable-toarray-1(system-collections-generic-ienumerable((-0)))) · [`Enumerable.Any`](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.any?view=net-10.0#system-linq-enumerable-any-1(system-collections-generic-ienumerable((-0))-system-func((-0-system-boolean)))) · [`string.Equals`](https://learn.microsoft.com/en-us/dotnet/api/system.string.equals?view=net-10.0#system-string-equals(system-string-system-string-system-stringcomparison)) · [`private set`](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/restricting-accessor-accessibility) · [Get-only auto-property `{ get; }`](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/auto-implemented-properties) · [Property initializer `{ get; set; } = value`](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/auto-implemented-properties) · [`is` pattern](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/is) · [`or` pattern](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/patterns#logical-patterns) · [`switch` expression](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/switch-expression) · [Conditional operator `c ? a : b`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/conditional-operator) · [`record`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/record) · [Target-typed `new()`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/new-operator#target-typed-new) · [Collection expression `[a, b]`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/collection-expressions) · [`enum`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/enum) · [`nameof`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/nameof)

**Read more.** [Design a microservice domain model](https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/microservice-domain-model) · [Design validations in the domain model layer](https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/domain-model-layer-validations) · [Implement a microservice domain model with .NET](https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/net-core-microservice-domain-model) · [EF Core: Entity types with constructors](https://learn.microsoft.com/en-us/ef/core/modeling/constructors) · [Pattern matching overview](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/patterns/pattern-matching) · [Best practices for exceptions](https://learn.microsoft.com/en-us/dotnet/standard/exceptions/best-practices-for-exceptions)

### Pattern 18: Async all the way with cancellation

**What it is.** Nearly every operation in this codebase waits for something outside the process: the database, the payroll provider, the queue. The methods that do this are *asynchronous*. An `async` method returns a `Task`, an object that stands for work that is still going on, and `await` pauses the method until that work is complete without holding a thread. "All the way" means that the chain is not broken anywhere. From the controller action down to the EF Core call, each method awaits the next one. A `CancellationToken` travels along the same chain. It is a small value through which the caller can announce that the result is no longer needed.

**Where to find it.** In every layer. The excerpt shows the one method that deliberately does not use the token of its caller: [Infrastructure/ImportBatchWorker.cs:101-117](src/Northstar.Infrastructure/Messaging/ImportBatchWorker.cs#L101-L117). The chain of an ordinary request is easiest to follow in `List`, [Api/ImportBatchesController.cs:15-24](src/Northstar.Api/Controllers/ImportBatchesController.cs#L15-L24).

**[`src/Northstar.Infrastructure/Messaging/ImportBatchWorker.cs`](src/Northstar.Infrastructure/Messaging/ImportBatchWorker.cs#L101-L117)** · lines 101–117

<!-- x:bg-fail -->
```csharp
private async Task MarkUnexpectedFailureAsync(Guid importBatchId)
{
    using var timeout = new CancellationTokenSource(TimeSpan.FromSeconds(5));
    try
    {
        await using var scope = scopeFactory.CreateAsyncScope();
        var processor = scope.ServiceProvider.GetRequiredService<ImportBatchProcessor>();
        await processor.MarkUnexpectedFailureAsync(importBatchId, timeout.Token);
    }
    catch (Exception exception)
    {
        logger.LogError(
            exception,
            "Import batch {ImportBatchId} could not be marked failed after an unexpected error.",
            importBatchId);
    }
}
```

**How to read the code.** One request from top to bottom, in `ImportBatchesController.List`:

1. The action is declared `public async Task<ActionResult<…>>` (line 15). `async` allows `await` inside the method. The return type `Task<T>` stands for a result that arrives later.
2. Its only parameter is `CancellationToken cancellationToken` (line 16). No request sends such a value. ASP.NET Core fills it in with the token of the request, `HttpContext.RequestAborted`, which is cancelled when the client breaks off the request.
3. Line 23 awaits the service and hands the token on: `await importBatchService.ListAsync(organizationId, cancellationToken)`. No thread waits while the service works. The method is paused, and it continues when the task is complete.
4. The service does the same one level down. `ListAsync` awaits `ToListAsync(cancellationToken)` ([Application/ImportBatchService.cs:51](src/Northstar.Application/Imports/ImportBatchService.cs#L51), shown in [Pattern 15](#pattern-15-read-queries-with-no-tracking-a-tenant-filter-and-projection)), and EF Core hands the token to the database driver.
5. When a token is cancelled, an operation that honours it stops and throws `OperationCanceledException`. The exception travels back up through the same `await` expressions, and the work for the request ends.

The excerpt:

6. `private async Task MarkUnexpectedFailureAsync(Guid importBatchId)` (line 101) returns `Task` without a type argument, which means asynchronous work without a result. The name ends in `Async`. The method has no token parameter, which is unusual in this codebase.
7. `using var timeout = new CancellationTokenSource(TimeSpan.FromSeconds(5));` (line 103) creates a *source*. A source is the object that can cancel, and its `Token` property is what other code receives. This constructor makes the source cancel itself after five seconds. `using var` disposes it when the method ends.
8. `await using var scope = scopeFactory.CreateAsyncScope();` (line 106) uses `await using`, the asynchronous form of `using`. At the end of the block it awaits the `DisposeAsync` method of the scope. The scope supplies a processor with a database context of its own ([Pattern 3](#pattern-3-constructor-injection-with-primary-constructors)).
9. `await processor.MarkUnexpectedFailureAsync(importBatchId, timeout.Token);` (line 108) passes the five-second token. The worker's `stoppingToken` does not appear in this method. The worker calls the method after an unexpected error ([Pattern 10](#pattern-10-background-worker-with-a-queue-and-a-recovery-scan)), and the work of marking the batch as failed gets a time limit of its own, whatever the state of the stopping token is.
10. `catch (Exception exception)` (line 110) logs any failure of this attempt, the timeout included, and does not rethrow. The loop of the worker goes on.

The pieces around it:

- The scan counts 72 `async` methods and 204 `await` expressions in the solution, tests included. The `src` folder declares 42 `CancellationToken` parameters.
- Nothing waits by blocking a thread. The code contains no `.Result`, no `.Wait()`, no `GetAwaiter().GetResult()` and no `Thread.Sleep`. A pause is written as `await Task.Delay(…)` (6 uses), which takes a token as well.
- Calls run one after another, each awaited before the next begins. The only task that is started and awaited later is the recovery scan of the worker ([Infrastructure/ImportBatchWorker.cs:18](src/Northstar.Infrastructure/Messaging/ImportBatchWorker.cs#L18) and [Infrastructure/ImportBatchWorker.cs:48](src/Northstar.Infrastructure/Messaging/ImportBatchWorker.cs#L48), [Pattern 10](#pattern-10-background-worker-with-a-queue-and-a-recovery-scan)).
- Every method in `src` and `tools` that returns a task has a name ending in `Async`, except the 11 controller actions, which are named after what they do: `List`, `Get`, `Create`, `Retry`, `Calculate`, `Approve`, `GetErrors`.
- The usual return types are `Task` and `Task<T>`. `ValueTask`, a lighter type for operations that are often complete at once, is the return type of `QueueAsync`, which passes on what the channel's `WriteAsync` returns. `IAsyncEnumerable<Guid>` appears once, for the stream of ids that the worker reads with `await foreach` ([Infrastructure/ImportBatchQueue.cs:15-19](src/Northstar.Infrastructure/Messaging/ImportBatchQueue.cs#L15-L19), [Pattern 10](#pattern-10-background-worker-with-a-queue-and-a-recovery-scan)).
- The token is a required parameter almost everywhere, so a caller cannot leave it out. One method in `src` gives it a default value, `DatabaseInitializer.InitializeAsync` ([Infrastructure/DatabaseInitializer.cs:17](src/Northstar.Infrastructure/Persistence/DatabaseInitializer.cs#L17)). It is called at startup, where there is no request ([Api/Program.cs:39-41](src/Northstar.Api/Program.cs#L39-L41)).
- `CancellationToken.None`, a token that is never cancelled, is passed in five places in `src`, all in [Application/ImportBatchService.cs](src/Northstar.Application/Imports/ImportBatchService.cs): the three rollbacks (lines 171, 254 and 261) and the two notifications to the queue (lines 190 and 278). These calls come after the outcome of the transaction is decided, and a request that the client has abandoned cannot cancel them.
- An `OperationCanceledException` does not say who cancelled. Where that matters, the code asks the token in an *exception filter*: `catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)` ([Infrastructure/ImportBatchWorker.cs:30](src/Northstar.Infrastructure/Messaging/ImportBatchWorker.cs#L30)). There are 6 filters, all described in [Pattern 10](#pattern-10-background-worker-with-a-queue-and-a-recovery-scan) and [Pattern 12](#pattern-12-bounded-retries-timeouts-and-exception-translation). [Pattern 12](#pattern-12-bounded-retries-timeouts-and-exception-translation) also shows a *linked* source, which combines the caller's token with a time limit for one attempt.
- `await using` appears 14 times, for scopes of the container and for database transactions ([Pattern 13](#pattern-13-persistence-behind-an-interface)).

**Why it is done this way.** Microsoft's best-practices page for ASP.NET Core gives the reason for the first half of the pattern. With asynchronous calls a small number of threads can serve many requests at the same time, because a thread that would otherwise wait for the database works on another request instead. The page advises against `Task.Wait` and `Task.Result`, and it recommends making the whole call stack asynchronous. The EF Core documentation adds a rule that explains why the calls here run one after another: a context does not support several operations in parallel, so each asynchronous call should be awaited before the next one starts. The token serves the second half. Work for a request that nobody is waiting for can stop early. The excerpt is the counterpart: work that must not be lost when its caller stops gets a token of its own. It carries out the first half of a sentence in the architecture note, "Unexpected worker failures become sanitized retryable failures" ([docs/architecture.md:54](docs/architecture.md#L54)). The processor method that it calls stores a fixed code and message and `canRetry: true` ([Application/ImportBatchProcessor.cs:173-198](src/Northstar.Application/Imports/ImportBatchProcessor.cs#L173-L198)). The second half of the sentence, "host cancellation is allowed to propagate and the lease enables later recovery", is the job of the exception filters: a cancellation at shutdown is not turned into a failure, and the batch keeps its lease until it runs out ([Pattern 16](#pattern-16-optimistic-concurrency-with-a-version-number)).

**Look up.** [`Task`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task?view=net-10.0) · [`ValueTask`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.valuetask?view=net-10.0) · [`Task.Delay`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.delay?view=net-10.0#system-threading-tasks-task-delay(system-timespan-system-threading-cancellationtoken)) · [`CancellationToken`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken?view=net-10.0) · [`CancellationToken.None`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken.none?view=net-10.0#system-threading-cancellationtoken-none) · [`CancellationToken.IsCancellationRequested`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken.iscancellationrequested?view=net-10.0#system-threading-cancellationtoken-iscancellationrequested) · [`CancellationTokenSource`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtokensource?view=net-10.0) · [`new CancellationTokenSource`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtokensource.-ctor?view=net-10.0#system-threading-cancellationtokensource-ctor) · [`CancellationTokenSource.Token`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtokensource.token?view=net-10.0#system-threading-cancellationtokensource-token) · [`CancellationTokenSource.CreateLinkedTokenSource`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtokensource.createlinkedtokensource?view=net-10.0) · [`OperationCanceledException`](https://learn.microsoft.com/en-us/dotnet/api/system.operationcanceledexception?view=net-10.0) · [`HttpContext.RequestAborted`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.httpcontext.requestaborted?view=aspnetcore-10.0#microsoft-aspnetcore-http-httpcontext-requestaborted) · [`IAsyncEnumerable`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.iasyncenumerable-1?view=net-10.0) · [`IAsyncDisposable.DisposeAsync`](https://learn.microsoft.com/en-us/dotnet/api/system.iasyncdisposable.disposeasync?view=net-10.0#system-iasyncdisposable-disposeasync) · [`ServiceProviderServiceExtensions.CreateAsyncScope`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.serviceproviderserviceextensions.createasyncscope?view=net-10.0-pp#microsoft-extensions-dependencyinjection-serviceproviderserviceextensions-createasyncscope(system-iserviceprovider)) · [`TimeSpan.FromSeconds`](https://learn.microsoft.com/en-us/dotnet/api/system.timespan.fromseconds?view=net-10.0#system-timespan-fromseconds(system-int64)) · [`async` method](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/async) · [`await`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/await) · [`await using`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/using) · [`await foreach`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/iteration-statements#await-foreach) · [`using` declaration](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/using) · [Exception filter `catch (X) when (...)`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/exception-handling-statements#a-when-exception-filter) · [Optional parameter `int x = 0`](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/named-and-optional-arguments#optional-arguments)

**Read more.** [Asynchronous programming with async and await](https://learn.microsoft.com/en-us/dotnet/csharp/asynchronous-programming/) · [Asynchronous programming scenarios](https://learn.microsoft.com/en-us/dotnet/csharp/asynchronous-programming/async-scenarios) · [Task-based asynchronous pattern (TAP)](https://learn.microsoft.com/en-us/dotnet/standard/asynchronous-programming-patterns/task-based-asynchronous-pattern-tap) · [ASP.NET Core best practices](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/best-practices?view=aspnetcore-10.0) · [Cancellation in managed threads](https://learn.microsoft.com/en-us/dotnet/standard/threading/cancellation-in-managed-threads) · [Listen for multiple cancellation requests](https://learn.microsoft.com/en-us/dotnet/standard/threading/how-to-listen-for-multiple-cancellation-requests) · [Model binding in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/mvc/models/model-binding?view=aspnetcore-10.0) · [Asynchronous programming with EF Core](https://learn.microsoft.com/en-us/ef/core/miscellaneous/async) · [Implement a DisposeAsync method](https://learn.microsoft.com/en-us/dotnet/standard/garbage-collection/implementing-disposeasync) · [Generate and consume async streams](https://learn.microsoft.com/en-us/dotnet/csharp/asynchronous-programming/generate-consume-asynchronous-stream)

### Pattern 19: Small conventions used everywhere

**What it is.** A number of small decisions are made the same way in every file. None of them is a design by itself. Together they are the reason why a file from this codebase looks familiar after the first few have been read. This section collects them, with a count for each and the place where a tool states or checks it.

**Where to find it.** Everywhere. The four excerpts show a request record and a view record ([Application/ImportBatchView.cs:3-34](src/Northstar.Application/Imports/ImportBatchView.cs#L3-L34)), a mapping method ([Application/ImportBatchMappings.cs:5-36](src/Northstar.Application/Imports/ImportBatchMappings.cs#L5-L36)), the clock ([Application/IClock.cs:3-11](src/Northstar.Application/Abstractions/IClock.cs#L3-L11)) and the rounding of money ([Domain/PayrollCalculator.cs:156](src/Northstar.Domain/PayrollRuns/PayrollCalculator.cs#L156)).

**[`src/Northstar.Application/Imports/ImportBatchView.cs`](src/Northstar.Application/Imports/ImportBatchView.cs#L3-L6)** · lines 3–6 and 27–34

<!-- x:records-view -->
```csharp
public sealed record CreateImportBatchRequest(
    DateOnly PeriodStart,
    DateOnly PeriodEnd,
    string ProviderScenario = "success");
// ... lines 7–26 omitted ...
public sealed record ImportRowView(
    Guid Id,
    Guid ImportBatchId,
    int RowNumber,
    string? ExternalTimeEntryId,
    string Status,
    string? ErrorCode,
    string? ErrorMessage);
```

**[`src/Northstar.Application/Imports/ImportBatchMappings.cs`](src/Northstar.Application/Imports/ImportBatchMappings.cs#L5-L6)** · lines 5–6 and 27–36

<!-- x:records-map -->
```csharp
internal static class ImportBatchMappings
{
    // ... lines 7–26 omitted ...
    public static ImportRowView ToView(this ImportRow row) =>
        new(
            row.Id,
            row.ImportBatchId,
            row.RowNumber,
            row.ExternalTimeEntryId,
            row.Status.ToString(),
            row.ErrorCode,
            row.ErrorMessage);
}
```

**[`src/Northstar.Application/Abstractions/IClock.cs`](src/Northstar.Application/Abstractions/IClock.cs#L3-L11)** · lines 3–11

<!-- x:clock -->
```csharp
public interface IClock
{
    DateTimeOffset UtcNow { get; }
}

public sealed class SystemClock : IClock
{
    public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;
}
```

**[`src/Northstar.Domain/PayrollRuns/PayrollCalculator.cs`](src/Northstar.Domain/PayrollRuns/PayrollCalculator.cs#L156)** · line 156

<!-- x:money -->
```csharp
private static decimal Money(decimal amount) => decimal.Round(amount, 2, MidpointRounding.AwayFromZero);
```

**How to read the code.** Records for data that crosses a boundary:

1. `public sealed record CreateImportBatchRequest(` (line 3) declares a *positional record*. The parameters in the parentheses become public properties that can only be set when the object is created. The compiler also writes the constructor, a comparison by value and a readable `ToString`. The whole type takes four lines.
2. `string ProviderScenario = "success");` (line 6) gives the last parameter a default value.
3. `public sealed record ImportRowView(` (line 27) is a *view*: the form in which an import row leaves the API. It holds seven values of the row. The status is declared as `string Status,` (line 32) and not with the enum type.
4. `public static ImportRowView ToView(this ImportRow row) =>` (line 27) turns an entity into its view. The word `this` before the first parameter makes it an *extension method*. It is called as `row.ToView()`, as if `ImportRow` had such a method. The class around it is declared with `internal static class ImportBatchMappings` (line 5), so the entity class in the Domain project knows nothing about views.
5. The body of the method is a single expression after `=>`, and it starts with `new(` without a type name. The compiler takes the type from the return type of the method ([Pattern 17](#pattern-17-entities-as-state-machines-and-pure-domain-services)).
6. `row.Status.ToString(),` (line 33) turns the enum value into its name, which is how `Status` becomes a `string`.

The clock:

7. `public interface IClock` (line 3) has one member, the current time as a `DateTimeOffset`. The services and the processor take this interface as a dependency ([Pattern 3](#pattern-3-constructor-injection-with-primary-constructors)). They read `clock.UtcNow` in ten places and pass the value into the methods of the entities ([Pattern 17](#pattern-17-entities-as-state-machines-and-pure-domain-services)).
8. `public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;` (line 10) is the implementation, registered as a singleton ([Pattern 2](#pattern-2-composition-root-and-service-registration)). In `src` the system clock is read in one other place only, the recovery scan of the worker ([Infrastructure/ImportBatchWorker.cs:66](src/Northstar.Infrastructure/Messaging/ImportBatchWorker.cs#L66)).

Money:

9. `private static decimal Money(decimal amount) => decimal.Round(amount, 2, MidpointRounding.AwayFromZero);` (line 156) is the only rounding in the codebase. It rounds to two decimal places, and a value that lies exactly in the middle goes away from zero: 2.125 becomes 2.13. Without the third argument, `decimal.Round` sends such a value to the nearest even digit, and 2.125 would become 2.12. The calculator calls `Money` five times, once for each money amount of a payroll line ([Domain/PayrollCalculator.cs:53-96](src/Northstar.Domain/PayrollRuns/PayrollCalculator.cs#L53-L96)).

The other conventions:

- Namespaces are *file-scoped*. 52 of the 54 C# files begin with a line such as `namespace Northstar.Domain.Imports;` that covers the whole file and needs no braces. The other two are the `Program.cs` files, which consist of top-level statements. `.editorconfig` asks for this form with `csharp_style_namespace_declarations = file_scoped:error` ([.editorconfig:15](.editorconfig#L15)).
- Classes are `sealed` unless there is a reason. 66 of the 68 classes and records that could be sealed are sealed, which means that no class can derive from them. The two exceptions are the `public partial class Program;` declarations at the end of the two `Program.cs` files. Two more classes are `abstract`, made to be derived from: `ApiControllerBase` and `PayrollProviderException`.
- All 22 records are positional and sealed. Records are used for request bodies, views, results and queries. Entities and services are classes.
- 18 types take their dependencies or values in a parameter list directly after the type name ([Pattern 3](#pattern-3-constructor-injection-with-primary-constructors)). That leaves few fields: `src` and `tools` have three private instance fields, `_channel`, `_options` and `_attempts`, each starting with an underscore. Static read-only fields have names in PascalCase: `ProcessingLease`, `SupportedScenarios`, `PeriodStart`.
- Mapping is written by hand, and there is no mapping library. Imports use the two `ToView` extension methods, called in four places in `ImportBatchService`. Payroll runs and wage rates use a private static method `Map` in their service ([Application/PayrollRunService.cs:235](src/Northstar.Application/PayrollRuns/PayrollRunService.cs#L235), [Application/WageRateService.cs:114](src/Northstar.Application/WageRates/WageRateService.cs#L114)).
- Enums leave the application as text, and each exit has its own means. A view gets the name through `ToString()` (7 uses). JSON bodies use `JsonStringEnumConverter` ([Api/Program.cs:11](src/Northstar.Api/Program.cs#L11), [Pattern 2](#pattern-2-composition-root-and-service-registration)). The database stores the name through `HasConversion<string>()` ([Pattern 14](#pattern-14-ef-core-model-configuration-with-the-fluent-api)).
- Two types represent time. `DateTimeOffset` (25 uses) stands for a moment, such as the time a batch was created. `DateOnly` (99 uses) stands for a calendar day, such as a work date or the start of a period. The type `DateTime` does not occur.
- Money, rates and hours are `decimal`. The types `double` and `float` do not occur. There are 80 decimal literals, written with the suffix `m` as in `1.5m` ([Domain/WageRate.cs:25](src/Northstar.Domain/WageRates/WageRate.cs#L25)).
- String comparisons name their rule. The code passes `StringComparison.Ordinal` 12 times, `StringComparison.OrdinalIgnoreCase` 6 times, `StringComparer.Ordinal` 4 times and `StringComparer.OrdinalIgnoreCase` 5 times. *Ordinal* means that the characters are compared by their numeric values, without the rules of a language or culture. No other kind of comparison is named anywhere.
- Short forms are used throughout: `var` for local variables (383 times), `new(…)` without a type name (55), collection expressions such as `[]` (30), and members whose body is one expression after `=>` (106).
- An argument whose meaning cannot be seen from its value is passed by name: `canRetry: true`, `track: false`. The scan counts 49 named arguments.
- Nullable reference types are switched on for all projects ([Directory.Build.props:7](Directory.Build.props#L7)). `string?` says that null is allowed and plain `string` says that it is not, and the compiler warns where code does not respect the difference. The next line of the file, `TreatWarningsAsErrors`, turns those warnings into errors.
- Error codes are upper-case words joined by underscores, such as `VERSION_CONFLICT`, `IMPORT_NOT_RETRYABLE` and `PROVIDER_TRANSIENT_FAILURE`. `src` and `tools` contain 36 different ones. They are the codes that [Pattern 6](#pattern-6-result-objects-mapped-to-http-responses) puts into its answers.

**Why it is done this way.** Several of these conventions are written down where a tool reads them. `.editorconfig` holds the form of the namespaces, and `Directory.Build.props` switches on nullable reference types, code-style analysis during the build and warnings as errors for every project ([section 7](#7-tooling-and-configuration) lists the settings). Others follow recommendations in the .NET documentation. Its guidance on strings says to call the overloads that state the comparison rule, and to compare ordinally when the values are identifiers and not text in a human language. Its guidance on date types describes `DateTimeOffset` as a value that identifies one point in time without ambiguity, and `DateOnly` as the type for a date without a time of day. The C# reference recommends `decimal` where the digits after the decimal point have to be exact, as in financial calculations. The clock interface makes the current time a dependency like any other, and the entity methods receive it as an argument, so their results depend only on what is passed in ([Pattern 17](#pattern-17-entities-as-state-machines-and-pure-domain-services)). .NET has had a built-in abstraction for the same purpose since .NET 8, the class `TimeProvider`. This codebase defines the small interface shown above.

**Look up.** [`DateTimeOffset`](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset?view=net-10.0) · [`DateTimeOffset.UtcNow`](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.utcnow?view=net-10.0#system-datetimeoffset-utcnow) · [`DateOnly`](https://learn.microsoft.com/en-us/dotnet/api/system.dateonly?view=net-10.0) · [`decimal.Round`](https://learn.microsoft.com/en-us/dotnet/api/system.decimal.round?view=net-10.0#system-decimal-round(system-decimal-system-int32-system-midpointrounding)) · [`MidpointRounding.AwayFromZero`](https://learn.microsoft.com/en-us/dotnet/api/system.midpointrounding?view=net-10.0) · [`StringComparison.Ordinal`](https://learn.microsoft.com/en-us/dotnet/api/system.stringcomparison?view=net-10.0) · [`StringComparison.OrdinalIgnoreCase`](https://learn.microsoft.com/en-us/dotnet/api/system.stringcomparison?view=net-10.0) · [`StringComparer.Ordinal`](https://learn.microsoft.com/en-us/dotnet/api/system.stringcomparer.ordinal?view=net-10.0#system-stringcomparer-ordinal) · [`StringComparer.OrdinalIgnoreCase`](https://learn.microsoft.com/en-us/dotnet/api/system.stringcomparer.ordinalignorecase?view=net-10.0#system-stringcomparer-ordinalignorecase) · [`Enum.ToString`](https://learn.microsoft.com/en-us/dotnet/api/system.enum.tostring?view=net-10.0#system-enum-tostring) · [`new JsonStringEnumConverter`](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.serialization.jsonstringenumconverter.-ctor?view=net-10.0#system-text-json-serialization-jsonstringenumconverter-ctor) · [`record`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/record) · [Positional record `record R(int A)`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/record#positional-syntax-for-property-and-field-definition) · [`sealed` type](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/sealed) · [Extension method `this T value`](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/extension-methods) · [`static` class](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/static-classes-and-static-class-members) · [File-scoped namespace](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/namespace) · [Primary constructor `class C(Dep dep)`](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/instance-constructors#primary-constructors) · [`var`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/declarations#implicitly-typed-local-variables) · [Target-typed `new()`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/new-operator#target-typed-new) · [Collection expression `[a, b]`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/collection-expressions) · [Expression-bodied member `=>`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/lambda-operator#expression-body-definition) · [Named argument `name: value`](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/named-and-optional-arguments#named-arguments) · [Optional parameter `int x = 0`](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/named-and-optional-arguments#optional-arguments) · [Nullable reference type `string?`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/nullable-reference-types) · [`decimal` literal `12.50m`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/floating-point-numeric-types#real-literals)

**Read more.** [Introduction to record types in C#](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/types/records) · [Tutorial: explore primary constructors](https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/tutorials/primary-constructors) · [Choose between DateTime, DateOnly, DateTimeOffset, TimeSpan, TimeOnly and TimeZoneInfo](https://learn.microsoft.com/en-us/dotnet/standard/datetime/choosing-between-datetime) · [How to use DateOnly and TimeOnly](https://learn.microsoft.com/en-us/dotnet/standard/datetime/how-to-use-dateonly-timeonly) · [What is TimeProvider?](https://learn.microsoft.com/en-us/dotnet/standard/datetime/timeprovider-overview) · [Best practices for comparing strings in .NET](https://learn.microsoft.com/en-us/dotnet/standard/base-types/best-practices-strings) · [Enums as strings](https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/customize-properties#enums-as-strings) · [Code-style rule options (.editorconfig)](https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/code-style-rule-options) · [Namespace declaration preferences (IDE0160 and IDE0161)](https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/style-rules/ide0160-ide0161) · [Nullable reference types](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/null-safety/nullable-reference-types) · [TreatWarningsAsErrors](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/compiler-options/errors-warnings#treatwarningsaserrors) · [EnforceCodeStyleInBuild](https://learn.microsoft.com/en-us/dotnet/core/project-sdk/msbuild-props#enforcecodestyleinbuild)

### Pattern 20: Tests at three levels in .NET

**What it is.** The .NET tests work at three levels, and each level starts only as much of the system as it needs. The *domain tests* create an entity or a domain service with `new` and call its methods. Nothing else runs. The *client tests* create the real provider client and give it a message handler of their own in place of the network. The *API tests* start the whole application in memory, with a real database engine and the real background worker, and talk to it over HTTP. All three use xUnit. A method marked `[Fact]` is one test, and a method marked `[Theory]` runs once for each row of data that is attached to it.

**Where to find it.** `tests/Northstar.Domain.UnitTests` holds the domain tests (17 test methods in four classes). `tests/Northstar.Api.IntegrationTests` holds the other two levels (26 test methods in three classes) and two helper classes: [IntegrationTests/CustomWebApplicationFactory.cs](tests/Northstar.Api.IntegrationTests/CustomWebApplicationFactory.cs) and [IntegrationTests/DeterministicPayrollProviderClient.cs](tests/Northstar.Api.IntegrationTests/DeterministicPayrollProviderClient.cs).

**How to read the code.** The levels are shown from the outside in, and each group of excerpts is followed by its steps. The API tests need a factory that starts the application:

**[`tests/Northstar.Api.IntegrationTests/CustomWebApplicationFactory.cs`](tests/Northstar.Api.IntegrationTests/CustomWebApplicationFactory.cs#L13-L63)** · lines 13–63

<!-- x:test-factory -->
```csharp
public sealed class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    private readonly string _connectionString =
        $"Data Source=northstar-tests-{Guid.NewGuid():N};Mode=Memory;Cache=Shared";
    private readonly SqliteConnection _anchorConnection;

    public CustomWebApplicationFactory()
    {
        _anchorConnection = new SqliteConnection(_connectionString);
        _anchorConnection.Open();
    }

    public DeterministicPayrollProviderClient PayrollProvider { get; } = new();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");
        builder.ConfigureAppConfiguration((_, configuration) =>
            configuration.AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["SeedData"] = "true",
                ["Logging:LogLevel:Default"] = "Warning",
                ["Logging:LogLevel:Microsoft.Hosting.Lifetime"] = "Warning"
            }));
        builder.ConfigureServices(services =>
        {
            services.RemoveAll<DbContextOptions<ApplicationDbContext>>();
            services.RemoveAll<ApplicationDbContext>();
            services.RemoveAll<IApplicationDbContext>();
            services.RemoveAll<IPayrollProviderClient>();

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(
                _connectionString,
                sqlite => sqlite.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));
            services.AddScoped<IApplicationDbContext>(provider =>
                provider.GetRequiredService<ApplicationDbContext>());
            services.AddSingleton(PayrollProvider);
            services.AddSingleton<IPayrollProviderClient>(provider =>
                provider.GetRequiredService<DeterministicPayrollProviderClient>());
        });
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        if (disposing)
        {
            _anchorConnection.Dispose();
        }
    }
}
```

1. `public sealed class CustomWebApplicationFactory : WebApplicationFactory<Program>` (line 13) derives from a class of the package `Microsoft.AspNetCore.Mvc.Testing`. `WebApplicationFactory<Program>` runs the `Program.cs` of the API ([Pattern 4](#pattern-4-request-pipeline-and-host-startup)) inside the test process, on a test server that exists only in memory.
2. `$"Data Source=northstar-tests-{Guid.NewGuid():N};Mode=Memory;Cache=Shared";` (line 16) is the connection string of a SQLite database that is kept in memory. `Mode=Memory` and `Cache=Shared` let several connections use the same in-memory database. The name contains a new GUID, so every factory has a database of its own.
3. `_anchorConnection.Open();` (line 22) opens one connection in the constructor and keeps it. A database of this kind exists as long as at least one connection to it is open. The connections that EF Core uses for the requests come and go, and this one holds the database until `_anchorConnection.Dispose();` (line 60) closes it.
4. `protected override void ConfigureWebHost(IWebHostBuilder builder)` (line 27) is the method that the base class calls before the application starts. What it registers is applied after the registrations of `Program.cs`, so it can replace them.
5. `builder.UseEnvironment("Testing");` (line 29) sets the name of the environment. `Program.cs` maps the OpenAPI document and switches on CORS only when the name is `Development` ([Api/Program.cs:25-29](src/Northstar.Api/Program.cs#L25-L29)), so the tests run without both.
6. `configuration.AddInMemoryCollection(new Dictionary<string, string?>` (line 31) adds configuration values from a dictionary. `["SeedData"] = "true",` (line 33) tells the application to create the tables and the demo data when it starts ([Api/Program.cs:37-42](src/Northstar.Api/Program.cs#L37-L42)). The other two entries reduce the logging to warnings.
7. `services.RemoveAll<DbContextOptions<ApplicationDbContext>>();` (line 39) is the first of four calls that take registrations out of the container: the options of the context, the context, the interface `IApplicationDbContext` and the provider client. The lines after them register the replacements.
8. `services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(` (line 44) registers the same context class again, now with the in-memory connection string. The context, the model configuration ([Pattern 14](#pattern-14-ef-core-model-configuration-with-the-fluent-api)) and the queries are the real ones. SQLite is also the database of a development run. The difference is that the data is kept in memory and not in a file.
9. `public DeterministicPayrollProviderClient PayrollProvider { get; } = new();` (line 25) creates a stand-in for the payroll provider. `services.AddSingleton(PayrollProvider);` (line 49) registers this one object, and `services.AddSingleton<IPayrollProviderClient>(provider =>` (line 50) makes the interface resolve to the same object. A test can therefore reach the object that the application uses, through the property of the factory.

One API test, with two of its helper methods:

**[`tests/Northstar.Api.IntegrationTests/ImportBatchesApiTests.cs`](tests/Northstar.Api.IntegrationTests/ImportBatchesApiTests.cs#L15-L43)** · lines 15–43

<!-- x:test-api -->
```csharp
[Fact]
public async Task Create_ReturnsAccepted_ThenWorkerCompletesTheBatch()
{
    using var factory = new CustomWebApplicationFactory();
    using var client = TenantClient(factory, DemoDataIds.OrganizationA);

    using var response = await CreateImportAsync(client, "success", "success-accepted");
    var accepted = await ReadBatchAsync(response);

    Assert.Equal(HttpStatusCode.Accepted, response.StatusCode);
    Assert.NotNull(response.Headers.Location);
    Assert.Contains(accepted.Id.ToString(), response.Headers.Location.ToString(), StringComparison.OrdinalIgnoreCase);

    var completed = await WaitForTerminalBatchAsync(client, accepted.Id);

    Assert.Equal("Completed", completed.Status);
    Assert.Equal(2, completed.TotalRows);
    Assert.Equal(2, completed.ImportedRows);
    Assert.Equal(0, completed.ErrorRows);
    Assert.Equal(0, completed.DuplicateRows);
    Assert.Equal(1, completed.AttemptCount);
    Assert.Equal(1, factory.PayrollProvider.GetCallCount("success"));

    using var listResponse = await client.GetAsync(new Uri("/api/import-batches", UriKind.Relative));
    var batches = await listResponse.Content.ReadFromJsonAsync<List<ImportBatchView>>();
    Assert.Equal(HttpStatusCode.OK, listResponse.StatusCode);
    Assert.NotNull(batches);
    Assert.Equal(accepted.Id, Assert.Single(batches).Id);
}
```

**[`tests/Northstar.Api.IntegrationTests/ImportBatchesApiTests.cs`](tests/Northstar.Api.IntegrationTests/ImportBatchesApiTests.cs#L322-L328)** · lines 322–328

<!-- x:test-tenant-client -->
```csharp
private static HttpClient TenantClient(CustomWebApplicationFactory factory, Guid organizationId)
{
    var client = factory.CreateClient();
    client.DefaultRequestHeaders.Add("X-Organization-Id", organizationId.ToString());
    client.DefaultRequestHeaders.Add("X-User-Role", "PayrollManager");
    return client;
}
```

**[`tests/Northstar.Api.IntegrationTests/ImportBatchesApiTests.cs`](tests/Northstar.Api.IntegrationTests/ImportBatchesApiTests.cs#L376-L400)** · lines 376–400

<!-- x:test-wait -->
```csharp
private static async Task<ImportBatchView> WaitForTerminalBatchAsync(
    HttpClient client,
    Guid batchId)
{
    ImportBatchView? lastBatch = null;

    for (var attempt = 0; attempt < 120; attempt++)
    {
        using var response = await client.GetAsync(
            new Uri($"/api/import-batches/{batchId}", UriKind.Relative));
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        lastBatch = await ReadBatchAsync(response);

        if (lastBatch.Status is not "Pending" and not "Processing")
        {
            return lastBatch;
        }

        var delayMilliseconds = Math.Min(10 + (attempt * 5), 50);
        await Task.Delay(TimeSpan.FromMilliseconds(delayMilliseconds));
    }

    throw new TimeoutException(
        $"Import batch {batchId} did not reach a terminal state. Last status: {lastBatch?.Status ?? "unknown"}.");
}
```

10. `[Fact]` (line 15) marks the method as a test. `public async Task Create_ReturnsAccepted_ThenWorkerCompletesTheBatch()` (line 16) returns a `Task`, so the test can use `await`. The name reads as a sentence about the behaviour that is expected.
11. `using var factory = new CustomWebApplicationFactory();` (line 18) gives this test an application and a database of its own. The `using` declaration disposes both when the test ends.
12. `using var client = TenantClient(factory, DemoDataIds.OrganizationA);` (line 19) calls the helper in the second excerpt. There, `var client = factory.CreateClient();` (line 324) returns an `HttpClient` that sends its requests to the test server, and `client.DefaultRequestHeaders.Add("X-Organization-Id", organizationId.ToString());` (line 325) adds the first of the two headers that stand for the signed-in user ([Pattern 7](#pattern-7-tenant-scoping-from-header-to-query)).
13. `using var response = await CreateImportAsync(client, "success", "success-accepted");` (line 21) posts a request to `/api/import-batches` with the scenario `success` and the `Idempotency-Key` header `success-accepted` ([IntegrationTests/ImportBatchesApiTests.cs:330-346](tests/Northstar.Api.IntegrationTests/ImportBatchesApiTests.cs#L330-L346)). The request goes through routing, model binding, the controller, the service and the database, as a request from a browser would.
14. `Assert.Equal(HttpStatusCode.Accepted, response.StatusCode);` (line 24) checks that the answer is `202` ([Pattern 9](#pattern-9-accept-now-process-later)). In `Assert.Equal` the expected value comes first and the actual value second.
15. `var completed = await WaitForTerminalBatchAsync(client, accepted.Id);` (line 28) waits for the background worker, which runs inside the test server as it does in the real application ([Pattern 10](#pattern-10-background-worker-with-a-queue-and-a-recovery-scan)). The third excerpt shows how. `for (var attempt = 0; attempt < 120; attempt++)` (line 382) asks for the batch up to 120 times, and `if (lastBatch.Status is not "Pending" and not "Processing")` (line 389) returns as soon as the status is a final one.
16. `var delayMilliseconds = Math.Min(10 + (attempt * 5), 50);` (line 394) sets the pause between two requests: 10 milliseconds at first, then longer, and never more than 50. When the limit of 120 requests is reached, `throw new TimeoutException(` (line 398) fails the test with the last status in the message.
17. `Assert.Equal(1, factory.PayrollProvider.GetCallCount("success"));` (line 36) reads the stand-in through the factory and expects exactly one call to the provider. The lines before it check the status, the four row counts and the number of attempts.
18. `Assert.Equal(accepted.Id, Assert.Single(batches).Id);` (line 42) uses a property of `Assert.Single`: it fails unless the collection has exactly one element, and it returns that element. The test expects the list for this organization to hold the new batch and nothing else.

The client tests replace the network:

**[`tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs`](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L13-L29)** · lines 13–29

<!-- x:test-stub-use -->
```csharp
[Fact]
public async Task GetTimeEntriesAsync_RetriesTwoRateLimitsThenReturnsRecords()
{
    var handler = new StubHttpMessageHandler((call, _, _) =>
        Task.FromResult(call < 3
            ? new HttpResponseMessage(HttpStatusCode.TooManyRequests)
            : JsonResponse(SuccessPayload)));
    using var httpClient = CreateHttpClient(handler);
    var client = CreateClient(httpClient, maxAttempts: 3);

    var records = await client.GetTimeEntriesAsync(Request(), CancellationToken.None);

    Assert.Equal(3, handler.CallCount);
    var record = Assert.Single(records);
    Assert.Equal("A-TIME-IMPORT-001", record.ExternalTimeEntryId);
    Assert.Equal("A-EMP-001", record.ExternalEmployeeId);
}
```

**[`tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs`](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L236-L252)** · lines 236–252

<!-- x:test-stub -->
```csharp
private sealed class StubHttpMessageHandler(
    Func<int, HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> sendAsync)
    : HttpMessageHandler
{
    private readonly Func<int, HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> _sendAsync = sendAsync;
    private int _callCount;

    public int CallCount => Volatile.Read(ref _callCount);

    protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var call = Interlocked.Increment(ref _callCount);
        return _sendAsync(call, request, cancellationToken);
    }
}
```

19. `private sealed class StubHttpMessageHandler(` (line 236) is declared with `: HttpMessageHandler` (line 238) as its base class. A *message handler* is the object inside an `HttpClient` that actually sends a request. The class overrides `protected override Task<HttpResponseMessage> SendAsync(` (line 245), and whatever this method returns is what the client receives as the response. No network is involved.
20. The handler gets its behaviour from the test, as a function with three parameters: the number of the call, the request and the token. `var call = Interlocked.Increment(ref _callCount);` (line 249) counts the calls, and `public int CallCount => Volatile.Read(ref _callCount);` (line 243) lets the test read the count. `Interlocked` and `Volatile` keep the counter correct when more than one thread uses it.
21. `var handler = new StubHttpMessageHandler((call, _, _) =>` (line 16) passes that function as a lambda. The two `_` are *discards*, parameters that this test does not need. `Task.FromResult(call < 3` (line 17) wraps the response in a task that is already complete: status `429` for the first two calls and a JSON body for the third.
22. `var client = CreateClient(httpClient, maxAttempts: 3);` (line 21) creates the real `FakePayrollProviderClient` with `new`, from the test's `HttpClient` and from options built with `Options.Create` ([IntegrationTests/FakePayrollProviderClientTests.cs:156-175](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L156-L175), [Pattern 11](#pattern-11-typed-httpclient-with-validated-options)). The helper sets `BaseRetryDelayMilliseconds = 0`, so the pauses between the attempts ([Pattern 12](#pattern-12-bounded-retries-timeouts-and-exception-translation)) take no time.
23. `var records = await client.GetTimeEntriesAsync(Request(), CancellationToken.None);` (line 23) is the call under test. `Assert.Equal(3, handler.CallCount);` (line 25) shows that the client tried three times, and `var record = Assert.Single(records);` (line 26) that the third answer was read into one record.

The domain tests call the code directly:

**[`tests/Northstar.Domain.UnitTests/ImportBatchStateTests.cs`](tests/Northstar.Domain.UnitTests/ImportBatchStateTests.cs#L83-L110)** · lines 83–110

<!-- x:test-theory -->
```csharp
[Theory]
[InlineData(-1, 0, 0, 0)]
[InlineData(1, -1, 1, 1)]
[InlineData(1, 1, -1, 1)]
[InlineData(1, 1, 1, -1)]
[InlineData(3, 1, 0, 1)]
public void Complete_RejectsNegativeOrInconsistentCounts(
    int totalRows,
    int importedRows,
    int errorRows,
    int duplicateRows)
{
    var batch = Batch();
    Assert.True(batch.BeginProcessing(Now, TimeSpan.FromMinutes(5)));

    Assert.Throws<ArgumentOutOfRangeException>(() => batch.Complete(
        totalRows,
        importedRows,
        errorRows,
        duplicateRows,
        Now.AddMinutes(1)));

    Assert.Equal(ImportBatchStatus.Processing, batch.Status);
    Assert.Equal(0, batch.TotalRows);
    Assert.Equal(0, batch.ImportedRows);
    Assert.Equal(0, batch.ErrorRows);
    Assert.Equal(0, batch.DuplicateRows);
}
```

**[`tests/Northstar.Domain.UnitTests/WageRateSelectorTests.cs`](tests/Northstar.Domain.UnitTests/WageRateSelectorTests.cs#L30-L40)** · lines 30–40

<!-- x:test-unit -->
```csharp
[Fact]
public void Select_PrefersProjectRateOverStateRate()
{
    var state = Rate(WageRateScope.State, new DateOnly(2026, 1, 1), null);
    var project = Rate(WageRateScope.ProjectClassification, new DateOnly(2026, 1, 1), null);

    var result = _selector.Select(Query(new DateOnly(2026, 8, 24)), [state, project]);

    Assert.Equal(RateSelectionStatus.Selected, result.Status);
    Assert.Same(project, result.Rate);
}
```

24. `[Theory]` (line 83) marks a test that has parameters. Each `[InlineData(…)]` line is one set of arguments, and xUnit runs the method once for each line. The five lines here are five sets of row counts that `Complete` has to refuse: four with a negative number and one, `[InlineData(3, 1, 0, 1)]` (line 88), in which the parts do not add up to the total.
25. `Assert.True(batch.BeginProcessing(Now, TimeSpan.FromMinutes(5)));` (line 96) first brings the batch into the status `Processing`. `Now` is a fixed value, a static field of the test class ([UnitTests/ImportBatchStateTests.cs:7](tests/Northstar.Domain.UnitTests/ImportBatchStateTests.cs#L7)). The entity takes the time as a parameter ([Pattern 17](#pattern-17-entities-as-state-machines-and-pure-domain-services)), so the test needs no clock.
26. `Assert.Throws<ArgumentOutOfRangeException>(() => batch.Complete(` (line 98) expects the call inside the lambda to throw this type of exception. The lines after it check that the batch is unchanged: `Assert.Equal(ImportBatchStatus.Processing, batch.Status);` (line 105) and four counts that are still zero.
27. `public void Select_PrefersProjectRateOverStateRate()` (line 31) has the usual shape of a test, in three parts separated by blank lines. The first part prepares two rates. The second makes the one call under test: `var result = _selector.Select(Query(new DateOnly(2026, 8, 24)), [state, project]);` (line 36). The third checks the result. The three parts are often called *arrange*, *act* and *assert*.
28. `Assert.Same(project, result.Rate);` (line 39) checks that the selector returned the very object that the test created, and not only an equal one.

The seven test classes:

| Test class | Level | Test methods | Subject |
|---|---|---|---|
| `WageRateSelectorTests` | Domain | 5 | Date boundaries, precedence of scopes, priority, ambiguous and missing rates |
| `PayrollCalculatorTests` | Domain | 3 | The rate for each work date, a missing rate, overtime and rounding |
| `PayrollRunStateTests` | Domain | 3 | The status after a calculation, approval, and a stale version |
| `ImportBatchStateTests` | Domain | 6 | Claiming with a lease, completing, retrying |
| `FakePayrollProviderClientTests` | Client | 8 | Retries, malformed answers, paging, cancellation, timeouts |
| `ImportBatchesApiTests` | API | 12 | The import from request to imported rows, idempotency, row errors, tenants, failures and retry |
| `PayrollRunsApiTests` | API | 6 | Health, calculation, idempotency, tenants, the path to approval |

The pieces around it:

- There are 41 `[Fact]` methods and 2 `[Theory]` methods with 7 data rows. A test run lists each data row as a test of its own.

- xUnit creates a new object of the test class for every test. A field such as `_selector` ([UnitTests/WageRateSelectorTests.cs:7](tests/Northstar.Domain.UnitTests/WageRateSelectorTests.cs#L7)) is therefore new each time. No test class here has a constructor or a fixture that shares objects between tests.
- The stand-in for the provider, `DeterministicPayrollProviderClient`, implements `IPayrollProviderClient` and makes no HTTP call. It chooses its answer from the name of the scenario in a `switch` expression, returns data with `Task.FromResult` and failures with `Task.FromException`, and counts the calls for each scenario in a `ConcurrentDictionary` ([IntegrationTests/DeterministicPayrollProviderClient.cs:18-45](tests/Northstar.Api.IntegrationTests/DeterministicPayrollProviderClient.cs#L18-L45)). For the scenario `transient` it fails on the first call and succeeds on the next.
- All stand-ins are small classes written by hand: this client, `StubHttpMessageHandler` and a stream that never delivers its content, `StallingJsonStream` ([IntegrationTests/FakePayrollProviderClientTests.cs:254](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L254)). No project references a mocking library.
- Some API tests look at the database directly or change the demo data before they send a request. Six helper methods open a scope on `factory.Services` and ask it for the `ApplicationDbContext` ([IntegrationTests/ImportBatchesApiTests.cs:402-474](tests/Northstar.Api.IntegrationTests/ImportBatchesApiTests.cs#L402-L474)).
- Two tests ask for a resource of one organization with the client of another and expect `404` ([IntegrationTests/ImportBatchesApiTests.cs:246-264](tests/Northstar.Api.IntegrationTests/ImportBatchesApiTests.cs#L246-L264), [IntegrationTests/PayrollRunsApiTests.cs:60-69](tests/Northstar.Api.IntegrationTests/PayrollRunsApiTests.cs#L60-L69), [Pattern 7](#pattern-7-tenant-scoping-from-header-to-query)).
- One client test covers cancellation. Its handler waits without end on `Task.Delay(Timeout.InfiniteTimeSpan, cancellationToken)`, the test cancels its `CancellationTokenSource`, and `Assert.ThrowsAnyAsync<OperationCanceledException>` expects the cancellation to come out of the client after one call ([IntegrationTests/FakePayrollProviderClientTests.cs:101-122](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L101-L122), [Pattern 18](#pattern-18-async-all-the-way-with-cancellation)).
- The version check of [Pattern 16](#pattern-16-optimistic-concurrency-with-a-version-number) has a domain test: `Approve` with an old version returns `Conflict` with the code `VERSION_CONFLICT`, and the status of the run stays as it was ([UnitTests/PayrollRunStateTests.cs:37-49](tests/Northstar.Domain.UnitTests/PayrollRunStateTests.cs#L37-L49)).
- `WebApplicationFactory<Program>` needs a `Program` class that the test project can see. That is the reason for the last line of the API's `Program.cs` ([Pattern 4](#pattern-4-request-pipeline-and-host-startup)).
- Both test projects import the namespace `Xunit` for all their files in the project file, so no test file needs a `using Xunit;` line. [Section 7](#7-tooling-and-configuration) lists the test packages.

**Why it is done this way.** The testing note of the repository begins with the rule: "Use the cheapest boundary that proves the risk" ([docs/testing-strategy.md:3](docs/testing-strategy.md#L3)). A rule of the domain is proved where that is cheapest, in a test that needs no host and no database. The application is started only for what cannot be shown in another way, and the note lists it: "API integration tests use the real ASP.NET Core pipeline, a background worker, a deterministic provider fake, and SQLite relational constraints." The choice of database is deliberate as well: "The in-memory EF provider is intentionally not used." The EF Core documentation explains the difference. Its in-memory provider is not a relational database and does not support transactions, and the documentation discourages its use as a stand-in. SQLite in memory is itself a full relational database and behaves more like a production database, and the ASP.NET Core documentation names the SQLite provider as the recommended choice for in-memory tests. The same pages add that SQLite still differs from a production database such as SQL Server, and the note names what is left open: "SQL Server container tests are the next addition for provider-specific query translation, precision, row-version semantics, migrations, and locking." Each test has its own data, as the README says: "The API integration tests use a fresh SQLite in-memory database for every test." No test can see what another one wrote. The waiting loop follows one more rule of the note: "Fixed one-shot delays are not part of the asynchronous test strategy." and "API tests poll to a bounded terminal condition" ([docs/testing-strategy.md:13](docs/testing-strategy.md#L13)).

**Look up.** [`WebApplicationFactory`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.testing.webapplicationfactory-1?view=aspnetcore-10.0) · [`WebApplicationFactory.ConfigureWebHost`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.testing.webapplicationfactory-1.configurewebhost?view=aspnetcore-10.0#microsoft-aspnetcore-mvc-testing-webapplicationfactory-1-configurewebhost(microsoft-aspnetcore-hosting-iwebhostbuilder)) · [`WebApplicationFactory.CreateClient`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.testing.webapplicationfactory-1.createclient?view=aspnetcore-10.0#microsoft-aspnetcore-mvc-testing-webapplicationfactory-1-createclient) · [`WebApplicationFactory.Services`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.testing.webapplicationfactory-1.services?view=aspnetcore-10.0#microsoft-aspnetcore-mvc-testing-webapplicationfactory-1-services) · [`HostingAbstractionsWebHostBuilderExtensions.UseEnvironment`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.hosting.hostingabstractionswebhostbuilderextensions.useenvironment?view=aspnetcore-10.0#microsoft-aspnetcore-hosting-hostingabstractionswebhostbuilderextensions-useenvironment(microsoft-aspnetcore-hosting-iwebhostbuilder-system-string)) · [`MemoryConfigurationBuilderExtensions.AddInMemoryCollection`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.configuration.memoryconfigurationbuilderextensions.addinmemorycollection?view=net-10.0-pp#microsoft-extensions-configuration-memoryconfigurationbuilderextensions-addinmemorycollection(microsoft-extensions-configuration-iconfigurationbuilder-system-collections-generic-ienumerable((system-collections-generic-keyvaluepair((system-string-system-string)))))) · [`ServiceCollectionDescriptorExtensions.RemoveAll`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.extensions.servicecollectiondescriptorextensions.removeall?view=net-10.0-pp#microsoft-extensions-dependencyinjection-extensions-servicecollectiondescriptorextensions-removeall-1(microsoft-extensions-dependencyinjection-iservicecollection)) · [`SqliteConnection`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.data.sqlite.sqliteconnection?view=msdata-sqlite-10.0.0) · [`SqliteDbContextOptionsBuilderExtensions.UseSqlite`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.sqlitedbcontextoptionsbuilderextensions.usesqlite?view=efcore-10.0#microsoft-entityframeworkcore-sqlitedbcontextoptionsbuilderextensions-usesqlite(microsoft-entityframeworkcore-dbcontextoptionsbuilder-system-string-system-action((microsoft-entityframeworkcore-infrastructure-sqlitedbcontextoptionsbuilder)))) · [`HttpMessageHandler`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpmessagehandler?view=net-10.0) · [`HttpMessageHandler.SendAsync`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpmessagehandler.sendasync?view=net-10.0#system-net-http-httpmessagehandler-sendasync(system-net-http-httprequestmessage-system-threading-cancellationtoken)) · [`Interlocked.Increment`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.interlocked.increment?view=net-10.0#system-threading-interlocked-increment(system-int32@)) · [`Volatile.Read`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.volatile.read?view=net-10.0#system-threading-volatile-read(system-int32@)) · [`Options.Create`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.options.options.create?view=net-10.0-pp#microsoft-extensions-options-options-create-1(-0)) · [`Task.FromResult`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.fromresult?view=net-10.0#system-threading-tasks-task-fromresult-1(-0)) · [`Task.FromException`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.fromexception?view=net-10.0#system-threading-tasks-task-fromexception-1(system-exception)) · [`JsonContent.Create`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.json.jsoncontent.create?view=net-10.0#system-net-http-json-jsoncontent-create-1(-0-system-net-http-headers-mediatypeheadervalue-system-text-json-jsonserializeroptions)) · [`HttpContentJsonExtensions.ReadFromJsonAsync`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.json.httpcontentjsonextensions.readfromjsonasync?view=net-10.0#system-net-http-json-httpcontentjsonextensions-readfromjsonasync-1(system-net-http-httpcontent-system-threading-cancellationtoken)) · [`FactAttribute`](https://github.com/xunit/xunit/blob/v2-2.9.3/src/xunit.core/FactAttribute.cs#L13) · [`TheoryAttribute`](https://github.com/xunit/xunit/blob/v2-2.9.3/src/xunit.core/TheoryAttribute.cs#L16) · [`InlineDataAttribute`](https://github.com/xunit/xunit/blob/v2-2.9.3/src/xunit.core/InlineDataAttribute.cs#L14) · [`Assert.Equal`](https://github.com/xunit/assert.xunit/blob/v2-2.9.3/EqualityAsserts.cs#L81) · [`Assert.Single`](https://github.com/xunit/assert.xunit/blob/v2-2.9.3/CollectionAsserts.cs#L653) · [`Assert.Same`](https://github.com/xunit/assert.xunit/blob/v2-2.9.3/IdentityAsserts.cs#L44) · [`Assert.Throws`](https://github.com/xunit/assert.xunit/blob/v2-2.9.3/ExceptionAsserts.cs#L93) · [`Assert.ThrowsAsync`](https://github.com/xunit/assert.xunit/blob/v2-2.9.3/ExceptionAsserts.cs#L269) · [`Assert.ThrowsAnyAsync`](https://github.com/xunit/assert.xunit/blob/v2-2.9.3/ExceptionAsserts.cs#L248) · [Attribute `[Name]`](https://learn.microsoft.com/en-us/dotnet/csharp/advanced-topics/reflection-and-attributes/) · [`using` declaration](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/using) · [Lambda `x => ...`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/lambda-expressions) · [`not` pattern](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/patterns#logical-patterns) · [`and` pattern](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/patterns#logical-patterns) · [`switch` expression](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/switch-expression) · [Case guard `when`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/switch-expression#case-guards) · [Raw string literal `"""..."""`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/tokens/raw-string) · [Collection expression `[a, b]`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/collection-expressions)

**Read more.** [Integration tests in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-10.0) · [Choosing a testing strategy](https://learn.microsoft.com/en-us/ef/core/testing/choosing-a-testing-strategy) · [Testing without your production database system: SQLite in-memory](https://learn.microsoft.com/en-us/ef/core/testing/testing-without-the-database#sqlite-in-memory) · [Microsoft.Data.Sqlite in-memory databases](https://learn.microsoft.com/en-us/dotnet/standard/data/sqlite/in-memory-databases) · [Configuration: memory provider](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-10.0#memory-configuration-provider) · [Use multiple environments in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/environments?view=aspnetcore-10.0) · [Getting started with xUnit.net v2](https://xunit.net/docs/getting-started/v2/getting-started) · [xUnit.net: shared context between tests](https://xunit.net/docs/shared-context) · [Unit testing best practices](https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-best-practices) · [dotnet test](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-test) · [Microsoft.AspNetCore.Mvc.Testing 10.0.11 on NuGet](https://www.nuget.org/packages/Microsoft.AspNetCore.Mvc.Testing/10.0.11) · [xunit 2.9.3 on NuGet](https://www.nuget.org/packages/xunit/2.9.3)

### Pattern 21: Minimal API endpoints in the fake provider

**What it is.** The payroll provider that the API calls is a second small web application in the same repository. It is written with *minimal APIs*: an endpoint is a route and a lambda, mapped directly on the application object, and no controller class is involved. ASP.NET Core fills the parameters of the lambda from the request and from the container, and it turns the `IResult` that the lambda returns into the HTTP response. The whole application is one file of 307 lines.

**Where to find it.** [FakeProvider/Program.cs](tools/Northstar.FakePayrollProvider/Program.cs). The two endpoints are mapped at lines 14 and 16 to 87. The functions and types that they use follow after `app.Run();`.

**[`tools/Northstar.FakePayrollProvider/Program.cs`](tools/Northstar.FakePayrollProvider/Program.cs#L5-L25)** · lines 5–25 and 83–89

<!-- x:minimal-api -->
```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProblemDetails();
builder.Services.AddSingleton<TransientScenarioState>();

var app = builder.Build();

app.UseExceptionHandler();

app.MapGet("/health", () => Results.Ok(new { status = "Healthy" }));

app.MapGet(
        "/api/time-entries",
        async (HttpContext context,
            string organizationReference,
            DateOnly periodStart,
            DateOnly periodEnd,
            string? cursor,
            string? scenario,
            TransientScenarioState transientState) =>
        {
        // ... lines 26–82 omitted ...
        })
    .WithName("GetTimeEntries")
    .Produces<TimeEntryPage>()
    .ProducesProblem(StatusCodes.Status400BadRequest)
    .ProducesProblem(StatusCodes.Status429TooManyRequests);

app.Run();
```

**[`tools/Northstar.FakePayrollProvider/Program.cs`](tools/Northstar.FakePayrollProvider/Program.cs#L91-L107)** · lines 91–107

<!-- x:minimal-api-switch -->
```csharp
static IResult CreateSuccessResponse(
    string organizationReference,
    string? cursor,
    DateOnly periodStart,
    DateOnly periodEnd) =>
    cursor switch
    {
        null or "" => Results.Ok(TimeEntryFixtures.SuccessPageOne(organizationReference, periodStart)),
        "page-2" => Results.Ok(TimeEntryFixtures.SuccessPageTwo(
            organizationReference,
            periodStart,
            periodEnd)),
        _ => Results.Problem(
            title: "Provider cursor is invalid.",
            detail: "The supplied synthetic pagination cursor is not recognized.",
            statusCode: StatusCodes.Status400BadRequest)
    };
```

**How to read the code.** The application and its two endpoints:

1. `var builder = WebApplication.CreateBuilder(args);` (line 5) starts in the same way as the API ([Pattern 2](#pattern-2-composition-root-and-service-registration)). Two services are registered. One of them, `builder.Services.AddSingleton<TransientScenarioState>();` (line 8), is the only state that the application keeps between requests.
2. `app.UseExceptionHandler();` (line 12) is the only middleware. Together with `AddProblemDetails()` it turns an exception that nothing has caught into a problem-details answer ([Pattern 4](#pattern-4-request-pipeline-and-host-startup)).
3. `app.MapGet("/health", () => Results.Ok(new { status = "Healthy" }));` (line 14) is a complete endpoint in one line: the HTTP method, the route, and a lambda that returns status `200` with a small JSON object.
4. `"/api/time-entries",` (line 17) is the route of the second endpoint, and `async (HttpContext context,` (line 18) begins its lambda. The parameter list of the lambda is the contract of the endpoint, because the framework decides from the type of each parameter where its value comes from.
5. `HttpContext` is one of a few special types that the framework supplies itself. `string organizationReference,` (line 19) and `DateOnly periodStart,` (line 20) are simple types. The route has no placeholder with these names, so their values are read from the query string: `?organizationReference=…&periodStart=2026-08-24`.
6. Both are required, because their types are not nullable. When one is missing, or when a date cannot be parsed, the framework answers `400` and the lambda does not run. `string? cursor,` (line 22) is nullable and therefore optional.
7. `TransientScenarioState transientState) =>` (line 24) has a type that is registered in the container, so the framework takes the value from there. A lambda has no constructor, and this is how a minimal endpoint receives its dependencies ([Pattern 3](#pattern-3-constructor-injection-with-primary-constructors)).
8. `.WithName("GetTimeEntries")` (line 84), `.Produces<TimeEntryPage>()` (line 85) and `.ProducesProblem(StatusCodes.Status400BadRequest)` (line 86) attach metadata to the endpoint: a name and the answers that it can give. The metadata describes the endpoint and does not change what it does.
9. `app.Run();` (line 89) starts the server and returns when the application is shut down. The two local functions and the types follow this line. In a file of top-level statements, the declarations of types must come after all statements.

A function that builds the answer:

10. `static IResult CreateSuccessResponse(` (line 91) is a *local function*: a function declared inside another one. Here the other one is the hidden entry-point method that the compiler builds from the top-level statements. `static` means that it cannot use the variables around it and works only with its parameters. `IResult` is the common type of everything that the methods of `Results` produce.
11. `cursor switch` (line 96) begins a *switch expression*, which chooses a value by comparing the cursor with patterns. `null or "" => Results.Ok(TimeEntryFixtures.SuccessPageOne(organizationReference, periodStart)),` (line 98) covers a request without a cursor. `null or ""` is an *or pattern*, and `Results.Ok(value)` answers `200` with the value written as JSON.
12. `"page-2" => Results.Ok(TimeEntryFixtures.SuccessPageTwo(` (line 99) answers the cursor that the first page handed out. The client follows this cursor ([Pattern 11](#pattern-11-typed-httpclient-with-validated-options)).
13. `_ => Results.Problem(` (line 103) is the arm for every other value. `Results.Problem` builds a problem-details body from the named arguments, and `statusCode: StatusCodes.Status400BadRequest)` (line 106) sets the status.

The pieces around it:

- The body of the lambda ([FakeProvider/Program.cs:26-82](tools/Northstar.FakePayrollProvider/Program.cs#L26-L82)) first checks its input and answers `400` for an empty organization reference or for a period that ends before it starts. Then it reads the scenario, with `success` as the default, and chooses the answer in a switch expression over six names:

| Scenario | Answer |
|---|---|
| `success` | Two pages with one row each. The first page carries the cursor `page-2`. |
| `mixed` | One page with four rows: a valid row, a row that repeats its external id, a row for an unknown employee and a row with negative hours. |
| `malformed` | Status `200` with JSON text that breaks off in the middle. |
| `permanent` | A problem with status `400`. |
| `transient` | A problem with status `429` and the header `Retry-After: 1` for the first two attempts, then the data. |
| `timeout` | A pause of 15 seconds before the data. |

- The six names are `const` fields of the static class `ProviderScenarios` ([FakeProvider/Program.cs:135-143](tools/Northstar.FakePayrollProvider/Program.cs#L135-L143)). The arms of a switch need constant values, and a `const` is one.
- The scenarios match the two kinds of failure that the client tells apart ([Pattern 12](#pattern-12-bounded-retries-timeouts-and-exception-translation)). `429` is retried, `400` and the broken JSON are not, and the pause of 15 seconds is longer than the 10 seconds that the client allows for one attempt ([Api/appsettings.json:13](src/Northstar.Api/appsettings.json#L13)).
- `TransientScenarioState` counts the attempts for each request key in a `ConcurrentDictionary` ([FakeProvider/Program.cs:145-153](tools/Northstar.FakePayrollProvider/Program.cs#L145-L153)). It is a singleton, so the count is still there when the next request arrives, and the dictionary type is safe for requests that run at the same time. After the third attempt the count begins again at one.
- For the throttled answer, the function sets the header on the response itself, `context.Response.Headers.RetryAfter = "1";`, and passes the attempt number to `Results.Problem` as an extension member of the body ([FakeProvider/Program.cs:109-133](tools/Northstar.FakePayrollProvider/Program.cs#L109-L133)).
- The pause is written as `await Task.Delay(TimeSpan.FromSeconds(15), context.RequestAborted);` ([FakeProvider/Program.cs:45](tools/Northstar.FakePayrollProvider/Program.cs#L45)). This is the one place in the codebase that reads `HttpContext.RequestAborted` directly. When the caller gives up, the token is cancelled and the wait ends ([Pattern 18](#pattern-18-async-all-the-way-with-cancellation)).
- The data comes from the static class `TimeEntryFixtures` ([FakeProvider/Program.cs:155-291](tools/Northstar.FakePayrollProvider/Program.cs#L155-L291)). The answers are two positional records, `TimeEntryPage` and `TimeEntryRecord` ([FakeProvider/Program.cs:293-305](tools/Northstar.FakePayrollProvider/Program.cs#L293-L305)), and the external ids are derived from the organization reference and the date. The same request therefore always gets the same rows.
- The API itself has one minimal endpoint, `/health` ([Api/Program.cs:34](src/Northstar.Api/Program.cs#L34)), next to its 11 controller actions ([Pattern 5](#pattern-5-thin-attribute-routed-controllers)). The two styles can be used side by side in one application.
- The README starts the provider with `dotnet run --project tools/Northstar.FakePayrollProvider --urls http://localhost:5130` ([README.md:32](README.md#L32)). That address is the `BaseUrl` in the settings of the API ([Api/appsettings.json:10](src/Northstar.Api/appsettings.json#L10)). The Playwright configuration starts it as well and waits for `/health` to answer ([Pattern 26](#pattern-26-frontend-tests-with-vitest-and-playwright)).
- The project file names the Web SDK and the target framework and nothing else. It references no package.

**Why it is done this way.** The provider stands in for a system that the team does not own. Because it is a real HTTP server, the typed client, its time limits and its retries ([Pattern 11](#pattern-11-typed-httpclient-with-validated-options), [Pattern 12](#pattern-12-bounded-retries-timeouts-and-exception-translation)) run against real requests on a developer machine and in the end-to-end tests. The README sums up what it offers: "deterministic success, mixed, throttling, malformed, permanent-failure, and timeout scenarios" ([README.md:116](README.md#L116)). *Deterministic* is the important word. An answer depends only on the request and on the attempt counter, so a test or a person at the keyboard can ask for a failure by name and get it every time. The ASP.NET Core documentation describes minimal APIs as a way to build HTTP endpoints with little code and configuration, without controllers. That suits an application with two routes.

**Look up.** [`WebApplication.CreateBuilder`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.builder.webapplication.createbuilder?view=aspnetcore-10.0#microsoft-aspnetcore-builder-webapplication-createbuilder(system-string())) · [`EndpointRouteBuilderExtensions.MapGet`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.builder.endpointroutebuilderextensions.mapget?view=aspnetcore-10.0#microsoft-aspnetcore-builder-endpointroutebuilderextensions-mapget(microsoft-aspnetcore-routing-iendpointroutebuilder-system-string-system-delegate)) · [`Results.Ok`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.results.ok?view=aspnetcore-10.0#microsoft-aspnetcore-http-results-ok-1(-0)) · [`Results.Problem`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.results.problem?view=aspnetcore-10.0#microsoft-aspnetcore-http-results-problem(system-string-system-string-system-nullable((system-int32))-system-string-system-string-system-collections-generic-ienumerable((system-collections-generic-keyvaluepair((system-string-system-object)))))) · [`Results.Content`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.results.content?view=aspnetcore-10.0#microsoft-aspnetcore-http-results-content(system-string-system-string-system-text-encoding-system-nullable((system-int32)))) · [`IResult`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.iresult?view=aspnetcore-10.0) · [`RoutingEndpointConventionBuilderExtensions.WithName`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.builder.routingendpointconventionbuilderextensions.withname?view=aspnetcore-10.0#microsoft-aspnetcore-builder-routingendpointconventionbuilderextensions-withname-1(-0-system-string)) · [`OpenApiRouteHandlerBuilderExtensions.Produces`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.openapiroutehandlerbuilderextensions.produces?view=aspnetcore-10.0#microsoft-aspnetcore-http-openapiroutehandlerbuilderextensions-produces-1(microsoft-aspnetcore-builder-routehandlerbuilder-system-int32-system-string-system-string())) · [`OpenApiRouteHandlerBuilderExtensions.ProducesProblem`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.openapiroutehandlerbuilderextensions.producesproblem?view=aspnetcore-10.0#microsoft-aspnetcore-http-openapiroutehandlerbuilderextensions-producesproblem(microsoft-aspnetcore-builder-routehandlerbuilder-system-int32-system-string)) · [`StatusCodes.Status429TooManyRequests`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.statuscodes.status429toomanyrequests?view=aspnetcore-10.0#microsoft-aspnetcore-http-statuscodes-status429toomanyrequests) · [`IHeaderDictionary.RetryAfter`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.iheaderdictionary.retryafter?view=aspnetcore-10.0#microsoft-aspnetcore-http-iheaderdictionary-retryafter) · [`HttpContext.RequestAborted`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.httpcontext.requestaborted?view=aspnetcore-10.0#microsoft-aspnetcore-http-httpcontext-requestaborted) · [`ProblemDetailsServiceCollectionExtensions.AddProblemDetails`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.problemdetailsservicecollectionextensions.addproblemdetails?view=aspnetcore-10.0#microsoft-extensions-dependencyinjection-problemdetailsservicecollectionextensions-addproblemdetails(microsoft-extensions-dependencyinjection-iservicecollection)) · [`ExceptionHandlerExtensions.UseExceptionHandler`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.builder.exceptionhandlerextensions.useexceptionhandler?view=aspnetcore-10.0#microsoft-aspnetcore-builder-exceptionhandlerextensions-useexceptionhandler(microsoft-aspnetcore-builder-iapplicationbuilder)) · [`ConcurrentDictionary.AddOrUpdate`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.concurrent.concurrentdictionary-2.addorupdate?view=net-10.0#system-collections-concurrent-concurrentdictionary-2-addorupdate(-0-1-system-func((-0-1-1)))) · [`WebApplication.Run`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.builder.webapplication.run?view=aspnetcore-10.0#microsoft-aspnetcore-builder-webapplication-run(system-string)) · [Top-level statements](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/program-structure/top-level-statements) · [`async` lambda](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/lambda-expressions#async-lambdas) · [Local function](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/building-software/local-functions) · [`static` local function](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/building-software/local-functions) · [`switch` expression](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/switch-expression) · [`or` pattern](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/patterns#logical-patterns) · [Anonymous type `new { ... }`](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/anonymous-types) · [`const`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/const) · [Named argument `name: value`](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/named-and-optional-arguments#named-arguments) · [Nullable reference type `string?`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/nullable-reference-types)

**Read more.** [Minimal APIs quick reference](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis?view=aspnetcore-10.0) · [Parameter binding in minimal API apps](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis/parameter-binding?view=aspnetcore-10.0) · [Create responses in minimal API apps](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis/responses?view=aspnetcore-10.0) · [APIs overview: controllers and minimal APIs](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/apis?view=aspnetcore-10.0) · [Problem details](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/error-handling?view=aspnetcore-10.0#problem-details) · [RFC 9457: Problem Details for HTTP APIs](https://www.rfc-editor.org/rfc/rfc9457.html) · [429 Too Many Requests (MDN)](https://developer.mozilla.org/en-US/docs/Web/HTTP/Reference/Status/429) · [Retry-After header (MDN)](https://developer.mozilla.org/en-US/docs/Web/HTTP/Reference/Headers/Retry-After) · [Pattern matching overview](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/patterns/pattern-matching) · [The Web SDK (Microsoft.NET.Sdk.Web)](https://learn.microsoft.com/en-us/aspnet/core/razor-pages/web-sdk?view=aspnetcore-10.0) · [dotnet run](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-run)

### Pattern 22: Angular standalone bootstrap and an HTTP interceptor

**What it is.** The Angular application starts with one function call. `main.ts` hands `bootstrapApplication` the root component and a configuration object. The configuration object holds a list of *providers*: calls that register the services of the whole application, such as the HTTP client and the router. No file declares an `NgModule`. One of the providers installs an *interceptor*, a function that every outgoing HTTP request passes through before it is sent.

**Where to find it.** [Web/main.ts](src/Northstar.Web/src/main.ts), [Web/app.config.ts](src/Northstar.Web/src/app/app.config.ts) and [Web/demo-auth.interceptor.ts](src/Northstar.Web/src/app/core/demo-auth.interceptor.ts). The page that the browser loads is [Web/index.html](src/Northstar.Web/src/index.html).

**[`src/Northstar.Web/src/main.ts`](src/Northstar.Web/src/main.ts#L1-L6)** · lines 1–6

<!-- x:ng-main -->
```ts
import { bootstrapApplication } from '@angular/platform-browser';
import { appConfig } from './app/app.config';
import { App } from './app/app';

bootstrapApplication(App, appConfig)
  .catch((err) => console.error(err));
```

**[`src/Northstar.Web/src/app/app.config.ts`](src/Northstar.Web/src/app/app.config.ts#L8-L14)** · lines 8–14

<!-- x:ng-config -->
```ts
export const appConfig: ApplicationConfig = {
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideHttpClient(withInterceptors([demoAuthInterceptor])),
    provideRouter(routes)
  ]
};
```

**[`src/Northstar.Web/src/app/core/demo-auth.interceptor.ts`](src/Northstar.Web/src/app/core/demo-auth.interceptor.ts#L3-L13)** · lines 3–13

<!-- x:ng-interceptor -->
```ts
export const demoOrganizationId = '11111111-1111-1111-1111-111111111111';

export const demoAuthInterceptor: HttpInterceptorFn = (request, next) =>
  next(
    request.clone({
      setHeaders: {
        'X-Organization-Id': demoOrganizationId,
        'X-User-Role': 'PayrollManager'
      }
    })
  );
```

**How to read the code.** Starting the application:

1. `bootstrapApplication(App, appConfig)` (line 5) creates the root component `App` and gives it the configuration. The component appears on the page at the tag `<app-root>` ([Web/index.html:11](src/Northstar.Web/src/index.html#L11)), because `app-root` is the `selector` of the component ([Web/app.ts:9](src/Northstar.Web/src/app/app.ts#L9)).
2. `.catch((err) => console.error(err));` (line 6) handles a failed start. `bootstrapApplication` returns a `Promise`, and an error during start-up is written to the console of the browser.
3. `export const appConfig: ApplicationConfig = {` (line 8) declares the configuration as a constant of the type `ApplicationConfig`. Its one property, `providers: [` (line 9), is the list of services that every part of the application can ask for.
4. `provideBrowserGlobalErrorListeners(),` (line 10) listens for errors that nothing has caught, and for rejected promises that nothing has handled, on the `window` object. It passes them to the error handler of Angular.
5. `provideHttpClient(withInterceptors([demoAuthInterceptor])),` (line 11) makes `HttpClient` available for injection. `withInterceptors` takes an array of interceptor functions, which run in the order of the array. Here the array has one entry.
6. `provideRouter(routes)` (line 12) registers the router with the route table. The table is an empty array ([Web/app.routes.ts:3](src/Northstar.Web/src/app/app.routes.ts#L3)), so the application has one page.

The interceptor:

7. `export const demoAuthInterceptor: HttpInterceptorFn = (request, next) =>` (line 5) declares the interceptor as an arrow function of the type `HttpInterceptorFn`. It receives the outgoing request and a function `next`, which stands for the rest of the chain.
8. `request.clone({` (line 7) makes a copy of the request. Most parts of a request object cannot be changed after it is created, so an interceptor copies the request and names the changes. `setHeaders: {` (line 8) lists headers to set on the copy.
9. `'X-Organization-Id': demoOrganizationId,` (line 9) and `'X-User-Role': 'PayrollManager'` (line 10) are the two headers that the API reads on every request ([Pattern 7](#pattern-7-tenant-scoping-from-header-to-query)). The organization id is the constant on line 3 of the same file.
10. `next(` (line 6) passes the copy on and returns the result, an Observable of the response ([Pattern 23](#pattern-23-angular-services-that-return-observables)). The interceptor does not touch the response.

The pieces around it:

- `App` is a component: a class with the `@Component` decorator ([Web/app.ts:8-13](src/Northstar.Web/src/app/app.ts#L8-L13)). The decorator names the selector, the template file and the style file. Its `imports` array lists what the template uses: two pipes and `ImportPanelComponent`. The template places that component with the tag `<app-import-panel>` ([Web/app.html:35](src/Northstar.Web/src/app/app.html#L35)).
- Components in current Angular are *standalone* by default. A component states its own dependencies in `imports`, and no module has to declare it. This is why `bootstrapApplication` can start from a component.
- The interceptor adds the two headers to every request that goes through `HttpClient`. The two API services therefore never mention them ([Pattern 23](#pattern-23-angular-services-that-return-observables)). The header `Idempotency-Key` is different: it has a new value for each command, and the services set it themselves ([Pattern 8](#pattern-8-idempotent-commands-with-an-idempotency-key)).
- The services use addresses without a host, such as `/api/import-batches`. The browser sends them to the server that the page came from. In development that is the Angular development server, which `npm start` runs with `--proxy-config proxy.conf.json` ([Web/package.json:6](src/Northstar.Web/package.json#L6)). The proxy file sends every request for `/api` and `/health` on to `http://localhost:5120`, where the API listens ([Web/proxy.conf.json](src/Northstar.Web/proxy.conf.json)).
- The list of providers has the same job as `builder.Services` in the API ([Pattern 2](#pattern-2-composition-root-and-service-registration)), and an interceptor has the shape of a middleware in the request pipeline of the API ([Pattern 4](#pattern-4-request-pipeline-and-host-startup)): receive the request, do some work, call `next`.
- Both test files of the frontend build their own list of providers and leave the interceptor out ([Pattern 26](#pattern-26-frontend-tests-with-vitest-and-playwright)).

**Why it is done this way.** The README states the purpose of the function in one sentence: "Angular supplies those headers through an interceptor." ([README.md:107](README.md#L107)). The headers stand in for a sign-in ([Pattern 7](#pattern-7-tenant-scoping-from-header-to-query)), and they belong to every request. An interceptor puts them in one place, so no service and no component repeats them. The Angular documentation describes interceptors as a form of middleware for `HttpClient`, and it names the adding of authentication headers to outgoing requests as a common use.

**Look up.** [`bootstrapApplication`](https://angular.dev/api/platform-browser/bootstrapApplication) · [`ApplicationConfig`](https://angular.dev/api/core/ApplicationConfig) · [`provideBrowserGlobalErrorListeners`](https://angular.dev/api/core/provideBrowserGlobalErrorListeners) · [`provideHttpClient`](https://angular.dev/api/common/http/provideHttpClient) · [`withInterceptors`](https://angular.dev/api/common/http/withInterceptors) · [`provideRouter`](https://angular.dev/api/router/provideRouter) · [`Routes`](https://angular.dev/api/router/Routes) · [`HttpInterceptorFn`](https://angular.dev/api/common/http/HttpInterceptorFn) · [`HttpRequest.clone`](https://angular.dev/api/common/http/HttpRequest#clone) · [`Component`](https://angular.dev/api/core/Component) · [`Component.selector`](https://angular.dev/api/core/Component#selector) · [`Component.imports`](https://angular.dev/api/core/Component#imports)

**Read more.** [Setting up HttpClient](https://angular.dev/guide/http/setup) · [Interceptors](https://angular.dev/guide/http/interceptors) · [Anatomy of a component](https://angular.dev/guide/components) · [Importing and using components](https://angular.dev/guide/components/importing) · [Dependency injection in Angular](https://angular.dev/guide/di) · [Angular routing](https://angular.dev/guide/routing) · [Proxying to a backend server](https://angular.dev/tools/cli/serve#proxying-to-a-backend-server) · [What is Angular?](https://angular.dev/overview)

### Pattern 23: Angular services that return Observables

**What it is.** Every HTTP call of the frontend lives in one of two classes, `ImportApiService` and `PayrollApiService`. Each method builds one request with `HttpClient` and returns it as an `Observable`: an object that stands for a result that will arrive later. The components never use `HttpClient` themselves. They call a method of a service and subscribe to what it returns.

**Where to find it.** [Web/import-api.service.ts](src/Northstar.Web/src/app/imports/import-api.service.ts) and [Web/payroll-api.service.ts](src/Northstar.Web/src/app/payroll-runs/payroll-api.service.ts). The types of the data are in [Web/import.models.ts](src/Northstar.Web/src/app/imports/import.models.ts) and [Web/payroll-run.models.ts](src/Northstar.Web/src/app/payroll-runs/payroll-run.models.ts).

**[`src/Northstar.Web/src/app/imports/import-api.service.ts`](src/Northstar.Web/src/app/imports/import-api.service.ts#L6-L22)** · lines 6–22 and 37

<!-- x:ng-service -->
```ts
@Injectable({ providedIn: 'root' })
export class ImportApiService {
  private readonly http = inject(HttpClient);

  listBatches(): Observable<ImportBatch[]> {
    return this.http.get<ImportBatch[]>('/api/import-batches');
  }

  createBatch(request: CreateImportBatchRequest, idempotencyKey: string): Observable<ImportBatch> {
    return this.http.post<ImportBatch>('/api/import-batches', request, {
      headers: new HttpHeaders({ 'Idempotency-Key': idempotencyKey }),
    });
  }

  getBatch(batchId: string): Observable<ImportBatch> {
    return this.http.get<ImportBatch>(`/api/import-batches/${batchId}`);
  }
// ... lines 23–36 omitted ...
}
```

**[`src/Northstar.Web/src/app/payroll-runs/payroll-api.service.ts`](src/Northstar.Web/src/app/payroll-runs/payroll-api.service.ts#L42-L46)** · lines 42–46

<!-- x:ng-approve -->
```ts
approve(run: PayrollRun): Observable<PayrollRun> {
  return this.http.post<PayrollRun>(`/api/payroll-runs/${run.id}/approve`, {
    expectedVersion: run.version
  });
}
```

**How to read the code.** A service and its first two methods:

1. `@Injectable({ providedIn: 'root' })` (line 6) marks the class as a *service*, a class that the injector of Angular can create and hand out. `providedIn: 'root'` registers it for the whole application, which then shares one instance.
2. `private readonly http = inject(HttpClient);` (line 8) asks the injector for the HTTP client. `inject` is called in the initializer of a field, so the class needs no constructor. The C# side does the same job with a primary constructor ([Pattern 3](#pattern-3-constructor-injection-with-primary-constructors)).
3. `listBatches(): Observable<ImportBatch[]> {` (line 10) declares the return type: an Observable that will deliver an array of `ImportBatch`.
4. `return this.http.get<ImportBatch[]>('/api/import-batches');` (line 11) describes a `GET` request and returns it. This line sends nothing. `HttpClient` sends the request when something subscribes to the Observable, and each further subscription sends the request again.
5. The type in angle brackets, `<ImportBatch[]>`, is a statement by the developer about the JSON that will come back. Angular parses the JSON, and it does not compare the result with the type.

A command with a body and a header:

6. `createBatch(request: CreateImportBatchRequest, idempotencyKey: string): Observable<ImportBatch> {` (line 14) takes the body of the request and the key as two parameters.
7. `return this.http.post<ImportBatch>('/api/import-batches', request, {` (line 15) describes a `POST` request. The second argument becomes the JSON body. The third argument is an object with options.
8. `headers: new HttpHeaders({ 'Idempotency-Key': idempotencyKey }),` (line 16) is the one option used here: a header for this call only ([Pattern 8](#pattern-8-idempotent-commands-with-an-idempotency-key)). The two headers that every call needs come from the interceptor ([Pattern 22](#pattern-22-angular-standalone-bootstrap-and-an-http-interceptor)).
9. ``return this.http.get<ImportBatch>(`/api/import-batches/${batchId}`);`` (line 21) builds the address with a *template literal*, a string in backticks in which `${batchId}` is replaced by the value of the variable.

Sending the version back:

10. `approve(run: PayrollRun): Observable<PayrollRun> {` (line 42) takes the whole run that the page shows, and `expectedVersion: run.version` (line 44) puts its version number into the body. The API compares that number with the stored one ([Pattern 16](#pattern-16-optimistic-concurrency-with-a-version-number)).

The nine methods of the two services:

| Method | Request | Extra header |
|---|---|---|
| `listBatches()` [10–12](src/Northstar.Web/src/app/imports/import-api.service.ts#L10-L12) | `GET /api/import-batches` | – |
| `createBatch(request, idempotencyKey)` [14–18](src/Northstar.Web/src/app/imports/import-api.service.ts#L14-L18) | `POST /api/import-batches` | `Idempotency-Key` |
| `getBatch(batchId)` [20–22](src/Northstar.Web/src/app/imports/import-api.service.ts#L20-L22) | `GET /api/import-batches/{id}` | – |
| `getErrors(batchId)` [24–26](src/Northstar.Web/src/app/imports/import-api.service.ts#L24-L26) | `GET /api/import-batches/{id}/errors` | – |
| `retryBatch(batchId, idempotencyKey)` [28–36](src/Northstar.Web/src/app/imports/import-api.service.ts#L28-L36) | `POST /api/import-batches/{id}/retry` | `Idempotency-Key` |
| `listRuns()` [13–15](src/Northstar.Web/src/app/payroll-runs/payroll-api.service.ts#L13-L15) | `GET /api/payroll-runs` | – |
| `calculate(runId, idempotencyKey)` [17–23](src/Northstar.Web/src/app/payroll-runs/payroll-api.service.ts#L17-L23) | `POST /api/payroll-runs/{id}/calculate` | `Idempotency-Key` |
| `addMissingCarpenterRate()` [25–40](src/Northstar.Web/src/app/payroll-runs/payroll-api.service.ts#L25-L40) | `POST /api/wage-rates` | – |
| `approve(run)` [42–46](src/Northstar.Web/src/app/payroll-runs/payroll-api.service.ts#L42-L46) | `POST /api/payroll-runs/{id}/approve` | – |

The pieces around it:

- These nine methods are every use of `HttpClient` in the application: `get` on 4 lines and `post` on 5. Each one matches a controller action of the API ([Pattern 5](#pattern-5-thin-attribute-routed-controllers)).
- A component calls a method and subscribes with two callbacks, as in `this.payrollApi.approve(run).subscribe({ next: …, error: … })` ([Web/app.ts:59-62](src/Northstar.Web/src/app/app.ts#L59-L62)). `next` receives the data. `error` receives an `HttpErrorResponse` when the request fails.
- Both components read a failure in the same way: `httpError.error?.detail ?? httpError.error?.title`, followed by a fixed text ([Web/import-panel.component.ts:204-211](src/Northstar.Web/src/app/imports/import-panel.component.ts#L204-L211), [Web/app.ts:90-95](src/Northstar.Web/src/app/app.ts#L90-L95)). For an error answer from the API, the property `error` holds the body of the response. That body is the problem details of [Pattern 6](#pattern-6-result-objects-mapped-to-http-responses), and `detail` and `title` are two of its members.
- The types are TypeScript interfaces written by hand. `ImportBatch` ([Web/import.models.ts:6-24](src/Northstar.Web/src/app/imports/import.models.ts#L6-L24)) has the 17 properties of the C# record `ImportBatchView` ([Application/ImportBatchView.cs:8-25](src/Northstar.Application/Imports/ImportBatchView.cs#L8-L25)), with names in camelCase, which is the default of ASP.NET Core for JSON. An interface exists only for the compiler and leaves no code in the browser.
- Dates and ids arrive as text, so `createdAt` and `id` are of the type `string`. The status arrives as the name of the C# enum value ([Pattern 19](#pattern-19-small-conventions-used-everywhere)), and the frontend narrows it to a union of the five names ([Web/import.models.ts:1-2](src/Northstar.Web/src/app/imports/import.models.ts#L1-L2)).
- `addMissingCarpenterRate()` posts a fixed body for the demonstration and returns `Observable<unknown>`. Its caller uses only the fact that the call succeeded ([Web/app.ts:43-52](src/Northstar.Web/src/app/app.ts#L43-L52)).
- The services subscribe to nothing and keep no state. What happens with a result is decided in the components ([Pattern 24](#pattern-24-signals-computed-values-and-control-flow-templates), [Pattern 25](#pattern-25-polling-with-rxjs-and-automatic-cleanup)).
- The C# side has the same division: one class, `FakePayrollProviderClient`, owns the addresses and the HTTP details of one remote API ([Pattern 11](#pattern-11-typed-httpclient-with-validated-options)).

**Why it is done this way.** With the addresses, the HTTP methods and the headers in two small classes, a component deals with batches and runs and not with HTTP, and a test can answer every request at one known address ([Pattern 26](#pattern-26-frontend-tests-with-vitest-and-playwright)). Returning the Observable, and not subscribing inside the service, leaves the timing to the caller. The Angular documentation states the two properties that the callers build on: no request is sent before something subscribes, and when a subscription is ended early, a request that is still under way is aborted. The polling of [Pattern 25](#pattern-25-polling-with-rxjs-and-automatic-cleanup) uses both.

**Look up.** [`Injectable`](https://angular.dev/api/core/Injectable) · [`inject`](https://angular.dev/api/core/inject) · [`HttpClient`](https://angular.dev/api/common/http/HttpClient) · [`HttpClient.get`](https://angular.dev/api/common/http/HttpClient#get) · [`HttpClient.post`](https://angular.dev/api/common/http/HttpClient#post) · [`HttpHeaders`](https://angular.dev/api/common/http/HttpHeaders) · [`HttpErrorResponse`](https://angular.dev/api/common/http/HttpErrorResponse) · [`Observable`](https://rxjs.dev/api/index/class/Observable) · [`Observable.subscribe`](https://rxjs.dev/api/index/class/Observable#subscribe)

**Read more.** [Creating and using services](https://angular.dev/guide/di/creating-and-using-services) · [Making HTTP requests](https://angular.dev/guide/http/making-requests) · [Understanding communicating with backend services using HTTP](https://angular.dev/guide/http) · [RxJS: Observable](https://rxjs.dev/guide/observable) · [RxJS overview](https://rxjs.dev/guide/overview) · [Format response data in ASP.NET Core Web API](https://learn.microsoft.com/en-us/aspnet/core/web-api/advanced/formatting?view=aspnetcore-10.0) · [The TypeScript Handbook](https://www.typescriptlang.org/docs/handbook/intro.html)

### Pattern 24: Signals, computed values and control-flow templates

**What it is.** A component keeps its state in *signals*. A signal is a wrapper around a value that tells Angular when the value changes. The template reads a signal by calling it, as in `loading()`, and Angular updates the page when a signal that the template has read gets a new value. For conditions and lists, the template uses the built-in blocks `@if` and `@for`.

**Where to find it.** [Web/import-panel.component.ts:19-44](src/Northstar.Web/src/app/imports/import-panel.component.ts#L19-L44) with its template [Web/import-panel.component.html](src/Northstar.Web/src/app/imports/import-panel.component.html). The root component is built in the same way ([Web/app.ts:17-26](src/Northstar.Web/src/app/app.ts#L17-L26), [Web/app.html](src/Northstar.Web/src/app/app.html)).

The excerpts follow in three groups, each with its steps: the class, a helper type, and the template.

**[`src/Northstar.Web/src/app/imports/import-panel.component.ts`](src/Northstar.Web/src/app/imports/import-panel.component.ts#L19-L44)** · lines 19–44

<!-- x:ng-signals -->
```ts
@Component({
  selector: 'app-import-panel',
  imports: [DatePipe],
  templateUrl: './import-panel.component.html',
  styleUrl: './import-panel.component.scss',
})
export class ImportPanelComponent implements OnInit {
  private readonly importApi = inject(ImportApiService);
  private readonly destroyRef = inject(DestroyRef);
  private pollingSubscription?: Subscription;
  private rowErrorsSubscription?: Subscription;

  protected readonly batches = signal<ImportBatch[]>([]);
  protected readonly selectedBatch = signal<ImportBatch | null>(null);
  protected readonly rowErrors = signal<ImportRow[]>([]);
  protected readonly loading = signal(true);
  protected readonly action = signal<string | null>(null);
  protected readonly error = signal<string | null>(null);
  protected readonly isActive = computed(() => {
    const batch = this.selectedBatch();
    return batch !== null && isActiveImportStatus(batch.status);
  });

  ngOnInit(): void {
    this.loadBatches();
  }
  // ... continues in the file ...
```

**How to read the code.** The class:

1. `selector: 'app-import-panel',` (line 20) is the tag that places this component in another template, and `templateUrl: './import-panel.component.html',` (line 22) names the file with its own template. `imports: [DatePipe],` (line 21) lists the one thing from outside that the template uses.
2. `export class ImportPanelComponent implements OnInit {` (line 25) promises a method `ngOnInit`. Angular calls it once after it has set the component up. Here it starts the first request with `this.loadBatches();` (line 43).
3. `private readonly importApi = inject(ImportApiService);` (line 26) gets the service of [Pattern 23](#pattern-23-angular-services-that-return-observables).
4. `protected readonly batches = signal<ImportBatch[]>([]);` (line 31) creates a signal. The type of its value is in angle brackets, and the first value, an empty array, is in round brackets. `protected` keeps the field out of reach of other classes, and the template of the component can still read it.
5. `protected readonly selectedBatch = signal<ImportBatch | null>(null);` (line 32) has a *union type*: the value is a batch or `null`. It starts as `null`, because no batch is selected at first.
6. `protected readonly loading = signal(true);` (line 34) has no type in angle brackets. TypeScript takes the type `boolean` from the first value.
7. `protected readonly isActive = computed(() => {` (line 37) creates a *computed signal*: a signal that cannot be set and whose value is worked out from other signals. `const batch = this.selectedBatch();` (line 38) reads a signal by calling it, and that read makes `selectedBatch` a dependency. The function runs again only when `selectedBatch` has changed and somebody reads `isActive`. Between changes, Angular returns the stored result.

**[`src/Northstar.Web/src/app/imports/import.models.ts`](src/Northstar.Web/src/app/imports/import.models.ts#L1-L4)** · lines 1–4 and 42–44

<!-- x:ng-models -->
```ts
export type ImportBatchStatus =
  'Pending' | 'Processing' | 'Completed' | 'CompletedWithErrors' | 'Failed';

export type ProviderScenario = 'success' | 'mixed' | 'transient';
// ... lines 5–41 omitted ...
export function isActiveImportStatus(status: ImportBatchStatus): boolean {
  return status === 'Pending' || status === 'Processing';
}
```

8. `export type ImportBatchStatus =` (line 1) declares a union of *string literal types*. A value of this type is one of the five texts on line 2, and the compiler rejects any other text. They are the names of the C# enum `ImportBatchStatus` ([Domain/ImportBatch.cs:3-10](src/Northstar.Domain/Imports/ImportBatch.cs#L3-L10)).
9. `return status === 'Pending' || status === 'Processing';` (line 43) is the whole test for "still running". The computed signal of step 7 and the polling of [Pattern 25](#pattern-25-polling-with-rxjs-and-automatic-cleanup) both call this function.

**[`src/Northstar.Web/src/app/imports/import-panel.component.html`](src/Northstar.Web/src/app/imports/import-panel.component.html#L10-L18)** · lines 10–18

<!-- x:ng-template-if -->
```html
@if (selectedBatch(); as batch) {
  <span
    class="status"
    [class]="'status ' + statusClass(batch.status)"
    data-testid="import-status"
  >
    {{ batch.status }}
  </span>
}
```

**[`src/Northstar.Web/src/app/imports/import-panel.component.html`](src/Northstar.Web/src/app/imports/import-panel.component.html#L34-L42)** · lines 34–42

<!-- x:ng-template-button -->
```html
<button
  type="button"
  class="secondary"
  data-testid="import-mixed"
  [disabled]="loading() || action() !== null || isActive()"
  (click)="startImport('mixed')"
>
  Import mixed batch
</button>
```

**[`src/Northstar.Web/src/app/imports/import-panel.component.html`](src/Northstar.Web/src/app/imports/import-panel.component.html#L139-L150)** · lines 139–150

<!-- x:ng-template-for -->
```html
@for (row of rowErrors(); track row.id) {
  <tr data-testid="import-error-row">
    <td>{{ row.rowNumber }}</td>
    <td>{{ row.externalTimeEntryId ?? '—' }}</td>
    <td>
      @if (row.errorCode) {
        <span class="error-code">{{ row.errorCode }}</span>
      }
      {{ row.errorMessage ?? 'The row could not be imported.' }}
    </td>
  </tr>
}
```

The template:

10. `@if (selectedBatch(); as batch) {` (line 10) shows its block only when the value of the signal is not `null`. `as batch` stores that value in a variable of the template, so the block can use `batch` without a second call.
11. `[class]="'status ' + statusClass(batch.status)"` (line 13) is a *property binding*. Square brackets around a name mean that the right-hand side is an expression and not text. Here the expression gives the list of CSS classes. `statusClass` is a method of the component with a `switch` over the union of step 8 ([Web/import-panel.component.ts:107-118](src/Northstar.Web/src/app/imports/import-panel.component.ts#L107-L118)).
12. Line 16 holds an *interpolation*, `{{ batch.status }}`. The text between the double curly braces is an expression, and its value is written into the page as text.
13. `[disabled]="loading() || action() !== null || isActive()"` (line 38) binds the property `disabled` of the button. The expression reads three signals. The button cannot be pressed while the list is loading, while a command is on its way, or while the selected batch is still running.
14. `(click)="startImport('mixed')"` (line 39) is an *event binding*. Round brackets around the name of an event mean that the statement on the right-hand side runs when the event happens.
15. `@for (row of rowErrors(); track row.id) {` (line 139) repeats its block for each element of the array. `track row.id` names the property that identifies a row. Angular uses it to match the rows that are already on the page with the rows of a new array.
16. Line 142 uses `??` inside an interpolation: when the id is `null`, a dash is shown. `@if (row.errorCode) {` (line 144) shows that blocks can be nested.

The pieces around it:

- State changes in one direction. An event calls a method of the class, the method writes to signals, and the page follows. A method writes with `set`, as in `this.action.set(providerScenario);` ([Web/import-panel.component.ts:49](src/Northstar.Web/src/app/imports/import-panel.component.ts#L49)), or with `update`, which works the new value out from the old one ([Web/import-panel.component.ts:171-173](src/Northstar.Web/src/app/imports/import-panel.component.ts#L171-L173)). The template never writes to a signal.
- The two components hold all the state of the page: `signal(` is on 10 lines and `computed(` on 3. `.set(` is on 34 lines and `.update(` on 1.
- The three `protected` fields `loading`, `action` and `error` exist in both components with the same meaning: the first load is running, a command is running, and the text of the last failure.
- An `@if` can have more branches. The main part of the panel is a chain of `@if (loading())`, `@else if (selectedBatch(); as batch)` and `@else` ([Web/import-panel.component.html:64-66](src/Northstar.Web/src/app/imports/import-panel.component.html#L64-L66), [Web/import-panel.component.html:157](src/Northstar.Web/src/app/imports/import-panel.component.html#L157)).
- A *pipe* formats a value for display. `{{ batch.createdAt | date: 'MMM d, h:mm a' }}` passes the text of a date through the `date` pipe with a pattern ([Web/import-panel.component.html:83](src/Northstar.Web/src/app/imports/import-panel.component.html#L83)). This is why `DatePipe` is in `imports`. The root template also formats money with the `currency` pipe ([Web/app.html:181](src/Northstar.Web/src/app/app.html#L181)).
- `[class.has-errors]="batch.errorRows > 0"` adds or removes one CSS class ([Web/import-panel.component.html:96](src/Northstar.Web/src/app/imports/import-panel.component.html#L96)).
- Many elements carry an attribute `data-testid`, such as `data-testid="import-status"` in the first template excerpt. Angular does nothing with it. The tests find the elements by this attribute ([Pattern 26](#pattern-26-frontend-tests-with-vitest-and-playwright)).
- The component takes no input from its parent. The root template places it with the single tag `<app-import-panel class="import-workflow" />` ([Web/app.html:35](src/Northstar.Web/src/app/app.html#L35)).
- [Section 6](#6-frontend-inventory) counts every kind of template syntax and every TypeScript feature in the frontend, with links.

**Why it is done this way.** The page is a function of a few values: the list, the selected batch, the row errors, and three flags. With each value in a signal, there is one place where it is stored and one way to change it, and the template states what the page looks like for the current values. Angular records which signals a template has read and updates the page when one of them changes. The documentation describes a computed signal as lazily evaluated and memoized: it is worked out when it is first read and then kept until one of its dependencies changes. `isActive` is such a value. It is derived from `selectedBatch`, so it is not stored a second time and cannot disagree with it.

**Look up.** [`signal`](https://angular.dev/api/core/signal) · [`computed`](https://angular.dev/api/core/computed) · [`WritableSignal.set`](https://angular.dev/api/core/WritableSignal#set) · [`WritableSignal.update`](https://angular.dev/api/core/WritableSignal#update) · [`Component`](https://angular.dev/api/core/Component) · [`Component.templateUrl`](https://angular.dev/api/core/Component#templateUrl) · [`Component.styleUrl`](https://angular.dev/api/core/Component#styleUrl) · [`OnInit`](https://angular.dev/api/core/OnInit) · [`OnInit.ngOnInit`](https://angular.dev/api/core/OnInit#ngOnInit) · [`inject`](https://angular.dev/api/core/inject) · [`DatePipe`](https://angular.dev/api/common/DatePipe) · [`CurrencyPipe`](https://angular.dev/api/common/CurrencyPipe)

**Read more.** [Angular Signals](https://angular.dev/guide/signals) · [Template syntax](https://angular.dev/guide/templates) · [Control flow (@if, @for, @switch)](https://angular.dev/guide/templates/control-flow) · [Binding dynamic text, properties and attributes](https://angular.dev/guide/templates/binding) · [Adding event listeners](https://angular.dev/guide/templates/event-listeners) · [Pipes](https://angular.dev/guide/templates/pipes) · [Component lifecycle](https://angular.dev/guide/components/lifecycle) · [Anatomy of a component](https://angular.dev/guide/components) · [Angular coding style guide](https://angular.dev/style-guide) · [The TypeScript Handbook](https://www.typescriptlang.org/docs/handbook/intro.html)

### Pattern 25: Polling with RxJS and automatic cleanup

**What it is.** The API accepts an import and finishes it later ([Pattern 9](#pattern-9-accept-now-process-later)), so the page has to ask again. After a batch is created, the component asks for its status once a second until the status is final. The whole loop is one RxJS *pipeline*: a timer produces ticks, each tick becomes a request, and two operators end the stream. One ends it when the status is final. The other ends it when the component leaves the page.

**Where to find it.** [Web/import-panel.component.ts:46-67](src/Northstar.Web/src/app/imports/import-panel.component.ts#L46-L67) starts an import, and [Web/import-panel.component.ts:150-167](src/Northstar.Web/src/app/imports/import-panel.component.ts#L150-L167) is the polling.

**[`src/Northstar.Web/src/app/imports/import-panel.component.ts`](src/Northstar.Web/src/app/imports/import-panel.component.ts#L46-L67)** · lines 46–67

<!-- x:ng-create -->
```ts
protected startImport(providerScenario: ProviderScenario): void {
  this.pollingSubscription?.unsubscribe();
  this.rowErrorsSubscription?.unsubscribe();
  this.action.set(providerScenario);
  this.error.set(null);
  this.rowErrors.set([]);

  this.importApi
    .createBatch({ periodStart, periodEnd, providerScenario }, globalThis.crypto.randomUUID())
    .pipe(takeUntilDestroyed(this.destroyRef))
    .subscribe({
      next: (batch) => {
        this.selectBatch(batch);
        if (isActiveImportStatus(batch.status)) {
          this.pollBatch(batch.id);
        } else {
          this.finishBatch(batch);
        }
      },
      error: (httpError: HttpErrorResponse) => this.failAction(httpError),
    });
}
```

**[`src/Northstar.Web/src/app/imports/import-panel.component.ts`](src/Northstar.Web/src/app/imports/import-panel.component.ts#L150-L174)** · lines 150–174

<!-- x:ng-poll -->
```ts
private pollBatch(batchId: string): void {
  this.pollingSubscription?.unsubscribe();
  this.pollingSubscription = timer(0, pollIntervalMilliseconds)
    .pipe(
      switchMap(() => this.importApi.getBatch(batchId)),
      takeWhile((batch) => isActiveImportStatus(batch.status), true),
      takeUntilDestroyed(this.destroyRef),
    )
    .subscribe({
      next: (batch) => {
        this.selectBatch(batch);
        if (!isActiveImportStatus(batch.status)) {
          this.finishBatch(batch);
        }
      },
      error: (httpError: HttpErrorResponse) => this.failAction(httpError),
    });
}

private selectBatch(batch: ImportBatch): void {
  this.selectedBatch.set(batch);
  this.batches.update((current) =>
    this.orderNewestFirst([batch, ...current.filter((item) => item.id !== batch.id)]),
  );
}
```

**How to read the code.** Starting an import:

1. `protected startImport(providerScenario: ProviderScenario): void {` (line 46) is the method behind the three import buttons ([Pattern 24](#pattern-24-signals-computed-values-and-control-flow-templates)).
2. `this.pollingSubscription?.unsubscribe();` (line 47) stops a poll that may still be running for an earlier batch. The field is declared as `private pollingSubscription?: Subscription;` and is empty before the first poll, which is why the call uses `?.`.
3. `this.action.set(providerScenario);` (line 49) and the two lines after it reset the state of the panel. From here on the buttons are disabled.
4. `.createBatch({ periodStart, periodEnd, providerScenario }, globalThis.crypto.randomUUID())` (line 54) calls the service ([Pattern 23](#pattern-23-angular-services-that-return-observables)). The first argument is the body. The second is a new random UUID, which becomes the `Idempotency-Key` of this command ([Pattern 8](#pattern-8-idempotent-commands-with-an-idempotency-key)).
5. `.pipe(takeUntilDestroyed(this.destroyRef))` (line 55) attaches an *operator* to the Observable. `pipe` returns a new Observable that passes through the operators in its argument list. `takeUntilDestroyed` ends the stream when the component is destroyed.
6. `.subscribe({` (line 56) sends the request. `next: (batch) => {` (line 57) receives the answer, and `this.selectBatch(batch);` (line 58) shows it.
7. `if (isActiveImportStatus(batch.status)) {` (line 59) decides what follows. A new batch comes back as `Pending`, so `this.pollBatch(batch.id);` (line 60) runs. A batch that is already finished goes straight to `finishBatch`.

The polling pipeline:

8. `this.pollingSubscription = timer(0, pollIntervalMilliseconds)` (line 152) creates the clock. `timer(0, 1_000)` emits a first value at once and then one value every 1,000 milliseconds. The values are numbers that count up, and the pipeline does not use them.
9. `switchMap(() => this.importApi.getBatch(batchId)),` (line 154) turns each tick into a request and passes the answers on. When a tick arrives while the request of the tick before is still unanswered, `switchMap` drops that request and goes on with the new one. Answers can therefore not arrive in the wrong order.
10. `takeWhile((batch) => isActiveImportStatus(batch.status), true),` (line 155) lets an answer through as long as the batch is still running, and ends the stream at the first answer that is not. The second argument, `true`, lets that last answer through as well. It is the one with the final status.
11. `takeUntilDestroyed(this.destroyRef),` (line 156) ends the stream when the component is destroyed, as in step 5.
12. `.subscribe({` (line 158) starts the clock and returns a `Subscription`, which step 8 stores in the field. Each answer is shown with `selectBatch`. For the last answer, `if (!isActiveImportStatus(batch.status)) {` (line 161) is true and `this.finishBatch(batch);` (line 162) runs: it clears `action`, which enables the buttons again, and it loads the row errors ([Web/import-panel.component.ts:176-196](src/Northstar.Web/src/app/imports/import-panel.component.ts#L176-L196)).

Updating the list:

13. `this.selectedBatch.set(batch);` (line 170) replaces the selected batch. `this.batches.update((current) =>` (line 171) then builds a new list from the current one: the batch itself, followed by all others, `this.orderNewestFirst([batch, ...current.filter((item) => item.id !== batch.id)]),` (line 172). `...` is the *spread* syntax, which copies the elements of one array into a new one.

The pieces around it:

- The stream can end in four ways. The status becomes final (`takeWhile`). The component is destroyed (`takeUntilDestroyed`). The user starts something else, and the method unsubscribes first ([Web/import-panel.component.ts:47](src/Northstar.Web/src/app/imports/import-panel.component.ts#L47), [Web/import-panel.component.ts:70](src/Northstar.Web/src/app/imports/import-panel.component.ts#L70), [Web/import-panel.component.ts:93](src/Northstar.Web/src/app/imports/import-panel.component.ts#L93), [Web/import-panel.component.ts:151](src/Northstar.Web/src/app/imports/import-panel.component.ts#L151)). Or a request fails: an error ends an Observable, and the `error` callback shows the message.
- Polling starts in four places: after a new import, after a retry ([Web/import-panel.component.ts:83](src/Northstar.Web/src/app/imports/import-panel.component.ts#L83)), when the user picks a running batch from the history ([Web/import-panel.component.ts:101](src/Northstar.Web/src/app/imports/import-panel.component.ts#L101)), and when the page loads and the newest batch is still running ([Web/import-panel.component.ts:137-138](src/Northstar.Web/src/app/imports/import-panel.component.ts#L137-L138)). A reload of the page therefore picks the polling up again.
- `takeUntilDestroyed` gets `this.destroyRef` as its argument on all 5 lines that use it. The operator can find the component by itself only where injection is possible, such as a field initializer. These calls are inside methods, so the component injects a `DestroyRef` once ([Web/import-panel.component.ts:27](src/Northstar.Web/src/app/imports/import-panel.component.ts#L27)) and passes it along.
- The component holds two subscriptions in fields, one for the poll and one for the row errors ([Web/import-panel.component.ts:28-29](src/Northstar.Web/src/app/imports/import-panel.component.ts#L28-L29)), so that it can end them by hand. `unsubscribe` is called on 8 lines.
- The interval is a constant, `const pollIntervalMilliseconds = 1_000;` ([Web/import-panel.component.ts:17](src/Northstar.Web/src/app/imports/import-panel.component.ts#L17)). The underscore is a separator for digits, as in C# ([Pattern 19](#pattern-19-small-conventions-used-everywhere)).
- The root component `App` calls `subscribe` four times without `takeUntilDestroyed` ([Web/app.ts:37](src/Northstar.Web/src/app/app.ts#L37), [Web/app.ts:45](src/Northstar.Web/src/app/app.ts#L45), [Web/app.ts:59](src/Northstar.Web/src/app/app.ts#L59), [Web/app.ts:68](src/Northstar.Web/src/app/app.ts#L68)). Each of those streams is one HTTP request.
- The other side of this exchange is the endpoint `GET /api/import-batches/{id}` ([Pattern 9](#pattern-9-accept-now-process-later)) and the worker that moves the batch to its final status ([Pattern 10](#pattern-10-background-worker-with-a-queue-and-a-recovery-scan)). The API tests poll the same endpoint in a loop ([Pattern 20](#pattern-20-tests-at-three-levels-in-net)), and the test of this pipeline moves a fake clock ([Pattern 26](#pattern-26-frontend-tests-with-vitest-and-playwright)).

**Why it is done this way.** The README describes the behaviour from the outside: the panel "polls until the batch reaches a truthful terminal state" ([README.md:52](README.md#L52)). The pipeline states the rules of that loop in four lines: how often to ask, which answer counts, and when to stop. Stopping matters as much as asking. A timer that nothing ends would go on sending a request every second after the batch is done, or after the component has left the page. The RxJS documentation describes `switchMap` as emitting values only from the most recent inner Observable, and the Angular documentation says that ending a subscription aborts a request that is still under way. Together they make sure that a slow answer for an old tick is never shown after a newer one.

**Look up.** [`timer`](https://rxjs.dev/api/index/function/timer) · [`switchMap`](https://rxjs.dev/api/index/function/switchMap) · [`takeWhile`](https://rxjs.dev/api/index/function/takeWhile) · [`takeUntilDestroyed`](https://angular.dev/api/core/rxjs-interop/takeUntilDestroyed) · [`DestroyRef`](https://angular.dev/api/core/DestroyRef) · [`Observable.pipe`](https://rxjs.dev/api/index/class/Observable#pipe) · [`Observable.subscribe`](https://rxjs.dev/api/index/class/Observable#subscribe) · [`Subscription`](https://rxjs.dev/api/index/class/Subscription) · [`Subscription.unsubscribe`](https://rxjs.dev/api/index/class/Subscription#unsubscribe) · [`crypto.randomUUID`](https://developer.mozilla.org/en-US/docs/Web/API/Crypto/randomUUID) · [`Array.prototype.sort`](https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Array/sort) · [`Date.parse`](https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Date/parse)

**Read more.** [Unsubscribing with takeUntilDestroyed](https://angular.dev/ecosystem/rxjs-interop/take-until-destroyed) · [RxJS interop with Angular signals](https://angular.dev/ecosystem/rxjs-interop) · [RxJS: Operators](https://rxjs.dev/guide/operators) · [RxJS: Subscription](https://rxjs.dev/guide/subscription) · [RxJS: Observable](https://rxjs.dev/guide/observable) · [Making HTTP requests](https://angular.dev/guide/http/making-requests) · [Asynchronous Request-Reply pattern](https://learn.microsoft.com/en-us/azure/architecture/patterns/asynchronous-request-reply)

### Pattern 26: Frontend tests with Vitest and Playwright

**What it is.** The frontend is tested at two levels. A *component test* creates one component in Node.js with a simulated page, answers its HTTP requests by hand and moves a fake clock. It runs under Vitest. An *end-to-end test* drives a real browser against the real Angular development server, the real API and the fake provider. It runs under Playwright, which starts those three programs itself.

**Where to find it.** The component tests are [Web/import-panel.component.spec.ts](src/Northstar.Web/src/app/imports/import-panel.component.spec.ts) and [Web/app.spec.ts](src/Northstar.Web/src/app/app.spec.ts). The end-to-end tests are [e2e/import-mixed-batch.spec.ts](tests/e2e/specs/import-mixed-batch.spec.ts) and [e2e/calculate-payroll-run.spec.ts](tests/e2e/specs/calculate-payroll-run.spec.ts), with their configuration in [e2e/playwright.config.ts](tests/e2e/playwright.config.ts).

The excerpts follow in three groups, each with its steps.

**[`src/Northstar.Web/src/app/imports/import-panel.component.spec.ts`](src/Northstar.Web/src/app/imports/import-panel.component.spec.ts#L7-L61)** · lines 7–61

<!-- x:ng-spec -->
```ts
describe('ImportPanelComponent', () => {
  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ImportPanelComponent],
      providers: [provideHttpClient(), provideHttpClientTesting()],
    }).compileComponents();
  });

  afterEach(() => {
    vi.useRealTimers();
  });

  it('starts a mixed import, polls until terminal, and renders row errors', async () => {
    vi.useFakeTimers();
    const fixture = TestBed.createComponent(ImportPanelComponent);
    const http = TestBed.inject(HttpTestingController);
    fixture.detectChanges();

    http.expectOne('/api/import-batches').flush([]);
    fixture.detectChanges();

    const button: HTMLButtonElement = fixture.nativeElement.querySelector(
      '[data-testid="import-mixed"]',
    );
    button.click();

    const create = http.expectOne('/api/import-batches');
    expect(create.request.method).toBe('POST');
    expect(create.request.body).toEqual({
      periodStart: '2026-08-24',
      periodEnd: '2026-08-30',
      providerScenario: 'mixed',
    });
    expect(create.request.headers.get('Idempotency-Key')).toBeTruthy();
    create.flush({ ...batch, status: 'Pending' });

    await vi.advanceTimersByTimeAsync(0);
    http.expectOne(`/api/import-batches/${batch.id}`).flush({
      ...batch,
      status: 'Processing',
    });

    await vi.advanceTimersByTimeAsync(1_000);
    http.expectOne(`/api/import-batches/${batch.id}`).flush(batch);
    http.expectOne(`/api/import-batches/${batch.id}/errors`).flush([rowError]);
    fixture.detectChanges();

    expect(
      fixture.nativeElement.querySelector('[data-testid="import-status"]').textContent,
    ).toContain('CompletedWithErrors');
    expect(fixture.nativeElement.querySelectorAll('[data-testid="import-error-row"]')).toHaveLength(
      1,
    );
    http.verify();
  });
  // ... continues in the file ...
```

**How to read the code.** A component test:

1. `describe('ImportPanelComponent', () => {` (line 7) groups the tests of one component, and each `it(…)` inside it is one test. The function given to `beforeEach` runs before every test.
2. `await TestBed.configureTestingModule({` (line 9) builds a small Angular environment for one test. It contains the component, and `providers: [provideHttpClient(), provideHttpClientTesting()],` (line 11) adds the HTTP client together with a test backend that takes the place of the network. No request leaves the test.
3. `vi.useFakeTimers();` (line 20) replaces the timers of the environment, such as `setTimeout` and `setInterval`, with a clock that only the test moves. The polling clock of [Pattern 25](#pattern-25-polling-with-rxjs-and-automatic-cleanup) is built on these timers.
4. `const fixture = TestBed.createComponent(ImportPanelComponent);` (line 21) creates the component. The *fixture* is the handle of the test on the component and on its part of the page. `const http = TestBed.inject(HttpTestingController);` (line 22) gets the controller of the test backend, which shows the requests that the code has made.
5. `fixture.detectChanges();` (line 23) runs a change detection cycle. The first cycle runs `ngOnInit`, and the component asks for the list of batches ([Pattern 24](#pattern-24-signals-computed-values-and-control-flow-templates)).
6. `http.expectOne('/api/import-batches').flush([]);` (line 25) does two things. `expectOne` checks that exactly one request for this address is waiting and returns it. `flush` answers it, here with an empty array. The next `detectChanges()` brings the page up to date.
7. `'[data-testid="import-mixed"]',` (line 29) is a CSS selector for the button of [Pattern 24](#pattern-24-signals-computed-values-and-control-flow-templates), and `button.click();` (line 31) presses it. The component calls `startImport('mixed')`.
8. `const create = http.expectOne('/api/import-batches');` (line 33) catches the `POST` that follows. The test checks its method, its body and, with `expect(create.request.headers.get('Idempotency-Key')).toBeTruthy();` (line 40), that a key was sent. The value of the key is random, so the test checks only that it is there.
9. `create.flush({ ...batch, status: 'Pending' });` (line 41) answers with a copy of the constant `batch` in which the status is replaced. The component now starts to poll.
10. `await vi.advanceTimersByTimeAsync(0);` (line 43) lets the timers run that are due now. The first tick of `timer(0, …)` fires, and the test answers the first status request with `Processing`.
11. `await vi.advanceTimersByTimeAsync(1_000);` (line 49) moves the clock one second on. The second tick fires. ``http.expectOne(`/api/import-batches/${batch.id}`).flush(batch);`` (line 50) answers it with the constant itself, whose status is `CompletedWithErrors`. That is a final status, so the component asks for the row errors, and ``http.expectOne(`/api/import-batches/${batch.id}/errors`).flush([rowError]);`` (line 51) answers that request too.
12. `).toContain('CompletedWithErrors');` (line 56) closes an assertion on the page itself: the element with the test id `import-status` shows the final status. The assertion after it counts one row in the table of errors.
13. `http.verify();` (line 60) fails the test if the code has made a request that the test did not answer. `vi.useRealTimers();` (line 16), in `afterEach`, puts the real timers back after every test.

**[`tests/e2e/specs/import-mixed-batch.spec.ts`](tests/e2e/specs/import-mixed-batch.spec.ts#L1-L17)** · lines 1–17

<!-- x:pw-spec -->
```ts
import { expect, test } from '@playwright/test';

test('imports a mixed provider batch and exposes row-level diagnostics', async ({ page }) => {
  await page.goto('/');

  await expect(page.getByTestId('import-panel')).toBeVisible();
  await page.getByTestId('import-mixed').click();

  await expect(page.getByTestId('import-status')).toHaveText('CompletedWithErrors');
  await expect(page.getByTestId('import-total-count')).toHaveText('4');
  await expect(page.getByTestId('import-error-count')).toHaveText('2');
  await expect(page.getByTestId('import-duplicate-count')).toHaveText('1');
  await expect(page.getByTestId('import-row-errors')).toContainText('EMPLOYEE_NOT_FOUND');
  await expect(page.getByTestId('import-row-errors')).toContainText('INVALID_HOURS');
  await expect(page.getByTestId('import-row-errors')).toContainText('DUPLICATE_TIME_ENTRY');
  await expect(page.getByTestId('import-error-row')).toHaveCount(3);
});
```

An end-to-end test:

14. `test('imports a mixed provider batch and exposes row-level diagnostics', async ({ page }) => {` (line 3) declares one test. Playwright hands it a `page`: a tab in a new browser context that no other test shares.
15. `await page.goto('/');` (line 4) opens the application. The address `/` is completed with the `baseURL` of the configuration, `http://127.0.0.1:4200` ([e2e/playwright.config.ts:11](tests/e2e/playwright.config.ts#L11)).
16. `await expect(page.getByTestId('import-panel')).toBeVisible();` (line 6) waits for the panel. `getByTestId` describes an element by its attribute `data-testid`. An assertion of this kind does not check once. It tries again until the condition holds or until its time limit has passed, which is five seconds unless the configuration says otherwise.
17. `await page.getByTestId('import-mixed').click();` (line 7) clicks the real button. From here on everything is real: the request, the `202`, the worker, the call to the fake provider with the scenario `mixed`, and the polling.
18. `await expect(page.getByTestId('import-status')).toHaveText('CompletedWithErrors');` (line 9) waits until the page shows the final status. The test contains no pause of a fixed length.
19. The next lines read the counters: four rows in total, two errors and one duplicate. These are the four rows of the scenario `mixed` ([Pattern 21](#pattern-21-minimal-api-endpoints-in-the-fake-provider)). `await expect(page.getByTestId('import-error-row')).toHaveCount(3);` (line 16) counts three rows in the table of errors, one for each of the three error codes named above it.

**[`tests/e2e/playwright.config.ts`](tests/e2e/playwright.config.ts#L36-L41)** · lines 36–41

<!-- x:pw-webserver -->
```ts
{
  command: 'npm --prefix ../../src/Northstar.Web start -- --host 127.0.0.1 --port 4200',
  url: 'http://127.0.0.1:4200',
  timeout: 120_000,
  reuseExistingServer: !process.env['CI']
}
```

Starting the servers:

20. The option `webServer` of the configuration is an array with three entries ([e2e/playwright.config.ts:21-42](tests/e2e/playwright.config.ts#L21-L42)), and the excerpt shows the third. `command: 'npm --prefix ../../src/Northstar.Web start -- --host 127.0.0.1 --port 4200',` (line 37) is the command that starts the Angular development server.
21. `url: 'http://127.0.0.1:4200',` (line 38) is the address that Playwright asks until it answers. Only then do the tests begin. `timeout: 120_000,` (line 39) allows two minutes for the start.
22. `reuseExistingServer: !process.env['CI']` (line 40) lets Playwright use a server that is already running at the address. The value is `false` when the environment variable `CI` is set.

The pieces around it:

- The other two entries start the fake provider and the API with `dotnet run` and wait for their `/health` endpoints ([e2e/playwright.config.ts:22-35](tests/e2e/playwright.config.ts#L22-L35)). The command for the API sets the environment variable `ConnectionStrings__Northstar` to a SQLite file under `/tmp` whose name contains `$$`. The shell replaces `$$` with its process id, so each start gets a new database, and the double underscore stands for the `:` of the configuration key `ConnectionStrings:Northstar`.
- There are 3 component tests and 2 end-to-end tests. The second test of the panel loads a failed batch that can be retried, presses the retry button, checks the `POST` to `/retry` with its key, and polls once to `Completed` ([Web/import-panel.component.spec.ts:63-116](src/Northstar.Web/src/app/imports/import-panel.component.spec.ts#L63-L116)). The test of the root component answers the two requests that the page makes when it loads and checks the status `Draft` ([Web/app.spec.ts:8-26](src/Northstar.Web/src/app/app.spec.ts#L8-L26)).
- The second end-to-end test walks through the payroll run: calculate, see two findings, add the missing rate, see the run become `ReadyForApproval`, approve ([e2e/calculate-payroll-run.spec.ts:3-21](tests/e2e/specs/calculate-payroll-run.spec.ts#L3-L21)). It also finds elements by role and by text, with `getByRole('heading', …)` and `getByText(…)`.
- Both levels find elements by the same `data-testid` attributes ([Pattern 24](#pattern-24-signals-computed-values-and-control-flow-templates)). The component tests use `querySelector('[data-testid="…"]')`, and Playwright uses `getByTestId`.
- The test data are two constants at the end of the file, typed with the interfaces of [Pattern 23](#pattern-23-angular-services-that-return-observables) ([Web/import-panel.component.spec.ts:119-147](src/Northstar.Web/src/app/imports/import-panel.component.spec.ts#L119-L147)). The spread syntax, as in `{ ...batch, status: 'Pending' }`, makes a changed copy for one answer.
- The component tests list their own providers and do not use `appConfig`, so the interceptor of [Pattern 22](#pattern-22-angular-standalone-bootstrap-and-an-http-interceptor) is not part of them. The headers are covered by the end-to-end tests, where the API would answer `401` without them ([Pattern 7](#pattern-7-tenant-scoping-from-header-to-query)).
- The spec files import nothing from Vitest. `describe`, `it`, `expect` and `vi` are global names, and [Web/tsconfig.spec.json](src/Northstar.Web/tsconfig.spec.json) tells TypeScript about them with the entry `vitest/globals`.
- `npm test` in `src/Northstar.Web` runs `ng test --watch=false` ([Web/package.json:9](src/Northstar.Web/package.json#L9)), and `npm test` in `tests/e2e` runs `playwright test` ([e2e/package.json](tests/e2e/package.json)). [Section 7](#7-tooling-and-configuration) lists the settings of both runners.

**Why it is done this way.** The testing notes of the repository give each level its job. For the first: "Angular unit tests verify polling, mixed row diagnostics, duplicate counts, and retry requests with a controlled HTTP backend." For the second: "Playwright proves the real import path and the complete calculate, remediate, recalculate, and approve path through the UI and API." ([docs/testing-strategy.md:8-9](docs/testing-strategy.md#L8-L9)). The same notes say how each level deals with waiting: "Angular advances a virtual timer" and "Playwright waits on user-visible state" ([docs/testing-strategy.md:13](docs/testing-strategy.md#L13)). A component test with a fake clock covers two seconds of polling in a few milliseconds and can produce any answer, including a failure. An end-to-end test is slower and needs three running programs, and it is the only test in which the browser, the proxy, the API, the worker and the provider work together. The README adds that Playwright "uses a unique temporary database" ([README.md:82](README.md#L82)), so a run starts from the seeded data and leaves the development database alone.

**Look up.** [`TestBed`](https://angular.dev/api/core/testing/TestBed) · [`TestBed.configureTestingModule`](https://angular.dev/api/core/testing/TestBed#configureTestingModule) · [`TestBed.createComponent`](https://angular.dev/api/core/testing/TestBed#createComponent) · [`TestBed.inject`](https://angular.dev/api/core/testing/TestBed#inject) · [`ComponentFixture.detectChanges`](https://angular.dev/api/core/testing/ComponentFixture#detectChanges) · [`ComponentFixture.nativeElement`](https://angular.dev/api/core/testing/ComponentFixture#nativeElement) · [`provideHttpClientTesting`](https://angular.dev/api/common/http/testing/provideHttpClientTesting) · [`HttpTestingController`](https://angular.dev/api/common/http/testing/HttpTestingController) · [`HttpTestingController.expectOne`](https://angular.dev/api/common/http/testing/HttpTestingController#expectOne) · [`HttpTestingController.verify`](https://angular.dev/api/common/http/testing/HttpTestingController#verify) · [`TestRequest.flush`](https://angular.dev/api/common/http/testing/TestRequest#flush) · [`describe`](https://vitest.dev/api/describe) · [`it`](https://vitest.dev/api/test) · [`beforeEach`](https://vitest.dev/api/hooks#beforeeach) · [`afterEach`](https://vitest.dev/api/hooks#aftereach) · [`expect`](https://vitest.dev/api/expect) · [`vi.useFakeTimers`](https://vitest.dev/api/vi#vi-usefaketimers) · [`vi.advanceTimersByTimeAsync`](https://vitest.dev/api/vi#vi-advancetimersbytimeasync) · [`vi.useRealTimers`](https://vitest.dev/api/vi#vi-userealtimers) · [`Element.querySelector`](https://developer.mozilla.org/en-US/docs/Web/API/Element/querySelector) · [`test`](https://playwright.dev/docs/api/class-test) · [`expect`](https://playwright.dev/docs/test-assertions) · [`page.goto`](https://playwright.dev/docs/api/class-page#page-goto) · [`page.getByTestId`](https://playwright.dev/docs/api/class-page#page-get-by-test-id) · [`page.getByRole`](https://playwright.dev/docs/api/class-page#page-get-by-role) · [`page.getByText`](https://playwright.dev/docs/api/class-page#page-get-by-text) · [`locator.click`](https://playwright.dev/docs/api/class-locator#locator-click) · [`expect(locator).toBeVisible`](https://playwright.dev/docs/api/class-locatorassertions#locator-assertions-to-be-visible) · [`expect(locator).toHaveText`](https://playwright.dev/docs/api/class-locatorassertions#locator-assertions-to-have-text) · [`expect(locator).toContainText`](https://playwright.dev/docs/api/class-locatorassertions#locator-assertions-to-contain-text) · [`expect(locator).toHaveCount`](https://playwright.dev/docs/api/class-locatorassertions#locator-assertions-to-have-count) · [`webServer (config)`](https://playwright.dev/docs/test-webserver)

**Read more.** [Unit testing](https://angular.dev/guide/testing) · [Basics of testing components](https://angular.dev/guide/testing/components-basics) · [Test requests (HttpClient testing)](https://angular.dev/guide/http/testing) · [Angular CLI: ng test](https://angular.dev/cli/test) · [Vitest: getting started](https://vitest.dev/guide/) · [Vitest: mocking timers](https://vitest.dev/guide/mocking/timers) · [Playwright: installation and first test](https://playwright.dev/docs/intro) · [Playwright: locators](https://playwright.dev/docs/locators) · [Playwright: assertions](https://playwright.dev/docs/test-assertions) · [Playwright: web server](https://playwright.dev/docs/test-webserver) · [Playwright test configuration](https://playwright.dev/docs/test-configuration) · [Configuration: environment variables provider](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-10.0#environment-variables-configuration-provider) · [jsdom](https://github.com/jsdom/jsdom)

## 4. C# language features

The compiler pass found 81 language features in the 54 C# files, with 2,508 uses between them. This section lists every one, grouped by what it is for. [Section 3](#3-patterns) shows most of them at work inside the patterns. Come here when you meet a piece of syntax and want its name, a short description and the page of the C# documentation that defines it.

### How to read the feature tables

- **Feature** is the name of the feature, linked to its page in the [C# language reference](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/) or in the C# guide on Microsoft Learn.
- **In plain words** says in a sentence what the feature does.
- **Uses** is the number of times the feature occurs in the syntax tree that the compiler builds from the code. A class declaration counts once, however long the class is, and a line with two lambdas counts twice.
- **Examples** gives up to two places to look at. Code under `src/` comes first, and the two places are in different files when that is possible. `+N more` says how many other places there are.
- **Patterns** gives the numbers of the patterns in section 3 whose text names the feature or counts it. A dash means that no pattern discusses it, and the examples are the place to look.

### The language version

No project file sets a language version, so every project gets the default for its target framework. All 7 projects target `net10.0`, and the default for that framework is C# 14 ([Configure the C# language version](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/configure-language-version), [What's new in C# 14](https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-14)). Two settings in [Directory.Build.props](Directory.Build.props) change how the language behaves in every project: `<Nullable>enable</Nullable>` turns on [nullable reference types](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/nullable-reference-types), and `<ImplicitUsings>enable</ImplicitUsings>` adds a set of `using` directives to every file without writing them ([Implicit using directives](https://learn.microsoft.com/en-us/dotnet/core/project-sdk/overview#implicit-using-directives)). [Section 7](#7-tooling-and-configuration) goes through that file line by line.

### What the counts show

- `var` is the most used feature, with 383 uses. `.editorconfig` states the preference: `csharp_style_var_when_type_is_apparent = true:suggestion` ([.editorconfig:16](.editorconfig#L16)).
- There is 1 constructor with a body in the whole codebase ([IntegrationTests/CustomWebApplicationFactory.cs:19](tests/Northstar.Api.IntegrationTests/CustomWebApplicationFactory.cs#L19)). Every other type gets its values through a [primary constructor](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/instance-constructors#primary-constructors) (18 classes), through a [positional record](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/record#positional-syntax-for-property-and-field-definition) (22 records) or through [property initializers](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/auto-implemented-properties) ([Pattern 3](#pattern-3-constructor-injection-with-primary-constructors)).
- `await` occurs 204 times in 72 `async` methods, and `try` with `finally` occurs 1 time. Cleanup is written with `using` declarations (78) and `await using` (14) instead ([Pattern 18](#pattern-18-async-all-the-way-with-cancellation)).
- Pattern matching takes the place of most comparisons with `null` and of most chains of `if`: there are 48 `is` patterns, 15 `not` patterns and 5 `switch` expressions ([Pattern matching overview](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/patterns/pattern-matching)).
- Types are closed and files are flat: 66 `sealed` types, 22 records and 52 file-scoped namespaces ([Pattern 19](#pattern-19-small-conventions-used-everywhere)).
- Some features appear exactly once, which makes them easy to study: [LINQ query syntax](https://learn.microsoft.com/en-us/dotnet/csharp/linq/get-started/query-expression-basics) ([Pattern 15](#pattern-15-read-queries-with-no-tracking-a-tenant-filter-and-projection)), [`await foreach`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/iteration-statements#await-foreach) ([Pattern 10](#pattern-10-background-worker-with-a-queue-and-a-recovery-scan)), a [`with` expression](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/with-expression) and a [Case guard `when`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/switch-expression#case-guards) in the tests, and a [`static` lambda](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/lambda-expressions#capture-of-outer-variables-and-variable-scope-in-lambda-expressions).

### The twelve most used features

| # | Feature | Uses | In plain words |
| ---: | --- | ---: | --- |
| 1 | [`var`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/declarations#implicitly-typed-local-variables) | 383 | Lets the compiler infer the type of a local variable. |
| 2 | [Lambda `x => ...`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/lambda-expressions) | 225 | An inline function; LINQ queries and EF Core configuration are built from them. |
| 3 | [`await`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/await) | 204 | Waits for an asynchronous operation and gives the thread back in the meantime. |
| 4 | [`using` directive](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/using-directive) | 134 | Imports a namespace for the file. |
| 5 | [Expression-bodied member `=>`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/lambda-operator#expression-body-definition) | 106 | A member whose whole body is one expression. |
| 6 | [Attribute `[Name]`](https://learn.microsoft.com/en-us/dotnet/csharp/advanced-topics/reflection-and-attributes/) | 93 | Metadata attached to code that frameworks read, such as `[HttpGet]` or `[Fact]`. |
| 7 | [`decimal` literal `12.50m`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/floating-point-numeric-types#real-literals) | 80 | The `m` suffix makes an exact base-10 number, the right type for money. |
| 8 | [`using` declaration](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/using) | 78 | `using var x = ...;` disposes the object when the enclosing scope ends. |
| 9 | [`async` method](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/async) | 72 | A method that can pause at `await` without blocking a thread; returns `Task`, `Task<T>` or `ValueTask`. |
| 10 | [`static` member of a normal class](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/static) | 69 | A helper that needs no object state. |
| 11 | [`sealed` type](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/sealed) | 66 | A type that cannot be inherited from; the default choice in this codebase. |
| 12 | [Nullable reference type `string?`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/nullable-reference-types) | 61 | Marks a reference that may be `null`, so the compiler can warn about unsafe use. |

### Types and members

*29 features, 654 uses.*

| Feature | In plain words | Uses | Examples | Patterns |
| --- | --- | ---: | --- | --- |
| [`class`](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/types/classes) | A reference type. | 55 | [Api/ApiControllerBase.cs:8](src/Northstar.Api/Controllers/ApiControllerBase.cs#L8)<br>[Api/ImportBatchesController.cs:9](src/Northstar.Api/Controllers/ImportBatchesController.cs#L9)<br>+53 more | – |
| [`record`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/record) | A type compared by its values, with a generated `ToString` and support for `with`. | 22 | [Api/PayrollRunsController.cs:91](src/Northstar.Api/Controllers/PayrollRunsController.cs#L91)<br>[Application/IPayrollProviderClient.cs:3](src/Northstar.Application/Abstractions/IPayrollProviderClient.cs#L3)<br>+20 more | [6](#pattern-6-result-objects-mapped-to-http-responses), [17](#pattern-17-entities-as-state-machines-and-pure-domain-services), [19](#pattern-19-small-conventions-used-everywhere) |
| [Positional record `record R(int A)`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/record#positional-syntax-for-property-and-field-definition) | A record declared by its constructor parameters, which also become its properties. | 22 | [Api/PayrollRunsController.cs:91](src/Northstar.Api/Controllers/PayrollRunsController.cs#L91)<br>[Application/IPayrollProviderClient.cs:3](src/Northstar.Application/Abstractions/IPayrollProviderClient.cs#L3)<br>+20 more | [3](#pattern-3-constructor-injection-with-primary-constructors), [19](#pattern-19-small-conventions-used-everywhere) |
| [Primary constructor `class C(Dep dep)`](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/instance-constructors#primary-constructors) | Constructor parameters written on the type itself and usable in the whole body. | 18 | [Api/ApiControllerBase.cs:8](src/Northstar.Api/Controllers/ApiControllerBase.cs#L8)<br>[Api/ImportBatchesController.cs:9](src/Northstar.Api/Controllers/ImportBatchesController.cs#L9)<br>+16 more | [3](#pattern-3-constructor-injection-with-primary-constructors), [13](#pattern-13-persistence-behind-an-interface), [19](#pattern-19-small-conventions-used-everywhere) |
| [`interface`](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/types/interfaces) | A contract a class implements; the Application layer owns the ones Infrastructure fulfils. | 5 | [Application/IApplicationDbContext.cs:12](src/Northstar.Application/Abstractions/IApplicationDbContext.cs#L12)<br>[Application/IClock.cs:3](src/Northstar.Application/Abstractions/IClock.cs#L3)<br>+3 more | [13](#pattern-13-persistence-behind-an-interface) |
| [`enum`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/enum) | A named set of constants, such as the batch and run statuses. | 8 | [Application/OperationResult.cs:3](src/Northstar.Application/Common/OperationResult.cs#L3)<br>[Domain/ImportBatch.cs:3](src/Northstar.Domain/Imports/ImportBatch.cs#L3)<br>+6 more | [17](#pattern-17-entities-as-state-machines-and-pure-domain-services) |
| [`abstract` class](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/abstract) | A base class that cannot be created directly; other classes inherit from it. | 2 | [Api/ApiControllerBase.cs:8](src/Northstar.Api/Controllers/ApiControllerBase.cs#L8)<br>[Application/IPayrollProviderClient.cs:28](src/Northstar.Application/Abstractions/IPayrollProviderClient.cs#L28) | [12](#pattern-12-bounded-retries-timeouts-and-exception-translation) |
| [`sealed` type](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/sealed) | A type that cannot be inherited from; the default choice in this codebase. | 66 | [Api/ImportBatchesController.cs:9](src/Northstar.Api/Controllers/ImportBatchesController.cs#L9)<br>[Api/PayrollRunsController.cs:8](src/Northstar.Api/Controllers/PayrollRunsController.cs#L8)<br>+64 more | [19](#pattern-19-small-conventions-used-everywhere) |
| [`static` class](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/static-classes-and-static-class-members) | A class that only holds static members; home of extension methods and helpers. | 7 | [Application/DependencyInjection.cs:11](src/Northstar.Application/DependencyInjection.cs#L11)<br>[Application/ImportBatchMappings.cs:5](src/Northstar.Application/Imports/ImportBatchMappings.cs#L5)<br>+5 more | [19](#pattern-19-small-conventions-used-everywhere) |
| [`partial` type](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/partial-classes-and-methods) | A type whose declaration may be split across files; `public partial class Program;` exposes the entry point to tests. | 2 | [Api/Program.cs:46](src/Northstar.Api/Program.cs#L46)<br>[FakeProvider/Program.cs:307](tools/Northstar.FakePayrollProvider/Program.cs#L307) | – |
| [Constructor with a body](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/constructors) | Code that runs when an object is created (most types here use primary constructors instead). | 1 | [IntegrationTests/CustomWebApplicationFactory.cs:19](tests/Northstar.Api.IntegrationTests/CustomWebApplicationFactory.cs#L19) | [3](#pattern-3-constructor-injection-with-primary-constructors) |
| [Get-only auto-property `{ get; }`](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/auto-implemented-properties) | A property that can be set only while the object is being created. | 22 | [Application/IApplicationDbContext.cs:20](src/Northstar.Application/Abstractions/IApplicationDbContext.cs#L20)<br>[Application/IClock.cs:5](src/Northstar.Application/Abstractions/IClock.cs#L5)<br>+20 more | [17](#pattern-17-entities-as-state-machines-and-pure-domain-services) |
| [`private set`](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/restricting-accessor-accessibility) | A property that anyone can read but only the type itself can change. | 17 | [Domain/ImportBatch.cs:19](src/Northstar.Domain/Imports/ImportBatch.cs#L19)<br>[Domain/PayrollRun.cs:23](src/Northstar.Domain/PayrollRuns/PayrollRun.cs#L23)<br>+15 more | [16](#pattern-16-optimistic-concurrency-with-a-version-number), [17](#pattern-17-entities-as-state-machines-and-pure-domain-services) |
| [Property initializer `{ get; set; } = value`](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/auto-implemented-properties) | Gives an auto-property its starting value. | 51 | [Application/IdempotencyRecord.cs:7](src/Northstar.Application/Idempotency/IdempotencyRecord.cs#L7)<br>[Domain/Employee.cs:7](src/Northstar.Domain/Employees/Employee.cs#L7)<br>+49 more | [17](#pattern-17-entities-as-state-machines-and-pure-domain-services) |
| [`readonly` field](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/readonly) | A field that can be assigned only during construction. | 12 | [Infrastructure/FakePayrollProviderClient.cs:13](src/Northstar.Infrastructure/Integrations/FakePayrollProviderClient.cs#L13)<br>[Infrastructure/ImportBatchQueue.cs:8](src/Northstar.Infrastructure/Messaging/ImportBatchQueue.cs#L8)<br>+10 more | – |
| [`static readonly` field](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/readonly) | One shared value for the type, assigned once. | 23 | [Application/ImportBatchProcessor.cs:14](src/Northstar.Application/Imports/ImportBatchProcessor.cs#L14)<br>[Application/ImportBatchService.cs:16](src/Northstar.Application/Imports/ImportBatchService.cs#L16)<br>+21 more | – |
| [`const`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/const) | A value fixed at compile time. | 9 | [Infrastructure/PayrollProviderOptions.cs:5](src/Northstar.Infrastructure/Integrations/PayrollProviderOptions.cs#L5)<br>[IntegrationTests/FakePayrollProviderClientTests.cs:200](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L200)<br>+7 more | [11](#pattern-11-typed-httpclient-with-validated-options), [21](#pattern-21-minimal-api-endpoints-in-the-fake-provider) |
| [Expression-bodied member `=>`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/lambda-operator#expression-body-definition) | A member whose whole body is one expression. | 106 | [Api/ApiControllerBase.cs:10](src/Northstar.Api/Controllers/ApiControllerBase.cs#L10)<br>[Api/ImportBatchesController.cs:118](src/Northstar.Api/Controllers/ImportBatchesController.cs#L118)<br>+104 more | [13](#pattern-13-persistence-behind-an-interface), [19](#pattern-19-small-conventions-used-everywhere) |
| [`override`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/override) | Replaces a virtual or abstract member of the base class. | 11 | [Infrastructure/ImportBatchWorker.cs:16](src/Northstar.Infrastructure/Messaging/ImportBatchWorker.cs#L16)<br>[Infrastructure/ApplicationDbContext.cs:51](src/Northstar.Infrastructure/Persistence/ApplicationDbContext.cs#L51)<br>+9 more | [14](#pattern-14-ef-core-model-configuration-with-the-fluent-api) |
| [Extension method `this T value`](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/extension-methods) | A static method that is called as if it belonged to the type, such as `AddApplication`. | 4 | [Application/DependencyInjection.cs:13](src/Northstar.Application/DependencyInjection.cs#L13)<br>[Application/ImportBatchMappings.cs:7](src/Northstar.Application/Imports/ImportBatchMappings.cs#L7)<br>+2 more | [19](#pattern-19-small-conventions-used-everywhere) |
| [Generic method `M<T>()`](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/generics/generic-methods) | A method with its own type parameter. | 3 | [Api/ApiControllerBase.cs:15](src/Northstar.Api/Controllers/ApiControllerBase.cs#L15)<br>[Application/IApplicationDbContext.cs:33](src/Northstar.Application/Abstractions/IApplicationDbContext.cs#L33)<br>+1 more | [13](#pattern-13-persistence-behind-an-interface) |
| [Generic constraint `where T : class`](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/generics/constraints-on-type-parameters) | Limits which types a type parameter accepts. | 2 | [Application/IApplicationDbContext.cs:33](src/Northstar.Application/Abstractions/IApplicationDbContext.cs#L33)<br>[Infrastructure/ApplicationDbContext.cs:44](src/Northstar.Infrastructure/Persistence/ApplicationDbContext.cs#L44) | [13](#pattern-13-persistence-behind-an-interface) |
| [Optional parameter `int x = 0`](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/named-and-optional-arguments#optional-arguments) | A parameter with a default value that callers may leave out. | 15 | [Api/ApiControllerBase.cs:42](src/Northstar.Api/Controllers/ApiControllerBase.cs#L42)<br>[Application/IPayrollProviderClient.cs:30](src/Northstar.Application/Abstractions/IPayrollProviderClient.cs#L30)<br>+13 more | [18](#pattern-18-async-all-the-way-with-cancellation), [19](#pattern-19-small-conventions-used-everywhere) |
| [`out` parameter](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/method-parameters#out-parameter-modifier) | A parameter the method must assign; the usual shape of `TryXxx` methods. | 2 | [Api/ApiControllerBase.cs:10](src/Northstar.Api/Controllers/ApiControllerBase.cs#L10)<br>[Api/HeaderUserContext.cs:5](src/Northstar.Api/Security/HeaderUserContext.cs#L5) | – |
| [`static` member of a normal class](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/static) | A helper that needs no object state. | 69 | [Application/OperationResult.cs:18](src/Northstar.Application/Common/OperationResult.cs#L18)<br>[Application/ImportBatchProcessor.cs:200](src/Northstar.Application/Imports/ImportBatchProcessor.cs#L200)<br>+67 more | – |
| [Attribute `[Name]`](https://learn.microsoft.com/en-us/dotnet/csharp/advanced-topics/reflection-and-attributes/) | Metadata attached to code that frameworks read, such as `[HttpGet]` or `[Fact]`. | 93 | [Api/ApiControllerBase.cs:7](src/Northstar.Api/Controllers/ApiControllerBase.cs#L7)<br>[Api/ImportBatchesController.cs:8](src/Northstar.Api/Controllers/ImportBatchesController.cs#L8)<br>+91 more | [20](#pattern-20-tests-at-three-levels-in-net) |
| [Attribute on a parameter](https://learn.microsoft.com/en-us/dotnet/csharp/advanced-topics/reflection-and-attributes/) | Metadata for one parameter, such as `[FromHeader]`. | 3 | [Api/ImportBatchesController.cs:69](src/Northstar.Api/Controllers/ImportBatchesController.cs#L69)<br>[Api/PayrollRunsController.cs:49](src/Northstar.Api/Controllers/PayrollRunsController.cs#L49)<br>+1 more | – |
| [Local function](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/building-software/local-functions) | A function declared inside another member. | 2 | [FakeProvider/Program.cs:91](tools/Northstar.FakePayrollProvider/Program.cs#L91)<br>[FakeProvider/Program.cs:109](tools/Northstar.FakePayrollProvider/Program.cs#L109) | [21](#pattern-21-minimal-api-endpoints-in-the-fake-provider) |
| [`static` local function](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/building-software/local-functions) | A local function that cannot capture surrounding variables. | 2 | [FakeProvider/Program.cs:91](tools/Northstar.FakePayrollProvider/Program.cs#L91)<br>[FakeProvider/Program.cs:109](tools/Northstar.FakePayrollProvider/Program.cs#L109) | [21](#pattern-21-minimal-api-endpoints-in-the-fake-provider) |

### Async and resource handling

*6 features, 372 uses.*

| Feature | In plain words | Uses | Examples | Patterns |
| --- | --- | ---: | --- | --- |
| [`async` method](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/async) | A method that can pause at `await` without blocking a thread; returns `Task`, `Task<T>` or `ValueTask`. | 72 | [Api/ImportBatchesController.cs:15](src/Northstar.Api/Controllers/ImportBatchesController.cs#L15)<br>[Api/PayrollRunsController.cs:14](src/Northstar.Api/Controllers/PayrollRunsController.cs#L14)<br>+70 more | [18](#pattern-18-async-all-the-way-with-cancellation) |
| [`async` lambda](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/lambda-expressions#async-lambdas) | A lambda that can use `await`. | 3 | [IntegrationTests/FakePayrollProviderClientTests.cs:105](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L105)<br>[FakeProvider/Program.cs:24](tools/Northstar.FakePayrollProvider/Program.cs#L24)<br>+1 more | [21](#pattern-21-minimal-api-endpoints-in-the-fake-provider) |
| [`await`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/await) | Waits for an asynchronous operation and gives the thread back in the meantime. | 204 | [Api/ImportBatchesController.cs:23](src/Northstar.Api/Controllers/ImportBatchesController.cs#L23)<br>[Api/PayrollRunsController.cs:22](src/Northstar.Api/Controllers/PayrollRunsController.cs#L22)<br>+202 more | [18](#pattern-18-async-all-the-way-with-cancellation) |
| [`await foreach`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/iteration-statements#await-foreach) | Loops over a stream whose items arrive asynchronously (`IAsyncEnumerable<T>`). | 1 | [Infrastructure/ImportBatchWorker.cs:22](src/Northstar.Infrastructure/Messaging/ImportBatchWorker.cs#L22) | [10](#pattern-10-background-worker-with-a-queue-and-a-recovery-scan), [18](#pattern-18-async-all-the-way-with-cancellation) |
| [`await using`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/using) | Disposes an object asynchronously (`DisposeAsync`) at the end of the scope. | 14 | [Api/Program.cs:39](src/Northstar.Api/Program.cs#L39)<br>[Application/ImportBatchProcessor.cs:120](src/Northstar.Application/Imports/ImportBatchProcessor.cs#L120)<br>+12 more | [10](#pattern-10-background-worker-with-a-queue-and-a-recovery-scan), [13](#pattern-13-persistence-behind-an-interface), [18](#pattern-18-async-all-the-way-with-cancellation) |
| [`using` declaration](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/using) | `using var x = ...;` disposes the object when the enclosing scope ends. | 78 | [Infrastructure/FakePayrollProviderClient.cs:80](src/Northstar.Infrastructure/Integrations/FakePayrollProviderClient.cs#L80)<br>[Infrastructure/ImportBatchWorker.cs:59](src/Northstar.Infrastructure/Messaging/ImportBatchWorker.cs#L59)<br>+76 more | [12](#pattern-12-bounded-retries-timeouts-and-exception-translation), [18](#pattern-18-async-all-the-way-with-cancellation), [20](#pattern-20-tests-at-three-levels-in-net) |

### Pattern matching

*9 features, 92 uses.*

| Feature | In plain words | Uses | Examples | Patterns |
| --- | --- | ---: | --- | --- |
| [`is` pattern](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/is) | Tests a value against a pattern, as in `is null` or `is { } value`. | 48 | [Application/ImportBatchProcessor.cs:23](src/Northstar.Application/Imports/ImportBatchProcessor.cs#L23)<br>[Application/ImportBatchService.cs:69](src/Northstar.Application/Imports/ImportBatchService.cs#L69)<br>+46 more | [16](#pattern-16-optimistic-concurrency-with-a-version-number), [17](#pattern-17-entities-as-state-machines-and-pure-domain-services) |
| [`not` pattern](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/patterns#logical-patterns) | Matches when the inner pattern does not; `is not null` is the common null check. | 15 | [Application/ImportBatchProcessor.cs:322](src/Northstar.Application/Imports/ImportBatchProcessor.cs#L322)<br>[Application/ImportBatchService.cs:114](src/Northstar.Application/Imports/ImportBatchService.cs#L114)<br>+13 more | [20](#pattern-20-tests-at-three-levels-in-net) |
| [`and` pattern](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/patterns#logical-patterns) | Both patterns must match, as in `is >= 1 and <= 10`. | 4 | [Infrastructure/DependencyInjection.cs:42](src/Northstar.Infrastructure/DependencyInjection.cs#L42)<br>[IntegrationTests/ImportBatchesApiTests.cs:389](tests/Northstar.Api.IntegrationTests/ImportBatchesApiTests.cs#L389)<br>+2 more | [11](#pattern-11-typed-httpclient-with-validated-options), [20](#pattern-20-tests-at-three-levels-in-net) |
| [`or` pattern](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/patterns#logical-patterns) | Either pattern may match, as in `is A or B`. | 6 | [Domain/PayrollRun.cs:33](src/Northstar.Domain/PayrollRuns/PayrollRun.cs#L33)<br>[Infrastructure/FakePayrollProviderClient.cs:184](src/Northstar.Infrastructure/Integrations/FakePayrollProviderClient.cs#L184)<br>+4 more | [12](#pattern-12-bounded-retries-timeouts-and-exception-translation), [17](#pattern-17-entities-as-state-machines-and-pure-domain-services), [21](#pattern-21-minimal-api-endpoints-in-the-fake-provider) |
| [Relational pattern `is >= 1`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/patterns#relational-patterns) | Compares a value with a constant inside a pattern. | 6 | [Infrastructure/DependencyInjection.cs:42](src/Northstar.Infrastructure/DependencyInjection.cs#L42)<br>[Infrastructure/DependencyInjection.cs:43](src/Northstar.Infrastructure/DependencyInjection.cs#L43)<br>+4 more | [11](#pattern-11-typed-httpclient-with-validated-options) |
| [Property pattern `is { } x`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/patterns#property-pattern) | Matches on properties; `{ }` matches any non-null value and can name it. | 2 | [Application/ImportBatchProcessor.cs:286](src/Northstar.Application/Imports/ImportBatchProcessor.cs#L286)<br>[Application/ImportBatchProcessor.cs:306](src/Northstar.Application/Imports/ImportBatchProcessor.cs#L306) | – |
| [Discard `_`](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/patterns/discards) | A placeholder for a value the code does not need; in a `switch` it is the catch-all arm. | 5 | [Api/ApiControllerBase.cs:31](src/Northstar.Api/Controllers/ApiControllerBase.cs#L31)<br>[Domain/WageRateSelector.cs:64](src/Northstar.Domain/WageRates/WageRateSelector.cs#L64)<br>+3 more | – |
| [`switch` expression](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/switch-expression) | Chooses a value by pattern; each arm is `pattern => result`. | 5 | [Api/ApiControllerBase.cs:16](src/Northstar.Api/Controllers/ApiControllerBase.cs#L16)<br>[Domain/WageRateSelector.cs:49](src/Northstar.Domain/WageRates/WageRateSelector.cs#L49)<br>+3 more | [6](#pattern-6-result-objects-mapped-to-http-responses), [17](#pattern-17-entities-as-state-machines-and-pure-domain-services), [20](#pattern-20-tests-at-three-levels-in-net), [21](#pattern-21-minimal-api-endpoints-in-the-fake-provider) |
| [Case guard `when`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/switch-expression#case-guards) | An extra condition on a `switch` arm. | 1 | [IntegrationTests/DeterministicPayrollProviderClient.cs:33](tests/Northstar.Api.IntegrationTests/DeterministicPayrollProviderClient.cs#L33) | [20](#pattern-20-tests-at-three-levels-in-net) |

### Null handling

*5 features, 138 uses.*

| Feature | In plain words | Uses | Examples | Patterns |
| --- | --- | ---: | --- | --- |
| [Nullable reference type `string?`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/nullable-reference-types) | Marks a reference that may be `null`, so the compiler can warn about unsafe use. | 61 | [Api/ApiControllerBase.cs:42](src/Northstar.Api/Controllers/ApiControllerBase.cs#L42)<br>[Api/ImportBatchesController.cs:69](src/Northstar.Api/Controllers/ImportBatchesController.cs#L69)<br>+59 more | [19](#pattern-19-small-conventions-used-everywhere), [21](#pattern-21-minimal-api-endpoints-in-the-fake-provider) |
| [Nullable value type `DateOnly?`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/nullable-value-types) | A value type that can also hold `null`. | 42 | [Application/IPayrollProviderClient.cs:14](src/Northstar.Application/Abstractions/IPayrollProviderClient.cs#L14)<br>[Application/ImportBatchView.cs:24](src/Northstar.Application/Imports/ImportBatchView.cs#L24)<br>+40 more | – |
| [Null-coalescing `??`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/null-coalescing-operator) | Uses the right-hand value when the left is `null`. | 15 | [Api/ApiControllerBase.cs:24](src/Northstar.Api/Controllers/ApiControllerBase.cs#L24)<br>[Api/ImportBatchesController.cs:85](src/Northstar.Api/Controllers/ImportBatchesController.cs#L85)<br>+13 more | [16](#pattern-16-optimistic-concurrency-with-a-version-number) |
| [Null-conditional `?.`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/member-access-operators#null-conditional-operators--and-) | Reads a member only when the object is not `null`. | 6 | [Api/HeaderUserContext.cs:7](src/Northstar.Api/Security/HeaderUserContext.cs#L7)<br>[Application/ImportBatchProcessor.cs:254](src/Northstar.Application/Imports/ImportBatchProcessor.cs#L254)<br>+4 more | [7](#pattern-7-tenant-scoping-from-header-to-query) |
| [Null-forgiving `!`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/null-forgiving) | Tells the compiler a value is not `null` here; it does nothing at run time. | 14 | [Api/ImportBatchesController.cs:121](src/Northstar.Api/Controllers/ImportBatchesController.cs#L121)<br>[Api/WageRatesController.cs:48](src/Northstar.Api/Controllers/WageRatesController.cs#L48)<br>+12 more | [9](#pattern-9-accept-now-process-later), [12](#pattern-12-bounded-retries-timeouts-and-exception-translation) |

### Expressions, literals and operators

*22 features, 995 uses.*

| Feature | In plain words | Uses | Examples | Patterns |
| --- | --- | ---: | --- | --- |
| [Lambda `x => ...`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/lambda-expressions) | An inline function; LINQ queries and EF Core configuration are built from them. | 225 | [Api/Program.cs:10](src/Northstar.Api/Program.cs#L10)<br>[Application/ImportBatchProcessor.cs:21](src/Northstar.Application/Imports/ImportBatchProcessor.cs#L21)<br>+223 more | [14](#pattern-14-ef-core-model-configuration-with-the-fluent-api), [15](#pattern-15-read-queries-with-no-tracking-a-tenant-filter-and-projection), [20](#pattern-20-tests-at-three-levels-in-net) |
| [`static` lambda](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/lambda-expressions#capture-of-outer-variables-and-variable-scope-in-lambda-expressions) | A lambda that is not allowed to capture variables from outside. | 1 | [IntegrationTests/DeterministicPayrollProviderClient.cs:25](tests/Northstar.Api.IntegrationTests/DeterministicPayrollProviderClient.cs#L25) | – |
| [`var`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/declarations#implicitly-typed-local-variables) | Lets the compiler infer the type of a local variable. | 383 | [Api/ImportBatchesController.cs:18](src/Northstar.Api/Controllers/ImportBatchesController.cs#L18)<br>[Api/PayrollRunsController.cs:17](src/Northstar.Api/Controllers/PayrollRunsController.cs#L17)<br>+381 more | [15](#pattern-15-read-queries-with-no-tracking-a-tenant-filter-and-projection), [19](#pattern-19-small-conventions-used-everywhere) |
| [Target-typed `new()`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/new-operator#target-typed-new) | Leaves out the type name when the compiler already knows it. | 55 | [Api/ApiControllerBase.cs:43](src/Northstar.Api/Controllers/ApiControllerBase.cs#L43)<br>[Application/OperationResult.cs:18](src/Northstar.Application/Common/OperationResult.cs#L18)<br>+53 more | [6](#pattern-6-result-objects-mapped-to-http-responses), [17](#pattern-17-entities-as-state-machines-and-pure-domain-services), [19](#pattern-19-small-conventions-used-everywhere) |
| [Object initializer `new T { A = 1 }`](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/object-and-collection-initializers#object-initializers) | Sets properties as the object is created. | 41 | [Api/ApiControllerBase.cs:44](src/Northstar.Api/Controllers/ApiControllerBase.cs#L44)<br>[Application/ImportBatchProcessor.cs:149](src/Northstar.Application/Imports/ImportBatchProcessor.cs#L149)<br>+39 more | – |
| [Collection initializer `new() { a, b }`](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/object-and-collection-initializers#collection-initializers) | Fills a collection as it is created. | 1 | [Application/ImportBatchService.cs:17](src/Northstar.Application/Imports/ImportBatchService.cs#L17) | – |
| [Collection expression `[a, b]`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/collection-expressions) | Short syntax that creates a list, array or other collection; `[]` is an empty one. | 30 | [Domain/ImportBatch.cs:33](src/Northstar.Domain/Imports/ImportBatch.cs#L33)<br>[Domain/PayrollRun.cs:28](src/Northstar.Domain/PayrollRuns/PayrollRun.cs#L28)<br>+28 more | [17](#pattern-17-entities-as-state-machines-and-pure-domain-services), [19](#pattern-19-small-conventions-used-everywhere), [20](#pattern-20-tests-at-three-levels-in-net) |
| [Anonymous type `new { ... }`](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/anonymous-types) | An unnamed object with read-only properties, used for projections, composite keys and JSON bodies. | 24 | [Api/ImportBatchesController.cs:121](src/Northstar.Api/Controllers/ImportBatchesController.cs#L121)<br>[Api/Program.cs:34](src/Northstar.Api/Program.cs#L34)<br>+22 more | [9](#pattern-9-accept-now-process-later), [10](#pattern-10-background-worker-with-a-queue-and-a-recovery-scan), [14](#pattern-14-ef-core-model-configuration-with-the-fluent-api), [21](#pattern-21-minimal-api-endpoints-in-the-fake-provider) |
| [`with` expression](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/with-expression) | Copies a record and changes some properties. | 1 | [UnitTests/PayrollCalculatorTests.cs:50](tests/Northstar.Domain.UnitTests/PayrollCalculatorTests.cs#L50) | – |
| [String interpolation `$"{x}"`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/tokens/interpolated) | Builds a string from expressions, with optional format specifiers. | 34 | [Api/ApiControllerBase.cs:31](src/Northstar.Api/Controllers/ApiControllerBase.cs#L31)<br>[Api/WageRatesController.cs:48](src/Northstar.Api/Controllers/WageRatesController.cs#L48)<br>+32 more | – |
| [Raw string literal `"""..."""`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/tokens/raw-string) | A string that needs no escaping, handy for JSON in tests. | 2 | [IntegrationTests/FakePayrollProviderClientTests.cs:200](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L200)<br>[IntegrationTests/FakePayrollProviderClientTests.cs:218](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L218) | [20](#pattern-20-tests-at-three-levels-in-net) |
| [`decimal` literal `12.50m`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/floating-point-numeric-types#real-literals) | The `m` suffix makes an exact base-10 number, the right type for money. | 80 | [Domain/PayrollCalculator.cs:55](src/Northstar.Domain/PayrollRuns/PayrollCalculator.cs#L55)<br>[Domain/WageRate.cs:25](src/Northstar.Domain/WageRates/WageRate.cs#L25)<br>+78 more | [19](#pattern-19-small-conventions-used-everywhere) |
| [Digit separator `10_000`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/integral-numeric-types#integer-literals) | Underscores that make numbers easier to read. | 6 | [Infrastructure/DependencyInjection.cs:43](src/Northstar.Infrastructure/DependencyInjection.cs#L43)<br>[Infrastructure/FakePayrollProviderClient.cs:176](src/Northstar.Infrastructure/Integrations/FakePayrollProviderClient.cs#L176)<br>+4 more | [11](#pattern-11-typed-httpclient-with-validated-options) |
| [`default`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/default#default-literal) | The default value of the expected type (`null`, `0`, an empty struct). | 5 | [Application/OperationResult.cs:14](src/Northstar.Application/Common/OperationResult.cs#L14)<br>[Infrastructure/DatabaseInitializer.cs:17](src/Northstar.Infrastructure/Persistence/DatabaseInitializer.cs#L17)<br>+3 more | – |
| [`nameof`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/nameof) | The name of a symbol as a string, checked by the compiler. | 4 | [Api/ImportBatchesController.cs:121](src/Northstar.Api/Controllers/ImportBatchesController.cs#L121)<br>[Domain/ImportBatch.cs:47](src/Northstar.Domain/Imports/ImportBatch.cs#L47)<br>+2 more | [9](#pattern-9-accept-now-process-later), [16](#pattern-16-optimistic-concurrency-with-a-version-number), [17](#pattern-17-entities-as-state-machines-and-pure-domain-services) |
| [Named argument `name: value`](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/named-and-optional-arguments#named-arguments) | Passes an argument by parameter name, which keeps long calls readable. | 49 | [Application/ImportBatchProcessor.cs:46](src/Northstar.Application/Imports/ImportBatchProcessor.cs#L46)<br>[Application/PayrollRunService.cs:37](src/Northstar.Application/PayrollRuns/PayrollRunService.cs#L37)<br>+47 more | [12](#pattern-12-bounded-retries-timeouts-and-exception-translation), [15](#pattern-15-read-queries-with-no-tracking-a-tenant-filter-and-projection), [19](#pattern-19-small-conventions-used-everywhere), [21](#pattern-21-minimal-api-endpoints-in-the-fake-provider) |
| [`out var` declaration](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/method-parameters#out-parameter-modifier) | Declares the variable right in the call: `TryParse(text, out var id)`. | 14 | [Api/ImportBatchesController.cs:18](src/Northstar.Api/Controllers/ImportBatchesController.cs#L18)<br>[Api/PayrollRunsController.cs:17](src/Northstar.Api/Controllers/PayrollRunsController.cs#L17)<br>+12 more | [7](#pattern-7-tenant-scoping-from-header-to-query) |
| [Cast `(T)value`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/type-testing-and-cast#cast-expression) | Explicit conversion to another type. | 3 | [Infrastructure/FakePayrollProviderClient.cs:108](src/Northstar.Infrastructure/Integrations/FakePayrollProviderClient.cs#L108)<br>[Infrastructure/FakePayrollProviderClient.cs:115](src/Northstar.Infrastructure/Integrations/FakePayrollProviderClient.cs#L115)<br>+1 more | [12](#pattern-12-bounded-retries-timeouts-and-exception-translation) |
| [Conditional operator `c ? a : b`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/conditional-operator) | Chooses one of two values. | 25 | [Api/ImportBatchesController.cs:121](src/Northstar.Api/Controllers/ImportBatchesController.cs#L121)<br>[Application/ImportBatchProcessor.cs:323](src/Northstar.Application/Imports/ImportBatchProcessor.cs#L323)<br>+23 more | [17](#pattern-17-entities-as-state-machines-and-pure-domain-services) |
| [Range `text[..n]`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/member-access-operators#range-operator-) | Takes a slice by start and end position. | 2 | [Application/ImportBatchProcessor.cs:323](src/Northstar.Application/Imports/ImportBatchProcessor.cs#L323)<br>[FakeProvider/Program.cs:288](tools/Northstar.FakePayrollProvider/Program.cs#L288) | – |
| [`throw` expression](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/exception-handling-statements#the-throw-expression) | `throw` used as an expression, for example after `??` or in a `switch` arm. | 9 | [Api/ApiControllerBase.cs:31](src/Northstar.Api/Controllers/ApiControllerBase.cs#L31)<br>[IntegrationTests/FakePayrollProviderClientTests.cs:262](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L262)<br>+7 more | – |
| [LINQ query syntax `from ... join ... select`](https://learn.microsoft.com/en-us/dotnet/csharp/linq/get-started/query-expression-basics) | SQL-like form of a LINQ query. | 1 | [Application/PayrollRunService.cs:105](src/Northstar.Application/PayrollRuns/PayrollRunService.cs#L105) | [15](#pattern-15-read-queries-with-no-tracking-a-tenant-filter-and-projection) |

### Statements and exceptions

*7 features, 69 uses.*

| Feature | In plain words | Uses | Examples | Patterns |
| --- | --- | ---: | --- | --- |
| [`foreach`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/iteration-statements#the-foreach-statement) | Loops over every item of a collection. | 7 | [Application/PayrollRunService.cs:149](src/Northstar.Application/PayrollRuns/PayrollRunService.cs#L149)<br>[Domain/PayrollCalculator.cs:39](src/Northstar.Domain/PayrollRuns/PayrollCalculator.cs#L39)<br>+5 more | – |
| [`for`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/iteration-statements#the-for-statement) | Counted loop. | 4 | [Application/ImportBatchProcessor.cs:122](src/Northstar.Application/Imports/ImportBatchProcessor.cs#L122)<br>[Infrastructure/FakePayrollProviderClient.cs:23](src/Northstar.Infrastructure/Integrations/FakePayrollProviderClient.cs#L23)<br>+2 more | – |
| [`try` / `catch`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/exception-handling-statements#the-try-catch-statement) | Handles exceptions raised in the `try` block. | 12 | [Application/ImportBatchProcessor.cs:28](src/Northstar.Application/Imports/ImportBatchProcessor.cs#L28)<br>[Application/ImportBatchService.cs:164](src/Northstar.Application/Imports/ImportBatchService.cs#L164)<br>+10 more | – |
| [`try` / `finally`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/exception-handling-statements#the-try-finally-statement) | Runs cleanup code whether or not an exception happened. | 1 | [Infrastructure/ImportBatchWorker.cs:20](src/Northstar.Infrastructure/Messaging/ImportBatchWorker.cs#L20) | [10](#pattern-10-background-worker-with-a-queue-and-a-recovery-scan) |
| [`catch (SpecificException)`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/exception-handling-statements#the-try-catch-statement) | Handles only one exception type and lets the others pass. | 19 | [Application/ImportBatchProcessor.cs:32](src/Northstar.Application/Imports/ImportBatchProcessor.cs#L32)<br>[Application/ImportBatchService.cs:169](src/Northstar.Application/Imports/ImportBatchService.cs#L169)<br>+17 more | [12](#pattern-12-bounded-retries-timeouts-and-exception-translation), [16](#pattern-16-optimistic-concurrency-with-a-version-number) |
| [Exception filter `catch (X) when (...)`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/exception-handling-statements#a-when-exception-filter) | Catches the exception only when the condition is true. | 6 | [Infrastructure/FakePayrollProviderClient.cs:122](src/Northstar.Infrastructure/Integrations/FakePayrollProviderClient.cs#L122)<br>[Infrastructure/ImportBatchWorker.cs:30](src/Northstar.Infrastructure/Messaging/ImportBatchWorker.cs#L30)<br>+4 more | [10](#pattern-10-background-worker-with-a-queue-and-a-recovery-scan), [12](#pattern-12-bounded-retries-timeouts-and-exception-translation), [18](#pattern-18-async-all-the-way-with-cancellation) |
| [`throw` statement](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/exception-handling-statements#the-throw-statement) | Raises an exception; a bare `throw;` re-raises the one being handled. | 20 | [Application/ImportBatchService.cs:184](src/Northstar.Application/Imports/ImportBatchService.cs#L184)<br>[Domain/ImportBatch.cs:47](src/Northstar.Domain/Imports/ImportBatch.cs#L47)<br>+18 more | – |

### File organization

*3 features, 188 uses.*

| Feature | In plain words | Uses | Examples | Patterns |
| --- | --- | ---: | --- | --- |
| [File-scoped namespace](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/namespace) | `namespace X;` applies to the whole file without braces. | 52 | [Api/ApiControllerBase.cs:5](src/Northstar.Api/Controllers/ApiControllerBase.cs#L5)<br>[Api/ImportBatchesController.cs:6](src/Northstar.Api/Controllers/ImportBatchesController.cs#L6)<br>+50 more | [19](#pattern-19-small-conventions-used-everywhere) |
| [`using` directive](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/using-directive) | Imports a namespace for the file. | 134 | [Api/ApiControllerBase.cs:1](src/Northstar.Api/Controllers/ApiControllerBase.cs#L1)<br>[Api/ImportBatchesController.cs:1](src/Northstar.Api/Controllers/ImportBatchesController.cs#L1)<br>+132 more | – |
| [Top-level statements](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/program-structure/top-level-statements) | A `Program.cs` without a `Main` method; the file itself is the entry point. | 2 | [Api/Program.cs:6](src/Northstar.Api/Program.cs#L6)<br>[FakeProvider/Program.cs:5](tools/Northstar.FakePayrollProvider/Program.cs#L5) | [21](#pattern-21-minimal-api-endpoints-in-the-fake-provider) |


## 5. .NET API inventory

This section lists every type and member that the C# code uses from outside this repository: 568 APIs (189 types and 379 members) in 37 namespaces, used in 3,570 places. The compiler resolved each one ([section 1](#1-how-to-read-this-document) says how), so an API is listed whenever the code refers to it, also when no line spells out its name.

### How to read the API tables

- **API.** A row in bold is a type. The rows under it are the members of that type that the code uses: constructors first, then properties, indexers, methods, operators, conversions, fields and enum members. The name links to the page of the API in the API browser on Microsoft Learn, in the version that this solution uses ([.NET API browser](https://learn.microsoft.com/en-us/dotnet/api/?view=net-10.0), [ASP.NET Core API browser](https://learn.microsoft.com/en-us/dotnet/api/?view=aspnetcore-10.0)). The xUnit rows have no page there, so their names are plain text. Under the name stands the kind of API, with a note when the code never writes the name, such as "called for you by `await`".
- **Overloads.** When the code uses several overloads of a method, each one has its own row. The parameter types in round brackets tell them apart.
- **What it does.** The first sentence of the summary in the XML documentation file that ships with the library. A text of more than 170 characters is cut at a word and ends in `…`. The words are Microsoft's, and those of the xUnit project on the `Xunit` rows. A † marks the four rows that this document describes in its own words.
- **Uses.** The number of places where the code refers to the API. On a type row it counts the places where the name of the type is written, and a C# keyword such as `string` or `int` counts for the type it stands for. A dash means that the type is used only through the members below it.
- **Where.** Up to two of those places, code under `src/` first, and `+N more` for the rest.
- **Source.** `source` opens the declaration in the [.NET Source Browser (source.dot.net)](https://source.dot.net/). `GitHub` opens it in the repository of the library at the release tag, for the three libraries that the Source Browser does not index.

To find an API by name, search the PDF or the Markdown file for its short name, such as `AddScoped`. In the PDF a long name can run over two lines of a table cell, and a search for the whole name can then miss it. To find it by topic, start from [section 3](#3-patterns): every pattern ends with a **Look up** line that names its APIs.

### The twenty most used APIs

| # | API | Uses | What it does |
| ---: | --- | ---: | --- |
| 1 | [`Assert`](https://github.com/xunit/assert.xunit/blob/v2-2.9.3/Assert.cs#L27)<br>*class* | 239 | Contains various static methods that are used to verify that conditions are met during the process of running tests. |
| 2 | [`string`](https://learn.microsoft.com/en-us/dotnet/api/system.string?view=net-10.0)<br>*class, C# keyword for `System.String`* | 238 | Represents text as a sequence of UTF-16 code units. |
| 3 | [`Guid`](https://learn.microsoft.com/en-us/dotnet/api/system.guid?view=net-10.0)<br>*struct* | 201 | Represents a globally unique identifier (GUID). |
| 4 | [`Task<TResult>.GetAwaiter()`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1.getawaiter?view=net-10.0#system-threading-tasks-task-1-getawaiter)<br>*method, called for you by `await`* | 177 | Gets an awaiter used to await this Task. |
| 5 | [`Assert.Equal<T>(T, T)`](https://github.com/xunit/assert.xunit/blob/v2-2.9.3/EqualityAsserts.cs#L81)<br>*static method* | 132 | Verifies that two objects are equal, using a default comparer. |
| 6 | [`DateOnly`](https://learn.microsoft.com/en-us/dotnet/api/system.dateonly?view=net-10.0)<br>*struct* | 99 | Represents dates with values ranging from January 1, 0001 Anno Domini (Common Era) through December 31, 9999 A.D. (C.E.) in the Gregorian calendar. |
| 7 | [`EntityTypeBuilder<TEntity>.Property<TProperty>(Expression<Func<TEntity, TProperty>>)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.metadata.builders.entitytypebuilder-1.property?view=efcore-10.0#microsoft-entityframeworkcore-metadata-builders-entitytypebuilder-1-property-1(system-linq-expressions-expression((system-func((-0-0))))))<br>*method* | 64 | Returns an object that can be used to configure a property of the entity type. |
| 8 | [`int`](https://learn.microsoft.com/en-us/dotnet/api/system.int32?view=net-10.0)<br>*struct, C# keyword for `System.Int32`* | 59 | Represents a 32-bit signed integer. |
| 9 | [`CancellationToken`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken?view=net-10.0)<br>*struct* | 59 | Propagates notification that operations should be canceled. |
| 10 | [`Task`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task?view=net-10.0)<br>*class* | 59 | Represents an asynchronous operation. |
| 11 | [`Guid.operator ==(Guid, Guid)`](https://learn.microsoft.com/en-us/dotnet/api/system.guid.op_equality?view=net-10.0#system-guid-op-equality(system-guid-system-guid))<br>*operator* | 49 | Indicates whether the values of two specified Guid objects are equal. |
| 12 | [`decimal`](https://learn.microsoft.com/en-us/dotnet/api/system.decimal?view=net-10.0)<br>*struct, C# keyword for `System.Decimal`* | 47 | Represents a decimal floating-point number. |
| 13 | [`void`](https://learn.microsoft.com/en-us/dotnet/api/system.void?view=net-10.0)<br>*struct, C# keyword for `System.Void`* | 45 | Specifies a return value type for a method that does not return a value. |
| 14 | [`Task<TResult>`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1?view=net-10.0)<br>*class* | 45 | Represents an asynchronous operation that can return a value. |
| 15 | [`PropertyBuilder<TProperty>.HasMaxLength(int)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.metadata.builders.propertybuilder-1.hasmaxlength?view=efcore-10.0#microsoft-entityframeworkcore-metadata-builders-propertybuilder-1-hasmaxlength(system-int32))<br>*method* | 42 | Configures the maximum length of data that can be stored in this property. |
| 16 | [`FactAttribute`](https://github.com/xunit/xunit/blob/v2-2.9.3/src/xunit.core/FactAttribute.cs#L13)<br>*class, used as an attribute* | 41 | Attribute that is applied to a method to indicate that it is a fact that should be run by the test runner. |
| 17 | [`StatusCodes`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.statuscodes?view=aspnetcore-10.0)<br>*static class* | 40 | A collection of constants for HTTP status codes. |
| 18 | [`new DateOnly(int, int, int)`](https://learn.microsoft.com/en-us/dotnet/api/system.dateonly.-ctor?view=net-10.0#system-dateonly-ctor(system-int32-system-int32-system-int32))<br>*constructor* | 40 | Creates a new instance of the DateOnly structure to the specified year, month, and day. |
| 19 | [`Assert.Equal(string, string)`](https://github.com/xunit/assert.xunit/blob/v2-2.9.3/StringAsserts.cs#L841)<br>*static method* | 36 | Verifies that two strings are equivalent. |
| 20 | [`IReadOnlyList<T>`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1?view=net-10.0)<br>*interface* | 34 | Represents a read-only collection of elements that can be accessed by index. |

### Namespaces at a glance

Each namespace in this table has its own table further down. The count in the last column adds up the uses of all its types and members.

| Area | Namespace | Types | Members | Uses |
| --- | --- | ---: | ---: | ---: |
| The .NET base class library | [`System`](#system) | 34 | 71 | 1,311 |
| The .NET base class library | [`System.Collections.Concurrent`](#systemcollectionsconcurrent) | 1 | 3 | 7 |
| The .NET base class library | [`System.Collections.Generic`](#systemcollectionsgeneric) | 10 | 19 | 121 |
| The .NET base class library | [`System.ComponentModel`](#systemcomponentmodel) | 1 | 1 | 1 |
| The .NET base class library | [`System.IO`](#systemio) | 2 | 11 | 13 |
| The .NET base class library | [`System.Linq`](#systemlinq) | 3 | 20 | 95 |
| The .NET base class library | [`System.Net`](#systemnet) | 1 | 8 | 59 |
| The .NET base class library | [`System.Net.Http`](#systemnethttp) | 10 | 23 | 132 |
| The .NET base class library | [`System.Net.Http.Headers`](#systemnethttpheaders) | 5 | 6 | 13 |
| The .NET base class library | [`System.Net.Http.Json`](#systemnethttpjson) | 3 | 3 | 20 |
| The .NET base class library | [`System.Security.Cryptography`](#systemsecuritycryptography) | 1 | 1 | 6 |
| The .NET base class library | [`System.Text`](#systemtext) | 1 | 2 | 14 |
| The .NET base class library | [`System.Text.Json`](#systemtextjson) | 3 | 2 | 4 |
| The .NET base class library | [`System.Text.Json.Serialization`](#systemtextjsonserialization) | 1 | 1 | 2 |
| The .NET base class library | [`System.Threading`](#systemthreading) | 6 | 15 | 110 |
| The .NET base class library | [`System.Threading.Channels`](#systemthreadingchannels) | 7 | 8 | 11 |
| The .NET base class library | [`System.Threading.Tasks`](#systemthreadingtasks) | 6 | 12 | 338 |
| ASP.NET Core | [`Microsoft.AspNetCore.Builder`](#microsoftaspnetcorebuilder) | 9 | 15 | 32 |
| ASP.NET Core | [`Microsoft.AspNetCore.Cors.Infrastructure`](#microsoftaspnetcorecorsinfrastructure) | 2 | 4 | 4 |
| ASP.NET Core | [`Microsoft.AspNetCore.Hosting`](#microsoftaspnetcorehosting) | 2 | 3 | 4 |
| ASP.NET Core | [`Microsoft.AspNetCore.Http`](#microsoftaspnetcorehttp) | 10 | 25 | 132 |
| ASP.NET Core | [`Microsoft.AspNetCore.Mvc`](#microsoftaspnetcoremvc) | 12 | 17 | 118 |
| ASP.NET Core | [`Microsoft.AspNetCore.Mvc.Testing`](#microsoftaspnetcoremvctesting) | 1 | 4 | 13 |
| Microsoft.Extensions | [`Microsoft.Extensions.Configuration`](#microsoftextensionsconfiguration) | 4 | 5 | 6 |
| Microsoft.Extensions | [`Microsoft.Extensions.DependencyInjection`](#microsoftextensionsdependencyinjection) | 17 | 22 | 69 |
| Microsoft.Extensions | [`Microsoft.Extensions.DependencyInjection.Extensions`](#microsoftextensionsdependencyinjectionextensions) | 1 | 1 | 4 |
| Microsoft.Extensions | [`Microsoft.Extensions.Hosting`](#microsoftextensionshosting) | 2 | 2 | 3 |
| Microsoft.Extensions | [`Microsoft.Extensions.Logging`](#microsoftextensionslogging) | 2 | 1 | 4 |
| Microsoft.Extensions | [`Microsoft.Extensions.Options`](#microsoftextensionsoptions) | 3 | 3 | 10 |
| Microsoft.Extensions | [`Microsoft.Extensions.Primitives`](#microsoftextensionsprimitives) | 1 | 2 | 3 |
| Entity Framework Core and SQLite | [`Microsoft.Data.Sqlite`](#microsoftdatasqlite) | 1 | 2 | 4 |
| Entity Framework Core and SQLite | [`Microsoft.EntityFrameworkCore`](#microsoftentityframeworkcore) | 12 | 23 | 155 |
| Entity Framework Core and SQLite | [`Microsoft.EntityFrameworkCore.ChangeTracking`](#microsoftentityframeworkcorechangetracking) | 1 | 1 | 1 |
| Entity Framework Core and SQLite | [`Microsoft.EntityFrameworkCore.Infrastructure`](#microsoftentityframeworkcoreinfrastructure) | 2 | 3 | 5 |
| Entity Framework Core and SQLite | [`Microsoft.EntityFrameworkCore.Metadata.Builders`](#microsoftentityframeworkcoremetadatabuilders) | 7 | 19 | 215 |
| Entity Framework Core and SQLite | [`Microsoft.EntityFrameworkCore.Storage`](#microsoftentityframeworkcorestorage) | 1 | 2 | 3 |
| xUnit test framework | [`Xunit`](#xunit) | 4 | 19 | 528 |

### The .NET base class library

*17 namespaces, 301 APIs.*

#### System

*105 APIs · 1,311 uses · [namespace page on Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/api/system?view=net-10.0)*

| API | What it does | Uses | Where | Source |
| --- | --- | ---: | --- | --- |
| **[`ArgumentNullException`](https://learn.microsoft.com/en-us/dotnet/api/system.argumentnullexception?view=net-10.0)**<br>*class* | The exception that is thrown when a null reference (Nothing in Visual Basic) is passed to a method that does not accept it as a valid argument. | 4 | [Domain/PayrollCalculator.cs:33](src/Northstar.Domain/PayrollRuns/PayrollCalculator.cs#L33)<br>[Domain/PayrollRun.cs:51](src/Northstar.Domain/PayrollRuns/PayrollRun.cs#L51)<br>+2 more | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/ArgumentNullException.cs,c742289497493bb8) |
| [`ThrowIfNull(object, string)`](https://learn.microsoft.com/en-us/dotnet/api/system.argumentnullexception.throwifnull?view=net-10.0#system-argumentnullexception-throwifnull(system-object-system-string))<br>*static method* | Throws an ArgumentNullException if argument is null. | 4 | [Domain/PayrollCalculator.cs:33](src/Northstar.Domain/PayrollRuns/PayrollCalculator.cs#L33)<br>[Domain/PayrollRun.cs:51](src/Northstar.Domain/PayrollRuns/PayrollRun.cs#L51)<br>+2 more | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/ArgumentNullException.cs,432088dc6d98b845) |
| **[`ArgumentOutOfRangeException`](https://learn.microsoft.com/en-us/dotnet/api/system.argumentoutofrangeexception?view=net-10.0)**<br>*class* | The exception that is thrown when the value of an argument is outside the allowable range of values as defined by the invoked method. | 4 | [Domain/ImportBatch.cs:47](src/Northstar.Domain/Imports/ImportBatch.cs#L47)<br>[Domain/PayrollCalculator.cs:152](src/Northstar.Domain/PayrollRuns/PayrollCalculator.cs#L152)<br>+2 more | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/ArgumentOutOfRangeException.cs,2975f0aa875d4a32) |
| [`new ArgumentOutOfRangeException(string)`](https://learn.microsoft.com/en-us/dotnet/api/system.argumentoutofrangeexception.-ctor?view=net-10.0#system-argumentoutofrangeexception-ctor(system-string))<br>*constructor* | Initializes a new instance of the ArgumentOutOfRangeException class with the name of the parameter that causes this exception. | 1 | [Domain/ImportBatch.cs:47](src/Northstar.Domain/Imports/ImportBatch.cs#L47) | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/ArgumentOutOfRangeException.cs,5fec5e06371b1f6c) |
| [`new ArgumentOutOfRangeException(string, string)`](https://learn.microsoft.com/en-us/dotnet/api/system.argumentoutofrangeexception.-ctor?view=net-10.0#system-argumentoutofrangeexception-ctor(system-string-system-string))<br>*constructor* | Initializes a new instance of the ArgumentOutOfRangeException class with the name of the parameter that causes this exception and a specified error message. | 2 | [Domain/ImportBatch.cs:77](src/Northstar.Domain/Imports/ImportBatch.cs#L77)<br>[Domain/PayrollCalculator.cs:152](src/Northstar.Domain/PayrollRuns/PayrollCalculator.cs#L152) | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/ArgumentOutOfRangeException.cs,27138d3b17d5ff6b) |
| **[`Array`](https://learn.microsoft.com/en-us/dotnet/api/system.array?view=net-10.0)**<br>*abstract class* | Provides methods for creating, manipulating, searching, and sorting arrays, thereby serving as the base class for all arrays in the common language runtime. | – | through its members | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Array.cs,156e066ecc4ccedf) |
| [`Length`](https://learn.microsoft.com/en-us/dotnet/api/system.array.length?view=net-10.0#system-array-length)<br>*property* | Gets the total number of elements in all the dimensions of the Array. | 4 | [Domain/WageRateSelector.cs:21](src/Northstar.Domain/WageRates/WageRateSelector.cs#L21)<br>[IntegrationTests/FakePayrollProviderClientTests.cs:277](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L277)<br>+2 more | [source](https://source.dot.net/#System.Private.CoreLib/System/Array.NativeAot.cs,42e9b7616956cf94) |
| **[`bool`](https://learn.microsoft.com/en-us/dotnet/api/system.boolean?view=net-10.0)**<br>*struct, C# keyword for `System.Boolean`* | Represents a Boolean (true or false) value. | 22 | [Api/ApiControllerBase.cs:10](src/Northstar.Api/Controllers/ApiControllerBase.cs#L10)<br>[Api/HeaderUserContext.cs:5](src/Northstar.Api/Security/HeaderUserContext.cs#L5)<br>+20 more | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Boolean.cs,f1b135ff6c380b37) |
| **[`byte`](https://learn.microsoft.com/en-us/dotnet/api/system.byte?view=net-10.0)**<br>*struct, C# keyword for `System.Byte`* | Represents an 8-bit unsigned integer. | 4 | [IntegrationTests/FakePayrollProviderClientTests.cs:256](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L256)<br>[IntegrationTests/FakePayrollProviderClientTests.cs:270](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L270)<br>+2 more | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Byte.cs,3933aebb80415ce0) |
| **[`Convert`](https://learn.microsoft.com/en-us/dotnet/api/system.convert?view=net-10.0)**<br>*static class* | Converts a base data type to another base data type. | 3 | [Application/ImportBatchService.cs:382](src/Northstar.Application/Imports/ImportBatchService.cs#L382)<br>[Application/PayrollRunService.cs:276](src/Northstar.Application/PayrollRuns/PayrollRunService.cs#L276)<br>+1 more | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Convert.cs,fc990bd1275d43d6) |
| [`ToHexString(byte[])`](https://learn.microsoft.com/en-us/dotnet/api/system.convert.tohexstring?view=net-10.0#system-convert-tohexstring(system-byte()))<br>*static method* | Converts an array of 8-bit unsigned integers to its equivalent string representation that is encoded with uppercase hex characters. | 3 | [Application/ImportBatchService.cs:382](src/Northstar.Application/Imports/ImportBatchService.cs#L382)<br>[Application/PayrollRunService.cs:276](src/Northstar.Application/PayrollRuns/PayrollRunService.cs#L276)<br>+1 more | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Convert.cs,7f5692bdedb06128) |
| **[`DateOnly`](https://learn.microsoft.com/en-us/dotnet/api/system.dateonly?view=net-10.0)**<br>*struct* | Represents dates with values ranging from January 1, 0001 Anno Domini (Common Era) through December 31, 9999 A.D. (C.E.) in the Gregorian calendar. | 99 | [Application/IPayrollProviderClient.cs:5](src/Northstar.Application/Abstractions/IPayrollProviderClient.cs#L5)<br>[Application/ImportBatchView.cs:4](src/Northstar.Application/Imports/ImportBatchView.cs#L4)<br>+97 more | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/DateOnly.cs,6495e51fffd21417) |
| [`new DateOnly(int, int, int)`](https://learn.microsoft.com/en-us/dotnet/api/system.dateonly.-ctor?view=net-10.0#system-dateonly-ctor(system-int32-system-int32-system-int32))<br>*constructor* | Creates a new instance of the DateOnly structure to the specified year, month, and day. | 40 | [Infrastructure/DatabaseInitializer.cs:13](src/Northstar.Infrastructure/Persistence/DatabaseInitializer.cs#L13)<br>[IntegrationTests/FakePayrollProviderClientTests.cs:180](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L180)<br>+38 more | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/DateOnly.cs,c07c16eb2f6d61b8) |
| [`DayNumber`](https://learn.microsoft.com/en-us/dotnet/api/system.dateonly.daynumber?view=net-10.0#system-dateonly-daynumber)<br>*property* | Gets the number of days since January 1, 0001 in the Proleptic Gregorian calendar represented by this instance. | 2 | [Application/ImportBatchService.cs:293](src/Northstar.Application/Imports/ImportBatchService.cs#L293)<br>+1 more | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/DateOnly.cs,96c7026eb551ab11) |
| [`MaxValue`](https://learn.microsoft.com/en-us/dotnet/api/system.dateonly.maxvalue?view=net-10.0#system-dateonly-maxvalue)<br>*static property* | Gets the latest possible date that can be created. | 1 | [Application/WageRateService.cs:79](src/Northstar.Application/WageRates/WageRateService.cs#L79) | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/DateOnly.cs,e70213b870699686) |
| [`AddDays(int)`](https://learn.microsoft.com/en-us/dotnet/api/system.dateonly.adddays?view=net-10.0#system-dateonly-adddays(system-int32))<br>*method* | Adds the specified number of days to the value of this instance. | 6 | [IntegrationTests/DeterministicPayrollProviderClient.cs:116](tests/Northstar.Api.IntegrationTests/DeterministicPayrollProviderClient.cs#L116)<br>[IntegrationTests/ImportBatchesApiTests.cs:136](tests/Northstar.Api.IntegrationTests/ImportBatchesApiTests.cs#L136)<br>+4 more | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/DateOnly.cs,49b8b2e29cdd7a84) |
| [`ToString(string)`](https://learn.microsoft.com/en-us/dotnet/api/system.dateonly.tostring?view=net-10.0#system-dateonly-tostring(system-string))<br>*method* | Converts the value of the current DateOnly object to its equivalent string representation using the specified format and the formatting conventions of the current… | 2 | [Infrastructure/FakePayrollProviderClient.cs:159](src/Northstar.Infrastructure/Integrations/FakePayrollProviderClient.cs#L159)<br>[Infrastructure/FakePayrollProviderClient.cs:160](src/Northstar.Infrastructure/Integrations/FakePayrollProviderClient.cs#L160) | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/DateOnly.cs,8b94ef5ec09ba83b) |
| [`operator <(DateOnly, DateOnly)`](https://learn.microsoft.com/en-us/dotnet/api/system.dateonly.op_lessthan?view=net-10.0#system-dateonly-op-lessthan(system-dateonly-system-dateonly))<br>*operator* | Determines whether one specified DateOnly is earlier than another specified DateOnly. | 10 | [Application/ImportBatchProcessor.cs:268](src/Northstar.Application/Imports/ImportBatchProcessor.cs#L268)<br>[Application/ImportBatchService.cs:292](src/Northstar.Application/Imports/ImportBatchService.cs#L292)<br>+8 more | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/DateOnly.cs,53b00991fc80a7c1) |
| [`operator <=(DateOnly, DateOnly)`](https://learn.microsoft.com/en-us/dotnet/api/system.dateonly.op_lessthanorequal?view=net-10.0#system-dateonly-op-lessthanorequal(system-dateonly-system-dateonly))<br>*operator* | Determines whether one specified DateOnly represents a date that is the same as or earlier than another specified DateOnly. | 4 | [Application/PayrollRunService.cs:114](src/Northstar.Application/PayrollRuns/PayrollRunService.cs#L114)<br>[Application/WageRateService.cs:79](src/Northstar.Application/WageRates/WageRateService.cs#L79)<br>+2 more | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/DateOnly.cs,5203685314f9e414) |
| [`operator ==(DateOnly, DateOnly)`](https://learn.microsoft.com/en-us/dotnet/api/system.dateonly.op_equality?view=net-10.0#system-dateonly-op-equality(system-dateonly-system-dateonly))<br>*operator* | Determines whether two specified instances of DateOnly are equal. | 2 | [Application/PayrollRunService.cs:139](src/Northstar.Application/PayrollRuns/PayrollRunService.cs#L139)<br>[Application/WageRateService.cs:80](src/Northstar.Application/WageRates/WageRateService.cs#L80) | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/DateOnly.cs,99b99b8341cb022f) |
| [`operator >(DateOnly, DateOnly)`](https://learn.microsoft.com/en-us/dotnet/api/system.dateonly.op_greaterthan?view=net-10.0#system-dateonly-op-greaterthan(system-dateonly-system-dateonly))<br>*operator* | Determines whether one specified DateOnly is later than another specified DateOnly. | 3 | [Application/ImportBatchProcessor.cs:268](src/Northstar.Application/Imports/ImportBatchProcessor.cs#L268)<br>[Application/ImportBatchProcessor.cs:286](src/Northstar.Application/Imports/ImportBatchProcessor.cs#L286)<br>+1 more | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/DateOnly.cs,a072de2512a8a554) |
| [`operator >=(DateOnly, DateOnly)`](https://learn.microsoft.com/en-us/dotnet/api/system.dateonly.op_greaterthanorequal?view=net-10.0#system-dateonly-op-greaterthanorequal(system-dateonly-system-dateonly))<br>*operator* | Determines whether one specified DateOnly represents a date that is the same as or later than another specified DateOnly. | 4 | [Application/PayrollRunService.cs:113](src/Northstar.Application/PayrollRuns/PayrollRunService.cs#L113)<br>[Application/WageRateService.cs:80](src/Northstar.Application/WageRates/WageRateService.cs#L80)<br>+2 more | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/DateOnly.cs,7925cd48ec4ee5a8) |
| **[`DateTimeOffset`](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset?view=net-10.0)**<br>*struct* | Represents a point in time, typically expressed as a date and time of day, relative to Coordinated Universal Time (UTC). | 25 | [Application/IClock.cs:5](src/Northstar.Application/Abstractions/IClock.cs#L5)<br>[Application/IdempotencyRecord.cs:11](src/Northstar.Application/Idempotency/IdempotencyRecord.cs#L11)<br>+23 more | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/DateTimeOffset.cs,68b4bb83ce8d1c31) |
| [`new DateTimeOffset(int, int, int, int, int, int, TimeSpan)`](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.-ctor?view=net-10.0#system-datetimeoffset-ctor(system-int32-system-int32-system-int32-system-int32-system-int32-system-int32-system-timespan))<br>*constructor* | Initializes a new instance of the DateTimeOffset structure using the specified year, month, day, hour, minute, second, and offset. | 2 | [Infrastructure/DatabaseInitializer.cs:26](src/Northstar.Infrastructure/Persistence/DatabaseInitializer.cs#L26)<br>[UnitTests/ImportBatchStateTests.cs:7](tests/Northstar.Domain.UnitTests/ImportBatchStateTests.cs#L7) | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/DateTimeOffset.cs,c5a5d2a27921e551) |
| [`UtcNow`](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.utcnow?view=net-10.0#system-datetimeoffset-utcnow)<br>*static property* | Gets a DateTimeOffset object whose date and time are set to the current Coordinated Universal Time (UTC) date and time and whose offset is TimeSpan.Zero. | 6 | [Application/IClock.cs:10](src/Northstar.Application/Abstractions/IClock.cs#L10)<br>[Infrastructure/ImportBatchWorker.cs:66](src/Northstar.Infrastructure/Messaging/ImportBatchWorker.cs#L66)<br>+4 more | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/DateTimeOffset.cs,04abe0cac293ebd0) |
| [`Add(TimeSpan)`](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.add?view=net-10.0#system-datetimeoffset-add(system-timespan))<br>*method* | Returns a new DateTimeOffset object that adds a specified time interval to the value of this instance. | 3 | [Domain/ImportBatch.cs:52](src/Northstar.Domain/Imports/ImportBatch.cs#L52)<br>[UnitTests/ImportBatchStateTests.cs:20](tests/Northstar.Domain.UnitTests/ImportBatchStateTests.cs#L20)<br>+1 more | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/DateTimeOffset.cs,5cff8f26f5c7c095) |
| [`AddMinutes(double)`](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.addminutes?view=net-10.0#system-datetimeoffset-addminutes(system-double))<br>*method* | Returns a new DateTimeOffset object that adds a specified number of whole and fractional minutes to the value of this instance. | 6 | [UnitTests/ImportBatchStateTests.cs:32](tests/Northstar.Domain.UnitTests/ImportBatchStateTests.cs#L32)<br>[UnitTests/ImportBatchStateTests.cs:46](tests/Northstar.Domain.UnitTests/ImportBatchStateTests.cs#L46)<br>+4 more | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/DateTimeOffset.cs,4658bd43b97f915c) |
| [`operator <=(DateTimeOffset, DateTimeOffset)`](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.op_lessthanorequal?view=net-10.0#system-datetimeoffset-op-lessthanorequal(system-datetimeoffset-system-datetimeoffset))<br>*operator* | Determines whether one specified DateTimeOffset object is less than a second specified DateTimeOffset object. | 2 | [Domain/ImportBatch.cs:39](src/Northstar.Domain/Imports/ImportBatch.cs#L39)<br>[Infrastructure/ImportBatchWorker.cs:81](src/Northstar.Infrastructure/Messaging/ImportBatchWorker.cs#L81) | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/DateTimeOffset.cs,3de8075fd838bfde) |
| **[`decimal`](https://learn.microsoft.com/en-us/dotnet/api/system.decimal?view=net-10.0)**<br>*struct, C# keyword for `System.Decimal`* | Represents a decimal floating-point number. | 47 | [Application/IPayrollProviderClient.cs:15](src/Northstar.Application/Abstractions/IPayrollProviderClient.cs#L15)<br>[Application/PayrollRunView.cs:10](src/Northstar.Application/PayrollRuns/PayrollRunView.cs#L10)<br>+45 more | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Decimal.cs,1b2858baf311cbf9) |
| [`Round(decimal, int, MidpointRounding)`](https://learn.microsoft.com/en-us/dotnet/api/system.decimal.round?view=net-10.0#system-decimal-round(system-decimal-system-int32-system-midpointrounding))<br>*static method* | Rounds a decimal value to the specified precision using the specified rounding strategy. | 1 | [Domain/PayrollCalculator.cs:156](src/Northstar.Domain/PayrollRuns/PayrollCalculator.cs#L156) | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Decimal.cs,f0e70b863c5ace79) |
| **[`Enum`](https://learn.microsoft.com/en-us/dotnet/api/system.enum?view=net-10.0)**<br>*abstract class* | Provides the base class for enumerations. | – | through its members | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Enum.cs,36729210e317a805) |
| [`ToString()`](https://learn.microsoft.com/en-us/dotnet/api/system.enum.tostring?view=net-10.0#system-enum-tostring)<br>*method* | Converts the value of this instance to its equivalent string representation. | 7 | [Application/ImportBatchMappings.cs:14](src/Northstar.Application/Imports/ImportBatchMappings.cs#L14)<br>[Application/ImportBatchService.cs:39](src/Northstar.Application/Imports/ImportBatchService.cs#L39)<br>+5 more | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Enum.cs,1365cfeffd45409d) |
| **[`Exception`](https://learn.microsoft.com/en-us/dotnet/api/system.exception?view=net-10.0)**<br>*class, also a base type here* | Represents errors that occur during application execution. | 8 | [Application/IPayrollProviderClient.cs:30](src/Northstar.Application/Abstractions/IPayrollProviderClient.cs#L30)<br>[Infrastructure/FakePayrollProviderClient.cs:75](src/Northstar.Infrastructure/Integrations/FakePayrollProviderClient.cs#L75)<br>+6 more | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Exception.cs,f092fb2b893a0162) |
| [`Message`](https://learn.microsoft.com/en-us/dotnet/api/system.exception.message?view=net-10.0#system-exception-message)<br>*property* | Gets a message that describes the current exception. | 7 | [Application/ImportBatchProcessor.cs:67](src/Northstar.Application/Imports/ImportBatchProcessor.cs#L67)<br>[IntegrationTests/FakePayrollProviderClientTests.cs:42](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L42)<br>+5 more | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Exception.cs,5fba0e16215b42a3) |
| **[`Func<T1, T2, T3, TResult>`](https://learn.microsoft.com/en-us/dotnet/api/system.func-4?view=net-10.0)**<br>*delegate* | Encapsulates a method that has three parameters and returns a value of the type specified by the TResult parameter. | 2 | [IntegrationTests/FakePayrollProviderClientTests.cs:237](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L237)<br>[IntegrationTests/FakePayrollProviderClientTests.cs:240](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L240) | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Function.cs,d6a8fafda0383fc5) |
| **[`Guid`](https://learn.microsoft.com/en-us/dotnet/api/system.guid?view=net-10.0)**<br>*struct* | Represents a globally unique identifier (GUID). | 201 | [Api/ApiControllerBase.cs:10](src/Northstar.Api/Controllers/ApiControllerBase.cs#L10)<br>[Api/ImportBatchesController.cs:30](src/Northstar.Api/Controllers/ImportBatchesController.cs#L30)<br>+199 more | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Guid.cs,b622ef5f6b76c10a) |
| [`NewGuid()`](https://learn.microsoft.com/en-us/dotnet/api/system.guid.newguid?view=net-10.0#system-guid-newguid)<br>*static method* | Initializes a new instance of the Guid structure. | 30 | [Application/ImportBatchProcessor.cs:150](src/Northstar.Application/Imports/ImportBatchProcessor.cs#L150)<br>[Application/ImportBatchService.cs:148](src/Northstar.Application/Imports/ImportBatchService.cs#L148)<br>+28 more | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Guid.Unix.cs,b7fae6994c22c22e) |
| [`Parse(string)`](https://learn.microsoft.com/en-us/dotnet/api/system.guid.parse?view=net-10.0#system-guid-parse(system-string))<br>*static method* | Converts the string representation of a GUID to the equivalent Guid structure. | 20 | [Infrastructure/DatabaseInitializer.cs:139](src/Northstar.Infrastructure/Persistence/DatabaseInitializer.cs#L139)<br>[Infrastructure/DemoDataIds.cs:5](src/Northstar.Infrastructure/Persistence/DemoDataIds.cs#L5)<br>+18 more | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Guid.cs,c0f15e43675529c4) |
| [`ToString()`](https://learn.microsoft.com/en-us/dotnet/api/system.guid.tostring?view=net-10.0#system-guid-tostring)<br>*method* | Returns a string representation of the value of this instance in registry format. | 3 | [IntegrationTests/ImportBatchesApiTests.cs:26](tests/Northstar.Api.IntegrationTests/ImportBatchesApiTests.cs#L26)<br>[IntegrationTests/PayrollRunsApiTests.cs:127](tests/Northstar.Api.IntegrationTests/PayrollRunsApiTests.cs#L127)<br>+1 more | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Guid.cs,a6547a472def7796) |
| [`TryParse(string, out Guid)`](https://learn.microsoft.com/en-us/dotnet/api/system.guid.tryparse?view=net-10.0#system-guid-tryparse(system-string-system-guid@))<br>*static method* | Converts the string representation of a GUID to the equivalent Guid structure. | 1 | [Api/HeaderUserContext.cs:8](src/Northstar.Api/Security/HeaderUserContext.cs#L8) | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Guid.cs,261ce0dae4819cd7) |
| [`operator !=(Guid, Guid)`](https://learn.microsoft.com/en-us/dotnet/api/system.guid.op_inequality?view=net-10.0#system-guid-op-inequality(system-guid-system-guid))<br>*operator* | Indicates whether the values of two specified Guid objects are not equal. | 2 | [Domain/WageRateSelector.cs:42](src/Northstar.Domain/WageRates/WageRateSelector.cs#L42)<br>[Domain/WageRateSelector.cs:43](src/Northstar.Domain/WageRates/WageRateSelector.cs#L43) | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Guid.cs,d7604412d0a67798) |
| [`operator ==(Guid, Guid)`](https://learn.microsoft.com/en-us/dotnet/api/system.guid.op_equality?view=net-10.0#system-guid-op-equality(system-guid-system-guid))<br>*operator* | Indicates whether the values of two specified Guid objects are equal. | 49 | [Application/ImportBatchProcessor.cs:21](src/Northstar.Application/Imports/ImportBatchProcessor.cs#L21)<br>[Application/ImportBatchService.cs:32](src/Northstar.Application/Imports/ImportBatchService.cs#L32)<br>+47 more | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Guid.cs,06f1e5df40f030ae) |
| **[`IAsyncDisposable`](https://learn.microsoft.com/en-us/dotnet/api/system.iasyncdisposable?view=net-10.0)**<br>*interface, used as a base type* | Provides a mechanism for releasing unmanaged resources asynchronously. | 1 | [Application/IApplicationDbContext.cs:12](src/Northstar.Application/Abstractions/IApplicationDbContext.cs#L12) | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/IAsyncDisposable.cs,4f4bd6a091aeee8b) |
| [`DisposeAsync()`](https://learn.microsoft.com/en-us/dotnet/api/system.iasyncdisposable.disposeasync?view=net-10.0#system-iasyncdisposable-disposeasync)<br>*method, also implemented here* | Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources asynchronously. | 2 | [Infrastructure/ApplicationDbContext.cs:263](src/Northstar.Infrastructure/Persistence/ApplicationDbContext.cs#L263)<br>+1 more | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/IAsyncDisposable.cs,ac0893cb18f74df0) |
| **[`Index`](https://learn.microsoft.com/en-us/dotnet/api/system.index?view=net-10.0)**<br>*struct* | Represents a type that can be used to index a collection either from the beginning or the end. | – | through its members | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Index.cs,019bce07c00229cb) |
| [`implicit operator Index(int)`](https://learn.microsoft.com/en-us/dotnet/api/system.index.op_implicit?view=net-10.0#system-index-op-implicit(system-int32)-system-index)<br>*conversion* | Converts an integer number to an Index. | 2 | [Application/ImportBatchProcessor.cs:323](src/Northstar.Application/Imports/ImportBatchProcessor.cs#L323)<br>[FakeProvider/Program.cs:288](tools/Northstar.FakePayrollProvider/Program.cs#L288) | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Index.cs,fdd5617c98dd2403) |
| **[`int`](https://learn.microsoft.com/en-us/dotnet/api/system.int32?view=net-10.0)**<br>*struct, C# keyword for `System.Int32`* | Represents a 32-bit signed integer. | 59 | [Api/ApiControllerBase.cs:42](src/Northstar.Api/Controllers/ApiControllerBase.cs#L42)<br>[Application/IApplicationDbContext.cs:35](src/Northstar.Application/Abstractions/IApplicationDbContext.cs#L35)<br>+57 more | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Int32.cs,225942ed7b7a3252) |
| **[`InvalidOperationException`](https://learn.microsoft.com/en-us/dotnet/api/system.invalidoperationexception?view=net-10.0)**<br>*class* | The exception that is thrown when a method call is invalid for the object's current state. | 6 | [Api/ApiControllerBase.cs:31](src/Northstar.Api/Controllers/ApiControllerBase.cs#L31)<br>[Domain/ImportBatch.cs:71](src/Northstar.Domain/Imports/ImportBatch.cs#L71)<br>+4 more | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/InvalidOperationException.cs,beac22dfca574cd4) |
| [`new InvalidOperationException(string)`](https://learn.microsoft.com/en-us/dotnet/api/system.invalidoperationexception.-ctor?view=net-10.0#system-invalidoperationexception-ctor(system-string))<br>*constructor* | Initializes a new instance of the InvalidOperationException class with a specified error message. | 6 | [Api/ApiControllerBase.cs:31](src/Northstar.Api/Controllers/ApiControllerBase.cs#L31)<br>[Domain/ImportBatch.cs:71](src/Northstar.Domain/Imports/ImportBatch.cs#L71)<br>+4 more | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/InvalidOperationException.cs,265d72f49e915224) |
| **[`long`](https://learn.microsoft.com/en-us/dotnet/api/system.int64?view=net-10.0)**<br>*struct, C# keyword for `System.Int64`* | Represents a 64-bit signed integer. | 16 | [Api/PayrollRunsController.cs:91](src/Northstar.Api/Controllers/PayrollRunsController.cs#L91)<br>[Application/ImportBatchView.cs:25](src/Northstar.Application/Imports/ImportBatchView.cs#L25)<br>+14 more | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Int64.cs,e566178cce890c36) |
| **[`Math`](https://learn.microsoft.com/en-us/dotnet/api/system.math?view=net-10.0)**<br>*static class* | Provides constants and static methods for trigonometric, logarithmic, and other common mathematical functions. | 4 | [Infrastructure/FakePayrollProviderClient.cs:176](src/Northstar.Infrastructure/Integrations/FakePayrollProviderClient.cs#L176)<br>[IntegrationTests/FakePayrollProviderClientTests.cs:279](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L279)<br>+2 more | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Math.cs,a4407e67b9a5afad) |
| [`Min(double, double)`](https://learn.microsoft.com/en-us/dotnet/api/system.math.min?view=net-10.0#system-math-min(system-double-system-double))<br>*static method* | Returns the smaller of two double-precision floating-point numbers. | 1 | [Infrastructure/FakePayrollProviderClient.cs:176](src/Northstar.Infrastructure/Integrations/FakePayrollProviderClient.cs#L176) | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Math.cs,e248b38c0156e9dd) |
| [`Min(int, int)`](https://learn.microsoft.com/en-us/dotnet/api/system.math.min?view=net-10.0#system-math-min(system-int32-system-int32))<br>*static method* | Returns the smaller of two 32-bit signed integers. | 2 | [IntegrationTests/FakePayrollProviderClientTests.cs:279](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L279)<br>[IntegrationTests/ImportBatchesApiTests.cs:394](tests/Northstar.Api.IntegrationTests/ImportBatchesApiTests.cs#L394) | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Math.cs,f441d55c88d635ad) |
| [`Pow(double, double)`](https://learn.microsoft.com/en-us/dotnet/api/system.math.pow?view=net-10.0#system-math-pow(system-double-system-double))<br>*static method* | Returns a specified number raised to the specified power. | 1 | [Infrastructure/FakePayrollProviderClient.cs:181](src/Northstar.Infrastructure/Integrations/FakePayrollProviderClient.cs#L181) | [source](https://source.dot.net/#System.Private.CoreLib/System/Math.NativeAot.cs,95354113fadc3083) |
| **[`Memory<T>`](https://learn.microsoft.com/en-us/dotnet/api/system.memory-1?view=net-10.0)**<br>*struct* | Represents a contiguous region of memory. | 1 | [IntegrationTests/FakePayrollProviderClientTests.cs:274](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L274) | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Memory.cs,9abb22fd9fdc902e) |
| [`Length`](https://learn.microsoft.com/en-us/dotnet/api/system.memory-1.length?view=net-10.0#system-memory-1-length)<br>*property* | Gets the number of items in the current instance. | 1 | [IntegrationTests/FakePayrollProviderClientTests.cs:279](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L279) | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Memory.cs,f04c5366de6f2f18) |
| [`CopyTo(Memory<T>)`](https://learn.microsoft.com/en-us/dotnet/api/system.memory-1.copyto?view=net-10.0#system-memory-1-copyto(system-memory((-0))))<br>*method* | Copies the contents of a Memory object into a destination Memory object. | 1 | [IntegrationTests/FakePayrollProviderClientTests.cs:280](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L280) | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Memory.cs,2ea2d54047565f2a) |
| **[`MemoryExtensions`](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions?view=net-10.0)**<br>*static class* | Provides extension methods for the memory-related and span-related types, such as Memory, ReadOnlyMemory, Span, and ReadOnlySpan. | – | through its members | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/MemoryExtensions.cs,a17a0e90616225db) |
| [`AsMemory<T>(this T[], int, int)`](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.asmemory?view=net-10.0#system-memoryextensions-asmemory-1(-0()-system-int32-system-int32))<br>*extension method* | Creates a new memory region over the portion of the target array beginning at a specified position with a specified length. | 1 | [IntegrationTests/FakePayrollProviderClientTests.cs:280](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L280) | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/MemoryExtensions.cs,cc710422d6d3a5e3) |
| [`Contains<T>(this ReadOnlySpan<T>, T)`](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.contains?view=net-10.0#system-memoryextensions-contains-1(system-readonlyspan((-0))-0))<br>*extension method* | Indicates whether a specified value is found in a read-only span. | 2 | [Application/ImportBatchProcessor.cs:110](src/Northstar.Application/Imports/ImportBatchProcessor.cs#L110)<br>[Application/PayrollRunService.cs:137](src/Northstar.Application/PayrollRuns/PayrollRunService.cs#L137) | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/MemoryExtensions.cs,6b8cfda2cbcefe14) |
| **[`MidpointRounding`](https://learn.microsoft.com/en-us/dotnet/api/system.midpointrounding?view=net-10.0)**<br>*enum* | Specifies the strategy that mathematical rounding methods should use to round a number. | 1 | [Domain/PayrollCalculator.cs:156](src/Northstar.Domain/PayrollRuns/PayrollCalculator.cs#L156) | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/MidpointRounding.cs,968edda91f6fd683) |
| [`AwayFromZero`](https://learn.microsoft.com/en-us/dotnet/api/system.midpointrounding?view=net-10.0)<br>*enum member* | The strategy of rounding to the nearest number, and when a number is halfway between two others, it's rounded toward the nearest number that's away from zero. | 1 | [Domain/PayrollCalculator.cs:156](src/Northstar.Domain/PayrollRuns/PayrollCalculator.cs#L156) | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/MidpointRounding.cs,6754ecd84d46a8ed) |
| **[`NotSupportedException`](https://learn.microsoft.com/en-us/dotnet/api/system.notsupportedexception?view=net-10.0)**<br>*class* | The exception that is thrown when an invoked method is not supported, or when there is an attempt to read, seek, or write to a stream that does not support the invoked… | 6 | [IntegrationTests/FakePayrollProviderClientTests.cs:262](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L262)<br>[IntegrationTests/FakePayrollProviderClientTests.cs:267](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L267)<br>+4 more | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/NotSupportedException.cs,99ee329017558ada) |
| [`new NotSupportedException()`](https://learn.microsoft.com/en-us/dotnet/api/system.notsupportedexception.-ctor?view=net-10.0#system-notsupportedexception-ctor)<br>*constructor* | Initializes a new instance of the NotSupportedException class, setting the Exception.Message property of the new instance to a system-supplied message that describes the… | 6 | [IntegrationTests/FakePayrollProviderClientTests.cs:262](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L262)<br>[IntegrationTests/FakePayrollProviderClientTests.cs:267](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L267)<br>+4 more | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/NotSupportedException.cs,ab0acd5f99886747) |
| **[`Nullable<T>`](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1?view=net-10.0)**<br>*struct* | Represents a value type that can be assigned null. | – | through its members | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Nullable.cs,ffebe438fd9cbf0e) |
| [`Value`](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1.value?view=net-10.0#system-nullable-1-value)<br>*property* | Gets the value of the current Nullable object if it has been assigned a valid underlying value. | 7 | [Application/ImportBatchProcessor.cs:156](src/Northstar.Application/Imports/ImportBatchProcessor.cs#L156)<br>[Domain/WageRate.cs:31](src/Northstar.Domain/WageRates/WageRate.cs#L31)<br>+5 more | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Nullable.cs,7b38d1fa76071c95) |
| **[`object`](https://learn.microsoft.com/en-us/dotnet/api/system.object?view=net-10.0)**<br>*class, C# keyword for `System.Object`* | Supports all classes in the .NET class hierarchy and provides low-level services to derived classes. | 1 | [FakeProvider/Program.cs:126](tools/Northstar.FakePayrollProvider/Program.cs#L126) | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Object.cs,d9262ceecc1719ab) |
| **[`OperationCanceledException`](https://learn.microsoft.com/en-us/dotnet/api/system.operationcanceledexception?view=net-10.0)**<br>*class* | The exception that is thrown in a thread upon cancellation of an operation that the thread was executing. | 6 | [Infrastructure/FakePayrollProviderClient.cs:133](src/Northstar.Infrastructure/Integrations/FakePayrollProviderClient.cs#L133)<br>[Infrastructure/ImportBatchWorker.cs:30](src/Northstar.Infrastructure/Messaging/ImportBatchWorker.cs#L30)<br>+4 more | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/OperationCanceledException.cs,a0a8b79c277cf327) |
| **[`string`](https://learn.microsoft.com/en-us/dotnet/api/system.string?view=net-10.0)**<br>*class, C# keyword for `System.String`* | Represents text as a sequence of UTF-16 code units. | 238 | [Api/ApiControllerBase.cs:42](src/Northstar.Api/Controllers/ApiControllerBase.cs#L42)<br>[Api/ImportBatchesController.cs:69](src/Northstar.Api/Controllers/ImportBatchesController.cs#L69)<br>+236 more | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/String.cs,8281103e6f23cb5c) |
| [`Length`](https://learn.microsoft.com/en-us/dotnet/api/system.string.length?view=net-10.0#system-string-length)<br>*property* | Gets the number of characters in the current String object. | 8 | [Application/ImportBatchProcessor.cs:250](src/Northstar.Application/Imports/ImportBatchProcessor.cs#L250)<br>[Application/ImportBatchService.cs:374](src/Northstar.Application/Imports/ImportBatchService.cs#L374)<br>+6 more | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/String.cs,e13f5829ef28aa07) |
| [`Equals(string, string, StringComparison)`](https://learn.microsoft.com/en-us/dotnet/api/system.string.equals?view=net-10.0#system-string-equals(system-string-system-string-system-stringcomparison))<br>*static method* | Determines whether two specified String objects have the same value. | 7 | [Api/HeaderUserContext.cs:11](src/Northstar.Api/Security/HeaderUserContext.cs#L11)<br>[Application/ImportBatchService.cs:330](src/Northstar.Application/Imports/ImportBatchService.cs#L330)<br>+5 more | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/String.Comparison.cs,d47c1f57ad1e1e6e) |
| [`IsNullOrWhiteSpace(string)`](https://learn.microsoft.com/en-us/dotnet/api/system.string.isnullorwhitespace?view=net-10.0#system-string-isnullorwhitespace(system-string))<br>*static method* | Indicates whether a specified string is null, empty, or consists only of white-space characters. | 10 | [Application/ImportBatchProcessor.cs:102](src/Northstar.Application/Imports/ImportBatchProcessor.cs#L102)<br>[Application/ImportBatchService.cs:300](src/Northstar.Application/Imports/ImportBatchService.cs#L300)<br>+8 more | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/String.cs,55e241b6143365ef) |
| [`Join(string, IEnumerable<string>)`](https://learn.microsoft.com/en-us/dotnet/api/system.string.join?view=net-10.0#system-string-join(system-string-system-collections-generic-ienumerable((system-string))))<br>*static method* | Concatenates the members of a constructed IEnumerable collection of type String, using the specified separator between each member. | 1 | [Infrastructure/FakePayrollProviderClient.cs:164](src/Northstar.Infrastructure/Integrations/FakePayrollProviderClient.cs#L164) | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/String.Manipulation.cs,fa961ce440526a4d) |
| [`StartsWith(string)`](https://learn.microsoft.com/en-us/dotnet/api/system.string.startswith?view=net-10.0#system-string-startswith(system-string))<br>*method* | Determines whether the beginning of this string instance matches the specified string. | 2 | [IntegrationTests/ImportBatchesApiTests.cs:412](tests/Northstar.Api.IntegrationTests/ImportBatchesApiTests.cs#L412)<br>[IntegrationTests/ImportBatchesApiTests.cs:434](tests/Northstar.Api.IntegrationTests/ImportBatchesApiTests.cs#L434) | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/String.Comparison.cs,23804789ea4c9c0e) |
| [`Substring(int, int)`](https://learn.microsoft.com/en-us/dotnet/api/system.string.substring?view=net-10.0#system-string-substring(system-int32-system-int32))<br>*method* | Retrieves a substring from this instance. | 2 | [Application/ImportBatchProcessor.cs:323](src/Northstar.Application/Imports/ImportBatchProcessor.cs#L323)<br>[FakeProvider/Program.cs:287](tools/Northstar.FakePayrollProvider/Program.cs#L287) | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/String.Manipulation.cs,8124961f027d9ac9) |
| [`ToLowerInvariant()`](https://learn.microsoft.com/en-us/dotnet/api/system.string.tolowerinvariant?view=net-10.0#system-string-tolowerinvariant)<br>*method* | Returns a copy of this String object converted to lowercase using the casing rules of the invariant culture. | 3 | [Application/ImportBatchService.cs:119](src/Northstar.Application/Imports/ImportBatchService.cs#L119)<br>[IntegrationTests/DeterministicPayrollProviderClient.cs:24](tests/Northstar.Api.IntegrationTests/DeterministicPayrollProviderClient.cs#L24)<br>+1 more | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/String.Manipulation.cs,2d51c212cb09178e) |
| [`ToUpperInvariant()`](https://learn.microsoft.com/en-us/dotnet/api/system.string.toupperinvariant?view=net-10.0#system-string-toupperinvariant)<br>*method* | Returns a copy of this String object converted to uppercase using the casing rules of the invariant culture. | 4 | [Application/ImportBatchProcessor.cs:327](src/Northstar.Application/Imports/ImportBatchProcessor.cs#L327)<br>[Application/WageRateService.cs:95](src/Northstar.Application/WageRates/WageRateService.cs#L95)<br>+2 more | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/String.Manipulation.cs,b1bb64f94f2814f6) |
| [`Trim()`](https://learn.microsoft.com/en-us/dotnet/api/system.string.trim?view=net-10.0#system-string-trim)<br>*method* | Removes all leading and trailing white-space characters from the current string. | 9 | [Application/ImportBatchProcessor.cs:327](src/Northstar.Application/Imports/ImportBatchProcessor.cs#L327)<br>[Application/ImportBatchService.cs:119](src/Northstar.Application/Imports/ImportBatchService.cs#L119)<br>+7 more | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/String.Manipulation.cs,06a5f7c688e69307) |
| [`Empty`](https://learn.microsoft.com/en-us/dotnet/api/system.string.empty?view=net-10.0#system-string-empty)<br>*static field* | Represents the empty string. | 29 | [Api/ImportBatchesController.cs:85](src/Northstar.Api/Controllers/ImportBatchesController.cs#L85)<br>[Api/PayrollRunsController.cs:60](src/Northstar.Api/Controllers/PayrollRunsController.cs#L60)<br>+27 more | [source](https://source.dot.net/#System.Private.CoreLib/System/String.NativeAot.cs,c9f70a27facb27cf) |
| **[`StringComparer`](https://learn.microsoft.com/en-us/dotnet/api/system.stringcomparer?view=net-10.0)**<br>*abstract class* | Represents a string comparison operation that uses specific case and culture-based or ordinal comparison rules. | 9 | [Application/ImportBatchProcessor.cs:88](src/Northstar.Application/Imports/ImportBatchProcessor.cs#L88)<br>[Application/ImportBatchService.cs:16](src/Northstar.Application/Imports/ImportBatchService.cs#L16)<br>+7 more | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/StringComparer.cs,a1c3d85bbf4ae20e) |
| [`Ordinal`](https://learn.microsoft.com/en-us/dotnet/api/system.stringcomparer.ordinal?view=net-10.0#system-stringcomparer-ordinal)<br>*static property* | Gets a StringComparer object that performs a case-sensitive ordinal string comparison. | 4 | [Application/ImportBatchProcessor.cs:105](src/Northstar.Application/Imports/ImportBatchProcessor.cs#L105)<br>[Infrastructure/FakePayrollProviderClient.cs:20](src/Northstar.Infrastructure/Integrations/FakePayrollProviderClient.cs#L20)<br>+2 more | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/StringComparer.cs,1e02b29426466539) |
| [`OrdinalIgnoreCase`](https://learn.microsoft.com/en-us/dotnet/api/system.stringcomparer.ordinalignorecase?view=net-10.0#system-stringcomparer-ordinalignorecase)<br>*static property* | Gets a StringComparer object that performs a case-insensitive ordinal string comparison. | 5 | [Application/ImportBatchProcessor.cs:88](src/Northstar.Application/Imports/ImportBatchProcessor.cs#L88)<br>[Application/ImportBatchService.cs:16](src/Northstar.Application/Imports/ImportBatchService.cs#L16)<br>+3 more | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/StringComparer.cs,59b9c33842417944) |
| **[`StringComparison`](https://learn.microsoft.com/en-us/dotnet/api/system.stringcomparison?view=net-10.0)**<br>*enum* | Specifies the culture, case, and sort rules to be used by certain overloads of the String.Compare and String.Equals methods. | 18 | [Api/HeaderUserContext.cs:14](src/Northstar.Api/Security/HeaderUserContext.cs#L14)<br>[Application/ImportBatchService.cs:330](src/Northstar.Application/Imports/ImportBatchService.cs#L330)<br>+16 more | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/StringComparison.cs,702797def97ecb7c) |
| [`Ordinal`](https://learn.microsoft.com/en-us/dotnet/api/system.stringcomparison?view=net-10.0)<br>*enum member* | Compare strings using ordinal (binary) sort rules. | 12 | [Application/ImportBatchService.cs:330](src/Northstar.Application/Imports/ImportBatchService.cs#L330)<br>[Application/PayrollRunService.cs:77](src/Northstar.Application/PayrollRuns/PayrollRunService.cs#L77)<br>+10 more | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/StringComparison.cs,87891a6d28c64c9b) |
| [`OrdinalIgnoreCase`](https://learn.microsoft.com/en-us/dotnet/api/system.stringcomparison?view=net-10.0)<br>*enum member* | Compare strings using ordinal (binary) sort rules and ignoring the case of the strings being compared. | 6 | [Api/HeaderUserContext.cs:14](src/Northstar.Api/Security/HeaderUserContext.cs#L14)<br>[Domain/WageRateSelector.cs:60](src/Northstar.Domain/WageRates/WageRateSelector.cs#L60)<br>+4 more | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/StringComparison.cs,6c3139b940557bf9) |
| **[`TimeoutException`](https://learn.microsoft.com/en-us/dotnet/api/system.timeoutexception?view=net-10.0)**<br>*class* | The exception that is thrown when the time allotted for a process or operation has expired. | 1 | [IntegrationTests/ImportBatchesApiTests.cs:398](tests/Northstar.Api.IntegrationTests/ImportBatchesApiTests.cs#L398) | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/TimeoutException.cs,43e40a96805cc4df) |
| [`new TimeoutException(string)`](https://learn.microsoft.com/en-us/dotnet/api/system.timeoutexception.-ctor?view=net-10.0#system-timeoutexception-ctor(system-string))<br>*constructor* | Initializes a new instance of the TimeoutException class with the specified error message. | 1 | [IntegrationTests/ImportBatchesApiTests.cs:398](tests/Northstar.Api.IntegrationTests/ImportBatchesApiTests.cs#L398) | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/TimeoutException.cs,eedbd59d0faf42ed) |
| **[`TimeSpan`](https://learn.microsoft.com/en-us/dotnet/api/system.timespan?view=net-10.0)**<br>*struct* | Represents a time interval. | 24 | [Application/ImportBatchProcessor.cs:14](src/Northstar.Application/Imports/ImportBatchProcessor.cs#L14)<br>[Domain/ImportBatch.cs:35](src/Northstar.Domain/Imports/ImportBatch.cs#L35)<br>+22 more | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/TimeSpan.cs,865ef7b89f41b632) |
| [`TotalMilliseconds`](https://learn.microsoft.com/en-us/dotnet/api/system.timespan.totalmilliseconds?view=net-10.0#system-timespan-totalmilliseconds)<br>*property* | Gets the value of the current TimeSpan structure expressed in whole and fractional milliseconds. | 1 | [Infrastructure/FakePayrollProviderClient.cs:176](src/Northstar.Infrastructure/Integrations/FakePayrollProviderClient.cs#L176) | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/TimeSpan.cs,0001a4a9e0a9b848) |
| [`FromMilliseconds(double)`](https://learn.microsoft.com/en-us/dotnet/api/system.timespan.frommilliseconds?view=net-10.0#system-timespan-frommilliseconds(system-double))<br>*static method* | Returns a TimeSpan that represents a specified number of milliseconds. | 2 | [Infrastructure/FakePayrollProviderClient.cs:176](src/Northstar.Infrastructure/Integrations/FakePayrollProviderClient.cs#L176)<br>[Infrastructure/FakePayrollProviderClient.cs:181](src/Northstar.Infrastructure/Integrations/FakePayrollProviderClient.cs#L181) | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/TimeSpan.cs,668d91d771bce54b) |
| [`FromMilliseconds(long)`](https://learn.microsoft.com/en-us/dotnet/api/system.timespan.frommilliseconds?view=net-10.0#system-timespan-frommilliseconds(system-int64))<br>*static method* | Initializes a new instance of the TimeSpan structure to a specified number of milliseconds. | 2 | [Infrastructure/FakePayrollProviderClient.cs:81](src/Northstar.Infrastructure/Integrations/FakePayrollProviderClient.cs#L81)<br>[IntegrationTests/ImportBatchesApiTests.cs:395](tests/Northstar.Api.IntegrationTests/ImportBatchesApiTests.cs#L395) | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/TimeSpan.cs,760e498eead7718d) |
| [`FromMinutes(long)`](https://learn.microsoft.com/en-us/dotnet/api/system.timespan.fromminutes?view=net-10.0#system-timespan-fromminutes(system-int64))<br>*static method* | Initializes a new instance of the TimeSpan structure to a specified number of minutes. | 9 | [Application/ImportBatchProcessor.cs:14](src/Northstar.Application/Imports/ImportBatchProcessor.cs#L14)<br>[UnitTests/ImportBatchStateTests.cs:13](tests/Northstar.Domain.UnitTests/ImportBatchStateTests.cs#L13)<br>+7 more | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/TimeSpan.cs,76468848d167326b) |
| [`FromSeconds(long)`](https://learn.microsoft.com/en-us/dotnet/api/system.timespan.fromseconds?view=net-10.0#system-timespan-fromseconds(system-int64))<br>*static method* | Initializes a new instance of the TimeSpan structure to a specified number of seconds. | 3 | [Infrastructure/ImportBatchWorker.cs:59](src/Northstar.Infrastructure/Messaging/ImportBatchWorker.cs#L59)<br>[FakeProvider/Program.cs:45](tools/Northstar.FakePayrollProvider/Program.cs#L45)<br>+1 more | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/TimeSpan.cs,0dc6593eed06b636) |
| [`operator <=(TimeSpan, TimeSpan)`](https://learn.microsoft.com/en-us/dotnet/api/system.timespan.op_lessthanorequal?view=net-10.0#system-timespan-op-lessthanorequal(system-timespan-system-timespan))<br>*operator* | Indicates whether a specified TimeSpan is less than or equal to another specified TimeSpan. | 1 | [Domain/ImportBatch.cs:45](src/Northstar.Domain/Imports/ImportBatch.cs#L45) | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/TimeSpan.cs,1a817d3a830a3258) |
| [`Zero`](https://learn.microsoft.com/en-us/dotnet/api/system.timespan.zero?view=net-10.0#system-timespan-zero)<br>*static field* | Represents the zero TimeSpan value. | 3 | [Domain/ImportBatch.cs:45](src/Northstar.Domain/Imports/ImportBatch.cs#L45)<br>[Infrastructure/DatabaseInitializer.cs:26](src/Northstar.Infrastructure/Persistence/DatabaseInitializer.cs#L26)<br>+1 more | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/TimeSpan.cs,225346b4cd3a8300) |
| **[`Uri`](https://learn.microsoft.com/en-us/dotnet/api/system.uri?view=net-10.0)**<br>*class* | Provides an object representation of a uniform resource identifier (URI) and easy access to the parts of the URI. | 19 | [Infrastructure/FakePayrollProviderClient.cs:168](src/Northstar.Infrastructure/Integrations/FakePayrollProviderClient.cs#L168)<br>[Infrastructure/PayrollProviderOptions.cs:7](src/Northstar.Infrastructure/Integrations/PayrollProviderOptions.cs#L7)<br>+17 more | [source](https://source.dot.net/#System.Private.Uri/System/Uri.cs,991565dd25f47c90) |
| [`new Uri(string)`](https://learn.microsoft.com/en-us/dotnet/api/system.uri.-ctor?view=net-10.0#system-uri-ctor(system-string))<br>*constructor* | Initializes a new instance of the Uri class with the specified URI. | 2 | [Infrastructure/PayrollProviderOptions.cs:7](src/Northstar.Infrastructure/Integrations/PayrollProviderOptions.cs#L7)<br>[IntegrationTests/FakePayrollProviderClientTests.cs:11](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L11) | [source](https://source.dot.net/#System.Private.Uri/System/Uri.cs,9e1bb2fef0d1159b) |
| [`new Uri(string, UriKind)`](https://learn.microsoft.com/en-us/dotnet/api/system.uri.-ctor?view=net-10.0#system-uri-ctor(system-string-system-urikind))<br>*constructor* | Initializes a new instance of the Uri class with the specified URI. This constructor allows you to specify if the URI string is a relative URI, absolute URI, or is… | 15 | [IntegrationTests/ImportBatchesApiTests.cs:38](tests/Northstar.Api.IntegrationTests/ImportBatchesApiTests.cs#L38)<br>[IntegrationTests/PayrollRunsApiTests.cs:18](tests/Northstar.Api.IntegrationTests/PayrollRunsApiTests.cs#L18)<br>+13 more | [source](https://source.dot.net/#System.Private.Uri/System/Uri.cs,d9fc45d783ee73ff) |
| [`IsAbsoluteUri`](https://learn.microsoft.com/en-us/dotnet/api/system.uri.isabsoluteuri?view=net-10.0#system-uri-isabsoluteuri)<br>*property* | Gets a value that indicates whether the Uri instance is absolute. | 1 | [Infrastructure/DependencyInjection.cs:41](src/Northstar.Infrastructure/DependencyInjection.cs#L41) | [source](https://source.dot.net/#System.Private.Uri/System/Uri.cs,2e1f2f7671a40125) |
| [`Query`](https://learn.microsoft.com/en-us/dotnet/api/system.uri.query?view=net-10.0#system-uri-query)<br>*property* | Gets any query information included in the specified URI, including the leading '?' character if not empty. | 2 | [IntegrationTests/FakePayrollProviderClientTests.cs:68](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L68)<br>[IntegrationTests/FakePayrollProviderClientTests.cs:72](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L72) | [source](https://source.dot.net/#System.Private.Uri/System/Uri.cs,5177e12380d8e70d) |
| [`EscapeDataString(string)`](https://learn.microsoft.com/en-us/dotnet/api/system.uri.escapedatastring?view=net-10.0#system-uri-escapedatastring(system-string))<br>*static method* | Converts a string to its escaped representation. | 2 | [Infrastructure/FakePayrollProviderClient.cs:168](src/Northstar.Infrastructure/Integrations/FakePayrollProviderClient.cs#L168)<br>+1 more | [source](https://source.dot.net/#System.Private.Uri/System/UriExt.cs,39926dc8cc434ff4) |
| [`ToString()`](https://learn.microsoft.com/en-us/dotnet/api/system.uri.tostring?view=net-10.0#system-uri-tostring)<br>*method* | Gets a canonical string representation for the specified Uri instance. | 1 | [IntegrationTests/ImportBatchesApiTests.cs:26](tests/Northstar.Api.IntegrationTests/ImportBatchesApiTests.cs#L26) | [source](https://source.dot.net/#System.Private.Uri/System/Uri.cs,bf3caa11ab73e433) |
| **[`UriKind`](https://learn.microsoft.com/en-us/dotnet/api/system.urikind?view=net-10.0)**<br>*enum* | Defines the different kinds of URIs. | 15 | [IntegrationTests/ImportBatchesApiTests.cs:38](tests/Northstar.Api.IntegrationTests/ImportBatchesApiTests.cs#L38)<br>[IntegrationTests/PayrollRunsApiTests.cs:18](tests/Northstar.Api.IntegrationTests/PayrollRunsApiTests.cs#L18)<br>+13 more | [source](https://source.dot.net/#System.Private.Uri/System/UriEnumTypes.cs,ac43655101ad74be) |
| [`Relative`](https://learn.microsoft.com/en-us/dotnet/api/system.urikind?view=net-10.0)<br>*enum member* | The URI is relative. | 15 | [IntegrationTests/ImportBatchesApiTests.cs:38](tests/Northstar.Api.IntegrationTests/ImportBatchesApiTests.cs#L38)<br>[IntegrationTests/PayrollRunsApiTests.cs:18](tests/Northstar.Api.IntegrationTests/PayrollRunsApiTests.cs#L18)<br>+13 more | [source](https://source.dot.net/#System.Private.Uri/System/UriEnumTypes.cs,c6d780fc34e3f702) |
| **[`void`](https://learn.microsoft.com/en-us/dotnet/api/system.void?view=net-10.0)**<br>*struct, C# keyword for `System.Void`* | Specifies a return value type for a method that does not return a value. | 45 | [Application/IApplicationDbContext.cs:33](src/Northstar.Application/Abstractions/IApplicationDbContext.cs#L33)<br>[Application/ImportBatchProcessor.cs:329](src/Northstar.Application/Imports/ImportBatchProcessor.cs#L329)<br>+43 more | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Void.cs,0325269a62139eb0) |

#### System.Collections.Concurrent

*4 APIs · 7 uses · [namespace page on Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/api/system.collections.concurrent?view=net-10.0)*

| API | What it does | Uses | Where | Source |
| --- | --- | ---: | --- | --- |
| **[`ConcurrentDictionary<TKey, TValue>`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.concurrent.concurrentdictionary-2?view=net-10.0)**<br>*class* | Represents a thread-safe collection of key/value pairs that can be accessed by multiple threads concurrently. | 2 | [IntegrationTests/DeterministicPayrollProviderClient.cs:8](tests/Northstar.Api.IntegrationTests/DeterministicPayrollProviderClient.cs#L8)<br>[FakeProvider/Program.cs:147](tools/Northstar.FakePayrollProvider/Program.cs#L147) | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Collections/Concurrent/ConcurrentDictionary.cs,2e5ef5704344a309) |
| [`new ConcurrentDictionary<TKey, TValue>(IEqualityComparer<TKey>)`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.concurrent.concurrentdictionary-2.-ctor?view=net-10.0#system-collections-concurrent-concurrentdictionary-2-ctor(system-collections-generic-iequalitycomparer((-0))))<br>*constructor* | Initializes a new instance of the ConcurrentDictionary class that is empty, has the default concurrency level and capacity, and uses the specified IEqualityComparer. | 2 | [IntegrationTests/DeterministicPayrollProviderClient.cs:9](tests/Northstar.Api.IntegrationTests/DeterministicPayrollProviderClient.cs#L9)<br>[FakeProvider/Program.cs:147](tools/Northstar.FakePayrollProvider/Program.cs#L147) | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Collections/Concurrent/ConcurrentDictionary.cs,6dabefc533af0715) |
| [`AddOrUpdate(TKey, TValue, Func<TKey, TValue, TValue>)`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.concurrent.concurrentdictionary-2.addorupdate?view=net-10.0#system-collections-concurrent-concurrentdictionary-2-addorupdate(-0-1-system-func((-0-1-1))))<br>*method* | Adds a key/value pair to the ConcurrentDictionary if the key does not already exist, or updates a key/value pair in the ConcurrentDictionary by using the specified… | 2 | [IntegrationTests/DeterministicPayrollProviderClient.cs:25](tests/Northstar.Api.IntegrationTests/DeterministicPayrollProviderClient.cs#L25)<br>[FakeProvider/Program.cs:149](tools/Northstar.FakePayrollProvider/Program.cs#L149) | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Collections/Concurrent/ConcurrentDictionary.cs,490ee025e9652769) |
| [`TryGetValue(TKey, out TValue)`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.concurrent.concurrentdictionary-2.trygetvalue?view=net-10.0#system-collections-concurrent-concurrentdictionary-2-trygetvalue(-0-1@))<br>*method* | Attempts to get the value associated with the specified key from the ConcurrentDictionary. | 1 | [IntegrationTests/DeterministicPayrollProviderClient.cs:13](tests/Northstar.Api.IntegrationTests/DeterministicPayrollProviderClient.cs#L13) | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Collections/Concurrent/ConcurrentDictionary.cs,4d0f4ac22dbeaf08) |

#### System.Collections.Generic

*29 APIs · 121 uses · [namespace page on Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic?view=net-10.0)*

| API | What it does | Uses | Where | Source |
| --- | --- | ---: | --- | --- |
| **[`Dictionary<TKey, TValue>`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2?view=net-10.0)**<br>*class* | Represents a collection of keys and values. | 3 | [Infrastructure/FakePayrollProviderClient.cs:156](src/Northstar.Infrastructure/Integrations/FakePayrollProviderClient.cs#L156)<br>[IntegrationTests/CustomWebApplicationFactory.cs:31](tests/Northstar.Api.IntegrationTests/CustomWebApplicationFactory.cs#L31)<br>+1 more | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Collections/Generic/Dictionary.cs,d3599058f8d79be0) |
| [`new Dictionary<TKey, TValue>()`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2.-ctor?view=net-10.0#system-collections-generic-dictionary-2-ctor)<br>*constructor* | Initializes a new instance of the Dictionary class that is empty, has the default initial capacity, and uses the default equality comparer for the key type. | 3 | [Infrastructure/FakePayrollProviderClient.cs:156](src/Northstar.Infrastructure/Integrations/FakePayrollProviderClient.cs#L156)<br>[IntegrationTests/CustomWebApplicationFactory.cs:31](tests/Northstar.Api.IntegrationTests/CustomWebApplicationFactory.cs#L31)<br>+1 more | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Collections/Generic/Dictionary.cs,bf6849e410b43c4c) |
| [`this[TKey]`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2.item?view=net-10.0#system-collections-generic-dictionary-2-item(-0))<br>*indexer* | Gets or sets the value associated with the specified key. | 12 | [Application/ImportBatchProcessor.cs:153](src/Northstar.Application/Imports/ImportBatchProcessor.cs#L153)<br>[Infrastructure/FakePayrollProviderClient.cs:158](src/Northstar.Infrastructure/Integrations/FakePayrollProviderClient.cs#L158)<br>+10 more | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Collections/Generic/Dictionary.cs,49962975508e2d83) |
| **[`HashSet<T>`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1?view=net-10.0)**<br>*class* | Represents a set of values. | 3 | [Application/ImportBatchProcessor.cs:234](src/Northstar.Application/Imports/ImportBatchProcessor.cs#L234)<br>[Application/ImportBatchService.cs:16](src/Northstar.Application/Imports/ImportBatchService.cs#L16)<br>+1 more | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Collections/Generic/HashSet.cs,2d265edc718b158b) |
| [`new HashSet<T>(IEqualityComparer<T>)`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1.-ctor?view=net-10.0#system-collections-generic-hashset-1-ctor(system-collections-generic-iequalitycomparer((-0))))<br>*constructor* | Initializes a new instance of the HashSet class that is empty and uses the specified equality comparer for the set type. | 2 | [Application/ImportBatchService.cs:16](src/Northstar.Application/Imports/ImportBatchService.cs#L16)<br>[Infrastructure/FakePayrollProviderClient.cs:20](src/Northstar.Infrastructure/Integrations/FakePayrollProviderClient.cs#L20) | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Collections/Generic/HashSet.cs,4398571cacc18ed5) |
| [`Add(T)`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1.add?view=net-10.0#system-collections-generic-hashset-1-add(-0))<br>*method* | Adds the specified element to a set. | 2 | [Application/ImportBatchProcessor.cs:261](src/Northstar.Application/Imports/ImportBatchProcessor.cs#L261)<br>[Infrastructure/FakePayrollProviderClient.cs:57](src/Northstar.Infrastructure/Integrations/FakePayrollProviderClient.cs#L57) | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Collections/Generic/HashSet.cs,43a2b651926474a7) |
| [`Contains(T)`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1.contains?view=net-10.0#system-collections-generic-hashset-1-contains(-0))<br>*method* | Determines whether a HashSet object contains the specified element. | 1 | [Application/ImportBatchService.cs:301](src/Northstar.Application/Imports/ImportBatchService.cs#L301) | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Collections/Generic/HashSet.cs,06d905a0ac76e4f5) |
| **[`IAsyncEnumerable<T>`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.iasyncenumerable-1?view=net-10.0)**<br>*interface* | Exposes an enumerator that provides asynchronous iteration over values of a specified type. | 1 | [Infrastructure/ImportBatchQueue.cs:18](src/Northstar.Infrastructure/Messaging/ImportBatchQueue.cs#L18) | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Collections/Generic/IAsyncEnumerable.cs,40a1440c806710a7) |
| [`GetAsyncEnumerator(CancellationToken)`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.iasyncenumerable-1.getasyncenumerator?view=net-10.0#system-collections-generic-iasyncenumerable-1-getasyncenumerator(system-threading-cancellationtoken))<br>*method, called for you by `foreach`* | Returns an enumerator that iterates asynchronously through the collection. | 1 | [Infrastructure/ImportBatchWorker.cs:22](src/Northstar.Infrastructure/Messaging/ImportBatchWorker.cs#L22) | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Collections/Generic/IAsyncEnumerable.cs,d024799238f87a57) |
| **[`ICollection<T>`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.icollection-1?view=net-10.0)**<br>*interface* | Defines methods to manipulate generic collections. | – | through its members | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Collections/Generic/ICollection.cs,a9bf1395d3addc77) |
| [`Add(T)`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.icollection-1.add?view=net-10.0#system-collections-generic-icollection-1-add(-0))<br>*method* | Adds an item to the ICollection. | 1 | [Api/Program.cs:11](src/Northstar.Api/Program.cs#L11) | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Collections/Generic/ICollection.cs,a9319db8c62ae453) |
| **[`IEnumerable<T>`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1?view=net-10.0)**<br>*interface* | Exposes the enumerator, which supports a simple iteration over a collection of a specified type. | 3 | [Domain/PayrollCalculator.cs:30](src/Northstar.Domain/PayrollRuns/PayrollCalculator.cs#L30)<br>[Domain/RateSelectionResult.cs:21](src/Northstar.Domain/WageRates/RateSelectionResult.cs#L21)<br>+1 more | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Collections/Generic/IEnumerable.cs,3acf01620172c7f0) |
| [`GetEnumerator()`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1.getenumerator?view=net-10.0#system-collections-generic-ienumerable-1-getenumerator)<br>*method, called for you by `foreach`* | Returns an enumerator that iterates through the collection. | 5 | [Domain/PayrollCalculator.cs:39](src/Northstar.Domain/PayrollRuns/PayrollCalculator.cs#L39)<br>[Domain/PayrollRun.cs:62](src/Northstar.Domain/PayrollRuns/PayrollRun.cs#L62)<br>+3 more | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Collections/Generic/IEnumerable.cs,a242475701b655b2) |
| **[`IReadOnlyCollection<T>`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlycollection-1?view=net-10.0)**<br>*interface* | Represents a strongly-typed, read-only collection of elements. | 1 | [Domain/PayrollCalculator.cs:31](src/Northstar.Domain/PayrollRuns/PayrollCalculator.cs#L31) | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Collections/Generic/IReadOnlyCollection.cs,7103bd3a82c9f352) |
| [`Count`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlycollection-1.count?view=net-10.0#system-collections-generic-ireadonlycollection-1-count)<br>*property* | Gets the number of elements in the collection. | 5 | [Application/ImportBatchProcessor.cs:122](src/Northstar.Application/Imports/ImportBatchProcessor.cs#L122)<br>[IntegrationTests/FakePayrollProviderClientTests.cs:81](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L81)<br>+3 more | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Collections/Generic/IReadOnlyCollection.cs,6f182bb82f68780e) |
| **[`IReadOnlyDictionary<TKey, TValue>`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlydictionary-2?view=net-10.0)**<br>*interface* | Represents a generic read-only collection of key/value pairs. | 3 | [Application/ImportBatchProcessor.cs:235](src/Northstar.Application/Imports/ImportBatchProcessor.cs#L235)<br>[Application/ImportBatchProcessor.cs:236](src/Northstar.Application/Imports/ImportBatchProcessor.cs#L236)<br>+1 more | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Collections/Generic/IReadOnlyDictionary.cs,f786bcc5e7b69c7d) |
| [`ContainsKey(TKey)`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlydictionary-2.containskey?view=net-10.0#system-collections-generic-ireadonlydictionary-2-containskey(-0))<br>*method* | Determines whether the read-only dictionary contains an element that has the specified key. | 1 | [Application/ImportBatchProcessor.cs:313](src/Northstar.Application/Imports/ImportBatchProcessor.cs#L313) | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Collections/Generic/IReadOnlyDictionary.cs,a5aa59e9bd3078ba) |
| [`TryGetValue(TKey, out TValue)`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlydictionary-2.trygetvalue?view=net-10.0#system-collections-generic-ireadonlydictionary-2-trygetvalue(-0-1@))<br>*method* | Gets the value that is associated with the specified key. | 2 | [Application/ImportBatchProcessor.cs:279](src/Northstar.Application/Imports/ImportBatchProcessor.cs#L279)<br>[Application/ImportBatchProcessor.cs:293](src/Northstar.Application/Imports/ImportBatchProcessor.cs#L293) | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Collections/Generic/IReadOnlyDictionary.cs,43db2f978cfd884d) |
| **[`IReadOnlyList<T>`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1?view=net-10.0)**<br>*interface* | Represents a read-only collection of elements that can be accessed by index. | 34 | [Api/ImportBatchesController.cs:14](src/Northstar.Api/Controllers/ImportBatchesController.cs#L14)<br>[Api/PayrollRunsController.cs:13](src/Northstar.Api/Controllers/PayrollRunsController.cs#L13)<br>+32 more | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Collections/Generic/IReadOnlyList.cs,b040fb780bdd59f4) |
| [`this[int]`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1.item?view=net-10.0#system-collections-generic-ireadonlylist-1-item(system-int32))<br>*indexer* | Gets the element at the specified index in the read-only list. | 5 | [Application/ImportBatchProcessor.cs:124](src/Northstar.Application/Imports/ImportBatchProcessor.cs#L124)<br>[IntegrationTests/FakePayrollProviderClientTests.cs:82](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L82)<br>+3 more | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Collections/Generic/IReadOnlyList.cs,82c76a7ac911cb07) |
| **[`KeyValuePair<TKey, TValue>`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.keyvaluepair-2?view=net-10.0)**<br>*struct* | Defines a key/value pair that can be set or retrieved. | – | through its members | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Collections/Generic/KeyValuePair.cs,8585965bb176a426) |
| [`Key`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.keyvaluepair-2.key?view=net-10.0#system-collections-generic-keyvaluepair-2-key)<br>*property* | Gets the key in the key/value pair. | 1 | [Infrastructure/FakePayrollProviderClient.cs:168](src/Northstar.Infrastructure/Integrations/FakePayrollProviderClient.cs#L168) | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Collections/Generic/KeyValuePair.cs,f9d1c04feb1af032) |
| [`Value`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.keyvaluepair-2.value?view=net-10.0#system-collections-generic-keyvaluepair-2-value)<br>*property* | Gets the value in the key/value pair. | 2 | [Infrastructure/FakePayrollProviderClient.cs:167](src/Northstar.Infrastructure/Integrations/FakePayrollProviderClient.cs#L167)<br>[Infrastructure/FakePayrollProviderClient.cs:168](src/Northstar.Infrastructure/Integrations/FakePayrollProviderClient.cs#L168) | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Collections/Generic/KeyValuePair.cs,38c0e86cc4b30170) |
| **[`List<T>`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1?view=net-10.0)**<br>*class* | Represents a strongly typed list of objects that can be accessed by index. | 12 | [Domain/ImportBatch.cs:33](src/Northstar.Domain/Imports/ImportBatch.cs#L33)<br>[Domain/PayrollCalculator.cs:36](src/Northstar.Domain/PayrollRuns/PayrollCalculator.cs#L36)<br>+10 more | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Collections/Generic/List.cs,cf7f4095e4de7646) |
| [`new List<T>()`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1.-ctor?view=net-10.0#system-collections-generic-list-1-ctor)<br>*constructor* | Initializes a new instance of the List class that is empty and has the default initial capacity. | 3 | [Domain/PayrollCalculator.cs:36](src/Northstar.Domain/PayrollRuns/PayrollCalculator.cs#L36)<br>[Infrastructure/FakePayrollProviderClient.cs:19](src/Northstar.Infrastructure/Integrations/FakePayrollProviderClient.cs#L19)<br>+1 more | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Collections/Generic/List.cs,9e5352b2b304ceba) |
| [`Count`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1.count?view=net-10.0#system-collections-generic-list-1-count)<br>*property* | Gets the number of elements contained in the List. | 2 | [IntegrationTests/ImportBatchesApiTests.cs:186](tests/Northstar.Api.IntegrationTests/ImportBatchesApiTests.cs#L186)<br>[IntegrationTests/ImportBatchesApiTests.cs:217](tests/Northstar.Api.IntegrationTests/ImportBatchesApiTests.cs#L217) | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Collections/Generic/List.cs,78a69d857716bc68) |
| [`Add(T)`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1.add?view=net-10.0#system-collections-generic-list-1-add(-0))<br>*method* | Adds an object to the end of the List. | 9 | [Application/ImportBatchProcessor.cs:164](src/Northstar.Application/Imports/ImportBatchProcessor.cs#L164)<br>[Domain/PayrollCalculator.cs:81](src/Northstar.Domain/PayrollRuns/PayrollCalculator.cs#L81)<br>+7 more | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Collections/Generic/List.cs,9cc11588bffd57c1) |
| [`Clear()`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1.clear?view=net-10.0#system-collections-generic-list-1-clear)<br>*method* | Removes all elements from the List. | 2 | [Domain/PayrollRun.cs:58](src/Northstar.Domain/PayrollRuns/PayrollRun.cs#L58)<br>[Domain/PayrollRun.cs:59](src/Northstar.Domain/PayrollRuns/PayrollRun.cs#L59) | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Collections/Generic/List.cs,ca7bce81a50b0aeb) |
| [`GetEnumerator()`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1.getenumerator?view=net-10.0#system-collections-generic-list-1-getenumerator)<br>*method, called for you by `foreach`* | Returns an enumerator that iterates through the List. | 2 | [Application/PayrollRunService.cs:149](src/Northstar.Application/PayrollRuns/PayrollRunService.cs#L149)<br>[Application/PayrollRunService.cs:154](src/Northstar.Application/PayrollRuns/PayrollRunService.cs#L154) | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Collections/Generic/List.cs,5d3accf5b217bdbf) |

#### System.ComponentModel

*2 APIs · 1 uses · [namespace page on Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel?view=net-10.0)*

| API | What it does | Uses | Where | Source |
| --- | --- | ---: | --- | --- |
| **[`Component`](https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.component?view=net-10.0)**<br>*class* | Provides the base implementation for the IComponent interface and enables object sharing between applications. | – | through its members | [source](https://source.dot.net/#System.ComponentModel.Primitives/System/ComponentModel/Component.cs,949b988c69942ee2) |
| [`Dispose()`](https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.component.dispose?view=net-10.0#system-componentmodel-component-dispose)<br>*method* | Releases all resources used by the Component. | 1 | [IntegrationTests/CustomWebApplicationFactory.cs:60](tests/Northstar.Api.IntegrationTests/CustomWebApplicationFactory.cs#L60) | [source](https://source.dot.net/#System.ComponentModel.Primitives/System/ComponentModel/Component.cs,ec402748f4bd1220) |

#### System.IO

*13 APIs · 13 uses · [namespace page on Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/api/system.io?view=net-10.0)*

| API | What it does | Uses | Where | Source |
| --- | --- | ---: | --- | --- |
| **[`SeekOrigin`](https://learn.microsoft.com/en-us/dotnet/api/system.io.seekorigin?view=net-10.0)**<br>*enum* | Specifies the position in a stream to use for seeking. | 1 | [IntegrationTests/FakePayrollProviderClientTests.cs:289](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L289) | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/IO/SeekOrigin.cs,2cc1cf6635f00a52) |
| **[`Stream`](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream?view=net-10.0)**<br>*abstract class, used as a base type* | Provides a generic view of a sequence of bytes. | 1 | [IntegrationTests/FakePayrollProviderClientTests.cs:254](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L254) | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/IO/Stream.cs,f956b0c07e86df64) |
| [`CanRead`](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream.canread?view=net-10.0#system-io-stream-canread)<br>*property, overridden here* | When overridden in a derived class, gets a value indicating whether the current stream supports reading. | 1 | [IntegrationTests/FakePayrollProviderClientTests.cs:259](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L259) | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/IO/Stream.cs,9d92fb3890523d38) |
| [`CanSeek`](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream.canseek?view=net-10.0#system-io-stream-canseek)<br>*property, overridden here* | When overridden in a derived class, gets a value indicating whether the current stream supports seeking. | 1 | [IntegrationTests/FakePayrollProviderClientTests.cs:260](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L260) | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/IO/Stream.cs,73e4413d240b4cee) |
| [`CanWrite`](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream.canwrite?view=net-10.0#system-io-stream-canwrite)<br>*property, overridden here* | When overridden in a derived class, gets a value indicating whether the current stream supports writing. | 1 | [IntegrationTests/FakePayrollProviderClientTests.cs:261](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L261) | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/IO/Stream.cs,ebab682837a70146) |
| [`Length`](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream.length?view=net-10.0#system-io-stream-length)<br>*property, overridden here* | When overridden in a derived class, gets the length in bytes of the stream. | 1 | [IntegrationTests/FakePayrollProviderClientTests.cs:262](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L262) | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/IO/Stream.cs,48b7e39cfd844dc5) |
| [`Position`](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream.position?view=net-10.0#system-io-stream-position)<br>*property, overridden here* | When overridden in a derived class, gets or sets the position within the current stream. | 1 | [IntegrationTests/FakePayrollProviderClientTests.cs:264](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L264) | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/IO/Stream.cs,35112b2a53fb536a) |
| [`Flush()`](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream.flush?view=net-10.0#system-io-stream-flush)<br>*method, overridden here* | When overridden in a derived class, clears all buffers for this stream and causes any buffered data to be written to the underlying device. | 1 | [IntegrationTests/FakePayrollProviderClientTests.cs:297](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L297) | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/IO/Stream.cs,1bd99e8a41c68d21) |
| [`Read(byte[], int, int)`](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream.read?view=net-10.0#system-io-stream-read(system-byte()-system-int32-system-int32))<br>*method, overridden here* | When overridden in a derived class, reads a sequence of bytes from the current stream and advances the position within the stream by the number of bytes read. | 1 | [IntegrationTests/FakePayrollProviderClientTests.cs:270](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L270) | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/IO/Stream.cs,6fb9c001d7524ba2) |
| [`ReadAsync(Memory<byte>, CancellationToken)`](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream.readasync?view=net-10.0#system-io-stream-readasync(system-memory((system-byte))-system-threading-cancellationtoken))<br>*method, overridden here* | Asynchronously reads a sequence of bytes from the current stream, advances the position within the stream by the number of bytes read, and monitors cancellation requests. | 1 | [IntegrationTests/FakePayrollProviderClientTests.cs:273](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L273) | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/IO/Stream.cs,d77b42560e8d3fb2) |
| [`Seek(long, SeekOrigin)`](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream.seek?view=net-10.0#system-io-stream-seek(system-int64-system-io-seekorigin))<br>*method, overridden here* | When overridden in a derived class, sets the position within the current stream. | 1 | [IntegrationTests/FakePayrollProviderClientTests.cs:289](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L289) | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/IO/Stream.cs,eb5599035c4388ce) |
| [`SetLength(long)`](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream.setlength?view=net-10.0#system-io-stream-setlength(system-int64))<br>*method, overridden here* | When overridden in a derived class, sets the length of the current stream. | 1 | [IntegrationTests/FakePayrollProviderClientTests.cs:292](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L292) | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/IO/Stream.cs,70f6f69c124200cc) |
| [`Write(byte[], int, int)`](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream.write?view=net-10.0#system-io-stream-write(system-byte()-system-int32-system-int32))<br>*method, overridden here* | When overridden in a derived class, writes a sequence of bytes to the current stream and advances the current position within this stream by the number of bytes written. | 1 | [IntegrationTests/FakePayrollProviderClientTests.cs:294](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L294) | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/IO/Stream.cs,809202cd37ae53a9) |

#### System.Linq

*23 APIs · 95 uses · [namespace page on Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/api/system.linq?view=net-10.0)*

| API | What it does | Uses | Where | Source |
| --- | --- | ---: | --- | --- |
| **[`Enumerable`](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable?view=net-10.0)**<br>*static class* | Provides a set of static (Shared in Visual Basic) methods for querying objects that implement IEnumerable. | – | through its members | [source](https://source.dot.net/#System.Linq/System/Linq/Aggregate.cs,577032c8811e20d3) |
| [`Any<TSource>(this IEnumerable<TSource>, Func<TSource, bool>)`](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.any?view=net-10.0#system-linq-enumerable-any-1(system-collections-generic-ienumerable((-0))-system-func((-0-system-boolean))))<br>*extension method* | Determines whether any element of a sequence satisfies a condition. | 2 | [Domain/PayrollRun.cs:77](src/Northstar.Domain/PayrollRuns/PayrollRun.cs#L77)<br>[Domain/PayrollRun.cs:95](src/Northstar.Domain/PayrollRuns/PayrollRun.cs#L95) | [source](https://source.dot.net/#System.Linq/System/Linq/AnyAll.cs,6a1af7c3d17845e3) |
| [`Cast<TResult>(this IEnumerable)`](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.cast?view=net-10.0#system-linq-enumerable-cast-1(system-collections-ienumerable))<br>*extension method* | Casts the elements of an IEnumerable to the specified type. | 1 | [Application/ImportBatchProcessor.cs:103](src/Northstar.Application/Imports/ImportBatchProcessor.cs#L103) | [source](https://source.dot.net/#System.Linq/System/Linq/Cast.cs,27bb217a6d5457ec) |
| [`Distinct<TSource>(this IEnumerable<TSource>)`](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.distinct?view=net-10.0#system-linq-enumerable-distinct-1(system-collections-generic-ienumerable((-0))))<br>*extension method* | Returns distinct elements from a sequence by using the default equality comparer to compare values. | 1 | [Application/PayrollRunService.cs:133](src/Northstar.Application/PayrollRuns/PayrollRunService.cs#L133) | [source](https://source.dot.net/#System.Linq/System/Linq/Distinct.cs,0ff831f61400afdf) |
| [`Distinct<TSource>(this IEnumerable<TSource>, IEqualityComparer<TSource>)`](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.distinct?view=net-10.0#system-linq-enumerable-distinct-1(system-collections-generic-ienumerable((-0))-system-collections-generic-iequalitycomparer((-0))))<br>*extension method* | Returns distinct elements from a sequence by using a specified IEqualityComparer to compare values. | 1 | [Application/ImportBatchProcessor.cs:105](src/Northstar.Application/Imports/ImportBatchProcessor.cs#L105) | [source](https://source.dot.net/#System.Linq/System/Linq/Distinct.cs,1246b23904e29c42) |
| [`Max<TSource>(this IEnumerable<TSource>, Func<TSource, int>)`](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.max?view=net-10.0#system-linq-enumerable-max-1(system-collections-generic-ienumerable((-0))-system-func((-0-system-int32))))<br>*extension method* | Invokes a transform function on each element of a sequence and returns the maximum Int32 value. | 1 | [Domain/WageRateSelector.cs:28](src/Northstar.Domain/WageRates/WageRateSelector.cs#L28) | [source](https://source.dot.net/#System.Linq/System/Linq/Max.cs,f74d20c7edf30968) |
| [`Min<TSource, TResult>(this IEnumerable<TSource>, Func<TSource, TResult>)`](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.min?view=net-10.0#system-linq-enumerable-min-2(system-collections-generic-ienumerable((-0))-system-func((-0-1))))<br>*extension method* | Invokes a transform function on each element of a generic sequence and returns the minimum resulting value. | 1 | [Domain/WageRateSelector.cs:26](src/Northstar.Domain/WageRates/WageRateSelector.cs#L26) | [source](https://source.dot.net/#System.Linq/System/Linq/Min.cs,082a392005a7ca65) |
| [`OrderBy<TSource, TKey>(this IEnumerable<TSource>, Func<TSource, TKey>)`](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.orderby?view=net-10.0#system-linq-enumerable-orderby-2(system-collections-generic-ienumerable((-0))-system-func((-0-1))))<br>*extension method* | Sorts the elements of a sequence in ascending order according to a key. | 3 | [Application/PayrollRunService.cs:246](src/Northstar.Application/PayrollRuns/PayrollRunService.cs#L246)<br>[Domain/WageRateSelector.cs:31](src/Northstar.Domain/WageRates/WageRateSelector.cs#L31)<br>+1 more | [source](https://source.dot.net/#System.Linq/System/Linq/OrderBy.cs,e449fbc07f49dc52) |
| [`OrderByDescending<TSource, TKey>(this IEnumerable<TSource>, Func<TSource, TKey>)`](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.orderbydescending?view=net-10.0#system-linq-enumerable-orderbydescending-2(system-collections-generic-ienumerable((-0))-system-func((-0-1))))<br>*extension method* | Sorts the elements of a sequence in descending order according to a key. | 1 | [Application/ImportBatchService.cs:55](src/Northstar.Application/Imports/ImportBatchService.cs#L55) | [source](https://source.dot.net/#System.Linq/System/Linq/OrderBy.cs,7eec09816d04e761) |
| [`Select<TSource, TResult>(this IEnumerable<TSource>, Func<TSource, TResult>)`](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.select?view=net-10.0#system-linq-enumerable-select-2(system-collections-generic-ienumerable((-0))-system-func((-0-1))))<br>*extension method* | Projects each element of a sequence into a new form. | 12 | [Application/ImportBatchProcessor.cs:101](src/Northstar.Application/Imports/ImportBatchProcessor.cs#L101)<br>[Application/PayrollRunService.cs:29](src/Northstar.Application/PayrollRuns/PayrollRunService.cs#L29)<br>+10 more | [source](https://source.dot.net/#System.Linq/System/Linq/Select.cs,5c652c53e80df013) |
| [`ThenBy<TSource, TKey>(this IOrderedEnumerable<TSource>, Func<TSource, TKey>)`](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.thenby?view=net-10.0#system-linq-enumerable-thenby-2(system-linq-iorderedenumerable((-0))-system-func((-0-1))))<br>*extension method* | Performs a subsequent ordering of the elements in a sequence in ascending order according to a key. | 3 | [Application/PayrollRunService.cs:247](src/Northstar.Application/PayrollRuns/PayrollRunService.cs#L247)<br>[Domain/WageRateSelector.cs:32](src/Northstar.Domain/WageRates/WageRateSelector.cs#L32)<br>+1 more | [source](https://source.dot.net/#System.Linq/System/Linq/OrderBy.cs,5fc6f26f82dec7e7) |
| [`ToArray<TSource>(this IEnumerable<TSource>)`](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.toarray?view=net-10.0#system-linq-enumerable-toarray-1(system-collections-generic-ienumerable((-0))))<br>*extension method* | Creates an array from a IEnumerable. | 9 | [Application/ImportBatchProcessor.cs:106](src/Northstar.Application/Imports/ImportBatchProcessor.cs#L106)<br>[Application/PayrollRunService.cs:29](src/Northstar.Application/PayrollRuns/PayrollRunService.cs#L29)<br>+7 more | [source](https://source.dot.net/#System.Linq/System/Linq/ToCollection.cs,783a052330e7d48d) |
| [`ToDictionary<TSource, TKey>(this IEnumerable<TSource>, Func<TSource, TKey>, IEqualityComparer<TKey>)`](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.todictionary?view=net-10.0#system-linq-enumerable-todictionary-2(system-collections-generic-ienumerable((-0))-system-func((-0-1))-system-collections-generic-iequalitycomparer((-1))))<br>*extension method* | Creates a Dictionary from an IEnumerable according to a specified key selector function and key comparer. | 3 | [Application/ImportBatchProcessor.cs:88](src/Northstar.Application/Imports/ImportBatchProcessor.cs#L88)<br>[Application/ImportBatchProcessor.cs:93](src/Northstar.Application/Imports/ImportBatchProcessor.cs#L93)<br>+1 more | [source](https://source.dot.net/#System.Linq/System/Linq/ToCollection.cs,9d51070756f6cb7c) |
| [`ToHashSet<TSource>(this IEnumerable<TSource>, IEqualityComparer<TSource>)`](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.tohashset?view=net-10.0#system-linq-enumerable-tohashset-1(system-collections-generic-ienumerable((-0))-system-collections-generic-iequalitycomparer((-0))))<br>*extension method* | Creates a HashSet from an IEnumerable using the comparer to compare keys. | 1 | [Application/ImportBatchProcessor.cs:115](src/Northstar.Application/Imports/ImportBatchProcessor.cs#L115) | [source](https://source.dot.net/#System.Linq/System/Linq/ToCollection.cs,3b25ba121f03ed73) |
| [`ToList<TSource>(this IEnumerable<TSource>)`](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.tolist?view=net-10.0#system-linq-enumerable-tolist-1(system-collections-generic-ienumerable((-0))))<br>*extension method* | Creates a List from an IEnumerable. | 1 | [Application/ImportBatchService.cs:55](src/Northstar.Application/Imports/ImportBatchService.cs#L55) | [source](https://source.dot.net/#System.Linq/System/Linq/ToCollection.cs,e276d6892241255b) |
| [`Where<TSource>(this IEnumerable<TSource>, Func<TSource, bool>)`](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.where?view=net-10.0#system-linq-enumerable-where-1(system-collections-generic-ienumerable((-0))-system-func((-0-system-boolean))))<br>*extension method* | Filters a sequence of values based on a predicate. | 6 | [Application/ImportBatchProcessor.cs:102](src/Northstar.Application/Imports/ImportBatchProcessor.cs#L102)<br>[Domain/WageRateSelector.cs:18](src/Northstar.Domain/WageRates/WageRateSelector.cs#L18)<br>+4 more | [source](https://source.dot.net/#System.Linq/System/Linq/Where.cs,e73922753675387a) |
| **[`IQueryable<T>`](https://learn.microsoft.com/en-us/dotnet/api/system.linq.iqueryable-1?view=net-10.0)**<br>*interface* | Provides functionality to evaluate queries against a specific data source wherein the type of the data is known. | 25 | [Application/IApplicationDbContext.cs:20](src/Northstar.Application/Abstractions/IApplicationDbContext.cs#L20)<br>[Application/PayrollRunService.cs:222](src/Northstar.Application/PayrollRuns/PayrollRunService.cs#L222)<br>+23 more | [source](https://source.dot.net/#System.Linq.Expressions/System/Linq/IQueryable.cs,965b3e30b9ed851f) |
| **[`Queryable`](https://learn.microsoft.com/en-us/dotnet/api/system.linq.queryable?view=net-10.0)**<br>*static class* | Provides a set of static (Shared in Visual Basic) methods for querying data structures that implement IQueryable. | – | through its members | [source](https://source.dot.net/#System.Linq.Queryable/System/Linq/Queryable.cs,a6b3f724cebbc54c) |
| [`OrderBy<TSource, TKey>(this IQueryable<TSource>, Expression<Func<TSource, TKey>>)`](https://learn.microsoft.com/en-us/dotnet/api/system.linq.queryable.orderby?view=net-10.0#system-linq-queryable-orderby-2(system-linq-iqueryable((-0))-system-linq-expressions-expression((system-func((-0-1))))))<br>*extension method* | Sorts the elements of a sequence in ascending order according to a key. | 3 | [Application/ImportBatchService.cs:94](src/Northstar.Application/Imports/ImportBatchService.cs#L94)<br>[Application/WageRateService.cs:16](src/Northstar.Application/WageRates/WageRateService.cs#L16)<br>+1 more | [source](https://source.dot.net/#System.Linq.Queryable/System/Linq/Queryable.cs,7a2005ae434e9d9d) |
| [`OrderByDescending<TSource, TKey>(this IQueryable<TSource>, Expression<Func<TSource, TKey>>)`](https://learn.microsoft.com/en-us/dotnet/api/system.linq.queryable.orderbydescending?view=net-10.0#system-linq-queryable-orderbydescending-2(system-linq-iqueryable((-0))-system-linq-expressions-expression((system-func((-0-1))))))<br>*extension method* | Sorts the elements of a sequence in descending order according to a key. | 1 | [Application/PayrollRunService.cs:26](src/Northstar.Application/PayrollRuns/PayrollRunService.cs#L26) | [source](https://source.dot.net/#System.Linq.Queryable/System/Linq/Queryable.cs,ef2cf82f58e5db5c) |
| [`Select<TSource, TResult>(this IQueryable<TSource>, Expression<Func<TSource, TResult>>)`](https://learn.microsoft.com/en-us/dotnet/api/system.linq.queryable.select?view=net-10.0#system-linq-queryable-select-2(system-linq-iqueryable((-0))-system-linq-expressions-expression((system-func((-0-1))))))<br>*extension method* | Projects each element of a sequence into a new form. | 6 | [Application/ImportBatchProcessor.cs:111](src/Northstar.Application/Imports/ImportBatchProcessor.cs#L111)<br>[Application/ImportBatchService.cs:33](src/Northstar.Application/Imports/ImportBatchService.cs#L33)<br>+4 more | [source](https://source.dot.net/#System.Linq.Queryable/System/Linq/Queryable.cs,017f3b205593b416) |
| [`ThenBy<TSource, TKey>(this IOrderedQueryable<TSource>, Expression<Func<TSource, TKey>>)`](https://learn.microsoft.com/en-us/dotnet/api/system.linq.queryable.thenby?view=net-10.0#system-linq-queryable-thenby-2(system-linq-iorderedqueryable((-0))-system-linq-expressions-expression((system-func((-0-1))))))<br>*extension method* | Performs a subsequent ordering of the elements in a sequence in ascending order according to a key. | 2 | [Application/WageRateService.cs:17](src/Northstar.Application/WageRates/WageRateService.cs#L17)<br>[Application/WageRateService.cs:18](src/Northstar.Application/WageRates/WageRateService.cs#L18) | [source](https://source.dot.net/#System.Linq.Queryable/System/Linq/Queryable.cs,0f1769d7f11b8b89) |
| [`Where<TSource>(this IQueryable<TSource>, Expression<Func<TSource, bool>>)`](https://learn.microsoft.com/en-us/dotnet/api/system.linq.queryable.where?view=net-10.0#system-linq-queryable-where-1(system-linq-iqueryable((-0))-system-linq-expressions-expression((system-func((-0-system-boolean))))))<br>*extension method* | Filters a sequence of values based on a predicate. | 12 | [Application/ImportBatchProcessor.cs:86](src/Northstar.Application/Imports/ImportBatchProcessor.cs#L86)<br>[Application/ImportBatchService.cs:32](src/Northstar.Application/Imports/ImportBatchService.cs#L32)<br>+10 more | [source](https://source.dot.net/#System.Linq.Queryable/System/Linq/Queryable.cs,fceeefcb290fb6aa) |

#### System.Net

*9 APIs · 59 uses · [namespace page on Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/api/system.net?view=net-10.0)*

| API | What it does | Uses | Where | Source |
| --- | --- | ---: | --- | --- |
| **[`HttpStatusCode`](https://learn.microsoft.com/en-us/dotnet/api/system.net.httpstatuscode?view=net-10.0)**<br>*enum* | Contains the values of status codes defined for HTTP defined in RFC 2616 for HTTP 1.1. | 30 | [Infrastructure/FakePayrollProviderClient.cs:183](src/Northstar.Infrastructure/Integrations/FakePayrollProviderClient.cs#L183)<br>[IntegrationTests/FakePayrollProviderClientTests.cs:18](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L18)<br>+28 more | [source](https://source.dot.net/#System.Net.Primitives/System/Net/HttpStatusCode.cs,9b95c882b40ef96e) |
| [`Accepted`](https://learn.microsoft.com/en-us/dotnet/api/system.net.httpstatuscode?view=net-10.0)<br>*enum member* | Equivalent to HTTP status 202. | 7 | [IntegrationTests/ImportBatchesApiTests.cs:24](tests/Northstar.Api.IntegrationTests/ImportBatchesApiTests.cs#L24)<br>[IntegrationTests/ImportBatchesApiTests.cs:58](tests/Northstar.Api.IntegrationTests/ImportBatchesApiTests.cs#L58)<br>+5 more | [source](https://source.dot.net/#System.Net.Primitives/System/Net/HttpStatusCode.cs,1e4f1250e8e5ac5c) |
| [`Conflict`](https://learn.microsoft.com/en-us/dotnet/api/system.net.httpstatuscode?view=net-10.0)<br>*enum member* | Equivalent to HTTP status 409. | 1 | [IntegrationTests/ImportBatchesApiTests.cs:79](tests/Northstar.Api.IntegrationTests/ImportBatchesApiTests.cs#L79) | [source](https://source.dot.net/#System.Net.Primitives/System/Net/HttpStatusCode.cs,a387765e716ffb3c) |
| [`Created`](https://learn.microsoft.com/en-us/dotnet/api/system.net.httpstatuscode?view=net-10.0)<br>*enum member* | Equivalent to HTTP status 201. | 1 | [IntegrationTests/PayrollRunsApiTests.cs:116](tests/Northstar.Api.IntegrationTests/PayrollRunsApiTests.cs#L116) | [source](https://source.dot.net/#System.Net.Primitives/System/Net/HttpStatusCode.cs,f7afac2697e5c882) |
| [`NotFound`](https://learn.microsoft.com/en-us/dotnet/api/system.net.httpstatuscode?view=net-10.0)<br>*enum member* | Equivalent to HTTP status 404. | 3 | [IntegrationTests/ImportBatchesApiTests.cs:262](tests/Northstar.Api.IntegrationTests/ImportBatchesApiTests.cs#L262)<br>[IntegrationTests/PayrollRunsApiTests.cs:68](tests/Northstar.Api.IntegrationTests/PayrollRunsApiTests.cs#L68)<br>+1 more | [source](https://source.dot.net/#System.Net.Primitives/System/Net/HttpStatusCode.cs,35e0ca1b70dbcf0d) |
| [`OK`](https://learn.microsoft.com/en-us/dotnet/api/system.net.httpstatuscode?view=net-10.0)<br>*enum member* | Equivalent to HTTP status 200. | 13 | [IntegrationTests/FakePayrollProviderClientTests.cs:185](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L185)<br>[IntegrationTests/ImportBatchesApiTests.cs:40](tests/Northstar.Api.IntegrationTests/ImportBatchesApiTests.cs#L40)<br>+11 more | [source](https://source.dot.net/#System.Net.Primitives/System/Net/HttpStatusCode.cs,cbf374bfc92513b9) |
| [`RequestTimeout`](https://learn.microsoft.com/en-us/dotnet/api/system.net.httpstatuscode?view=net-10.0)<br>*enum member* | Equivalent to HTTP status 408. | 1 | [Infrastructure/FakePayrollProviderClient.cs:184](src/Northstar.Infrastructure/Integrations/FakePayrollProviderClient.cs#L184) | [source](https://source.dot.net/#System.Net.Primitives/System/Net/HttpStatusCode.cs,11ccbb9970ac79e5) |
| [`TooManyRequests`](https://learn.microsoft.com/en-us/dotnet/api/system.net.httpstatuscode?view=net-10.0)<br>*enum member* | Equivalent to HTTP status 429. | 2 | [Infrastructure/FakePayrollProviderClient.cs:184](src/Northstar.Infrastructure/Integrations/FakePayrollProviderClient.cs#L184)<br>[IntegrationTests/FakePayrollProviderClientTests.cs:18](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L18) | [source](https://source.dot.net/#System.Net.Primitives/System/Net/HttpStatusCode.cs,f33268163e4dc401) |
| [`UnprocessableEntity`](https://learn.microsoft.com/en-us/dotnet/api/system.net.httpstatuscode?view=net-10.0)<br>*enum member* | Equivalent to HTTP status 422. | 1 | [IntegrationTests/PayrollRunsApiTests.cs:81](tests/Northstar.Api.IntegrationTests/PayrollRunsApiTests.cs#L81) | [source](https://source.dot.net/#System.Net.Primitives/System/Net/HttpStatusCode.cs,950236eaeacab0fa) |

#### System.Net.Http

*33 APIs · 132 uses · [namespace page on Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/api/system.net.http?view=net-10.0)*

| API | What it does | Uses | Where | Source |
| --- | --- | ---: | --- | --- |
| **[`HttpClient`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient?view=net-10.0)**<br>*class* | Provides a class for sending HTTP requests and receiving HTTP responses from a resource identified by a URI. | 10 | [Infrastructure/FakePayrollProviderClient.cs:10](src/Northstar.Infrastructure/Integrations/FakePayrollProviderClient.cs#L10)<br>[IntegrationTests/FakePayrollProviderClientTests.cs:157](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L157)<br>+8 more | [source](https://source.dot.net/#System.Net.Http/System/Net/Http/HttpClient.cs,7c0c7a5434c1bab0) |
| [`new HttpClient(HttpMessageHandler)`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient.-ctor?view=net-10.0#system-net-http-httpclient-ctor(system-net-http-httpmessagehandler))<br>*constructor* | Initializes a new instance of the HttpClient class with the specified handler. | 1 | [IntegrationTests/FakePayrollProviderClientTests.cs:171](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L171) | [source](https://source.dot.net/#System.Net.Http/System/Net/Http/HttpClient.cs,34b14e2466149209) |
| [`BaseAddress`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient.baseaddress?view=net-10.0#system-net-http-httpclient-baseaddress)<br>*property* | Gets or sets the base address of Uniform Resource Identifier (URI) of the Internet resource used when sending requests. | 2 | [Infrastructure/DependencyInjection.cs:53](src/Northstar.Infrastructure/DependencyInjection.cs#L53)<br>[IntegrationTests/FakePayrollProviderClientTests.cs:173](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L173) | [source](https://source.dot.net/#System.Net.Http/System/Net/Http/HttpClient.cs,f3b1c6e22c3525ad) |
| [`DefaultRequestHeaders`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient.defaultrequestheaders?view=net-10.0#system-net-http-httpclient-defaultrequestheaders)<br>*property* | Gets the headers which should be sent with each request. | 4 | [IntegrationTests/ImportBatchesApiTests.cs:325](tests/Northstar.Api.IntegrationTests/ImportBatchesApiTests.cs#L325)<br>[IntegrationTests/PayrollRunsApiTests.cs:127](tests/Northstar.Api.IntegrationTests/PayrollRunsApiTests.cs#L127)<br>+2 more | [source](https://source.dot.net/#System.Net.Http/System/Net/Http/HttpClient.cs,254343c6d23c44b5) |
| [`Timeout`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient.timeout?view=net-10.0#system-net-http-httpclient-timeout)<br>*property* | Gets or sets the timespan to wait before the request times out. | 2 | [Infrastructure/DependencyInjection.cs:57](src/Northstar.Infrastructure/DependencyInjection.cs#L57)<br>[IntegrationTests/FakePayrollProviderClientTests.cs:174](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L174) | [source](https://source.dot.net/#System.Net.Http/System/Net/Http/HttpClient.cs,be1998ce8af6f7b2) |
| [`GetAsync(string, HttpCompletionOption, CancellationToken)`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient.getasync?view=net-10.0#system-net-http-httpclient-getasync(system-string-system-net-http-httpcompletionoption-system-threading-cancellationtoken))<br>*method* | Send a GET request to the specified URI with an HTTP completion option and a cancellation token as an asynchronous operation. | 1 | [Infrastructure/FakePayrollProviderClient.cs:86](src/Northstar.Infrastructure/Integrations/FakePayrollProviderClient.cs#L86) | [source](https://source.dot.net/#System.Net.Http/System/Net/Http/HttpClient.cs,30538d756abbb599) |
| [`GetAsync(Uri)`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient.getasync?view=net-10.0#system-net-http-httpclient-getasync(system-uri))<br>*method* | Send a GET request to the specified URI as an asynchronous operation. | 9 | [IntegrationTests/ImportBatchesApiTests.cs:38](tests/Northstar.Api.IntegrationTests/ImportBatchesApiTests.cs#L38)<br>[IntegrationTests/PayrollRunsApiTests.cs:18](tests/Northstar.Api.IntegrationTests/PayrollRunsApiTests.cs#L18)<br>+7 more | [source](https://source.dot.net/#System.Net.Http/System/Net/Http/HttpClient.cs,6c9b2562597f0842) |
| [`SendAsync(HttpRequestMessage)`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient.sendasync?view=net-10.0#system-net-http-httpclient-sendasync(system-net-http-httprequestmessage))<br>*method* | Send an HTTP request as an asynchronous operation. | 3 | [IntegrationTests/ImportBatchesApiTests.cs:345](tests/Northstar.Api.IntegrationTests/ImportBatchesApiTests.cs#L345)<br>[IntegrationTests/PayrollRunsApiTests.cs:141](tests/Northstar.Api.IntegrationTests/PayrollRunsApiTests.cs#L141)<br>+1 more | [source](https://source.dot.net/#System.Net.Http/System/Net/Http/HttpClient.cs,890db7cd792805d6) |
| **[`HttpCompletionOption`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpcompletionoption?view=net-10.0)**<br>*enum* | Indicates if HttpClient operations should be considered completed either as soon as a response is available, or after reading the entire response message including the… | 1 | [Infrastructure/FakePayrollProviderClient.cs:88](src/Northstar.Infrastructure/Integrations/FakePayrollProviderClient.cs#L88) | [source](https://source.dot.net/#System.Net.Http/System/Net/Http/HttpCompletionOption.cs,15b60d7f196f3bd5) |
| [`ResponseHeadersRead`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpcompletionoption?view=net-10.0)<br>*enum member* | The operation should complete as soon as a response is available and headers are read. | 1 | [Infrastructure/FakePayrollProviderClient.cs:88](src/Northstar.Infrastructure/Integrations/FakePayrollProviderClient.cs#L88) | [source](https://source.dot.net/#System.Net.Http/System/Net/Http/HttpCompletionOption.cs,6c4923f55a5ac36b) |
| **[`HttpContent`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpcontent?view=net-10.0)**<br>*abstract class* | A base class representing an HTTP entity body and content headers. | – | through its members | [source](https://source.dot.net/#System.Net.Http/System/Net/Http/HttpContent.cs,cd45b85bd505649d) |
| [`Headers`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpcontent.headers?view=net-10.0#system-net-http-httpcontent-headers)<br>*property* | Gets the HTTP content headers as defined in RFC 2616. | 1 | [IntegrationTests/FakePayrollProviderClientTests.cs:193](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L193) | [source](https://source.dot.net/#System.Net.Http/System/Net/Http/HttpContent.cs,17af8e915e3d354c) |
| [`ReadAsStringAsync()`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpcontent.readasstringasync?view=net-10.0#system-net-http-httpcontent-readasstringasync)<br>*method* | Serialize the HTTP content to a string as an asynchronous operation. | 3 | [IntegrationTests/ImportBatchesApiTests.cs:77](tests/Northstar.Api.IntegrationTests/ImportBatchesApiTests.cs#L77)<br>[IntegrationTests/PayrollRunsApiTests.cs:21](tests/Northstar.Api.IntegrationTests/PayrollRunsApiTests.cs#L21)<br>+1 more | [source](https://source.dot.net/#System.Net.Http/System/Net/Http/HttpContent.cs,14ea003de970131e) |
| **[`HttpMessageHandler`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpmessagehandler?view=net-10.0)**<br>*abstract class, also a base type here* | A base type for HTTP message handlers. | 2 | [IntegrationTests/FakePayrollProviderClientTests.cs:170](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L170)<br>[IntegrationTests/FakePayrollProviderClientTests.cs:238](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L238) | [source](https://source.dot.net/#System.Net.Http/System/Net/Http/HttpMessageHandler.cs,fb9c0fb7b796fb1b) |
| [`SendAsync(HttpRequestMessage, CancellationToken)`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpmessagehandler.sendasync?view=net-10.0#system-net-http-httpmessagehandler-sendasync(system-net-http-httprequestmessage-system-threading-cancellationtoken))<br>*method, overridden here* | Send an HTTP request as an asynchronous operation. | 1 | [IntegrationTests/FakePayrollProviderClientTests.cs:245](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L245) | [source](https://source.dot.net/#System.Net.Http/System/Net/Http/HttpMessageHandler.cs,9cd4f651554d3bb0) |
| **[`HttpMethod`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpmethod?view=net-10.0)**<br>*class* | A helper class for retrieving and comparing standard HTTP methods and for creating new HTTP methods. | 3 | [IntegrationTests/ImportBatchesApiTests.cs:336](tests/Northstar.Api.IntegrationTests/ImportBatchesApiTests.cs#L336)<br>[IntegrationTests/PayrollRunsApiTests.cs:135](tests/Northstar.Api.IntegrationTests/PayrollRunsApiTests.cs#L135)<br>+1 more | [source](https://source.dot.net/#System.Net.Http/System/Net/Http/HttpMethod.cs,a54e3ad64f2c9f29) |
| [`Post`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpmethod.post?view=net-10.0#system-net-http-httpmethod-post)<br>*static property* | Represents an HTTP POST protocol method that is used to post a new entity as an addition to a URI. | 3 | [IntegrationTests/ImportBatchesApiTests.cs:336](tests/Northstar.Api.IntegrationTests/ImportBatchesApiTests.cs#L336)<br>[IntegrationTests/PayrollRunsApiTests.cs:135](tests/Northstar.Api.IntegrationTests/PayrollRunsApiTests.cs#L135)<br>+1 more | [source](https://source.dot.net/#System.Net.Http/System/Net/Http/HttpMethod.cs,c5789756905426d9) |
| **[`HttpRequestException`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httprequestexception?view=net-10.0)**<br>*class* | A base class for exceptions thrown by the HttpClient and HttpMessageHandler classes. | 2 | [Infrastructure/FakePayrollProviderClient.cs:122](src/Northstar.Infrastructure/Integrations/FakePayrollProviderClient.cs#L122)<br>[Infrastructure/FakePayrollProviderClient.cs:127](src/Northstar.Infrastructure/Integrations/FakePayrollProviderClient.cs#L127) | [source](https://source.dot.net/#System.Net.Http/System/Net/Http/HttpRequestException.cs,f252af5f3f328762) |
| **[`HttpRequestMessage`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httprequestmessage?view=net-10.0)**<br>*class* | Represents a HTTP request message. | 6 | [IntegrationTests/FakePayrollProviderClientTests.cs:237](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L237)<br>[IntegrationTests/ImportBatchesApiTests.cs:335](tests/Northstar.Api.IntegrationTests/ImportBatchesApiTests.cs#L335)<br>+4 more | [source](https://source.dot.net/#System.Net.Http/System/Net/Http/HttpRequestMessage.cs,118c5c844829ffb3) |
| [`new HttpRequestMessage(HttpMethod, Uri)`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httprequestmessage.-ctor?view=net-10.0#system-net-http-httprequestmessage-ctor(system-net-http-httpmethod-system-uri))<br>*constructor* | Initializes a new instance of the HttpRequestMessage class with an HTTP method and a request Uri. | 3 | [IntegrationTests/ImportBatchesApiTests.cs:335](tests/Northstar.Api.IntegrationTests/ImportBatchesApiTests.cs#L335)<br>[IntegrationTests/PayrollRunsApiTests.cs:134](tests/Northstar.Api.IntegrationTests/PayrollRunsApiTests.cs#L134)<br>+1 more | [source](https://source.dot.net/#System.Net.Http/System/Net/Http/HttpRequestMessage.cs,4ca7c4ecc362231e) |
| [`Content`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httprequestmessage.content?view=net-10.0#system-net-http-httprequestmessage-content)<br>*property* | Gets or sets the contents of the HTTP message. | 3 | [IntegrationTests/ImportBatchesApiTests.cs:339](tests/Northstar.Api.IntegrationTests/ImportBatchesApiTests.cs#L339)<br>[IntegrationTests/PayrollRunsApiTests.cs:138](tests/Northstar.Api.IntegrationTests/PayrollRunsApiTests.cs#L138)<br>+1 more | [source](https://source.dot.net/#System.Net.Http/System/Net/Http/HttpRequestMessage.cs,270cd6bc72d1d796) |
| [`Headers`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httprequestmessage.headers?view=net-10.0#system-net-http-httprequestmessage-headers)<br>*property* | Gets the collection of HTTP request headers. | 3 | [IntegrationTests/ImportBatchesApiTests.cs:344](tests/Northstar.Api.IntegrationTests/ImportBatchesApiTests.cs#L344)<br>[IntegrationTests/PayrollRunsApiTests.cs:140](tests/Northstar.Api.IntegrationTests/PayrollRunsApiTests.cs#L140)<br>+1 more | [source](https://source.dot.net/#System.Net.Http/System/Net/Http/HttpRequestMessage.cs,c9df50ca835bc0bd) |
| [`RequestUri`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httprequestmessage.requesturi?view=net-10.0#system-net-http-httprequestmessage-requesturi)<br>*property* | Gets or sets the Uri used for the HTTP request. | 2 | [IntegrationTests/FakePayrollProviderClientTests.cs:68](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L68)<br>[IntegrationTests/FakePayrollProviderClientTests.cs:72](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L72) | [source](https://source.dot.net/#System.Net.Http/System/Net/Http/HttpRequestMessage.cs,7f16a0aef607696b) |
| **[`HttpResponseMessage`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpresponsemessage?view=net-10.0)**<br>*class* | Represents a HTTP response message including the status code and data. | 12 | [Infrastructure/FakePayrollProviderClient.cs:172](src/Northstar.Infrastructure/Integrations/FakePayrollProviderClient.cs#L172)<br>[IntegrationTests/FakePayrollProviderClientTests.cs:18](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L18)<br>+10 more | [source](https://source.dot.net/#System.Net.Http/System/Net/Http/HttpResponseMessage.cs,05a8b293e3792b58) |
| [`new HttpResponseMessage(HttpStatusCode)`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpresponsemessage.-ctor?view=net-10.0#system-net-http-httpresponsemessage-ctor(system-net-httpstatuscode))<br>*constructor* | Initializes a new instance of the HttpResponseMessage class with a specific HttpResponseMessage.StatusCode. | 3 | [IntegrationTests/FakePayrollProviderClientTests.cs:18](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L18)<br>[IntegrationTests/FakePayrollProviderClientTests.cs:185](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L185)<br>+1 more | [source](https://source.dot.net/#System.Net.Http/System/Net/Http/HttpResponseMessage.cs,9a9e2779dee5bf4c) |
| [`Content`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpresponsemessage.content?view=net-10.0#system-net-http-httpresponsemessage-content)<br>*property* | Gets or sets the content of a HTTP response message. | 16 | [Infrastructure/FakePayrollProviderClient.cs:95](src/Northstar.Infrastructure/Integrations/FakePayrollProviderClient.cs#L95)<br>[IntegrationTests/FakePayrollProviderClientTests.cs:187](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L187)<br>+14 more | [source](https://source.dot.net/#System.Net.Http/System/Net/Http/HttpResponseMessage.cs,29d639c7ef3851ec) |
| [`Headers`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpresponsemessage.headers?view=net-10.0#system-net-http-httpresponsemessage-headers)<br>*property* | Gets the collection of HTTP response headers. | 3 | [Infrastructure/FakePayrollProviderClient.cs:174](src/Northstar.Infrastructure/Integrations/FakePayrollProviderClient.cs#L174)<br>[IntegrationTests/ImportBatchesApiTests.cs:25](tests/Northstar.Api.IntegrationTests/ImportBatchesApiTests.cs#L25)<br>+1 more | [source](https://source.dot.net/#System.Net.Http/System/Net/Http/HttpResponseMessage.cs,7ca03ee6e9ddb820) |
| [`IsSuccessStatusCode`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpresponsemessage.issuccessstatuscode?view=net-10.0#system-net-http-httpresponsemessage-issuccessstatuscode)<br>*property* | Gets a value that indicates if the HTTP response was successful. | 1 | [Infrastructure/FakePayrollProviderClient.cs:91](src/Northstar.Infrastructure/Integrations/FakePayrollProviderClient.cs#L91) | [source](https://source.dot.net/#System.Net.Http/System/Net/Http/HttpResponseMessage.cs,85a7ef9189314fea) |
| [`StatusCode`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpresponsemessage.statuscode?view=net-10.0#system-net-http-httpresponsemessage-statuscode)<br>*property* | Gets or sets the status code of the HTTP response. | 27 | [Infrastructure/FakePayrollProviderClient.cs:106](src/Northstar.Infrastructure/Integrations/FakePayrollProviderClient.cs#L106)<br>[IntegrationTests/ImportBatchesApiTests.cs:24](tests/Northstar.Api.IntegrationTests/ImportBatchesApiTests.cs#L24)<br>+25 more | [source](https://source.dot.net/#System.Net.Http/System/Net/Http/HttpResponseMessage.cs,5f0a91b0e794dad4) |
| **[`StreamContent`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.streamcontent?view=net-10.0)**<br>*class* | Provides HTTP content based on a stream. | 1 | [IntegrationTests/FakePayrollProviderClientTests.cs:192](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L192) | [source](https://source.dot.net/#System.Net.Http/System/Net/Http/StreamContent.cs,907301bd1787b369) |
| [`new StreamContent(Stream)`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.streamcontent.-ctor?view=net-10.0#system-net-http-streamcontent-ctor(system-io-stream))<br>*constructor* | Creates a new instance of the StreamContent class. | 1 | [IntegrationTests/FakePayrollProviderClientTests.cs:192](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L192) | [source](https://source.dot.net/#System.Net.Http/System/Net/Http/StreamContent.cs,a3ec77c75eaff273) |
| **[`StringContent`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.stringcontent?view=net-10.0)**<br>*class* | Provides HTTP content based on a string. | 1 | [IntegrationTests/FakePayrollProviderClientTests.cs:187](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L187) | [source](https://source.dot.net/#System.Net.Http/System/Net/Http/StringContent.cs,b14d05f33e64fbe5) |
| [`new StringContent(string, Encoding, string)`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.stringcontent.-ctor?view=net-10.0#system-net-http-stringcontent-ctor(system-string-system-text-encoding-system-string))<br>*constructor* | Creates a new instance of the StringContent class. | 1 | [IntegrationTests/FakePayrollProviderClientTests.cs:187](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L187) | [source](https://source.dot.net/#System.Net.Http/System/Net/Http/StringContent.cs,3d1736de57f159db) |

#### System.Net.Http.Headers

*11 APIs · 13 uses · [namespace page on Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.headers?view=net-10.0)*

| API | What it does | Uses | Where | Source |
| --- | --- | ---: | --- | --- |
| **[`HttpContentHeaders`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.headers.httpcontentheaders?view=net-10.0)**<br>*class* | Represents the collection of Content Headers as defined in RFC 2616. | – | through its members | [source](https://source.dot.net/#System.Net.Http/System/Net/Http/Headers/HttpContentHeaders.cs,0f53e9be0eba446a) |
| [`ContentType`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.headers.httpcontentheaders.contenttype?view=net-10.0#system-net-http-headers-httpcontentheaders-contenttype)<br>*property* | Gets or sets the value of the Content-Type content header on an HTTP response. | 1 | [IntegrationTests/FakePayrollProviderClientTests.cs:193](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L193) | [source](https://source.dot.net/#System.Net.Http/System/Net/Http/Headers/HttpContentHeaders.cs,3290ce74d0637fa1) |
| **[`HttpHeaders`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.headers.httpheaders?view=net-10.0)**<br>*abstract class* | A collection of headers and their values as defined in RFC 2616. | – | through its members | [source](https://source.dot.net/#System.Net.Http/System/Net/Http/Headers/HttpHeaders.cs,560965e4f867292b) |
| [`Add(string, string)`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.headers.httpheaders.add?view=net-10.0#system-net-http-headers-httpheaders-add(system-string-system-string))<br>*method* | Adds the specified header and its value into the HttpHeaders collection. | 7 | [IntegrationTests/ImportBatchesApiTests.cs:325](tests/Northstar.Api.IntegrationTests/ImportBatchesApiTests.cs#L325)<br>[IntegrationTests/PayrollRunsApiTests.cs:127](tests/Northstar.Api.IntegrationTests/PayrollRunsApiTests.cs#L127)<br>+5 more | [source](https://source.dot.net/#System.Net.Http/System/Net/Http/Headers/HttpHeaders.cs,1bd98466f4f2ace6) |
| **[`HttpResponseHeaders`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.headers.httpresponseheaders?view=net-10.0)**<br>*class* | Represents the collection of Response Headers as defined in RFC 2616. | – | through its members | [source](https://source.dot.net/#System.Net.Http/System/Net/Http/Headers/HttpResponseHeaders.cs,50f2e4753114470d) |
| [`Location`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.headers.httpresponseheaders.location?view=net-10.0#system-net-http-headers-httpresponseheaders-location)<br>*property* | Gets or sets the value of the Location header for an HTTP response. | 2 | [IntegrationTests/ImportBatchesApiTests.cs:25](tests/Northstar.Api.IntegrationTests/ImportBatchesApiTests.cs#L25)<br>[IntegrationTests/ImportBatchesApiTests.cs:26](tests/Northstar.Api.IntegrationTests/ImportBatchesApiTests.cs#L26) | [source](https://source.dot.net/#System.Net.Http/System/Net/Http/Headers/HttpResponseHeaders.cs,fee2004ab8017ee8) |
| [`RetryAfter`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.headers.httpresponseheaders.retryafter?view=net-10.0#system-net-http-headers-httpresponseheaders-retryafter)<br>*property* | Gets or sets the value of the Retry-After header for an HTTP response. | 1 | [Infrastructure/FakePayrollProviderClient.cs:174](src/Northstar.Infrastructure/Integrations/FakePayrollProviderClient.cs#L174) | [source](https://source.dot.net/#System.Net.Http/System/Net/Http/Headers/HttpResponseHeaders.cs,bd4937048184670d) |
| **[`MediaTypeHeaderValue`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.headers.mediatypeheadervalue?view=net-10.0)**<br>*class* | Represents a media type used in a Content-Type header as defined in the RFC 2616. | – | through its members | [source](https://source.dot.net/#System.Net.Http/System/Net/Http/Headers/MediaTypeHeaderValue.cs,9856dc052045cf60) |
| [`new MediaTypeHeaderValue(string)`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.headers.mediatypeheadervalue.-ctor?view=net-10.0#system-net-http-headers-mediatypeheadervalue-ctor(system-string))<br>*constructor* | Initializes a new instance of the MediaTypeHeaderValue class. | 1 | [IntegrationTests/FakePayrollProviderClientTests.cs:193](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L193) | [source](https://source.dot.net/#System.Net.Http/System/Net/Http/Headers/MediaTypeHeaderValue.cs,3324354f1a345956) |
| **[`RetryConditionHeaderValue`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.headers.retryconditionheadervalue?view=net-10.0)**<br>*class* | Represents a Retry-After header value that can either be a date/time or a timespan value. | – | through its members | [source](https://source.dot.net/#System.Net.Http/System/Net/Http/Headers/RetryConditionHeaderValue.cs,1f5610f7636c8bc9) |
| [`Delta`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.headers.retryconditionheadervalue.delta?view=net-10.0#system-net-http-headers-retryconditionheadervalue-delta)<br>*property* | Gets the delta in seconds from the RetryConditionHeaderValue object. | 1 | [Infrastructure/FakePayrollProviderClient.cs:174](src/Northstar.Infrastructure/Integrations/FakePayrollProviderClient.cs#L174) | [source](https://source.dot.net/#System.Net.Http/System/Net/Http/Headers/RetryConditionHeaderValue.cs,36145a1709a5ef21) |

#### System.Net.Http.Json

*6 APIs · 20 uses · [namespace page on Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.json?view=net-10.0)*

| API | What it does | Uses | Where | Source |
| --- | --- | ---: | --- | --- |
| **[`HttpClientJsonExtensions`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.json.httpclientjsonextensions?view=net-10.0)**<br>*static class* | Contains extension methods to send and receive HTTP content as JSON. | – | through its members | [source](https://source.dot.net/#System.Net.Http.Json/System/Net/Http/Json/HttpClientJsonExtensions.cs,5c6802086c2a62d1) |
| [`PostAsJsonAsync<TValue>(this HttpClient, Uri, TValue, JsonSerializerOptions, CancellationToken)`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.json.httpclientjsonextensions.postasjsonasync?view=net-10.0#system-net-http-json-httpclientjsonextensions-postasjsonasync-1(system-net-http-httpclient-system-uri-0-system-text-json-jsonserializeroptions-system-threading-cancellationtoken))<br>*extension method* | Sends a POST request to the specified Uri containing the value serialized as JSON in the request body. | 3 | [IntegrationTests/PayrollRunsApiTests.cs:77](tests/Northstar.Api.IntegrationTests/PayrollRunsApiTests.cs#L77)<br>[IntegrationTests/PayrollRunsApiTests.cs:104](tests/Northstar.Api.IntegrationTests/PayrollRunsApiTests.cs#L104)<br>+1 more | [source](https://source.dot.net/#System.Net.Http.Json/System/Net/Http/Json/HttpClientJsonExtensions.Post.cs,71a0a0a721021634) |
| **[`HttpContentJsonExtensions`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.json.httpcontentjsonextensions?view=net-10.0)**<br>*static class* | Contains extension methods to read and then parse the HttpContent from JSON. | – | through its members | [source](https://source.dot.net/#System.Net.Http.Json/System/Net/Http/Json/HttpContentJsonExtensions.cs,8780268fe1d20f2b) |
| [`ReadFromJsonAsync<T>(this HttpContent, CancellationToken)`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.json.httpcontentjsonextensions.readfromjsonasync?view=net-10.0#system-net-http-json-httpcontentjsonextensions-readfromjsonasync-1(system-net-http-httpcontent-system-threading-cancellationtoken))<br>*extension method* | Reads the HTTP content and returns the value that results from deserializing the content as JSON in an asynchronous operation. | 11 | [Infrastructure/FakePayrollProviderClient.cs:95](src/Northstar.Infrastructure/Integrations/FakePayrollProviderClient.cs#L95)<br>[IntegrationTests/ImportBatchesApiTests.cs:39](tests/Northstar.Api.IntegrationTests/ImportBatchesApiTests.cs#L39)<br>+9 more | [source](https://source.dot.net/#System.Net.Http.Json/System/Net/Http/Json/HttpContentJsonExtensions.cs,9d811353ca085b5e) |
| **[`JsonContent`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.json.jsoncontent?view=net-10.0)**<br>*class* | Provides HTTP content based on JSON. | 3 | [IntegrationTests/ImportBatchesApiTests.cs:339](tests/Northstar.Api.IntegrationTests/ImportBatchesApiTests.cs#L339)<br>[IntegrationTests/PayrollRunsApiTests.cs:138](tests/Northstar.Api.IntegrationTests/PayrollRunsApiTests.cs#L138)<br>+1 more | [source](https://source.dot.net/#System.Net.Http.Json/System/Net/Http/Json/JsonContent.cs,2eaf86f203b97526) |
| [`Create<T>(T, MediaTypeHeaderValue, JsonSerializerOptions)`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.json.jsoncontent.create?view=net-10.0#system-net-http-json-jsoncontent-create-1(-0-system-net-http-headers-mediatypeheadervalue-system-text-json-jsonserializeroptions))<br>*static method* | Creates a new instance of the JsonContent class that will contain the inputValue serialized as JSON. | 3 | [IntegrationTests/ImportBatchesApiTests.cs:339](tests/Northstar.Api.IntegrationTests/ImportBatchesApiTests.cs#L339)<br>[IntegrationTests/PayrollRunsApiTests.cs:138](tests/Northstar.Api.IntegrationTests/PayrollRunsApiTests.cs#L138)<br>+1 more | [source](https://source.dot.net/#System.Net.Http.Json/System/Net/Http/Json/JsonContent.cs,763e949d9eaa5b8b) |

#### System.Security.Cryptography

*2 APIs · 6 uses · [namespace page on Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography?view=net-10.0)*

| API | What it does | Uses | Where | Source |
| --- | --- | ---: | --- | --- |
| **[`SHA256`](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.sha256?view=net-10.0)**<br>*abstract class* | Computes the SHA256 hash for the input data. | 3 | [Application/ImportBatchService.cs:382](src/Northstar.Application/Imports/ImportBatchService.cs#L382)<br>[Application/PayrollRunService.cs:276](src/Northstar.Application/PayrollRuns/PayrollRunService.cs#L276)<br>+1 more | [source](https://source.dot.net/#System.Security.Cryptography/System/Security/Cryptography/SHA256.cs,b87ea7e64312d482) |
| [`HashData(byte[])`](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.sha256.hashdata?view=net-10.0#system-security-cryptography-sha256-hashdata(system-byte()))<br>*static method* | Computes the hash of data using the SHA-256 algorithm. | 3 | [Application/ImportBatchService.cs:382](src/Northstar.Application/Imports/ImportBatchService.cs#L382)<br>[Application/PayrollRunService.cs:276](src/Northstar.Application/PayrollRuns/PayrollRunService.cs#L276)<br>+1 more | [source](https://source.dot.net/#System.Security.Cryptography/System/Security/Cryptography/SHA256.cs,2b805a540db3f023) |

#### System.Text

*3 APIs · 14 uses · [namespace page on Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/api/system.text?view=net-10.0)*

| API | What it does | Uses | Where | Source |
| --- | --- | ---: | --- | --- |
| **[`Encoding`](https://learn.microsoft.com/en-us/dotnet/api/system.text.encoding?view=net-10.0)**<br>*abstract class* | Represents a character encoding. | 5 | [Application/ImportBatchService.cs:382](src/Northstar.Application/Imports/ImportBatchService.cs#L382)<br>[Application/PayrollRunService.cs:276](src/Northstar.Application/PayrollRuns/PayrollRunService.cs#L276)<br>+3 more | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Text/Encoding.cs,3b6090c501893c25) |
| [`UTF8`](https://learn.microsoft.com/en-us/dotnet/api/system.text.encoding.utf8?view=net-10.0#system-text-encoding-utf8)<br>*static property* | Gets an encoding for the UTF-8 format. | 5 | [Application/ImportBatchService.cs:382](src/Northstar.Application/Imports/ImportBatchService.cs#L382)<br>[Application/PayrollRunService.cs:276](src/Northstar.Application/PayrollRuns/PayrollRunService.cs#L276)<br>+3 more | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Text/Encoding.cs,a10eb90a3d884500) |
| [`GetBytes(string)`](https://learn.microsoft.com/en-us/dotnet/api/system.text.encoding.getbytes?view=net-10.0#system-text-encoding-getbytes(system-string))<br>*method* | When overridden in a derived class, encodes all the characters in the specified string into a sequence of bytes. | 4 | [Application/ImportBatchService.cs:382](src/Northstar.Application/Imports/ImportBatchService.cs#L382)<br>[Application/PayrollRunService.cs:276](src/Northstar.Application/PayrollRuns/PayrollRunService.cs#L276)<br>+2 more | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Text/Encoding.cs,83f2d2c6f22db1a9) |

#### System.Text.Json

*5 APIs · 4 uses · [namespace page on Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/api/system.text.json?view=net-10.0)*

| API | What it does | Uses | Where | Source |
| --- | --- | ---: | --- | --- |
| **[`JsonException`](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.jsonexception?view=net-10.0)**<br>*class* | Defines a custom exception object that is thrown when invalid JSON text is encountered, the defined maximum depth is passed, or the JSON text is not compatible with the… | 1 | [Infrastructure/FakePayrollProviderClient.cs:98](src/Northstar.Infrastructure/Integrations/FakePayrollProviderClient.cs#L98) | [source](https://source.dot.net/#System.Text.Json/System/Text/Json/JsonException.cs,8f167087a7a6d1da) |
| **[`JsonSerializer`](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.jsonserializer?view=net-10.0)**<br>*static class* | Provides functionality to serialize objects or value types to JSON and to deserialize JSON into objects or value types. | 1 | [Application/ImportBatchProcessor.cs:218](src/Northstar.Application/Imports/ImportBatchProcessor.cs#L218) | [source](https://source.dot.net/#System.Text.Json/System/Text/Json/Serialization/JsonSerializer.Helpers.cs,2ca342a99419a245) |
| [`Serialize<TValue>(TValue, JsonSerializerOptions)`](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.jsonserializer.serialize?view=net-10.0#system-text-json-jsonserializer-serialize-1(-0-system-text-json-jsonserializeroptions))<br>*static method* | Converts the value of a type specified by a generic type parameter into a JSON string. | 1 | [Application/ImportBatchProcessor.cs:218](src/Northstar.Application/Imports/ImportBatchProcessor.cs#L218) | [source](https://source.dot.net/#System.Text.Json/System/Text/Json/Serialization/JsonSerializer.Write.String.cs,6280f7c9112e930b) |
| **[`JsonSerializerOptions`](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.jsonserializeroptions?view=net-10.0)**<br>*class* | Provides options to be used with JsonSerializer. | – | through its members | [source](https://source.dot.net/#System.Text.Json/System/Text/Json/Serialization/JsonSerializerOptions.cs,58d9e4c4da028e18) |
| [`Converters`](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.jsonserializeroptions.converters?view=net-10.0#system-text-json-jsonserializeroptions-converters)<br>*property* | Gets the list of user-defined converters that were registered. | 1 | [Api/Program.cs:11](src/Northstar.Api/Program.cs#L11) | [source](https://source.dot.net/#System.Text.Json/System/Text/Json/Serialization/JsonSerializerOptions.Converters.cs,72df4307f70008b8) |

#### System.Text.Json.Serialization

*2 APIs · 2 uses · [namespace page on Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.serialization?view=net-10.0)*

| API | What it does | Uses | Where | Source |
| --- | --- | ---: | --- | --- |
| **[`JsonStringEnumConverter`](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.serialization.jsonstringenumconverter?view=net-10.0)**<br>*class* | Converts enumeration values to and from strings. | 1 | [Api/Program.cs:11](src/Northstar.Api/Program.cs#L11) | [source](https://source.dot.net/#System.Text.Json/System/Text/Json/Serialization/JsonStringEnumConverter.cs,66907b2ded1a2bb8) |
| [`new JsonStringEnumConverter()`](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.serialization.jsonstringenumconverter.-ctor?view=net-10.0#system-text-json-serialization-jsonstringenumconverter-ctor)<br>*constructor* | Initializes an instance of the JsonStringEnumConverter class with the default naming policy that allows integer values. | 1 | [Api/Program.cs:11](src/Northstar.Api/Program.cs#L11) | [source](https://source.dot.net/#System.Text.Json/System/Text/Json/Serialization/JsonStringEnumConverter.cs,96759c0d33bb7b35) |

#### System.Threading

*21 APIs · 110 uses · [namespace page on Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/api/system.threading?view=net-10.0)*

| API | What it does | Uses | Where | Source |
| --- | --- | ---: | --- | --- |
| **[`CancellationToken`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken?view=net-10.0)**<br>*struct* | Propagates notification that operations should be canceled. | 59 | [Api/ImportBatchesController.cs:16](src/Northstar.Api/Controllers/ImportBatchesController.cs#L16)<br>[Api/PayrollRunsController.cs:15](src/Northstar.Api/Controllers/PayrollRunsController.cs#L15)<br>+57 more | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Threading/CancellationToken.cs,36b17ded8b1a228c) |
| [`IsCancellationRequested`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken.iscancellationrequested?view=net-10.0#system-threading-cancellationtoken-iscancellationrequested)<br>*property* | Gets whether cancellation has been requested for this token. | 5 | [Infrastructure/FakePayrollProviderClient.cs:134](src/Northstar.Infrastructure/Integrations/FakePayrollProviderClient.cs#L134)<br>[Infrastructure/ImportBatchWorker.cs:30](src/Northstar.Infrastructure/Messaging/ImportBatchWorker.cs#L30)<br>+3 more | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Threading/CancellationToken.cs,f32abbe8e2f1f6c1) |
| [`None`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken.none?view=net-10.0#system-threading-cancellationtoken-none)<br>*static property* | Returns an empty CancellationToken value. | 12 | [Application/ImportBatchService.cs:171](src/Northstar.Application/Imports/ImportBatchService.cs#L171)<br>[IntegrationTests/FakePayrollProviderClientTests.cs:23](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L23)<br>+10 more | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Threading/CancellationToken.cs,ec32c41597007382) |
| [`ThrowIfCancellationRequested()`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken.throwifcancellationrequested?view=net-10.0#system-threading-cancellationtoken-throwifcancellationrequested)<br>*method* | Throws a OperationCanceledException if this token has had cancellation requested. | 1 | [IntegrationTests/DeterministicPayrollProviderClient.cs:22](tests/Northstar.Api.IntegrationTests/DeterministicPayrollProviderClient.cs#L22) | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Threading/CancellationToken.cs,d2864d73804288fc) |
| **[`CancellationTokenSource`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtokensource?view=net-10.0)**<br>*class* | Signals to a CancellationToken that it should be canceled. | 3 | [Infrastructure/FakePayrollProviderClient.cs:80](src/Northstar.Infrastructure/Integrations/FakePayrollProviderClient.cs#L80)<br>[Infrastructure/ImportBatchWorker.cs:103](src/Northstar.Infrastructure/Messaging/ImportBatchWorker.cs#L103)<br>+1 more | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Threading/CancellationTokenSource.cs,130a9536ac96b392) |
| [`new CancellationTokenSource()`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtokensource.-ctor?view=net-10.0#system-threading-cancellationtokensource-ctor)<br>*constructor* | Initializes a new instance of the CancellationTokenSource class. | 1 | [IntegrationTests/FakePayrollProviderClientTests.cs:113](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L113) | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Threading/CancellationTokenSource.cs,0fd973cb6741d972) |
| [`new CancellationTokenSource(TimeSpan)`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtokensource.-ctor?view=net-10.0#system-threading-cancellationtokensource-ctor(system-timespan))<br>*constructor* | Initializes a new instance of the CancellationTokenSource class that will be canceled after the specified time span. | 1 | [Infrastructure/ImportBatchWorker.cs:103](src/Northstar.Infrastructure/Messaging/ImportBatchWorker.cs#L103) | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Threading/CancellationTokenSource.cs,2f4ad148fd857d6f) |
| [`Token`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtokensource.token?view=net-10.0#system-threading-cancellationtokensource-token)<br>*property* | Gets the CancellationToken associated with this CancellationTokenSource. | 4 | [Infrastructure/FakePayrollProviderClient.cs:89](src/Northstar.Infrastructure/Integrations/FakePayrollProviderClient.cs#L89)<br>[Infrastructure/ImportBatchWorker.cs:108](src/Northstar.Infrastructure/Messaging/ImportBatchWorker.cs#L108)<br>+2 more | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Threading/CancellationTokenSource.cs,9e22b9c8995bf195) |
| [`CancelAfter(TimeSpan)`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtokensource.cancelafter?view=net-10.0#system-threading-cancellationtokensource-cancelafter(system-timespan))<br>*method* | Schedules a cancel operation on this CancellationTokenSource after the specified time span. | 1 | [Infrastructure/FakePayrollProviderClient.cs:81](src/Northstar.Infrastructure/Integrations/FakePayrollProviderClient.cs#L81) | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Threading/CancellationTokenSource.cs,70756b542df0e802) |
| [`CancelAsync()`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtokensource.cancelasync?view=net-10.0#system-threading-cancellationtokensource-cancelasync)<br>*method* | Communicates a request for cancellation asynchronously. | 1 | [IntegrationTests/FakePayrollProviderClientTests.cs:117](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L117) | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Threading/CancellationTokenSource.cs,e934519ad6922297) |
| [`CreateLinkedTokenSource(CancellationToken)`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtokensource.createlinkedtokensource?view=net-10.0)<br>*static method* | Creates a CancellationTokenSource that will be in the canceled state when the supplied token is in the canceled state. | 1 | [Infrastructure/FakePayrollProviderClient.cs:80](src/Northstar.Infrastructure/Integrations/FakePayrollProviderClient.cs#L80) | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Threading/CancellationTokenSource.cs,d0475512482e2cf5) |
| **[`Interlocked`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.interlocked?view=net-10.0)**<br>*static class* | Provides atomic operations for variables that are shared by multiple threads. | 3 | [IntegrationTests/DeterministicPayrollProviderClient.cs:16](tests/Northstar.Api.IntegrationTests/DeterministicPayrollProviderClient.cs#L16)<br>[IntegrationTests/FakePayrollProviderClientTests.cs:249](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L249)<br>+1 more | [source](https://source.dot.net/#System.Private.CoreLib/Generated/Basic.CompilerLog.Util/Basic.CompilerLog.Util.Impl.BasicGeneratedFilesAnalyzerReference/LibraryImports.g.cs,8792520ddc6dadcb) |
| [`Exchange(ref int, int)`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.interlocked.exchange?view=net-10.0#system-threading-interlocked-exchange(system-int32@-system-int32))<br>*static method* | Sets a 32-bit signed integer to a specified value and returns the original value, as an atomic operation. | 2 | [IntegrationTests/DeterministicPayrollProviderClient.cs:16](tests/Northstar.Api.IntegrationTests/DeterministicPayrollProviderClient.cs#L16)<br>[IntegrationTests/DeterministicPayrollProviderClient.cs:31](tests/Northstar.Api.IntegrationTests/DeterministicPayrollProviderClient.cs#L31) | [source](https://source.dot.net/#System.Private.CoreLib/System/Threading/Interlocked.cs,52be0cc9b3954ae9) |
| [`Increment(ref int)`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.interlocked.increment?view=net-10.0#system-threading-interlocked-increment(system-int32@))<br>*static method* | Increments a specified variable and stores the result, as an atomic operation. | 1 | [IntegrationTests/FakePayrollProviderClientTests.cs:249](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L249) | [source](https://source.dot.net/#System.Private.CoreLib/System/Threading/Interlocked.cs,09bb1e21afd76cf2) |
| **[`PeriodicTimer`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.periodictimer?view=net-10.0)**<br>*class* | Provides a periodic timer that enables waiting asynchronously for timer ticks. | 1 | [Infrastructure/ImportBatchWorker.cs:59](src/Northstar.Infrastructure/Messaging/ImportBatchWorker.cs#L59) | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Threading/PeriodicTimer.cs,c20c40d62c678c19) |
| [`new PeriodicTimer(TimeSpan)`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.periodictimer.-ctor?view=net-10.0#system-threading-periodictimer-ctor(system-timespan))<br>*constructor* | Initializes the timer. | 1 | [Infrastructure/ImportBatchWorker.cs:59](src/Northstar.Infrastructure/Messaging/ImportBatchWorker.cs#L59) | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Threading/PeriodicTimer.cs,d3328f7bd7d4d39d) |
| [`WaitForNextTickAsync(CancellationToken)`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.periodictimer.waitfornexttickasync?view=net-10.0#system-threading-periodictimer-waitfornexttickasync(system-threading-cancellationtoken))<br>*method* | Waits for the next tick of the timer, or for the timer to be stopped. | 1 | [Infrastructure/ImportBatchWorker.cs:98](src/Northstar.Infrastructure/Messaging/ImportBatchWorker.cs#L98) | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Threading/PeriodicTimer.cs,9fddf99edae16ebb) |
| **[`Timeout`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.timeout?view=net-10.0)**<br>*static class* | Contains constants that specify infinite time-out intervals. | 5 | [Infrastructure/DependencyInjection.cs:57](src/Northstar.Infrastructure/DependencyInjection.cs#L57)<br>[IntegrationTests/FakePayrollProviderClientTests.cs:108](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L108)<br>+3 more | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Threading/Timeout.cs,6b53654d3452a8df) |
| [`InfiniteTimeSpan`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.timeout.infinitetimespan?view=net-10.0#system-threading-timeout-infinitetimespan)<br>*static field* | A constant used to specify an infinite waiting period, for methods that accept a TimeSpan parameter. | 5 | [Infrastructure/DependencyInjection.cs:57](src/Northstar.Infrastructure/DependencyInjection.cs#L57)<br>[IntegrationTests/FakePayrollProviderClientTests.cs:108](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L108)<br>+3 more | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Threading/Timeout.cs,c679290f50556e4e) |
| **[`Volatile`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.volatile?view=net-10.0)**<br>*static class* | Contains methods for performing volatile memory operations. | 1 | [IntegrationTests/FakePayrollProviderClientTests.cs:243](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L243) | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Threading/Volatile.cs,d7abcac016fb599d) |
| [`Read(ref readonly int)`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.volatile.read?view=net-10.0#system-threading-volatile-read(system-int32@))<br>*static method* | Reads the value of the specified field. | 1 | [IntegrationTests/FakePayrollProviderClientTests.cs:243](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L243) | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Threading/Volatile.cs,9eb597a4d9ba5db9) |

#### System.Threading.Channels

*15 APIs · 11 uses · [namespace page on Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/api/system.threading.channels?view=net-10.0)*

| API | What it does | Uses | Where | Source |
| --- | --- | ---: | --- | --- |
| **[`Channel`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.channels.channel?view=net-10.0)**<br>*static class* | Provides static methods for creating channels. | 1 | [Infrastructure/ImportBatchQueue.cs:8](src/Northstar.Infrastructure/Messaging/ImportBatchQueue.cs#L8) | [source](https://source.dot.net/#System.Threading.Channels/System/Threading/Channels/Channel.cs,e421c29ff5f629a0) |
| [`CreateUnbounded<T>(UnboundedChannelOptions)`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.channels.channel.createunbounded?view=net-10.0#system-threading-channels-channel-createunbounded-1(system-threading-channels-unboundedchanneloptions))<br>*static method* | Creates an unbounded channel subject to the provided options. | 1 | [Infrastructure/ImportBatchQueue.cs:8](src/Northstar.Infrastructure/Messaging/ImportBatchQueue.cs#L8) | [source](https://source.dot.net/#System.Threading.Channels/System/Threading/Channels/Channel.cs,ae2dfd21b37e065e) |
| **[`Channel<T>`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.channels.channel-1?view=net-10.0)**<br>*abstract class* | Provides a base class for channels that support reading and writing elements of type T. | 1 | [Infrastructure/ImportBatchQueue.cs:8](src/Northstar.Infrastructure/Messaging/ImportBatchQueue.cs#L8) | [source](https://source.dot.net/#System.Threading.Channels/System/Threading/Channels/Channel_1.cs,65a299de755359b2) |
| **[`Channel<TWrite, TRead>`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.channels.channel-2?view=net-10.0)**<br>*abstract class* | Provides a base class for channels that support reading elements of type TRead and writing elements of type TWrite. | – | through its members | [source](https://source.dot.net/#System.Threading.Channels/System/Threading/Channels/Channel_2.cs,d080f319a347bae1) |
| [`Reader`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.channels.channel-2.reader?view=net-10.0#system-threading-channels-channel-2-reader)<br>*property* | Gets the readable half of this channel. | 1 | [Infrastructure/ImportBatchQueue.cs:19](src/Northstar.Infrastructure/Messaging/ImportBatchQueue.cs#L19) | [source](https://source.dot.net/#System.Threading.Channels/System/Threading/Channels/Channel_2.cs,66bcc21ad5f53c0a) |
| [`Writer`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.channels.channel-2.writer?view=net-10.0#system-threading-channels-channel-2-writer)<br>*property* | Gets the writable half of this channel. | 1 | [Infrastructure/ImportBatchQueue.cs:16](src/Northstar.Infrastructure/Messaging/ImportBatchQueue.cs#L16) | [source](https://source.dot.net/#System.Threading.Channels/System/Threading/Channels/Channel_2.cs,037a870d82701a38) |
| **[`ChannelOptions`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.channels.channeloptions?view=net-10.0)**<br>*abstract class* | Provides options that control the behavior of channel instances. | – | through its members | [source](https://source.dot.net/#System.Threading.Channels/System/Threading/Channels/ChannelOptions.cs,17ca88c7ce7f5c88) |
| [`SingleReader`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.channels.channeloptions.singlereader?view=net-10.0#system-threading-channels-channeloptions-singlereader)<br>*property* | true readers from the channel guarantee that there will only ever be at most one read operation at a time; false if no such constraint is guaranteed. | 1 | [Infrastructure/ImportBatchQueue.cs:11](src/Northstar.Infrastructure/Messaging/ImportBatchQueue.cs#L11) | [source](https://source.dot.net/#System.Threading.Channels/System/Threading/Channels/ChannelOptions.cs,d91f597d60c79403) |
| [`SingleWriter`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.channels.channeloptions.singlewriter?view=net-10.0#system-threading-channels-channeloptions-singlewriter)<br>*property* | true if writers to the channel guarantee that there will only ever be at most one write operation at a time; false if no such constraint is guaranteed. | 1 | [Infrastructure/ImportBatchQueue.cs:12](src/Northstar.Infrastructure/Messaging/ImportBatchQueue.cs#L12) | [source](https://source.dot.net/#System.Threading.Channels/System/Threading/Channels/ChannelOptions.cs,db0906d7c370909f) |
| **[`ChannelReader<T>`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.channels.channelreader-1?view=net-10.0)**<br>*abstract class* | Provides a base class for reading from a channel. | – | through its members | [source](https://source.dot.net/#System.Threading.Channels/System/Threading/Channels/ChannelReader.cs,62e469b9eff47d90) |
| [`ReadAllAsync(CancellationToken)`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.channels.channelreader-1.readallasync?view=net-10.0#system-threading-channels-channelreader-1-readallasync(system-threading-cancellationtoken))<br>*method* | Creates an IAsyncEnumerable that enables reading all of the data from the channel. | 1 | [Infrastructure/ImportBatchQueue.cs:19](src/Northstar.Infrastructure/Messaging/ImportBatchQueue.cs#L19) | [source](https://source.dot.net/#System.Threading.Channels/System/Threading/Channels/ChannelReader.cs,cba14ccd7fceae72) |
| **[`ChannelWriter<T>`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.channels.channelwriter-1?view=net-10.0)**<br>*abstract class* | Provides a base class for writing to a channel. | – | through its members | [source](https://source.dot.net/#System.Threading.Channels/System/Threading/Channels/ChannelWriter.cs,be49f7c14a71fa96) |
| [`WriteAsync(T, CancellationToken)`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.channels.channelwriter-1.writeasync?view=net-10.0#system-threading-channels-channelwriter-1-writeasync(-0-system-threading-cancellationtoken))<br>*method* | Asynchronously writes an item to the channel. | 1 | [Infrastructure/ImportBatchQueue.cs:16](src/Northstar.Infrastructure/Messaging/ImportBatchQueue.cs#L16) | [source](https://source.dot.net/#System.Threading.Channels/System/Threading/Channels/ChannelWriter.cs,b45ee6add1bfed52) |
| **[`UnboundedChannelOptions`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.channels.unboundedchanneloptions?view=net-10.0)**<br>*class* | Provides options that control the behavior of unbounded Channel instances. | 1 | [Infrastructure/ImportBatchQueue.cs:9](src/Northstar.Infrastructure/Messaging/ImportBatchQueue.cs#L9) | [source](https://source.dot.net/#System.Threading.Channels/System/Threading/Channels/ChannelOptions.cs,753e9e0cc4910825) |
| [`new UnboundedChannelOptions()`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.channels.unboundedchanneloptions.-ctor?view=net-10.0#system-threading-channels-unboundedchanneloptions-ctor)<br>*constructor* | Initializes a new instance of the UnboundedChannelOptions class. | 1 | [Infrastructure/ImportBatchQueue.cs:9](src/Northstar.Infrastructure/Messaging/ImportBatchQueue.cs#L9) | [source](https://source.dot.net/#System.Threading.Channels/System/Threading/Channels/ChannelOptions.cs,572e2ad6157e4b42) |

#### System.Threading.Tasks

*18 APIs · 338 uses · [namespace page on Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks?view=net-10.0)*

| API | What it does | Uses | Where | Source |
| --- | --- | ---: | --- | --- |
| **[`Task`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task?view=net-10.0)**<br>*class* | Represents an asynchronous operation. | 59 | [Application/IApplicationDbContext.cs:14](src/Northstar.Application/Abstractions/IApplicationDbContext.cs#L14)<br>[Application/ImportBatchProcessor.cs:16](src/Northstar.Application/Imports/ImportBatchProcessor.cs#L16)<br>+57 more | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Threading/Tasks/Task.cs,045a746eb48cbaa9) |
| [`Delay(TimeSpan)`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.delay?view=net-10.0#system-threading-tasks-task-delay(system-timespan))<br>*static method* | Creates a task that completes after a specified time interval. | 1 | [IntegrationTests/ImportBatchesApiTests.cs:395](tests/Northstar.Api.IntegrationTests/ImportBatchesApiTests.cs#L395) | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Threading/Tasks/Task.cs,87a671fb5b9b4384) |
| [`Delay(TimeSpan, CancellationToken)`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.delay?view=net-10.0#system-threading-tasks-task-delay(system-timespan-system-threading-cancellationtoken))<br>*static method* | Creates a cancellable task that completes after a specified time interval. | 5 | [Infrastructure/FakePayrollProviderClient.cs:146](src/Northstar.Infrastructure/Integrations/FakePayrollProviderClient.cs#L146)<br>[IntegrationTests/FakePayrollProviderClientTests.cs:108](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L108)<br>+3 more | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Threading/Tasks/Task.cs,b070ae859dd3f7b7) |
| [`FromException<TResult>(Exception)`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.fromexception?view=net-10.0#system-threading-tasks-task-fromexception-1(system-exception))<br>*static method* | Creates a Task that's completed with a specified exception. | 5 | [IntegrationTests/DeterministicPayrollProviderClient.cs:33](tests/Northstar.Api.IntegrationTests/DeterministicPayrollProviderClient.cs#L33)<br>[IntegrationTests/DeterministicPayrollProviderClient.cs:36](tests/Northstar.Api.IntegrationTests/DeterministicPayrollProviderClient.cs#L36)<br>+3 more | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Threading/Tasks/Task.cs,231dc5e596ff14d1) |
| [`FromResult<TResult>(TResult)`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.fromresult?view=net-10.0#system-threading-tasks-task-fromresult-1(-0))<br>*static method* | Creates a Task that's completed successfully with the specified result. | 9 | [IntegrationTests/DeterministicPayrollProviderClient.cs:50](tests/Northstar.Api.IntegrationTests/DeterministicPayrollProviderClient.cs#L50)<br>[IntegrationTests/FakePayrollProviderClientTests.cs:17](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L17)<br>+7 more | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Threading/Tasks/Task.cs,11a386e7d7cae64a) |
| [`GetAwaiter()`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.getawaiter?view=net-10.0#system-threading-tasks-task-getawaiter)<br>*method, called for you by `await`* | Gets an awaiter used to await this Task. | 23 | [Api/Program.cs:40](src/Northstar.Api/Program.cs#L40)<br>[Application/ImportBatchProcessor.cs:170](src/Northstar.Application/Imports/ImportBatchProcessor.cs#L170)<br>+21 more | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Threading/Tasks/Task.cs,9865ec4fb8abca74) |
| **[`Task<TResult>`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1?view=net-10.0)**<br>*class* | Represents an asynchronous operation that can return a value. | 45 | [Api/ImportBatchesController.cs:15](src/Northstar.Api/Controllers/ImportBatchesController.cs#L15)<br>[Api/PayrollRunsController.cs:14](src/Northstar.Api/Controllers/PayrollRunsController.cs#L14)<br>+43 more | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Threading/Tasks/Task_T.cs,5ca7b77f3df89fc6) |
| [`GetAwaiter()`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1.getawaiter?view=net-10.0#system-threading-tasks-task-1-getawaiter)<br>*method, called for you by `await`* | Gets an awaiter used to await this Task. | 177 | [Api/ImportBatchesController.cs:23](src/Northstar.Api/Controllers/ImportBatchesController.cs#L23)<br>[Api/PayrollRunsController.cs:22](src/Northstar.Api/Controllers/PayrollRunsController.cs#L22)<br>+175 more | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Threading/Tasks/Task_T.cs,e1d639f68b66715a) |
| **[`TaskCompletionSource`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.taskcompletionsource?view=net-10.0)**<br>*class* | Represents the producer side of a Task unbound to a delegate, providing access to the consumer side through the Task property. | 1 | [IntegrationTests/FakePayrollProviderClientTests.cs:104](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L104) | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Threading/Tasks/TaskCompletionSource.cs,2f19bceafd0067bd) |
| [`new TaskCompletionSource(TaskCreationOptions)`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.taskcompletionsource.-ctor?view=net-10.0#system-threading-tasks-taskcompletionsource-ctor(system-threading-tasks-taskcreationoptions))<br>*constructor* | Creates a TaskCompletionSource with the specified options. | 1 | [IntegrationTests/FakePayrollProviderClientTests.cs:104](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L104) | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Threading/Tasks/TaskCompletionSource.cs,43c6451fd81c9053) |
| [`Task`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.taskcompletionsource.task?view=net-10.0#system-threading-tasks-taskcompletionsource-task)<br>*property* | Gets the Task created by this TaskCompletionSource. | 1 | [IntegrationTests/FakePayrollProviderClientTests.cs:116](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L116) | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Threading/Tasks/TaskCompletionSource.cs,6c601052e8fa6d92) |
| [`SetResult()`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.taskcompletionsource.setresult?view=net-10.0#system-threading-tasks-taskcompletionsource-setresult)<br>*method* | Transitions the underlying Task into the TaskStatus.RanToCompletion state. | 1 | [IntegrationTests/FakePayrollProviderClientTests.cs:107](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L107) | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Threading/Tasks/TaskCompletionSource.cs,765b4fc86685a356) |
| **[`TaskCreationOptions`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.taskcreationoptions?view=net-10.0)**<br>*enum* | Specifies flags that control optional behavior for the creation and execution of tasks. | 1 | [IntegrationTests/FakePayrollProviderClientTests.cs:104](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L104) | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Threading/Tasks/Task.cs,17b17e17ae1468b0) |
| [`RunContinuationsAsynchronously`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.taskcreationoptions?view=net-10.0)<br>*enum member* | Forces continuations added to the current task to be executed asynchronously. | 1 | [IntegrationTests/FakePayrollProviderClientTests.cs:104](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L104) | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Threading/Tasks/Task.cs,8a4ba0b9ea2b26f8) |
| **[`ValueTask`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.valuetask?view=net-10.0)**<br>*struct* | Provides an awaitable result of an asynchronous operation. | 3 | [Application/IImportBatchQueue.cs:5](src/Northstar.Application/Abstractions/IImportBatchQueue.cs#L5)<br>[Infrastructure/ImportBatchQueue.cs:15](src/Northstar.Infrastructure/Messaging/ImportBatchQueue.cs#L15)<br>+1 more | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Threading/Tasks/ValueTask.cs,001d610aba2df461) |
| [`GetAwaiter()`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.valuetask.getawaiter?view=net-10.0#system-threading-tasks-valuetask-getawaiter)<br>*method, called for you by `await`* | Creates an awaiter for this value. | 3 | [Application/ImportBatchService.cs:190](src/Northstar.Application/Imports/ImportBatchService.cs#L190)<br>[Infrastructure/ImportBatchWorker.cs:86](src/Northstar.Infrastructure/Messaging/ImportBatchWorker.cs#L86)<br>+1 more | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Threading/Tasks/ValueTask.cs,e1e6145b38914784) |
| **[`ValueTask<TResult>`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.valuetask-1?view=net-10.0)**<br>*struct* | Provides a value type that wraps a Task and a TResult, only one of which is used. | 1 | [IntegrationTests/FakePayrollProviderClientTests.cs:273](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L273) | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Threading/Tasks/ValueTask.cs,77a292425839ae85) |
| [`GetAwaiter()`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.valuetask-1.getawaiter?view=net-10.0#system-threading-tasks-valuetask-1-getawaiter)<br>*method, called for you by `await`* | Creates an awaiter for this value. | 1 | [Infrastructure/ImportBatchWorker.cs:98](src/Northstar.Infrastructure/Messaging/ImportBatchWorker.cs#L98) | [source](https://source.dot.net/#System.Private.CoreLib/src/runtime/src/libraries/System.Private.CoreLib/src/System/Threading/Tasks/ValueTask.cs,7d93bac6dfbdd767) |

### ASP.NET Core

*6 namespaces, 104 APIs.*

#### Microsoft.AspNetCore.Builder

*24 APIs · 32 uses*

| API | What it does | Uses | Where | Source |
| --- | --- | ---: | --- | --- |
| **[`AuthorizationAppBuilderExtensions`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.builder.authorizationappbuilderextensions?view=aspnetcore-10.0)**<br>*static class* | Extension methods to add authorization capabilities to an HTTP application pipeline. | – | through its members | [source](https://source.dot.net/#Microsoft.AspNetCore.Authorization.Policy/AuthorizationAppBuilderExtensions.cs,03de6488378d58e7) |
| [`UseAuthorization(this IApplicationBuilder)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.builder.authorizationappbuilderextensions.useauthorization?view=aspnetcore-10.0#microsoft-aspnetcore-builder-authorizationappbuilderextensions-useauthorization(microsoft-aspnetcore-builder-iapplicationbuilder))<br>*extension method* | Adds the AuthorizationMiddleware to the specified IApplicationBuilder, which enables authorization capabilities. | 1 | [Api/Program.cs:32](src/Northstar.Api/Program.cs#L32) | [source](https://source.dot.net/#Microsoft.AspNetCore.Authorization.Policy/AuthorizationAppBuilderExtensions.cs,a75da16ff8887012) |
| **[`ControllerEndpointRouteBuilderExtensions`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.builder.controllerendpointroutebuilderextensions?view=aspnetcore-10.0)**<br>*static class* | Contains extension methods for using Controllers with IEndpointRouteBuilder. | – | through its members | [source](https://source.dot.net/#Microsoft.AspNetCore.Mvc.Core/Builder/ControllerEndpointRouteBuilderExtensions.cs,dffa439589eaade7) |
| [`MapControllers(this IEndpointRouteBuilder)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.builder.controllerendpointroutebuilderextensions.mapcontrollers?view=aspnetcore-10.0#microsoft-aspnetcore-builder-controllerendpointroutebuilderextensions-mapcontrollers(microsoft-aspnetcore-routing-iendpointroutebuilder))<br>*extension method* | Adds endpoints for controller actions to the IEndpointRouteBuilder without specifying any routes. | 1 | [Api/Program.cs:35](src/Northstar.Api/Program.cs#L35) | [source](https://source.dot.net/#Microsoft.AspNetCore.Mvc.Core/Builder/ControllerEndpointRouteBuilderExtensions.cs,368649f855f47a38) |
| **[`CorsMiddlewareExtensions`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.builder.corsmiddlewareextensions?view=aspnetcore-10.0)**<br>*static class* | The IApplicationBuilder extensions for adding CORS middleware support. | – | through its members | [source](https://source.dot.net/#Microsoft.AspNetCore.Cors/Infrastructure/CorsMiddlewareExtensions.cs,b80fd5b290bb0565) |
| [`UseCors(this IApplicationBuilder, string)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.builder.corsmiddlewareextensions.usecors?view=aspnetcore-10.0#microsoft-aspnetcore-builder-corsmiddlewareextensions-usecors(microsoft-aspnetcore-builder-iapplicationbuilder-system-string))<br>*extension method* | Adds a CORS middleware to your web application pipeline to allow cross domain requests. | 1 | [Api/Program.cs:28](src/Northstar.Api/Program.cs#L28) | [source](https://source.dot.net/#Microsoft.AspNetCore.Cors/Infrastructure/CorsMiddlewareExtensions.cs,0239b6bfe85ffc4b) |
| **[`EndpointRouteBuilderExtensions`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.builder.endpointroutebuilderextensions?view=aspnetcore-10.0)**<br>*static class* | Provides extension methods for IEndpointRouteBuilder to add endpoints. | – | through its members | [source](https://source.dot.net/#Microsoft.AspNetCore.Routing/Builder/EndpointRouteBuilderExtensions.cs,58e6d807ba9ad507) |
| [`MapGet(this IEndpointRouteBuilder, string, Delegate)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.builder.endpointroutebuilderextensions.mapget?view=aspnetcore-10.0#microsoft-aspnetcore-builder-endpointroutebuilderextensions-mapget(microsoft-aspnetcore-routing-iendpointroutebuilder-system-string-system-delegate))<br>*extension method* | Adds a RouteEndpoint to the IEndpointRouteBuilder that matches HTTP GET requests for the specified pattern. | 3 | [Api/Program.cs:34](src/Northstar.Api/Program.cs#L34)<br>[FakeProvider/Program.cs:14](tools/Northstar.FakePayrollProvider/Program.cs#L14)<br>+1 more | [source](https://source.dot.net/#Microsoft.AspNetCore.Routing/Builder/EndpointRouteBuilderExtensions.cs,8f371bb108052f8b) |
| **[`ExceptionHandlerExtensions`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.builder.exceptionhandlerextensions?view=aspnetcore-10.0)**<br>*static class* | Extension methods for enabling ExceptionHandlerExtensions. | – | through its members | [source](https://source.dot.net/#Microsoft.AspNetCore.Diagnostics/ExceptionHandler/ExceptionHandlerExtensions.cs,2e31f801e86aeb54) |
| [`UseExceptionHandler(this IApplicationBuilder)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.builder.exceptionhandlerextensions.useexceptionhandler?view=aspnetcore-10.0#microsoft-aspnetcore-builder-exceptionhandlerextensions-useexceptionhandler(microsoft-aspnetcore-builder-iapplicationbuilder))<br>*extension method* | Adds a middleware to the pipeline that will catch exceptions, log them, and re-execute the request in an alternate pipeline. | 2 | [Api/Program.cs:31](src/Northstar.Api/Program.cs#L31)<br>[FakeProvider/Program.cs:12](tools/Northstar.FakePayrollProvider/Program.cs#L12) | [source](https://source.dot.net/#Microsoft.AspNetCore.Diagnostics/ExceptionHandler/ExceptionHandlerExtensions.cs,851388d0ab63af50) |
| **[`OpenApiEndpointRouteBuilderExtensions`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.builder.openapiendpointroutebuilderextensions?view=aspnetcore-10.0)**<br>*static class* | OpenAPI-related methods for IEndpointRouteBuilder. | – | through its members | [source](https://source.dot.net/#Microsoft.AspNetCore.OpenApi/Extensions/OpenApiEndpointRouteBuilderExtensions.cs,3f0a7915af892fea) |
| [`MapOpenApi(this IEndpointRouteBuilder, string)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.builder.openapiendpointroutebuilderextensions.mapopenapi?view=aspnetcore-10.0#microsoft-aspnetcore-builder-openapiendpointroutebuilderextensions-mapopenapi(microsoft-aspnetcore-routing-iendpointroutebuilder-system-string))<br>*extension method* | Register an endpoint onto the current application for resolving the OpenAPI document associated with the current application. | 1 | [Api/Program.cs:27](src/Northstar.Api/Program.cs#L27) | [source](https://source.dot.net/#Microsoft.AspNetCore.OpenApi/Extensions/OpenApiEndpointRouteBuilderExtensions.cs,da6124cfb6e2f8d8) |
| **[`RoutingEndpointConventionBuilderExtensions`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.builder.routingendpointconventionbuilderextensions?view=aspnetcore-10.0)**<br>*static class* | Extension methods for adding routing metadata to endpoint instances using IEndpointConventionBuilder. | – | through its members | [source](https://source.dot.net/#Microsoft.AspNetCore.Routing/Builder/RoutingEndpointConventionBuilderExtensions.cs,f7a0031f4466ba7c) |
| [`WithName<TBuilder>(this TBuilder, string)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.builder.routingendpointconventionbuilderextensions.withname?view=aspnetcore-10.0#microsoft-aspnetcore-builder-routingendpointconventionbuilderextensions-withname-1(-0-system-string))<br>*extension method* | Adds the IEndpointNameMetadata to the Metadata collection for all endpoints produced on the target IEndpointConventionBuilder given the endpointName. | 1 | [FakeProvider/Program.cs:84](tools/Northstar.FakePayrollProvider/Program.cs#L84) | [source](https://source.dot.net/#Microsoft.AspNetCore.Routing/Builder/RoutingEndpointConventionBuilderExtensions.cs,0cb24624e1ce569f) |
| **[`WebApplication`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.builder.webapplication?view=aspnetcore-10.0)**<br>*class* | The web application used to configure the HTTP pipeline, and routes. | 2 | [Api/Program.cs:6](src/Northstar.Api/Program.cs#L6)<br>[FakeProvider/Program.cs:5](tools/Northstar.FakePayrollProvider/Program.cs#L5) | [source](https://source.dot.net/#Microsoft.AspNetCore/WebApplication.cs,e41b5d12c49f9700) |
| [`Configuration`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.builder.webapplication.configuration?view=aspnetcore-10.0#microsoft-aspnetcore-builder-webapplication-configuration)<br>*property* | The application's configured IConfiguration. | 1 | [Api/Program.cs:37](src/Northstar.Api/Program.cs#L37) | [source](https://source.dot.net/#Microsoft.AspNetCore/WebApplication.cs,6ef36c1bba2f7d4c) |
| [`Environment`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.builder.webapplication.environment?view=aspnetcore-10.0#microsoft-aspnetcore-builder-webapplication-environment)<br>*property* | The application's configured IWebHostEnvironment. | 1 | [Api/Program.cs:25](src/Northstar.Api/Program.cs#L25) | [source](https://source.dot.net/#Microsoft.AspNetCore/WebApplication.cs,47d34ad24ec84fc2) |
| [`Services`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.builder.webapplication.services?view=aspnetcore-10.0#microsoft-aspnetcore-builder-webapplication-services)<br>*property* | The application's configured services. | 1 | [Api/Program.cs:39](src/Northstar.Api/Program.cs#L39) | [source](https://source.dot.net/#Microsoft.AspNetCore/WebApplication.cs,45c2e44cb3d510b1) |
| [`CreateBuilder(string[])`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.builder.webapplication.createbuilder?view=aspnetcore-10.0#microsoft-aspnetcore-builder-webapplication-createbuilder(system-string()))<br>*static method* | Initializes a new instance of the WebApplicationBuilder class with preconfigured defaults. | 2 | [Api/Program.cs:6](src/Northstar.Api/Program.cs#L6)<br>[FakeProvider/Program.cs:5](tools/Northstar.FakePayrollProvider/Program.cs#L5) | [source](https://source.dot.net/#Microsoft.AspNetCore/WebApplication.cs,77d59c0a99dedcf9) |
| [`Run(string)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.builder.webapplication.run?view=aspnetcore-10.0#microsoft-aspnetcore-builder-webapplication-run(system-string))<br>*method* | Runs an application and blocks the calling thread until host shutdown. | 2 | [Api/Program.cs:44](src/Northstar.Api/Program.cs#L44)<br>[FakeProvider/Program.cs:89](tools/Northstar.FakePayrollProvider/Program.cs#L89) | [source](https://source.dot.net/#Microsoft.AspNetCore/WebApplication.cs,a01dc35d221adf28) |
| **[`WebApplicationBuilder`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.builder.webapplicationbuilder?view=aspnetcore-10.0)**<br>*class* | A builder for web applications and services. | – | through its members | [source](https://source.dot.net/#Microsoft.AspNetCore/WebApplicationBuilder.cs,25a352b50e81d95b) |
| [`Configuration`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.builder.webapplicationbuilder.configuration?view=aspnetcore-10.0#microsoft-aspnetcore-builder-webapplicationbuilder-configuration)<br>*property* | A collection of configuration providers for the application to compose. | 1 | [Api/Program.cs:15](src/Northstar.Api/Program.cs#L15) | [source](https://source.dot.net/#Microsoft.AspNetCore/WebApplicationBuilder.cs,23e9f8dfac9a4b41) |
| [`Services`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.builder.webapplicationbuilder.services?view=aspnetcore-10.0#microsoft-aspnetcore-builder-webapplicationbuilder-services)<br>*property* | A collection of services for the application to compose. | 10 | [Api/Program.cs:8](src/Northstar.Api/Program.cs#L8)<br>[FakeProvider/Program.cs:7](tools/Northstar.FakePayrollProvider/Program.cs#L7)<br>+8 more | [source](https://source.dot.net/#Microsoft.AspNetCore/WebApplicationBuilder.cs,e7079b57fa3f5590) |
| [`Build()`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.builder.webapplicationbuilder.build?view=aspnetcore-10.0#microsoft-aspnetcore-builder-webapplicationbuilder-build)<br>*method* | Builds the WebApplication. | 2 | [Api/Program.cs:23](src/Northstar.Api/Program.cs#L23)<br>[FakeProvider/Program.cs:10](tools/Northstar.FakePayrollProvider/Program.cs#L10) | [source](https://source.dot.net/#Microsoft.AspNetCore/WebApplicationBuilder.cs,06eb5ff86e015ba3) |

#### Microsoft.AspNetCore.Cors.Infrastructure

*6 APIs · 4 uses · [namespace page on Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.cors.infrastructure?view=aspnetcore-10.0)*

| API | What it does | Uses | Where | Source |
| --- | --- | ---: | --- | --- |
| **[`CorsOptions`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.cors.infrastructure.corsoptions?view=aspnetcore-10.0)**<br>*class* | Provides programmatic configuration for Cors. | – | through its members | [source](https://source.dot.net/#Microsoft.AspNetCore.Cors/Infrastructure/CorsOptions.cs,5ac862897f028859) |
| [`AddPolicy(string, Action<CorsPolicyBuilder>)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.cors.infrastructure.corsoptions.addpolicy?view=aspnetcore-10.0#microsoft-aspnetcore-cors-infrastructure-corsoptions-addpolicy(system-string-system-action((microsoft-aspnetcore-cors-infrastructure-corspolicybuilder))))<br>*method* | Adds a new policy. | 1 | [Api/Program.cs:18](src/Northstar.Api/Program.cs#L18) | [source](https://source.dot.net/#Microsoft.AspNetCore.Cors/Infrastructure/CorsOptions.cs,5e6c2db2eada0499) |
| **[`CorsPolicyBuilder`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.cors.infrastructure.corspolicybuilder?view=aspnetcore-10.0)**<br>*class* | Exposes methods to build a policy. | – | through its members | [source](https://source.dot.net/#Microsoft.AspNetCore.Cors/Infrastructure/CorsPolicyBuilder.cs,50cd615005fd8db7) |
| [`AllowAnyHeader()`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.cors.infrastructure.corspolicybuilder.allowanyheader?view=aspnetcore-10.0#microsoft-aspnetcore-cors-infrastructure-corspolicybuilder-allowanyheader)<br>*method* | Ensures that the policy allows any header. | 1 | [Api/Program.cs:20](src/Northstar.Api/Program.cs#L20) | [source](https://source.dot.net/#Microsoft.AspNetCore.Cors/Infrastructure/CorsPolicyBuilder.cs,83b3fdeec532a39e) |
| [`AllowAnyMethod()`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.cors.infrastructure.corspolicybuilder.allowanymethod?view=aspnetcore-10.0#microsoft-aspnetcore-cors-infrastructure-corspolicybuilder-allowanymethod)<br>*method* | Ensures that the policy allows any method. | 1 | [Api/Program.cs:21](src/Northstar.Api/Program.cs#L21) | [source](https://source.dot.net/#Microsoft.AspNetCore.Cors/Infrastructure/CorsPolicyBuilder.cs,2a3ed4c716729293) |
| [`WithOrigins(params string[])`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.cors.infrastructure.corspolicybuilder.withorigins?view=aspnetcore-10.0#microsoft-aspnetcore-cors-infrastructure-corspolicybuilder-withorigins(system-string()))<br>*method* | Adds the specified origins to the policy. | 1 | [Api/Program.cs:19](src/Northstar.Api/Program.cs#L19) | [source](https://source.dot.net/#Microsoft.AspNetCore.Cors/Infrastructure/CorsPolicyBuilder.cs,d58b34553e290c98) |

#### Microsoft.AspNetCore.Hosting

*5 APIs · 4 uses · [namespace page on Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.hosting?view=aspnetcore-10.0)*

| API | What it does | Uses | Where | Source |
| --- | --- | ---: | --- | --- |
| **[`HostingAbstractionsWebHostBuilderExtensions`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.hosting.hostingabstractionswebhostbuilderextensions?view=aspnetcore-10.0)**<br>*static class* | Contains extension methods for configuring the IWebHostBuilder. | – | through its members | [source](https://source.dot.net/#Microsoft.AspNetCore.Hosting.Abstractions/HostingAbstractionsWebHostBuilderExtensions.cs,bfb27559fab1d5b1) |
| [`UseEnvironment(this IWebHostBuilder, string)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.hosting.hostingabstractionswebhostbuilderextensions.useenvironment?view=aspnetcore-10.0#microsoft-aspnetcore-hosting-hostingabstractionswebhostbuilderextensions-useenvironment(microsoft-aspnetcore-hosting-iwebhostbuilder-system-string))<br>*extension method* | Specify the environment to be used by the web host. | 1 | [IntegrationTests/CustomWebApplicationFactory.cs:29](tests/Northstar.Api.IntegrationTests/CustomWebApplicationFactory.cs#L29) | [source](https://source.dot.net/#Microsoft.AspNetCore.Hosting.Abstractions/HostingAbstractionsWebHostBuilderExtensions.cs,6e3203c8a79629d4) |
| **[`IWebHostBuilder`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.hosting.iwebhostbuilder?view=aspnetcore-10.0)**<br>*interface* | A builder for IWebHost. | 1 | [IntegrationTests/CustomWebApplicationFactory.cs:27](tests/Northstar.Api.IntegrationTests/CustomWebApplicationFactory.cs#L27) | [source](https://source.dot.net/#Microsoft.AspNetCore.Hosting.Abstractions/IWebHostBuilder.cs,5f2a269aacb82de6) |
| [`ConfigureAppConfiguration(Action<WebHostBuilderContext, IConfigurationBuilder>)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.hosting.iwebhostbuilder.configureappconfiguration?view=aspnetcore-10.0#microsoft-aspnetcore-hosting-iwebhostbuilder-configureappconfiguration(system-action((microsoft-aspnetcore-hosting-webhostbuildercontext-microsoft-extensions-configuration-iconfigurationbuilder))))<br>*method* | Adds a delegate for configuring the IConfigurationBuilder that will construct an IConfiguration. | 1 | [IntegrationTests/CustomWebApplicationFactory.cs:30](tests/Northstar.Api.IntegrationTests/CustomWebApplicationFactory.cs#L30) | [source](https://source.dot.net/#Microsoft.AspNetCore.Hosting.Abstractions/IWebHostBuilder.cs,b6315eadb55d2e56) |
| [`ConfigureServices(Action<IServiceCollection>)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.hosting.iwebhostbuilder.configureservices?view=aspnetcore-10.0#microsoft-aspnetcore-hosting-iwebhostbuilder-configureservices(system-action((microsoft-extensions-dependencyinjection-iservicecollection))))<br>*method* | Adds a delegate for configuring additional services for the host or web application. | 1 | [IntegrationTests/CustomWebApplicationFactory.cs:37](tests/Northstar.Api.IntegrationTests/CustomWebApplicationFactory.cs#L37) | [source](https://source.dot.net/#Microsoft.AspNetCore.Hosting.Abstractions/IWebHostBuilder.cs,393a9b5bb0f62853) |

#### Microsoft.AspNetCore.Http

*35 APIs · 132 uses · [namespace page on Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http?view=aspnetcore-10.0)*

| API | What it does | Uses | Where | Source |
| --- | --- | ---: | --- | --- |
| **[`HttpContext`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.httpcontext?view=aspnetcore-10.0)**<br>*abstract class* | Encapsulates all HTTP-specific information about an individual HTTP request. | 2 | [FakeProvider/Program.cs:18](tools/Northstar.FakePayrollProvider/Program.cs#L18)<br>[FakeProvider/Program.cs:110](tools/Northstar.FakePayrollProvider/Program.cs#L110) | [source](https://source.dot.net/#Microsoft.AspNetCore.Http.Abstractions/HttpContext.cs,9bde6e3833d169c1) |
| [`Request`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.httpcontext.request?view=aspnetcore-10.0#microsoft-aspnetcore-http-httpcontext-request)<br>*property* | Gets the HttpRequest object for this request. | 3 | [Api/ApiControllerBase.cs:48](src/Northstar.Api/Controllers/ApiControllerBase.cs#L48)<br>[Api/HeaderUserContext.cs:7](src/Northstar.Api/Security/HeaderUserContext.cs#L7)<br>+1 more | [source](https://source.dot.net/#Microsoft.AspNetCore.Http.Abstractions/HttpContext.cs,b287623353d5b85a) |
| [`RequestAborted`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.httpcontext.requestaborted?view=aspnetcore-10.0#microsoft-aspnetcore-http-httpcontext-requestaborted)<br>*property* | Notifies when the connection underlying this request is aborted and thus request operations should be cancelled. | 1 | [FakeProvider/Program.cs:45](tools/Northstar.FakePayrollProvider/Program.cs#L45) | [source](https://source.dot.net/#Microsoft.AspNetCore.Http.Abstractions/HttpContext.cs,476768f088a342e9) |
| [`Response`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.httpcontext.response?view=aspnetcore-10.0#microsoft-aspnetcore-http-httpcontext-response)<br>*property* | Gets the HttpResponse object for this request. | 1 | [FakeProvider/Program.cs:120](tools/Northstar.FakePayrollProvider/Program.cs#L120) | [source](https://source.dot.net/#Microsoft.AspNetCore.Http.Abstractions/HttpContext.cs,a2734c75f775944a) |
| **[`HttpRequest`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.httprequest?view=aspnetcore-10.0)**<br>*abstract class* | Represents the incoming side of an individual HTTP request. | – | through its members | [source](https://source.dot.net/#Microsoft.AspNetCore.Http.Abstractions/HttpRequest.cs,ea81be9b74317002) |
| [`Headers`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.httprequest.headers?view=aspnetcore-10.0#microsoft-aspnetcore-http-httprequest-headers)<br>*property* | Gets the request headers. | 2 | [Api/HeaderUserContext.cs:7](src/Northstar.Api/Security/HeaderUserContext.cs#L7)<br>[Api/HeaderUserContext.cs:12](src/Northstar.Api/Security/HeaderUserContext.cs#L12) | [source](https://source.dot.net/#Microsoft.AspNetCore.Http.Abstractions/HttpRequest.cs,d0fbd133dc43af91) |
| [`Path`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.httprequest.path?view=aspnetcore-10.0#microsoft-aspnetcore-http-httprequest-path)<br>*property* | Gets or sets the portion of the request path that identifies the requested resource. | 1 | [Api/ApiControllerBase.cs:48](src/Northstar.Api/Controllers/ApiControllerBase.cs#L48) | [source](https://source.dot.net/#Microsoft.AspNetCore.Http.Abstractions/HttpRequest.cs,cdf34d4812b83f42) |
| **[`HttpResponse`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.httpresponse?view=aspnetcore-10.0)**<br>*abstract class* | Represents the outgoing side of an individual HTTP request. | – | through its members | [source](https://source.dot.net/#Microsoft.AspNetCore.Http.Abstractions/HttpResponse.cs,7642421540ea6ef2) |
| [`Headers`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.httpresponse.headers?view=aspnetcore-10.0#microsoft-aspnetcore-http-httpresponse-headers)<br>*property* | Gets the response headers. | 1 | [FakeProvider/Program.cs:120](tools/Northstar.FakePayrollProvider/Program.cs#L120) | [source](https://source.dot.net/#Microsoft.AspNetCore.Http.Abstractions/HttpResponse.cs,46a68eae152970c4) |
| **[`IHeaderDictionary`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.iheaderdictionary?view=aspnetcore-10.0)**<br>*interface* | Represents HttpRequest and HttpResponse headers | – | through its members | [source](https://source.dot.net/#Microsoft.AspNetCore.Http.Features/IHeaderDictionary.cs,0b9d30cbbb1c8cbf) |
| [`RetryAfter`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.iheaderdictionary.retryafter?view=aspnetcore-10.0#microsoft-aspnetcore-http-iheaderdictionary-retryafter)<br>*property* | Gets or sets the Retry-After HTTP header. | 1 | [FakeProvider/Program.cs:120](tools/Northstar.FakePayrollProvider/Program.cs#L120) | [source](https://source.dot.net/#Microsoft.AspNetCore.Http.Features/IHeaderDictionary.Keyed.cs,70b7d7d56c321bc9) |
| [`this[string]`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.iheaderdictionary.item?view=aspnetcore-10.0#microsoft-aspnetcore-http-iheaderdictionary-item(system-string))<br>*indexer* | IHeaderDictionary has a different indexer contract than IDictionary, where it will return StringValues.Empty for missing entries. | 2 | [Api/HeaderUserContext.cs:7](src/Northstar.Api/Security/HeaderUserContext.cs#L7)<br>[Api/HeaderUserContext.cs:12](src/Northstar.Api/Security/HeaderUserContext.cs#L12) | [source](https://source.dot.net/#Microsoft.AspNetCore.Http.Features/IHeaderDictionary.cs,04117162bc87eadb) |
| **[`IHttpContextAccessor`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.ihttpcontextaccessor?view=aspnetcore-10.0)**<br>*interface* | Provides access to the current IHttpContextAccessor.HttpContext, if one is available. | 1 | [Api/HeaderUserContext.cs:3](src/Northstar.Api/Security/HeaderUserContext.cs#L3) | [source](https://source.dot.net/#Microsoft.AspNetCore.Http.Abstractions/IHttpContextAccessor.cs,fe18ee6b17223921) |
| [`HttpContext`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.ihttpcontextaccessor.httpcontext?view=aspnetcore-10.0#microsoft-aspnetcore-http-ihttpcontextaccessor-httpcontext)<br>*property* | Gets or sets the current IHttpContextAccessor.HttpContext. | 2 | [Api/HeaderUserContext.cs:7](src/Northstar.Api/Security/HeaderUserContext.cs#L7)<br>[Api/HeaderUserContext.cs:12](src/Northstar.Api/Security/HeaderUserContext.cs#L12) | [source](https://source.dot.net/#Microsoft.AspNetCore.Http.Abstractions/IHttpContextAccessor.cs,3e9c2d9112bb9b89) |
| **[`IResult`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.iresult?view=aspnetcore-10.0)**<br>*interface* | Defines a contract that represents the result of an HTTP endpoint. | 2 | [FakeProvider/Program.cs:91](tools/Northstar.FakePayrollProvider/Program.cs#L91)<br>[FakeProvider/Program.cs:109](tools/Northstar.FakePayrollProvider/Program.cs#L109) | [source](https://source.dot.net/#Microsoft.AspNetCore.Http.Abstractions/HttpResults/IResult.cs,a98b52b37fb3344b) |
| **[`OpenApiRouteHandlerBuilderExtensions`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.openapiroutehandlerbuilderextensions?view=aspnetcore-10.0)**<br>*static class* | Extension methods for adding Endpoint.Metadata that is meant to be consumed by OpenAPI libraries. | – | through its members | [source](https://source.dot.net/#Microsoft.AspNetCore.Routing/Builder/OpenApiRouteHandlerBuilderExtensions.cs,85f36582f409f65c) |
| [`Produces<TResponse>(this RouteHandlerBuilder, int, string, params string[])`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.openapiroutehandlerbuilderextensions.produces?view=aspnetcore-10.0#microsoft-aspnetcore-http-openapiroutehandlerbuilderextensions-produces-1(microsoft-aspnetcore-builder-routehandlerbuilder-system-int32-system-string-system-string()))<br>*extension method* | Adds an IProducesResponseTypeMetadata to EndpointBuilder.Metadata for all endpoints produced by builder. | 1 | [FakeProvider/Program.cs:85](tools/Northstar.FakePayrollProvider/Program.cs#L85) | [source](https://source.dot.net/#Microsoft.AspNetCore.Routing/Builder/OpenApiRouteHandlerBuilderExtensions.cs,c470639d9c036b90) |
| [`ProducesProblem(this RouteHandlerBuilder, int, string)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.openapiroutehandlerbuilderextensions.producesproblem?view=aspnetcore-10.0#microsoft-aspnetcore-http-openapiroutehandlerbuilderextensions-producesproblem(microsoft-aspnetcore-builder-routehandlerbuilder-system-int32-system-string))<br>*extension method* | Adds an IProducesResponseTypeMetadata with a ProblemDetails type to EndpointBuilder.Metadata for all endpoints produced by builder. | 2 | [FakeProvider/Program.cs:86](tools/Northstar.FakePayrollProvider/Program.cs#L86)<br>[FakeProvider/Program.cs:87](tools/Northstar.FakePayrollProvider/Program.cs#L87) | [source](https://source.dot.net/#Microsoft.AspNetCore.Routing/Builder/OpenApiRouteHandlerBuilderExtensions.cs,a2cd688b1e11d2ec) |
| **[`PathString`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.pathstring?view=aspnetcore-10.0)**<br>*struct* | Provides correct escaping for Path and PathBase values when needed to reconstruct a request or redirect URI string | – | through its members | [source](https://source.dot.net/#Microsoft.AspNetCore.Http.Abstractions/PathString.cs,ff3b073b1591ea45) |
| [`implicit operator string(PathString)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.pathstring.op_implicit?view=aspnetcore-10.0#microsoft-aspnetcore-http-pathstring-op-implicit(microsoft-aspnetcore-http-pathstring)-system-string)<br>*conversion* | Implicitly calls ToString(). | 2 | [Api/ApiControllerBase.cs:48](src/Northstar.Api/Controllers/ApiControllerBase.cs#L48)<br>+1 more | [source](https://source.dot.net/#Microsoft.AspNetCore.Http.Abstractions/PathString.cs,e1bd120f7746f075) |
| **[`Results`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.results?view=aspnetcore-10.0)**<br>*static class* | A factory for IResult. | 14 | [Api/Program.cs:34](src/Northstar.Api/Program.cs#L34)<br>[FakeProvider/Program.cs:14](tools/Northstar.FakePayrollProvider/Program.cs#L14)<br>+12 more | [source](https://source.dot.net/#Microsoft.AspNetCore.Http.Results/Results.cs,916908aabae3e716) |
| [`Content(string, string, Encoding, int?)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.results.content?view=aspnetcore-10.0#microsoft-aspnetcore-http-results-content(system-string-system-string-system-text-encoding-system-nullable((system-int32))))<br>*static method* | Writes the content string to the HTTP response. | 1 | [FakeProvider/Program.cs:63](tools/Northstar.FakePayrollProvider/Program.cs#L63) | [source](https://source.dot.net/#Microsoft.AspNetCore.Http.Results/Results.cs,de94b7f3b3d3243e) |
| [`Ok<TValue>(TValue)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.results.ok?view=aspnetcore-10.0#microsoft-aspnetcore-http-results-ok-1(-0))<br>*static method* | Produces a StatusCodes.Status200OK response. | 7 | [Api/Program.cs:34](src/Northstar.Api/Program.cs#L34)<br>[FakeProvider/Program.cs:14](tools/Northstar.FakePayrollProvider/Program.cs#L14)<br>+5 more | [source](https://source.dot.net/#Microsoft.AspNetCore.Http.Results/Results.cs,5d4b383d5fd262f0) |
| [`Problem(string, string, int?, string, string, IEnumerable<KeyValuePair<string, object>>)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.results.problem?view=aspnetcore-10.0#microsoft-aspnetcore-http-results-problem(system-string-system-string-system-nullable((system-int32))-system-string-system-string-system-collections-generic-ienumerable((system-collections-generic-keyvaluepair((system-string-system-object))))))<br>*static method* | Produces a ProblemDetails response. | 6 | [FakeProvider/Program.cs:28](tools/Northstar.FakePayrollProvider/Program.cs#L28)<br>[FakeProvider/Program.cs:36](tools/Northstar.FakePayrollProvider/Program.cs#L36)<br>+4 more | [source](https://source.dot.net/#Microsoft.AspNetCore.Http.Results/Results.cs,3a80748c5d65654d) |
| **[`StatusCodes`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.statuscodes?view=aspnetcore-10.0)**<br>*static class* | A collection of constants for HTTP status codes. | 40 | [Api/ApiControllerBase.cs:20](src/Northstar.Api/Controllers/ApiControllerBase.cs#L20)<br>[Api/ImportBatchesController.cs:14](src/Northstar.Api/Controllers/ImportBatchesController.cs#L14)<br>+38 more | [source](https://source.dot.net/#Microsoft.AspNetCore.Http.Abstractions/StatusCodes.cs,37ecc92968c68b9b) |
| [`Status200OK`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.statuscodes.status200ok?view=aspnetcore-10.0#microsoft-aspnetcore-http-statuscodes-status200ok)<br>*static field* | HTTP status code 200. | 9 | [Api/ImportBatchesController.cs:14](src/Northstar.Api/Controllers/ImportBatchesController.cs#L14)<br>[Api/PayrollRunsController.cs:13](src/Northstar.Api/Controllers/PayrollRunsController.cs#L13)<br>+7 more | [source](https://source.dot.net/#Microsoft.AspNetCore.Http.Abstractions/StatusCodes.cs,8d956cca040c3501) |
| [`Status201Created`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.statuscodes.status201created?view=aspnetcore-10.0#microsoft-aspnetcore-http-statuscodes-status201created)<br>*static field* | HTTP status code 201. | 1 | [Api/WageRatesController.cs:26](src/Northstar.Api/Controllers/WageRatesController.cs#L26) | [source](https://source.dot.net/#Microsoft.AspNetCore.Http.Abstractions/StatusCodes.cs,233845f6daf7dba3) |
| [`Status202Accepted`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.statuscodes.status202accepted?view=aspnetcore-10.0#microsoft-aspnetcore-http-statuscodes-status202accepted)<br>*static field* | HTTP status code 202. | 2 | [Api/ImportBatchesController.cs:63](src/Northstar.Api/Controllers/ImportBatchesController.cs#L63)<br>[Api/ImportBatchesController.cs:91](src/Northstar.Api/Controllers/ImportBatchesController.cs#L91) | [source](https://source.dot.net/#Microsoft.AspNetCore.Http.Abstractions/StatusCodes.cs,d786860b3890a0ef) |
| [`Status400BadRequest`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.statuscodes.status400badrequest?view=aspnetcore-10.0#microsoft-aspnetcore-http-statuscodes-status400badrequest)<br>*static field* | HTTP status code 400. | 6 | [FakeProvider/Program.cs:31](tools/Northstar.FakePayrollProvider/Program.cs#L31)<br>[FakeProvider/Program.cs:39](tools/Northstar.FakePayrollProvider/Program.cs#L39)<br>+4 more | [source](https://source.dot.net/#Microsoft.AspNetCore.Http.Abstractions/StatusCodes.cs,e4f4a98fd889bf03) |
| [`Status401Unauthorized`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.statuscodes.status401unauthorized?view=aspnetcore-10.0#microsoft-aspnetcore-http-statuscodes-status401unauthorized)<br>*static field* | HTTP status code 401. | 1 | [Api/ApiControllerBase.cs:35](src/Northstar.Api/Controllers/ApiControllerBase.cs#L35) | [source](https://source.dot.net/#Microsoft.AspNetCore.Http.Abstractions/StatusCodes.cs,9073e086c6870dad) |
| [`Status403Forbidden`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.statuscodes.status403forbidden?view=aspnetcore-10.0#microsoft-aspnetcore-http-statuscodes-status403forbidden)<br>*static field* | HTTP status code 403. | 5 | [Api/ApiControllerBase.cs:30](src/Northstar.Api/Controllers/ApiControllerBase.cs#L30)<br>[Api/ImportBatchesController.cs:64](src/Northstar.Api/Controllers/ImportBatchesController.cs#L64)<br>+3 more | [source](https://source.dot.net/#Microsoft.AspNetCore.Http.Abstractions/StatusCodes.cs,696ea30c3a839a88) |
| [`Status404NotFound`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.statuscodes.status404notfound?view=aspnetcore-10.0#microsoft-aspnetcore-http-statuscodes-status404notfound)<br>*static field* | HTTP status code 404. | 4 | [Api/ApiControllerBase.cs:20](src/Northstar.Api/Controllers/ApiControllerBase.cs#L20)<br>[Api/ImportBatchesController.cs:28](src/Northstar.Api/Controllers/ImportBatchesController.cs#L28)<br>+2 more | [source](https://source.dot.net/#Microsoft.AspNetCore.Http.Abstractions/StatusCodes.cs,d67b9672bafc54d2) |
| [`Status409Conflict`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.statuscodes.status409conflict?view=aspnetcore-10.0#microsoft-aspnetcore-http-statuscodes-status409conflict)<br>*static field* | HTTP status code 409. | 5 | [Api/ApiControllerBase.cs:23](src/Northstar.Api/Controllers/ApiControllerBase.cs#L23)<br>[Api/ImportBatchesController.cs:65](src/Northstar.Api/Controllers/ImportBatchesController.cs#L65)<br>+3 more | [source](https://source.dot.net/#Microsoft.AspNetCore.Http.Abstractions/StatusCodes.cs,39f38b07344fe33b) |
| [`Status422UnprocessableEntity`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.statuscodes.status422unprocessableentity?view=aspnetcore-10.0#microsoft-aspnetcore-http-statuscodes-status422unprocessableentity)<br>*static field* | HTTP status code 422. | 5 | [Api/ApiControllerBase.cs:27](src/Northstar.Api/Controllers/ApiControllerBase.cs#L27)<br>[Api/ImportBatchesController.cs:66](src/Northstar.Api/Controllers/ImportBatchesController.cs#L66)<br>+3 more | [source](https://source.dot.net/#Microsoft.AspNetCore.Http.Abstractions/StatusCodes.cs,0ccd0fa5e59127b8) |
| [`Status429TooManyRequests`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.statuscodes.status429toomanyrequests?view=aspnetcore-10.0#microsoft-aspnetcore-http-statuscodes-status429toomanyrequests)<br>*static field* | HTTP status code 429. | 2 | [FakeProvider/Program.cs:87](tools/Northstar.FakePayrollProvider/Program.cs#L87)<br>[FakeProvider/Program.cs:125](tools/Northstar.FakePayrollProvider/Program.cs#L125) | [source](https://source.dot.net/#Microsoft.AspNetCore.Http.Abstractions/StatusCodes.cs,37dd77e0997d674e) |

#### Microsoft.AspNetCore.Mvc

*29 APIs · 118 uses · [namespace page on Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc?view=aspnetcore-10.0)*

| API | What it does | Uses | Where | Source |
| --- | --- | ---: | --- | --- |
| **[`ActionResult`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.actionresult?view=aspnetcore-10.0)**<br>*abstract class* | A default implementation of IActionResult. | 2 | [Api/ApiControllerBase.cs:34](src/Northstar.Api/Controllers/ApiControllerBase.cs#L34)<br>[Api/ApiControllerBase.cs:39](src/Northstar.Api/Controllers/ApiControllerBase.cs#L39) | [source](https://source.dot.net/#Microsoft.AspNetCore.Mvc.Core/ActionResult.cs,5c2548bb3f42589d) |
| **[`ActionResult<TValue>`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.actionresult-1?view=aspnetcore-10.0)**<br>*class* | A type that wraps either an TValue instance or an ActionResult. | 13 | [Api/ApiControllerBase.cs:15](src/Northstar.Api/Controllers/ApiControllerBase.cs#L15)<br>[Api/ImportBatchesController.cs:15](src/Northstar.Api/Controllers/ImportBatchesController.cs#L15)<br>+11 more | [source](https://source.dot.net/#Microsoft.AspNetCore.Mvc.Core/ActionResultOfT.cs,13730549baef2e0e) |
| [`implicit operator ActionResult<TValue>(ActionResult)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.actionresult-1.op_implicit?view=aspnetcore-10.0#microsoft-aspnetcore-mvc-actionresult-1-op-implicit(microsoft-aspnetcore-mvc-actionresult)-microsoft-aspnetcore-mvc-actionresult((-0)))<br>*conversion* | Implicitly converts the specified result to an ActionResult. | 25 | [Api/ApiControllerBase.cs:18](src/Northstar.Api/Controllers/ApiControllerBase.cs#L18)<br>[Api/ImportBatchesController.cs:20](src/Northstar.Api/Controllers/ImportBatchesController.cs#L20)<br>+23 more | [source](https://source.dot.net/#Microsoft.AspNetCore.Mvc.Core/ActionResultOfT.cs,e94af514b1ae99d3) |
| **[`ApiControllerAttribute`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.apicontrollerattribute?view=aspnetcore-10.0)**<br>*class, used as an attribute* | Indicates that a type and all derived types are used to serve HTTP API responses. | 1 | [Api/ApiControllerBase.cs:7](src/Northstar.Api/Controllers/ApiControllerBase.cs#L7) | [source](https://source.dot.net/#Microsoft.AspNetCore.Mvc.Core/ApiControllerAttribute.cs,01927e0ecdfbc759) |
| **[`ControllerBase`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.controllerbase?view=aspnetcore-10.0)**<br>*abstract class, used as a base type* | A base class for an MVC controller without view support. | 1 | [Api/ApiControllerBase.cs:8](src/Northstar.Api/Controllers/ApiControllerBase.cs#L8) | [source](https://source.dot.net/#Microsoft.AspNetCore.Mvc.Core/ControllerBase.cs,65999d3095ca52b1) |
| [`HttpContext`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.controllerbase.httpcontext?view=aspnetcore-10.0#microsoft-aspnetcore-mvc-controllerbase-httpcontext)<br>*property* | Gets the HttpContext for the executing action. | 1 | [Api/ApiControllerBase.cs:48](src/Northstar.Api/Controllers/ApiControllerBase.cs#L48) | [source](https://source.dot.net/#Microsoft.AspNetCore.Mvc.Core/ControllerBase.cs,4eaabea3a3fe1fdf) |
| [`AcceptedAtAction(string, object, object)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.controllerbase.acceptedataction?view=aspnetcore-10.0#microsoft-aspnetcore-mvc-controllerbase-acceptedataction(system-string-system-object-system-object))<br>*method* | Creates an AcceptedAtActionResult object that produces a StatusCodes.Status202Accepted response. | 1 | [Api/ImportBatchesController.cs:121](src/Northstar.Api/Controllers/ImportBatchesController.cs#L121) | [source](https://source.dot.net/#Microsoft.AspNetCore.Mvc.Core/ControllerBase.cs,82110bd357e53aae) |
| [`Conflict(object)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.controllerbase.conflict?view=aspnetcore-10.0#microsoft-aspnetcore-mvc-controllerbase-conflict(system-object))<br>*method* | Creates a ConflictObjectResult that produces a StatusCodes.Status409Conflict response. | 1 | [Api/ApiControllerBase.cs:22](src/Northstar.Api/Controllers/ApiControllerBase.cs#L22) | [source](https://source.dot.net/#Microsoft.AspNetCore.Mvc.Core/ControllerBase.cs,2ac117032f2e5644) |
| [`Created(string, object)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.controllerbase.created?view=aspnetcore-10.0#microsoft-aspnetcore-mvc-controllerbase-created(system-string-system-object))<br>*method* | Creates a CreatedResult object that produces a StatusCodes.Status201Created response. | 1 | [Api/WageRatesController.cs:48](src/Northstar.Api/Controllers/WageRatesController.cs#L48) | [source](https://source.dot.net/#Microsoft.AspNetCore.Mvc.Core/ControllerBase.cs,d19a2f6a492d6389) |
| [`NotFound(object)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.controllerbase.notfound?view=aspnetcore-10.0#microsoft-aspnetcore-mvc-controllerbase-notfound(system-object))<br>*method* | Creates a NotFoundObjectResult that produces a StatusCodes.Status404NotFound response. | 1 | [Api/ApiControllerBase.cs:19](src/Northstar.Api/Controllers/ApiControllerBase.cs#L19) | [source](https://source.dot.net/#Microsoft.AspNetCore.Mvc.Core/ControllerBase.cs,ee30b9e53f7677c8) |
| [`Ok(object)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.controllerbase.ok?view=aspnetcore-10.0#microsoft-aspnetcore-mvc-controllerbase-ok(system-object))<br>*method* | Creates an OkObjectResult object that produces a StatusCodes.Status200OK response. | 4 | [Api/ApiControllerBase.cs:18](src/Northstar.Api/Controllers/ApiControllerBase.cs#L18)<br>[Api/ImportBatchesController.cs:23](src/Northstar.Api/Controllers/ImportBatchesController.cs#L23)<br>+2 more | [source](https://source.dot.net/#Microsoft.AspNetCore.Mvc.Core/ControllerBase.cs,b38073baed6aeb21) |
| [`StatusCode(int)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.controllerbase.statuscode?view=aspnetcore-10.0#microsoft-aspnetcore-mvc-controllerbase-statuscode(system-int32))<br>*method* | Creates a StatusCodeResult object by specifying a statusCode. | 2 | [Api/ApiControllerBase.cs:30](src/Northstar.Api/Controllers/ApiControllerBase.cs#L30)<br>[Api/ApiControllerBase.cs:40](src/Northstar.Api/Controllers/ApiControllerBase.cs#L40) | [source](https://source.dot.net/#Microsoft.AspNetCore.Mvc.Core/ControllerBase.cs,432399de46955997) |
| [`Unauthorized(object)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.controllerbase.unauthorized?view=aspnetcore-10.0#microsoft-aspnetcore-mvc-controllerbase-unauthorized(system-object))<br>*method* | Creates an UnauthorizedObjectResult that produces a StatusCodes.Status401Unauthorized response. | 1 | [Api/ApiControllerBase.cs:34](src/Northstar.Api/Controllers/ApiControllerBase.cs#L34) | [source](https://source.dot.net/#Microsoft.AspNetCore.Mvc.Core/ControllerBase.cs,987fe25488c014cd) |
| [`UnprocessableEntity(object)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.controllerbase.unprocessableentity?view=aspnetcore-10.0#microsoft-aspnetcore-mvc-controllerbase-unprocessableentity(system-object))<br>*method* | Creates an UnprocessableEntityObjectResult that produces a StatusCodes.Status422UnprocessableEntity response. | 1 | [Api/ApiControllerBase.cs:26](src/Northstar.Api/Controllers/ApiControllerBase.cs#L26) | [source](https://source.dot.net/#Microsoft.AspNetCore.Mvc.Core/ControllerBase.cs,c61af14bb076f36f) |
| **[`FromHeaderAttribute`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.fromheaderattribute?view=aspnetcore-10.0)**<br>*class, used as an attribute* | Specifies that a parameter or property should be bound using the request headers. | 3 | [Api/ImportBatchesController.cs:69](src/Northstar.Api/Controllers/ImportBatchesController.cs#L69)<br>[Api/PayrollRunsController.cs:49](src/Northstar.Api/Controllers/PayrollRunsController.cs#L49)<br>+1 more | [source](https://source.dot.net/#Microsoft.AspNetCore.Mvc.Core/FromHeaderAttribute.cs,7a235fa8488ebaae) |
| [`Name`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.fromheaderattribute.name?view=aspnetcore-10.0#microsoft-aspnetcore-mvc-fromheaderattribute-name)<br>*property* | The name of the request header the parameter value is read from. † | 3 | [Api/ImportBatchesController.cs:69](src/Northstar.Api/Controllers/ImportBatchesController.cs#L69)<br>[Api/PayrollRunsController.cs:49](src/Northstar.Api/Controllers/PayrollRunsController.cs#L49)<br>+1 more | [source](https://source.dot.net/#Microsoft.AspNetCore.Mvc.Core/FromHeaderAttribute.cs,8f307c6f43aad393) |
| **[`HttpGetAttribute`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.httpgetattribute?view=aspnetcore-10.0)**<br>*class, used as an attribute* | Identifies an action that supports the HTTP GET method. | 6 | [Api/ImportBatchesController.cs:13](src/Northstar.Api/Controllers/ImportBatchesController.cs#L13)<br>[Api/PayrollRunsController.cs:12](src/Northstar.Api/Controllers/PayrollRunsController.cs#L12)<br>+4 more | [source](https://source.dot.net/#Microsoft.AspNetCore.Mvc.Core/HttpGetAttribute.cs,298fa20f1208ca01) |
| **[`HttpPostAttribute`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.httppostattribute?view=aspnetcore-10.0)**<br>*class, used as an attribute* | Identifies an action that supports the HTTP POST method. | 5 | [Api/ImportBatchesController.cs:62](src/Northstar.Api/Controllers/ImportBatchesController.cs#L62)<br>[Api/PayrollRunsController.cs:43](src/Northstar.Api/Controllers/PayrollRunsController.cs#L43)<br>+3 more | [source](https://source.dot.net/#Microsoft.AspNetCore.Mvc.Core/HttpPostAttribute.cs,021c8afec5da9ca8) |
| **[`JsonOptions`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.jsonoptions?view=aspnetcore-10.0)**<br>*class* | Options to configure SystemTextJsonInputFormatter and SystemTextJsonOutputFormatter. | – | through its members | [source](https://source.dot.net/#Microsoft.AspNetCore.Mvc.Core/JsonOptions.cs,5f1e07d8dd18cb5e) |
| [`JsonSerializerOptions`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.jsonoptions.jsonserializeroptions?view=aspnetcore-10.0#microsoft-aspnetcore-mvc-jsonoptions-jsonserializeroptions)<br>*property* | Gets the JsonSerializerOptions used by SystemTextJsonInputFormatter and SystemTextJsonOutputFormatter. | 1 | [Api/Program.cs:11](src/Northstar.Api/Program.cs#L11) | [source](https://source.dot.net/#Microsoft.AspNetCore.Mvc.Core/JsonOptions.cs,7e2ee51a4067e1f2) |
| **[`ProblemDetails`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.problemdetails?view=aspnetcore-10.0)**<br>*class* | A machine-readable format for specifying errors in HTTP API responses based on https://tools.ietf.org/html/rfc7807. | 12 | [Api/ApiControllerBase.cs:42](src/Northstar.Api/Controllers/ApiControllerBase.cs#L42)<br>[Api/ImportBatchesController.cs:28](src/Northstar.Api/Controllers/ImportBatchesController.cs#L28)<br>+10 more | [source](https://source.dot.net/#Microsoft.AspNetCore.Http.Abstractions/ProblemDetails/ProblemDetails.cs,9fba0244fad2ef1b) |
| [`new ProblemDetails()`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.problemdetails.-ctor?view=aspnetcore-10.0#microsoft-aspnetcore-mvc-problemdetails-ctor)<br>*constructor* | Creates an empty problem-details object; its properties are set afterwards. † | 1 | [Api/ApiControllerBase.cs:43](src/Northstar.Api/Controllers/ApiControllerBase.cs#L43) | [source](https://source.dot.net/#Microsoft.AspNetCore.Http.Abstractions/ProblemDetails/ProblemDetails.cs,147c2624823ef87d) |
| [`Detail`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.problemdetails.detail?view=aspnetcore-10.0#microsoft-aspnetcore-mvc-problemdetails-detail)<br>*property* | A human-readable explanation specific to this occurrence of the problem. | 1 | [Api/ApiControllerBase.cs:47](src/Northstar.Api/Controllers/ApiControllerBase.cs#L47) | [source](https://source.dot.net/#Microsoft.AspNetCore.Http.Abstractions/ProblemDetails/ProblemDetails.cs,66613a44ef0d5f22) |
| [`Instance`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.problemdetails.instance?view=aspnetcore-10.0#microsoft-aspnetcore-mvc-problemdetails-instance)<br>*property* | A URI reference that identifies the specific occurrence of the problem. | 1 | [Api/ApiControllerBase.cs:48](src/Northstar.Api/Controllers/ApiControllerBase.cs#L48) | [source](https://source.dot.net/#Microsoft.AspNetCore.Http.Abstractions/ProblemDetails/ProblemDetails.cs,99238c47e59c8135) |
| [`Status`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.problemdetails.status?view=aspnetcore-10.0#microsoft-aspnetcore-mvc-problemdetails-status)<br>*property* | The HTTP status code(\[RFC7231\], Section 6) generated by the origin server for this occurrence of the problem. | 1 | [Api/ApiControllerBase.cs:45](src/Northstar.Api/Controllers/ApiControllerBase.cs#L45) | [source](https://source.dot.net/#Microsoft.AspNetCore.Http.Abstractions/ProblemDetails/ProblemDetails.cs,81e57d68c8c75886) |
| [`Title`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.problemdetails.title?view=aspnetcore-10.0#microsoft-aspnetcore-mvc-problemdetails-title)<br>*property* | A short, human-readable summary of the problem type. | 1 | [Api/ApiControllerBase.cs:46](src/Northstar.Api/Controllers/ApiControllerBase.cs#L46) | [source](https://source.dot.net/#Microsoft.AspNetCore.Http.Abstractions/ProblemDetails/ProblemDetails.cs,372620895fabc832) |
| **[`ProducesResponseTypeAttribute`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.producesresponsetypeattribute?view=aspnetcore-10.0)**<br>*class, used as an attribute* | A filter that specifies the type of the value and status code returned by the action. | 3 | [Api/ImportBatchesController.cs:64](src/Northstar.Api/Controllers/ImportBatchesController.cs#L64)<br>[Api/PayrollRunsController.cs:66](src/Northstar.Api/Controllers/PayrollRunsController.cs#L66)<br>+1 more | [source](https://source.dot.net/#Microsoft.AspNetCore.Mvc.Core/ProducesResponseTypeAttribute.cs,b5f4a9f224b8e7cc) |
| **[`ProducesResponseTypeAttribute<T>`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.producesresponsetypeattribute-1?view=aspnetcore-10.0)**<br>*class, used as an attribute* | Declares a status code an action can return, with the response body type given as the type argument. † | 22 | [Api/ImportBatchesController.cs:14](src/Northstar.Api/Controllers/ImportBatchesController.cs#L14)<br>[Api/PayrollRunsController.cs:13](src/Northstar.Api/Controllers/PayrollRunsController.cs#L13)<br>+20 more | [source](https://source.dot.net/#Microsoft.AspNetCore.Mvc.Core/ProducesResponseTypeOfTAttribute.cs,5530d149a6c39582) |
| **[`RouteAttribute`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.routeattribute?view=aspnetcore-10.0)**<br>*class, used as an attribute* | Specifies an attribute route on a controller. | 3 | [Api/ImportBatchesController.cs:8](src/Northstar.Api/Controllers/ImportBatchesController.cs#L8)<br>[Api/PayrollRunsController.cs:7](src/Northstar.Api/Controllers/PayrollRunsController.cs#L7)<br>+1 more | [source](https://source.dot.net/#Microsoft.AspNetCore.Mvc.Core/RouteAttribute.cs,ae2c98dd081eb97f) |

#### Microsoft.AspNetCore.Mvc.Testing

*5 APIs · 13 uses · [namespace page on Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.testing?view=aspnetcore-10.0)*

| API | What it does | Uses | Where | Source |
| --- | --- | ---: | --- | --- |
| **[`WebApplicationFactory<TEntryPoint>`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.testing.webapplicationfactory-1?view=aspnetcore-10.0)**<br>*class, used as a base type* | Factory for bootstrapping an application in memory for functional end to end tests. | 1 | [IntegrationTests/CustomWebApplicationFactory.cs:13](tests/Northstar.Api.IntegrationTests/CustomWebApplicationFactory.cs#L13) | [source](https://source.dot.net/#Microsoft.AspNetCore.Mvc.Testing/Generated/361d1da7f942c932/CustomJsonSerializerContext.GetJsonTypeInfo.g.cs,4965cc9370a327d3) |
| [`Services`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.testing.webapplicationfactory-1.services?view=aspnetcore-10.0#microsoft-aspnetcore-mvc-testing-webapplicationfactory-1-services)<br>*property* | Gets the IServiceProvider created by the server associated with this WebApplicationFactory. | 6 | [IntegrationTests/ImportBatchesApiTests.cs:406](tests/Northstar.Api.IntegrationTests/ImportBatchesApiTests.cs#L406)<br>[IntegrationTests/ImportBatchesApiTests.cs:417](tests/Northstar.Api.IntegrationTests/ImportBatchesApiTests.cs#L417)<br>+4 more | [source](https://source.dot.net/#Microsoft.AspNetCore.Mvc.Testing/WebApplicationFactory.cs,a4af5f8d6c1e41af) |
| [`ConfigureWebHost(IWebHostBuilder)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.testing.webapplicationfactory-1.configurewebhost?view=aspnetcore-10.0#microsoft-aspnetcore-mvc-testing-webapplicationfactory-1-configurewebhost(microsoft-aspnetcore-hosting-iwebhostbuilder))<br>*method, overridden here* | Gives a fixture an opportunity to configure the application before it gets built. | 1 | [IntegrationTests/CustomWebApplicationFactory.cs:27](tests/Northstar.Api.IntegrationTests/CustomWebApplicationFactory.cs#L27) | [source](https://source.dot.net/#Microsoft.AspNetCore.Mvc.Testing/WebApplicationFactory.cs,2be601b3f5e6ce2c) |
| [`CreateClient()`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.testing.webapplicationfactory-1.createclient?view=aspnetcore-10.0#microsoft-aspnetcore-mvc-testing-webapplicationfactory-1-createclient)<br>*method* | Creates an instance of HttpClient that automatically follows redirects and handles cookies. | 3 | [IntegrationTests/ImportBatchesApiTests.cs:324](tests/Northstar.Api.IntegrationTests/ImportBatchesApiTests.cs#L324)<br>[IntegrationTests/PayrollRunsApiTests.cs:16](tests/Northstar.Api.IntegrationTests/PayrollRunsApiTests.cs#L16)<br>+1 more | [source](https://source.dot.net/#Microsoft.AspNetCore.Mvc.Testing/WebApplicationFactory.cs,a834a4df33508243) |
| [`Dispose(bool)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.testing.webapplicationfactory-1.dispose?view=aspnetcore-10.0#microsoft-aspnetcore-mvc-testing-webapplicationfactory-1-dispose(system-boolean))<br>*method, also overridden here* | Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources. | 2 | [IntegrationTests/CustomWebApplicationFactory.cs:55](tests/Northstar.Api.IntegrationTests/CustomWebApplicationFactory.cs#L55)<br>[IntegrationTests/CustomWebApplicationFactory.cs:57](tests/Northstar.Api.IntegrationTests/CustomWebApplicationFactory.cs#L57) | [source](https://source.dot.net/#Microsoft.AspNetCore.Mvc.Testing/WebApplicationFactory.cs,ce557e5fb17fc270) |

### Microsoft.Extensions

*7 namespaces, 66 APIs.*

#### Microsoft.Extensions.Configuration

*9 APIs · 6 uses · [namespace page on Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.configuration?view=net-10.0-pp)*

| API | What it does | Uses | Where | Source |
| --- | --- | ---: | --- | --- |
| **[`ConfigurationBinder`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.configuration.configurationbinder?view=net-10.0-pp)**<br>*static class* | Static helper class that allows binding strongly typed objects to configuration values. | – | through its members | [source](https://source.dot.net/#Microsoft.Extensions.Configuration.Binder/ConfigurationBinder.cs,d94dc1ea7643ba86) |
| [`GetValue<T>(this IConfiguration, string, T)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.configuration.configurationbinder.getvalue?view=net-10.0-pp#microsoft-extensions-configuration-configurationbinder-getvalue-1(microsoft-extensions-configuration-iconfiguration-system-string-0))<br>*extension method* | Extracts the value with the specified key and converts it to type T. | 1 | [Api/Program.cs:37](src/Northstar.Api/Program.cs#L37) | [source](https://source.dot.net/#Microsoft.Extensions.Configuration.Binder/ConfigurationBinder.cs,232ab46b757d72f9) |
| **[`ConfigurationExtensions`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.configuration.configurationextensions?view=net-10.0-pp)**<br>*static class* | Provides extension methods for configuration classes. | – | through its members | [source](https://source.dot.net/#Microsoft.Extensions.Configuration.Abstractions/ConfigurationExtensions.cs,d90e68c41e32579c) |
| [`GetConnectionString(this IConfiguration, string)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.configuration.configurationextensions.getconnectionstring?view=net-10.0-pp#microsoft-extensions-configuration-configurationextensions-getconnectionstring(microsoft-extensions-configuration-iconfiguration-system-string))<br>*extension method* | Gets the specified connection string from the specified configuration. | 1 | [Infrastructure/DependencyInjection.cs:18](src/Northstar.Infrastructure/DependencyInjection.cs#L18) | [source](https://source.dot.net/#Microsoft.Extensions.Configuration.Abstractions/ConfigurationExtensions.cs,a5adc9d3e8779072) |
| **[`IConfiguration`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.configuration.iconfiguration?view=net-10.0-pp)**<br>*interface* | Represents a set of key/value application configuration properties. | 1 | [Infrastructure/DependencyInjection.cs:15](src/Northstar.Infrastructure/DependencyInjection.cs#L15) | [source](https://source.dot.net/#Microsoft.Extensions.Configuration.Abstractions/IConfiguration.cs,168064d5c8bd0f8d) |
| [`this[string]`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.configuration.iconfiguration.item?view=net-10.0-pp#microsoft-extensions-configuration-iconfiguration-item(system-string))<br>*indexer* | Gets or sets a configuration value. | 1 | [Infrastructure/DependencyInjection.cs:17](src/Northstar.Infrastructure/DependencyInjection.cs#L17) | [source](https://source.dot.net/#Microsoft.Extensions.Configuration.Abstractions/IConfiguration.cs,f1d8f17deeb31c75) |
| [`GetSection(string)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.configuration.iconfiguration.getsection?view=net-10.0-pp#microsoft-extensions-configuration-iconfiguration-getsection(system-string))<br>*method* | Gets a configuration sub-section with the specified key. | 1 | [Infrastructure/DependencyInjection.cs:40](src/Northstar.Infrastructure/DependencyInjection.cs#L40) | [source](https://source.dot.net/#Microsoft.Extensions.Configuration.Abstractions/IConfiguration.cs,8479241783b3be92) |
| **[`MemoryConfigurationBuilderExtensions`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.configuration.memoryconfigurationbuilderextensions?view=net-10.0-pp)**<br>*static class* | IConfigurationBuilder extension methods for the MemoryConfigurationProvider. | – | through its members | [source](https://source.dot.net/#Microsoft.Extensions.Configuration/MemoryConfigurationBuilderExtensions.cs,32aca20bb4b52285) |
| [`AddInMemoryCollection(this IConfigurationBuilder, IEnumerable<KeyValuePair<string, string>>)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.configuration.memoryconfigurationbuilderextensions.addinmemorycollection?view=net-10.0-pp#microsoft-extensions-configuration-memoryconfigurationbuilderextensions-addinmemorycollection(microsoft-extensions-configuration-iconfigurationbuilder-system-collections-generic-ienumerable((system-collections-generic-keyvaluepair((system-string-system-string))))))<br>*extension method* | Adds the memory configuration provider to configurationBuilder. | 1 | [IntegrationTests/CustomWebApplicationFactory.cs:31](tests/Northstar.Api.IntegrationTests/CustomWebApplicationFactory.cs#L31) | [source](https://source.dot.net/#Microsoft.Extensions.Configuration/MemoryConfigurationBuilderExtensions.cs,c485e5b955d5deed) |

#### Microsoft.Extensions.DependencyInjection

*39 APIs · 69 uses · [namespace page on Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection?view=net-10.0-pp)*

| API | What it does | Uses | Where | Source |
| --- | --- | ---: | --- | --- |
| **[`AsyncServiceScope`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.asyncservicescope?view=net-10.0-pp)**<br>*struct* | An IServiceScope implementation that implements IAsyncDisposable. | – | through its members | [source](https://source.dot.net/#Microsoft.Extensions.DependencyInjection.Abstractions/AsyncServiceScope.cs,172c96779a9c8c01) |
| [`ServiceProvider`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.asyncservicescope.serviceprovider?view=net-10.0-pp#microsoft-extensions-dependencyinjection-asyncservicescope-serviceprovider)<br>*property* | The service provider that resolves services inside this scope. † | 10 | [Api/Program.cs:41](src/Northstar.Api/Program.cs#L41)<br>[Infrastructure/ImportBatchWorker.cs:27](src/Northstar.Infrastructure/Messaging/ImportBatchWorker.cs#L27)<br>+8 more | [source](https://source.dot.net/#Microsoft.Extensions.DependencyInjection.Abstractions/AsyncServiceScope.cs,79d154afe8fb3025) |
| **[`CorsServiceCollectionExtensions`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.corsservicecollectionextensions?view=aspnetcore-10.0)**<br>*static class* | Extension methods for setting up cross-origin resource sharing services in an IServiceCollection. | – | through its members | [source](https://source.dot.net/#Microsoft.AspNetCore.Cors/CorsServiceCollectionExtensions.cs,e18f5495bf549f8f) |
| [`AddCors(this IServiceCollection, Action<CorsOptions>)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.corsservicecollectionextensions.addcors?view=aspnetcore-10.0#microsoft-extensions-dependencyinjection-corsservicecollectionextensions-addcors(microsoft-extensions-dependencyinjection-iservicecollection-system-action((microsoft-aspnetcore-cors-infrastructure-corsoptions))))<br>*extension method* | Adds cross-origin resource sharing services to the specified IServiceCollection. | 1 | [Api/Program.cs:18](src/Northstar.Api/Program.cs#L18) | [source](https://source.dot.net/#Microsoft.AspNetCore.Cors/CorsServiceCollectionExtensions.cs,348bfc06c6464201) |
| **[`EntityFrameworkServiceCollectionExtensions`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.entityframeworkservicecollectionextensions?view=efcore-10.0)**<br>*static class* | Extension methods for setting up Entity Framework related services in an IServiceCollection. | – | through its members | [GitHub](https://github.com/dotnet/efcore/blob/v10.0.15/src/EFCore/Extensions/EntityFrameworkServiceCollectionExtensions.cs#L14) |
| [`AddDbContext<TContext>(this IServiceCollection, Action<DbContextOptionsBuilder>, ServiceLifetime, ServiceLifetime)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.entityframeworkservicecollectionextensions.adddbcontext?view=efcore-10.0#microsoft-extensions-dependencyinjection-entityframeworkservicecollectionextensions-adddbcontext-1(microsoft-extensions-dependencyinjection-iservicecollection-system-action((microsoft-entityframeworkcore-dbcontextoptionsbuilder))-microsoft-extensions-dependencyinjection-servicelifetime-microsoft-extensions-dependencyinjection-servicelifetime))<br>*extension method* | Registers the given context as a service in the IServiceCollection. | 2 | [Infrastructure/DependencyInjection.cs:21](src/Northstar.Infrastructure/DependencyInjection.cs#L21)<br>[IntegrationTests/CustomWebApplicationFactory.cs:44](tests/Northstar.Api.IntegrationTests/CustomWebApplicationFactory.cs#L44) | [GitHub](https://github.com/dotnet/efcore/blob/v10.0.15/src/EFCore/Extensions/EntityFrameworkServiceCollectionExtensions.cs#L58) |
| **[`HttpClientFactoryServiceCollectionExtensions`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.httpclientfactoryservicecollectionextensions?view=net-10.0-pp)**<br>*static class* | Extension methods to configure an IServiceCollection for IHttpClientFactory. | – | through its members | [source](https://source.dot.net/#Microsoft.Extensions.Http/DependencyInjection/HttpClientFactoryServiceCollectionExtensions.cs,996dad610791ba56) |
| [`AddHttpClient<TClient, TImplementation>(this IServiceCollection, Action<IServiceProvider, HttpClient>)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.httpclientfactoryservicecollectionextensions.addhttpclient?view=net-10.0-pp#microsoft-extensions-dependencyinjection-httpclientfactoryservicecollectionextensions-addhttpclient-2(microsoft-extensions-dependencyinjection-iservicecollection-system-action((system-iserviceprovider-system-net-http-httpclient))))<br>*extension method* | Adds the IHttpClientFactory and related services to the IServiceCollection and configures a binding between the TClient type and a named HttpClient. | 1 | [Infrastructure/DependencyInjection.cs:48](src/Northstar.Infrastructure/DependencyInjection.cs#L48) | [source](https://source.dot.net/#Microsoft.Extensions.Http/DependencyInjection/HttpClientFactoryServiceCollectionExtensions.cs,398855a7eab6e2ef) |
| **[`HttpServiceCollectionExtensions`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.httpservicecollectionextensions?view=aspnetcore-10.0)**<br>*static class* | Extension methods for configuring HttpContext services. | – | through its members | [source](https://source.dot.net/#Microsoft.AspNetCore.Http/HttpServiceCollectionExtensions.cs,0084bae0e6f0c481) |
| [`AddHttpContextAccessor(this IServiceCollection)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.httpservicecollectionextensions.addhttpcontextaccessor?view=aspnetcore-10.0#microsoft-extensions-dependencyinjection-httpservicecollectionextensions-addhttpcontextaccessor(microsoft-extensions-dependencyinjection-iservicecollection))<br>*extension method* | Adds a default implementation for the IHttpContextAccessor service. | 1 | [Api/Program.cs:16](src/Northstar.Api/Program.cs#L16) | [source](https://source.dot.net/#Microsoft.AspNetCore.Http/HttpServiceCollectionExtensions.cs,311e627947384f0b) |
| **[`IServiceCollection`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.iservicecollection?view=net-10.0-pp)**<br>*interface* | Specifies the contract for a collection of service descriptors. | 4 | [Application/DependencyInjection.cs:13](src/Northstar.Application/DependencyInjection.cs#L13)<br>[Infrastructure/DependencyInjection.cs:13](src/Northstar.Infrastructure/DependencyInjection.cs#L13)<br>+2 more | [source](https://source.dot.net/#Microsoft.Extensions.DependencyInjection.Abstractions/IServiceCollection.cs,2f0e403ea0d41c8e) |
| **[`IServiceScopeFactory`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.iservicescopefactory?view=net-10.0-pp)**<br>*interface* | Creates instances of IServiceScope, which is used to create services within a scope. | 1 | [Infrastructure/ImportBatchWorker.cs:13](src/Northstar.Infrastructure/Messaging/ImportBatchWorker.cs#L13) | [source](https://source.dot.net/#Microsoft.Extensions.DependencyInjection.Abstractions/IServiceScopeFactory.cs,214994a80197fb23) |
| **[`MvcCoreMvcBuilderExtensions`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.mvccoremvcbuilderextensions?view=aspnetcore-10.0)**<br>*static class* | Extensions for configuring MVC using an IMvcBuilder. | – | through its members | [source](https://source.dot.net/#Microsoft.AspNetCore.Mvc.Core/DependencyInjection/MvcCoreMvcBuilderExtensions.cs,5d350e90678c463f) |
| [`AddJsonOptions(this IMvcBuilder, Action<JsonOptions>)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.mvccoremvcbuilderextensions.addjsonoptions?view=aspnetcore-10.0#microsoft-extensions-dependencyinjection-mvccoremvcbuilderextensions-addjsonoptions(microsoft-extensions-dependencyinjection-imvcbuilder-system-action((microsoft-aspnetcore-mvc-jsonoptions))))<br>*extension method* | Configures JsonOptions for the specified builder. | 1 | [Api/Program.cs:10](src/Northstar.Api/Program.cs#L10) | [source](https://source.dot.net/#Microsoft.AspNetCore.Mvc.Core/DependencyInjection/MvcCoreMvcBuilderExtensions.cs,cd56b1e993e62b34) |
| **[`MvcServiceCollectionExtensions`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.mvcservicecollectionextensions?view=aspnetcore-10.0)**<br>*static class* | Extension methods for setting up MVC services in an IServiceCollection. | – | through its members | [source](https://source.dot.net/#Microsoft.AspNetCore.Mvc/MvcServiceCollectionExtensions.cs,fd414e3a3100e7e2) |
| [`AddControllers(this IServiceCollection)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.mvcservicecollectionextensions.addcontrollers?view=aspnetcore-10.0#microsoft-extensions-dependencyinjection-mvcservicecollectionextensions-addcontrollers(microsoft-extensions-dependencyinjection-iservicecollection))<br>*extension method* | Adds services for controllers to the specified IServiceCollection. | 1 | [Api/Program.cs:9](src/Northstar.Api/Program.cs#L9) | [source](https://source.dot.net/#Microsoft.AspNetCore.Mvc/MvcServiceCollectionExtensions.cs,0dc249f7fc0b852b) |
| **[`OpenApiServiceCollectionExtensions`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.openapiservicecollectionextensions?view=aspnetcore-10.0)**<br>*static class* | OpenAPI-related methods for IServiceCollection. | – | through its members | [source](https://source.dot.net/#Microsoft.AspNetCore.OpenApi/Extensions/OpenApiServiceCollectionExtensions.cs,550920d4da94560b) |
| [`AddOpenApi(this IServiceCollection)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.openapiservicecollectionextensions.addopenapi?view=aspnetcore-10.0#microsoft-extensions-dependencyinjection-openapiservicecollectionextensions-addopenapi(microsoft-extensions-dependencyinjection-iservicecollection))<br>*extension method* | Adds OpenAPI services related to the default document to the specified IServiceCollection. | 1 | [Api/Program.cs:13](src/Northstar.Api/Program.cs#L13) | [source](https://source.dot.net/#Microsoft.AspNetCore.OpenApi/Extensions/OpenApiServiceCollectionExtensions.cs,66158041cba8c238) |
| **[`OptionsBuilderConfigurationExtensions`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.optionsbuilderconfigurationextensions?view=net-10.0-pp)**<br>*static class* | Extension methods for adding configuration related options services to the DI container via OptionsBuilder. | – | through its members | [source](https://source.dot.net/#Microsoft.Extensions.Options.ConfigurationExtensions/OptionsBuilderConfigurationExtensions.cs,aee124e81f406b1f) |
| [`Bind<TOptions>(this OptionsBuilder<TOptions>, IConfiguration)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.optionsbuilderconfigurationextensions.bind?view=net-10.0-pp#microsoft-extensions-dependencyinjection-optionsbuilderconfigurationextensions-bind-1(microsoft-extensions-options-optionsbuilder((-0))-microsoft-extensions-configuration-iconfiguration))<br>*extension method* | Registers a configuration instance which TOptions will bind against. | 1 | [Infrastructure/DependencyInjection.cs:40](src/Northstar.Infrastructure/DependencyInjection.cs#L40) | [source](https://source.dot.net/#Microsoft.Extensions.Options.ConfigurationExtensions/OptionsBuilderConfigurationExtensions.cs,084201907e2bd9bb) |
| **[`OptionsBuilderExtensions`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.optionsbuilderextensions?view=net-10.0-pp)**<br>*static class* | Extension methods for adding configuration-related options services to the DI container via OptionsBuilder. | – | through its members | [source](https://source.dot.net/#Microsoft.Extensions.Options/OptionsBuilderExtensions.cs,70f7c8fb50da443a) |
| [`ValidateOnStart<TOptions>(this OptionsBuilder<TOptions>)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.optionsbuilderextensions.validateonstart?view=net-10.0-pp#microsoft-extensions-dependencyinjection-optionsbuilderextensions-validateonstart-1(microsoft-extensions-options-optionsbuilder((-0))))<br>*extension method* | Enforces options validation check on start rather than at run time. | 1 | [Infrastructure/DependencyInjection.cs:47](src/Northstar.Infrastructure/DependencyInjection.cs#L47) | [source](https://source.dot.net/#Microsoft.Extensions.Options/OptionsBuilderExtensions.cs,6bed8e39569e9298) |
| **[`OptionsServiceCollectionExtensions`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.optionsservicecollectionextensions?view=net-10.0-pp)**<br>*static class* | Extension methods for adding options services to the DI container. | – | through its members | [source](https://source.dot.net/#Microsoft.Extensions.Options/OptionsServiceCollectionExtensions.cs,0dd0bdb39a0aea57) |
| [`AddOptions<TOptions>(this IServiceCollection)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.optionsservicecollectionextensions.addoptions?view=net-10.0-pp#microsoft-extensions-dependencyinjection-optionsservicecollectionextensions-addoptions-1(microsoft-extensions-dependencyinjection-iservicecollection))<br>*extension method* | Gets an options builder that forwards Configure calls for the same TOptions to the underlying service collection. | 1 | [Infrastructure/DependencyInjection.cs:39](src/Northstar.Infrastructure/DependencyInjection.cs#L39) | [source](https://source.dot.net/#Microsoft.Extensions.Options/OptionsServiceCollectionExtensions.cs,0d90cd2ff970fa74) |
| **[`ProblemDetailsServiceCollectionExtensions`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.problemdetailsservicecollectionextensions?view=aspnetcore-10.0)**<br>*static class* | Contains extension methods to IServiceCollection. | – | through its members | [source](https://source.dot.net/#Microsoft.AspNetCore.Http.Extensions/ProblemDetailsServiceCollectionExtensions.cs,b6c37b52c5f8e474) |
| [`AddProblemDetails(this IServiceCollection)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.problemdetailsservicecollectionextensions.addproblemdetails?view=aspnetcore-10.0#microsoft-extensions-dependencyinjection-problemdetailsservicecollectionextensions-addproblemdetails(microsoft-extensions-dependencyinjection-iservicecollection))<br>*extension method* | Adds services required for creation of ProblemDetails for failed requests. | 2 | [Api/Program.cs:12](src/Northstar.Api/Program.cs#L12)<br>[FakeProvider/Program.cs:7](tools/Northstar.FakePayrollProvider/Program.cs#L7) | [source](https://source.dot.net/#Microsoft.AspNetCore.Http.Extensions/ProblemDetailsServiceCollectionExtensions.cs,bc0f7b6e2cbdd051) |
| **[`ServiceCollectionHostedServiceExtensions`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.servicecollectionhostedserviceextensions?view=net-10.0-pp)**<br>*static class* | Extension methods for adding hosted services to an IServiceCollection. | – | through its members | [source](https://source.dot.net/#Microsoft.Extensions.Hosting.Abstractions/ServiceCollectionHostedServiceExtensions.cs,44c8ac8d84044702) |
| [`AddHostedService<THostedService>(this IServiceCollection)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.servicecollectionhostedserviceextensions.addhostedservice?view=net-10.0-pp#microsoft-extensions-dependencyinjection-servicecollectionhostedserviceextensions-addhostedservice-1(microsoft-extensions-dependencyinjection-iservicecollection))<br>*extension method* | Add an IHostedService registration for the given type. | 1 | [Infrastructure/DependencyInjection.cs:62](src/Northstar.Infrastructure/DependencyInjection.cs#L62) | [source](https://source.dot.net/#Microsoft.Extensions.Hosting.Abstractions/ServiceCollectionHostedServiceExtensions.cs,7a9ac7b282b7b4d3) |
| **[`ServiceCollectionServiceExtensions`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.servicecollectionserviceextensions?view=net-10.0-pp)**<br>*static class* | Extension methods for adding services to an IServiceCollection. | – | through its members | [source](https://source.dot.net/#Microsoft.Extensions.DependencyInjection.Abstractions/ServiceCollectionServiceExtensions.cs,f84ac4bda398aafa) |
| [`AddScoped<TService>(this IServiceCollection)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.servicecollectionserviceextensions.addscoped?view=net-10.0-pp#microsoft-extensions-dependencyinjection-servicecollectionserviceextensions-addscoped-1(microsoft-extensions-dependencyinjection-iservicecollection))<br>*extension method* | Adds a scoped service of the type specified in TService to the specified IServiceCollection. | 5 | [Api/Program.cs:17](src/Northstar.Api/Program.cs#L17)<br>[Application/DependencyInjection.cs:18](src/Northstar.Application/DependencyInjection.cs#L18)<br>+3 more | [source](https://source.dot.net/#Microsoft.Extensions.DependencyInjection.Abstractions/ServiceCollectionServiceExtensions.cs,51ebce7c7caa4b7b) |
| [`AddScoped<TService>(this IServiceCollection, Func<IServiceProvider, TService>)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.servicecollectionserviceextensions.addscoped?view=net-10.0-pp#microsoft-extensions-dependencyinjection-servicecollectionserviceextensions-addscoped-1(microsoft-extensions-dependencyinjection-iservicecollection-system-func((system-iserviceprovider-0))))<br>*extension method* | Adds a scoped service of the type specified in TService with a factory specified in implementationFactory to the specified IServiceCollection. | 2 | [Infrastructure/DependencyInjection.cs:37](src/Northstar.Infrastructure/DependencyInjection.cs#L37)<br>[IntegrationTests/CustomWebApplicationFactory.cs:47](tests/Northstar.Api.IntegrationTests/CustomWebApplicationFactory.cs#L47) | [source](https://source.dot.net/#Microsoft.Extensions.DependencyInjection.Abstractions/ServiceCollectionServiceExtensions.cs,1483a9cd82f66189) |
| [`AddSingleton<TService, TImplementation>(this IServiceCollection)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.servicecollectionserviceextensions.addsingleton?view=net-10.0-pp#microsoft-extensions-dependencyinjection-servicecollectionserviceextensions-addsingleton-2(microsoft-extensions-dependencyinjection-iservicecollection))<br>*extension method* | Adds a singleton service of the type specified in TService with an implementation type specified in TImplementation to the specified IServiceCollection. | 1 | [Application/DependencyInjection.cs:15](src/Northstar.Application/DependencyInjection.cs#L15) | [source](https://source.dot.net/#Microsoft.Extensions.DependencyInjection.Abstractions/ServiceCollectionServiceExtensions.cs,c5648fbe8dc950e8) |
| [`AddSingleton<TService>(this IServiceCollection)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.servicecollectionserviceextensions.addsingleton?view=net-10.0-pp#microsoft-extensions-dependencyinjection-servicecollectionserviceextensions-addsingleton-1(microsoft-extensions-dependencyinjection-iservicecollection))<br>*extension method* | Adds a singleton service of the type specified in TService to the specified IServiceCollection. | 4 | [Application/DependencyInjection.cs:16](src/Northstar.Application/DependencyInjection.cs#L16)<br>[Infrastructure/DependencyInjection.cs:59](src/Northstar.Infrastructure/DependencyInjection.cs#L59)<br>+2 more | [source](https://source.dot.net/#Microsoft.Extensions.DependencyInjection.Abstractions/ServiceCollectionServiceExtensions.cs,14e58a210e5d0b4c) |
| [`AddSingleton<TService>(this IServiceCollection, Func<IServiceProvider, TService>)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.servicecollectionserviceextensions.addsingleton?view=net-10.0-pp#microsoft-extensions-dependencyinjection-servicecollectionserviceextensions-addsingleton-1(microsoft-extensions-dependencyinjection-iservicecollection-system-func((system-iserviceprovider-0))))<br>*extension method* | Adds a singleton service of the type specified in TService with a factory specified in implementationFactory to the specified IServiceCollection. | 2 | [Infrastructure/DependencyInjection.cs:60](src/Northstar.Infrastructure/DependencyInjection.cs#L60)<br>[IntegrationTests/CustomWebApplicationFactory.cs:50](tests/Northstar.Api.IntegrationTests/CustomWebApplicationFactory.cs#L50) | [source](https://source.dot.net/#Microsoft.Extensions.DependencyInjection.Abstractions/ServiceCollectionServiceExtensions.cs,0b949544b75dd21a) |
| [`AddSingleton<TService>(this IServiceCollection, TService)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.servicecollectionserviceextensions.addsingleton?view=net-10.0-pp#microsoft-extensions-dependencyinjection-servicecollectionserviceextensions-addsingleton-1(microsoft-extensions-dependencyinjection-iservicecollection-0))<br>*extension method* | Adds a singleton service of the type specified in TService with an instance specified in implementationInstance to the specified IServiceCollection. | 1 | [IntegrationTests/CustomWebApplicationFactory.cs:49](tests/Northstar.Api.IntegrationTests/CustomWebApplicationFactory.cs#L49) | [source](https://source.dot.net/#Microsoft.Extensions.DependencyInjection.Abstractions/ServiceCollectionServiceExtensions.cs,fd41d3e12501b25d) |
| **[`ServiceProviderServiceExtensions`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.serviceproviderserviceextensions?view=net-10.0-pp)**<br>*static class* | Extension methods for getting services from an IServiceProvider. | – | through its members | [source](https://source.dot.net/#Microsoft.Extensions.DependencyInjection.Abstractions/ServiceProviderServiceExtensions.cs,30dedc1df10c8d1f) |
| [`CreateAsyncScope(this IServiceProvider)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.serviceproviderserviceextensions.createasyncscope?view=net-10.0-pp#microsoft-extensions-dependencyinjection-serviceproviderserviceextensions-createasyncscope(system-iserviceprovider))<br>*extension method* | Creates a new AsyncServiceScope that can be used to resolve scoped services. | 7 | [Api/Program.cs:39](src/Northstar.Api/Program.cs#L39)<br>[IntegrationTests/ImportBatchesApiTests.cs:406](tests/Northstar.Api.IntegrationTests/ImportBatchesApiTests.cs#L406)<br>+5 more | [source](https://source.dot.net/#Microsoft.Extensions.DependencyInjection.Abstractions/ServiceProviderServiceExtensions.cs,478af7c6d9193e2a) |
| [`CreateAsyncScope(this IServiceScopeFactory)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.serviceproviderserviceextensions.createasyncscope?view=net-10.0-pp#microsoft-extensions-dependencyinjection-serviceproviderserviceextensions-createasyncscope(microsoft-extensions-dependencyinjection-iservicescopefactory))<br>*extension method* | Creates a new AsyncServiceScope that can be used to resolve scoped services. | 3 | [Infrastructure/ImportBatchWorker.cs:26](src/Northstar.Infrastructure/Messaging/ImportBatchWorker.cs#L26)<br>[Infrastructure/ImportBatchWorker.cs:64](src/Northstar.Infrastructure/Messaging/ImportBatchWorker.cs#L64)<br>+1 more | [source](https://source.dot.net/#Microsoft.Extensions.DependencyInjection.Abstractions/ServiceProviderServiceExtensions.cs,f45ecbe0b9fb9005) |
| [`GetRequiredService<T>(this IServiceProvider)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.serviceproviderserviceextensions.getrequiredservice?view=net-10.0-pp#microsoft-extensions-dependencyinjection-serviceproviderserviceextensions-getrequiredservice-1(system-iserviceprovider))<br>*extension method* | Get service of type T from the IServiceProvider. | 15 | [Api/Program.cs:41](src/Northstar.Api/Program.cs#L41)<br>[Infrastructure/DependencyInjection.cs:38](src/Northstar.Infrastructure/DependencyInjection.cs#L38)<br>+13 more | [source](https://source.dot.net/#Microsoft.Extensions.DependencyInjection.Abstractions/ServiceProviderServiceExtensions.cs,796dfe053760ad43) |

#### Microsoft.Extensions.DependencyInjection.Extensions

*2 APIs · 4 uses · [namespace page on Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.extensions?view=net-10.0-pp)*

| API | What it does | Uses | Where | Source |
| --- | --- | ---: | --- | --- |
| **[`ServiceCollectionDescriptorExtensions`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.extensions.servicecollectiondescriptorextensions?view=net-10.0-pp)**<br>*static class* | Extension methods for adding and removing services to an IServiceCollection. | – | through its members | [source](https://source.dot.net/#Microsoft.Extensions.DependencyInjection.Abstractions/Extensions/ServiceCollectionDescriptorExtensions.cs,5fb80c5606eecf56) |
| [`RemoveAll<T>(this IServiceCollection)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.extensions.servicecollectiondescriptorextensions.removeall?view=net-10.0-pp#microsoft-extensions-dependencyinjection-extensions-servicecollectiondescriptorextensions-removeall-1(microsoft-extensions-dependencyinjection-iservicecollection))<br>*extension method* | Removes all services of type T in IServiceCollection. | 4 | [IntegrationTests/CustomWebApplicationFactory.cs:39](tests/Northstar.Api.IntegrationTests/CustomWebApplicationFactory.cs#L39)<br>[IntegrationTests/CustomWebApplicationFactory.cs:40](tests/Northstar.Api.IntegrationTests/CustomWebApplicationFactory.cs#L40)<br>+2 more | [source](https://source.dot.net/#Microsoft.Extensions.DependencyInjection.Abstractions/Extensions/ServiceCollectionDescriptorExtensions.cs,b83bce9c71136f26) |

#### Microsoft.Extensions.Hosting

*4 APIs · 3 uses · [namespace page on Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.hosting?view=net-10.0-pp)*

| API | What it does | Uses | Where | Source |
| --- | --- | ---: | --- | --- |
| **[`BackgroundService`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.hosting.backgroundservice?view=net-10.0-pp)**<br>*abstract class, used as a base type* | Base class for implementing a long running IHostedService. | 1 | [Infrastructure/ImportBatchWorker.cs:14](src/Northstar.Infrastructure/Messaging/ImportBatchWorker.cs#L14) | [source](https://source.dot.net/#Microsoft.Extensions.Hosting.Abstractions/BackgroundService.cs,780240fc8462ba27) |
| [`ExecuteAsync(CancellationToken)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.hosting.backgroundservice.executeasync?view=net-10.0-pp#microsoft-extensions-hosting-backgroundservice-executeasync(system-threading-cancellationtoken))<br>*method, overridden here* | This method is called when the IHostedService starts. | 1 | [Infrastructure/ImportBatchWorker.cs:16](src/Northstar.Infrastructure/Messaging/ImportBatchWorker.cs#L16) | [source](https://source.dot.net/#Microsoft.Extensions.Hosting.Abstractions/BackgroundService.cs,145911ac3bd8eb33) |
| **[`HostEnvironmentEnvExtensions`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.hosting.hostenvironmentenvextensions?view=net-10.0-pp)**<br>*static class* | Extension methods for IHostEnvironment. | – | through its members | [source](https://source.dot.net/#Microsoft.Extensions.Hosting.Abstractions/HostEnvironmentEnvExtensions.cs,7e4c782fa1486860) |
| [`IsDevelopment(this IHostEnvironment)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.hosting.hostenvironmentenvextensions.isdevelopment?view=net-10.0-pp#microsoft-extensions-hosting-hostenvironmentenvextensions-isdevelopment(microsoft-extensions-hosting-ihostenvironment))<br>*extension method* | Checks if the current host environment name is Environments.Development. | 1 | [Api/Program.cs:25](src/Northstar.Api/Program.cs#L25) | [source](https://source.dot.net/#Microsoft.Extensions.Hosting.Abstractions/HostEnvironmentEnvExtensions.cs,7b43b7392a224997) |

#### Microsoft.Extensions.Logging

*3 APIs · 4 uses · [namespace page on Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.logging?view=net-10.0-pp)*

| API | What it does | Uses | Where | Source |
| --- | --- | ---: | --- | --- |
| **[`ILogger<TCategoryName>`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.logging.ilogger-1?view=net-10.0-pp)**<br>*interface* | A generic interface for logging where the category name is derived from the specified TCategoryName type name. | 1 | [Infrastructure/ImportBatchWorker.cs:14](src/Northstar.Infrastructure/Messaging/ImportBatchWorker.cs#L14) | [source](https://source.dot.net/#Microsoft.Extensions.Logging.Abstractions/ILoggerT.cs,f7011245adc3f447) |
| **[`LoggerExtensions`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.logging.loggerextensions?view=net-10.0-pp)**<br>*static class* | ILogger extension methods for common scenarios. | – | through its members | [source](https://source.dot.net/#Microsoft.Extensions.Logging.Abstractions/LoggerExtensions.cs,c04734fc35ce2b66) |
| [`LogError(this ILogger, Exception, string, params object[])`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.logging.loggerextensions.logerror?view=net-10.0-pp#microsoft-extensions-logging-loggerextensions-logerror(microsoft-extensions-logging-ilogger-system-exception-system-string-system-object()))<br>*extension method* | Formats and writes an error log message. | 3 | [Infrastructure/ImportBatchWorker.cs:36](src/Northstar.Infrastructure/Messaging/ImportBatchWorker.cs#L36)<br>[Infrastructure/ImportBatchWorker.cs:95](src/Northstar.Infrastructure/Messaging/ImportBatchWorker.cs#L95)<br>+1 more | [source](https://source.dot.net/#Microsoft.Extensions.Logging.Abstractions/LoggerExtensions.cs,8d318bfc53ec9b58) |

#### Microsoft.Extensions.Options

*6 APIs · 10 uses · [namespace page on Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.options?view=net-10.0-pp)*

| API | What it does | Uses | Where | Source |
| --- | --- | ---: | --- | --- |
| **[`IOptions<TOptions>`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.options.ioptions-1?view=net-10.0-pp)**<br>*interface* | Retrieves configured TOptions instances. | 2 | [Infrastructure/DependencyInjection.cs:51](src/Northstar.Infrastructure/DependencyInjection.cs#L51)<br>[Infrastructure/FakePayrollProviderClient.cs:11](src/Northstar.Infrastructure/Integrations/FakePayrollProviderClient.cs#L11) | [source](https://source.dot.net/#Microsoft.Extensions.Options/IOptions.cs,63b7cb270afa5bd8) |
| [`Value`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.options.ioptions-1.value?view=net-10.0-pp#microsoft-extensions-options-ioptions-1-value)<br>*property* | Gets the default configured TOptions instance. | 2 | [Infrastructure/DependencyInjection.cs:52](src/Northstar.Infrastructure/DependencyInjection.cs#L52)<br>[Infrastructure/FakePayrollProviderClient.cs:13](src/Northstar.Infrastructure/Integrations/FakePayrollProviderClient.cs#L13) | [source](https://source.dot.net/#Microsoft.Extensions.Options/IOptions.cs,f01debd9f4020f2e) |
| **[`Options`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.options.options?view=net-10.0-pp)**<br>*static class* | Helper class for options. | 1 | [IntegrationTests/FakePayrollProviderClientTests.cs:162](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L162) | [source](https://source.dot.net/#Microsoft.Extensions.Options/Options.cs,4cf8aeae03f59450) |
| [`Create<TOptions>(TOptions)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.options.options.create?view=net-10.0-pp#microsoft-extensions-options-options-create-1(-0))<br>*static method* | Creates a wrapper around an instance of TOptions to return itself as an IOptions. | 1 | [IntegrationTests/FakePayrollProviderClientTests.cs:162](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L162) | [source](https://source.dot.net/#Microsoft.Extensions.Options/Options.cs,de2807c517954beb) |
| **[`OptionsBuilder<TOptions>`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.options.optionsbuilder-1?view=net-10.0-pp)**<br>*class* | Configures TOptions instances. | – | through its members | [source](https://source.dot.net/#Microsoft.Extensions.Options/OptionsBuilder.cs,148d726263cfbbfa) |
| [`Validate(Func<TOptions, bool>, string)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.options.optionsbuilder-1.validate?view=net-10.0-pp#microsoft-extensions-options-optionsbuilder-1-validate(system-func((-0-system-boolean))-system-string))<br>*method* | Registers a validation action for an options type. | 4 | [Infrastructure/DependencyInjection.cs:41](src/Northstar.Infrastructure/DependencyInjection.cs#L41)<br>[Infrastructure/DependencyInjection.cs:42](src/Northstar.Infrastructure/DependencyInjection.cs#L42)<br>+2 more | [source](https://source.dot.net/#Microsoft.Extensions.Options/OptionsBuilder.cs,b64ff06426fa2964) |

#### Microsoft.Extensions.Primitives

*3 APIs · 3 uses · [namespace page on Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.primitives?view=net-10.0-pp)*

| API | What it does | Uses | Where | Source |
| --- | --- | ---: | --- | --- |
| **[`StringValues`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.primitives.stringvalues?view=net-10.0-pp)**<br>*struct* | Represents zero/null, one, or many strings in an efficient way. | – | through its members | [source](https://source.dot.net/#Microsoft.Extensions.Primitives/StringValues.cs,f7f329a587dc1a5e) |
| [`ToString()`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.primitives.stringvalues.tostring?view=net-10.0-pp#microsoft-extensions-primitives-stringvalues-tostring)<br>*method* | Converts the value of the current StringValues object to its equivalent string representation, with multiple values joined as a comma separated string. | 2 | [Api/HeaderUserContext.cs:7](src/Northstar.Api/Security/HeaderUserContext.cs#L7)<br>[Api/HeaderUserContext.cs:12](src/Northstar.Api/Security/HeaderUserContext.cs#L12) | [source](https://source.dot.net/#Microsoft.Extensions.Primitives/StringValues.cs,a69707cfb8098edc) |
| [`implicit operator StringValues(string)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.primitives.stringvalues.op_implicit?view=net-10.0-pp)<br>*conversion* | Defines an implicit conversion of a given string to a StringValues. | 1 | [FakeProvider/Program.cs:120](tools/Northstar.FakePayrollProvider/Program.cs#L120) | [source](https://source.dot.net/#Microsoft.Extensions.Primitives/StringValues.cs,be1db815986c98e2) |

### Entity Framework Core and SQLite

*6 namespaces, 74 APIs.*

#### Microsoft.Data.Sqlite

*3 APIs · 4 uses · [namespace page on Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/api/microsoft.data.sqlite?view=msdata-sqlite-10.0.0)*

| API | What it does | Uses | Where | Source |
| --- | --- | ---: | --- | --- |
| **[`SqliteConnection`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.data.sqlite.sqliteconnection?view=msdata-sqlite-10.0.0)**<br>*class* | Represents a connection to a SQLite database. | 2 | [IntegrationTests/CustomWebApplicationFactory.cs:17](tests/Northstar.Api.IntegrationTests/CustomWebApplicationFactory.cs#L17)<br>[IntegrationTests/CustomWebApplicationFactory.cs:21](tests/Northstar.Api.IntegrationTests/CustomWebApplicationFactory.cs#L21) | [GitHub](https://github.com/dotnet/efcore/blob/v10.0.15/src/Microsoft.Data.Sqlite.Core/SqliteConnection.cs#L24) |
| [`new SqliteConnection(string)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.data.sqlite.sqliteconnection.-ctor?view=msdata-sqlite-10.0.0#microsoft-data-sqlite-sqliteconnection-ctor(system-string))<br>*constructor* | Initializes a new instance of the SqliteConnection class. | 1 | [IntegrationTests/CustomWebApplicationFactory.cs:21](tests/Northstar.Api.IntegrationTests/CustomWebApplicationFactory.cs#L21) | [GitHub](https://github.com/dotnet/efcore/blob/v10.0.15/src/Microsoft.Data.Sqlite.Core/SqliteConnection.cs#L121) |
| [`Open()`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.data.sqlite.sqliteconnection.open?view=msdata-sqlite-10.0.0#microsoft-data-sqlite-sqliteconnection-open)<br>*method* | Opens a connection to the database using the value of SqliteConnection.ConnectionString. | 1 | [IntegrationTests/CustomWebApplicationFactory.cs:22](tests/Northstar.Api.IntegrationTests/CustomWebApplicationFactory.cs#L22) | [GitHub](https://github.com/dotnet/efcore/blob/v10.0.15/src/Microsoft.Data.Sqlite.Core/SqliteConnection.cs#L247) |

#### Microsoft.EntityFrameworkCore

*35 APIs · 155 uses · [namespace page on Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore?view=efcore-10.0)*

| API | What it does | Uses | Where | Source |
| --- | --- | ---: | --- | --- |
| **[`DbContext`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.dbcontext?view=efcore-10.0)**<br>*class, used as a base type* | A DbContext instance represents a session with the database and can be used to query and save instances of your entities. | 1 | [Infrastructure/ApplicationDbContext.cs:16](src/Northstar.Infrastructure/Persistence/ApplicationDbContext.cs#L16) | [GitHub](https://github.com/dotnet/efcore/blob/v10.0.15/src/EFCore/DbContext.cs#L48) |
| [`ChangeTracker`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.dbcontext.changetracker?view=efcore-10.0#microsoft-entityframeworkcore-dbcontext-changetracker)<br>*property* | Provides access to information and operations for entity instances this context is tracking. | 1 | [Infrastructure/ApplicationDbContext.cs:46](src/Northstar.Infrastructure/Persistence/ApplicationDbContext.cs#L46) | [GitHub](https://github.com/dotnet/efcore/blob/v10.0.15/src/EFCore/DbContext.cs#L157) |
| [`Database`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.dbcontext.database?view=efcore-10.0#microsoft-entityframeworkcore-dbcontext-database)<br>*property* | Provides access to database related information and operations for this context. | 2 | [Infrastructure/ApplicationDbContext.cs:49](src/Northstar.Infrastructure/Persistence/ApplicationDbContext.cs#L49)<br>[Infrastructure/DatabaseInitializer.cs:19](src/Northstar.Infrastructure/Persistence/DatabaseInitializer.cs#L19) | [GitHub](https://github.com/dotnet/efcore/blob/v10.0.15/src/EFCore/DbContext.cs#L135) |
| [`OnModelCreating(ModelBuilder)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.dbcontext.onmodelcreating?view=efcore-10.0#microsoft-entityframeworkcore-dbcontext-onmodelcreating(microsoft-entityframeworkcore-modelbuilder))<br>*method, overridden here* | Override this method to further configure the model that was discovered by convention from the entity types exposed in DbSet properties on your derived context. | 1 | [Infrastructure/ApplicationDbContext.cs:51](src/Northstar.Infrastructure/Persistence/ApplicationDbContext.cs#L51) | [GitHub](https://github.com/dotnet/efcore/blob/v10.0.15/src/EFCore/DbContext.cs#L561) |
| [`SaveChangesAsync(CancellationToken)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.dbcontext.savechangesasync?view=efcore-10.0#microsoft-entityframeworkcore-dbcontext-savechangesasync(system-threading-cancellationtoken))<br>*method* | Saves all changes made in this context to the database. | 4 | [Infrastructure/DatabaseInitializer.cs:224](src/Northstar.Infrastructure/Persistence/DatabaseInitializer.cs#L224)<br>[IntegrationTests/ImportBatchesApiTests.cs:449](tests/Northstar.Api.IntegrationTests/ImportBatchesApiTests.cs#L449)<br>+2 more | [GitHub](https://github.com/dotnet/efcore/blob/v10.0.15/src/EFCore/DbContext.cs#L731) |
| [`Set<TEntity>()`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.dbcontext.set?view=efcore-10.0#microsoft-entityframeworkcore-dbcontext-set-1)<br>*method* | Creates a DbSet that can be used to query and save instances of TEntity. | 13 | [Infrastructure/ApplicationDbContext.cs:18](src/Northstar.Infrastructure/Persistence/ApplicationDbContext.cs#L18)<br>[Infrastructure/ApplicationDbContext.cs:19](src/Northstar.Infrastructure/Persistence/ApplicationDbContext.cs#L19)<br>+11 more | [GitHub](https://github.com/dotnet/efcore/blob/v10.0.15/src/EFCore/DbContext.cs#L363) |
| **[`DbContextOptions<TContext>`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.dbcontextoptions-1?view=efcore-10.0)**<br>*class* | The options to be used by a DbContext. | 2 | [Infrastructure/ApplicationDbContext.cs:15](src/Northstar.Infrastructure/Persistence/ApplicationDbContext.cs#L15)<br>[IntegrationTests/CustomWebApplicationFactory.cs:39](tests/Northstar.Api.IntegrationTests/CustomWebApplicationFactory.cs#L39) | [GitHub](https://github.com/dotnet/efcore/blob/v10.0.15/src/EFCore/DbContextOptions%60.cs#L17) |
| **[`DbSet<TEntity>`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.dbset-1?view=efcore-10.0)**<br>*abstract class* | A DbSet can be used to query and save instances of TEntity. | 12 | [Infrastructure/ApplicationDbContext.cs:18](src/Northstar.Infrastructure/Persistence/ApplicationDbContext.cs#L18)<br>[Infrastructure/ApplicationDbContext.cs:19](src/Northstar.Infrastructure/Persistence/ApplicationDbContext.cs#L19)<br>+10 more | [GitHub](https://github.com/dotnet/efcore/blob/v10.0.15/src/EFCore/DbSet.cs#L43) |
| [`Add(TEntity)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.dbset-1.add?view=efcore-10.0#microsoft-entityframeworkcore-dbset-1-add(-0))<br>*method* | Begins tracking the given entity, and any other reachable entities that are not already being tracked, in the EntityState.Added state such that they will be inserted… | 1 | [Infrastructure/ApplicationDbContext.cs:44](src/Northstar.Infrastructure/Persistence/ApplicationDbContext.cs#L44) | [GitHub](https://github.com/dotnet/efcore/blob/v10.0.15/src/EFCore/DbSet.cs#L172) |
| [`AddRange(params TEntity[])`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.dbset-1.addrange?view=efcore-10.0#microsoft-entityframeworkcore-dbset-1-addrange(-0()))<br>*method* | Begins tracking the given entities, and any other reachable entities that are not already being tracked, in the EntityState.Added state such that they will be inserted… | 7 | [Infrastructure/DatabaseInitializer.cs:28](src/Northstar.Infrastructure/Persistence/DatabaseInitializer.cs#L28)<br>[Infrastructure/DatabaseInitializer.cs:44](src/Northstar.Infrastructure/Persistence/DatabaseInitializer.cs#L44)<br>+5 more | [GitHub](https://github.com/dotnet/efcore/blob/v10.0.15/src/EFCore/DbSet.cs#L327) |
| **[`DbUpdateConcurrencyException`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.dbupdateconcurrencyexception?view=efcore-10.0)**<br>*class* | An exception that is thrown when a concurrency violation is encountered while saving to the database. | 4 | [Application/ImportBatchProcessor.cs:32](src/Northstar.Application/Imports/ImportBatchProcessor.cs#L32)<br>[Application/ImportBatchService.cs:252](src/Northstar.Application/Imports/ImportBatchService.cs#L252)<br>+2 more | [GitHub](https://github.com/dotnet/efcore/blob/v10.0.15/src/EFCore/DbUpdateConcurrencyException.cs#L18) |
| **[`DbUpdateException`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.dbupdateexception?view=efcore-10.0)**<br>*class* | An exception that is thrown when an error is encountered while saving to the database. | 2 | [Application/ImportBatchService.cs:169](src/Northstar.Application/Imports/ImportBatchService.cs#L169)<br>[Application/ImportBatchService.cs:259](src/Northstar.Application/Imports/ImportBatchService.cs#L259) | [GitHub](https://github.com/dotnet/efcore/blob/v10.0.15/src/EFCore/DbUpdateException.cs#L15) |
| **[`DeleteBehavior`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.deletebehavior?view=efcore-10.0)**<br>*enum* | Indicates how a delete operation is applied to dependent entities in a relationship when the principal is deleted or the relationship is severed. | 4 | [Infrastructure/ApplicationDbContext.cs:160](src/Northstar.Infrastructure/Persistence/ApplicationDbContext.cs#L160)<br>[Infrastructure/ApplicationDbContext.cs:172](src/Northstar.Infrastructure/Persistence/ApplicationDbContext.cs#L172)<br>+2 more | [GitHub](https://github.com/dotnet/efcore/blob/v10.0.15/src/EFCore.Abstractions/DeleteBehavior.cs#L23) |
| [`Cascade`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.deletebehavior?view=efcore-10.0)<br>*enum member* | Automatically deletes dependent entities when the principal is deleted or the relationship to the principal is severed, and creates a foreign key constraint in the… | 3 | [Infrastructure/ApplicationDbContext.cs:172](src/Northstar.Infrastructure/Persistence/ApplicationDbContext.cs#L172)<br>[Infrastructure/ApplicationDbContext.cs:176](src/Northstar.Infrastructure/Persistence/ApplicationDbContext.cs#L176)<br>+1 more | [GitHub](https://github.com/dotnet/efcore/blob/v10.0.15/src/EFCore.Abstractions/DeleteBehavior.cs#L93) |
| [`SetNull`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.deletebehavior?view=efcore-10.0)<br>*enum member* | Sets foreign key values to null as appropriate when changes are made to tracked entities and creates a foreign key constraint in the database that propagates null values… | 1 | [Infrastructure/ApplicationDbContext.cs:160](src/Northstar.Infrastructure/Persistence/ApplicationDbContext.cs#L160) | [GitHub](https://github.com/dotnet/efcore/blob/v10.0.15/src/EFCore.Abstractions/DeleteBehavior.cs#L76) |
| **[`EntityFrameworkQueryableExtensions`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.entityframeworkqueryableextensions?view=efcore-10.0)**<br>*static class* | Entity Framework LINQ related extension methods. | – | through its members | [GitHub](https://github.com/dotnet/efcore/blob/v10.0.15/src/EFCore/Extensions/EntityFrameworkQueryableExtensions.cs#L26) |
| [`AnyAsync<TSource>(this IQueryable<TSource>, CancellationToken)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.entityframeworkqueryableextensions.anyasync?view=efcore-10.0#microsoft-entityframeworkcore-entityframeworkqueryableextensions-anyasync-1(system-linq-iqueryable((-0))-system-threading-cancellationtoken))<br>*extension method* | Asynchronously determines whether a sequence contains any elements. | 1 | [Infrastructure/DatabaseInitializer.cs:21](src/Northstar.Infrastructure/Persistence/DatabaseInitializer.cs#L21) | [GitHub](https://github.com/dotnet/efcore/blob/v10.0.15/src/EFCore/Extensions/EntityFrameworkQueryableExtensions.cs#L75) |
| [`AnyAsync<TSource>(this IQueryable<TSource>, Expression<Func<TSource, bool>>, CancellationToken)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.entityframeworkqueryableextensions.anyasync?view=efcore-10.0#microsoft-entityframeworkcore-entityframeworkqueryableextensions-anyasync-1(system-linq-iqueryable((-0))-system-linq-expressions-expression((system-func((-0-system-boolean))))-system-threading-cancellationtoken))<br>*extension method* | Asynchronously determines whether any element of a sequence satisfies a condition. | 6 | [Application/ImportBatchService.cs:81](src/Northstar.Application/Imports/ImportBatchService.cs#L81)<br>[Application/WageRateService.cs:42](src/Northstar.Application/WageRates/WageRateService.cs#L42)<br>+4 more | [GitHub](https://github.com/dotnet/efcore/blob/v10.0.15/src/EFCore/Extensions/EntityFrameworkQueryableExtensions.cs#L106) |
| [`AsNoTracking<TEntity>(this IQueryable<TEntity>)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.entityframeworkqueryableextensions.asnotracking?view=efcore-10.0#microsoft-entityframeworkcore-entityframeworkqueryableextensions-asnotracking-1(system-linq-iqueryable((-0))))<br>*extension method* | The change tracker will not track any of the entities that are returned from a LINQ query. | 23 | [Application/ImportBatchProcessor.cs:39](src/Northstar.Application/Imports/ImportBatchProcessor.cs#L39)<br>[Application/ImportBatchService.cs:31](src/Northstar.Application/Imports/ImportBatchService.cs#L31)<br>+21 more | [GitHub](https://github.com/dotnet/efcore/blob/v10.0.15/src/EFCore/Extensions/EntityFrameworkQueryableExtensions.cs#L2755) |
| [`CountAsync<TSource>(this IQueryable<TSource>, Expression<Func<TSource, bool>>, CancellationToken)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.entityframeworkqueryableextensions.countasync?view=efcore-10.0#microsoft-entityframeworkcore-entityframeworkqueryableextensions-countasync-1(system-linq-iqueryable((-0))-system-linq-expressions-expression((system-func((-0-system-boolean))))-system-threading-cancellationtoken))<br>*extension method* | Asynchronously returns the number of elements in a sequence that satisfy a condition. | 2 | [IntegrationTests/ImportBatchesApiTests.cs:410](tests/Northstar.Api.IntegrationTests/ImportBatchesApiTests.cs#L410)<br>[IntegrationTests/ImportBatchesApiTests.cs:421](tests/Northstar.Api.IntegrationTests/ImportBatchesApiTests.cs#L421) | [GitHub](https://github.com/dotnet/efcore/blob/v10.0.15/src/EFCore/Extensions/EntityFrameworkQueryableExtensions.cs#L209) |
| [`Include<TEntity, TProperty>(this IQueryable<TEntity>, Expression<Func<TEntity, TProperty>>)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.entityframeworkqueryableextensions.include?view=efcore-10.0#microsoft-entityframeworkcore-entityframeworkqueryableextensions-include-2(system-linq-iqueryable((-0))-system-linq-expressions-expression((system-func((-0-1))))))<br>*extension method* | Specifies related entities to include in the query results. | 4 | [Application/PayrollRunService.cs:24](src/Northstar.Application/PayrollRuns/PayrollRunService.cs#L24)<br>[Application/PayrollRunService.cs:25](src/Northstar.Application/PayrollRuns/PayrollRunService.cs#L25)<br>+2 more | [GitHub](https://github.com/dotnet/efcore/blob/v10.0.15/src/EFCore/Extensions/EntityFrameworkQueryableExtensions.cs#L2466) |
| [`SingleAsync<TSource>(this IQueryable<TSource>, Expression<Func<TSource, bool>>, CancellationToken)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.entityframeworkqueryableextensions.singleasync?view=efcore-10.0#microsoft-entityframeworkcore-entityframeworkqueryableextensions-singleasync-1(system-linq-iqueryable((-0))-system-linq-expressions-expression((system-func((-0-system-boolean))))-system-threading-cancellationtoken))<br>*extension method* | Asynchronously returns the only element of a sequence that satisfies a specified condition, and throws an exception if more than one such element exists. | 3 | [IntegrationTests/ImportBatchesApiTests.cs:447](tests/Northstar.Api.IntegrationTests/ImportBatchesApiTests.cs#L447)<br>[IntegrationTests/ImportBatchesApiTests.cs:459](tests/Northstar.Api.IntegrationTests/ImportBatchesApiTests.cs#L459)<br>+1 more | [GitHub](https://github.com/dotnet/efcore/blob/v10.0.15/src/EFCore/Extensions/EntityFrameworkQueryableExtensions.cs#L742) |
| [`SingleOrDefaultAsync<TSource>(this IQueryable<TSource>, CancellationToken)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.entityframeworkqueryableextensions.singleordefaultasync?view=efcore-10.0#microsoft-entityframeworkcore-entityframeworkqueryableextensions-singleordefaultasync-1(system-linq-iqueryable((-0))-system-threading-cancellationtoken))<br>*extension method* | Asynchronously returns the only element of a sequence, or a default value if the sequence is empty; this method throws an exception if there is more than one element in… | 1 | [Application/PayrollRunService.cs:232](src/Northstar.Application/PayrollRuns/PayrollRunService.cs#L232) | [GitHub](https://github.com/dotnet/efcore/blob/v10.0.15/src/EFCore/Extensions/EntityFrameworkQueryableExtensions.cs#L778) |
| [`SingleOrDefaultAsync<TSource>(this IQueryable<TSource>, Expression<Func<TSource, bool>>, CancellationToken)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.entityframeworkqueryableextensions.singleordefaultasync?view=efcore-10.0#microsoft-entityframeworkcore-entityframeworkqueryableextensions-singleordefaultasync-1(system-linq-iqueryable((-0))-system-linq-expressions-expression((system-func((-0-system-boolean))))-system-threading-cancellationtoken))<br>*extension method* | Asynchronously returns the only element of a sequence that satisfies a specified condition or a default value if no such element exists; this method throws an exception… | 8 | [Application/ImportBatchProcessor.cs:20](src/Northstar.Application/Imports/ImportBatchProcessor.cs#L20)<br>[Application/ImportBatchService.cs:65](src/Northstar.Application/Imports/ImportBatchService.cs#L65)<br>+6 more | [GitHub](https://github.com/dotnet/efcore/blob/v10.0.15/src/EFCore/Extensions/EntityFrameworkQueryableExtensions.cs#L814) |
| [`ToListAsync<TSource>(this IQueryable<TSource>, CancellationToken)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.entityframeworkqueryableextensions.tolistasync?view=efcore-10.0#microsoft-entityframeworkcore-entityframeworkqueryableextensions-tolistasync-1(system-linq-iqueryable((-0))-system-threading-cancellationtoken))<br>*extension method* | Asynchronously creates a List from an IQueryable by enumerating it asynchronously. | 12 | [Application/ImportBatchProcessor.cs:87](src/Northstar.Application/Imports/ImportBatchProcessor.cs#L87)<br>[Application/ImportBatchService.cs:51](src/Northstar.Application/Imports/ImportBatchService.cs#L51)<br>+10 more | [GitHub](https://github.com/dotnet/efcore/blob/v10.0.15/src/EFCore/Extensions/EntityFrameworkQueryableExtensions.cs#L2305) |
| **[`ModelBuilder`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.modelbuilder?view=efcore-10.0)**<br>*class* | Provides a simple API surface for configuring a IMutableModel that defines the shape of your entities, the relationships between them, and how they map to the database. | 13 | [Infrastructure/ApplicationDbContext.cs:51](src/Northstar.Infrastructure/Persistence/ApplicationDbContext.cs#L51)<br>[Infrastructure/ApplicationDbContext.cs:67](src/Northstar.Infrastructure/Persistence/ApplicationDbContext.cs#L67)<br>+11 more | [GitHub](https://github.com/dotnet/efcore/blob/v10.0.15/src/EFCore/ModelBuilder.cs#L24) |
| [`Entity<TEntity>()`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.modelbuilder.entity?view=efcore-10.0#microsoft-entityframeworkcore-modelbuilder-entity-1)<br>*method* | Returns an object that can be used to configure a given entity type in the model. | 12 | [Infrastructure/ApplicationDbContext.cs:69](src/Northstar.Infrastructure/Persistence/ApplicationDbContext.cs#L69)<br>[Infrastructure/ApplicationDbContext.cs:79](src/Northstar.Infrastructure/Persistence/ApplicationDbContext.cs#L79)<br>+10 more | [GitHub](https://github.com/dotnet/efcore/blob/v10.0.15/src/EFCore/ModelBuilder.cs) |
| **[`QuerySplittingBehavior`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.querysplittingbehavior?view=efcore-10.0)**<br>*enum* | Indicates how the related collections in a query should be loaded from database. | 3 | [Infrastructure/DependencyInjection.cs:27](src/Northstar.Infrastructure/DependencyInjection.cs#L27)<br>[IntegrationTests/CustomWebApplicationFactory.cs:46](tests/Northstar.Api.IntegrationTests/CustomWebApplicationFactory.cs#L46)<br>+1 more | [GitHub](https://github.com/dotnet/efcore/blob/v10.0.15/src/EFCore.Relational/QuerySplittingBehavior.cs#L12) |
| [`SplitQuery`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.querysplittingbehavior?view=efcore-10.0)<br>*enum member* | The related collections will be loaded in separate database queries from the parent query. | 3 | [Infrastructure/DependencyInjection.cs:27](src/Northstar.Infrastructure/DependencyInjection.cs#L27)<br>[IntegrationTests/CustomWebApplicationFactory.cs:46](tests/Northstar.Api.IntegrationTests/CustomWebApplicationFactory.cs#L46)<br>+1 more | [GitHub](https://github.com/dotnet/efcore/blob/v10.0.15/src/EFCore.Relational/QuerySplittingBehavior.cs#L33) |
| **[`RelationalEntityTypeBuilderExtensions`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.relationalentitytypebuilderextensions?view=efcore-10.0)**<br>*static class* | Relational database specific extension methods for EntityTypeBuilder. | – | through its members | [GitHub](https://github.com/dotnet/efcore/blob/v10.0.15/src/EFCore.Relational/Extensions/RelationalEntityTypeBuilderExtensions.cs#L16) |
| [`ToTable<TEntity>(this EntityTypeBuilder<TEntity>, Action<TableBuilder<TEntity>>)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.relationalentitytypebuilderextensions.totable?view=efcore-10.0#microsoft-entityframeworkcore-relationalentitytypebuilderextensions-totable-1(microsoft-entityframeworkcore-metadata-builders-entitytypebuilder((-0))-system-action((microsoft-entityframeworkcore-metadata-builders-tablebuilder((-0))))))<br>*extension method* | Configures the table that the entity type maps to when targeting a relational database. | 3 | [Infrastructure/ApplicationDbContext.cs:125](src/Northstar.Infrastructure/Persistence/ApplicationDbContext.cs#L125)<br>[Infrastructure/ApplicationDbContext.cs:153](src/Northstar.Infrastructure/Persistence/ApplicationDbContext.cs#L153)<br>+1 more | [GitHub](https://github.com/dotnet/efcore/blob/v10.0.15/src/EFCore.Relational/Extensions/RelationalEntityTypeBuilderExtensions.cs) |
| **[`SqliteDbContextOptionsBuilderExtensions`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.sqlitedbcontextoptionsbuilderextensions?view=efcore-10.0)**<br>*static class* | SQLite specific extension methods for DbContextOptionsBuilder. | – | through its members | [GitHub](https://github.com/dotnet/efcore/blob/v10.0.15/src/EFCore.Sqlite.Core/Extensions/SqliteDbContextOptionsBuilderExtensions.cs#L16) |
| [`UseSqlite(this DbContextOptionsBuilder, string, Action<SqliteDbContextOptionsBuilder>)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.sqlitedbcontextoptionsbuilderextensions.usesqlite?view=efcore-10.0#microsoft-entityframeworkcore-sqlitedbcontextoptionsbuilderextensions-usesqlite(microsoft-entityframeworkcore-dbcontextoptionsbuilder-system-string-system-action((microsoft-entityframeworkcore-infrastructure-sqlitedbcontextoptionsbuilder))))<br>*extension method* | Configures the context to connect to a SQLite database. | 2 | [Infrastructure/DependencyInjection.cs:32](src/Northstar.Infrastructure/DependencyInjection.cs#L32)<br>[IntegrationTests/CustomWebApplicationFactory.cs:44](tests/Northstar.Api.IntegrationTests/CustomWebApplicationFactory.cs#L44) | [GitHub](https://github.com/dotnet/efcore/blob/v10.0.15/src/EFCore.Sqlite.Core/Extensions/SqliteDbContextOptionsBuilderExtensions.cs#L60) |
| **[`SqlServerDbContextOptionsExtensions`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.sqlserverdbcontextoptionsextensions?view=efcore-10.0)**<br>*static class* | SQL Server specific extension methods for DbContextOptionsBuilder. | – | through its members | [GitHub](https://github.com/dotnet/efcore/blob/v10.0.15/src/EFCore.SqlServer/Extensions/SqlServerDbContextOptionsBuilderExtensions.cs#L17) |
| [`UseSqlServer(this DbContextOptionsBuilder, string, Action<SqlServerDbContextOptionsBuilder>)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.sqlserverdbcontextoptionsextensions.usesqlserver?view=efcore-10.0#microsoft-entityframeworkcore-sqlserverdbcontextoptionsextensions-usesqlserver(microsoft-entityframeworkcore-dbcontextoptionsbuilder-system-string-system-action((microsoft-entityframeworkcore-infrastructure-sqlserverdbcontextoptionsbuilder))))<br>*extension method* | Configures the context to connect to a SQL Server database. | 1 | [Infrastructure/DependencyInjection.cs:25](src/Northstar.Infrastructure/DependencyInjection.cs#L25) | [GitHub](https://github.com/dotnet/efcore/blob/v10.0.15/src/EFCore.SqlServer/Extensions/SqlServerDbContextOptionsBuilderExtensions.cs#L61) |

#### Microsoft.EntityFrameworkCore.ChangeTracking

*2 APIs · 1 uses · [namespace page on Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.changetracking?view=efcore-10.0)*

| API | What it does | Uses | Where | Source |
| --- | --- | ---: | --- | --- |
| **[`ChangeTracker`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.changetracking.changetracker?view=efcore-10.0)**<br>*class* | Provides access to change tracking information and operations for entity instances the context is tracking. | – | through its members | [GitHub](https://github.com/dotnet/efcore/blob/v10.0.15/src/EFCore/ChangeTracking/ChangeTracker.cs#L18) |
| [`Clear()`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.changetracking.changetracker.clear?view=efcore-10.0#microsoft-entityframeworkcore-changetracking-changetracker-clear)<br>*method* | Stops tracking all currently tracked entities. | 1 | [Infrastructure/ApplicationDbContext.cs:46](src/Northstar.Infrastructure/Persistence/ApplicationDbContext.cs#L46) | [GitHub](https://github.com/dotnet/efcore/blob/v10.0.15/src/EFCore/ChangeTracking/ChangeTracker.cs#L609) |

#### Microsoft.EntityFrameworkCore.Infrastructure

*5 APIs · 5 uses · [namespace page on Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.infrastructure?view=efcore-10.0)*

| API | What it does | Uses | Where | Source |
| --- | --- | ---: | --- | --- |
| **[`DatabaseFacade`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.infrastructure.databasefacade?view=efcore-10.0)**<br>*class* | Provides access to database related information and operations for a context. | – | through its members | [GitHub](https://github.com/dotnet/efcore/blob/v10.0.15/src/EFCore/Infrastructure/DatabaseFacade.cs#L14) |
| [`BeginTransactionAsync(CancellationToken)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.infrastructure.databasefacade.begintransactionasync?view=efcore-10.0#microsoft-entityframeworkcore-infrastructure-databasefacade-begintransactionasync(system-threading-cancellationtoken))<br>*method* | Asynchronously starts a new transaction. | 1 | [Infrastructure/ApplicationDbContext.cs:49](src/Northstar.Infrastructure/Persistence/ApplicationDbContext.cs#L49) | [GitHub](https://github.com/dotnet/efcore/blob/v10.0.15/src/EFCore/Infrastructure/DatabaseFacade.cs#L293) |
| [`EnsureCreatedAsync(CancellationToken)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.infrastructure.databasefacade.ensurecreatedasync?view=efcore-10.0#microsoft-entityframeworkcore-infrastructure-databasefacade-ensurecreatedasync(system-threading-cancellationtoken))<br>*method* | Ensures that the database for the context exists. | 1 | [Infrastructure/DatabaseInitializer.cs:19](src/Northstar.Infrastructure/Persistence/DatabaseInitializer.cs#L19) | [GitHub](https://github.com/dotnet/efcore/blob/v10.0.15/src/EFCore/Infrastructure/DatabaseFacade.cs#L134) |
| **[`RelationalDbContextOptionsBuilder<TBuilder, TExtension>`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.infrastructure.relationaldbcontextoptionsbuilder-2?view=efcore-10.0)**<br>*abstract class* | Allows relational database specific configuration to be performed on DbContextOptions. | – | through its members | [GitHub](https://github.com/dotnet/efcore/blob/v10.0.15/src/EFCore.Relational/Infrastructure/RelationalDbContextOptionsBuilder.cs#L20) |
| [`UseQuerySplittingBehavior(QuerySplittingBehavior)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.infrastructure.relationaldbcontextoptionsbuilder-2.usequerysplittingbehavior?view=efcore-10.0#microsoft-entityframeworkcore-infrastructure-relationaldbcontextoptionsbuilder-2-usequerysplittingbehavior(microsoft-entityframeworkcore-querysplittingbehavior))<br>*method* | Configures the QuerySplittingBehavior to use when loading related collections in a query. | 3 | [Infrastructure/DependencyInjection.cs:27](src/Northstar.Infrastructure/DependencyInjection.cs#L27)<br>[IntegrationTests/CustomWebApplicationFactory.cs:46](tests/Northstar.Api.IntegrationTests/CustomWebApplicationFactory.cs#L46)<br>+1 more | [GitHub](https://github.com/dotnet/efcore/blob/v10.0.15/src/EFCore.Relational/Infrastructure/RelationalDbContextOptionsBuilder.cs#L144) |

#### Microsoft.EntityFrameworkCore.Metadata.Builders

*26 APIs · 215 uses · [namespace page on Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.metadata.builders?view=efcore-10.0)*

| API | What it does | Uses | Where | Source |
| --- | --- | ---: | --- | --- |
| **[`CollectionNavigationBuilder<TEntity, TRelatedEntity>`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.metadata.builders.collectionnavigationbuilder-2?view=efcore-10.0)**<br>*class* | Provides a simple API for configuring a relationship where configuration began on an end of the relationship with a collection that contains instances of another entity… | – | through its members | [GitHub](https://github.com/dotnet/efcore/blob/v10.0.15/src/EFCore/Metadata/Builders/CollectionNavigationBuilder%60.cs#L19) |
| [`WithOne(string)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.metadata.builders.collectionnavigationbuilder-2.withone?view=efcore-10.0#microsoft-entityframeworkcore-metadata-builders-collectionnavigationbuilder-2-withone(system-string))<br>*method* | Configures this as a one-to-many relationship. | 3 | [Infrastructure/ApplicationDbContext.cs:170](src/Northstar.Infrastructure/Persistence/ApplicationDbContext.cs#L170)<br>[Infrastructure/ApplicationDbContext.cs:174](src/Northstar.Infrastructure/Persistence/ApplicationDbContext.cs#L174)<br>+1 more | [GitHub](https://github.com/dotnet/efcore/blob/v10.0.15/src/EFCore/Metadata/Builders/CollectionNavigationBuilder%60.cs#L50) |
| **[`EntityTypeBuilder<TEntity>`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.metadata.builders.entitytypebuilder-1?view=efcore-10.0)**<br>*class* | Provides a simple API for configuring an IMutableEntityType. | – | through its members | [GitHub](https://github.com/dotnet/efcore/blob/v10.0.15/src/EFCore/Metadata/Builders/EntityTypeBuilder%60.cs#L19) |
| [`HasAlternateKey(Expression<Func<TEntity, object>>)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.metadata.builders.entitytypebuilder-1.hasalternatekey?view=efcore-10.0#microsoft-entityframeworkcore-metadata-builders-entitytypebuilder-1-hasalternatekey(system-linq-expressions-expression((system-func((-0-system-object))))))<br>*method* | Creates an alternate key in the model for this entity type if one does not already exist over the specified properties. | 1 | [Infrastructure/ApplicationDbContext.cs:226](src/Northstar.Infrastructure/Persistence/ApplicationDbContext.cs#L226) | [GitHub](https://github.com/dotnet/efcore/blob/v10.0.15/src/EFCore/Metadata/Builders/EntityTypeBuilder%60.cs#L113) |
| [`HasIndex(Expression<Func<TEntity, object>>)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.metadata.builders.entitytypebuilder-1.hasindex?view=efcore-10.0#microsoft-entityframeworkcore-metadata-builders-entitytypebuilder-1-hasindex(system-linq-expressions-expression((system-func((-0-system-object))))))<br>*method* | Configures an unnamed index on the specified properties. | 13 | [Infrastructure/ApplicationDbContext.cs:73](src/Northstar.Infrastructure/Persistence/ApplicationDbContext.cs#L73)<br>[Infrastructure/ApplicationDbContext.cs:87](src/Northstar.Infrastructure/Persistence/ApplicationDbContext.cs#L87)<br>+11 more | [GitHub](https://github.com/dotnet/efcore/blob/v10.0.15/src/EFCore/Metadata/Builders/EntityTypeBuilder%60.cs#L844) |
| [`HasKey(Expression<Func<TEntity, object>>)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.metadata.builders.entitytypebuilder-1.haskey?view=efcore-10.0#microsoft-entityframeworkcore-metadata-builders-entitytypebuilder-1-haskey(system-linq-expressions-expression((system-func((-0-system-object))))))<br>*method* | Sets the properties that make up the primary key for this entity type. | 12 | [Infrastructure/ApplicationDbContext.cs:70](src/Northstar.Infrastructure/Persistence/ApplicationDbContext.cs#L70)<br>[Infrastructure/ApplicationDbContext.cs:80](src/Northstar.Infrastructure/Persistence/ApplicationDbContext.cs#L80)<br>+10 more | [GitHub](https://github.com/dotnet/efcore/blob/v10.0.15/src/EFCore/Metadata/Builders/EntityTypeBuilder%60.cs#L81) |
| [`HasMany<TRelatedEntity>(Expression<Func<TEntity, IEnumerable<TRelatedEntity>>>)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.metadata.builders.entitytypebuilder-1.hasmany?view=efcore-10.0#microsoft-entityframeworkcore-metadata-builders-entitytypebuilder-1-hasmany-1(system-linq-expressions-expression((system-func((-0-system-collections-generic-ienumerable((-0))))))))<br>*method* | Configures a relationship where this entity type has a collection that contains instances of the other type in the relationship. | 3 | [Infrastructure/ApplicationDbContext.cs:169](src/Northstar.Infrastructure/Persistence/ApplicationDbContext.cs#L169)<br>[Infrastructure/ApplicationDbContext.cs:173](src/Northstar.Infrastructure/Persistence/ApplicationDbContext.cs#L173)<br>+1 more | [GitHub](https://github.com/dotnet/efcore/blob/v10.0.15/src/EFCore/Metadata/Builders/EntityTypeBuilder%60.cs#L1857) |
| [`HasOne<TRelatedEntity>(Expression<Func<TEntity, TRelatedEntity>>)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.metadata.builders.entitytypebuilder-1.hasone?view=efcore-10.0#microsoft-entityframeworkcore-metadata-builders-entitytypebuilder-1-hasone-1(system-linq-expressions-expression((system-func((-0-0))))))<br>*method* | Configures a relationship where this entity type has a reference that points to a single instance of the other type in the relationship. | 1 | [Infrastructure/ApplicationDbContext.cs:157](src/Northstar.Infrastructure/Persistence/ApplicationDbContext.cs#L157) | [GitHub](https://github.com/dotnet/efcore/blob/v10.0.15/src/EFCore/Metadata/Builders/EntityTypeBuilder%60.cs#L1761) |
| [`Property<TProperty>(Expression<Func<TEntity, TProperty>>)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.metadata.builders.entitytypebuilder-1.property?view=efcore-10.0#microsoft-entityframeworkcore-metadata-builders-entitytypebuilder-1-property-1(system-linq-expressions-expression((system-func((-0-0))))))<br>*method* | Returns an object that can be used to configure a property of the entity type. | 64 | [Infrastructure/ApplicationDbContext.cs:71](src/Northstar.Infrastructure/Persistence/ApplicationDbContext.cs#L71)<br>[Infrastructure/ApplicationDbContext.cs:72](src/Northstar.Infrastructure/Persistence/ApplicationDbContext.cs#L72)<br>+62 more | [GitHub](https://github.com/dotnet/efcore/blob/v10.0.15/src/EFCore/Metadata/Builders/EntityTypeBuilder%60.cs#L148) |
| **[`IndexBuilder<T>`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.metadata.builders.indexbuilder-1?view=efcore-10.0)**<br>*class* | Provides a simple API for configuring an IMutableIndex. | – | through its members | [GitHub](https://github.com/dotnet/efcore/blob/v10.0.15/src/EFCore/Metadata/Builders/IndexBuilder%60.cs#L20) |
| [`IsUnique(bool)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.metadata.builders.indexbuilder-1.isunique?view=efcore-10.0#microsoft-entityframeworkcore-metadata-builders-indexbuilder-1-isunique(system-boolean))<br>*method* | Configures whether this index is unique (i.e. the value(s) for each instance must be unique). | 7 | [Infrastructure/ApplicationDbContext.cs:73](src/Northstar.Infrastructure/Persistence/ApplicationDbContext.cs#L73)<br>[Infrastructure/ApplicationDbContext.cs:87](src/Northstar.Infrastructure/Persistence/ApplicationDbContext.cs#L87)<br>+5 more | [GitHub](https://github.com/dotnet/efcore/blob/v10.0.15/src/EFCore/Metadata/Builders/IndexBuilder%60.cs#L50) |
| **[`PropertyBuilder<TProperty>`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.metadata.builders.propertybuilder-1?view=efcore-10.0)**<br>*class* | Provides a simple API for configuring a IMutableProperty. | – | through its members | [GitHub](https://github.com/dotnet/efcore/blob/v10.0.15/src/EFCore/Metadata/Builders/PropertyBuilder%60.cs#L19) |
| [`HasConversion<TConversion>()`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.metadata.builders.propertybuilder-1.hasconversion?view=efcore-10.0#microsoft-entityframeworkcore-metadata-builders-propertybuilder-1-hasconversion-1)<br>*method* | Configures the property so that the property value is converted before writing to the database and converted back when reading from the database. | 5 | [Infrastructure/ApplicationDbContext.cs:118](src/Northstar.Infrastructure/Persistence/ApplicationDbContext.cs#L118)<br>[Infrastructure/ApplicationDbContext.cs:167](src/Northstar.Infrastructure/Persistence/ApplicationDbContext.cs#L167)<br>+3 more | [GitHub](https://github.com/dotnet/efcore/blob/v10.0.15/src/EFCore/Metadata/Builders/PropertyBuilder%60.cs#L370) |
| [`HasConversion<TProvider>(Expression<Func<TProperty, TProvider>>, Expression<Func<TProvider, TProperty>>)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.metadata.builders.propertybuilder-1.hasconversion?view=efcore-10.0#microsoft-entityframeworkcore-metadata-builders-propertybuilder-1-hasconversion-1(system-linq-expressions-expression((system-func((-0-0))))-system-linq-expressions-expression((system-func((-0-0))))))<br>*method* | Configures the property so that the property value is converted to and from the database using the given conversion expressions. | 1 | [Infrastructure/ApplicationDbContext.cs:145](src/Northstar.Infrastructure/Persistence/ApplicationDbContext.cs#L145) | [GitHub](https://github.com/dotnet/efcore/blob/v10.0.15/src/EFCore/Metadata/Builders/PropertyBuilder%60.cs#L393) |
| [`HasMaxLength(int)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.metadata.builders.propertybuilder-1.hasmaxlength?view=efcore-10.0#microsoft-entityframeworkcore-metadata-builders-propertybuilder-1-hasmaxlength(system-int32))<br>*method* | Configures the maximum length of data that can be stored in this property. | 42 | [Infrastructure/ApplicationDbContext.cs:71](src/Northstar.Infrastructure/Persistence/ApplicationDbContext.cs#L71)<br>[Infrastructure/ApplicationDbContext.cs:72](src/Northstar.Infrastructure/Persistence/ApplicationDbContext.cs#L72)<br>+40 more | [GitHub](https://github.com/dotnet/efcore/blob/v10.0.15/src/EFCore/Metadata/Builders/PropertyBuilder%60.cs#L61) |
| [`HasPrecision(int, int)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.metadata.builders.propertybuilder-1.hasprecision?view=efcore-10.0#microsoft-entityframeworkcore-metadata-builders-propertybuilder-1-hasprecision(system-int32-system-int32))<br>*method* | Configures the precision and scale of the property. | 16 | [Infrastructure/ApplicationDbContext.cs:84](src/Northstar.Infrastructure/Persistence/ApplicationDbContext.cs#L84)<br>[Infrastructure/ApplicationDbContext.cs:85](src/Northstar.Infrastructure/Persistence/ApplicationDbContext.cs#L85)<br>+14 more | [GitHub](https://github.com/dotnet/efcore/blob/v10.0.15/src/EFCore/Metadata/Builders/PropertyBuilder%60.cs#L90) |
| [`IsConcurrencyToken(bool)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.metadata.builders.propertybuilder-1.isconcurrencytoken?view=efcore-10.0#microsoft-entityframeworkcore-metadata-builders-propertybuilder-1-isconcurrencytoken(system-boolean))<br>*method* | Configures whether this property should be used as a concurrency token. | 6 | [Infrastructure/ApplicationDbContext.cs:74](src/Northstar.Infrastructure/Persistence/ApplicationDbContext.cs#L74)<br>[Infrastructure/ApplicationDbContext.cs:86](src/Northstar.Infrastructure/Persistence/ApplicationDbContext.cs#L86)<br>+4 more | [GitHub](https://github.com/dotnet/efcore/blob/v10.0.15/src/EFCore/Metadata/Builders/PropertyBuilder%60.cs#L274) |
| [`IsRequired(bool)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.metadata.builders.propertybuilder-1.isrequired?view=efcore-10.0#microsoft-entityframeworkcore-metadata-builders-propertybuilder-1-isrequired(system-boolean))<br>*method* | Configures whether this property must have a value assigned or whether null is a valid value. | 27 | [Infrastructure/ApplicationDbContext.cs:71](src/Northstar.Infrastructure/Persistence/ApplicationDbContext.cs#L71)<br>[Infrastructure/ApplicationDbContext.cs:72](src/Northstar.Infrastructure/Persistence/ApplicationDbContext.cs#L72)<br>+25 more | [GitHub](https://github.com/dotnet/efcore/blob/v10.0.15/src/EFCore/Metadata/Builders/PropertyBuilder%60.cs#L50) |
| **[`ReferenceCollectionBuilder<TPrincipalEntity, TDependentEntity>`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.metadata.builders.referencecollectionbuilder-2?view=efcore-10.0)**<br>*class* | Provides a simple API for configuring a one-to-many relationship. | – | through its members | [GitHub](https://github.com/dotnet/efcore/blob/v10.0.15/src/EFCore/Metadata/Builders/ReferenceCollectionBuilder%60.cs#L23) |
| [`HasForeignKey(Expression<Func<TDependentEntity, object>>)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.metadata.builders.referencecollectionbuilder-2.hasforeignkey?view=efcore-10.0#microsoft-entityframeworkcore-metadata-builders-referencecollectionbuilder-2-hasforeignkey(system-linq-expressions-expression((system-func((-1-system-object))))))<br>*method* | Configures the property(s) to use as the foreign key for this relationship. | 4 | [Infrastructure/ApplicationDbContext.cs:159](src/Northstar.Infrastructure/Persistence/ApplicationDbContext.cs#L159)<br>[Infrastructure/ApplicationDbContext.cs:171](src/Northstar.Infrastructure/Persistence/ApplicationDbContext.cs#L171)<br>+2 more | [GitHub](https://github.com/dotnet/efcore/blob/v10.0.15/src/EFCore/Metadata/Builders/ReferenceCollectionBuilder%60.cs#L125) |
| [`HasPrincipalKey(Expression<Func<TPrincipalEntity, object>>)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.metadata.builders.referencecollectionbuilder-2.hasprincipalkey?view=efcore-10.0#microsoft-entityframeworkcore-metadata-builders-referencecollectionbuilder-2-hasprincipalkey(system-linq-expressions-expression((system-func((-0-system-object))))))<br>*method* | Configures the unique property(s) that this relationship targets. | 1 | [Infrastructure/ApplicationDbContext.cs:230](src/Northstar.Infrastructure/Persistence/ApplicationDbContext.cs#L230) | [GitHub](https://github.com/dotnet/efcore/blob/v10.0.15/src/EFCore/Metadata/Builders/ReferenceCollectionBuilder%60.cs#L164) |
| [`OnDelete(DeleteBehavior)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.metadata.builders.referencecollectionbuilder-2.ondelete?view=efcore-10.0#microsoft-entityframeworkcore-metadata-builders-referencecollectionbuilder-2-ondelete(microsoft-entityframeworkcore-deletebehavior))<br>*method* | Configures the operation applied to dependent entities in the relationship when the principal is deleted or the relationship is severed. | 4 | [Infrastructure/ApplicationDbContext.cs:160](src/Northstar.Infrastructure/Persistence/ApplicationDbContext.cs#L160)<br>[Infrastructure/ApplicationDbContext.cs:172](src/Northstar.Infrastructure/Persistence/ApplicationDbContext.cs#L172)<br>+2 more | [GitHub](https://github.com/dotnet/efcore/blob/v10.0.15/src/EFCore/Metadata/Builders/ReferenceCollectionBuilder%60.cs#L186) |
| **[`ReferenceNavigationBuilder<TEntity, TRelatedEntity>`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.metadata.builders.referencenavigationbuilder-2?view=efcore-10.0)**<br>*class* | Provides a simple API for configuring a relationship where configuration began on an end of the relationship with a reference that points to an instance of another… | – | through its members | [GitHub](https://github.com/dotnet/efcore/blob/v10.0.15/src/EFCore/Metadata/Builders/ReferenceNavigationBuilder%60.cs#L22) |
| [`WithMany(string)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.metadata.builders.referencenavigationbuilder-2.withmany?view=efcore-10.0#microsoft-entityframeworkcore-metadata-builders-referencenavigationbuilder-2-withmany(system-string))<br>*method* | Configures this as a one-to-many relationship. | 1 | [Infrastructure/ApplicationDbContext.cs:158](src/Northstar.Infrastructure/Persistence/ApplicationDbContext.cs#L158) | [GitHub](https://github.com/dotnet/efcore/blob/v10.0.15/src/EFCore/Metadata/Builders/ReferenceNavigationBuilder%60.cs#L71) |
| **[`TableBuilder`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.metadata.builders.tablebuilder?view=efcore-10.0)**<br>*class* | Instances of this class are returned from methods when using the ModelBuilder API and it is not designed to be directly constructed in your application code. | – | through its members | [GitHub](https://github.com/dotnet/efcore/blob/v10.0.15/src/EFCore.Relational/Metadata/Builders/TableBuilder.cs#L13) |
| [`HasCheckConstraint(string, string)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.metadata.builders.tablebuilder.hascheckconstraint?view=efcore-10.0#microsoft-entityframeworkcore-metadata-builders-tablebuilder-hascheckconstraint(system-string-system-string))<br>*method* | Configures a database check constraint when targeting a relational database. | 4 | [Infrastructure/ApplicationDbContext.cs:127](src/Northstar.Infrastructure/Persistence/ApplicationDbContext.cs#L127)<br>[Infrastructure/ApplicationDbContext.cs:128](src/Northstar.Infrastructure/Persistence/ApplicationDbContext.cs#L128)<br>+2 more | [GitHub](https://github.com/dotnet/efcore/blob/v10.0.15/src/EFCore.Relational/Metadata/Builders/TableBuilder.cs#L101) |

#### Microsoft.EntityFrameworkCore.Storage

*3 APIs · 3 uses · [namespace page on Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.storage?view=efcore-10.0)*

| API | What it does | Uses | Where | Source |
| --- | --- | ---: | --- | --- |
| **[`IDbContextTransaction`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.storage.idbcontexttransaction?view=efcore-10.0)**<br>*interface* | A transaction against the database. | 1 | [Infrastructure/ApplicationDbContext.cs:257](src/Northstar.Infrastructure/Persistence/ApplicationDbContext.cs#L257) | [GitHub](https://github.com/dotnet/efcore/blob/v10.0.15/src/EFCore/Storage/IDbContextTransaction.cs#L18) |
| [`CommitAsync(CancellationToken)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.storage.idbcontexttransaction.commitasync?view=efcore-10.0#microsoft-entityframeworkcore-storage-idbcontexttransaction-commitasync(system-threading-cancellationtoken))<br>*method* | Commits all changes made to the database in the current transaction asynchronously. | 1 | [Infrastructure/ApplicationDbContext.cs:259](src/Northstar.Infrastructure/Persistence/ApplicationDbContext.cs#L259) | [GitHub](https://github.com/dotnet/efcore/blob/v10.0.15/src/EFCore/Storage/IDbContextTransaction.cs#L45) |
| [`RollbackAsync(CancellationToken)`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.storage.idbcontexttransaction.rollbackasync?view=efcore-10.0#microsoft-entityframeworkcore-storage-idbcontexttransaction-rollbackasync(system-threading-cancellationtoken))<br>*method* | Discards all changes made to the database in the current transaction asynchronously. | 1 | [Infrastructure/ApplicationDbContext.cs:261](src/Northstar.Infrastructure/Persistence/ApplicationDbContext.cs#L261) | [GitHub](https://github.com/dotnet/efcore/blob/v10.0.15/src/EFCore/Storage/IDbContextTransaction.cs#L64) |

### xUnit test framework

*1 namespace, 23 APIs.*

#### Xunit

*23 APIs · 528 uses*

| API | What it does | Uses | Where | Source |
| --- | --- | ---: | --- | --- |
| **`Assert`**<br>*class* | Contains various static methods that are used to verify that conditions are met during the process of running tests. | 239 | [IntegrationTests/FakePayrollProviderClientTests.cs:25](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L25)<br>[IntegrationTests/ImportBatchesApiTests.cs:24](tests/Northstar.Api.IntegrationTests/ImportBatchesApiTests.cs#L24)<br>+237 more | [GitHub](https://github.com/xunit/assert.xunit/blob/v2-2.9.3/Assert.cs#L27) |
| `All<T>(IEnumerable<T>, Action<T>)`<br>*static method* | Verifies that all items in the collection pass when executed against action. | 2 | [IntegrationTests/ImportBatchesApiTests.cs:187](tests/Northstar.Api.IntegrationTests/ImportBatchesApiTests.cs#L187)<br>[IntegrationTests/ImportBatchesApiTests.cs:218](tests/Northstar.Api.IntegrationTests/ImportBatchesApiTests.cs#L218) | [GitHub](https://github.com/xunit/assert.xunit/blob/v2-2.9.3/CollectionAsserts.cs#L47) |
| `Collection<T>(IEnumerable<T>, params Action<T>[])`<br>*static method* | Verifies that a collection contains exactly a given number of elements, which meet the criteria provided by the element inspectors. | 1 | [IntegrationTests/ImportBatchesApiTests.cs:107](tests/Northstar.Api.IntegrationTests/ImportBatchesApiTests.cs#L107) | [GitHub](https://github.com/xunit/assert.xunit/blob/v2-2.9.3/CollectionAsserts.cs#L155) |
| `Contains(string, string, StringComparison)`<br>*static method* | Verifies that a string contains a given sub-string, using the given comparison type. | 10 | [IntegrationTests/FakePayrollProviderClientTests.cs:42](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L42)<br>[IntegrationTests/ImportBatchesApiTests.cs:26](tests/Northstar.Api.IntegrationTests/ImportBatchesApiTests.cs#L26)<br>+8 more | [GitHub](https://github.com/xunit/assert.xunit/blob/v2-2.9.3/StringAsserts.cs#L54) |
| `Contains<T>(IEnumerable<T>, Predicate<T>)`<br>*static method* | Verifies that a collection contains a given object. | 2 | [IntegrationTests/PayrollRunsApiTests.cs:37](tests/Northstar.Api.IntegrationTests/PayrollRunsApiTests.cs#L37)<br>[IntegrationTests/PayrollRunsApiTests.cs:38](tests/Northstar.Api.IntegrationTests/PayrollRunsApiTests.cs#L38) | [GitHub](https://github.com/xunit/assert.xunit/blob/v2-2.9.3/CollectionAsserts.cs#L291) |
| `DoesNotContain(string, string, StringComparison)`<br>*static method* | Verifies that a string does not contain a given sub-string, using the current culture. | 1 | [IntegrationTests/FakePayrollProviderClientTests.cs:68](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L68) | [GitHub](https://github.com/xunit/assert.xunit/blob/v2-2.9.3/StringAsserts.cs#L297) |
| `Empty(IEnumerable)`<br>*static method* | Verifies that a collection is empty. | 2 | [UnitTests/PayrollCalculatorTests.cs:24](tests/Northstar.Domain.UnitTests/PayrollCalculatorTests.cs#L24)<br>[UnitTests/WageRateSelectorTests.cs:78](tests/Northstar.Domain.UnitTests/WageRateSelectorTests.cs#L78) | [GitHub](https://github.com/xunit/assert.xunit/blob/v2-2.9.3/CollectionAsserts.cs#L458) |
| `Equal(string, string)`<br>*static method* | Verifies that two strings are equivalent. | 36 | [IntegrationTests/FakePayrollProviderClientTests.cs:27](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L27)<br>[IntegrationTests/ImportBatchesApiTests.cs:30](tests/Northstar.Api.IntegrationTests/ImportBatchesApiTests.cs#L30)<br>+34 more | [GitHub](https://github.com/xunit/assert.xunit/blob/v2-2.9.3/StringAsserts.cs#L841) |
| `Equal<T>(IEnumerable<T>, IEnumerable<T>)`<br>*static method* | Verifies that two sequences are equivalent, using a default comparer. | 1 | [IntegrationTests/PayrollRunsApiTests.cs:57](tests/Northstar.Api.IntegrationTests/PayrollRunsApiTests.cs#L57) | [GitHub](https://github.com/xunit/assert.xunit/blob/v2-2.9.3/CollectionAsserts.cs#L477) |
| `Equal<T>(T, T)`<br>*static method* | Verifies that two objects are equal, using a default comparer. | 132 | [IntegrationTests/FakePayrollProviderClientTests.cs:25](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L25)<br>[IntegrationTests/ImportBatchesApiTests.cs:24](tests/Northstar.Api.IntegrationTests/ImportBatchesApiTests.cs#L24)<br>+130 more | [GitHub](https://github.com/xunit/assert.xunit/blob/v2-2.9.3/EqualityAsserts.cs#L81) |
| `False(bool)`<br>*static method* | Verifies that the condition is false. | 6 | [IntegrationTests/ImportBatchesApiTests.cs:281](tests/Northstar.Api.IntegrationTests/ImportBatchesApiTests.cs#L281)<br>[UnitTests/ImportBatchStateTests.cs:34](tests/Northstar.Domain.UnitTests/ImportBatchStateTests.cs#L34)<br>+4 more | [GitHub](https://github.com/xunit/assert.xunit/blob/v2-2.9.3/BooleanAsserts.cs#L32) |
| `NotNull(object)`<br>*static method* | Verifies that an object reference is not null. | 9 | [IntegrationTests/ImportBatchesApiTests.cs:25](tests/Northstar.Api.IntegrationTests/ImportBatchesApiTests.cs#L25)<br>[IntegrationTests/PayrollRunsApiTests.cs:34](tests/Northstar.Api.IntegrationTests/PayrollRunsApiTests.cs#L34)<br>+7 more | [GitHub](https://github.com/xunit/assert.xunit/blob/v2-2.9.3/NullAsserts.cs#L31) |
| `Null(object)`<br>*static method* | Verifies that an object reference is null. | 4 | [UnitTests/ImportBatchStateTests.cs:131](tests/Northstar.Domain.UnitTests/ImportBatchStateTests.cs#L131)<br>[UnitTests/WageRateSelectorTests.cs:51](tests/Northstar.Domain.UnitTests/WageRateSelectorTests.cs#L51)<br>+2 more | [GitHub](https://github.com/xunit/assert.xunit/blob/v2-2.9.3/NullAsserts.cs#L66) |
| `Null<T>(T?)`<br>*static method* | Verifies that a nullable struct value is null. | 6 | [UnitTests/ImportBatchStateTests.cs:79](tests/Northstar.Domain.UnitTests/ImportBatchStateTests.cs#L79)<br>[UnitTests/PayrollCalculatorTests.cs:36](tests/Northstar.Domain.UnitTests/PayrollCalculatorTests.cs#L36)<br>+4 more | [GitHub](https://github.com/xunit/assert.xunit/blob/v2-2.9.3/NullAsserts.cs#L80) |
| `Same(object, object)`<br>*static method* | Verifies that two objects are the same instance. | 4 | [UnitTests/WageRateSelectorTests.cs:26](tests/Northstar.Domain.UnitTests/WageRateSelectorTests.cs#L26)<br>[UnitTests/WageRateSelectorTests.cs:27](tests/Northstar.Domain.UnitTests/WageRateSelectorTests.cs#L27)<br>+2 more | [GitHub](https://github.com/xunit/assert.xunit/blob/v2-2.9.3/IdentityAsserts.cs#L44) |
| `Single<T>(IEnumerable<T>)`<br>*static method* | Verifies that the given collection contains only a single element of the given type. | 7 | [IntegrationTests/FakePayrollProviderClientTests.cs:26](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L26)<br>[IntegrationTests/ImportBatchesApiTests.cs:42](tests/Northstar.Api.IntegrationTests/ImportBatchesApiTests.cs#L42)<br>+5 more | [GitHub](https://github.com/xunit/assert.xunit/blob/v2-2.9.3/CollectionAsserts.cs#L653) |
| `Throws<T>(Action)`<br>*static method* | Verifies that the exact exception is thrown (and not a derived exception type). | 1 | [UnitTests/ImportBatchStateTests.cs:98](tests/Northstar.Domain.UnitTests/ImportBatchStateTests.cs#L98) | [GitHub](https://github.com/xunit/assert.xunit/blob/v2-2.9.3/ExceptionAsserts.cs#L93) |
| `ThrowsAnyAsync<T>(Func<Task>)`<br>*static method* | Verifies that the exact exception or a derived exception type is thrown. | 1 | [IntegrationTests/FakePayrollProviderClientTests.cs:119](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L119) | [GitHub](https://github.com/xunit/assert.xunit/blob/v2-2.9.3/ExceptionAsserts.cs#L248) |
| `ThrowsAsync<T>(Func<Task>)`<br>*static method* | Verifies that the exact exception is thrown (and not a derived exception type). | 5 | [IntegrationTests/FakePayrollProviderClientTests.cs:39](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L39)<br>[IntegrationTests/FakePayrollProviderClientTests.cs:54](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L54)<br>+3 more | [GitHub](https://github.com/xunit/assert.xunit/blob/v2-2.9.3/ExceptionAsserts.cs#L269) |
| `True(bool)`<br>*static method* | Verifies that an expression is true. | 9 | [IntegrationTests/ImportBatchesApiTests.cs:299](tests/Northstar.Api.IntegrationTests/ImportBatchesApiTests.cs#L299)<br>[UnitTests/ImportBatchStateTests.cs:17](tests/Northstar.Domain.UnitTests/ImportBatchStateTests.cs#L17)<br>+7 more | [GitHub](https://github.com/xunit/assert.xunit/blob/v2-2.9.3/BooleanAsserts.cs#L91) |
| **`FactAttribute`**<br>*class, used as an attribute* | Attribute that is applied to a method to indicate that it is a fact that should be run by the test runner. | 41 | [IntegrationTests/FakePayrollProviderClientTests.cs:13](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L13)<br>[IntegrationTests/ImportBatchesApiTests.cs:15](tests/Northstar.Api.IntegrationTests/ImportBatchesApiTests.cs#L15)<br>+39 more | [GitHub](https://github.com/xunit/xunit/blob/v2-2.9.3/src/xunit.core/FactAttribute.cs#L13) |
| **`InlineDataAttribute`**<br>*class, used as an attribute* | Provides a data source for a data theory, with the data coming from inline values. | 7 | [IntegrationTests/ImportBatchesApiTests.cs:267](tests/Northstar.Api.IntegrationTests/ImportBatchesApiTests.cs#L267)<br>[UnitTests/ImportBatchStateTests.cs:84](tests/Northstar.Domain.UnitTests/ImportBatchStateTests.cs#L84)<br>+5 more | [GitHub](https://github.com/xunit/xunit/blob/v2-2.9.3/src/xunit.core/InlineDataAttribute.cs#L14) |
| **`TheoryAttribute`**<br>*class, used as an attribute* | Marks a test method as being a data theory. | 2 | [IntegrationTests/ImportBatchesApiTests.cs:266](tests/Northstar.Api.IntegrationTests/ImportBatchesApiTests.cs#L266)<br>[UnitTests/ImportBatchStateTests.cs:83](tests/Northstar.Domain.UnitTests/ImportBatchStateTests.cs#L83) | [GitHub](https://github.com/xunit/xunit/blob/v2-2.9.3/src/xunit.core/TheoryAttribute.cs#L16) |


## 6. Frontend inventory

The Angular application in `src/Northstar.Web` and its two sets of tests use 84 APIs from Angular, RxJS, Vitest, Playwright and the browser. This section lists them, and after them the template syntax and the TypeScript features that the code relies on. [Patterns 22](#pattern-22-angular-standalone-bootstrap-and-an-http-interceptor) to [26](#pattern-26-frontend-tests-with-vitest-and-playwright) show them at work.

### How the frontend numbers were made

There is no compiler pass behind this section. The names were collected by reading the code, and each name is then searched for with a text pattern in 18 files: the ten TypeScript files of the application, the two spec files, `vitest.config.ts`, the three TypeScript files under `tests/e2e`, and the two HTML templates. A *use* is a line that matches. The `import` lines at the top of a file are not counted.

### How to read the frontend tables

- **API** is the name, linked to its page in the documentation of the library: the [Angular API reference](https://angular.dev/api), rxjs.dev, vitest.dev, playwright.dev or MDN. Under the name stands the kind of API.
- **From** is the module that the code imports the name from. A global name of the browser or of the test runner has no import.
- **What it does** is a description in this document's own words.
- **Uses** is the number of matching lines, and **Where** gives up to two of them, written as in section 5.

### The files of the frontend

| File | Lines | What it holds | Patterns |
|---|---:|---|---|
| [Web/main.ts](src/Northstar.Web/src/main.ts) | 6 | The entry point. It starts the application with the root component and the configuration. | [22](#pattern-22-angular-standalone-bootstrap-and-an-http-interceptor) |
| [Web/app.config.ts](src/Northstar.Web/src/app/app.config.ts) | 14 | The providers of the application: error listeners, the router, and the HTTP client with its interceptor. | [22](#pattern-22-angular-standalone-bootstrap-and-an-http-interceptor) |
| [Web/app.routes.ts](src/Northstar.Web/src/app/app.routes.ts) | 3 | The table of routes, which is empty. | [22](#pattern-22-angular-standalone-bootstrap-and-an-http-interceptor) |
| [Web/demo-auth.interceptor.ts](src/Northstar.Web/src/app/core/demo-auth.interceptor.ts) | 13 | The interceptor that adds the two demo headers to every request. | [22](#pattern-22-angular-standalone-bootstrap-and-an-http-interceptor) |
| [Web/app.ts](src/Northstar.Web/src/app/app.ts)<br>[Web/app.html](src/Northstar.Web/src/app/app.html) | 96<br>207 | The root component: the payroll run with its findings, its lines and the three commands. | [23](#pattern-23-angular-services-that-return-observables), [24](#pattern-24-signals-computed-values-and-control-flow-templates) |
| [Web/payroll-api.service.ts](src/Northstar.Web/src/app/payroll-runs/payroll-api.service.ts) | 47 | The service for the payroll and wage-rate endpoints. | [23](#pattern-23-angular-services-that-return-observables) |
| [Web/payroll-run.models.ts](src/Northstar.Web/src/app/payroll-runs/payroll-run.models.ts) | 37 | The interfaces for the JSON of a payroll run. | [23](#pattern-23-angular-services-that-return-observables) |
| [Web/import-panel.component.ts](src/Northstar.Web/src/app/imports/import-panel.component.ts)<br>[Web/import-panel.component.html](src/Northstar.Web/src/app/imports/import-panel.component.html) | 212<br>181 | The import panel: start an import, poll its status, show row errors and the history. | [24](#pattern-24-signals-computed-values-and-control-flow-templates), [25](#pattern-25-polling-with-rxjs-and-automatic-cleanup) |
| [Web/import-api.service.ts](src/Northstar.Web/src/app/imports/import-api.service.ts) | 37 | The service for the import endpoints. | [23](#pattern-23-angular-services-that-return-observables) |
| [Web/import.models.ts](src/Northstar.Web/src/app/imports/import.models.ts) | 44 | The interfaces and union types for the JSON of an import batch. | [23](#pattern-23-angular-services-that-return-observables), [24](#pattern-24-signals-computed-values-and-control-flow-templates) |
| [Web/app.spec.ts](src/Northstar.Web/src/app/app.spec.ts)<br>[Web/import-panel.component.spec.ts](src/Northstar.Web/src/app/imports/import-panel.component.spec.ts) | 40<br>147 | The component tests. | [26](#pattern-26-frontend-tests-with-vitest-and-playwright) |
| [e2e/import-mixed-batch.spec.ts](tests/e2e/specs/import-mixed-batch.spec.ts)<br>[e2e/calculate-payroll-run.spec.ts](tests/e2e/specs/calculate-payroll-run.spec.ts) | 17<br>21 | The end-to-end tests. | [26](#pattern-26-frontend-tests-with-vitest-and-playwright) |
| [Web/index.html](src/Northstar.Web/src/index.html) | 13 | The page that the browser loads. It contains the tag `<app-root>`, where the root component is placed. | [22](#pattern-22-angular-standalone-bootstrap-and-an-http-interceptor) |
| [Web/styles.scss](src/Northstar.Web/src/styles.scss)<br>[Web/app.scss](src/Northstar.Web/src/app/app.scss)<br>[Web/import-panel.component.scss](src/Northstar.Web/src/app/imports/import-panel.component.scss) | 35<br>534<br>249 | The styles: global ones, and one file for each component. | – |

The configuration files of the frontend (`angular.json`, `package.json`, the `tsconfig` files, `proxy.conf.json`, `vitest.config.ts` and `playwright.config.ts`) are covered in [section 7](#7-tooling-and-configuration).

### Angular

*32 APIs, 125 uses.*

| API | From | What it does | Uses | Where |
| --- | --- | --- | ---: | --- |
| [`bootstrapApplication`](https://angular.dev/api/platform-browser/bootstrapApplication)<br>*function* | `@angular/platform-browser` | Starts the app from one root standalone component and a config object holding the app-wide providers. | 1 | [Web/main.ts:5](src/Northstar.Web/src/main.ts#L5) |
| [`ApplicationConfig`](https://angular.dev/api/core/ApplicationConfig)<br>*interface* | `@angular/core` | Type of the config object passed to `bootstrapApplication`; its `providers` array lists the app-wide services. | 1 | [Web/app.config.ts:8](src/Northstar.Web/src/app/app.config.ts#L8) |
| [`provideBrowserGlobalErrorListeners`](https://angular.dev/api/core/provideBrowserGlobalErrorListeners)<br>*function* | `@angular/core` | Listens for uncaught errors and unhandled promise rejections on `window` and passes them to Angular's error handler. | 1 | [Web/app.config.ts:10](src/Northstar.Web/src/app/app.config.ts#L10) |
| [`provideRouter`](https://angular.dev/api/router/provideRouter)<br>*function* | `@angular/router` | Registers the router with the route table. | 1 | [Web/app.config.ts:12](src/Northstar.Web/src/app/app.config.ts#L12) |
| [`Routes`](https://angular.dev/api/router/Routes)<br>*type* | `@angular/router` | Type of the route table: an array of route definitions (empty in this app). | 1 | [Web/app.routes.ts:3](src/Northstar.Web/src/app/app.routes.ts#L3) |
| [`Component`](https://angular.dev/api/core/Component)<br>*decorator* | `@angular/core` | Decorator that turns a class into a component and attaches its template and styles. | 2 | [Web/app.ts:8](src/Northstar.Web/src/app/app.ts#L8)<br>[Web/import-panel.component.ts:19](src/Northstar.Web/src/app/imports/import-panel.component.ts#L19) |
| [`Component.selector`](https://angular.dev/api/core/Component#selector)<br>*option* | `@angular/core` | The HTML tag name that places the component in a template (`app-root`, `app-import-panel`). | 2 | [Web/app.ts:9](src/Northstar.Web/src/app/app.ts#L9)<br>[Web/import-panel.component.ts:20](src/Northstar.Web/src/app/imports/import-panel.component.ts#L20) |
| [`Component.imports`](https://angular.dev/api/core/Component#imports)<br>*option* | `@angular/core` | The components, directives and pipes this component's template may use (standalone components declare their own). | 2 | [Web/app.ts:10](src/Northstar.Web/src/app/app.ts#L10)<br>[Web/import-panel.component.ts:21](src/Northstar.Web/src/app/imports/import-panel.component.ts#L21) |
| [`Component.templateUrl`](https://angular.dev/api/core/Component#templateUrl)<br>*option* | `@angular/core` | Path of the HTML template file. | 2 | [Web/app.ts:11](src/Northstar.Web/src/app/app.ts#L11)<br>[Web/import-panel.component.ts:22](src/Northstar.Web/src/app/imports/import-panel.component.ts#L22) |
| [`Component.styleUrl`](https://angular.dev/api/core/Component#styleUrl)<br>*option* | `@angular/core` | Path of the one stylesheet file for the component. | 2 | [Web/app.ts:12](src/Northstar.Web/src/app/app.ts#L12)<br>[Web/import-panel.component.ts:23](src/Northstar.Web/src/app/imports/import-panel.component.ts#L23) |
| [`Injectable`](https://angular.dev/api/core/Injectable)<br>*decorator* | `@angular/core` | Marks a class as something the injector can create and hand out. | 2 | [Web/import-api.service.ts:6](src/Northstar.Web/src/app/imports/import-api.service.ts#L6)<br>[Web/payroll-api.service.ts:9](src/Northstar.Web/src/app/payroll-runs/payroll-api.service.ts#L9) |
| [`Injectable.providedIn`](https://angular.dev/api/core/Injectable)<br>*option* | `@angular/core` | `providedIn: 'root'` registers the service once for the whole app (one shared instance). | 2 | [Web/import-api.service.ts:6](src/Northstar.Web/src/app/imports/import-api.service.ts#L6)<br>[Web/payroll-api.service.ts:9](src/Northstar.Web/src/app/payroll-runs/payroll-api.service.ts#L9) |
| [`inject`](https://angular.dev/api/core/inject)<br>*function* | `@angular/core` | Gets a dependency from the injector; used in field initializers instead of constructor parameters. | 5 | [Web/app.ts:15](src/Northstar.Web/src/app/app.ts#L15)<br>[Web/import-api.service.ts:8](src/Northstar.Web/src/app/imports/import-api.service.ts#L8)<br>+3 more |
| [`signal`](https://angular.dev/api/core/signal)<br>*function* | `@angular/core` | Creates a writable signal: a value holder that tells Angular when its value changes. Read it by calling it, as in `loading()`. | 10 | [Web/app.ts:17](src/Northstar.Web/src/app/app.ts#L17)<br>[Web/import-panel.component.ts:31](src/Northstar.Web/src/app/imports/import-panel.component.ts#L31)<br>+8 more |
| [`WritableSignal.set`](https://angular.dev/api/core/WritableSignal#set)<br>*method* | `@angular/core` | Replaces the signal's value. | 34 | [Web/app.ts:47](src/Northstar.Web/src/app/app.ts#L47)<br>[Web/import-panel.component.ts:49](src/Northstar.Web/src/app/imports/import-panel.component.ts#L49)<br>+32 more |
| [`WritableSignal.update`](https://angular.dev/api/core/WritableSignal#update)<br>*method* | `@angular/core` | Computes the new value from the current one. | 1 | [Web/import-panel.component.ts:171](src/Northstar.Web/src/app/imports/import-panel.component.ts#L171) |
| [`computed`](https://angular.dev/api/core/computed)<br>*function* | `@angular/core` | Creates a read-only signal derived from other signals; it recalculates only when one of them changed and it is read. | 3 | [Web/app.ts:21](src/Northstar.Web/src/app/app.ts#L21)<br>[Web/import-panel.component.ts:37](src/Northstar.Web/src/app/imports/import-panel.component.ts#L37)<br>+1 more |
| [`OnInit`](https://angular.dev/api/core/OnInit)<br>*interface* | `@angular/core` | Lifecycle interface; a component that implements it has an `ngOnInit` method. | 2 | [Web/app.ts:14](src/Northstar.Web/src/app/app.ts#L14)<br>[Web/import-panel.component.ts:25](src/Northstar.Web/src/app/imports/import-panel.component.ts#L25) |
| [`OnInit.ngOnInit`](https://angular.dev/api/core/OnInit#ngOnInit)<br>*method* | `@angular/core` | Called once after Angular has set up the component; this app starts its first HTTP load here. | 2 | [Web/app.ts:28](src/Northstar.Web/src/app/app.ts#L28)<br>[Web/import-panel.component.ts:42](src/Northstar.Web/src/app/imports/import-panel.component.ts#L42) |
| [`DestroyRef`](https://angular.dev/api/core/DestroyRef)<br>*class* | `@angular/core` | Lets code react to the component being destroyed; passed to `takeUntilDestroyed`. | 1 | [Web/import-panel.component.ts:27](src/Northstar.Web/src/app/imports/import-panel.component.ts#L27) |
| [`takeUntilDestroyed`](https://angular.dev/api/core/rxjs-interop/takeUntilDestroyed)<br>*function* | `@angular/core/rxjs-interop` | RxJS operator that ends a stream when the component is destroyed, so no subscription outlives it. | 5 | [Web/import-panel.component.ts:55](src/Northstar.Web/src/app/imports/import-panel.component.ts#L55)<br>[Web/import-panel.component.ts:78](src/Northstar.Web/src/app/imports/import-panel.component.ts#L78)<br>+3 more |
| [`DatePipe`](https://angular.dev/api/common/DatePipe)<br>*pipe* | `@angular/common` | Template pipe `date`: formats a date value (`{{ value \| date: 'medium' }}`). | 8 | [Web/app.html:45](src/Northstar.Web/src/app/app.html#L45)<br>[Web/app.ts:10](src/Northstar.Web/src/app/app.ts#L10)<br>+6 more |
| [`CurrencyPipe`](https://angular.dev/api/common/CurrencyPipe)<br>*pipe* | `@angular/common` | Template pipe `currency`: formats a number as money (currency code, symbol display, digit rule). | 4 | [Web/app.html:181](src/Northstar.Web/src/app/app.html#L181)<br>[Web/app.ts:10](src/Northstar.Web/src/app/app.ts#L10)<br>+2 more |
| [`provideHttpClient`](https://angular.dev/api/common/http/provideHttpClient)<br>*function* | `@angular/common/http` | Makes `HttpClient` injectable; accepts feature functions such as `withInterceptors`. | 3 | [Web/app.config.ts:11](src/Northstar.Web/src/app/app.config.ts#L11)<br>[Web/app.spec.ts:11](src/Northstar.Web/src/app/app.spec.ts#L11)<br>+1 more |
| [`withInterceptors`](https://angular.dev/api/common/http/withInterceptors)<br>*function* | `@angular/common/http` | Adds interceptor functions to the HTTP pipeline, in the order given. | 1 | [Web/app.config.ts:11](src/Northstar.Web/src/app/app.config.ts#L11) |
| [`HttpInterceptorFn`](https://angular.dev/api/common/http/HttpInterceptorFn)<br>*type* | `@angular/common/http` | Type of an interceptor function: it receives the outgoing request and a `next` function and returns the response stream. | 1 | [Web/demo-auth.interceptor.ts:5](src/Northstar.Web/src/app/core/demo-auth.interceptor.ts#L5) |
| [`HttpRequest.clone`](https://angular.dev/api/common/http/HttpRequest#clone)<br>*method* | `@angular/common/http` | Requests are immutable; `clone` returns a copy with changes such as extra headers (`setHeaders`). | 1 | [Web/demo-auth.interceptor.ts:7](src/Northstar.Web/src/app/core/demo-auth.interceptor.ts#L7) |
| [`HttpClient`](https://angular.dev/api/common/http/HttpClient)<br>*class* | `@angular/common/http` | The service that sends HTTP requests and returns their results as Observables. | 2 | [Web/import-api.service.ts:8](src/Northstar.Web/src/app/imports/import-api.service.ts#L8)<br>[Web/payroll-api.service.ts:11](src/Northstar.Web/src/app/payroll-runs/payroll-api.service.ts#L11) |
| [`HttpClient.get`](https://angular.dev/api/common/http/HttpClient#get)<br>*method* | `@angular/common/http` | Builds a GET request; nothing is sent until something subscribes. The JSON body arrives typed as `T`. | 4 | [Web/import-api.service.ts:11](src/Northstar.Web/src/app/imports/import-api.service.ts#L11)<br>[Web/payroll-api.service.ts:14](src/Northstar.Web/src/app/payroll-runs/payroll-api.service.ts#L14)<br>+2 more |
| [`HttpClient.post`](https://angular.dev/api/common/http/HttpClient#post)<br>*method* | `@angular/common/http` | Builds a POST request with a JSON body and optional headers. | 5 | [Web/import-api.service.ts:15](src/Northstar.Web/src/app/imports/import-api.service.ts#L15)<br>[Web/payroll-api.service.ts:18](src/Northstar.Web/src/app/payroll-runs/payroll-api.service.ts#L18)<br>+3 more |
| [`HttpHeaders`](https://angular.dev/api/common/http/HttpHeaders)<br>*class* | `@angular/common/http` | Immutable set of request headers; here it carries `Idempotency-Key`. | 3 | [Web/import-api.service.ts:16](src/Northstar.Web/src/app/imports/import-api.service.ts#L16)<br>[Web/payroll-api.service.ts:21](src/Northstar.Web/src/app/payroll-runs/payroll-api.service.ts#L21)<br>+1 more |
| [`HttpErrorResponse`](https://angular.dev/api/common/http/HttpErrorResponse)<br>*class* | `@angular/common/http` | The error value HttpClient emits for a failed request; `error` holds the parsed response body (the API's ProblemDetails). | 11 | [Web/app.ts:39](src/Northstar.Web/src/app/app.ts#L39)<br>[Web/import-panel.component.ts:65](src/Northstar.Web/src/app/imports/import-panel.component.ts#L65)<br>+9 more |

### Angular testing

*12 APIs, 64 uses.*

| API | From | What it does | Uses | Where |
| --- | --- | --- | ---: | --- |
| [`TestBed`](https://angular.dev/api/core/testing/TestBed)<br>*class* | `@angular/core/testing` | The test harness: builds a small Angular environment for one test. | 8 | [Web/app.spec.ts:9](src/Northstar.Web/src/app/app.spec.ts#L9)<br>[Web/import-panel.component.spec.ts:9](src/Northstar.Web/src/app/imports/import-panel.component.spec.ts#L9)<br>+6 more |
| [`TestBed.configureTestingModule`](https://angular.dev/api/core/testing/TestBed#configureTestingModule)<br>*method* | `@angular/core/testing` | Declares what the test needs: components to import and providers to use. | 2 | [Web/app.spec.ts:9](src/Northstar.Web/src/app/app.spec.ts#L9)<br>[Web/import-panel.component.spec.ts:9](src/Northstar.Web/src/app/imports/import-panel.component.spec.ts#L9) |
| [`TestBed.compileComponents`](https://angular.dev/api/core/testing/TestBed#compileComponents)<br>*method* | `@angular/core/testing` | Compiles components that load their template and styles from files. | 2 | [Web/app.spec.ts:12](src/Northstar.Web/src/app/app.spec.ts#L12)<br>[Web/import-panel.component.spec.ts:12](src/Northstar.Web/src/app/imports/import-panel.component.spec.ts#L12) |
| [`TestBed.createComponent`](https://angular.dev/api/core/testing/TestBed#createComponent)<br>*method* | `@angular/core/testing` | Creates the component under test and returns a `ComponentFixture` for it. | 3 | [Web/app.spec.ts:14](src/Northstar.Web/src/app/app.spec.ts#L14)<br>[Web/import-panel.component.spec.ts:21](src/Northstar.Web/src/app/imports/import-panel.component.spec.ts#L21)<br>+1 more |
| [`TestBed.inject`](https://angular.dev/api/core/testing/TestBed#inject)<br>*method* | `@angular/core/testing` | Gets a service from the test injector (here the `HttpTestingController`). | 3 | [Web/app.spec.ts:16](src/Northstar.Web/src/app/app.spec.ts#L16)<br>[Web/import-panel.component.spec.ts:22](src/Northstar.Web/src/app/imports/import-panel.component.spec.ts#L22)<br>+1 more |
| [`ComponentFixture.detectChanges`](https://angular.dev/api/core/testing/ComponentFixture#detectChanges)<br>*method* | `@angular/core/testing` | Runs change detection so the rendered DOM matches the component's current state. | 8 | [Web/app.spec.ts:15](src/Northstar.Web/src/app/app.spec.ts#L15)<br>[Web/import-panel.component.spec.ts:23](src/Northstar.Web/src/app/imports/import-panel.component.spec.ts#L23)<br>+6 more |
| [`ComponentFixture.nativeElement`](https://angular.dev/api/core/testing/ComponentFixture#nativeElement)<br>*property* | `@angular/core/testing` | The component's root DOM element, used with `querySelector` to read what was rendered. | 8 | [Web/app.spec.ts:21](src/Northstar.Web/src/app/app.spec.ts#L21)<br>[Web/import-panel.component.spec.ts:28](src/Northstar.Web/src/app/imports/import-panel.component.spec.ts#L28)<br>+6 more |
| [`provideHttpClientTesting`](https://angular.dev/api/common/http/testing/provideHttpClientTesting)<br>*function* | `@angular/common/http/testing` | Swaps the real HTTP backend for a test backend, so no network call leaves the test. | 2 | [Web/app.spec.ts:11](src/Northstar.Web/src/app/app.spec.ts#L11)<br>[Web/import-panel.component.spec.ts:11](src/Northstar.Web/src/app/imports/import-panel.component.spec.ts#L11) |
| [`HttpTestingController`](https://angular.dev/api/common/http/testing/HttpTestingController)<br>*class* | `@angular/common/http/testing` | Lets a test look at the requests the code made and answer them. | 3 | [Web/app.spec.ts:16](src/Northstar.Web/src/app/app.spec.ts#L16)<br>[Web/import-panel.component.spec.ts:22](src/Northstar.Web/src/app/imports/import-panel.component.spec.ts#L22)<br>+1 more |
| [`HttpTestingController.expectOne`](https://angular.dev/api/common/http/testing/HttpTestingController#expectOne)<br>*method* | `@angular/common/http/testing` | Asserts that exactly one request matches and returns it as a `TestRequest`. | 11 | [Web/app.spec.ts:17](src/Northstar.Web/src/app/app.spec.ts#L17)<br>[Web/import-panel.component.spec.ts:25](src/Northstar.Web/src/app/imports/import-panel.component.spec.ts#L25)<br>+9 more |
| [`HttpTestingController.verify`](https://angular.dev/api/common/http/testing/HttpTestingController#verify)<br>*method* | `@angular/common/http/testing` | Fails the test if a request was made that the test never answered. | 3 | [Web/app.spec.ts:25](src/Northstar.Web/src/app/app.spec.ts#L25)<br>[Web/import-panel.component.spec.ts:60](src/Northstar.Web/src/app/imports/import-panel.component.spec.ts#L60)<br>+1 more |
| [`TestRequest.flush`](https://angular.dev/api/common/http/testing/TestRequest#flush)<br>*method* | `@angular/common/http/testing` | Answers the captured request with a body, as if the server had replied. | 11 | [Web/app.spec.ts:17](src/Northstar.Web/src/app/app.spec.ts#L17)<br>[Web/import-panel.component.spec.ts:25](src/Northstar.Web/src/app/imports/import-panel.component.spec.ts#L25)<br>+9 more |

### RxJS

*8 APIs, 36 uses.*

| API | From | What it does | Uses | Where |
| --- | --- | --- | ---: | --- |
| [`Observable`](https://rxjs.dev/api/index/class/Observable)<br>*class* | `rxjs` | A lazy stream of values. The API services return one per HTTP call; nothing runs until `subscribe`. | 9 | [Web/import-api.service.ts:10](src/Northstar.Web/src/app/imports/import-api.service.ts#L10)<br>[Web/payroll-api.service.ts:13](src/Northstar.Web/src/app/payroll-runs/payroll-api.service.ts#L13)<br>+7 more |
| [`Observable.subscribe`](https://rxjs.dev/api/index/class/Observable#subscribe)<br>*method* | `rxjs` | Starts the stream and registers the `next` and `error` callbacks; returns a `Subscription`. | 9 | [Web/app.ts:37](src/Northstar.Web/src/app/app.ts#L37)<br>[Web/import-panel.component.ts:56](src/Northstar.Web/src/app/imports/import-panel.component.ts#L56)<br>+7 more |
| [`Observable.pipe`](https://rxjs.dev/api/index/class/Observable#pipe)<br>*method* | `rxjs` | Chains operators onto a stream and returns the resulting stream. | 5 | [Web/import-panel.component.ts:55](src/Northstar.Web/src/app/imports/import-panel.component.ts#L55)<br>[Web/import-panel.component.ts:78](src/Northstar.Web/src/app/imports/import-panel.component.ts#L78)<br>+3 more |
| [`Subscription`](https://rxjs.dev/api/index/class/Subscription)<br>*class* | `rxjs` | The handle for a running stream. | 2 | [Web/import-panel.component.ts:28](src/Northstar.Web/src/app/imports/import-panel.component.ts#L28)<br>[Web/import-panel.component.ts:29](src/Northstar.Web/src/app/imports/import-panel.component.ts#L29) |
| [`Subscription.unsubscribe`](https://rxjs.dev/api/index/class/Subscription#unsubscribe)<br>*method* | `rxjs` | Stops the stream; used to cancel the running poll before starting another. | 8 | [Web/import-panel.component.ts:47](src/Northstar.Web/src/app/imports/import-panel.component.ts#L47)<br>[Web/import-panel.component.ts:48](src/Northstar.Web/src/app/imports/import-panel.component.ts#L48)<br>+6 more |
| [`timer`](https://rxjs.dev/api/index/function/timer)<br>*function* | `rxjs` | `timer(0, 1000)` emits immediately and then once per second: the polling clock. | 1 | [Web/import-panel.component.ts:152](src/Northstar.Web/src/app/imports/import-panel.component.ts#L152) |
| [`switchMap`](https://rxjs.dev/api/index/function/switchMap)<br>*operator* | `rxjs` | For each tick, starts a new inner request and drops the previous one if it has not finished. | 1 | [Web/import-panel.component.ts:154](src/Northstar.Web/src/app/imports/import-panel.component.ts#L154) |
| [`takeWhile`](https://rxjs.dev/api/index/function/takeWhile)<br>*operator* | `rxjs` | Lets values through while the condition holds; with `inclusive = true` it also emits the first value that fails, then completes. | 1 | [Web/import-panel.component.ts:155](src/Northstar.Web/src/app/imports/import-panel.component.ts#L155) |

### Vitest

*14 APIs, 36 uses.*

| API | From | What it does | Uses | Where |
| --- | --- | --- | ---: | --- |
| [`describe`](https://vitest.dev/api/describe)<br>*function* | `vitest` | Groups related tests under one name. | 2 | [Web/app.spec.ts:7](src/Northstar.Web/src/app/app.spec.ts#L7)<br>[Web/import-panel.component.spec.ts:7](src/Northstar.Web/src/app/imports/import-panel.component.spec.ts#L7) |
| [`it`](https://vitest.dev/api/test)<br>*function* | `vitest` | Defines one test (`it` is an alias of `test`). | 3 | [Web/app.spec.ts:8](src/Northstar.Web/src/app/app.spec.ts#L8)<br>[Web/import-panel.component.spec.ts:19](src/Northstar.Web/src/app/imports/import-panel.component.spec.ts#L19)<br>+1 more |
| [`beforeEach`](https://vitest.dev/api/hooks#beforeeach)<br>*function* | `vitest` | Runs before every test in the group; here it builds the TestBed. | 1 | [Web/import-panel.component.spec.ts:8](src/Northstar.Web/src/app/imports/import-panel.component.spec.ts#L8) |
| [`afterEach`](https://vitest.dev/api/hooks#aftereach)<br>*function* | `vitest` | Runs after every test; here it restores real timers. | 1 | [Web/import-panel.component.spec.ts:15](src/Northstar.Web/src/app/imports/import-panel.component.spec.ts#L15) |
| [`expect`](https://vitest.dev/api/expect)<br>*function* | `vitest` | Wraps a value so a matcher can assert on it. | 11 | [Web/app.spec.ts:21](src/Northstar.Web/src/app/app.spec.ts#L21)<br>[Web/import-panel.component.spec.ts:34](src/Northstar.Web/src/app/imports/import-panel.component.spec.ts#L34)<br>+9 more |
| [`expect(...).toBe`](https://vitest.dev/api/expect#tobe)<br>*matcher* | `vitest` | Passes when the value is the same primitive or the same object reference. | 2 | [Web/import-panel.component.spec.ts:34](src/Northstar.Web/src/app/imports/import-panel.component.spec.ts#L34)<br>[Web/import-panel.component.spec.ts:94](src/Northstar.Web/src/app/imports/import-panel.component.spec.ts#L94) |
| [`expect(...).toEqual`](https://vitest.dev/api/expect#toequal)<br>*matcher* | `vitest` | Passes when the value has the same structure (deep comparison). | 1 | [Web/import-panel.component.spec.ts:35](src/Northstar.Web/src/app/imports/import-panel.component.spec.ts#L35) |
| [`expect(...).toContain`](https://vitest.dev/api/expect#tocontain)<br>*matcher* | `vitest` | Passes when a string contains the text or an array contains the item. | 5 | [Web/app.spec.ts:21](src/Northstar.Web/src/app/app.spec.ts#L21)<br>[Web/import-panel.component.spec.ts:56](src/Northstar.Web/src/app/imports/import-panel.component.spec.ts#L56)<br>+3 more |
| [`expect(...).toBeTruthy`](https://vitest.dev/api/expect#tobetruthy)<br>*matcher* | `vitest` | Passes when the value converts to `true`. | 2 | [Web/import-panel.component.spec.ts:40](src/Northstar.Web/src/app/imports/import-panel.component.spec.ts#L40)<br>[Web/import-panel.component.spec.ts:95](src/Northstar.Web/src/app/imports/import-panel.component.spec.ts#L95) |
| [`expect(...).toHaveLength`](https://vitest.dev/api/expect#tohavelength)<br>*matcher* | `vitest` | Passes when the value's `length` equals the number. | 1 | [Web/import-panel.component.spec.ts:57](src/Northstar.Web/src/app/imports/import-panel.component.spec.ts#L57) |
| [`vi.useFakeTimers`](https://vitest.dev/api/vi#vi-usefaketimers)<br>*function* | `vitest` | Replaces `setTimeout`/`setInterval` with a clock the test controls. | 2 | [Web/import-panel.component.spec.ts:20](src/Northstar.Web/src/app/imports/import-panel.component.spec.ts#L20)<br>[Web/import-panel.component.spec.ts:64](src/Northstar.Web/src/app/imports/import-panel.component.spec.ts#L64) |
| [`vi.advanceTimersByTimeAsync`](https://vitest.dev/api/vi#vi-advancetimersbytimeasync)<br>*function* | `vitest` | Moves the fake clock forward by the given milliseconds and waits for asynchronous timer callbacks. | 3 | [Web/import-panel.component.spec.ts:43](src/Northstar.Web/src/app/imports/import-panel.component.spec.ts#L43)<br>[Web/import-panel.component.spec.ts:49](src/Northstar.Web/src/app/imports/import-panel.component.spec.ts#L49)<br>+1 more |
| [`vi.useRealTimers`](https://vitest.dev/api/vi#vi-userealtimers)<br>*function* | `vitest` | Puts the real timers back. | 1 | [Web/import-panel.component.spec.ts:16](src/Northstar.Web/src/app/imports/import-panel.component.spec.ts#L16) |
| [`defineConfig`](https://vitest.dev/config/)<br>*function* | `vitest/config` | Typed helper for the Vitest config file (`vitest.config.ts`). | 1 | [Web/vitest.config.ts:3](src/Northstar.Web/vitest.config.ts#L3) |

### Playwright

*14 APIs, 69 uses.*

| API | From | What it does | Uses | Where |
| --- | --- | --- | ---: | --- |
| [`test`](https://playwright.dev/docs/api/class-test)<br>*function* | `@playwright/test` | Defines one end-to-end test and gives it a fresh browser `page`. | 2 | [e2e/calculate-payroll-run.spec.ts:3](tests/e2e/specs/calculate-payroll-run.spec.ts#L3)<br>[e2e/import-mixed-batch.spec.ts:3](tests/e2e/specs/import-mixed-batch.spec.ts#L3) |
| [`expect`](https://playwright.dev/docs/test-assertions)<br>*function* | `@playwright/test` | Creates an assertion that retries until it passes or times out. | 18 | [e2e/calculate-payroll-run.spec.ts:6](tests/e2e/specs/calculate-payroll-run.spec.ts#L6)<br>[e2e/import-mixed-batch.spec.ts:6](tests/e2e/specs/import-mixed-batch.spec.ts#L6)<br>+16 more |
| [`defineConfig`](https://playwright.dev/docs/test-configuration)<br>*function* | `@playwright/test` | Typed helper for `playwright.config.ts`. | 1 | [e2e/playwright.config.ts:3](tests/e2e/playwright.config.ts#L3) |
| [`devices`](https://playwright.dev/docs/emulation#devices)<br>*object* | `@playwright/test` | Preset browser and viewport settings such as `Desktop Chrome`. | 1 | [e2e/playwright.config.ts:18](tests/e2e/playwright.config.ts#L18) |
| [`webServer (config)`](https://playwright.dev/docs/test-webserver)<br>*option* | `@playwright/test` | Starts the fake provider, the API and the Angular dev server before the tests and waits for each URL. | 1 | [e2e/playwright.config.ts:21](tests/e2e/playwright.config.ts#L21) |
| [`page.goto`](https://playwright.dev/docs/api/class-page#page-goto)<br>*method* | `@playwright/test` | Opens a URL in the page. | 2 | [e2e/calculate-payroll-run.spec.ts:4](tests/e2e/specs/calculate-payroll-run.spec.ts#L4)<br>[e2e/import-mixed-batch.spec.ts:4](tests/e2e/specs/import-mixed-batch.spec.ts#L4) |
| [`page.getByTestId`](https://playwright.dev/docs/api/class-page#page-get-by-test-id)<br>*method* | `@playwright/test` | Finds elements by their `data-testid` attribute. | 17 | [e2e/calculate-payroll-run.spec.ts:7](tests/e2e/specs/calculate-payroll-run.spec.ts#L7)<br>[e2e/import-mixed-batch.spec.ts:6](tests/e2e/specs/import-mixed-batch.spec.ts#L6)<br>+15 more |
| [`page.getByText`](https://playwright.dev/docs/api/class-page#page-get-by-text)<br>*method* | `@playwright/test` | Finds elements by their visible text. | 4 | [e2e/calculate-payroll-run.spec.ts:11](tests/e2e/specs/calculate-payroll-run.spec.ts#L11)<br>[e2e/calculate-payroll-run.spec.ts:12](tests/e2e/specs/calculate-payroll-run.spec.ts#L12)<br>+2 more |
| [`page.getByRole`](https://playwright.dev/docs/api/class-page#page-get-by-role)<br>*method* | `@playwright/test` | Finds elements by accessibility role and name. | 1 | [e2e/calculate-payroll-run.spec.ts:6](tests/e2e/specs/calculate-payroll-run.spec.ts#L6) |
| [`locator.click`](https://playwright.dev/docs/api/class-locator#locator-click)<br>*method* | `@playwright/test` | Clicks the element after waiting for it to be ready. | 4 | [e2e/calculate-payroll-run.spec.ts:9](tests/e2e/specs/calculate-payroll-run.spec.ts#L9)<br>[e2e/import-mixed-batch.spec.ts:7](tests/e2e/specs/import-mixed-batch.spec.ts#L7)<br>+2 more |
| [`expect(locator).toHaveText`](https://playwright.dev/docs/api/class-locatorassertions#locator-assertions-to-have-text)<br>*matcher* | `@playwright/test` | Waits until the element's text equals the expected text. | 8 | [e2e/calculate-payroll-run.spec.ts:7](tests/e2e/specs/calculate-payroll-run.spec.ts#L7)<br>[e2e/import-mixed-batch.spec.ts:9](tests/e2e/specs/import-mixed-batch.spec.ts#L9)<br>+6 more |
| [`expect(locator).toContainText`](https://playwright.dev/docs/api/class-locatorassertions#locator-assertions-to-contain-text)<br>*matcher* | `@playwright/test` | Waits until the element's text contains the expected text. | 3 | [e2e/import-mixed-batch.spec.ts:13](tests/e2e/specs/import-mixed-batch.spec.ts#L13)<br>[e2e/import-mixed-batch.spec.ts:14](tests/e2e/specs/import-mixed-batch.spec.ts#L14)<br>+1 more |
| [`expect(locator).toBeVisible`](https://playwright.dev/docs/api/class-locatorassertions#locator-assertions-to-be-visible)<br>*matcher* | `@playwright/test` | Waits until the element is visible. | 5 | [e2e/calculate-payroll-run.spec.ts:6](tests/e2e/specs/calculate-payroll-run.spec.ts#L6)<br>[e2e/import-mixed-batch.spec.ts:6](tests/e2e/specs/import-mixed-batch.spec.ts#L6)<br>+3 more |
| [`expect(locator).toHaveCount`](https://playwright.dev/docs/api/class-locatorassertions#locator-assertions-to-have-count)<br>*matcher* | `@playwright/test` | Waits until the locator matches exactly that many elements. | 2 | [e2e/calculate-payroll-run.spec.ts:17](tests/e2e/specs/calculate-payroll-run.spec.ts#L17)<br>[e2e/import-mixed-batch.spec.ts:16](tests/e2e/specs/import-mixed-batch.spec.ts#L16) |

### Browser and JavaScript

*4 APIs, 11 uses.*

| API | From | What it does | Uses | Where |
| --- | --- | --- | ---: | --- |
| [`crypto.randomUUID`](https://developer.mozilla.org/en-US/docs/Web/API/Crypto/randomUUID)<br>*method* | `Web Crypto` | Returns a random version 4 UUID string; used as the `Idempotency-Key` for each button press. | 3 | [Web/app.ts:37](src/Northstar.Web/src/app/app.ts#L37)<br>[Web/import-panel.component.ts:54](src/Northstar.Web/src/app/imports/import-panel.component.ts#L54)<br>+1 more |
| [`Date.parse`](https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Date/parse)<br>*method* | `JavaScript` | Parses a date string into milliseconds since 1970; used to sort batches newest first. | 1 | [Web/import-panel.component.ts:200](src/Northstar.Web/src/app/imports/import-panel.component.ts#L200) |
| [`Array.prototype.sort`](https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Array/sort)<br>*method* | `JavaScript` | Sorts an array in place, which is why the code sorts a copy (`[...batches]`). | 1 | [Web/import-panel.component.ts:199](src/Northstar.Web/src/app/imports/import-panel.component.ts#L199) |
| [`Element.querySelector`](https://developer.mozilla.org/en-US/docs/Web/API/Element/querySelector)<br>*method* | `DOM` | Returns the first descendant element that matches a CSS selector. | 6 | [Web/app.spec.ts:21](src/Northstar.Web/src/app/app.spec.ts#L21)<br>[Web/import-panel.component.spec.ts:28](src/Northstar.Web/src/app/imports/import-panel.component.spec.ts#L28)<br>+4 more |


### Angular template syntax

The two templates use nine kinds of syntax. Pattern [24](#pattern-24-signals-computed-values-and-control-flow-templates) reads examples of most of them line by line.

| Feature | In plain words | Uses | Examples |
| --- | --- | ---: | --- |
| [`@if` / `@else if` / `@else`](https://angular.dev/guide/templates/control-flow#conditionally-display-content-with-if-else-if-and-else) | Built-in conditional block; replaces the older `*ngIf` directive. | 23 | [Web/app.html:31](src/Northstar.Web/src/app/app.html#L31)<br>[Web/import-panel.component.html:10](src/Northstar.Web/src/app/imports/import-panel.component.html#L10)<br>+21 more |
| [`@if (value(); as name)`](https://angular.dev/guide/templates/control-flow#referencing-the-conditional-expressions-result) | Saves the tested value under a name for use inside the block, so the signal is read once. | 5 | [Web/app.html:39](src/Northstar.Web/src/app/app.html#L39)<br>[Web/import-panel.component.html:10](src/Northstar.Web/src/app/imports/import-panel.component.html#L10)<br>+3 more |
| [`@for (item of items; track item.id)`](https://angular.dev/guide/templates/control-flow#repeat-content-with-the-for-block) | Built-in loop; `track` names the value that identifies each item so Angular can reuse DOM rows. | 4 | [Web/app.html:111](src/Northstar.Web/src/app/app.html#L111)<br>[Web/import-panel.component.html:139](src/Northstar.Web/src/app/imports/import-panel.component.html#L139)<br>+2 more |
| [Interpolation `{{ expression }}`](https://angular.dev/guide/templates/binding#render-dynamic-text-with-text-interpolation) | Writes the value of an expression into the page as text. | 42 | [Web/app.html:32](src/Northstar.Web/src/app/app.html#L32)<br>[Web/import-panel.component.html:16](src/Northstar.Web/src/app/imports/import-panel.component.html#L16)<br>+40 more |
| [Property binding `[disabled]="..."`](https://angular.dev/guide/templates/binding#binding-dynamic-properties-and-attributes) | Sets a DOM property from an expression and keeps it up to date. | 7 | [Web/app.html:82](src/Northstar.Web/src/app/app.html#L82)<br>[Web/import-panel.component.html:29](src/Northstar.Web/src/app/imports/import-panel.component.html#L29)<br>+5 more |
| [Class binding `[class.name]="..."` and `[class]="..."`](https://angular.dev/guide/templates/binding#css-class-and-style-property-bindings) | Adds or removes a CSS class depending on a condition, or sets the class list from a value. | 8 | [Web/app.html:49](src/Northstar.Web/src/app/app.html#L49)<br>[Web/import-panel.component.html:13](src/Northstar.Web/src/app/imports/import-panel.component.html#L13)<br>+6 more |
| [Event binding `(click)="..."`](https://angular.dev/guide/templates/event-listeners) | Runs a component method when the DOM event fires. | 8 | [Web/app.html:81](src/Northstar.Web/src/app/app.html#L81)<br>[Web/import-panel.component.html:30](src/Northstar.Web/src/app/imports/import-panel.component.html#L30)<br>+6 more |
| [Pipes `value \| date` and `value \| currency`](https://angular.dev/guide/templates/pipes) | Formats a value for display inside a template expression. | 9 | [Web/app.html:45](src/Northstar.Web/src/app/app.html#L45)<br>[Web/import-panel.component.html:72](src/Northstar.Web/src/app/imports/import-panel.component.html#L72)<br>+7 more |
| [Reading a signal `loading()`](https://angular.dev/guide/signals#reading-signals-in-onpush-components) | Templates read signals by calling them; Angular re-renders the part that read a signal when it changes. | 25 | [Web/app.html:31](src/Northstar.Web/src/app/app.html#L31)<br>[Web/import-panel.component.html:10](src/Northstar.Web/src/app/imports/import-panel.component.html#L10)<br>+23 more |

### TypeScript features

The TypeScript in this application is plain: classes, interfaces, union types and arrow functions. Several features have a direct counterpart in the C# code, such as `?.` and `??`, digit separators, and generics.

| Feature | In plain words | Uses | Examples |
| --- | --- | ---: | --- |
| [`interface`](https://www.typescriptlang.org/docs/handbook/2/everyday-types.html#interfaces) | Names the shape of an object; used for the JSON the API returns (`ImportBatch`, `PayrollRun`). | 6 | [Web/import.models.ts:6](src/Northstar.Web/src/app/imports/import.models.ts#L6)<br>[Web/payroll-run.models.ts:1](src/Northstar.Web/src/app/payroll-runs/payroll-run.models.ts#L1)<br>+4 more |
| [String-literal union types](https://www.typescriptlang.org/docs/handbook/2/everyday-types.html#literal-types) | A type that allows only the listed strings, such as the import statuses; the TypeScript counterpart of the C# enums sent as strings. | 2 | [Web/import.models.ts:1](src/Northstar.Web/src/app/imports/import.models.ts#L1)<br>[Web/import.models.ts:4](src/Northstar.Web/src/app/imports/import.models.ts#L4) |
| [Union with `null`](https://www.typescriptlang.org/docs/handbook/2/everyday-types.html#null-and-undefined) | `string \| null` says a value may be missing; the compiler then makes the code check before using it. | 18 | [Web/app.ts:17](src/Northstar.Web/src/app/app.ts#L17)<br>[Web/import-panel.component.ts:32](src/Northstar.Web/src/app/imports/import-panel.component.ts#L32)<br>+16 more |
| [`readonly` / `private` / `protected` members](https://www.typescriptlang.org/docs/handbook/2/classes.html#member-visibility) | Fields that are set once and visible only inside the class (or to the template for `protected`). | 18 | [Web/app.ts:15](src/Northstar.Web/src/app/app.ts#L15)<br>[Web/import-api.service.ts:8](src/Northstar.Web/src/app/imports/import-api.service.ts#L8)<br>+16 more |
| [`implements`](https://www.typescriptlang.org/docs/handbook/2/classes.html#implements-clauses) | Checks that the class provides the members of an interface (`OnInit`). | 2 | [Web/app.ts:14](src/Northstar.Web/src/app/app.ts#L14)<br>[Web/import-panel.component.ts:25](src/Northstar.Web/src/app/imports/import-panel.component.ts#L25) |
| [Generics](https://www.typescriptlang.org/docs/handbook/2/generics.html) | Type parameters, as in `signal<ImportBatch[]>([])` and `http.get<PayrollRun[]>(...)`. | 25 | [Web/app.ts:17](src/Northstar.Web/src/app/app.ts#L17)<br>[Web/import-api.service.ts:10](src/Northstar.Web/src/app/imports/import-api.service.ts#L10)<br>+23 more |
| [Arrow functions](https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Functions/Arrow_functions) | Short function syntax that keeps the surrounding `this`; used for callbacks and for the interceptor. | 28 | [Web/app.ts:22](src/Northstar.Web/src/app/app.ts#L22)<br>[Web/demo-auth.interceptor.ts:5](src/Northstar.Web/src/app/core/demo-auth.interceptor.ts#L5)<br>+26 more |
| [Optional chaining `?.` and nullish coalescing `??`](https://www.typescriptlang.org/docs/handbook/release-notes/typescript-3-7.html#optional-chaining) | `a?.b` stops at `undefined` instead of throwing; `x ?? y` uses `y` only when `x` is `null` or `undefined`. | 15 | [Web/app.ts:22](src/Northstar.Web/src/app/app.ts#L22)<br>[Web/import-panel.component.ts:47](src/Northstar.Web/src/app/imports/import-panel.component.ts#L47)<br>+13 more |
| [Spread `[...items]`](https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Operators/Spread_syntax) | Copies an array (or object) into a new one. | 1 | [Web/import-panel.component.ts:199](src/Northstar.Web/src/app/imports/import-panel.component.ts#L199) |
| [Template literals](https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Template_literals) | Strings with embedded expressions, used to build request URLs. | 5 | [Web/import-api.service.ts:21](src/Northstar.Web/src/app/imports/import-api.service.ts#L21)<br>[Web/payroll-api.service.ts:19](src/Northstar.Web/src/app/payroll-runs/payroll-api.service.ts#L19)<br>+3 more |
| [Numeric separators `1_000`](https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Lexical_grammar#numeric_separators) | Underscores that make long numbers easier to read. | 1 | [Web/import-panel.component.ts:17](src/Northstar.Web/src/app/imports/import-panel.component.ts#L17) |
| [`switch` over a union](https://www.typescriptlang.org/docs/handbook/2/narrowing.html#exhaustiveness-checking) | A `switch` on a string-literal union; the compiler narrows the type in each `case`. | 1 | [Web/import-panel.component.ts:108](src/Northstar.Web/src/app/imports/import-panel.component.ts#L108) |

## 7. Tooling and configuration

The C# and TypeScript files are only part of the repository. Around them sit the files that tell each tool what to do: which .NET SDK to use, which settings every project shares, which package versions are allowed, how the code must be formatted, what the CI service checks, and how the frontend is built and tested. This section goes through those files one at a time. For each file it shows the text, says what every setting does and links the page that documents it.

### How to read this section

- **Excerpts and quotes** work as in [section 3](#3-patterns). A quote such as `"rollForward": "latestPatch"` (line 4) is copied from an excerpt, and the number is the line it sits on in the file.
- **What a setting does** is taken from the linked documentation and put in plain words. Where the pages that were read do not describe a setting, the text gives the line as it stands and says so.
- **Documented default** in a table is what the tool does when the setting is left out, according to the linked page.
- **The sample password is never shown.** The repository holds a sample password for the optional SQL Server container. Lines that contain it are left out of the excerpts.

### The files at a glance

| File | Read by | What it sets |
|---|---|---|
| [global.json](global.json) | the `dotnet` command | Which version of the .NET SDK is used. |
| [Directory.Build.props](Directory.Build.props) | MSBuild, the build engine of .NET | Build settings shared by every project. |
| [Directory.Packages.props](Directory.Packages.props) | NuGet, the package manager of .NET | The version of every NuGet package. |
| The seven `.csproj` files | MSBuild | Per project: the project SDK, the target framework and the references. |
| [Northstar.PayrollCompliance.sln](Northstar.PayrollCompliance.sln) | the `dotnet` command and Visual Studio | Which projects belong together. |
| [.editorconfig](.editorconfig) | editors, the code-style analyzers and `dotnet format` | Indentation, line endings and C# style preferences. |
| [Api/appsettings.json](src/Northstar.Api/appsettings.json) and its two siblings | the configuration system of ASP.NET Core | The database provider, the connection string, the settings of the provider client and the log levels. |
| [Api/launchSettings.json](src/Northstar.Api/Properties/launchSettings.json) | `dotnet run` and Visual Studio | The URL and the environment name for a local run. |
| [Api/Northstar.Api.http](src/Northstar.Api/Northstar.Api.http), [docs/api/example-requests.http](docs/api/example-requests.http) | the HTTP file editor of Visual Studio | Sample requests that can be sent with one click. |
| [.github/workflows/ci.yml](.github/workflows/ci.yml) | GitHub Actions | What is checked on every pull request and on every push to `main`. |
| [docker-compose.yml](docker-compose.yml) | Docker Compose | Two optional services: SQL Server and a storage emulator. |
| [Web/angular.json](src/Northstar.Web/angular.json) | the Angular CLI | How the frontend is built, served and tested. |
| [Web/package.json](src/Northstar.Web/package.json), [e2e/package.json](tests/e2e/package.json) | npm | The npm packages and the scripts behind `npm start` and `npm test`. |
| [Web/tsconfig.json](src/Northstar.Web/tsconfig.json) and its two children | the TypeScript compiler and the Angular compiler | Type-checking options. |
| [Web/.prettierrc](src/Northstar.Web/.prettierrc), [Web/.editorconfig](src/Northstar.Web/.editorconfig) | Prettier and editors | Formatting of the frontend code. |
| [Web/proxy.conf.json](src/Northstar.Web/proxy.conf.json) | the Angular development server | Which URLs are passed on to the API. |
| [Web/vitest.config.ts](src/Northstar.Web/vitest.config.ts) | Vitest | Settings of the unit test runner. |
| [e2e/playwright.config.ts](tests/e2e/playwright.config.ts) | Playwright | Settings of the end-to-end test runner. |
| [.gitignore](.gitignore) | git | Which files are never committed. |

### The SDK version: `global.json`

The *.NET SDK* is the set of tools behind the `dotnet` command. `global.json` defines which SDK version is used when a `dotnet` command runs in this folder. The documentation adds that this choice is separate from the runtime version a project targets, which is set in the project files ([global.json overview](https://learn.microsoft.com/en-us/dotnet/core/tools/global-json)).

**[`global.json`](global.json#L1-L6)** · lines 1–6

<!-- x:global-json -->
```json
{
  "sdk": {
    "version": "10.0.301",
    "rollForward": "latestPatch"
  }
}
```

1. `"version": "10.0.301"` (line 3) names the SDK. An SDK version has the form `x.y.znn`: `x` is the major version, `y` the minor version, `z` the *feature band* and `nn` the patch. So `10.0.301` is .NET 10.0, feature band 3, patch 01.
2. `"rollForward": "latestPatch"` (line 4) says what to do when that exact version is not the one installed. With `latestPatch` the `dotnet` command uses the latest installed patch level that has the same major version, minor version and feature band and is not lower than the one asked for. If there is none, the command fails ([global.json: rollForward](https://learn.microsoft.com/en-us/dotnet/core/tools/global-json#rollforward)). Here that means any installed SDK from `10.0.301` to `10.0.399`.

The README names the same version in its list of prerequisites: ".NET SDK 10.0.301 or a compatible 10.0 feature band" ([README.md:22](README.md#L22)). Without a `global.json` the highest installed SDK would be used ([global.json overview](https://learn.microsoft.com/en-us/dotnet/core/tools/global-json)).

**Read more.** [global.json overview](https://learn.microsoft.com/en-us/dotnet/core/tools/global-json) · [global.json: rollForward](https://learn.microsoft.com/en-us/dotnet/core/tools/global-json#rollforward) · [.NET project SDKs](https://learn.microsoft.com/en-us/dotnet/core/project-sdk/overview)

### Settings for every project: `Directory.Build.props`

MSBuild is the engine that builds a .NET project. Before it reads a project file, it looks for a file named `Directory.Build.props`. It starts in the folder of the project, walks up through the parent folders and stops at the first file it finds. That file is imported early, so a setting in it applies to the project unless the project file sets the same property again ([Customize the build by folder (Directory.Build.props)](https://learn.microsoft.com/en-us/visualstudio/msbuild/customize-by-directory)). This repository has one such file, in the root, so all 7 projects get it.

**[`Directory.Build.props`](Directory.Build.props#L1-L10)** · lines 1–10

<!-- x:build-props -->
```xml
<Project>
  <PropertyGroup>
    <AnalysisLevel>latest</AnalysisLevel>
    <EnforceCodeStyleInBuild>true</EnforceCodeStyleInBuild>
    <ImplicitUsings>enable</ImplicitUsings>
    <ManagePackageVersionsCentrally>true</ManagePackageVersionsCentrally>
    <Nullable>enable</Nullable>
    <TreatWarningsAsErrors>true</TreatWarningsAsErrors>
  </PropertyGroup>
</Project>
```

Line by line:

- `<AnalysisLevel>latest</AnalysisLevel>` (line 3) chooses the set of code-analysis rules. *Code analysis* is the checking that runs beside the compiler: rules that look for likely mistakes and for style ([Code analysis in .NET](https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/overview)). With `latest`, the newest released analyzers are used ([AnalysisLevel](https://learn.microsoft.com/en-us/dotnet/core/project-sdk/msbuild-props#analysislevel)).
- `<EnforceCodeStyleInBuild>true</EnforceCodeStyleInBuild>` (line 4) makes the build check code style as well. The style rules are the ones whose identifiers start with `IDE`. By default they are not checked in a command-line build, and this property switches them on ([EnforceCodeStyleInBuild](https://learn.microsoft.com/en-us/dotnet/core/project-sdk/msbuild-props#enforcecodestyleinbuild)). The preferences they check come from `.editorconfig`, described under [Code style](#code-style-editorconfig-and-dotnet-format) below.
- `<ImplicitUsings>enable</ImplicitUsings>` (line 5) adds `global using` directives for a fixed list of namespaces to every project, so the files do not have to repeat them ([Implicit using directives](https://learn.microsoft.com/en-us/dotnet/core/project-sdk/overview#implicit-using-directives)). The list depends on the project SDK. It is shown in the table after this list.
- `<ManagePackageVersionsCentrally>true</ManagePackageVersionsCentrally>` (line 6) turns on *central package management*: package versions are written once, in `Directory.Packages.props`, and the project files name packages without a version ([Central Package Management](https://learn.microsoft.com/en-us/nuget/consume-packages/central-package-management)).
- `<Nullable>enable</Nullable>` (line 7) turns on [nullable reference types](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/nullable-reference-types). The compiler then tracks which references may be `null` and warns where a possible `null` is used without a check ([Nullable reference types](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/null-safety/nullable-reference-types), [Pattern 19](#pattern-19-small-conventions-used-everywhere)).
- `<TreatWarningsAsErrors>true</TreatWarningsAsErrors>` (line 8) makes the C# compiler report every warning as an error. The documentation notes that it applies to the compiler and not to other build tasks ([TreatWarningsAsErrors](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/compiler-options/errors-warnings#treatwarningsaserrors)).

Lines 7 and 8 work together. A nullable warning is a compiler warning, and line 8 turns compiler warnings into errors, so a possible `null` that is not handled stops the build.

| Project SDK | Namespaces added by `ImplicitUsings` |
|---|---|
| `Microsoft.NET.Sdk` | `System`, `System.Collections.Generic`, `System.IO`, `System.Linq`, `System.Net.Http`, `System.Threading`, `System.Threading.Tasks` |
| `Microsoft.NET.Sdk.Web` | The seven above, and `System.Net.Http.Json`, `Microsoft.AspNetCore.Builder`, `Microsoft.AspNetCore.Hosting`, `Microsoft.AspNetCore.Http`, `Microsoft.AspNetCore.Routing`, `Microsoft.Extensions.Configuration`, `Microsoft.Extensions.DependencyInjection`, `Microsoft.Extensions.Hosting`, `Microsoft.Extensions.Logging` |

The effect shows in the code. [Api/Program.cs:1-4](src/Northstar.Api/Program.cs#L1-L4) has four `using` lines, and none of them names `Microsoft.AspNetCore.Builder`, the namespace of `WebApplication`. The Web SDK list supplies it. No C# file in the solution writes `using System.Linq;` or `using System.Threading.Tasks;`. The Infrastructure project uses the plain SDK, so it writes `using Microsoft.Extensions.DependencyInjection;` itself ([Infrastructure/DependencyInjection.cs:3](src/Northstar.Infrastructure/DependencyInjection.cs#L3)).

Two things are not in this file. No `<LangVersion>` is set, so the C# version follows the target framework ([section 4](#4-c-language-features)). And six of the seven project files set `Nullable` and `ImplicitUsings` again, to the same values, for example [Api/Northstar.Api.csproj:5-6](src/Northstar.Api/Northstar.Api.csproj#L5-L6). The project file of the fake provider sets only the target framework ([FakeProvider/Northstar.FakePayrollProvider.csproj:4](tools/Northstar.FakePayrollProvider/Northstar.FakePayrollProvider.csproj#L4)) and takes both settings from `Directory.Build.props`.

**Read more.** [Customize the build by folder (Directory.Build.props)](https://learn.microsoft.com/en-us/visualstudio/msbuild/customize-by-directory) · [Common MSBuild project properties](https://learn.microsoft.com/en-us/visualstudio/msbuild/common-msbuild-project-properties) · [Code analysis in .NET](https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/overview) · [AnalysisLevel](https://learn.microsoft.com/en-us/dotnet/core/project-sdk/msbuild-props#analysislevel) · [EnforceCodeStyleInBuild](https://learn.microsoft.com/en-us/dotnet/core/project-sdk/msbuild-props#enforcecodestyleinbuild) · [Implicit using directives](https://learn.microsoft.com/en-us/dotnet/core/project-sdk/overview#implicit-using-directives) · [Central Package Management](https://learn.microsoft.com/en-us/nuget/consume-packages/central-package-management) · [Nullable reference types](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/null-safety/nullable-reference-types) · [TreatWarningsAsErrors](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/compiler-options/errors-warnings#treatwarningsaserrors) · [Configure the C# language version](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/configure-language-version)

### Project files and the solution file

Every project has a `.csproj` file. Its first line names a *project SDK*: a set of build targets and tasks that knows how to compile, pack and publish one kind of project ([.NET project SDKs](https://learn.microsoft.com/en-us/dotnet/core/project-sdk/overview)). Two are used here. `Microsoft.NET.Sdk` is the base SDK. `Microsoft.NET.Sdk.Web` is the SDK for ASP.NET Core apps: it builds on the base SDK and references the ASP.NET Core shared framework, so a web project needs no package reference for ASP.NET Core itself ([The Web SDK (Microsoft.NET.Sdk.Web)](https://learn.microsoft.com/en-us/aspnet/core/razor-pages/web-sdk?view=aspnetcore-10.0)).

| Project file | SDK | Project references | Package references |
|---|---|---|---|
| [Domain/Northstar.Domain.csproj](src/Northstar.Domain/Northstar.Domain.csproj) | `Microsoft.NET.Sdk` | none | none |
| [Application/Northstar.Application.csproj](src/Northstar.Application/Northstar.Application.csproj) | `Microsoft.NET.Sdk` | Domain | `Microsoft.EntityFrameworkCore` |
| [Infrastructure/Northstar.Infrastructure.csproj](src/Northstar.Infrastructure/Northstar.Infrastructure.csproj) | `Microsoft.NET.Sdk` | Application, Domain | `Microsoft.EntityFrameworkCore.Design`, `Microsoft.EntityFrameworkCore.Sqlite`, `Microsoft.EntityFrameworkCore.SqlServer`, `Microsoft.Extensions.Hosting.Abstractions`, `Microsoft.Extensions.Http` |
| [Api/Northstar.Api.csproj](src/Northstar.Api/Northstar.Api.csproj) | `Microsoft.NET.Sdk.Web` | Application, Infrastructure | `Microsoft.AspNetCore.OpenApi`, `Microsoft.OpenApi` |
| [UnitTests/Northstar.Domain.UnitTests.csproj](tests/Northstar.Domain.UnitTests/Northstar.Domain.UnitTests.csproj) | `Microsoft.NET.Sdk` | Domain | `coverlet.collector`, `Microsoft.NET.Test.Sdk`, `xunit`, `xunit.runner.visualstudio` |
| [IntegrationTests/Northstar.Api.IntegrationTests.csproj](tests/Northstar.Api.IntegrationTests/Northstar.Api.IntegrationTests.csproj) | `Microsoft.NET.Sdk` | Api, Infrastructure | `coverlet.collector`, `Microsoft.AspNetCore.Mvc.Testing`, `Microsoft.EntityFrameworkCore.Sqlite`, `Microsoft.NET.Test.Sdk`, `xunit`, `xunit.runner.visualstudio` |
| [FakeProvider/Northstar.FakePayrollProvider.csproj](tools/Northstar.FakePayrollProvider/Northstar.FakePayrollProvider.csproj) | `Microsoft.NET.Sdk.Web` | none | none |

The project references are the arrows of [Pattern 1](#pattern-1-layered-modular-monolith): Domain refers to nothing, and each layer refers only to layers further in. Every project sets `<TargetFramework>net10.0</TargetFramework>`.

The project file of the integration tests shows the parts that the others share:

**[`tests/Northstar.Api.IntegrationTests/Northstar.Api.IntegrationTests.csproj`](tests/Northstar.Api.IntegrationTests/Northstar.Api.IntegrationTests.csproj#L3-L26)** · lines 3–26

<!-- x:csproj-tests -->
```xml
<PropertyGroup>
  <TargetFramework>net10.0</TargetFramework>
  <ImplicitUsings>enable</ImplicitUsings>
  <Nullable>enable</Nullable>
  <IsPackable>false</IsPackable>
</PropertyGroup>

<ItemGroup>
  <PackageReference Include="coverlet.collector" />
  <PackageReference Include="Microsoft.AspNetCore.Mvc.Testing" />
  <PackageReference Include="Microsoft.EntityFrameworkCore.Sqlite" />
  <PackageReference Include="Microsoft.NET.Test.Sdk" />
  <PackageReference Include="xunit" />
  <PackageReference Include="xunit.runner.visualstudio" />
</ItemGroup>

<ItemGroup>
  <Using Include="Xunit" />
</ItemGroup>

<ItemGroup>
  <ProjectReference Include="..\..\src\Northstar.Api\Northstar.Api.csproj" />
  <ProjectReference Include="..\..\src\Northstar.Infrastructure\Northstar.Infrastructure.csproj" />
</ItemGroup>
```

1. `<IsPackable>false</IsPackable>` (line 7) says the project cannot be packed into a NuGet package. The default is `true` ([NuGet pack and restore as MSBuild targets](https://learn.microsoft.com/en-us/nuget/reference/msbuild-targets)). Both test projects set it.
2. The six `<PackageReference>` lines name packages and give no version, for example `<PackageReference Include="xunit" />` (line 15). The versions come from `Directory.Packages.props`, described next.
3. `<Using Include="Xunit" />` (line 20) adds one more implicit `global using` directive, for the namespace `Xunit` ([MSBuild reference: Using items](https://learn.microsoft.com/en-us/dotnet/core/project-sdk/msbuild-props#using)). That is why the test files use `[Fact]` and `Assert` without a `using Xunit;` line ([Pattern 20](#pattern-20-tests-at-three-levels-in-net)).
4. The two `<ProjectReference>` lines point at the API project and the Infrastructure project. The tests start the real API in memory and replace parts of its Infrastructure registration ([Pattern 20](#pattern-20-tests-at-three-levels-in-net)).

One package reference in the Infrastructure project is written differently: `Microsoft.EntityFrameworkCore.Design` carries `<PrivateAssets>all</PrivateAssets>` ([Infrastructure/Northstar.Infrastructure.csproj:9-12](src/Northstar.Infrastructure/Northstar.Infrastructure.csproj#L9-L12)). [Pattern 1](#pattern-1-layered-modular-monolith) explains it.

The *solution file* [Northstar.PayrollCompliance.sln](Northstar.PayrollCompliance.sln) lists the seven projects in three solution folders: `src` (Domain, Application, Infrastructure, Api), `tests` (the two test projects) and `tools` (the fake provider). A `dotnet` command that is given the solution file works on all of them at once, which is how the CI workflow restores, formats, builds and tests ([.github/workflows/ci.yml:16-19](.github/workflows/ci.yml#L16-L19)).

**Read more.** [.NET project SDKs](https://learn.microsoft.com/en-us/dotnet/core/project-sdk/overview) · [The Web SDK (Microsoft.NET.Sdk.Web)](https://learn.microsoft.com/en-us/aspnet/core/razor-pages/web-sdk?view=aspnetcore-10.0) · [Common MSBuild project properties](https://learn.microsoft.com/en-us/visualstudio/msbuild/common-msbuild-project-properties) · [NuGet pack and restore as MSBuild targets](https://learn.microsoft.com/en-us/nuget/reference/msbuild-targets) · [MSBuild reference: Using items](https://learn.microsoft.com/en-us/dotnet/core/project-sdk/msbuild-props#using) · [Controlling dependency assets](https://learn.microsoft.com/en-us/nuget/consume-packages/package-references-in-project-files#controlling-dependency-assets)

### Package versions: `Directory.Packages.props`

With central package management switched on, one file holds the version of every NuGet package in the solution. Each `<PackageVersion>` item pairs a package id with a version, and each project file refers to the package by id alone ([Central Package Management](https://learn.microsoft.com/en-us/nuget/consume-packages/central-package-management)).

**[`Directory.Packages.props`](Directory.Packages.props#L1-L17)** · lines 1–17

<!-- x:packages-props -->
```xml
<Project>
  <ItemGroup>
    <PackageVersion Include="coverlet.collector" Version="6.0.4" />
    <PackageVersion Include="Microsoft.AspNetCore.Mvc.Testing" Version="10.0.11" />
    <PackageVersion Include="Microsoft.AspNetCore.OpenApi" Version="10.0.11" />
    <PackageVersion Include="Microsoft.EntityFrameworkCore" Version="10.0.15" />
    <PackageVersion Include="Microsoft.EntityFrameworkCore.Design" Version="10.0.15" />
    <PackageVersion Include="Microsoft.EntityFrameworkCore.Sqlite" Version="10.0.15" />
    <PackageVersion Include="Microsoft.EntityFrameworkCore.SqlServer" Version="10.0.15" />
    <PackageVersion Include="Microsoft.Extensions.Hosting.Abstractions" Version="10.0.11" />
    <PackageVersion Include="Microsoft.Extensions.Http" Version="10.0.11" />
    <PackageVersion Include="Microsoft.NET.Test.Sdk" Version="17.14.1" />
    <PackageVersion Include="Microsoft.OpenApi" Version="2.12.2" />
    <PackageVersion Include="xunit" Version="2.9.3" />
    <PackageVersion Include="xunit.runner.visualstudio" Version="3.1.4" />
  </ItemGroup>
</Project>
```

The file has 13 entries. The four EF Core packages share one version (10.0.15), and the four ASP.NET Core and `Microsoft.Extensions` packages share another (10.0.11). The table under [NuGet packages](#nuget-packages) in [section 2](#2-solution-map) says what each package brings and which project refers to it.

Six of the packages exist only for the tests:

| Package | Version | Referenced by | What it is for |
|---|---|---|---|
| [xunit](https://www.nuget.org/packages/xunit/2.9.3) | 2.9.3 | both test projects | The test framework: the `[Fact]` and `[Theory]` attributes and the `Assert` class ([Pattern 20](#pattern-20-tests-at-three-levels-in-net)). |
| [xunit.runner.visualstudio](https://www.nuget.org/packages/xunit.runner.visualstudio/3.1.4) | 3.1.4 | both test projects | The adapter that lets `dotnet test` and the Test Explorer of Visual Studio discover and run xUnit tests. |
| [Microsoft.NET.Test.Sdk](https://www.nuget.org/packages/Microsoft.NET.Test.Sdk/17.14.1) | 17.14.1 | both test projects | The test host that lets `dotnet test` and Visual Studio run a test project. |
| [coverlet.collector](https://www.nuget.org/packages/coverlet.collector/6.0.4) | 6.0.4 | both test projects | A data collector for code coverage. It takes effect when `dotnet test` runs with `--collect:"XPlat Code Coverage"`, and it writes a `coverage.cobertura.xml` file ([Use code coverage for unit testing](https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-code-coverage)). |
| [Microsoft.AspNetCore.Mvc.Testing](https://www.nuget.org/packages/Microsoft.AspNetCore.Mvc.Testing/10.0.11) | 10.0.11 | integration tests | `WebApplicationFactory<TEntryPoint>`, which runs the API on an in-memory test server ([Pattern 20](#pattern-20-tests-at-three-levels-in-net)). |
| [Microsoft.EntityFrameworkCore.Sqlite](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Sqlite/10.0.15) | 10.0.15 | integration tests, Infrastructure | The SQLite provider of EF Core. The README says of the tests: "The API integration tests use a fresh SQLite in-memory database for every test." ([README.md:82](README.md#L82)). |

**Read more.** [Central Package Management](https://learn.microsoft.com/en-us/nuget/consume-packages/central-package-management) · [Central Package Management: transitive pinning](https://learn.microsoft.com/en-us/nuget/consume-packages/central-package-management#transitive-pinning) · [Getting started with xUnit.net v2](https://xunit.net/docs/getting-started/v2/getting-started) · [dotnet test](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-test) · [Use code coverage for unit testing](https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-code-coverage)

### Code style: `.editorconfig` and `dotnet format`

An `.editorconfig` file holds formatting and style settings as `name = value` lines, grouped under headers that say which files they apply to. Editors read it, and so do the .NET code-style analyzers and `dotnet format` ([EditorConfig](https://editorconfig.org/), [Code-style rule options (.editorconfig)](https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/code-style-rule-options)).

**[`.editorconfig`](.editorconfig#L1-L22)** · lines 1–22

<!-- x:editorconfig -->
```ini
root = true

[*]
charset = utf-8
end_of_line = lf
insert_final_newline = true
indent_style = space
indent_size = 2
trim_trailing_whitespace = true

[*.cs]
indent_size = 4
dotnet_sort_system_directives_first = true
csharp_new_line_before_open_brace = all
csharp_style_namespace_declarations = file_scoped:error
csharp_style_var_when_type_is_apparent = true:suggestion

[*.{csproj,props,targets}]
indent_size = 2

[*.md]
trim_trailing_whitespace = false
```

**The general settings.** `root = true` (line 1) stops the search for further `.editorconfig` files in parent folders. The header `[*]` applies to every file:

| Line | Setting | What it does |
|---:|---|---|
| 4 | `charset = utf-8` | The character set of the files. |
| 5 | `end_of_line = lf` | How line breaks are stored: a single line feed. |
| 6 | `insert_final_newline = true` | A file ends with a newline when it is saved. |
| 7 | `indent_style = space` | Indentation uses spaces, not tab characters. |
| 8 | `indent_size = 2` | Two columns for each level of indentation. |
| 9 | `trim_trailing_whitespace = true` | Whitespace before a line break is removed. |

Later groups override earlier ones for the files they match. C# files get four columns (`indent_size = 4` (line 12)), project and props files are set to two again (line 19), and for Markdown files the trimming of trailing whitespace is switched off (line 22).

**The C# settings.** Four lines under `[*.cs]` are specific to .NET:

1. `dotnet_sort_system_directives_first = true` (line 13) sorts the `using` directives for `System` namespaces before the others ([.NET formatting options](https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/style-rules/dotnet-formatting-options)).
2. `csharp_new_line_before_open_brace = all` (line 14) puts every opening brace on a line of its own, the layout known as Allman style ([C# formatting options](https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/style-rules/csharp-formatting-options)).
3. `csharp_style_namespace_declarations = file_scoped:error` (line 15) prefers [file-scoped namespaces](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/namespace) such as `namespace Northstar.Domain;` over a namespace with braces. The rule behind it is IDE0161 ([Namespace declaration preferences (IDE0160 and IDE0161)](https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/style-rules/ide0160-ide0161)). All 52 namespace declarations in the code are file-scoped.
4. `csharp_style_var_when_type_is_apparent = true:suggestion` (line 16) prefers `var` when the type is already written on the right-hand side, as in `var options = new JsonSerializerOptions();`. The rule is IDE0007 (['var' preferences (IDE0007 and IDE0008)](https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/style-rules/ide0007-ide0008)).

**The part after the colon.** Lines 15 and 16 end in `:error` and `:suggestion`. A style option can be written as `option = value` or as `option = value:severity`, where the severity says how a violation is reported ([Language rules: option format](https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/style-rules/language-rules#option-format)). With `error`, violations appear as build errors and make the build fail. With `suggestion`, they appear as build messages and as suggestions in Visual Studio ([Code analysis: severity levels](https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/configuration-options#severity-level)). The documentation states that from .NET 9 on this severity is respected in command-line builds as well as in the editor ([Language rules: option format](https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/style-rules/language-rules#option-format)). Together with `EnforceCodeStyleInBuild` in `Directory.Build.props`, line 15 means that a namespace written with braces is reported as an error.

**`dotnet format`.** The command formats code to match the `.editorconfig` settings. With `--verify-no-changes` it changes nothing and ends with a non-zero exit code if any file would have been formatted ([dotnet format](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-format)). The CI workflow runs it in that mode: `dotnet format Northstar.PayrollCompliance.sln --verify-no-changes --no-restore` ([.github/workflows/ci.yml:17](.github/workflows/ci.yml#L17)).

**The frontend has its own file.** [Web/.editorconfig](src/Northstar.Web/.editorconfig) also starts with `root = true`, so for files under `src/Northstar.Web` the search stops there and the root file is not read. Its `[*]` group sets the same character set, indentation and trimming as the root file. Its `[*.ts]` group holds `quote_type = single` and `ij_typescript_use_double_quotes = false`, and its `[*.md]` group holds `max_line_length = off`. These three names are not among the properties that editorconfig.org describes, so they are given here as they stand. Prettier, the formatter of the frontend, also reads `.editorconfig` and turns the properties it knows into its own settings, and the settings in `.prettierrc` take priority ([Prettier configuration file](https://prettier.io/docs/configuration)).

**Read more.** [EditorConfig](https://editorconfig.org/) · [Code-style rule options (.editorconfig)](https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/code-style-rule-options) · [Language rules: option format](https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/style-rules/language-rules#option-format) · [Code analysis: severity levels](https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/configuration-options#severity-level) · [Namespace declaration preferences (IDE0160 and IDE0161)](https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/style-rules/ide0160-ide0161) · ['var' preferences (IDE0007 and IDE0008)](https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/style-rules/ide0007-ide0008) · [C# formatting options](https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/style-rules/csharp-formatting-options) · [.NET formatting options](https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/style-rules/dotnet-formatting-options) · [dotnet format](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-format)

### Application settings: `appsettings.json`

ASP.NET Core gathers its *configuration* from several sources and hands the result to the code as one set of keys and values. `appsettings.json` is one of those sources ([Configuration: appsettings.json](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-10.0#app-settings-file-configuration-appsettingsjson-appsettingsenvironmentjson)).

**[`src/Northstar.Api/appsettings.json`](src/Northstar.Api/appsettings.json#L1-L22)** · lines 1–22

<!-- x:appsettings -->
```json
{
  "Database": {
    "Provider": "Sqlite"
  },
  "ConnectionStrings": {
    "Northstar": "Data Source=northstar.db"
  },
  "SeedData": true,
  "PayrollProvider": {
    "BaseUrl": "http://localhost:5130",
    "MaxAttempts": 3,
    "BaseRetryDelayMilliseconds": 100,
    "AttemptTimeoutMilliseconds": 10000
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

A nested name is joined to its parent with a colon when the code asks for a value. The value on line 3 has the key `Database:Provider`.

| Key | Value here | Read by | See |
|---|---|---|---|
| `Database:Provider` | `Sqlite` | `configuration["Database:Provider"]` in [Infrastructure/DependencyInjection.cs:17](src/Northstar.Infrastructure/DependencyInjection.cs#L17). It decides between `UseSqlServer` and `UseSqlite`. | [Pattern 2](#pattern-2-composition-root-and-service-registration) |
| `ConnectionStrings:Northstar` | `Data Source=northstar.db` | `configuration.GetConnectionString("Northstar")` in [Infrastructure/DependencyInjection.cs:18](src/Northstar.Infrastructure/DependencyInjection.cs#L18) | [Pattern 2](#pattern-2-composition-root-and-service-registration) |
| `SeedData` | `true` | `app.Configuration.GetValue("SeedData", true)` in [Api/Program.cs:37](src/Northstar.Api/Program.cs#L37) | [Pattern 4](#pattern-4-request-pipeline-and-host-startup) |
| `PayrollProvider:BaseUrl`, `MaxAttempts`, `BaseRetryDelayMilliseconds`, `AttemptTimeoutMilliseconds` | `http://localhost:5130`, `3`, `100`, `10000` | Bound to the class `PayrollProviderOptions` and validated in [Infrastructure/DependencyInjection.cs:39-46](src/Northstar.Infrastructure/DependencyInjection.cs#L39-L46). The section name is a constant ([Infrastructure/PayrollProviderOptions.cs:5](src/Northstar.Infrastructure/Integrations/PayrollProviderOptions.cs#L5)). | [Pattern 11](#pattern-11-typed-httpclient-with-validated-options), [Pattern 12](#pattern-12-bounded-retries-timeouts-and-exception-translation) |
| `Logging:LogLevel` | `Default`: `Information`, `Microsoft.AspNetCore`: `Warning` | the logging system of .NET | below |
| `AllowedHosts` | `*` | the host filtering middleware | below |

**Log levels.** `LogLevel` gives the lowest level that is written for a *category*. By convention the category is the full name of the class that writes the log entry. The levels, from lowest to highest, are `Trace`, `Debug`, `Information`, `Warning`, `Error` and `Critical`. `"Default": "Information"` (line 17) applies to every category without an entry of its own. `"Microsoft.AspNetCore": "Warning"` (line 18) applies to every category whose name starts with `Microsoft.AspNetCore`, so entries from ASP.NET Core itself are written only from `Warning` up ([Logging in .NET and ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/logging/?view=aspnetcore-10.0)).

**Allowed hosts.** `"AllowedHosts": "*"` (line 21) configures the host filtering middleware. The value is a list of host names separated by semicolons, without port numbers, and the middleware matches the `Host` header of each request against it. The value `*` is a wildcard: every non-empty `Host` header is allowed ([Host filtering with the Kestrel web server](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/servers/kestrel/host-filtering?view=aspnetcore-10.0)).

**Which source wins.** When two sources hold the same key, the source with the higher priority wins. For an app built with `WebApplication.CreateBuilder`, the documentation gives this order, from highest priority to lowest ([Configuration in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-10.0)):

1. Command-line arguments.
2. Environment variables without the prefix `ASPNETCORE_` or `DOTNET_`.
3. User secrets, when the app runs in the Development environment.
4. `appsettings.{Environment}.json`, for example `appsettings.Development.json`.
5. `appsettings.json`.
6. Host configuration, as a fallback.

The repository uses three of these levels:

- **A command-line argument.** The fake provider is started with `--urls http://localhost:5130` ([README.md:32](README.md#L32)). `--urls` gives the addresses the server listens on ([Configure endpoints for the Kestrel web server](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/servers/kestrel/endpoints?view=aspnetcore-10.0)).
- **Environment variables.** The README switches the API to SQL Server by setting two variables before `dotnet run`. The first is `Database__Provider=SqlServer` ([README.md:91](README.md#L91)). In the name of an environment variable a double underscore stands for the colon, so this sets `Database:Provider` ([Configuration: environment variables provider](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-10.0#environment-variables-configuration-provider)). Environment variables rank above both JSON files, so no file has to be edited. The end-to-end tests use the same mechanism for the connection string ([Pattern 26](#pattern-26-frontend-tests-with-vitest-and-playwright)).
- **The environment file.** [Api/appsettings.Development.json](src/Northstar.Api/appsettings.Development.json) holds the `Database`, `ConnectionStrings` and `Logging` sections with the same values as `appsettings.json`.

A third JSON file sits next to these two. [Api/appsettings.SqlServer.json](src/Northstar.Api/appsettings.SqlServer.json) holds `"Provider": "SqlServer"` and a connection string for the SQL Server container. That line contains the sample password and is not shown. The file name fits the pattern `appsettings.{Environment}.json`, with `SqlServer` in the place of the environment name. No C# file calls `AddJsonFile` or names this file, and the README sets the same two keys through environment variables ([README.md:91-93](README.md#L91-L93)).

**Read more.** [Configuration in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-10.0) · [Configuration: appsettings.json](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-10.0#app-settings-file-configuration-appsettingsjson-appsettingsenvironmentjson) · [Configuration: environment variables provider](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-10.0#environment-variables-configuration-provider) · [Logging in .NET and ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/logging/?view=aspnetcore-10.0) · [Host filtering with the Kestrel web server](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/servers/kestrel/host-filtering?view=aspnetcore-10.0) · [Configure endpoints for the Kestrel web server](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/servers/kestrel/endpoints?view=aspnetcore-10.0)

### Launch profiles: `launchSettings.json`

A *launch profile* is a named set of start-up settings for a local run. `launchSettings.json` holds the profiles of a project. The file is used only on the development machine and is not deployed when the app is published ([Set the environment with launchSettings.json](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/environments?view=aspnetcore-10.0#set-the-environment-with-the-launch-settings-file-launchsettingsjson)).

**[`src/Northstar.Api/Properties/launchSettings.json`](src/Northstar.Api/Properties/launchSettings.json#L3-L13)** · lines 3–13

<!-- x:launch-settings -->
```json
"profiles": {
  "http": {
    "commandName": "Project",
    "dotnetRunMessages": true,
    "launchBrowser": false,
    "applicationUrl": "http://localhost:5120",
    "environmentVariables": {
      "ASPNETCORE_ENVIRONMENT": "Development"
    }
  }
}
```

1. `"http": {` (line 4) opens the only profile in the file. When no profile name is given, `dotnet run` takes the first profile in the file whose `commandName` it supports ([dotnet run: launch profiles](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-run#launch-profiles)).
2. `"commandName": "Project"` (line 5) is the kind of profile: `dotnet run` builds the project and starts the program that the build produces ([dotnet run: launch profiles](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-run#launch-profiles)).
3. `"dotnetRunMessages": true` (line 6) shows a message while the project is building ([launchSettings.json schema (JSON Schema Store)](https://www.schemastore.org/launchsettings.json)).
4. `"launchBrowser": false` (line 7) tells the launching tool not to open a browser. `dotnet run` itself never opens one ([dotnet run: launch profiles](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-run#launch-profiles)).
5. `"applicationUrl": "http://localhost:5120"` (line 8) sets the variable `ASPNETCORE_URLS` in the started process, so the API listens on port 5120. The same address appears in the proxy configuration of the frontend and in the `.http` files.
6. `"ASPNETCORE_ENVIRONMENT": "Development"` (line 10) sets the *environment name*. With `Development`, `appsettings.Development.json` is loaded and `app.Environment.IsDevelopment()` is true ([Pattern 4](#pattern-4-request-pipeline-and-host-startup)). A value set here overrides a value from the system environment ([Use multiple environments in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/environments?view=aspnetcore-10.0)).

The framework knows three environment names, `Development`, `Staging` and `Production`, and any other string is allowed. When no name is set, the environment is `Production` ([Use multiple environments in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/environments?view=aspnetcore-10.0)).

A profile can be skipped. `dotnet run --no-launch-profile` does not read `launchSettings.json` at all ([dotnet run](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-run)). The Playwright configuration starts both .NET programs this way and passes the address with `--urls` instead ([e2e/playwright.config.ts:23-24](tests/e2e/playwright.config.ts#L23-L24), [e2e/playwright.config.ts:30-31](tests/e2e/playwright.config.ts#L30-L31)).

**Read more.** [Set the environment with launchSettings.json](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/environments?view=aspnetcore-10.0#set-the-environment-with-the-launch-settings-file-launchsettingsjson) · [Use multiple environments in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/environments?view=aspnetcore-10.0) · [dotnet run](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-run) · [dotnet run: launch profiles](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-run#launch-profiles) · [launchSettings.json schema (JSON Schema Store)](https://www.schemastore.org/launchsettings.json)

### Request files: `.http`

A `.http` file holds HTTP requests as plain text. Visual Studio shows a *Send request* link above each request, sends it when the link is clicked and shows the response in a pane beside the file ([Use .http files in Visual Studio](https://learn.microsoft.com/en-us/aspnet/core/test/http-files?view=aspnetcore-10.0)). The API project has a small one:

**[`src/Northstar.Api/Northstar.Api.http`](src/Northstar.Api/Northstar.Api.http#L1-L8)** · lines 1–8

<!-- x:http-file -->
```http
@host = http://localhost:5120
@organizationA = 11111111-1111-1111-1111-111111111111
@runA = aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaa1001

GET {{host}}/api/payroll-runs/{{runA}}
Accept: application/json
X-Organization-Id: {{organizationA}}
X-User-Role: PayrollManager
```

1. Lines 1 to 3 define variables. A line that starts with `@` has the form `@name=value`.
2. Line 5 is the request line: the HTTP method, then the URL. A variable is used by writing its name between double curly braces, so this request goes to `http://localhost:5120/api/payroll-runs/` followed by the id on line 3.
3. Lines 6 to 8 are headers. Each header is on a line of its own, directly under the request line. `X-User-Role: PayrollManager` (line 8) and the organization header above it are the two headers that the API reads to find the tenant and the role ([Pattern 7](#pattern-7-tenant-scoping-from-header-to-query)).
4. The file ends with a line that holds `###` ([Api/Northstar.Api.http:10](src/Northstar.Api/Northstar.Api.http#L10)). Such a line separates one request from the next, so one file can hold many requests. A line that starts with `#` or `//` is a comment.

The second file, [docs/api/example-requests.http](docs/api/example-requests.http), holds eight requests that walk through the main flows of the API. The first asks for `/health`. The others come in groups, and each group has a title after the `###`:

| Line | Title in the file | Request under it | See |
|---:|---|---|---|
| 8 | List the current tenant's runs | `GET /api/payroll-runs` | [Pattern 7](#pattern-7-tenant-scoping-from-header-to-query) |
| 13 | Calculate the seeded run | `POST /api/payroll-runs/{id}/calculate` with the header `Idempotency-Key: demo-calculate-1` | [Pattern 8](#pattern-8-idempotent-commands-with-an-idempotency-key) |
| 22 | Reusing a key with a different operation payload returns 409 | `POST /api/payroll-runs/{id}/approve` with the body `"expectedVersion": 2` | [Pattern 8](#pattern-8-idempotent-commands-with-an-idempotency-key), [Pattern 16](#pattern-16-optimistic-concurrency-with-a-version-number) |
| 32 | Queue a mixed provider import (returns 202 + Location) | `POST /api/import-batches` with `"providerScenario": "mixed"` | [Pattern 9](#pattern-9-accept-now-process-later) |
| 45 | Poll status and inspect rejected/duplicate rows | `GET /api/import-batches/{id}` and `GET /api/import-batches/{id}/errors` | [Pattern 9](#pattern-9-accept-now-process-later), [Pattern 25](#pattern-25-polling-with-rxjs-and-automatic-cleanup) |
| 54 | Retry a transient terminal failure idempotently | `POST /api/import-batches/{id}/retry` with the header `Idempotency-Key: demo-import-retry-1` | [Pattern 8](#pattern-8-idempotent-commands-with-an-idempotency-key), [Pattern 12](#pattern-12-bounded-retries-timeouts-and-exception-translation) |

The import requests need the id of a batch. The variable for it starts as a placeholder, `@batch = replace-with-import-batch-id` ([docs/api/example-requests.http:4](docs/api/example-requests.http#L4)), to be replaced with the id that the `POST` on line 33 returns. The table under [HTTP endpoints](#http-endpoints) in [section 2](#2-solution-map) lists every endpoint with its controller action.

**Read more.** [Use .http files in Visual Studio](https://learn.microsoft.com/en-us/aspnet/core/test/http-files?view=aspnetcore-10.0)

### The CI workflow: `.github/workflows/ci.yml`

*Continuous integration* (CI) means that a server builds and tests the code on every change. This repository uses GitHub Actions for it. A *workflow* is a YAML file that says when to run and what to do. It is made of *jobs*, and each job is a sequence of *steps* ([GitHub Actions: workflow syntax](https://docs.github.com/en/actions/reference/workflows-and-actions/workflow-syntax)).

The file has three jobs: `backend`, `web` and `e2e`. Jobs run in parallel unless one of them names another under `needs`, and none does here. Each job starts with `runs-on: ubuntu-latest`, which names the type of machine that the job runs on.

**The trigger and the backend job**

**[`.github/workflows/ci.yml`](.github/workflows/ci.yml#L1-L19)** · lines 1–19

<!-- x:ci-backend -->
```yaml
name: CI

on:
  pull_request:
  push:
    branches: [main]

jobs:
  backend:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v4
      - uses: actions/setup-dotnet@v4
        with:
          dotnet-version: 10.0.x
      - run: dotnet restore Northstar.PayrollCompliance.sln
      - run: dotnet format Northstar.PayrollCompliance.sln --verify-no-changes --no-restore
      - run: dotnet build Northstar.PayrollCompliance.sln --no-restore --configuration Release
      - run: dotnet test Northstar.PayrollCompliance.sln --no-build --configuration Release --logger trx
```

1. Lines 3 to 6 say when the workflow runs: for every pull request, and for every push to the branch `main`.
2. `uses: actions/checkout@v4` (line 12) runs a ready-made *action*. This one checks out the repository so that the later steps can work on the files ([actions/checkout](https://github.com/actions/checkout)).
3. `uses: actions/setup-dotnet@v4` (line 13) installs a .NET SDK. The version is given as `dotnet-version: 10.0.x` (line 15), which stands for the latest patch version on the 10.0 channel ([actions/setup-dotnet](https://github.com/actions/setup-dotnet)).
4. `dotnet restore Northstar.PayrollCompliance.sln` (line 16) restores the dependencies of the projects, which means downloading the NuGet packages they refer to ([dotnet restore](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-restore)).
5. `dotnet format Northstar.PayrollCompliance.sln --verify-no-changes --no-restore` (line 17) checks the formatting. It changes nothing, and it fails the job if any file would have been changed ([dotnet format](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-format)).
6. `dotnet build Northstar.PayrollCompliance.sln --no-restore --configuration Release` (line 18) compiles all seven projects. `--no-restore` skips the restore that `dotnet build` would otherwise run first, since step 4 did it. `--configuration Release` chooses the build configuration, which is `Debug` when the option is left out ([dotnet build](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-build)). The settings of `Directory.Build.props` apply here, so a compiler warning fails this step.
7. `dotnet test Northstar.PayrollCompliance.sln --no-build --configuration Release --logger trx` (line 19) runs both test projects. `--no-build` uses the output of step 6 and does not build again. `--logger trx` writes the results to a file in the TRX format ([dotnet test with VSTest](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-test-vstest)).

**The web job**

**[`.github/workflows/ci.yml`](.github/workflows/ci.yml#L21-L35)** · lines 21–35

<!-- x:ci-web -->
```yaml
web:
  runs-on: ubuntu-latest
  defaults:
    run:
      working-directory: src/Northstar.Web
  steps:
    - uses: actions/checkout@v4
    - uses: actions/setup-node@v4
      with:
        node-version: 22
        cache: npm
        cache-dependency-path: src/Northstar.Web/package-lock.json
    - run: npm ci
    - run: npm run build
    - run: npm test
```

1. Lines 23 to 25 set a default for the job: every `run` step starts in the folder `src/Northstar.Web` ([GitHub Actions: workflow syntax](https://docs.github.com/en/actions/reference/workflows-and-actions/workflow-syntax)).
2. `uses: actions/setup-node@v4` (line 28) installs Node.js, here version 22. `cache: npm` (line 31) keeps the download cache of npm between runs. The action caches that global package data and not the `node_modules` folder. The cache key is a hash of the file named on line 32, the lock file ([actions/setup-node](https://github.com/actions/setup-node)).
3. `run: npm ci` (line 33) installs the packages exactly as the lock file lists them. `npm ci` needs a `package-lock.json`, stops with an error if it does not match `package.json`, removes an existing `node_modules` folder first, and never writes to either file ([npm ci](https://docs.npmjs.com/cli/v11/commands/npm-ci)).
4. `run: npm run build` (line 34) runs the script named `build`, which is `ng build` ([Web/package.json:7](src/Northstar.Web/package.json#L7)).
5. `run: npm test` (line 35) runs the script named `test`, which is `ng test --watch=false` ([Web/package.json:9](src/Northstar.Web/package.json#L9)). These are the component tests of [Pattern 26](#pattern-26-frontend-tests-with-vitest-and-playwright).

**The end-to-end job**

**[`.github/workflows/ci.yml`](.github/workflows/ci.yml#L37-L64)** · lines 37–64

<!-- x:ci-e2e -->
```yaml
e2e:
  runs-on: ubuntu-latest
  timeout-minutes: 15
  defaults:
    run:
      working-directory: tests/e2e
  steps:
    - uses: actions/checkout@v4
    - uses: actions/setup-dotnet@v4
      with:
        dotnet-version: 10.0.x
    - uses: actions/setup-node@v4
      with:
        node-version: 22
        cache: npm
        cache-dependency-path: |
          src/Northstar.Web/package-lock.json
          tests/e2e/package-lock.json
    - run: npm ci --prefix ../../src/Northstar.Web
    - run: npm ci
    - run: npx playwright install --with-deps chromium
    - run: npm test
    - uses: actions/upload-artifact@v4
      if: failure()
      with:
        name: playwright-report
        path: tests/e2e/playwright-report
        retention-days: 7
```

1. `timeout-minutes: 15` (line 39) cancels the job when it runs longer than 15 minutes. Without the line the limit is 360 minutes ([GitHub Actions: workflow syntax](https://docs.github.com/en/actions/reference/workflows-and-actions/workflow-syntax)).
2. The job needs both toolchains, because Playwright starts the two .NET programs and the Angular development server itself ([Pattern 26](#pattern-26-frontend-tests-with-vitest-and-playwright)). So it installs the .NET SDK and Node.js, and the npm cache key is built from two lock files (lines 53 and 54).
3. `npm ci --prefix ../../src/Northstar.Web` (line 55) installs the packages of the frontend. `--prefix` runs npm in another folder without changing the working directory ([npm: config](https://docs.npmjs.com/cli/v11/using-npm/config)). Then `- run: npm ci` on line 56 installs the packages of the test project itself.
4. `npx playwright install --with-deps chromium` (line 57) downloads the browser that the tests drive. `--with-deps` also installs the operating-system packages the browser needs, and naming `chromium` limits the download to that one browser ([Playwright: browsers](https://playwright.dev/docs/browsers)).
5. `run: npm test` (line 58) runs `playwright test` ([e2e/package.json:6](tests/e2e/package.json#L6)).
6. The last step uploads the HTML report as an *artifact*: a file or a set of files that a workflow run produces and that is kept after the job has ended ([GitHub Actions: workflow artifacts](https://docs.github.com/en/actions/concepts/workflows-and-actions/workflow-artifacts)). `if: failure()` (line 60) makes the step run only when an earlier step of the job has failed ([GitHub Actions: expressions](https://docs.github.com/en/actions/reference/workflows-and-actions/expressions)). `retention-days: 7` (line 64) removes the artifact after seven days ([actions/upload-artifact](https://github.com/actions/upload-artifact)).

**The `CI` variable.** GitHub Actions sets the environment variable `CI` to `true` in every run ([GitHub Actions: variables](https://docs.github.com/en/actions/reference/workflows-and-actions/variables)). The Playwright configuration reads it on four lines ([e2e/playwright.config.ts:7](tests/e2e/playwright.config.ts#L7) and lines 27, 34 and 40): on the CI machine a test marked `.only` is an error, and a server that is already running is not reused ([Pattern 26](#pattern-26-frontend-tests-with-vitest-and-playwright)).

The README gives the same commands for a local run ([README.md:69-79](README.md#L69-L79)).

**Read more.** [GitHub Actions: workflow syntax](https://docs.github.com/en/actions/reference/workflows-and-actions/workflow-syntax) · [GitHub Actions and .NET](https://learn.microsoft.com/en-us/dotnet/devops/github-actions-overview) · [actions/checkout](https://github.com/actions/checkout) · [actions/setup-dotnet](https://github.com/actions/setup-dotnet) · [actions/setup-node](https://github.com/actions/setup-node) · [actions/upload-artifact](https://github.com/actions/upload-artifact) · [GitHub Actions: workflow artifacts](https://docs.github.com/en/actions/concepts/workflows-and-actions/workflow-artifacts) · [GitHub Actions: expressions](https://docs.github.com/en/actions/reference/workflows-and-actions/expressions) · [GitHub Actions: variables](https://docs.github.com/en/actions/reference/workflows-and-actions/variables) · [dotnet restore](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-restore) · [dotnet build](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-build) · [dotnet test with VSTest](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-test-vstest) · [npm ci](https://docs.npmjs.com/cli/v11/commands/npm-ci)

### Optional services: `docker-compose.yml`

Docker Compose starts one or more *containers* from a single YAML file. A container is a program that runs in isolation with everything it needs, started from a packaged *image* ([Docker Compose documentation](https://docs.docker.com/compose/)). The API does not need this file for a normal run. The README lists Docker among the prerequisites only for "the optional SQL Server/Azurite services" ([README.md:25](README.md#L25)).

**[`docker-compose.yml`](docker-compose.yml#L1-L5)** · lines 1–5 and 7–28

<!-- x:compose -->
```yaml
services:
  sqlserver:
    image: mcr.microsoft.com/mssql/server:2022-latest
    environment:
      ACCEPT_EULA: "Y"
    # ... line 6 omitted ...
    ports:
      - "1433:1433"
    volumes:
      - northstar-sql:/var/opt/mssql
    healthcheck:
      test: ["CMD-SHELL", "/opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P '$$MSSQL_SA_PASSWORD' -C -Q 'SELECT 1' || exit 1"]
      interval: 10s
      timeout: 5s
      retries: 12

  azurite:
    image: mcr.microsoft.com/azure-storage/azurite:latest
    command: azurite --blobHost 0.0.0.0 --queueHost 0.0.0.0
    ports:
      - "10000:10000"
      - "10001:10001"
    volumes:
      - northstar-azurite:/data

volumes:
  northstar-sql:
  northstar-azurite:
```

Line 6 is left out of the excerpt because it contains the sample password.

**The `sqlserver` service**

1. `image: mcr.microsoft.com/mssql/server:2022-latest` (line 3) names the image: SQL Server 2022 for Linux, from Microsoft's container registry.
2. `ACCEPT_EULA: "Y"` (line 5) is an environment variable for the container. Setting it, to any value, confirms acceptance of the licence agreement ([Docker: run containers for SQL Server on Linux](https://learn.microsoft.com/en-us/sql/linux/install-upgrade/quickstart-install-docker?view=sql-server-ver17)).
3. Line 6 sets `MSSQL_SA_PASSWORD`, the password of the administrator account `sa`. It is written in the form `${NAME:-default}`: Compose puts in the value of the variable `NAME` if it is set and not empty, and the default otherwise ([Compose file reference: interpolation](https://docs.docker.com/reference/compose-file/interpolation/)). The variable can come from the shell or from a file named `.env` next to the Compose file ([Compose: set, use and manage variables with interpolation](https://docs.docker.com/compose/how-tos/environment-variables/variable-interpolation/)). The repository has a template for that file, [.env.example](.env.example), and `.gitignore` keeps a real `.env` out of version control ([.gitignore:17](.gitignore#L17)).
4. `- "1433:1433"` (line 8) publishes a port in the form `HOST:CONTAINER`. Port 1433, the standard port of SQL Server, is reachable on the developer's machine.
5. `- northstar-sql:/var/opt/mssql` (line 10) mounts a *named volume* at the folder where SQL Server keeps its data. A volume is storage that Docker manages outside the container, so the data outlives the container ([Docker: volumes](https://docs.docker.com/engine/storage/volumes/)). Both volumes are declared at the end of the file (lines 26 to 28).
6. Lines 11 to 15 are a *health check*: a command that Compose runs to find out whether the container is healthy. With `CMD-SHELL` the command is a string that runs in the default shell of the container. Here it calls `sqlcmd` to run `SELECT 1` and exits with 1 if that fails. `$$` is how a Compose file writes a literal dollar sign, so `$MSSQL_SA_PASSWORD` is resolved by the shell inside the container and not by Compose ([Compose file reference: services](https://docs.docker.com/reference/compose-file/services/), [Compose file reference: interpolation](https://docs.docker.com/reference/compose-file/interpolation/)).
7. `interval: 10s` (line 13), `timeout: 5s` (line 14) and `retries: 12` (line 15) time the check. Compose follows the rules of the Dockerfile reference here: the check runs again one interval after the previous run has finished, a run that takes longer than the timeout counts as failed, and after that many failures in a row the container is marked unhealthy. The documented defaults are 30 seconds, 30 seconds and 3 ([Dockerfile reference: HEALTHCHECK](https://docs.docker.com/reference/dockerfile/#healthcheck)).

**The `azurite` service**

1. `image: mcr.microsoft.com/azure-storage/azurite:latest` (line 18) is Azurite, an emulator of Azure Storage for local development ([Use the Azurite emulator for local Azure Storage development](https://learn.microsoft.com/en-us/azure/storage/common/storage-use-azurite)).
2. `command: azurite --blobHost 0.0.0.0 --queueHost 0.0.0.0` (line 19) replaces the start command of the image. By default Azurite listens on `127.0.0.1` only. The address `0.0.0.0` lets it accept remote requests ([Install and run the Azurite emulator: listening host](https://learn.microsoft.com/en-us/azure/storage/common/storage-install-azurite#listening-host)). Azurite's own README describes the same switches for its Docker image: they set the listening endpoints so that the services accept requests from the host machine ([Azure/Azurite on GitHub](https://github.com/Azure/Azurite)).
3. `- "10000:10000"` (line 21) and `- "10001:10001"` (line 22) publish the default ports of the Blob service and the Queue service ([Install and run the Azurite emulator: listening host](https://learn.microsoft.com/en-us/azure/storage/common/storage-install-azurite#listening-host)).
4. `- northstar-azurite:/data` (line 24) is the volume for the emulator's data.

**How the services are used.** The README starts only the database: `docker compose up -d sqlserver` ([README.md:89](README.md#L89)). `-d` runs the container in the background ([docker compose up](https://docs.docker.com/reference/cli/docker/compose/up/)). The API is then pointed at it with two environment variables, as described under [Application settings](#application-settings-appsettingsjson). The README says what the service is for: "The application includes the SQL Server provider and a Compose service for the SQL-specific exercises:" ([README.md:86](README.md#L86)). About the password it says: "The checked-in password is local-only sample configuration. Do not reuse it outside the lab." ([README.md:96](README.md#L96)).

No source file under `src`, `tests` or `tools` refers to Azurite or to Azure Storage. The README places that work in a later phase: "Durable queue/outbox dispatch, reports, Blob Storage, and legacy adapters remain later phases rather than empty scaffolding." ([README.md:121](README.md#L121)).

**Read more.** [Docker Compose documentation](https://docs.docker.com/compose/) · [Compose file reference: services](https://docs.docker.com/reference/compose-file/services/) · [Compose file reference: interpolation](https://docs.docker.com/reference/compose-file/interpolation/) · [Compose: set, use and manage variables with interpolation](https://docs.docker.com/compose/how-tos/environment-variables/variable-interpolation/) · [docker compose up](https://docs.docker.com/reference/cli/docker/compose/up/) · [Docker: volumes](https://docs.docker.com/engine/storage/volumes/) · [Dockerfile reference: HEALTHCHECK](https://docs.docker.com/reference/dockerfile/#healthcheck) · [Docker: run containers for SQL Server on Linux](https://learn.microsoft.com/en-us/sql/linux/install-upgrade/quickstart-install-docker?view=sql-server-ver17) · [Use the Azurite emulator for local Azure Storage development](https://learn.microsoft.com/en-us/azure/storage/common/storage-use-azurite) · [Install and run the Azurite emulator: listening host](https://learn.microsoft.com/en-us/azure/storage/common/storage-install-azurite#listening-host) · [SQL Server EF Core database provider](https://learn.microsoft.com/en-us/ef/core/providers/sql-server/)

### The Angular workspace: `angular.json`

`angular.json` is the *workspace configuration* of the Angular CLI. Its `projects` section has one part for each application or library in the workspace. This workspace has one project, named `northstar-web` ([Web/angular.json:10](src/Northstar.Web/angular.json#L10)).

**Workspace and project settings.** The first lines of the file set these values ([Angular workspace configuration (angular.json)](https://angular.dev/reference/configs/workspace-config)):

| Line | Setting | What it means |
|---:|---|---|
| 3 | `"version": 1` | The version of the configuration-file format. |
| 5 | `"packageManager": "npm"` | The package manager that the CLI uses. |
| 6 | `"analytics": false` | No anonymous usage data is shared with the Angular team. |
| 8 | `"newProjectRoot": "projects"` | The folder in which the CLI would create further projects. `projects` is also the documented default. |
| 11 | `"projectType": "application"` | An application can run on its own in a browser. The other kind, a library, cannot. |
| 14 | `"style": "scss"` | A default for `ng generate`: the `style` option of the component generator is set to `scss`. The style files of the two components here are `.scss` files. |
| 17 | `"root": ""` | The folder of the project, relative to the workspace. It is empty for the first application, which sits at the top level of the workspace. |
| 18 | `"sourceRoot": "src"` | The folder of the project's source files. |
| 19 | `"prefix": "app"` | The text that `ng generate` puts in front of the selector of a new component. The two components here have the selectors `app-root` and `app-import-panel` ([Web/app.ts:9](src/Northstar.Web/src/app/app.ts#L9), [Web/import-panel.component.ts:20](src/Northstar.Web/src/app/imports/import-panel.component.ts#L20)). |

**Targets.** Under `architect` ([Web/angular.json:20](src/Northstar.Web/angular.json#L20)) the project has three *targets*: `build`, `serve` and `test`. Architect is the tool that the CLI uses to run tasks such as compiling and testing. Each target names a *builder*, the code that performs the task, in the form `package:builder`. `options` holds the default settings of the target, and `configurations` holds named sets of settings that override them ([Angular workspace configuration (angular.json)](https://angular.dev/reference/configs/workspace-config)).

**[`src/Northstar.Web/angular.json`](src/Northstar.Web/angular.json#L21-L60)** · lines 21–60

<!-- x:angular-build -->
```json
"build": {
  "builder": "@angular/build:application",
  "options": {
    "browser": "src/main.ts",
    "tsConfig": "tsconfig.app.json",
    "inlineStyleLanguage": "scss",
    "assets": [
      {
        "glob": "**/*",
        "input": "public"
      }
    ],
    "styles": [
      "src/styles.scss"
    ]
  },
  "configurations": {
    "production": {
      "budgets": [
        {
          "type": "initial",
          "maximumWarning": "500kB",
          "maximumError": "1MB"
        },
        {
          "type": "anyComponentStyle",
          "maximumWarning": "8kB",
          "maximumError": "12kB"
        }
      ],
      "outputHashing": "all"
    },
    "development": {
      "optimization": false,
      "extractLicenses": false,
      "sourceMap": true
    }
  },
  "defaultConfiguration": "production"
},
```

1. `"builder": "@angular/build:application"` (line 22) is the application builder, the one that new Angular applications use by default ([Building Angular apps](https://angular.dev/tools/cli/build)).
2. `"browser": "src/main.ts"` (line 24) is the entry point for the browser: the file with which the application starts ([Pattern 22](#pattern-22-angular-standalone-bootstrap-and-an-http-interceptor)).
3. `"tsConfig": "tsconfig.app.json"` (line 25) names the TypeScript configuration file for the build. [TypeScript configuration](#typescript-configuration-the-tsconfig-files) describes it.
4. `"inlineStyleLanguage": "scss"` (line 26) is the stylesheet language for styles that are written inside a component file. The documented default is `css`.
5. `assets` (lines 27 to 32) lists files that are copied to the output as they are, without processing or hashing. Here that is every file (`**/*`) in the folder `public`.
6. `styles` (lines 33 to 35) lists the global stylesheets. There is one: `"src/styles.scss"` (line 34).

**The two configurations.** The Angular CLI comes with two build configurations, `production` and `development` ([Angular workspace configuration (angular.json)](https://angular.dev/reference/configs/workspace-config)).

The `production` configuration (lines 38 to 52) adds two settings. `"outputHashing": "all"` (line 51) turns on cache-busting hashes in the names of the output files, in the mode `all`. The documented default is `none`. `budgets` sets size limits. A *budget* is a limit that the builder checks: it warns or reports an error when a part of the application reaches or goes over the limit ([Configuring size budgets](https://angular.dev/tools/cli/build#configuring-size-budgets)).

| Budget type | What is measured | Warning here | Error here | Documented default |
|---|---|---|---|---|
| `initial` | The JavaScript and CSS that are needed to start the application. | `"500kB"` | `"1MB"` | Warning at 500kb, error at 1mb. |
| `anyComponentStyle` | Any one component stylesheet. | `"8kB"` | `"12kB"` | Warning at 2kb, error at 4kb. |

The `development` configuration (lines 53 to 57) sets three options, each to the opposite of its documented default:

| Option | Value here | Documented default | What it does |
|---|---|---|---|
| `optimization` | `false` | `true` | Optimizes the build output, for example by minifying scripts and styles and by removing code that is never used. |
| `extractLicenses` | `false` | `true` | Puts all licences into a separate file. |
| `sourceMap` | `true` | `false` | Writes source maps for scripts and styles. A *source map* lets a debugger show the original source for the built code. |

`"defaultConfiguration": "production"` (line 59) names `production` as the default configuration of the target. The workspace reference does not describe this key, so the line is given here as it stands. The reference does say that `ng build` uses the `production` configuration by default ([Angular workspace configuration (angular.json)](https://angular.dev/reference/configs/workspace-config)). The `watch` script asks for the other one: `ng build --watch --configuration development` ([Web/package.json:8](src/Northstar.Web/package.json#L8)).

**The serve and test targets**

**[`src/Northstar.Web/angular.json`](src/Northstar.Web/angular.json#L61-L79)** · lines 61–79

<!-- x:angular-serve-test -->
```json
"serve": {
  "builder": "@angular/build:dev-server",
  "configurations": {
    "production": {
      "buildTarget": "northstar-web:build:production"
    },
    "development": {
      "buildTarget": "northstar-web:build:development"
    }
  },
  "defaultConfiguration": "development"
},
"test": {
  "builder": "@angular/build:unit-test",
  "options": {
    "runnerConfig": "vitest.config.ts",
    "isolate": false
  }
}
```

1. `"builder": "@angular/build:dev-server"` (line 62) is the development server behind `ng serve`.
2. The target has no `options`. Each of its two configurations only names a build target in the form `project:target:configuration`. `"buildTarget": "northstar-web:build:development"` (line 68) means: serve what the `build` target produces with its `development` configuration.
3. `"defaultConfiguration": "development"` (line 71) names `development` as the default configuration of this target.
4. Host and port are not set, so the documented defaults apply: `localhost` and port 4200 ([Angular CLI: ng serve](https://angular.dev/cli/serve)). The README gives the resulting address, `http://localhost:4200` ([README.md:52](README.md#L52)). The end-to-end tests pass their own values on the command line: `start -- --host 127.0.0.1 --port 4200` ([e2e/playwright.config.ts:37](tests/e2e/playwright.config.ts#L37)).
5. The proxy file is not named in `angular.json`. The `start` script passes it on the command line ([Web/package.json:6](src/Northstar.Web/package.json#L6), [Pattern 22](#pattern-22-angular-standalone-bootstrap-and-an-http-interceptor)).
6. `"builder": "@angular/build:unit-test"` (line 74) is the builder behind `ng test`. [Unit test runner settings](#unit-test-runner-settings-angularjson-and-vitestconfigts) goes through its options.

**Read more.** [Angular workspace configuration (angular.json)](https://angular.dev/reference/configs/workspace-config) · [The Angular CLI](https://angular.dev/tools/cli) · [Building Angular apps](https://angular.dev/tools/cli/build) · [Configuring size budgets](https://angular.dev/tools/cli/build#configuring-size-budgets) · [Angular CLI: ng build](https://angular.dev/cli/build) · [Angular CLI: ng serve](https://angular.dev/cli/serve) · [Angular application build system](https://angular.dev/tools/cli/build-system-migration)

### npm packages and scripts: `package.json`

A `package.json` file describes an npm project: its name, the packages it needs, and a set of named *scripts*, which are command lines that npm can run ([package.json](https://docs.npmjs.com/cli/v11/configuring-npm/package-json), [npm scripts](https://docs.npmjs.com/cli/v11/using-npm/scripts)). The repository has two such files, one for the frontend and one for the end-to-end tests.

**The scripts of the frontend** ([Web/package.json:4-10](src/Northstar.Web/package.json#L4-L10)):

| Script | Command line | Started with | What it does |
|---|---|---|---|
| `ng` | `"ng": "ng"` | `npm run ng` | Runs the Angular CLI. |
| `start` | `ng serve --proxy-config proxy.conf.json` | `npm start` | Starts the development server with the proxy file ([Pattern 22](#pattern-22-angular-standalone-bootstrap-and-an-http-interceptor)). |
| `build` | `ng build` | `npm run build` | Builds the application. |
| `watch` | `ng build --watch --configuration development` | `npm run watch` | Builds with the `development` configuration, and builds again when files change. |
| `test` | `ng test --watch=false` | `npm test` | Runs the unit tests once. |

`npm run` adds the folder `node_modules/.bin` to the `PATH` that a script sees ([npm run](https://docs.npmjs.com/cli/v11/commands/npm-run)). That is how a script can call `ng`, the command of the package `@angular/cli`, which is installed in the project ([Web/package.json:25](src/Northstar.Web/package.json#L25)). `npm start` and `npm test` run the scripts named `start` and `test` ([npm start](https://docs.npmjs.com/cli/v11/commands/npm-start), [npm test](https://docs.npmjs.com/cli/v11/commands/npm-test)).

The `test` script passes `--watch=false`. Watch mode runs the tests again when a source file changes. It is on by default in an interactive terminal, so the script switches it off ([Angular CLI: ng test](https://angular.dev/cli/test)).

**Passing extra arguments.** Arguments after `--` are handed to the script and are not read by npm ([npm run](https://docs.npmjs.com/cli/v11/commands/npm-run)). The Playwright configuration uses this to start the same development server on a fixed address: `npm --prefix ../../src/Northstar.Web start -- --host 127.0.0.1 --port 4200` ([e2e/playwright.config.ts:37](tests/e2e/playwright.config.ts#L37)). `--prefix` makes npm work in the folder of the frontend ([npm: config](https://docs.npmjs.com/cli/v11/using-npm/config)).

**Other fields**

- `"private": true` makes npm refuse to publish the package ([package.json](https://docs.npmjs.com/cli/v11/configuring-npm/package-json)).
- `"packageManager": "npm@11.16.0"` names the package manager and its exact version. Corepack reads this field. Corepack is a tool that sits between a Node.js project and the package manager that the project is meant to be used with ([Corepack](https://github.com/nodejs/corepack)). The README names the same major version: "Node.js 22 and npm 11" ([README.md:23](README.md#L23)).
- `dependencies` (lines 13 to 22) maps package names to version ranges. `devDependencies` (lines 23 to 31) does the same for packages that are needed only for development, such as build and test tools ([package.json](https://docs.npmjs.com/cli/v11/configuring-npm/package-json)).

**Version ranges.** The sign in front of a version says which newer versions npm may install ([package.json](https://docs.npmjs.com/cli/v11/configuring-npm/package-json), [npm: about semantic versioning](https://docs.npmjs.com/about-semantic-versioning)):

| Written as | Meaning | Example |
|---|---|---|
| `^22.1.0` | Compatible with that version. Newer minor and patch releases are accepted. | `"@angular/core": "^22.1.0"` |
| `~7.8.0` | About that version. Only newer patch releases are accepted. | `"rxjs": "~7.8.0"` |
| `1.62.1` | Exactly that version. | `"@playwright/test": "1.62.1"` |

**The lock file.** Next to each `package.json` lies a `package-lock.json`. npm writes it. It describes the exact tree of packages that was installed, so that a later install can produce the same tree ([package-lock.json](https://docs.npmjs.com/cli/v11/configuring-npm/package-lock-json)). `npm ci` installs exactly that tree ([npm ci](https://docs.npmjs.com/cli/v11/commands/npm-ci)). The CI workflow uses `npm ci`, and so do the test instructions of the README ([README.md:72](README.md#L72), [README.md:77](README.md#L77)). The instructions for a local run use `npm install` ([README.md:48](README.md#L48)).

**The end-to-end project.** [e2e/package.json](tests/e2e/package.json) has two scripts. `test` runs `playwright test`. `test:headed` runs `playwright test --headed`, which runs the tests in *headed* browsers, that is, with a visible browser window ([Playwright: command line](https://playwright.dev/docs/test-cli)). Its two packages have exact versions: `@playwright/test` 1.62.1 and `"typescript": "6.0.2"`.

Section 2 lists all npm packages with their versions under [npm packages](#npm-packages).

**Read more.** [package.json](https://docs.npmjs.com/cli/v11/configuring-npm/package-json) · [npm scripts](https://docs.npmjs.com/cli/v11/using-npm/scripts) · [npm run](https://docs.npmjs.com/cli/v11/commands/npm-run) · [npm start](https://docs.npmjs.com/cli/v11/commands/npm-start) · [npm test](https://docs.npmjs.com/cli/v11/commands/npm-test) · [npm: about semantic versioning](https://docs.npmjs.com/about-semantic-versioning) · [package-lock.json](https://docs.npmjs.com/cli/v11/configuring-npm/package-lock-json) · [npm ci](https://docs.npmjs.com/cli/v11/commands/npm-ci) · [Corepack](https://github.com/nodejs/corepack)

### TypeScript configuration: the `tsconfig` files

A `tsconfig.json` file marks a folder as the root of a TypeScript project. It names the files of the project and the compiler options ([What is a tsconfig.json](https://www.typescriptlang.org/docs/handbook/tsconfig-json.html)). The frontend has three such files. [Web/tsconfig.json](src/Northstar.Web/tsconfig.json) holds the options. [Web/tsconfig.app.json](src/Northstar.Web/tsconfig.app.json) and [Web/tsconfig.spec.json](src/Northstar.Web/tsconfig.spec.json) say which files are compiled for the application and for the tests.

**[`src/Northstar.Web/tsconfig.json`](src/Northstar.Web/tsconfig.json#L3-L31)** · lines 3–31

<!-- x:tsconfig -->
```json
{
  "compileOnSave": false,
  "compilerOptions": {
    "noImplicitOverride": true,
    "noPropertyAccessFromIndexSignature": true,
    "noImplicitReturns": true,
    "noFallthroughCasesInSwitch": true,
    "skipLibCheck": true,
    "isolatedModules": true,
    "experimentalDecorators": true,
    "importHelpers": true,
    "target": "ES2022",
    "module": "preserve"
  },
  "angularCompilerOptions": {
    "enableI18nLegacyMessageIdFormat": false,
    "strictInjectionParameters": true,
    "strictInputAccessModifiers": true
  },
  "files": [],
  "references": [
    {
      "path": "./tsconfig.app.json"
    },
    {
      "path": "./tsconfig.spec.json"
    }
  ]
}
```

**Compiler options** (lines 5 to 16). Each name links to its entry in the [TSConfig reference](https://www.typescriptlang.org/tsconfig/).

| Line | Option | Value | What it does |
|---:|---|---|---|
| 6 | [`noImplicitOverride`](https://www.typescriptlang.org/tsconfig/noImplicitOverride.html) | `true` | A member that overrides a member of the base class must carry the `override` modifier. |
| 7 | [`noPropertyAccessFromIndexSignature`](https://www.typescriptlang.org/tsconfig/noPropertyAccessFromIndexSignature.html) | `true` | A key that is declared only through an index signature must be read with brackets, as in `obj['key']`, and not with a dot. |
| 8 | [`noImplicitReturns`](https://www.typescriptlang.org/tsconfig/noImplicitReturns.html) | `true` | An error is reported when some path through a function does not return a value. |
| 9 | [`noFallthroughCasesInSwitch`](https://www.typescriptlang.org/tsconfig/noFallthroughCasesInSwitch.html) | `true` | An error is reported when a non-empty `case` of a `switch` falls through to the next one. |
| 10 | [`skipLibCheck`](https://www.typescriptlang.org/tsconfig/skipLibCheck.html) | `true` | Declaration files (`.d.ts`) are not type-checked. |
| 11 | [`isolatedModules`](https://www.typescriptlang.org/tsconfig/isolatedModules.html) | `true` | Every file must be written so that it can be compiled on its own, without relying on other files. |
| 12 | [`experimentalDecorators`](https://www.typescriptlang.org/tsconfig/experimentalDecorators.html) | `true` | Turns on the experimental form of decorators, the TC39 stage 2 draft. |
| 13 | [`importHelpers`](https://www.typescriptlang.org/tsconfig/importHelpers.html) | `true` | Helper functions are imported from the package `tslib` once, instead of being repeated in every file. `tslib` is a dependency ([Web/package.json:21](src/Northstar.Web/package.json#L21)). |
| 14 | [`target`](https://www.typescriptlang.org/tsconfig/target.html) | `"ES2022"` | The JavaScript version of the output, with the matching library declarations. |
| 15 | [`module`](https://www.typescriptlang.org/tsconfig/module.html) | `"preserve"` | The module format of the output. With `preserve`, the `import` and `export` statements stay as they are written. |

**What the file does not say**

- `"compileOnSave": false` (line 4) is given here as it stands. The TSConfig reference has no entry for it.
- There is no `strict` line. From TypeScript 6.0 on, `strict` is `true` by default ([TypeScript 6.0 release notes](https://www.typescriptlang.org/docs/handbook/release-notes/typescript-6-0.html)), and the project uses TypeScript `~6.0.2` ([Web/package.json:29](src/Northstar.Web/package.json#L29)). `strict` turns on all strict type-checking options ([TSConfig: strict](https://www.typescriptlang.org/tsconfig/strict.html)).

**Three files that work together.** `"files": []` (line 22) is an empty list of files, so this file compiles nothing by itself. `references` (lines 23 to 30) points to two other projects ([TSConfig: files](https://www.typescriptlang.org/tsconfig/files.html), [TSConfig: references](https://www.typescriptlang.org/tsconfig/references.html), [TypeScript: project references](https://www.typescriptlang.org/docs/handbook/project-references.html)). Each of those begins with `"extends": "./tsconfig.json"`, which makes it inherit the settings of the base file ([TSConfig: extends](https://www.typescriptlang.org/tsconfig/extends.html)).

| File | `types` | `include` | `exclude` | Used by |
|---|---|---|---|---|
| [Web/tsconfig.app.json](src/Northstar.Web/tsconfig.app.json) | `"types": []` | `"src/**/*.ts"` | `"src/**/*.spec.ts"` | The `build` target ([Web/angular.json:25](src/Northstar.Web/angular.json#L25)). |
| [Web/tsconfig.spec.json](src/Northstar.Web/tsconfig.spec.json) | `"vitest/globals"` | `"src/**/*.d.ts"`, `"src/**/*.spec.ts"` | none | The `test` target, which uses `tsconfig.spec.json` by default when the file exists. |

`include` lists patterns for the files of the project, and `exclude` filters files out of that list ([TSConfig: include](https://www.typescriptlang.org/tsconfig/include.html), [TSConfig: exclude](https://www.typescriptlang.org/tsconfig/exclude.html)). So the application project holds every TypeScript file under `src` except the spec files. `types` names packages whose type declarations are available everywhere without an `import`. When it is given, only the listed packages are in the global scope ([TSConfig: types](https://www.typescriptlang.org/tsconfig/types.html)). The application project lists none. The test project lists `vitest/globals`, which is how the spec files know `describe`, `it` and `expect` ([Pattern 26](#pattern-26-frontend-tests-with-vitest-and-playwright)).

**Angular compiler options** (lines 17 to 21). The part named `angularCompilerOptions` is read by the Angular compiler, which compiles the templates ([Angular compiler options](https://angular.dev/reference/configs/angular-compiler-options)).

| Line | Option | Value here | Documented default | What it does |
|---:|---|---|---|---|
| 18 | `enableI18nLegacyMessageIdFormat` | `false` | `true` | Whether the compiler writes ids in the legacy format for messages marked with `i18n`. The documentation says to set it to `false` unless a project relies on translations made with the legacy ids. |
| 19 | `strictInjectionParameters` | `true` | `false` | Reports an error for a supplied parameter whose injection type cannot be determined. |
| 20 | `strictInputAccessModifiers` | `true` | `false` | Whether `private`, `protected` and `readonly` are honored when a template binds a value to an input. |

The file has no `strictTemplates` line. The documented default of that option is `true` ([Angular compiler options](https://angular.dev/reference/configs/angular-compiler-options)), and `true` is the value that selects the strict mode of template type checking ([Template type checking](https://angular.dev/tools/cli/template-typecheck)).

**Read more.** [What is a tsconfig.json](https://www.typescriptlang.org/docs/handbook/tsconfig-json.html) · [TSConfig reference](https://www.typescriptlang.org/tsconfig/) · [TypeScript: project references](https://www.typescriptlang.org/docs/handbook/project-references.html) · [TypeScript 6.0 release notes](https://www.typescriptlang.org/docs/handbook/release-notes/typescript-6-0.html) · [Angular compiler options](https://angular.dev/reference/configs/angular-compiler-options) · [Template type checking](https://angular.dev/tools/cli/template-typecheck)

### Formatting and the development proxy: `.prettierrc` and `proxy.conf.json`

**Prettier** is a code formatter, and [Web/.prettierrc](src/Northstar.Web/.prettierrc) is its configuration file ([Prettier configuration file](https://prettier.io/docs/configuration)). The package is a development dependency ([Web/package.json:28](src/Northstar.Web/package.json#L28)). The file sets three things ([Prettier options](https://prettier.io/docs/options)):

| Line | Setting | Documented default | What it does |
|---:|---|---|---|
| 2 | `"printWidth": 100` | 80 | The line length at which Prettier wraps code. |
| 3 | `"singleQuote": true` | `false` | Strings are written with single quotes instead of double quotes. |
| 4–11 | `overrides` | none | Other settings for some files. Here files that match `"*.html"` get `"parser": "angular"`, the parser for Angular templates. |

As [Code style](#code-style-editorconfig-and-dotnet-format) says, Prettier also reads `.editorconfig`, and the settings of `.prettierrc` come first.

**The proxy file**

**[`src/Northstar.Web/proxy.conf.json`](src/Northstar.Web/proxy.conf.json#L1-L12)** · lines 1–12

<!-- x:proxy -->
```json
{
  "/api": {
    "target": "http://localhost:5120",
    "secure": false,
    "changeOrigin": true
  },
  "/health": {
    "target": "http://localhost:5120",
    "secure": false,
    "changeOrigin": true
  }
}
```

The file has two entries. The key of an entry is the start of a URL path, here `/api` and `/health`. `"target": "http://localhost:5120"` (line 3) is the server that matching requests are passed on to: the API, at the address from its launch profile. [Pattern 22](#pattern-22-angular-standalone-bootstrap-and-an-http-interceptor) explains why the frontend needs this. The options `secure` and `changeOrigin` are given here as they stand.

**Read more.** [Prettier configuration file](https://prettier.io/docs/configuration) · [Prettier options](https://prettier.io/docs/options) · [Proxying to a backend server](https://angular.dev/tools/cli/serve#proxying-to-a-backend-server)

### Unit test runner settings: `angular.json` and `vitest.config.ts`

`ng test` runs the `test` target of `angular.json` (lines 73 to 79, shown under [The Angular workspace](#the-angular-workspace-angularjson)). Its builder runs the unit tests with Vitest, the default runner ([Angular CLI: ng test](https://angular.dev/cli/test)). The table lists the options of the builder that matter here. A dash in the second column means that the option is not set, so the documented default applies.

| Option | Value here | Documented default | What it does |
|---|---|---|---|
| `runner` | – | `vitest` | The test runner. |
| `runnerConfig` | `"vitest.config.ts"` | `false` | A text value is the path of a configuration file for the runner. |
| `isolate` | `false` | `false` | Whether the tests run isolated from each other, in separate threads or processes. |
| `watch` | `false`, from the `test` script | `true` in an interactive terminal, otherwise `false` | Runs the tests again when a source file changes. |
| `tsConfig` | – | `tsconfig.spec.json`, when the file exists | The TypeScript configuration for the tests. |
| `buildTarget` | – | The `build` target with the `development` configuration. | The build that the tests are based on. |
| `browsers` | – | None: the tests run in Node.js with jsdom. | Browsers to run the tests in. |
| `include` | – | `**/*.spec.ts`, `**/*.test.ts` | The files that hold tests. |

jsdom is an implementation of web standards in JavaScript for use with Node.js ([jsdom](https://github.com/jsdom/jsdom)). It gives the tests a `document` to render into without a browser. The package is a development dependency ([Web/package.json:27](src/Northstar.Web/package.json#L27)). The two spec files of the project, [Web/app.spec.ts](src/Northstar.Web/src/app/app.spec.ts) and [Web/import-panel.component.spec.ts](src/Northstar.Web/src/app/imports/import-panel.component.spec.ts), hold 3 tests ([Pattern 26](#pattern-26-frontend-tests-with-vitest-and-playwright)).

**[`src/Northstar.Web/vitest.config.ts`](src/Northstar.Web/vitest.config.ts#L1-L13)** · lines 1–13

<!-- x:vitest-config -->
```ts
import { defineConfig } from 'vitest/config';

export default defineConfig({
  test: {
    // VM workers are reliable in WSL/CI environments where spawning Vitest's
    // worker-thread bootstrap can stall before test collection.
    pool: 'vmThreads',
    maxWorkers: 1,
    fileParallelism: false,
    testTimeout: 15_000,
    hookTimeout: 15_000,
  },
});
```

The file exports the result of [`defineConfig`](https://vitest.dev/config/). The settings are inside `test`, and each name links to its page in the Vitest documentation:

| Line | Setting | Value | Documented default | What it does |
|---:|---|---|---|---|
| 7 | [`pool`](https://vitest.dev/config/pool) | `'vmThreads'` | `'forks'` | The kind of worker that runs the test files. `vmThreads` runs them in worker threads, each inside a VM context. |
| 8 | [`maxWorkers`](https://vitest.dev/config/maxworkers) | `1` | All available parallelism when watch mode is off, half of it when watch mode is on. | The largest number of test workers that run at the same time. |
| 9 | [`fileParallelism`](https://vitest.dev/config/fileparallelism) | `false` | `true` | Whether test files run in parallel. `false` also sets `maxWorkers` to 1. |
| 10 | [`testTimeout`](https://vitest.dev/config/testtimeout) | `15_000` | 5,000 ms | How long one test may take. |
| 11 | [`hookTimeout`](https://vitest.dev/config/hooktimeout) | `15_000` | 10,000 ms | How long one hook, such as `beforeEach`, may take. |

The underscore in `15_000` is a *numeric separator*. It only makes the number easier to read: the value is 15000 milliseconds, which is 15 seconds ([Numeric separators (MDN)](https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Lexical_grammar#numeric_separators)).

The file gives its own reason for the choice of pool in a comment: "VM workers are reliable in WSL/CI environments where spawning Vitest's worker-thread bootstrap can stall before test collection." ([Web/vitest.config.ts:5-6](src/Northstar.Web/vitest.config.ts#L5-L6))

**Read more.** [Angular CLI: ng test](https://angular.dev/cli/test) · [Unit testing](https://angular.dev/guide/testing) · [Vitest: configuration](https://vitest.dev/config/) · [Vitest: getting started](https://vitest.dev/guide/) · [jsdom](https://github.com/jsdom/jsdom)

### End-to-end test runner settings: `playwright.config.ts`

[e2e/playwright.config.ts](tests/e2e/playwright.config.ts) configures Playwright Test ([Playwright test configuration](https://playwright.dev/docs/test-configuration)). The first part of the file holds the general settings.

**[`tests/e2e/playwright.config.ts`](tests/e2e/playwright.config.ts#L3-L20)** · lines 3–20

<!-- x:pw-settings -->
```ts
export default defineConfig({
  testDir: './specs',
  fullyParallel: false,
  workers: 1,
  forbidOnly: Boolean(process.env['CI']),
  retries: 0,
  reporter: [['list'], ['html', { open: 'never' }]],
  use: {
    baseURL: 'http://127.0.0.1:4200',
    trace: 'retain-on-failure',
    screenshot: 'only-on-failure'
  },
  projects: [
    {
      name: 'chromium',
      use: { ...devices['Desktop Chrome'] }
    }
  ],
```

Each name in the table links to its entry in the Playwright reference. A dash means that the entry states no default.

| Line | Setting | Documented default | What it does |
|---:|---|---|---|
| 4 | [`testDir`](https://playwright.dev/docs/api/class-testconfig#test-config-test-dir) | The folder of the configuration file. | The folder that is searched for test files. |
| 5 | [`fullyParallel`](https://playwright.dev/docs/api/class-testconfig#test-config-fully-parallel) | – | Whether all tests in all files may run at the same time. Without it, the tests of one file run in order in the same worker process. |
| 6 | [`workers`](https://playwright.dev/docs/api/class-testconfig#test-config-workers) | Half the number of logical CPU cores. | The largest number of worker processes that run at the same time. Here it is one. |
| 7 | [`forbidOnly`](https://playwright.dev/docs/api/class-testconfig#test-config-forbid-only) | – | Whether the run ends with an error when a test is marked `.only`. Here that is the case when the variable `CI` is set. |
| 8 | [`retries`](https://playwright.dev/docs/api/class-testconfig#test-config-retries) | Failed tests are not retried. | How many times a failed test is run again. |
| 11 | [`baseURL`](https://playwright.dev/docs/api/class-testoptions#test-options-base-url) | Not set. | The address that relative URLs are resolved against. Both spec files open the page with `page.goto('/')`. |
| 12 | [`trace`](https://playwright.dev/docs/api/class-testoptions#test-options-trace) | `'off'` | A *trace* is a recording of a test run. This value records one for every run and keeps it only when the run failed. |
| 13 | [`screenshot`](https://playwright.dev/docs/api/class-testoptions#test-options-screenshot) | `'off'` | Takes a screenshot after each test that failed. |

**Reporters.** `reporter: [['list'], ['html', { open: 'never' }]],` (line 9) sets two reporters ([Playwright TestConfig: reporter](https://playwright.dev/docs/api/class-testconfig#test-config-reporter)). `list` prints a line for each test. `html` writes a report as a web page into the folder `playwright-report`. Its option `open: 'never'` keeps Playwright from opening the report in a browser. The documented default is `on-failure`, which opens it when tests have failed ([Playwright: reporters](https://playwright.dev/docs/test-reporters)). When no reporter is set, Playwright uses `list`, or `dot` on a CI machine. The CI workflow uploads the report folder when the job fails ([.github/workflows/ci.yml:59-64](.github/workflows/ci.yml#L59-L64)).

**Projects.** `projects` (lines 15 to 20) lists the *projects* of the test run. Playwright can run several projects at the same time, each with its own options ([Playwright TestConfig: projects](https://playwright.dev/docs/api/class-testconfig#test-config-projects)). Here there is one, named `chromium`. `...devices['Desktop Chrome']` (line 18) copies its options from [`devices`](https://playwright.dev/docs/emulation#devices), Playwright's registry of device parameters such as the user agent and the screen size.

**The three servers.** The rest of the file (lines 21 to 42) is the `webServer` list. It starts three programs before the tests and waits until each one answers at its `url`. [Pattern 26](#pattern-26-frontend-tests-with-vitest-and-playwright) reads one entry line by line.

| Lines | Starts | Waits for |
|---|---|---|
| 22–28 | The fake provider, with `dotnet run`. | `http://127.0.0.1:5130/health` |
| 29–35 | The API, with `dotnet run`. | `http://127.0.0.1:5120/health` |
| 36–41 | The frontend, with `npm start`. | `http://127.0.0.1:4200` |

Each entry sets `timeout: 120_000`, which is two minutes. The documented default is 60,000 ms ([Playwright: web server](https://playwright.dev/docs/test-webserver)). Both `dotnet run` commands pass `--no-launch-profile` and `--urls`, which [Launch profiles](#launch-profiles-launchsettingsjson) describes.

**Output folders.** Files that are created during a test run, such as traces and screenshots, go to the folder `test-results` next to `package.json` by default ([Playwright TestConfig: output-dir](https://playwright.dev/docs/api/class-testconfig#test-config-output-dir)). That folder and `playwright-report` are both listed in `.gitignore` ([.gitignore:7-8](.gitignore#L7-L8)).

**Read more.** [Playwright test configuration](https://playwright.dev/docs/test-configuration) · [Playwright test use options](https://playwright.dev/docs/test-use-options) · [Playwright TestConfig: use](https://playwright.dev/docs/api/class-testconfig#test-config-use) · [Playwright: reporters](https://playwright.dev/docs/test-reporters) · [Playwright TestConfig: web-server](https://playwright.dev/docs/api/class-testconfig#test-config-web-server) · [Playwright: web server](https://playwright.dev/docs/test-webserver) · [Playwright: command line](https://playwright.dev/docs/test-cli) · [Playwright: installation and first test](https://playwright.dev/docs/intro)

### Files that git leaves out: `.gitignore`

A `.gitignore` file lists files that git should leave untracked. Files that git already tracks are not affected ([gitignore: patterns for files that git ignores](https://git-scm.com/docs/gitignore)). Each line is one pattern. Three forms appear in [.gitignore](.gitignore):

- `**/bin/` has a leading `**/`, which matches in all folders. The slash at the end makes the pattern match folders only.
- `*.db` uses `*`, which matches anything except a slash. A pattern with no slash at its start or in its middle matches at every level below the file.
- `.env` is a plain name.

| Lines | Patterns | What they keep out of git |
|---|---|---|
| 1–2 | `**/bin/`, `**/obj/` | The output of a .NET build. `bin` is the default folder for the built files ([dotnet build](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-build)), and `obj` is the default folder for intermediate files ([Common MSBuild project properties](https://learn.microsoft.com/en-us/visualstudio/msbuild/common-msbuild-project-properties)). |
| 3–4 | `**/TestResults/`, `**/coverage/` | Test result files and coverage reports. |
| 5–6 | `**/node_modules/`, `**/dist/` | The installed npm packages, and the output of `ng build`, which goes to a folder under `dist` by default ([Building Angular apps](https://angular.dev/tools/cli/build)). |
| 7–8 | `**/playwright-report/`, `**/test-results/` | The HTML report and the output folder of Playwright. |
| 9 | `.angular/` | The disk cache of the Angular CLI. Its documented default folder is `.angular/cache` ([Angular workspace configuration (angular.json)](https://angular.dev/reference/configs/workspace-config)). |
| 10–13 | `.vs/`, `.vscode/`, `*.user`, `*.suo` | Folders and files that Visual Studio and Visual Studio Code write on a developer's machine. |
| 14–16 | `*.db`, `*.db-shm`, `*.db-wal` | SQLite database files. The API creates `northstar.db` when it first starts ([README.md:42](README.md#L42), [README.md:54](README.md#L54)). The other two are the extra files that SQLite keeps next to a database in write-ahead-log mode ([SQLite: temporary files](https://www.sqlite.org/tempfiles.html)). |
| 17 | `.env` | The local file with values for Docker Compose. |

The remaining lines of the file list files of local tools.

**Read more.** [gitignore: patterns for files that git ignores](https://git-scm.com/docs/gitignore)

## Appendix A: Types declared in this codebase

The compiler pass found 90 types that the C# code declares itself: 55 classes, 22 records, 8 enums and 5 interfaces. [Section 5](#5-net-api-inventory) lists the types that the code takes from outside. This appendix lists its own, project by project, in the order of file and line.

- **Type** is the name of the type. A nested type carries the name of the type around it, as in `ImportBatchProcessor.RowValidation`. 7 of the types are nested.
- **Declared as** gives the modifiers and the keyword of the declaration, as the source writes them. `primary constructor` is added when a class takes its constructor parameters in its header ([Pattern 3](#pattern-3-constructor-injection-with-primary-constructors)). All 22 records are written with a parameter list too, so the note is left out for them.
- **Where** is the file and the line of the declaration.
- **Base type and interfaces** names the class that the type derives from and the interfaces that its declaration lists. A dash means that it names neither.
- **Members** is the number of members that the source writes for the type: constructors, properties, methods, fields, enum values and nested types. A primary constructor counts as one member, and each parameter of a record counts as the property that it creates. Members that the compiler adds by itself, such as the `Equals` method of a record, are not counted.

### Northstar.Domain

*25 types.*

| Type | Declared as | Where | Base type and interfaces | Members |
| --- | --- | --- | --- | ---: |
| `Employee` | public sealed class | [Domain/Employee.cs:3](src/Northstar.Domain/Employees/Employee.cs#L3) | – | 10 |
| `ImportBatchStatus` | public enum | [Domain/ImportBatch.cs:3](src/Northstar.Domain/Imports/ImportBatch.cs#L3) | – | 5 |
| `ImportBatch` | public sealed class | [Domain/ImportBatch.cs:12](src/Northstar.Domain/Imports/ImportBatch.cs#L12) | – | 24 |
| `ImportRowStatus` | public enum | [Domain/ImportRow.cs:3](src/Northstar.Domain/Imports/ImportRow.cs#L3) | – | 3 |
| `ImportRow` | public sealed class | [Domain/ImportRow.cs:10](src/Northstar.Domain/Imports/ImportRow.cs#L10) | – | 16 |
| `Organization` | public sealed class | [Domain/Organization.cs:3](src/Northstar.Domain/Organizations/Organization.cs#L3) | – | 6 |
| `FindingSeverity` | public enum | [Domain/ComplianceFinding.cs:3](src/Northstar.Domain/PayrollRuns/ComplianceFinding.cs#L3) | – | 3 |
| `ComplianceFinding` | public sealed class | [Domain/ComplianceFinding.cs:10](src/Northstar.Domain/PayrollRuns/ComplianceFinding.cs#L10) | – | 7 |
| `PayrollWorkSegment` | public sealed record | [Domain/PayrollCalculator.cs:5](src/Northstar.Domain/PayrollRuns/PayrollCalculator.cs#L5) | – | 15 |
| `PayrollCalculation` | public sealed record | [Domain/PayrollCalculator.cs:21](src/Northstar.Domain/PayrollRuns/PayrollCalculator.cs#L21) | – | 3 |
| `PayrollCalculator` | public sealed class, primary constructor | [Domain/PayrollCalculator.cs:25](src/Northstar.Domain/PayrollRuns/PayrollCalculator.cs#L25) | – | 5 |
| `PayrollLine` | public sealed class | [Domain/PayrollLine.cs:3](src/Northstar.Domain/PayrollRuns/PayrollLine.cs#L3) | – | 20 |
| `PayrollRunActionStatus` | public enum | [Domain/PayrollRun.cs:3](src/Northstar.Domain/PayrollRuns/PayrollRun.cs#L3) | – | 3 |
| `PayrollRunActionResult` | public sealed record | [Domain/PayrollRun.cs:10](src/Northstar.Domain/PayrollRuns/PayrollRun.cs#L10) | – | 6 |
| `PayrollRun` | public sealed class | [Domain/PayrollRun.cs:17](src/Northstar.Domain/PayrollRuns/PayrollRun.cs#L17) | – | 14 |
| `PayrollRunStatus` | public enum | [Domain/PayrollRunStatus.cs:3](src/Northstar.Domain/PayrollRuns/PayrollRunStatus.cs#L3) | – | 10 |
| `Project` | public sealed class | [Domain/Project.cs:3](src/Northstar.Domain/Projects/Project.cs#L3) | – | 13 |
| `WorkClassification` | public sealed class | [Domain/WorkClassification.cs:3](src/Northstar.Domain/Projects/WorkClassification.cs#L3) | – | 4 |
| `TimeEntry` | public sealed class | [Domain/TimeEntry.cs:3](src/Northstar.Domain/TimeEntries/TimeEntry.cs#L3) | – | 11 |
| `RateSelectionStatus` | public enum | [Domain/RateSelectionResult.cs:3](src/Northstar.Domain/WageRates/RateSelectionResult.cs#L3) | – | 3 |
| `RateSelectionResult` | public sealed record | [Domain/RateSelectionResult.cs:10](src/Northstar.Domain/WageRates/RateSelectionResult.cs#L10) | – | 7 |
| `WageRateScope` | public enum | [Domain/WageRate.cs:3](src/Northstar.Domain/WageRates/WageRate.cs#L3) | – | 5 |
| `WageRate` | public sealed class | [Domain/WageRate.cs:12](src/Northstar.Domain/WageRates/WageRate.cs#L12) | – | 16 |
| `WageRateQuery` | public sealed record | [Domain/WageRateSelector.cs:3](src/Northstar.Domain/WageRates/WageRateSelector.cs#L3) | – | 7 |
| `WageRateSelector` | public sealed class | [Domain/WageRateSelector.cs:11](src/Northstar.Domain/WageRates/WageRateSelector.cs#L11) | – | 3 |

### Northstar.Application

*29 types.*

| Type | Declared as | Where | Base type and interfaces | Members |
| --- | --- | --- | --- | ---: |
| `IApplicationTransaction` | public interface | [Application/IApplicationDbContext.cs:12](src/Northstar.Application/Abstractions/IApplicationDbContext.cs#L12) | `IAsyncDisposable` | 2 |
| `IApplicationDbContext` | public interface | [Application/IApplicationDbContext.cs:18](src/Northstar.Application/Abstractions/IApplicationDbContext.cs#L18) | – | 16 |
| `IClock` | public interface | [Application/IClock.cs:3](src/Northstar.Application/Abstractions/IClock.cs#L3) | – | 1 |
| `SystemClock` | public sealed class | [Application/IClock.cs:8](src/Northstar.Application/Abstractions/IClock.cs#L8) | `IClock` | 1 |
| `IImportBatchQueue` | public interface | [Application/IImportBatchQueue.cs:3](src/Northstar.Application/Abstractions/IImportBatchQueue.cs#L3) | – | 1 |
| `ProviderImportRequest` | public sealed record | [Application/IPayrollProviderClient.cs:3](src/Northstar.Application/Abstractions/IPayrollProviderClient.cs#L3) | – | 5 |
| `ProviderTimeEntryRecord` | public sealed record | [Application/IPayrollProviderClient.cs:9](src/Northstar.Application/Abstractions/IPayrollProviderClient.cs#L9) | – | 9 |
| `IPayrollProviderClient` | public interface | [Application/IPayrollProviderClient.cs:19](src/Northstar.Application/Abstractions/IPayrollProviderClient.cs#L19) | – | 1 |
| `PayrollProviderException` | public abstract class, primary constructor | [Application/IPayrollProviderClient.cs:28](src/Northstar.Application/Abstractions/IPayrollProviderClient.cs#L28) | `Exception` | 1 |
| `PayrollProviderTransientException` | public sealed class, primary constructor | [Application/IPayrollProviderClient.cs:34](src/Northstar.Application/Abstractions/IPayrollProviderClient.cs#L34) | `PayrollProviderException` | 1 |
| `PayrollProviderContractException` | public sealed class, primary constructor | [Application/IPayrollProviderClient.cs:43](src/Northstar.Application/Abstractions/IPayrollProviderClient.cs#L43) | `PayrollProviderException` | 1 |
| `OperationOutcome` | public enum | [Application/OperationResult.cs:3](src/Northstar.Application/Common/OperationResult.cs#L3) | – | 5 |
| `OperationResult<T>` | public sealed record | [Application/OperationResult.cs:12](src/Northstar.Application/Common/OperationResult.cs#L12) | – | 10 |
| `DependencyInjection` | public static class | [Application/DependencyInjection.cs:11](src/Northstar.Application/DependencyInjection.cs#L11) | – | 1 |
| `IdempotencyRecord` | public sealed class | [Application/IdempotencyRecord.cs:3](src/Northstar.Application/Idempotency/IdempotencyRecord.cs#L3) | – | 7 |
| `ImportBatchMappings` | internal static class | [Application/ImportBatchMappings.cs:5](src/Northstar.Application/Imports/ImportBatchMappings.cs#L5) | – | 2 |
| `ImportBatchProcessor` | public sealed class, primary constructor | [Application/ImportBatchProcessor.cs:9](src/Northstar.Application/Imports/ImportBatchProcessor.cs#L9) | – | 10 |
| `ImportBatchProcessor.RowValidation` | private sealed record | [Application/ImportBatchProcessor.cs:336](src/Northstar.Application/Imports/ImportBatchProcessor.cs#L336) | – | 7 |
| `ImportBatchService` | public sealed class, primary constructor | [Application/ImportBatchService.cs:11](src/Northstar.Application/Imports/ImportBatchService.cs#L11) | – | 12 |
| `CreateImportBatchRequest` | public sealed record | [Application/ImportBatchView.cs:3](src/Northstar.Application/Imports/ImportBatchView.cs#L3) | – | 4 |
| `ImportBatchView` | public sealed record | [Application/ImportBatchView.cs:8](src/Northstar.Application/Imports/ImportBatchView.cs#L8) | – | 18 |
| `ImportRowView` | public sealed record | [Application/ImportBatchView.cs:27](src/Northstar.Application/Imports/ImportBatchView.cs#L27) | – | 8 |
| `PayrollRunService` | public sealed class, primary constructor | [Application/PayrollRunService.cs:12](src/Northstar.Application/PayrollRuns/PayrollRunService.cs#L12) | – | 8 |
| `PayrollLineView` | public sealed record | [Application/PayrollRunView.cs:3](src/Northstar.Application/PayrollRuns/PayrollRunView.cs#L3) | – | 15 |
| `ComplianceFindingView` | public sealed record | [Application/PayrollRunView.cs:19](src/Northstar.Application/PayrollRuns/PayrollRunView.cs#L19) | – | 6 |
| `PayrollRunView` | public sealed record | [Application/PayrollRunView.cs:26](src/Northstar.Application/PayrollRuns/PayrollRunView.cs#L26) | – | 11 |
| `WageRateService` | public sealed class, primary constructor | [Application/WageRateService.cs:8](src/Northstar.Application/WageRates/WageRateService.cs#L8) | – | 4 |
| `CreateWageRateRequest` | public sealed record | [Application/WageRateView.cs:5](src/Northstar.Application/WageRates/WageRateView.cs#L5) | – | 13 |
| `WageRateView` | public sealed record | [Application/WageRateView.cs:19](src/Northstar.Application/WageRates/WageRateView.cs#L19) | – | 14 |

### Northstar.Infrastructure

*11 types.*

| Type | Declared as | Where | Base type and interfaces | Members |
| --- | --- | --- | --- | ---: |
| `DependencyInjection` | public static class | [Infrastructure/DependencyInjection.cs:11](src/Northstar.Infrastructure/DependencyInjection.cs#L11) | – | 1 |
| `FakePayrollProviderClient` | public sealed class, primary constructor | [Infrastructure/FakePayrollProviderClient.cs:9](src/Northstar.Infrastructure/Integrations/FakePayrollProviderClient.cs#L9) | `IPayrollProviderClient` | 10 |
| `FakePayrollProviderClient.ProviderTimeEntryPage` | private sealed record | [Infrastructure/FakePayrollProviderClient.cs:187](src/Northstar.Infrastructure/Integrations/FakePayrollProviderClient.cs#L187) | – | 3 |
| `FakePayrollProviderClient.ProviderTimeEntryDto` | private sealed record | [Infrastructure/FakePayrollProviderClient.cs:191](src/Northstar.Infrastructure/Integrations/FakePayrollProviderClient.cs#L191) | – | 9 |
| `PayrollProviderOptions` | public sealed class | [Infrastructure/PayrollProviderOptions.cs:3](src/Northstar.Infrastructure/Integrations/PayrollProviderOptions.cs#L3) | – | 5 |
| `ImportBatchQueue` | public sealed class | [Infrastructure/ImportBatchQueue.cs:6](src/Northstar.Infrastructure/Messaging/ImportBatchQueue.cs#L6) | `IImportBatchQueue` | 3 |
| `ImportBatchWorker` | public sealed class, primary constructor | [Infrastructure/ImportBatchWorker.cs:11](src/Northstar.Infrastructure/Messaging/ImportBatchWorker.cs#L11) | `BackgroundService` | 4 |
| `ApplicationDbContext` | public sealed class, primary constructor | [Infrastructure/ApplicationDbContext.cs:15](src/Northstar.Infrastructure/Persistence/ApplicationDbContext.cs#L15) | `DbContext`, `IApplicationDbContext` | 42 |
| `ApplicationDbContext.EfApplicationTransaction` | private sealed class, primary constructor | [Infrastructure/ApplicationDbContext.cs:257](src/Northstar.Infrastructure/Persistence/ApplicationDbContext.cs#L257) | `IApplicationTransaction` | 4 |
| `DatabaseInitializer` | public static class | [Infrastructure/DatabaseInitializer.cs:11](src/Northstar.Infrastructure/Persistence/DatabaseInitializer.cs#L11) | – | 5 |
| `DemoDataIds` | public static class | [Infrastructure/DemoDataIds.cs:3](src/Northstar.Infrastructure/Persistence/DemoDataIds.cs#L3) | – | 15 |

### Northstar.Api

*7 types.*

| Type | Declared as | Where | Base type and interfaces | Members |
| --- | --- | --- | --- | ---: |
| `ApiControllerBase` | public abstract class, primary constructor | [Api/ApiControllerBase.cs:8](src/Northstar.Api/Controllers/ApiControllerBase.cs#L8) | `ControllerBase` | 7 |
| `ImportBatchesController` | public sealed class, primary constructor | [Api/ImportBatchesController.cs:9](src/Northstar.Api/Controllers/ImportBatchesController.cs#L9) | `ApiControllerBase` | 7 |
| `PayrollRunsController` | public sealed class, primary constructor | [Api/PayrollRunsController.cs:8](src/Northstar.Api/Controllers/PayrollRunsController.cs#L8) | `ApiControllerBase` | 5 |
| `ApprovePayrollRunRequest` | public sealed record | [Api/PayrollRunsController.cs:91](src/Northstar.Api/Controllers/PayrollRunsController.cs#L91) | – | 2 |
| `WageRatesController` | public sealed class, primary constructor | [Api/WageRatesController.cs:8](src/Northstar.Api/Controllers/WageRatesController.cs#L8) | `ApiControllerBase` | 3 |
| `Program` | public partial class | [Api/Program.cs:46](src/Northstar.Api/Program.cs#L46) | – | 1 |
| `HeaderUserContext` | public sealed class, primary constructor | [Api/HeaderUserContext.cs:3](src/Northstar.Api/Security/HeaderUserContext.cs#L3) | – | 3 |

### Northstar.FakePayrollProvider

*6 types.*

| Type | Declared as | Where | Base type and interfaces | Members |
| --- | --- | --- | --- | ---: |
| `ProviderScenarios` | internal static class | [FakeProvider/Program.cs:135](tools/Northstar.FakePayrollProvider/Program.cs#L135) | – | 6 |
| `TransientScenarioState` | internal sealed class | [FakeProvider/Program.cs:145](tools/Northstar.FakePayrollProvider/Program.cs#L145) | – | 2 |
| `TimeEntryFixtures` | internal static class | [FakeProvider/Program.cs:155](tools/Northstar.FakePayrollProvider/Program.cs#L155) | – | 5 |
| `TimeEntryPage` | internal sealed record | [FakeProvider/Program.cs:293](tools/Northstar.FakePayrollProvider/Program.cs#L293) | – | 3 |
| `TimeEntryRecord` | internal sealed record | [FakeProvider/Program.cs:297](tools/Northstar.FakePayrollProvider/Program.cs#L297) | – | 9 |
| `Program` | public partial class | [FakeProvider/Program.cs:307](tools/Northstar.FakePayrollProvider/Program.cs#L307) | – | 1 |

### Northstar.Domain.UnitTests

*5 types.*

| Type | Declared as | Where | Base type and interfaces | Members |
| --- | --- | --- | --- | ---: |
| `ImportBatchStateTests` | public sealed class | [UnitTests/ImportBatchStateTests.cs:5](tests/Northstar.Domain.UnitTests/ImportBatchStateTests.cs#L5) | – | 9 |
| `PayrollCalculatorTests` | public sealed class | [UnitTests/PayrollCalculatorTests.cs:6](tests/Northstar.Domain.UnitTests/PayrollCalculatorTests.cs#L6) | – | 4 |
| `PayrollCalculatorTests.TestIds` | private sealed class | [UnitTests/PayrollCalculatorTests.cs:63](tests/Northstar.Domain.UnitTests/PayrollCalculatorTests.cs#L63) | – | 7 |
| `PayrollRunStateTests` | public sealed class | [UnitTests/PayrollRunStateTests.cs:5](tests/Northstar.Domain.UnitTests/PayrollRunStateTests.cs#L5) | – | 5 |
| `WageRateSelectorTests` | public sealed class | [UnitTests/WageRateSelectorTests.cs:5](tests/Northstar.Domain.UnitTests/WageRateSelectorTests.cs#L5) | – | 12 |

### Northstar.Api.IntegrationTests

*7 types.*

| Type | Declared as | Where | Base type and interfaces | Members |
| --- | --- | --- | --- | ---: |
| `CustomWebApplicationFactory` | public sealed class | [IntegrationTests/CustomWebApplicationFactory.cs:13](tests/Northstar.Api.IntegrationTests/CustomWebApplicationFactory.cs#L13) | `WebApplicationFactory<Program>` | 6 |
| `DeterministicPayrollProviderClient` | public sealed class | [IntegrationTests/DeterministicPayrollProviderClient.cs:6](tests/Northstar.Api.IntegrationTests/DeterministicPayrollProviderClient.cs#L6) | `IPayrollProviderClient` | 8 |
| `FakePayrollProviderClientTests` | public sealed class | [IntegrationTests/FakePayrollProviderClientTests.cs:9](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L9) | – | 18 |
| `FakePayrollProviderClientTests.StubHttpMessageHandler` | private sealed class, primary constructor | [IntegrationTests/FakePayrollProviderClientTests.cs:236](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L236) | `HttpMessageHandler` | 5 |
| `FakePayrollProviderClientTests.StallingJsonStream` | private sealed class | [IntegrationTests/FakePayrollProviderClientTests.cs:254](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs#L254) | `Stream` | 13 |
| `ImportBatchesApiTests` | public sealed class | [IntegrationTests/ImportBatchesApiTests.cs:10](tests/Northstar.Api.IntegrationTests/ImportBatchesApiTests.cs#L10) | – | 26 |
| `PayrollRunsApiTests` | public sealed class | [IntegrationTests/PayrollRunsApiTests.cs:10](tests/Northstar.Api.IntegrationTests/PayrollRunsApiTests.cs#L10) | – | 8 |


## Appendix B: File index

The table lists every file that this document points into: the files behind the code excerpts, the examples in the tables and the citations in the text.

- **Shown as** is the short name that the text uses for the file. [Section 1](#1-how-to-read-this-document) explains the short names.
- **Path from the repository root** is the full path. In the Markdown file it is a link, and it opens the file when the document lies in the root folder of the repository.
- **Lines** is the number of lines that the file had when the snapshot was taken.

| Shown as | Path from the repository root | Lines |
| --- | --- | ---: |
| .editorconfig | [`.editorconfig`](.editorconfig) | 22 |
| .env.example | [`.env.example`](.env.example) | 1 |
| .github/workflows/ci.yml | [`.github/workflows/ci.yml`](.github/workflows/ci.yml) | 64 |
| .gitignore | [`.gitignore`](.gitignore) | 28 |
| Directory.Build.props | [`Directory.Build.props`](Directory.Build.props) | 10 |
| Directory.Packages.props | [`Directory.Packages.props`](Directory.Packages.props) | 17 |
| Northstar.PayrollCompliance.sln | [`Northstar.PayrollCompliance.sln`](Northstar.PayrollCompliance.sln) | 133 |
| README.md | [`README.md`](README.md) | 121 |
| docker-compose.yml | [`docker-compose.yml`](docker-compose.yml) | 28 |
| docs/adr/0001-use-modular-monolith.md | [`docs/adr/0001-use-modular-monolith.md`](docs/adr/0001-use-modular-monolith.md) | 15 |
| docs/api/example-requests.http | [`docs/api/example-requests.http`](docs/api/example-requests.http) | 61 |
| docs/architecture.md | [`docs/architecture.md`](docs/architecture.md) | 54 |
| docs/domain-glossary.md | [`docs/domain-glossary.md`](docs/domain-glossary.md) | 11 |
| docs/testing-strategy.md | [`docs/testing-strategy.md`](docs/testing-strategy.md) | 13 |
| global.json | [`global.json`](global.json) | 6 |
| Api/ApiControllerBase.cs | [`src/Northstar.Api/Controllers/ApiControllerBase.cs`](src/Northstar.Api/Controllers/ApiControllerBase.cs) | 50 |
| Api/ImportBatchesController.cs | [`src/Northstar.Api/Controllers/ImportBatchesController.cs`](src/Northstar.Api/Controllers/ImportBatchesController.cs) | 123 |
| Api/PayrollRunsController.cs | [`src/Northstar.Api/Controllers/PayrollRunsController.cs`](src/Northstar.Api/Controllers/PayrollRunsController.cs) | 91 |
| Api/WageRatesController.cs | [`src/Northstar.Api/Controllers/WageRatesController.cs`](src/Northstar.Api/Controllers/WageRatesController.cs) | 50 |
| Api/Northstar.Api.csproj | [`src/Northstar.Api/Northstar.Api.csproj`](src/Northstar.Api/Northstar.Api.csproj) | 19 |
| Api/Northstar.Api.http | [`src/Northstar.Api/Northstar.Api.http`](src/Northstar.Api/Northstar.Api.http) | 10 |
| Api/Program.cs | [`src/Northstar.Api/Program.cs`](src/Northstar.Api/Program.cs) | 46 |
| Api/launchSettings.json | [`src/Northstar.Api/Properties/launchSettings.json`](src/Northstar.Api/Properties/launchSettings.json) | 14 |
| Api/HeaderUserContext.cs | [`src/Northstar.Api/Security/HeaderUserContext.cs`](src/Northstar.Api/Security/HeaderUserContext.cs) | 15 |
| Api/appsettings.Development.json | [`src/Northstar.Api/appsettings.Development.json`](src/Northstar.Api/appsettings.Development.json) | 14 |
| Api/appsettings.SqlServer.json | [`src/Northstar.Api/appsettings.SqlServer.json`](src/Northstar.Api/appsettings.SqlServer.json) | 8 |
| Api/appsettings.json | [`src/Northstar.Api/appsettings.json`](src/Northstar.Api/appsettings.json) | 22 |
| Application/IApplicationDbContext.cs | [`src/Northstar.Application/Abstractions/IApplicationDbContext.cs`](src/Northstar.Application/Abstractions/IApplicationDbContext.cs) | 37 |
| Application/IClock.cs | [`src/Northstar.Application/Abstractions/IClock.cs`](src/Northstar.Application/Abstractions/IClock.cs) | 11 |
| Application/IImportBatchQueue.cs | [`src/Northstar.Application/Abstractions/IImportBatchQueue.cs`](src/Northstar.Application/Abstractions/IImportBatchQueue.cs) | 8 |
| Application/IPayrollProviderClient.cs | [`src/Northstar.Application/Abstractions/IPayrollProviderClient.cs`](src/Northstar.Application/Abstractions/IPayrollProviderClient.cs) | 48 |
| Application/OperationResult.cs | [`src/Northstar.Application/Common/OperationResult.cs`](src/Northstar.Application/Common/OperationResult.cs) | 29 |
| Application/DependencyInjection.cs | [`src/Northstar.Application/DependencyInjection.cs`](src/Northstar.Application/DependencyInjection.cs) | 22 |
| Application/IdempotencyRecord.cs | [`src/Northstar.Application/Idempotency/IdempotencyRecord.cs`](src/Northstar.Application/Idempotency/IdempotencyRecord.cs) | 12 |
| Application/ImportBatchMappings.cs | [`src/Northstar.Application/Imports/ImportBatchMappings.cs`](src/Northstar.Application/Imports/ImportBatchMappings.cs) | 36 |
| Application/ImportBatchProcessor.cs | [`src/Northstar.Application/Imports/ImportBatchProcessor.cs`](src/Northstar.Application/Imports/ImportBatchProcessor.cs) | 347 |
| Application/ImportBatchService.cs | [`src/Northstar.Application/Imports/ImportBatchService.cs`](src/Northstar.Application/Imports/ImportBatchService.cs) | 383 |
| Application/ImportBatchView.cs | [`src/Northstar.Application/Imports/ImportBatchView.cs`](src/Northstar.Application/Imports/ImportBatchView.cs) | 34 |
| Application/Northstar.Application.csproj | [`src/Northstar.Application/Northstar.Application.csproj`](src/Northstar.Application/Northstar.Application.csproj) | 17 |
| Application/PayrollRunService.cs | [`src/Northstar.Application/PayrollRuns/PayrollRunService.cs`](src/Northstar.Application/PayrollRuns/PayrollRunService.cs) | 277 |
| Application/PayrollRunView.cs | [`src/Northstar.Application/PayrollRuns/PayrollRunView.cs`](src/Northstar.Application/PayrollRuns/PayrollRunView.cs) | 36 |
| Application/WageRateService.cs | [`src/Northstar.Application/WageRates/WageRateService.cs`](src/Northstar.Application/WageRates/WageRateService.cs) | 129 |
| Application/WageRateView.cs | [`src/Northstar.Application/WageRates/WageRateView.cs`](src/Northstar.Application/WageRates/WageRateView.cs) | 32 |
| Domain/Employee.cs | [`src/Northstar.Domain/Employees/Employee.cs`](src/Northstar.Domain/Employees/Employee.cs) | 15 |
| Domain/ImportBatch.cs | [`src/Northstar.Domain/Imports/ImportBatch.cs`](src/Northstar.Domain/Imports/ImportBatch.cs) | 126 |
| Domain/ImportRow.cs | [`src/Northstar.Domain/Imports/ImportRow.cs`](src/Northstar.Domain/Imports/ImportRow.cs) | 28 |
| Domain/Northstar.Domain.csproj | [`src/Northstar.Domain/Northstar.Domain.csproj`](src/Northstar.Domain/Northstar.Domain.csproj) | 9 |
| Domain/Organization.cs | [`src/Northstar.Domain/Organizations/Organization.cs`](src/Northstar.Domain/Organizations/Organization.cs) | 11 |
| Domain/ComplianceFinding.cs | [`src/Northstar.Domain/PayrollRuns/ComplianceFinding.cs`](src/Northstar.Domain/PayrollRuns/ComplianceFinding.cs) | 19 |
| Domain/PayrollCalculator.cs | [`src/Northstar.Domain/PayrollRuns/PayrollCalculator.cs`](src/Northstar.Domain/PayrollRuns/PayrollCalculator.cs) | 157 |
| Domain/PayrollLine.cs | [`src/Northstar.Domain/PayrollRuns/PayrollLine.cs`](src/Northstar.Domain/PayrollRuns/PayrollLine.cs) | 25 |
| Domain/PayrollRun.cs | [`src/Northstar.Domain/PayrollRuns/PayrollRun.cs`](src/Northstar.Domain/PayrollRuns/PayrollRun.cs) | 105 |
| Domain/PayrollRunStatus.cs | [`src/Northstar.Domain/PayrollRuns/PayrollRunStatus.cs`](src/Northstar.Domain/PayrollRuns/PayrollRunStatus.cs) | 15 |
| Domain/Project.cs | [`src/Northstar.Domain/Projects/Project.cs`](src/Northstar.Domain/Projects/Project.cs) | 18 |
| Domain/WorkClassification.cs | [`src/Northstar.Domain/Projects/WorkClassification.cs`](src/Northstar.Domain/Projects/WorkClassification.cs) | 9 |
| Domain/TimeEntry.cs | [`src/Northstar.Domain/TimeEntries/TimeEntry.cs`](src/Northstar.Domain/TimeEntries/TimeEntry.cs) | 16 |
| Domain/RateSelectionResult.cs | [`src/Northstar.Domain/WageRates/RateSelectionResult.cs`](src/Northstar.Domain/WageRates/RateSelectionResult.cs) | 23 |
| Domain/WageRate.cs | [`src/Northstar.Domain/WageRates/WageRate.cs`](src/Northstar.Domain/WageRates/WageRate.cs) | 32 |
| Domain/WageRateSelector.cs | [`src/Northstar.Domain/WageRates/WageRateSelector.cs`](src/Northstar.Domain/WageRates/WageRateSelector.cs) | 70 |
| Infrastructure/DependencyInjection.cs | [`src/Northstar.Infrastructure/DependencyInjection.cs`](src/Northstar.Infrastructure/DependencyInjection.cs) | 66 |
| Infrastructure/FakePayrollProviderClient.cs | [`src/Northstar.Infrastructure/Integrations/FakePayrollProviderClient.cs`](src/Northstar.Infrastructure/Integrations/FakePayrollProviderClient.cs) | 200 |
| Infrastructure/PayrollProviderOptions.cs | [`src/Northstar.Infrastructure/Integrations/PayrollProviderOptions.cs`](src/Northstar.Infrastructure/Integrations/PayrollProviderOptions.cs) | 11 |
| Infrastructure/ImportBatchQueue.cs | [`src/Northstar.Infrastructure/Messaging/ImportBatchQueue.cs`](src/Northstar.Infrastructure/Messaging/ImportBatchQueue.cs) | 20 |
| Infrastructure/ImportBatchWorker.cs | [`src/Northstar.Infrastructure/Messaging/ImportBatchWorker.cs`](src/Northstar.Infrastructure/Messaging/ImportBatchWorker.cs) | 118 |
| Infrastructure/Northstar.Infrastructure.csproj | [`src/Northstar.Infrastructure/Northstar.Infrastructure.csproj`](src/Northstar.Infrastructure/Northstar.Infrastructure.csproj) | 25 |
| Infrastructure/ApplicationDbContext.cs | [`src/Northstar.Infrastructure/Persistence/ApplicationDbContext.cs`](src/Northstar.Infrastructure/Persistence/ApplicationDbContext.cs) | 265 |
| Infrastructure/DatabaseInitializer.cs | [`src/Northstar.Infrastructure/Persistence/DatabaseInitializer.cs`](src/Northstar.Infrastructure/Persistence/DatabaseInitializer.cs) | 280 |
| Infrastructure/DemoDataIds.cs | [`src/Northstar.Infrastructure/Persistence/DemoDataIds.cs`](src/Northstar.Infrastructure/Persistence/DemoDataIds.cs) | 24 |
| Web/.editorconfig | [`src/Northstar.Web/.editorconfig`](src/Northstar.Web/.editorconfig) | 17 |
| Web/.prettierrc | [`src/Northstar.Web/.prettierrc`](src/Northstar.Web/.prettierrc) | 12 |
| Web/angular.json | [`src/Northstar.Web/angular.json`](src/Northstar.Web/angular.json) | 83 |
| Web/package.json | [`src/Northstar.Web/package.json`](src/Northstar.Web/package.json) | 32 |
| Web/proxy.conf.json | [`src/Northstar.Web/proxy.conf.json`](src/Northstar.Web/proxy.conf.json) | 12 |
| Web/app.config.ts | [`src/Northstar.Web/src/app/app.config.ts`](src/Northstar.Web/src/app/app.config.ts) | 14 |
| Web/app.html | [`src/Northstar.Web/src/app/app.html`](src/Northstar.Web/src/app/app.html) | 207 |
| Web/app.routes.ts | [`src/Northstar.Web/src/app/app.routes.ts`](src/Northstar.Web/src/app/app.routes.ts) | 3 |
| Web/app.scss | [`src/Northstar.Web/src/app/app.scss`](src/Northstar.Web/src/app/app.scss) | 534 |
| Web/app.spec.ts | [`src/Northstar.Web/src/app/app.spec.ts`](src/Northstar.Web/src/app/app.spec.ts) | 40 |
| Web/app.ts | [`src/Northstar.Web/src/app/app.ts`](src/Northstar.Web/src/app/app.ts) | 96 |
| Web/demo-auth.interceptor.ts | [`src/Northstar.Web/src/app/core/demo-auth.interceptor.ts`](src/Northstar.Web/src/app/core/demo-auth.interceptor.ts) | 13 |
| Web/import-api.service.ts | [`src/Northstar.Web/src/app/imports/import-api.service.ts`](src/Northstar.Web/src/app/imports/import-api.service.ts) | 37 |
| Web/import-panel.component.html | [`src/Northstar.Web/src/app/imports/import-panel.component.html`](src/Northstar.Web/src/app/imports/import-panel.component.html) | 181 |
| Web/import-panel.component.scss | [`src/Northstar.Web/src/app/imports/import-panel.component.scss`](src/Northstar.Web/src/app/imports/import-panel.component.scss) | 249 |
| Web/import-panel.component.spec.ts | [`src/Northstar.Web/src/app/imports/import-panel.component.spec.ts`](src/Northstar.Web/src/app/imports/import-panel.component.spec.ts) | 147 |
| Web/import-panel.component.ts | [`src/Northstar.Web/src/app/imports/import-panel.component.ts`](src/Northstar.Web/src/app/imports/import-panel.component.ts) | 212 |
| Web/import.models.ts | [`src/Northstar.Web/src/app/imports/import.models.ts`](src/Northstar.Web/src/app/imports/import.models.ts) | 44 |
| Web/payroll-api.service.ts | [`src/Northstar.Web/src/app/payroll-runs/payroll-api.service.ts`](src/Northstar.Web/src/app/payroll-runs/payroll-api.service.ts) | 47 |
| Web/payroll-run.models.ts | [`src/Northstar.Web/src/app/payroll-runs/payroll-run.models.ts`](src/Northstar.Web/src/app/payroll-runs/payroll-run.models.ts) | 37 |
| Web/index.html | [`src/Northstar.Web/src/index.html`](src/Northstar.Web/src/index.html) | 13 |
| Web/main.ts | [`src/Northstar.Web/src/main.ts`](src/Northstar.Web/src/main.ts) | 6 |
| Web/styles.scss | [`src/Northstar.Web/src/styles.scss`](src/Northstar.Web/src/styles.scss) | 35 |
| Web/tsconfig.app.json | [`src/Northstar.Web/tsconfig.app.json`](src/Northstar.Web/tsconfig.app.json) | 14 |
| Web/tsconfig.json | [`src/Northstar.Web/tsconfig.json`](src/Northstar.Web/tsconfig.json) | 31 |
| Web/tsconfig.spec.json | [`src/Northstar.Web/tsconfig.spec.json`](src/Northstar.Web/tsconfig.spec.json) | 14 |
| Web/vitest.config.ts | [`src/Northstar.Web/vitest.config.ts`](src/Northstar.Web/vitest.config.ts) | 13 |
| IntegrationTests/CustomWebApplicationFactory.cs | [`tests/Northstar.Api.IntegrationTests/CustomWebApplicationFactory.cs`](tests/Northstar.Api.IntegrationTests/CustomWebApplicationFactory.cs) | 63 |
| IntegrationTests/DeterministicPayrollProviderClient.cs | [`tests/Northstar.Api.IntegrationTests/DeterministicPayrollProviderClient.cs`](tests/Northstar.Api.IntegrationTests/DeterministicPayrollProviderClient.cs) | 118 |
| IntegrationTests/FakePayrollProviderClientTests.cs | [`tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs`](tests/Northstar.Api.IntegrationTests/FakePayrollProviderClientTests.cs) | 301 |
| IntegrationTests/ImportBatchesApiTests.cs | [`tests/Northstar.Api.IntegrationTests/ImportBatchesApiTests.cs`](tests/Northstar.Api.IntegrationTests/ImportBatchesApiTests.cs) | 475 |
| IntegrationTests/Northstar.Api.IntegrationTests.csproj | [`tests/Northstar.Api.IntegrationTests/Northstar.Api.IntegrationTests.csproj`](tests/Northstar.Api.IntegrationTests/Northstar.Api.IntegrationTests.csproj) | 28 |
| IntegrationTests/PayrollRunsApiTests.cs | [`tests/Northstar.Api.IntegrationTests/PayrollRunsApiTests.cs`](tests/Northstar.Api.IntegrationTests/PayrollRunsApiTests.cs) | 143 |
| UnitTests/ImportBatchStateTests.cs | [`tests/Northstar.Domain.UnitTests/ImportBatchStateTests.cs`](tests/Northstar.Domain.UnitTests/ImportBatchStateTests.cs) | 160 |
| UnitTests/Northstar.Domain.UnitTests.csproj | [`tests/Northstar.Domain.UnitTests/Northstar.Domain.UnitTests.csproj`](tests/Northstar.Domain.UnitTests/Northstar.Domain.UnitTests.csproj) | 25 |
| UnitTests/PayrollCalculatorTests.cs | [`tests/Northstar.Domain.UnitTests/PayrollCalculatorTests.cs`](tests/Northstar.Domain.UnitTests/PayrollCalculatorTests.cs) | 105 |
| UnitTests/PayrollRunStateTests.cs | [`tests/Northstar.Domain.UnitTests/PayrollRunStateTests.cs`](tests/Northstar.Domain.UnitTests/PayrollRunStateTests.cs) | 71 |
| UnitTests/WageRateSelectorTests.cs | [`tests/Northstar.Domain.UnitTests/WageRateSelectorTests.cs`](tests/Northstar.Domain.UnitTests/WageRateSelectorTests.cs) | 103 |
| e2e/package.json | [`tests/e2e/package.json`](tests/e2e/package.json) | 13 |
| e2e/playwright.config.ts | [`tests/e2e/playwright.config.ts`](tests/e2e/playwright.config.ts) | 43 |
| e2e/calculate-payroll-run.spec.ts | [`tests/e2e/specs/calculate-payroll-run.spec.ts`](tests/e2e/specs/calculate-payroll-run.spec.ts) | 21 |
| e2e/import-mixed-batch.spec.ts | [`tests/e2e/specs/import-mixed-batch.spec.ts`](tests/e2e/specs/import-mixed-batch.spec.ts) | 17 |
| FakeProvider/Northstar.FakePayrollProvider.csproj | [`tools/Northstar.FakePayrollProvider/Northstar.FakePayrollProvider.csproj`](tools/Northstar.FakePayrollProvider/Northstar.FakePayrollProvider.csproj) | 7 |
| FakeProvider/Program.cs | [`tools/Northstar.FakePayrollProvider/Program.cs`](tools/Northstar.FakePayrollProvider/Program.cs) | 307 |

## Appendix C: How the links were made and checked

Scripts built the API links in this document from what the compiler reported, and a script tested every address against the live site on 16 November 2026. This appendix says what was done, so that you can judge how far to trust a link and can repeat a check yourself.

### Finding the APIs

The C# compiler read the 54 C# files of the 7 projects, with the same reference assemblies that a build uses, and reported no errors. For every name in the code the compiler then gave the *symbol* behind it: the exact type, method, property or field. The symbols that are declared outside the repository are the 568 APIs of [section 5](#5-net-api-inventory).

Every symbol has a *documentation id*, a text such as `M:System.Math.Min(System.Int32,System.Int32)`. The first letter gives the kind of symbol, for example `T` for a type and `M` for a method, and the parameter types tell the overloads of a method apart ([XML documentation comments: ID strings](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/xmldoc/#id-strings)). The code calls two overloads of `Math.Min`, and their ids differ only in the part in round brackets. Every link below starts from this id.

### Documentation links for .NET APIs

545 of the 568 APIs have a page in the API browser on Microsoft Learn. The address of the page follows from the documentation id. The part `?view=…` at its end asks for one version of the page. Five views are used:

| View | Name on Microsoft Learn | Links |
|---|---|---:|
| `net-10.0` | .NET 10 | 301 |
| `aspnetcore-10.0` | ASP.NET Core in .NET 10 | 116 |
| `efcore-10.0` | Entity Framework Core 10.0 | 73 |
| `net-10.0-pp` | .NET 10 (package-provided) | 52 |
| `msdata-sqlite-10.0.0` | Microsoft.Data.Sqlite 10.0 | 3 |

A link was kept only when three things held:

1. The site answered with the HTTP status 200.
2. The page was served in one of the five views. Microsoft Learn shows in the address when a page falls back to another version, and such a page was not accepted.
3. The page names the same API in its own metadata as the documentation id does.

A link to a member ends in an anchor that leads to the exact member or overload on its page. The anchor was taken from the ids inside the page, and only when exactly one of them matched the documentation id. 341 links have such an anchor. A link to an enum value opens the page of its enum.

The 23 xUnit APIs have no page in the API browser. They have a source link only.

### Source links to the .NET Source Browser

469 APIs link to the [.NET Source Browser (source.dot.net)](https://source.dot.net/). That site gives every symbol a short id that it computes from the documentation id. The script computed the same id, looked it up in the index that the site publishes for each assembly, and so found the file that declares the symbol. A second pass loaded each of those file pages and confirmed that the page holds the id as an anchor. All 469 links passed.

The Source Browser is built from the current main branch of the .NET repositories. For 4 APIs the main branch declares the type in another assembly than .NET 10 does, and the site's own search gave the new place.

### Source links to GitHub

The Source Browser does not index EF Core, Microsoft.Data.Sqlite or xUnit. For those 99 APIs the script read the list of files in the repository at the release tag, picked the file of the type, and searched it for the declaration. A line number was attached only when exactly one declaration fitted both the name and the parameter list. Otherwise the link opens the file.

| Repository | Tag | Links | To a line | To a file |
|---|---|---:|---:|---:|
| `dotnet/efcore` | `v10.0.15` | 76 | 74 | 2 |
| `xunit/assert.xunit` | `v2-2.9.3` | 20 | 20 | 0 |
| `xunit/xunit` | `v2-2.9.3` | 3 | 3 | 0 |

One detail about the EF Core tag. The last commit pins the four EF Core packages at version 10.0.11, and the compiler pass read those assemblies. The working tree changes the pin to 10.0.15 ([Directory.Packages.props](Directory.Packages.props)), and this document follows the working tree. All 76 declarations were therefore looked up again at the tag `v10.0.15`. Each one is in the same file and on the same line there as at `v10.0.11`.

### Links to guides and reference pages

The links to guides, to the C# language reference and to the documentation of the frontend tools were chosen by hand and then tested by a script. 559 addresses went through it. An address passed when three things held:

1. The site answered with the HTTP status 200.
2. The address was still the same after any redirect. An address that redirected was replaced by the address it led to, and tested again.
3. For a link that ends in `#name`, the page holds an element with that id, so the link lands on the right heading.

Three sites needed more care. A Microsoft Learn page was rejected when it fell back to another version. angular.dev answers an unknown address with its home page and the status 200, so a page that carried the title of the home page was rejected. rxjs.dev builds its pages in the browser, so the script read the document that the site loads for each page.

### File links and code excerpts

- Every path was checked against the snapshot: the file exists, and it has the lines that the text cites.
- Every code excerpt is cut out of its file by line numbers when the document is built. No excerpt is typed by hand.
- Every short quote from a file, such as a setting in a table, was checked to be an exact part of the line that is cited.

### The summaries in the API tables

The "What it does" text of the .NET tables does not come from a web page. A library ships an *XML documentation file* next to its assembly: the file that the C# compiler produces from the `///` comments in the source of the library ([XML documentation comments](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/xmldoc/)). The script read the summary of each API from that file and kept the first sentence. When a member only points to the documentation of another member, the script followed the pointer.

564 APIs have a summary in those files. 15 of the texts were longer than 170 characters and were cut at a word. The other 4 APIs have none, and their rows are marked †.

### Totals and limits

This document holds 2,320 links to 1,617 different addresses.

- A check shows that a page existed and had the right anchor on the day of the check. Sites move their pages, so a link can break later. The name of the API is then enough to find the page again with the search of the [.NET API browser](https://learn.microsoft.com/en-us/dotnet/api/?view=net-10.0) or of the [.NET Source Browser (source.dot.net)](https://source.dot.net/).
- The checks test addresses. They do not test the explanations, which are this document's own wording of what the linked pages say.
