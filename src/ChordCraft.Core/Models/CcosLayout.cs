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
