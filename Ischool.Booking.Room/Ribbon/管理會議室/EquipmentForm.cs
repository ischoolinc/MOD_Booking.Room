using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FISCA.Presentation.Controls;
using FISCA.UDT;

namespace Ischool.Booking.Room
{
    public partial class EquipmentForm : BaseForm
    {
        public EquipmentForm(string roomID)
        {
            InitializeComponent();

            AccessHelper access = new AccessHelper();
            List<UDT.MeetingRoomEquipment> equipmentList = access.Select<UDT.MeetingRoomEquipment>("ref_meetingroom_id = " + roomID);

            foreach (UDT.MeetingRoomEquipment equipment in equipmentList)
            {
                DataGridViewRow datarow = new DataGridViewRow();
                datarow.CreateCells(dataGridViewX1);
                int index = 0;

                datarow.Cells[index++].Value = equipment.Name;
                datarow.Cells[index++].Value = equipment.Count;
                datarow.Cells[index++].Value = equipment.Status;

                dataGridViewX1.Rows.Add(datarow);
            }
        }
    }
}
