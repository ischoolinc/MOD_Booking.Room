using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.UDT;

namespace Ischool.Booking.Room.UDT
{
    /// <summary>
    /// 單位管理員
    /// </summary>
    [TableName("meetingroom_unit_admin")]
    class MeetingRoomUnitAdmin : ActiveRecord
    {
        /// <summary>
        /// 管理單位編號
        /// </summary>
        [Field(Field = "ref_unit_id", Indexed = false)]
        public int RefUnitID { get; set; }

        /// <summary>
        /// 教師編號
        /// </summary>
        [Field(Field = "ref_teacher_id", Indexed = false)]
        public int RefTeacherID { get; set; }

        /// <summary>
        /// 是否為單位主管
        /// </summary>
        [Field(Field = "is_boss", Indexed = false)]
        public bool IsBoss { get; set; }

        /// <summary>
        /// 建立日期
        /// </summary>
        [Field(Field = "create_time", Indexed = false)]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 建立者帳號
        /// </summary>
        [Field(Field = "created_by", Indexed = false)]
        public string CreatedBy { get; set; }
    }
}
