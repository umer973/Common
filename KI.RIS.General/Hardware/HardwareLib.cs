using Microsoft.Win32;
using System;
using System.Collections;
using System.Management;
using System.Security.Cryptography;
using System.Text;

namespace KI.RIS.General.Hardware
{
    public class BLHardwareLib
    {
        private const string _charList = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private static readonly char[] _charArray = _charList.ToCharArray();
        public static string GenerateHardwareUid()
        {
            string hardwareUid = string.Empty;
            byte[] _checksum = GetHardwareUID();

            //Convert checksum into 4 ulong parts and use BASE36 to encode both
            string _part1Id = Encode(BitConverter.ToUInt32(_checksum, 0));
            string _part2Id = Encode(BitConverter.ToUInt32(_checksum, 4));
            string _part3Id = Encode(BitConverter.ToUInt32(_checksum, 8));
            string _part4Id = Encode(BitConverter.ToUInt32(_checksum, 12));

            //Concat these 4 part into one string
            hardwareUid = string.Format("{0}-{1}-{2}-{3}", _part1Id, _part2Id, _part3Id, _part4Id);

            return hardwareUid;
        }
        public static string ReadLicenseKeyFromRegistry()
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

        private static long Decode(string input)
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

        private static string Encode(ulong input)
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