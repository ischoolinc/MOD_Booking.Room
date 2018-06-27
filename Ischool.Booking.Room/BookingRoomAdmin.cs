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

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ContentPanePanel
            // 
            this.ContentPanePanel.Location = new System.Drawing.Point(0, 163);
            this.ContentPanePanel.Size = new System.Drawing.Size(870, 421);
            // 
            // BookingRoomAdmin
            // 
            this.Name = "BookingRoomAdmin";
            this.ResumeLayout(false);

        }
    }
}
