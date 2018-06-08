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
        /// 確認身分
        /// </summary>
        public Actor()
        {
            AccessHelper access = new AccessHelper();
            List<UDT.MeetingRoomSystemAdmin> systemAdminList = access.Select<UDT.MeetingRoomSystemAdmin>("account = '"+ Account +"'");
            List<UDT.MeetingRoomUnitAdmin> unitAdminList = access.Select<UDT.MeetingRoomUnitAdmin>("account = '"+ Account + "'");

            if (systemAdminList.Count > 0)
            {
                Identity = "系統管理員";
                RefTeacherID = systemAdminList[0].RefTeacherID;
            }

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
