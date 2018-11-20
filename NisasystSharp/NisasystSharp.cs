using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace NisasystSharp
{
    class NisasystSharp
    {
        private static string KeyMaterialString = "e413645fa69cafe34a76192843e48cbd691d1f9fba87e8a23d40e02ce13b0d534d10301576f31bc70b763a60cf07149cfca50e2a6b3955b98f26ca84a5844a8aeca7318f8d7dba406af4e45c4806fa4d7b736d51cceaaf0e96f657bb3a8af9b175d51b9bddc1ed475677260f33c41ddbc1ee30b46c4df1b24a25cf7cb6019794";
        private static readonly char[] KeyMaterial = KeyMaterialString.ToCharArray();

        private static readonly string MagicNumbers = "nisasyst";

        public static void Decrypt(string[] args)
        {
            using (FileStream fileStream = new FileStream(args[1], FileMode.Open))
            using (StreamReader streamReader = new StreamReader(fileStream))
            {
                // Test the file length
                if (fileStream.Length <= 8)
                {
                    // This can't be a valid file
                    throw new Exception("The file is too small to be a nisasyst container");
                }

                // Seek to the magic numbers
                fileStream.Seek(-8, SeekOrigin.End);

                // Verify the magic numbers
                if (streamReader.ReadToEnd() != MagicNumbers)
                {
                    // This isn't a valid file
                    throw new Exception("This file isn't a nisasyst container");
                }

                // Generate a CRC32 over the game path
                Crc32 crc32 = new Crc32();
                uint seed = crc32.Get(Encoding.ASCII.GetBytes(args[0]));

                // Create a new SeadRandom instance using the seed
                SeadRandom seadRandom = new SeadRandom(seed);

                // Create the encryption key and IV
                byte[] encryptionKey = CreateSequence(seadRandom);
                byte[] iv = CreateSequence(seadRandom);

                using (MemoryStream memoryStream = new MemoryStream())
                using (AesManaged cryptor = new AesManaged())
                {
                    cryptor.Mode = CipherMode.CBC;
                    cryptor.Padding = PaddingMode.PKCS7;
                    cryptor.KeySize = 128;
                    cryptor.BlockSize = 128;

                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptor.CreateDecryptor(encryptionKey, iv), CryptoStreamMode.Write))
                    {
                        // Seek to the beginning of the file
                        fileStream.Seek(0, SeekOrigin.Begin);

                        // Copy the encrypted data
                        CopyStream(fileStream, cryptoStream, (int)fileStream.Length - 8);
                    }

                    // Write out the new file
                    File.WriteAllBytes(args[1] + ".decrypted", memoryStream.ToArray());
                }
            }
        }

        private static byte[] CreateSequence(SeadRandom random)
        {
            // Create byte array
            byte[] sequence = new byte[16];

            // Create each byte
            for (int i = 0; i < sequence.Length; i++)
            {
                // Create empty byte string
                string byteString = "";// "0x";

                // Get characters from key material
                byteString += KeyMaterial[random.GetUInt32() >> 24];
                byteString += KeyMaterial[random.GetUInt32() >> 24];

                // Parse the resulting byte
                sequence[i] = Convert.ToByte(byteString, 16);
            }

            // Return the sequence
            return sequence;
        }

        // https://stackoverflow.com/questions/13021866/any-way-to-use-stream-copyto-to-copy-only-certain-number-of-bytes
        private static void CopyStream(Stream input, Stream output, int bytes)
        {
            byte[] buffer = new byte[32768];
            int read;
            while (bytes > 0 &&
                   (read = input.Read(buffer, 0, Math.Min(buffer.Length, bytes))) > 0)
            {
                output.Write(buffer, 0, read);
                bytes -= read;
            }
        }

    }
}
