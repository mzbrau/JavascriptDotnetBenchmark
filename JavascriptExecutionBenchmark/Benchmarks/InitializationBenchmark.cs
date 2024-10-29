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
public class InitializationBenchmark
{
    [Benchmark, BenchmarkCategory("Initialization")]
    public void Jint_Initialization()
    {
        var engine = new Engine();
    }

    [Benchmark, BenchmarkCategory("Initialization")]
    public void ClearScript_Initialization()
    {
        using var engine = new V8ScriptEngine();
    }

    [Benchmark, BenchmarkCategory("Initialization")]
    public void NiLJS_EngineInitialization()
    {
        var context = new Context();
    }

    [Benchmark, BenchmarkCategory("Initialization")]
    public void Jurassic_EngineInitialization()
    {
        var engine = new ScriptEngine();
    }
}