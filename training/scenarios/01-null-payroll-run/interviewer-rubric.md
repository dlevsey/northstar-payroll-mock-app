# Interviewer rubric

Look for concrete discussion of the nullable `FirstOrDefaultAsync` result, missing tenant/date/project filters, nondeterministic multiple matches, missing cancellation, broad exception handling, swallowed cancellation, false success, misleading zero-dollar fallback, and insufficient tests.

Strong reviews propose an explicit missing/ambiguous result and preserve unexpected exceptions for centralized problem handling.
