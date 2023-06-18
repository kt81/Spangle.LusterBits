using System.Runtime.InteropServices;

namespace Spangle.LusterBits.Tests.TestClass;

[LusterCharm(GenerateType.All)]
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public unsafe partial struct AutoTestStruct
{
    [
        BitField(typeof(byte), "F1Byte", 8, description: "Zero"),
        BitField(typeof(bool), "F2Bool", 1, description: "Flag1"),
        BitField(typeof(bool), "F3Bool", 1, description: "Flag2"),
        BitField(typeof(byte), "F4Byte", 1, description: "1 bit field"),
        BitField(typeof(ushort), "F5UShort", 13, description: "13bits ushort over 16bits alignment"),
        BitField(typeof(byte), "F6Byte", 2, description: "2bits byte"),
        BitField(typeof(byte), "F7Byte", 2, description: "2bits byte"),
        BitField(typeof(byte), "F8Byte", 4, description: "4bits byte"),
    ]
    private fixed byte _data[4];
}
