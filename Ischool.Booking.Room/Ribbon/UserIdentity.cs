using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ischool.Booking.Room
{
    public enum UserIdentity
    {
        /// <summary>
        /// 會議室預約模組管理者
        /// </summary>
        [Description("會議室模組管理者")]
        ModuleAdmin = 0,

        /// <summary>
        /// 單位主管
        /// </summary>
        [Description("單位主管")]
        UnitBoss = 1,

        /// <summary>
        /// 單位管理員
        /// </summary>
        [Description("單位管理員")]
        UnitAdmin = 2
    }
}
