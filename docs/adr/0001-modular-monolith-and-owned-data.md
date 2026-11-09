---
status: accepted
---

# A modular monolith with module-owned persistence

Use one .NET deployment with module-owned schemas and explicit Contracts rather than microservices or shared persistence/domain models. This keeps operations manageable for one developer while preserving independently evolving business rules. The [architecture](../architecture/system.md) defines the concrete owners, permitted references and layers.

Library and Reading remain separate because saving/ownership and reading outcomes are independent. Reviews owns explicit public evaluation rather than deriving votes from private rereads; Clubs owns collective membership/authority while Social owns publication/conversation. Personal goals belong to Reading, contribution recognition to Catalog, account erasure to Accounts and shared campaigns to M8 Challenges. Stateful decisions justify Domain/Application/Infrastructure; Discovery/Notifications/Media start with Contracts/Implementation. Create layers with real capability, not empty scaffolds. Test actual references and schema isolation with implementation because changing data ownership later is expensive.
