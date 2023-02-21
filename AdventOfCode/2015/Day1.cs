namespace AdventOfCode._2015;

public class Day1 : Puzzle
{
    public Day1(string instructions) : base(instructions) { }

    public override object Solve_Part1()
    {
        return Solve_Part1_With_Sum();
    }

    public int Solve_Part1_With_Sum() => InstructionLines[0]
        .Sum(c => GetFloorDirection(c));

    public int Solve_Part1_With_Aggregate() => InstructionLines[0]
            .Aggregate(0, (currentFloor, next) => currentFloor + GetFloorDirection(next));

    public override object Solve_Part2()
    {
        return Solve_Part2_With_First();
    }

    public int Solve_Part2_With_First()
    {
        int currentPosition = 0;
        return InstructionLines[0]
            .Select((c, i) => (index: i + 1, move: c))
            .First(t => IsEnteringBasement(ref currentPosition, t.move))
            .index
            ;
    }

    private static bool IsEnteringBasement(ref int currentPosition, char nextMove) =>
        (currentPosition += GetFloorDirection(nextMove)) == -1;

    private static int GetFloorDirection(char next) =>
        next == '(' ? 1     // '(' -> go up one floor
        : next == ')' ? -1  // ')' -> go down one floor
        : throw new ArgumentException("Unknown direction symbol");
}