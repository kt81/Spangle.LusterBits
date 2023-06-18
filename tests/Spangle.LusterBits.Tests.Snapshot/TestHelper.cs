using Spangle.LusterBits.Generator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Spangle.LusterBits;

namespace Spangle.LusterBits.Tests;

public static class TestHelper
{
    private static readonly IEnumerable<string> s_defaultNamespaces = Array.Empty<string>();
    // {
    //     "System", "System.IO", "System.Net", "System.Linq", "System.Text", "System.Text.RegularExpressions",
    //     "System.Collections.Generic"
    // };

    private static readonly CSharpCompilationOptions s_defaultCompilationOptions =
        new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary)
            .WithOverflowChecks(true).WithOptimizationLevel(OptimizationLevel.Release)
            .WithAllowUnsafe(true)
            .WithUsings(s_defaultNamespaces);

    public static Task Verify(string source)
    {
        // Parse the provided string into a C# syntax tree
        SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(source);
        string assemblyDirectoryPath = Path.GetDirectoryName(typeof(object).Assembly.Location)!;
        var references = new List<PortableExecutableReference>
        {
            MetadataReference.CreateFromFile(Path.Join(assemblyDirectoryPath, "mscorlib.dll")),
            MetadataReference.CreateFromFile(Path.Join(assemblyDirectoryPath, "System.dll")),
            MetadataReference.CreateFromFile(Path.Join(assemblyDirectoryPath, "System.Core.dll")),
            MetadataReference.CreateFromFile(Path.Join(assemblyDirectoryPath, "System.Runtime.dll")),
            MetadataReference.CreateFromFile(Path.Join(assemblyDirectoryPath, "System.Private.CoreLib.dll")),
            MetadataReference.CreateFromFile(Path.Join(assemblyDirectoryPath, "netstandard.dll")),
            MetadataReference.CreateFromFile(typeof(LusterCharmAttribute).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(Attribute).Assembly.Location),
        };

        // Create a Roslyn compilation for the syntax tree.
        CSharpCompilation compilation = CSharpCompilation.Create(
            assemblyName: "Tests",
            syntaxTrees: new[] { syntaxTree },
            references: references,
            options: s_defaultCompilationOptions);

        // Create an instance of our EnumGenerator incremental source generator
        var generator = new LusterBitsGenerator();

        // The GeneratorDriver is used to run our generator against a compilation
        GeneratorDriver driver = CSharpGeneratorDriver.Create(generator);

        // To test basic references are working
        using var stream = new MemoryStream();
        Microsoft.CodeAnalysis.Emit.EmitResult emitResult = compilation.Emit(stream);
        if (!emitResult.Success)
        {
            throw new InvalidOperationException(string.Join("\n", emitResult.Diagnostics.Select(x => x.ToString())));
        }

        // Run the source generator!
        driver = driver.RunGenerators(compilation);

        // Use verify to snapshot test the source generator output!
        return Verifier.Verify(driver);
    }
}
