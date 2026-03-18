# ChordCraft Foundation Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Build the .NET solution scaffold, domain models, EF Core data layer, seed data, and all API endpoints — producing a fully functional backend that can be tested via Swagger.

**Architecture:** A 4-project .NET 9 solution: Core (domain models, interfaces), Infrastructure (EF Core, Identity, repositories), Api (controllers, services, JWT auth), and Client (placeholder Blazor WASM). The API serves as the single deployment unit — Blazor WASM files will be served from its wwwroot.

**Tech Stack:** .NET 9, ASP.NET Core Web API, EF Core 9, PostgreSQL, ASP.NET Core Identity, JWT Bearer auth, xUnit, MudBlazor (placeholder only in this plan)

**Reference docs:**
- PRD: `docs/superpowers/specs/2026-03-18-chordcraft-design.md`
- TDD: `docs/TDD.md`

---

## File Structure

```
C:\Developer\chordcraft\
├── src\
│   ├── ChordCraft.Core\
│   │   ├── ChordCraft.Core.csproj
│   │   ├── Entities\
│   │   │   ├── User.cs
│   │   │   ├── Phase.cs
│   │   │   ├── Lesson.cs
│   │   │   ├── LessonAttempt.cs
│   │   │   ├── UserProgress.cs
│   │   │   ├── Badge.cs
│   │   │   ├── UserBadge.cs
│   │   │   └── ChordEntry.cs
│   │   ├── Enums\
│   │   │   ├── LessonType.cs
│   │   │   ├── SwitchId.cs
│   │   │   └── SwitchDirection.cs
│   │   ├── Models\
│   │   │   ├── SwitchInput.cs
│   │   │   └── CcosLayout.cs
│   │   ├── DTOs\
│   │   │   ├── Auth\
│   │   │   │   ├── RegisterRequest.cs
│   │   │   │   ├── LoginRequest.cs
│   │   │   │   ├── AuthResponse.cs
│   │   │   │   └── MigrateProgressRequest.cs
│   │   │   ├── Lessons\
│   │   │   │   ├── PhaseDto.cs
│   │   │   │   ├── LessonSummaryDto.cs
│   │   │   │   └── LessonDetailDto.cs
│   │   │   ├── Progress\
│   │   │   │   ├── SubmitAttemptRequest.cs
│   │   │   │   ├── SubmitAttemptResponse.cs
│   │   │   │   ├── UserProgressDto.cs
│   │   │   │   └── StatsDto.cs
│   │   │   └── Badges\
│   │   │       └── BadgeDto.cs
│   │   └── Interfaces\
│   │       ├── ILessonService.cs
│   │       ├── IProgressService.cs
│   │       ├── IBadgeService.cs
│   │       ├── IStatsService.cs
│   │       └── IChordService.cs
│   ├── ChordCraft.Infrastructure\
│   │   ├── ChordCraft.Infrastructure.csproj
│   │   ├── Data\
│   │   │   ├── AppDbContext.cs
│   │   │   ├── Migrations\           (auto-generated)
│   │   │   └── Seed\
│   │   │       ├── PhaseSeedData.cs
│   │   │       ├── LessonSeedData.cs
│   │   │       ├── BadgeSeedData.cs
│   │   │       └── ChordSeedData.cs
│   │   └── Services\
│   │       ├── LessonService.cs
│   │       ├── ProgressService.cs
│   │       ├── BadgeService.cs
│   │       ├── StatsService.cs
│   │       ├── ChordService.cs
│   │       └── TokenService.cs
│   ├── ChordCraft.Api\
│   │   ├── ChordCraft.Api.csproj
│   │   ├── Program.cs
│   │   ├── appsettings.json
│   │   ├── appsettings.Development.json
│   │   └── Controllers\
│   │       ├── AuthController.cs
│   │       ├── LessonsController.cs
│   │       ├── ProgressController.cs
│   │       ├── BadgesController.cs
│   │       ├── ChordsController.cs
│   │       └── PlacementTestController.cs
│   └── ChordCraft.Client\
│       ├── ChordCraft.Client.csproj
│       ├── Program.cs
│       ├── App.razor
│       ├── Routes.razor
│       ├── _Imports.razor
│       ├── Layout\
│       │   └── MainLayout.razor
│       └── wwwroot\
│           └── index.html
├── tests\
│   ├── ChordCraft.Core.Tests\
│   │   ├── ChordCraft.Core.Tests.csproj
│   │   ├── CcosLayoutTests.cs
│   │   └── ScoringTests.cs
│   └── ChordCraft.Api.Tests\
│       ├── ChordCraft.Api.Tests.csproj
│       ├── AuthControllerTests.cs
│       ├── LessonsControllerTests.cs
│       ├── ProgressControllerTests.cs
│       └── BadgesControllerTests.cs
├── ChordCraft.sln
├── .gitignore
└── Dockerfile
```

---

### Task 1: Solution Scaffold

**Files:**
- Create: `ChordCraft.sln`
- Create: `src/ChordCraft.Core/ChordCraft.Core.csproj`
- Create: `src/ChordCraft.Infrastructure/ChordCraft.Infrastructure.csproj`
- Create: `src/ChordCraft.Api/ChordCraft.Api.csproj`
- Create: `src/ChordCraft.Client/ChordCraft.Client.csproj`
- Create: `tests/ChordCraft.Core.Tests/ChordCraft.Core.Tests.csproj`
- Create: `tests/ChordCraft.Api.Tests/ChordCraft.Api.Tests.csproj`
- Create: `.gitignore`

- [ ] **Step 1: Create the solution and projects**

```bash
cd C:/Developer/chordcraft

# Create solution
dotnet new sln -n ChordCraft

# Create projects
dotnet new classlib -n ChordCraft.Core -o src/ChordCraft.Core -f net9.0
dotnet new classlib -n ChordCraft.Infrastructure -o src/ChordCraft.Infrastructure -f net9.0
dotnet new webapi -n ChordCraft.Api -o src/ChordCraft.Api -f net9.0 --no-openapi false
dotnet new blazorwasm -n ChordCraft.Client -o src/ChordCraft.Client -f net9.0 --empty

# Create test projects
dotnet new xunit -n ChordCraft.Core.Tests -o tests/ChordCraft.Core.Tests -f net9.0
dotnet new xunit -n ChordCraft.Api.Tests -o tests/ChordCraft.Api.Tests -f net9.0

# Add projects to solution
dotnet sln add src/ChordCraft.Core
dotnet sln add src/ChordCraft.Infrastructure
dotnet sln add src/ChordCraft.Api
dotnet sln add src/ChordCraft.Client
dotnet sln add tests/ChordCraft.Core.Tests
dotnet sln add tests/ChordCraft.Api.Tests
```

- [ ] **Step 2: Add project references**

```bash
cd C:/Developer/chordcraft

# Infrastructure depends on Core
dotnet add src/ChordCraft.Infrastructure reference src/ChordCraft.Core

# Api depends on Core and Infrastructure
dotnet add src/ChordCraft.Api reference src/ChordCraft.Core
dotnet add src/ChordCraft.Api reference src/ChordCraft.Infrastructure

# Api hosts the Client (for serving WASM from wwwroot)
dotnet add src/ChordCraft.Api reference src/ChordCraft.Client

# Test project references
dotnet add tests/ChordCraft.Core.Tests reference src/ChordCraft.Core
dotnet add tests/ChordCraft.Api.Tests reference src/ChordCraft.Api
dotnet add tests/ChordCraft.Api.Tests reference src/ChordCraft.Infrastructure
dotnet add tests/ChordCraft.Api.Tests reference src/ChordCraft.Core
```

- [ ] **Step 3: Add NuGet packages**

```bash
cd C:/Developer/chordcraft

# Core: no external dependencies (pure domain)

# Infrastructure
dotnet add src/ChordCraft.Infrastructure package Microsoft.EntityFrameworkCore -v 9.*
dotnet add src/ChordCraft.Infrastructure package Npgsql.EntityFrameworkCore.PostgreSQL -v 9.*
dotnet add src/ChordCraft.Infrastructure package Microsoft.AspNetCore.Identity.EntityFrameworkCore -v 9.*

# Api
dotnet add src/ChordCraft.Api package Microsoft.AspNetCore.Authentication.JwtBearer -v 9.*
dotnet add src/ChordCraft.Api package Microsoft.EntityFrameworkCore.Design -v 9.*
dotnet add src/ChordCraft.Api package Swashbuckle.AspNetCore

# Client
dotnet add src/ChordCraft.Client package MudBlazor

# Test packages
dotnet add tests/ChordCraft.Api.Tests package Microsoft.AspNetCore.Mvc.Testing
dotnet add tests/ChordCraft.Api.Tests package Microsoft.EntityFrameworkCore.InMemory -v 9.*
```

- [ ] **Step 4: Add .gitignore**

Create `.gitignore` at `C:\Developer\chordcraft\.gitignore`:

```
bin/
obj/
.vs/
*.user
*.suo
appsettings.Development.json
.superpowers/
```

- [ ] **Step 5: Build to verify scaffold**

```bash
cd C:/Developer/chordcraft
dotnet build
```

Expected: Build succeeded with 0 errors.

- [ ] **Step 6: Commit**

```bash
cd C:/Developer/chordcraft
git add -A
git commit -m "feat: scaffold .NET 9 solution with 4 projects and 2 test projects"
```

---

### Task 2: Core Domain — Enums and Models

**Files:**
- Create: `src/ChordCraft.Core/Enums/LessonType.cs`
- Create: `src/ChordCraft.Core/Enums/SwitchId.cs`
- Create: `src/ChordCraft.Core/Enums/SwitchDirection.cs`
- Create: `src/ChordCraft.Core/Models/SwitchInput.cs`
- Delete: `src/ChordCraft.Core/Class1.cs` (template file)

- [ ] **Step 1: Create enums**

`src/ChordCraft.Core/Enums/LessonType.cs`:
```csharp
namespace ChordCraft.Core.Enums;

public enum LessonType
{
    Intro,
    Review,
    Practice,
    Play,
    Video
}
```

`src/ChordCraft.Core/Enums/SwitchId.cs`:
```csharp
namespace ChordCraft.Core.Enums;

public enum SwitchId
{
    L_Pinky, L_Ring, L_Middle, L_Index, L_Thumb, L_ThumbInner, L_PalmUpper, L_PalmLower, L_Edge,
    R_Pinky, R_Ring, R_Middle, R_Index, R_Thumb, R_ThumbInner, R_PalmUpper, R_PalmLower, R_Edge
}
```

`src/ChordCraft.Core/Enums/SwitchDirection.cs`:
```csharp
namespace ChordCraft.Core.Enums;

public enum SwitchDirection
{
    Push,
    Up,
    Down,
    Left,
    Right
}
```

- [ ] **Step 2: Create SwitchInput record**

`src/ChordCraft.Core/Models/SwitchInput.cs`:
```csharp
using ChordCraft.Core.Enums;

namespace ChordCraft.Core.Models;

public record SwitchInput(SwitchId Switch, SwitchDirection Direction);
```

- [ ] **Step 3: Delete template file and build**

```bash
cd C:/Developer/chordcraft
rm src/ChordCraft.Core/Class1.cs
dotnet build src/ChordCraft.Core
```

Expected: Build succeeded.

- [ ] **Step 4: Commit**

```bash
cd C:/Developer/chordcraft
git add src/ChordCraft.Core
git commit -m "feat: add core domain enums and SwitchInput model"
```

---

### Task 3: Core Domain — CCOS Layout

**Files:**
- Create: `src/ChordCraft.Core/Models/CcosLayout.cs`
- Create: `tests/ChordCraft.Core.Tests/CcosLayoutTests.cs`
- Delete: `tests/ChordCraft.Core.Tests/UnitTest1.cs` (template file)

- [ ] **Step 1: Write failing tests**

`tests/ChordCraft.Core.Tests/CcosLayoutTests.cs`:
```csharp
using ChordCraft.Core.Enums;
using ChordCraft.Core.Models;

namespace ChordCraft.Core.Tests;

public class CcosLayoutTests
{
    [Fact]
    public void Map_Contains_90_Entries()
    {
        Assert.Equal(90, CcosLayout.Map.Count);
    }

    [Fact]
    public void Map_Contains_All_18_Switches()
    {
        var switches = CcosLayout.Map.Keys.Select(k => k.Switch).Distinct().ToList();
        Assert.Equal(18, switches.Count);
        foreach (SwitchId s in Enum.GetValues<SwitchId>())
            Assert.Contains(s, switches);
    }

    [Fact]
    public void Map_Contains_All_5_Directions_Per_Switch()
    {
        foreach (SwitchId s in Enum.GetValues<SwitchId>())
        {
            var directions = CcosLayout.Map.Keys
                .Where(k => k.Switch == s)
                .Select(k => k.Direction)
                .ToList();
            Assert.Equal(5, directions.Count);
            foreach (SwitchDirection d in Enum.GetValues<SwitchDirection>())
                Assert.Contains(d, directions);
        }
    }

    [Theory]
    [InlineData(SwitchId.L_Index, SwitchDirection.Push, "f")]
    [InlineData(SwitchId.L_Index, SwitchDirection.Up, "r")]
    [InlineData(SwitchId.L_Index, SwitchDirection.Down, "v")]
    [InlineData(SwitchId.L_Index, SwitchDirection.Left, "g")]
    [InlineData(SwitchId.L_Index, SwitchDirection.Right, "t")]
    [InlineData(SwitchId.R_Index, SwitchDirection.Push, "j")]
    [InlineData(SwitchId.L_Thumb, SwitchDirection.Push, " ")]
    [InlineData(SwitchId.L_Pinky, SwitchDirection.Push, "a")]
    public void Map_Returns_Correct_Character(SwitchId sw, SwitchDirection dir, string expected)
    {
        Assert.Equal(expected, CcosLayout.Map[new SwitchInput(sw, dir)]);
    }

    [Fact]
    public void ReverseMap_Returns_Correct_SwitchInput()
    {
        var input = CcosLayout.ReverseMap["f"];
        Assert.Equal(SwitchId.L_Index, input.Switch);
        Assert.Equal(SwitchDirection.Push, input.Direction);
    }

    [Fact]
    public void All_Map_Values_Are_NonEmpty()
    {
        foreach (var kvp in CcosLayout.Map)
            Assert.False(string.IsNullOrEmpty(kvp.Value),
                $"Empty value for {kvp.Key.Switch} {kvp.Key.Direction}");
    }
}
```

- [ ] **Step 2: Run tests to verify they fail**

```bash
cd C:/Developer/chordcraft
rm tests/ChordCraft.Core.Tests/UnitTest1.cs
dotnet test tests/ChordCraft.Core.Tests --verbosity quiet
```

Expected: FAIL — `CcosLayout` does not exist.

- [ ] **Step 3: Implement CcosLayout**

`src/ChordCraft.Core/Models/CcosLayout.cs`:
```csharp
using ChordCraft.Core.Enums;

namespace ChordCraft.Core.Models;

public static class CcosLayout
{
    public static readonly Dictionary<SwitchInput, string> Map = new()
    {
        // Left Pinky
        [new(SwitchId.L_Pinky, SwitchDirection.Push)] = "a",
        [new(SwitchId.L_Pinky, SwitchDirection.Up)] = "q",
        [new(SwitchId.L_Pinky, SwitchDirection.Down)] = "z",
        [new(SwitchId.L_Pinky, SwitchDirection.Left)] = "1",
        [new(SwitchId.L_Pinky, SwitchDirection.Right)] = "!",
        // Left Ring
        [new(SwitchId.L_Ring, SwitchDirection.Push)] = "s",
        [new(SwitchId.L_Ring, SwitchDirection.Up)] = "w",
        [new(SwitchId.L_Ring, SwitchDirection.Down)] = "x",
        [new(SwitchId.L_Ring, SwitchDirection.Left)] = "2",
        [new(SwitchId.L_Ring, SwitchDirection.Right)] = "@",
        // Left Middle
        [new(SwitchId.L_Middle, SwitchDirection.Push)] = "d",
        [new(SwitchId.L_Middle, SwitchDirection.Up)] = "e",
        [new(SwitchId.L_Middle, SwitchDirection.Down)] = "c",
        [new(SwitchId.L_Middle, SwitchDirection.Left)] = "3",
        [new(SwitchId.L_Middle, SwitchDirection.Right)] = "#",
        // Left Index
        [new(SwitchId.L_Index, SwitchDirection.Push)] = "f",
        [new(SwitchId.L_Index, SwitchDirection.Up)] = "r",
        [new(SwitchId.L_Index, SwitchDirection.Down)] = "v",
        [new(SwitchId.L_Index, SwitchDirection.Left)] = "g",
        [new(SwitchId.L_Index, SwitchDirection.Right)] = "t",
        // Left Thumb
        [new(SwitchId.L_Thumb, SwitchDirection.Push)] = " ",
        [new(SwitchId.L_Thumb, SwitchDirection.Up)] = "\b",
        [new(SwitchId.L_Thumb, SwitchDirection.Down)] = "\t",
        [new(SwitchId.L_Thumb, SwitchDirection.Left)] = "\x7f",
        [new(SwitchId.L_Thumb, SwitchDirection.Right)] = "\n",
        // Left ThumbInner
        [new(SwitchId.L_ThumbInner, SwitchDirection.Push)] = "SHIFT",
        [new(SwitchId.L_ThumbInner, SwitchDirection.Up)] = "CTRL",
        [new(SwitchId.L_ThumbInner, SwitchDirection.Down)] = "ALT",
        [new(SwitchId.L_ThumbInner, SwitchDirection.Left)] = "GUI",
        [new(SwitchId.L_ThumbInner, SwitchDirection.Right)] = "ESC",
        // Left PalmUpper
        [new(SwitchId.L_PalmUpper, SwitchDirection.Push)] = "(",
        [new(SwitchId.L_PalmUpper, SwitchDirection.Up)] = "[",
        [new(SwitchId.L_PalmUpper, SwitchDirection.Down)] = "{",
        [new(SwitchId.L_PalmUpper, SwitchDirection.Left)] = "<",
        [new(SwitchId.L_PalmUpper, SwitchDirection.Right)] = "/",
        // Left PalmLower
        [new(SwitchId.L_PalmLower, SwitchDirection.Push)] = ")",
        [new(SwitchId.L_PalmLower, SwitchDirection.Up)] = "]",
        [new(SwitchId.L_PalmLower, SwitchDirection.Down)] = "}",
        [new(SwitchId.L_PalmLower, SwitchDirection.Left)] = ">",
        [new(SwitchId.L_PalmLower, SwitchDirection.Right)] = "\\",
        // Left Edge
        [new(SwitchId.L_Edge, SwitchDirection.Push)] = "-",
        [new(SwitchId.L_Edge, SwitchDirection.Up)] = "_",
        [new(SwitchId.L_Edge, SwitchDirection.Down)] = "=",
        [new(SwitchId.L_Edge, SwitchDirection.Left)] = "+",
        [new(SwitchId.L_Edge, SwitchDirection.Right)] = "`",
        // Right Thumb
        [new(SwitchId.R_Thumb, SwitchDirection.Push)] = " ",
        [new(SwitchId.R_Thumb, SwitchDirection.Up)] = "\b",
        [new(SwitchId.R_Thumb, SwitchDirection.Down)] = "\t",
        [new(SwitchId.R_Thumb, SwitchDirection.Left)] = "\n",
        [new(SwitchId.R_Thumb, SwitchDirection.Right)] = "\x7f",
        // Right ThumbInner
        [new(SwitchId.R_ThumbInner, SwitchDirection.Push)] = "SHIFT",
        [new(SwitchId.R_ThumbInner, SwitchDirection.Up)] = "CTRL",
        [new(SwitchId.R_ThumbInner, SwitchDirection.Down)] = "ALT",
        [new(SwitchId.R_ThumbInner, SwitchDirection.Left)] = "ESC",
        [new(SwitchId.R_ThumbInner, SwitchDirection.Right)] = "GUI",
        // Right Index
        [new(SwitchId.R_Index, SwitchDirection.Push)] = "j",
        [new(SwitchId.R_Index, SwitchDirection.Up)] = "u",
        [new(SwitchId.R_Index, SwitchDirection.Down)] = "m",
        [new(SwitchId.R_Index, SwitchDirection.Left)] = "h",
        [new(SwitchId.R_Index, SwitchDirection.Right)] = "y",
        // Right Middle
        [new(SwitchId.R_Middle, SwitchDirection.Push)] = "k",
        [new(SwitchId.R_Middle, SwitchDirection.Up)] = "i",
        [new(SwitchId.R_Middle, SwitchDirection.Down)] = ",",
        [new(SwitchId.R_Middle, SwitchDirection.Left)] = "8",
        [new(SwitchId.R_Middle, SwitchDirection.Right)] = "*",
        // Right Ring
        [new(SwitchId.R_Ring, SwitchDirection.Push)] = "l",
        [new(SwitchId.R_Ring, SwitchDirection.Up)] = "o",
        [new(SwitchId.R_Ring, SwitchDirection.Down)] = ".",
        [new(SwitchId.R_Ring, SwitchDirection.Left)] = "9",
        [new(SwitchId.R_Ring, SwitchDirection.Right)] = "(",
        // Right Pinky
        [new(SwitchId.R_Pinky, SwitchDirection.Push)] = ";",
        [new(SwitchId.R_Pinky, SwitchDirection.Up)] = "p",
        [new(SwitchId.R_Pinky, SwitchDirection.Down)] = "/",
        [new(SwitchId.R_Pinky, SwitchDirection.Left)] = "0",
        [new(SwitchId.R_Pinky, SwitchDirection.Right)] = ")",
        // Right PalmUpper
        [new(SwitchId.R_PalmUpper, SwitchDirection.Push)] = "'",
        [new(SwitchId.R_PalmUpper, SwitchDirection.Up)] = "\"",
        [new(SwitchId.R_PalmUpper, SwitchDirection.Down)] = ":",
        [new(SwitchId.R_PalmUpper, SwitchDirection.Left)] = "?",
        [new(SwitchId.R_PalmUpper, SwitchDirection.Right)] = "|",
        // Right PalmLower
        [new(SwitchId.R_PalmLower, SwitchDirection.Push)] = "4",
        [new(SwitchId.R_PalmLower, SwitchDirection.Up)] = "5",
        [new(SwitchId.R_PalmLower, SwitchDirection.Down)] = "6",
        [new(SwitchId.R_PalmLower, SwitchDirection.Left)] = "7",
        [new(SwitchId.R_PalmLower, SwitchDirection.Right)] = "&",
        // Right Edge
        [new(SwitchId.R_Edge, SwitchDirection.Push)] = "$",
        [new(SwitchId.R_Edge, SwitchDirection.Up)] = "%",
        [new(SwitchId.R_Edge, SwitchDirection.Down)] = "^",
        [new(SwitchId.R_Edge, SwitchDirection.Left)] = "~",
        [new(SwitchId.R_Edge, SwitchDirection.Right)] = "`",
    };

    public static readonly Dictionary<string, SwitchInput> ReverseMap =
        Map.Where(kvp => kvp.Value.Length == 1 && !char.IsControl(kvp.Value[0]))
           .GroupBy(kvp => kvp.Value)
           .ToDictionary(g => g.Key, g => g.First().Key);
}
```

- [ ] **Step 4: Run tests to verify they pass**

```bash
cd C:/Developer/chordcraft
dotnet test tests/ChordCraft.Core.Tests --verbosity quiet
```

Expected: All tests PASS.

- [ ] **Step 5: Commit**

```bash
cd C:/Developer/chordcraft
git add src/ChordCraft.Core/Models/CcosLayout.cs tests/ChordCraft.Core.Tests
git commit -m "feat: add CCOS default layout mapping with 90 inputs"
```

---

### Task 4: Core Domain — Entities

**Files:**
- Create: `src/ChordCraft.Core/Entities/User.cs`
- Create: `src/ChordCraft.Core/Entities/Phase.cs`
- Create: `src/ChordCraft.Core/Entities/Lesson.cs`
- Create: `src/ChordCraft.Core/Entities/LessonAttempt.cs`
- Create: `src/ChordCraft.Core/Entities/UserProgress.cs`
- Create: `src/ChordCraft.Core/Entities/Badge.cs`
- Create: `src/ChordCraft.Core/Entities/UserBadge.cs`
- Create: `src/ChordCraft.Core/Entities/ChordEntry.cs`

- [ ] **Step 1: Create all entity classes**

`src/ChordCraft.Core/Entities/User.cs`:
```csharp
using Microsoft.AspNetCore.Identity;

namespace ChordCraft.Core.Entities;

public class User : IdentityUser<Guid>
{
    public string? DisplayName { get; set; }
    public string? SessionId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime LastActiveAt { get; set; } = DateTime.UtcNow;

    public ICollection<LessonAttempt> Attempts { get; set; } = [];
    public ICollection<UserProgress> Progress { get; set; } = [];
    public ICollection<UserBadge> Badges { get; set; } = [];
}
```

`src/ChordCraft.Core/Entities/Phase.cs`:
```csharp
namespace ChordCraft.Core.Entities;

public class Phase
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Order { get; set; }

    public ICollection<Lesson> Lessons { get; set; } = [];
}
```

`src/ChordCraft.Core/Entities/Lesson.cs`:
```csharp
using ChordCraft.Core.Enums;

namespace ChordCraft.Core.Entities;

public class Lesson
{
    public int Id { get; set; }
    public int PhaseId { get; set; }
    public int Number { get; set; }
    public string Title { get; set; } = string.Empty;
    public LessonType Type { get; set; }
    public string Description { get; set; } = string.Empty;
    public string TargetInputs { get; set; } = "[]";
    public decimal PassAccuracyThreshold { get; set; } = 0.80m;
    public int? SpeedGoal { get; set; }
    public string Content { get; set; } = "{}";

    public Phase Phase { get; set; } = null!;
    public ICollection<LessonAttempt> Attempts { get; set; } = [];
    public ICollection<UserProgress> UserProgress { get; set; } = [];
}
```

`src/ChordCraft.Core/Entities/LessonAttempt.cs`:
```csharp
namespace ChordCraft.Core.Entities;

public class LessonAttempt
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public int LessonId { get; set; }
    public DateTime StartedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public decimal Accuracy { get; set; }
    public decimal Speed { get; set; }
    public int Stars { get; set; }
    public int Points { get; set; }
    public bool Passed { get; set; }
    public string? InputLog { get; set; }

    public User User { get; set; } = null!;
    public Lesson Lesson { get; set; } = null!;
}
```

`src/ChordCraft.Core/Entities/UserProgress.cs`:
```csharp
namespace ChordCraft.Core.Entities;

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

    public User User { get; set; } = null!;
    public Lesson Lesson { get; set; } = null!;
}
```

`src/ChordCraft.Core/Entities/Badge.cs`:
```csharp
namespace ChordCraft.Core.Entities;

public class Badge
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string IconUrl { get; set; } = string.Empty;
    public string Criteria { get; set; } = "{}";

    public ICollection<UserBadge> UserBadges { get; set; } = [];
}
```

`src/ChordCraft.Core/Entities/UserBadge.cs`:
```csharp
namespace ChordCraft.Core.Entities;

public class UserBadge
{
    public Guid UserId { get; set; }
    public int BadgeId { get; set; }
    public DateTime EarnedAt { get; set; } = DateTime.UtcNow;

    public User User { get; set; } = null!;
    public Badge Badge { get; set; } = null!;
}
```

`src/ChordCraft.Core/Entities/ChordEntry.cs`:
```csharp
namespace ChordCraft.Core.Entities;

public class ChordEntry
{
    public int Id { get; set; }
    public string OutputText { get; set; } = string.Empty;
    public string InputKeys { get; set; } = "[]";
    public int Difficulty { get; set; }
    public string Category { get; set; } = string.Empty;
}
```

- [ ] **Step 2: Add Identity NuGet to Core project**

```bash
cd C:/Developer/chordcraft
dotnet add src/ChordCraft.Core package Microsoft.Extensions.Identity.Stores -v 9.*
```

> This gives access to `IdentityUser<Guid>` without pulling in the full ASP.NET Core Identity stack.

- [ ] **Step 3: Build to verify entities compile**

```bash
cd C:/Developer/chordcraft
dotnet build src/ChordCraft.Core
```

Expected: Build succeeded.

- [ ] **Step 4: Commit**

```bash
cd C:/Developer/chordcraft
git add src/ChordCraft.Core/Entities
git commit -m "feat: add all domain entities (User, Phase, Lesson, Attempt, Progress, Badge, Chord)"
```

---

### Task 5: Core Domain — DTOs

**Files:**
- Create: `src/ChordCraft.Core/DTOs/Auth/RegisterRequest.cs`
- Create: `src/ChordCraft.Core/DTOs/Auth/LoginRequest.cs`
- Create: `src/ChordCraft.Core/DTOs/Auth/AuthResponse.cs`
- Create: `src/ChordCraft.Core/DTOs/Auth/MigrateProgressRequest.cs`
- Create: `src/ChordCraft.Core/DTOs/Lessons/PhaseDto.cs`
- Create: `src/ChordCraft.Core/DTOs/Lessons/LessonSummaryDto.cs`
- Create: `src/ChordCraft.Core/DTOs/Lessons/LessonDetailDto.cs`
- Create: `src/ChordCraft.Core/DTOs/Progress/SubmitAttemptRequest.cs`
- Create: `src/ChordCraft.Core/DTOs/Progress/SubmitAttemptResponse.cs`
- Create: `src/ChordCraft.Core/DTOs/Progress/UserProgressDto.cs`
- Create: `src/ChordCraft.Core/DTOs/Progress/StatsDto.cs`
- Create: `src/ChordCraft.Core/DTOs/Badges/BadgeDto.cs`

- [ ] **Step 1: Create all DTO records**

`src/ChordCraft.Core/DTOs/Auth/RegisterRequest.cs`:
```csharp
using System.ComponentModel.DataAnnotations;

namespace ChordCraft.Core.DTOs.Auth;

public record RegisterRequest(
    [Required, EmailAddress] string Email,
    [Required, MinLength(8)] string Password,
    [Required, MaxLength(50)] string DisplayName);
```

`src/ChordCraft.Core/DTOs/Auth/LoginRequest.cs`:
```csharp
using System.ComponentModel.DataAnnotations;

namespace ChordCraft.Core.DTOs.Auth;

public record LoginRequest(
    [Required, EmailAddress] string Email,
    [Required] string Password);
```

`src/ChordCraft.Core/DTOs/Auth/AuthResponse.cs`:
```csharp
namespace ChordCraft.Core.DTOs.Auth;

public record AuthResponse(
    string AccessToken,
    string RefreshToken,
    DateTime ExpiresAt,
    string DisplayName);
```

`src/ChordCraft.Core/DTOs/Auth/MigrateProgressRequest.cs`:
```csharp
namespace ChordCraft.Core.DTOs.Auth;

public record MigrateProgressRequest(
    string SessionId,
    List<MigrateProgressEntry> Progress,
    List<MigrateLessonAttemptEntry> Attempts);

public record MigrateProgressEntry(
    int LessonId, int BestStars, decimal BestAccuracy,
    decimal BestSpeed, int TotalAttempts);

public record MigrateLessonAttemptEntry(
    int LessonId, DateTime StartedAt, DateTime? CompletedAt,
    decimal Accuracy, decimal Speed, int Stars, int Points, bool Passed);
```

`src/ChordCraft.Core/DTOs/Lessons/LessonSummaryDto.cs`:
```csharp
using ChordCraft.Core.Enums;

namespace ChordCraft.Core.DTOs.Lessons;

public record LessonSummaryDto(
    int Id, int Number, string Title, LessonType Type,
    string Description, int? SpeedGoal);
```

`src/ChordCraft.Core/DTOs/Lessons/PhaseDto.cs`:
```csharp
namespace ChordCraft.Core.DTOs.Lessons;

public record PhaseDto(
    int Id, string Name, string Description, int Order,
    List<LessonSummaryDto> Lessons);
```

`src/ChordCraft.Core/DTOs/Lessons/LessonDetailDto.cs`:
```csharp
using ChordCraft.Core.Enums;

namespace ChordCraft.Core.DTOs.Lessons;

public record LessonDetailDto(
    int Id, int Number, string Title, LessonType Type,
    string Description, string TargetInputs, decimal PassAccuracyThreshold,
    int? SpeedGoal, string Content);
```

`src/ChordCraft.Core/DTOs/Progress/SubmitAttemptRequest.cs`:
```csharp
using System.ComponentModel.DataAnnotations;

namespace ChordCraft.Core.DTOs.Progress;

public record SubmitAttemptRequest(
    [Required] int LessonId,
    [Range(0, 1)] decimal Accuracy,
    [Range(0, 500)] decimal Speed,
    string? InputLog);
```

`src/ChordCraft.Core/DTOs/Progress/SubmitAttemptResponse.cs`:
```csharp
using ChordCraft.Core.DTOs.Badges;

namespace ChordCraft.Core.DTOs.Progress;

public record SubmitAttemptResponse(
    int Stars, int Points, bool Passed,
    List<BadgeDto>? NewBadges);
```

`src/ChordCraft.Core/DTOs/Progress/UserProgressDto.cs`:
```csharp
namespace ChordCraft.Core.DTOs.Progress;

public record UserProgressDto(
    int LessonId, int BestStars, decimal BestAccuracy,
    decimal BestSpeed, int TotalAttempts);
```

`src/ChordCraft.Core/DTOs/Progress/StatsDto.cs`:
```csharp
namespace ChordCraft.Core.DTOs.Progress;

public record StatsDto(
    decimal OverallAccuracy,
    decimal Coverage,
    decimal AverageSpeed,
    TimeSpan TotalPracticeTime,
    List<DailyStatsDto> DailyStats,
    List<SwitchAccuracyDto> SwitchAccuracies);

public record DailyStatsDto(
    DateOnly Date, TimeSpan PracticeTime, decimal Accuracy, decimal Speed);

public record SwitchAccuracyDto(
    string SwitchId, string Direction, decimal Accuracy, int TotalInputs);
```

`src/ChordCraft.Core/DTOs/Badges/BadgeDto.cs`:
```csharp
namespace ChordCraft.Core.DTOs.Badges;

public record BadgeDto(
    int Id, string Name, string Description,
    string IconUrl, bool Earned, DateTime? EarnedAt);
```

- [ ] **Step 2: Build to verify**

```bash
cd C:/Developer/chordcraft
dotnet build src/ChordCraft.Core
```

Expected: Build succeeded.

- [ ] **Step 3: Commit**

```bash
cd C:/Developer/chordcraft
git add src/ChordCraft.Core/DTOs
git commit -m "feat: add all DTO records for Auth, Lessons, Progress, Badges"
```

---

### Task 6: Core Domain — Service Interfaces

**Files:**
- Create: `src/ChordCraft.Core/Interfaces/ILessonService.cs`
- Create: `src/ChordCraft.Core/Interfaces/IProgressService.cs`
- Create: `src/ChordCraft.Core/Interfaces/IBadgeService.cs`
- Create: `src/ChordCraft.Core/Interfaces/IStatsService.cs`
- Create: `src/ChordCraft.Core/Interfaces/IChordService.cs`
- Create: `src/ChordCraft.Core/Interfaces/ITokenService.cs`

- [ ] **Step 1: Create all interfaces**

`src/ChordCraft.Core/Interfaces/ILessonService.cs`:
```csharp
using ChordCraft.Core.DTOs.Lessons;

namespace ChordCraft.Core.Interfaces;

public interface ILessonService
{
    Task<List<PhaseDto>> GetAllPhasesAsync();
    Task<LessonDetailDto?> GetLessonAsync(int id);
    Task<bool> IsLessonUnlockedAsync(int lessonId, Guid userId);
}
```

`src/ChordCraft.Core/Interfaces/IProgressService.cs`:
```csharp
using ChordCraft.Core.DTOs.Progress;
using ChordCraft.Core.Entities;

namespace ChordCraft.Core.Interfaces;

public interface IProgressService
{
    Task<SubmitAttemptResponse> SubmitAttemptAsync(Guid userId, SubmitAttemptRequest request);
    Task<List<UserProgressDto>> GetAllProgressAsync(Guid userId);
    int CalculateStars(decimal accuracy, decimal speed, Lesson lesson);
    int CalculatePoints(int stars, decimal accuracy, decimal speed, Lesson lesson);
}
```

`src/ChordCraft.Core/Interfaces/IBadgeService.cs`:
```csharp
using ChordCraft.Core.DTOs.Badges;

namespace ChordCraft.Core.Interfaces;

public interface IBadgeService
{
    Task<List<BadgeDto>> GetBadgesForUserAsync(Guid userId);
    Task<List<BadgeDto>> EvaluateAndAwardBadgesAsync(Guid userId);
}
```

`src/ChordCraft.Core/Interfaces/IStatsService.cs`:
```csharp
using ChordCraft.Core.DTOs.Progress;

namespace ChordCraft.Core.Interfaces;

public interface IStatsService
{
    Task<StatsDto> GetStatsAsync(Guid userId);
}
```

`src/ChordCraft.Core/Interfaces/IChordService.cs`:
```csharp
using ChordCraft.Core.Entities;

namespace ChordCraft.Core.Interfaces;

public interface IChordService
{
    Task<List<ChordEntry>> GetChordsAsync(int? difficulty = null, string? category = null);
}
```

`src/ChordCraft.Core/Interfaces/ITokenService.cs`:
```csharp
using ChordCraft.Core.Entities;

namespace ChordCraft.Core.Interfaces;

public interface ITokenService
{
    string GenerateAccessToken(User user);
    string GenerateRefreshToken();
}
```

- [ ] **Step 2: Build to verify**

```bash
cd C:/Developer/chordcraft
dotnet build src/ChordCraft.Core
```

Expected: Build succeeded.

- [ ] **Step 3: Commit**

```bash
cd C:/Developer/chordcraft
git add src/ChordCraft.Core/Interfaces
git commit -m "feat: add service interfaces for Lessons, Progress, Badges, Stats, Chords, Token"
```

---

### Task 7: Infrastructure — AppDbContext and EF Core Configuration

**Files:**
- Create: `src/ChordCraft.Infrastructure/Data/AppDbContext.cs`
- Delete: `src/ChordCraft.Infrastructure/Class1.cs` (template file)

- [ ] **Step 1: Create AppDbContext**

`src/ChordCraft.Infrastructure/Data/AppDbContext.cs`:
```csharp
using ChordCraft.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChordCraft.Infrastructure.Data;

public class AppDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Phase> Phases => Set<Phase>();
    public DbSet<Lesson> Lessons => Set<Lesson>();
    public DbSet<LessonAttempt> LessonAttempts => Set<LessonAttempt>();
    public DbSet<UserProgress> UserProgress => Set<UserProgress>();
    public DbSet<Badge> Badges => Set<Badge>();
    public DbSet<UserBadge> UserBadges => Set<UserBadge>();
    public DbSet<ChordEntry> ChordEntries => Set<ChordEntry>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Composite keys
        builder.Entity<UserProgress>()
            .HasKey(up => new { up.UserId, up.LessonId });

        builder.Entity<UserBadge>()
            .HasKey(ub => new { ub.UserId, ub.BadgeId });

        // JSON columns (PostgreSQL jsonb)
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

        // Indexes
        builder.Entity<LessonAttempt>()
            .HasIndex(la => la.UserId);
        builder.Entity<LessonAttempt>()
            .HasIndex(la => la.LessonId);
        builder.Entity<UserProgress>()
            .HasIndex(up => up.UserId);

        // Decimal precision
        builder.Entity<Lesson>()
            .Property(l => l.PassAccuracyThreshold).HasPrecision(5, 4);
        builder.Entity<LessonAttempt>()
            .Property(la => la.Accuracy).HasPrecision(5, 4);
        builder.Entity<LessonAttempt>()
            .Property(la => la.Speed).HasPrecision(7, 2);
        builder.Entity<UserProgress>()
            .Property(up => up.BestAccuracy).HasPrecision(5, 4);
        builder.Entity<UserProgress>()
            .Property(up => up.BestSpeed).HasPrecision(7, 2);

        // Relationships
        builder.Entity<Lesson>()
            .HasOne(l => l.Phase)
            .WithMany(p => p.Lessons)
            .HasForeignKey(l => l.PhaseId);

        builder.Entity<LessonAttempt>()
            .HasOne(la => la.User)
            .WithMany(u => u.Attempts)
            .HasForeignKey(la => la.UserId);

        builder.Entity<LessonAttempt>()
            .HasOne(la => la.Lesson)
            .WithMany(l => l.Attempts)
            .HasForeignKey(la => la.LessonId);

        builder.Entity<UserProgress>()
            .HasOne(up => up.User)
            .WithMany(u => u.Progress)
            .HasForeignKey(up => up.UserId);

        builder.Entity<UserProgress>()
            .HasOne(up => up.Lesson)
            .WithMany(l => l.UserProgress)
            .HasForeignKey(up => up.LessonId);

        builder.Entity<UserBadge>()
            .HasOne(ub => ub.User)
            .WithMany(u => u.Badges)
            .HasForeignKey(ub => ub.UserId);

        builder.Entity<UserBadge>()
            .HasOne(ub => ub.Badge)
            .WithMany(b => b.UserBadges)
            .HasForeignKey(ub => ub.BadgeId);
    }
}
```

- [ ] **Step 2: Delete template file and build**

```bash
cd C:/Developer/chordcraft
rm src/ChordCraft.Infrastructure/Class1.cs
dotnet build src/ChordCraft.Infrastructure
```

Expected: Build succeeded.

- [ ] **Step 3: Commit**

```bash
cd C:/Developer/chordcraft
git add src/ChordCraft.Infrastructure
git commit -m "feat: add AppDbContext with EF Core configuration, indexes, and relationships"
```

---

### Task 8: Infrastructure — Seed Data

**Files:**
- Create: `src/ChordCraft.Infrastructure/Data/Seed/PhaseSeedData.cs`
- Create: `src/ChordCraft.Infrastructure/Data/Seed/BadgeSeedData.cs`
- Create: `src/ChordCraft.Infrastructure/Data/Seed/LessonSeedData.cs`

- [ ] **Step 1: Create Phase seed data**

`src/ChordCraft.Infrastructure/Data/Seed/PhaseSeedData.cs`:
```csharp
using ChordCraft.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChordCraft.Infrastructure.Data.Seed;

public class PhaseSeedData : IEntityTypeConfiguration<Phase>
{
    public void Configure(EntityTypeBuilder<Phase> builder)
    {
        builder.HasData(
            new Phase { Id = 1, Name = "Switch Basics", Description = "Learn the push direction on all finger switches", Order = 1 },
            new Phase { Id = 2, Name = "Directional Inputs", Description = "Master all 4 directional inputs across all switches", Order = 2 },
            new Phase { Id = 3, Name = "CCE Speed Building", Description = "Build typing speed with character-by-character entry", Order = 3 },
            new Phase { Id = 4, Name = "Introduction to Chording", Description = "Learn to press multiple switches simultaneously for whole words", Order = 4 },
            new Phase { Id = 5, Name = "Advanced Chording", Description = "Build chord vocabulary and speed for real-world proficiency", Order = 5 }
        );
    }
}
```

- [ ] **Step 2: Create Badge seed data**

`src/ChordCraft.Infrastructure/Data/Seed/BadgeSeedData.cs`:
```csharp
using ChordCraft.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChordCraft.Infrastructure.Data.Seed;

public class BadgeSeedData : IEntityTypeConfiguration<Badge>
{
    public void Configure(EntityTypeBuilder<Badge> builder)
    {
        builder.HasData(
            new Badge { Id = 1, Name = "First Steps", Description = "Complete your first lesson", IconUrl = "/images/badges/first-steps.svg", Criteria = """{"type":"first-lesson"}""" },
            new Badge { Id = 2, Name = "Switch Basics", Description = "Complete Phase 1", IconUrl = "/images/badges/switch-basics.svg", Criteria = """{"type":"phase-complete","phaseId":1}""" },
            new Badge { Id = 3, Name = "Direction Master", Description = "Complete Phase 2", IconUrl = "/images/badges/direction-master.svg", Criteria = """{"type":"phase-complete","phaseId":2}""" },
            new Badge { Id = 4, Name = "Speed Demon", Description = "Reach 20 WPM in a practice lesson", IconUrl = "/images/badges/speed-demon.svg", Criteria = """{"type":"speed-threshold","wpm":20}""" },
            new Badge { Id = 5, Name = "First Chord", Description = "Successfully type your first chord", IconUrl = "/images/badges/first-chord.svg", Criteria = """{"type":"first-chord"}""" },
            new Badge { Id = 6, Name = "Chord Collector", Description = "Learn 10 different chords", IconUrl = "/images/badges/chord-collector.svg", Criteria = """{"type":"chord-count","count":10}""" },
            new Badge { Id = 7, Name = "Chord Master", Description = "Learn 50 different chords", IconUrl = "/images/badges/chord-master.svg", Criteria = """{"type":"chord-count","count":50}""" },
            new Badge { Id = 8, Name = "5 Day Streak", Description = "Practice 5 days in a row", IconUrl = "/images/badges/5-day-streak.svg", Criteria = """{"type":"streak","days":5}""" },
            new Badge { Id = 9, Name = "Weekend Warrior", Description = "Practice on Saturday and Sunday", IconUrl = "/images/badges/weekend-warrior.svg", Criteria = """{"type":"weekend"}""" },
            new Badge { Id = 10, Name = "Marathon Runner", Description = "Practice for 30 minutes in one session", IconUrl = "/images/badges/marathon-runner.svg", Criteria = """{"type":"session-time","minutes":30}""" },
            new Badge { Id = 11, Name = "Perfectionist", Description = "Earn 5 stars on 10 lessons", IconUrl = "/images/badges/perfectionist.svg", Criteria = """{"type":"star-count","stars":5,"count":10}""" },
            new Badge { Id = 12, Name = "Full Layout", Description = "Show proficiency on all 90 CCOS inputs", IconUrl = "/images/badges/full-layout.svg", Criteria = """{"type":"full-layout"}""" },
            new Badge { Id = 13, Name = "The Heavyweight", Description = "Earn 5 stars on 5 lessons in one day", IconUrl = "/images/badges/heavyweight.svg", Criteria = """{"type":"daily-stars","stars":5,"count":5}""" },
            new Badge { Id = 14, Name = "Keyboard Crusher", Description = "Rack up 100 total lesson attempts", IconUrl = "/images/badges/keyboard-crusher.svg", Criteria = """{"type":"attempt-count","count":100}""" }
        );
    }
}
```

- [ ] **Step 3: Create Lesson seed data (Phase 1 — first 15 lessons)**

`src/ChordCraft.Infrastructure/Data/Seed/LessonSeedData.cs`:
```csharp
using ChordCraft.Core.Entities;
using ChordCraft.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChordCraft.Infrastructure.Data.Seed;

public class LessonSeedData : IEntityTypeConfiguration<Lesson>
{
    public void Configure(EntityTypeBuilder<Lesson> builder)
    {
        var lessons = new List<Lesson>();

        // Phase 1: Switch Basics (Lessons 1-15)
        lessons.AddRange(new[]
        {
            new Lesson { Id = 1, PhaseId = 1, Number = 1, Title = "Introduction to CharaChorder", Type = LessonType.Video, Description = "Learn what the CharaChorder One is and how to hold it", Content = """{"videoUrl":"/videos/intro.mp4","durationSeconds":120,"completionThreshold":0.9}""" },
            new Lesson { Id = 2, PhaseId = 1, Number = 2, Title = "Left Index Push: f", Type = LessonType.Intro, Description = "Push the left index switch to type f", TargetInputs = """["L_Index:Push"]""", Content = """{"steps":[{"instruction":"Push the left index switch down to type f","targetInput":"L_Index:Push","targetChar":"f"}]}""" },
            new Lesson { Id = 3, PhaseId = 1, Number = 3, Title = "Right Index Push: j", Type = LessonType.Intro, Description = "Push the right index switch to type j", TargetInputs = """["R_Index:Push"]""", Content = """{"steps":[{"instruction":"Push the right index switch down to type j","targetInput":"R_Index:Push","targetChar":"j"}]}""" },
            new Lesson { Id = 4, PhaseId = 1, Number = 4, Title = "Review: f & j", Type = LessonType.Review, Description = "Practice typing f and j", TargetInputs = """["L_Index:Push","R_Index:Push"]""", Content = """{"sequence":["f","j","f","j","f","f","j","j","f","j","j","f","f","j","f","j","j","f","j","f"]}""" },
            new Lesson { Id = 5, PhaseId = 1, Number = 5, Title = "Space (Left Thumb Push)", Type = LessonType.Intro, Description = "Push the left thumb switch for space", TargetInputs = """["L_Thumb:Push"]""", Content = """{"steps":[{"instruction":"Push the left thumb switch to type a space","targetInput":"L_Thumb:Push","targetChar":" "}]}""" },
            new Lesson { Id = 6, PhaseId = 1, Number = 6, Title = "Left Middle Push: d", Type = LessonType.Intro, Description = "Push the left middle switch to type d", TargetInputs = """["L_Middle:Push"]""", Content = """{"steps":[{"instruction":"Push the left middle switch down to type d","targetInput":"L_Middle:Push","targetChar":"d"}]}""" },
            new Lesson { Id = 7, PhaseId = 1, Number = 7, Title = "Right Middle Push: k", Type = LessonType.Intro, Description = "Push the right middle switch to type k", TargetInputs = """["R_Middle:Push"]""", Content = """{"steps":[{"instruction":"Push the right middle switch down to type k","targetInput":"R_Middle:Push","targetChar":"k"}]}""" },
            new Lesson { Id = 8, PhaseId = 1, Number = 8, Title = "Review: d & k", Type = LessonType.Review, Description = "Practice typing d and k", TargetInputs = """["L_Middle:Push","R_Middle:Push"]""", Content = """{"sequence":["d","k","d","k","d","d","k","k","d","k","k","d","d","k","d","k","k","d","k","d"]}""" },
            new Lesson { Id = 9, PhaseId = 1, Number = 9, Title = "Practice: f j d k", Type = LessonType.Practice, Description = "Practice all four keys together", TargetInputs = """["L_Index:Push","R_Index:Push","L_Middle:Push","R_Middle:Push"]""", SpeedGoal = 5, Content = """{"text":"f j d k f k d j f d k j d f j k f j d k","timeLimitSeconds":120}""" },
            new Lesson { Id = 10, PhaseId = 1, Number = 10, Title = "Left Ring Push: s", Type = LessonType.Intro, Description = "Push the left ring switch to type s", TargetInputs = """["L_Ring:Push"]""", Content = """{"steps":[{"instruction":"Push the left ring switch down to type s","targetInput":"L_Ring:Push","targetChar":"s"}]}""" },
            new Lesson { Id = 11, PhaseId = 1, Number = 11, Title = "Right Ring Push: l", Type = LessonType.Intro, Description = "Push the right ring switch to type l", TargetInputs = """["R_Ring:Push"]""", Content = """{"steps":[{"instruction":"Push the right ring switch down to type l","targetInput":"R_Ring:Push","targetChar":"l"}]}""" },
            new Lesson { Id = 12, PhaseId = 1, Number = 12, Title = "Left Pinky Push: a", Type = LessonType.Intro, Description = "Push the left pinky switch to type a", TargetInputs = """["L_Pinky:Push"]""", Content = """{"steps":[{"instruction":"Push the left pinky switch down to type a","targetInput":"L_Pinky:Push","targetChar":"a"}]}""" },
            new Lesson { Id = 13, PhaseId = 1, Number = 13, Title = "Right Pinky Push: ;", Type = LessonType.Intro, Description = "Push the right pinky switch to type ;", TargetInputs = """["R_Pinky:Push"]""", Content = """{"steps":[{"instruction":"Push the right pinky switch down to type ;","targetInput":"R_Pinky:Push","targetChar":";"}]}""" },
            new Lesson { Id = 14, PhaseId = 1, Number = 14, Title = "All Push Keys", Type = LessonType.Review, Description = "Review all finger push inputs", TargetInputs = """["L_Pinky:Push","L_Ring:Push","L_Middle:Push","L_Index:Push","R_Index:Push","R_Middle:Push","R_Ring:Push","R_Pinky:Push"]""", Content = """{"sequence":["a","s","d","f","j","k","l",";","f","j","a",";","s","l","d","k","f","j","a",";","s","l","d","k"]}""" },
            new Lesson { Id = 15, PhaseId = 1, Number = 15, Title = "Play: Push Keys", Type = LessonType.Play, Description = "Game challenge with push keys", TargetInputs = """["L_Pinky:Push","L_Ring:Push","L_Middle:Push","L_Index:Push","R_Index:Push","R_Middle:Push","R_Ring:Push","R_Pinky:Push"]""", SpeedGoal = 8, Content = """{"gameType":"falling-characters","timeLimitSeconds":60,"allowedInputs":["L_Pinky:Push","L_Ring:Push","L_Middle:Push","L_Index:Push","R_Index:Push","R_Middle:Push","R_Ring:Push","R_Pinky:Push"],"wordPool":["a","s","d","f","j","k","l",";"]}""" },
        });

        // Remaining phases (16-160) will be added in subsequent seed data files
        // as part of the content creation phase. The structure and Content JSON
        // format is established by the Phase 1 examples above.

        builder.HasData(lessons);
    }
}
```

- [ ] **Step 4: Register seed data in AppDbContext**

Add to `AppDbContext.OnModelCreating()`:
```csharp
        // Seed data
        new PhaseSeedData().Configure(builder.Entity<Phase>());
        new BadgeSeedData().Configure(builder.Entity<Badge>());
        new LessonSeedData().Configure(builder.Entity<Lesson>());
```

Add the using at the top of `AppDbContext.cs`:
```csharp
using ChordCraft.Infrastructure.Data.Seed;
```

- [ ] **Step 5: Build to verify**

```bash
cd C:/Developer/chordcraft
dotnet build src/ChordCraft.Infrastructure
```

Expected: Build succeeded.

- [ ] **Step 6: Commit**

```bash
cd C:/Developer/chordcraft
git add src/ChordCraft.Infrastructure/Data/Seed
git commit -m "feat: add seed data for Phases, Badges, and Phase 1 Lessons"
```

---

### Task 9: Core Domain — Scoring Logic with Tests

**Files:**
- Create: `src/ChordCraft.Core/Scoring/ScoringCalculator.cs`
- Create: `tests/ChordCraft.Core.Tests/ScoringTests.cs`

- [ ] **Step 1: Write failing tests**

`tests/ChordCraft.Core.Tests/ScoringTests.cs`:
```csharp
using ChordCraft.Core.Enums;
using ChordCraft.Core.Scoring;

namespace ChordCraft.Core.Tests;

public class ScoringTests
{
    // Star calculation — with SpeedGoal
    [Theory]
    [InlineData(0.79, 30, 20, 0)]   // Below threshold
    [InlineData(0.80, 5, 20, 1)]    // At threshold, low speed
    [InlineData(0.86, 11, 20, 2)]   // 85%+ accuracy, 50%+ speed
    [InlineData(0.91, 15, 20, 3)]   // 90%+ accuracy, 70%+ speed
    [InlineData(0.96, 18, 20, 4)]   // 95%+ accuracy, 85%+ speed
    [InlineData(0.99, 21, 20, 5)]   // 98%+ accuracy, 100%+ speed
    public void CalculateStars_WithSpeedGoal(double accuracy, double speed, int speedGoal, int expectedStars)
    {
        var result = ScoringCalculator.CalculateStars((decimal)accuracy, (decimal)speed, 0.80m, speedGoal);
        Assert.Equal(expectedStars, result);
    }

    // Star calculation — without SpeedGoal
    [Theory]
    [InlineData(0.79, 0)]
    [InlineData(0.80, 1)]
    [InlineData(0.86, 2)]
    [InlineData(0.91, 3)]
    [InlineData(0.96, 4)]
    [InlineData(0.99, 5)]
    public void CalculateStars_WithoutSpeedGoal(double accuracy, int expectedStars)
    {
        var result = ScoringCalculator.CalculateStars((decimal)accuracy, 0m, 0.80m, null);
        Assert.Equal(expectedStars, result);
    }

    // Points calculation
    [Fact]
    public void CalculatePoints_VideoLesson_Returns10()
    {
        var result = ScoringCalculator.CalculatePoints(1, 0.90m, 0m, null, LessonType.Video);
        Assert.Equal(10, result);
    }

    [Fact]
    public void CalculatePoints_ZeroStars_ReturnsZero()
    {
        var result = ScoringCalculator.CalculatePoints(0, 0.70m, 10m, 20, LessonType.Practice);
        Assert.Equal(0, result);
    }

    [Fact]
    public void CalculatePoints_WithSpeedGoal_IncludesSpeedBonus()
    {
        // 3 stars * 10 = 30, (90 - 80) * 2 = 20, speed: min(15,20)/20 * 20 = 15 → 65
        var result = ScoringCalculator.CalculatePoints(3, 0.90m, 15m, 20, LessonType.Practice);
        Assert.Equal(65, result);
    }

    [Fact]
    public void CalculatePoints_WithoutSpeedGoal_NoSpeedBonus()
    {
        // 3 stars * 10 = 30, (90 - 80) * 2 = 20 → 50
        var result = ScoringCalculator.CalculatePoints(3, 0.90m, 15m, null, LessonType.Review);
        Assert.Equal(50, result);
    }

    [Fact]
    public void CalculatePoints_CappedAt100()
    {
        // 5 * 10 = 50, (100 - 80) * 2 = 40, speed: 20/20 * 20 = 20 → 110 → capped 100
        var result = ScoringCalculator.CalculatePoints(5, 1.00m, 25m, 20, LessonType.Practice);
        Assert.Equal(100, result);
    }
}
```

- [ ] **Step 2: Run tests to verify they fail**

```bash
cd C:/Developer/chordcraft
dotnet test tests/ChordCraft.Core.Tests --verbosity quiet
```

Expected: FAIL — `ScoringCalculator` does not exist.

- [ ] **Step 3: Implement ScoringCalculator**

`src/ChordCraft.Core/Scoring/ScoringCalculator.cs`:
```csharp
using ChordCraft.Core.Enums;

namespace ChordCraft.Core.Scoring;

public static class ScoringCalculator
{
    public static int CalculateStars(decimal accuracy, decimal speed, decimal passThreshold, int? speedGoal)
    {
        if (accuracy < passThreshold) return 0;

        if (speedGoal.HasValue)
        {
            var goal = (decimal)speedGoal.Value;
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

    public static int CalculatePoints(int stars, decimal accuracy, decimal speed, int? speedGoal, LessonType lessonType)
    {
        if (stars == 0) return 0;
        if (lessonType == LessonType.Video) return 10;

        var points = stars * 10m + (accuracy * 100m - 80m) * 2m;

        if (speedGoal.HasValue && speedGoal.Value > 0)
        {
            var speedRatio = Math.Min(speed, speedGoal.Value) / speedGoal.Value;
            points += speedRatio * 20m;
        }

        return Math.Min((int)Math.Round(points), 100);
    }
}
```

- [ ] **Step 4: Run tests to verify they pass**

```bash
cd C:/Developer/chordcraft
dotnet test tests/ChordCraft.Core.Tests --verbosity quiet
```

Expected: All tests PASS.

- [ ] **Step 5: Commit**

```bash
cd C:/Developer/chordcraft
git add src/ChordCraft.Core/Scoring tests/ChordCraft.Core.Tests/ScoringTests.cs
git commit -m "feat: add star and points calculation with full test coverage"
```

---

### Task 10: API — Program.cs Setup and Configuration

**Files:**
- Modify: `src/ChordCraft.Api/Program.cs`
- Create: `src/ChordCraft.Api/appsettings.json`
- Create: `src/ChordCraft.Api/appsettings.Development.json`

- [ ] **Step 1: Configure Program.cs**

Replace `src/ChordCraft.Api/Program.cs` with:
```csharp
using System.Text;
using ChordCraft.Core.Entities;
using ChordCraft.Core.Interfaces;
using ChordCraft.Infrastructure.Data;
using ChordCraft.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Identity
builder.Services.AddIdentity<User, IdentityRole<Guid>>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false;
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

// JWT Authentication
var jwtKey = builder.Configuration["Jwt:Key"]
    ?? throw new InvalidOperationException("Jwt:Key is not configured");
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});

// Services
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<ILessonService, LessonService>();
builder.Services.AddScoped<IProgressService, ProgressService>();
builder.Services.AddScoped<IBadgeService, BadgeService>();
builder.Services.AddScoped<IStatsService, StatsService>();
builder.Services.AddScoped<IChordService, ChordService>();

// Controllers + Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowClient", policy =>
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

// Auto-migrate in development
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await db.Database.MigrateAsync();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowClient");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

// For WebApplicationFactory in tests
public partial class Program { }
```

- [ ] **Step 2: Create appsettings files**

`src/ChordCraft.Api/appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": ""
  },
  "Jwt": {
    "Key": "",
    "Issuer": "ChordCraft",
    "Audience": "ChordCraft"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information"
    }
  }
}
```

`src/ChordCraft.Api/appsettings.Development.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=chordcraft_dev;Username=postgres;Password=postgres"
  },
  "Jwt": {
    "Key": "ChordCraft-Dev-Secret-Key-Must-Be-At-Least-32-Chars!",
    "Issuer": "ChordCraft",
    "Audience": "ChordCraft"
  }
}
```

- [ ] **Step 3: Build to verify**

```bash
cd C:/Developer/chordcraft
dotnet build src/ChordCraft.Api
```

Expected: Build will fail because service implementations don't exist yet. That's expected — we'll create them in the next tasks.

- [ ] **Step 4: Commit**

```bash
cd C:/Developer/chordcraft
git add src/ChordCraft.Api/Program.cs src/ChordCraft.Api/appsettings.json
git commit -m "feat: configure API with Identity, JWT auth, EF Core, and service DI"
```

---

### Task 11: Infrastructure — Service Implementations

**Files:**
- Create: `src/ChordCraft.Infrastructure/Services/TokenService.cs`
- Create: `src/ChordCraft.Infrastructure/Services/LessonService.cs`
- Create: `src/ChordCraft.Infrastructure/Services/ProgressService.cs`
- Create: `src/ChordCraft.Infrastructure/Services/BadgeService.cs`
- Create: `src/ChordCraft.Infrastructure/Services/StatsService.cs`
- Create: `src/ChordCraft.Infrastructure/Services/ChordService.cs`

- [ ] **Step 1: Create TokenService**

`src/ChordCraft.Infrastructure/Services/TokenService.cs`:
```csharp
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ChordCraft.Core.Entities;
using ChordCraft.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ChordCraft.Infrastructure.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _config;

    public TokenService(IConfiguration config) => _config = config;

    public string GenerateAccessToken(User user)
    {
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email ?? ""),
            new Claim(ClaimTypes.Name, user.DisplayName ?? ""),
        };

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        var bytes = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(bytes);
        return Convert.ToBase64String(bytes);
    }
}
```

- [ ] **Step 2: Create LessonService**

`src/ChordCraft.Infrastructure/Services/LessonService.cs`:
```csharp
using ChordCraft.Core.DTOs.Lessons;
using ChordCraft.Core.Interfaces;
using ChordCraft.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ChordCraft.Infrastructure.Services;

public class LessonService : ILessonService
{
    private readonly AppDbContext _db;

    public LessonService(AppDbContext db) => _db = db;

    public async Task<List<PhaseDto>> GetAllPhasesAsync()
    {
        return await _db.Phases
            .OrderBy(p => p.Order)
            .Select(p => new PhaseDto(
                p.Id, p.Name, p.Description, p.Order,
                p.Lessons.OrderBy(l => l.Number)
                    .Select(l => new LessonSummaryDto(
                        l.Id, l.Number, l.Title, l.Type,
                        l.Description, l.SpeedGoal))
                    .ToList()))
            .ToListAsync();
    }

    public async Task<LessonDetailDto?> GetLessonAsync(int id)
    {
        var lesson = await _db.Lessons.FindAsync(id);
        if (lesson is null) return null;

        return new LessonDetailDto(
            lesson.Id, lesson.Number, lesson.Title, lesson.Type,
            lesson.Description, lesson.TargetInputs, lesson.PassAccuracyThreshold,
            lesson.SpeedGoal, lesson.Content);
    }

    public async Task<bool> IsLessonUnlockedAsync(int lessonId, Guid userId)
    {
        var lesson = await _db.Lessons.FindAsync(lessonId);
        if (lesson is null) return false;
        if (lesson.Number == 1) return true;

        // Find the previous lesson
        var prevLesson = await _db.Lessons
            .Where(l => l.Number == lesson.Number - 1)
            .FirstOrDefaultAsync();

        if (prevLesson is null) return true;

        var progress = await _db.UserProgress
            .FirstOrDefaultAsync(up => up.UserId == userId && up.LessonId == prevLesson.Id);

        return progress is not null && progress.BestStars >= 1;
    }
}
```

- [ ] **Step 3: Create ProgressService**

`src/ChordCraft.Infrastructure/Services/ProgressService.cs`:
```csharp
using ChordCraft.Core.DTOs.Badges;
using ChordCraft.Core.DTOs.Progress;
using ChordCraft.Core.Entities;
using ChordCraft.Core.Interfaces;
using ChordCraft.Core.Scoring;
using ChordCraft.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ChordCraft.Infrastructure.Services;

public class ProgressService : IProgressService
{
    private readonly AppDbContext _db;
    private readonly IBadgeService _badgeService;

    public ProgressService(AppDbContext db, IBadgeService badgeService)
    {
        _db = db;
        _badgeService = badgeService;
    }

    public async Task<SubmitAttemptResponse> SubmitAttemptAsync(Guid userId, SubmitAttemptRequest request)
    {
        var lesson = await _db.Lessons.FindAsync(request.LessonId)
            ?? throw new ArgumentException("Lesson not found");

        var stars = CalculateStars(request.Accuracy, request.Speed, lesson);
        var points = CalculatePoints(stars, request.Accuracy, request.Speed, lesson);
        var passed = stars >= 1;

        // Create attempt record
        var attempt = new LessonAttempt
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            LessonId = request.LessonId,
            StartedAt = DateTime.UtcNow.AddMinutes(-5), // approximate
            CompletedAt = DateTime.UtcNow,
            Accuracy = request.Accuracy,
            Speed = request.Speed,
            Stars = stars,
            Points = points,
            Passed = passed,
            InputLog = request.InputLog
        };
        _db.LessonAttempts.Add(attempt);

        // Update or create UserProgress
        var progress = await _db.UserProgress
            .FirstOrDefaultAsync(up => up.UserId == userId && up.LessonId == request.LessonId);

        if (progress is null)
        {
            progress = new UserProgress
            {
                UserId = userId,
                LessonId = request.LessonId,
                BestStars = stars,
                BestAccuracy = request.Accuracy,
                BestSpeed = request.Speed,
                TotalAttempts = 1,
                FirstCompletedAt = passed ? DateTime.UtcNow : null,
                LastAttemptAt = DateTime.UtcNow
            };
            _db.UserProgress.Add(progress);
        }
        else
        {
            progress.TotalAttempts++;
            progress.LastAttemptAt = DateTime.UtcNow;
            if (stars > progress.BestStars) progress.BestStars = stars;
            if (request.Accuracy > progress.BestAccuracy) progress.BestAccuracy = request.Accuracy;
            if (request.Speed > progress.BestSpeed) progress.BestSpeed = request.Speed;
            if (passed && progress.FirstCompletedAt is null) progress.FirstCompletedAt = DateTime.UtcNow;
        }

        await _db.SaveChangesAsync();

        // Evaluate badges
        List<BadgeDto>? newBadges = null;
        if (passed)
        {
            newBadges = await _badgeService.EvaluateAndAwardBadgesAsync(userId);
        }

        return new SubmitAttemptResponse(stars, points, passed, newBadges);
    }

    public async Task<List<UserProgressDto>> GetAllProgressAsync(Guid userId)
    {
        return await _db.UserProgress
            .Where(up => up.UserId == userId)
            .Select(up => new UserProgressDto(
                up.LessonId, up.BestStars, up.BestAccuracy,
                up.BestSpeed, up.TotalAttempts))
            .ToListAsync();
    }

    public int CalculateStars(decimal accuracy, decimal speed, Lesson lesson)
    {
        return ScoringCalculator.CalculateStars(
            accuracy, speed, lesson.PassAccuracyThreshold, lesson.SpeedGoal);
    }

    public int CalculatePoints(int stars, decimal accuracy, decimal speed, Lesson lesson)
    {
        return ScoringCalculator.CalculatePoints(
            stars, accuracy, speed, lesson.SpeedGoal, lesson.Type);
    }
}
```

- [ ] **Step 4: Create BadgeService (simplified — evaluates after each attempt)**

`src/ChordCraft.Infrastructure/Services/BadgeService.cs`:
```csharp
using System.Text.Json;
using ChordCraft.Core.DTOs.Badges;
using ChordCraft.Core.Entities;
using ChordCraft.Core.Interfaces;
using ChordCraft.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ChordCraft.Infrastructure.Services;

public class BadgeService : IBadgeService
{
    private readonly AppDbContext _db;

    public BadgeService(AppDbContext db) => _db = db;

    public async Task<List<BadgeDto>> GetBadgesForUserAsync(Guid userId)
    {
        var allBadges = await _db.Badges.ToListAsync();
        var earnedBadges = await _db.UserBadges
            .Where(ub => ub.UserId == userId)
            .ToDictionaryAsync(ub => ub.BadgeId, ub => ub.EarnedAt);

        return allBadges.Select(b => new BadgeDto(
            b.Id, b.Name, b.Description, b.IconUrl,
            earnedBadges.ContainsKey(b.Id),
            earnedBadges.GetValueOrDefault(b.Id)))
            .ToList();
    }

    public async Task<List<BadgeDto>> EvaluateAndAwardBadgesAsync(Guid userId)
    {
        var alreadyEarned = await _db.UserBadges
            .Where(ub => ub.UserId == userId)
            .Select(ub => ub.BadgeId)
            .ToHashSetAsync();

        var allBadges = await _db.Badges.ToListAsync();
        var newBadges = new List<BadgeDto>();

        foreach (var badge in allBadges.Where(b => !alreadyEarned.Contains(b.Id)))
        {
            if (await EvaluateBadge(badge, userId))
            {
                _db.UserBadges.Add(new UserBadge
                {
                    UserId = userId,
                    BadgeId = badge.Id,
                    EarnedAt = DateTime.UtcNow
                });
                newBadges.Add(new BadgeDto(
                    badge.Id, badge.Name, badge.Description,
                    badge.IconUrl, true, DateTime.UtcNow));
            }
        }

        if (newBadges.Count > 0)
            await _db.SaveChangesAsync();

        return newBadges;
    }

    private async Task<bool> EvaluateBadge(Badge badge, Guid userId)
    {
        var criteria = JsonDocument.Parse(badge.Criteria).RootElement;
        var type = criteria.GetProperty("type").GetString();

        return type switch
        {
            "first-lesson" => await _db.LessonAttempts
                .AnyAsync(la => la.UserId == userId && la.Passed),

            "phase-complete" => await EvaluatePhaseComplete(
                criteria.GetProperty("phaseId").GetInt32(), userId),

            "speed-threshold" => await _db.LessonAttempts
                .AnyAsync(la => la.UserId == userId && la.Passed
                    && la.Speed >= criteria.GetProperty("wpm").GetInt32()),

            "attempt-count" => await _db.LessonAttempts
                .CountAsync(la => la.UserId == userId)
                >= criteria.GetProperty("count").GetInt32(),

            "streak" => await EvaluateStreak(
                criteria.GetProperty("days").GetInt32(), userId),

            "star-count" => await _db.UserProgress
                .CountAsync(up => up.UserId == userId && up.BestStars == 5)
                >= criteria.GetProperty("count").GetInt32(),

            _ => false // Unknown criteria types are skipped
        };
    }

    private async Task<bool> EvaluatePhaseComplete(int phaseId, Guid userId)
    {
        var lessonIds = await _db.Lessons
            .Where(l => l.PhaseId == phaseId)
            .Select(l => l.Id)
            .ToListAsync();

        var completedCount = await _db.UserProgress
            .CountAsync(up => up.UserId == userId
                && lessonIds.Contains(up.LessonId)
                && up.BestStars >= 1);

        return completedCount >= lessonIds.Count;
    }

    private async Task<bool> EvaluateStreak(int requiredDays, Guid userId)
    {
        var dates = await _db.LessonAttempts
            .Where(la => la.UserId == userId && la.Passed)
            .Select(la => la.CompletedAt!.Value.Date)
            .Distinct()
            .OrderByDescending(d => d)
            .Take(requiredDays + 5)
            .ToListAsync();

        if (dates.Count < requiredDays) return false;

        var streak = 1;
        for (int i = 1; i < dates.Count; i++)
        {
            if ((dates[i - 1] - dates[i]).Days == 1)
            {
                streak++;
                if (streak >= requiredDays) return true;
            }
            else streak = 1;
        }
        return streak >= requiredDays;
    }
}
```

- [ ] **Step 5: Create StatsService**

`src/ChordCraft.Infrastructure/Services/StatsService.cs`:
```csharp
using ChordCraft.Core.DTOs.Progress;
using ChordCraft.Core.Interfaces;
using ChordCraft.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ChordCraft.Infrastructure.Services;

public class StatsService : IStatsService
{
    private readonly AppDbContext _db;

    public StatsService(AppDbContext db) => _db = db;

    public async Task<StatsDto> GetStatsAsync(Guid userId)
    {
        var attempts = await _db.LessonAttempts
            .Where(la => la.UserId == userId && la.CompletedAt != null)
            .ToListAsync();

        var totalLessons = await _db.Lessons.CountAsync();
        var completedLessons = await _db.UserProgress
            .CountAsync(up => up.UserId == userId && up.BestStars >= 1);

        var overallAccuracy = attempts.Count > 0
            ? attempts.Average(a => a.Accuracy)
            : 0m;
        var averageSpeed = attempts.Count > 0
            ? attempts.Where(a => a.Speed > 0).DefaultIfEmpty()
                .Average(a => a?.Speed ?? 0)
            : 0m;
        var coverage = totalLessons > 0
            ? (decimal)completedLessons / totalLessons
            : 0m;
        var totalTime = TimeSpan.FromSeconds(
            attempts.Where(a => a.CompletedAt.HasValue)
                .Sum(a => (a.CompletedAt!.Value - a.StartedAt).TotalSeconds));

        var dailyStats = attempts
            .GroupBy(a => DateOnly.FromDateTime(a.StartedAt))
            .OrderBy(g => g.Key)
            .Select(g => new DailyStatsDto(
                g.Key,
                TimeSpan.FromSeconds(g.Sum(a =>
                    a.CompletedAt.HasValue ? (a.CompletedAt.Value - a.StartedAt).TotalSeconds : 0)),
                g.Average(a => a.Accuracy),
                g.Where(a => a.Speed > 0).DefaultIfEmpty()
                    .Average(a => a?.Speed ?? 0)))
            .ToList();

        return new StatsDto(
            overallAccuracy, coverage, averageSpeed,
            totalTime, dailyStats, []);
    }
}
```

- [ ] **Step 6: Create ChordService**

`src/ChordCraft.Infrastructure/Services/ChordService.cs`:
```csharp
using ChordCraft.Core.Entities;
using ChordCraft.Core.Interfaces;
using ChordCraft.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ChordCraft.Infrastructure.Services;

public class ChordService : IChordService
{
    private readonly AppDbContext _db;

    public ChordService(AppDbContext db) => _db = db;

    public async Task<List<ChordEntry>> GetChordsAsync(int? difficulty = null, string? category = null)
    {
        var query = _db.ChordEntries.AsQueryable();
        if (difficulty.HasValue) query = query.Where(c => c.Difficulty == difficulty.Value);
        if (!string.IsNullOrEmpty(category)) query = query.Where(c => c.Category == category);
        return await query.OrderBy(c => c.Difficulty).ThenBy(c => c.Id).ToListAsync();
    }
}
```

- [ ] **Step 7: Add Microsoft.IdentityModel.Tokens NuGet to Infrastructure**

```bash
cd C:/Developer/chordcraft
dotnet add src/ChordCraft.Infrastructure package System.IdentityModel.Tokens.Jwt
dotnet add src/ChordCraft.Infrastructure package Microsoft.Extensions.Configuration.Abstractions
```

- [ ] **Step 8: Build to verify**

```bash
cd C:/Developer/chordcraft
dotnet build
```

Expected: Build succeeded.

- [ ] **Step 9: Commit**

```bash
cd C:/Developer/chordcraft
git add src/ChordCraft.Infrastructure/Services
git commit -m "feat: implement all service layer (Token, Lesson, Progress, Badge, Stats, Chord)"
```

---

### Task 12: API — Controllers

**Files:**
- Create: `src/ChordCraft.Api/Controllers/AuthController.cs`
- Create: `src/ChordCraft.Api/Controllers/LessonsController.cs`
- Create: `src/ChordCraft.Api/Controllers/ProgressController.cs`
- Create: `src/ChordCraft.Api/Controllers/BadgesController.cs`
- Create: `src/ChordCraft.Api/Controllers/ChordsController.cs`
- Delete: template files from `dotnet new webapi`

- [ ] **Step 1: Create AuthController**

`src/ChordCraft.Api/Controllers/AuthController.cs`:
```csharp
using System.Security.Claims;
using ChordCraft.Core.DTOs.Auth;
using ChordCraft.Core.Entities;
using ChordCraft.Core.Interfaces;
using ChordCraft.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ChordCraft.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly ITokenService _tokenService;
    private readonly AppDbContext _db;

    public AuthController(
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        ITokenService tokenService,
        AppDbContext db)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
        _db = db;
    }

    [HttpPost("register")]
    public async Task<ActionResult<AuthResponse>> Register(RegisterRequest request)
    {
        var user = new User
        {
            UserName = request.Email,
            Email = request.Email,
            DisplayName = request.DisplayName,
            CreatedAt = DateTime.UtcNow,
            LastActiveAt = DateTime.UtcNow
        };

        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
            return BadRequest(result.Errors);

        var token = _tokenService.GenerateAccessToken(user);
        var refresh = _tokenService.GenerateRefreshToken();

        return Ok(new AuthResponse(token, refresh, DateTime.UtcNow.AddHours(1), user.DisplayName ?? ""));
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponse>> Login(LoginRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is null) return Unauthorized("Invalid credentials");

        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
        if (!result.Succeeded) return Unauthorized("Invalid credentials");

        user.LastActiveAt = DateTime.UtcNow;
        await _userManager.UpdateAsync(user);

        var token = _tokenService.GenerateAccessToken(user);
        var refresh = _tokenService.GenerateRefreshToken();

        return Ok(new AuthResponse(token, refresh, DateTime.UtcNow.AddHours(1), user.DisplayName ?? ""));
    }

    [Authorize]
    [HttpPost("migrate")]
    public async Task<IActionResult> Migrate(MigrateProgressRequest request)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        foreach (var p in request.Progress)
        {
            _db.UserProgress.Add(new UserProgress
            {
                UserId = userId,
                LessonId = p.LessonId,
                BestStars = p.BestStars,
                BestAccuracy = p.BestAccuracy,
                BestSpeed = p.BestSpeed,
                TotalAttempts = p.TotalAttempts,
                FirstCompletedAt = DateTime.UtcNow,
                LastAttemptAt = DateTime.UtcNow
            });
        }

        foreach (var a in request.Attempts)
        {
            _db.LessonAttempts.Add(new LessonAttempt
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                LessonId = a.LessonId,
                StartedAt = a.StartedAt,
                CompletedAt = a.CompletedAt,
                Accuracy = a.Accuracy,
                Speed = a.Speed,
                Stars = a.Stars,
                Points = a.Points,
                Passed = a.Passed
            });
        }

        await _db.SaveChangesAsync();
        return Ok();
    }
}
```

- [ ] **Step 2: Create LessonsController**

`src/ChordCraft.Api/Controllers/LessonsController.cs`:
```csharp
using System.Security.Claims;
using ChordCraft.Core.DTOs.Progress;
using ChordCraft.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChordCraft.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LessonsController : ControllerBase
{
    private readonly ILessonService _lessonService;
    private readonly IProgressService _progressService;

    public LessonsController(ILessonService lessonService, IProgressService progressService)
    {
        _lessonService = lessonService;
        _progressService = progressService;
    }

    [HttpGet("~/api/phases")]
    public async Task<IActionResult> GetPhases()
    {
        var phases = await _lessonService.GetAllPhasesAsync();
        return Ok(phases);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetLesson(int id)
    {
        var lesson = await _lessonService.GetLessonAsync(id);
        if (lesson is null) return NotFound();
        return Ok(lesson);
    }

    [Authorize]
    [HttpGet("{id}/progress")]
    public async Task<IActionResult> GetLessonProgress(int id)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var progress = (await _progressService.GetAllProgressAsync(userId))
            .FirstOrDefault(p => p.LessonId == id);
        if (progress is null) return NotFound();
        return Ok(progress);
    }
}
```

- [ ] **Step 3: Create ProgressController**

`src/ChordCraft.Api/Controllers/ProgressController.cs`:
```csharp
using System.Security.Claims;
using ChordCraft.Core.DTOs.Progress;
using ChordCraft.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChordCraft.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProgressController : ControllerBase
{
    private readonly IProgressService _progressService;
    private readonly IStatsService _statsService;

    public ProgressController(IProgressService progressService, IStatsService statsService)
    {
        _progressService = progressService;
        _statsService = statsService;
    }

    [HttpPost("~/api/attempts")]
    public async Task<IActionResult> SubmitAttempt(SubmitAttemptRequest request)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var result = await _progressService.SubmitAttemptAsync(userId, request);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetProgress()
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var progress = await _progressService.GetAllProgressAsync(userId);
        return Ok(progress);
    }

    [HttpGet("~/api/stats")]
    public async Task<IActionResult> GetStats()
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var stats = await _statsService.GetStatsAsync(userId);
        return Ok(stats);
    }
}
```

- [ ] **Step 4: Create BadgesController**

`src/ChordCraft.Api/Controllers/BadgesController.cs`:
```csharp
using System.Security.Claims;
using ChordCraft.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChordCraft.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class BadgesController : ControllerBase
{
    private readonly IBadgeService _badgeService;

    public BadgesController(IBadgeService badgeService) => _badgeService = badgeService;

    [HttpGet]
    public async Task<IActionResult> GetBadges()
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var badges = await _badgeService.GetBadgesForUserAsync(userId);
        return Ok(badges);
    }
}
```

- [ ] **Step 5: Create ChordsController**

`src/ChordCraft.Api/Controllers/ChordsController.cs`:
```csharp
using ChordCraft.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ChordCraft.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChordsController : ControllerBase
{
    private readonly IChordService _chordService;

    public ChordsController(IChordService chordService) => _chordService = chordService;

    [HttpGet]
    public async Task<IActionResult> GetChords([FromQuery] int? difficulty, [FromQuery] string? category)
    {
        var chords = await _chordService.GetChordsAsync(difficulty, category);
        return Ok(chords);
    }
}
```

- [ ] **Step 6: Delete template files and build**

```bash
cd C:/Developer/chordcraft
rm -f src/ChordCraft.Api/Controllers/WeatherForecastController.cs
rm -f src/ChordCraft.Api/WeatherForecast.cs
dotnet build
```

Expected: Build succeeded.

- [ ] **Step 7: Commit**

```bash
cd C:/Developer/chordcraft
git add src/ChordCraft.Api/Controllers
git commit -m "feat: add all API controllers (Auth, Lessons, Progress, Badges, Chords)"
```

---

### Task 13: Blazor WASM Client — Minimal Placeholder

**Files:**
- Modify: `src/ChordCraft.Client/Program.cs`
- Create: `src/ChordCraft.Client/Layout/MainLayout.razor`

- [ ] **Step 1: Set up minimal Blazor WASM client with MudBlazor**

`src/ChordCraft.Client/Program.cs`:
```csharp
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using ChordCraft.Client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices();
builder.Services.AddScoped(sp =>
    new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
```

`src/ChordCraft.Client/Layout/MainLayout.razor`:
```razor
@inherits LayoutComponentBase
@using MudBlazor

<MudThemeProvider />
<MudPopoverProvider />
<MudDialogProvider />
<MudSnackbarProvider />

<MudLayout>
    <MudAppBar Elevation="1">
        <MudText Typo="Typo.h5" Class="ml-3">ChordCraft</MudText>
        <MudSpacer />
        <MudButton Variant="Variant.Text" Color="Color.Inherit">Get Started</MudButton>
    </MudAppBar>
    <MudMainContent>
        @Body
    </MudMainContent>
</MudLayout>
```

- [ ] **Step 2: Build to verify**

```bash
cd C:/Developer/chordcraft
dotnet build
```

Expected: Build succeeded.

- [ ] **Step 3: Commit**

```bash
cd C:/Developer/chordcraft
git add src/ChordCraft.Client
git commit -m "feat: add minimal Blazor WASM client with MudBlazor placeholder"
```

---

### Task 14: EF Core Migration and Dockerfile

**Files:**
- Create: `src/ChordCraft.Infrastructure/Data/Migrations/` (auto-generated)
- Create: `Dockerfile`

- [ ] **Step 1: Create initial migration**

```bash
cd C:/Developer/chordcraft
dotnet ef migrations add InitialCreate --project src/ChordCraft.Infrastructure --startup-project src/ChordCraft.Api --output-dir Data/Migrations
```

Expected: Migration files created in `src/ChordCraft.Infrastructure/Data/Migrations/`.

- [ ] **Step 2: Create Dockerfile**

`Dockerfile` at project root:
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
RUN cp -r /client/wwwroot/* /app/wwwroot/

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app .
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "ChordCraft.Api.dll"]
```

- [ ] **Step 3: Build to verify everything compiles**

```bash
cd C:/Developer/chordcraft
dotnet build
dotnet test --verbosity quiet
```

Expected: Build succeeded. All tests pass.

- [ ] **Step 4: Commit**

```bash
cd C:/Developer/chordcraft
git add -A
git commit -m "feat: add EF Core initial migration and Dockerfile for Railway deployment"
```

---

## Plan Complete

This plan produces a fully functional backend with:
- All domain entities, enums, and the CCOS layout mapping
- EF Core with PostgreSQL, migrations, and seed data (5 phases, 14 badges, 15 Phase 1 lessons)
- JWT authentication with register/login/migrate
- All API endpoints (Auth, Lessons, Progress, Badges, Chords)
- Star and points calculation with full test coverage
- CCOS layout mapping with full test coverage
- Minimal Blazor WASM client placeholder
- Dockerfile for Railway deployment

**Next plans:**
- Plan 2: Client Core (landing page, lesson catalog, auth pages, stats, badges)
- Plan 3: Typing Experience (typing interface, device visualization, hand overlay, input detection)
