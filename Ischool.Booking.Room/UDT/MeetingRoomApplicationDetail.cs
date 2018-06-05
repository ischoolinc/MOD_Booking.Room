﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.UDT;

namespace Ischool.Booking.Room.UDT
{
    [TableName("meetingroom_application_detail")]
    class MeetingRoomApplicationDetail : ActiveRecord
    {
        /// <summary>
        /// 申請紀錄系統編號
        /// </summary>
        [Field(Field = "ref_meetingroom_application_id", Indexed = false)]
        public int RefMeetingroomApplicationID { get; set; }

        /// <summary>
        /// 預約開始時間
        /// </summary>
        [Field(Field = "star_time", Indexed = false)]
        public DateTime StarTime { get; set; }

        /// <summary>
        /// 預約結束時間
        /// </summary>
        [Field(Field = "end_time", Indexed = false)]
        public DateTime EndTime { get; set; }
    }
}
