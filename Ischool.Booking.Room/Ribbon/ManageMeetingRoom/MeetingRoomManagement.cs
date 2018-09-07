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
        private Dictionary<string, string> _dicUnitIDByName = new Dictionary<string, string>();
        private AccessHelper _access = new AccessHelper();
        private Dictionary<string, UserIdentity> _dicIdentityByDescription = new Dictionary<string, UserIdentity>();
        private Actor actor = Actor.Instance;
        private RoleUnitDecorator decorator;

        public MeetingRoomManagement()
        {
            InitializeComponent();
        }

        private void MeetingRoomManagement_Load(object sender, EventArgs e)
        {
            List<UDT.MeetingRoomUnit> listUnit = this._access.Select<UDT.MeetingRoomUnit>();
            foreach (UDT.MeetingRoomUnit unit in listUnit)
            {
                _dicUnitIDByName.Add(unit.Name, unit.UID);
            }

            _dicUnitIDByName.Add("--未指定--", "");

            // Init 畫面
            this.decorator = new RoleUnitDecorator(this.lblSysAdminRole, this.cbxIdentity, this.cbxUnit, true);
            // 紀錄UserIdentity列舉
            this._dicIdentityByDescription.Add(EnumDescription.GetIdentityDescription(typeof(UserIdentity),UserIdentity.ModuleAdmin.ToString()),UserIdentity.ModuleAdmin);
            this._dicIdentityByDescription.Add(EnumDescription.GetIdentityDescription(typeof(UserIdentity), UserIdentity.UnitBoss.ToString()), UserIdentity.UnitBoss);
            this._dicIdentityByDescription.Add(EnumDescription.GetIdentityDescription(typeof(UserIdentity), UserIdentity.UnitAdmin.ToString()), UserIdentity.UnitAdmin);
        }

        public void ReloadDataGridView()
        {
            dataGridViewX1.Rows.Clear();

            string unitID = _dicUnitIDByName[cbxUnit.Text];

            Dictionary<string, MeetingRoomRecord> dataDic = BookingRecord.SelectMeetingRoomByUnitID(unitID);

            this.SuspendLayout();
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
                datarow.Tag = dataDic[roomID].UID; // 會議室系統編號
                dataGridViewX1.Rows.Add(datarow);
                //dataGridViewX1.AutoResizeRow(rowIndex);
                dataGridViewX1.Rows[rowIndex].Height = height;
                rowIndex++;
            }
            this.ResumeLayout();
        }

        private void ReloadUnit(string identity)
        {
            cbxUnit.Items.Clear();

            if (identity == EnumDescription.GetIdentityDescription(typeof(UserIdentity), UserIdentity.UnitAdmin.ToString())/*"單位管理員"*/)
            {
                foreach (DAO.UnitRoleInfo unit in actor.getUnitAdminUnits())
                {
                    cbxUnit.Items.Add(unit.Name);
                }

            }
            else if (identity == EnumDescription.GetIdentityDescription(typeof(UserIdentity), UserIdentity.UnitBoss.ToString())/*"單位主管"*/)
            {
                foreach (DAO.UnitRoleInfo unit in actor.getBossUnits())
                {
                    cbxUnit.Items.Add(unit.Name);
                }
            }
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            string unitID = _dicUnitIDByName[cbxUnit.Text];
            EditForm form;
            if (actor.isSysAdmin())
            {
                form = new EditForm(FormMode.Add, unitID,"", UserIdentity.ModuleAdmin);
            }
            else
            {
                form = new EditForm(FormMode.Add, unitID,"",this._dicIdentityByDescription[cbxIdentity.SelectedItem.ToString()]/*(UserIdentity)int.Parse(cbxIdentity.SelectedItem.ToString())*/);
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
                string unitID = _dicUnitIDByName[((DAO.UnitRoleInfo)cbxUnit.SelectedItem).Name];
                string roomID = "" + dataGridViewX1.Rows[row].Tag;
                EditForm form;
                if (actor.isSysAdmin())
                {
                    form = new EditForm(FormMode.Update, unitID, roomID, UserIdentity.ModuleAdmin);
                }
                else
                {
                    form = new EditForm(FormMode.Update, unitID, roomID, this._dicIdentityByDescription[cbxIdentity.SelectedItem.ToString()]);
                }

                form.FormClosed += delegate {
                    ReloadDataGridView();
                };
                form.Text = "修改會議室";
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

                DialogResult result = MsgBox.Show("確定是否刪除" + roomName + "此會議室資料", "警告", MessageBoxButtons.YesNo);

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

        private void identityCbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            string identity = cbxIdentity.SelectedItem.ToString(); //cbxIdentity.Text;
            ReloadUnit(identity);
        }

        private void unitCbx_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            ReloadDataGridView();
        }

        private void leaveBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }    
}
