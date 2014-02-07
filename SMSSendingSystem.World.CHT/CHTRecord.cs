using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMSSendingSystem.World.CHT
{
    public class CHTRecord
    {
        /// <summary>
        /// 中華簡訊 - 使用者名稱
        /// </summary>
        public string _username = "14525";

        /// <summary>
        /// 中華簡訊 - 密碼
        /// </summary>
        public string _password = "14525";

        /// <summary>
        /// 中華簡訊 - 網址
        /// </summary>
        public string _url = "http://imsp.emome.net:8008/imsp/sms/servlet/SubmitSM?account={0}&password={1}&from_addr_type=0&from_addr=&to_addr_type=0&to_addr={2}&msg_expire_time=0&msg_type=0&msg={3}";
        

        /// <summary>
        /// 台灣簡訊 - 取得簡訊狀態網址
        /// </summary>
        //public string _GetStateUrl = "http://api.twsms.com/smsQuery.php?username={0}&password={1}&mobile={2}&msgid={3}";
    }
}
