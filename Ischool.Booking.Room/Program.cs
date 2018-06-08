using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using FISCA;
using FISCA.UDT;
using K12.Data.Configuration;
using FISCA.Presentation;
using FISCA.Permission;
using FISCA.Data;
using K12.Data;
using System.Windows.Forms;

namespace Ischool.Booking.Room
{
    public class Program
    {
        [MainMethod()]
        static public void Main()
        {

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

            // 取得登入帳號身分
            Actor actor = new Actor();
            string identity = Actor.Identity;

            MotherForm.AddPanel(BookingRoomAdmin.Instance);

            RibbonBarItem settingItem = FISCA.Presentation.MotherForm.RibbonBarItems["會議室預約", "基本設定"];
            settingItem["管理"].Size = RibbonBarButton.MenuButtonSize.Large;
            settingItem["管理"].Image = Properties.Resources.network_lock_64;

            settingItem["管理"]["管理場地"].Enable = Permissions.管理場地權限;
            settingItem["管理"]["管理場地"].Click += delegate
            {
                if (identity == "系統管理員" || identity == "單位管理員" || identity == "單位主管")
                {
                    MeetingRoomManagement form = new MeetingRoomManagement();
                    form.ShowDialog();
                }
                else
                {
                    MessageBox.Show("此帳號沒有場地預約管理權限!");
                }
            };

            RibbonBarItem assignment = MotherForm.RibbonBarItems["會議室預約","場地預約"];
            assignment["審核作業"].Size = RibbonBarButton.MenuButtonSize.Large;
            assignment["審核作業"].Image = Properties.Resources.architecture_zoom_64;
            assignment["審核作業"].Enable = Permissions.審核作業權限;
            assignment["審核作業"].Click += delegate 
            {
                if (identity == "系統管理員" || identity == "單位管理員" || identity == "單位主管")
                {
                    ReviewForm form = new ReviewForm();
                    form.ShowDialog();
                }
                else
                {
                    MessageBox.Show("此帳號沒有場地預約管理權限!");
                }
            };
            
            #region 權限管理
            Catalog detail = new Catalog();
            detail = RoleAclSource.Instance["會議室預約"]["功能按鈕"];
            detail.Add(new RibbonFeature(Permissions.管理場地, "管理場地"));

            detail.Add(new RibbonFeature(Permissions.審核作業,"審核作業"));

            #endregion
        }
    }
}
