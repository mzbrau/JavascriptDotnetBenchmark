using Jurassic;

namespace JavascriptExecutionBenchmark.Benchmarks;

using Benchly;
using BenchmarkDotNet.Attributes;
using TestObjects;
using Jint;
using Microsoft.ClearScript.V8;
using NiL.JS.Core;

[BoxPlot(Title = "Box Plot", Colors = "red,slateblue")]
[ColumnChart(Title = "Column Chart", Colors = "red,slateblue")]
[MemoryDiagnoser(false)]
public class ComplexObjectManipulationBenchmark
{
    private static readonly string ManipulateObjectScript = @"
        obj.Name = 'Modified';
        obj.Level += 1;
        obj.Children.forEach(child => {
            child.Name = 'Modified_' + child.Name;
            child.Level += 1;
            child.Children.forEach(grandchild => {
                grandchild.Name = 'Modified_' + grandchild.Name;
                grandchild.Level += 1;
            });
        });
    ";

    private static readonly string ManipulateObjectScriptClearScript = @"
    obj.Name = 'Modified';
    obj.Level += 1;
    Array.from(obj.Children).forEach(child => {
        child.Name = 'Modified_' + child.Name;
        child.Level += 1;
        Array.from(child.Children).forEach(grandchild => {
            grandchild.Name = 'Modified_' + grandchild.Name;
            grandchild.Level += 1;
        });
    });
";

    private static readonly string ManipulateObjectScriptJurassic = @"
    obj.Name = 'Modified';
    obj.Level += 1;
    obj.Children.forEach(function(child) {
        child.Name = 'Modified_' + child.Name;
        child.Level += 1;
        child.Children.forEach(function(grandchild) {
            grandchild.Name = 'Modified_' + grandchild.Name;
            grandchild.Level += 1;
        });
    });
";

    [Benchmark, BenchmarkCategory("ComplexObjectManipulation")]
    public void Jint_ComplexObjectManipulation()
    {
        var engine = new Engine();
        var complexObject = CreateComplexObject();

        engine.SetValue("obj", complexObject);
        engine.Execute(ManipulateObjectScript);
    }

    [Benchmark, BenchmarkCategory("ComplexObjectManipulation")]
    public void ClearScript_ComplexObjectManipulation()
    {
        using var engine = new V8ScriptEngine();
        var complexObject = CreateComplexObject();
        engine.AddHostObject("obj", complexObject);
        engine.Execute(ManipulateObjectScriptClearScript); // Note this didn't work with the ManipulateObjectScript. Microsoft.ClearScript.ScriptEngineException: 'TypeError: obj.Children.forEach is not a function'
    }

    [Benchmark, BenchmarkCategory("ComplexObjectManipulation")]
    public void Jurassic_ComplexObjectManipulation()
    {
        var engine = new ScriptEngine
        {
            EnableExposedClrTypes = true
        };
        var complexObject = CreateComplexObject();
        engine.SetGlobalValue("obj", complexObject);
        engine.Execute(ManipulateObjectScriptJurassic); // Does not support ES6. Jurassic.JavaScriptException: 'SyntaxError: Unexpected token '>' in expression.'
    }

    [Benchmark, BenchmarkCategory("ComplexObjectManipulation")]
    public void NiLJS_ComplexObjectManipulation()
    {
        var context = new Context();
        var complexObject = CreateComplexObject();
        var jsObject = Context.CurrentGlobalContext.ProxyValue(complexObject);
        context.DefineVariable("obj").Assign(jsObject);
        context.Eval(ManipulateObjectScript);
    }

    private ComplexObject CreateComplexObject()
    {
        var root = new ComplexObject { Name = "Root", Level = 1 };
        for (int i = 0; i < 10; i++)
        {
            var child = new ComplexObject { Name = $"Child_{i}", Level = 2 };
            for (int j = 0; j < 5; j++)
            {
                child.Children.Add(new ComplexObject { Name = $"Child_{i}_Grandchild_{j}", Level = 3 });
            }
            root.Children.Add(child);
        }
        return root;
    }
}