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
using K12.Data;

namespace Ischool.Booking.Room
{
    public partial class ApplicationReviewForm : BaseForm
    {
        List<UDT.MeetingRoomApplication> applyList = new List<UDT.MeetingRoomApplication>();
        List<UDT.MeetingRoomApplicationDetail> applyDetailList = new List<UDT.MeetingRoomApplicationDetail>();
        List<UDT.MeetingRoom> roomList = new List<UDT.MeetingRoom>();
        TeacherRecord teacherR = new TeacherRecord();
        Actor actor = new Actor();
        string _applicationID;

        public ApplicationReviewForm(string applicationID)
        {
            InitializeComponent();

            _applicationID = applicationID;
            AccessHelper access = new AccessHelper();
            // 取得資料
            applyList = access.Select<UDT.MeetingRoomApplication>("uid ="+ applicationID);
            applyDetailList = access.Select<UDT.MeetingRoomApplicationDetail>("ref_application_id ="+ applicationID);
            roomList = access.Select<UDT.MeetingRoom>("uid ="+ applyList[0].RefMeetingRoomID);
            
            teacherR = Teacher.SelectByID("" + Actor.RefTeacherID);

            #region Init
            reviewDateLb.Text = DateTime.Now.ToShortDateString();
            applicantTbx.Text = applyList[0].ApplicantName;
            hostTbx.Text = applyList[0].TeacherName;
            roomNameTbx.Text = roomList[0].Name;
            applyStartTbx.Text = applyList[0].ApplyStarDate.ToShortDateString();
            RepeatEndTbx.Text = applyList[0].RepeatEndDate.ToShortDateString();
            applyReasonTbx.Text = applyList[0].ApplyReason;

            foreach (UDT.MeetingRoomApplicationDetail ad in applyDetailList)
            {
                DataGridViewRow datarow = new DataGridViewRow();
                datarow.CreateCells(dataGridViewX1);
                int index = 0;

                datarow.Cells[index++].Value = ad.StarTime.ToShortDateString();
                datarow.Cells[index++].Value = ad.StarTime.ToShortTimeString();
                datarow.Cells[index++].Value = ad.EndTime.ToShortTimeString();

                dataGridViewX1.Rows.Add(datarow);
            }

            #endregion
        }

        private void leaveBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            if (falseCbx.Checked && "" + rejectReasonTbx.Text == "")
            {
                MsgBox.Show("請填寫未核准原因!");
                return;
            }
            else if (trueCbx.Checked && rejectReasonTbx.Text != "")
            {
                MsgBox.Show("不須填寫未核准原因!");
                return;
            }
            else if (!trueCbx.Checked && !falseCbx.Checked)
            {
                MsgBox.Show("請勾選是否核准此申請紀錄!");
                return;
            }
            else
            {
                DialogResult result = MsgBox.Show("確定儲存此次審核紀錄?", "提醒", MessageBoxButtons.YesNo);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    string sql = string.Format(@"
UPDATE
    $ischool.booking.meetingroom_application
SET
    is_approved = '{0}'
    , reviewed_date = '{1}'
    , reject_reason = '{2}'
    , ref_admin_id = {3}
    , admin_name = '{4}'
WHERE
    uid = {5}
                    ", trueCbx.Checked, DateTime.Parse(reviewDateLb.Text).ToShortDateString(), rejectReasonTbx.Text, Actor.RefTeacherID, teacherR.Name, _applicationID);

                    MsgBox.Show("儲存成功!");
                    this.Close();
                }
            }
        }

        private void trueCbx_CheckedChanged(object sender, EventArgs e)
        {
            if (trueCbx.Checked)
            {
                falseCbx.Checked = false;
                errorText.Visible = false;
            }
        }

        private void falseCbx_CheckedChanged(object sender, EventArgs e)
        {
            if (falseCbx.Checked)
            {
                trueCbx.Checked = false;
                errorText.Visible = true;
                errorText.Text = "請填寫未核准原因!";
            }
        }

        private void rejectReasonTbx_TextChanged(object sender, EventArgs e)
        {
            if (rejectReasonTbx.Text != "")
            {
                errorText.Visible = false;
            }
        }
    }
}
