namespace AdventOfCode._2022;

public class Day4 : Puzzle
{
    public readonly record struct Coordinates(int Start, int End);
    public readonly record struct Pair(Coordinates First, Coordinates Second);
    private readonly List<Pair> _pairs;

    public Day4(string inputFileName) : base(inputFileName)
    {
        _pairs = InstructionLines.ConvertAll(ToPair);
    }

    // In how many assignment pairs does one range fully contain the other
    public override int Solve_Part1() => _pairs.Count(IsFullyOverlapping);

    private Pair ToPair(string line)
    {
        string[] pairs = line.Split(',');
        string[] firstCoordinates = pairs[0].Split('-');
        string[] secondCoordinates = pairs[1].Split('-');
        Coordinates first = new()
        {
            Start = int.Parse(firstCoordinates[0]),
            End = int.Parse(firstCoordinates[1]),
        };

        Coordinates second = new()
        {
            Start = int.Parse(secondCoordinates[0]),
            End = int.Parse(secondCoordinates[1]),
        };

        Pair pair = new(first, second);
        return pair;
    }

    private bool IsFullyOverlapping(Pair pair)
    {
        // is first included in second
        if (pair.First.Start >= pair.Second.Start && pair.First.End <= pair.Second.End)
            return true;
        // is second included in first
        if (pair.Second.Start >= pair.First.Start && pair.Second.End <= pair.First.End)
            return true;

        return false;
    }

    public override int Solve_Part2() => _pairs.Count(IsOverlapping);

    private bool IsOverlapping(Pair pair)
    {
        if (pair.First.Start > pair.Second.End)
            return false;
        if (pair.Second.Start > pair.First.End)
            return false;
        return true;
    }
}