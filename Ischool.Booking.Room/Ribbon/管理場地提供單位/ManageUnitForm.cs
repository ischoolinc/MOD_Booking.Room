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
    public partial class ManageUnitForm : BaseForm
    {
        public ManageUnitForm()
        {
            InitializeComponent();

            ReloadDataGridView();
        }

        public void ReloadDataGridView()
        {
            dataGridViewX1.Rows.Clear();

            #region SQL
            string sql = @"
SELECT
    unit.*
    , unit_admin.uid AS ref_admin_id
    , unit_admin.is_boss
    , teacher.teacher_name AS boss_name
    , teacher2.teacher_name AS created_name
    , system_admin.is_default
FROM 
    $ischool.booking.meetingroom_unit AS unit
    LEFT OUTER JOIN $ischool.booking.meetingroom_unit_admin AS unit_admin
        ON unit.uid = unit_admin.ref_unit_id
        AND unit_admin.is_boss = 'true'
    LEFT OUTER JOIN teacher 
        ON unit_admin.ref_teacher_id = teacher.id
    LEFT OUTER JOIN  teacher AS teacher2
        ON unit.created_by = teacher2.st_login_name
    LEFT OUTER JOIN $ischool.booking.meetingroom_system_admin AS system_admin
        ON unit.created_by = system_admin.account
";
            #endregion

            QueryHelper qh = new QueryHelper();
            DataTable dt = qh.Select(sql);

            foreach (DataRow row in dt.Rows)
            {
                DataGridViewRow datarow = new DataGridViewRow();
                datarow.CreateCells(dataGridViewX1);

                int index = 0;

                datarow.Cells[index++].Value = "" + row["name"];
                datarow.Cells[index].Tag = "" + row["ref_admin_id"]; // 單位主管編號
                datarow.Cells[index++].Value = "" + row["boss_name"];
                datarow.Cells[index++].Value = "" + row["created_name"];
                datarow.Cells[index++].Value = ("" + row["is_default"]) == "true" ? "系統預設管理員" : "系統管理員";
                datarow.Cells[index++].Value = DateTime.Parse("" + row["create_time"]).ToShortDateString();
                datarow.Tag = "" + row["uid"]; // 管理單位編號

                dataGridViewX1.Rows.Add(datarow);
            }
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            EditUnitForm form = new EditUnitForm("新增","");
            form.FormClosed += delegate 
            {
                ReloadDataGridView();
            };
            form.ShowDialog();
        }

        private void updateBtn_Click(object sender, EventArgs e)
        {
            int row = dataGridViewX1.SelectedCells[0].RowIndex;
            string unitID = "" + dataGridViewX1.Rows[row].Tag; // 管理單位編號

            EditUnitForm form = new EditUnitForm("修改", unitID);
            form.Text = "修改場地管理單位";
            form.FormClosed += delegate
            {
                ReloadDataGridView();
            };
            form.ShowDialog();
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            int row = dataGridViewX1.SelectedCells[0].RowIndex;
            string unitName = "" + dataGridViewX1.Rows[row].Cells[0].Value;
            string unitID = "" + dataGridViewX1.Rows[row].Tag;

            DialogResult result = MsgBox.Show("確定是否將' "+ unitName + " '管理單位刪除?","提醒",MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                string sql = string.Format(@"
DELETE
FROM
    $ischool.booking.meetingroom_unit
WHERE
    uid = {0}
                ",unitID);

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

        private void leaveBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
