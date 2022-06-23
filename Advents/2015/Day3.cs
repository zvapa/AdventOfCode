using Utils;

namespace AdventOfCode._2015;

public class Day3 : Puzzle
{
    private static readonly Point2D _start = new(0, 0);
    private static readonly Point2D_struct _start_s = new Point2D_struct(0, 0);

    /// <summary>
    /// A dictionary of locations and how many times they were visited.
    /// </summary>
    private static readonly Dictionary<Point2D, int> _houses = new() { [_start] = 1 };
    private static readonly Dictionary<Point2D_struct, int> _houses_s = new() { [_start_s] = 1 };

    public Day3(string inputFileName) : base(inputFileName) { }

    /// <summary>
    /// How many houses receive at least one present?
    /// </summary>
    public override int Solve_Part1()
    {
        return Solve_Part1_UsingRecords();
    }

    public int Solve_Part1_UsingRecords()
    {
        Point2D currentLocation = _start;
        foreach (char instruction in _instructions[0])
        {
            currentLocation = currentLocation.NextDeliveryLocation(instruction);
            MarkHouse(currentLocation);
        }
        return _houses.Count;
    }

    public int Solve_Part1_UsingReadonlyStructs()
    {
        return 0;
    }


    public override int Solve_Part2()
    {
        throw new NotImplementedException();
    }

    public void MarkHouse(Point2D location)
    {
        if (!_houses.TryGetValue(location, out _))
        {
            _houses.Add(location, 1);
        }
        else
        {
            _houses[location]++;
        }
    }
}
