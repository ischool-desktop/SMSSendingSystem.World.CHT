using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMSSendingSystem.World.CHT
{
    internal class HttpAction
    {
        /// <summary>
        /// 中華簡訊基本設定
        /// </summary>
        private CHTRecord tr { get; set; }

        /// <summary>
        /// 接收電話
        /// </summary>
        public string _mobile = "";

        /// <summary>
        /// 訊息
        /// </summary>
        public string _message = "";

        /// <summary>
        /// 學生物件
        /// </summary>
        public KeyBoStudent student { get; set; }

        /// <summary>
        /// 起始建立 每個訊息實體
        /// </summary>
        public HttpAction()
        {
            tr = new CHTRecord();
        }

        /// <summary>
        /// 設定所要傳送的 電話 與 訊息內容
        /// </summary>
        /// <param name="mobile">學生電話</param>
        /// <param name="message">訊息內容</param>
        public void GetStudentMessage(string mobile, string message)
        {
            _mobile = mobile;
            _message = message;
        }
    }
}
