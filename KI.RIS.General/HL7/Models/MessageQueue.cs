using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KI.RIS.General.HL7.Models
{
    [Serializable()]
    public class MessageQueue
    {

        #region "    Variable Declarations  "

        // Private _ID As Integer = 0
        private string _Message = string.Empty;
        private int _ChannelID = 0;
        private byte _Status = byte.MinValue;
        private System.DateTime? _SendTime = System.DateTime.MinValue;
        private int _ResendCount = 0;
        private string _MessageControlID = string.Empty;
        private int _SequenceID = 0;
        //Private _ackNeeded As Byte
        private int _MessageSentID = 0;
        private byte _ACKReceived = byte.MinValue;
        private bool _isACK = false;
        private byte _channelStatus = byte.MinValue;

        private int _sessionID = 0;
        #endregion

        #region "    Properties  "

        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }
        public byte Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        public System.DateTime? SendTime
        {
            get { return _SendTime; }
            set { _SendTime = value; }
        }
        public int ResendCount
        {
            get { return _ResendCount; }
            set { _ResendCount = value; }
        }
        public int ChannelID
        {
            get { return _ChannelID; }
            set { _ChannelID = value; }
        }
        public string MessageControlID
        {
            get { return _MessageControlID; }
            set { _MessageControlID = value; }
        }
        public int SequenceID
        {
            get { return _SequenceID; }
            set { _SequenceID = value; }
        }

        public int MessageSentID
        {
            get { return _MessageSentID; }
            set { _MessageSentID = value; }
        }
        public byte ACKReceived
        {
            get { return _ACKReceived; }
            set { _ACKReceived = value; }
        }
        public bool IsACK
        {
            get { return _isACK; }
            set { _isACK = value; }
        }
        public byte ChannelStatus
        {
            get { return _channelStatus; }
            set { _channelStatus = value; }
        }
        public int SessionID
        {
            get { return _sessionID; }
            set { _sessionID = value; }
        }
        private long hl7CommonMessageID;
        public long Hl7CommonMessageID
        {
            get { return hl7CommonMessageID; }
            set { hl7CommonMessageID = value; }

        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is marked for remove.
        /// </summary>
        /// <value><c>true</c> if this instance is marked for remove; otherwise, <c>false</c>.</value>
        private bool _isMarkedForRemove;
        public bool IsMarkedForRemove
        {
            get { return _isMarkedForRemove; }
            set { _isMarkedForRemove = value; }
        }
        #endregion

    }
}
