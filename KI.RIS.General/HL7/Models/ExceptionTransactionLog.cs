using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*-----------------------------------------------------------------------
<Copyright file="BLHl7TransactionLog.cs" Company="Kameda Infologics">
    Copyright@Kameda Infologics Pvt Ltd. All rights reserved.
</Copyright>

 Description     :Model class for Hl7 Transaction Log
 Created  By     :Muhammed Haris
 Created  Date   :31-Jan-2018 
 Modified By     :ModifiedBy  
 Modified Date   :ModifiedDate 
 Modified Purpose:ModifiedPur 
 -----------------------------------------------------------------------*/
namespace KI.RIS.General.HL7.Models
{
    public class ExceptionTransactionLog
    {
        /// <summary>
        /// Writes to log.
        /// </summary>
        /// <param name="ex">The ex.</param>
        public static void WriteToLog(Exception ex)
        {
            try
            {
                string ExceptionLocation = ConfigurationManager.AppSettings["HL7ExceptionWriteLog"].ToString();
                if (!System.IO.Directory.Exists(ExceptionLocation))
                {
                    System.IO.Directory.CreateDirectory(ExceptionLocation);
                }

                if (HL7.Static.Data.Hl7ExceptionLogFileName == null)
                {
                    HL7.Static.Data.Hl7ExceptionLogFileName = ExceptionLocation + "\\" + DateTime.Now.ToString("ddMMMyyyy") + "_" + DateTime.Now.Ticks + ".txt";
                }

                var directory = new DirectoryInfo(HL7.Static.Data.Hl7ExceptionLogFileName);

                double filesize = (directory.FullName.Length / 1024f) / 1024f;
                if (filesize >= 2)
                {
                    HL7.Static.Data.Hl7ExceptionLogFileName = ExceptionLocation + "\\" + DateTime.Now.ToString("ddMMMyyyy") + "_" + DateTime.Now.Ticks + ".txt";
                }

                string strWrite = DateTime.Now.ToString() + " " + GetSavingData(Convert.ToString(ex.Message)) + " " + GetSavingData(Convert.ToString(ex.Source)) + " " + GetSavingData((GetStackTraceData(ex) + Convert.ToString(ex.StackTrace)));

                lock (HL7.Static.Data.Hl7ExceptionLogFileName)
                {
                    StreamWriter objWriter = new StreamWriter(HL7.Static.Data.Hl7ExceptionLogFileName, true);
                    lock (objWriter)
                    {
                        objWriter.WriteLine(strWrite);
                        objWriter.WriteLine("------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                        objWriter.Flush();
                        objWriter.Close();

                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void WriteToLog(string LogData, Exception ex)
        {
            try
            {
                string ExceptionLocation = ConfigurationManager.AppSettings["HL7ExceptionWriteLog"].ToString();
                if (!System.IO.Directory.Exists(ExceptionLocation))
                {
                    System.IO.Directory.CreateDirectory(ExceptionLocation);
                }

                if (HL7.Static.Data.Hl7ExceptionLogFileName == null)
                {
                    HL7.Static.Data.Hl7ExceptionLogFileName = ExceptionLocation + "\\" + DateTime.Now.ToString("ddMMMyyyy") + "_" + DateTime.Now.Ticks + ".txt";
                }

                var directory = new DirectoryInfo(HL7.Static.Data.Hl7ExceptionLogFileName);

                double filesize = (directory.FullName.Length / 1024f) / 1024f;
                if (filesize >= 2)
                {
                    HL7.Static.Data.Hl7ExceptionLogFileName = ExceptionLocation + "\\" + DateTime.Now.ToString("ddMMMyyyy") + "_" + DateTime.Now.Ticks + ".txt";
                }

                string strWrite = GetSavingData(Convert.ToString(ex.Message)) + " " + GetSavingData(Convert.ToString(ex.Source)) + " " + GetSavingData((GetStackTraceData(ex) + Convert.ToString(ex.StackTrace)));

                lock (HL7.Static.Data.Hl7ExceptionLogFileName)
                {
                    StreamWriter objWriter = new StreamWriter(HL7.Static.Data.Hl7ExceptionLogFileName, true);
                    lock (objWriter)
                    {
                        objWriter.WriteLine(DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + " - " + LogData);
                        objWriter.WriteLine(strWrite);
                        objWriter.WriteLine("------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                        objWriter.Flush();
                        objWriter.Close();

                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

        }
        /// <summary>
        /// Gets the saving data.
        /// </summary>
        /// <param name="strData">The STR data.</param>
        /// <returns></returns>
        private static string GetSavingData(string strData)
        {
            if (strData.Length > 4000)
            {
                strData = strData.Substring(0, 4000);
            }
            return strData;
        }

        /// <summary>
        /// Gets the stack trace data.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <returns></returns>
        private static string GetStackTraceData(Exception ex)
        {
            StringBuilder strTrace = new StringBuilder();
            StackTrace st = new StackTrace(ex, true);
            StackFrame[] frames = st.GetFrames();
            if (frames != null)
            {
                // Iterate over the frames extracting the information you need
                foreach (StackFrame frame in frames)
                {
                    if (frame != null)
                    {
                        strTrace.AppendLine(String.Format("File:{0},Method:{1}(),Line:{2},Col:{3}", frame.GetFileName(), frame.GetMethod().Name, frame.GetFileLineNumber(), frame.GetFileColumnNumber()));
                    }
                }
            }
            return Convert.ToString(strTrace);
        }
        /// <summary>
        /// Writes to log.
        /// </summary>
        /// <param name="LogData">The log data.</param>
        public static void WriteToLog(string LogData)
        {
            try
            {
                if (!System.IO.Directory.Exists(HL7Data.exceptionLogPath))
                {
                    System.IO.Directory.CreateDirectory(HL7Data.exceptionLogPath);
                }

                if (HL7Data.Hl7ExceptionLogFileName == null)
                {
                    HL7Data.Hl7ExceptionLogFileName = HL7Data.exceptionLogPath + "\\" + DateTime.Now.ToString("ddMMMyyyy") + "_" + DateTime.Now.Ticks + ".txt";
                }

                var directory = new DirectoryInfo(HL7Data.Hl7ExceptionLogFileName);

                double filesize = (directory.FullName.Length / 1024f) / 1024f;
                if (filesize >= 2)
                {
                    HL7Data.Hl7ExceptionLogFileName = HL7Data.exceptionLogPath + "\\" + DateTime.Now.ToString("ddMMMyyyy") + "_" + DateTime.Now.Ticks + ".txt";
                }

                lock (HL7Data.Hl7ExceptionLogFileName)
                {
                    StreamWriter objWriter = new StreamWriter(HL7Data.Hl7ExceptionLogFileName, true);
                    lock (objWriter)
                    {
                        objWriter.WriteLine(DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + " - " + LogData);
                        objWriter.WriteLine("------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                        objWriter.Flush();
                        objWriter.Close();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
