namespace AdventOfCode._2015;

public class Day4 : Puzzle
{
    public Day4(string inputFileName) : base(inputFileName) { }

    public override int Solve_Part1()
    {
        return Solve_Using_Linq(StartsWithAtLeastFiveZeros);
        // return Solve_Using_Loop(StartsWithAtLeastFiveZeros);
    }

    public override int Solve_Part2()
    {
        return Solve_Using_Linq(StartsWithAtLeastSixZeros);
        // return Solve_Using_Loop();
    }

    public int Solve_Using_Linq(Func<string, bool> predicate)
    {
        string key = _instructions[0];
        using MD5 md5Hash = MD5.Create();
        int result = Enumerable
            .Range(1, int.MaxValue)
            .Select(n => (number: n, hash: GetMd5Hash(md5Hash, key + n)))
            .FirstOrDefault(t => predicate(t.hash))
            .number
            ;
        return result;
    }

    public int Solve_Using_Loop(Func<string, bool> predicate)
    {
        string key = _instructions[0];
        using MD5 md5Hash = MD5.Create();
        for (int i = 0; i < int.MaxValue; i++)
        {
            if (predicate(GetMd5Hash(md5Hash, key + i)))
            {
                return i;
            }
        }
        throw new Exception("No answer could be found for the given input to this puzzle!");
    }
    public static string GetMd5Hash(MD5 md5Hash, string input)
    {
        // Convert the input string to a byte array and compute the hash.
        byte[] data = md5Hash.ComputeHash(Encoding.ASCII.GetBytes(input));

        // Create a new Stringbuilder to collect the bytes
        StringBuilder sBuilder = new();
        // Loop through each byte of the hashed data
        // and format each one as a hexadecimal string.
        for (int i = 0; i < data.Length; i++)
        {
            sBuilder.Append(data[i].ToString("x2"));
        }
        // Return the hexadecimal string.
        // return sBuilder.ToString();

        return Convert.ToHexString(data);
    }

    public static bool StartsWithAtLeastFiveZeros(string hash) => hash.StartsWith("00000");
    public static bool StartsWithAtLeastSixZeros(string hash) => hash.StartsWith("000000");
}