using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FISCA.Presentation;
using CefSharp;
using CefSharp.WinForms;

namespace Ischool.Booking.Room
{
    public partial class BookingRoomAdmin : BlankPanel
    {
        public CefSharp.WinForms.ChromiumWebBrowser browser;

        public BookingRoomAdmin()
        {
            InitializeComponent();

            Group = "會議室預約";

            browser = new ChromiumWebBrowser("https://sites.google.com/ischool.com.tw/facilities-service/%E9%A6%96%E9%A0%81");
            browser.Dock = DockStyle.Fill;
            ContentPanePanel.Controls.Add(browser);

            //改寫
            //browser.RequestHandler = new HandlerNe(); //Parse URL
            //browser.LifeSpanHandler = new HandlerLife(); //關閉 OnBeforePopup

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
