namespace AdventOfCode._2015;

public class Day6 : Puzzle
{
    public Day6(string inputFileName) : base(inputFileName) { }

    public enum LightAction { TurnOn, TurnOff, Toggle }
    public enum LightStatus { Off, On }
    public readonly record struct Instruction(LightAction LightAction, (int x, int y) UpperLeftCorner, (int x, int y) LowerRightCorner);

    public override int Solve_Part1()
    {
        var grid = new Matrix<LightStatus>(1000, 1000);
        foreach (var line in _instructions)
        {
            Instruction instruction = ParseInstruction(line);
            LightAction lightAction = instruction.LightAction;
            Func<LightAction, Func<LightStatus, LightStatus>> applyLightAction = ApplyLightAction;
            UpdateLightsGrid(ref grid, instruction.UpperLeftCorner, instruction.LowerRightCorner, applyLightAction(lightAction));
        }
        return grid.Count(l => l == LightStatus.On);
    }

    public static void UpdateLightsGrid(ref Matrix<LightStatus> grid, (int x, int y) upperLeftCorner, (int x, int y) lowerRightCorner, Func<LightStatus, LightStatus> @switch)
    {
        for (int x = upperLeftCorner.x; x <= lowerRightCorner.x; x++)
        {
            for (int y = upperLeftCorner.y; y <= lowerRightCorner.y; y++)
            {
                grid[x, y] = @switch(grid[x, y]);
            }
        }
    }

    public override int Solve_Part2()
    {
        //todo how to quick fix all

        throw new NotImplementedException();
    }

    public static Dictionary<Point2D_rrs, bool> BuildGrid_UsingDictionary(Point2D_rrs upperLeftCorner, Point2D_rrs lowerRightCorner)
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
            LightAction.TurnOn => (lightStatus) => lightStatus = LightStatus.On,
            LightAction.TurnOff => (lightStatus) => lightStatus = LightStatus.Off,
            LightAction.Toggle => (lightStatus) => lightStatus == LightStatus.On ? lightStatus = LightStatus.Off : lightStatus = LightStatus.On,
            _ => throw new NotImplementedException(),
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

    public static HashSet<Point2D_rrs> ApplicableLights(Point2D_rrs upperLeftCorner, Point2D_rrs lowerRightCorner)
    {
        HashSet<Point2D_rrs> applicableLights = new();
        for (int x = upperLeftCorner.X; x <= lowerRightCorner.X; x++)
        {
            for (int y = upperLeftCorner.Y; y <= lowerRightCorner.Y; y++)
            {
                applicableLights.Add(new Point2D_rrs(x, y));
            }
        }
        return applicableLights;
    }

    public static HashSet<(int x, int y)> ApplicableLights((int x, int y) upperLeftCorner, (int x, int y) lowerRightCorner)
    {
        HashSet<(int x, int y)> applicableLights = new();
        for (int x = upperLeftCorner.x; x <= lowerRightCorner.x; x++)
        {
            for (int y = upperLeftCorner.y; y <= lowerRightCorner.y; y++)
            {
                applicableLights.Add((x, y));
            }
        }
        return applicableLights;
    }
}