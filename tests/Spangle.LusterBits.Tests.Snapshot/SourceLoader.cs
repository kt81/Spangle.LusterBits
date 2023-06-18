using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Spangle.LusterBits.Tests;

public static class SourceLoader
{
    private static readonly string[] s_resourceNames;

    static SourceLoader()
    {
        s_resourceNames = Assembly.GetExecutingAssembly().GetManifestResourceNames();
    }

    public static Task<string> LoadAsync(string resourceName)
    {
        string? fullName = s_resourceNames.FirstOrDefault(s => s.EndsWith($".{resourceName}.cs"));
        if (fullName is null)
        {
            throw new MissingManifestResourceException($"Cannot find the test resource: {resourceName}");
        }
        using var stream = typeof(SourceLoader).Assembly.GetManifestResourceStream(fullName);
        if (stream is null)
        {
            throw new Exception("Something went wrong 🤪");
        }

        return new StreamReader(stream).ReadToEndAsync();
    }

}
