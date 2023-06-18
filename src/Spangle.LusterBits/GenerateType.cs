namespace Spangle.LusterBits;

/// <summary>
/// Specify the type of code to be generated.
/// </summary>
[Flags]
#if BFS_GENERATOR
internal
#else
public
#endif
    enum GenerateType : byte
{
    /// <summary>
    /// Generate expression properties to get the value specified with LusterCharmAttribute.
    /// </summary>
    Expression = 0b0001,

    /// <summary>
    /// Generate another struct that has simple host type of auto properties and a factory method to deserialize from the original.
    /// </summary>
    /// <remarks>
    /// Currently, this type does not work by itself. It requires code generated with `Expression` type.
    /// Do not use this type for the protocol that has dynamic field layout.
    /// </remarks>
    Deserialized = 0b0010,

    /// <summary>
    /// Generate an interface for the target struct and have other generated types implement it.
    /// </summary>
    Interface = 0b0100,

    /// <summary>
    /// Generate all type.
    /// </summary>
    All = Expression | Deserialized | Interface,
}

internal static class GenerateTypeExtensions
{
    public static bool HasInvalidFlag(this GenerateType self)
    {
        return self == 0 || (self & ~GenerateType.All) != 0;
    }
}
