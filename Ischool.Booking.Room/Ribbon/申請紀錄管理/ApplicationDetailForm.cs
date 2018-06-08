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
    , app_detail.start_time
    , app_detail.end_time
    , room.name AS room_name
FROM
    $ischool.booking.meetingroom_application AS app
    LEFT OUTER JOIN $ischool.booking.meetingroom_application_detail AS app_detail
        ON app.uid = app_detail.ref_meetingroom_application_id
    LEFT OUTER JOIN $ischool.booking.meetingroom AS room
        ON app.ref_meetingroom_id = room.uid
WHERE
    app.uid = {0}
            ", applicationID);
            QueryHelper qh = new QueryHelper();
            DataTable dt = qh.Select(sql);
            #endregion

            #region 整理資料

            ApplicationRecord appr = new ApplicationRecord(); 

            foreach (DataTable row in dt.Rows)
            {

            }

            #endregion


        }
    }

    public class ApplicationRecord
    {
        public string TeacherName { get; set; }

        public int RefTeacherID { get; set; }

        public string ApplicantName { get; set; }

        public int ApplyBy { get; set; }

        public int RefMeetingRoomID { get; set; }

        public string RefMeetingRoomName { get; set; }

        public DateTime ApplyDate { get; set; }

        public string RepeatType { get; set; }

        public DateTime ApplyStarDate { get; set; }

        public DateTime RepeatEndDate { get; set; }

        public bool IsCanceled { get; set; }

        public DateTime CanceledTime { get; set; }

        public string CanceledBy { get; set; }

        public string CancelReason { get; set; }

        public bool IsApproved { get; set; }

        public string RejectReason { get; set; }

        public int RefAdminID { get; set; }

        public DateTime ReviewedDate { get; set; }

        List<ApplicationDetailRecord> ApplicationList { get; set; }

    }

    public class ApplicationDetailRecord
    {
        public string UID { get; set; }

        public string RefApplicationID { get; set; }

        public string StartTime { get; set; }

        public string EndTime { get; set; }
    }
}
