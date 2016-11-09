using System;
using System.Security.Cryptography;
using System.Text;

namespace AsymCryptor.Logics
{
    public static class AsymCryptoHelper
    {
        public static Int32 KeySize = 512;
        public static bool IsUsingOaepPadding = false;

        public static string EncryptToBase64String(string data, string key)
        {
            var cryptoServiceProvider = CreateCrpytoServiceProvider();
            cryptoServiceProvider.FromXmlString(key);
            var encoding = new UTF8Encoding();
            byte[] inputData = Convert.FromBase64String(Convert.ToBase64String(encoding.GetBytes(data)));
            byte[] outputData = cryptoServiceProvider.Encrypt(inputData, IsUsingOaepPadding);
            return Convert.ToBase64String(outputData);
        }

        public static string DecryptToString(string data, string key)
        {
            var cryptoServiceProvider = CreateCrpytoServiceProvider();
            cryptoServiceProvider.FromXmlString(key);
            var encoding = new UTF8Encoding();
            byte[] inputData = Convert.FromBase64String(data);
            byte[] outputData = cryptoServiceProvider.Decrypt(inputData, IsUsingOaepPadding);
            return encoding.GetString(outputData);
        }

        private static RSACryptoServiceProvider CreateCrpytoServiceProvider()
        {
            return new RSACryptoServiceProvider(KeySize);
        }
    }
}
