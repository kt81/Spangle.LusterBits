using System.Runtime.InteropServices;

namespace Spangle.LusterBits.Tests.Example;

public class SomePacketTest
{
    [Fact]
    public void SomePacketTest1()
    {
        var buffer = new byte[]
        {
            0b10_111111,         // SomeByte & SomeNotAlignedUInt32
            0xFF, 0xFF, 0xFE,    // SomeNotAlignedUInt32
            0b0101_1111,         // SomeEnumCastedValue & SomeUShort
            0xFF, 0b110_1_01_11, // SomeUShortValue & SomeBoolean & SomeByte2 & SomeUShort2
            0xFE                 // SomeUShort2
        };
        buffer.Length.Should().Be(8, "It's true! It's true!!!!!");
        ref readonly var packet = ref MemoryMarshal.AsRef<SomePacket>(buffer);
        packet.SomeByte.Should().Be(0b10);
        packet.SomeNotAlignedUInt32.Should().Be(0b00111111_11111111_11111111_11111110);
        packet.SomeEnumCastedValue.Should().Be(SomeEnum.Value5);
        packet.SomeUShort.Should().Be(0b01111111_11111110);
        packet.SomeBoolean.Should().BeTrue();
        packet.SomeByte2.Should().Be(0b01);
        packet.SomeUShort2.Should().Be(0b11_11111110);
    }
}
