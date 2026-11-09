---
status: accepted
---

# Owner transactions and durable cross-module workflows

Protect immediate invariants inside the owning module and use PostgreSQL owner-transaction outbox records plus idempotent participant receipts for cross-owner changes/projections. This avoids distributed transactions and a separate initial broker, at the cost of explicit retry/updating states and backlog monitoring.

Catalog owns canonical reconciliation; Accounts owns account exit and required erasure receipts; Portability owns transfer state. Source owners provide real participant/source adapters as those capabilities are implemented. Moderation owns conduct decisions, never general data-erasure access. Cached feed/search/notification data cannot grant access; source owners decide current permission. Architecture and behavioral integration tests accompany actual implementation. Required verification accompanies the code that implements each workflow.
