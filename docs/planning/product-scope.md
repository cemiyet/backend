# Cemiyet Product Scope

Status: **Confirmed product scope — 2026-11-09.**
Updated: 2026-11-09.

This document records the agreed product behavior and constraints. Capability areas are not software modules or an all-at-launch promise; the separately maintained [architecture](../architecture/system.md) defines current ownership and the [accepted roadmap](roadmap.md) defines release allocation.

## Product purpose and delivery

Cemiyet helps readers manage libraries, follow their reading journeys, discover books, and participate in reading communities. It must be compelling without following anyone or joining a club. Usage evidence may change the emphasis toward individual use.

Initial audience: individual readers in Turkey; Turkish and English UI, with independently multilingual content. Author/publisher capabilities are a priority immediately after the development MVP, adaptable as the product evolves.

One developer, variable availability, no calendar delivery schedule. Prefer free/open-source tools and low operating cost; begin with a controlled pilot and grow from evidence. Solo development does not imply deliberately minimal functionality. No numerical traffic target or spending ceiling is assumed.

- Development MVP, launch readiness, controlled pilot, and public availability are distinct milestones. MVP is not public launch. Final capability allocation belongs to the roadmap.
- Responsive web comes first. Native mobile is a later decision. Complete the entire agreed M1–M8 interface/component design in FigCraft before product frontend implementation; the local design plan/status records that prerequisite.
- Rebuild backend from scratch; extend the existing frontend shell.
- .NET 10 and a modular monolith are selected in the independently revised architecture. Enforce its layer/module rules with relevant tests when actual code is written.
- Google/Facebook/Microsoft are the desired pre-user login package, delivered late in development if appropriate. Actual enrollment/readiness must be verified. Apple login is deferred until a qualifying mobile app exists.
- First public release is free. Billing, subscriptions, sponsored placement, wallets, and paid ticketing are not first-release requirements. Later revenue remains undecided.

## Accepted capability inventory

| Area | Target behavior |
| --- | --- |
| Catalog | Book works include novels, poetry/story collections, nonfiction, children's books, and comics/manga. Group editions/translations; distinguish work, publication/edition, and optional owned copy. Initial periodical issues, podcasts, and blog catalogs are excluded. |
| Catalog identity | Every record has a permanent internal identity. Public admission requires the applicable ISBN or reviewed alternative source evidence. Provisional personal entries remain usable without nonexistent external IDs. ISBN is publication/format scoped, not genre/work/copy identity. The roadmap places ISBN lookup in M1, with book-ISBN barcode scanning as a progressively validated enhancement and manual fallback; arbitrary QR support is not promised. |
| Contributions | Reuse supplied metadata; match before creating proposals; let private entries remain usable during review. Authorized approval reconciles public records and informs/thanks contributors. Duplicate detection/prompts may be automated; initial merges require approval and preserve histories. |
| Library | Ownership is independent from reading. Detailed copies/edition/format/quantity are optional. Reuse catalog data; simple ownership remains easy. Custom lists can overlap; removing lists/memberships does not erase ownership or readings. |
| Reading | Each attempt is a journey. Support concurrency and reading/paused/completed/abandoned states. Resume or explicitly restart; want-to-read is intention. Historical completion can be entered directly, with unknown/partial dates. Derived views preserve earlier completion during rereads. Correct/enrich edition associations without losing history or equating page positions across editions; dedicated mid-reading edition switching is deferred until needed. |
| Progress and notes | Progress is optional: start/completion alone, pages, percentages, or listening time as appropriate. Private notes/quotes can link to a work or journey, optionally with position. Publication is explicit. Chapter-based progress remains an idea. |
| Ownership changes | Lending is not ownership transfer; optional return/loss and nonmember private notes are supported. Disposal/sale/gifting preserves reading history. A loan does not automatically change the recipient's data; guidance/notifications may help them act. |
| Discovery | Work without history/follows through browsing, filters, and curated surfaces. Optional editable interests; controllable/resettable inferred preferences. Private signals improve only their owner's personalized experience; initial public trends/social indicators use public signals. |
| Search | Search works/editions/ISBNs, authors, readers, clubs, and accessible lists. Results/snippets respect current permissions. Metadata suppliers, seed coverage, ranking, and exact facets still require validation/design. |
| Reader identity | Unique username plus changeable display name/pseudonym; no required public legal name, private email. Priority registration/login, verification, recovery, and session revocation. Deliberate provider linking after account-control proof; matching emails alone do not merge accounts. |
| Sharing | Personal library/history/progress/notes are private by default. Appropriate private/public/selected-person/selected-club audiences, managed simply. Remember explicitly chosen preferences; recurring activity sharing is opt-in and does not bulk-publish old records. Group frequent updates. |
| Following and feed | Public following needs no reciprocity. Newest-first following feed; distinguish discovery. Initial posts are contextual to books/readings/reviews/lists/clubs. No guaranteed next-phase private chat: require strong demand. |
| Reviews | Rating/text independently optional; unfinished books eligible. Verified-email sign-in for public publication. Separate reread impressions/reviews and optional journey linkage. At most one current account/work aggregate rating, regardless of rereads/copies. |
| Discussion | Comments/replies and a basic like on reviews and club discussions. Whole/partial spoiler protection also applies to previews, with reporting/correction. Generic standalone statuses remain later scope. |
| Clubs | Membership distinct from following; online or optionally local, open/approval/invitation admission, independent discoverability/content visibility. Fixed owner/manager/moderator/member roles, without an initial permission-builder. |
| Shared reading/events | Clubs organize reading while participants control personal start/progress/completion and publication. Free online/in-person gatherings support time/location/link and attendance intent. Paid ticketing is later scope. |
| Moderation | Reporting, blocking, authorized moderation, and basic audit history accompany public content. Separate personal block, club expulsion, public-posting restrictions, and account/security suspension. Give reasons/appeal paths; report count alone does not cause automatic deletion or permanent punishment. |
| Authority | Author/publisher verification does not authorize deleting negative reviews, manipulating scores, or reading private data. Personal blocking does not remove legitimate club duties. Leaving/removal revokes old member-only access while retaining personal reading history. |
| Authors/publishers | Existing accounts can gain verified links to catalog author identities; publishers are organizations managed by authorized accounts. A second personal login is not required solely to act in those roles. |
| Goals/recognition | Optional personal/shared goals; distinguish completed readings from distinct works. Useful approved contributions may earn optional nonfinancial badges. No initial spendable currency/shop. |
| Insights/sharing cards | Useful personal statistics are base capability. Designed periodic/annual external-sharing summaries follow in a later development phase with adequate data and explicit preview/privacy controls. |
| Portability | Export personal library/reading data and provide separate import adapters for exportable reading-library platforms popular among Turkish users, including Goodreads and other platforms only after their actual export formats/routes are verified. Shopping history is excluded. Whole-account/club-post export is not assumed settled. No universal API or all-source launch promise. |
| Import integrity | Validate supported private uploads, reuse mappings, batch-preview uncertainties/conflicts, confirm once, report row outcomes, and make retries safe. Preserve edits, real rereads/copies, date precision, and unknowns. Unmatched books use the manual contribution flow. |
| Media/language | Bounded uploads: avatars, covers, contribution evidence. General audio/video posting later. Preserve original content with language tags/filtering; automatic translation is optional after MVP only if inexpensive. |
| Notifications | In-app first, with type controls/grouping. Social email/push later as needed; account verification/recovery emails are separate. |
| Deletion | Preserve independent bibliographic catalog facts; delete personal data. The selected policy removes deleted-account ratings from current totals and deletes personal rating/review/comment records, preserving placeholders for others' replies. No hidden permanent identity hash presented as anonymity. |
| Operations | Basic monitoring and minimal usage measurement by public availability; no private note/quote bodies as analytics payloads. Small, free-first measured pilot; sophisticated abuse analytics later. Online-first persistence with protected drafts; full offline sync deferred. |

## Cross-cutting invariants

- Personal data cannot become public merely because a profile is public, someone follows an account, a book enters a shared list, or a reader joins a club.
- Revocation affects previous platform-controlled feeds/search/notifications/links too. Previously distributed external copies are outside that guarantee.
- Known catalog metadata is selected/reused, not repeatedly typed. Missing data stays unknown; imports do not invent ownership, dates, copies, or reading journeys.
- Approved catalog corrections survive external refreshes. ISBN/source matches do not authorize an automatic public merge or guarantee a cover.
- Reading corrections/deletions update current goals/statistics; preserving an old summary is not justification for incorrect current totals.
- Private activity cannot leak through public social indicators or recommendation explanations.
- More detailed ownership, progress, sharing, goals, and achievements remain optional; low configuration burden is a product requirement.

## Conditional and deferred branches

- Inactivity-based club succession: candidate with explicit nomination/approval if implemented.
- Automatic translation: cost-gated post-MVP candidate; may be omitted.
- Native apps/Apple login, full offline sync, private chat, generic statuses, general audio/video, monetary rewards: conditional or deferred as described above.
- Later monetization/affiliate/sponsorship/paid events: validation scope, without a chosen implementation.
- Demonstrated backup/restore and deletion replay is now an accepted roadmap pilot gate. Numerical operating/recovery targets are defined from actual workload/budget before pilot.
- External metadata sources, reuse/storage rights, actual provider approvals, source fixtures, and Turkish coverage: validation gates before relevant implementation/launch promises.
- Lifecycles, book relations, ownership and role rules are defined in the local domain model; modules/dependencies in architecture; release allocation in the roadmap. Concrete operating limits, retention, provider readiness and source fixtures are implementation/release prerequisites.

## Additional agreed behavior

| Area | Confirmed behavior |
| --- | --- |
| Reading scope | Reading/listening means tracking journeys. A full ebook reader, audiobook player, full-book hosting, or digital-content storefront is not an initial capability. |
| Rating intent | Ratings use 0.5–5 stars in half-star steps; unrated differs from zero. An explicit public-rating action changes the one current work rating. Private notes or new rereads do not silently replace it. Historical reviews may retain context and optionally identify an edition. |
| Personal deletion | Account deletion removes that person's own opinion/discussion text inside clubs, preserving placeholders/others' replies. Shared structured resources are not deleted solely because their creator leaves; retain only appropriate nonpersonal data. Last-owner deletion uses handover or club archiving rather than indefinitely blocking personal-data deletion. |
| Deferred social mechanics | No additional mutual-friendship system, streak mechanic, leaderboard, or general-purpose forum is an initial requirement without a demonstrated use case. These remain possible later candidates, not implied requirements of the first architecture. |

## Delivery readiness

Product/domain planning and the independent architecture baseline are complete. The accepted roadmap defines release allocation; architectural and behavioral tests accompany actual implementation.

Next work is backend task 01 for its first implementation slice, or frontend design task 01 for the full FigCraft gate. Frontend product implementation waits for that gate. Provider/source/numerical operating prerequisites remain attached to their capability/release gates.
