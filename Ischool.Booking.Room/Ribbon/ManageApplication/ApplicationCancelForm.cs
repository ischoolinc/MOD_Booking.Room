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

        public ApplicationCancelForm(string applicationID)
        {
            InitializeComponent();

            this._applicationID = applicationID;
            // 取得資料
            this._listApplication = this._access.Select<UDT.MeetingRoomApplication>(string.Format("uid = {0}",applicationID));
            this._listApplicationDetail = this._access.Select<UDT.MeetingRoomApplicationDetail>(string.Format("ref_application_id = {0}",applicationID));
            this._listRoom = this._access.Select<UDT.MeetingRoom>(string.Format("uid = {0}",_listApplication[0].RefMeetingRoomID));

            this._teacherR = Teacher.SelectByID("" + actor.getTeacherID());

            #region Init
            cancelDateLb.Text = DateTime.Now.ToShortDateString();
            applicantTbx.Text = _listApplication[0].ApplicantName;
            hostTbx.Text = _listApplication[0].TeacherName;
            roomNameTbx.Text = _listRoom[0].Name;
            applyStartTbx.Text = _listApplication[0].ApplyStarDate.ToShortDateString();
            RepeatEndTbx.Text = _listApplication[0].RepeatEndDate.ToShortDateString();
            applyReasonTbx.Text = _listApplication[0].ApplyReason;
            bool type = false;
            //bool.TryParse(("" + applyList[0].IsRepeat),out type)
            repeatTbx.Text = bool.TryParse(("" + _listApplication[0].IsRepeat), out type) ? "是" : "否";
            repeatTypeTbx.Text = ("" + _listApplication[0].RepeatType) == "null" ? "" : "" + _listApplication[0].RepeatType;

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
                        DAO.Application.UpdateApplicationByCancel("" + ckbxTrue.Checked, tbxCancelReason.Text.Trim(), actor.getTeacherID(), _teacherR.Name, this._applicationID);
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
            //ckbxTrue.Checked = true;
        }

        private void ckbxFalse_Click(object sender, EventArgs e)
        {
            ckbxTrue.Checked = false;
            //ckbxFalse.Checked = true;
        }

        private void leaveBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
