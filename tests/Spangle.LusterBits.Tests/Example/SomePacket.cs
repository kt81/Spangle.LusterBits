using System.Runtime.InteropServices;

namespace Spangle.LusterBits.Tests.Example;

// All target structures would have such a signature:
// `accessibility unsafe partial struct StructName`
[LusterCharm]
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public unsafe partial struct SomePacket
{
    public const int PacketLength = 8; // 64 bits

    [
        BitField(typeof(byte), "SomeByte", 2),
        BitField(typeof(uint), "SomeNotAlignedUInt32", 30),
        BitField(typeof(SomeEnum), "SomeEnumCastedValue", 4),
        BitField(typeof(ushort), "SomeUShort", 15),
        BitField(typeof(bool), "SomeBoolean", 1),
        BitField(typeof(byte), "SomeByte2", 2),
        BitField(typeof(ushort), "SomeUShort2", 10),
    ]
    private fixed byte _data[PacketLength];
}
public enum SomeEnum : byte
{
    Value0 = 0b0000,
    Value1 = 0b0001,
    Value2 = 0b0010,
    Value3 = 0b0011,
    Value4 = 0b0100,
    Value5 = 0b0101,
    // ... can use 4 bits for value
}

public static class SomeAwesomePacketProcessor
{
    public static uint DealWithPacket(ReadOnlySpan<byte> buffer)
    {
        if (buffer.Length < SomePacket.PacketLength)
        {
            throw new ArgumentException($"Packet length is smaller than {SomePacket.PacketLength} bytes", nameof(buffer));
        }
        ref readonly var packet = ref MemoryMarshal.AsRef<SomePacket>(buffer);
        // Do something with packet. Numeric fields can be access in Host Endianness.
        return packet.SomeNotAlignedUInt32;
    }
}
