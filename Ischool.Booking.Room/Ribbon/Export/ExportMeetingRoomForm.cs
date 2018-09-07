using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FISCA.Presentation.Controls;
using SmartSchool.API.PlugIn;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Rendering;
using System.Xml;
using FISCA.Data;
using Aspose.Cells;

namespace Ischool.Booking.Room
{
    public partial class ExportMeetingRoomForm : BaseForm
    {
        public ExportMeetingRoomForm()
        {
            InitializeComponent();

            #region 設定Wizard會跟著Style跑
            //this.wizard1.HeaderStyle.ApplyStyle((GlobalManager.Renderer as Office2007Renderer).ColorTable.GetClass(ElementStyleClassKeys.RibbonFileMenuBottomContainerKey));
            //this.wizard1.FooterStyle.BackColorGradientAngle = -90;
            //this.wizard1.FooterStyle.BackColorGradientType = eGradientType.Linear;
            //this.wizard1.FooterStyle.BackColor = (GlobalManager.Renderer as Office2007Renderer).ColorTable.RibbonBar.Default.TopBackground.Start;
            //this.wizard1.FooterStyle.BackColor2 = (GlobalManager.Renderer as Office2007Renderer).ColorTable.RibbonBar.Default.TopBackground.End;
            //this.wizard1.BackColor = (GlobalManager.Renderer as Office2007Renderer).ColorTable.RibbonBar.Default.TopBackground.Start;
            //this.wizard1.BackgroundImage = null;
            //for (int i = 0; i < 5; i++)
            //{
            //    (this.wizard1.Controls[1].Controls[i] as ButtonX).ColorTable = eButtonColor.OrangeWithBackground;
            //}
            //(this.wizard1.Controls[0].Controls[1] as System.Windows.Forms.Label).ForeColor = (GlobalManager.Renderer as Office2007Renderer).ColorTable.RibbonBar.MouseOver.TitleText;
            //(this.wizard1.Controls[0].Controls[2] as System.Windows.Forms.Label).ForeColor = (GlobalManager.Renderer as Office2007Renderer).ColorTable.RibbonBar.Default.TitleText;
            #endregion

        }

        private void ExportMeetingRoomForm_Load(object sender, EventArgs e)
        {
            XmlDocument doc = new XmlDocument();
        }

        private void wizard1_FinishButtonClick(object sender, CancelEventArgs e)
        {
            List<string> exportFieldList = new List<string>();

            #region 取得選取欄位
            foreach (ListViewItem item in listViewEx1.Items)
            {
                if (item.Checked)
                {
                    exportFieldList.Add(item.Text.Trim());
                }
            }
            #endregion

            #region 取得資料、寫入資料

            string sql = string.Format(@"
SELECT
    room.*
    , equipment.uid AS equipment_id
    , equipment.name AS equipment_name
    , equipment.count 
    , equipment.status AS equipment_status
    , unit.name AS unit_name
FROM
    $ischool.booking.meetingroom AS room
    LEFT OUTER JOIN $ischool.booking.meetingroom_equipment AS equipment
        ON room.uid = equipment.ref_meetingroom_id
    LEFT OUTER JOIN $ischool.booking.meetingroom_unit AS unit
        ON room.ref_unit_id = unit.uid
");
            QueryHelper qh = new QueryHelper();
            DataTable dt = qh.Select(sql);

            Workbook report = new Workbook();
            report.Worksheets[0].Name = "會議室資料";
            
            //填表頭
            for (int i = 0; i < exportFieldList.Count; i++)
            {
                report.Worksheets[0].Cells[0, i].PutValue(exportFieldList[i]);
            }
            //填資料
            int rowIndex = 1;
            foreach (DataRow row in dt.Rows)
            {
                
                if (rowIndex < 65536)
                {
                    for (int col = 0; col < exportFieldList.Count(); col++)
                    {
                        switch (exportFieldList[col])
                        {
                            case "會議室系統編號":
                                report.Worksheets[0].Cells[rowIndex, col].PutValue("" + row["uid"]);
                                break;
                            case "會議室名稱":
                                report.Worksheets[0].Cells[rowIndex, col].PutValue("" + row["name"]);
                                break;
                            case "照片":
                                report.Worksheets[0].Cells[rowIndex, col].PutValue("" + row["picture"]);
                                break;
                            case "所屬大樓名稱":
                                report.Worksheets[0].Cells[rowIndex, col].PutValue("" + row["building"]);
                                break;
                            case "容納人數":
                                report.Worksheets[0].Cells[rowIndex, col].PutValue("" + row["capacity"]);
                                break;
                            case "會議室目前狀態":
                                report.Worksheets[0].Cells[rowIndex, col].PutValue("" + row["status"]);
                                break;
                            case "管理單位系統編號":
                                report.Worksheets[0].Cells[rowIndex, col].PutValue("" + row["ref_unit_id"]);
                                break;
                            case "管理單位名稱":
                                report.Worksheets[0].Cells[rowIndex, col].PutValue("" + row["unit_name"]);
                                break;
                            case "是否為特殊場地":
                                report.Worksheets[0].Cells[rowIndex, col].PutValue(("" + row["is_special"]) == "true" ? "是" : "否");
                                break;
                            case "建立日期":
                                report.Worksheets[0].Cells[rowIndex, col].PutValue("" + row["create_time"]);
                                break;
                            case "建立者帳號":
                                report.Worksheets[0].Cells[rowIndex, col].PutValue("" + row["created_by"]);
                                break;
                            case "修改日期":
                                report.Worksheets[0].Cells[rowIndex, col].PutValue("" + row["last_update"]);
                                break;
                            case "設備系統編號":
                                report.Worksheets[0].Cells[rowIndex, col].PutValue("" + row["equipment_id"]);
                                break;
                            case "設備名稱":
                                report.Worksheets[0].Cells[rowIndex, col].PutValue("" + row["equipment_name"]);
                                break;
                            case "設備數量":
                                report.Worksheets[0].Cells[rowIndex, col].PutValue("" + row["count"]);
                                break;
                            case "設備狀態":
                                report.Worksheets[0].Cells[rowIndex, col].PutValue("" + row["equipment_status"]);
                                break;
                            default:
                                break;
                        }
                    }
                }
                rowIndex++;
            }

            #endregion

            #region 儲存資料

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "匯出會議室清單";
            saveFileDialog.FileName = "匯出會議室清單.xls";
            saveFileDialog.Filter = "Excel (*.xls)|*.xls|所有檔案 (*.*)|*.*";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                DialogResult result = new DialogResult();
                try
                {
                    report.Save(saveFileDialog.FileName);
                    result = MsgBox.Show("檔案儲存完成，是否開啟檔案?", "是否開啟", MessageBoxButtons.YesNo);
                }
                catch(Exception ex)
                {
                    MsgBox.Show(ex.Message);
                    return;
                }
                
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        System.Diagnostics.Process.Start(saveFileDialog.FileName);
                    }
                    catch (Exception ex)
                    {
                        MsgBox.Show("開啟檔案發生失敗:" + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                this.Close();
            }

            #endregion
        }

        private void wizard1_CancelButtonClick(object sender, CancelEventArgs e)
        {
            this.Close();
        }

        private void selectAllCbx_CheckedChanged(object sender, EventArgs e)
        {
            if (selectAllCbx.Checked)
            {
                for (int i = 0;i< listViewEx1.Items.Count;i++)
                {
                    listViewEx1.Items[i].Checked = true;
                }
            }
        }
    }
}
