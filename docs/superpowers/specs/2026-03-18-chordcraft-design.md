# ChordCraft — Product Requirements Document

## Overview

ChordCraft is a web-based typing tutor for CharaChorder One beginners, modeled after TypingClub.com. It teaches users to type on the CharaChorder One device through structured, gamified lessons — first mastering individual character entry (CCE) on the device's unique multi-directional switches, then progressing to chord-based input where multiple switches are pressed simultaneously to produce entire words.

### Target Audience

Beginner CharaChorder One users who have just purchased the device and need structured guidance to build proficiency.

### Target Device

CharaChorder One only, using the default CCOS layout. Custom keymaps are out of scope.

---

## Pages & Navigation

### 1. Landing Page (`/`)

Marketing page introducing ChordCraft to new visitors.

**Sections:**
- **Hero:** Headline "Learn CharaChorder Typing", subheadline explaining the value prop, prominent "Get Started" CTA button
- **What is ChordCraft?:** Brief explanation of the app — free, web-based, structured lessons for CharaChorder One
- **Feature Highlights:** Grid of 6-9 feature cards:
  - Interactive lessons with real-time feedback
  - CharaChorder One device visualization
  - Finger guide with directional indicators
  - Gamified progression (stars, points, badges)
  - Progress tracking and stats
  - Works with your CharaChorder One — just plug in and go
- **How It Works:** "Practice each lesson until you earn all five stars. A few minutes a day for a few weeks and you'll be chordin' like a pro!"
- **Footer:** Copyright, links

**Navigation bar:** ChordCraft logo, Get Started, Login

### 2. Lesson Catalog (`/lessons`)

The main hub showing all lessons organized by phase. This is the first page users see after clicking "Get Started."

**Top bar:** ChordCraft logo | Home | Stats | Badges | Save Progress | Login

**Progress summary bar:** `X% progress | X stars | X points`

**Lesson grid:**
- Lessons displayed as numbered cards in a responsive grid (5 per row on desktop, adapts on mobile)
- Cards grouped under phase section headers (e.g., "Switch Basics", "Directional Inputs")
- Each card shows:
  - Lesson number
  - Icon indicating lesson type (different icons for Intro, Review, Practice, Play, Video)
  - Lesson title
  - Lock icon if not yet unlocked
  - Star rating (0-5) if attempted
- First unlocked incomplete lesson is visually highlighted with a tooltip: "Click the lesson to begin"
- Decorative artwork between phase sections (similar to TypingClub's ice/jungle theme artwork)

**Placement test:** Optional shortcut button visible on the catalog page, allowing experienced users to skip ahead based on demonstrated proficiency.

### 3. Typing Interface (`/lessons/{id}`)

The core lesson experience where users practice typing on their CharaChorder One.

**Layout (top to bottom):**

1. **Minimal top bar:**
   - Hamburger menu (back to catalog)
   - Keyboard visualization toggle
   - Sound toggle
   - Settings gear

2. **Instruction area:**
   - Lesson type label (e.g., "NEW INPUT INTRODUCTION", "REVIEW", "PRACTICE")
   - Instruction text (e.g., "Push the left index switch **up** to type **r**")
   - For typing lessons: the target text/characters to type, with cursor highlighting the current character, typed characters shown in green (correct) or red (incorrect)

3. **CharaChorder One device visualization:**
   - Compact circle diagram: 18 circles arranged as two groups of 5 (left hand, right hand) with 4 extra for additional thumb/palm switches
   - Each circle shows the "push" character by default (the most common/home direction)
   - When a switch is the active target, it glows blue and a popup expands above it showing all 5 directional mappings (up/down/left/right/push) with the target direction highlighted
   - Inactive switches are dimmed gray

4. **Hand overlay:**
   - Stylized hand silhouettes in the "resting on cradles" position, overlapping the switch diagram
   - Active finger glows/highlights to show which finger to use
   - A directional arrow on the active finger indicates which direction to push the switch (up/down/left/right for directional, a press indicator for push)
   - Inactive fingers are dimmed

5. **Bottom navigation bar:**
   - "Previous" button (go to prior lesson)
   - Progress bar showing position within the current lesson
   - "Skip" button (advance to next screen/lesson)

**Post-lesson results overlay:**
- Stars earned (1-5, animated)
- Accuracy percentage
- Speed (WPM for text lessons, inputs per minute for single-character lessons)
- Points earned
- "Next Lesson" and "Try Again" buttons

**Lesson type behaviors:**

- **Intro:** Shows one new switch+direction at a time. User must press the correct input to advance. Minimal text, focus on the device visualization and finger guide.
- **Review:** Shows a sequence of characters using recently learned inputs. User types them in order. Accuracy and basic speed tracked.
- **Practice:** Timed drill of characters/words using learned inputs. Full accuracy and WPM tracking. Star rating based on performance.
- **Play:** Gamified challenge — e.g., falling characters that must be typed before reaching the bottom, or a "type the word" race. Uses only learned inputs.
- **Video:** Embedded instructional video. No typing input. Marked complete on watch.

**Chord-specific lesson behaviors (Phase 4-5):**

- **Chord Introduction:** Shows the target word, the chord combination (which switches to press simultaneously), and a "press all at once" animation on the device visualization. All involved fingers highlight simultaneously.
- **Chord Practice:** Target words appear; user must press the correct chord. Feedback shows which keys were detected and whether the chord was correct.
- **Mixed Practice:** Some words should be chorded (common words with known chords), others typed via CCE. Teaches the user when to chord vs. spell out.

### 4. Stats Page (`/stats`)

Progress tracking and performance analytics.

**Progress Overview:**
- Line/bar chart showing metrics over time (toggleable: Practice Time, Accuracy %, Coverage, Speed)
- Summary stats: overall accuracy, coverage (% of lessons completed), average speed, total practice time

**Active Practice Time:**
- Horizontal bar chart breaking down time spent on passed attempts, failed attempts, and partial attempts

**Per-Switch Accuracy Heatmap (unique to ChordCraft):**
- Visual representation of the 18 switches with color coding from red (low accuracy) to green (high accuracy)
- Each switch expandable to show per-direction accuracy
- Helps users identify which switches/directions need more practice

**Requires account:** Anonymous users see a preview with a "Sign up to see your stats!" prompt (matching TypingClub's approach).

### 5. Badges Page (`/badges`)

Achievement system for motivation and engagement.

**Sections:**
- **Earned Badges:** Grid of unlocked badge cards with earned date
- **Un-earned Badges:** Grid of locked badge cards with descriptions of how to earn them

**Badge list:**
| Badge | Criteria |
|-------|----------|
| First Steps | Complete your first lesson |
| Switch Basics | Complete Phase 1 (all switch pushes) |
| Direction Master | Complete Phase 2 (all directional inputs) |
| Speed Demon | Reach 20 WPM in a practice lesson |
| First Chord | Successfully type your first chord |
| Chord Collector | Learn 10 different chords |
| Chord Master | Learn 50 different chords |
| 5 Day Streak | Practice 5 days in a row |
| Weekend Warrior | Practice on both Saturday and Sunday |
| Marathon Runner | Practice for 30 minutes in a single session |
| Perfectionist | Earn 5 stars on 10 lessons |
| Full Layout | Demonstrate proficiency on all 90 CCOS inputs |
| The Heavyweight | Earn 5 gold stars in one day on 5 different lessons |
| Keyboard Crusher | Rack up 100 total lesson attempts |

### 6. Authentication

**Anonymous flow:**
- Users can start lessons immediately without signing up
- Progress saved to browser LocalStorage with a generated SessionId
- Prompt to create account appears periodically ("Save your progress!")

**Authenticated flow:**
- Email/password registration via ASP.NET Core Identity
- On signup, any existing LocalStorage progress is migrated to the server
- Login persists progress across devices

---

## Lesson Curriculum

### Phase 1: Switch Basics (Lessons 1-15)

Introduces the CharaChorder One device and teaches the "push" direction on all finger switches.

| # | Title | Type | Target Inputs |
|---|-------|------|---------------|
| 1 | Introduction to CharaChorder | Video | — |
| 2 | Left Index Push: f | Intro | L-Index-Push |
| 3 | Right Index Push: j | Intro | R-Index-Push |
| 4 | Review: f & j | Review | L-Index-Push, R-Index-Push |
| 5 | Space (Left Thumb Push) | Intro | L-Thumb-Push |
| 6 | Left Middle Push: d | Intro | L-Middle-Push |
| 7 | Right Middle Push: k | Intro | R-Middle-Push |
| 8 | Review: d & k | Review | L-Middle-Push, R-Middle-Push |
| 9 | Practice: f j d k | Practice | All learned |
| 10 | Left Ring Push: s | Intro | L-Ring-Push |
| 11 | Right Ring Push: l | Intro | R-Ring-Push |
| 12 | Left Pinky Push: a | Intro | L-Pinky-Push |
| 13 | Right Pinky Push: ; | Intro | R-Pinky-Push |
| 14 | All Push Keys | Review | All finger pushes |
| 15 | Play: Push Keys | Play | All finger pushes |

### Phase 2: Directional Inputs (Lessons 16-50)

Teaches all 4 directional inputs (up, down, left, right) across all finger switches.

| # | Title | Type | Target Inputs |
|---|-------|------|---------------|
| 16 | Directions Explained | Video | — |
| 17 | Left Index Up: r | Intro | L-Index-Up |
| 18 | Left Index Down: v | Intro | L-Index-Down |
| 19 | Review: r & v | Review | L-Index-Up, L-Index-Down |
| 20 | Left Index Left: g | Intro | L-Index-Left |
| 21 | Left Index Right: t | Intro | L-Index-Right |
| 22 | Review: All Left Index | Review | All L-Index directions |
| 23 | Practice: Left Index | Practice | All L-Index directions |
| 24 | Right Index Up: u | Intro | R-Index-Up |
| 25 | Right Index Down: m | Intro | R-Index-Down |
| 26 | Right Index Left: h | Intro | R-Index-Left |
| 27 | Right Index Right: y | Intro | R-Index-Right |
| 28 | Review: All Right Index | Review | All R-Index directions |
| 29 | Play: Index Fingers | Play | All index directions |
| 30 | Left Middle Directions | Intro | L-Middle all directions |
| 31 | Review: Left Middle | Review | L-Middle all directions |
| 32 | Right Middle Directions | Intro | R-Middle all directions |
| 33 | Review: Right Middle | Review | R-Middle all directions |
| 34 | Practice: Middle Fingers | Practice | All middle directions |
| 35 | Left Ring Directions | Intro | L-Ring all directions |
| 36 | Review: Left Ring | Review | L-Ring all directions |
| 37 | Right Ring Directions | Intro | R-Ring all directions |
| 38 | Review: Right Ring | Review | R-Ring all directions |
| 39 | Practice: Ring Fingers | Practice | All ring directions |
| 40 | Left Pinky Directions | Intro | L-Pinky all directions |
| 41 | Review: Left Pinky | Review | L-Pinky all directions |
| 42 | Right Pinky Directions | Intro | R-Pinky all directions |
| 43 | Review: Right Pinky | Review | R-Pinky all directions |
| 44 | Practice: Pinky Fingers | Practice | All pinky directions |
| 45 | Thumb Directions | Intro | Both thumb all directions |
| 46 | Review: Thumbs | Review | Both thumb all directions |
| 47 | All Directions Review | Review | All switches, all directions |
| 48 | Practice: Full Layout | Practice | All 90 inputs |
| 49 | Play: Direction Challenge | Play | All 90 inputs |
| 50 | Full Layout Mastery Test | Practice | All 90 inputs (speed + accuracy) |

### Phase 3: CCE Speed Building (Lessons 51-80)

Builds typing speed using character-by-character entry with all learned inputs.

| # | Title | Type | Notes |
|---|-------|------|-------|
| 51 | Common 3-Letter Words | Practice | the, and, for, are, etc. |
| 52 | Common 4-Letter Words | Practice | that, with, have, this, etc. |
| 53 | Mixed Common Words | Practice | Top 50 English words |
| 54 | Simple Sentences | Practice | Short sentences, common words |
| 55 | Play: Word Race | Play | Type words before timer |
| 56-58 | Speed Drill Series (Goal: 10 WPM) | Practice | Progressively longer passages |
| 59 | Numbers & Symbols Intro | Intro | Thumb/edge switch special chars |
| 60-62 | Mixed Text with Numbers | Practice | Sentences with numbers |
| 63-65 | Speed Drill Series (Goal: 15 WPM) | Practice | Paragraphs |
| 66-68 | Punctuation Practice | Practice | Periods, commas, question marks |
| 69 | Play: Falling Characters | Play | Arcade-style character game |
| 70-73 | Speed Drill Series (Goal: 20 WPM) | Practice | Full paragraphs with punctuation |
| 74-77 | Speed Drill Series (Goal: 25 WPM) | Practice | Longer passages |
| 78 | Play: Speed Challenge | Play | 1-minute speed test format |
| 79 | CCE Mastery Test | Practice | Comprehensive test |
| 80 | Ready for Chords! | Video | Intro to the chording concept |

### Phase 4: Introduction to Chording (Lessons 81-120)

Teaches the unique CharaChorder chording system — pressing multiple switches simultaneously to output entire words.

| # | Title | Type | Notes |
|---|-------|------|-------|
| 81 | What is a Chord? | Video | Explains simultaneous key press = word |
| 82 | Your First Chord: "the" | Intro | Most common English word |
| 83 | Chord: "and" | Intro | Second chord |
| 84 | Chord: "is" | Intro | Third chord |
| 85 | Review: the, and, is | Review | First 3 chords |
| 86 | Chord: "to" | Intro | |
| 87 | Chord: "in" | Intro | |
| 88 | Chord: "it" | Intro | |
| 89 | Review: to, in, it | Review | |
| 90 | Practice: First 6 Chords | Practice | All 6 chords in sentences |
| 91 | Play: Chord or Spell? | Play | Choose whether to chord or CCE |
| 92-95 | Common Word Chords Set 2 | Intro/Review | for, you, was, are, with, that |
| 96-99 | Common Word Chords Set 3 | Intro/Review | have, this, from, they, been, will |
| 100 | Practice: Top 18 Chords | Practice | Mixed chord practice |
| 101-105 | Common Word Chords Set 4-5 | Intro/Review | More common words |
| 106-110 | Mixed CCE + Chord Practice | Practice | Full sentences, chord common words |
| 111-115 | 3-Key Chord Series | Intro/Practice | More complex chords |
| 116-118 | Chord Recall Drills | Practice | Flash word, user produces chord |
| 119 | Play: Chord Challenge | Play | Gamified chord recall |
| 120 | Chord Foundations Test | Practice | Comprehensive chord test |

### Phase 5: Advanced Chording (Lessons 121-160+)

Builds chord vocabulary and speed for real-world typing proficiency.

| # | Title | Type | Notes |
|---|-------|------|-------|
| 121-125 | Phrase Chords | Intro/Practice | Multi-word chords |
| 126-130 | Speed Building (Goal: 30 WPM) | Practice | Mixed CCE + chords, real text |
| 131-135 | Advanced Word Chords | Intro/Practice | Less common but useful words |
| 136-140 | Speed Building (Goal: 40 WPM) | Practice | Longer passages |
| 141-145 | Context Switching | Practice | Know when to chord vs. CCE |
| 146-150 | Speed Building (Goal: 50 WPM) | Practice | Real-world text passages |
| 151-155 | Advanced Drills | Practice | Mixed difficulty |
| 156-158 | Speed Building (Goal: 60 WPM) | Practice | Expert-level passages |
| 159 | Play: Ultimate Challenge | Play | Final gamified test |
| 160 | ChordCraft Master Test | Practice | Comprehensive final exam |

---

## Device Visualization

### Switch Diagram

The CharaChorder One device is rendered as an interactive SVG component.

**Layout:** Two groups of circles — 9 switches per hand (5 fingers + potential palm/edge switches), arranged to mirror the physical device's finger cradle positions. Left hand group on the left, right hand group on the right, with spacing between them.

**Default state:** Each circle shows the "push" direction character label (the home/default input for that switch). Circles are gray with subtle borders.

**Active state:** When a lesson targets a specific switch+direction:
- The circle glows blue with a pulsing animation
- A popup/tooltip expands above the circle showing all 5 directional mappings arranged as a compass rose (up/down/left/right around a center push label)
- The target direction within the popup is highlighted in blue; others remain gray
- An animated directional arrow indicates the physical direction to push

**Chord mode:** When teaching chords, multiple switches highlight simultaneously in blue. The popup is suppressed; instead, all involved switches glow and a chord label appears above the diagram.

### Hand Overlay

Stylized hand silhouettes rendered as SVG, positioned over the switch diagram.

**Default state:** Semi-transparent gray hands in the "resting on cradles" position, with each finger aligned to its corresponding switch circle.

**Active state:**
- The target finger increases opacity and changes color (blue glow)
- A small directional arrow appears on or near the active finger indicating push direction
- All other fingers remain dimmed

**Chord state:** Multiple fingers highlight simultaneously.

---

## Data Model

### Entities

```
User
├── Id: Guid (PK)
├── Email: string (unique, nullable for anonymous)
├── PasswordHash: string (nullable for anonymous)
├── DisplayName: string
├── SessionId: string (for anonymous-to-authenticated migration)
├── CreatedAt: DateTime
└── LastActiveAt: DateTime

Phase
├── Id: int (PK)
├── Name: string
├── Description: string
├── Order: int
└── Lessons: ICollection<Lesson>

Lesson
├── Id: int (PK)
├── PhaseId: int (FK → Phase)
├── Number: int (display order)
├── Title: string
├── Type: enum (Intro, Review, Practice, Play, Video)
├── Description: string
├── TargetInputs: string (JSON — array of switch+direction identifiers)
├── PassAccuracyThreshold: decimal (e.g., 0.80)
├── SpeedGoal: int? (WPM, nullable)
├── Content: string (JSON — lesson-specific content: character sequences, word lists, video URLs, game config)
└── Phase: Phase

LessonAttempt
├── Id: Guid (PK)
├── UserId: Guid (FK → User)
├── LessonId: int (FK → Lesson)
├── StartedAt: DateTime
├── CompletedAt: DateTime?
├── Accuracy: decimal
├── Speed: decimal (WPM or inputs/min)
├── Stars: int (1-5, 0 if failed)
├── Points: int
├── Passed: bool
├── InputLog: string (JSON — detailed per-keystroke data for playback and per-switch analytics)
├── User: User
└── Lesson: Lesson

UserProgress
├── UserId: Guid (FK → User, composite PK)
├── LessonId: int (FK → Lesson, composite PK)
├── BestStars: int
├── BestAccuracy: decimal
├── BestSpeed: decimal
├── TotalAttempts: int
├── FirstCompletedAt: DateTime?
├── LastAttemptAt: DateTime
├── User: User
└── Lesson: Lesson

Badge
├── Id: int (PK)
├── Name: string
├── Description: string
├── IconUrl: string
├── Criteria: string (JSON — e.g., {"type":"streak","days":5})
└── UserBadges: ICollection<UserBadge>

UserBadge
├── UserId: Guid (FK → User, composite PK)
├── BadgeId: int (FK → Badge, composite PK)
├── EarnedAt: DateTime
├── User: User
└── Badge: Badge

ChordEntry (static reference table)
├── Id: int (PK)
├── OutputText: string (e.g., "the")
├── InputKeys: string (JSON — array of {Switch, Direction} pairs)
├── Difficulty: int (1-5)
└── Category: string (e.g., "common-words", "phrases")
```

### Unlock Logic

A lesson with `Number = N` is unlocked when:
- `N == 1` (first lesson is always unlocked), OR
- A `UserProgress` record exists for the lesson with `Number = N-1` in the same phase where `BestStars >= 1`, OR
- The previous phase's final lesson has `BestStars >= 1` (for the first lesson of a new phase)

### Star Calculation

Stars are awarded based on accuracy and speed relative to the lesson's goals:

| Stars | Criteria |
|-------|----------|
| 1 | Accuracy >= PassAccuracyThreshold |
| 2 | Accuracy >= 85% |
| 3 | Accuracy >= 90% |
| 4 | Accuracy >= 95% |
| 5 | Accuracy >= 98% AND Speed >= SpeedGoal (if set) |

### Points Calculation

`Points = Stars * 10 + (Accuracy% - 80) * 2 + min(Speed, SpeedGoal) / SpeedGoal * 20`

Capped at 100 points per lesson attempt. Only awarded if the lesson is passed.

---

## Tech Stack

### Frontend
- **Blazor WebAssembly (.NET 9)** — client-side execution for zero-latency input capture
- **MudBlazor** — UI component library for layout, cards, navigation, dialogs, charts
- **SVG Blazor Components** — custom components for switch diagram and hand overlay, rendered as inline SVG with CSS transitions
- **CSS Isolation** — scoped styles per component
- **JS Interop** — minimal, used for:
  - `keydown`/`keyup` event capture (raw key events need sub-millisecond handling)
  - Web Audio API for sound effects (key press sounds, success/failure)
  - LocalStorage access for anonymous progress

### Backend
- **.NET 9 Web API** — RESTful API with controllers or minimal APIs
- **PostgreSQL** — primary database
- **EF Core 9** — ORM with code-first migrations
- **ASP.NET Core Identity** — authentication (email/password, JWT tokens for API auth)

### Deployment
- **Railway** — both frontend (static WASM files) and backend (.NET API + PostgreSQL)
- **Railway CLI** for deployment

### Project Structure

```
C:\Developer\chordcraft\
├── src\
│   ├── ChordCraft.Client\          # Blazor WASM project
│   │   ├── Components\
│   │   │   ├── Layout\             # MainLayout, NavMenu
│   │   │   ├── Lessons\            # LessonCard, LessonGrid, TypingInterface
│   │   │   ├── Device\             # SwitchDiagram, HandOverlay, DirectionPopup
│   │   │   ├── Stats\              # ProgressChart, SwitchHeatmap
│   │   │   ├── Badges\             # BadgeCard, BadgeGrid
│   │   │   └── Shared\             # Common components
│   │   ├── Pages\                  # Routable pages (Landing, Lessons, Stats, Badges)
│   │   ├── Services\               # API client services, LocalStorage service
│   │   ├── Models\                 # Client-side DTOs
│   │   └── wwwroot\                # Static assets, CSS, JS interop files
│   ├── ChordCraft.Api\             # .NET Web API project
│   │   ├── Controllers\            # API endpoints
│   │   ├── Services\               # Business logic
│   │   └── Program.cs
│   ├── ChordCraft.Core\            # Shared domain models, interfaces
│   │   ├── Entities\
│   │   ├── DTOs\
│   │   └── Interfaces\
│   └── ChordCraft.Infrastructure\  # EF Core, Identity, data access
│       ├── Data\
│       │   ├── AppDbContext.cs
│       │   ├── Migrations\
│       │   └── Seed\               # Lesson/Phase/Badge/Chord seed data
│       └── Repositories\
├── tests\
│   ├── ChordCraft.Api.Tests\
│   └── ChordCraft.Client.Tests\
├── docs\
│   └── superpowers\specs\
└── ChordCraft.sln
```

---

## API Endpoints

### Auth
- `POST /api/auth/register` — create account
- `POST /api/auth/login` — login, returns JWT
- `POST /api/auth/migrate` — migrate anonymous SessionId progress to authenticated user

### Lessons
- `GET /api/phases` — list all phases with lesson summaries
- `GET /api/lessons/{id}` — get full lesson content
- `GET /api/lessons/{id}/progress` — get user's progress for a lesson

### Progress
- `POST /api/attempts` — submit a lesson attempt (accuracy, speed, input log)
- `GET /api/progress` — get all user progress (for catalog display)
- `GET /api/stats` — get aggregated stats for the stats page

### Badges
- `GET /api/badges` — list all badges with earned status
- Badge awarding happens server-side: after each attempt submission, the API checks badge criteria and awards any newly earned badges. The response to `POST /api/attempts` includes any new badges earned.

### Chords
- `GET /api/chords?phase=4` — get chord library for a phase/difficulty level

---

## Key UX Behaviors

### Input Detection

The Blazor WASM app captures raw `keydown`/`keyup` events via JS interop. The CharaChorder One presents itself as a standard USB HID keyboard, so inputs arrive as regular keystrokes. The app does not need special device drivers.

**For CCE lessons:** Each keypress is matched against the expected character. Correct = advance cursor + green highlight. Incorrect = red highlight + error count.

**For chord lessons:** The app watches for a burst of near-simultaneous keypresses (the CharaChorder firmware handles the chord resolution and sends the output word as a rapid sequence of keystrokes). The app compares the received word against the expected chord output.

### Lesson Locking

- Lessons unlock sequentially: completing lesson N (with at least 1 star) unlocks lesson N+1
- The placement test can unlock multiple lessons at once based on demonstrated skill level
- Users cannot skip locked lessons (clicking shows a "Complete the previous lesson first" message)

### Anonymous Progress

- On first visit, a unique SessionId is generated and stored in LocalStorage
- All progress is saved locally under this SessionId
- API calls include the SessionId header for anonymous users
- On account creation, `POST /api/auth/migrate` transfers all progress from the SessionId to the new user account

### Sound Effects

- Key press: subtle click sound on each correct input
- Error: soft error tone on incorrect input
- Lesson complete: success chime
- Star earned: ascending tone
- Badge earned: achievement sound
- All sounds togglable via the top bar sound button

### Responsive Design

- **Desktop (primary):** Full layout with side-by-side hand groups, device visualization below text area
- **Tablet:** Slightly compressed layout, device viz may stack
- **Mobile:** Informational only — typing practice requires a CharaChorder One (physical device), so mobile shows lesson content/progress but not the active typing interface. A message prompts users to switch to desktop with their CC1.
