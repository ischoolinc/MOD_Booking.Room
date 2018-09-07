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

        public ApplicationReviewForm(string applicationID)
        {
            InitializeComponent();

            _applicationID = applicationID;
        }

        private void ApplicationReviewForm_Load(object sender, EventArgs e)
        {
            listApplication = this._access.Select<UDT.MeetingRoomApplication>(string.Format("uid = {0}", _applicationID));
            listApplicationDetail = this._access.Select<UDT.MeetingRoomApplicationDetail>(string.Format("ref_application_id = {0}", _applicationID));
            listRoom = this._access.Select<UDT.MeetingRoom>(string.Format("uid = {0}", listApplication[0].RefMeetingRoomID));

            teacherR = Teacher.SelectByID("" + actor.getTeacherID());

            #region Init
            lbReviewDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            applicantTbx.Text = listApplication[0].ApplicantName;
            hostTbx.Text = listApplication[0].TeacherName;
            roomNameTbx.Text = listRoom[0].Name;
            applyStartTbx.Text = listApplication[0].ApplyStarDate.ToShortDateString();
            RepeatEndTbx.Text = listApplication[0].RepeatEndDate.ToShortDateString();
            applyReasonTbx.Text = listApplication[0].ApplyReason;
            repeatTbx.Text = listApplication[0].IsRepeat ? "是" : "否";
            repeatTypeTbx.Text = ("" + listApplication[0].RepeatType) == "null" ? "" : "" + listApplication[0].RepeatType;

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
                        DAO.Application.UpdateApplicationByReview("" + ckbxTrue.Checked, tbxCancelReason.Text.Trim(), actor.getTeacherID(), teacherR.Name, this._applicationID);
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
