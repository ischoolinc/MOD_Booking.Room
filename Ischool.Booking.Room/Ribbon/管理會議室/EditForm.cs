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
        private string _mode;
        private string _identity;

        Dictionary<string, string> _unitNameDic = new Dictionary<string, string>();

        Dictionary<string, UDT.MeetingRoomUnit> dicUnit = new Dictionary<string, UDT.MeetingRoomUnit>();

        /// <summary>
        /// 使用者身分
        /// </summary>
        Actor actor = Actor.Instance;

        public EditForm(string mode,string unitID,string roomID,string identity)
        {
            InitializeComponent();

            this._unitID = unitID;
            this._roomID = roomID;
            this._mode = mode;
            this._identity = identity;
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            #region 資料驗證
            if (picError != "")
            {
                DialogResult result = MsgBox.Show("場地照片URL錯誤，使否繼續儲存作業?","提醒",MessageBoxButtons.YesNo);
                if (result == DialogResult.No) 
                {
                    return;
                }
            }
            if (errorText.Visible)
            {
                MsgBox.Show(errorText.Text);
                return;
            }
            if (roomNameTbx.Text == "" )
            {
                MsgBox.Show("場地名稱欄位空白!");
                return;
            }
            if (buildingTbx.Text == "")
            {
                MsgBox.Show("所屬大樓欄位空白!");
                return;
            }
            if (capacityTbx.Text == "")
            {
                MsgBox.Show("容納人數欄位空白!");
                return;
            }
            string error = "";
            foreach (DataGridViewRow datarow in dataGridViewX1.Rows)
            {
                for (int i = 0; i < 2; i++)
                {
                    if (datarow.Cells[i].ErrorText != "")
                    {
                        error = "設備資料填寫錯誤，無法儲存!";
                    }
                }
            }
            if (error != "")
            {
                MsgBox.Show(error);
                return;
            }
            #endregion

            string sql = "";
            if (_mode == "新增")
            {
                #region 資料整理
                string roomData = string.Format(@"
SELECT
    '{0}'::TEXT AS name
    ,'{1}'::TEXT AS building
    ,{2} ::INTEGER AS capacity
    ,{3} ::BIGINT AS ref_unit_id 
    ,'{4}'::BOOLEAN AS is_special
    ,'{5}'::TIMESTAMP AS create_time
    ,'{6}'::TEXT AS picture
    ,'{7}'::TEXT AS created_by
    ,'{8}'::TEXT AS status
                ", roomNameTbx.Text, buildingTbx.Text, capacityTbx.Text, _unitID == "" ? "null" : _unitID, isSpecialCbx.Checked, DateTime.Now.ToShortDateString(),pictureURLTbx.Text,Actor.Account,cbxStatus.SelectedItem == null ? "" : cbxStatus.SelectedItem.ToString());


                List<string> equipmentDataList = new List<string>();

                foreach (DataGridViewRow row in dataGridViewX1.Rows)
                {
                    if (row.Cells[0].Value != null)
                    {
                        string equipmentData = string.Format(@"
SELECT
    '{0}'::TEXT AS name
    ,{1} ::INTEGER AS count
    ,'{2}' ::TEXT AS status
                    ", row.Cells[0].Value, row.Cells[1].Value, row.Cells[2].Value);

                        equipmentDataList.Add(equipmentData);
                    }
                }
                #endregion

                #region SQL
                if (equipmentDataList.Count != 0)
                {
                    string equipmentDataRow = string.Join(" UNION ALL ", equipmentDataList);

                    sql = string.Format(@"
WITH meetingroom_data AS(
    {0}
) ,equipment_data AS(
    {1}
) ,insert_meetingroom AS(
    INSERT INTO $ischool.booking.meetingroom(
        name
        , building
        , capacity
        , ref_unit_id
        , is_special
        , create_time
        , picture
        , created_by
        , status
    )
    SELECT
        *
    FROM
        meetingroom_data
    RETURNING $ischool.booking.meetingroom.*
) 
INSERT INTO $ischool.booking.meetingroom_equipment(
    name
    , count
    , status
    , ref_meetingroom_id
)
SELECT
    equipment_data.name
    ,equipment_data.count
    ,equipment_data.status
    , (SELECT uid FROM insert_meetingroom)
FROM
    equipment_data

                ", roomData, equipmentDataRow);
                }

                else
                {
                    sql = string.Format(@"
WITH meetingroom_data AS(
    {0}
)
INSERT INTO $ischool.booking.meetingroom(
    name
    , building
    , capacity
    , ref_unit_id
    , is_special
    , create_time
    , picture
    , created_by
    , status
)
SELECT
    *
FROM
    meetingroom_data
                ", roomData);
                }
                #endregion
            }

            if (_mode == "修改")
            {
                #region 資料整理

                string unitID = _unitNameDic[unitCbx.Text];

                string roomData = string.Format(@"
SELECT
    {0}::BIGINT AS uid
    ,'{1}'::TEXT AS name
    ,'{2}'::TEXT AS building
    ,{3} ::INTEGER AS capacity
    ,{4} ::BIGINT AS ref_unit_id 
    ,'{5}'::BOOLEAN AS is_special
    ,'{6}'::TIMESTAMP AS create_time
    ,'{7}'::TEXT AS picture
    ,'{8}'::TEXT AS created_by
    ,'{9}'::TEXT AS status
                ",_roomID, roomNameTbx.Text, buildingTbx.Text, capacityTbx.Text, unitID, isSpecialCbx.Checked, DateTime.Now.ToShortDateString(),pictureURLTbx.Text, Actor.Account, cbxStatus.SelectedItem == null ? "" : cbxStatus.SelectedItem.ToString());

                List<string> equipmentDataList = new List<string>();

                foreach (DataGridViewRow row in dataGridViewX1.Rows)
                {
                    if (row.Cells[0].Value != null)
                    {
                        string equipmentData = string.Format(@"
SELECT
    {0}::BIGINT AS uid
    ,'{1}'::TEXT AS name
    ,{2} ::INTEGER AS count
    ,'{3}' ::TEXT AS status
    ,{4}::BIGINT AS ref_meetingroom_id
                    ",("" + row.Tag ) == "" ? "null" : row.Tag ,row.Cells[0].Value, row.Cells[1].Value, row.Cells[2].Value, _roomID);

                        equipmentDataList.Add(equipmentData);
                    }
                }

                #endregion

                #region SQL
                // 有設備資料
                if (equipmentDataList.Count != 0)
                {
                    string equipmentDataRow = string.Join(" UNION ALL ", equipmentDataList);

                    sql = string.Format(@"
WITH meetingroom_data AS(
    {0}
) ,equipment_data AS(
    {1}
) ,update_meetingroom AS(
    UPDATE
        $ischool.booking.meetingroom
    SET
       name = meetingroom_data.name
        , building = meetingroom_data.building
        , capacity = meetingroom_data.capacity
        , ref_unit_id = meetingroom_data.ref_unit_id
        , is_special = meetingroom_data.is_special
        , create_time = meetingroom_data.create_time
        , picture = meetingroom_data.picture
        , created_by = meetingroom_data.created_by
        , status = meetingroom_data.status
    FROM
        meetingroom_data
    WHERE
         $ischool.booking.meetingroom.uid = meetingroom_data.uid
) ,update_equipment_data AS(
    UPDATE 
        $ischool.booking.meetingroom_equipment
    SET
        name = equipment_data.name
        ,count = equipment_data.count
        ,status = equipment_data.status
    FROM
        equipment_data
    WHERE
        $ischool.booking.meetingroom_equipment.uid = equipment_data.uid
) ,insert_equipment_data AS(
    INSERT INTO $ischool.booking.meetingroom_equipment(
        name
        , count
        , status
        , ref_meetingroom_id
    )
    SELECT
        name
        , count
        , status
        , ref_meetingroom_id
    FROM
        equipment_data
    WHERE
        equipment_data.uid IS NULL
) ,delete_equipment_data AS(
    SELECT
        equipment.uid
    FROM(
        SELECT
            equipment.*
        FROM
            meetingroom_data
            LEFT OUTER JOIN $ischool.booking.meetingroom_equipment AS equipment
                ON meetingroom_data.uid = equipment.ref_meetingroom_id
    ) equipment        
        LEFT OUTER JOIN equipment_data
            ON equipment.uid = equipment_data.uid
    WHERE
        equipment_data.uid IS NULL
)   
    DELETE FROM $ischool.booking.meetingroom_equipment WHERE uid IN(SELECT * FROM delete_equipment_data)
                    ", roomData, equipmentDataRow);
                }
                // 沒有設備資料
                else
                {
                    sql = string.Format(@"
WITH meetingroom_data AS(
    {0}
) , update_meetingroom_data AS(
    UPDATE
        $ischool.booking.meetingroom
    SET
        name = meetingroom_data.name
        , building = meetingroom_data.building
        , capacity = meetingroom_data.capacity
        , ref_unit_id = meetingroom_data.ref_unit_id
        , is_special = meetingroom_data.is_special
        , create_time = meetingroom_data.create_time
        , picture = meetingroom_data.picture
        , created_by = meetingroom_data.created_by
        , status = meetingroom_data.status
    FROM
        meetingroom_data
    WHERE
        $ischool.booking.meetingroom.uid = meetingroom_data.uid 
) , delete_equipment_data AS(
    SELECT
        equipment.uid
    FROM
        meetingroom_data
        LEFT OUTER JOIN $ischool.booking.meetingroom_equipment AS equipment
            ON meetingroom_data.uid = equipment.ref_meetingroom_id
)
DELETE 
FROM 
    $ischool.booking.meetingroom_equipment 
WHERE 
    uid IN(
        SELECT * FROM delete_equipment_data
    )
                    ", roomData);
                }
                #endregion
            }


            UpdateHelper up = new UpdateHelper();
            try
            {
                up.Execute(sql);
            }
            catch(Exception ex)
            {
                MsgBox.Show(ex.Message);
                return;
            }
            MsgBox.Show("儲存成功!");
            this.Close();
        }

        private void leaveBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void roomNameTbx_TextChanged(object sender, EventArgs e)
        {
            if (roomNameTbx.Text == "")
            {
                errorText.Visible = true;
                errorText.Text = "場地名稱欄位空白!";
            }
            else
            {
                errorText.Visible = false;
            }
        }

        private void buildingTbx_TextChanged(object sender, EventArgs e)
        {
            if (buildingTbx.Text == "")
            {
                errorText.Visible = true;
                errorText.Text = "所屬大樓欄位空白!";
            }
            else
            {
                errorText.Visible = false;
            }
        }

        private void capacityTbx_TextChanged(object sender, EventArgs e)
        {
            int number = 0;
            if (capacityTbx.Text != "")
            {
                if (!int.TryParse(capacityTbx.Text, out number))
                {
                    errorText.Visible = true;
                    errorText.Text = "容納人數欄位只允許填入數值!";
                }
                else
                {
                    errorText.Visible = false;
                }
            }
            else if (capacityTbx.Text == "")
            {
                errorText.Visible = true;
                errorText.Text = "容納人數欄位空白!";
            }
            else
            {
                errorText.Visible = false;
            }
        }

        private void dataGridViewX1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0  && e.ColumnIndex >= 0)
            {
                string cellValue = "" + dataGridViewX1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

                if (e.ColumnIndex == 0)
                {
                    if (cellValue == "")
                    {
                        dataGridViewX1.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "欄位空白!";
                    }
                    else
                    {
                        dataGridViewX1.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = null;
                    }
                }
                if (e.ColumnIndex == 1)
                {
                    int number = 0;
                    
                    if (!int.TryParse(cellValue, out number))
                    {
                        dataGridViewX1.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "只允許填入數值!";
                    }
                    else
                    {
                        dataGridViewX1.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = null;
                    }
                }
            }
        }

        string picError = "";
        private void pictureURLTbx_TextChanged(object sender, EventArgs e)
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls; 
            try
            {
                //pictureBox1.ImageLocation = ;
                pictureBox1.Load(pictureURLTbx.Text);
                picError = "";
            }
            catch(Exception ex)
            {
                MsgBox.Show(ex.Message);
                picError = ex.Message;
            }
            
        }

        private void EditForm_Load(object sender, EventArgs e)
        {
            cbxStatus.SelectedIndex = 0;
            // 整理所有管理單位 名稱與編號 供 unitCbx 使用
            AccessHelper access = new AccessHelper();
            List<UDT.MeetingRoomUnit> unitList = access.Select<UDT.MeetingRoomUnit>();

            foreach (UDT.MeetingRoomUnit unit in unitList)
            {
                _unitNameDic.Add(unit.Name, unit.UID);

                dicUnit.Add(unit.UID,unit);
            }

            if (_mode == "新增")
            {
                ReloadUnitCbx();
            }
            if (_mode == "修改")
            {
                //_roomID = _unitID;

                ReloadUnitCbx();

                #region Init

                List<UDT.MeetingRoom> roomList = access.Select<UDT.MeetingRoom>("uid = " + _roomID);
                List<UDT.MeetingRoomEquipment> equipmentList = access.Select<UDT.MeetingRoomEquipment>("ref_meetingroom_id = " + _roomID);

                roomNameTbx.Text = roomList[0].Name;
                buildingTbx.Text = roomList[0].Building;
                capacityTbx.Text = "" + roomList[0].Capacity;
                isSpecialCbx.Checked = roomList[0].IsSpecial;
                _unitID = "" + roomList[0].RefUnitID;
                pictureBox1.ImageLocation = "" + roomList[0].Picture;
                if (("" + roomList[0].Picture) != "")
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

                pictureURLTbx.Text = "" + roomList[0].Picture;

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
            if (this._identity == "會議室模組管理者")
            {
                int index = 0;
                int n = 0;
                foreach (DAO.UnitRoleInfo unit in actor.getSysAdminUnits())
                {
                    if (_unitID == unit.ID)
                    {
                        index = n;
                    }
                    unitCbx.Items.Add(unit.Name);
                    n++;
                }
                if (unitCbx.Items.Count > 0)
                {
                    unitCbx.SelectedIndex = index;
                }
            }
            else if (this._identity == "單位管理員")
            {
                int index = 0;
                int n = 0;
                foreach (DAO.UnitRoleInfo unit in actor.getUnitAdminUnits())
                {
                    if (_unitID == unit.ID)
                    {
                        index = n;
                    }
                    unitCbx.Items.Add(unit.Name);
                    n++;
                }
                if (unitCbx.Items.Count > 0)
                {
                    unitCbx.SelectedIndex = index;
                }
            }
            else if (this._identity == "單位主管")
            {
                int index = 0;
                int n = 0;
                foreach (DAO.UnitRoleInfo unit in actor.getBossUnits())
                {
                    if (_unitID == unit.ID)
                    {
                        index = n;
                    }
                    unitCbx.Items.Add(unit.Name);
                    n++;
                }
                if (unitCbx.Items.Count > 0)
                {
                    unitCbx.SelectedIndex = 0;
                }
            }
        }
    }
}
