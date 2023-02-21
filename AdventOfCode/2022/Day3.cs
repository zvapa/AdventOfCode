namespace AdventOfCode._2022;

public class Day3 : Puzzle
{
    readonly record struct RucksackItem(char Letter, int Priority);

    private readonly HashSet<RucksackItem> _rucksackItems = new();
    private readonly Dictionary<char, int> _lettersAndPriority = new();

    public Day3(string inputFileName) : base(inputFileName)
    {
        for ((char c, int i) = ('a', 1); c <= 'z'; c++, i++)
        {
            _lettersAndPriority.Add(c, i);
        }
        for ((char c, int i) = ('A', 27); c <= 'Z'; c++, i++)
        {
            _lettersAndPriority.Add(c, i);
        }
    }

    public override object Solve_Part1() => InstructionLines.Sum(SumOfDuplicates);

    private int SumOfDuplicates(string line)
    {
        List<char> duplicates = new();
        bool watchForDuplicates = false;
        HashSet<char> chars = new();
        for (int i = 0; i < line.Length; i++)
        {
            char current = line[i];
            if (!watchForDuplicates) _ = chars.Add(current);
            if (watchForDuplicates && chars.Contains(current))
            {
                duplicates.Add(current);
                break;
            }
            if (i == line.Length / 2 - 1) watchForDuplicates = true;
        }
        return duplicates.Sum(c => _lettersAndPriority[c]);
    }

    public override object Solve_Part2()
    {
        var groups = SplitIntoChunks(InstructionLines, 3);
        int sumOfThePriorities = groups
            .Select(g => FindGroupKey(g.ToArray()))
            .Sum(c => _lettersAndPriority[c]);
        return sumOfThePriorities;
    }

    private static IEnumerable<IEnumerable<T>> SplitIntoChunks<T>(
        List<T> fullList,
        int batchSize)
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
                temporary = new List<T>() { e };
            }
            // last element
            if (i == fullList.Count - 1) chunkedList.Add(temporary);
        }
        return chunkedList;
    }

    // find the only item type that appears in all three
    public static char FindGroupKey(string[] groupLines)
    {
        HashSet<char> firstLine = groupLines[0].ToHashSet();
        HashSet<char> secondLine = groupLines[1].ToHashSet();
        HashSet<char> thirdLine = groupLines[2].ToHashSet();
        firstLine.IntersectWith(secondLine);
        firstLine.IntersectWith(thirdLine);
        return firstLine.SingleOrDefault();
    }
}