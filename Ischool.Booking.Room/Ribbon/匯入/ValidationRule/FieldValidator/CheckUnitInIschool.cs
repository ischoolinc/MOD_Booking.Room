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
    class CheckUnitInIschool : IFieldValidator
    {
        private List<string> unitNames;
        private Task mTask;

        public CheckUnitInIschool()
        {
            unitNames = new List<string>();

            QueryHelper Helper = new QueryHelper();

            string sql = @"SELECT name FROM $ischool.booking.meetingroom_unit";

            DataTable Table = Helper.Select(sql);
                
            foreach (DataRow Row in Table.Rows)
            {
                string unitName = Row.Field<string>("name");
                string unitKey = unitName;

                if (!unitNames.Contains(unitKey))
                    unitNames.Add(unitKey);
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

            return unitNames.Contains(Value);
        }

        #endregion
    }
}
