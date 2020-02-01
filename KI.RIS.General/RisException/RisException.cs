using KI.RIS.Enumerators.Common;
using KI.RIS.General.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KI.RIS.General.RisException
{
    public class RisException : Exception
    {
        public RisException()
        {
            MessageType = ValidationMessageType.Info;
        }
        public ValidationMessageType MessageType { get; set; }

        public RisException(string message) : base(MessageLib.GetMultilingualMessage(message)) { MessageType = ValidationMessageType.Info; }
        public RisException(ValidationMessageType messageType, string message) : base(MessageLib.GetMultilingualMessage(message))
        {
            MessageType = messageType;
        }
        //public ProcessException(string message, Exception innerException):base(message,innerException)
        //{

        //}
    }
}