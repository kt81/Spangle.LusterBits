using System.Runtime.InteropServices;
using Spangle.LusterBits.Tests.TestClass;

namespace Spangle.LusterBits.Tests;

public class ByteFieldStructGeneratorTests
{
    private static readonly byte[] s_data =
    {
        0b0000_0000, // 0
        0b1011_1010, // true, false, 1, first 5 bits for F5
        0b0101_0101, // next 8 bits for F5
        0b0110_1111, // 1, 2, 15
    };

    [Fact]
    public void ExplicitPositionTest()
    {
        ref var testStruct = ref MemoryMarshal.AsRef<ExplicitTestStruct>(s_data);
        testStruct.F1Byte.Should().Be(0b0000_0000);
        testStruct.F2Bool.Should().BeTrue();
        testStruct.F3Bool.Should().BeFalse();
        testStruct.F4Byte.Should().Be(0b0000_0001);
        testStruct.F5UShort.Should().Be(0b0001_1010_0101_0101, "converted to host endian");
        testStruct.F6Byte.Should().Be(0b01);
        testStruct.F7Byte.Should().Be(0b10);
        testStruct.F8Byte.Should().Be(0b1111);
    }

    private static void TestAutoPositionInstance<T>(in T testStruct) where T : struct, IAutoTestStruct
    {
        testStruct.F1Byte.Should().Be(0b0000_0000);
        testStruct.F2Bool.Should().BeTrue();
        testStruct.F3Bool.Should().BeFalse();
        testStruct.F4Byte.Should().Be(0b0000_0001);
        testStruct.F5UShort.Should().Be(0b0001_1010_0101_0101, "converted to host endian");
        testStruct.F6Byte.Should().Be(0b01);
        testStruct.F7Byte.Should().Be(0b10);
        testStruct.F8Byte.Should().Be(0b1111);
    }

    [Fact]
    public void AutoPositionTest()
    {
        ref var testStruct = ref MemoryMarshal.AsRef<AutoTestStruct>(s_data);
        TestAutoPositionInstance(testStruct);
    }

    [Fact]
    public void DeserializedTest()
    {
        ref var sourceStruct = ref MemoryMarshal.AsRef<AutoTestStruct>(s_data);
        var testStruct = AutoTestStructDeserialized.FromBitFields(sourceStruct);
        TestAutoPositionInstance(testStruct);
    }
}
