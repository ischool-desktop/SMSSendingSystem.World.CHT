using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA;
using FISCA.Presentation;
using FISCA.Permission;

namespace SMSSendingSystem.World.CHT
{
    public class Program
    {
        [MainMethod()]
        public static void Main()
        {
            RibbonBarItem bh = K12.Presentation.NLDPanels.Student.RibbonBarItems["其它"];
            bh["簡訊"].Image = Properties.Resources.salesman_64;
            bh["簡訊"]["學生簡訊發送"].Enable = false;
            bh["簡訊"]["學生簡訊發送"].Click += delegate
            {
                SendNow SN = new SendNow(tool.tag.student, K12.Presentation.NLDPanels.Student.SelectedSource);
                SN.ShowDialog();
            };

            K12.Presentation.NLDPanels.Student.SelectedSourceChanged += delegate
            {
                bh["簡訊"]["學生簡訊發送"].Enable = K12.Presentation.NLDPanels.Student.SelectedSource.Count > 0 && Permissions.學生簡訊發送權限;
            };

            Catalog detail = RoleAclSource.Instance["學生"]["功能按鈕"];
            detail.Add(new ReportFeature(Permissions.學生簡訊發送, "學生簡訊發送"));
        }
    }
}
