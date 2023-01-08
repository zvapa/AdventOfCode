namespace AdventOfCode._2015;

public class Day2 : Puzzle
{
    public Day2(string inputFileName) : base(inputFileName) { }

    /// <summary>
    /// How many total square feet of wrapping paper should they order?
    /// </summary>
    public override int Solve_Part1()
    {
        return Solve_Part1_Method3();
    }

    /// <summary>
    /// How many total feet of ribbon should they order?
    /// </summary>
    public override int Solve_Part2()
    {
        return Solve_Part2_Method1();
    }

    public int Solve_Part1_Method1() =>
            InstructionLines
                .Select(GetDimensions)
                .Select(d => Area(d.length, d.width, d.height) + SmallestAreaAndPerimeter(d.length, d.width, d.height).smallestArea)
                .Sum();

    public int Solve_Part1_Method2() =>
            InstructionLines
                .Select(s => s.Split('x'))
                .Select(x => x.Select(int.Parse))
                .Select(w => w.OrderBy(x => x).ToArray())
                .Select(w => (3 * w[0] * w[1]) + (2 * w[0] * w[2]) + (2 * w[1] * w[2]))
                .Sum();

    public int Solve_Part1_Method3()
    {
        int sum = 0;
        foreach (var line in InstructionLines)
        {
            (int length, int width, int height) = GetDimensions(line);

            if (length >= width && length >= height)
            {
                sum += (3 * width * height) + (2 * width * length) + (2 * length * height);
                continue;
            }

            if (width >= length && width >= height)
            {
                sum += (3 * length * height) + (2 * length * width) + (2 * width * height);
                continue;
            }

            if (height >= length && height >= width)
            {
                sum += (3 * length * width) + (2 * length * height) + (2 * width * height);
            }
        }
        return sum;
    }

    public int Solve_Part2_Method1()
    {
        return InstructionLines
            .Select(GetDimensions)
            .Select(d => SmallestAreaAndPerimeter(d.length, d.width, d.height).smallestPerimeter + BowLength(d.length, d.width, d.height))
            .Sum();
    }

    private static (int smallestArea, int smallestPerimeter) SmallestAreaAndPerimeter(int length, int width, int height)
    {
        if (length >= width && length >= height) return (width * height, 2 * (width + height));
        if (width >= length && width >= height) return (length * height, 2 * (length + height));
        if (height >= length && height >= width) return (length * width, 2 * (length + width));
        return (0, 0);
    }

    private static int BowLength(int length, int width, int height) => length * width * height;

    /// <summary>
    /// Returns a tuple of (length, width, height) dimensions given a string (ex: "20x3x11" -> (20,3,11))
    /// </summary>
    private static (int length, int width, int height) GetDimensions(string strDimensions)
    {
        var splitDimensions = strDimensions.Split('x');
        _ = int.TryParse(splitDimensions[0], out var l);
        _ = int.TryParse(splitDimensions[1], out var w);
        _ = int.TryParse(splitDimensions[2], out var h);
        return (l, w, h);
    }

    /// <summary>
    /// The area of a box (squared feet), given its length, width and height (feet).
    /// </summary>
    private static int Area(int length, int width, int height) => (2 * length * width) + (2 * width * height) + (2 * height * length);
}