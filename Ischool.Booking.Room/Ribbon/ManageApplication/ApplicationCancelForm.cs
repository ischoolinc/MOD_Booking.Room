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
    public partial class ApplicationCancelForm : BaseForm
    {
        private List<UDT.MeetingRoomApplication> _listApplication = new List<UDT.MeetingRoomApplication>();
        private List<UDT.MeetingRoomApplicationDetail> _listApplicationDetail = new List<UDT.MeetingRoomApplicationDetail>();
        private List<UDT.MeetingRoom> _listRoom = new List<UDT.MeetingRoom>();
        private TeacherRecord _teacherR = new TeacherRecord();
        private Actor actor =  Actor.Instance;
        private AccessHelper _access = new AccessHelper();
        private string _applicationID;
        private string _identity;

        public ApplicationCancelForm(string applicationID,string identity)
        {
            InitializeComponent();

            this._applicationID = applicationID;
            this._identity = identity;
        }

        private void ApplicationCancelForm_Load(object sender, EventArgs e)
        {
            // 取得資料
            this._listApplication = this._access.Select<UDT.MeetingRoomApplication>(string.Format("uid = {0}", this._applicationID));
            this._listApplicationDetail = this._access.Select<UDT.MeetingRoomApplicationDetail>(string.Format("ref_application_id = {0}", this._applicationID));
            this._listRoom = this._access.Select<UDT.MeetingRoom>(string.Format("uid = {0}", _listApplication[0].RefMeetingRoomID));

            this._teacherR = Teacher.SelectByID("" + actor.getTeacherID());

            #region Init
            cancelDateLb.Text = DateTime.Now.ToShortDateString();
            tbxApplicant.Text = _listApplication[0].ApplicantName;
            tbxHost.Text = _listApplication[0].TeacherName;
            tbxRoomName.Text = _listRoom[0].Name;
            tbxStartDate.Text = _listApplication[0].ApplyStarDate.ToShortDateString();
            tbxEndDate.Text = _listApplication[0].RepeatEndDate.ToShortDateString();
            tbxApplyReason.Text = _listApplication[0].ApplyReason;
            bool type = false;
            tbxRepeat.Text = bool.TryParse(("" + _listApplication[0].IsRepeat), out type) ? "是" : "否";
            tbxRepeatType.Text = ("" + _listApplication[0].RepeatType) == "null" ? "" : "" + _listApplication[0].RepeatType;

            foreach (UDT.MeetingRoomApplicationDetail ad in _listApplicationDetail)
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
            if (cancelResult_Validate() && tbxCancelReason_Validate())
            {
                DialogResult result = MsgBox.Show("確定儲存此次取消申請紀錄?", "提醒", MessageBoxButtons.YesNo);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    try
                    {
                        StringBuilder logs = GetLog();
                        DAO.Application.UpdateApplicationByCancel("" + ckbxTrue.Checked, tbxCancelReason.Text.Trim(), actor.getTeacherID(), _teacherR.Name, this._applicationID);
                        FISCA.LogAgent.ApplicationLog.Log("會議室預約", "取消申請紀錄", logs.ToString());
                        MsgBox.Show("儲存成功!");
                        this.DialogResult = DialogResult.Yes;
                        this.Close();
                    }
                    catch(Exception ex)
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
                    @"{0}「{1}」取消「{2}」申請「{3}」「{4}」~「{5}」的申請紀錄: "
                    , this._identity, teacherName, tbxApplicant.Text, tbxRoomName.Text, tbxStartDate.Text, tbxEndDate.Text));
            if (!ckbxTrue.Checked)
            {
                logs.AppendLine(string.Format("\n 作業時間「{0}」是否取消「{1}」", DateTime.Now.ToString("yyyy/MM/dd HH:mm"), "否"));
            }
            else
            {
                logs.AppendLine(string.Format("\n 作業時間「{0}」是否取消「{1}」取消原因「{2}」", DateTime.Now.ToString("yyyy/MM/dd HH:mm"), "是", tbxCancelReason.Text.Trim()));
            }

            return logs;
        }

        private bool cancelResult_Validate()
        {
            if (!ckbxTrue.Checked && !ckbxFalse.Checked)
            {
                errorProvider1.SetError(labelX9, "請勾選是否取消此申請紀錄!");
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
            if (ckbxTrue.Checked)
            {
                if (string.IsNullOrEmpty(tbxCancelReason.Text.Trim()))
                {
                    errorProvider1.SetError(tbxCancelReason, "請填寫取消原因!");
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
                    errorProvider1.SetError(tbxCancelReason, "不須填寫取消原因!");
                    return false;
                }
                else
                {
                    errorProvider1.SetError(tbxCancelReason, null);
                    return true;
                }
            }
        }

        private void ckbxTrue_Click(object sender, EventArgs e)
        {
            ckbxFalse.Checked = false;
        }

        private void ckbxFalse_Click(object sender, EventArgs e)
        {
            ckbxTrue.Checked = false;
        }

        private void leaveBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
