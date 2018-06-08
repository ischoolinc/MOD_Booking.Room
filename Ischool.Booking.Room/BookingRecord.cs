using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using FISCA.Data;
using FISCA.UDT;

namespace Ischool.Booking.Room
{
    public class BookingRecord
    {
        /// <summary>
        /// 透過使用者帳號取得管理單位資料
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public static UnitRecord SelectUnitByAccount(string account)
        {
            string sql = string.Format(@"
SELECT
	unit.*
FROM
	$ischool.booking.meetingroom_unit_admin AS unit_admin
	LEFT OUTER JOIN $ischool.booking.meetingroom_unit AS unit
		ON unit_admin.ref_uint_id = unit.uid 
WHERE
	unit_admin.account = '{0}'
                ", account);

            QueryHelper qh = new QueryHelper();
            DataTable dt =  qh.Select(sql);
            AccessHelper access = new AccessHelper();

            UnitRecord ur = new UnitRecord();

            foreach (DataRow row in dt.Rows)
            {
                ur.UID = "" + row["uid"];
                ur.Name = "" + row["name"];
                ur.CreatedBy = "" + row["created_by"];
                ur.CreateTime = DateTime.Parse("" + row["create_time"]);
            }

            return ur;
        }

        /// <summary>
        /// 透過使用者帳號取得管理場地資料
        /// </summary>
        /// <param name="unitID"></param>
        /// <returns></returns>
        public static MeetingRoomRecord SelectMeetingRoomByAccount(string unitID)
        {
            MeetingRoomRecord mr = new MeetingRoomRecord();
            string sql = string.Format(@"
SELECT
    
FROM
    $ischool.booking.meetingroom
WHERE
    uid = 
");
            return mr;
        }


    }

    public class UnitRecord
    {
        public string UID { get; set; }

        public string Name { get; set; }

        public DateTime CreateTime { get; set; }

        public string CreatedBy { get; set; }
    }
    public class MeetingRoomRecord
    {
        public string Name { get; set; }

        public string Building { get; set; }

        public int Capacity { get; set; }

        public string Status { get; set; }

        public int RefUnitID { get; set; }

        public bool IsSpecial { get; set; }

        public DateTime CreateTime { get; set; }

        public string CreatedBy { get; set; }
    }

   
}
