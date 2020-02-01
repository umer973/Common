
namespace KI.RIS.EncryptionDecryption.Algorithms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Security.Cryptography;

    public class CryptographySymmetricAlgorithm
    {

        public enum SymmetricAlgorithmType : int
        {
            DES, TripleDES, RC2, Rijndael
        }

        private SymmetricAlgorithm objCryptoService;
        private Int32 SymmetricAlgorithmKey;

        /// <summary>
        /// Cryptography Symmetric Algorithm
        /// </summary>
        /// <param name="NetSelected">SymmetricAlgorithmType</param>
        public CryptographySymmetricAlgorithm(SymmetricAlgorithmType NetSelected)
        {
            switch (NetSelected)
            {
                case SymmetricAlgorithmType.DES:
                    {
                        objCryptoService = new DESCryptoServiceProvider();
                        SymmetricAlgorithmKey = 1;
                        break;
                    }
                case SymmetricAlgorithmType.TripleDES:
                    {
                        objCryptoService = new TripleDESCryptoServiceProvider();
                        SymmetricAlgorithmKey = 2;
                        break;
                    }
                case SymmetricAlgorithmType.RC2:
                    {
                        objCryptoService = new RC2CryptoServiceProvider();
                        SymmetricAlgorithmKey = 3;
                        break;
                    }
                case SymmetricAlgorithmType.Rijndael:
                    {
                        objCryptoService = new RijndaelManaged();
                        SymmetricAlgorithmKey = 4;
                        break;
                    }
            }
        }

        /// <summary>
        /// CryptographySymmetricAlgorithm
        /// </summary>
        /// <param name="ServiceProvider">SymmetricAlgorithm</param>
        public CryptographySymmetricAlgorithm(SymmetricAlgorithm ServiceProvider)
        {
            objCryptoService = ServiceProvider;
        }

        /// <summary>
        /// Encrypting
        /// </summary>
        /// <param name="Source">string</param>
        /// <param name="Key">string</param>
        /// <returns>string</returns>
        public string Encrypting(string Source, string Key)
        {
            try
            {
                byte[] bytIn = System.Text.ASCIIEncoding.ASCII.GetBytes(Source);
                // create a MemoryStream so that the process can be done without I/O files
                System.IO.MemoryStream ms = new System.IO.MemoryStream();

                byte[] bytKey = GetLegalKey(Key);

                // set the private key
                objCryptoService.Key = bytKey;
                objCryptoService.IV = bytKey;

                // create an Encryptor from the Provider Service instance
                ICryptoTransform encrypto = objCryptoService.CreateEncryptor();

                // create Crypto Stream that transforms a stream using the encryption
                CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Write);

                // write out encrypted content into MemoryStream
                cs.Write(bytIn, 0, bytIn.Length);
                cs.FlushFinalBlock();

                // get the output and trim the '\0' bytes
                byte[] bytOut = ms.GetBuffer();
                int i = 0;
                for (i = 0; i < bytOut.Length; i++)
                    if (bytOut[i] == 0)
                        break;

                // convert into Base64 so that the result can be used in xml
                //return System.Convert.ToBase64String(bytOut, 0, i);


                string ReturnValue = string.Empty;
                ReturnValue = System.Convert.ToBase64String(bytOut, 0, i);

                if ((SymmetricAlgorithmKey == 1) && (ReturnValue.Length <= 8))
                    ReturnValue = Encrypting("YASASII_ERROR_PROTECTOR" + Source, Key);

                return ReturnValue;
            }
            catch 
            {
                throw;
            }
        }

        /// <summary>
        /// Decrypting
        /// </summary>
        /// <param name="Source">string</param>
        /// <param name="Key">string</param>
        /// <returns>string</returns>
        public string Decrypting(string Source, string Key)
        {
            try
            {
                // convert from Base64 to binary
                byte[] bytIn = System.Convert.FromBase64String(Source);
                // create a MemoryStream with the input
                System.IO.MemoryStream ms = new System.IO.MemoryStream(bytIn, 0, bytIn.Length);

                byte[] bytKey = GetLegalKey(Key);

                // set the private key
                objCryptoService.Key = bytKey;
                objCryptoService.IV = bytKey;

                // create a Decryptor from the Provider Service instance
                ICryptoTransform encrypto = objCryptoService.CreateDecryptor();

                // create Crypto Stream that transforms a stream using the decryption
                CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Read);

                // read out the result from the Crypto Stream
                System.IO.StreamReader sr = new System.IO.StreamReader(cs);
                //return sr.ReadToEnd();

                string ReturnValue = string.Empty;
                ReturnValue = sr.ReadToEnd();
                if (SymmetricAlgorithmKey == 1)
                    ReturnValue = ReturnValue.Replace("YASASII_ERROR_PROTECTOR", "");

                return ReturnValue;

            }
            catch 
            {
                throw;
            }
        }

        /// <summary>
        /// GetLegalKey
        /// </summary>
        /// <param name="Key">string</param>
        /// <returns>byte[]</returns>
        private byte[] GetLegalKey(string Key)
        {
            string sTemp;
            if (objCryptoService.LegalKeySizes.Length > 0)
            {
                int lessSize = 0, moreSize = objCryptoService.LegalKeySizes[0].MinSize;
                // key sizes are in bits
                while (Key.Length * 8 > moreSize)
                {
                    lessSize = moreSize;
                    moreSize += objCryptoService.LegalKeySizes[0].SkipSize;
                }
                sTemp = Key.PadRight(moreSize / 8, ' ');
            }
            else
                sTemp = Key;

            // convert the secret key to byte array
            return ASCIIEncoding.ASCII.GetBytes(sTemp);
        }
    }
}
