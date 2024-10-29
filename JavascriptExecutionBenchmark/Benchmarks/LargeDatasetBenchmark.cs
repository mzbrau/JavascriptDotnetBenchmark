using Benchly;
using BenchmarkDotNet.Attributes;
using Jint;
using Jurassic;
using Microsoft.ClearScript.V8;
using NiL.JS.Core;

namespace JavascriptExecutionBenchmark.Benchmarks;

[BoxPlot(Title = "Box Plot", Colors = "red,slateblue")]
[ColumnChart(Title = "Column Chart", Colors = "red,slateblue")]
[MemoryDiagnoser(false)]
public class LargeDatasetBenchmark
{
    private static readonly string LargeArrayScript = @"
    function processArray(arr) {
        let sum = 0;
        for (let i = 0; i < arr.length; i++) {
            sum += arr[i];
        }
        return sum;
    }
    processArray(data);
";

    [Benchmark, BenchmarkCategory("LargeDataSet")]
    public void Jint_LargeDataSet()
    {
        var engine = new Engine();
        int[] largeArray = Enumerable.Range(0, 100000).ToArray();
        engine.SetValue("data", largeArray);
        engine.Execute(LargeArrayScript);
    }

    [Benchmark, BenchmarkCategory("LargeDataSet")]
    public void ClearScript_LargeDataSet()
    {
        using var engine = new V8ScriptEngine();
        int[] largeArray = Enumerable.Range(0, 100000).ToArray();
        engine.AddHostObject("data", largeArray);
        engine.Execute(LargeArrayScript);
    }

    [Benchmark, BenchmarkCategory("LargeDataSet")]
    public void Jurassic_LargeDataSet()
    {
        var engine = new ScriptEngine
        {
            EnableExposedClrTypes = true
        };
        int[] largeArray = Enumerable.Range(0, 100000).ToArray();
        engine.SetGlobalValue("data", largeArray);
        engine.Execute(LargeArrayScript);
    }

    [Benchmark, BenchmarkCategory("LargeDataSet")]
    public void NiLJS_LargeDataSet()
    {
        var context = new Context();
        int[] largeArray = Enumerable.Range(0, 100000).ToArray();
        var jsArray = Context.CurrentGlobalContext.ProxyValue(largeArray);
        context.DefineVariable("data").Assign(jsArray);
        context.Eval(LargeArrayScript);
    }
}