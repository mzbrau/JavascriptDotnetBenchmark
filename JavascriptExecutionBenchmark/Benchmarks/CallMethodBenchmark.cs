using Benchly;
using BenchmarkDotNet.Attributes;
using JavascriptExecutionBenchmark.TestObjects;
using Jint;
using Jurassic;
using Microsoft.ClearScript.V8;
using NiL.JS.Core;

namespace JavascriptExecutionBenchmark.Benchmarks;

[BoxPlot(Title = "Box Plot", Colors = "red,slateblue")]
[ColumnChart(Title = "Column Chart", Colors = "red,slateblue")]
[MemoryDiagnoser(false)]
public class CallMethodBenchmark
{
    private static readonly string CallMethodScript = "obj.Multiply(7, 6);";

    [Benchmark, BenchmarkCategory("CallMethod")]
    public void Jint_CallMethod()
    {
        var engine = new Engine();
        var obj = new BenchmarkObject();
        engine.SetValue("obj", obj);
        engine.Execute(CallMethodScript);
    }

    [Benchmark, BenchmarkCategory("CallMethod")]
    public void ClearScript_CallMethod()
    {
        using var engine = new V8ScriptEngine();
        var obj = new BenchmarkObject();
        engine.AddHostObject("obj", obj);
        engine.Execute(CallMethodScript);
    }

    [Benchmark, BenchmarkCategory("CallMethod")]
    public void Jurassic_CallMethod()
    {
        var engine = new ScriptEngine
        {
            EnableExposedClrTypes = true
        };
        var obj = new BenchmarkObject();
        engine.SetGlobalValue("obj", obj);
        engine.Execute(CallMethodScript);
    }

    [Benchmark, BenchmarkCategory("CallMethod")]
    public void NiLJS_CallMethod()
    {
        var context = new Context();
        var obj = new BenchmarkObject();
        context.DefineVariable("obj").Assign(Context.CurrentGlobalContext.ProxyValue(obj));
        context.Eval(CallMethodScript);
    }
}