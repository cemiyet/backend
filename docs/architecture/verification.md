# Architecture Verification Plan

Tests are added in the same implementation ticket as the code they protect. This is required acceptance scope, not passing-test output. Select frameworks from the actual project/runtime and run them in CI.

## Add checks with actual code

| When | Required evidence | Scope and failure witness |
| --- | --- | --- |
| First backend slice | Real project/assembly/namespace architecture tests in the .NET test solution/CI. | Allowed own-layer/foreign-Contracts references pass. Another module's Domain/Application/persistence reference, public implementation leakage, Domain I/O dependency and Host business dependency fail. Nontransitive references are demonstrated by actual compilation. Select the test framework from the real project. |
| First persistence owners | Restricted schema-role/migration integration checks. | An owner can access its own schema; cross-schema read/write/join access is denied. Owner state plus outbox commits or rolls back together. Project rules alone cannot prove SQL isolation. |
| First real web features | Import architecture checks compatible with the actual compiler/bundler. | Public feature/package entries pass; deep imports, invalid layer directions and cycles fail. Aliases, type-only imports and lazy imports resolve, and file coverage proves the checker did not silently skip TS/TSX. No backend-name feature allowlist. |
| Each participant workflow | Explicit active-owner registry/completion tests. | Missing participant cannot produce success; retries reuse receipts; failure stays visible. Add only actual implemented owners, never pretend an empty list proves the workflow. |
| Every source shared/searched/delivered | Source-owner current-authorization integration checks. | Removed club membership, revoked audience or restricted target cannot leak through old feeds/search/inbox/media previews. Source authorization unavailable means protected content withheld. |
| Before external pilot | Provider, access/security, privacy-safe logging, quota, accessibility, migration and isolated restore evidence. | Actual provider prerequisites verified; restored data replays deletion decisions; private access remains protected. Record real recovery time/data gap and chosen retention/budget/limits. |

Tests are added in the same implementation ticket as the code they protect. CI runs actual project and import rules; choose tooling then, without reconstructing the removed speculative harness. Changing a dependency means updating the architecture rationale and the corresponding real tests. No code can skip its tests merely because architectural enforcement was deferred out of planning.

## Domain scenario evidence to implement

| Scenario | Expected owner behavior |
| --- | --- |
| Unknown book without saving/ownership | Catalog creates a private provisional identity; Reading can start independently; no Library write or public submission. Public queries and other accounts cannot discover that private record; canonical resolution does not bypass owner checks. |
| Saved/owned book without reading | Library changes only its association/optional copy/loan; no intention/journey created. |
| Concurrent translations and rereads | Reading retains separate explicit journeys; Reviews keeps one explicit current account/Work vote. |
| Correct/delete a completion | Reading updates personal goals/statistics using date precision; future Challenges reconciles its result without owning the reading. |
| Contribution admission/merge | Catalog preserves source evidence and downstream IDs/history. Reviews reconciles latest eligible explicit vote; Catalog recognition survives harmless merges. |
| Public unknown Work evaluation | Publication waits for Work admission; private activity continues; unknown optional Edition alone does not block a known Work. |
| Loan concurrency | One active loan for an identified copy; a simple unknown-copy loan invents no inventory. |
| Club role/membership changes | Clubs owns admission and authority. Social/source reads lose old protected access after departure; legitimate moderation does not grant private-library access. |
| Posting restriction | Moderation records reason/scope/appeal; owners enforce publication rules without deleting private books/notes. |
| Account erasure | Accounts revokes first and waits for all required owner receipts. Catalog deletes provisional metadata/attribution/credit while keeping independent accepted facts; last-owner Club is transferred or archived. |
| Import preview/confirmation/retry | Portability preview performs no writes; confirmed rows use owner commands and stable receipts. Partial success stays recoverable without silent overwrite or invented rereads/copies. |
| Export | Portability packages authorized personal library/reading data supplied by owners. Export is not public sharing or an implicit whole-account/club-post export. |
| Summary sharing | Reading derives accurate period evidence; explicit preview chooses content/audience. Downloaded external artifacts are not promised revocable. |

The [domain model](../domain/model.md) defines the corresponding identities, transitions and access rules.
