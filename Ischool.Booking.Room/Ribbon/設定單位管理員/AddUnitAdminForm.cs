﻿using System;
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
        string _unitID = "";
        public AddUnitAdminForm(string unitID)
        {
            InitializeComponent();

            _unitID = unitID;

            // 取得未指定為系統管理員、單位管理員、單位主管的老師清單
            string sql = @"
SELECT 
    teacher.* 
FROM 
    teacher 
    LEFT OUTER JOIN $ischool.booking.meetingroom_system_admin AS system_admin
        ON teacher.id = system_admin.ref_teacher_id
    LEFT OUTER JOIN $ischool.booking.meetingroom_unit_admin AS unit_admin
        ON teacher.id = unit_admin.ref_teacher_id
WHERE 
    status = 1
    AND system_admin.uid IS NULL
    AND unit_admin.uid IS NULL
";
            QueryHelper qh = new QueryHelper();
            DataTable dt = qh.Select(sql);

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
                    //if ("" + row.Cells[0].Value == searchTbx.Text)
                    //{
                    //    row.Visible = true;
                    //}
                    //else
                    //{
                    //    row.Visible = false;
                    //}
                }
            }
        }

        private void dataGridViewX1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int col = e.ColumnIndex;
            if (col == 5)
            {
                string name = "" + dataGridViewX1.Rows[e.RowIndex].Cells[0].Value;
                string text = string.Format("確認是否將' {0} '設定為系統管理員?", name);

                DialogResult result = MsgBox.Show(text, "確認", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    string teacherAccount = "" + dataGridViewX1.Rows[e.RowIndex].Cells[3].Value;
                    string teacherName = "" + dataGridViewX1.Rows[e.RowIndex].Cells[0].Value;

                    if (teacherAccount == "")
                    {
                        MsgBox.Show(string.Format("{0}老師沒有登入帳號，\n無法設定為單位管理員! ", teacherName));
                        return;
                    }

                    string sql = GetSQL(dataGridViewX1.Rows[e.RowIndex]);

                    UpdateHelper up = new UpdateHelper();
                    try
                    {
                        up.Execute(sql);
                        MsgBox.Show("儲存成功!");

                        this.Close();
                    }
                    catch(Exception ex)
                    {
                        MsgBox.Show(ex.Message);
                    }
                    
                }
            }
        }

        public string GetSQL(DataGridViewRow dgvRow)
        {
            string teacherAccount = "" + dgvRow.Cells[3].Value;
            string teacherName = "" + dgvRow.Cells[0].Value;
            string teacherID = "" + dgvRow.Tag;
            string time = DateTime.Now.ToShortDateString();
            string actor = Actor.Account;
            string loginID = Actor.GetLoginIDByAccount(teacherAccount);

            string sql = "";
            // 新增unit_admin 、 新增_login 、新增 lr_belong
            if (loginID == "")
            {
                sql = string.Format(@"
WITH insert_unit_admin AS(
    INSERT INTO 
        $ischool.booking.meetingroom_unit_admin( 
            account
            , ref_teacher_id
            , ref_unit_id
            , is_boss
            , create_time
            , created_by
        )
        VALUES(
            '{0}'
            , {1}
            , {2}
            , 'false'
            , '{3}'
            , '{4}'
        )
) ,insert_login AS(
    INSERT INTO _login(
        login_name
        , password
        , sys_admin
        , account_type
    )
    VALUES(
        '{0}'
        , '1234'
        , '0'
        , 'greening'
    )
    RETURNING _login.id
) 
INSERT INTO _lr_belong(
    _login_id
    , _role_id
)
SELECT 
    id 
    , {5}
FROM 
    insert_login

                        ", teacherAccount, teacherID, _unitID, time, actor, Program._roleID);
            }
            // 新增unit_admin、新增 lr_belong
            else
            {
                sql = string.Format(@"
WITH insert_unit_admin AS(
    INSERT INTO 
        $ischool.booking.meetingroom_unit_admin( 
            account
            , ref_teacher_id
            , ref_unit_id
            , is_boss
            , create_time
            , created_by
        )
        VALUES(
            '{0}'
            , {1}
            , {2}
            , 'false'
            , '{3}'
            , '{4}'
        )
)
INSERT INTO _lr_belong(
    _login_id
    , _role_id
)
SELECT 
    {5}
    , {6}
                        ", teacherAccount, teacherID, _unitID, time, actor, loginID,Program._roleID);
            }
            
            return sql;
        }
    }
}
