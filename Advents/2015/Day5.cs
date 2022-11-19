using Utils;

namespace AdventOfCode._2015;

public class Day5 : Puzzle
{
    public Day5(string inputFileName) : base(inputFileName) { }

    public override int Solve_Part1()
    {
        return Solve_Part1_Linq().Count;
    }

    public List<string> Solve_Part1_Linq()
    {
        var source = _instructions;

        var seed = new Day5Part1Accumulator(
                    Previous: '\0',
                    CharAppearsTwice: false,
                    VowelsCount: new Dictionary<char, int> { ['a'] = 0, ['e'] = 0, ['i'] = 0, ['o'] = 0, ['u'] = 0 },
                    ExcludedStringsCount: new Dictionary<string, int> { ["ab"] = 0, ["cd"] = 0, ["pq"] = 0, ["xy"] = 0 }
                    );

        return source.Where(IsNiceString).ToList();
    }

    public List<string> Solve_Part1_Loop()
    {
        var result = new List<string>();

        foreach (var word in _instructions)
        {
            var vowelsCount = new Dictionary<char, int> { ['a'] = 0, ['e'] = 0, ['i'] = 0, ['o'] = 0, ['u'] = 0 };
            var excludedStrings = new HashSet<string> { "ab", "cd", "pq", "xy" };
            var prevChar = '\0';

            bool containsAtLeastThreeVowels = false;
            bool containsAtLeastOneLetterTwiceInARow = false;
            bool containsExcludedString = false;

            foreach (var currentChar in word)
            {
                // It contains at least one letter that appears twice in a row
                if (prevChar == currentChar)
                {
                    containsAtLeastOneLetterTwiceInARow = true;
                }

                // does not contain the strings ab, cd, pq, or xy, even if they are part of one of the other requirements.
                var currentPair = new string(new char[] { prevChar, currentChar });
                if (excludedStrings.Contains(currentPair))
                {
                    containsExcludedString = true;
                }

                if (vowelsCount.Keys.Contains(currentChar))
                {
                    vowelsCount[currentChar]++;
                }

                prevChar = currentChar;
            }

            // must contain at least three vowels
            if (vowelsCount.Values.Sum() >= 3)
            {
                containsAtLeastThreeVowels = true;
            }

            if (containsAtLeastThreeVowels && containsAtLeastOneLetterTwiceInARow && !containsExcludedString)
            {
                result.Add(word);
            }
        }
        return result;
    }

    private static Day5Part1Accumulator Day5Part1Accumulate(Day5Part1Accumulator acc, char currentChar)
    {
        if (acc.VowelsCount.ContainsKey(currentChar))
        {
            acc.VowelsCount[currentChar]++;
        }

        var currentCharPair = new string(new[] { acc.Previous, currentChar });
        if (acc.ExcludedStringsCount.ContainsKey(currentCharPair))
        {
            acc.ExcludedStringsCount[currentCharPair]++;
        }

        var newAcc = acc with
        {
            Previous = currentChar,
            VowelsCount = acc.VowelsCount,
            CharAppearsTwice = acc.CharAppearsTwice ? acc.CharAppearsTwice : currentChar == acc.Previous,
            ExcludedStringsCount = acc.ExcludedStringsCount
        };

        return newAcc;
    }

    private static bool IsNiceString(string s)
    {
        var seed = new Day5Part1Accumulator(
                    Previous: '\0',
                    CharAppearsTwice: false,
                    VowelsCount: new Dictionary<char, int> { ['a'] = 0, ['e'] = 0, ['i'] = 0, ['o'] = 0, ['u'] = 0 },
                    ExcludedStringsCount: new Dictionary<string, int> { ["ab"] = 0, ["cd"] = 0, ["pq"] = 0, ["xy"] = 0 }
                    );

        var finalAcc = s.Aggregate(seed: seed, Day5Part1Accumulate);

        return
            finalAcc.VowelsCount.Values.Sum() >= 3 &&
            finalAcc.CharAppearsTwice &&
            finalAcc.ExcludedStringsCount.Values.Sum() == 0;
    }

    public override int Solve_Part2()
    {
        // return Solve_Part2_TestBothConditionsInOneParsing();
        return Solve_Part2_TestEachConditionSeparately();
    }

    public int Solve_Part2_TestEachConditionSeparately()
    {
        var niceWordsCount = 0;
        foreach (var word in _instructions)
        {
            if (ContainsAtLeastOneLetterWhichRepeatsWithExactlyOneLetterBetweenThem(word) &&
                ContainsNonOverlappingLetterPairThatAppearsAtLeastTwice(word))
            {
                niceWordsCount++;
            }
        }
        return niceWordsCount;
    }

    public int Solve_Part2_TestBothConditionsInOneParsing()
    {
        var niceWordsCount = 0;
        foreach (var word in _instructions)
        {
            if (IsNiceWord(word))
            {
                niceWordsCount++;
            }
        }
        return niceWordsCount;
    }

    public List<string> Part2_GetNiceWords()
    {
        List<string> res = new();
        foreach (var word in _instructions)
        {
            if (IsNiceWord(word))
            {
                res.Add(word);
            }
        }
        return res;
    }

    public static bool IsNiceWord(string word)
    {
        // the List<int> values of this dictionary represent the positions where this pair is encountered, i.e. sums of indices of the letters
        Dictionary<string, List<int>> pairsAndPositions = new() { };
        List<char> lettersRepeatingWithExactlyOneLetterInBetween = new();

        char? lastLetter = null;
        char? letterBeforeLast = null;
        for (int i = 0; i < word.Length; i++)
        {
            char currentLetter = word[i];

            if (lastLetter is null)
            {
                lastLetter = currentLetter;
                continue;
            }

            string currentPair = new(new char[] { lastLetter.Value, currentLetter });
            if (pairsAndPositions.TryGetValue(currentPair, out List<int>? value))
            {
                value.Add(i - 1 + i);
            }
            else
            {
                pairsAndPositions.Add(currentPair, new List<int> { i - 1 + i });
            }

            if (letterBeforeLast is null)
            {
                letterBeforeLast = lastLetter;
                lastLetter = currentLetter;
                continue;
            }

            if (letterBeforeLast == currentLetter)
            {
                lettersRepeatingWithExactlyOneLetterInBetween.Add(currentLetter);
            }

            letterBeforeLast = lastLetter;
            lastLetter = currentLetter;
        }

        bool isThereALetterRepeatingWithExactlyOneLetterInBetween = lettersRepeatingWithExactlyOneLetterInBetween.Count > 0;
        bool isThereANonOverlappingLetterPairThatAppearsAtLeastTwice = IsThereANonOverlappingLetterPairThatAppearsAtLeastTwice(pairsAndPositions);

        return isThereALetterRepeatingWithExactlyOneLetterInBetween && isThereANonOverlappingLetterPairThatAppearsAtLeastTwice;
    }

    public static bool IsThereANonOverlappingLetterPairThatAppearsAtLeastTwice(Dictionary<string, List<int>> pairsAndPositions)
    {
        var pairsAppearingTwice = pairsAndPositions.Where(kvp => pairsAndPositions[kvp.Key].Count > 1);

        if (!pairsAppearingTwice.Any())
        {
            return false;
        }

        // the difference between any 2 positions (sums of indices) has to be greater than 2 in order for them not to overlap
        foreach (var (pair, positions) in pairsAppearingTwice)
        {
            var firstAppearance = positions[0];

            foreach (var position in positions.Skip(1))
            {
                return position - firstAppearance > 2;
            }
        }
        return false;
    }

    public static bool ContainsNonOverlappingLetterPairThatAppearsAtLeastTwice(string @string)
    {
        Dictionary<string, List<int>> pairsAndPositions = new() { };
        char? lastLetter = null;
        for (int i = 0; i < @string.Length; i++)
        {
            char currentLetter = @string[i];
            if (lastLetter is null)
            {
                lastLetter = currentLetter;
                continue;
            }
            string currentPair = new(new char[] { lastLetter.Value, currentLetter });
            if (pairsAndPositions.TryGetValue(currentPair, out List<int>? value))
            {
                value.Add(i - 1 + i);
            }
            else
            {
                pairsAndPositions.Add(currentPair, new List<int> { i - 1 + i });
            }
            lastLetter = currentLetter;
        }

        return IsThereANonOverlappingLetterPairThatAppearsAtLeastTwice(pairsAndPositions);
    }

    public static bool ContainsAtLeastOneLetterWhichRepeatsWithExactlyOneLetterBetweenThem(string @string)
    {
        List<char> lettersRepeatingWithExactlyOneLetterInBetween = new();
        char? lastLetter = null;
        char? letterBeforeLast = null;
        for (int i = 0; i < @string.Length; i++)
        {
            char currentLetter = @string[i];
            if (lastLetter is null || letterBeforeLast is null)
            {
                letterBeforeLast = lastLetter;
                lastLetter = currentLetter;
                continue;
            }

            if (letterBeforeLast == currentLetter)
            {
                lettersRepeatingWithExactlyOneLetterInBetween.Add(currentLetter);
            }

            letterBeforeLast = lastLetter;
            lastLetter = currentLetter;
        }

        return lettersRepeatingWithExactlyOneLetterInBetween.Count > 0;
    }
}