﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ischool.Booking.Room
{
    class Permissions
    {
        public static string 管理場地 { get { return "26751E07-00A0-4500-BC31-F2E57EE1C6F2"; } }
        public static bool 管理場地權限
        {
            get
            {
                return FISCA.Permission.UserAcl.Current[管理場地].Executable;
            }
        }
        public static string 審核作業 { get { return "AB164E2A-516E-4427-ADB0-79D27F1685CA"; } }
        public static bool 審核作業權限
        {
            get
            {
                return FISCA.Permission.UserAcl.Current[審核作業].Executable;
            }
        }
    }
}