using ChordCraft.Core.Enums;

namespace ChordCraft.Core.Models;

/// <summary>
/// CC2 (CharaChorder 2) default English A1 (base) layer layout.
/// Each switch has 5 directions: Up=North, Down=South, Right=East, Left=West, Push=center press.
/// The CC2 center press (Push) produces modifier actions (ctrl, etc.), not typeable characters,
/// so most letter/symbol output comes from N/S/E/W tilts.
///
/// SwitchId → JSON switch mapping:
///   L_Ring=ring, L_Middle=middle, L_Index=index, L_PalmUpper=thumb1,
///   L_ThumbInner=thumb2, L_PalmLower=thumb3
///   R_Ring=ring, R_Middle=middle, R_Index=index, R_ThumbInner=thumb1,
///   R_PalmUpper=thumb2, R_PalmLower=thumb3
///
/// All 26 letters accounted for.
/// </summary>
public static class CcosLayout
{
    public static readonly Dictionary<SwitchInput, string> Map = new()
    {
        // ── Left Ring (CTRL switch) ───────────────────────────────────────────
        [new(SwitchId.L_Ring, SwitchDirection.Up)]    = ",",
        [new(SwitchId.L_Ring, SwitchDirection.Right)] = "'",
        [new(SwitchId.L_Ring, SwitchDirection.Down)]  = "u",
        [new(SwitchId.L_Ring, SwitchDirection.Left)]  = "|",

        // ── Left Middle ───────────────────────────────────────────────────────
        [new(SwitchId.L_Middle, SwitchDirection.Up)]    = ".",
        [new(SwitchId.L_Middle, SwitchDirection.Right)] = "i",
        [new(SwitchId.L_Middle, SwitchDirection.Down)]  = "o",   // south tilt

        // ── Left Index (backspace/r/e/space switch) ──────────────────────────────
        [new(SwitchId.L_Index, SwitchDirection.Up)]    = "\b",  // north = backspace
        [new(SwitchId.L_Index, SwitchDirection.Right)] = "r",   // east tilt
        [new(SwitchId.L_Index, SwitchDirection.Down)]  = "e",   // south tilt
        [new(SwitchId.L_Index, SwitchDirection.Left)]  = " ",   // west = space

        // ── Left PalmUpper (thumb1: v/k/c/m cluster) ─────────────────────────
        [new(SwitchId.L_PalmUpper, SwitchDirection.Up)]    = "v",
        [new(SwitchId.L_PalmUpper, SwitchDirection.Right)] = "k",
        [new(SwitchId.L_PalmUpper, SwitchDirection.Down)]  = "c",
        [new(SwitchId.L_PalmUpper, SwitchDirection.Left)]  = "m",

        // ── Left ThumbInner (thumb2: w/z/g cluster) ──────────────────────────
        [new(SwitchId.L_ThumbInner, SwitchDirection.Right)] = "w",  // east tilt
        [new(SwitchId.L_ThumbInner, SwitchDirection.Down)]  = "z",
        [new(SwitchId.L_ThumbInner, SwitchDirection.Left)]  = "g",

        // ── Left PalmLower (thumb3: -// cluster) ──────────────────────────────
        [new(SwitchId.L_PalmLower, SwitchDirection.Up)]    = "-",
        [new(SwitchId.L_PalmLower, SwitchDirection.Right)] = "/",

        // ── Right Ring (tab/n/l switch) ───────────────────────────────────────
        [new(SwitchId.R_Ring, SwitchDirection.Up)]   = "\t",   // north = tab
        [new(SwitchId.R_Ring, SwitchDirection.Down)] = "n",    // south tilt
        [new(SwitchId.R_Ring, SwitchDirection.Left)] = "l",    // west tilt

        // ── Right Middle (y/;/s/j switch) ────────────────────────────────────
        [new(SwitchId.R_Middle, SwitchDirection.Up)]    = "y",
        [new(SwitchId.R_Middle, SwitchDirection.Right)] = ";",
        [new(SwitchId.R_Middle, SwitchDirection.Down)]  = "s",  // south tilt
        [new(SwitchId.R_Middle, SwitchDirection.Left)]  = "j",  // west tilt

        // ── Right Index (enter/t/a switch) ────────────────────────────────────
        [new(SwitchId.R_Index, SwitchDirection.Up)]   = "\n",  // north = enter
        [new(SwitchId.R_Index, SwitchDirection.Down)] = "t",   // south tilt
        [new(SwitchId.R_Index, SwitchDirection.Left)] = "a",   // west tilt

        // ── Right ThumbInner (thumb1: p/h/d/f cluster) ───────────────────────
        [new(SwitchId.R_ThumbInner, SwitchDirection.Up)]    = "p",  // north tilt
        [new(SwitchId.R_ThumbInner, SwitchDirection.Right)] = "h",
        [new(SwitchId.R_ThumbInner, SwitchDirection.Down)]  = "d",
        [new(SwitchId.R_ThumbInner, SwitchDirection.Left)]  = "f",

        // ── Right PalmUpper (thumb2: x/b/q cluster) ──────────────────────────
        [new(SwitchId.R_PalmUpper, SwitchDirection.Up)]   = "x",
        [new(SwitchId.R_PalmUpper, SwitchDirection.Down)] = "q",
        [new(SwitchId.R_PalmUpper, SwitchDirection.Left)] = "b",  // west tilt

        // ── Right PalmLower (thumb3: ? switch) ────────────────────────────────
        [new(SwitchId.R_PalmLower, SwitchDirection.Up)] = "?",
    };

    public static readonly Dictionary<string, SwitchInput> ReverseMap =
        Map.Where(kvp => kvp.Value.Length == 1 && !char.IsControl(kvp.Value[0]))
           .GroupBy(kvp => kvp.Value)
           .ToDictionary(g => g.Key, g => g.First().Key);
}
