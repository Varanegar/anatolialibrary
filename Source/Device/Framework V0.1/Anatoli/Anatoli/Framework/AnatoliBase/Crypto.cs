using PCLCrypto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.Framework.AnatoliBase
{
    public static class Crypto
    {
        private const string stringMaterial = "lB#2g%Au";
        public static byte[] EncryptAES(string content)
        {
            byte[] keyMaterial = Encoding.Unicode.GetBytes(stringMaterial);
            byte[] data = Encoding.Unicode.GetBytes(content);
            var provider = WinRTCrypto.SymmetricKeyAlgorithmProvider.OpenAlgorithm(SymmetricAlgorithm.AesCbcPkcs7);
            var key = provider.CreateSymmetricKey(keyMaterial);
            byte[] iv = null; // this is optional, but must be the same for both encrypting and decrypting
            byte[] cipherText = WinRTCrypto.CryptographicEngine.Encrypt(key, data, iv);
            return cipherText;
        }
        public static byte[] DecryptAES(byte[] cipherText)
        {
            byte[] keyMaterial = Encoding.Unicode.GetBytes(stringMaterial);
            var provider = WinRTCrypto.SymmetricKeyAlgorithmProvider.OpenAlgorithm(SymmetricAlgorithm.AesCbcPkcs7);
            var key = provider.CreateSymmetricKey(keyMaterial);
            byte[] iv = null; // this is optional, but must be the same for both encrypting and decrypting
            byte[] plainText = WinRTCrypto.CryptographicEngine.Decrypt(key, cipherText, iv);
            return plainText;
        }
    }
}
