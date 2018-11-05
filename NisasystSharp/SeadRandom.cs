namespace NisasystSharp
{
    internal class SeadRandom
    {
        private uint[] internalData;

        public SeadRandom(uint seed)
        {
            internalData = new uint[4];
	        internalData[0] = 1812433253 * (seed ^ (seed >> 30)) + 1;
	        internalData[1] = 1812433253 * (internalData[0] ^ (internalData[0] >> 30)) + 2;
	        internalData[2] = 1812433253 * (internalData[1] ^ (internalData[1] >> 30)) + 3;
	        internalData[3] = 1812433253 * (internalData[2] ^ (internalData[2] >> 30)) + 4;
        }

        public SeadRandom(uint seedOne, uint seedTwo, uint seedThree, uint seedFour)
        {
            internalData = new uint[] { seedOne, seedTwo, seedThree, seedFour };
        }

        public uint GetUInt32()
        {
            uint v1;
            uint v2;
            uint v3;

            v1 = internalData[0] ^ (internalData[0] << 11);
            internalData[0] = internalData[1];
            v2 = internalData[3];
            v3 = v1 ^ (v1 >> 8) ^ v2 ^ (v2 >> 19);
            internalData[1] = internalData[2];
            internalData[2] = v2;
            internalData[3] = v3;

            return v3;
        }

    }
}
