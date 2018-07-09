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
using FISCA.UDT;

namespace Ischool.Booking.Room
{
    public partial class EditUnitForm : BaseForm
    {
        /// <summary>
        /// 編輯模式: 新增or修改
        /// </summary>
        string _mode;
        /// <summary>
        /// 管理單位編號
        /// </summary>
        string _unitID;
        /// <summary>
        /// 系統已存在管理單位名稱
        /// </summary>
        List<string> _unitNameList = new List<string>();

        Dictionary<string, DataRow> _teacherDic = new Dictionary<string, DataRow>();

        public EditUnitForm(string mode,string unitID)
        {
            InitializeComponent();

            _mode = mode;
            _unitID = unitID;

        }

        private void EditUnitForm_Load(object sender, EventArgs e)
        {
            #region 讀取管理單位資料
            AccessHelper access = new AccessHelper();
            List<UDT.MeetingRoomUnit> unitList = access.Select<UDT.MeetingRoomUnit>();
            foreach (UDT.MeetingRoomUnit unit in unitList)
            {
                _unitNameList.Add(unit.Name);
            }

            #endregion

            QueryHelper qh = new QueryHelper();

            #region 讀取教師資料

            string _sql = "SELECT * FROM teacher WHERE status = 1";

            DataTable _dt = qh.Select(_sql);

            foreach (DataRow row in _dt.Rows)
            {
                _teacherDic.Add("" + row["id"], row);
            }

            #endregion

            if (_mode == "新增")
            {
                dateLb.Text = "建立日期:   " + DateTime.Now.ToShortDateString();

                ReloadDataGridview();
            }
            else if (_mode == "修改")
            {
                string sql = string.Format(@"
SELECT
    unit.name AS unit_name
    , teacher.teacher_name
    , unit.create_time
    , teacher.id AS ref_teacher_id
FROM
    $ischool.booking.meetingroom_unit AS unit
    LEFT OUTER JOIN $ischool.booking.meetingroom_unit_admin AS unit_admin
        ON unit.uid = unit_admin.ref_unit_id
        AND unit_admin.is_boss = 'true'
    LEFT OUTER JOIN teacher
        ON unit_admin.ref_teacher_id = teacher.id
WHERE
    unit.uid = {0}
                    ", _unitID);
                DataTable dt = qh.Select(sql);

                unitNameTbx.Text = "" + dt.Rows[0]["unit_name"];
                unitBossTbx.Text = "" + dt.Rows[0]["teacher_name"];
                unitBossTbx.Tag = "" + dt.Rows[0]["ref_teacher_id"];
                dateLb.Text = "建立日期:   " + DateTime.Parse("" + dt.Rows[0]["create_time"]).ToShortDateString();

                ReloadDataGridview();
            }
        }

        public void ReloadDataGridview()
        {
            // 取得未指定為會議室模組管理者以及尚未在這個管理單位擔任腳色之教師
            string sql = "";
            if (_unitID == "")
            {
                sql = string.Format(@"
SELECT DISTINCT
    teacher.*
FROM 
    teacher
    LEFT OUTER JOIN _login
        ON teacher.st_login_name = _login.login_name
    LEFT OUTER JOIN _lr_belong
        ON _login.id = _lr_belong._login_id 
        AND _lr_belong._role_id = {0}
WHERE 
    _lr_belong.id IS NULL
    AND teacher.status = 1
            ", Program._roleAdminID);
            }
            else
            {
                sql = string.Format(@"
SELECT DISTINCT
    teacher.*
FROM 
    teacher
    LEFT OUTER JOIN _login
        ON teacher.st_login_name = _login.login_name
    LEFT OUTER JOIN _lr_belong
        ON _login.id = _lr_belong._login_id 
        AND _lr_belong._role_id = {0}
    LEFT OUTER JOIN $ischool.booking.equip_unit_admin AS unit_admin
        ON  teacher.id = unit_admin.ref_teacher_id
        AND unit_admin.ref_unit_id = {1}
WHERE 
    _lr_belong.id IS NULL
    AND unit_admin.uid IS NULL
    AND teacher.status = 1
            ", Program._roleAdminID,_unitID);
            }
            

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
                datarow.Tag = "" + row["id"];   // 老師系統編號
                dataGridViewX1.Rows.Add(datarow);
            }
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            #region 資料驗證
            if (unitNameTbx.Text == "")
            {
                errorLb1.Visible = true;
                return;
            }
            else if (unitBossTbx.Text == "")
            {
                errorLb2.Visible = true;
                return;
            }
            else if (_unitNameList.Contains(unitNameTbx.Text) && _mode == "新增")
            {
                errorLb1.Text = "此單位名稱已存在系統!";
                errorLb1.Visible = true;
                return;
            }
            #endregion

            string adminAccount = "" + _teacherDic["" + unitBossTbx.Tag]["st_login_name"];
            // 沒有登入帳號的老師不能被指定管理員身分
            if (adminAccount == "")
            {
                MsgBox.Show(string.Format("{0}老師沒有登入帳號，\n無法設定為單位主管! ", unitBossTbx.Text));
                return;
            }

            string sql = GetSQL();
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
                return;
            }
            
        }

        private void leaveBtn_Click(object sender, EventArgs e)
        {
            this.Close();
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
            if (e.RowIndex > -1)
            {
                int col = e.ColumnIndex;
                int row = e.RowIndex;

                unitBossTbx.Text = "" + dataGridViewX1.Rows[row].Cells[0].Value;
                unitBossTbx.Tag = dataGridViewX1.Rows[row].Tag;  // 教師編號
            }
        }

        // 資料驗證
        private void unitNameTbx_TextChanged(object sender, EventArgs e)
        {
            string unitName = unitNameTbx.Text.Trim();
            if (_unitNameList.Contains(unitName) && _mode == "新增")
            {
                errorLb1.Text = "此單位名稱已存在!";
                errorLb1.Visible = true;
                saveBtn.Enabled = false;
            }
            else if (unitName == "")
            {
                errorLb1.Text = "請輸入單位名稱!";
                errorLb1.Visible = true;
                saveBtn.Enabled = false;
            }
            else
            {
                errorLb1.Visible = false;
                saveBtn.Enabled = true;
            }
        }

        private void unitBossTbx_TextChanged(object sender, EventArgs e)
        {
            if (unitBossTbx.Text == "")
            {
                errorLb2.Visible = true;
            }
            else
            {
                errorLb2.Visible = false;
            }
        }

        public string GetSQL()
        {
            // 根據模式調整SQL內容
            #region SQL
            // 避免管理單位名稱中有空白的問題
            string unitName = unitNameTbx.Text.Trim();
            string createTime = DateTime.Now.ToShortDateString();
            string createdBy = Actor.Account;
            // 避免帳號中有空白的問題
            string teacherAccount = ("" + _teacherDic["" + unitBossTbx.Tag]["st_login_name"]).Trim();
            string refTeacherID = "" + _teacherDic["" + unitBossTbx.Tag]["id"];
            string isBoss = "true";
            string loginID = Actor.GetLoginIDByAccount(teacherAccount);
            string sql = "";
            // 新增場地管理單位 、 新增單位主管 、 新增_login 、 新增_lr_belong
            if (_mode == "新增")
            {
                #region sql

                if (loginID == "")
                {
                    sql = string.Format(@"
WITH data_row AS(
    SELECT
        '{0}'::TEXT AS unit_name
        , '{1}'::TIMESTAMP AS create_time
        , '{2}'::TEXT AS created_by
        , '{3}'::TEXT AS admin_account
        , {4}::BIGINT AS ref_teacher_id
        , '{5}'::BOOLEAN AS is_boss
) ,insert_unit_data AS(
    INSERT INTO $ischool.booking.meetingroom_unit(
        name
        , create_time
        , created_by
    )
    SELECT
        unit_name
        , create_time
        , created_by
    FROM
        data_row

    RETURNING $ischool.booking.meetingroom_unit.*
) ,insert_unit_admin AS(
    INSERT INTO $ischool.booking.meetingroom_unit_admin(
    account
    , ref_unit_id
    , ref_teacher_id
    , is_boss
    , create_time
    , created_by
)
SELECT
    data_row.admin_account
    , insert_unit_data.uid
    , data_row.ref_teacher_id
    , data_row.is_boss
    , data_row.create_time
    , data_row.created_by
FROM
    data_row
    LEFT OUTER JOIN insert_unit_data
        ON data_row.unit_name = insert_unit_data.name
) ,insert_login AS(
    INSERT INTO _login(
        login_name
        , password
        , sys_admin
        , account_type
    )
    VALUES(
        '{3}'
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
    , {6}
FROM 
    insert_login
                ", unitName, createTime, createdBy, teacherAccount, refTeacherID, isBoss, Program._roleID);
                }
                else
                {
                    sql = string.Format(@"
WITH data_row AS(
    SELECT
        '{0}'::TEXT AS unit_name
        , '{1}'::TIMESTAMP AS create_time
        , '{2}'::TEXT AS created_by
        , '{3}'::TEXT AS admin_account
        , {4}::BIGINT AS ref_teacher_id
        , '{5}'::BOOLEAN AS is_boss
) ,insert_unit_data AS(
    INSERT INTO $ischool.booking.meetingroom_unit(
        name
        , create_time
        , created_by
    )
    SELECT
        unit_name
        , create_time
        , created_by
    FROM
        data_row

    RETURNING $ischool.booking.meetingroom_unit.*
) ,insert_unit_admin AS(
    INSERT INTO $ischool.booking.meetingroom_unit_admin(
    account
    , ref_unit_id
    , ref_teacher_id
    , is_boss
    , create_time
    , created_by
)
SELECT
    data_row.admin_account
    , insert_unit_data.uid
    , data_row.ref_teacher_id
    , data_row.is_boss
    , data_row.create_time
    , data_row.created_by
FROM
    data_row
    LEFT OUTER JOIN insert_unit_data
        ON data_row.unit_name = insert_unit_data.name
) 
INSERT INTO _lr_belong(
    _login_id
    , _role_id
)
SELECT 
    {6} 
    , {7}
                ", unitName, createTime, createdBy, teacherAccount, refTeacherID, isBoss, loginID, Program._roleID);
                }

                #endregion

            }
           
            else if (_mode == "修改")
            {
                #region sql
                // 修改場地管理單位 、 刪除舊單位主管 、刪除lr_belong、新增單位主管、新增login 、新增lr_belong
                if (loginID == "")
                {
                    sql = string.Format(@"
WITH data_row AS(
    SELECT
        '{0}'::TEXT AS unit_name
        , {1}::BIGINT AS unit_id
        , '{2}'::TEXT AS created_by
        , '{3}'::TEXT AS account
        , '{4}'::BIGINT AS ref_teacher_id
        , '{5}'::BOOLEAN AS is_boss
        , '{6}'::TIMESTAMP AS create_time
) ,update_unit_data AS(
    UPDATE
        $ischool.booking.meetingroom_unit
    SET
        name = data_row.unit_name
    FROM
        data_row
    WHERE
        uid = data_row.unit_id
) ,delete_unit_admin AS(
    DELETE
    FROM
        $ischool.booking.meetingroom_unit_admin
    WHERE
        ref_unit_id IN (SELECT unit_id FROM data_row )
        AND is_boss = 'true'
    RETURNING account
) , insert_unit_admin AS(
    INSERT INTO $ischool.booking.meetingroom_unit_admin(
        account
        , ref_unit_id
        , ref_teacher_id
        , is_boss
        , create_time
        , created_by
    )    
    SELECT
        data_row.account
        , data_row.unit_id
        , data_row.ref_teacher_id
        , data_row.is_boss
        , data_row.create_time
        , data_row.created_by
    FROM
        data_row
) ,delete_lr_belong AS(
    DELETE
    FROM
        _lr_belong
    WHERE
        _login_id IN(
            SELECT 
                id
            FROM
                _login
                LEFT OUTER JOIN delete_unit_admin
                        ON _login.login_name = delete_unit_admin.account
            WHERE
                delete_unit_admin.account IS NOT NULL
        )
) ,insert_login AS(
    INSERT INTO _login(
        login_name
        , password
        , sys_admin
        , account_type
    )
    VALUES(
        '{3}'
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
    , {7}
FROM 
    insert_login
                    ", unitName, _unitID, createdBy, teacherAccount, refTeacherID, isBoss, createTime, Program._roleID);
                }
                // 修改場地管理單位 、 刪除舊單位主管 、刪除lr_belong、新增單位主管 、新增lr_belong
                else
                {
                    sql = string.Format(@"
WITH data_row AS(
    SELECT
        '{0}'::TEXT AS unit_name
        , {1}::BIGINT AS unit_id
        , '{2}'::TEXT AS created_by
        , '{3}'::TEXT AS account
        , '{4}'::BIGINT AS ref_teacher_id
        , '{5}'::BOOLEAN AS is_boss
        , '{6}'::TIMESTAMP AS create_time
) ,update_unit_data AS(
    UPDATE
        $ischool.booking.meetingroom_unit
    SET
        name = data_row.unit_name
    FROM
        data_row
    WHERE
        uid = data_row.unit_id
) ,delete_unit_admin AS(
    DELETE
    FROM
        $ischool.booking.meetingroom_unit_admin
    WHERE
        ref_unit_id IN (SELECT unit_id FROM data_row )
        AND is_boss = 'true'
    RETURNING account
) , insert_unit_admin AS(
    INSERT INTO $ischool.booking.meetingroom_unit_admin(
        account
        , ref_unit_id
        , ref_teacher_id
        , is_boss
        , create_time
        , created_by
    )    
    SELECT
        data_row.account
        , data_row.unit_id
        , data_row.ref_teacher_id
        , data_row.is_boss
        , data_row.create_time
        , data_row.created_by
    FROM
        data_row
) ,delete_lr_belong AS(
    DELETE
    FROM
        _lr_belong
    WHERE
        _login_id IN(
            SELECT 
                id
            FROM
                _login
                LEFT OUTER JOIN delete_unit_admin
                    ON _login.login_name = delete_unit_admin.account
            WHERE
                delete_unit_admin.account IS NOT NULL
        )
) 
INSERT INTO _lr_belong(
    _login_id
    , _role_id
)
SELECT 
    {7} 
    , {8}
                    ", unitName, _unitID, createdBy, teacherAccount, refTeacherID, isBoss, createTime, loginID ,Program._roleID);
                }
                
                #endregion
            }

            #endregion

            return sql;
        }

        
    }
}
