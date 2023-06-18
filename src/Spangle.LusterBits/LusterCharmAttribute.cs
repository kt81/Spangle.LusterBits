// ReSharper disable UnusedType.Global

using System.Diagnostics.CodeAnalysis;

namespace Spangle.LusterBits;

/// <summary>
/// LusterCharmAttribute indicates the struct has bit fields.
/// </summary>
[AttributeUsage(AttributeTargets.Struct)]
public sealed class LusterCharmAttribute : Attribute
{
    public readonly GenerateType GenerateType;

    public LusterCharmAttribute(GenerateType generateType = GenerateType.Expression)
    {
        GenerateType = generateType;
    }
}

/// <summary>
/// BitFieldAttribute represents a bit field obtained by slicing the target fixed array.
/// </summary>
/// <remarks>
/// The field `position` is optional. And it is placed next to `length`. This may seem counterintuitive.
/// If you do not specify `position`, the generator will place the field next to the previous field.
/// This feature is useful when you want to declare a LusterCharmAttribute from a document such as a protocol.
/// `description` is also optional. But it is recommended to write it.
/// </remarks>
[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
[SuppressMessage("ReSharper", "NotAccessedField.Global")]
public class BitFieldAttribute : Attribute
{
    private const int AutoPosition = -1;

    public readonly Type    PropType;
    public readonly string  PropName;
    public readonly int     Length;
    public readonly int     Position;
    public readonly string? Description;

    public BitFieldAttribute(Type propType, string propName, int length, int position = AutoPosition,
        string? description = null)
    {
        PropType = propType;
        PropName = propName;
        Length = length;
        Position = position;
        Description = description;
    }
}

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
[SuppressMessage("ReSharper", "NotAccessedField.Global")]
public sealed class BitFieldOptionsAttribute : Attribute
{
    public const int Auto = -1;

    public readonly int TargetFieldLength;

    /// <summary>
    /// Create BitFieldOptionsAttribute.
    /// </summary>
    /// <param name="targetFieldLength">
    /// Specify the length of the target field explicitly. This affects the inclusion range of the ***AsSpan method.
    /// </param>
    public BitFieldOptionsAttribute(int targetFieldLength = Auto)
    {
        TargetFieldLength = targetFieldLength;
    }

}
