using System;
using static AdventOfCode._2015.Day6;
namespace Tests._2015;

public class Day6Tests
{
    public class Part1
    {
        const string SkipOrNot = null; // Run all tests
        // const string SkipOrNot = "Remove the Skip argument to run this test."; // Skip all tests

        [Theory(Skip = SkipOrNot)]
        [MemberData(nameof(GridData))]
        public void Part1_BuildLightsGrid_UsingDictionary(Point2D_rrs lowerRight, int expectedSize)
        {
            var grid = BuildGrid_UsingDictionary(lowerRight);
            int actualSize = grid.Count;
            Assert.True(actualSize == expectedSize, $"The grid size was expected to be {expectedSize} but was {actualSize}.");
        }
        public static TheoryData<Point2D_rrs, int> GridData => new()
        {
            { new Point2D_rrs(2,2), 9},
            { new Point2D_rrs(3,3), 16}
        };

        [Fact(Skip = SkipOrNot)]
        public void Part1_BuildLightsGrid_UsingCustomType()
        {
            // Given
            (int rows, int columns) = (3, 3);
            Matrix<LightStatus> lightsGrid = new(rows, columns);
            // When
            var expectedSize = rows * columns;
            var actualSize = lightsGrid.Count();
            const LightStatus expectedStatus = LightStatus.Off;
            // Then
            Assert.True(lightsGrid.Count() == expectedSize, $"The grid should have {expectedSize} lights, but {actualSize} were found.");
            Assert.True(lightsGrid.All(l => l == expectedStatus), "All lights should be off by default, but not all are.");
        }

        [Fact(Skip = SkipOrNot)]
        public void Part1_UpdateLightsGrid_TurnOnSomeLights()
        {
            // Given
            (int rows, int columns) = (3, 3);
            Matrix<LightStatus> lightsGrid = new(rows, columns);
            const string InstructionLine = "turn on 1,1 through 2,2";
            var instruction = ParseInstruction(InstructionLine);
            Func<LightAction, Func<LightStatus, LightStatus>> applyLightAction = ApplyLightAction;
            const int ExpectedLightsOn = 4;
            // When
            UpdateLightsGrid(ref lightsGrid, instruction.LightAction, applyLightAction, instruction.UpperLeftCorner, instruction.LowerRightCorner);
            // Then
            int actualLightsOn = lightsGrid.Count(l => l == LightStatus.On);
            Assert.True(actualLightsOn == ExpectedLightsOn, $"There should be {ExpectedLightsOn} lights on, but {actualLightsOn} found.");
        }

        [Theory(Skip = SkipOrNot)]
        [InlineData(LightAction.TurnOn, LightStatus.Off, LightStatus.On)]
        [InlineData(LightAction.TurnOn, LightStatus.On, LightStatus.On)]
        [InlineData(LightAction.TurnOff, LightStatus.Off, LightStatus.Off)]
        [InlineData(LightAction.TurnOff, LightStatus.On, LightStatus.Off)]
        [InlineData(LightAction.Toggle, LightStatus.Off, LightStatus.On)]
        [InlineData(LightAction.Toggle, LightStatus.On, LightStatus.Off)]
        public void Part1_ApplyLightAction(LightAction lightAction, LightStatus initialLightStatus, LightStatus expectedFinalLightStatus)
        {
            // Given
            Func<LightAction, Func<LightStatus, LightStatus>> applyLightAction = ApplyLightAction;
            // When
            var actualFinalLightStatus = applyLightAction(lightAction)(initialLightStatus);
            // Then
            Assert.Equal(expectedFinalLightStatus, actualFinalLightStatus);
        }
    }

    public class Part2
    {
        const string SkipOrNot = null; // Run all tests
        // const string SkipOrNot = "Remove the Skip argument to run this test."; // Skip all tests

        [Theory(Skip = SkipOrNot)]
        [InlineData(LightAction.TurnOn, 10, 11)]
        [InlineData(LightAction.TurnOff, 10, 9)]
        [InlineData(LightAction.Toggle, 10, 12)]
        public void Part2_ApplyLightAction(LightAction lightAction, int startValue, int expectedEndValue)
        {
            // Given
            Func<LightAction, Func<int, int>> applyLightAction = ApplyLightAction_Part2;
            // When
            int actualEndValue = applyLightAction(lightAction)(startValue);
            // Then
            Assert.Equal(expectedEndValue, actualEndValue);
        }
    }
}
