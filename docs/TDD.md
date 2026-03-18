# ChordCraft — Technical Design Document

## 1. Introduction

### 1.1 Purpose

This document describes the technical architecture, system design, and implementation details for ChordCraft — a web-based typing tutor for CharaChorder One beginners. It translates the product requirements (see `docs/superpowers/specs/2026-03-18-chordcraft-design.md`) into an implementable technical blueprint.

### 1.2 Scope

ChordCraft is a full-stack web application consisting of:
- A Blazor WebAssembly (WASM) client for the browser-based typing experience
- A .NET 9 Web API backend for authenticated user data
- A PostgreSQL database for persistent storage
- LocalStorage for anonymous user progress

### 1.3 Conventions

- All code targets .NET 9 / C# 13
- Entity Framework Core 9 with code-first migrations
- MudBlazor component library for UI
- xUnit + bUnit for testing
- All dates stored as UTC `DateTime`
- All IDs use `Guid` for User entities, `int` for reference/seed data entities

---

## 2. System Architecture

### 2.1 High-Level Architecture

```
┌─────────────────────────────────────────────────────────┐
│                      Browser                             │
│  ┌───────────────────────────────────────────────────┐  │
│  │            Blazor WebAssembly Client               │  │
│  │  ┌─────────┐ ┌──────────┐ ┌───────────────────┐  │  │
│  │  │  Pages  │ │Components│ │    Services        │  │  │
│  │  │         │ │          │ │ ┌───────────────┐  │  │  │
│  │  │ Landing │ │ Switch   │ │ │ IProgressSvc  │  │  │  │
│  │  │ Catalog │ │ Diagram  │ │ │ (interface)   │  │  │  │
│  │  │ Typing  │ │ Hand     │ │ ├───────────────┤  │  │  │
│  │  │ Stats   │ │ Overlay  │ │ │LocalProgressSvc│ │  │  │
│  │  │ Badges  │ │ Lesson   │ │ │ApiProgressSvc │  │  │  │
│  │  │ Auth    │ │ Cards    │ │ └───────────────┘  │  │  │
│  │  └─────────┘ └──────────┘ └───────────────────┘  │  │
│  │                    │              │                │  │
│  │              JS Interop     LocalStorage           │  │
│  │           (keydown, audio)  (anon progress)        │  │
│  └───────────────────────────────────────────────────┘  │
│                          │ HTTP/JWT                       │
└──────────────────────────┼───────────────────────────────┘
                           │
┌──────────────────────────┼───────────────────────────────┐
│                   Railway Platform                        │
│  ┌───────────────────────┼───────────────────────────┐   │
│  │              .NET 9 Web API                        │   │
│  │  ┌──────────┐ ┌──────────┐ ┌──────────────────┐  │   │
│  │  │Controllers│ │ Services │ │   Identity       │  │   │
│  │  │ Auth     │ │ Lesson   │ │   JWT Auth       │  │   │
│  │  │ Lessons  │ │ Progress │ │   Password Hash  │  │   │
│  │  │ Progress │ │ Badge    │ └──────────────────┘  │   │
│  │  │ Badges   │ │ Stats    │                        │   │
│  │  │ Chords   │ │ Placement│                        │   │
│  │  └──────────┘ └──────────┘                        │   │
│  │                    │                               │   │
│  │              EF Core 9                             │   │
│  └────────────────────┼──────────────────────────────┘   │
│                       │                                   │
│  ┌────────────────────┼──────────────────────────────┐   │
│  │              PostgreSQL                            │   │
│  │  Users, Phases, Lessons, LessonAttempts,          │   │
│  │  UserProgress, Badges, UserBadges, ChordEntries   │   │
│  └───────────────────────────────────────────────────┘   │
└──────────────────────────────────────────────────────────┘
```

### 2.2 Key Architectural Decisions

| Decision | Choice | Rationale |
|----------|--------|-----------|
| Client rendering | Blazor WASM | Zero-latency keypress handling; typing apps cannot tolerate server round-trips |
| Anonymous storage | LocalStorage only | Avoids server-side anonymous session management; incentivizes signup |
| Auth strategy | JWT tokens | Stateless API auth; WASM client stores token in memory (not LocalStorage for security) |
| Progress service | Interface + 2 implementations | `LocalProgressService` for anon, `ApiProgressService` for authenticated; swapped on login |
| Device visualization | SVG Blazor components | Scalable, animatable, no external dependencies |
| Chord detection | Character buffer with timeout | CC1 firmware sends chord output as rapid keystrokes; 80ms timeout distinguishes chord from CCE |

### 2.3 Solution Structure

```
ChordCraft.sln
├── src/
│   ├── ChordCraft.Client/              # Blazor WASM (.NET 9)
│   ├── ChordCraft.Api/                 # ASP.NET Core Web API
│   ├── ChordCraft.Core/                # Shared domain: entities, DTOs, interfaces, enums
│   └── ChordCraft.Infrastructure/      # EF Core, Identity, repositories, seed data
├── tests/
│   ├── ChordCraft.Core.Tests/          # Domain logic unit tests
│   ├── ChordCraft.Api.Tests/           # API integration tests
│   └── ChordCraft.Client.Tests/        # bUnit component tests
└── docs/
```

---

## 3. Data Layer

### 3.1 Entity Definitions

#### User

```csharp
public class User
{
    public Guid Id { get; set; }
    public string? Email { get; set; }
    public string? DisplayName { get; set; }
    public string? SessionId { get; set; }       // For anonymous-to-auth migration
    public DateTime CreatedAt { get; set; }
    public DateTime LastActiveAt { get; set; }

    // Navigation
    public ICollection<LessonAttempt> Attempts { get; set; } = [];
    public ICollection<UserProgress> Progress { get; set; } = [];
    public ICollection<UserBadge> Badges { get; set; } = [];
}
```

> ASP.NET Core Identity manages `PasswordHash`, `NormalizedEmail`, etc. via `IdentityUser`. The `User` entity extends `IdentityUser<Guid>`.

#### Phase

```csharp
public class Phase
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Order { get; set; }

    public ICollection<Lesson> Lessons { get; set; } = [];
}
```

#### Lesson

```csharp
public enum LessonType { Intro, Review, Practice, Play, Video }

public class Lesson
{
    public int Id { get; set; }
    public int PhaseId { get; set; }
    public int Number { get; set; }              // Global display order (1-160+)
    public string Title { get; set; } = string.Empty;
    public LessonType Type { get; set; }
    public string Description { get; set; } = string.Empty;
    public string TargetInputs { get; set; } = "[]";   // JSON: SwitchInput[]
    public decimal PassAccuracyThreshold { get; set; } = 0.80m;
    public int? SpeedGoal { get; set; }                  // WPM, nullable
    public string Content { get; set; } = "{}";          // JSON: varies by type

    // Navigation
    public Phase Phase { get; set; } = null!;
    public ICollection<LessonAttempt> Attempts { get; set; } = [];
    public ICollection<UserProgress> UserProgress { get; set; } = [];
}
```

#### LessonAttempt

```csharp
public class LessonAttempt
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public int LessonId { get; set; }
    public DateTime StartedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public decimal Accuracy { get; set; }        // 0.00 to 1.00
    public decimal Speed { get; set; }           // WPM or inputs/min
    public int Stars { get; set; }               // 0-5
    public int Points { get; set; }
    public bool Passed { get; set; }
    public string? InputLog { get; set; }        // JSON: keystroke-level data

    // Navigation
    public User User { get; set; } = null!;
    public Lesson Lesson { get; set; } = null!;
}
```

#### UserProgress

```csharp
public class UserProgress
{
    public Guid UserId { get; set; }
    public int LessonId { get; set; }
    public int BestStars { get; set; }
    public decimal BestAccuracy { get; set; }
    public decimal BestSpeed { get; set; }
    public int TotalAttempts { get; set; }
    public DateTime? FirstCompletedAt { get; set; }
    public DateTime LastAttemptAt { get; set; }

    // Navigation
    public User User { get; set; } = null!;
    public Lesson Lesson { get; set; } = null!;
}
```

#### Badge & UserBadge

```csharp
public class Badge
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string IconUrl { get; set; } = string.Empty;
    public string Criteria { get; set; } = "{}";  // JSON: BadgeCriteria

    public ICollection<UserBadge> UserBadges { get; set; } = [];
}

public class UserBadge
{
    public Guid UserId { get; set; }
    public int BadgeId { get; set; }
    public DateTime EarnedAt { get; set; }

    public User User { get; set; } = null!;
    public Badge Badge { get; set; } = null!;
}
```

#### ChordEntry

```csharp
public class ChordEntry
{
    public int Id { get; set; }
    public string OutputText { get; set; } = string.Empty;  // e.g., "the"
    public string InputKeys { get; set; } = "[]";           // JSON: SwitchInput[]
    public int Difficulty { get; set; }                      // 1-5
    public string Category { get; set; } = string.Empty;    // "common-words", "phrases"
}
```

### 3.2 Shared Domain Types

```csharp
// Identifies a specific input on the CharaChorder One
public record SwitchInput(SwitchId Switch, SwitchDirection Direction);

public enum SwitchId
{
    L_Pinky, L_Ring, L_Middle, L_Index, L_Thumb, L_ThumbInner, L_PalmUpper, L_PalmLower, L_Edge,
    R_Pinky, R_Ring, R_Middle, R_Index, R_Thumb, R_ThumbInner, R_PalmUpper, R_PalmLower, R_Edge
}

public enum SwitchDirection { Push, Up, Down, Left, Right }
```

### 3.3 EF Core Configuration

**AppDbContext** inherits `IdentityDbContext<User, IdentityRole<Guid>, Guid>`.

Key configurations:
- `UserProgress` has composite PK: `(UserId, LessonId)`
- `UserBadge` has composite PK: `(UserId, BadgeId)`
- `Lesson.Content` and `Lesson.TargetInputs` are stored as `jsonb` columns in PostgreSQL
- `LessonAttempt.InputLog` is stored as `jsonb`
- `Badge.Criteria` is stored as `jsonb`
- `ChordEntry.InputKeys` is stored as `jsonb`
- Indexes on: `LessonAttempt.UserId`, `LessonAttempt.LessonId`, `UserProgress.UserId`

```csharp
protected override void OnModelCreating(ModelBuilder builder)
{
    base.OnModelCreating(builder);

    builder.Entity<UserProgress>()
        .HasKey(up => new { up.UserId, up.LessonId });

    builder.Entity<UserBadge>()
        .HasKey(ub => new { ub.UserId, ub.BadgeId });

    builder.Entity<Lesson>()
        .Property(l => l.Content).HasColumnType("jsonb");
    builder.Entity<Lesson>()
        .Property(l => l.TargetInputs).HasColumnType("jsonb");
    builder.Entity<LessonAttempt>()
        .Property(la => la.InputLog).HasColumnType("jsonb");
    builder.Entity<Badge>()
        .Property(b => b.Criteria).HasColumnType("jsonb");
    builder.Entity<ChordEntry>()
        .Property(ce => ce.InputKeys).HasColumnType("jsonb");

    builder.Entity<LessonAttempt>()
        .HasIndex(la => la.UserId);
    builder.Entity<LessonAttempt>()
        .HasIndex(la => la.LessonId);
    builder.Entity<UserProgress>()
        .HasIndex(up => up.UserId);
}
```

### 3.4 Seed Data Strategy

Seed data is loaded via `IEntityTypeConfiguration` classes in `ChordCraft.Infrastructure/Data/Seed/`:

| File | Contents |
|------|----------|
| `PhaseSeedData.cs` | 5 phases with order and descriptions |
| `LessonSeedData.cs` | All 160+ lessons with Content JSON per type |
| `BadgeSeedData.cs` | All 14 badges with criteria JSON |
| `ChordSeedData.cs` | Chord library (~100+ entries) with input keys |
| `CcosLayoutData.cs` | Static class (not seeded to DB) — the full CCOS mapping as a `Dictionary<SwitchInput, char>` used by client and API |

Seed data is applied via `HasData()` in EF Core migrations, ensuring deterministic database state.

---

## 4. API Layer

### 4.1 Project Setup

- ASP.NET Core Web API with controllers
- JWT Bearer authentication via ASP.NET Core Identity
- CORS configured for the Blazor WASM origin
- Swagger/OpenAPI for development

### 4.2 Authentication

**Registration flow:**
1. Client sends `POST /api/auth/register` with `{ email, password, displayName }`
2. API creates `IdentityUser` via `UserManager<User>`
3. API returns JWT access token + refresh token

**Login flow:**
1. Client sends `POST /api/auth/login` with `{ email, password }`
2. API validates via `SignInManager<User>`
3. API returns JWT access token + refresh token

**Migration flow:**
1. Client sends `POST /api/auth/migrate` with `{ sessionId, progress[] }` (bulk LocalStorage data)
2. API creates `UserProgress`, `LessonAttempt`, `UserBadge` records from the payload
3. Client clears LocalStorage on success

**JWT configuration:**
```csharp
services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetimeExpiry = true,
            ValidIssuer = config["Jwt:Issuer"],
            ValidAudience = config["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(config["Jwt:Key"]!))
        };
    });
```

Access tokens expire in 60 minutes. Refresh tokens stored server-side, expire in 30 days.

### 4.3 Controller Endpoints

#### AuthController

| Method | Route | Auth | Description |
|--------|-------|------|-------------|
| POST | `/api/auth/register` | None | Create account, return JWT |
| POST | `/api/auth/login` | None | Authenticate, return JWT |
| POST | `/api/auth/refresh` | None | Refresh expired access token |
| POST | `/api/auth/migrate` | JWT | Migrate anonymous progress to account |

#### LessonsController

| Method | Route | Auth | Description |
|--------|-------|------|-------------|
| GET | `/api/phases` | None | List all phases with lesson summaries (no Content field) |
| GET | `/api/lessons/{id}` | None | Get full lesson including Content JSON |
| GET | `/api/lessons/{id}/progress` | JWT | Get user's UserProgress for a lesson |

#### ProgressController

| Method | Route | Auth | Description |
|--------|-------|------|-------------|
| POST | `/api/attempts` | JWT | Submit lesson attempt; returns stars, points, new badges |
| GET | `/api/progress` | JWT | Get all UserProgress records for catalog display |
| GET | `/api/stats` | JWT | Get aggregated stats (charts data) |

#### BadgesController

| Method | Route | Auth | Description |
|--------|-------|------|-------------|
| GET | `/api/badges` | JWT | List all badges with earned status for current user |

#### ChordsController

| Method | Route | Auth | Description |
|--------|-------|------|-------------|
| GET | `/api/chords` | None | Get chord library, filterable by difficulty/category |

#### PlacementTestController

| Method | Route | Auth | Description |
|--------|-------|------|-------------|
| POST | `/api/placement-test` | JWT | Submit test results, bulk-unlock lessons |

### 4.4 Key DTOs

```csharp
// Lesson catalog display (no Content field)
public record LessonSummaryDto(
    int Id, int Number, string Title, LessonType Type,
    string Description, int? SpeedGoal);

public record PhaseDto(
    int Id, string Name, string Description, int Order,
    List<LessonSummaryDto> Lessons);

// Full lesson for the typing interface
public record LessonDetailDto(
    int Id, int Number, string Title, LessonType Type,
    string Description, string TargetInputs, decimal PassAccuracyThreshold,
    int? SpeedGoal, string Content);

// Submitting a lesson attempt
public record SubmitAttemptRequest(
    int LessonId, decimal Accuracy, decimal Speed,
    string? InputLog);

public record SubmitAttemptResponse(
    int Stars, int Points, bool Passed,
    List<BadgeDto>? NewBadges);

// Progress
public record UserProgressDto(
    int LessonId, int BestStars, decimal BestAccuracy,
    decimal BestSpeed, int TotalAttempts);

// Stats
public record StatsDto(
    decimal OverallAccuracy, decimal Coverage, decimal AverageSpeed,
    TimeSpan TotalPracticeTime,
    List<DailyStatsDto> DailyStats,
    List<SwitchAccuracyDto> SwitchAccuracies);

public record DailyStatsDto(
    DateOnly Date, TimeSpan PracticeTime, decimal Accuracy, decimal Speed);

public record SwitchAccuracyDto(
    string SwitchId, string Direction, decimal Accuracy, int TotalInputs);

// Migration
public record MigrateProgressRequest(
    string SessionId,
    List<UserProgressDto> Progress,
    List<MigrateLessonAttemptDto> Attempts);

public record MigrateLessonAttemptDto(
    int LessonId, DateTime StartedAt, DateTime? CompletedAt,
    decimal Accuracy, decimal Speed, int Stars, int Points, bool Passed);
```

### 4.5 Service Layer

Services contain business logic and are injected into controllers:

| Service | Responsibility |
|---------|---------------|
| `ILessonService` | Fetch lessons/phases, check unlock status |
| `IProgressService` | Record attempts, calculate stars/points, update UserProgress |
| `IBadgeService` | Evaluate badge criteria after each attempt, award new badges |
| `IStatsService` | Aggregate attempt data for stats page |
| `IPlacementTestService` | Evaluate test results, bulk-create progress records |
| `IChordService` | Query chord library |

**Star calculation logic** (in `IProgressService`):

```csharp
public int CalculateStars(decimal accuracy, decimal speed, Lesson lesson)
{
    if (accuracy < lesson.PassAccuracyThreshold) return 0;

    if (lesson.SpeedGoal.HasValue)
    {
        var goal = lesson.SpeedGoal.Value;
        if (accuracy >= 0.98m && speed >= goal) return 5;
        if (accuracy >= 0.95m && speed >= goal * 0.85m) return 4;
        if (accuracy >= 0.90m && speed >= goal * 0.70m) return 3;
        if (accuracy >= 0.85m && speed >= goal * 0.50m) return 2;
        return 1;
    }

    if (accuracy >= 0.98m) return 5;
    if (accuracy >= 0.95m) return 4;
    if (accuracy >= 0.90m) return 3;
    if (accuracy >= 0.85m) return 2;
    return 1;
}
```

**Points calculation logic:**

```csharp
public int CalculatePoints(int stars, decimal accuracy, decimal speed, Lesson lesson)
{
    if (stars == 0) return 0;
    if (lesson.Type == LessonType.Video) return 10;

    var points = stars * 10 + (accuracy * 100 - 80) * 2;

    if (lesson.SpeedGoal.HasValue && lesson.SpeedGoal.Value > 0)
    {
        var speedRatio = Math.Min(speed, lesson.SpeedGoal.Value) / lesson.SpeedGoal.Value;
        points += speedRatio * 20;
    }

    return Math.Min((int)Math.Round(points), 100);
}
```

### 4.6 Badge Evaluation

After each `POST /api/attempts`, the `BadgeService` runs all badge criteria checks:

```csharp
public record BadgeCriteria(string Type, JsonElement? Params);

// Criteria types and their evaluation:
// "first-lesson"    → TotalAttempts across all lessons >= 1 with Passed
// "phase-complete"  → All lessons in specified phase have BestStars >= 1
// "speed-threshold" → Any attempt with Speed >= threshold
// "first-chord"     → Any chord lesson attempt with Passed
// "chord-count"     → Count of distinct chord lessons passed >= threshold
// "streak"          → Consecutive calendar days with at least 1 passed attempt >= days
// "weekend"         → Passed attempts exist on both Saturday and Sunday of the same week
// "session-time"    → Single session (attempt StartedAt to CompletedAt) >= minutes
// "star-count"      → Count of lessons with BestStars == 5 >= threshold
// "full-layout"     → All 90 CCOS inputs have accuracy >= 70% in InputLog data
// "daily-stars"     → Count of distinct lessons with 5 stars earned today >= threshold
// "attempt-count"   → Total attempts across all lessons >= threshold
```

---

## 5. Client Layer (Blazor WASM)

### 5.1 Project Setup

- Blazor WebAssembly standalone app (not ASP.NET Core hosted)
- MudBlazor NuGet package for UI components
- `HttpClient` configured with base URL pointing to the API
- JWT token stored in memory (not LocalStorage — XSS protection)
- `AuthenticationStateProvider` custom implementation for JWT

### 5.2 Service Architecture

The client uses an `IProgressService` interface with two implementations, swapped based on auth state:

```csharp
public interface IClientProgressService
{
    Task<List<UserProgressDto>> GetAllProgressAsync();
    Task<SubmitAttemptResponse> SubmitAttemptAsync(SubmitAttemptRequest request);
    Task<StatsDto?> GetStatsAsync();
    Task<List<BadgeDto>> GetBadgesAsync();
    Task<bool> IsLessonUnlockedAsync(int lessonNumber, int phaseId);
}

// Anonymous: reads/writes LocalStorage JSON
public class LocalProgressService : IClientProgressService { ... }

// Authenticated: calls API via HttpClient
public class ApiProgressService : IClientProgressService { ... }
```

On login/signup, `LocalProgressService` data is migrated to the API, then the DI container switches to `ApiProgressService`.

### 5.3 Page Components

| Page | Route | Component | Description |
|------|-------|-----------|-------------|
| Landing | `/` | `Landing.razor` | Marketing page, Get Started CTA |
| Lesson Catalog | `/lessons` | `LessonCatalog.razor` | Grid of lesson cards by phase |
| Typing Interface | `/lessons/{id:int}` | `TypingInterface.razor` | Core lesson experience |
| Stats | `/stats` | `StatsPage.razor` | Charts and heatmap |
| Badges | `/badges` | `BadgesPage.razor` | Earned/un-earned grid |
| Login | `/login` | `LoginPage.razor` | Email/password form |
| Register | `/register` | `RegisterPage.razor` | Signup form |

### 5.4 Key Components

#### SwitchDiagram.razor

Interactive SVG rendering of the 18 CharaChorder One switches.

**Parameters:**
- `ActiveInputs: List<SwitchInput>` — which switches/directions to highlight
- `ChordMode: bool` — when true, highlights multiple switches without popup
- `OnSwitchClick: EventCallback<SwitchId>` — optional click handler

**Rendering approach:**
- Each switch is an SVG `<circle>` with CSS class toggling for active/inactive states
- Positions defined in a static `SwitchLayout` dictionary mapping `SwitchId` → `(x, y)` coordinates
- The direction popup is a Blazor `<g>` element positioned relative to the active switch, showing 5 labels in a compass rose pattern
- CSS transitions handle the glow animation (`transition: fill 0.2s, filter 0.3s`)

#### HandOverlay.razor

SVG hand silhouettes positioned over the switch diagram.

**Parameters:**
- `ActiveFingers: List<SwitchId>` — which fingers to highlight
- `ActiveDirection: SwitchDirection?` — directional arrow on active finger

**Rendering approach:**
- Two SVG `<path>` groups (left hand, right hand) pre-designed as stylized hand outlines
- Each finger segment is a separate `<path>` with an ID matching its `SwitchId`
- Active finger: opacity transitions from 0.3 to 1.0, fill changes to highlight color
- Direction arrow: small SVG arrow element rotated based on `SwitchDirection`

#### TypingEngine.razor (headless component)

Manages the typing interaction logic — no visual output, pure state management.

**State:**
- `CurrentLesson: LessonDetailDto`
- `CurrentPosition: int` — cursor index in the target sequence
- `TypedCharacters: List<TypedChar>` — record of each keypress (char, correct, timestamp)
- `StartTime: DateTime?`
- `CharacterBuffer: List<(char Char, long TimestampMs)>` — for chord detection
- `BufferFlushTimer: Timer` — fires after `ChordBufferTimeoutMs` (80ms)

**Key events via JS Interop:**

```csharp
// wwwroot/js/inputCapture.js
window.ChordCraft = {
    registerKeyHandler: function (dotNetRef) {
        document.addEventListener('keydown', (e) => {
            e.preventDefault();
            dotNetRef.invokeMethodAsync('OnKeyDown', e.key, performance.now());
        });
    }
};
```

```csharp
[JSInvokable]
public async Task OnKeyDown(string key, double timestampMs)
{
    if (_currentLesson.Type is LessonType.Practice or LessonType.Play
        && _currentLesson.PhaseId >= 4) // Chord phases
    {
        // Buffer mode: collect chars, flush after timeout
        _characterBuffer.Add((key[0], (long)timestampMs));
        _bufferFlushTimer.Stop();
        _bufferFlushTimer.Interval = ChordBufferTimeoutMs;
        _bufferFlushTimer.Start();
    }
    else
    {
        // CCE mode: process immediately
        ProcessSingleInput(key);
    }
}

private void OnBufferFlush()
{
    var buffered = string.Concat(_characterBuffer.Select(b => b.Char));
    _characterBuffer.Clear();
    ProcessChordOrCCE(buffered);
}
```

#### LessonResults.razor

Post-lesson overlay showing performance.

**Parameters:**
- `Stars: int`, `Accuracy: decimal`, `Speed: decimal`, `Points: int`
- `OnNextLesson: EventCallback`, `OnRetry: EventCallback`

**Star animation:** Stars fill in sequentially with a scale-up CSS animation (`@keyframes starPop`), 300ms delay between each star.

### 5.5 Audio System

Sound effects handled via JS interop with the Web Audio API:

```javascript
// wwwroot/js/audio.js
window.ChordCraftAudio = {
    _ctx: null,
    _buffers: {},

    init: async function () {
        this._ctx = new AudioContext();
        const sounds = ['keypress', 'error', 'success', 'star', 'badge'];
        for (const name of sounds) {
            const resp = await fetch(`/audio/${name}.mp3`);
            const buffer = await resp.arrayBuffer();
            this._buffers[name] = await this._ctx.decodeAudioData(buffer);
        }
    },

    play: function (name) {
        if (!this._ctx || !this._buffers[name]) return;
        const source = this._ctx.createBufferSource();
        source.buffer = this._buffers[name];
        source.connect(this._ctx.destination);
        source.start(0);
    }
};
```

Blazor interop:

```csharp
public class AudioService
{
    private readonly IJSRuntime _js;
    private bool _enabled = true;

    public async Task InitAsync() => await _js.InvokeVoidAsync("ChordCraftAudio.init");
    public async Task PlayAsync(string sound)
    {
        if (_enabled) await _js.InvokeVoidAsync("ChordCraftAudio.play", sound);
    }
    public void Toggle() => _enabled = !_enabled;
}
```

### 5.6 CCOS Layout Data (Client-Side)

A static class available in `ChordCraft.Core` provides the full CCOS mapping:

```csharp
public static class CcosLayout
{
    // Maps each SwitchInput to its output character
    public static readonly Dictionary<SwitchInput, string> Map = new()
    {
        [new(SwitchId.L_Pinky, SwitchDirection.Push)] = "a",
        [new(SwitchId.L_Pinky, SwitchDirection.Up)] = "q",
        [new(SwitchId.L_Pinky, SwitchDirection.Down)] = "z",
        // ... all 90 entries
    };

    // Reverse lookup: character → SwitchInput
    public static readonly Dictionary<string, SwitchInput> ReverseMap =
        Map.ToDictionary(kvp => kvp.Value, kvp => kvp.Key);
}
```

This is used by:
- `SwitchDiagram.razor` to display character labels on switches
- `TypingEngine` to map a target character to the correct switch+direction for highlighting
- `HandOverlay` to determine which finger to highlight

### 5.7 LocalStorage Schema

For anonymous users, progress is stored as JSON in LocalStorage under the key `chordcraft_progress`:

```json
{
  "sessionId": "uuid-string",
  "progress": {
    "1": { "bestStars": 3, "bestAccuracy": 0.92, "bestSpeed": 0, "totalAttempts": 2 },
    "2": { "bestStars": 5, "bestAccuracy": 0.99, "bestSpeed": 12.5, "totalAttempts": 4 }
  },
  "attempts": [
    { "lessonId": 1, "startedAt": "ISO-date", "accuracy": 0.92, "speed": 0, "stars": 3, "points": 34, "passed": true }
  ],
  "badges": [1, 14],
  "lastActiveAt": "ISO-date"
}
```

---

## 6. Deployment

### 6.1 Railway Configuration

**Two Railway services:**

1. **chordcraft-api** — .NET 9 Web API
   - Dockerfile-based deployment
   - Environment variables: `ConnectionStrings__DefaultConnection`, `Jwt__Key`, `Jwt__Issuer`, `Jwt__Audience`
   - Exposes HTTP port 8080

2. **chordcraft-client** — Blazor WASM static files
   - Served via a lightweight static file server (e.g., `dotnet serve` or nginx container)
   - OR: served as static files from the API project's wwwroot (single-service deployment)
   - Environment variable: `API_BASE_URL` baked into `appsettings.json` at build time

**Single-service alternative (recommended for simplicity):**
- The API project serves the Blazor WASM files from its wwwroot
- One Railway service, one URL
- Configured via `app.UseBlazorFrameworkFiles()` and `app.MapFallbackToFile("index.html")`

3. **PostgreSQL** — Railway managed PostgreSQL plugin

### 6.2 Dockerfile

```dockerfile
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ChordCraft.sln .
COPY src/ChordCraft.Core/*.csproj src/ChordCraft.Core/
COPY src/ChordCraft.Infrastructure/*.csproj src/ChordCraft.Infrastructure/
COPY src/ChordCraft.Api/*.csproj src/ChordCraft.Api/
COPY src/ChordCraft.Client/*.csproj src/ChordCraft.Client/
RUN dotnet restore

COPY . .
RUN dotnet publish src/ChordCraft.Api -c Release -o /app
RUN dotnet publish src/ChordCraft.Client -c Release -o /client

# Copy WASM output into API's wwwroot
RUN cp -r /client/wwwroot/* /app/wwwroot/

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app .
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "ChordCraft.Api.dll"]
```

### 6.3 Database Migrations

Migrations run automatically on startup in development. In production, migrations are applied via a startup check:

```csharp
// Program.cs
if (app.Environment.IsProduction())
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await db.Database.MigrateAsync();
}
```

---

## 7. Security Considerations

| Concern | Mitigation |
|---------|-----------|
| XSS via JWT | JWT stored in memory, not LocalStorage. Refresh via httpOnly cookie if needed. |
| CSRF | Not applicable — API uses Bearer tokens, not cookies |
| Input validation | All DTOs validated with data annotations; `InputLog` JSON size capped at 1MB |
| Rate limiting | `POST /api/attempts` rate-limited to 60 requests/minute per user via ASP.NET Core rate limiting middleware |
| SQL injection | Mitigated by EF Core parameterized queries |
| CORS | Restricted to the Blazor WASM origin in production |

---

## 8. Performance Considerations

| Area | Approach |
|------|----------|
| WASM load time | Enable Brotli compression, lazy-load non-critical assemblies |
| Typing latency | All input processing in WASM (client-side), no API calls during active typing |
| Lesson catalog | Phases/lessons fetched once on catalog load, cached in memory |
| Stats queries | Aggregated via efficient SQL queries with proper indexes |
| SVG rendering | Static SVG paths, only dynamic attributes are CSS classes (no DOM thrashing) |
| Audio | Sounds pre-loaded on first interaction (AudioContext requires user gesture) |

---

## 9. Testing Strategy

| Layer | Framework | Scope |
|-------|-----------|-------|
| Core domain logic | xUnit | Star calculation, points calculation, unlock logic, CCOS mapping |
| API integration | xUnit + WebApplicationFactory | Controller endpoints, auth flow, badge evaluation |
| Blazor components | bUnit | SwitchDiagram rendering, TypingEngine state, LessonResults display |
| E2E (future) | Playwright | Full lesson flow from catalog to completion |

**Key test scenarios:**
- Star calculation with and without SpeedGoal
- Points formula edge cases (null SpeedGoal, Video type, boundary accuracy)
- Unlock logic across phases
- Chord buffer timeout distinguishing chord vs CCE
- Anonymous → authenticated migration preserving all progress
- Badge criteria evaluation for each badge type
- CCOS mapping completeness (all 90 inputs mapped)
