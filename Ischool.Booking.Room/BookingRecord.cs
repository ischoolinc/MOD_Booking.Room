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
        /// 透過管理單位編號取得管理場地資料
        /// </summary>
        /// <param name="unitID"></param>
        /// <returns></returns>
        public static Dictionary<string, MeetingRoomRecord> SelectMeetingRoomByUnitID(string unitID)
        {
            Dictionary<string, MeetingRoomRecord> roomDic = new Dictionary<string, MeetingRoomRecord>();

            string sql = string.Format(@"
SELECT
	room.*
	, equipment.uid AS equipment_id
	, equipment.name  AS equipment_name
	, equipment.count AS equipment_count
	, equipment.status AS equipment_status
FROM
	$ischool.booking.meetingroom AS room
	LEFT OUTER JOIN $ischool.booking.meetingroom_equipment AS equipment
		ON room.uid = equipment.ref_meetingroom_id
WHERE
	room.ref_unit_id = {0}
                ",unitID);
            QueryHelper qh = new QueryHelper();
            DataTable dt = qh.Select(sql);

            foreach (DataRow row in dt.Rows)
            {
                if (!roomDic.ContainsKey("" + row["uid"]))
                {
                    MeetingRoomRecord mr = new MeetingRoomRecord();
                    mr.UID = "" + row["uid"];
                    mr.Picture = "" + row["picture"];
                    mr.Name = "" + row["name"];
                    mr.Building = "" + row["building"];
                    mr.Capacity = "" + row["capacity"];
                    mr.Status = "" + row["status"];
                    mr.RefUnitID = "" + row["ref_unit_id"];
                    mr.IsSpecial = "" + row["is_special"];
                    mr.CreateTime = "" + row["create_time"];
                    mr.CreatedBy = "" + row["created_by"];

                    mr.EquipmentList = new List<MeetingRoomEqipmentRecord>();
                    MeetingRoomEqipmentRecord mEpR = new MeetingRoomEqipmentRecord();
                    mEpR.UID = "" + row["equipment_id"];
                    mEpR.Name = "" + row["equipment_name"];
                    mEpR.Count = "" + row["equipment_count"];
                    mEpR.Status = "" + row["equipment_status"];

                    mr.EquipmentList.Add(mEpR);

                    roomDic.Add("" + row["uid"], mr);
                }
                else
                {
                    MeetingRoomEqipmentRecord mEpR = new MeetingRoomEqipmentRecord();
                    mEpR.UID = "" + row["equipment_id"];
                    mEpR.Name = "" + row["equipment_name"];
                    mEpR.Count = "" + row["equipment_count"];
                    mEpR.Status = "" + row["equipment_status"];

                    roomDic["" + row["uid"]].EquipmentList.Add(mEpR);
                }
            }

            return roomDic;
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
        public string UID { get; set; }

        public string Picture { get; set; }

        public string Name { get; set; }

        public string Building { get; set; }

        public string Capacity { get; set; }

        public string Status { get; set; }

        public string RefUnitID { get; set; }

        public string IsSpecial { get; set; }

        public string CreateTime { get; set; }

        public string CreatedBy { get; set; }

        public List<MeetingRoomEqipmentRecord> EquipmentList {get;set;}
    }

    public class MeetingRoomEqipmentRecord
    {
        public string UID { get; set; }
        public string RefMeetingRoomID { get; set; }
        public string Name { get; set; }
        public string Count { get; set; }
        public string Status { get; set; }
    }
}
