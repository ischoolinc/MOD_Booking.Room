using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.Presentation;

namespace Ischool.Booking.Room
{
    public class BookingRoomAdmin : BlankPanel
    {
        public BookingRoomAdmin()
        {
            Group = "會議室預約";
            
        }

        private static BookingRoomAdmin _BookingRoomAdmin;

        public static BookingRoomAdmin Instance
        {
            get
            {
                if (_BookingRoomAdmin == null)
                {
                    _BookingRoomAdmin = new BookingRoomAdmin();
                }
                return _BookingRoomAdmin;
            }
        }
    }
}
