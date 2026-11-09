# Cemiyet Architecture

Status: **Accepted implementation baseline.** The M0–M8 [roadmap](../planning/roadmap.md) is agreed; architectural tests accompany actual code. [Product scope](../planning/product-scope.md), [domain model](../domain/model.md) and [ADRs](../adr/0001-modular-monolith-and-owned-data.md) define behavior, ownership and trade-offs.

## System shape

Use a .NET 10 modular monolith with a React/Vite responsive web application, PostgreSQL and private object storage. Backend starts from the user's cleared repository; frontend builds on the existing shell. One deployable backend contains independently owned modules. No separate deployment or empty project is created simply because a responsibility has a name.

Use EF Core/Npgsql persistence adapters compatible with the chosen .NET baseline. Pin actual supported SDK/package versions with the first implementation slice. Start with PostgreSQL search and a persisted transactional outbox; add cache/broker/search infrastructure only for a demonstrated requirement. Deployment provider, budget and numerical operating targets are explicit pre-pilot decisions, not invented here.

## Module ownership

Stateful business modules use Contracts / Domain / Application / Infrastructure. Supporting modules start with Contracts / Implementation. The future Challenges module gets code only with its M8 capability; an unimplemented module is not an empty assembly.

| Module | Owns | Public interface examples | Shape / first capability |
| --- | --- | --- | --- |
| Accounts | Local/external login associations, sessions, profile, account-wide locale/time-zone preferences, author claims, publisher organization memberships, account deletion workflow/receipts. | Authenticate, LinkLogin, ReadProfile, VerifyClaim, RequestAccountDeletion, ReadErasureStatus. | Stateful / M1; erasure M2; author/publisher tools M4. |
| Catalog | Work, Edition, credited author/publisher facts, series/adaptation relations, source provenance, owner-private Provisional book identities, contribution/reconciliation workflow, useful-contribution credit/badges. | ResolveBook, CreateProvisionalBook, SubmitContribution, ReviewContribution, ReadMergeStatus. | Stateful / M1; recognition M8. |
| Library | Saved associations, ownership/disposition, optional copies, overlapping custom lists and simple loans. | SaveBook, EditList, RecordOwnership, LendBook, ReturnLoan. | Stateful / M2. No reading intention or book-identity ownership. |
| Reading | Reading intention, independent journeys, progress/date precision, personal notes/quotations, personal goals/statistics and reading-summary meaning. | PlanRead, Start/Continue/Complete/CorrectJourney, ReadGoalProgress, ReadStatistics. | Stateful / M2; designed summaries M8. |
| Reviews | Authored reviews/spoilers, explicit public evaluation, one current account/Work rating, score reconciliation. | PublishReview, SetCurrentRating, ReadScore. | Stateful / M3. Comments/reactions belong to Social. |
| Clubs | Club membership/admission/roles, handover/archive, shared readings, free gatherings and attendance. | JoinClub, AuthorizeClubAction, PlanSharedReading, ScheduleGathering. | Stateful / M3. Joining does not start personal reading. |
| Social | Follows, personal blocks, deliberate published activities, chronological following feed, contextual threads/comments/basic likes and social interaction policy. | Follow, Block, PublishActivity, Reply, ReadFeed. | Stateful / M3. Does not own original reviews, club membership or private reading records. |
| Moderation | Reports, cases/evidence, scoped publication sanctions/actions, reasons, appeals and moderation audit. | ReportTarget, ReadRestriction, DecideCase, Appeal. | Stateful / M3, with necessary publication restrictions introduced alongside the affected capability. No account-erasure workflow. |
| Portability | Owner export packages/manifests, private import files/batches, source mappings, reviewed row decisions, confirmation, row receipts and cleanup. | RequestReadingExport, PreviewImport, ConfirmImport, ReadTransferStatus. | Stateful / export M2; import engine/adapters M5. Owns transfer state, not copied authoritative books/readings. |
| Discovery | Rebuildable search/ranking projections, discovery interests and recommendation results. | Search, Browse, Recommend, ResetInferredInterests. | Supporting / search M1; personalized recommendations M8. |
| Notifications | In-app inbox, delivery-specific preferences, grouping/deduplication, security-email delivery and delivery receipts. | ReadInbox, Acknowledge, DispatchSecurityMessage. | Supporting / security delivery M1; product inbox M3. |
| Media | Bounded asset uploads, quarantine/processing, object metadata/lifecycle and source-authorized delivery. | RequestUpload, CompleteUpload, ReadAsset, DeleteAsset. | Supporting / M1. Raw import files stay in Portability. |
| Challenges | Shared campaign definitions/windows/counting rules, opt-in participation and campaign results; optional club scope. | JoinChallenge, ReadParticipation, ReconcileReadingEvidence. | Stateful / M8 only. Does not own private personal goals or contribution awards. |

Reader-only flows work without followers, clubs or owned copies. Catalog's owner-private provisional identity can be used by Reading without a Library entry; public submission is a separate explicit action. Catalog must erase those private records/attribution/recognition on account deletion while retaining accepted independent bibliographic facts. Provisional records use a distinct owner-private store/query surface, not unreviewed rows in public Work search. Do not deduplicate or reveal private identities across readers. Source-reference resolution checks private ownership before returning a canonical link.

Personal goals belong with Reading evidence and correction rules. Contribution recognition belongs with approved Catalog evidence. Shared challenges have their own participation/rule lifecycle. There is no generic Engagement module. Moderation is about conduct decisions; Accounts owns the end of an account. Clubs is the concrete current scope, rather than a broad Communities container.

## Layers and physical seams

Create only layers needed by a working capability. Portability's batch Domain appears when real preview/confirmation rules are implemented; no empty Domain is required for an initial export adapter.

```
src/Modules/<Module>/Cemiyet.<Module>.Contracts/
src/Modules/<Module>/Cemiyet.<Module>.Domain/
src/Modules/<Module>/Cemiyet.<Module>.Application/
src/Modules/<Module>/Cemiyet.<Module>.Infrastructure/
# Supporting modules use Contracts and Implementation instead.
src/Hosts/Cemiyet.Api/
src/BuildingBlocks/Cemiyet.BuildingBlocks.Abstractions/
src/BuildingBlocks/Cemiyet.BuildingBlocks.Infrastructure/
tests/Architecture/
tests/<Module>/
tests/Integration/
```

| Layer | Purpose and permitted dependencies |
| --- | --- |
| Contracts | Immutable consumer-facing operations/results/events/IDs. No domain entities, EF/HTTP types or foreign Contracts references. Cross-owner identifiers at this seam use stable primitive/opaque wire values, not another module's implementation types. Small shared technical Abstractions are optional. |
| Domain | Aggregates, value concepts, local invariants and decisions. No other project, database/network/web dependency or ambient I/O. Supply time/identity evidence as values. |
| Application | Use cases, current authorization, transaction/operation intent and internal I/O seams. Own Domain/Contracts, technical Abstractions and only justified foreign Contracts listed below. No persistence/web adapter dependency. |
| Infrastructure | Endpoints, EF/migrations, source/participant adapters, handlers and registration. Own inner layers, technical infrastructure and the explicitly justified integration Contracts. |
| Implementation | Supporting module's use cases/endpoints/adapters; internal organization grows with real needs. Own Contracts and justified external Contracts/technical infrastructure. |
| Host | Module registration, configuration, middleware, health. References outer projects only; no business orchestration or direct module persistence. |
| BuildingBlocks | Small technical actor/event/time abstractions and outbox/dispatch implementations when actually shared. No shared Work/User/Review aggregate, universal repository or permission authority. No references to modules. |

Only a module's deliberate Contracts and registration surface are public. Enable nontransitive project references with the first .NET implementation and prove undeclared references are unavailable. Tests that compare actual project/assembly references, public types and namespaces enforce the real shape. No standalone graph-policy JSON or speculative test harness remains.

## Foreign Contracts rules

The table is a ceiling justified by known use cases, not a list of references to scaffold. Contracts stay leaf projects; a bidirectional relationship between consumers of leaf Contracts is not permission for synchronous command recursion.

| Consumer | Allowed application/implementation dependencies and reason |
| --- | --- |
| Accounts | Catalog for author/publisher identity claims; Media for profile assets. |
| Catalog | Media for cover/evidence; Moderation for applicable publication/contribution restrictions. |
| Library | Catalog for book identity; Clubs for club-shared list access; Moderation for applicable public-list restrictions. No Reading dependency. |
| Reading | Catalog for book identity; Clubs for explicitly club-shared reading/note access; Moderation for applicable publication restrictions. No Library dependency. |
| Reviews | Catalog for accepted Work/canonical identity; Reading only to verify an optional owner/journey association; Moderation for publication restrictions. A review does not require a saved entry or completed journey. |
| Clubs | Catalog for shared-reading identity; Media for club assets; Moderation for current scoped restrictions. No command to start/change a member's Reading. |
| Social | Catalog for book context; Clubs for membership/club scope; Moderation for posting/interaction restrictions; Media for bounded contextual assets. Other source checks use Social-owned consumer interfaces with source-owner adapters. |
| Moderation | Accounts for account/platform authority; Clubs for current club-role authority. Source metadata/actions use Moderation-owned interfaces implemented by source owners. No general source repository access. |
| Portability | Catalog for authorized book identity/labels and confirmed imports; Library, Reading and Reviews commands for confirmed imports. Export source adapters implement Portability's interface; no direct source-table reads. |
| Discovery | Accounts, Catalog, Library, Reading, Reviews and Clubs for indexed facts/current authorization; Moderation where a target restriction affects visibility. No writes to those owners. |
| Notifications | Accounts for private recipient/security delivery facts. Subscribe to declared Accounts/Catalog/Reading/Clubs/Social/Moderation/Portability events only as real notification handlers are added. Current content access uses recipient/source adapters, not copied event text as authority. |
| Media | Its own source-authorization interface. No direct business-module repository or assumed global content permission. |
| Challenges (M8) | Reading evidence and optional Clubs scope; Moderation for applicable public-campaign restrictions. No Library prerequisite or Catalog recognition authority. |

Explicit outer-layer integration exceptions:

- Personal-data owners implement **Accounts.Contracts** erasure participation. An owner cannot acknowledge before its own data policy is fulfilled; Accounts stores receipts, not permission to query every private table.
- Owners storing book/canonical references implement **Catalog.Contracts** reconciliation participation or a documented canonical-on-read strategy. Never silently omit an affected owner. A stateless adapter need not invent persistent duplicate data just to participate.
- Library/Reading/Reviews/Clubs and later relevant owners can implement **Social.Contracts** and **Media.Contracts** source authorization adapters when their content is shared/delivered there.
- Reportable source owners implement **Moderation.Contracts** target-context/action participation. Committed sanctions are owner-independent facts; content mutation remains owner-local.
- Library/Reading implement **Portability.Contracts** export participation for the agreed personal library/reading export. Whole-account/club-post export is not added implicitly.
- An event handler may reference only the publishing owner's Contracts for its documented event. Adding a handler requires an explicit test-backed dependency, not a blanket wildcard dependency on every module.

Define a seam when a real caller/adapter needs it; the multi-owner erasure/export/source workflows supply real consumers. Do not generate speculative participant registries before those owners exist. A runtime write handler must not re-enter itself through another module; compose read-only eligibility checks and use durable workflow steps for cross-owner changes.

## Persistence, consistency and access

Use one PostgreSQL server/database, a schema and migration history per persistence owner. No cross-owner DbContext/repository, table read/write/join/view or foreign key. Store opaque foreign identities; validate through Contracts. Runtime restricted schema roles and integration tests establish SQL isolation when implemented; migration credentials have separate controlled authority. Outbox/inbox implementation can be shared, but owner change plus its outbox record commits in the owner's transaction.

Owner-local uniqueness/version constraints protect one current account/Work vote, one active known-copy loan and lifecycle transitions. Return conflicts with recoverable input. Search/feed/inbox/goal projections may lag; current permission, account revocation, protected source visibility and eligibility are not delegated to stale projections.

Durable cross-owner workflow steps carry stable operation/event identities and idempotent receipts. Partial failure stays visible/retryable; no distributed transaction or exactly-once delivery promise. Event payloads are minimal and do not expose private text to unrelated consumers. Security-message secrets are narrowly scoped, protected and never logged.

- **Unknown identity:** Catalog creates an owner-private provisional identity without Library/Reading/public-contribution side effects. Admission resolves references while preserving personal identities/history; unaccepted Work still cannot receive public evaluations.
- **Catalog merge:** Catalog owns canonical reconciliation. Affected owners reconcile separately; legitimate copies/rereads remain distinct. Reviews preserves one latest explicit eligible vote per canonical Work and shows updating during reconciliation. Catalog recognition uses approved useful evidence and survives harmless merges.
- **Account erasure:** Accounts proves control, revokes access first and coordinates required data-owner receipts. Owners remove personal records/opinions/attribution; catalog facts and others' replies survive appropriately. Clubs handles last-owner transfer/archive. Durable deletion decisions are replayed after restore; necessary retention exceptions are narrowly specified before release.
- **Moderation:** Moderation decides/records scoped reasons, sanctions and appeals. Source owners apply committed actions and check current restrictions; club authority remains with Clubs. Posting restrictions do not erase private reading/library data. Target/source checks cannot recursively issue moderation commands.
- **Publication/privacy:** each source owns its audience and current source access. Social owns additional activity/thread rules, which cannot widen a parent's audience. Media, Discovery and Notifications obtain current authorized source/recipient decisions. Fail closed when source authorization is unavailable. Platform-controlled revocation is enforced on subsequent reads; already downloaded external copies cannot be recalled. Private delivery avoids long-lived publicly reusable URLs.
- **Portability:** export packages contain only the authorized agreed personal scope; source owners supply it. Preview has no owner writes. Confirmed import rows call owner commands with stable row receipts; retry is not a new journey/copy/vote. Raw files expire according to the transfer policy. Source adapters require verified export formats and legitimate transfer routes individually.

## Frontend architecture

The user requires full agreed M1–M8 interface/component design in FigCraft before product frontend implementation. The frontend owns its local design inventory/progress/handoff. Backend implementation may proceed independently; frontend design does not require a completed backend. Existing frontend shell remains a starting point while the design gate is pending.

Keep React/Vite/pnpm. Product UI design uses FigCraft. Organize features by reader tasks, independently from backend module names/counts. A book-details feature can use Catalog, Reviews and personal actions without changing their data ownership. Route/page composition joins feature interfaces.

```
apps/web/src/app/                   # Routes/pages/composition
apps/web/src/features/<user-task>/  # Examples: account-security, book-details, reading-journal
apps/web/src/features/<user-task>/index.ts
apps/web/src/shared/                # Technical utilities/i18n, no feature ownership
packages/ui/src/index.ts            # Reusable product-independent presentation
packages/api-client/src/index.ts    # Add when actual OpenAPI endpoints exist
```

Features expose deliberate public entries and do not import each other's implementations. App/pages compose feature interfaces; shared technical code cannot depend on features. UI cannot depend on app/business/data access; API client cannot depend on UI/app. Transport DTOs are not copies of backend aggregates. Repeated product presentation may become an explicit reusable module when real consumers justify it; do not pre-create a miscellaneous shared business model.

Architecture tests/checks added with real features must resolve aliases, type-only imports and lazy imports, detect cycles/deep imports and prove source coverage. No hard-coded feature-name list or parser-specific syntax prohibition is selected during planning. Pick tooling compatible with the actual compiler/bundler when implementing the tests.

Responsive TR/EN, keyboard/focus/semantic UI and an accessibility review target of WCAG 2.2 AA begin with real screens. Persist online-first; preserve unsent drafts, isolate owner-specific local data and clear it on logout/deletion/expiry. A pending write is not displayed as saved. Full offline synchronization remains deferred.

## Identity and operation

Serve web and `/api` on one public origin. Accounts uses ASP.NET Core Identity for credentials and server-side external callbacks. Secure HttpOnly cookies and anti-CSRF validation protect browser mutations. Linking requires account-control proof and provider identity, not matching email alone. Google/Microsoft/Facebook are pre-external-user readiness work; Apple remains conditional on a qualifying native app. Validate actual provider enrollment constraints before enabling them. Protected requests check current server-side session/account eligibility; revocation/deletion is not delayed until a long-lived cookie expires. Background owner mutations recheck eligibility, and erasure cancels/waits for in-flight personal transfer jobs before acknowledging their owner.

Use REST/JSON/OpenAPI, bounded pagination, validation/problem responses and actor/operation/request-bound idempotency for retry-sensitive commands. No credentials, private note bodies or raw imports in telemetry. Media uses bounded validation/quarantine/re-encoding and source-authorized delivery. No initial full books, arbitrary video/audio or payment infrastructure.

The accepted roadmap makes a demonstrated isolated restore an external-pilot gate: include database/assets/configuration references and deletion replay, verify access and record measured data gap/recovery time. Choose cadence/retention/quotas/budget/RPO/RTO from actual workload before pilot. Instrument health, failures, outbox lag and privacy-safe personal/community usage; test migration/recovery runbooks before external traffic.

## Implementation-time architecture verification

Per user instruction, architecture enforcement is part of each real capability's tests/CI. Add actual .NET project/assembly/namespace rules and frontend import rules with code; prove allowed references work and representative forbidden references fail. Add source-owner authorization, SQL isolation, reconciliation/erasure/transfer integration tests where those behaviors appear. Framework choice belongs with the real project/runtime.

See the [required implementation tests](verification.md). The backend currently has no application projects or application CI. Task 01 creates the real solution, tests and build pipeline; no backend test/build success is claimed before that code exists.

## Technical references

These are implementation references. Pin and verify actual compatible package versions and constraints when implementation starts; accepted architecture choices do not establish current external-service terms.

- [MSBuild explicit project references](https://learn.microsoft.com/en-us/dotnet/core/project-sdk/msbuild-props#disabletransitiveprojectreferences).
- [EF Core 10](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-10.0/whatsnew) and [Npgsql 10 provider](https://www.npgsql.org/efcore/release-notes/10.0.html).
- [ASP.NET Core antiforgery](https://learn.microsoft.com/en-us/aspnet/core/security/anti-request-forgery?view=aspnetcore-10.0) and [PostgreSQL text search](https://www.postgresql.org/docs/current/textsearch.html).
