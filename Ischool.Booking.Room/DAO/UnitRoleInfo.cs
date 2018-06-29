using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ischool.Booking.Room.DAO
{
    class UnitRoleInfo
    {
        /// <summary>
        /// 管理單位編號
        /// </summary>
        public string ID { get; }
        /// <summary>
        /// 管理單位名稱
        /// </summary>
        public string Name { get;  }
        /// <summary>
        /// 老師編號
        /// </summary>
        public string RefTeacherID { get;  }
        /// <summary>
        /// 是否為單位主管
        /// </summary>
        public bool IsBoss { get; }

        public UnitRoleInfo(string id, string name, bool isBoss, string teacherID)
        {
            this.ID = id;
            this.Name = name;
            this.IsBoss = isBoss;
            this.RefTeacherID = teacherID;
        }
    
    }
}
