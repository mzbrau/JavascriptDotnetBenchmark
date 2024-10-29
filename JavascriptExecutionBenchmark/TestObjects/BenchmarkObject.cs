namespace JavascriptExecutionBenchmark.TestObjects;

public class BenchmarkObject
{
    public int Value { get; set; }

    public int Multiply(int x, int y)
    {
        return x * y;
    }
}