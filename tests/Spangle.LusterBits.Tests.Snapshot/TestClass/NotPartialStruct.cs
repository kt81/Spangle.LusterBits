using System.Runtime.InteropServices;

namespace Spangle.LusterBits.Tests.TestClass;

[LusterCharm]
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public unsafe struct NotPartialStruct
{
    [
        BitField(typeof(byte), "ByteField", 2, description: "Cool Field"),
    ]
    private fixed byte _data[1];
}
