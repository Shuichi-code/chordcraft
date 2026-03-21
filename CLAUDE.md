# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## What is ChordCraft

A web-based typing tutor for the **CharaChorder One** (CC1) — a chord-based input device. Users practice pressing individual switches and chords to build muscle memory. The app visualizes the CC1's 18 switches and highlights which finger/direction to use for each target input.

## Commands

```bash
# Build entire solution
dotnet build ChordCraft.sln

# Run the API (serves Blazor WASM from wwwroot in dev)
dotnet run --project src/ChordCraft.Api

# Run all tests
dotnet test ChordCraft.sln

# Run a single test project
dotnet test tests/ChordCraft.Core.Tests
dotnet test tests/ChordCraft.Api.Tests
dotnet test tests/ChordCraft.Client.Tests

# Run a specific test by name
dotnet test tests/ChordCraft.Core.Tests --filter "DisplayName~StarCalculation"

# Add a new EF Core migration
dotnet ef migrations add <MigrationName> --project src/ChordCraft.Infrastructure --startup-project src/ChordCraft.Api

# Apply migrations
dotnet ef database update --project src/ChordCraft.Infrastructure --startup-project src/ChordCraft.Api

# Publish for production (API includes WASM output in wwwroot)
dotnet publish src/ChordCraft.Api -c Release -o ./publish
```

## Architecture

**Single-service deployment:** The API hosts the Blazor WASM files from its `wwwroot`. One Railway service, one URL. The Dockerfile copies WASM publish output into the API's wwwroot.

```
src/
  ChordCraft.Core/          # Domain: entities, enums, interfaces, DTOs, CcosLayout
  ChordCraft.Infrastructure/ # EF Core, Identity, service implementations, seed data
  ChordCraft.Api/           # ASP.NET Core Web API + serves Blazor WASM
  ChordCraft.Client/        # Blazor WebAssembly frontend
tests/
  ChordCraft.Core.Tests/    # xUnit — domain logic
  ChordCraft.Api.Tests/     # xUnit + WebApplicationFactory — integration
  ChordCraft.Client.Tests/  # bUnit — component tests
docs/TDD.md                 # Full technical design document — authoritative reference
```

## Key Architectural Decisions

| Decision | Choice | Why |
|----------|--------|-----|
| Client rendering | Blazor WASM | Zero-latency keypress — typing apps cannot tolerate server round-trips |
| Anonymous storage | LocalStorage only | Avoids server-side anonymous sessions; incentivizes signup |
| JWT storage | In-memory (not LocalStorage) | XSS protection |
| Progress service | Interface + 2 implementations | `LocalProgressService` for anon, `ApiProgressService` for authenticated; swapped on login |
| Chord detection | 80ms character buffer + flush timer | CC1 firmware sends chord output as rapid sequential keystrokes |
| Landing page layout | `LandingLayout` (no AppBar) | Landing page uses its own navbar — `@layout LandingLayout` bypasses `MainLayout`'s `MudAppBar` |

## Client Layouts

- `src/ChordCraft.Client/Layout/MainLayout.razor` — default layout with `MudAppBar` (all authenticated/app pages)
- `src/ChordCraft.Client/Layout/LandingLayout.razor` — bare layout (MudBlazor providers only, no AppBar) used by `Landing.razor`
- `src/ChordCraft.Client/Layouts/DashboardLayout.razor` — dashboard-specific layout

Scoped CSS files (`.razor.css`) are used for component-level styles. Use `padding-left`/`padding-right` longhand (not `padding` shorthand) on elements that also carry the `.container` class, to avoid the container's padding override.

## Domain Model

- **User** — extends `IdentityUser<Guid>`. All user IDs are `Guid`.
- **Lesson** — has `Content` (jsonb) and `TargetInputs` (jsonb). Reference data IDs are `int`.
- **UserProgress** — composite PK: `(UserId, LessonId)`. Best stars/accuracy/speed per lesson.
- **LessonAttempt** — individual attempt record with `InputLog` (jsonb) for keystroke data.
- **SwitchInput** — `record(SwitchId Switch, SwitchDirection Direction)` — the core domain type identifying a single CC1 input across 18 switches × 5 directions.
- **CcosLayout** — static class in Core mapping all 90 `SwitchInput` → character. Used by the typing engine, switch diagram, and hand overlay.

## Progress Service Pattern

```csharp
IClientProgressService  →  ProgressRouterService  →  LocalProgressService (anon)
                                                    →  ApiProgressService (authenticated)
```

On login/register, anonymous LocalStorage data is migrated to the API via `POST /api/auth/migrate`, then the router switches to `ApiProgressService`.

## Typing Engine (Chord Detection)

The `TypingEngine` uses a JS interop keydown listener (`wwwroot/js/inputCapture.js`). For chord phases (Phase 4+), keystrokes are buffered for 80ms before processing — this distinguishes a CC1 chord (multiple rapid chars) from a CCE (single char). For earlier phases, each keypress is processed immediately.

## Star & Points Calculation

Stars are calculated in `IProgressService.CalculateStars()`. With a `SpeedGoal`: 5★ requires ≥98% accuracy + ≥100% speed goal; scales down from there. Without a `SpeedGoal`: accuracy-only thresholds (98/95/90/85%). Points max at 100; Video lessons always award 10 points.

## Database

- PostgreSQL via Npgsql
- `jsonb` columns: `Lesson.Content`, `Lesson.TargetInputs`, `LessonAttempt.InputLog`, `Badge.Criteria`, `ChordEntry.InputKeys`
- Seed data applied via `HasData()` in migrations — files in `ChordCraft.Infrastructure/Data/Seed/`
- Migrations auto-apply in development on startup

## Testing

- **xUnit** for Core and API tests, **bUnit** for Blazor component tests
- API tests use `WebApplicationFactory` + `EF Core InMemory` provider
- Key scenarios to cover: star/points edge cases, chord buffer timeout, anonymous→auth migration, badge criteria evaluation, CCOS mapping completeness (all 90 inputs)
