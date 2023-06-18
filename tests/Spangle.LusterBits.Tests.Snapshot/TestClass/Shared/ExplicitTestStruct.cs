using System.Runtime.InteropServices;

namespace Spangle.LusterBits.Tests.TestClass;

/*
  0                   1                   2                   3
  0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1
 +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
 |      F1       |2|3|4|           F5            | 6 | 7 |  F8   |
 */
[LusterCharm]
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public unsafe partial struct ExplicitTestStruct
{
    [
        BitField(typeof(byte), "F1Byte", position: 0, length: 8, description: "Zero"),
        BitField(typeof(bool), "F2Bool", position: 8, length: 1, description: "Flag1"),
        BitField(typeof(bool), "F3Bool", position: 9, length: 1, description: "Flag2"),
        BitField(typeof(byte), "F4Byte", position: 10, length: 1, description: "1 bit field"),
        BitField(typeof(ushort), "F5UShort", position: 11, length: 13, description: "13bits ushort over 16bits alignment"),
        BitField(typeof(byte), "F6Byte", position: 24, length: 2, description: "2bits byte"),
        BitField(typeof(byte), "F7Byte", position: 26, length: 2, description: "2bits byte"),
        BitField(typeof(byte), "F8Byte", position: 28, length: 4, description: "4bits byte"),
    ]
    private fixed byte _data[4];
}
