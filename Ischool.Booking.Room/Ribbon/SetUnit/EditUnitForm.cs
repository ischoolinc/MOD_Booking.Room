using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FISCA.Presentation.Controls;
using FISCA.Data;
using K12.Data;
using FISCA.UDT;

namespace Ischool.Booking.Room
{
    public partial class EditUnitForm : BaseForm
    {
        FormMode _mode;
        // 管理單位編號
        private string _unitID;
        private bool _initFinish = false;
        // 紀錄系統已存在管理單位名稱
        private Dictionary<string, UDT.MeetingRoomUnit> _dicUnitDataByName = new Dictionary<string, UDT.MeetingRoomUnit>();
        private AccessHelper _access = new AccessHelper();

        public EditUnitForm(FormMode mode,string unitID)
        {
            InitializeComponent();

            this._mode = mode;
            this._unitID = unitID;
        }

        private void EditUnitForm_Load(object sender, EventArgs e)
        {
            #region 紀錄系統已存在管理單位名稱
            {
                List<UDT.MeetingRoomUnit> unitList = this._access.Select<UDT.MeetingRoomUnit>();
                foreach (UDT.MeetingRoomUnit unit in unitList)
                {
                    this._dicUnitDataByName.Add(unit.Name,unit);
                }
            }
            #endregion

            if (_mode == FormMode.Add)
            {
                dateLb.Text = string.Format("建立日期:   {0}", DateTime.Now.ToString("yyyy/MM/dd"));

                ReloadDataGridview();
            }
            else if (_mode == FormMode.Update)
            {

                DataTable dt = DAO.Unit.GetUnitDataByID(this._unitID);

                tbxUnitName.Text = "" + dt.Rows[0]["unit_name"];
                tbxUnitBoss.Text = "" + dt.Rows[0]["teacher_name"];
                tbxUnitBoss.Tag = dt.Rows[0]; //"" + dt.Rows[0]["ref_teacher_id"];
                dateLb.Text = string.Format("建立日期:   {0}",DateTime.Parse("" + dt.Rows[0]["create_time"]).ToString("yyyy/MM/dd"));

                ReloadDataGridview();

                this._dicUnitDataByName.Remove("" + dt.Rows[0]["unit_name"]); // 移除修改中的管理單位名稱，避免驗證時判斷單位名稱重複!
            }

            this._initFinish = true;
        }

        public void ReloadDataGridview()
        {
            DataTable dt = DAO.Teacher.GetTeacherData(this._unitID);

            this.SuspendLayout();
            foreach (DataRow row in dt.Rows)
            {
                DataGridViewRow datarow = new DataGridViewRow();
                datarow.CreateCells(dataGridViewX1);

                int index = 0;

                string gender = "";
                switch ("" + row["gender"])
                {
                    case "0":
                        gender = "女";
                        break;
                    case "1":
                        gender = "男";
                        break;
                    default:
                        gender = "";
                        break;
                }

                datarow.Cells[index++].Value = "" + row["teacher_name"];
                datarow.Cells[index++].Value = "" + row["nickname"];
                datarow.Cells[index++].Value = gender;
                datarow.Cells[index++].Value = "" + row["st_login_name"];
                datarow.Cells[index++].Value = "" + row["dept"];
                datarow.Tag = row; 
                dataGridViewX1.Rows.Add(datarow);
            }
            this.ResumeLayout();
        }

        private void dataGridViewX1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (string.IsNullOrEmpty("" + dataGridViewX1.Rows[e.RowIndex].Cells[3].Value))
                {
                    MsgBox.Show(string.Format("{0}老師沒有登入帳號，\n無法設定為單位主管! ", tbxUnitBoss.Text));
                }
                else
                {
                    tbxUnitBoss.Text = "" + dataGridViewX1.Rows[e.RowIndex].Cells[0].Value;
                    tbxUnitBoss.Tag = (DataRow)dataGridViewX1.Rows[e.RowIndex].Tag;
                }
            }
        }

        private void searchTbx_TextChanged(object sender, EventArgs e)
        {
            if (searchTbx.Text == "")
            {
                foreach (DataGridViewRow row in dataGridViewX1.Rows)
                {
                    row.Visible = true;
                }
            }
            else
            {
                foreach (DataGridViewRow row in dataGridViewX1.Rows)
                {
                    if (row.Cells[0].Value != null)
                    {
                        row.Visible = (row.Cells[0].Value.ToString().IndexOf(searchTbx.Text) > -1);
                    }
                }
            }
        }

        private bool tbxUnitBoss_Validate()
        {
            if (string.IsNullOrEmpty(tbxUnitBoss.Text.Trim()))
            {
                errorProvider1.SetError(tbxUnitBoss, "請選擇單位主管!");
                return false;
            }
            else
            {
                errorProvider1.SetError(tbxUnitBoss, null);
                return true;
            }
        }

        private bool tbxUnitName_Validate()
        {
            if (string.IsNullOrEmpty(tbxUnitName.Text.Trim()))
            {
                errorProvider1.SetError(tbxUnitName,"單位名稱不可空白!");
                return false;
            }
            else
            {
                if (this._dicUnitDataByName.ContainsKey(tbxUnitName.Text.Trim()))
                {
                    errorProvider1.SetError(tbxUnitName, "此單位名稱已存在!");
                    return false;
                }
                else
                {
                    errorProvider1.SetError(tbxUnitName,null);
                    return true;
                }
            }
        }

        private void unitNameTbx_TextChanged(object sender, EventArgs e)
        {
            if (this._initFinish)
            {
                tbxUnitName_Validate();
            }
        }

        private void unitBossTbx_TextChanged(object sender, EventArgs e)
        {
            if (this._initFinish)
            {
                tbxUnitBoss_Validate();
            }
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            if (tbxUnitBoss_Validate() && tbxUnitName_Validate())
            {
                string unitName = tbxUnitName.Text.Trim();
                string createTime = DateTime.Now.ToString("yyyy/MM/dd");
                string createdBy = Actor.Account;

                string teacherAccount = "" + ((DataRow)tbxUnitBoss.Tag)["st_login_name"];
                string refTeacherID = "" + ((DataRow)tbxUnitBoss.Tag)["id"];
                string isBoss = "true";
                string loginID = Actor.GetLoginIDByAccount(teacherAccount);

                try
                {
                    if (this._mode == FormMode.Add)
                    {
                        DAO.Unit.InsertUnitData(loginID,unitName,createTime,createdBy,teacherAccount,refTeacherID,isBoss);
                    }
                    if (this._mode == FormMode.Update)
                    {
                        DAO.Unit.UpdateUnitData(loginID,unitName,this._unitID,createdBy,teacherAccount,refTeacherID,isBoss,createTime);
                    }
                    
                    MsgBox.Show("儲存成功!");
                    this.DialogResult = DialogResult.Yes;
                    //this.Close();
                }
                catch (Exception ex)
                {
                    MsgBox.Show(ex.Message);
                }
            }
        }

        private void leaveBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
