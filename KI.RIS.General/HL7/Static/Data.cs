using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*-----------------------------------------------------------------------
<Copyright file="Hl7Data.cs" Company="Kameda Infologics">
    Copyright@Kameda Infologics Pvt Ltd. All rights reserved.
</Copyright>

 Description     :Hl7 Static Data 
 Created  By     :Biju S J 
 Created  Date   :12-Jan-2018 
 Modified By     :ModifiedBy  
 Modified Date   :ModifiedDate 
 Modified Purpose:ModifiedPur 
 -----------------------------------------------------------------------*/


namespace KI.RIS.General.HL7.Static
{
    public static class Data
    {
        public static string Hl7ExceptionLogFileName = null;

        /// <summary>
        /// get the Medication Shared Data
        /// </summary>
        public static DataTable dtGenApplicationSetting = null;
        public static bool IsServiceStopped { get; set; }
        public static DataTable dtGenLookup = null;
        public static DataTable dtHospital = null;
        public static string strReportHeader = string.Empty;
        public static object objServiceThreadLocked = new object();
        public static DataTable dtGenUser = null;
        public static DataTable dtGenUserConfig = null;
        public static DateTime daResetDate = DateTime.MinValue;
    }
}
