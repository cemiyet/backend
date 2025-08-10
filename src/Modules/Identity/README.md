# Cemiyet.Modules.Identity

## Purpose

Describe responsibilities and boundaries of this module.

## Projects

- `Cemiyet.Modules.Identity.Domain` — domain entities, value objects
- `Cemiyet.Modules.Identity.Application` — commands/queries, DTOs, services
- `Cemiyet.Modules.Identity.Infrastructure` — EF Core, Repository implementations, external integrations

## Responsibilities

- Business invariants
- Public application use-cases
- Contracts (events, DTOs)

## DB

Tables (DDL sketch)

## Events

Domain events and integration events produced

## Configuration

- appsettings keys
- required secrets

## Tests

- Unit tests in `tests/Cemiyet.Modules.Identity.Tests`

## Notes

Cross-module interactions and allowed references
