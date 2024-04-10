using System.Security.Cryptography;
using System.Text;

namespace Common.Security.Encryptions
{
    public static class DesEncryptionHelper
    {
        //DES Algorithm Encryption
        public static string EncryptDes(string plainText, string key)
        {
            using (DES des = DES.Create())
            {
                des.Key = Encoding.UTF8.GetBytes(key.Substring(0, 8));
                des.IV = Encoding.UTF8.GetBytes(key.Substring(0, 8));

                ICryptoTransform encryptor = des.CreateEncryptor(des.Key, des.IV);

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

        public static string DecryptDes(string cipherText, string key)
        {
            using (DES des = DES.Create())
            {
                des.Key = Encoding.UTF8.GetBytes(key.Substring(0, 8));
                des.IV = Encoding.UTF8.GetBytes(key.Substring(0, 8));

                ICryptoTransform decryptor = des.CreateDecryptor(des.Key, des.IV);

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
