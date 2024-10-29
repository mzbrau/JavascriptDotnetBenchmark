namespace JavascriptExecutionBenchmark.TestObjects;

public class ComplexObject
{
    public string Name { get; set; }
    public int Level { get; set; }
    public List<ComplexObject> Children { get; set; } = new();
}