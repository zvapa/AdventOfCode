namespace AdventOfCode._2015;

public class Day6 : Puzzle
{
    public enum LightAction { TurnOn, TurnOff, Toggle }
    public enum LightStatus { Off, On }
    public readonly record struct Instruction(LightAction LightAction, (int x, int y) UpperLeftCorner, (int x, int y) LowerRightCorner);

    public Day6(string inputFileName) : base(inputFileName) { }

    public override int Solve_Part1()
    {
        Matrix<LightStatus> grid = new(1000, 1000);
        Solve(ref grid, ApplyLightAction);
        return grid.Count(l => l == LightStatus.On);
    }

    public override int Solve_Part2()
    {
        Matrix<int> grid = new(1000, 1000);
        Solve(ref grid, ApplyLightAction_Part2);
        return grid.Sum();
    }

    private void Solve<TMatrix>(ref Matrix<TMatrix> grid, Func<LightAction, Func<TMatrix, TMatrix>> lightActionApplication)
    {
        foreach (var line in _instructionLines)
        {
            Instruction instruction = ParseInstruction(line);
            UpdateLightsGrid(ref grid, instruction.LightAction, lightActionApplication, instruction.UpperLeftCorner, instruction.LowerRightCorner);
        }
    }

    public static void UpdateLightsGrid<TMatrix>(
        ref Matrix<TMatrix> grid,
        LightAction lightAction,
        Func<LightAction, Func<TMatrix, TMatrix>> lightActionApplication,
        (int x, int y) upperLeftCorner = default,
        (int x, int y) lowerRightCorner = default)
    {
        for (int x = upperLeftCorner.x; x <= lowerRightCorner.x; x++)
        {
            for (int y = upperLeftCorner.y; y <= lowerRightCorner.y; y++)
            {
                grid[x, y] = lightActionApplication(lightAction)(grid[x, y]);
            }
        }
    }

    public static Dictionary<Point2D_rrs, bool> BuildGrid_UsingDictionary(Point2D_rrs lowerRightCorner)
    {
        Dictionary<Point2D_rrs, bool> gridLightsWithLitStatus = new();

        for (int x = 0; x <= lowerRightCorner.X; x++)
        {
            for (int y = 0; y <= lowerRightCorner.Y; y++)
            {
                gridLightsWithLitStatus.Add(new Point2D_rrs(x, y), false);
            }
        }
        return gridLightsWithLitStatus;
    }

    public static Func<LightStatus, LightStatus> ApplyLightAction(LightAction lightAction)
    {
        return lightAction switch
        {
            LightAction.TurnOn => lightStatus => lightStatus = LightStatus.On,
            LightAction.TurnOff => lightStatus => lightStatus = LightStatus.Off,
            LightAction.Toggle => lightStatus => lightStatus == LightStatus.On ? lightStatus = LightStatus.Off : lightStatus = LightStatus.On,
            _ => throw new NotImplementedException()
        };
    }

    /// <summary>
    /// From a <see cref="LightAction"/> to a <see cref="Func{int, int}"/> based on the new translation of the instructions.
    /// </summary>
    /// <param name="lightAction"></param>
    /// <returns>A func that changes a brightness level from one value to another.</returns>
    public static Func<int, int> ApplyLightAction_Part2(LightAction lightAction)
    {
        return lightAction switch
        {
            LightAction.TurnOn => level => ++level,
            LightAction.TurnOff => level => level == 0 ? 0 : --level,
            LightAction.Toggle => level => level + 2,
            _ => throw new NotImplementedException()
        };
    }

    public static Instruction ParseInstruction(string @string)
    {
        (int x, int y) upperLeftCorner = new();
        (int x, int y) lowerRightCorner = new();
        LightAction instruction = default;
        const string TurnOn = "turn on ";
        const string TurnOff = "turn off ";
        const string Toggle = "toggle ";
        const string SplitString = " through ";

        foreach (var instructionText in new string[] { TurnOn, TurnOff, Toggle })
        {
            if (@string.StartsWith(instructionText))
            {
                var coordsText = @string.Remove(0, instructionText.Length).Split(SplitString);
                var startingCoord = coordsText[0].Split(',').Map(int.Parse).ToList();
                var endingCoord = coordsText[1].Split(',').Map(int.Parse).ToList();
                upperLeftCorner = (startingCoord[0], startingCoord[1]);
                lowerRightCorner = (endingCoord[0], endingCoord[1]);
                instruction = (LightAction)Enum.Parse(
                    enumType: typeof(LightAction),
                    value: CultureInfo.CurrentCulture.TextInfo.ToTitleCase(instructionText).Replace(" ", string.Empty));
                break;
            }
        }

        return new Instruction(instruction, upperLeftCorner, lowerRightCorner);
    }
}