using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.UDT;

namespace Ischool.Booking.Room.UDT
{
    /// <summary>
    /// 會議室設備
    /// </summary>
    [TableName("ischool.booking.meetingroom_equipment")]
    class MeetingRoomEquipment : ActiveRecord
    {
        /// <summary>
        /// 設備名稱
        /// </summary>
        [Field(Field = "name", Indexed = false)]
        public string Name { get; set; }

        /// <summary>
        /// 會議室系統編號
        /// </summary>
        [Field(Field = "ref_meetingroom_id", Indexed = false)]
        public int RefMeetingroomID { get; set; }

        /// <summary>
        /// 設備數量
        /// </summary>
        [Field(Field = "count", Indexed = false)]
        public int Count { get; set; }

        /// <summary>
        /// 設備狀態
        /// </summary>
        [Field(Field = "status", Indexed = false)]
        public string Status { get; set; }
    }
}
