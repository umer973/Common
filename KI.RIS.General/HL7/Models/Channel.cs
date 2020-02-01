using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*-----------------------------------------------------------------------
<Copyright file="Hl7Channel.cs" Company="Kameda Infologics">
    Copyright@Kameda Infologics Pvt Ltd. All rights reserved.
</Copyright>

 Description     :Model class for Hl7Channel 
 Created  By     :Biju S J 
 Created  Date   :12-Jan-2018 
 Modified By     :ModifiedBy  
 Modified Date   :ModifiedDate 
 Modified Purpose:ModifiedPur 
 -----------------------------------------------------------------------*/


namespace KI.RIS.General.HL7.Models
{
    [Serializable()]
    public class Channel
    {
        #region "    Variable Declarations  "

        /// <summary>
        /// The identifier
        /// </summary>
        private int _ID = 0;
        /// <summary>
        /// The mode
        /// </summary>
        private int _mode = byte.MinValue;
        /// <summary>
        /// The name
        /// </summary>
        private string _name = string.Empty;
        /// <summary>
        /// The ip address
        /// </summary>
        private string _IPAddress = string.Empty;
        /// <summary>
        /// The port
        /// </summary>
        private int _Port = 0;
        /// <summary>
        /// The ack needed
        /// </summary>
        private int _ackNeeded = byte.MinValue;
        /// <summary>
        /// The time delay
        /// </summary>
        private int _timeDelay = 0;
        /// <summary>
        /// The retry count
        /// </summary>
        private int _retryCount = 0;
        /// <summary>
        /// The ack time out
        /// </summary>
        private int _ackTimeOut = 0;
        /// <summary>
        /// The receiving application
        /// </summary>
        private string _receivingApplication = string.Empty;
        /// <summary>
        /// The receiving facility
        /// </summary>
        private string _receivingFacility = string.Empty;
        /// <summary>
        /// The fs message folder
        /// </summary>
        private string _FSMessageFolder = string.Empty;
        /// <summary>
        /// The fsack folder
        /// </summary>
        private string _FSACKFolder = string.Empty;
        /// <summary>
        /// The fs message interval
        /// </summary>
        private int _FSMessageInterval = 0;
        /// <summary>
        /// The isvalid
        /// </summary>
        private int _ISVALID = 0;
        /// <summary>
        /// The interface type
        /// </summary>
        private string _interfaceType = string.Empty;
        /// <summary>
        /// The is port sending required
        /// </summary>
        private string isPortSendingRequired = string.Empty;
        /// <summary>
        /// The HL7 remoting client path
        /// </summary>
        private string hl7RemotingClientPath = string.Empty;
        /// <summary>
        /// The HL7 remoting listener path
        /// </summary>
        private string hl7RemotingListenerPath = string.Empty;
        /// <summary>
        /// The version
        /// </summary>
        private string version = string.Empty;
        /// <summary>
        /// The service type
        /// </summary>
        private string serviceType = string.Empty;
        /// <summary>
        /// The interface DLL
        /// </summary>
        private string interfaceDll = string.Empty;
        /// <summary>
        /// The interface class
        /// </summary>
        private string interfaceClass = string.Empty;
        /// <summary>
        /// The premessagedelimiter
        /// </summary>
        private string premessagedelimiter = string.Empty;
        /// <summary>
        /// The postmessagedelimiter
        /// </summary>
        private string postmessagedelimiter = string.Empty;
        /// <summary>
        /// The sendingfacilityname
        /// </summary>
        private string sendingfacilityname = string.Empty;
        /// <summary>
        /// The fs ack extn
        /// </summary>
        private string _FsACKExtn = string.Empty;
        /// <summary>
        /// The fs message extn
        /// </summary>
        private string _FsMessageExtn = string.Empty;
        /// <summary>
        /// The HL7 DTLS client channel
        /// </summary>
        private DataTable hl7DtlsClientChannel = null;

        //Private _sequenceNo As Integer

        /// <summary>
        /// The status
        /// </summary>
        private int _status = 0;
        #endregion

        #region "    Properties  "

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        /// <summary>
        /// Gets or sets the mode.
        /// </summary>
        /// <value>The mode.</value>
        public int Mode
        {
            get { return _mode; }
            set { _mode = value; }
        }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        /// <summary>
        /// Gets or sets the host.
        /// </summary>
        /// <value>The host.</value>
        public string Host
        {
            get { return _IPAddress; }
            set { _IPAddress = value; }
        }
        /// <summary>
        /// Gets or sets the port.
        /// </summary>
        /// <value>The port.</value>
        public int Port
        {
            get { return _Port; }
            set { _Port = value; }
        }
        /// <summary>
        /// Gets or sets the ack needed.
        /// </summary>
        /// <value>The ack needed.</value>
        public int ACKNeeded
        {
            get { return _ackNeeded; }
            set { _ackNeeded = value; }
        }
        /// <summary>
        /// Gets or sets the retry count.
        /// </summary>
        /// <value>The retry count.</value>
        public int RetryCount
        {
            get { return _retryCount; }
            set { _retryCount = value; }
        }
        /// <summary>
        /// Gets or sets the time delay.
        /// </summary>
        /// <value>The time delay.</value>
        public int TimeDelay
        {
            get { return _timeDelay; }
            set { _timeDelay = value; }
        }
        /// <summary>
        /// Gets or sets the ack time out.
        /// </summary>
        /// <value>The ack time out.</value>
        public int ACKTimeOut
        {
            get { return _ackTimeOut; }
            set { _ackTimeOut = value; }
        }
        /// <summary>
        /// Gets or sets the receiving application.
        /// </summary>
        /// <value>The receiving application.</value>
        public string ReceivingApplication
        {
            get { return _receivingApplication; }
            set { _receivingApplication = value; }
        }
        /// <summary>
        /// Gets or sets the receiving facility.
        /// </summary>
        /// <value>The receiving facility.</value>
        public string ReceivingFacility
        {
            get { return _receivingFacility; }
            set { _receivingFacility = value; }
        }
        /// <summary>
        /// Gets or sets the fs message folder.
        /// </summary>
        /// <value>The fs message folder.</value>
        public string FSMessageFolder
        {
            get { return _FSMessageFolder; }
            set { _FSMessageFolder = value; }
        }
        /// <summary>
        /// Gets or sets the fsack folder.
        /// </summary>
        /// <value>The fsack folder.</value>
        public string FSACKFolder
        {
            get { return _FSACKFolder; }
            set { _FSACKFolder = value; }
        }
        /// <summary>
        /// Gets or sets the fs message interval.
        /// </summary>
        /// <value>The fs message interval.</value>
        public int FSMessageInterval
        {
            get { return _FSMessageInterval; }
            set { _FSMessageInterval = value; }
        }
        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public int Status
        {
            get { return _status; }
            set { _status = value; }
        }
        /// <summary>
        /// Gets or sets the isvalid.
        /// </summary>
        /// <value>The isvalid.</value>
        public int ISVALID
        {
            get { return _ISVALID; }
            set { _ISVALID = value; }
        }

        /// <summary>
        /// Gets or sets the type of the interface.
        /// </summary>
        /// <value>The type of the interface.</value>
        public string InterfaceType
        {
            get { return _interfaceType; }
            set { _interfaceType = value; }
        }

        /// <summary>
        /// Gets or sets the is port sending required.
        /// </summary>
        /// <value>The is port sending required.</value>
        public string IsPortSendingRequired
        {
            get { return isPortSendingRequired; }
            set { isPortSendingRequired = value; }
        }
        /// <summary>
        /// Gets or sets the h l7 remoting client path.
        /// </summary>
        /// <value>The h l7 remoting client path.</value>
        public string HL7RemotingClientPath
        {
            get { return hl7RemotingClientPath; }
            set { hl7RemotingClientPath = value; }
        }

        /// <summary>
        /// Gets or sets the h l7 remoting listener path.
        /// </summary>
        /// <value>The h l7 remoting listener path.</value>
        public string HL7RemotingListenerPath
        {
            get { return hl7RemotingListenerPath; }
            set { hl7RemotingListenerPath = value; }
        }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>The version.</value>
        public string Version
        {
            get { return version; }
            set { version = value; }
        }

        /// <summary>
        /// Gets or sets the type of the service.
        /// </summary>
        /// <value>The type of the service.</value>
        public string ServiceType
        {
            get { return serviceType; }
            set { serviceType = value; }
        }

        /// <summary>
        /// Gets or sets the interface DLL.
        /// </summary>
        /// <value>The interface DLL.</value>
        public string InterfaceDll
        {
            get { return interfaceDll; }
            set { interfaceDll = value; }
        }

        /// <summary>
        /// Gets or sets the interface class.
        /// </summary>
        /// <value>The interface class.</value>
        public string InterfaceClass
        {
            get { return interfaceClass; }
            set { interfaceClass = value; }
        }
        /// <summary>
        /// Gets or sets the postmessagedelimiter.
        /// </summary>
        /// <value>The postmessagedelimiter.</value>
        public string Postmessagedelimiter
        {
            get { return postmessagedelimiter; }
            set { postmessagedelimiter = value; }
        }
        /// <summary>
        /// Gets or sets the premessagedelimiter.
        /// </summary>
        /// <value>The premessagedelimiter.</value>
        public string Premessagedelimiter
        {
            get { return premessagedelimiter; }
            set { premessagedelimiter = value; }
        }
        /// <summary>
        /// Gets or sets the sendingfacilityname.
        /// </summary>
        /// <value>The sendingfacilityname.</value>
        public string Sendingfacilityname
        {
            get { return sendingfacilityname; }
            set { sendingfacilityname = value; }
        }
        /// <summary>
        /// Gets or sets the fs message extn.
        /// </summary>
        /// <value>The fs message extn.</value>
        public string FsMessageExtn
        {
            get { return _FsMessageExtn; }
            set { _FsMessageExtn = value; }
        }
        /// <summary>
        /// Gets or sets the fs ack extn.
        /// </summary>
        /// <value>The fs ack extn.</value>
        public string FsACKExtn
        {
            get { return _FsACKExtn; }
            set { _FsACKExtn = value; }
        }

        /// <summary>
        /// Gets or sets the h l7 DTLS client channel.
        /// </summary>
        /// <value>The h l7 DTLS client channel.</value>
        public DataTable HL7DtlsClientChannel
        {
            get { return hl7DtlsClientChannel; }
            set { hl7DtlsClientChannel = value; }
        }


        #endregion
    }
}
