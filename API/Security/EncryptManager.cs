using API.Security.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace API.Security
{
    public class EncryptManager
    {
        public string CreateSaltString()
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buffer = new byte[20];

            rng.GetBytes(buffer);
            string salt = BitConverter.ToString(buffer);
            Console.WriteLine(salt);
            return salt;
        }

        public string Hash(string salt, string password)
        {
            string saltstring = String.Concat(password, salt);
            HMACSHA1 hash = new HMACSHA1();
            string encoded = Convert.ToBase64String(hash.ComputeHash(Encoding.Unicode.GetBytes(saltstring)));
            Console.Write(encoded);
            return encoded;
        }
    }
}
