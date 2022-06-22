using System.Reflection;
using Utils;

namespace AdventOfCode;

public abstract class Puzzle
{
    public readonly List<string> _instructions;

    protected Puzzle(string inputFileName)
    {
        var path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, inputFileName);
        _instructions = Readers.ReadInstructionLines(path);
    }

    public abstract int Solve_Part1();
    public abstract int Solve_Part2();
}
