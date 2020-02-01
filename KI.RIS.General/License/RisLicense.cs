using Microsoft.Win32;
using System;
using System.Collections;
using System.Management;
using System.Security.Cryptography;
using System.Text;

namespace KI.RIS.General.License
{
    public class RisLicense
    {
        public static string GenerateLicense(string hardwareId, string refId, DateTime fromDate, DateTime toDate, string siteName)
        {
            string licenseKey = string.Empty;
            string uidCombination = string.Empty;

            //Concat these 4 part into one string
            uidCombination = hardwareId.Trim() + "|" + fromDate.Date.ToString("dd-MMM-yyy") + "|" + toDate.Date.ToString("dd-MMM-yyy") + "|" + siteName.Trim();

            licenseKey = EncryptLicense(refId, uidCombination);

            return licenseKey.Trim();
        }

        public static string GenerateHardwareUid()
        {
            string hardwareUid = string.Empty;
            byte[] _checksum = GetHardwareUID();

            //Convert checksum into 4 ulong parts and use BASE36 to encode both
            string _part1Id = BASE36.Encode(BitConverter.ToUInt32(_checksum, 0));
            string _part2Id = BASE36.Encode(BitConverter.ToUInt32(_checksum, 4));
            string _part3Id = BASE36.Encode(BitConverter.ToUInt32(_checksum, 8));
            string _part4Id = BASE36.Encode(BitConverter.ToUInt32(_checksum, 12));

            //Concat these 4 part into one string
            hardwareUid = string.Format("{0}-{1}-{2}-{3}", _part1Id, _part2Id, _part3Id, _part4Id);

            return hardwareUid;
        }

        public static string ReadLicenseKey()
        {
            try
            {
                string licensekey;
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\KI", false))
                {
                    if (key != null)
                    {
                        licensekey = key.GetValue("Uid") as string;
                    }
                    else
                    {
                        licensekey = string.Empty;
                    }
                }
                return licensekey;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static bool ValidateLicense(string licenseKey, string siteName, string refId)
        {
            bool validity = false;
            string hardwardUid = string.Empty;
            DateTime fromDate = DateTime.MinValue;
            DateTime toDate = DateTime.MinValue;
            string licenseSiteName = string.Empty;
            string decryptData = DecryptLicense(licenseKey.Trim(), refId);
            if (ValidateLicenseFormat(decryptData))
            {
                string[] licenseSplitup = decryptData.Split('|');
                hardwardUid = licenseSplitup[0].Trim();
                fromDate = Convert.ToDateTime(licenseSplitup[1].Trim());
                toDate = Convert.ToDateTime(licenseSplitup[2].Trim());
                licenseSiteName = licenseSplitup[3].Trim();
                if (siteName == licenseSiteName && toDate.Date >= DateTime.Now.Date)
                {
                    if (ValidateUIDFormat(hardwardUid))
                    {
                        byte[] hardwardUidInsideLicense = DecryptHardwareUID(hardwardUid);

                        byte[] hardwardUidOfSystem = GetHardwareUID();

                        validity = ((IStructuralEquatable)hardwardUidInsideLicense).Equals(hardwardUidOfSystem, StructuralComparisons.StructuralEqualityComparer);
                    }
                }
            }
            return validity;
        }

        private static byte[] GetHardwareUID()
        {
            //Combine the IDs and get bytes
            string _id = string.Concat(GetProcessorId(), GetMotherboardID());
            byte[] _byteIds = Encoding.UTF8.GetBytes(_id);

            //Use MD5 to get the fixed length checksum of the ID string
            MD5CryptoServiceProvider _md5 = new MD5CryptoServiceProvider();
            byte[] _checksum = _md5.ComputeHash(_byteIds);

            return _checksum;
        }

        private static bool ValidateUIDFormat(string UID)
        {
            if (!string.IsNullOrWhiteSpace(UID))
            {
                string[] _ids = UID.Split('-');

                return (_ids.Length == 4);
            }
            else
            {
                return false;
            }
        }

        private static bool ValidateLicenseFormat(string licenseId)
        {
            if (!string.IsNullOrWhiteSpace(licenseId))
            {
                string[] _ids = licenseId.Split('|');

                return (_ids.Length == 4);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Get volume serial number of drive C
        /// </summary>
        /// <returns></returns>
        private static string GetDiskVolumeSerialNumber(string drive)
        {
            try
            {
                ManagementObject _disk = new ManagementObject(@"win32_logicaldisk.deviceid=""" + drive + @":""");
                _disk.Get();
                return _disk["VolumeSerialNumber"].ToString();
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Get CPU ID
        /// </summary>
        /// <returns></returns>
        private static string GetProcessorId()
        {
            try
            {
                ManagementObjectSearcher _mbs = new ManagementObjectSearcher("Select ProcessorId From Win32_processor");
                ManagementObjectCollection _mbsList = _mbs.Get();
                string _id = string.Empty;
                foreach (ManagementObject _mo in _mbsList)
                {
                    _id = _mo["ProcessorId"].ToString();
                    break;
                }

                return _id;
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Get motherboard serial number
        /// </summary>
        /// <returns></returns>
        private static string GetMotherboardID()
        {
            try
            {
                ManagementObjectSearcher _mbs = new ManagementObjectSearcher("Select SerialNumber From Win32_BaseBoard");
                ManagementObjectCollection _mbsList = _mbs.Get();
                string _id = string.Empty;
                foreach (ManagementObject _mo in _mbsList)
                {
                    _id = _mo["SerialNumber"].ToString();
                    break;
                }

                return _id;
            }
            catch
            {
                return string.Empty;
            }
        }

        private static byte[] DecryptHardwareUID(string UID)
        {
            //Split 4 part Id into 4 ulong
            string[] _ids = UID.Split('-');

            //Combine 4 part Id into one byte array
            byte[] _value = new byte[16];
            Buffer.BlockCopy(BitConverter.GetBytes(BASE36.Decode(_ids[0])), 0, _value, 0, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(BASE36.Decode(_ids[1])), 0, _value, 4, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(BASE36.Decode(_ids[2])), 0, _value, 8, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(BASE36.Decode(_ids[3])), 0, _value, 12, 4);

            return _value;
        }

        private static string EncryptLicense(string refId, string uidCombination)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                using (var tdes = new TripleDESCryptoServiceProvider())
                {
                    tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(refId));
                    tdes.Mode = CipherMode.ECB;
                    tdes.Padding = PaddingMode.PKCS7;

                    using (var transform = tdes.CreateEncryptor())
                    {
                        byte[] textBytes = UTF8Encoding.UTF8.GetBytes(uidCombination);
                        byte[] bytes = transform.TransformFinalBlock(textBytes, 0, textBytes.Length);
                        return Convert.ToBase64String(bytes, 0, bytes.Length);
                    }
                }
            }
        }

        private static string DecryptLicense(string cipher, string refId)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                using (var tdes = new TripleDESCryptoServiceProvider())
                {
                    tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(refId));
                    tdes.Mode = CipherMode.ECB;
                    tdes.Padding = PaddingMode.PKCS7;

                    using (var transform = tdes.CreateDecryptor())
                    {
                        byte[] cipherBytes = Convert.FromBase64String(cipher);
                        byte[] bytes = transform.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
                        return UTF8Encoding.UTF8.GetString(bytes);
                    }
                }
            }
        }
    }

    public class BASE36
    {
        private const string _charList = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private static readonly char[] _charArray = _charList.ToCharArray();

        public static long Decode(string input)
        {
            long _result = 0;
            double _pow = 0;
            for (int _i = input.Length - 1; _i >= 0; _i--)
            {
                char _c = input[_i];
                int pos = _charList.IndexOf(_c);
                if (pos > -1)
                    _result += pos * (long)Math.Pow(_charList.Length, _pow);
                else
                    return -1;
                _pow++;
            }
            return _result;
        }

        public static string Encode(ulong input)
        {
            StringBuilder _sb = new StringBuilder();
            do
            {
                _sb.Append(_charArray[input % (ulong)_charList.Length]);
                input /= (ulong)_charList.Length;
            } while (input != 0);

            return Reverse(_sb.ToString());
        }

        private static string Reverse(string s)
        {
            var charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}