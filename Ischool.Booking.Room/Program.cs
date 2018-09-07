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
using FISCA.Presentation.Controls;
using Campus.DocumentValidator;
using Customization.Tagging;
using System.Drawing;

namespace Ischool.Booking.Room
{
    public class Program
    {
        /// <summary>
        /// 會議室預約管理員角色名稱
        /// </summary>
        public static string _roleName = "會議室預約模組專用";
        /// <summary>
        /// 會議室預約模組專用角色ID
        /// </summary>
        public static string _roleID;

        /// <summary>
        /// 會議室預約管理者角色名稱
        /// </summary>
        public static string _roleAdminName = "會議室預約管理者";
        /// <summary>
        /// 會議室預約管理者專用角色ID
        /// </summary>
        public static string _roleAdminID;

        /// <summary>
        /// 會議室預約模組角色功能權限
        /// </summary>
        public static string _permission = @"
<Permissions>
<Feature Code=""8EFBEC7D-D438-44EA-81E3-6AFA00862429"" Permission=""Execute""/>
<Feature Code=""26751E07-00A0-4500-BC31-F2E57EE1C6F2"" Permission=""Execute""/>
<Feature Code=""24821EBA-426E-4811-95B8-DBF8D9AEEFA2"" Permission=""Execute""/>
<Feature Code=""AB164E2A-516E-4427-ADB0-79D27F1685CA"" Permission=""Execute""/>
<Feature Code=""1B4FE9E3-E763-4E2D-9F5D-92979A18CEC1"" Permission=""Execute""/>
<Feature Code=""0CB5F779-97B7-4950-877F-DE8731F4F63C"" Permission=""Execute""/>
<Feature Code=""40111E44-1A30-4D3C-9D43-3069F7F46014"" Permission=""Execute""/>
</Permissions>
";

        [MainMethod()]
        static public void Main()
        {
            // Init 會議室預約模組Panel
            BookingRoomAdmin ba = new BookingRoomAdmin();
            MotherForm.AddPanel(BookingRoomAdmin.Instance);

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

            #region Init Role
            {
                #region 建立會議室預約模組管理者專用角色
                if (!DAO.Role.CheckRoleIsExist(_roleAdminName))
                {
                    _roleAdminID = DAO.Role.InsertRole(_roleAdminName); // 建立角色
                }
                else
                {
                    DAO.Role.UpdateRole(_roleAdminID); // 更新角色
                }

                #endregion

                #region 建立會議室預約專用角色
                if (!DAO.Role.CheckRoleIsExist(_roleName))
                {
                    _roleID = DAO.Role.InsertRole(_roleName); // 建立角色
                }
                else
                {
                    DAO.Role.UpdateRole(_roleID);  // 更新角色
                }
                #endregion
            }
            #endregion

            #region 匯入的驗證規則
            {
                FactoryProvider.FieldFactory.Add(new MeetingRoomFieldValidatorFactory());
                FactoryProvider.RowFactory.Add(new MeetingRoomRowValidatorFactory());
            }
            #endregion

            #region 會議室預約
            {
                // 取得登入帳號身分
                Actor actor = Actor.Instance;

                MotherForm.RibbonBarItems["會議室預約", "基本設定"]["會議室管理"].Size = RibbonBarButton.MenuButtonSize.Large;
                MotherForm.RibbonBarItems["會議室預約", "基本設定"]["會議室管理"].Image = Properties.Resources.architecture_config_64;
                MotherForm.RibbonBarItems["會議室預約", "基本設定"]["設定管理單位"].Size = RibbonBarButton.MenuButtonSize.Medium;
                MotherForm.RibbonBarItems["會議室預約", "基本設定"]["設定管理單位"].Image = Properties.Resources.meeting_config_64;
                MotherForm.RibbonBarItems["會議室預約", "基本設定"]["設定單位管理員"].Size = RibbonBarButton.MenuButtonSize.Medium;
                MotherForm.RibbonBarItems["會議室預約", "基本設定"]["設定單位管理員"].Image = Properties.Resources.foreign_language_config_64;
                MotherForm.RibbonBarItems["會議室預約", "資料統計"]["匯出"].Size = RibbonBarButton.MenuButtonSize.Large;
                MotherForm.RibbonBarItems["會議室預約", "資料統計"]["匯出"].Image = Properties.Resources.Export_Image;
                MotherForm.RibbonBarItems["會議室預約", "資料統計"]["匯入"].Size = RibbonBarButton.MenuButtonSize.Large;
                MotherForm.RibbonBarItems["會議室預約", "資料統計"]["匯入"].Image = Properties.Resources.Import_Image;
                MotherForm.RibbonBarItems["會議室預約", "資料統計"]["報表"].Size = RibbonBarButton.MenuButtonSize.Large;
                MotherForm.RibbonBarItems["會議室預約", "資料統計"]["報表"].Image = Properties.Resources.Report;
                MotherForm.RibbonBarItems["會議室預約", "會議室預約作業"]["審核預約紀錄"].Size = RibbonBarButton.MenuButtonSize.Large;
                MotherForm.RibbonBarItems["會議室預約", "會議室預約作業"]["審核預約紀錄"].Image = Properties.Resources.document_ok_64;

                #region 會議室管理
                {
                    MotherForm.RibbonBarItems["會議室預約", "基本設定"]["會議室管理"].Enable = Permissions.管理會議室權限;
                    MotherForm.RibbonBarItems["會議室預約", "基本設定"]["會議室管理"].Click += delegate
                    {
                        if (actor.isSysAdmin() || actor.isUnitAdmin() || actor.isUnitBoss())
                        {
                            MeetingRoomManagement form = new MeetingRoomManagement();
                            form.ShowDialog();
                        }
                        else
                        {
                            MsgBox.Show("此帳號沒有管理會議室權限!");
                        }
                    };
                }
                #endregion

                #region 設定管理單位
                {
                    MotherForm.RibbonBarItems["會議室預約", "基本設定"]["設定管理單位"].Enable = Permissions.設定會議室管理單位權限;
                    MotherForm.RibbonBarItems["會議室預約", "基本設定"]["設定管理單位"].Click += delegate
                    {
                        if (actor.isSysAdmin())
                        {
                            ManageUnitForm form = new ManageUnitForm();
                            form.ShowDialog();
                        }
                        else
                        {
                            MsgBox.Show("此帳號沒有設定會議室管理單位權限");
                        }
                    };
                }
                #endregion

                #region 設定單位管理員
                {
                    MotherForm.RibbonBarItems["會議室預約", "基本設定"]["設定單位管理員"].Enable = Permissions.設定單位管理員權限;
                    MotherForm.RibbonBarItems["會議室預約", "基本設定"]["設定單位管理員"].Click += delegate
                    {
                        if (actor.isSysAdmin())
                        {
                            ManageUnitAdminForm form = new ManageUnitAdminForm();
                            form.ShowDialog();
                        }
                        else if (actor.isUnitBoss())
                        {
                            ManageUnitAdminForm form = new ManageUnitAdminForm();
                            form.ShowDialog();
                        }
                        else
                        {
                            MsgBox.Show("此帳號沒有設定單位管理員權限");
                        }
                    };
                }
                #endregion

                #region 匯出場地清單
                {
                    MotherForm.RibbonBarItems["會議室預約", "資料統計"]["匯出"]["會議室清單"].Enable = Permissions.匯出會議室清單權限;
                    MotherForm.RibbonBarItems["會議室預約", "資料統計"]["匯出"]["會議室清單"].Click += delegate
                    {
                        if (actor.isSysAdmin() || actor.isUnitBoss() || actor.isUnitAdmin())
                        {
                            ExportMeetingRoomForm form = new ExportMeetingRoomForm();
                            form.ShowDialog();
                        }
                        else
                        {
                            MsgBox.Show("此帳號沒有使用權限!");
                        }
                    };
                }
                #endregion

                #region 匯入場地清單
                {
                    MotherForm.RibbonBarItems["會議室預約", "資料統計"]["匯入"]["會議室清單"].Enable = Permissions.匯入會議室清單權限;
                    MotherForm.RibbonBarItems["會議室預約", "資料統計"]["匯入"]["會議室清單"].Click += delegate
                    {
                        if (actor.isSysAdmin() || actor.isUnitBoss() || actor.isUnitAdmin())
                        {
                            new ImportMeetingRoomData().Execute();
                        }
                        else
                        {
                            MsgBox.Show("此帳號沒有使用權限!");
                        }
                    };
                }
                #endregion

                #region 統計場地使用狀況
                {
                    MotherForm.RibbonBarItems["會議室預約", "資料統計"]["報表"]["統計會議室使用狀況"].Enable = Permissions.統計會議室使用狀況權限;
                    MotherForm.RibbonBarItems["會議室預約", "資料統計"]["報表"]["統計會議室使用狀況"].Click += delegate
                    {
                        if (actor.isSysAdmin() || actor.isUnitBoss() || actor.isUnitAdmin())
                        {
                            StatisticalReportForm form = new StatisticalReportForm();
                            form.ShowDialog();
                        }
                        else
                        {
                            MsgBox.Show("此帳號沒有使用權限!");
                        }

                    };
                }
                #endregion

                #region 審核作業
                {
                    MotherForm.RibbonBarItems["會議室預約", "會議室預約作業"]["審核預約紀錄"].Enable = Permissions.審核作業權限;
                    MotherForm.RibbonBarItems["會議室預約", "會議室預約作業"]["審核預約紀錄"].Click += delegate
                    {
                        if (actor.isSysAdmin() || actor.isUnitBoss() || actor.isUnitAdmin())
                        {
                            ReviewForm form = new ReviewForm();
                            form.ShowDialog();
                        }
                        else
                        {
                            MsgBox.Show("此帳號沒有使用權限!");
                        }
                    };
                }
                #endregion

                #region 權限管理
                Catalog detail = new Catalog();
                detail = RoleAclSource.Instance["會議室預約"]["功能按鈕"];
                detail.Add(new RibbonFeature(Permissions.會議室管理單位, "設定會議室管理單位"));
                detail.Add(new RibbonFeature(Permissions.管理會議室, "管理會議室"));
                //detail.Add(new RibbonFeature(Permissions.系統管理員,"系統管理員")); ---已廢除此功能
                detail.Add(new RibbonFeature(Permissions.單位管理員, "設定單位管理員"));
                detail.Add(new RibbonFeature(Permissions.審核作業, "審核作業"));
                detail.Add(new RibbonFeature(Permissions.匯入會議室清單, "匯入會議室清單"));
                detail.Add(new RibbonFeature(Permissions.匯出會議室清單, "匯出會議室清單"));
                detail.Add(new RibbonFeature(Permissions.統計會議室使用狀況, "統計會議室使用狀況"));
                #endregion
            }
            #endregion
        }

    }
}
