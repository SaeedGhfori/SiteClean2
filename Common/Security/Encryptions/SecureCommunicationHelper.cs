using System.Security.Cryptography;
using System.Text;

namespace Common.Security.Encryptions
{
    public static class SecureCommunicationHelper
    {
        //RSA Algorithm Encryption

        // Generate a public/private key pair
        public static (string publicKey, string privateKey) GenerateKeys()
        {
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                return (rsa.ToXmlString(false), rsa.ToXmlString(true));
            }
        }

        public static string EncryptData(string plainText, string publicKey)
        {
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.FromXmlString(publicKey);
                var encryptedData = rsa.Encrypt(Encoding.UTF8.GetBytes(plainText), true);
                return Convert.ToBase64String(encryptedData);
            }
        }

        public static string DecryptData(string encryptedText, string privateKey)
        {
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.FromXmlString(privateKey);
                var decryptedData = rsa.Decrypt(Convert.FromBase64String(encryptedText), true);
                return Encoding.UTF8.GetString(decryptedData);
            }
        }
    }
}
