using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.UDT;

namespace Ischool.Booking.Room.UDT
{
    /// <summary>
    /// 會議室預約申請
    /// </summary>
    [TableName("ischool.booking.meetingroom_application")]
    class MeetingRoomApplication : ActiveRecord
    {
        /// <summary>
        /// 會議主持人姓名
        /// </summary>
        [Field(Field = "teacher_name", Indexed = false)]
        public string TeacherName { get; set; }

        /// <summary>
        /// 會議主持人教師編號
        /// </summary>
        [Field(Field = "ref_teacher_id", Indexed = false)]
        public int RefTeacherID { get; set; }

        /// <summary>
        /// 申請者姓名
        /// </summary>
        [Field(Field = "applicant_name", Indexed = false)]
        public string ApplicantName { get; set; }

        /// <summary>
        /// 申請者教師編號
        /// </summary>
        [Field(Field = "apply_by", Indexed = false)]
        public int ApplyBy { get; set; }

        /// <summary>
        /// 會議室編號
        /// </summary>
        [Field(Field = "ref_meetingroom_id", Indexed = false)]
        public int RefMeetingRoomID { get; set; }

        /// <summary>
        /// 申請時間
        /// </summary>
        [Field(Field = "apply_date", Indexed = false)]
        public DateTime ApplyDate { get; set; }

        /// <summary>
        /// 申請事由
        /// </summary>
        [Field(Field = "apply_reason", Indexed = false)]
        public string ApplyReason { get; set; }

        /// <summary>
        /// 是否重複
        /// </summary>
        [Field(Field = "is_repeat", Indexed = false)]
        public bool IsRepeat { get; set; }

        /// <summary>
        /// 重複類型
        /// </summary>
        [Field(Field = "repeat_type", Indexed = false)]
        public string RepeatType { get; set; }

        /// <summary>
        /// 開始日期
        /// </summary>
        [Field(Field = "apply_start_date", Indexed = false)]
        public DateTime ApplyStarDate { get; set; }

        /// <summary>
        /// 重複結束日期
        /// </summary>
        [Field(Field = "repeat_end_date", Indexed = false)]
        public DateTime RepeatEndDate { get; set; }

        /// <summary>
        /// 是否取消
        /// </summary>
        [Field(Field = "is_canceled", Indexed = false)]
        public bool IsCanceled { get; set; }

        /// <summary>
        /// 取消時間
        /// </summary>
        [Field(Field = "canceled_time", Indexed = false)]
        public DateTime CanceledTime { get; set; }

        /// <summary>
        /// 取消者姓名
        /// </summary>
        [Field(Field = "canceled_name", Indexed = false)]
        public string CanceledName { get; set; }

        /// <summary>
        /// 取消者教師編號
        /// </summary>
        [Field(Field = "canceled_by", Indexed = false)]
        public int CanceledBy { get; set; }

        /// <summary>
        /// 取消原因
        /// </summary>
        [Field(Field = "cancel_reason", Indexed = false)]
        public string CancelReason { get; set; }

        /// <summary>
        /// 是否核准
        /// </summary>
        [Field(Field = "is_approved", Indexed = false)]
        public bool IsApproved { get; set; }

        /// <summary>
        /// 未核准的原因
        /// </summary>
        [Field(Field = "reject_reason", Indexed = false)]
        public string RejectReason { get; set; }

        /// <summary>
        /// 審查者教師姓名
        /// </summary>
        [Field(Field = "admin_name", Indexed = false)]
        public string RefAdminName { get; set; }

        /// <summary>
        /// 審查者教師編號
        /// </summary>
        [Field(Field = "ref_admin_id", Indexed = false)]
        public int RefAdminID { get; set; }

        /// <summary>
        /// 審查日期
        /// </summary>
        [Field(Field = "reviewed_date", Indexed = false)]
        public DateTime ReviewedDate { get; set; }

    }
}
