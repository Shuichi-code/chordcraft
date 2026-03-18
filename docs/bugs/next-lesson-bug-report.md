# Bug Report: Next Lesson Button Does Not Load Next Lesson

**Date:** 2026-03-19
**Severity:** Critical — blocks all lesson progression
**Affected component:** `src/ChordCraft.Client/Pages/TypingInterface.razor`

---

## Summary

Clicking "Next Lesson" after completing any lesson advances the URL correctly but the page never renders the new lesson. The "Lesson Complete!" overlay from the completed lesson persists indefinitely, with the previous lesson's title in the header.

## Root Cause

Blazor WASM reuses the same component instance when navigating between routes of the same component type (i.e., `/lessons/1` → `/lessons/2`). The `[Parameter] LessonId` updates, but `OnInitializedAsync` does **not** re-run on parameter changes — only `OnParametersSetAsync` does.

`TypingInterface.razor` only loads lesson data and resets state in `OnInitializedAsync`. There was no `OnParametersSetAsync` override to detect the `LessonId` change and reset `showResults`, `lesson`, and result fields. So after `Navigation.NavigateTo("/lessons/2")`:

- `LessonId` = 2 (updated by Blazor)
- `lesson` = still the lesson 1 object
- `showResults` = still `true`
- The overlay continues to render with stale data

Every subsequent click on "Next Lesson" advances the URL by 1 but the UI never changes.

## Fix Applied

Added `OnParametersSetAsync` to `TypingInterface.razor` that detects a `LessonId` change and resets all state before loading the new lesson:

```csharp
private int _loadedLessonId;

protected override async Task OnParametersSetAsync()
{
    if (LessonId != _loadedLessonId)
    {
        _loadedLessonId = LessonId;
        showResults = false;
        lesson = null;
        resultStars = 0;
        resultAccuracy = 0;
        resultSpeed = 0;
        resultPoints = 0;
        StateHasChanged(); // show spinner immediately
        lesson = await LessonData.GetLessonAsync(LessonId);
    }
}
```

Also added `_loadedLessonId = LessonId;` in `OnInitializedAsync` to prime the tracking field.

---

## Test Results (pre-fix, on live app)

| Test | URL After Click | Overlay Dismissed | New Lesson Loaded |
|------|----------------|-------------------|-------------------|
| Lesson 1 (Video) → Next | /lessons/2 | NO | NO |
| Lesson 2 (Intro) → Next | /lessons/3 | NO | NO |
| Lesson 4 (Review) → Next | /lessons/5 | NO | NO |

## Screenshots

- `lesson1-loaded.png` — Lesson 1 loaded, Continue button visible
- `lesson1-complete.png` — Lesson Complete overlay after clicking Continue
- `lesson1-next-result.png` — BUG: URL is /lessons/2 but overlay from lesson 1 still showing
- `lesson-next-bug.png` — BUG: Second click → /lessons/3, still showing lesson 1 overlay
- `lesson2-complete.png` — Lesson 2 completed (Intro type, pressed 'f')
- `lesson4-complete.png` — Lesson 4 completed (Review type, full sequence typed)
- `lesson81-beyond-last.png` — Navigating past lesson 80 shows stale lesson 80 content at /lessons/81

---

## Secondary Bug: No Boundary Handling for Last Lesson

When the user clicks Next Lesson from lesson 80 (the last lesson), the app navigates to `/lessons/81`. The API returns 404 for lesson 81.

**Current behavior (pre-fix):** Stale lesson 80 content shown at /lessons/81 (same root cause).

**Behavior after fix:** Loading spinner shown indefinitely — the `GetLessonAsync` call returns null/throws for 404, so `lesson` stays null and the spinner never resolves. There is no "Congratulations, you've completed all lessons!" message or redirect.

**Recommended fix:** In `TypingInterface.razor`, after `lesson = await LessonData.GetLessonAsync(LessonId)` in `OnParametersSetAsync`, check if `lesson is null` and handle gracefully (e.g., navigate to `/lessons` with a completion message).

---

## Reproduction Steps (Playwright)

```
1. Navigate to /lessons/1
2. Wait 6 seconds for Blazor WASM to hydrate
3. Click "Continue" button
4. Verify "Lesson Complete!" overlay appears
5. Click "Next Lesson"
6. Assert URL = /lessons/2
7. Assert "Lesson Complete!" overlay is NOT visible  ← FAILS
8. Assert lesson 2 content ("Left Index Push: f") is visible  ← FAILS
```
