namespace AdventOfCode;

public abstract class Puzzle
{
    public readonly List<string> _instructionLines;
    public readonly string _instructions;

    protected Puzzle(string inputFileName)
    {
        string? subpath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var path = Path.Combine(subpath!, inputFileName);

        _instructionLines = Readers.ReadInstructionLines(path);
        _instructions = Readers.ReadInstructions(path);
    }

    public abstract int Solve_Part1();
    public abstract int Solve_Part2();
}
