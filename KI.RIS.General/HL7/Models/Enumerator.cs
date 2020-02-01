using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KI.RIS.General.HL7.Models
{
    [Serializable()]
    public class Enumerator
    {
        public enum ChannelStatus
        {
            Started = 0,
            Disconnected = 1
        }

        public enum ChannelMode
        {
            // Purpose :  This Enum Used to known the Chanel Mode.
            LLP_Client = 0,
            FS_Client = 1,
            LLP_Listener = 2,
            FS_Listener = 3
        }

        public enum ACKNeededClient
        {
            Yes = 0,
            No = 1,
        }

        public enum ACKNeededListener
        {
            No = 3,
            Immediate = 4,
            Deferred = 5
        }

        public enum ACKReceiveStatus
        {
            Not_Received = 0,
            Received = 1,
            Cancelled = 2,
            Not_Connected = 3,
            Pass_To_Next_Session = 4
        }

        public enum MessageQueueStatus
        {
            //Purpose : To know the Queue Message status.
            Not_Send = 0,
            Waiting_ACK = 1
        }

        public enum SessionEndMode
        {
            Abnormal = 0,
            Normal = 1
        }

        public enum ACKSendStatus
        {
            //Purpose : Acknowledgement Sending Status
            Not_Sent = 0,
            Send = 1
        }

        public enum MessageType
        {
            NormalMessage = 0,
            ACKMessage = 1
        }

    }
}
