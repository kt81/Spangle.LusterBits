using System.Text;

namespace Spangle.LusterBits;

/// <summary>
/// Indicates that the field is a UTF8 string
/// </summary>
public readonly ref struct UTF8String
{
    private readonly ReadOnlySpan<byte> _data;

    public UTF8String(ReadOnlySpan<byte> data)
    {
        _data = data;
    }

    public override string ToString()
    {
        return ((UTF8Encoding)Encoding.UTF8).GetString(_data);
    }
}

/// <summary>
/// Indicates that the field is a UTF16 string
/// </summary>
public readonly ref struct UTF16String
{
    private readonly ReadOnlySpan<byte> _data;

    public UTF16String(ReadOnlySpan<byte> data)
    {
        _data = data;
    }

    public override string ToString()
    {
        return Encoding.Unicode.GetString(_data);
    }
}

/// <summary>
/// Indicates that the field is a UTF16 big endian string
/// </summary>
public readonly ref struct UTF16BigEndianString
{
    private readonly ReadOnlySpan<byte> _data;

    public UTF16BigEndianString(ReadOnlySpan<byte> data)
    {
        _data = data;
    }

    public override string ToString()
    {
        return Encoding.BigEndianUnicode.GetString(_data);
    }
}

