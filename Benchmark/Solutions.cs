using BenchmarkDotNet.Attributes;

namespace Benchmark;

[MemoryDiagnoser]
public class Solutions
{
    private readonly AdventOfCode._2015.Day2 sut = new AdventOfCode._2015.Day2(@"2015\day2_input.txt");

    [Benchmark]
    public void Method1()
    {
        sut.Solve_Part1_Method1();
    }

    [Benchmark]
    public void Method2()
    {
        sut.Solve_Part1_Method2();
    }

    [Benchmark]
    public void Method3()
    {
        sut.Solve_Part1_Method3();
    }
}