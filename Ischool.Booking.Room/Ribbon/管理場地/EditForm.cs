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
        string _unitID;
        string _roomID;
        string _mode;

        public EditForm(string mode,string ID)
        {
            InitializeComponent();

            _mode = mode;

            if (mode == "新增")
            {
                _unitID = ID;
            }
            if (mode == "修改")
            {
                _roomID = ID;

                #region Init
                AccessHelper access = new AccessHelper();
                List<UDT.MeetingRoom> roomList = access.Select<UDT.MeetingRoom>("uid = "+ _roomID);
                List<UDT.MeetingRoomEquipment> equipmentList = access.Select<UDT.MeetingRoomEquipment>("ref_meetingroom_id = "+ _roomID);

                roomNameTbx.Text = roomList[0].Name;
                buildingTbx.Text = roomList[0].Building;
                capacityTbx.Text = "" + roomList[0].Capacity;
                isSpecialCbx.Checked = roomList[0].IsSpecial;
                _unitID = "" + roomList[0].RefUnitID;

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

        private void saveBtn_Click(object sender, EventArgs e)
        {
            #region 資料驗證
            if (errorText.Visible || roomNameTbx.Text == "" || buildingTbx.Text == "" || capacityTbx.Text == "")
            {
                MessageBox.Show(errorText.Text);
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
                MessageBox.Show(error);
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
    ,'{1}' ::TEXT AS building
    ,{2} ::INTEGER AS capacity
    ,{3} ::BIGINT AS ref_unit_id 
    ,'{4}' ::BOOLEAN AS is_special
    ,'{5}' ::TIMESTAMP AS create_time
                ", roomNameTbx.Text, buildingTbx.Text, capacityTbx.Text, _unitID, isSpecialCbx.Checked, DateTime.Now.ToShortDateString());


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

                string equipmentDataRow = string.Join(" UNION ALL ", equipmentDataList);

                #endregion

                #region SQL
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
                #endregion
            }

            if (_mode == "修改")
            {
                #region 資料整理
                string roomData = string.Format(@"
SELECT
    {0}::BIGINT AS uid
    ,'{1}'::TEXT AS name
    ,'{2}' ::TEXT AS building
    ,{3} ::INTEGER AS capacity
    ,{4} ::BIGINT AS ref_unit_id 
    ,'{5}' ::BOOLEAN AS is_special
    ,'{6}' ::TIMESTAMP AS create_time
                ",_roomID, roomNameTbx.Text, buildingTbx.Text, capacityTbx.Text, _unitID, isSpecialCbx.Checked, DateTime.Now.ToShortDateString());

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
                    ",row.Tag ,row.Cells[0].Value, row.Cells[1].Value, row.Cells[2].Value);

                        equipmentDataList.Add(equipmentData);
                    }
                }

                string equipmentDataRow = string.Join(" UNION ALL ", equipmentDataList);

                #endregion

                #region SQL
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
        ,building = meetingroom_data.building
        ,capacity = meetingroom_data.capacity
        ,ref_unit_id = meetingroom_data.ref_unit_id
        ,is_special = meetingroom_data.is_special
        ,create_time = meetingroom_data.create_time
    FROM
        meetingroom_data
    WHERE
         $ischool.booking.meetingroom.uid = meetingroom_data.uid
) 
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
                    
                    ", roomData, equipmentDataRow);
                #endregion
            }


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
            MessageBox.Show("儲存成功!");
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
    }
}
