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
using FISCA.Data;

namespace Ischool.Booking.Room
{
    public partial class ReviewForm : BaseForm
    {
        /// <summary>
        /// Name / UID
        /// </summary>
        Dictionary<string, string> unitDic = new Dictionary<string, string>();

        /// <summary>
        /// Name/ID
        /// </summary>
        Dictionary<string, string> meetingroomDic = new Dictionary<string, string>();

        /// <summary>
        /// 管理單位ID
        /// </summary>
        public string _unitID { get; set; }

        /// <summary>
        /// Winform是否載入完成
        /// </summary>
        public bool initFinish { get; set; }

        public ReviewForm()
        {
            initFinish = false;
            InitializeComponent();

            string identity = Actor.Identity;
            actorLb.Text = identity;

            // Init 日期區間
            starTime.Text = DateTime.Now.ToShortDateString();
            endTime.Text = DateTime.Now.AddDays(7).ToShortDateString();
            
            AccessHelper access = new AccessHelper();
            
            if (identity == "系統管理員")
            {
                unitLb.Visible = false;
                List<UDT.MeetingRoomUnit> unitList = access.Select<UDT.MeetingRoomUnit>();
                foreach (UDT.MeetingRoomUnit unit in unitList)
                {
                    unitCbx.Items.Add(unit.Name);
                    unitDic.Add(unit.Name,unit.UID);
                }
                unitCbx.SelectedIndex = 0;
            }
            if (identity == "單位管理員" || identity == "單位主管")
            {
                unitCbx.Visible = false;

                UnitRecord ur = BookingRecord.SelectUnitByAccount(Actor.Account);
                
                unitLb.Text = ur.Name;
                _unitID = ur.UID;
                ReloadRoomCbx(_unitID);
            }
            initFinish = true;
        }

        public void ReloadRoomCbx(string unitID)
        {
            roomCbx.Items.Clear();
            meetingroomDic.Clear();

            AccessHelper access = new AccessHelper();
            
            List<UDT.MeetingRoom> roomList = access.Select<UDT.MeetingRoom>("ref_unit_id = " + unitID);

            roomCbx.Items.Add("--全部--");
            meetingroomDic.Add("--全部--", "全部");
            foreach (UDT.MeetingRoom room in roomList)
            {
                roomCbx.Items.Add(room.Name);
                meetingroomDic.Add(room.Name,room.UID);
            }

            roomCbx.SelectedIndex = 0;
            string roomName = "" + roomCbx.Items[roomCbx.SelectedIndex];
            string roomID = meetingroomDic[roomName];

            ReloadDataGridView(unitID,roomID);

        }

        /// <summary>
        /// DataGridview重新載入
        /// </summary>
        /// <param 管理單位編號="unitID"></param>
        /// <param 場地編號="roomID"></param>
        /// <param 查詢開始時間="starTime"></param>
        /// <param 查詢結束時間="endTime"></param>
        public void ReloadDataGridView(string unitID,string roomID)
        {
            dataGridViewX1.Rows.Clear();

            #region 取得資料

            #region SQL
            string sql;
            if (roomID == "全部" && !conditionCbx.Checked)
            {
                sql = string.Format(@"
SELECT
    app.*
	,room.name AS room_name
    ,room.is_special
FROM	
	$ischool.booking.meetingroom_application AS app
	LEFT OUTER JOIN $ischool.booking.meetingroom AS room
		ON app.ref_meetingroom_id = room.uid
    LEFT OUTER JOIN $ischool.booking.meetingroom_unit AS unit
        ON room.ref_unit_id = unit.uid
WHERE
    unit.uid = {0}
	AND app.apply_start_date >= '{1}'
	AND app.apply_start_date <= '{2}'
                ", unitID, starTime.Value.ToShortDateString(), endTime.Value.ToShortDateString());
            }
            else if (roomID == "全部" && conditionCbx.Checked)
            {
                sql = string.Format(@"
SELECT
    app.*
	,room.name AS room_name
    ,room.is_special
FROM	
	$ischool.booking.meetingroom_application AS app
	LEFT OUTER JOIN $ischool.booking.meetingroom AS room
		ON app.ref_meetingroom_id = room.uid
    LEFT OUTER JOIN $ischool.booking.meetingroom_unit AS unit
        ON room.ref_unit_id = unit.uid
WHERE
    unit.uid = {0}
	AND app.reviewed_date IS NULL
    AND room.is_special = true
                ", unitID);
            }
            else
            {
                sql = string.Format(@"
SELECT
    app.*
	,room.name AS room_name
    ,room.is_special
FROM	
	$ischool.booking.meetingroom_application AS app
	LEFT OUTER JOIN $ischool.booking.meetingroom AS room
		ON app.ref_meetingroom_id = room.uid
    LEFT OUTER JOIN $ischool.booking.meetingroom_unit AS unit
        ON room.ref_unit_id = unit.uid
WHERE
    unit.uid = {0}
	AND app.ref_meetingroom_id = '{1}'
	AND app.apply_start_date >= '{2}'
	AND app.apply_start_date <= '{3}'
                ", unitID, roomID, starTime.Value.ToShortDateString(), endTime.Value.ToShortDateString());
            }
            #endregion

            QueryHelper qh = new QueryHelper();
            DataTable dt = qh.Select(sql);
            #endregion

            foreach (DataRow row in dt.Rows)
            {
                DataGridViewRow datarow = new DataGridViewRow();
                datarow.CreateCells(dataGridViewX1);
                int index = 0;
                datarow.Cells[index++].Value = DateTime.Parse("" + row["apply_date"]).ToShortDateString();
                datarow.Cells[index++].Value = "" + row["room_name"];
                datarow.Cells[index++].Value = "" + row["applicant_name"];
                datarow.Cells[index++].Value = DateTime.Parse("" + row["apply_start_date"]).ToShortDateString();
                datarow.Cells[index++].Value = DateTime.Parse("" + row["repeat_end_date"]).ToShortDateString();
                datarow.Tag = "" + row["uid"];

                // 是否為特殊場地
                if (bool.Parse("" + row["is_special"]))
                {
                    //審核狀態: 待審核、已審核、無須審核
                    if (("" + row["reviewed_date"]) == "")
                    {
                        datarow.Cells[index++].Value = "待審核";
                    }
                    else
                    {
                        datarow.Cells[index].Value = "已審核";
                        datarow.Cells[index].Style.BackColor = Color.LightGray;
                        datarow.Cells[index++].ReadOnly = true;
                    }
                }
                else
                {
                    datarow.Cells[index].Value = "無須審核";
                    datarow.Cells[index].Style.BackColor = Color.LightGray;
                    datarow.Cells[index++].ReadOnly = true;

                }

                // 申請狀態: 申請核准、申請未核准、""
                if (("" + row["is_approved"]) == "true" && bool.Parse("" + row["is_special"]))
                {
                    datarow.Cells[index++].Value = "申請核准";
                }
                else if (("" + row["is_approved"]) == "false" && bool.Parse("" + row["is_special"]))
                {
                    datarow.Cells[index++].Value = "申請未核准";
                }
                else
                {
                    datarow.Cells[index++].Value = "";
                }
                // 申請結果: 成立、取消
                if (bool.Parse(("" + row["is_canceled"]) == "" ? "false" : ("" + row["is_canceled"])))
                {
                    datarow.Cells[index].Style.BackColor = Color.LightPink;
                    datarow.Cells[index++].Value = "取消";
                }
                else if (!bool.Parse(("" + row["is_approved"]) == "" ? "false" : ("" + row["is_approved"])) && bool.Parse("" + row["is_special"]))
                {
                    datarow.Cells[index].Style.BackColor = Color.LightPink;
                    datarow.Cells[index++].Value = "未成立";
                }
                else
                {
                    datarow.Cells[index].Style.BackColor = Color.LightGreen;
                    datarow.Cells[index++].Value = "成立";
                }
                dataGridViewX1.Rows.Add(datarow);
            } 

        }

        private void roomCbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (initFinish)
            {
                string roomName = "" + roomCbx.Items[roomCbx.SelectedIndex];
                string roomID;
                if (roomName == "--全部--")
                {
                    roomID = "全部";
                }
                else
                {
                    roomID = meetingroomDic[roomName];
                }

                string _unitName = "" + unitCbx.Items[unitCbx.SelectedIndex];
                _unitID = unitDic[_unitName];

                ReloadDataGridView(_unitID, roomID);
            }
        }

        private void unitCbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            string unitName = "" + unitCbx.Items[unitCbx.SelectedIndex];
            _unitID = unitDic[unitName];

            ReloadRoomCbx(_unitID);
        }

        private void leaveBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void starTime_ValueChanged(object sender, EventArgs e)
        {
            RunDateTimeCondition();
        }

        private void endTime_ValueChanged(object sender, EventArgs e)
        {
            RunDateTimeCondition();
        }

        public void RunDateTimeCondition()
        {
            if (initFinish)
            {
                string roomName = "" + roomCbx.Items[roomCbx.SelectedIndex];
                string roomID = meetingroomDic[roomName];

                int result = DateTime.Compare(starTime.Value, endTime.Value);

                if (result > 0)
                {
                    errorLb.Text = "查詢條件錯誤:開始時間不能大於結束時間!";
                    errorLb.Visible = true;
                    dataGridViewX1.Rows.Clear();
                }
                else
                {
                    errorLb.Visible = false;
                    ReloadDataGridView(_unitID, roomID);
                }
            }
        }

        private void dataGridViewX1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != 5)
            {
                string applicationID = "" + dataGridViewX1.Rows[e.RowIndex].Tag;
                ApplicationDetailForm form = new ApplicationDetailForm(applicationID);
                form.ShowDialog();
            }
        }

        private void dataGridViewX1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 5)
            {
                if ("" + dataGridViewX1.Rows[e.RowIndex].Cells[5].Value == "待審核")
                {
                    string applicationID = "" + dataGridViewX1.Rows[e.RowIndex].Tag;
                    ApplicationReviewForm form = new ApplicationReviewForm(applicationID);
                    form.FormClosed += delegate 
                    {
                        string roomName = "" + roomCbx.Items[roomCbx.SelectedIndex];
                        string roomID = meetingroomDic[roomName];
                        ReloadDataGridView(_unitID, roomID);
                    };
                    form.ShowDialog();
                }
                else
                {
                    MessageBox.Show("此申請紀錄無須審核!");
                }
            }
        }

        private void dataGridViewX1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // 顯示右鍵選單
                contextMenuStrip1.Show(dataGridViewX1,new Point(e.X,e.Y));
            }
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            if ("" + dataGridViewX1.SelectedCells[7].Value == "成立")
            {
                string application = "" + dataGridViewX1.SelectedRows[0].Tag;
                ApplicationCancelForm form = new ApplicationCancelForm(application);
                form.FormClosed += delegate 
                {
                    string roomName = "" + roomCbx.Items[roomCbx.SelectedIndex];
                    string roomID = meetingroomDic[roomName];
                    ReloadDataGridView(_unitID, roomID);
                };
                form.ShowDialog();
            }
            else
            {
                MessageBox.Show("此申請紀錄未成立或已取消!");
            }
        }

        private void conditionCbx_CheckedChanged(object sender, EventArgs e)
        {
            if (initFinish)
            {
                if (conditionCbx.Checked)
                {
                    starTime.Enabled = false;
                    endTime.Enabled = false;
                }
                else
                {
                    starTime.Enabled = true;
                    endTime.Enabled = true;
                }

                string roomName = "" + roomCbx.Items[roomCbx.SelectedIndex];
                string roomID = meetingroomDic[roomName];

                ReloadDataGridView(_unitID, roomID);
            }
        }
    }
}
