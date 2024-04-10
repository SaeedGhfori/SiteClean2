using System.Security.Cryptography;
using System.Text;

namespace Common.Security.Encryptions
{
    public static class TripleDesEncryptionHelper
    {
        //3DES Algorithm Encryption
        public static string EncryptTripleDes(string plainText, string key)
        {
            using (TripleDESCryptoServiceProvider tripleDes = new TripleDESCryptoServiceProvider())
            {
                tripleDes.Key = Encoding.UTF8.GetBytes(key.Substring(0, 24));
                tripleDes.IV = new byte[8]; // Initialize to zero or some other IV if required
                tripleDes.Mode = CipherMode.ECB; // Or CipherMode.CBC
                tripleDes.Padding = PaddingMode.PKCS7;

                ICryptoTransform encryptor = tripleDes.CreateEncryptor(tripleDes.Key, tripleDes.IV);

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
        }

        public static string DecryptTripleDes(string cipherText, string key)
        {
            using (TripleDESCryptoServiceProvider tripleDes = new TripleDESCryptoServiceProvider())
            {
                tripleDes.Key = Encoding.UTF8.GetBytes(key.Substring(0, 24));
                tripleDes.IV = new byte[8]; // Initialize to zero or some other IV if required
                tripleDes.Mode = CipherMode.ECB; // Or CipherMode.CBC
                tripleDes.Padding = PaddingMode.PKCS7;

                ICryptoTransform decryptor = tripleDes.CreateDecryptor(tripleDes.Key, tripleDes.IV);

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

}
