using System.IO;

namespace Yaz0Enc
{
    // Code heavily based on https://github.com/LordNed/WArchive-Tools/blob/master/ArchiveToolsLib/Compression/Yaz0Encoder.cs
    public static class Yaz0
    {
        private static int sNumBytes1, sMatchPos;
        private static bool sPrevFlag = false;

        public static byte[] Encode(Stream Input)
        {
            using (var Strm = new MemoryStream())
            using (var Wrt = new BinaryWriter(Strm))
            {
                Wrt.Write(0x307a6159);
                Wrt.Write((int)Input.Length);
                Wrt.Write(0L);
                SimpleEnc(Input, Wrt);
                return Strm.ToArray();
            }
        }

        private static void SimpleEnc(Stream Input, BinaryWriter Output)
        {
            int srcPos = 0, dstPos = 0, validBitCount = 0, Len = (int)Input.Length;
            byte[] dst = new byte[24], Buf = new byte[Len];
            byte curCodeByte = 0;
            Input.Read(Buf, 0, Len);
            while (srcPos < Len)
            {
                NintendoEnc(Buf, srcPos, out int numBytes, out int matchPos);
                if (numBytes < 3)
                {
                    dst[dstPos] = Buf[srcPos];
                    srcPos++;
                    dstPos++;
                    curCodeByte |= (byte)(0x80 >> validBitCount);
                }
                else
                {
                    var dist = (uint)(srcPos - matchPos - 1);
                    byte byte1, byte2, byte3;
                    if (numBytes >= 0x12)
                    {
                        byte1 = (byte)(0 | (dist >> 8));
                        byte2 = (byte)(dist & 0xFF);
                        dst[dstPos++] = byte1;
                        dst[dstPos++] = byte2;
                        if (numBytes > 0xFF + 0x12) numBytes = 0xFF + 0x12;
                        byte3 = (byte)(numBytes - 0x12);
                        dst[dstPos++] = byte3;
                    }
                    else
                    {
                        byte1 = (byte)((uint)((numBytes - 2) << 4) | (dist >> 8));
                        byte2 = (byte)(dist & 0xFF);
                        dst[dstPos++] = byte1;
                        dst[dstPos++] = byte2;
                    }
                    srcPos += numBytes;
                }
                validBitCount++;
                if (validBitCount == 8)
                {
                    Output.Write(curCodeByte);
                    for (int i = 0; i < dstPos; i++) Output.Write(dst[i]);
                    curCodeByte = 0;
                    validBitCount = 0;
                    dstPos = 0;
                }
            }
            if (validBitCount > 0)
            {
                Output.Write(curCodeByte);
                for (int i = 0; i < dstPos; i++) Output.Write(dst[i]);
                curCodeByte = 0;
                validBitCount = 0;
                dstPos = 0;
            }
        }

        private static void NintendoEnc(byte[] src, int srcPos, out int outNumBytes, out int outMatchPos)
        {
            var startPos = srcPos - 0x1000;
            if (sPrevFlag)
            {
                outMatchPos = sMatchPos;
                sPrevFlag = false;
                outNumBytes = sNumBytes1;
                return;
            }
            sPrevFlag = false;
            RLEEncode(src, srcPos, out int numBytes, out sMatchPos);
            outMatchPos = sMatchPos;
            if (numBytes >= 3)
            {
                RLEEncode(src, srcPos + 1, out sNumBytes1, out sMatchPos);

                if (sNumBytes1 >= numBytes + 2)
                {
                    numBytes = 1;
                    sPrevFlag = true;
                }
            }
            outNumBytes = numBytes;
        }

        private static void RLEEncode(byte[] src, int srcPos, out int outNumBytes, out int outMatchPos)
        {
            int startPos = srcPos - 0x400, numBytes = 1, matchPos = 0;
            if (startPos < 0) startPos = 0;
            for (int i = startPos, j; i < srcPos; i++)
            {
                for (j = 0; j < src.Length - srcPos; j++)
                {
                    if (src[i + j] != src[j + srcPos])
                        break;
                }
                if (j > numBytes)
                {
                    numBytes = j;
                    matchPos = i;
                }
            }
            outMatchPos = matchPos;
            if (numBytes == 2) numBytes = 1;
            outNumBytes = numBytes;
        }
    }
}