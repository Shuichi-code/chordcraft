# ChordCraft Client MVP Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Build the Blazor WASM client for the ChordCraft MVP — landing page, lesson catalog, typing interface with CharaChorder One device visualization, and anonymous progress via LocalStorage. Covers Phase 1-2 lessons (Intro, Review, Practice types only).

**Architecture:** Blazor WASM standalone app with MudBlazor UI components. All progress stored in LocalStorage (no API calls). SVG-based device visualization and hand overlay rendered as Blazor components. Key input captured via JS interop.

**Tech Stack:** .NET 9 Blazor WASM, MudBlazor, SVG, JS Interop (keydown capture + audio + LocalStorage), CSS isolation

**Reference docs:**
- PRD: `docs/superpowers/specs/2026-03-18-chordcraft-design.md`
- TDD: `docs/TDD.md`
- Foundation plan: `docs/superpowers/plans/2026-03-18-foundation.md`

**MVP scope reminder:** Phase 1-2 (lessons 1-50), anonymous-only, Intro/Review/Practice types only. No auth, no stats, no badges, no chording, no Play/Video types.

---

## File Structure

```
src/ChordCraft.Client/
├── Program.cs                          (modify — add LocalStorage service)
├── App.razor                           (exists)
├── Routes.razor                        (exists)
├── _Imports.razor                      (modify — add project usings)
├── Layout/
│   └── MainLayout.razor                (replace — full nav layout)
├── Pages/
│   ├── Landing.razor                   (create — marketing landing page)
│   ├── LessonCatalog.razor             (create — lesson grid by phase)
│   └── TypingInterface.razor           (create — core typing experience)
├── Components/
│   ├── Device/
│   │   ├── SwitchDiagram.razor         (create — 18-switch SVG diagram)
│   │   ├── SwitchDiagram.razor.css     (create — switch styles)
│   │   ├── DirectionPopup.razor        (create — 5-direction compass popup)
│   │   └── HandOverlay.razor           (create — finger highlight SVG)
│   ├── Lessons/
│   │   ├── LessonCard.razor            (create — single lesson card)
│   │   ├── LessonCard.razor.css        (create — card styles)
│   │   ├── IntroLesson.razor           (create — intro lesson renderer)
│   │   ├── ReviewLesson.razor          (create — review lesson renderer)
│   │   ├── PracticeLesson.razor        (create — practice lesson renderer)
│   │   └── LessonResults.razor         (create — post-lesson star overlay)
│   └── Shared/
│       └── StarDisplay.razor           (create — reusable star rating)
├── Services/
│   ├── LocalStorageService.cs          (create — JS interop for localStorage)
│   ├── LocalProgressService.cs         (create — progress CRUD via localStorage)
│   ├── LessonDataService.cs            (create — fetches lesson data from API)
│   └── AudioService.cs                 (create — sound effects via Web Audio)
├── Models/
│   ├── LocalProgressData.cs            (create — localStorage JSON model)
│   └── TypingState.cs                  (create — typing engine state)
└── wwwroot/
    ├── index.html                      (modify — add MudBlazor + audio preload)
    ├── css/
    │   └── app.css                     (create — global styles)
    ├── js/
    │   ├── inputCapture.js             (create — keydown/keyup interop)
    │   ├── localStorage.js             (create — localStorage interop)
    │   └── audio.js                    (create — Web Audio API interop)
    └── audio/
        ├── keypress.mp3                (create — placeholder/silence)
        ├── error.mp3                   (create — placeholder/silence)
        └── success.mp3                 (create — placeholder/silence)
```

---

### Task 1: JS Interop Layer

**Files:**
- Create: `src/ChordCraft.Client/wwwroot/js/localStorage.js`
- Create: `src/ChordCraft.Client/wwwroot/js/inputCapture.js`
- Create: `src/ChordCraft.Client/wwwroot/js/audio.js`
- Modify: `src/ChordCraft.Client/wwwroot/index.html`

- [ ] **Step 1: Create localStorage.js**

`src/ChordCraft.Client/wwwroot/js/localStorage.js`:
```javascript
window.ChordCraftStorage = {
    get: function (key) {
        return localStorage.getItem(key);
    },
    set: function (key, value) {
        localStorage.setItem(key, value);
    },
    remove: function (key) {
        localStorage.removeItem(key);
    }
};
```

- [ ] **Step 2: Create inputCapture.js**

`src/ChordCraft.Client/wwwroot/js/inputCapture.js`:
```javascript
window.ChordCraftInput = {
    _dotNetRef: null,

    register: function (dotNetRef) {
        this._dotNetRef = dotNetRef;
        this._onKeyDown = (e) => {
            if (e.key.length === 1 || e.key === 'Backspace' || e.key === 'Tab' || e.key === 'Enter' || e.key === ' ') {
                e.preventDefault();
                dotNetRef.invokeMethodAsync('OnKeyDown', e.key, performance.now());
            }
        };
        document.addEventListener('keydown', this._onKeyDown);
    },

    unregister: function () {
        if (this._onKeyDown) {
            document.removeEventListener('keydown', this._onKeyDown);
            this._onKeyDown = null;
        }
        this._dotNetRef = null;
    }
};
```

- [ ] **Step 3: Create audio.js**

`src/ChordCraft.Client/wwwroot/js/audio.js`:
```javascript
window.ChordCraftAudio = {
    _ctx: null,
    _buffers: {},
    _enabled: true,

    init: async function () {
        try {
            this._ctx = new AudioContext();
        } catch (e) {
            console.warn('Web Audio not available');
        }
    },

    toggle: function () {
        this._enabled = !this._enabled;
        return this._enabled;
    },

    playTone: function (frequency, duration, type) {
        if (!this._ctx || !this._enabled) return;
        var osc = this._ctx.createOscillator();
        var gain = this._ctx.createGain();
        osc.type = type || 'sine';
        osc.frequency.value = frequency;
        gain.gain.value = 0.1;
        gain.gain.exponentialRampToValueAtTime(0.001, this._ctx.currentTime + duration);
        osc.connect(gain);
        gain.connect(this._ctx.destination);
        osc.start();
        osc.stop(this._ctx.currentTime + duration);
    },

    playKeypress: function () { this.playTone(800, 0.05, 'sine'); },
    playError: function () { this.playTone(200, 0.15, 'square'); },
    playSuccess: function () {
        this.playTone(523, 0.1, 'sine');
        setTimeout(() => this.playTone(659, 0.1, 'sine'), 100);
        setTimeout(() => this.playTone(784, 0.2, 'sine'), 200);
    }
};
```

- [ ] **Step 4: Update index.html to load JS files**

Add before the closing `</body>` tag in `src/ChordCraft.Client/wwwroot/index.html`:
```html
    <script src="js/localStorage.js"></script>
    <script src="js/inputCapture.js"></script>
    <script src="js/audio.js"></script>
```

- [ ] **Step 5: Build to verify**

```bash
cd C:/Developer/chordcraft && dotnet build src/ChordCraft.Client
```

- [ ] **Step 6: Commit**

```bash
git add src/ChordCraft.Client/wwwroot
git commit -m "feat: add JS interop layer (localStorage, input capture, audio)"
```

---

### Task 2: Client Services

**Files:**
- Create: `src/ChordCraft.Client/Services/LocalStorageService.cs`
- Create: `src/ChordCraft.Client/Services/AudioService.cs`
- Create: `src/ChordCraft.Client/Services/LessonDataService.cs`
- Create: `src/ChordCraft.Client/Services/LocalProgressService.cs`
- Create: `src/ChordCraft.Client/Models/LocalProgressData.cs`
- Create: `src/ChordCraft.Client/Models/TypingState.cs`
- Modify: `src/ChordCraft.Client/Program.cs`
- Modify: `src/ChordCraft.Client/_Imports.razor`

- [ ] **Step 1: Create LocalStorageService**

`src/ChordCraft.Client/Services/LocalStorageService.cs`:
```csharp
using Microsoft.JSInterop;

namespace ChordCraft.Client.Services;

public class LocalStorageService
{
    private readonly IJSRuntime _js;
    public LocalStorageService(IJSRuntime js) => _js = js;

    public async Task<string?> GetAsync(string key)
        => await _js.InvokeAsync<string?>("ChordCraftStorage.get", key);

    public async Task SetAsync(string key, string value)
        => await _js.InvokeVoidAsync("ChordCraftStorage.set", key, value);

    public async Task RemoveAsync(string key)
        => await _js.InvokeVoidAsync("ChordCraftStorage.remove", key);
}
```

- [ ] **Step 2: Create AudioService**

`src/ChordCraft.Client/Services/AudioService.cs`:
```csharp
using Microsoft.JSInterop;

namespace ChordCraft.Client.Services;

public class AudioService
{
    private readonly IJSRuntime _js;
    private bool _initialized;

    public AudioService(IJSRuntime js) => _js = js;

    public async Task InitAsync()
    {
        if (!_initialized)
        {
            await _js.InvokeVoidAsync("ChordCraftAudio.init");
            _initialized = true;
        }
    }

    public async Task PlayKeypressAsync() => await _js.InvokeVoidAsync("ChordCraftAudio.playKeypress");
    public async Task PlayErrorAsync() => await _js.InvokeVoidAsync("ChordCraftAudio.playError");
    public async Task PlaySuccessAsync() => await _js.InvokeVoidAsync("ChordCraftAudio.playSuccess");
    public async Task<bool> ToggleAsync() => await _js.InvokeAsync<bool>("ChordCraftAudio.toggle");
}
```

- [ ] **Step 3: Create LocalProgressData model**

`src/ChordCraft.Client/Models/LocalProgressData.cs`:
```csharp
namespace ChordCraft.Client.Models;

public class LocalProgressData
{
    public string SessionId { get; set; } = Guid.NewGuid().ToString();
    public Dictionary<int, LessonProgressEntry> Progress { get; set; } = new();
    public DateTime LastActiveAt { get; set; } = DateTime.UtcNow;
}

public class LessonProgressEntry
{
    public int BestStars { get; set; }
    public decimal BestAccuracy { get; set; }
    public decimal BestSpeed { get; set; }
    public int TotalAttempts { get; set; }
}
```

- [ ] **Step 4: Create TypingState model**

`src/ChordCraft.Client/Models/TypingState.cs`:
```csharp
namespace ChordCraft.Client.Models;

public class TypingState
{
    public int CurrentPosition { get; set; }
    public List<TypedCharacter> TypedCharacters { get; set; } = [];
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public bool IsComplete { get; set; }

    public decimal Accuracy => TypedCharacters.Count == 0 ? 0
        : (decimal)TypedCharacters.Count(c => c.IsCorrect) / TypedCharacters.Count;

    public decimal Speed
    {
        get
        {
            if (StartTime is null || EndTime is null || EndTime == StartTime) return 0;
            var minutes = (decimal)(EndTime.Value - StartTime.Value).TotalMinutes;
            if (minutes <= 0) return 0;
            var correctChars = TypedCharacters.Count(c => c.IsCorrect);
            return correctChars / 5m / minutes; // WPM = chars / 5 / minutes
        }
    }
}

public record TypedCharacter(char Expected, char Actual, bool IsCorrect, double TimestampMs);
```

- [ ] **Step 5: Create LessonDataService**

`src/ChordCraft.Client/Services/LessonDataService.cs`:
```csharp
using System.Net.Http.Json;
using ChordCraft.Core.DTOs.Lessons;

namespace ChordCraft.Client.Services;

public class LessonDataService
{
    private readonly HttpClient _http;
    private List<PhaseDto>? _cachedPhases;

    public LessonDataService(HttpClient http) => _http = http;

    public async Task<List<PhaseDto>> GetPhasesAsync()
    {
        _cachedPhases ??= await _http.GetFromJsonAsync<List<PhaseDto>>("api/phases") ?? [];
        return _cachedPhases;
    }

    public async Task<LessonDetailDto?> GetLessonAsync(int id)
        => await _http.GetFromJsonAsync<LessonDetailDto>($"api/lessons/{id}");
}
```

- [ ] **Step 6: Create LocalProgressService**

`src/ChordCraft.Client/Services/LocalProgressService.cs`:
```csharp
using System.Text.Json;
using ChordCraft.Client.Models;
using ChordCraft.Core.Scoring;

namespace ChordCraft.Client.Services;

public class LocalProgressService
{
    private readonly LocalStorageService _storage;
    private const string StorageKey = "chordcraft_progress";
    private LocalProgressData? _cache;

    public LocalProgressService(LocalStorageService storage) => _storage = storage;

    public async Task<LocalProgressData> LoadAsync()
    {
        if (_cache is not null) return _cache;
        var json = await _storage.GetAsync(StorageKey);
        _cache = json is not null
            ? JsonSerializer.Deserialize<LocalProgressData>(json) ?? new()
            : new();
        return _cache;
    }

    public async Task SaveAsync()
    {
        if (_cache is null) return;
        _cache.LastActiveAt = DateTime.UtcNow;
        var json = JsonSerializer.Serialize(_cache);
        await _storage.SetAsync(StorageKey, json);
    }

    public async Task RecordAttemptAsync(int lessonId, decimal accuracy, decimal speed, int stars)
    {
        var data = await LoadAsync();
        if (data.Progress.TryGetValue(lessonId, out var entry))
        {
            entry.TotalAttempts++;
            if (stars > entry.BestStars) entry.BestStars = stars;
            if (accuracy > entry.BestAccuracy) entry.BestAccuracy = accuracy;
            if (speed > entry.BestSpeed) entry.BestSpeed = speed;
        }
        else
        {
            data.Progress[lessonId] = new LessonProgressEntry
            {
                BestStars = stars,
                BestAccuracy = accuracy,
                BestSpeed = speed,
                TotalAttempts = 1
            };
        }
        await SaveAsync();
    }

    public async Task<bool> IsLessonUnlockedAsync(int lessonNumber)
    {
        if (lessonNumber <= 1) return true;
        var data = await LoadAsync();
        // Find the lesson with Number = lessonNumber - 1
        // Since we only have IDs in progress, we need lesson number → ID mapping
        // This is handled by the catalog page which passes unlock state
        return true; // Simplified: catalog computes unlock from progress data
    }

    public async Task<int> GetTotalStarsAsync()
    {
        var data = await LoadAsync();
        return data.Progress.Values.Sum(p => p.BestStars);
    }

    public async Task<int> GetTotalPointsAsync()
    {
        var data = await LoadAsync();
        return data.Progress.Values.Sum(p => p.BestStars * 10); // Simplified
    }

    public async Task<decimal> GetCompletionPercentAsync(int totalLessons)
    {
        var data = await LoadAsync();
        var completed = data.Progress.Values.Count(p => p.BestStars >= 1);
        return totalLessons > 0 ? (decimal)completed / totalLessons * 100 : 0;
    }
}
```

- [ ] **Step 7: Register services in Program.cs**

Add to `src/ChordCraft.Client/Program.cs` before `await builder.Build().RunAsync();`:
```csharp
using ChordCraft.Client.Services;

builder.Services.AddScoped<LocalStorageService>();
builder.Services.AddScoped<AudioService>();
builder.Services.AddScoped<LessonDataService>();
builder.Services.AddScoped<LocalProgressService>();
```

- [ ] **Step 8: Update _Imports.razor**

Add these to `src/ChordCraft.Client/_Imports.razor`:
```razor
@using ChordCraft.Client.Services
@using ChordCraft.Client.Models
@using ChordCraft.Client.Components.Device
@using ChordCraft.Client.Components.Lessons
@using ChordCraft.Client.Components.Shared
@using ChordCraft.Core.Enums
@using ChordCraft.Core.Models
@using ChordCraft.Core.DTOs.Lessons
```

- [ ] **Step 9: Build to verify**

```bash
cd C:/Developer/chordcraft && dotnet build src/ChordCraft.Client
```

- [ ] **Step 10: Commit**

```bash
git add src/ChordCraft.Client
git commit -m "feat: add client services (localStorage, progress, lessons, audio) and models"
```

---

### Task 3: MainLayout and Landing Page

**Files:**
- Modify: `src/ChordCraft.Client/Layout/MainLayout.razor`
- Create: `src/ChordCraft.Client/Pages/Landing.razor`

- [ ] **Step 1: Replace MainLayout.razor**

`src/ChordCraft.Client/Layout/MainLayout.razor`:
```razor
@inherits LayoutComponentBase
@using MudBlazor

<MudThemeProvider Theme="_theme" />
<MudPopoverProvider />
<MudDialogProvider />
<MudSnackbarProvider />

<MudLayout>
    <MudAppBar Elevation="1" Color="Color.Dark">
        <MudText Typo="Typo.h5" Class="ml-3 cursor-pointer" @onclick="@(() => Navigation.NavigateTo("/"))">
            ChordCraft
        </MudText>
        <MudSpacer />
        <MudButton Variant="Variant.Text" Color="Color.Inherit" Href="/lessons">Lessons</MudButton>
    </MudAppBar>
    <MudMainContent>
        @Body
    </MudMainContent>
</MudLayout>

@code {
    [Inject] private NavigationManager Navigation { get; set; } = null!;

    private readonly MudTheme _theme = new()
    {
        PaletteLight = new PaletteLight
        {
            Primary = "#1976d2",
            Secondary = "#424242",
            AppbarBackground = "#2d3748",
            Background = "#f5f7fa",
            Surface = "#ffffff"
        }
    };
}
```

- [ ] **Step 2: Create Landing.razor**

`src/ChordCraft.Client/Pages/Landing.razor`:
```razor
@page "/"
@using MudBlazor

<MudContainer MaxWidth="MaxWidth.Large" Class="mt-8">
    @* Hero *@
    <MudPaper Class="pa-12 mb-8" Elevation="0"
              Style="background: linear-gradient(135deg, #1976d2, #1565c0); color: white; border-radius: 16px;">
        <MudText Typo="Typo.h2" Align="Align.Center" Class="mb-4" Style="font-weight: 700;">
            Learn CharaChorder Typing
        </MudText>
        <MudText Typo="Typo.h5" Align="Align.Center" Class="mb-6" Style="opacity: 0.9;">
            The most effective way to master your CharaChorder One.
            Structured lessons, real-time feedback, and visual guides.
        </MudText>
        <MudStack Row="true" Justify="Justify.Center">
            <MudButton Variant="Variant.Filled" Color="Color.Surface"
                       Size="Size.Large" Href="/lessons"
                       Style="color: #1976d2; font-weight: 600;">
                Get Started
            </MudButton>
        </MudStack>
    </MudPaper>

    @* What is ChordCraft *@
    <MudText Typo="Typo.h4" Align="Align.Center" Class="mb-2" Style="font-weight: 600;">
        What is ChordCraft?
    </MudText>
    <MudText Typo="Typo.body1" Align="Align.Center" Class="mb-8" Style="max-width: 700px; margin: 0 auto;">
        ChordCraft is a free, web-based typing tutor built specifically for the CharaChorder One.
        Learn to type on your device through structured, gamified lessons — from your first switch press
        to full directional mastery.
    </MudText>

    @* Features *@
    <MudGrid Class="mb-8">
        <MudItem xs="12" sm="6" md="4">
            <MudCard Elevation="2" Class="pa-4" Style="height: 100%;">
                <MudIcon Icon="@Icons.Material.Filled.School" Size="Size.Large" Color="Color.Primary" />
                <MudText Typo="Typo.h6" Class="mt-2">Structured Lessons</MudText>
                <MudText Typo="Typo.body2">50+ lessons organized into phases. Learn switch basics, then master all directional inputs.</MudText>
            </MudCard>
        </MudItem>
        <MudItem xs="12" sm="6" md="4">
            <MudCard Elevation="2" Class="pa-4" Style="height: 100%;">
                <MudIcon Icon="@Icons.Material.Filled.Keyboard" Size="Size.Large" Color="Color.Primary" />
                <MudText Typo="Typo.h6" Class="mt-2">Device Visualization</MudText>
                <MudText Typo="Typo.body2">See exactly which switch to press and in which direction with our interactive CharaChorder diagram.</MudText>
            </MudCard>
        </MudItem>
        <MudItem xs="12" sm="6" md="4">
            <MudCard Elevation="2" Class="pa-4" Style="height: 100%;">
                <MudIcon Icon="@Icons.Material.Filled.FrontHand" Size="Size.Large" Color="Color.Primary" />
                <MudText Typo="Typo.h6" Class="mt-2">Finger Guides</MudText>
                <MudText Typo="Typo.body2">Hand overlays show you which finger to use and which direction to push — just like a typing teacher.</MudText>
            </MudCard>
        </MudItem>
        <MudItem xs="12" sm="6" md="4">
            <MudCard Elevation="2" Class="pa-4" Style="height: 100%;">
                <MudIcon Icon="@Icons.Material.Filled.Star" Size="Size.Large" Color="Color.Primary" />
                <MudText Typo="Typo.h6" Class="mt-2">Star Ratings</MudText>
                <MudText Typo="Typo.body2">Earn 1-5 stars per lesson based on accuracy and speed. Keep practicing to get all five!</MudText>
            </MudCard>
        </MudItem>
        <MudItem xs="12" sm="6" md="4">
            <MudCard Elevation="2" Class="pa-4" Style="height: 100%;">
                <MudIcon Icon="@Icons.Material.Filled.TrendingUp" Size="Size.Large" Color="Color.Primary" />
                <MudText Typo="Typo.h6" Class="mt-2">Track Progress</MudText>
                <MudText Typo="Typo.body2">Your progress saves automatically. Pick up right where you left off.</MudText>
            </MudCard>
        </MudItem>
        <MudItem xs="12" sm="6" md="4">
            <MudCard Elevation="2" Class="pa-4" Style="height: 100%;">
                <MudIcon Icon="@Icons.Material.Filled.Devices" Size="Size.Large" Color="Color.Primary" />
                <MudText Typo="Typo.h6" Class="mt-2">Just Plug In</MudText>
                <MudText Typo="Typo.body2">Works with any CharaChorder One on the default CCOS layout. No drivers needed.</MudText>
            </MudCard>
        </MudItem>
    </MudGrid>

    @* How it works *@
    <MudPaper Class="pa-8 mb-8" Elevation="1" Style="border-radius: 12px; text-align: center;">
        <MudText Typo="Typo.h5" Class="mb-4" Style="font-weight: 600;">How Does It Work?</MudText>
        <MudText Typo="Typo.body1" Style="max-width: 600px; margin: 0 auto;">
            Practice each lesson until you earn all five stars. It really doesn't take much to learn —
            a few minutes a day for one to two weeks and you'll have mastered the basics!
        </MudText>
        <MudButton Variant="Variant.Filled" Color="Color.Primary" Size="Size.Large"
                   Class="mt-6" Href="/lessons">
            Start Learning
        </MudButton>
    </MudPaper>

    @* Footer *@
    <MudText Typo="Typo.caption" Align="Align.Center" Class="mb-4" Style="color: #999;">
        ChordCraft is a free, open-source community project. Not affiliated with CharaChorder Inc.
    </MudText>
</MudContainer>
```

- [ ] **Step 3: Build to verify**

```bash
cd C:/Developer/chordcraft && dotnet build src/ChordCraft.Client
```

- [ ] **Step 4: Commit**

```bash
git add src/ChordCraft.Client
git commit -m "feat: add MainLayout with theme and Landing page with hero and features"
```

---

### Task 4: Lesson Catalog Page

**Files:**
- Create: `src/ChordCraft.Client/Components/Shared/StarDisplay.razor`
- Create: `src/ChordCraft.Client/Components/Lessons/LessonCard.razor`
- Create: `src/ChordCraft.Client/Components/Lessons/LessonCard.razor.css`
- Create: `src/ChordCraft.Client/Pages/LessonCatalog.razor`

- [ ] **Step 1: Create StarDisplay component**

`src/ChordCraft.Client/Components/Shared/StarDisplay.razor`:
```razor
@using MudBlazor

<span class="star-display">
    @for (int i = 1; i <= 5; i++)
    {
        var starNum = i;
        <MudIcon Icon="@(starNum <= Stars ? Icons.Material.Filled.Star : Icons.Material.Outlined.StarBorder)"
                 Size="Size" Color="@(starNum <= Stars ? Color.Warning : Color.Default)" />
    }
</span>

@code {
    [Parameter] public int Stars { get; set; }
    [Parameter] public Size Size { get; set; } = Size.Small;
}
```

- [ ] **Step 2: Create LessonCard component**

`src/ChordCraft.Client/Components/Lessons/LessonCard.razor`:
```razor
@using MudBlazor

<MudCard Class="@CardClass" Elevation="@(IsUnlocked ? 2 : 0)" Style="@CardStyle"
         @onclick="OnClick">
    <MudCardContent Class="pa-3 text-center">
        <MudText Typo="Typo.h6" Style="font-weight: 700;">@Lesson.Number</MudText>

        @if (!IsUnlocked)
        {
            <MudIcon Icon="@Icons.Material.Filled.Lock" Size="Size.Medium" Color="Color.Default" />
        }
        else
        {
            <MudIcon Icon="@TypeIcon" Size="Size.Medium" Color="Color.Primary" />
        }

        <MudText Typo="Typo.caption" Class="mt-1" Style="@(IsUnlocked ? "" : "opacity: 0.5;")">
            @Lesson.Title
        </MudText>

        @if (BestStars > 0)
        {
            <StarDisplay Stars="BestStars" Size="Size.Small" />
        }
    </MudCardContent>
</MudCard>

@code {
    [Parameter, EditorRequired] public LessonSummaryDto Lesson { get; set; } = null!;
    [Parameter] public bool IsUnlocked { get; set; }
    [Parameter] public bool IsActive { get; set; }
    [Parameter] public int BestStars { get; set; }
    [Parameter] public EventCallback OnClick { get; set; }

    private string CardClass => IsActive ? "lesson-card active" : "lesson-card";
    private string CardStyle => IsUnlocked
        ? (IsActive ? "border: 2px solid #1976d2; cursor: pointer;" : "cursor: pointer;")
        : "opacity: 0.6; cursor: default;";

    private string TypeIcon => Lesson.Type switch
    {
        LessonType.Intro => Icons.Material.Filled.NewReleases,
        LessonType.Review => Icons.Material.Filled.Search,
        LessonType.Practice => Icons.Material.Filled.Timer,
        _ => Icons.Material.Filled.School
    };
}
```

- [ ] **Step 3: Create LessonCard.razor.css**

`src/ChordCraft.Client/Components/Lessons/LessonCard.razor.css`:
```css
.lesson-card {
    transition: transform 0.2s, box-shadow 0.2s;
    width: 140px;
    min-height: 130px;
}
.lesson-card:hover {
    transform: translateY(-2px);
}
.lesson-card.active {
    animation: pulse 2s infinite;
}
@keyframes pulse {
    0%, 100% { box-shadow: 0 0 0 0 rgba(25, 118, 210, 0.4); }
    50% { box-shadow: 0 0 0 8px rgba(25, 118, 210, 0); }
}
```

- [ ] **Step 4: Create LessonCatalog page**

`src/ChordCraft.Client/Pages/LessonCatalog.razor`:
```razor
@page "/lessons"
@using MudBlazor
@inject LessonDataService LessonData
@inject LocalProgressService Progress
@inject NavigationManager Navigation

<MudContainer MaxWidth="MaxWidth.Large" Class="mt-4">
    @* Progress summary *@
    <MudPaper Class="pa-3 mb-4" Elevation="1">
        <MudStack Row="true" Spacing="4" Justify="Justify.Center">
            <MudText Typo="Typo.body2">
                <strong>@completionPercent.ToString("0")%</strong> progress
            </MudText>
            <MudText Typo="Typo.body2">|</MudText>
            <MudText Typo="Typo.body2">
                <MudIcon Icon="@Icons.Material.Filled.Star" Size="Size.Small" Color="Color.Warning" />
                <strong>@totalStars</strong> stars
            </MudText>
        </MudStack>
    </MudPaper>

    @if (phases is null)
    {
        <MudProgressCircular Indeterminate="true" Color="Color.Primary" Class="mx-auto d-block mt-8" />
    }
    else
    {
        @foreach (var phase in phases)
        {
            <MudText Typo="Typo.h5" Class="mt-6 mb-3" Style="font-weight: 600;">@phase.Name</MudText>
            <MudText Typo="Typo.body2" Class="mb-3" Style="color: #666;">@phase.Description</MudText>

            <MudStack Row="true" Wrap="Wrap.Wrap" Spacing="2" Class="mb-4">
                @foreach (var lesson in phase.Lessons)
                {
                    var isUnlocked = IsLessonUnlocked(lesson);
                    var isActive = isUnlocked && !HasCompleted(lesson.Id) && IsFirstUncompletedLesson(lesson);
                    var bestStars = GetBestStars(lesson.Id);

                    <LessonCard Lesson="lesson"
                                IsUnlocked="isUnlocked"
                                IsActive="isActive"
                                BestStars="bestStars"
                                OnClick="@(() => OpenLesson(lesson, isUnlocked))" />
                }
            </MudStack>
        }
    }
</MudContainer>

@code {
    private List<PhaseDto>? phases;
    private LocalProgressData? progressData;
    private int totalStars;
    private decimal completionPercent;
    private int? firstUncompletedLessonId;

    protected override async Task OnInitializedAsync()
    {
        phases = await LessonData.GetPhasesAsync();
        progressData = await Progress.LoadAsync();
        totalStars = await Progress.GetTotalStarsAsync();

        var totalLessons = phases?.Sum(p => p.Lessons.Count) ?? 0;
        completionPercent = await Progress.GetCompletionPercentAsync(totalLessons);

        // Find the first uncompleted lesson
        if (phases is not null)
        {
            foreach (var phase in phases)
            {
                foreach (var lesson in phase.Lessons)
                {
                    if (IsLessonUnlocked(lesson) && !HasCompleted(lesson.Id))
                    {
                        firstUncompletedLessonId = lesson.Id;
                        goto found;
                    }
                }
            }
            found:;
        }
    }

    private bool IsLessonUnlocked(LessonSummaryDto lesson)
    {
        if (lesson.Number <= 1) return true;
        if (phases is null || progressData is null) return false;

        // Find previous lesson
        var allLessons = phases.SelectMany(p => p.Lessons).OrderBy(l => l.Number).ToList();
        var prevLesson = allLessons.FirstOrDefault(l => l.Number == lesson.Number - 1);
        if (prevLesson is null) return true;

        return progressData.Progress.TryGetValue(prevLesson.Id, out var p) && p.BestStars >= 1;
    }

    private bool HasCompleted(int lessonId)
        => progressData?.Progress.TryGetValue(lessonId, out var p) == true && p.BestStars >= 1;

    private bool IsFirstUncompletedLesson(LessonSummaryDto lesson)
        => firstUncompletedLessonId == lesson.Id;

    private int GetBestStars(int lessonId)
        => progressData?.Progress.TryGetValue(lessonId, out var p) == true ? p.BestStars : 0;

    private void OpenLesson(LessonSummaryDto lesson, bool isUnlocked)
    {
        if (isUnlocked) Navigation.NavigateTo($"/lessons/{lesson.Id}");
    }
}
```

- [ ] **Step 5: Build to verify**

```bash
cd C:/Developer/chordcraft && dotnet build src/ChordCraft.Client
```

- [ ] **Step 6: Commit**

```bash
git add src/ChordCraft.Client
git commit -m "feat: add lesson catalog page with progress tracking and unlock logic"
```

---

### Task 5: SwitchDiagram and HandOverlay Components

**Files:**
- Create: `src/ChordCraft.Client/Components/Device/SwitchDiagram.razor`
- Create: `src/ChordCraft.Client/Components/Device/SwitchDiagram.razor.css`
- Create: `src/ChordCraft.Client/Components/Device/DirectionPopup.razor`
- Create: `src/ChordCraft.Client/Components/Device/HandOverlay.razor`

- [ ] **Step 1: Create SwitchDiagram.razor**

`src/ChordCraft.Client/Components/Device/SwitchDiagram.razor`:
```razor
<svg viewBox="0 0 800 200" class="switch-diagram" xmlns="http://www.w3.org/2000/svg">
    @* Left hand switches *@
    @foreach (var (switchId, pos) in LeftHandPositions)
    {
        var isActive = ActiveInputs.Any(a => a.Switch == switchId);
        var pushChar = CcosLayout.Map.GetValueOrDefault(new SwitchInput(switchId, SwitchDirection.Push)) ?? "";
        var label = GetSwitchLabel(switchId);

        <g transform="translate(@pos.X, @pos.Y)">
            <circle r="28" class="@(isActive ? "switch active" : "switch")"
                    fill="@(isActive ? "#1976d2" : "#e0e0e0")"
                    stroke="@(isActive ? "#1565c0" : "#bbb")" stroke-width="2" />
            <text text-anchor="middle" dy="5" class="switch-char"
                  fill="@(isActive ? "white" : "#555")" font-size="14" font-weight="bold">
                @(pushChar.Length == 1 && !char.IsControl(pushChar[0]) ? pushChar : label)
            </text>
            <text text-anchor="middle" dy="-34" class="switch-label"
                  fill="#999" font-size="9">@label</text>

            @if (isActive)
            {
                <DirectionPopup SwitchId="switchId"
                                ActiveDirection="@ActiveInputs.FirstOrDefault(a => a.Switch == switchId)?.Direction" />
            }
        </g>
    }

    @* Right hand switches *@
    @foreach (var (switchId, pos) in RightHandPositions)
    {
        var isActive = ActiveInputs.Any(a => a.Switch == switchId);
        var pushChar = CcosLayout.Map.GetValueOrDefault(new SwitchInput(switchId, SwitchDirection.Push)) ?? "";
        var label = GetSwitchLabel(switchId);

        <g transform="translate(@pos.X, @pos.Y)">
            <circle r="28" class="@(isActive ? "switch active" : "switch")"
                    fill="@(isActive ? "#1976d2" : "#e0e0e0")"
                    stroke="@(isActive ? "#1565c0" : "#bbb")" stroke-width="2" />
            <text text-anchor="middle" dy="5" class="switch-char"
                  fill="@(isActive ? "white" : "#555")" font-size="14" font-weight="bold">
                @(pushChar.Length == 1 && !char.IsControl(pushChar[0]) ? pushChar : label)
            </text>
            <text text-anchor="middle" dy="-34" class="switch-label"
                  fill="#999" font-size="9">@label</text>

            @if (isActive)
            {
                <DirectionPopup SwitchId="switchId"
                                ActiveDirection="@ActiveInputs.FirstOrDefault(a => a.Switch == switchId)?.Direction" />
            }
        </g>
    }
</svg>

@code {
    [Parameter] public List<SwitchInput> ActiveInputs { get; set; } = [];

    private static readonly List<(SwitchId Id, (int X, int Y) Pos)> LeftHandPositions =
    [
        (SwitchId.L_Pinky,      (50,  100)),
        (SwitchId.L_Ring,       (110, 80)),
        (SwitchId.L_Middle,     (170, 65)),
        (SwitchId.L_Index,      (230, 80)),
        (SwitchId.L_Thumb,      (260, 150)),
        (SwitchId.L_ThumbInner, (200, 155)),
        (SwitchId.L_PalmUpper,  (130, 145)),
        (SwitchId.L_PalmLower,  (70,  155)),
        (SwitchId.L_Edge,       (30,  145)),
    ];

    private static readonly List<(SwitchId Id, (int X, int Y) Pos)> RightHandPositions =
    [
        (SwitchId.R_Thumb,      (540, 150)),
        (SwitchId.R_ThumbInner, (600, 155)),
        (SwitchId.R_Index,      (570, 80)),
        (SwitchId.R_Middle,     (630, 65)),
        (SwitchId.R_Ring,       (690, 80)),
        (SwitchId.R_Pinky,      (750, 100)),
        (SwitchId.R_PalmUpper,  (670, 145)),
        (SwitchId.R_PalmLower,  (730, 155)),
        (SwitchId.R_Edge,       (770, 145)),
    ];

    private static string GetSwitchLabel(SwitchId id) => id switch
    {
        SwitchId.L_Pinky => "pinky", SwitchId.L_Ring => "ring", SwitchId.L_Middle => "mid",
        SwitchId.L_Index => "index", SwitchId.L_Thumb => "thumb", SwitchId.L_ThumbInner => "thIn",
        SwitchId.L_PalmUpper => "pUp", SwitchId.L_PalmLower => "pLo", SwitchId.L_Edge => "edge",
        SwitchId.R_Pinky => "pinky", SwitchId.R_Ring => "ring", SwitchId.R_Middle => "mid",
        SwitchId.R_Index => "index", SwitchId.R_Thumb => "thumb", SwitchId.R_ThumbInner => "thIn",
        SwitchId.R_PalmUpper => "pUp", SwitchId.R_PalmLower => "pLo", SwitchId.R_Edge => "edge",
        _ => ""
    };
}
```

- [ ] **Step 2: Create SwitchDiagram.razor.css**

`src/ChordCraft.Client/Components/Device/SwitchDiagram.razor.css`:
```css
.switch-diagram {
    width: 100%;
    max-width: 800px;
    margin: 0 auto;
    display: block;
}
.switch {
    transition: fill 0.2s, stroke 0.2s;
}
.switch.active {
    filter: drop-shadow(0 0 8px rgba(25, 118, 210, 0.5));
    animation: switchPulse 1.5s infinite;
}
@keyframes switchPulse {
    0%, 100% { filter: drop-shadow(0 0 8px rgba(25, 118, 210, 0.5)); }
    50% { filter: drop-shadow(0 0 16px rgba(25, 118, 210, 0.8)); }
}
```

- [ ] **Step 3: Create DirectionPopup.razor**

`src/ChordCraft.Client/Components/Device/DirectionPopup.razor`:
```razor
<g class="direction-popup" transform="translate(0, -75)">
    <rect x="-45" y="-35" width="90" height="70" rx="8" ry="8"
          fill="#2d3748" stroke="#1976d2" stroke-width="1.5" opacity="0.95" />

    @* Up *@
    <text text-anchor="middle" y="-18"
          fill="@(ActiveDirection == SwitchDirection.Up ? "#4fc3f7" : "#aaa")"
          font-size="@(ActiveDirection == SwitchDirection.Up ? "12" : "10")"
          font-weight="@(ActiveDirection == SwitchDirection.Up ? "bold" : "normal")">
        @GetChar(SwitchDirection.Up)
    </text>

    @* Left / Push / Right *@
    <text text-anchor="end" x="-12" y="5"
          fill="@(ActiveDirection == SwitchDirection.Left ? "#4fc3f7" : "#aaa")"
          font-size="@(ActiveDirection == SwitchDirection.Left ? "12" : "10")"
          font-weight="@(ActiveDirection == SwitchDirection.Left ? "bold" : "normal")">
        @GetChar(SwitchDirection.Left)
    </text>

    <text text-anchor="middle" y="5"
          fill="@(ActiveDirection == SwitchDirection.Push ? "#4fc3f7" : "#ccc")"
          font-size="@(ActiveDirection == SwitchDirection.Push ? "13" : "11")"
          font-weight="bold">
        @GetChar(SwitchDirection.Push)
    </text>

    <text text-anchor="start" x="12" y="5"
          fill="@(ActiveDirection == SwitchDirection.Right ? "#4fc3f7" : "#aaa")"
          font-size="@(ActiveDirection == SwitchDirection.Right ? "12" : "10")"
          font-weight="@(ActiveDirection == SwitchDirection.Right ? "bold" : "normal")">
        @GetChar(SwitchDirection.Right)
    </text>

    @* Down *@
    <text text-anchor="middle" y="28"
          fill="@(ActiveDirection == SwitchDirection.Down ? "#4fc3f7" : "#aaa")"
          font-size="@(ActiveDirection == SwitchDirection.Down ? "12" : "10")"
          font-weight="@(ActiveDirection == SwitchDirection.Down ? "bold" : "normal")">
        @GetChar(SwitchDirection.Down)
    </text>

    @* Arrow pointing down to switch *@
    <line x1="0" y1="35" x2="0" y2="45" stroke="#1976d2" stroke-width="1.5" />
</g>

@code {
    [Parameter, EditorRequired] public SwitchId SwitchId { get; set; }
    [Parameter] public SwitchDirection? ActiveDirection { get; set; }

    private string GetChar(SwitchDirection dir)
    {
        var val = CcosLayout.Map.GetValueOrDefault(new SwitchInput(SwitchId, dir)) ?? "?";
        if (val.Length == 1 && char.IsControl(val[0]))
        {
            return dir switch
            {
                SwitchDirection.Push => "SPC",
                SwitchDirection.Up => "BS",
                SwitchDirection.Down => "TAB",
                SwitchDirection.Left => "DEL",
                SwitchDirection.Right => "ENT",
                _ => "?"
            };
        }
        return val.Length > 3 ? val[..3] : val;
    }
}
```

- [ ] **Step 4: Create HandOverlay.razor**

`src/ChordCraft.Client/Components/Device/HandOverlay.razor`:
```razor
<svg viewBox="0 0 800 200" class="hand-overlay" xmlns="http://www.w3.org/2000/svg"
     style="position: absolute; top: 0; left: 0; width: 100%; height: 100%; pointer-events: none;">

    @* Left hand - simplified finger indicators *@
    @foreach (var (switchId, pos) in FingerPositions.Where(f => f.Id.ToString().StartsWith("L_")))
    {
        var isActive = ActiveFingers.Contains(switchId);
        <circle cx="@pos.X" cy="@(pos.Y + 40)" r="15"
                fill="@(isActive ? "rgba(25, 118, 210, 0.3)" : "rgba(150, 150, 150, 0.15)")"
                stroke="@(isActive ? "#1976d2" : "transparent")" stroke-width="1.5"
                style="transition: fill 0.3s, stroke 0.3s;" />

        @if (isActive && ActiveDirection.HasValue)
        {
            @* Direction arrow *@
            var (dx, dy) = GetArrowOffset(ActiveDirection.Value);
            <line x1="@pos.X" y1="@(pos.Y + 40)"
                  x2="@(pos.X + dx)" y2="@(pos.Y + 40 + dy)"
                  stroke="#1976d2" stroke-width="2.5" stroke-linecap="round"
                  marker-end="url(#arrowhead)" />
        }
    }

    @* Right hand *@
    @foreach (var (switchId, pos) in FingerPositions.Where(f => f.Id.ToString().StartsWith("R_")))
    {
        var isActive = ActiveFingers.Contains(switchId);
        <circle cx="@pos.X" cy="@(pos.Y + 40)" r="15"
                fill="@(isActive ? "rgba(25, 118, 210, 0.3)" : "rgba(150, 150, 150, 0.15)")"
                stroke="@(isActive ? "#1976d2" : "transparent")" stroke-width="1.5"
                style="transition: fill 0.3s, stroke 0.3s;" />

        @if (isActive && ActiveDirection.HasValue)
        {
            var (dx, dy) = GetArrowOffset(ActiveDirection.Value);
            <line x1="@pos.X" y1="@(pos.Y + 40)"
                  x2="@(pos.X + dx)" y2="@(pos.Y + 40 + dy)"
                  stroke="#1976d2" stroke-width="2.5" stroke-linecap="round" />
        }
    }

    @* Arrow marker definition *@
    <defs>
        <marker id="arrowhead" markerWidth="6" markerHeight="4" refX="6" refY="2" orient="auto">
            <polygon points="0 0, 6 2, 0 4" fill="#1976d2" />
        </marker>
    </defs>
</svg>

@code {
    [Parameter] public List<SwitchId> ActiveFingers { get; set; } = [];
    [Parameter] public SwitchDirection? ActiveDirection { get; set; }

    // Same positions as SwitchDiagram
    private static readonly List<(SwitchId Id, (int X, int Y) Pos)> FingerPositions =
    [
        (SwitchId.L_Pinky, (50, 100)), (SwitchId.L_Ring, (110, 80)),
        (SwitchId.L_Middle, (170, 65)), (SwitchId.L_Index, (230, 80)),
        (SwitchId.L_Thumb, (260, 150)),
        (SwitchId.R_Thumb, (540, 150)),
        (SwitchId.R_Index, (570, 80)), (SwitchId.R_Middle, (630, 65)),
        (SwitchId.R_Ring, (690, 80)), (SwitchId.R_Pinky, (750, 100)),
    ];

    private static (int X, int Y) GetArrowOffset(SwitchDirection dir) => dir switch
    {
        SwitchDirection.Up => (0, -18),
        SwitchDirection.Down => (0, 18),
        SwitchDirection.Left => (-18, 0),
        SwitchDirection.Right => (18, 0),
        SwitchDirection.Push => (0, 10),
        _ => (0, 0)
    };
}
```

- [ ] **Step 5: Build to verify**

```bash
cd C:/Developer/chordcraft && dotnet build src/ChordCraft.Client
```

- [ ] **Step 6: Commit**

```bash
git add src/ChordCraft.Client/Components/Device
git commit -m "feat: add SwitchDiagram, DirectionPopup, and HandOverlay SVG components"
```

---

### Task 6: Lesson Type Components (Intro, Review, Practice)

**Files:**
- Create: `src/ChordCraft.Client/Components/Lessons/IntroLesson.razor`
- Create: `src/ChordCraft.Client/Components/Lessons/ReviewLesson.razor`
- Create: `src/ChordCraft.Client/Components/Lessons/PracticeLesson.razor`
- Create: `src/ChordCraft.Client/Components/Lessons/LessonResults.razor`

- [ ] **Step 1: Create IntroLesson.razor**

`src/ChordCraft.Client/Components/Lessons/IntroLesson.razor`:
```razor
@using System.Text.Json
@using MudBlazor

<div class="intro-lesson">
    <MudText Typo="Typo.overline" Color="Color.Primary" Class="mb-2">NEW INPUT INTRODUCTION</MudText>

    @if (currentStep < steps.Count)
    {
        var step = steps[currentStep];
        <MudText Typo="Typo.h5" Class="mb-6">@step.Instruction</MudText>

        <div style="position: relative; max-width: 800px; margin: 0 auto;">
            <SwitchDiagram ActiveInputs="@activeInputs" />
            <HandOverlay ActiveFingers="@activeFingers" ActiveDirection="@activeDirection" />
        </div>

        <MudText Typo="Typo.body2" Align="Align.Center" Class="mt-4" Style="color: #888;">
            Press the correct key to continue (@(currentStep + 1) / @steps.Count)
        </MudText>
    }
</div>

@code {
    [Parameter, EditorRequired] public string Content { get; set; } = "{}";
    [Parameter] public EventCallback<(decimal Accuracy, decimal Speed)> OnComplete { get; set; }

    private List<IntroStep> steps = [];
    private int currentStep;
    private int correctCount;
    private int totalAttempts;
    private List<SwitchInput> activeInputs = [];
    private List<SwitchId> activeFingers = [];
    private SwitchDirection? activeDirection;

    protected override void OnParametersSet()
    {
        var doc = JsonDocument.Parse(Content);
        steps = doc.RootElement.GetProperty("steps").EnumerateArray()
            .Select(s => new IntroStep(
                s.GetProperty("instruction").GetString() ?? "",
                s.GetProperty("targetInput").GetString() ?? "",
                s.GetProperty("targetChar").GetString() ?? ""))
            .ToList();
        SetActiveFromStep();
    }

    private void SetActiveFromStep()
    {
        if (currentStep >= steps.Count) return;
        var target = steps[currentStep];
        if (CcosLayout.ReverseMap.TryGetValue(target.TargetChar, out var input))
        {
            activeInputs = [input];
            activeFingers = [input.Switch];
            activeDirection = input.Direction;
        }
    }

    [JSInvokable]
    public async Task OnKeyDown(string key, double timestamp)
    {
        if (currentStep >= steps.Count) return;
        totalAttempts++;

        var expected = steps[currentStep].TargetChar;
        if (key == expected)
        {
            correctCount++;
            currentStep++;
            if (currentStep >= steps.Count)
            {
                activeInputs = [];
                activeFingers = [];
                activeDirection = null;
                var accuracy = totalAttempts > 0 ? (decimal)correctCount / totalAttempts : 1m;
                await OnComplete.InvokeAsync((accuracy, 0m));
            }
            else
            {
                SetActiveFromStep();
            }
            StateHasChanged();
        }
    }

    private record IntroStep(string Instruction, string TargetInput, string TargetChar);
}
```

- [ ] **Step 2: Create ReviewLesson.razor**

`src/ChordCraft.Client/Components/Lessons/ReviewLesson.razor`:
```razor
@using System.Text.Json
@using MudBlazor

<div class="review-lesson">
    <MudText Typo="Typo.overline" Color="Color.Primary" Class="mb-2">REVIEW</MudText>

    <div class="char-display mb-6" style="font-size: 2rem; font-family: monospace; text-align: center; letter-spacing: 8px;">
        @for (int i = 0; i < sequence.Count; i++)
        {
            var idx = i;
            var c = sequence[idx];
            string color;
            if (idx < typedResults.Count)
                color = typedResults[idx] ? "#4caf50" : "#f44336";
            else if (idx == currentPos)
                color = "#1976d2";
            else
                color = "#ccc";

            <span style="color: @color; font-weight: @(idx == currentPos ? "bold" : "normal"); text-decoration: @(idx == currentPos ? "underline" : "none");">@(c == " " ? "␣" : c.ToString())</span>
        }
    </div>

    <div style="position: relative; max-width: 800px; margin: 0 auto;">
        <SwitchDiagram ActiveInputs="@activeInputs" />
        <HandOverlay ActiveFingers="@activeFingers" ActiveDirection="@activeDirection" />
    </div>

    <MudText Typo="Typo.body2" Align="Align.Center" Class="mt-4" Style="color: #888;">
        @currentPos / @sequence.Count
    </MudText>
</div>

@code {
    [Parameter, EditorRequired] public string Content { get; set; } = "{}";
    [Parameter] public EventCallback<(decimal Accuracy, decimal Speed)> OnComplete { get; set; }

    private List<string> sequence = [];
    private int currentPos;
    private List<bool> typedResults = [];
    private DateTime? startTime;
    private List<SwitchInput> activeInputs = [];
    private List<SwitchId> activeFingers = [];
    private SwitchDirection? activeDirection;

    protected override void OnParametersSet()
    {
        var doc = JsonDocument.Parse(Content);
        sequence = doc.RootElement.GetProperty("sequence").EnumerateArray()
            .Select(s => s.GetString() ?? "").ToList();
        UpdateActiveInput();
    }

    private void UpdateActiveInput()
    {
        if (currentPos >= sequence.Count) return;
        var target = sequence[currentPos];
        if (CcosLayout.ReverseMap.TryGetValue(target, out var input))
        {
            activeInputs = [input];
            activeFingers = [input.Switch];
            activeDirection = input.Direction;
        }
    }

    [JSInvokable]
    public async Task OnKeyDown(string key, double timestamp)
    {
        if (currentPos >= sequence.Count) return;
        startTime ??= DateTime.UtcNow;

        var expected = sequence[currentPos];
        typedResults.Add(key == expected);
        currentPos++;

        if (currentPos >= sequence.Count)
        {
            activeInputs = [];
            activeFingers = [];
            var elapsed = (DateTime.UtcNow - startTime.Value).TotalMinutes;
            var accuracy = (decimal)typedResults.Count(r => r) / typedResults.Count;
            var wpm = elapsed > 0 ? (decimal)(typedResults.Count / 5.0 / elapsed) : 0m;
            await OnComplete.InvokeAsync((accuracy, wpm));
        }
        else
        {
            UpdateActiveInput();
        }
        StateHasChanged();
    }
}
```

- [ ] **Step 3: Create PracticeLesson.razor**

`src/ChordCraft.Client/Components/Lessons/PracticeLesson.razor`:
```razor
@using System.Text.Json
@using MudBlazor

<div class="practice-lesson">
    <MudText Typo="Typo.overline" Color="Color.Primary" Class="mb-2">PRACTICE</MudText>

    @if (speedGoal.HasValue)
    {
        <MudText Typo="Typo.caption" Class="mb-2" Style="color: #888;">Speed goal: @speedGoal WPM</MudText>
    }

    <div class="text-display mb-6" style="font-size: 1.4rem; font-family: monospace; line-height: 2; text-align: center; max-width: 700px; margin: 0 auto;">
        @for (int i = 0; i < text.Length; i++)
        {
            var idx = i;
            var c = text[idx];
            string color;
            if (idx < state.TypedCharacters.Count)
                color = state.TypedCharacters[idx].IsCorrect ? "#4caf50" : "#f44336";
            else if (idx == state.CurrentPosition)
                color = "#1976d2";
            else
                color = "#999";

            <span style="color: @color; background: @(idx == state.CurrentPosition ? "#e3f2fd" : "transparent"); font-weight: @(idx == state.CurrentPosition ? "bold" : "normal");">@(c == ' ' ? ' ' : c)</span>
        }
    </div>

    <div style="position: relative; max-width: 800px; margin: 0 auto;">
        <SwitchDiagram ActiveInputs="@activeInputs" />
        <HandOverlay ActiveFingers="@activeFingers" ActiveDirection="@activeDirection" />
    </div>

    <MudStack Row="true" Justify="Justify.Center" Class="mt-4" Spacing="4">
        <MudText Typo="Typo.body2" Style="color: #888;">
            Accuracy: @((state.Accuracy * 100).ToString("0"))%
        </MudText>
        <MudText Typo="Typo.body2" Style="color: #888;">|</MudText>
        <MudText Typo="Typo.body2" Style="color: #888;">
            @state.CurrentPosition / @text.Length characters
        </MudText>
    </MudStack>
</div>

@code {
    [Parameter, EditorRequired] public string Content { get; set; } = "{}";
    [Parameter] public int? SpeedGoal { get; set; }
    [Parameter] public EventCallback<(decimal Accuracy, decimal Speed)> OnComplete { get; set; }

    private string text = "";
    private int? speedGoal;
    private TypingState state = new();
    private List<SwitchInput> activeInputs = [];
    private List<SwitchId> activeFingers = [];
    private SwitchDirection? activeDirection;

    protected override void OnParametersSet()
    {
        var doc = JsonDocument.Parse(Content);
        text = doc.RootElement.GetProperty("text").GetString() ?? "";
        speedGoal = SpeedGoal;
        state = new TypingState();
        UpdateActiveInput();
    }

    private void UpdateActiveInput()
    {
        if (state.CurrentPosition >= text.Length) return;
        var target = text[state.CurrentPosition].ToString();
        if (CcosLayout.ReverseMap.TryGetValue(target, out var input))
        {
            activeInputs = [input];
            activeFingers = [input.Switch];
            activeDirection = input.Direction;
        }
        else
        {
            activeInputs = [];
            activeFingers = [];
            activeDirection = null;
        }
    }

    [JSInvokable]
    public async Task OnKeyDown(string key, double timestamp)
    {
        if (state.IsComplete || state.CurrentPosition >= text.Length) return;
        state.StartTime ??= DateTime.UtcNow;

        var expected = text[state.CurrentPosition];
        var actual = key.Length == 1 ? key[0] : ' ';
        var isCorrect = actual == expected;

        state.TypedCharacters.Add(new TypedCharacter(expected, actual, isCorrect, timestamp));
        state.CurrentPosition++;

        if (state.CurrentPosition >= text.Length)
        {
            state.EndTime = DateTime.UtcNow;
            state.IsComplete = true;
            activeInputs = [];
            activeFingers = [];
            await OnComplete.InvokeAsync((state.Accuracy, state.Speed));
        }
        else
        {
            UpdateActiveInput();
        }
        StateHasChanged();
    }
}
```

- [ ] **Step 4: Create LessonResults.razor**

`src/ChordCraft.Client/Components/Lessons/LessonResults.razor`:
```razor
@using MudBlazor

<MudOverlay Visible="true" DarkBackground="true">
    <MudPaper Class="pa-8" Elevation="8" Style="border-radius: 16px; text-align: center; min-width: 350px;">
        <MudText Typo="Typo.h5" Class="mb-4" Style="font-weight: 700;">Lesson Complete!</MudText>

        <div class="mb-4">
            <StarDisplay Stars="Stars" Size="Size.Large" />
        </div>

        <MudStack Spacing="1" Class="mb-6">
            <MudText Typo="Typo.body1">
                Accuracy: <strong>@((Accuracy * 100).ToString("0"))%</strong>
            </MudText>
            @if (Speed > 0)
            {
                <MudText Typo="Typo.body1">
                    Speed: <strong>@Speed.ToString("0.0") WPM</strong>
                </MudText>
            }
            <MudText Typo="Typo.body1">
                Points: <strong>@Points</strong>
            </MudText>
        </MudStack>

        <MudStack Row="true" Justify="Justify.Center" Spacing="2">
            <MudButton Variant="Variant.Outlined" Color="Color.Primary"
                       OnClick="OnRetry" Size="Size.Large">
                Try Again
            </MudButton>
            <MudButton Variant="Variant.Filled" Color="Color.Primary"
                       OnClick="OnNextLesson" Size="Size.Large">
                Next Lesson
            </MudButton>
        </MudStack>
    </MudPaper>
</MudOverlay>

@code {
    [Parameter] public int Stars { get; set; }
    [Parameter] public decimal Accuracy { get; set; }
    [Parameter] public decimal Speed { get; set; }
    [Parameter] public int Points { get; set; }
    [Parameter] public EventCallback OnRetry { get; set; }
    [Parameter] public EventCallback OnNextLesson { get; set; }
}
```

- [ ] **Step 5: Build to verify**

```bash
cd C:/Developer/chordcraft && dotnet build src/ChordCraft.Client
```

- [ ] **Step 6: Commit**

```bash
git add src/ChordCraft.Client/Components/Lessons
git commit -m "feat: add Intro, Review, Practice lesson components and LessonResults overlay"
```

---

### Task 7: Typing Interface Page

**Files:**
- Create: `src/ChordCraft.Client/Pages/TypingInterface.razor`

- [ ] **Step 1: Create TypingInterface.razor**

`src/ChordCraft.Client/Pages/TypingInterface.razor`:
```razor
@page "/lessons/{LessonId:int}"
@using MudBlazor
@using ChordCraft.Core.Scoring
@implements IAsyncDisposable
@inject LessonDataService LessonData
@inject LocalProgressService Progress
@inject AudioService Audio
@inject NavigationManager Navigation
@inject IJSRuntime JS

<MudContainer MaxWidth="MaxWidth.Large" Class="mt-2">
    @* Top bar *@
    <MudStack Row="true" Justify="Justify.SpaceBetween" AlignItems="AlignItems.Center" Class="mb-4">
        <MudIconButton Icon="@Icons.Material.Filled.ArrowBack" OnClick="GoBack" />
        <MudText Typo="Typo.h6">@lesson?.Title</MudText>
        <MudStack Row="true" Spacing="1">
            <MudIconButton Icon="@(soundEnabled ? Icons.Material.Filled.VolumeUp : Icons.Material.Filled.VolumeOff)"
                           OnClick="ToggleSound" />
        </MudStack>
    </MudStack>

    @if (lesson is null)
    {
        <MudProgressCircular Indeterminate="true" Color="Color.Primary" Class="mx-auto d-block mt-8" />
    }
    else if (showResults)
    {
        <LessonResults Stars="resultStars" Accuracy="resultAccuracy" Speed="resultSpeed" Points="resultPoints"
                       OnRetry="Retry" OnNextLesson="NextLesson" />
    }
    else
    {
        @switch (lesson.Type)
        {
            case LessonType.Intro:
                <IntroLesson @ref="introRef" Content="@lesson.Content" OnComplete="OnLessonComplete" />
                break;
            case LessonType.Review:
                <ReviewLesson @ref="reviewRef" Content="@lesson.Content" OnComplete="OnLessonComplete" />
                break;
            case LessonType.Practice:
                <PracticeLesson @ref="practiceRef" Content="@lesson.Content" SpeedGoal="@lesson.SpeedGoal"
                                OnComplete="OnLessonComplete" />
                break;
        }
    }

    @* Bottom nav *@
    <MudStack Row="true" Justify="Justify.SpaceBetween" Class="mt-6 mb-4">
        <MudButton Variant="Variant.Outlined" OnClick="PrevLesson" Disabled="@(LessonId <= 1)">
            Previous
        </MudButton>
        <MudButton Variant="Variant.Outlined" OnClick="SkipLesson">
            Skip
        </MudButton>
    </MudStack>
</MudContainer>

@code {
    [Parameter] public int LessonId { get; set; }

    private LessonDetailDto? lesson;
    private DotNetObjectReference<TypingInterface>? dotNetRef;
    private bool soundEnabled = true;
    private bool showResults;
    private int resultStars;
    private decimal resultAccuracy;
    private decimal resultSpeed;
    private int resultPoints;

    private IntroLesson? introRef;
    private ReviewLesson? reviewRef;
    private PracticeLesson? practiceRef;

    protected override async Task OnInitializedAsync()
    {
        await Audio.InitAsync();
        lesson = await LessonData.GetLessonAsync(LessonId);
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            dotNetRef = DotNetObjectReference.Create(this);
            await JS.InvokeVoidAsync("ChordCraftInput.register", dotNetRef);
        }
    }

    [JSInvokable]
    public async Task OnKeyDown(string key, double timestamp)
    {
        if (showResults || lesson is null) return;

        // Forward to active lesson component
        switch (lesson.Type)
        {
            case LessonType.Intro:
                if (introRef is not null) await introRef.OnKeyDown(key, timestamp);
                break;
            case LessonType.Review:
                if (reviewRef is not null) await reviewRef.OnKeyDown(key, timestamp);
                break;
            case LessonType.Practice:
                if (practiceRef is not null) await practiceRef.OnKeyDown(key, timestamp);
                break;
        }

        if (soundEnabled)
            await Audio.PlayKeypressAsync();
    }

    private async Task OnLessonComplete((decimal Accuracy, decimal Speed) result)
    {
        if (lesson is null) return;

        resultAccuracy = result.Accuracy;
        resultSpeed = result.Speed;
        resultStars = ScoringCalculator.CalculateStars(
            result.Accuracy, result.Speed, lesson.PassAccuracyThreshold, lesson.SpeedGoal);
        resultPoints = ScoringCalculator.CalculatePoints(
            resultStars, result.Accuracy, result.Speed, lesson.SpeedGoal, lesson.Type);

        await Progress.RecordAttemptAsync(lesson.Id, resultAccuracy, resultSpeed, resultStars);

        if (resultStars > 0 && soundEnabled)
            await Audio.PlaySuccessAsync();

        showResults = true;
        StateHasChanged();
    }

    private async Task Retry()
    {
        showResults = false;
        lesson = await LessonData.GetLessonAsync(LessonId);
        StateHasChanged();
    }

    private void NextLesson()
    {
        Navigation.NavigateTo($"/lessons/{LessonId + 1}");
    }

    private void PrevLesson()
    {
        if (LessonId > 1)
            Navigation.NavigateTo($"/lessons/{LessonId - 1}");
    }

    private void SkipLesson() => NextLesson();

    private void GoBack() => Navigation.NavigateTo("/lessons");

    private async Task ToggleSound()
    {
        soundEnabled = await Audio.ToggleAsync();
    }

    public async ValueTask DisposeAsync()
    {
        await JS.InvokeVoidAsync("ChordCraftInput.unregister");
        dotNetRef?.Dispose();
    }
}
```

- [ ] **Step 2: Build to verify**

```bash
cd C:/Developer/chordcraft && dotnet build src/ChordCraft.Client
```

- [ ] **Step 3: Commit**

```bash
git add src/ChordCraft.Client/Pages/TypingInterface.razor
git commit -m "feat: add TypingInterface page with key capture, lesson routing, and results"
```

---

### Task 8: Global CSS and Final Build Verification

**Files:**
- Create: `src/ChordCraft.Client/wwwroot/css/app.css`
- Verify full solution builds and all tests pass

- [ ] **Step 1: Create app.css**

`src/ChordCraft.Client/wwwroot/css/app.css`:
```css
html, body {
    font-family: 'Roboto', sans-serif;
    margin: 0;
    padding: 0;
}

/* Prevent text selection during typing practice */
.practice-lesson, .review-lesson, .intro-lesson {
    user-select: none;
    -webkit-user-select: none;
}

/* Smooth page transitions */
.page {
    animation: fadeIn 0.2s ease-in;
}
@keyframes fadeIn {
    from { opacity: 0; }
    to { opacity: 1; }
}
```

- [ ] **Step 2: Add CSS reference to index.html**

Ensure `<link href="css/app.css" rel="stylesheet" />` is in the `<head>` of `index.html`.

- [ ] **Step 3: Full solution build and test**

```bash
cd C:/Developer/chordcraft
dotnet build
dotnet test --verbosity quiet
```

Expected: Build succeeded. All tests pass.

- [ ] **Step 4: Commit**

```bash
git add -A
git commit -m "feat: add global CSS and finalize client MVP build"
```

---

## Plan Complete

This plan produces the full client-side MVP:
- Landing page with hero, features, and CTAs
- Lesson catalog with phase sections, progress tracking, star display, and unlock logic
- Typing interface with three lesson type renderers (Intro, Review, Practice)
- CharaChorder One device visualization (18-switch SVG diagram with direction popups)
- Hand overlay with active finger highlighting and directional arrows
- Anonymous progress via LocalStorage
- Sound effects via Web Audio API
- Star scoring and lesson results overlay
- JS interop for key capture, localStorage, and audio

**What's NOT in this plan (post-MVP):**
- Auth/account pages
- Stats page with charts
- Badges page
- Play/Video lesson types
- Chording phases (4-5)
- Placement test
