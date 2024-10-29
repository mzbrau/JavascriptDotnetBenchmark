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
public class RepeatedExecutionBenchmark
{
    private static readonly string ComplexScript = @"
        function complexCalculation() {
            let result = 0;
            for (let i = 0; i < 1000000; i++) {
                result += Math.sqrt(i) * Math.log(i + 1);
            }
            return result;
        }
        complexCalculation();
    ";

    [Benchmark, BenchmarkCategory("RepeatedExecution")]
    public void Jint_RepeatedExecution()
    {
        var engine = new Engine();
        for (int i = 0; i < 100; i++)
        {
            engine.Execute(ComplexScript);
        }
    }

    [Benchmark, BenchmarkCategory("RepeatedExecution")]
    public void ClearScript_RepeatedExecution()
    {
        using var engine = new V8ScriptEngine();
        for (int i = 0; i < 100; i++)
        {
            engine.Execute(ComplexScript);
        }
    }

    [Benchmark, BenchmarkCategory("RepeatedExecution")]
    public void Jurassic_RepeatedExecution()
    {
        var engine = new ScriptEngine();
        for (int i = 0; i < 100; i++)
        {
            engine.Execute(ComplexScript);
        }
    }

    [Benchmark, BenchmarkCategory("RepeatedExecution")]
    public void NiLJS_RepeatedExecution()
    {
        var context = new Context();
        for (int i = 0; i < 100; i++)
        {
            context.Eval(ComplexScript);
        }
    }
}