using System;

namespace NGNSA
{
    class Randomizer
    {
        const int n = 624;
        const int m = 397;
        int index = n;
        uint[] mt = new uint[n];

        public Randomizer(uint seed)
        {
            mt[0] = seed & 0xffffffffU;
            for (var mti = 1; mti < n; ++mti)
            {
                mt[mti] = (69069 * mt[mti - 1]) & 0xffffffffU;
            }
        }

        uint ExtractNumber()
        {
            if (index >= n)
                Twist();

            uint y = mt[index];

            // right shift by 11 bits
            y = y ^ (y >> 11);
            y = y ^ ((y << 7) & 2636928640);
            y = y ^ ((y << 15) & 4022730752);
            y = y ^ (y >> 18);
            index++;

            return y;
        }

        uint _int32(long x)
        {
            return (uint)(0xFFFFFFF & x);
        }

        void Twist()
        {
            for (int i = 0; i < n; i++)
            {
                uint y = ((mt[i]) & 0x80000000) +
                    ((mt[(i + 1) % n]) & 0x7fffffff);
                mt[i] = mt[(i + m) % n] ^ (uint)(y >> 1);
                if (y % 2 != 0)
                    mt[i] = mt[i] ^ 0x9908b0df;
            }
            index = 0;
        }

        // Real functions
        public uint NextUInt() { return ExtractNumber(); }
        // Can be negative
        public int NextInt() { return unchecked((int)ExtractNumber()); }
        // max is NOT included
        public int NextInt(int max) { return (int)(NextUInt() % max); }
        // between min (included) and max (excluded)
        public int NextInt(int min, int max) { return (int)(NextUInt() % (max - min) + min); }
        // between 0 and 1 (included)
        public float NextFloat() { return (float)(NextUInt() % 65536) / 65535.0f; }
    }
}