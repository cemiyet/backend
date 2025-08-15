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

```sql
-- Create schema for Identity module
CREATE SCHEMA IF NOT EXISTS identity;

-- Users table: core identity info
CREATE TABLE identity.Users (
    Id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    Email VARCHAR(255) NOT NULL UNIQUE,
    PasswordHash VARCHAR(512) NOT NULL,
    DisplayName VARCHAR(255),
    EmailConfirmed BOOLEAN NOT NULL DEFAULT FALSE,
    CreatedAt TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    UpdatedAt TIMESTAMPTZ NOT NULL DEFAULT NOW()
);

CREATE UNIQUE INDEX IX_Users_Email ON identity.Users (Email);

-- Trigger to update UpdatedAt column on Users table when row changes
CREATE OR REPLACE FUNCTION identity.update_users_updated_at()
RETURNS TRIGGER AS $$
BEGIN
   NEW.UpdatedAt = NOW();
   RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trg_users_updated_at
BEFORE UPDATE ON identity.Users
FOR EACH ROW
EXECUTE FUNCTION identity.update_users_updated_at();
```

## Events

- `UserRegistered` — published when a new user successfully registers
- `UserEmailConfirmed` — published when a user confirms their email address
- `UserPasswordChanged` — published when a user updates their password

These events are published as domain or integration events to enable loose coupling with other modules like Notifications or Social.

## Configuration

- Connection Strings
  - ConnectionStrings:Identity — PostgreSQL connection string for Identity database

## Tests

- Unit tests in `tests/Cemiyet.Modules.Identity.Tests`

## Notes

- The Identity module is designed to be independent of UI or API layers.
- It exposes only Application layer services for consumption.
- It references SharedKernel for common abstractions and event contracts.
- Other modules may consume Identity’s domain events to react to user lifecycle changes without direct coupling.
- The Gateway project is responsible for exposing Identity APIs and composing middleware for authentication.
