using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Data;
using FISCA.Data;
using System.IO;
using FISCA.UDT;
using System.Net;
using System.Xml;

namespace SMSSendingSystem.World.CHT
{
    public class tool
    {
        public enum tag { student, teacher }
        public enum State { 不傳送, 傳送中, 已傳送 };

        /// <summary>
        /// 中華電信回傳狀態字典
        /// </summary>
        static public Dictionary<string, string> MsgCodeDic;

        /// <summary>
        /// Query
        /// </summary>
        static public QueryHelper _q = new QueryHelper();

        /// <summary>
        /// UDT
        /// </summary>
        static public AccessHelper _a = new AccessHelper();

        /// <summary>
        /// 中華簡訊基本設定
        /// </summary>
        static public CHTRecord tr = new CHTRecord();

        /// <summary>
        /// 建立HttpWebRequest
        /// 向網路端 送出 或 取得 資料
        /// </summary>
        /// <param name="url">網路位置與參數組合</param>
        /// <returns>回傳之資料</returns>
        static public string REQ(string url)
        {
            HttpWebRequest req = WebRequest.Create(url) as HttpWebRequest;
            HttpWebResponse rsp = req.GetResponse() as HttpWebResponse;
            //從 Server 回傳的資料。
            Stream data = rsp.GetResponseStream();
            StreamReader sr = new StreamReader(data);
            string resp = sr.ReadToEnd();
            sr.Close();
            data.Close();

            //過濾不必要字元
            resp = resp.Replace("\n", "");
            resp = resp.Replace("<br>", "");

            return resp;
        }

        /// <summary>
        /// 檢查傳入之字串,共幾字元
        /// 範圍為 (a~z A~Z) = (65~122)
        /// </summary>
        static public string CheckTextCount(string _tbMessage)
        {
            _tbMessage = _tbMessage.Replace("\r", "");
            char[] a = _tbMessage.ToArray();
            int countA = 0;
            int countB = 0;
            foreach (char b in a)
            {
                //(a~z A~Z) = (65~122)
                int intValue = Convert.ToInt32(b);
                if (intValue >= 65 && intValue <= 122)
                {
                    countA++;
                }
                else
                {
                    countB++;
                }
            }

            return string.Format("簡訊內容：(英文:{0} 中文與各類符號:{1})", countA, countB);
        }

        /// <summary>
        /// 設定一個Column
        /// </summary>
        static public ColumnHeader SetColumn(string text, int width)
        {
            ColumnHeader column = new ColumnHeader();
            column.Text = text;
            column.Width = width;
            return column;
        }

        /// <summary>
        /// 取得學生資料
        /// </summary>
        static public List<KeyBoStudent> GetStudent(List<string> _IDList)
        {
            StringBuilder sb = new StringBuilder();

            List<KeyBoStudent> Students = new List<KeyBoStudent>();
            sb.Append("select student.id,student.name,student.student_number,student.seat_no,student.ref_class_id,class.class_name,$cht_access_control_card.student_cardno.cell_phone from student ");
            sb.Append("left join class on student.ref_class_id=class.id ");
            sb.Append("left join $cht_access_control_card.student_cardno on student.id=$cht_access_control_card.student_cardno.ref_student_id ");
            sb.Append(string.Format("where student.id in ('{0}') ", string.Join("','", _IDList)));
            sb.Append("order by class.class_name,student.seat_no");

            DataTable dt = tool._q.Select(sb.ToString());

            foreach (DataRow row in dt.Rows)
            {
                KeyBoStudent stud = new KeyBoStudent(row);
                Students.Add(stud);
            }
            return Students;
        }

        /// <summary>
        /// 檢查錯誤狀態的電話號碼
        /// 沒有電話 / 電話不是10碼者 / 電話前2碼不為09之電話
        /// </summary>
        static public bool CheckPhone(string phone)
        {
            //沒有電話
            if (string.IsNullOrEmpty(phone))
            {
                return false;
            }

            //電話不是10碼者
            if (phone.Length != 10)
            {
                return false;
            }

            //電話前2碼不為09之電話
            if (phone.Length > 2)
            {
                if (phone.Substring(0, 2) != "09")
                    return false;
                else
                    return true;
            }

            return true;
        }

        /// <summary>
        /// 將 ListViewItem 文字顏色設定為紅色
        /// </summary>
        static public ListViewItem SetListViewColor(ListViewItem item)
        {
            foreach (ListViewItem.ListViewSubItem subitem in item.SubItems)
            {
                subitem.ForeColor = Color.Red;
            }
            return item;
        }

        /// <summary>
        /// ~學生~
        /// 設定 ListViewItem 的文字內容
        /// </summary>
        static public ListViewItem SetSubItemValue(KeyBoStudent stud)
        {
            ListViewItem item = new ListViewItem("");
            item.SubItems.Add(stud.ClassName);
            item.SubItems.Add(stud.SeatNo);
            item.SubItems.Add(stud.Name);
            item.SubItems.Add(stud.SMS_Phone);
            //Cloud 2014.3.10
            item.Tag = stud;

            stud.ListViewItem = item;

            return item;
        }

        static public XmlElement GetXml(string _xml)
        {
            if (string.IsNullOrEmpty(_xml))
            {
                FISCA.DSAUtil.DSXmlHelper dsx = new FISCA.DSAUtil.DSXmlHelper("smsResp");
                return dsx.BaseElement;
            }
            else
            {
                FISCA.DSAUtil.DSXmlHelper dsx = new FISCA.DSAUtil.DSXmlHelper();
                dsx.Load(_xml);
                return dsx.BaseElement;
            }
        }

        public static void BuildDic()
        {
            MsgCodeDic = new Dictionary<string, string>();
            #region 中華電信回傳字典
            MsgCodeDic = new Dictionary<string, string>();
            MsgCodeDic.Add("-1", "系統或是資料庫故障。");
            MsgCodeDic.Add("0", "訊息已成功發送至接收端。");
            MsgCodeDic.Add("1", "訊息傳送中。");
            MsgCodeDic.Add("2", "系統無法找到您要找的訊息。請檢查你的 toaddr 和messageid 是否正確。");
            MsgCodeDic.Add("3", "訊息無法成功送達手機。");
            MsgCodeDic.Add("4", "系統或是資料庫故障。");
            MsgCodeDic.Add("5", "訊息狀態不明。此筆訊息已被刪除。");
            MsgCodeDic.Add("8", "接收端 SIM 已滿，造成訊息傳送失敗。");
            MsgCodeDic.Add("9", "錯誤的接收端號碼，可能是空號。");
            MsgCodeDic.Add("11", "號碼格式錯誤。");
            MsgCodeDic.Add("12", "收訊手機已設定拒收簡訊。");
            MsgCodeDic.Add("13", "手機錯誤。");
            MsgCodeDic.Add("16", "系統無法執行msisdn<->subno，請稍候再試。");
            MsgCodeDic.Add("17", "系統無法找出對應此 subno之電話號碼，請查明 subno是否正確。");
            MsgCodeDic.Add("18", "請檢查受訊方號碼格式是否正確。");
            MsgCodeDic.Add("21", "請檢查 Message id 格式是否正確。");
            MsgCodeDic.Add("23", "你的登入 IP 未在系統註冊。");
            MsgCodeDic.Add("24", "帳號已停用。");
            MsgCodeDic.Add("31", "訊息尚未傳送到 SMSC 。");
            MsgCodeDic.Add("32", "訊息無法傳送到簡訊中心。");
            MsgCodeDic.Add("33", "訊息無法傳送到簡訊中心(訊務繁忙)。");
            MsgCodeDic.Add("48", "受訊客戶要求拒收加值簡訊，請不要再重送。");
            MsgCodeDic.Add("55", "http (port 8008) 連線不允許使用 GET 方法，請改用 POST 或改為 https(port 4443) 連線。");
            #endregion
        }

        /// <summary>
        /// 取得發送帳號密碼
        /// </summary>
        public static bool GetAccountPassword()
        {
            AccessHelper a = new AccessHelper();
            List<CHTAccount> udt = a.Select<CHTAccount>();

            try
            {
                tr._username = udt[0].Account;
                tr._password = udt[0].Password;
            }
            catch
            {
                tr._username = "";
                tr._password = "";
            }

            if (!string.IsNullOrWhiteSpace(tr._username) && !string.IsNullOrWhiteSpace(tr._password))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
