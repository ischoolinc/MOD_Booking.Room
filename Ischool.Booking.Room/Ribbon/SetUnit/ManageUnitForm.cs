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
    public partial class ManageUnitForm : BaseForm
    {
        private QueryHelper _qh = new QueryHelper();
        private AccessHelper _access = new AccessHelper();

        public ManageUnitForm()
        {
            InitializeComponent();
        }

        private void ManageUnitForm_Load(object sender, EventArgs e)
        {
            ReloadDataGridView();
        }

        public void ReloadDataGridView()
        {
            dataGridViewX1.Rows.Clear();

            DataTable dt = DAO.Unit.GetUnitData();

            this.SuspendLayout();
            foreach (DataRow row in dt.Rows)
            {
                DataGridViewRow datarow = new DataGridViewRow();
                datarow.CreateCells(dataGridViewX1);

                int col = 0;
                datarow.Cells[col++].Value = "" + row["name"];
                datarow.Cells[col++].Value = "" + row["boss_name"];
                datarow.Cells[col++].Value = "" + row["created_name"];
                datarow.Cells[col++].Value = ("" + row["is_default"]) == "true" ? "系統預設管理員" : "系統管理員";
                datarow.Cells[col++].Value = DateTime.Parse("" + row["create_time"]).ToShortDateString();
                datarow.Tag = row;

                dataGridViewX1.Rows.Add(datarow);
            }
            this.ResumeLayout();
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            EditUnitForm form = new EditUnitForm(FormMode.Add,"");
            form.FormClosed += delegate
            {
                if (form.DialogResult == DialogResult.Yes)
                {
                    ReloadDataGridView();
                }
            };
            form.ShowDialog();
        }

        private void updateBtn_Click(object sender, EventArgs e)
        {
            if (dataGridViewX1.SelectedRows.Count > 0 && dataGridViewX1.SelectedRows[0].Index > -1)
            {
                DataGridViewRow dgvrow = dataGridViewX1.SelectedRows[0];
                string unitID = "" + ((DataRow)dgvrow.Tag)["uid"]; // 管理單位編號

                EditUnitForm form = new EditUnitForm(FormMode.Update, unitID);
                form.Text = "修改管理單位";
                if (form.DialogResult == DialogResult.Yes)
                {
                    ReloadDataGridView();
                }
                form.ShowDialog();
            }
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            DataGridViewRow dgvrow = dataGridViewX1.SelectedRows[0];

            string unitName = "" + dgvrow.Cells[0].Value;
            string unitID = "" + ((DataRow)dgvrow.Tag)["uid"];

            List<UDT.MeetingRoomUnitAdmin> unitAdminList = this._access.Select<UDT.MeetingRoomUnitAdmin>("ref_unit_id = " + unitID);
            List<string> loginIDList = new List<string>();
            // 取得該單位所有管理員登入ID
            foreach (UDT.MeetingRoomUnitAdmin admin in unitAdminList)
            {
                string loginID = Actor.GetLoginIDByAccount(admin.Account);
                loginIDList.Add(loginID);
            }

            string loginIDs = string.Join(" , ", loginIDList);

            DialogResult result = MsgBox.Show("確定是否將' "+ unitName + " '管理單位刪除? \n 該單位管理員與主管將同步刪除，\n 該單位管理場地將更新為無歸屬單位","提醒",MessageBoxButtons.YesNo);
           
            if (result == DialogResult.Yes)
            {
                try
                {
                    // 刪除unit_admin 、刪除unit、刪除lr_belong
                    DAO.Unit.DeleteUnitData(unitID,loginIDs);
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
