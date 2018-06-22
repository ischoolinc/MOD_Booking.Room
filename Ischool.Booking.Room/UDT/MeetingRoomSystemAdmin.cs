using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.UDT;

namespace Ischool.Booking.Room.UDT
{
    /// <summary>
    /// 總管理員
    /// </summary>
    [TableName("ischool.booking.meetingroom_system_admin")]
    class MeetingRoomSystemAdmin : ActiveRecord
    {
        /// <summary>
        /// 登入帳號
        /// </summary>
        [Field(Field = "account", Indexed = false)]
        public string Account { get; set; }

        /// <summary>
        /// 教師系統編號
        /// </summary>
        [Field(Field = "ref_teacher_id", Indexed = false)]
        public int RefTeacherID { get; set; }

        /// <summary>
        /// 預設管理員
        /// </summary>
        [Field(Field = "is_default", Indexed = false)]
        public bool IsDefault { get; set; }

        /// <summary>
        /// 建立日期
        /// </summary>
        [Field(Field = "create_time", Indexed = false)]
        public string CreatedTime { get; set; }

        /// <summary>
        /// 建立者帳號
        /// </summary>
        [Field(Field = "created_by", Indexed = false)]
        public string CreatedBy { get; set; }
    }
}
