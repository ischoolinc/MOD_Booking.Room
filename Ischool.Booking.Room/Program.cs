using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FISCA;
using FISCA.UDT;
using K12.Data.Configuration;

namespace Ischool.Booking.Room
{
    public class Program
    {
        [MainMethod()]
        static public void Main()
        {
            //ServerModule.AutoManaged("");
            #region Init UDT

            ConfigData cd = K12.Data.School.Configuration["場地預約模組載入設定"];
            bool checkUDT = false;
            string name = "場地預約UDT是否已載入";

            //如果尚無設定值,預設為
            if (string.IsNullOrEmpty(cd[name]))
            {
                cd[name] = "false";
            }

            //檢查是否為布林
            bool.TryParse(cd[name], out checkUDT);

            if (!checkUDT)
            {
                AccessHelper access = new AccessHelper();
                access.Select<UDT.MeetingRoom>("UID = '00000'");
                access.Select<UDT.MeetingRoomEquipment>("UID = '00000'");
                access.Select<UDT.MeetingRoomUnit>("UID = '00000'");
                access.Select<UDT.MeetingRoomUnitAdmin>("UID = '00000'");
                access.Select<UDT.MeetingRoomSystemAdmin>("UID = '00000'");
                access.Select<UDT.MeetingRoomApplication>("UID = '00000'");
                access.Select<UDT.MeetingRoomApplicationDetail>("UID = '00000'");

                cd[name] = "true";
                cd.Save();
            }
            
            #endregion
        }
    }
}
