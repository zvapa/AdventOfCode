namespace AdventOfCode._2015;

public class Day6 : Puzzle
{
    public Day6(string inputFileName) : base(inputFileName) { }

    public enum LightAction { TurnOn, TurnOff, Toggle }
    public enum LightStatus { Off, On }
    public readonly record struct Instruction(LightAction LightAction, (int x, int y) StartSelection_UpperLeftCorner, (int x, int y) EndSelection_LowerRightCorner);

    public override int Solve_Part1()
    {
        var grid = new Matrix<LightStatus>(1000, 1000);
        foreach (var line in _instructions)
        {
            var instr = ParseInstruction(line);
            DoAsInstructed(instr, ref grid);
        }
        return grid.Count(l => l == LightStatus.On);
    }

    public override int Solve_Part2()
    {
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

    public void DoAsInstructed(Instruction instruction, ref Matrix<LightStatus> grid)
    {
        var applicableLights = ApplicableLights(instruction.StartSelection_UpperLeftCorner, instruction.EndSelection_LowerRightCorner);

        foreach (var lightAddress in applicableLights)
        {
            switch (instruction.LightAction)
            {
                case LightAction.TurnOn:
                    grid[lightAddress.x, lightAddress.y] = LightStatus.On;
                    break;
                case LightAction.TurnOff:
                    grid[lightAddress.x, lightAddress.y] = LightStatus.Off;
                    break;
                case LightAction.Toggle:
                    grid[lightAddress.x, lightAddress.y] = grid[lightAddress.x, lightAddress.y] == LightStatus.On ? LightStatus.Off : LightStatus.On;
                    break;
            }
        }
    }

    public static Instruction ParseInstruction(string @string)
    {
        (int x, int y) upperLeftCorner = new();
        (int x, int y) lowerRightCorner = new();
        LightAction instruction = default;
        var turn_on = "turn on ";
        var turn_off = "turn off ";
        var toggle = "toggle ";
        var splitString = " through ";

        foreach (var instructionText in new string[] { turn_on, turn_off, toggle })
        {
            if (@string.StartsWith(instructionText))
            {
                var coordsText = @string.Remove(0, instructionText.Length).Split(splitString);

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

    public static HashSet<(int x, int y)> ApplicableLights((int x, int y) StartSelection_UpperLeftCorner, (int x, int y) EndSelection_LowerRightCorner)
    {
        HashSet<(int x, int y)> applicableLights = new();
        for (int x = StartSelection_UpperLeftCorner.x; x <= EndSelection_LowerRightCorner.x; x++)
        {
            for (int y = StartSelection_UpperLeftCorner.y; y <= EndSelection_LowerRightCorner.y; y++)
            {
                applicableLights.Add((x, y));
            }
        }
        return applicableLights;
    }
}