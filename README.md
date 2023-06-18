Spangle.LusterBits
==================

Spangle.LusterBits is a C# Source Generator library that generates accessor methods for struct that has fixed byte array
fields
(e.g. `private fixed byte _data[SomeFixedLength];`).
It is useful for accessing binary data such as network packets, especially, when only a few fields of the data are
needed.

Spangle.LusterBits targets a struct that has Network-Ordered (Big-Endian) fixed byte array that has no alignment.
So you do not need to unmarshal the network packets, only just do `MemoryMarshal.Ref<TargetStruct>(...)` for it.

Getting Started
---------------

Spangle.LusterBits is available as a NuGet package.
You can install it from the NuGet Package Manager Console:

```shell
nuget install Spangle.LusterBits
```

Or via the .NET Core command-line interface:

```shell
dotnet add package Spangle.LusterBits
```

Usage
-----

Spangle.LusterBits searches *marker attribute* that is specified by `LusterCharmAttribute`.
The marker attribute must be specified on a struct that has fixed byte array fields and the fields also have the setting
attribute that is specified by `BitFieldAttribute`.
Spangle.LusterBits searches for the *marker attribute* specified by `LusterCharmAttribute`.
The marker attribute must be specified on a struct that has fixed byte array fields and the fields also have
the *field setting attributes* specified by `BitFieldAttribute`.
Of course, the struct must also be a `partial`.

```cs
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
    // ... can use 4 bits for value
}

```

Then, the following accessor methods are generated:

```cs
public unsafe partial struct SomePacket
{
    public byte SomeByte => (byte)(_data[0] >>> 6);

    public uint SomeNotAlignedUInt32 => (uint)(((uint)(_data[0] & 0x3F) << 24) + ((uint)_data[1] << 16) + ((uint)_data[2] << 8) + ((uint)_data[3]));

    public Spangle.LusterBits.Tests.Learning.SomeEnum SomeEnumCastedValue => (Spangle.LusterBits.Tests.Learning.SomeEnum)(_data[4] >>> 4);

    public ushort SomeUShort => (ushort)(((_data[4] & 0x0F) << 11) + (_data[5] << 3) + (_data[6] >>> 5));

    public bool SomeBoolean => 0 != ((_data[6] & 0x10) >>> 4);

    public byte SomeByte2 => (byte)((_data[6] & 0x0C) >>> 2);

    public ushort SomeUShort2 => (ushort)(((_data[6] & 0x03) << 8) + (_data[7]));

}
```

You can use the generated accessor methods as follows:

```cs
ref readonly var packet = ref MemoryMarshal.Read<SomePacket>(buffer);
return packet.SomeByte; // The value is first 2bit of the byte sequence
```

Field Settings
--------------

The *field setting attributes* specified by `LusterCharmAttribute` have the following parameters:

- `type`: Type of the output parameter. Calculated value is casted to this type. The type MUST be a primitive type or
  enum.
- `name`: Name of the output parameter. This is used for the name of the accessor method.
- `length`: Length of the field in bits. If length is over the type, higher bits are ignored.
- `position`: Position of the field in bits. This value can be omitted if the fields are specified in order without
  alignment.
- `description`: Description of the field. When this value is specified, the accessor method is also documented with
  comment.

Types
-----

- Almost all primitive types
    - `byte`
    - `sbyte`
    - `ushort`
    - `short`
    - `uint`
    - `int`
    - `ulong`
    - `long`
    - `decimal`
    - `float`
    - `double`
    - `bool`
      - Evaluated as `fieldVal != 0`
- Enum types
- String types (**Experimental**)
    - `UTF8String`
    - `UTF16String`
    - `UTF16BigEndianString`
    - `string` (treated as `UTF8String`)

String types are experimental, because they can only be used in the rare case of fixed-length strings.

There may be cases where an enum-like (e.g. `A1234` | `B5678`) fixed length ASCII string is needed, in such cases, you
can use `UTF8String` and assume that each character is a single byte, and the string will be treated normally as an
ASCII string.

Nested Structs
--------------

Spangle.LusterBits does not support nested struct out of the box.
If you want to use nested struct, you should separate its own fixed byte array and child struct that
has `BitFieldAttribute`.

```cs
[LusterCharm]
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public unsafe partial struct SomePacket
{
    public const int PacketLength = 8; // 64 bits

    [
        BitField(typeof(byte), "SomeByte", 2),
        // its own field settings...
    ]
    private fixed byte _data[PacketLength];

    public readonly SomeNestedPacket SomeNestedPacket;
}
```

Request for Comments
--------------------

If this project is interesting to you and you have a real use for it, please let us know if there are any missing features.
We may not implement everything, but we may respond to make this project better.

Roadmap
-------

- [ ] Support for fields whose position depends on a condition specified by other fields
- [ ] Support for fields that can be present or absent depending on conditions
- [ ] Support reading from `IStream` and `PipeReader` directly for `GenerateType.Deserialized`
- [ ] Support changing of accessibility for `FieldAsSpan` (currently, always `internal`)
- [ ] Support giving the size of fixed array field explicitly for `FieldAsSpan`
- [ ] Support for explicitly specifying the size of a fixed array field to allow `FieldAsSpan` to have a free data field
  at the end.

Disclaimer
----------

This library is still in its early development stage.
The APIs are not stable and may change in future releases.

License
-------

This library is under the MIT License.
See the [LICENSE](./LICENSE) file for the full license text.
