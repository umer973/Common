using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KI.RIS.General.HL7.Models
{
    [Serializable()]
    public class MessageSent
    {
        private int _id = 0;
        private string _MessageControlID = string.Empty;
        private int _channelID = 0;
        private int _sequenceID = 0;
        private string _message = string.Empty;
        private System.DateTime? _sendTime = System.DateTime.MinValue;
        private int _resentCount = 0;
        private byte _ACKReceived = byte.MinValue;

        private int _sessionID = 0;
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }
        public string MessageControlID
        {
            get { return _MessageControlID; }
            set { _MessageControlID = value; }
        }
        public int ChannelID
        {
            get { return _channelID; }
            set { _channelID = value; }
        }
        public int SequenceID
        {
            get { return _sequenceID; }
            set { _sequenceID = value; }
        }
        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }
        public System.DateTime? SendTime
        {
            get { return _sendTime; }
            set { _sendTime = value; }
        }
        public int ResendCount
        {
            get { return _resentCount; }
            set { _resentCount = value; }
        }
        public byte ACKReceived
        {
            get { return _ACKReceived; }
            set { _ACKReceived = value; }
        }
        public int SessionID
        {
            get { return _sessionID; }
            set { _sessionID = value; }
        }
    }
}
