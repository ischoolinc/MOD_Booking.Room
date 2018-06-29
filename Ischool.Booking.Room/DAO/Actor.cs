using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using FISCA.Data;
using FISCA.UDT;

namespace Ischool.Booking.Room
{
    class Actor
    {
        private static Actor _actor;

        public static Actor Instance
        {
            get
            {
                if (_actor == null)
                {
                    _actor = new Actor();
                }
                return _actor;
            }
        }

        /// <summary>
        /// 登入帳號
        /// </summary>
        public static string Account
        {
            get
            {
                return FISCA.Authentication.DSAServices.UserAccount.Replace("'", "''");
            }
        }

        /// <summary>
        /// 老師系統編號
        /// </summary>
        public static int RefTeacherID { get; set; }

        /// <summary>
        /// 透過使用者登入帳號取得_loginID
        /// </summary>
        /// <returns></returns>
        public static string GetLoginIDByAccount(string targetAccount)
        {
            string loginID;
            string sql = string.Format("SELECT * FROM _login WHERE login_name = '{0}'", targetAccount);
            QueryHelper qh = new QueryHelper();
            DataTable dt = qh.Select(sql);

            if (dt.Rows.Count > 0)
            {
                loginID = "" + dt.Rows[0]["id"];
            }
            else
            {
                loginID = "";
            }

            return loginID;
        }

        /// <summary>
        /// 是否為模組管理員
        /// </summary>
        private bool _isSysAdmin = false;

        /// <summary>
        /// 是否為單位主管
        /// </summary>
        private bool _isUnitBoss = false;

        /// <summary>
        /// 是否為單位管理員
        /// </summary>
        private bool _isUnitAdmin = false;

        /// <summary>
        /// 老師系統編號
        /// </summary>
        private String _teacherID = "";

        /// <summary>
        /// 登入人員管理角色清單
        /// </summary>
        private List<DAO.UnitRoleInfo> _units;

        //private List<UDT.MeetingRoomUnit> allUnits;

        public bool isSysAdmin()
        {
            return this._isSysAdmin;
        }

        public bool isUnitBoss()
        {
            return this._isUnitBoss;
        }

        public bool isUnitAdmin()
        {
            return this._isUnitAdmin;
        }

        public string getTeacherID()
        {
            return this._teacherID;
        }

        /// <summary>
        /// 確認身分
        /// </summary>
        private Actor()
        {
            this._units = new List<DAO.UnitRoleInfo>();
            //1. 判斷是否是系統管理者
            checkIsAdmin();

            //2. 判斷在各單位的角色
            findUnits();

            //3. 取得所有管理單位資料
            AccessHelper access = new AccessHelper();
            List<UDT.MeetingRoomUnit> listUnit = access.Select<UDT.MeetingRoomUnit>();
            foreach (UDT.MeetingRoomUnit unit in listUnit)
            {
                DAO.UnitRoleInfo unitRole = new DAO.UnitRoleInfo(unit.UID,unit.Name,false,"");
                _units.Add(unitRole);
            }

        }

        private void findUnits()
        {
            QueryHelper qh = new QueryHelper();

            string sql = string.Format(@"
                                    SELECT 
                                        unit.name AS unit_name
                                        , unit_admin.*
                                    FROM
                                        $ischool.booking.meetingroom_unit_admin AS unit_admin
                                        LEFT OUTER JOIN $ischool.booking.meetingroom_unit AS unit
                                            ON unit_admin.ref_unit_id = unit.uid
                                    WHERE
                                        unit_admin.account = '{0}'
                ", Actor.Account);

            DataTable dt = qh.Select(sql);
            foreach (DataRow row in dt.Rows)
            {
                string unitID = "" + row["ref_unit_id"];
                string unitName = "" + row["unit_name"];
                bool isBoss = ("" + row["is_boss"]) == "true" ? true : false;
                string teacherID = "" + row["ref_teacher_id"];
                DAO.UnitRoleInfo unitRole = new DAO.UnitRoleInfo(unitID, unitName, isBoss, teacherID);
                if (unitRole.IsBoss)
                {
                    this._isUnitBoss = true;
                }
                else
                {
                    this._isUnitAdmin = true;
                }

                
            }

        }

        private void checkIsAdmin()
        {
            QueryHelper qh = new QueryHelper();
            string sql = string.Format(@"
                                    SELECT 
                                        teacher.id
                                        , _login.login_name
                                    FROM
                                        _lr_belong
                                        LEFT OUTER JOIN _login
                                            ON _lr_belong._login_id = _login.id
                                        LEFT OUTER JOIN teacher
                                            ON _login.login_name = teacher.st_login_name
                                    WHERE
                                        _lr_belong._role_id = {0} 
                                        AND _login.login_name='{1}'
            ", Program._roleAdminID, Actor.Account);
            DataTable dt = qh.Select(sql);

            this._isSysAdmin = (dt.Rows.Count > 0);
            this._teacherID = (dt.Rows.Count > 0 ? dt.Rows[0]["id"].ToString() : "");

        }


        /// <summary>
        /// 取得是單位主管的管理單位
        /// </summary>
        /// <returns></returns>
        public List<DAO.UnitRoleInfo> getBossUnits()
        {
            List<DAO.UnitRoleInfo> result = new List<DAO.UnitRoleInfo>();

            if (this._units != null)
            {
                foreach (DAO.UnitRoleInfo unitRole in this._units)
                {
                    if (unitRole.IsBoss)
                    {
                        result.Add(unitRole);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 取得是單位管理員的管理單位
        /// </summary>
        /// <returns></returns>
        public List<DAO.UnitRoleInfo> getUnitAdminUnits()
        {
            List<DAO.UnitRoleInfo> result = new List<DAO.UnitRoleInfo>();

            if (this._units != null)
            {
                foreach (DAO.UnitRoleInfo unitRole in this._units)
                {
                    if (!unitRole.IsBoss)
                    {
                        result.Add(unitRole);
                    }
                }
            }

            return result;
        }

        public List<DAO.UnitRoleInfo> getUnits()
        {
            return this._units;

        }
    }
}
