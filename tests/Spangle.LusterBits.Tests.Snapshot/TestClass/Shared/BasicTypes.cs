namespace Spangle.LusterBits.Tests.TestClass;

[LusterCharm(GenerateType.All)]
public unsafe partial struct BasicTypes
{
    public const int Size = 256;

    [
        BitFieldOptions(targetFieldLength: Size),
        BitField(typeof(int), "IntField", length: sizeof(int)),
        BitField(typeof(long), "LongField", length: sizeof(long)),
        BitField(typeof(float), "FloatField", length: sizeof(float)),
        BitField(typeof(double), "DoubleField", length: sizeof(double)),
        BitField(typeof(bool), "BoolField", length: sizeof(bool)),
        BitField(typeof(byte), "ByteField", length: sizeof(byte)),
        BitField(typeof(sbyte), "SByteField", length: sizeof(sbyte)),
        BitField(typeof(short), "ShortField", length: sizeof(short)),
        BitField(typeof(ushort), "UShortField", length: sizeof(ushort)),
        BitField(typeof(uint), "UIntField", length: sizeof(uint)),
        BitField(typeof(ulong), "ULongField", length: sizeof(ulong)),
        BitField(typeof(decimal), "DecimalField", length: sizeof(decimal)),

        BitField(typeof(string), "StringField", position: 50 * 8, length: 3 * 8 * 10),
        BitField(typeof(UTF8String), "UTF8StringField", length: 3 * 8 * 10),
        BitField(typeof(UTF16String), "UTF16StringField", length: 2 * 8 * 10),
        BitField(typeof(UTF16BigEndianString), "UTF16BigEndianStringField", length: 2 * 8 * 20),
    ]
    private fixed byte _data[Size];
}
