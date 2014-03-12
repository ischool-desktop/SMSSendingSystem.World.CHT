using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.UDT;

namespace SMSSendingSystem.World.CHT
{
    [FISCA.UDT.TableName("cht_access_control_card.account")]
    public class CHTAccount : FISCA.UDT.ActiveRecord
    {
        /// <summary>
        /// 帳號
        /// </summary>
        [FISCA.UDT.Field(Field = "account")]
        public string Account { get; set; }

        /// <summary>
        /// 密碼
        /// </summary>
        [FISCA.UDT.Field(Field = "password")]
        public string Password { get; set; }
    }
}