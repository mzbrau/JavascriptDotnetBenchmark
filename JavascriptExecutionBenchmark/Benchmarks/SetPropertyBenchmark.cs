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
public class SetPropertyBenchmark
{
    private static readonly string SetPropertyScript = "obj.Value = 42;";

    [Benchmark, BenchmarkCategory("SetProperty")]
    public void Jint_SetProperty()
    {
        var engine = new Engine();
        var obj = new BenchmarkObject();
        engine.SetValue("obj", obj);
        engine.Execute(SetPropertyScript);
        if (obj.Value != 42)
            throw new Exception("Test Failed");
    }

    [Benchmark, BenchmarkCategory("SetProperty")]
    public void ClearScript_SetProperty()
    {
        using var engine = new V8ScriptEngine();
        var obj = new BenchmarkObject();
        engine.AddHostObject("obj", obj);
        engine.Execute(SetPropertyScript);
        if (obj.Value != 42)
            throw new Exception("Test Failed");
    }

    [Benchmark, BenchmarkCategory("SetProperty")]
    public void Jurassic_SetProperty()
    {
        var engine = new ScriptEngine
        {
            EnableExposedClrTypes = true
        };
        var obj = new BenchmarkObject();
        engine.SetGlobalValue("obj", obj);
        engine.Execute(SetPropertyScript);
        if (obj.Value != 42)
            throw new Exception("Test Failed");
    }

    [Benchmark, BenchmarkCategory("SetProperty")]
    public void NiLJS_SetProperty()
    {
        var context = new Context();
        var obj = new BenchmarkObject();

        context.DefineVariable("obj").Assign(Context.CurrentGlobalContext.ProxyValue(obj));
        context.Eval(SetPropertyScript);
        if (obj.Value != 42)
            throw new Exception("Test Failed");
    }
}