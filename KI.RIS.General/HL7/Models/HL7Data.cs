using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KI.RIS.General.HL7.Models
{
    public class HL7Data
    {
        /// <summary>
        /// The segment delimiter
        /// </summary>
        public static string segmentDelimiter = Convert.ToString(Strings.Chr(13));
        /// <summary>
        /// Gets or sets a value indicating whether this instance is service stopped.
        /// </summary>
        /// <value><c>true</c> if this instance is service stopped; otherwise, <c>false</c>.</value>
        public static bool IsServiceStopped { get; set; }

        /// <summary>
        /// The dt data
        /// </summary>
        private static DataTable dtData = null;

        /// <summary>
        /// The HL7 exception log file name
        /// </summary>
        public static string Hl7ExceptionLogFileName = null;

        /// <summary>
        /// The client chnnael identifier
        /// </summary>
        public static long clientChnnaelId = 0;

        /// <summary>
        /// The exception log path
        /// </summary>
        public static string exceptionLogPath = string.Empty;

        /// <summary>
        /// The h l7 message queue items count
        /// </summary>
        public static int hL7MessageQueueItemsCount = 10;
        /// <summary>
        /// The HL7 channel master data
        /// </summary>
        public static Channel hl7ChannelMasterData = null;
        /// <summary>
        /// The client restart time gap in secs
        /// </summary>
        public static int clientRestartTimeGapInSecs = 10;
        /// <summary>
        /// The time between message fetch in sec
        /// </summary>
        public static int timeBetweenMessageFetchInSec = 10;
        /// <summary>
        /// The time between read non processed message in minimum
        /// </summary>
        public static int timeBetweenReadNonProcessedMessageInMin = 120;
        /// <summary>
        /// The is string parsing enabled
        /// </summary>
        public static bool isStringParsingEnabled = true;
        /// <summary>
        /// The is processed marking after ack
        /// </summary>
        public static bool isProcessedMarkingAfterAck = false;
        /// <summary>
        /// The is processed marking after ack
        /// </summary>
        public static bool isSocketReconnectOnEachMsg = false;
        /// <summary>
        /// The is SocketReconnectOnEachMsgTimeGapInMilliSec
        /// </summary>
        public static int SocketReconnectOnEachMsgTimeGapInMilliSec = 100;
        /// <summary>
        /// for testing with file
        /// </summary>
        public static bool isTestingWithFile = false;
        /// <summary>
        /// To specify the test File fath for testing
        /// </summary>
        public static string testFilePath = string.Empty;

        public static string employeeId = string.Empty;

        private static DataTable dtGenApplicationSettings = null;

        /// <summary>
        /// Sets the h l7 master data.
        /// </summary>
        /// <param name="dtMasterChannel">The dt master channel.</param>
        /// <param name="dtDtlsChannel">The dt DTLS channel.</param>
        public static void SetHL7MasterData(DataTable dtMasterChannel, DataTable dtDtlsChannel)
        {
            try
            {
                hl7ChannelMasterData = new Channel();
                DataRow drMaster = dtMasterChannel.Rows[0];
                if (drMaster != null && Convert.ToInt32(drMaster["ChannelMode"]) == 0)
                {
                    hl7ChannelMasterData.Host = drMaster["Host"] != DBNull.Value ? Convert.ToString(drMaster["Host"]) : string.Empty;
                    hl7ChannelMasterData.ACKNeeded = drMaster["AckNeeded"] != DBNull.Value ? Convert.ToInt32(drMaster["AckNeeded"]) : 0;
                    hl7ChannelMasterData.Port = drMaster["Port"] != DBNull.Value ? Convert.ToInt32(drMaster["Port"]) : 0;
                    hl7ChannelMasterData.RetryCount = drMaster["MessageRetryCount"] != DBNull.Value ? Convert.ToInt32(drMaster["MessageRetryCount"]) : 0;
                    hl7ChannelMasterData.TimeDelay = drMaster["MessageInterval"] != DBNull.Value ? Convert.ToInt32(drMaster["MessageInterval"]) : 0;
                    hl7ChannelMasterData.ACKTimeOut = drMaster["AckTimeOut"] != DBNull.Value ? Convert.ToInt32(drMaster["AckTimeOut"]) : 0;
                    hl7ChannelMasterData.ReceivingApplication = drMaster["ReceivingApplication"] != DBNull.Value ? Convert.ToString(drMaster["ReceivingApplication"]) : string.Empty;
                    hl7ChannelMasterData.ReceivingFacility = drMaster["ReceivingFacility"] != DBNull.Value ? Convert.ToString(drMaster["ReceivingFacility"]) : string.Empty;
                    //hl7ChannelMasterData.IsPortSendingRequired = drMaster["IS_PORT_SENDING_REQUIRED"] != DBNull.Value ? Convert.ToString(drMaster["IS_PORT_SENDING_REQUIRED"]) : string.Empty;
                    hl7ChannelMasterData.HL7RemotingClientPath = drMaster["RemotingClient"] != DBNull.Value ? Convert.ToString(drMaster["RemotingClient"]) : string.Empty;
                    hl7ChannelMasterData.HL7RemotingListenerPath = drMaster["RemotingListener"] != DBNull.Value ? Convert.ToString(drMaster["RemotingListener"]) : string.Empty;
                }
                else if (drMaster != null && Convert.ToInt32(drMaster["ChannelMode"]) == 1)
                {
                    hl7ChannelMasterData.FSACKFolder = drMaster["FsAckPath"] != DBNull.Value ? Convert.ToString(drMaster["FsAckPath"]) : string.Empty;
                    hl7ChannelMasterData.FSMessageFolder = drMaster["FsMessagePath"] != DBNull.Value ? Convert.ToString(drMaster["FsMessagePath"]) : string.Empty;
                    hl7ChannelMasterData.ReceivingApplication = drMaster["ReceivingApplication"] != DBNull.Value ? Convert.ToString(drMaster["ReceivingApplication"]) : string.Empty;
                    hl7ChannelMasterData.ReceivingFacility = drMaster["ReceivingFacility"] != DBNull.Value ? Convert.ToString(drMaster["ReceivingFacility"]) : string.Empty;
                    hl7ChannelMasterData.FSMessageInterval = drMaster["MessageInterval"] != DBNull.Value ? Convert.ToInt32(drMaster["MessageInterval"]) : 0;
                }
                hl7ChannelMasterData.ID = drMaster["Hl7ChannelId"] != DBNull.Value ? Convert.ToInt32(drMaster["Hl7ChannelId"]) : 0;
                hl7ChannelMasterData.Mode = drMaster["ChannelMode"] != DBNull.Value ? Convert.ToInt32(drMaster["ChannelMode"]) : 0;
                hl7ChannelMasterData.Name = drMaster["Name"] != DBNull.Value ? Convert.ToString(drMaster["Name"]) : string.Empty;
                hl7ChannelMasterData.ISVALID = drMaster["IsValid"] != DBNull.Value ? Convert.ToInt32(drMaster["IsValid"]) : 0;
                hl7ChannelMasterData.InterfaceType = drMaster["InterfaceType"] != DBNull.Value ? Convert.ToString(drMaster["InterfaceType"]) : string.Empty;
                hl7ChannelMasterData.InterfaceDll = drMaster["InterfaceDll"] != DBNull.Value ? Convert.ToString(drMaster["InterfaceDll"]) : string.Empty;
                hl7ChannelMasterData.InterfaceClass = drMaster["InterfaceClass"] != DBNull.Value ? Convert.ToString(drMaster["InterfaceClass"]) : string.Empty;
                hl7ChannelMasterData.Postmessagedelimiter = drMaster["PostMessageDelimiter"] != DBNull.Value ? Convert.ToString(drMaster["PostMessageDelimiter"]) : string.Empty;
                hl7ChannelMasterData.Premessagedelimiter = drMaster["PreMessageDelimiter"] != DBNull.Value ? Convert.ToString(drMaster["PreMessageDelimiter"]) : string.Empty;
                hl7ChannelMasterData.Sendingfacilityname = drMaster["SendingFacility"] != DBNull.Value ? Convert.ToString(drMaster["SendingFacility"]) : string.Empty;
               
                hl7ChannelMasterData.HL7DtlsClientChannel = dtDtlsChannel;
                hl7ChannelMasterData.Status = Convert.ToInt32(Enumerator.ChannelStatus.Started);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
