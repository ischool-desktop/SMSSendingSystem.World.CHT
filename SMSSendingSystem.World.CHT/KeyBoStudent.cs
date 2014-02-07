using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;

namespace SMSSendingSystem.World.CHT
{
    public class KeyBoStudent
    {
        public KeyBoStudent(DataRow row)
        {
            ref_student_id = "" + row["id"];
            Name = "" + row["name"];
            StudentNumber = "" + row["student_number"];
            SeatNo = "" + row["seat_no"];

            ClassID = "" + row["ref_class_id"];
            ClassName = "" + row["class_name"];

            SMS_Phone = "" + row["sms_phone"];
        }

        public ListViewItem ListViewItem { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 學生系統編號
        /// </summary>
        public string ref_student_id { get; set; }

        /// <summary>
        /// 學號
        /// </summary>
        public string StudentNumber { get; set; }

        /// <summary>
        /// 座號
        /// </summary>
        public string SeatNo { get; set; }

        /// <summary>
        /// 班級系統編號
        /// </summary>
        public string ClassID { get; set; }

        /// <summary>
        /// 班級名稱
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// 電話
        /// </summary>
        public string SMS_Phone { get; set; }

        /// <summary>
        /// 狀態
        /// </summary>
        public tool.State State
        {
            set
            {
                switch (value)
                {
                    case tool.State.已傳送:
                        ListViewItem.SubItems[0].Text = "已傳送";
                        break;
                    case tool.State.不傳送:
                        ListViewItem.SubItems[0].Text = "不傳送";
                        break;
                    case tool.State.傳送中:
                        ListViewItem.SubItems[0].Text = "傳送中";
                        break;
                    default:
                        ListViewItem.SubItems[0].Text = "失敗";
                        break;
                }

            }
        }
    }
}
