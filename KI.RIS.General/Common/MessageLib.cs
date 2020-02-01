
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KI.RIS.General.Common
{
    public static class MessageLib
    {
        public static string Save { get; set; } //= "CmnMsgSave";// Saved Successfully";
        public static string Update { get; set; }//= "CmnMsgUpdate";//"Updated Successfully";
        public static string Delete { get; set; } //= "CmnMsgDelete";// "Deleted Successfully";
        public static string Error { get; set; }
        private static DataTable CustomMessages { get; set; }

        static MessageLib()
        {
            var OrderLookup = GlobalCache.GlobalCaching.GenLookup.AsEnumerable().Where(r =>
              Convert.ToString(r["LookupType"]).Equals("MultilingualCustomMessage"));
            if (OrderLookup.Any())
            {
                CustomMessages = OrderLookup.CopyToDataTable();
            }

            Save = GetMultilingualMessage("¥CmnMsgSave¥");
            Update = GetMultilingualMessage("¥CmnMsgUpdate¥");
            Delete = GetMultilingualMessage("¥CmnMsgDelete¥");
            Error = GetMultilingualMessage("¥CmnMsgError¥");
        }

        public static string GetMultilingualMessage(string message)
        {
            if (message.Contains("¥"))
            {
                MatchCollection allMatchResults = null;
                var regexObj = new Regex(@"¥\w*¥");
                allMatchResults = regexObj.Matches(message);
                foreach (var item in allMatchResults)
                {
                    var result = CustomMessages.AsEnumerable().Where(dr => dr["LookupValue"].ToString() == item.ToString());
                    if (result.Count() > 0)
                    {
                        if (KI.RIS.GlobalCache.GlobalCaching.MulitlingualCode.ToString().ToUpper().Equals("EN"))
                        {
                            message = message.Replace(item.ToString(), result.ElementAt(0)["Field1"].ToString());
                        }
                        else if (KI.RIS.GlobalCache.GlobalCaching.MulitlingualCode.ToString().ToUpper().Equals("FR"))
                        {
                            message = message.Replace(item.ToString(), result.ElementAt(0)["Field2"].ToString());
                        }
                    }
                }
            }
            return message;
        }
    }
}
