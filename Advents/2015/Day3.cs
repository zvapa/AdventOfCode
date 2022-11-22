namespace AdventOfCode._2015;

public class Day3 : Puzzle
{
    private static readonly Point2D _start = new(0, 0);
    private static readonly Point2D_rs _start_s = new(0, 0);

    /// <summary>
    /// A dictionary of locations and how many times they were visited. Includes the starting location.
    /// </summary>
    private static readonly Dictionary<Point2D, int> _houses = new() { [_start] = 1 };
    private static readonly Dictionary<Point2D_rs, int> _houses_s = new() { [_start_s] = 1 };

    public Day3(string inputFileName) : base(inputFileName) { }

    /// <summary>
    /// How many houses receive at least one present?
    /// </summary>
    public override int Solve_Part1()
    {
        return Solve_Part1_UsingRecords();
        // return Solve_Part1_UsingReadonlyStructs();
    }

    public int Solve_Part1_UsingRecords()
    {
        Point2D currentLocation = _start;
        foreach (char instruction in _instructions[0])
        {
            currentLocation = currentLocation.NextLocation(instruction);
            MarkHouse(currentLocation);
        }
        return _houses.Count;
    }

    public int Solve_Part1_UsingReadonlyStructs()
    {
        Point2D_rs currentLocation = _start_s;
        foreach (char instruction in _instructions[0])
        {
            currentLocation = currentLocation.NextLocation(instruction);
            MarkHouse(currentLocation);
        }
        return _houses_s.Count;
    }

    public override int Solve_Part2()
    {
        bool isSantasTurn = true;
        Point2D santasCurrentLocation = _start;
        Point2D roboSantasCurrentLocation = _start;
        _houses[_start] = 2; // both Santa and Robo-Santa delivered a present here

        foreach (char instruction in _instructions[0])
        {
            if (isSantasTurn)
            {
                santasCurrentLocation = santasCurrentLocation.NextLocation(instruction);
                MarkHouse(santasCurrentLocation);
                isSantasTurn = false;
            }
            else
            {
                roboSantasCurrentLocation = roboSantasCurrentLocation.NextLocation(instruction);
                MarkHouse(roboSantasCurrentLocation);
                isSantasTurn = true;
            }
        }
        return _houses.Count;
    }

    private static void MarkHouse(Point2D location)
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
    private static void MarkHouse(Point2D_rs location)
    {
        if (!_houses_s.TryGetValue(location, out _))
        {
            _houses_s.Add(location, 1);
        }
        else
        {
            _houses_s[location]++;
        }
    }
}
