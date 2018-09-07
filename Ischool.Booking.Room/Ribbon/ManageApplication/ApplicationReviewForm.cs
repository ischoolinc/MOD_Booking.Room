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
        private List<UDT.MeetingRoomApplication> listApplication = new List<UDT.MeetingRoomApplication>();
        private List<UDT.MeetingRoomApplicationDetail> listApplicationDetail = new List<UDT.MeetingRoomApplicationDetail>();
        private List<UDT.MeetingRoom> listRoom = new List<UDT.MeetingRoom>();
        private TeacherRecord teacherR = new TeacherRecord();
        private Actor actor = Actor.Instance;
        private AccessHelper _access = new AccessHelper();
        private string _applicationID;
        private string _identity;

        public ApplicationReviewForm(string applicationID,string identity)
        {
            InitializeComponent();

            this._applicationID = applicationID;
            this._identity = identity;
        }

        private void ApplicationReviewForm_Load(object sender, EventArgs e)
        {
            listApplication = this._access.Select<UDT.MeetingRoomApplication>(string.Format("uid = {0}", _applicationID));
            listApplicationDetail = this._access.Select<UDT.MeetingRoomApplicationDetail>(string.Format("ref_application_id = {0}", _applicationID));
            listRoom = this._access.Select<UDT.MeetingRoom>(string.Format("uid = {0}", listApplication[0].RefMeetingRoomID));

            teacherR = Teacher.SelectByID("" + actor.getTeacherID());

            #region Init
            lbReviewDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            tbxApplicant.Text = listApplication[0].ApplicantName;
            tbxHost.Text = listApplication[0].TeacherName;
            tbxRoomName.Text = listRoom[0].Name;
            tbxStartDate.Text = listApplication[0].ApplyStarDate.ToShortDateString();
            tbxEndDate.Text = listApplication[0].RepeatEndDate.ToShortDateString();
            tbxApplyReason.Text = listApplication[0].ApplyReason;
            tbxIsRepeat.Text = listApplication[0].IsRepeat ? "是" : "否";
            tbxRepeatType.Text = ("" + listApplication[0].RepeatType) == "null" ? "" : "" + listApplication[0].RepeatType;

            foreach (UDT.MeetingRoomApplicationDetail ad in listApplicationDetail)
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

        private void saveBtn_Click(object sender, EventArgs e)
        {
            if (reviewResult_Validate() && tbxCancelReason_Validate())
            {
                DialogResult result = MsgBox.Show("確定儲存此次審核紀錄?", "提醒", MessageBoxButtons.YesNo);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    try
                    {
                        StringBuilder logs = GetLog();
                        DAO.Application.UpdateApplicationByReview("" + ckbxTrue.Checked, tbxCancelReason.Text.Trim(), actor.getTeacherID(), teacherR.Name, this._applicationID);
                        FISCA.LogAgent.ApplicationLog.Log("會議室預約", "審核申請紀錄", logs.ToString());
                        MsgBox.Show("儲存成功!");
                        this.DialogResult = DialogResult.Yes;
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MsgBox.Show(ex.Message);
                    }
                }
            }
        }

        private StringBuilder GetLog()
        {
            string teacherName = Teacher.SelectByID(Actor.Instance.getTeacherID()).Name;

            StringBuilder logs = new StringBuilder(string.Format(
                    @"{0}「{1}」審核「{2}」申請「{3}」「{4}」~「{5}」的申請紀錄: "
                    , this._identity, teacherName,tbxApplicant.Text,tbxRoomName.Text,tbxStartDate.Text,tbxEndDate.Text));
            if (ckbxTrue.Checked)
            {
                logs.AppendLine(string.Format("\n 審核時間「{0}」審核結果「{1}」",DateTime.Now.ToString("yyyy/MM/dd HH:mm"),"核准"));
            }
            else
            {
                logs.AppendLine(string.Format("\n 審核時間「{0}」審核結果「{1}」未核准原因「{2}」", DateTime.Now.ToString("yyyy/MM/dd HH:mm"), "未核准",tbxCancelReason.Text.Trim()));
            }

            return logs;
        }

        private bool reviewResult_Validate()
        {
            if (!ckbxTrue.Checked && !ckbxFalse.Checked)
            {
                errorProvider1.SetError(labelX9, "請勾選是否核准此申請紀錄!");
                return false;
            }
            else
            {
                errorProvider1.SetError(labelX9, null);
                return true;
            }
        }

        private bool tbxCancelReason_Validate()
        {
            if (!ckbxTrue.Checked)
            {
                if (string.IsNullOrEmpty(tbxCancelReason.Text.Trim()))
                {
                    errorProvider1.SetError(tbxCancelReason, "請填寫未核准原因!");
                    return false;
                }
                else
                {
                    errorProvider1.SetError(tbxCancelReason, null);
                    return true;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(tbxCancelReason.Text.Trim()))
                {
                    errorProvider1.SetError(tbxCancelReason, "不須填寫未核准原因!");
                    return false;
                }
                else
                {
                    errorProvider1.SetError(tbxCancelReason, null);
                    return true;
                }
            }
        }

        private void trueCbx_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbxTrue.Checked)
            {
                ckbxFalse.Checked = false;
            }
        }

        private void falseCbx_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbxFalse.Checked)
            {
                ckbxTrue.Checked = false;
            }
        }

        private void rejectReasonTbx_TextChanged(object sender, EventArgs e)
        {
            tbxCancelReason_Validate();   
        }

        private void leaveBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
