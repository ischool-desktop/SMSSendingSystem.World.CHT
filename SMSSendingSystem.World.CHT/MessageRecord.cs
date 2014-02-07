using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.UDT;
using System.Xml;

namespace SMSSendingSystem.World.CHT
{
    [TableName("cht_access_control_card.history")]
    class MessageRecord : FISCA.UDT.ActiveRecord
    {
        /// <summary>
        /// 系統編號
        /// </summary>
        [Field(Field = "ref_student_id", Indexed = true)]
        public int Ref_ID { get; set; }

        /// <summary>
        /// 發送之電話號碼
        /// </summary>
        [Field(Field = "cell_phone", Indexed = true)]
        public string Phone { get; set; }

        /// <summary>
        /// 簡訊內容
        /// </summary>
        [Field(Field = "send_message", Indexed = false)]
        public string Content { get; set; }

        /// <summary>
        /// 使用者電腦之簡訊發送時間
        /// </summary>
        [Field(Field = "send_date", Indexed = false)]
        public DateTime ComputerSendTime { get; set; }

        #region 中華簡訊

        /// <summary>
        /// 中華簡訊回傳ID
        /// </summary>
        [Field(Field = "cht_msg_id", Indexed = false)]
        public string ResponseMsgid { get; set; }

        /// <summary>
        /// 中華簡訊回傳狀態
        /// </summary>
        [Field(Field = "cht_status", Indexed = false)]
        public string ResponseStatus { get; set; }

        /// <summary>
        /// 中華簡訊回傳狀態訊息
        /// </summary>
        [Field(Field = "cht_message", Indexed = false)]
        public string ResponseMessage { get; set; }

        /// <summary>
        /// 中華簡訊狀態檢查日期
        /// </summary>
        [Field(Field = "cht_chk_date", Indexed = false)]
        public DateTime CheckDate { get; set; }

        #endregion
    }
}
