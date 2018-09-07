using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using FISCA.Data;
using K12.Data;

namespace Ischool.Booking.Room.DAO
{
    class Application
    {
        /// <summary>
        /// 取得申請紀錄
        /// </summary>
        public static DataTable GetApplicationData(bool selectAll,string roomID,string unitID,string startTime,string endTime)
        {
            string sql = "";
            if (selectAll) // 檢視該單位所有申請紀錄
            {
                if (roomID == "全部") // 全部會議室申請資料
                {
                    #region sql
                    sql = string.Format(@"
SELECT
	app.*
    , room.name AS room_name
    , room.is_special
FROM
	$ischool.booking.meetingroom AS room
	LEFT OUTER JOIN $ischool.booking.meetingroom_application AS app
		ON room.uid = app.ref_meetingroom_id
	LEFT OUTER JOIN $ischool.booking.meetingroom_unit AS unit
		ON room.ref_unit_id = unit.uid
WHERE
	app.uid IS NOT NULL
	AND unit.uid = {0}
                ", unitID);
                    #endregion
                }
                else 
                {
                    #region sql
                    sql = string.Format(@"
SELECT
	app.*
    , room.name AS room_name
    , room.is_special
FROM
	$ischool.booking.meetingroom AS room
	LEFT OUTER JOIN $ischool.booking.meetingroom_application AS app
		ON room.uid = app.ref_meetingroom_id
	LEFT OUTER JOIN $ischool.booking.meetingroom_unit AS unit
		ON room.ref_unit_id = unit.uid
WHERE
	app.uid IS NOT NULL
	AND unit.uid = {0}
    AND room.uid = {1}
                ", unitID, roomID);
                    #endregion
                }
            }
            else // 檢視指定單位、日期區間的申請紀錄
            {
                if (roomID == "全部") // 取得該單位所有會議室的申請紀錄
                {
                    #region sql
                    sql = string.Format(@"
SELECT
	app.*
    , room.name AS room_name
    , room.is_special
FROM
	$ischool.booking.meetingroom AS room
	LEFT OUTER JOIN $ischool.booking.meetingroom_application AS app
		ON room.uid = app.ref_meetingroom_id
	LEFT OUTER JOIN $ischool.booking.meetingroom_unit AS unit
		ON room.ref_unit_id = unit.uid
WHERE
	room.is_special = true
	AND app.uid IS NOT NULL
	AND app.reviewed_date IS NULL
	AND app.is_canceled = false
	AND unit.uid = {0}
	AND app.apply_start_date >= '{1}'
	AND app.apply_start_date <= '{2}'
                ", unitID, startTime, endTime);
                    #endregion

                }
                else
                {
                    #region sql
                    sql = string.Format(@"
SELECT
	app.*
    , room.name AS room_name
    , room.is_special
FROM
	$ischool.booking.meetingroom AS room
	LEFT OUTER JOIN $ischool.booking.meetingroom_application AS app
		ON room.uid = app.ref_meetingroom_id
	LEFT OUTER JOIN $ischool.booking.meetingroom_unit AS unit
		ON room.ref_unit_id = unit.uid
WHERE
	room.is_special = true
	AND app.uid IS NOT NULL
	AND app.reviewed_date IS NULL
	AND app.is_canceled = false
	AND unit.uid = {0}
	AND app.apply_start_date >= '{1}'
	AND app.apply_start_date <= '{2}'
    AND room.uid = {3}
                ", unitID, startTime, endTime, roomID);
                    #endregion
                }
            }

            QueryHelper qh = new QueryHelper();
            return qh.Select(sql);
        }

        public static DataTable GetApplicationByID(string applicationID)
        {
            string sql = string.Format(@"
SELECT 
	app.*
	, room.name 
    , room.is_special
	, app_detail.uid AS app_detail_uid
	, app_detail.start_time
	, app_detail.end_time
FROM(
	SELECT
		*
	FROM
		$ischool.booking.meetingroom_application
	WHERE
		uid = {0}
	) app
	LEFT OUTER JOIN $ischool.booking.meetingroom AS room
		ON app.ref_meetingroom_id = room.uid
	LEFT OUTER JOIN $ischool.booking.meetingroom_application_detail AS app_detail
		ON app.uid = app_detail.ref_application_id
            ", applicationID);

            QueryHelper qh = new QueryHelper();
            return qh.Select(sql);
        }

        /// <summary>
        /// 審核申請紀錄
        /// </summary>
        public static void UpdateApplicationByReview(string isApproved,string cancelReason,string teacherID,string teacherName,string applicationID)
        {
            string sql = string.Format(@"
UPDATE
    $ischool.booking.meetingroom_application
SET
    is_approved = '{0}'
    , reviewed_date = '{1}'
    , reject_reason = '{2}'
    , ref_admin_id = {3}
    , admin_name = '{4}'
WHERE
    uid = {5}
            ", isApproved, DateTime.Now.ToString("yyyy/MM/dd"), cancelReason, teacherID/*actor.getTeacherID()*/, teacherName, applicationID);

            UpdateHelper up = new UpdateHelper();
            up.Execute(sql);
        }

        /// <summary>
        /// 取消申請紀錄
        /// </summary>
        public static void UpdateApplicationByCancel(string isCanceled,string cancelReasonn,string teacherID,string teacherName, string applicationID)
        {
            string sql = string.Format(@"
UPDATE
$ischool.booking.meetingroom_application
SET
is_canceled = '{0}'
, canceled_time = '{1}'
, cancel_reason = '{2}'
, canceled_by = {3}
, canceled_name = '{4}'
WHERE
uid = {5}
                ", isCanceled, DateTime.Now.ToString("yyyy/MM/dd"), cancelReasonn, teacherID, teacherName, applicationID);

            UpdateHelper up = new UpdateHelper();
            up.Execute(sql);
        }
    }
}
