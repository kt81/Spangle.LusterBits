using System.Runtime.CompilerServices;

namespace Spangle.LusterBits.Tests;

public static class VerifyInitializer
{
    [ModuleInitializer]
    public static void Init() =>
        VerifySourceGenerators.Initialize();
}
