using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using FISCA.Data;
using K12.Data;

namespace Ischool.Booking.Room.DAO
{
    class Unit
    {
        public static DataTable GetUnitData()
        {
            string sql = @"
SELECT
    unit.*
    , unit_admin.uid AS ref_admin_id
    , unit_admin.is_boss
    , unit_admin.account 
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
            QueryHelper qh = new QueryHelper();

            return qh.Select(sql);
        }

        public static DataTable GetUnitDataByID(string unitID)
        {
            string sql = string.Format(@"
SELECT
    unit.name AS unit_name
    , teacher.id
    , teacher.teacher_name
    , teacher.st_login_name
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
                    ", unitID);

            QueryHelper qh = new QueryHelper();

            return qh.Select(sql);
        }

        /// <summary>
        ///  新增場地管理單位 、新增單位主管 、新增_login 、新增_lr_belong
        /// </summary>
        /// <param name="loginID"></param>
        /// <param name="unitName"></param>
        /// <param name="createTime"></param>
        /// <param name="createdBy"></param>
        /// <param name="teacherAccount"></param>
        /// <param name="refTeacherID"></param>
        /// <param name="isBoss"></param>
        public static void InsertUnitData(string loginID,string unitName,string createTime,string createdBy,string teacherAccount,string refTeacherID,string isBoss)
        {
            string sql = "";

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
                ", unitName, createTime, createdBy, teacherAccount, refTeacherID, isBoss, Program._roleUnitBossID);
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
                ", unitName, createTime, createdBy, teacherAccount, refTeacherID, isBoss, loginID, Program._roleUnitBossID);
            }
            #endregion

            UpdateHelper up = new UpdateHelper();
            up.Execute(sql);
        }

        /// <summary>
        /// 修改場地管理單位 、刪除舊單位主管 、刪除lr_belong、新增單位主管、新增login 、新增lr_belong
        /// </summary>
        /// <param name="loginID"></param>
        /// <param name="unitName"></param>
        /// <param name="unitID"></param>
        /// <param name="createdBy"></param>
        /// <param name="teacherAccount"></param>
        /// <param name="refTeacherID"></param>
        /// <param name="isBoss"></param>
        /// <param name="createTime"></param>
        public static void UpdateUnitData(string loginID, string unitName,string unitID,string createdBy,string teacherAccount,string refTeacherID,string isBoss,string createTime)
        {
            string sql = "";
            #region sql
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
                    ", unitName, unitID, createdBy, teacherAccount, refTeacherID, isBoss, createTime, Program._roleUnitBossID);
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
                    ", unitName, unitID, createdBy, teacherAccount, refTeacherID, isBoss, createTime, loginID, Program._roleUnitBossID);
            }

            #endregion

            UpdateHelper up = new UpdateHelper();
            up.Execute(sql);
        }

        public static void DeleteUnitData(string unitID,string loginIDs)
        {
            string sql = string.Format(@"
WITH delete_unit AS(
    DELETE
    FROM
        $ischool.booking.meetingroom_unit
    WHERE
        uid = {0}
) ,delete_unit_admin AS(
    DELETE 
    FROM
        $ischool.booking.meetingroom_unit_admin
    WHERE
        ref_unit_id = {0}
) ,delete_lr_belong AS(
    DELETE
    FROM
        _lr_belong
    WHERE
        _login_id IN( {1} )
)
UPDATE
    $ischool.booking.meetingroom
SET
    ref_unit_id = null
WHERE
    ref_unit_id = {0}
                ", unitID, loginIDs);

            UpdateHelper up = new UpdateHelper();
            up.Execute(sql);
        }
    }
}
