using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FISCA.Presentation.Controls;
using FISCA.UDT;
using FISCA.Data;
using Aspose.Cells;
using System.IO;
using System.Diagnostics;

namespace Ischool.Booking.Room
{
    public partial class StatisticalReportForm : BaseForm
    {
        /// <summary>
        /// 使用者身分
        /// </summary>
        string _identity = Actor.Identity;

        /// <summary>
        /// Name/UID
        /// </summary>
        Dictionary<string, string> unitDic = new Dictionary<string, string>();

        public StatisticalReportForm()
        {
            InitializeComponent();

            identifyLb.Text = _identity;

            AccessHelper access = new AccessHelper();
            List<UDT.MeetingRoomUnit> unitList = access.Select<UDT.MeetingRoomUnit>();
            foreach (UDT.MeetingRoomUnit unit in unitList)
            {
                unitDic.Add(unit.Name,unit.UID);
            }

            // 確認身分
            if (_identity == "系統管理員")
            {
                unitCbx.Items.Add("--全部--");

                foreach (UDT.MeetingRoomUnit unit in unitList)
                {
                    unitCbx.Items.Add(unit.Name);
                }

                unitCbx.SelectedIndex = 0;
            }
            else if (_identity == "單位管理員")
            {
                UnitRecord ur = BookingRecord.SelectUnitByAccount(Actor.Account);

                unitCbx.Items.Add(ur.Name);

                unitCbx.SelectedIndex = 0;
            }

            #region InitDateTimeInput

            startTime.Text = DateTime.Now.AddDays(-7).ToShortDateString();

            endTime.Text = DateTime.Now.ToShortDateString();

            #endregion
        }

        private void printBtn_Click(object sender, EventArgs e)
        {
            #region SQL

            string sql = "";

            if (unitCbx.Text == "--全部--")
            {
                sql = string.Format(@"
WITH data_row AS(
	SELECT
		unit.name AS unit_name
		, room.name AS room_name
		, room.uid AS room_id
		, app.uid
		, app_detail.start_time
		, app_detail.end_time
	FROM
		$ischool.booking.meetingroom_unit AS unit
		LEFT OUTER JOIN $ischool.booking.meetingroom AS room
			ON unit.uid = room.ref_unit_id
		LEFT OUTER JOIN $ischool.booking.meetingroom_application AS app
			ON room.uid = app.ref_meetingroom_id
		LEFT OUTER JOIN $ischool.booking.meetingroom_application_detail AS app_detail
			ON app.uid = app_detail.ref_application_id
	WHERE
		room.uid IS NOT NULL
		AND app.uid IS NOT NULL
		AND app.is_canceled = false
		AND app.is_approved = true
        AND app_detail.start_time >= '{0}'
        AND app_detail.start_time <= '{1}'
) 
SELECT
	unit_name
	, room_name
	, count(*) AS 使用次數
	, SUM(end_time - start_time) AS 使用時數
FROM
	data_row
GROUP BY
	unit_name
	,room_name
                    ",startTime.Value.ToShortDateString(),endTime.Value.ToShortDateString());
            }
            else
            {
                string unitID = unitDic[unitCbx.Text];
                sql = string.Format(@"
WITH data_row AS(
	SELECT
		unit.name AS unit_name
		, room.name AS room_name
		, room.uid AS room_id
		, app.uid
		, app_detail.start_time
		, app_detail.end_time
	FROM
		$ischool.booking.meetingroom_unit AS unit
		LEFT OUTER JOIN $ischool.booking.meetingroom AS room
			ON unit.uid = room.ref_unit_id
		LEFT OUTER JOIN $ischool.booking.meetingroom_application AS app
			ON room.uid = app.ref_meetingroom_id
		LEFT OUTER JOIN $ischool.booking.meetingroom_application_detail AS app_detail
			ON app.uid = app_detail.ref_application_id
	WHERE
		room.uid IS NOT NULL
		AND app.uid IS NOT NULL
		AND app.is_canceled = false
		AND app.is_approved = true
        AND unit.uid = {0}
) 
SELECT
	unit_name
	, room_name
	, count(*) AS 使用次數
	, SUM(end_time - start_time) AS 使用時數
FROM
	data_row
GROUP BY
	unit_name
	,room_name
                    ", unitID);
            }
            #endregion

            QueryHelper qh = new QueryHelper();
            DataTable dt = qh.Select(sql);

            Workbook book = new Workbook();
            Workbook template = new Workbook();
            template.Open(new MemoryStream(Properties.Resources.統計會議室報表樣板),FileFormatType.Excel2003);

            book.Copy(template);
            Worksheet sheet = book.Worksheets[0];
            Style style = sheet.Cells[2, 0].Style;

            #region 寫入資料
            int rowIndex = 3;
            sheet.Cells[0, 1].PutValue(startTime.Value.ToShortDateString() + " ~ " + endTime.Value.ToShortDateString());

            foreach (DataRow row in dt.Rows)
            {
                int colIndex = 0;

                sheet.Cells[rowIndex, colIndex].PutValue("" + row["unit_name"]);
                sheet.Cells[rowIndex, colIndex++].Style = style;

                sheet.Cells[rowIndex, colIndex].PutValue("" + row["room_name"]);
                sheet.Cells[rowIndex, colIndex++].Style = style;

                sheet.Cells[rowIndex, colIndex].PutValue("" + row["使用次數"]);
                sheet.Cells[rowIndex, colIndex++].Style = style;

                sheet.Cells[rowIndex, colIndex].PutValue("" + row["使用時數"]);
                sheet.Cells[rowIndex, colIndex++].Style = style;

            }
            #endregion

            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "Excel (*.xls)|*.xls|所有檔案 (*.*)|*.*";
            string fileName = "會議室統計報表";
            saveFile.FileName = fileName;

            try
            {
                if (saveFile.ShowDialog() == DialogResult.OK)
                {
                    book.Save(saveFile.FileName);
                    MsgBox.Show("會議室統計報表列印完成!");
                    this.Close();
                    Process.Start(saveFile.FileName);
                }
                else
                {
                    FISCA.Presentation.Controls.MsgBox.Show("檔案未儲存");
                    return;
                }
            }
            catch
            {
                MsgBox.Show("檔案儲存錯誤,請檢查檔案是否開啟中!!");
            }


        }

        private void leaveBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
