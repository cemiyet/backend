# Backend Development Documentation

Status: **Ready for the first implementation slice.** The backend is intentionally cleared; implementation starts from scratch.

## Start here

1. Read [product scope](planning/product-scope.md), [vocabulary](../GLOSSARY.md) and [domain model](domain/model.md).
2. Use [architecture](architecture/system.md), [required implementation tests](architecture/verification.md) and the accepted ADRs: [module/data ownership](adr/0001-modular-monolith-and-owned-data.md), [durable workflows](adr/0002-owner-transactions-and-durable-workflows.md), [private book identity](adr/0003-private-bibliographic-identities.md).
3. Follow the [roadmap](planning/roadmap.md) and [local task queue](../.scratch/architecture-roadmap/spec.md). [Task 01](../.scratch/architecture-roadmap/issues/01-foundation.md) creates the first working slice, actual tests/CI and supported database setup.

Domain/product records are supplied locally; another repository is not required to read them. Work uses .scratch/, root GLOSSARY.md and docs/adr/ under the existing skill configuration. Frontend's separate complete FigCraft design gate does not block backend implementation.
