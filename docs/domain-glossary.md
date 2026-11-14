# Domain glossary

- **Effective rate**: the wage and fringe requirement selected for one work date and classification.
- **Fringe**: an hourly benefit obligation tracked separately from cash wages.
- **Finding**: a blocker, warning, or informational result created by calculation.
- **Import batch**: one tenant-scoped, idempotently accepted request to retrieve and validate provider time entries.
- **Import row**: a sanitized staging record retained as imported, invalid, or duplicate so partial provider quality remains diagnosable.
- **Processing lease**: the bounded claim window after which an interrupted local worker may safely attempt to reclaim a batch using optimistic concurrency.
- **Payroll run**: the aggregate containing a pay period, calculated lines, findings, and approval state.
- **Rate scope**: fictional precedence category used by this lab: employee override, project, union, state, then federal.
- **Tenant**: an organization whose records must not be disclosed to another organization.
