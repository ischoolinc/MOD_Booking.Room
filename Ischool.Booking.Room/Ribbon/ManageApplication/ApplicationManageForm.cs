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
        private Dictionary<string, string> _dicUnitIDByName = new Dictionary<string, string>();
        private Dictionary<string, string> _dicRoomIDByName = new Dictionary<string, string>();
        private string _unitID;
        private bool _initFinish = false;

        private AccessHelper _access = new AccessHelper();
        private Actor actor = Actor.Instance;
        private RoleUnitDecorator decorator;

        public ReviewForm()
        {            
            InitializeComponent();
        }

        private void ReviewForm_Load(object sender, EventArgs e)
        {
            List<UDT.MeetingRoomUnit> listUnit = this._access.Select<UDT.MeetingRoomUnit>();
            foreach (UDT.MeetingRoomUnit unit in listUnit)
            {
                this._dicUnitIDByName.Add(unit.Name, unit.UID);
            }

            #region Init 日期區間
            starTime.Text = DateTime.Now.ToShortDateString();
            endTime.Text = DateTime.Now.AddDays(7).ToShortDateString(); 
            #endregion

            this.decorator = new RoleUnitDecorator(actorLb, cbxIdentity, cbxUnit);

            _initFinish = true;
        }

        public void ReloadRoomCbx(string unitID)
        {
            cbxRoom.Items.Clear();
            _dicRoomIDByName.Clear();
            
            List<UDT.MeetingRoom> roomList = this._access.Select<UDT.MeetingRoom>(string.Format("ref_unit_id = {0}",unitID));

            cbxRoom.Items.Add("--全部--");
            _dicRoomIDByName.Add("--全部--", "全部");

            foreach (UDT.MeetingRoom room in roomList)
            {
                cbxRoom.Items.Add(room.Name);
                _dicRoomIDByName.Add(room.Name,room.UID);
            }
            if (cbxRoom.Items.Count > 0)
            {
                cbxRoom.SelectedIndex = 0;
            }
            
            string roomName = "" + cbxRoom.Items[cbxRoom.SelectedIndex];
            string roomID = _dicRoomIDByName[roomName];

            ReloadDataGridView(unitID,roomID);
        }

        public void ReloadDataGridView(string unitID,string roomID)
        {
            this.SuspendLayout();
            dataGridViewX1.Rows.Clear();

            // 取得資料
            DataTable dt = DAO.Application.GetApplicationData(ckbxSelectAll.Checked,roomID,unitID,starTime.Value.ToString("yyyy/MM/dd"),endTime.Value.ToString("yyyy/MM/dd"));

            foreach (DataRow row in dt.Rows)
            {
                DataGridViewRow datarow = new DataGridViewRow();
                datarow.CreateCells(dataGridViewX1);
                int index = 0;
                datarow.Cells[index++].Value = DateTime.Parse("" + row["apply_date"]).ToString("yyyy/MM/dd");
                datarow.Cells[index++].Value = "" + row["room_name"];
                datarow.Cells[index++].Value = "" + row["applicant_name"];
                datarow.Cells[index++].Value = DateTime.Parse("" + row["apply_start_date"]).ToString("yyyy/MM/dd");
                datarow.Cells[index++].Value = DateTime.Parse("" + row["repeat_end_date"]).ToString("yyyy/MM/dd");
                datarow.Tag = "" + row["uid"];

                #region 審核狀態 : 待審核、已審核、無須審核
                {
                    Type type = typeof(ReviewStats);
                    if (bool.Parse("" + row["is_special"])) // 是否為特殊場地
                    {
                        if (string.IsNullOrEmpty(("" + row["reviewed_date"])))  // 尚未審查
                        {
                            datarow.Cells[index++].Value = EnumDescription.GetIdentityDescription(type,ReviewStats.Reviewing.ToString()); // 進行審核
                        }
                        else
                        {
                            datarow.Cells[index].Value = EnumDescription.GetIdentityDescription(type,ReviewStats.Reviewed.ToString()); // 已審核
                            datarow.Cells[index].Style.BackColor = Color.LightGray;
                            datarow.Cells[index++].ReadOnly = true;
                        }
                    }
                    else
                    {
                        datarow.Cells[index].Value = EnumDescription.GetIdentityDescription(type,ReviewStats.None.ToString()); // 無
                        datarow.Cells[index].Style.BackColor = Color.LightGray;
                        datarow.Cells[index++].ReadOnly = true;

                    }
                }
                #endregion

                #region 審核結果: 通過、未通過、空白
                {
                    Type type = typeof(ReviewResult);
                    if (("" + row["is_approved"]) == "true" && bool.Parse("" + row["is_special"]))
                    {
                        datarow.Cells[index++].Value = EnumDescription.GetIdentityDescription(type,ReviewResult.Pass.ToString()); // 通過
                    }
                    else if (("" + row["is_approved"]) == "false" && bool.Parse("" + row["is_special"]))
                    {
                        datarow.Cells[index++].Value = EnumDescription.GetIdentityDescription(type,ReviewResult.UnPass.ToString()); // 未通過
                    }
                    else
                    {
                        datarow.Cells[index++].Value = EnumDescription.GetIdentityDescription(type,ReviewResult.Empty.ToString()); // 
                    }
                }
                #endregion

                #region 申請單狀態: 待審核、取消、通過、未通過
                {
                    Type type = typeof(ApplicationStats);
                    if (bool.Parse(("" + row["is_canceled"]) == "" ? "false" : ("" + row["is_canceled"])))
                    {
                        datarow.Cells[index].Style.BackColor = Color.LightGray;
                        datarow.Cells[index++].Value = EnumDescription.GetIdentityDescription(type,ApplicationStats.Cancel.ToString()); // 取消
                    }
                    else if (("" + row["reviewed_date"]) == "" && bool.Parse("" + row["is_special"]))
                    {
                        datarow.Cells[index].Style.BackColor = Color.LightPink;
                        datarow.Cells[index++].Value = EnumDescription.GetIdentityDescription(type,ApplicationStats.WaitReview.ToString()); // 待審核
                    }
                    else if (("" + row["is_approved"]) == "false" && bool.Parse("" + row["is_special"]))
                    {
                        datarow.Cells[index].Style.BackColor = Color.LightPink;
                        datarow.Cells[index++].Value = EnumDescription.GetIdentityDescription(type,ApplicationStats.UnPass.ToString()); // 未通過
                    }
                    else
                    {
                        datarow.Cells[index].Style.BackColor = Color.LightGreen;
                        datarow.Cells[index++].Value = EnumDescription.GetIdentityDescription(type,ApplicationStats.Pass.ToString()); // 通過
                    }
                }
                #endregion

                dataGridViewX1.Rows.Add(datarow);
            }
            this.ResumeLayout();
        }

        private void roomCbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_initFinish)
            {
                string roomName = "" + cbxRoom.Items[cbxRoom.SelectedIndex];
                string roomID;
                if (roomName == "--全部--")
                {
                    roomID = "全部";
                }
                else
                {
                    roomID = _dicRoomIDByName[roomName];
                }

                string _unitName = cbxUnit.Text;
                this._unitID = _dicUnitIDByName[_unitName];

                ReloadDataGridView(this._unitID, roomID);
            }
        }

        private void unitCbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            this._unitID = _dicUnitIDByName[cbxUnit.Text];

            ReloadRoomCbx(this._unitID);
        }

        private void starTime_ValueChanged(object sender, EventArgs e)
        {
            if (this._initFinish)
            {
                string unitID = _dicUnitIDByName[cbxUnit.Text];
                string roomID = _dicRoomIDByName[cbxRoom.Text];

                ReloadDataGridView(unitID, roomID);
            }
        }

        private void endTime_ValueChanged(object sender, EventArgs e)
        {
            if (this._initFinish)
            {
                string unitID = _dicUnitIDByName[cbxUnit.Text];
                string roomID = _dicRoomIDByName[cbxRoom.Text];

                ReloadDataGridView(unitID, roomID);
            }
        }

        private void conditionCbx_CheckedChanged(object sender, EventArgs e)
        {
            if (this._initFinish)
            {
                if (ckbxSelectAll.Checked)
                {
                    starTime.Enabled = false;
                    endTime.Enabled = false;
                }
                else
                {
                    starTime.Enabled = true;
                    endTime.Enabled = true;
                }

                string roomName = "" + cbxRoom.Items[cbxRoom.SelectedIndex];
                string roomID = _dicRoomIDByName[roomName];

                ReloadDataGridView(this._unitID, roomID);
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
                // 審核狀態-進行審核 、申請單狀態-待審核
                if ("" + dataGridViewX1.Rows[e.RowIndex].Cells[5].Value == EnumDescription.GetIdentityDescription(typeof(ReviewStats),ReviewStats.Reviewing.ToString()) && "" + dataGridViewX1.Rows[e.RowIndex].Cells[7].Value == EnumDescription.GetIdentityDescription(typeof(ApplicationStats),ApplicationStats.WaitReview.ToString())) 
                {
                    string applicationID = "" + dataGridViewX1.Rows[e.RowIndex].Tag;
                    ApplicationReviewForm form = new ApplicationReviewForm(applicationID);
                    form.FormClosed += delegate 
                    {
                        if (form.DialogResult == DialogResult.Yes)
                        {
                            string roomName = "" + cbxRoom.Items[cbxRoom.SelectedIndex];
                            string roomID = _dicRoomIDByName[roomName];
                            ReloadDataGridView(_unitID, roomID);
                        }
                    };
                    form.ShowDialog();
                }
                else
                {
                    MsgBox.Show("此申請紀錄無須審核!");
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
            if ("" + dataGridViewX1.SelectedCells[7].Value == EnumDescription.GetIdentityDescription(typeof(ApplicationStats),ApplicationStats.Pass.ToString())) // 申請單狀態-通過
            {
                string application = "" + dataGridViewX1.SelectedRows[0].Tag;
                ApplicationCancelForm form = new ApplicationCancelForm(application);
                form.FormClosed += delegate 
                {
                    if (form.DialogResult == DialogResult.Yes)
                    {
                        string roomName = "" + cbxRoom.Items[cbxRoom.SelectedIndex];
                        string roomID = _dicRoomIDByName[roomName];
                        ReloadDataGridView(_unitID, roomID);
                    }
                };
                form.ShowDialog();
            }
            else
            {
                MsgBox.Show("此申請紀錄未成立或已取消!");
            }
        }

        private void leaveBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

    /// <summary>
    /// 審核狀態列舉
    /// </summary>
    public enum ReviewStats
    {
        [Description("進行審核")]
        Reviewing = 0,
        [Description("已審核")]
        Reviewed = 1,
        [Description("無")]
        None = 2
    }
    /// <summary>
    /// 審核結果列舉
    /// </summary>
    public enum ReviewResult
    {
        [Description("通過")]
        Pass = 0,
        [Description("未通過")]
        UnPass = 1,
        [Description("")]
        Empty = 2
    }
    /// <summary>
    /// 申請單狀態列舉
    /// </summary>
    public enum ApplicationStats
    {
        [Description("待審核")]
        WaitReview = 0,
        [Description("取消")]
        Cancel = 1,
        [Description("通過")]
        Pass = 2,
        [Description("未通過")]
        UnPass = 3
    }
}
