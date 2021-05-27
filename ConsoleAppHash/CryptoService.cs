using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ConsoleAppHash
{
    public class CryptoService
    {
        private static byte[] _salt = new byte[] { 0xC3, 0xA9, 0xE3, 0x1F, 0xCC, 0xD1, 0xD2, 0x2E, 0x68, 0x25, 0xB9, 0x9B, 0xCA, 0xE9, 0x41, 0x63 };

        /// <summary>???.</summary>
        /// <param name="lPassword"><c>string</c>???.</param>
        /// <returns><c>string</c>???.</returns>
        public static string DescriptografaSenhaPARSUSU(string lPassword)
        {
            string lSenha = lPassword;
            StringBuilder sb = new StringBuilder();

            for (int lInd = 1; (lInd <= 8); lInd++)
            {
                char lCaracter = lSenha[(lInd - 1)];
                sb.Append(((char)((lCaracter) - (20 + lInd))).ToString());
            }

            return sb.ToString().Trim();
        }

        /// <summary>Encrypt the given string using AES. The string can be decrypted using DecryptStringAES(). The sharedSecret parameters must match.</summary>
        /// <param name="plainText"><c>string</c>The text to encrypt.</param>
        /// <param name="sharedSecret"><c>string</c>A password used to generate a key for encryption.</param>
        public static string EncryptStringAES(string plainText, string sharedSecret)
        {

            if (string.IsNullOrEmpty(plainText))
                throw new ArgumentNullException("plainText");
            if (string.IsNullOrEmpty(sharedSecret))
                throw new ArgumentNullException("sharedSecret");

            string outStr = null;                       // Encrypted string to return
            RijndaelManaged aesAlg = null;              // RijndaelManaged object used to encrypt the data.

            MemoryStream msEncrypt = null;

            CryptoStream csEncrypt = null;

            try
            {
                // generate the key from the shared secret and the salt
                Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(sharedSecret, _salt);

                // Create a RijndaelManaged object
                aesAlg = new RijndaelManaged();
                aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);
                aesAlg.Padding = PaddingMode.Zeros;
                // Create a decryptor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                msEncrypt = new MemoryStream();

                msEncrypt.Write(BitConverter.GetBytes(aesAlg.IV.Length), 0, sizeof(int));
                msEncrypt.Write(aesAlg.IV, 0, aesAlg.IV.Length);

                csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);

                using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                {
                    //Write all data to the stream.
                    swEncrypt.Write(plainText);
                }

                outStr = Convert.ToBase64String(msEncrypt.ToArray());
            }
            finally
            {
                // Clear the RijndaelManaged object.
                if (aesAlg != null)
                {
                    aesAlg.Clear();
                }
            }

            // Return the encrypted bytes from the memory stream.
            return outStr;
        }

        /// <summary>Decrypt the given string. Assumes the string was encrypted using EncryptStringAES(), using an identical sharedSecret.</summary>
        /// <param name="inBuffer"><c>byte[]</c>???.</param>
        /// <param name="sharedSecret"><c>string</c>???.</param>
        /// <returns><c>string</c>???.</returns>
        public static string DecryptStringAES(byte[] inBuffer, string sharedSecret)
        {
            if (inBuffer == null || inBuffer.Length == 0)
                throw new ArgumentNullException("inBuffer");
            if (string.IsNullOrEmpty(sharedSecret))
                throw new ArgumentNullException("sharedSecret");

            // Declare the RijndaelManaged object
            // used to decrypt the data.
            RijndaelManaged aesAlg = null;
            MemoryStream msDecrypt = null;
            CryptoStream csDecrypt = null;
            // Declare the string used to hold
            // the decrypted text.
            byte[] outBuffer = null;

            try
            {
                // generate the key from the shared secret and the salt
                Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(sharedSecret, _salt);
                byte[] preBuffer = Convert.FromBase64String(Encoding.Default.GetString(inBuffer));
                // Create the streams used for decryption. 

                msDecrypt = new MemoryStream(preBuffer);

                // Create a RijndaelManaged object
                // with the specified key and IV.
                aesAlg = new RijndaelManaged();
                aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);

                // Get the initialization vector from the encrypted stream
                aesAlg.IV = ReadByteArray(msDecrypt);
                aesAlg.Padding = PaddingMode.Zeros;
                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);

                using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                {
                    outBuffer = Encoding.Default.GetBytes(srDecrypt.ReadToEnd());
                }

            }
            finally
            {
                if (aesAlg != null)
                {
                    aesAlg.Clear();
                }
            }

            return Encoding.Default.GetString(outBuffer).TrimEnd('\0');
        }

        /// <summary>???.</summary>
        /// <param name="s"><c>string</c>???.</param>
        /// <returns><c>Stream</c>???.</returns>
        public static byte[] ReadByteArray(Stream s)
        {
            byte[] rawLength = new byte[sizeof(int)];
            if (s.Read(rawLength, 0, rawLength.Length) != rawLength.Length)
            {
                throw new IOException("Stream did not contain properly formatted byte array");
            }

            byte[] buffer = new byte[BitConverter.ToInt32(rawLength, 0)];
            if (s.Read(buffer, 0, buffer.Length) != buffer.Length)
            {
                throw new IOException("Did not read byte array properly");
            }

            return buffer;
        }
    }
}
