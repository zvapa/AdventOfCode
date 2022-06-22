using System.Text.Json;
using Utils;

namespace AdventOfCode;

public static class Program
{
    public static void Main()
    {
        var _2015_day1 = new _2015.Day1(@"2015\day1_input.txt");
        Console.WriteLine("_2015.Day1.Part1: " + _2015_day1.Solve_Part1());
        Console.WriteLine("_2015.Day1.Part1: " + _2015_day1.Solve_Part2());
        var _2015_day2 = new _2015.Day2(@"2015\day2_input.txt");
        Console.WriteLine("_2015.Day2.Part1: " + _2015_day2.Solve_Part1());
        Console.WriteLine("_2015.Day2.Part2: " + _2015_day2.Solve_Part2());




        Console.ReadKey();
    }
}
