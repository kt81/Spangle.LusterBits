using Microsoft.CodeAnalysis;

namespace Spangle.LusterBits.Generator;

public static class DiagnosticDescriptors
{
    private const string Category = "BFGen";

    public static readonly DiagnosticDescriptor NotPartial = new(
        id: "BFG001",
        title: "Not a partial struct",
        messageFormat: "{0} must be partial",
        category: Category,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true);
    public static readonly DiagnosticDescriptor NotSupportedTypeField = new(
        id: "BFG002",
        title: "Unsupported field type",
        messageFormat: "The field type of {0} is not supported",
        category: Category,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true);
    public static readonly DiagnosticDescriptor NoSuitableFieldDefinitions = new(
        id: "BFG003",
        title: "No suitable field definitions",
        messageFormat: "{0} has no suitable field definitions",
        category: Category,
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true);
    public static readonly DiagnosticDescriptor InvalidGenerateType = new(
        id: "BFG004",
        title: "Invalid GenerateType",
        messageFormat: "The generate type of {0} is invalid",
        category: Category,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true);
    public static readonly DiagnosticDescriptor InvalidByteBoundary = new(
        id: "BFG005",
        title: "Invalid byte boundary",
        messageFormat: "The type {0} must be adjusted to byte boundaries",
        category: Category,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true);
}
