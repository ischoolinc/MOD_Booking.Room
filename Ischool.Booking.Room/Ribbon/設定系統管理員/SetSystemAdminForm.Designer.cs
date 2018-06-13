namespace Ischool.Booking.Room
{
    partial class SetSystemAdminForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.dataGridViewX1 = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.leaveBtn = new DevComponents.DotNetBar.ButtonX();
            this.addSystemAdminBtn = new DevComponents.DotNetBar.ButtonX();
            this.deleteSystemAdminBtn = new DevComponents.DotNetBar.ButtonX();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewX1)).BeginInit();
            this.SuspendLayout();
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Font = new System.Drawing.Font("微軟正黑體", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelX1.Location = new System.Drawing.Point(12, 12);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(124, 23);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "系統管理員清單";
            // 
            // dataGridViewX1
            // 
            this.dataGridViewX1.AllowUserToAddRows = false;
            this.dataGridViewX1.AllowUserToDeleteRows = false;
            this.dataGridViewX1.AllowUserToResizeColumns = false;
            this.dataGridViewX1.AllowUserToResizeRows = false;
            this.dataGridViewX1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewX1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewX1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewX1.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewX1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dataGridViewX1.Location = new System.Drawing.Point(12, 41);
            this.dataGridViewX1.Name = "dataGridViewX1";
            this.dataGridViewX1.RowTemplate.Height = 24;
            this.dataGridViewX1.Size = new System.Drawing.Size(562, 324);
            this.dataGridViewX1.TabIndex = 1;
            // 
            // leaveBtn
            // 
            this.leaveBtn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.leaveBtn.BackColor = System.Drawing.Color.Transparent;
            this.leaveBtn.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.leaveBtn.Location = new System.Drawing.Point(499, 371);
            this.leaveBtn.Name = "leaveBtn";
            this.leaveBtn.Size = new System.Drawing.Size(75, 23);
            this.leaveBtn.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.leaveBtn.TabIndex = 2;
            this.leaveBtn.Text = "離開";
            this.leaveBtn.Click += new System.EventHandler(this.leaveBtn_Click);
            // 
            // addSystemAdminBtn
            // 
            this.addSystemAdminBtn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.addSystemAdminBtn.BackColor = System.Drawing.Color.Transparent;
            this.addSystemAdminBtn.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.addSystemAdminBtn.Location = new System.Drawing.Point(241, 371);
            this.addSystemAdminBtn.Name = "addSystemAdminBtn";
            this.addSystemAdminBtn.Size = new System.Drawing.Size(123, 23);
            this.addSystemAdminBtn.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.addSystemAdminBtn.TabIndex = 3;
            this.addSystemAdminBtn.Text = "新增系統管理員";
            this.addSystemAdminBtn.Click += new System.EventHandler(this.addSystemAdminBtn_Click);
            // 
            // deleteSystemAdminBtn
            // 
            this.deleteSystemAdminBtn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.deleteSystemAdminBtn.BackColor = System.Drawing.Color.Transparent;
            this.deleteSystemAdminBtn.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.deleteSystemAdminBtn.Location = new System.Drawing.Point(370, 371);
            this.deleteSystemAdminBtn.Name = "deleteSystemAdminBtn";
            this.deleteSystemAdminBtn.Size = new System.Drawing.Size(123, 23);
            this.deleteSystemAdminBtn.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.deleteSystemAdminBtn.TabIndex = 4;
            this.deleteSystemAdminBtn.Text = "刪除系統管理員";
            this.deleteSystemAdminBtn.Click += new System.EventHandler(this.deleteSystemAdminBtn_Click);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "姓名";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "登入帳號";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "管理身分";
            this.Column3.Name = "Column3";
            this.Column3.Width = 110;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "建立者";
            this.Column4.Name = "Column4";
            // 
            // Column5
            // 
            this.Column5.HeaderText = "建立日期";
            this.Column5.Name = "Column5";
            // 
            // SetSystemAdminForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(586, 406);
            this.Controls.Add(this.deleteSystemAdminBtn);
            this.Controls.Add(this.addSystemAdminBtn);
            this.Controls.Add(this.leaveBtn);
            this.Controls.Add(this.dataGridViewX1);
            this.Controls.Add(this.labelX1);
            this.DoubleBuffered = true;
            this.Name = "SetSystemAdminForm";
            this.Text = "設定系統管理員";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewX1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.DataGridViewX dataGridViewX1;
        private DevComponents.DotNetBar.ButtonX leaveBtn;
        private DevComponents.DotNetBar.ButtonX addSystemAdminBtn;
        private DevComponents.DotNetBar.ButtonX deleteSystemAdminBtn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
    }
}