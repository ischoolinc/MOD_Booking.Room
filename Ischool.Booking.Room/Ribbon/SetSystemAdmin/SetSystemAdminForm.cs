using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FISCA.Presentation.Controls;
using K12.Data;
using FISCA.Data;

namespace Ischool.Booking.Room
{
    public partial class SetSystemAdminForm : BaseForm
    {
        public SetSystemAdminForm()
        {
            InitializeComponent();

            ReloadDataGridView();
        }

        public void ReloadDataGridView()
        {
            dataGridViewX1.Rows.Clear();
            string sql = @"
SELECT
	admin.*
	, teacher.teacher_name AS admin_name
	, teacher2.teacher_name AS created_name
FROM
	$ischool.booking.meetingroom_system_admin AS admin
	LEFT OUTER JOIN teacher
		ON admin.ref_teacher_id = teacher.id
	LEFT OUTER JOIN teacher AS teacher2
		ON admin.created_by = teacher2.st_login_name
            ";

            QueryHelper qh = new QueryHelper();
            DataTable dt = qh.Select(sql);

            foreach (DataRow row in dt.Rows)
            {
                DataGridViewRow datarow = new DataGridViewRow();
                datarow.CreateCells(dataGridViewX1);

                int index = 0;

                datarow.Cells[index++].Value = "" + row["admin_name"];
                datarow.Cells[index++].Value = "" + row["account"];
                datarow.Cells[index++].Value = ("" + row["is_default"]) == "true" ? "系統預設管理員" : "系統管理員";
                datarow.Cells[index++].Value = ("" + row["is_default"]) == "true" ? "系統預設" : row["created_name"];
                datarow.Cells[index++].Value = DateTime.Parse("" + row["create_time"]).ToShortDateString();
                datarow.Tag = "" + row["uid"]; // 系統管理員編號

                dataGridViewX1.Rows.Add(datarow);
            }
        }

        private void leaveBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void addSystemAdminBtn_Click(object sender, EventArgs e)
        {
            AddSystemAdminForm form = new AddSystemAdminForm();
            form.FormClosed += delegate 
            {
                ReloadDataGridView();
            };
            form.ShowDialog();
        }

        private void deleteSystemAdminBtn_Click(object sender, EventArgs e)
        {
            int row = dataGridViewX1.SelectedCells[0].RowIndex;
            string adminID = "" + dataGridViewX1.Rows[row].Tag;
            string identity = "" + dataGridViewX1.Rows[row].Cells[2].Value;
            string name = "" + dataGridViewX1.Rows[row].Cells[0].Value;
            string account = "" + dataGridViewX1.Rows[row].Cells[1].Value;
            string loginID = Actor.GetLoginIDByAccount(account);

            if (identity == "系統預設管理員")
            {
                MsgBox.Show("無法刪除系統預設管理員!");
                return;
            }
            DialogResult result = MsgBox.Show("確認是否要將' "+name+" '系統管理員身分刪除?", "警告", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                // 刪除system_admin 、  刪除_lr_belong
                string sql = string.Format(@"
WITH delete_system_admin AS(
    DELETE
    FROM
        $ischool.booking.meetingroom_system_admin
    WHERE
        uid = {0}
)
DELETE
FROM
    _lr_belong
WHERE
    _login_id = {1}
                    ", adminID, loginID);

                UpdateHelper up = new UpdateHelper();

                try
                {
                    up.Execute(sql);
                    MsgBox.Show("刪除成功!");
                    ReloadDataGridView();
                }
                catch(Exception ex)
                {
                    MsgBox.Show(ex.Message);
                }
                
            }
        }
    }
}
