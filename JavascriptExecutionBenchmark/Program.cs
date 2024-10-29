using BenchmarkDotNet.Running;
using JavascriptExecutionBenchmark.Benchmarks;

namespace JavascriptExecutionBenchmark;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine(nameof(InitializationBenchmark));
        Console.WriteLine(BenchmarkRunner.Run<InitializationBenchmark>());

        Console.WriteLine(nameof(SetPropertyBenchmark));
        Console.WriteLine(BenchmarkRunner.Run<SetPropertyBenchmark>());

        Console.WriteLine(nameof(CallMethodBenchmark));
        Console.WriteLine(BenchmarkRunner.Run<CallMethodBenchmark>());

        Console.WriteLine(nameof(ComplexObjectManipulationBenchmark));
        Console.WriteLine(BenchmarkRunner.Run<ComplexObjectManipulationBenchmark>());

        Console.WriteLine(nameof(LargeDatasetBenchmark));
        Console.WriteLine(BenchmarkRunner.Run<LargeDatasetBenchmark>());

        Console.WriteLine(nameof(ParallelExecutionBenchmark));
        Console.WriteLine(BenchmarkRunner.Run<ParallelExecutionBenchmark>());

        Console.WriteLine(nameof(RepeatedExecutionBenchmark));
        Console.WriteLine(BenchmarkRunner.Run<RepeatedExecutionBenchmark>());

        Console.WriteLine(nameof(ScriptExecutionBenchmark));
        Console.WriteLine(BenchmarkRunner.Run<ScriptExecutionBenchmark>());
    }
}