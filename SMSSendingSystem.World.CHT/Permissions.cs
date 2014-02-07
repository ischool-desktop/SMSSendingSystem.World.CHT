using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMSSendingSystem.World.CHT
{
    class Permissions
    {
        public static string 學生簡訊發送 { get { return "SMSSendingSystem.Student.SendNow.cs"; } }

        public static bool 學生簡訊發送權限
        {
            get
            {
                return FISCA.Permission.UserAcl.Current[學生簡訊發送].Executable;
            }
        }
    }
}
