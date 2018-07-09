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
        /// <summary>
        /// Name / UID
        /// </summary>
        Dictionary<string, string> unitDic = new Dictionary<string, string>();
        AccessHelper _access = new AccessHelper();

        /// <summary>
        /// 使用者身分
        /// </summary>
        Actor actor = Actor.Instance;

        RoleUnitDecorator decorator;

        public MeetingRoomManagement()
        {
            InitializeComponent();
        }

        public void ReloadDataGridView()
        {
            dataGridViewX1.Rows.Clear();

            string unitID = unitDic[cbxUnit.Text];

            Dictionary<string, MeetingRoomRecord> dataDic = BookingRecord.SelectMeetingRoomByUnitID(unitID);

            int rowIndex = 0;
            foreach (string roomID in dataDic.Keys)
            {
                DataGridViewRow datarow = new DataGridViewRow();
                datarow.CreateCells(dataGridViewX1);

                int index = 0;

                datarow.Cells[index++].Value = dataDic[roomID].Name;
                datarow.Cells[index++].Value = dataDic[roomID].Building;
                datarow.Cells[index++].Value = dataDic[roomID].Status;
                datarow.Cells[index++].Value = dataDic[roomID].Capacity;
                datarow.Cells[index++].Value = dataDic[roomID].IsSpecial == "true" ? "是" : "否";
                datarow.Cells[index++].Value = dataDic[roomID].CreatedName;
                List<string> equipment = new List<string>();
                int height = 25;
                foreach (MeetingRoomEqipmentRecord ep in dataDic[roomID].EquipmentList)
                {
                    if (ep.Name == "" && ep.Count == "" && ep.Status == "")
                    {

                    }
                    else
                    {
                        string data = string.Format("名稱: {0}， 數量: {1}， 狀態: {2}", ep.Name, ep.Count, ep.Status);
                        equipment.Add(data);
                        height += 15;
                    }
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
            string unitID = unitDic[cbxUnit.Text];
            EditForm form;
            if (actor.isSysAdmin())
            {
                form = new EditForm("新增", unitID,"","會議室模組管理者");
            }
            else
            {
                form = new EditForm("新增", unitID,"",cbxIdentity.Text);
            }
            form.FormClosed += delegate {
                ReloadDataGridView();
            };
            form.ShowDialog();
        }

        private void updateBtn_Click(object sender, EventArgs e)
        {
            if (dataGridViewX1.SelectedCells.Count > 0) // 避免使用者沒有選擇資料直接點擊按鈕
            {
                int row = dataGridViewX1.SelectedCells[0].RowIndex;
                string unitID = unitDic[cbxUnit.Text];
                string roomID = "" + dataGridViewX1.Rows[row].Tag;
                EditForm form;
                if (actor.isSysAdmin())
                {
                    form = new EditForm("修改", unitID, roomID, "會議室模組管理者");
                }
                else
                {
                    form = new EditForm("修改", unitID, roomID, cbxIdentity.Text);
                }

                form.FormClosed += delegate {
                    ReloadDataGridView();
                };
                form.Text = "修改場地";
                form.ShowDialog();
            }
            
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            if (dataGridViewX1.SelectedCells.Count > 0) // 避免使用者沒有選擇資料直接點擊按鈕
            {
                int row = dataGridViewX1.SelectedCells[0].RowIndex;
                string roomName = "" + dataGridViewX1.Rows[row].Cells[0].Value;
                string roomID = "" + dataGridViewX1.Rows[row].Tag;

                DialogResult result = MsgBox.Show("確定是否刪除" + roomName + "此場地資料", "警告", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    string sql = string.Format("DELETE FROM $ischool.booking.meetingroom WHERE uid = {0}", roomID);
                    UpdateHelper up = new UpdateHelper();
                    try
                    {
                        up.Execute(sql);
                    }
                    catch (Exception ex)
                    {
                        MsgBox.Show(ex.Message);
                        return;
                    }

                    MsgBox.Show("資料刪除成功!");
                    ReloadDataGridView();
                }
            }
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

        private void identityCbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            string identity = cbxIdentity.Text;
            ReloadUnit(identity);
        }

        private void unitCbx_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            ReloadDataGridView();
        }

        public void ReloadUnit(string identity)
        {
            cbxUnit.Items.Clear();

            if (identity == "單位管理員")
            {
                foreach (DAO.UnitRoleInfo unit in actor.getUnitAdminUnits())
                {
                    cbxUnit.Items.Add(unit.Name);
                }
                
            }
            else if (identity == "單位主管")
            {
                foreach(DAO.UnitRoleInfo unit in actor.getBossUnits())
                {
                    cbxUnit.Items.Add(unit.Name);
                }
            }
        }

        private void MeetingRoomManagement_Load(object sender, EventArgs e)
        {
            // Init unitDic 供ReloadDataGridView使用
            AccessHelper access = new AccessHelper();
            List<UDT.MeetingRoomUnit> listUnit = access.Select<UDT.MeetingRoomUnit>();
            foreach (UDT.MeetingRoomUnit unit in listUnit)
            {
                unitDic.Add(unit.Name, unit.UID);
            }

            unitDic.Add("--未指定--", "");

            // Init 畫面
            this.decorator = new RoleUnitDecorator(this.lblSysAdminRole, this.cbxIdentity, this.cbxUnit ,true);

        }

    }    
}
