using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ischool.Booking.Room
{
    class Permissions
    {
        public static string 會議室管理單位 { get { return "8EFBEC7D-D438-44EA-81E3-6AFA00862429"; } }
        public static bool 設定會議室管理單位權限
        {
            get
            {
                return FISCA.Permission.UserAcl.Current[會議室管理單位].Executable;
            }
        }

        public static string 管理會議室 { get { return "26751E07-00A0-4500-BC31-F2E57EE1C6F2"; } }
        public static bool 管理會議室權限
        {
            get
            {
                return FISCA.Permission.UserAcl.Current[管理會議室].Executable;
            }
        }

        public static string 系統管理員 { get { return "74E0D4FA-F698-400D-B8A8-60F4DF304BBA"; } }
        public static bool 設定系統管理員權限
        {
            get
            {
                return FISCA.Permission.UserAcl.Current[系統管理員].Executable;
            }
        }

        public static string 單位管理員 { get { return "24821EBA-426E-4811-95B8-DBF8D9AEEFA2"; } }
        public static bool 設定單位管理員權限
        {
            get
            {
                return FISCA.Permission.UserAcl.Current[單位管理員].Executable;
            }
        }

        public static string 審核作業 { get { return "AB164E2A-516E-4427-ADB0-79D27F1685CA"; } }
        public static bool 審核作業權限
        {
            get
            {
                return FISCA.Permission.UserAcl.Current[審核作業].Executable;
            }
        }

        public static string 匯出會議室清單 { get { return "0CB5F779-97B7-4950-877F-DE8731F4F63C"; } }
        public static bool 匯出會議室清單權限
        {
            get
            {
                return FISCA.Permission.UserAcl.Current[匯出會議室清單].Executable;
            }
        }

        public static string 匯入會議室清單 { get { return "1B4FE9E3-E763-4E2D-9F5D-92979A18CEC1"; } }
        public static bool 匯入會議室清單權限
        {
            get
            {
                return FISCA.Permission.UserAcl.Current[匯入會議室清單].Executable;
            }
        }

        public static string 統計會議室使用狀況 { get { return "40111E44-1A30-4D3C-9D43-3069F7F46014"; } }
        public static bool 統計會議室使用狀況權限
        {
            get
            {
                return FISCA.Permission.UserAcl.Current[統計會議室使用狀況].Executable;
            }
        }
    }
}
