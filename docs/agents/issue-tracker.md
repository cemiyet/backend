# Issue tracker: Local Markdown

All paths are relative to this repository.

- Specs: `.scratch/<feature-slug>/spec.md`.
- Tickets: `.scratch/<feature-slug>/issues/<NN>-<slug>.md`,
  numbered from 01; one file per ticket.
- Triage: `Status:` near the top, using `triage-labels.md`.
- Comments: append under `## Comments`.
- Publishing: create the corresponding spec or ticket file.
- Fetching: read the referenced file; resolve numbers within the feature.

## Wayfinding

- Map: `.scratch/<effort>/map.md`, containing Notes,
  Decisions-so-far, and Fog.
- Children: `.scratch/<effort>/issues/<NN>-<slug>.md`.
- `Type:` is research, prototype, grilling, or task.
- Wayfinding lifecycle uses `Status: open`, `claimed`, or `resolved`.
- Dependencies: `Blocked by: NN, NN`; unblocked when all are resolved.
- Frontier: first numbered open, unblocked, unclaimed ticket.
- Claim: save `Status: claimed` before starting.
- Resolve: append `## Answer`, set `Status: resolved`, then add
  a gist and link to the map's Decisions-so-far.
