using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.Data;
using System.Threading.Tasks;
using Campus.DocumentValidator;
using System.Data;
using FISCA.UDT;

namespace Ischool.Booking.Room
{
    public class CheckUnitInIschool : IFieldValidator
    {
        private List<string> unitNames;
        private Task mTask;

        public CheckUnitInIschool()
        {
            unitNames = new List<string>();

            mTask = Task.Factory.StartNew
            (() =>
            {
                AccessHelper access = new AccessHelper();
                List<UDT.MeetingRoomUnit> listUnits = access.Select<UDT.MeetingRoomUnit>();

                foreach (UDT.MeetingRoomUnit unit in listUnits)
                {
                    string unitName = unit.Name.Trim();
                    //unitNames.Add(unitName);
                    if (!unitNames.Contains(unitName))
                    {
                        unitNames.Add(unitName);
                    }
                }
            }
            );
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
            mTask.Wait();

            return unitNames.Contains(Value);
        }

        #endregion
    }
}
