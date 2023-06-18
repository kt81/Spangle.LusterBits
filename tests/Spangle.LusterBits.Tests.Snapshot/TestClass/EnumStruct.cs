using System.Runtime.InteropServices;

namespace Spangle.LusterBits.Tests.TestClass;

[LusterCharm]
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public unsafe partial struct EnumStruct
{
    [
        BitField(typeof(SomeEnum), "EnumField", 2, description: "Cool Enum Field"),
    ]
    private fixed byte _data[1];
}

public enum SomeEnum : byte
{
    Val1,
    Val2,
    Val3,
}
