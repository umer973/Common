using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KI.RIS.General.HL7.Models
{
    [Serializable()]
    public class MessageReceive
    {

        private int _id = 0;
        private string _MessageControlID = string.Empty;
        private int _channelID = 0;
        private string _sequenceID = string.Empty;
        private string _message = string.Empty;
        private System.DateTime _receiveTime = System.DateTime.MinValue;
        private int _ACKSent = Convert.ToInt32(byte.MinValue);
        private int _sessionID = 0;
        private int _receivedPort = 0;

        private string _receivedIPAddress = string.Empty;
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
        public string SequenceID
        {
            get { return _sequenceID; }
            set { _sequenceID = value; }
        }
        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }
        public System.DateTime ReceiveTime
        {
            get { return _receiveTime; }
            set { _receiveTime = value; }
        }
        public int ACKSent
        {
            get { return _ACKSent; }
            set { _ACKSent = value; }
        }
        public int ReceivedPort
        {
            get { return _receivedPort; }
            set { _receivedPort = value; }
        }
        public int SessionID
        {
            get { return _sessionID; }
            set { _sessionID = value; }
        }
        public string ReceivedIPAddress
        {
            get { return _receivedIPAddress; }
            set { _receivedIPAddress = value; }
        }

    }
}
