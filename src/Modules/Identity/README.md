# Cemiyet.Modules.Identity

## Purpose

The Identity module is responsible for all aspects of user authentication and identity management within LitHub. It handles user registration, login, password management, email confirmation, and the issuance of authentication tokens. This module owns the security-sensitive data such as password hashes and email confirmation status and enforces authentication business rules.

**Boundaries:**

Identity proves **who the user is** and manages credentials and authentication lifecycle. It does **not** manage detailed user profiles, social features, roles beyond basic claims, or authorization policies. Other modules consume Identity’s public APIs and domain events to implement their domain-specific logic.

## Projects

- `Cemiyet.Modules.Identity.Domain`  
  Contains domain entities (e.g., `User`), value objects, domain events, and repository interfaces defining core business invariants.

- `Cemiyet.Modules.Identity.Application`  
  Implements application services, commands and queries, DTOs, and orchestrates use cases such as user registration and login.

- `Cemiyet.Modules.Identity.Infrastructure`  
  Provides EF Core database context, repository implementations, external service integrations (e.g., email token generation), and dependency injection wiring.

## Responsibilities

- Enforce authentication business rules and domain invariants
- Manage user lifecycle: registration, login, email confirmation, password reset
- Provide application use-cases as public service interfaces
- Define contracts such as domain events (e.g., `UserRegistered`) and DTOs for use by other modules
- Hash and verify passwords securely using industry-standard algorithms
- Generate and validate authentication tokens (JWT or similar)

## DB

To be Updated.

## Events

- `UserRegistered` — published when a new user successfully registers
- `UserEmailConfirmed` — published when a user confirms their email address
- `UserPasswordChanged` — published when a user updates their password

These events are published as domain or integration events to enable loose coupling with other modules like Notifications or Social.

## Configuration

- Connection Strings
  - ConnectionStrings:Identity — PostgreSQL connection string for Identity database
- Secrets
  - JWT signing key (Jwt:SigningKey) and token settings
  - Email token secret keys (if applicable)
- AppSettings
  - Identity:PasswordPolicy (optional password complexity settings)
  - Identity:Jwt section with token expiration and issuer settings

## Tests

- Unit and integration tests are located in: `tests/Cemiyet.Modules.Identity.Tests`

Tests cover domain invariants, application service workflows, and repository persistence.

## Notes

- The Identity module is designed to be independent of UI or API layers.
- It exposes only Application layer services for consumption.
- It references SharedKernel for common abstractions and event contracts.
- Other modules may consume Identity’s domain events to react to user lifecycle changes without direct coupling.
- The Gateway project is responsible for exposing Identity APIs and composing middleware for authentication.
