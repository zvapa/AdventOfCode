using static AdventOfCode._2015.Day6;
namespace Tests._2015;

public class Day6Tests
{
    const string SkipOrNot = null; // Run all tests
    // const string SkipOrNot = "Remove the Skip argument to run this test."; // Skip all tests

    [Theory(Skip = SkipOrNot)]
    [MemberData(nameof(GridData))]
    public void Part1_BuildLightsGrid_UsingDictionary(Point2D_rrs upperLeft, Point2D_rrs lowerRight, int expectedSize)
    {
        var grid = Day6.BuildGrid_UsingDictionary(upperLeft, lowerRight);
        int actualSize = grid.Count;
        Assert.True(actualSize == expectedSize, $"The grid size was expected to be {expectedSize} but was {actualSize}.");
    }
    public static TheoryData<Point2D_rrs, Point2D_rrs, int> GridData => new()
    {
        { new Point2D_rrs(0,0), new Point2D_rrs(2,2), 9},
        { new Point2D_rrs(0,0), new Point2D_rrs(3,3), 16}
    };

    [Fact(Skip = SkipOrNot)]
    public void Part1_BuildLightsGrid_UsingCustomType()
    {
        // Given
        (int rows, int columns) = (3, 3);
        Matrix<Day6.LightStatus> lightsGrid = new(rows, columns);
        // When
        var expectedSize = 9;
        var actualSize = lightsGrid.Count();
        const Day6.LightStatus expectedStatus = Day6.LightStatus.Off;
        // Then
        Assert.True(lightsGrid.Count() == expectedSize, $"The grid should have {expectedSize} lights, but {actualSize} were found.");
        Assert.True(lightsGrid.All(l => l == expectedStatus), "All lights should be off by default, but not all are.");
    }

    // [Fact/* (Skip = $"Remove the Skip argument to run this test.") */]
    // public void Part1_TurnOnSomeLights()
    // {
    //     // Given
    //     var grid = Day6.BuildGrid(new Point2D_rrs(0, 0), new Point2D_rrs(3, 3));
    //     string instruction = "turn on 1,1 through 2,2";
    //     // When
    //     var actual = Day6.ParseInstruction(instruction);
    //     // Then
    //     Assert.Equal(actual.UpperLeftCorner, new Point2D_rrs(1, 1));
    //     Assert.Equal(actual.LowerRightCorner, new Point2D_rrs(2, 2));
    // }

    // [Fact/* (Skip = $"Remove the Skip argument to run this test.") */]
    // public void ParseInstructions()
    // {
    //     // Given
    //     var turnOffInstruction = "turn off 660,55 through 986,197";
    //     // When

    //     // Then
    //     // turnOffInstruction.StartsWith("turn off ");
    //     Assert.StartsWith("turn off ", turnOffInstruction);
    // }

    [Theory(Skip = SkipOrNot)]
    [MemberData(nameof(ApplicableLightsData))]
    public void ApplicableLights(Point2D_rrs upperLeftCorner, Point2D_rrs lowerRightCorner, HashSet<Point2D_rrs> expectedGrid)
    {
        // Given
        // When
        var actualApplicableLights = Day6.ApplicableLights(upperLeftCorner, lowerRightCorner);
        // Then
        Assert.True(actualApplicableLights.SetEquals(expectedGrid));
    }
    public static TheoryData<Point2D_rrs, Point2D_rrs, HashSet<Point2D_rrs>> ApplicableLightsData => new()
    {
        {
            new Point2D_rrs(1, 1),
            new Point2D_rrs(2, 2),
            new HashSet<Point2D_rrs>() { new(1, 1), new(1, 2), new(2, 1), new(2, 2) }
        },
        {
            new Point2D_rrs(1, 0),
            new Point2D_rrs(2, 3),
            new HashSet<Point2D_rrs>() { new(1, 0), new(1, 1), new(1, 2), new(1, 3), new(2, 0), new(2, 1), new(2, 2), new(2, 3) }
        }
    };
}