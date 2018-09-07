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
using FISCA.Data;
using K12.Data;

namespace Ischool.Booking.Room
{
    public partial class ManageUnitAdminForm : BaseForm
    {
        private AccessHelper _access = new AccessHelper();
        private Dictionary<string, string> _dicUnitIDByName = new Dictionary<string, string>();
        private Actor actor = Actor.Instance;
        private RoleUnitDecorator decorator;

        public ManageUnitAdminForm()
        {
            InitializeComponent();
        }

        private void ManageUnitAdminForm_Load(object sender, EventArgs e)
        {
            List<UDT.MeetingRoomUnit> listUnit = this._access.Select<UDT.MeetingRoomUnit>();
            foreach (UDT.MeetingRoomUnit unit in listUnit)
            {
                _dicUnitIDByName.Add(unit.Name, unit.UID);
            }

            this.decorator = new RoleUnitDecorator(identityLb, cbxIdentity, unitCbx, false, false);
        }

        private void unitCbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            string unitID = this._dicUnitIDByName[unitCbx.Text];

            ReloadDataGridview(unitID);
        }

        public void ReloadDataGridview(string unitID)
        {
            this.SuspendLayout();
            dataGridViewX1.Rows.Clear();
            DataTable dt = DAO.UnitAdmin.GetUnitAdminByUnitID(unitID);
            foreach (DataRow row in dt.Rows)
            {
                DataGridViewRow datarow = new DataGridViewRow();
                datarow.CreateCells(dataGridViewX1);

                int col = 0;
                datarow.Cells[col++].Value = "" + row["teacher_name"];
                datarow.Cells[col++].Value = "" + row["account"];
                datarow.Cells[col++].Value = ("" + row["is_boss"]) == "true" ? EnumDescription.GetIdentityDescription(typeof(UserIdentity),UserIdentity.UnitBoss.ToString()) : EnumDescription.GetIdentityDescription(typeof(UserIdentity), UserIdentity.UnitAdmin.ToString());
                datarow.Cells[col++].Value = "" + row["create_name"];
                datarow.Cells[col++].Value = DateTime.Parse("" + row["create_time"]).ToString("yyyy/MM/dd");
                datarow.Tag = "" + row["uid"]; // 單位管理員編號

                dataGridViewX1.Rows.Add(datarow);
            }
            this.ResumeLayout();
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            int row = dataGridViewX1.SelectedCells[0].RowIndex;
            string adminID = "" + dataGridViewX1.Rows[row].Tag;
            string identity = "" + dataGridViewX1.Rows[row].Cells[2].Value;
            string name = "" + dataGridViewX1.Rows[row].Cells[0].Value;
            string teacherAccount = "" + dataGridViewX1.Rows[row].Cells[1].Value;
            //int index = unitCbx.SelectedIndex;
            string unitID = _dicUnitIDByName[unitCbx.Text];
            string loginID = Actor.GetLoginIDByAccount(teacherAccount);

            if (identity == EnumDescription.GetIdentityDescription(typeof(UserIdentity), UserIdentity.UnitBoss.ToString()))
            {
                MsgBox.Show("無法刪除單位主管!");
                return;
            }
            DialogResult result = MsgBox.Show("確認是否要將' " + name + " '單位管理員身分刪除?", "警告", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                try
                {
                    DAO.UnitAdmin.DeleteUnitAdmin(adminID,loginID);
                    MsgBox.Show("刪除成功!");

                    ReloadDataGridview(unitID);
                }
                catch(Exception ex)
                {
                    MsgBox.Show(ex.Message);
                }
            }
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            if (actor.isSysAdmin())
            {
                string unitID = _dicUnitIDByName[unitCbx.Text];

                AddUnitAdminForm form = new AddUnitAdminForm(unitID);
                form.Text = string.Format("新增{0}管理員", unitCbx.Text);
                form.FormClosed += delegate 
                {
                    if (form.DialogResult == DialogResult.Yes)
                    {
                        ReloadDataGridview(unitID);
                    }
                };
                form.ShowDialog();
            }
            else if (actor.isUnitBoss())
            {
                int index = unitCbx.SelectedIndex;

            }
        }

        private void leaveBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
