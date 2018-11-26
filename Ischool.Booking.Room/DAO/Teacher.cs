using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using FISCA.Data;

namespace Ischool.Booking.Room.DAO
{
    class Teacher
    {
        /// <summary>
        /// 取得尚未在這個管理單位擔任角色之教師
        /// </summary>
        /// <param name="unitID"></param>
        /// <returns></returns>
        public static DataTable GetTeacherData(string unitID)
        {
            string sql = "";
            if (unitID == "")
            {
                sql = @"
SELECT
    *
FROM
    teacher
WHERE
    teacher.status = 1
";

                #region old sql
                //                sql = string.Format(@"
                //SELECT DISTINCT
                //    teacher.*
                //FROM 
                //    teacher
                //    LEFT OUTER JOIN _login
                //        ON teacher.st_login_name = _login.login_name
                //    LEFT OUTER JOIN _lr_belong
                //        ON _login.id = _lr_belong._login_id 
                //        AND _lr_belong._role_id = {0}
                //WHERE 
                //    _lr_belong.id IS NULL
                //    AND teacher.status = 1
                //            ", Program._roleAdminID); 
                #endregion
            }
            else
            {
                sql = string.Format(@"
SELECT DISTINCT
    teacher.*
FROM
    teacher
    LEFT OUTER JOIN $ischool.booking.equip_unit_admin AS unit_admin
        ON unit_admin.ref_teacher_id = teacher.id
        AND unit_admin.ref_unit_id = {0}
WHERE
    unit_admin.uid IS NULL
    AND teacher.status = 1
                ",unitID);
                #region old sql
                //                sql = string.Format(@"
                //SELECT DISTINCT
                //    teacher.*
                //FROM 
                //    teacher
                //    LEFT OUTER JOIN _login
                //        ON teacher.st_login_name = _login.login_name
                //    LEFT OUTER JOIN _lr_belong
                //        ON _login.id = _lr_belong._login_id 
                //        AND _lr_belong._role_id = {0}
                //    LEFT OUTER JOIN $ischool.booking.equip_unit_admin AS unit_admin
                //        ON  teacher.id = unit_admin.ref_teacher_id
                //        AND unit_admin.ref_unit_id = {1}
                //WHERE 
                //    _lr_belong.id IS NULL
                //    AND unit_admin.uid IS NULL
                //    AND teacher.status = 1
                //            ", Program._roleAdminID, unitID); 
                #endregion
            }

            QueryHelper qh = new QueryHelper();

            return qh.Select(sql);
        }
    }
}
