using KI.RIS.Enumerators.HL7;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KI.RIS.General.HL7.Models
{
    public class HL7GeneralMessageParser
    {
        /// <summary>
        /// The segment delimiter
        /// </summary>
        public static string segmentDelimiter = Convert.ToString(Strings.Chr(13));
        /// <summary>
        /// The message delimiter
        /// </summary>
        public static string messageDelimiter = Convert.ToString(Strings.Chr(28));
        /// <summary>
        /// The postmessagedelimiter
        /// </summary>
        public static string postmessagedelimiter = string.Empty;
        /// <summary>
        /// The premessagedelimiter
        /// </summary>
        public static string premessagedelimiter = string.Empty;
        /// <summary>
        /// The message starting character
        /// </summary>
        public static string messageStartingChar = Convert.ToString(Strings.Chr(11));
        /// <summary>
        /// Sets the message control character.
        /// </summary>
        /// <param name="channel">The channel.</param>
        public static void SetMessageControlCharacter(Channel channel)
        {
            try
            {
                if (channel.Postmessagedelimiter != null && channel.Postmessagedelimiter == Convert.ToString((int)(Enumerators.HL7.HL7PostMsgDelimiter.Ox1C_OxOD)))
                {
                    postmessagedelimiter = messageDelimiter + segmentDelimiter;
                }
                else if (channel.Postmessagedelimiter != null && channel.Postmessagedelimiter == Convert.ToString((int)(Enumerators.HL7.HL7PostMsgDelimiter.Ox1C)))
                {
                    postmessagedelimiter = messageDelimiter;
                }
                else
                {
                    postmessagedelimiter = messageDelimiter;
                }

                if (channel.Premessagedelimiter != null && channel.Premessagedelimiter == Convert.ToString((int)(Enumerators.HL7.HL7PreMsgDelimiter.OxOB)))
                {
                    premessagedelimiter = messageStartingChar;
                }
                else
                {
                    premessagedelimiter = messageStartingChar;
                }
            }
            catch (Exception ex)
            {
                ExceptionTransactionLog.WriteToLog("Exception occure on HL7GeneralMessageParser, SetMessageControlCharacter method", ex);
            }


        }
        /// <summary>
        /// Strings the parse and get message control identifier.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        /// <returns>System.String.</returns>
        public static string StringParseAndGetMessageControlID(string msg)
        {
            //Purpose : Return the Message Control ID if Exist Otherwise return Integer.Min value
            string MsgControlID = Convert.ToString(int.MinValue);
            try
            {
                string[] dataBlocks = null;
                string[] sectionBlocks = null;

                dataBlocks = msg.Split('\r');
                foreach (string dataBlock in dataBlocks)
                {
                    if (dataBlock.Trim().Length > 0)
                    {
                        sectionBlocks = dataBlock.Replace('\n', ' ').Trim().Split('|');
                        if (sectionBlocks[0].Trim().Equals("MSH"))
                        {
                            if (sectionBlocks.Length > 9 && sectionBlocks[9].Trim().ToString().Length > 0)
                            {
                                MsgControlID = sectionBlocks[9].Trim().ToString();
                            }
                            break;
                        }
                    }
                }
                return MsgControlID;
            }
            catch (Exception ex)
            {
                ExceptionTransactionLog.WriteToLog("Exception occure on HL7GeneralMessageParser, StringParseAndGetMessageControlID method", ex);
                return MsgControlID;
            }
        }
        /// <summary>
        /// Gets the message array.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>System.String[].</returns>
        public static string[] GetMessageArray(string data)
        {
            //Purpose : Return One Single Message the "DATA" .
            string lineNo = "0\t";
            try
            {
                string[] returnMessage = new string[2];
                returnMessage[0] = string.Empty;
                returnMessage[1] = data;
                lineNo += "1\t";
                if (data.Trim() != string.Empty)
                {
                    string message = string.Empty;
                    if (data.Contains(Convert.ToChar(messageDelimiter)))
                    {
                        if (data.Contains(Convert.ToChar(messageStartingChar)))
                        {
                            lineNo += "2\t";
                            data = data.Substring(data.IndexOf(messageStartingChar), data.Length - data.IndexOf(messageStartingChar));
                        }
                        lineNo += "3\t";
                        if (data.Contains(Convert.ToChar(messageDelimiter)))
                        {
                            message = data.Substring(0, data.IndexOf(messageDelimiter));
                            lineNo += "4\t";
                            if (message.Length > 0)
                            {
                                lineNo += "5\t";
                                if (data.IndexOf(messageDelimiter) + 1 == data.Length)
                                {
                                    lineNo += "6\t";
                                    data = string.Empty;
                                }
                                else
                                {
                                    lineNo += "7\t";
                                    data = data.Substring(data.IndexOf(messageDelimiter) + 1, data.Length - (data.IndexOf(messageDelimiter) + 1));
                                    lineNo += "8\t";
                                    if (data.Contains(Convert.ToChar(messageStartingChar)))
                                    {
                                        lineNo += "9\t";
                                        data = data.Substring(data.IndexOf(messageStartingChar), data.Length - data.IndexOf(messageStartingChar));
                                    }
                                    else
                                    {
                                        lineNo += "10\t";
                                        data = string.Empty;
                                    }
                                }
                                lineNo += "11\t";
                                message = message + messageDelimiter;
                                lineNo += "12\t";
                                returnMessage[0] = message;
                                lineNo += "13\t";
                                returnMessage[1] = data;
                            }
                        }
                        else
                        {
                            returnMessage[1] = data;
                        }
                    }
                }
                lineNo += "15\t";
                return returnMessage;
            }
            catch (Exception ex)
            {
                ExceptionTransactionLog.WriteToLog("Exception occure on GetMessageArray Tarace : " + lineNo + " Actual Exception: " + ex.Message);
                throw ex;
            }
        }
        /// <summary>
        /// Checks the validity of message.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool CheckValidityOfMessage(string msg)
        {
            bool functionReturnValue = false;
            try
            {
                functionReturnValue = true;
                if (!msg.Contains(messageDelimiter))
                {
                    return false;
                }

                char[] arry = messageStartingChar.ToCharArray();
                msg = msg.Trim(arry);
                char[] arry1 = segmentDelimiter.ToCharArray();
                msg = msg.Trim(arry1);
                char[] arry2 = messageDelimiter.ToCharArray();
                msg = msg.Trim(arry2);
                return isValidHL7Msg(msg.Trim(Strings.Chr(28)));
            }
            catch (Exception ex)
            {
                ExceptionTransactionLog.WriteToLog(ex);
                return false;
            }
        }
        /// <summary>
        /// Determines whether [is valid h l7 MSG] [the specified string MSG].
        /// </summary>
        /// <param name="strMsg">The string MSG.</param>
        /// <returns><c>true</c> if [is valid h l7 MSG] [the specified string MSG]; otherwise, <c>false</c>.</returns>
        public static bool isValidHL7Msg(string strMsg)
        {
            bool functionReturnValue = false;
            //Purpose : Validate the HL7 Message.
            try
            {
                functionReturnValue = true;
                if (strMsg.Length > 0)
                {
                    // First Case
                    if (strMsg.Length > 3 && Strings.StrComp(strMsg.Substring(0, 3).Trim(), "MSH", CompareMethod.Binary) == 0)
                    {
                        if (strMsg.Length > 4 && !string.IsNullOrEmpty(strMsg.Substring(3, 1)) && IsSpecialCharacter(strMsg.Substring(3, 1)))
                        {
                            if (!strMsg.Contains(Convert.ToString(Strings.Chr(13))))
                            {
                                return false;
                            }
                            string[] ar = strMsg.Substring(0, strMsg.IndexOf(Strings.Chr(13))).Split(strMsg.Substring(3, 1).ToCharArray());
                            //      string[] ar = Strings.Split(strMsg.Substring(0, strMsg.IndexOf(Strings.Chr(13))), strMsg.Substring(3, 1));
                            if (ar.Length >= 11)
                            {
                                //Message Type, Message Control ID, Processing ID and Versions are Not Null
                                if (!string.IsNullOrEmpty(ar[8].Trim()) && !string.IsNullOrEmpty(ar[9].Trim()) && !string.IsNullOrEmpty(ar[10].Trim()) && !string.IsNullOrEmpty(ar[11].Trim()))
                                {
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }

            }
            catch (Exception ex)
            {
                ExceptionTransactionLog.WriteToLog(ex);
                return false;
            }
            return functionReturnValue;
        }
        /// <summary>
        /// Determines whether [is special character] [the specified string character].
        /// </summary>
        /// <param name="strCharacter">The string character.</param>
        /// <returns><c>true</c> if [is special character] [the specified string character]; otherwise, <c>false</c>.</returns>
        public static bool IsSpecialCharacter(string strCharacter)
        {
            //Purpose : Validate the asc of the Character is not between 0-9 ,a-z and A-Z
            const string CHARACTERPATTERN = "[0-9a-zA-Z.\\s\\b]";
            if (System.Text.RegularExpressions.Regex.IsMatch(strCharacter, CHARACTERPATTERN, System.Text.RegularExpressions.RegexOptions.IgnoreCase))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Gets the manual parsing value.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="channel">The channel.</param>
        /// <returns>System.Int32.</returns>
        public static int GetManualParsingValue(string message, Channel channel)
        {
            try
            {
                int returnValue = 0;
                HL7Message? messageType = GetMessageType(message);
                if (messageType != null)
                {
                    var clientChanelFilter = from clintChannel in channel.HL7DtlsClientChannel.AsEnumerable()
                                             where Convert.ToString(clintChannel["MESSAGE_TYPE"]).Trim() == ((int)messageType).ToString()
                                             select clintChannel;
                    //returnValue = clientChanelFilter.Count() > 0 ? clientChanelFilter.CopyToDataTable().Rows[0]["PARSER_TYPE"] : DBNull.Value;
                    DataTable tempDt = clientChanelFilter.Count() > 0 ? clientChanelFilter.CopyToDataTable() : null;
                    if (tempDt != null && tempDt.Rows.Count > 0 && tempDt.Rows[0]["PARSER_TYPE"] != DBNull.Value)
                        int.TryParse(tempDt.Rows[0]["PARSER_TYPE"].ToString(), out returnValue);
                }
                return returnValue;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Gets the type of the message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>System.Nullable&lt;HL7Message&gt;.</returns>
        public static HL7Message? GetMessageType(string message)
        {
            try
            {
                HL7Message? enumMessageType = null;
                string[] dataBlocks = null;
                string[] sectionBlocks = null;
                message = message.Replace("\n", "");
                dataBlocks = message.Split(System.Convert.ToChar(13));
                foreach (string str in dataBlocks)
                {
                    sectionBlocks = str.Split('|');
                    if (sectionBlocks.Count() > 8 && sectionBlocks[0].Trim().Contains("MSH"))
                    {
                        string tempVal = sectionBlocks[8].Contains("^") ? sectionBlocks[8].Split('^')[0] + "_" + sectionBlocks[8].Split('^')[1] : sectionBlocks[8];
                        switch (tempVal)
                        {
                            case "ADT_A01":
                                enumMessageType = HL7Message.ADT_A01;
                                break;
                            case "ADT_A08":
                                enumMessageType = HL7Message.ADT_A08;
                                break;
                            case "ADT_A04":
                                enumMessageType = HL7Message.ADT_A04;
                                break;
                            case "ORM_O01":
                                enumMessageType = HL7Message.ORM_O01;
                                break;
                            case "ORU_R01":
                                enumMessageType = HL7Message.ORU_R01;
                                break;
                            case "ADT_A02":
                                enumMessageType = HL7Message.ADT_A02;
                                break;
                            case "ADT_A03":
                                enumMessageType = HL7Message.ADT_A03;
                                break;
                            case "ADT_A11":
                                enumMessageType = HL7Message.ADT_A11;
                                break;
                            case "ADT_A12":
                                enumMessageType = HL7Message.ADT_A12;
                                break;
                            case "ADT_A13":
                                enumMessageType = HL7Message.ADT_A13;
                                break;
                          
                        }
                        break;
                    }
                }
                return enumMessageType;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Strings the parse and check message is ack.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool StringParseAndCheckMessageIsAck(string msg)
        {
            try
            {
                //Purpose : Check the Message is Acknowledgment Or Not
                string[] dataBlocks = null;
                string[] sectionBlocks = null;
                bool isAck = false;

                dataBlocks = msg.Split('\r');
                foreach (string str in dataBlocks)
                {
                    sectionBlocks = str.Split('|');

                    if (sectionBlocks.Length > 8 && sectionBlocks[0].Trim().Contains("MSH") && sectionBlocks[8].Trim().Contains("ACK"))
                    {
                        isAck = true;
                        break;
                    }
                }
                return isAck;

            }

            catch (Exception ex)
            {
                ExceptionTransactionLog.WriteToLog(ex);
                return false;
            }
        }
        /// <summary>
        /// Strings the parse and get ack message control identifier.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        /// <returns>System.String.</returns>
        public static string StringParseAndGetACKMessageControlID(string msg)
        {
            //Purpose : Return the Message Control ID if Exist Otherwise return Integer.Min value
            string MsgControlID = Convert.ToString(int.MinValue);
            try
            {
                string[] dataBlocks = null;
                string[] sectionBlocks = null;

                dataBlocks = msg.Split('\r');
                foreach (string dataBlock in dataBlocks)
                {
                    if (dataBlock.Trim().Length > 0)
                    {
                        sectionBlocks = dataBlock.Replace('\n', ' ').Trim().Split('|');
                        if (sectionBlocks[0].Trim().Equals("MSA"))
                        {
                            if (sectionBlocks.Length > 2 && sectionBlocks[2].Trim().ToString().Length > 0)
                            {
                                MsgControlID = sectionBlocks[2].Trim().ToString();
                            }
                            break;
                        }
                    }
                }
                return MsgControlID;
            }
            catch (Exception ex)
            {
                ExceptionTransactionLog.WriteToLog(ex);
                return MsgControlID;
            }
        }

        /// <summary>
        /// Strings the parse and check message is positive ack.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool StringParseAndCheckMessageIsPositiveAck(string msg)
        {
            try
            {
                //Purpose : Check the Message is Acknowledgment Or Not
                string[] dataBlocks = null;
                string[] sectionBlocks = null;
                bool isPositiveAck = false;

                dataBlocks = msg.Split('\r');
                foreach (string dataBlock in dataBlocks)
                {
                    if (dataBlock.Trim().Length > 0)
                    {
                        sectionBlocks = dataBlock.Replace('\n', ' ').Trim().Split('|');
                        if (sectionBlocks.Length > 1 && sectionBlocks[1].Trim().Equals("AA"))
                        {
                            isPositiveAck = true;
                            break;
                        }
                    }
                }
                return isPositiveAck;
            }

            catch (Exception ex)
            {
                ExceptionTransactionLog.WriteToLog(ex);
                return false;
            }
        }
        /// <summary>
        /// Strings the parse and get message sequence identifier.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        /// <returns>System.String.</returns>
        public static string StringParseAndGetMessageSequenceID(string msg)
        {
            //Purpose : Return the Message Sequence ID if Exist Otherwise return Integer.Min value
            string MsgSequenceID = Convert.ToString(int.MinValue);
            try
            {
                string[] dataBlocks = null;
                string[] sectionBlocks = null;

                dataBlocks = msg.Split('\r');
                foreach (string dataBlock in dataBlocks)
                {
                    if (dataBlock.Trim().Length > 0)
                    {
                        sectionBlocks = dataBlock.Replace('\n', ' ').Trim().Split('|');
                        if (sectionBlocks[0].Trim().Equals("MSH"))
                        {
                            if (sectionBlocks.Length > 12 && sectionBlocks[12].Trim().ToString().Length > 0)
                            {
                                MsgSequenceID = sectionBlocks[12].Trim().ToString();
                            }
                            break;
                        }
                    }
                }
                return MsgSequenceID;
            }
            catch (Exception ex)
            {
                ExceptionTransactionLog.WriteToLog(ex);
                return MsgSequenceID;
            }
        }


    }
}
