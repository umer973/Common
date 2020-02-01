using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace KI.RIS.EncryptionDecryption.Algorithms
{
    public class SHA1Service
    {
        public string Encrypt(string sourceString)
        {
            SHA1 sha1 = new SHA1CryptoServiceProvider();

            byte[] data = Encoding.Default.GetBytes(sourceString);
            byte[] hash = sha1.ComputeHash(data);
            // Transforms as hexa
            string hexaHash = "";
            foreach (byte b in hash)
            {
                hexaHash += String.Format("{0:x2}", b);
            }
            return hexaHash;
        }
    }
}
