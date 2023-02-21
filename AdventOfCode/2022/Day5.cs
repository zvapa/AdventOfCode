namespace AdventOfCode._2022;

public class Day5 : Puzzle
{
    readonly record struct Move(int Quantity, int FromStack, int ToStack);
    readonly Dictionary<int, Stack<char>> _stacks = new();
    readonly List<Move> _moveInstructions = new();

    public Day5(string inputFileName) : base(inputFileName)
    {
        string[] instructionParts = Instructions.Split(
            new string[] { "\n\n" },
            StringSplitOptions.RemoveEmptyEntries);
        GetStacks(instructionParts[0]);
        _moveInstructions = GetInstructions(instructionParts[1]);
    }

    private void GetStacks(string instructions)
    {
        List<string> lines = instructions.Split('\n').ToList();
        // start from the bottom line to get the keys;
        lines.Reverse();
        foreach (string s in lines[0].Split(' ', StringSplitOptions.RemoveEmptyEntries))
        {
            _ = int.TryParse(s, out int key);
            _stacks.Add(key, new());
        }
        // build the stacks
        foreach (var line in lines.Skip(1))
        {
            // split every 4th char
            List<char> chars = new(line);
            List<List<char>> chunks = SplitIntoChunks(chars, 4);
            // get the second one, after the '['
            int key = 1;
            foreach (List<char> chunk in chunks)
            {
                if (chunk[0] is '[')
                {
                    _stacks[key].Push(chunk[1]);
                }
                key++;
            }
        }
    }

    private static List<List<T>> SplitIntoChunks<T>(List<T> fullList, int batchSize)
    {
        var chunkedList = new List<List<T>>();
        var temporary = new List<T>();
        for (int i = 0; i < fullList.Count; i++)
        {
            var e = fullList[i];
            if (temporary.Count < batchSize)
            {
                temporary.Add(e);
            }
            else
            {
                chunkedList.Add(temporary);
                // clear temp list and add next element
                temporary = new List<T>() { e };
            }
            // last element
            if (i == fullList.Count - 1) chunkedList.Add(temporary);
        }
        return chunkedList;
    }

    private void ExecuteMove_LIFO(Move move)
    {
        int loads = move.Quantity;
        while (loads > 0)
        {
            char crate = _stacks[move.FromStack].Pop();
            _stacks[move.ToStack].Push(crate);
            loads--;
        }
    }

    private void ExecuteMove_FIFO(Move move)
    {
        Stack<char> intermediate = new();
        int loads = move.Quantity;
        while (loads > 0)
        {
            char crate = _stacks[move.FromStack].Pop();
            intermediate.Push(crate);
            loads--;
        }
        loads = move.Quantity;
        while (loads > 0)
        {
            char crate = intermediate.Pop();
            _stacks[move.ToStack].Push(crate);
            loads--;
        }
    }

    private static List<Move> GetInstructions(string instructions) =>
        instructions
            .Split("\n", StringSplitOptions.RemoveEmptyEntries)
            .Select(s =>
            {
                string[] move = s.Split(' ');
                return new Move(
                    Quantity: int.Parse(move[1]),
                    FromStack: int.Parse(move[3]),
                    ToStack: int.Parse(move[5]));
            })
            .ToList();

    // which crate will end up on top of each stack
    public override object Solve_Part1()
    {
        // execute moves
        _moveInstructions.ForEach(ExecuteMove_LIFO);
        var finalArrangement = string.Concat(_stacks.Select(s => s.Value.Pop()));
        return finalArrangement;
    }

    public override object Solve_Part2()
    {
        _moveInstructions.ForEach(ExecuteMove_FIFO);
        var finalArrangement = string.Concat(_stacks.Select(s => s.Value.Pop()));
        return finalArrangement;
    }
}