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

namespace Ischool.Booking.Room
{
    public partial class AddUnitAdminForm : BaseForm
    {
        private string _unitID = "";

        public AddUnitAdminForm(string unitID)
        {
            InitializeComponent();

            this._unitID = unitID;
        }

        private void AddUnitAdminForm_Load(object sender, EventArgs e)
        {
            DataTable dt = DAO.Teacher.GetTeacherData(this._unitID);

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
                datarow.Cells[index++].Value = ("" + row["st_login_name"]).Trim();
                datarow.Cells[index++].Value = "" + row["dept"];
                datarow.Cells[index++].Value = "指定";
                datarow.Tag = "" + row["id"];
                dataGridViewX1.Rows.Add(datarow);
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

        private void dataGridViewX1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                int col = e.ColumnIndex;
                if (col == 5)
                {
                    string name = "" + dataGridViewX1.Rows[e.RowIndex].Cells[0].Value;
                    string text = string.Format("確認是否將' {0} '設定為單位管理員?", name);

                    DialogResult result = MsgBox.Show(text, "確認", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {

                        string teacherAccount = "" + dataGridViewX1.Rows[e.RowIndex].Cells[3].Value;
                        string teacherName = "" + dataGridViewX1.Rows[e.RowIndex].Cells[0].Value;
                        string teacherID = "" + dataGridViewX1.Rows[e.RowIndex].Tag;
                        string time = DateTime.Now.ToString("yyyy/MM/dd");
                        string actor = Actor.Account;
                        string loginID = Actor.GetLoginIDByAccount(teacherAccount);

                        if (string.IsNullOrEmpty(teacherAccount.Trim()))
                        {
                            MsgBox.Show(string.Format("{0}老師沒有登入帳號，\n無法設定為單位管理員! ", teacherName));
                            return;
                        }

                        try
                        {
                            DAO.UnitAdmin.InsertUnitAdmin(loginID, teacherAccount, teacherID,this._unitID, time, actor);
                            MsgBox.Show("儲存成功!");
                            this.DialogResult = DialogResult.Yes;
                            this.Close();
                        }
                        catch (Exception ex)
                        {
                            MsgBox.Show(ex.Message);
                        }
                    }
                }
            }
        }
    }
}
