using System.Runtime.InteropServices;

namespace Spangle.LusterBits.Tests.TestClass;

[LusterCharm]
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public unsafe partial struct InvalidByteBoundaryStruct
{
    [BitField(typeof(UTF8String), "DislocatedString", position: 1, length: 3 * 8 * 4)]
    private fixed byte _data[8];
}
