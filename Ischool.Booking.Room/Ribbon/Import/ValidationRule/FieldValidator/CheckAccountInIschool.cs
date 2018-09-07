using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.Data;
using System.Threading.Tasks;
using Campus.DocumentValidator;
using System.Data;

namespace Ischool.Booking.Room
{
    class CheckAccountInIschool : IFieldValidator
    {
        private List<string> mTeacherNames;

        public CheckAccountInIschool()
        {
            mTeacherNames = new List<string>();

                QueryHelper Helper = new QueryHelper();

                string sql = "SELECT st_login_name FROM teacher WHERE status = 1";

                DataTable Table = Helper.Select(sql);

                foreach (DataRow Row in Table.Rows)
                {
                    //string TeacherName = Row.Field<string>("teacher_name");
                    string account = Row.Field<string>("st_login_name");
                    string TeacherKey = account;

                    if (!mTeacherNames.Contains(TeacherKey))
                        mTeacherNames.Add(TeacherKey);
                }
        }

        #region IFieldValidator 成員

        /// <summary>
        /// 自動修正
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public string Correct(string Value)
        {
            return string.Empty;
        }

        /// <summary>
        /// 回傳訊息
        /// </summary>
        /// <param name="template"></param>
        /// <returns></returns>
        public string ToString(string template)
        {
            return template;
        }

        /// <summary>
        /// 驗證
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public bool Validate(string Value)
        {

            return mTeacherNames.Contains(Value);
        }

        #endregion
    }
}
