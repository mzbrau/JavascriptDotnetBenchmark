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
public class ScriptExecutionBenchmark
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

    [Benchmark, BenchmarkCategory("ScriptExecution")]
    public void Jint_ScriptExecution()
    {
        var engine = new Engine();
        engine.Execute(ComplexScript);
    }

    [Benchmark, BenchmarkCategory("ScriptExecution")]
    public void ClearScript_ScriptExecution()
    {
        using var engine = new V8ScriptEngine();
        engine.Execute(ComplexScript);
    }

    [Benchmark, BenchmarkCategory("ScriptExecution")]
    public void NiLJS_ScriptExecution()
    {
        var context = new Context();
        context.Eval(ComplexScript);
    }

    [Benchmark, BenchmarkCategory("ScriptExecution")]
    public void Jurassic_ScriptExecution()
    {
        var engine = new ScriptEngine();
        engine.Execute(ComplexScript);
    }
}