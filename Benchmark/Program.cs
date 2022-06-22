using BenchmarkDotNet.Running;

namespace Benchmark
{
    public static class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<Solutions>();
        }
    }
}