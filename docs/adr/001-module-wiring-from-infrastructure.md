# ADR 0001 - Module wiring (Endpoints registration) from Module.Infrastructure

- Status: Accepted
- Date: 2025-12-14

## Context
This solution follows a modular monolith approach. Each module is split into projects/layers such as:

- Module.Domain
- Module.Application
- Module.Persistence
- Module.Endpoints (delivery layer)
- Module.Infrastructure (technical implementations)

We want the host (`Program.cs`) to stay minimal and avoid referencing each module’s Endpoints assembly directly.
We also want each module to be "self-registrable" so the host can load modules consistently.

In practice, registering endpoints (Controllers) requires adding the module’s Endpoints assembly to MVC via
`AddApplicationPart(...)`.

## Decision
For **all modules**, `Module.Infrastructure` is allowed to reference `Module.Endpoints` **only** for endpoint assembly
registration (composition/wiring concern), e.g.:

```csharp
services.AddControllers().AddApplicationPart(Module.Endpoints.AssemblyReference.Assembly);
