using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KI.RIS.General.HL7.Models
{
    [Serializable()]
    public class ACKMessageSent
    {

        private int _id = 0;
        private string _MessageControlID = string.Empty;
        private int _messagesReceiveID = 0;
        private long _sequenceID = 0;
        private string _message = string.Empty;

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
        public int MessageReceiveID
        {
            get { return _messagesReceiveID; }
            set { _messagesReceiveID = value; }
        }
        public long SequenceID
        {
            get { return _sequenceID; }
            set { _sequenceID = value; }
        }
        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }
        public int SessionID
        {
            get { return _sessionID; }
            set { _sessionID = value; }
        }

    }
}
