using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Treinamento.REST.Domain.Interfaces.Services
{
    public interface IEncryptorService
    {
        string Encrypt(string password);

        string Decrypt(string encryptedPassword);
    }
}
