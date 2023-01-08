using BenchmarkDotNet.Running;

namespace Benchmark
{
    public static class Program
    {
        static void Main()
        {
            _ = BenchmarkRunner.Run<Solutions>();
        }
    }
}