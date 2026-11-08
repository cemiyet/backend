# Cemiyet MVP cloud-cost research

**Checked:** 2026-07-11  
**Scope:** a low-traffic public beta (roughly 250 Readers, one region, no high
availability), with a containerised .NET 10 API, an Angular static site,
PostgreSQL, modest cover/avatar uploads, transactional email, and basic logs.
Prices are USD/month, before VAT, domain registration, and payment-provider
fees. They are planning estimates, not a quote.

## Bottom line

Cemiyet can stay well below the agreed USD 100/month ceiling. A sensible
public-beta budget is **USD 30/month**, with a **USD 50/month alert**. The
lowest credible managed configuration is about **USD 26/month**; an Azure-first
configuration is about **USD 38/month**. Neither needs a reader subscription
to cover its infrastructure at first.

The cost driver is the always-on API plus PostgreSQL, not Angular hosting,
email, or a few gigabytes of covers. Do not launch on a provider's expiring or
sleeping free database as the only production database.

## Working assumptions

- The API is one small Linux container; background jobs are in-process or a
  low-frequency scheduled job. No Redis, search cluster, queue, or CDN image
  transformations yet.
- Files stay below 10 GB; object reads stay below 10 million/month; API/static
  delivery and email stay within the included allowances below.
- A single-region service and a single PostgreSQL instance are acceptable for a
  beta. This is **not** an HA or disaster-recovery design.
- Existing local `compose.yaml` already models a single PostgreSQL service, so
  no extra data services are costed.

## Configurations to compare

| Configuration | API + database | Frontend, files, email | Estimated recurring cost | Why / main trade-off |
| --- | --- | --- | ---: | --- |
| **Recommended managed beta** | Render Starter API: $7; Render Basic 1 GB PostgreSQL: $19 | Cloudflare Pages + R2: $0 at assumed usage; Resend: $0 | **$26** | Simple deployment and hosted database. The 1 GB database is a safer starting point than the $6/256 MB floor. Watch Render's included 5 GB bandwidth and then $0.15/GB. |
| **Azure-first beta** | Azure App Service Linux B1: $13.14; Supabase Pro PostgreSQL/storage: $25 | Azure Static Web Apps Free; R2: $0; Resend: $0 | **$38.14** | Keeps the .NET API in Azure while avoiding an early Azure-managed-PostgreSQL commitment. Two vendors, but comfortably inside the cap. |
| **AWS cost-minimised beta** | One AWS Lightsail Linux 2 GB instance: $12, running API and PostgreSQL with Docker | Cloudflare Pages + R2: $0; Resend: $0 | **$12** | Lowest steady bill, but Cemiyet owns PostgreSQL patching, backups, restore tests, monitoring, and the single-server failure mode. Do not use this if that operational work is unwelcome. |
| **AWS managed-service variant** | Lightsail container micro: $10; Lightsail managed database: $15 | Cloudflare Pages + R2: $0; Resend: $0 | **$25** | Reduces database operations while retaining predictable pricing; verify the selected region and sizing in the Lightsail console before committing. |

The two AWS managed prices are published bundle prices; selected qualifying
Lightsail bundles also have a three-month new-account offer, which is not
included in the estimates. [Lightsail pricing](https://aws.amazon.com/lightsail/pricing/)

## Component evidence and cost guardrails

### API and PostgreSQL

- [Render's pricing](https://render.com/pricing) lists a $7/month Starter web
  service (512 MB / 0.5 CPU), $6/month 256 MB PostgreSQL, and $19/month 1 GB
  PostgreSQL. Its free database lasts 30 days, so it is a development/preview
  option rather than the public-beta database. The $26 recommendation uses the
  $19 tier deliberately.
- [Azure App Service on Linux pricing](https://azure.microsoft.com/en-us/pricing/details/app-service/linux/)
  lists B1 at $13.14/month with 1 core, 1.75 GB RAM and 10 GB storage. This is
  a stable cost for a small .NET API. Azure charges dedicated App Service
  capacity even when apps are stopped, so it is not a scale-to-zero option.
- [Supabase pricing](https://supabase.com/pricing) lists Pro at $25/month for
  the first project, including a $10 compute credit (one 1 GB Micro instance),
  8 GB disk, 250 GB egress, 100 GB file storage, daily backups with seven-day
  retention, and 100,000 MAU. Cemiyet may use the hosted PostgreSQL directly
  from its .NET API; adopting Supabase Auth is optional, not assumed here.
  Supabase Free has only 500 MB database storage and pauses after a week of
  inactivity, so reserve it for development or a private prototype.
- [AWS Lightsail pricing](https://aws.amazon.com/lightsail/pricing/) lists the
  2 GB Linux/Unix instance at $12/month (60 GB SSD, 3 TB transfer), a $10/month
  container micro bundle, and a $15/month database bundle. Self-hosting API and
  database together should start at 2 GB, not the 0.5 GB/1 GB plans.
- For a Dockerised Azure alternative, [Azure Container Apps pricing](https://azure.microsoft.com/en-us/pricing/details/container-apps/)
  includes 180,000 vCPU-seconds, 360,000 GiB-seconds, and two million requests
  per subscription per month, and can scale to zero. Its exact post-grant rate
  and Azure Database for PostgreSQL Flexible Server cost are region-specific;
  obtain a calculator estimate before choosing it. Azure's managed PostgreSQL
  bills compute, provisioned storage, and backup storage separately.

### Angular hosting and object storage

- [Azure Static Web Apps](https://azure.microsoft.com/en-us/pricing/details/app-service/static/)
  Free includes 100 GB bandwidth, two custom domains and SSL, but has no SLA
  and stops serving when its quota is exceeded. It is suitable while the beta
  remains small.
- [Cloudflare Pages limits](https://developers.cloudflare.com/pages/platform/limits/)
  and [static-asset billing](https://developers.cloudflare.com/workers/static-assets/billing-and-limitations/)
  make it a good $0 Angular host: static asset requests are free; do not add
  Pages Functions/Workers unless their separate request limits and pricing are
  acceptable.
- [Cloudflare R2 pricing](https://developers.cloudflare.com/r2/pricing/) has a
  free monthly allowance of 10 GB storage, one million Class-A writes, ten
  million Class-B reads, and no Internet egress fee. Above the free tier,
  standard storage is $0.015/GB-month, writes $4.50/million, and reads
  $0.36/million. Store covers and avatars in R2 behind signed URLs or an
  application policy; do not expose a write-capable public bucket.
- If AWS-native storage is chosen, [Amazon S3 pricing](https://aws.amazon.com/s3/pricing/)
  starts S3 Standard at $0.023/GB-month before request and transfer charges.
  R2 is clearer and cheaper for a media-light beta because of its included
  usage and zero egress.

### Email and observability

- [Resend's pricing](https://resend.com/docs/knowledge-base/what-is-resend-pricing)
  gives 3,000 transactional emails/month free, with a 100/day limit. That is
  enough for verification, password reset, and early notifications; its Pro
  tier is $20/month for 50,000. Use a verified Cemiyet domain and configure
  SPF/DKIM before inviting Readers.
- [AWS SES pricing](https://aws.amazon.com/ses/pricing/) is an alternative at
  $0.10 per 1,000 sent/received messages (plus applicable data charges), if
  the service is already on AWS.
- Start with structured application logs, health checks, provider metrics and
  a retention limit. For the Azure path, [Application Insights](https://learn.microsoft.com/en-us/azure/azure-monitor/app/application-insights-faq)
  includes 5 GB/month telemetry ingestion. Set a budget alert before enabling
  verbose request/body logging; observability can otherwise become the first
  surprising bill.

## Recommendation and decision rule

1. **Use the recommended managed beta**: Cloudflare Pages for Angular,
   Cloudflare R2 for user media, Render Starter for the .NET API, Render 1 GB
   PostgreSQL, and Resend Free. Budget **$30/month** including headroom, plus
   the annual domain. It is the lowest-friction way to validate use without
   accepting a sleeping/free database.
2. Choose the **$12 Lightsail** design only if the founder will automate
   encrypted daily database dumps to separate object storage and periodically
   test restores. Its lower invoice is purchased with operational responsibility.
3. Prefer the **$38 Azure-first** design only if Azure experience, procurement,
   or future Microsoft integrations are strategically valuable. It is not the
   cheapest route, but it leaves substantial room under $100.
4. Do not add Redis, Elasticsearch/OpenSearch, paid email, managed search,
   multi-region replicas, or a subscription billing system until usage makes a
   specific one necessary. Each is a separate operating-cost decision.

Set account-level budget alerts at **$30** (warning), **$50** (action), and
**$80** (freeze new paid services and investigate). Re-estimate when either
database storage passes 5 GB, media passes R2's 10 GB free tier, API egress
passes a provider's included allowance, or monthly active Readers reach 1,000.
