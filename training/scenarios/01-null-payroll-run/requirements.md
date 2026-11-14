# Requirements

- A missing rate creates a blocker finding with no required-dollar amounts.
- A database or provider exception fails the request and is handled by the API exception boundary.
- The response must never claim calculation succeeded after rollback.
- Cancellation must propagate unchanged.
