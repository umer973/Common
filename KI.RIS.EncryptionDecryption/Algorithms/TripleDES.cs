using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace KI.RIS.EncryptionDecryption.Algorithms
{
    /// <summary>
    /// Class to encrypt and decrypt data
    /// </summary>
    public class TripleDES
    {
        /// <summary>
        /// encrypt the string with a specific key value
        /// </summary>
        /// <param name="value"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public string Encrypt(string value, string key)
        {
            System.Security.Cryptography.TripleDESCryptoServiceProvider DES = new System.Security.Cryptography.TripleDESCryptoServiceProvider();
            System.Security.Cryptography.MD5CryptoServiceProvider hashMD5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            // scramble the key
            key = ScrambleKey(key);
            // Compute the MD5 hash.
            DES.Key = hashMD5.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(key));
            // Set the cipher mode.
            DES.Mode = System.Security.Cryptography.CipherMode.ECB;
            // Create the encryptor.
            System.Security.Cryptography.ICryptoTransform DESEncrypt = DES.CreateEncryptor();
            // Get a byte array of the string.
            byte[] Buffer = System.Text.ASCIIEncoding.ASCII.GetBytes(value);
            // Transform and return the string.
            return Convert.ToBase64String(DESEncrypt.TransformFinalBlock(Buffer, 0, Buffer.Length));
        }
        /// <summary>
        /// decrypt the string
        /// </summary>
        /// <param name="value"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public string Decrypt(string value, string key)
        {
            System.Security.Cryptography.TripleDESCryptoServiceProvider DES = new System.Security.Cryptography.TripleDESCryptoServiceProvider();
            System.Security.Cryptography.MD5CryptoServiceProvider hashMD5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            // scramble the key
            key = ScrambleKey(key);
            // Compute the MD5 hash.
            DES.Key = hashMD5.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(key));
            // Set the cipher mode.
            DES.Mode = System.Security.Cryptography.CipherMode.ECB;
            // Create the decryptor.
            System.Security.Cryptography.ICryptoTransform DESDecrypt = DES.CreateDecryptor();
            byte[] Buffer = Convert.FromBase64String(value);
            // Transform and return the string.
            return System.Text.ASCIIEncoding.ASCII.GetString(DESDecrypt.TransformFinalBlock(Buffer, 0, Buffer.Length));
        }
        /// <summary>
        /// scrable the key 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private string ScrambleKey(string key)
        {
            System.Text.StringBuilder sbKey = new System.Text.StringBuilder();
            int intPtr = 0;
            for (intPtr = 1; intPtr <= key.Length; intPtr++)
            {
                int intIn = key.Length - intPtr + 1;
                sbKey.Append(key.Substring(intIn - 1, 1));
            }

            string strKey = sbKey.ToString();

            return sbKey.ToString();

        }




    }
   


}
