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
        }
    }

    [Theory]
    [InlineData(SwitchId.L_Index, SwitchDirection.Push, "f")]
    [InlineData(SwitchId.L_Index, SwitchDirection.Up, "r")]
    [InlineData(SwitchId.R_Index, SwitchDirection.Push, "j")]
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
}
