# Cemiyet Delivery Roadmap

Status: **Accepted roadmap — 2026-11-09.** No dates or capacity estimates. The user accepted this capability/release order; module ownership follows the independent revised architecture. [Architecture](../architecture/system.md) defines ownership/layers; [local task queue](../../.scratch/architecture-roadmap/spec.md) identifies the next action.

## Milestones and gates

Each milestone is complete through working end-to-end capability and recorded evidence, not a count of created classes/projects. Implement capabilities as vertical slices across backend/web, with FigCraft design before product UI. Dependencies order work; they do not require finishing every backend feature before touching frontend.

| Milestone | Capability and outcome | Completion gate |
| --- | --- | --- |
| M0 — Architecture baseline | Domain model, independently revised modules/layers/contracts, implementation-time architecture-test plan and accepted backlog. | Planning decisions/scenarios are documented; no standalone guard/test success is required against an empty backend. Tests accompany actual implementation. |
| M1 — Foundation and catalog | First tested backend slice/CI, accounts/security email, bounded media, accepted catalog/contributions and useful search without history. Responsive TR/EN design foundations. | A new reader signs in, searches/selects known metadata and submits missing-book evidence; protected files and access tested. No public traffic yet. |
| M2 — Individual reader value | Private library/intention/lists, optional ownership/copies/loans, independent journeys/progress/notes, base statistics/personal goals, export/deletion. | A reader with zero follows/clubs completes the full personal loop; partial dates, rereads, corrections, data export, erasure and draft recovery work. |
| M3 — Community development MVP | Reviews/current ratings, basic moderation/appeals, clubs/roles, shared reading/free gatherings, explicit sharing/following feed, contextual discussion and in-app notifications. | All accepted core personal/community journeys work internally with tested audience revocation and no score/authority manipulation. This is **development MVP, not launch**. |
| M4 — First post-MVP priority | Verified author claims and managed publisher organizations on existing accounts. | Useful authorized participation without new powers over reviews/private data; claim revocation and organization membership tested. Reorder only from explicit new product evidence. |
| M5 — Launch readiness | Google/Microsoft/Facebook package before external users, import engine plus first verified adapter work, real access/security/erasure/backup operations, accessibility/performance review and observability. | Provider enrollment readiness, TR/EN keyboard/screen-reader review, restore/deletion replay, capacity/quotas/retention/budget and all external-user critical failures resolved. Import-source evidence is evaluated per adapter. |
| M6 — Controlled pilot | Small bounded cohort using a free service. Measure personal-library/discovery and community value, failure rates, support needs and costs. | Explicit pilot checklist passes before invitation; collect evidence, fix privacy/data-loss/auth failures immediately, record remaining release blockers. No arbitrary traffic target. |
| M7 — Free public availability | Expand a measured, recoverable service after pilot evidence. | Public release checklist passes; support/moderation and restore runbooks usable; first verified library-import adapter accepted, or an explicit reviewed scope change records why unavailable. No all-source support claim. |
| M8 — Evidence-led growth | More verified export-source adapters, controllable personalized recommendations, shared challenges, useful contribution badges, designed periodic/annual sharing summaries when meaningful data exists. | Each capability receives its own validated scope, privacy/failure tests and release decision. Base stats/personal goals already exist in M2. |

Work on provider approvals and source-format validation can start earlier when they threaten later gates. M4 remains the first product expansion after MVP; readiness research is not a competing product expansion. No rule requires waiting until the pilot to fix core discovery/library usability.

## Capability traceability

| Confirmed capability | Primary owner | Delivery placement |
| --- | --- | --- |
| Local accounts, verification/recovery/sessions and private profile data | Accounts, Notifications | M1. |
| Work/Edition/source identities, adaptations/series, corrected metadata and reviewed contributions/merges | Catalog, Media | M1; participant coverage grows as owners appear. |
| ISBN lookup | Catalog, Discovery | M1. Book ISBN-barcode camera scan: progressive enhancement with manual fallback after a real browser/device feasibility check; no arbitrary QR guarantee. |
| Saved associations/lists, optional inventory/lending/disposal | Library | M2. |
| Private provisional book identity, independent from saved/owned records | Catalog | Catalog identity/contribution foundation M1; personal use M2. |
| Reading intention, parallel/reread/historical journeys, optional progress, notes/quotes, partial dates | Reading | M2. |
| Base personal stats, optional personal goals and owner export/deletion | Reading, Portability and each personal-data owner; Accounts coordinates erasure | M2. |
| Ratings/reviews/spoilers, basic reports/blocks/reasons/appeals | Reviews, Social, Moderation | M3, same milestone as public UGC. |
| Clubs/roles/admission, shared reading, free events and handover/archive | Clubs | M3. |
| Private defaults, remembered explicit audiences, opt-in recurring sharing, public follows, chronological feed, comments/basic like, grouped inbox | Source owners, Social, Notifications | M3. |
| Verified author/publisher capabilities without review control | Accounts and Catalog | M4, immediate post-MVP priority. |
| Google/Microsoft/Facebook sign-in/linking | Accounts | M5, before any external pilot users. Apple conditional later. |
| Import engine, Goodreads and other verified exportable reader-library sources | Portability and source owners | M5 engine/first verified adapter; M7 first-adapter acceptance gate; remaining adapters M8 as individually verified features. No shopping-history scope. |
| Search readers/clubs/accessible lists and language filtering | Discovery and source owners | M3 as those owners become available; privacy enforced before inclusion. |
| Personalized recommendations and public-signal-only trends | Discovery | M8; M1 already offers useful cold-start browse/search. |
| Shared challenges and useful approved-contribution recognition | Challenges and Catalog respectively | M8; no currency/shop. |
| Designed summary/share cards | Reading, Media, frontend | M8 when adequate data supports meaningful output; explicit preview/publication. |
| Minimal safe monitoring, growth measurement and recovery | All owners, Host/operations | Instrument from M1, full external-user gates M5–M7. |

## Pilot and public gates

Before external users: real provider prerequisites; secure verified account/recovery/session flows; per-resource access and current revocation tests across feeds/search/media/notifications; baseline moderation; erasure and durable restore replay; private-data-safe diagnostics; numerical size/rate limits; measured backup/recovery objectives; tested migrations; supported responsive TR/EN journeys and accessibility review. These are release checklist decisions with concrete recorded evidence, not assertions that the empty backend currently implements them.

The accepted roadmap makes demonstrated backup/restore a pilot gate. Actual RPO/RTO, retention and operating budget are defined using deployment/workload evidence before pilot; this roadmap does not invent calendar or numeric service promises.

Before public availability: pilot findings triaged, core personal/community loops reliable, useful catalog coverage verified, support/moderation operations usable and first verified library-import adapter accepted or explicitly rescoped. Source-specific missing export support remains visible, not replaced with scraping or a universal-import claim.

## Frontend design prerequisite

Complete the full agreed M1–M8 interface and reusable components in FigCraft before product frontend implementation. This is the frontend D0 design gate, not a change to capability/release order. Include responsive/interaction states, prototypes and recorded coverage/handoff. Backend implementation can begin independently. Conditional unapproved features are outside the gate.

## Local issue tracker

[Backend queue](../../.scratch/architecture-roadmap/spec.md): 22 scoped tasks with dependencies and acceptance criteria. Backend 01 is ready; no ticket has started. Provider enrollment/source fixtures remain explicit evidence gates. Work records retain the selected local Markdown location, .scratch/architecture-roadmap/. Existing ignore rules are unchanged.

Frontend counterpart work is tracked independently in `cemiyet/frontend`: full FigCraft design task 01 precedes implementation task 18 and other code tasks. Counterpart IDs are references, not a requirement for an adjacent checkout.

## Conditional branches

Private chat requires strong demand. Automatic translation requires acceptable cost and may be omitted. Native/Apple requires a qualifying app decision. Inactivity-based succession needs protected nomination/approval and appeal. Full offline sync, generic statuses, general audio/video, sophisticated abuse analysis and monetary features retain their explicit conditions; they are not extra design or release prerequisites. First public release has no payment/subscription/wallet/sponsored-placement requirement.

## Next action

Backend 01 is the next executable backend task when implementation is requested. Add architecture tests alongside real code. Frontend independently begins complete FigCraft design, with product implementation blocked until its design gate is recorded complete. Actual provider/source/workload/budget/retention/recovery values remain capability/release prerequisites.
