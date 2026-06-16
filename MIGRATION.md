Migration plan (short)

Objective
Migrate the legacy single-class OrderProcessor into a layered, testable .NET 8 solution that respects the project architecture (Domain, Application, Infrastructure, Web).

Approach
- Implement an application-level service (OrderService) that owns the orchestration: read school tier, query product prices, check inventory, compute price via a PricingService, charge payment via IPaymentService, and send notifications via IEmailSender.
- Introduce small interfaces (ISchoolRepository, IProductRepository, IInventoryService, IPaymentService, IEmailSender) so infrastructure concerns can be swapped (in-memory, EF Core, external APIs).
- Keep business rules unchanged (tier discounts, embroidery pricing, stock checks, payment failure semantics) but return a structured Result object.
- Start with in-memory Infrastructure implementations for rapid validation and automated tests; then replace with EF Core repositories, real payment gateway integration, and SMTP/service bus for email.

Why this fits
- Aligns with existing layered projects in the solution and supports dependency injection used by ASP.NET Core Razor Pages.
- Testability: business rules are isolated from I/O and can be validated with unit tests before production integration.
- Incremental migration: in-memory fakes allow safe verification in CI and staging prior to switching live services.

Risk to surface
Data consistency around stock reduction: the current design checks stock before payment but does not guarantee atomicity across payment + stock update. In production this can cause overselling under concurrency. I would surface the need for a transactional approach (database-level transaction or reservation system) and a rollback/compensation strategy before live rollout.
