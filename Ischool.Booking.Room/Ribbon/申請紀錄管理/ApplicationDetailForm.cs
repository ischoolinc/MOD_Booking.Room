using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FISCA.Presentation.Controls;
using FISCA.Data;

namespace Ischool.Booking.Room
{
    public partial class ApplicationDetailForm : BaseForm
    {
        public ApplicationDetailForm(string applicationID)
        {
            InitializeComponent();

            #region 取得資料
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
            DataTable dt = qh.Select(sql);
            #endregion

            ApplicationRecord appR = new ApplicationRecord();
            Dictionary<string, ApplicationDetailRecord> appDetailDic = new Dictionary<string, ApplicationDetailRecord>();

            #region 整理資料
            foreach (DataRow row in dt.Rows)
            {
                if (appR.UID == null)
                {
                    appR.UID = "" + row["uid"];
                    appR.TeacherName = "" + row["teacher_name"];
                    appR.ApplicantName = "" + row["applicant_name"];
                    appR.RefMeetingRoomName = "" + row["name"];
                    appR.IsSpecial = bool.Parse("" + row["is_special"]);
                    appR.ApplyDate = "" + row["apply_date"];
                    appR.ApplyReason = "" + row["apply_reason"];
                    appR.ApplyStartDate = "" + row["apply_start_date"];
                    appR.RepeatEndDate = "" + row["repeat_end_date"];
                    appR.RefAdminID = "" + row["ref_admin_id"];
                    appR.AdminName = "" + row["admin_name"];
                    appR.ReviewedDate = "" + row["reviewed_date"];
                    if (bool.Parse("" + row["is_special"]))
                    {
                        switch ("" + row["is_approved"])
                        {
                            case "true":
                                appR.IsApproved = "是";
                                break;
                            case "false":
                                appR.IsApproved = "否";
                                break;
                            default:
                                appR.IsApproved = "尚未審核";
                                break;
                        }
                    }
                    else
                    {
                        appR.IsApproved = "無須審核";
                    }
                    appR.RejectReason = "" + row["reject_reason"];
                    switch ("" + row["is_canceled"])
                    {
                        case "true":
                            appR.IsCanceled = "是";
                            break;
                        case "false":
                            appR.IsCanceled = "否";
                            break;
                        default:
                            appR.IsCanceled = "否";
                            break;
                    }
                    appR.CanceledName = "" + row["canceled_name"];
                    appR.CanceledBy = "" + row["canceled_by"];
                    appR.CanceledTime = "" + row["canceled_time"];
                    appR.CancelReason = "" + row["cancel_reason"];
                }
                if (!appDetailDic.ContainsKey("" + row["app_detail_uid"]))
                {
                    ApplicationDetailRecord appDetailR = new ApplicationDetailRecord();
                    appDetailR.UID = "" + row["app_detail_uid"];
                    appDetailR.RefApplicationID = "" + row["uid"];
                    appDetailR.StartTime = "" + row["start_time"];
                    appDetailR.EndTime = "" + row["end_time"];

                    appDetailDic.Add(appDetailR.UID, appDetailR);
                }
            }
            #endregion

            #region Init 
            applicantTbx.Text = appR.ApplicantName;
            hostNameTbx.Text = appR.TeacherName;
            roomNameTbx.Text = appR.RefMeetingRoomName;
            applyStartTbx.Text = DateTime.Parse(appR.ApplyStartDate).ToShortDateString();
            repeatEndTbx.Text = DateTime.Parse(appR.RepeatEndDate).ToShortDateString();
            applyReasonTbx.Text = appR.ApplyReason;
            adminTbx.Text = appR.AdminName;
            reviewDateTbx.Text = appR.ReviewedDate;
            isApproveTbx.Text = appR.IsApproved;
            rejectReasonTbx.Text = appR.RejectReason;
            // (特殊場地、審核通過、未取消) ， (一般場地、未取消)  
            if ((appR.IsSpecial && appR.IsApproved == "是" && appR.IsCanceled == "否") || (!appR.IsSpecial && appR.IsCanceled == "否"))
            {
                resultTbx.Text = "成立";
            }
            else
            {
                resultTbx.Text = "未成立";
            }
            isCancelTbx.Text = appR.IsCanceled;
            cancelByTbx.Text = appR.CanceledName;
            cancelTimeTbx.Text = appR.CanceledTime;
            cancelReasonTbx.Text = appR.CancelReason;

            applyDateLb.Text = "申請時間: " + DateTime.Parse(appR.ApplyDate).ToShortDateString();

            foreach (string appDetailUID in appDetailDic.Keys)
            {
                DataGridViewRow datarow = new DataGridViewRow();
                datarow.CreateCells(dataGridViewX1);

                int index = 0;
                DateTime result = DateTime.Now;
                if (DateTime.TryParse("" + appDetailDic[appDetailUID].StartTime, out result))
                {
                    datarow.Cells[index++].Value = DateTime.Parse("" + appDetailDic[appDetailUID].StartTime).ToShortDateString();
                    datarow.Cells[index++].Value = DateTime.Parse("" + appDetailDic[appDetailUID].StartTime).ToShortTimeString();
                    datarow.Cells[index++].Value = DateTime.Parse("" + appDetailDic[appDetailUID].EndTime).ToShortTimeString();
                }
                datarow.Tag = appDetailDic[appDetailUID].UID;

                dataGridViewX1.Rows.Add(datarow);
            }

            #endregion
        }

        private void leaveBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

    public class ApplicationRecord
    {
        /// <summary>
        /// 申請紀錄編號
        /// </summary>
        public string UID { get; set; }

        /// <summary>
        /// 會議主持人
        /// </summary>
        public string TeacherName { get; set; }

        /// <summary>
        /// 會議主持人教師編號
        /// </summary>
        public int RefTeacherID { get; set; }

        /// <summary>
        /// 申請者姓名
        /// </summary>
        public string ApplicantName { get; set; }

        /// <summary>
        /// 申請教師編號
        /// </summary>
        public int ApplyBy { get; set; }

        public int RefMeetingRoomID { get; set; }

        /// <summary>
        /// 會議室名稱
        /// </summary>
        public string RefMeetingRoomName { get; set; }

        /// <summary>
        /// 是否為特殊場地
        /// </summary>
        public bool IsSpecial { get; set; }

        /// <summary>
        /// 申請時間
        /// </summary>
        public string ApplyDate { get; set; }

        /// <summary>
        /// 申請事由
        /// </summary>
        public string ApplyReason { get; set; }

        public string RepeatType { get; set; }

        /// <summary>
        /// 開始日期
        /// </summary>
        public string ApplyStartDate { get; set; }

        /// <summary>
        /// 重複結束日期
        /// </summary>
        public string RepeatEndDate { get; set; }

        /// <summary>
        /// 是否取消
        /// </summary>
        public string IsCanceled { get; set; }

        /// <summary>
        /// 取消時間
        /// </summary>
        public string CanceledTime { get; set; }

        /// <summary>
        /// 取消者教師編號
        /// </summary>
        public string CanceledBy { get; set; }

        /// <summary>
        /// 取消者教師姓名
        /// </summary>
        public string CanceledName { get; set; }

        public string CancelReason { get; set; }

        /// <summary>
        /// 是否核准
        /// </summary>
        public string IsApproved { get; set; }

        /// <summary>
        /// 未核准原因
        /// </summary>
        public string RejectReason { get; set; }

        /// <summary>
        /// 審查者教師姓名
        /// </summary>
        public string AdminName { get; set; }

        /// <summary>
        /// 審查者教師編號
        /// </summary>
        public string RefAdminID { get; set; }

        /// <summary>
        /// 審查日期
        /// </summary>
        public string ReviewedDate { get; set; }
    }

    public class ApplicationDetailRecord
    {
        public string UID { get; set; }

        public string RefApplicationID { get; set; }

        public string StartTime { get; set; }

        public string EndTime { get; set; }
    }
}
