namespace Spangle.LusterBits.Tests;

public class UnicodeStringTests
{
    private unsafe struct StringStruct
    {
        public fixed byte _data[256];

        private ReadOnlySpan<byte> AsSpan()
        {
            fixed (void* p = _data)
            {
                return new ReadOnlySpan<byte>(p, 256);
            }
        }

        public string UTF8StringField => new UTF8String(AsSpan()[..10]).ToString();
    }
}
