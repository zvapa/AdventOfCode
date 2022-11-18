using AdventOfCode._2015;
namespace Tests;

public class _2015
{
    [Theory]
    [InlineData("qjhvhtzxzqqjkmpb", true)]
    [InlineData("xxyxx", true)]
    [InlineData("uurcxstgmygtbstg", false)]
    [InlineData("ieodomkazucvgmuy", false)]
    [InlineData("sleaoasjspnjctqt", false)]
    public void Day5_Part2(string input, bool expected)
    {
        bool actual = Day5.IsNiceWord(input);
        string message = $"IsNiceWord({input}) was expected to be {expected} but was {actual}.";
        Assert.True(actual == expected, message);
    }
}