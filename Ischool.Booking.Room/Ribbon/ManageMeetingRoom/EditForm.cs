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
using K12.Data;

namespace Ischool.Booking.Room
{
    public partial class EditForm : BaseForm
    {
        private string _unitID;
        private string _roomID;
        private FormMode _mode;
        private UserIdentity _identity;

        private Dictionary<string, string> _dicUnitIDByName = new Dictionary<string, string>();
        private Dictionary<string, List<string>> _dicRoomNameByUnitID = new Dictionary<string, List<string>>();
        private Actor actor = Actor.Instance;
        private AccessHelper _access = new AccessHelper();

        public EditForm(FormMode mode,string unitID,string roomID,UserIdentity identity)
        {
            InitializeComponent();

            this._unitID = unitID;
            this._roomID = roomID;
            this._mode = mode;
            this._identity = identity;
        }

        private void EditForm_Load(object sender, EventArgs e)
        {
            List<UDT.MeetingRoomUnit> unitList = this._access.Select<UDT.MeetingRoomUnit>();
            foreach (UDT.MeetingRoomUnit unit in unitList)
            {
                _dicUnitIDByName.Add(unit.Name, unit.UID);

                if (!this._dicRoomNameByUnitID.ContainsKey("" + unit.UID))
                {
                    this._dicRoomNameByUnitID.Add("" + unit.UID, new List<string>());
                }
            }

            List<UDT.MeetingRoom> listRoom = this._access.Select<UDT.MeetingRoom>();
            foreach (UDT.MeetingRoom data in listRoom)
            {
                //if (!this._dicRoomNameByUnitID.ContainsKey("" + data.RefUnitID))
                //{
                //    this._dicRoomNameByUnitID.Add("" + data.RefUnitID,new List<string>());
                //}
                this._dicRoomNameByUnitID["" + data.RefUnitID].Add(data.Name);
            }

            if (this._mode == FormMode.Add)
            {
                cbxStatus.SelectedIndex = 0;
                ReloadUnitCbx();
            }
            if (this._mode == FormMode.Update)
            {
                ReloadUnitCbx();

                #region Init

                List<UDT.MeetingRoom> listMeetingRoom = this._access.Select<UDT.MeetingRoom>("uid = " + _roomID);
                List<UDT.MeetingRoomEquipment> equipmentList = this._access.Select<UDT.MeetingRoomEquipment>("ref_meetingroom_id = " + _roomID);

                this._dicRoomNameByUnitID[this._unitID].Remove(listMeetingRoom[0].Name); // 移除修改項目的名稱，避免驗證出名稱重複

                tbxRoomName.Text = listMeetingRoom[0].Name;
                tbxBuilding.Text = listMeetingRoom[0].Building;
                tbxCapacity.Text = "" + listMeetingRoom[0].Capacity;
                cbxStatus.SelectedIndex = ("" + listMeetingRoom[0].Status) == "開放" ? 0 : 1;
                ckbxIsSpecial.Checked = listMeetingRoom[0].IsSpecial;
                _unitID = "" + listMeetingRoom[0].RefUnitID;
                pictureBox1.ImageLocation = "" + listMeetingRoom[0].Picture;
                if (("" + listMeetingRoom[0].Picture) != "")
                {
                    try
                    {
                        System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                        System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls;
                        pictureBox1.Load();
                    }
                    catch (Exception ex)
                    {
                        MsgBox.Show(ex.Message);
                    }

                }

                tbxPictureURL.Text = "" + listMeetingRoom[0].Picture;

                foreach (UDT.MeetingRoomEquipment equipment in equipmentList)
                {
                    DataGridViewRow datarow = new DataGridViewRow();
                    datarow.CreateCells(dataGridViewX1);

                    int index = 0;
                    datarow.Cells[index++].Value = equipment.Name;
                    datarow.Cells[index++].Value = equipment.Count;
                    datarow.Cells[index++].Value = equipment.Status;
                    datarow.Tag = equipment.UID;

                    dataGridViewX1.Rows.Add(datarow);
                }

                #endregion
            }
        }

        public void ReloadUnitCbx()
        {
            if (this._identity == UserIdentity.ModuleAdmin/*"會議室模組管理者"*/)
            {
                int index = 0;
                int n = 0;
                foreach (DAO.UnitRoleInfo unit in actor.getSysAdminUnits())
                {
                    if (_unitID == unit.ID)
                    {
                        index = n;
                    }
                    cbxUnit.Items.Add(unit.Name);
                    n++;
                }
                if (cbxUnit.Items.Count > 0)
                {
                    cbxUnit.SelectedIndex = index;
                }
            }
            else if (this._identity == UserIdentity.UnitAdmin/*"單位管理員"*/)
            {
                int index = 0;
                int n = 0;
                foreach (DAO.UnitRoleInfo unit in actor.getUnitAdminUnits())
                {
                    if (_unitID == unit.ID)
                    {
                        index = n;
                    }
                    cbxUnit.Items.Add(unit.Name);
                    n++;
                }
                if (cbxUnit.Items.Count > 0)
                {
                    cbxUnit.SelectedIndex = index;
                }
            }
            else if (this._identity == UserIdentity.UnitBoss/*"單位主管"*/)
            {
                int index = 0;
                int n = 0;
                foreach (DAO.UnitRoleInfo unit in actor.getBossUnits())
                {
                    if (_unitID == unit.ID)
                    {
                        index = n;
                    }
                    cbxUnit.Items.Add(unit.Name);
                    n++;
                }
                if (cbxUnit.Items.Count > 0)
                {
                    cbxUnit.SelectedIndex = 0;
                }
            }
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            bool isValidateSuccess;

            #region 資料驗證
            if (tbxRoomName_Validate() && tbxBuilding_Validate() && tbxCapacity_Validate())
            {
                isValidateSuccess = true;
                foreach (DataGridViewRow dgvrow in dataGridViewX1.Rows)
                {
                    if (dgvrow.Index == dataGridViewX1.Rows.Count - 1)
                    {
                        break;
                    }
                    if (!dgvEquipName_Validate(dgvrow.Index,"" + dgvrow.Cells[0].Value) || !dgvEquipCount_Validate(dgvrow.Index, "" + dgvrow.Cells[1].Value))
                    {
                        isValidateSuccess = false;
                    }
                }
            }
            else
            {
                isValidateSuccess = false;
            }
            #endregion

            if (isValidateSuccess)
            {
                if (_mode == FormMode.Add)
                {
                    try
                    {
                        DAO.MeetingRoom.InsertMeetingRoom(tbxRoomName.Text.Trim(), tbxBuilding.Text.Trim(), tbxCapacity.Text.Trim(), this._unitID == "" ? "null" : _unitID, "" + ckbxIsSpecial.Checked, tbxPictureURL.Text.Trim(), cbxStatus.SelectedItem == null ? "" : cbxStatus.SelectedItem.ToString(), dataGridViewX1);
                        MsgBox.Show("儲存成功!");
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MsgBox.Show(ex.Message);
                    }
                }
                if (_mode == FormMode.Update)
                {
                    try
                    {
                        string unitID = _dicUnitIDByName[cbxUnit.SelectedItem.ToString()];
                        DAO.MeetingRoom.UpdateMeetingRoom(this._roomID, tbxRoomName.Text.Trim(), tbxBuilding.Text.Trim(), tbxCapacity.Text.Trim(), unitID, "" + ckbxIsSpecial.Checked, tbxPictureURL.Text.Trim(), cbxStatus.SelectedItem == null ? "" : cbxStatus.SelectedItem.ToString(), dataGridViewX1);
                        MsgBox.Show("儲存成功!");
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MsgBox.Show(ex.Message);
                    }
                }
            }
        }

        private bool tbxRoomName_Validate()
        {
            if (string.IsNullOrEmpty(tbxRoomName.Text.Trim()))
            {
                errorProvider1.SetError(tbxRoomName, "不可空白!");
                return false;
            }
            else
            {
                if (this._dicRoomNameByUnitID[this._dicUnitIDByName[cbxUnit.SelectedItem.ToString()]].Contains(tbxRoomName.Text.Trim()))
                {
                    errorProvider1.SetError(tbxRoomName, "會議室名稱重複!");
                    return false;
                }
                else
                {
                    errorProvider1.SetError(tbxRoomName, null);
                    return true;
                }
            }
        }

        private bool tbxBuilding_Validate()
        {
            if (string.IsNullOrEmpty(tbxBuilding.Text.Trim()))
            {
                errorProvider1.SetError(tbxBuilding, "所屬大樓欄位空白!");
                return false;
            }
            else
            {
                errorProvider1.SetError(tbxBuilding,null);
                return true;
            }
        }

        private bool tbxCapacity_Validate()
        {
            int n = 0;
            if (string.IsNullOrEmpty(tbxCapacity.Text.Trim()))
            {
                errorProvider1.SetError(tbxCapacity, "不可空白!");
                return false;
            }
            else 
            {
                if (!int.TryParse(tbxCapacity.Text, out n))
                {
                    errorProvider1.SetError(tbxCapacity, "只允許填入數值!");
                    return false;
                }
                else
                {
                    errorProvider1.SetError(tbxCapacity,null);
                    return true;
                }
            }
        }

        private void roomNameTbx_TextChanged(object sender, EventArgs e)
        {
            tbxRoomName_Validate();
        }

        private void buildingTbx_TextChanged(object sender, EventArgs e)
        {
            tbxBuilding_Validate();
        }

        private void capacityTbx_TextChanged(object sender, EventArgs e)
        {
            tbxCapacity_Validate();
        }

        private void pictureURLTbx_TextChanged(object sender, EventArgs e)
        {
            //System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls;
            try
            {
                pictureBox1.Load(tbxPictureURL.Text);
                errorProvider1.SetError(tbxPictureURL, null);
            }
            catch (Exception ex)
            {
                errorProvider1.SetError(tbxPictureURL, ex.Message);
                //MsgBox.Show(ex.Message);
            }
        }

        private bool dgvEquipName_Validate(int rowIndex,string equipName)
        {
            if (string.IsNullOrEmpty(equipName))
            {
                dataGridViewX1.Rows[rowIndex].Cells[0].ErrorText = "不可空白!";
                return false;
            }
            else
            {
                dataGridViewX1.Rows[rowIndex].Cells[0].ErrorText = null;
                return true;
            }
        }

        private bool dgvEquipCount_Validate(int rowIndex,string cellValue)
        {
            int n = 0;

            if (!int.TryParse(cellValue, out n))
            {
                dataGridViewX1.Rows[rowIndex].Cells[1].ErrorText = "只允許填入數值!";
                return false;
            }
            else
            {
                dataGridViewX1.Rows[rowIndex].Cells[1].ErrorText = null;
                return true;
            }
        }

        private void dataGridViewX1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                string cellValue = "" + dataGridViewX1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

                if (e.ColumnIndex == 0)
                {
                    dgvEquipName_Validate(e.RowIndex, cellValue);
                }
                if (e.ColumnIndex == 1)
                {
                    dgvEquipCount_Validate(e.RowIndex, cellValue);
                }
            }
        }

        private void leaveBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
