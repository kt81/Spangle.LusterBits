
namespace Spangle.LusterBits.Tests;

[UsesVerify]
public class ByteFieldStructSnapshotTests
{
    /// <summary>
    /// Generates partial struct for ExplicitTestStruct
    /// </summary>
    [Fact]
    public async Task GenerateExplicitPositionedStruct()
    {
        string source = await SourceLoader.LoadAsync("ExplicitTestStruct");
        await TestHelper.Verify(source);
    }

    /// <summary>
    /// Generates partial struct for AutoTestStruct
    /// </summary>
    [Fact]
    public async Task GenerateAutoPositionedStruct()
    {
        string source = await SourceLoader.LoadAsync("AutoTestStruct");
        await TestHelper.Verify(source);
    }

    /// <summary>
    /// Enum ByteField works
    /// </summary>
    [Fact]
    public async Task GenerateEnumStruct()
    {
        string source = await SourceLoader.LoadAsync("EnumStruct");
        await TestHelper.Verify(source);
    }

    /// <summary>
    /// Basic type field definition works
    /// </summary>
    [Fact]
    public async Task GenerateBasicTypesStruct()
    {
        string source = await SourceLoader.LoadAsync("BasicTypes");
        await TestHelper.Verify(source);
    }

    /// <summary>
    /// Error on no field definition struct
    /// </summary>
    [Fact]
    public async Task ErrorNoFieldStruct()
    {
        string source = await SourceLoader.LoadAsync("NoFieldStruct");
        await TestHelper.Verify(source);
    }

    /// <summary>
    /// Error on not partial struct
    /// </summary>
    [Fact]
    public async Task ErrorNotPartialStruct()
    {
        string source = await SourceLoader.LoadAsync("NotPartialStruct");
        await TestHelper.Verify(source);
    }

    /// <summary>
    /// Error on invalid byte boundary struct
    /// </summary>
    [Fact]
    public async Task ErrorInvalidByteBoundaryStruct()
    {
        string source = await SourceLoader.LoadAsync("InvalidByteBoundaryStruct");
        await TestHelper.Verify(source);
    }
}
