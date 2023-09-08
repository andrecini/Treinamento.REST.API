using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Treinamento.REST.Domain.Interfaces.Services;
using Treinamento.REST.Domain.Settings;

namespace Treinamento.REST.Services.Services
{
    public class EncryptorService : IEncryptorService
    {
        public string Encrypt(string password)
        {
            using var aesAlg = Aes.Create();
            using var encryptor = aesAlg.CreateEncryptor(EncryptorSettings.Key, aesAlg.IV);

            using var msEncrypt = new MemoryStream();
            using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
            using (var swEncrypt = new StreamWriter(csEncrypt))
            {
                swEncrypt.Write(password);
            }

            var iv = aesAlg.IV;
            var encryptedContent = msEncrypt.ToArray();

            var result = new byte[iv.Length + encryptedContent.Length];
            Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
            Buffer.BlockCopy(encryptedContent, 0, result, iv.Length, encryptedContent.Length);

            return Convert.ToBase64String(result);
        }

        public string Decrypt(string encryptedPassword)
        {
            var fullCipher = Convert.FromBase64String(encryptedPassword);

            using var aesAlg = Aes.Create();
            var iv = new byte[aesAlg.IV.Length];
            var cipher = new byte[fullCipher.Length - iv.Length];

            Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
            Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, cipher.Length);

            using var decryptor = aesAlg.CreateDecryptor(EncryptorSettings.Key, iv);

            using var msDecrypt = new MemoryStream(cipher);
            using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
            using var srDecrypt = new StreamReader(csDecrypt);

            return srDecrypt.ReadToEnd();
        }
    }
}
