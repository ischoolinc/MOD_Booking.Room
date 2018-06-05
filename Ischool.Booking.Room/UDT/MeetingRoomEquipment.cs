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
    [TableName("meetingroom_equipment")]
    class MeetingRoomEquipment : ActiveRecord
    {
        /// <summary>
        /// 設備名稱
        /// </summary>
        [Field(Field = "name", Indexed = false)]
        public string Name { get; set; }

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
