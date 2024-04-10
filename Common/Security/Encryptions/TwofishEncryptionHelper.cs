using Medo.Security.Cryptography;
using System.Security.Cryptography;
using System.Text;

namespace Common.Security.Encryptions
{
    public static class TwofishEncryptionHelper
    {
        //Twofish Algorithm Encryption
        //Install-Package Medo.Twofish -Version 2.1.0

        public static string EncryptTwofish(string plainText, string key)
        {
            byte[] keyArray = Encoding.UTF8.GetBytes(key);
            Twofish twofish = new Twofish
            {
                Key = keyArray,
                Mode = CipherMode.CBC, // یا CipherMode.ECB
                IV = new byte[16] // برای حالت CBC
            };

            ICryptoTransform encryptor = twofish.CreateEncryptor();

            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                    {
                        streamWriter.Write(plainText);
                    }
                }

                return Convert.ToBase64String(memoryStream.ToArray());
            }
        }

        public static string DecryptTwofish(string cipherText, string key)
        {
            byte[] keyArray = Encoding.UTF8.GetBytes(key);
            Twofish twofish = new Twofish
            {
                Key = keyArray,
                Mode = CipherMode.CBC, // یا CipherMode.ECB
                IV = new byte[16] // برای حالت CBC
            };

            ICryptoTransform decryptor = twofish.CreateDecryptor();

            using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(cipherText)))
            {
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader streamReader = new StreamReader(cryptoStream))
                    {
                        return streamReader.ReadToEnd();
                    }
                }
            }
        }
    }

}
