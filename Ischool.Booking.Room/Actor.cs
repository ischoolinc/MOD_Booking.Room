using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using FISCA.Data;
using FISCA.UDT;

namespace Ischool.Booking.Room
{
    class Actor
    {
        /// <summary>
        /// 場地預約管理身分
        /// </summary>
        public static string Identity { get; set; }

        /// <summary>
        /// 登入帳號
        /// </summary>
        public static string Account
        {
            get
            {
                return FISCA.Authentication.DSAServices.UserAccount.Replace("'", "''");
            }
        }

        /// <summary>
        /// 老師系統編號
        /// </summary>
        public static int RefTeacherID { get; set; }

        /// <summary>
        /// 透過使用者登入帳號取得_loginID
        /// </summary>
        /// <returns></returns>
        public static string GetLoginIDByAccount(string targetAccount)
        {
            string loginID;
            string sql = string.Format("SELECT * FROM _login WHERE login_name = '{0}'", targetAccount);
            QueryHelper qh = new QueryHelper();
            DataTable dt = qh.Select(sql);

            if (dt.Rows.Count > 0)
            {
                loginID = "" + dt.Rows[0]["id"];
            }
            else
            {
                loginID = "";
            }

            return loginID;
        }

        /// <summary>
        /// 確認身分
        /// </summary>
        public Actor()
        {
            AccessHelper access = new AccessHelper();
            //List<UDT.MeetingRoomSystemAdmin> systemAdminList = access.Select<UDT.MeetingRoomSystemAdmin>("account = '"+ Account +"'");
            List<UDT.MeetingRoomUnitAdmin> unitAdminList = access.Select<UDT.MeetingRoomUnitAdmin>("account = '"+ Account + "'");
            Dictionary<string, string> systemAdminDic = new Dictionary<string, string>();

            #region 取得會議室預約管理者帳號

            QueryHelper qh = new QueryHelper();
            string sql = string.Format(@"
SELECT 
    teacher.id
    , _login.login_name
FROM
    _lr_belong
    LEFT OUTER JOIN _login
        ON _lr_belong._login_id = _login.id
    LEFT OUTER JOIN teacher
        ON _login.login_name = teacher.st_login_name
WHERE
    _lr_belong._role_id = {0}
                ", Program._roleAdminID);
            DataTable dt = qh.Select(sql);

            foreach (DataRow row in dt.Rows)
            {
                systemAdminDic.Add("" + row["login_name"],"" + row["id"]);
            }

            #endregion

            if (systemAdminDic.ContainsKey(Account))
            {
                Identity = "系統管理員";
                RefTeacherID = int.Parse(systemAdminDic[Account]);
            }

            //if (systemAdminList.Count > 0)
            //{
            //    Identity = "系統管理員";
            //    RefTeacherID = systemAdminList[0].RefTeacherID;
            //}

            else if (unitAdminList.Count > 0 && unitAdminList[0].IsBoss == false)
            {
                Identity = "單位管理員";
                RefTeacherID = unitAdminList[0].RefTeacherID;
            }

            else if (unitAdminList.Count > 0 && unitAdminList[0].IsBoss == true)
            {
                Identity = "單位主管";
                RefTeacherID = unitAdminList[0].RefTeacherID;
            }
            else
            {
                Identity = "身分錯誤";
            }
                
        }
    }
}
