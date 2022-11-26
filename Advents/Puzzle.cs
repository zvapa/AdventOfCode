namespace AdventOfCode;

public abstract class Puzzle
{
    public readonly List<string> _instructions;

    protected Puzzle(string inputFileName)
    {
        string? subpath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var path = Path.Combine(subpath!, inputFileName);

        _instructions = Readers.ReadInstructionLines(path);
    }

    public abstract int Solve_Part1();
    public abstract int Solve_Part2();
}
