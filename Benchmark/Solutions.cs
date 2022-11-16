using BenchmarkDotNet.Attributes;

namespace Benchmark;

[MemoryDiagnoser]
public class Solutions
{
    // private readonly AdventOfCode._2015.Day2 sut = new AdventOfCode._2015.Day2(@"2015\day2_input.txt");
    // private readonly AdventOfCode._2015.Day3 sut = new AdventOfCode._2015.Day3(@"2015\day3_input.txt");
    private readonly AdventOfCode._2015.Day4 sut = new AdventOfCode._2015.Day4(@"2015\day4_input.txt");

    [Benchmark]
    public void Method1()
    {
        sut.Solve_Using_Linq(AdventOfCode._2015.Day4.StartsWithAtLeastFiveZeros);
    }

    [Benchmark]
    public void Method2()
    {
        sut.Solve_Using_Loop(AdventOfCode._2015.Day4.StartsWithAtLeastFiveZeros);
    }
}