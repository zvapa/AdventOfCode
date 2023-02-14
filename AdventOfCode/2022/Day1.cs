namespace AdventOfCode._2022;

public class Day1 : Puzzle
{
    public Day1(string instructions) : base(instructions) { }

    // how many Calories are being carried by the Elf carrying the most Calories?
    public override int Solve_Part1()
    {
        return ElvesCalories().Max();
    }

    private List<int> ElvesCalories()
    {
        return Instructions
            .Split(new string[] { "\n\n" }, StringSplitOptions.RemoveEmptyEntries)
            .Select(g => g.Split("\n", StringSplitOptions.RemoveEmptyEntries).Sum(n => int.Parse(n)))
            .ToList();
    }

    /*
    Find the top three Elves carrying the most Calories.
    How many Calories are those Elves carrying in total?
    */
    public override int Solve_Part2()
    {
        var x = ElvesCalories().ToArray();
        Array.Sort(x);
        return x[^3..].Sum();
    }
}