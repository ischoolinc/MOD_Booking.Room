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
using FISCA.Data;
using K12.Data;

namespace Ischool.Booking.Room
{
    public partial class MeetingRoomManagement : BaseForm
    {
        Dictionary<string, string> unitDic = new Dictionary<string, string>();
        AccessHelper _access = new AccessHelper();
        string _unitID;

        public MeetingRoomManagement()
        {
            InitializeComponent();

            #region 確認身分
            string identity = Actor.Identity;
            
            actorLb.Text = identity;
            List<UDT.MeetingRoomUnit> unitList = _access.Select<UDT.MeetingRoomUnit>();
            foreach (UDT.MeetingRoomUnit unit in unitList)
            {
                unitCbx.Items.Add(unit.Name);
                unitDic.Add(unit.Name, unit.UID);
            }

            if (identity == "系統管理員")
            {
                unitLb.Visible = false;
                if (unitCbx.Items.Count > 0)
                {
                    unitCbx.SelectedIndex = 0;
                }
                _unitID = unitDic["" + unitCbx.Items[unitCbx.SelectedIndex]];
                ReloadDataGridView();
            }
            if (identity == "單位主管" || identity == "單位管理員")
            {
                unitCbx.Visible = false;
                UnitRecord ur = BookingRecord.SelectUnitByAccount(Actor.Account);
                unitLb.Text = ur.Name;
                _unitID = unitDic[unitLb.Text];
                ReloadDataGridView();
            }
            #endregion
        }

        public void ReloadDataGridView()
        {
            dataGridViewX1.Rows.Clear();

            Dictionary<string, MeetingRoomRecord> dataDic = BookingRecord.SelectMeetingRoomByUnitID(_unitID);
            int rowIndex = 0;
            foreach (string roomID in dataDic.Keys)
            {
                DataGridViewRow datarow = new DataGridViewRow();
                datarow.CreateCells(dataGridViewX1);

                int index = 0;

                datarow.Cells[index++].Value = dataDic[roomID].Name;
                datarow.Cells[index++].Value = dataDic[roomID].Building;
                datarow.Cells[index++].Value = dataDic[roomID].Capacity;
                datarow.Cells[index++].Value = dataDic[roomID].IsSpecial == "true" ? "是" : "否";
                List<string> equipment = new List<string>();
                int height = 15;
                foreach (MeetingRoomEqipmentRecord ep in dataDic[roomID].EquipmentList)
                {
                    string data = string.Format("名稱: {0}， 數量: {1}， 狀態: {2}",ep.Name,ep.Count,ep.Status);
                    equipment.Add(data);
                    height += 15;
                }
                datarow.Cells[index++].Value = string.Join(Environment.NewLine, equipment);
                datarow.Tag = dataDic[roomID].UID;
                dataGridViewX1.Rows.Add(datarow);
                //dataGridViewX1.AutoResizeRow(rowIndex);
                dataGridViewX1.Rows[rowIndex].Height = height;
                rowIndex++;
            }
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            EditForm form = new EditForm("新增",_unitID);
            form.FormClosed += delegate {
                ReloadDataGridView();
            };
            form.ShowDialog();
        }

        private void updateBtn_Click(object sender, EventArgs e)
        {
            int row = dataGridViewX1.SelectedCells[0].RowIndex;
            string roomID = "" + dataGridViewX1.Rows[row].Tag;
            EditForm form = new EditForm("修改", roomID);
            form.FormClosed += delegate {
                ReloadDataGridView();
            };
            form.Text = "修改場地";
            form.ShowDialog();
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            int row = dataGridViewX1.SelectedRows[0].Index;
            string roomID = "" + dataGridViewX1.Rows[row].Tag;

            string sql = string.Format("DELETE FROM $ischool.booking.meetingroom WHERE uid = {0}",roomID);
            UpdateHelper up = new UpdateHelper();
            try
            {
                up.Execute(sql);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            MessageBox.Show("資料刪除成功!");
            ReloadDataGridView();
        }

        private void leaveBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridViewX1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                string roomID = "" + dataGridViewX1.Rows[e.RowIndex].Tag;
                string roomName = "" + dataGridViewX1.Rows[e.RowIndex].Cells[0].Value;
                EquipmentForm form = new EquipmentForm(roomID);
                form.Text = roomName + " 設備清單";
                form.ShowDialog();

            }
        }

        private void unitCbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            _unitID = unitDic["" + unitCbx.Items[unitCbx.SelectedIndex]];
            ReloadDataGridView();
        }
    }    
}
