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
    class UnitAdmin
    {
        /// <summary>
        /// 取得單位管理員資料
        /// </summary>
        /// <param name="unitID"></param>
        /// <returns></returns>
        public static DataTable GetUnitAdminByUnitID(string unitID)
        {
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
            ", unitID);

            QueryHelper qh = new QueryHelper();
            return qh.Select(sql);

        }

        /// <summary>
        /// 刪除單位管理員
        /// </summary>
        /// <param name="adminID"></param>
        /// <param name="loginID"></param>
        public static void DeleteUnitAdmin(string adminID,string loginID)
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
            up.Execute(sql);
        }

        public static void InsertUnitAdmin(string loginID,string teacherAccount,string teacherID,string _unitID,string time,string actor)
        {
            string sql = "";
            if (string.IsNullOrEmpty(loginID)) // 新增unit_admin 、 新增_login 、新增 lr_belong
            {
                #region SQL
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

                        ", teacherAccount, teacherID, _unitID, time, actor, Program._roleUnitAdminID); 
                #endregion
            }
            else // 新增unit_admin、新增 lr_belong
            {
                #region SQL
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
                        ", teacherAccount, teacherID, _unitID, time, actor, loginID, Program._roleUnitAdminID); 
                #endregion
            }

            UpdateHelper up = new UpdateHelper();
            up.Execute(sql);
        }
    }
}
