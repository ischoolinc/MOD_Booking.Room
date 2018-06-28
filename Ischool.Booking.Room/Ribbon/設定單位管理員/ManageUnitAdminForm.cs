﻿using System;
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
        /// <summary>
        /// 管理單位編號 unitName / unitID
        /// </summary>
        Dictionary<string, string> unitDic = new Dictionary<string, string>();

        public ManageUnitAdminForm()
        {
            InitializeComponent();

            string identity = Actor.Identity;
            identityLb.Text = identity;

            if (identity == "系統管理員")
            {
                unitCbx.Visible = true;

                AccessHelper access = new AccessHelper();
                List<UDT.MeetingRoomUnit> unitList = access.Select<UDT.MeetingRoomUnit>();
                foreach (UDT.MeetingRoomUnit unit in unitList)
                {
                    unitDic.Add(unit.Name, unit.UID);

                    unitCbx.Items.Add(unit.Name);
                }
                unitCbx.SelectedIndex = 0;

                string unitID = unitDic["" + unitCbx.Items[0]];
                ReloadDataGridview(unitID);
            }
            else if (identity == "單位主管")
            {               
                unitLb.Visible = true;

                UnitRecord ur = BookingRecord.SelectUnitByAccount(Actor.Account);
                unitLb.Text = ur.Name;
                unitLb.Tag = ur.UID;

                ReloadDataGridview(ur.UID);
            }
        }


        public void ReloadDataGridview(string unitID)
        {
            dataGridViewX1.Rows.Clear();

            string sql = string.Format(@"
SELECT
    unit_admin.*
    , teacher.teacher_name 
    , teacher2.teacher_name AS create_name
FROM
    $ischool.booking.meetingroom_unit_admin AS unit_admin
    LEFT OUTER JOIN teacher
        ON unit_admin.ref_teacher_id = teacher.id
    LEFT OUTER JOIN teacher AS teacher2
        ON unit_admin.created_by = teacher2.st_login_name
WHERE
    unit_admin.ref_unit_id = {0}
                ",unitID);

            QueryHelper qh = new QueryHelper();
            DataTable dt = qh.Select(sql);

            foreach (DataRow row in dt.Rows)
            {
                DataGridViewRow datarow = new DataGridViewRow();
                datarow.CreateCells(dataGridViewX1);

                int index = 0;

                datarow.Cells[index++].Value = "" + row["teacher_name"];
                datarow.Cells[index++].Value = "" + row["account"];
                datarow.Cells[index++].Value = ("" + row["is_boss"]) == "true" ? "單位主管" : "單位管理員";
                datarow.Cells[index++].Value = "" + row["create_name"];
                datarow.Cells[index++].Value = DateTime.Parse("" + row["create_time"]).ToShortDateString();
                datarow.Tag = "" + row["uid"]; // 單位管理員編號

                dataGridViewX1.Rows.Add(datarow);
            }

        }

        private void leaveBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            int row = dataGridViewX1.SelectedCells[0].RowIndex;
            string adminID = "" + dataGridViewX1.Rows[row].Tag;
            string identity = "" + dataGridViewX1.Rows[row].Cells[2].Value;
            string name = "" + dataGridViewX1.Rows[row].Cells[0].Value;
            string teacherAccount = "" + dataGridViewX1.Rows[row].Cells[1].Value;
            int index = unitCbx.SelectedIndex;
            string unitID = unitDic["" + unitCbx.Items[index]];
            string loginID = Actor.GetLoginIDByAccount(teacherAccount);

            if (identity == "單位主管")
            {
                MsgBox.Show("無法刪除單位主管!");
                return;
            }
            DialogResult result = MsgBox.Show("確認是否要將' " + name + " '單位管理員身分刪除?", "警告", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                string sql = string.Format(@"
WITH delete_unit_admin AS(
    DELETE
    FROM
        $ischool.booking.meetingroom_unit_admin
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
            if (Actor.Identity == "系統管理員")
            {
                int index = unitCbx.SelectedIndex;
                string unitName = "" + unitCbx.Items[index];
                string unitID = unitDic[unitName];

                AddUnitAdminForm form = new AddUnitAdminForm(unitID);
                form.Text = string.Format("新增{0}管理員",unitName);
                form.FormClosed += delegate 
                {
                    ReloadDataGridview(unitID);
                };
                form.ShowDialog();
            }
            else if (Actor.Identity == "單位主管")
            {
                int index = unitCbx.SelectedIndex;
                string unitName = unitLb.Text;
                string unitID = "" + unitLb.Tag;

                AddUnitAdminForm form = new AddUnitAdminForm(unitID);
                form.Text = string.Format("新增{0}管理員", unitName);
                form.FormClosed += delegate
                {
                    ReloadDataGridview(unitID);
                };
                form.ShowDialog();
            }
        }

        private void unitCbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = unitCbx.SelectedIndex;

            string unitID = unitDic[unitCbx.Items[index].ToString()];

            ReloadDataGridview(unitID);
        }
    }
}
