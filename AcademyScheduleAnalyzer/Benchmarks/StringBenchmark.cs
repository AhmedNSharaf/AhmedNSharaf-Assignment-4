using BenchmarkDotNet.Attributes;
using System.Text;

namespace Academy_Schedule_Analyzer_Assignment.Benchmarks
{
    [SimpleJob(warmupCount: 1, iterationCount: 5)]
    [MemoryDiagnoser]
    public class StringBenchmark
    {
        [Params(100, 1000, 10000, 100000)]
        public int Iterations;

        [Benchmark]
        public string StringConcatenation()
        {
            string result = "";
            for (int i = 0; i < Iterations; i++)
            {
                result += "Session - 10/09/2026 06:00 PM - 180 minutes\n";
            }
            return result;
        }

        [Benchmark]
        public string StringBuilderConcatenation()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < Iterations; i++)
            {
                sb.Append("Session - 10/09/2026 06:00 PM - 180 minutes\n");
            }
            return sb.ToString();
        }
    }
}
