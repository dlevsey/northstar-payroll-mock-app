# Hidden test intent

- Zero matching rows must not throw a null-reference exception or create a zero rate.
- Two matching rows must not be selected without an explicit ordering or ambiguity rule.
- A cancelled query must surface cancellation.
- A database failure must not return a successful calculation.
- A cross-tenant rate must never be selected.
