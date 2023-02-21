namespace AdventOfCode;

public abstract class Puzzle
{
    public readonly List<string> InstructionLines;
    public readonly string Instructions;

    protected Puzzle(string inputFileName)
    {
        string? subpath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var path = Path.Combine(subpath!, inputFileName);

        InstructionLines = Readers.ReadInstructionLines(path);
        Instructions = Readers.ReadInstructions(path);
    }
    public abstract object Solve_Part1();
    public abstract object Solve_Part2();
}