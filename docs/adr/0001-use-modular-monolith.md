# ADR 0001: Use a modular monolith

Status: accepted

## Context

The lab must make one business operation easy to trace while exposing realistic database, authorization, and background-work seams.

## Decision

Keep Domain, Application, Infrastructure, API, and Web as explicit modules in one solution. Domain has no framework dependency. Provider-specific and asynchronous boundaries will be added behind application-owned interfaces.

## Consequences

Transactions and query behavior remain visible, local setup stays small, and review exercises can mix modern and isolated legacy styles. Modules may be extracted only after a concrete scaling or ownership boundary appears.
