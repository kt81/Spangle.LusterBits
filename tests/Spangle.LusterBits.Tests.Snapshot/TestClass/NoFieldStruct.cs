using System.Runtime.InteropServices;

namespace Spangle.LusterBits.Tests.TestClass;

[LusterCharm]
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public unsafe partial struct NoFieldStruct
{
    private fixed byte _data[8];
}
