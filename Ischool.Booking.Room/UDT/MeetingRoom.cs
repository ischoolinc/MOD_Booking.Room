using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.UDT;

namespace Ischool.Booking.Room.UDT
{
    /// <summary>
    /// 會議室
    /// </summary>
    [TableName("ischool.booking.meetingroom")]
    class MeetingRoom : ActiveRecord
    {
        /// <summary>
        /// 會議室名稱
        /// </summary>
        [Field(Field = "name", Indexed = false)]
        public string Name { get; set; }

        /// <summary>
        /// 所屬大樓名稱
        /// </summary>
        [Field(Field = "building", Indexed = false)]
        public string Building { get; set; }

        /// <summary>
        /// 容納人數
        /// </summary>
        [Field(Field = "capacity", Indexed = false)]
        public int Capacity { get; set; }

        /// <summary>
        /// 會議室目前狀態
        /// </summary>
        [Field(Field = "status", Indexed = false)]
        public string Status { get; set; }

        /// <summary>
        /// 管理單位編號
        /// </summary>
        [Field(Field = "ref_unit_id", Indexed = false)]
        public int RefUnitID { get; set; }

        /// <summary>
        /// 是否為特殊場地
        /// </summary>
        [Field(Field = "is_special", Indexed = false)]
        public bool IsSpecial { get; set; }

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
