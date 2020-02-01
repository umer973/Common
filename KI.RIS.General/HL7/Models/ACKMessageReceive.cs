using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KI.RIS.General.HL7.Models
{
    [Serializable()]
    public class ACKMessageReceive
    {

        private long _id = 0;
        private string _MessageControlID = string.Empty;
        private int _sequenceID = 0;
        private string _message = Convert.ToString(int.MinValue);
        private long _Hl7CommonMessageId;

        public long ID
        {
            get { return _id; }
            set { _id = value; }
        }
        public string MessageControlID
        {
            get { return _MessageControlID; }
            set { _MessageControlID = value; }
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
        public long Hl7CommonMessageId
        {
            get { return _Hl7CommonMessageId; }
            set { _Hl7CommonMessageId = value; }
        }



    }
}
