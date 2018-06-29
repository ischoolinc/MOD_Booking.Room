namespace Ischool.Booking.Room
{
    partial class MeetingRoomManagement
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridViewX1 = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.cbxUnit = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.addBtn = new DevComponents.DotNetBar.ButtonX();
            this.updateBtn = new DevComponents.DotNetBar.ButtonX();
            this.deleteBtn = new DevComponents.DotNetBar.ButtonX();
            this.leaveBtn = new DevComponents.DotNetBar.ButtonX();
            this.cbxIdentity = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.lblSysAdminRole = new DevComponents.DotNetBar.LabelX();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewX1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewX1
            // 
            this.dataGridViewX1.AllowUserToAddRows = false;
            this.dataGridViewX1.AllowUserToDeleteRows = false;
            this.dataGridViewX1.AllowUserToResizeColumns = false;
            this.dataGridViewX1.AllowUserToResizeRows = false;
            this.dataGridViewX1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewX1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewX1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewX1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column7,
            this.Column3,
            this.Column4,
            this.Column6,
            this.Column5});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewX1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewX1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dataGridViewX1.Location = new System.Drawing.Point(12, 41);
            this.dataGridViewX1.MultiSelect = false;
            this.dataGridViewX1.Name = "dataGridViewX1";
            this.dataGridViewX1.ReadOnly = true;
            this.dataGridViewX1.RowTemplate.Height = 24;
            this.dataGridViewX1.Size = new System.Drawing.Size(824, 351);
            this.dataGridViewX1.TabIndex = 0;
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(256, 10);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(60, 23);
            this.labelX1.TabIndex = 1;
            this.labelX1.Text = "管理單位";
            // 
            // labelX2
            // 
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.Class = "";
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(12, 10);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(60, 23);
            this.labelX2.TabIndex = 2;
            this.labelX2.Text = "管理身分";
            // 
            // cbxUnit
            // 
            this.cbxUnit.DisplayMember = "Text";
            this.cbxUnit.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbxUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxUnit.FormattingEnabled = true;
            this.cbxUnit.ItemHeight = 19;
            this.cbxUnit.Location = new System.Drawing.Point(318, 9);
            this.cbxUnit.Name = "cbxUnit";
            this.cbxUnit.Size = new System.Drawing.Size(132, 25);
            this.cbxUnit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbxUnit.TabIndex = 5;
            this.cbxUnit.SelectedIndexChanged += new System.EventHandler(this.unitCbx_SelectedIndexChanged_1);
            // 
            // addBtn
            // 
            this.addBtn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.addBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.addBtn.BackColor = System.Drawing.Color.Transparent;
            this.addBtn.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.addBtn.Location = new System.Drawing.Point(517, 398);
            this.addBtn.Name = "addBtn";
            this.addBtn.Size = new System.Drawing.Size(75, 23);
            this.addBtn.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.addBtn.TabIndex = 6;
            this.addBtn.Text = "新增";
            this.addBtn.Click += new System.EventHandler(this.addBtn_Click);
            // 
            // updateBtn
            // 
            this.updateBtn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.updateBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.updateBtn.BackColor = System.Drawing.Color.Transparent;
            this.updateBtn.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.updateBtn.Location = new System.Drawing.Point(598, 398);
            this.updateBtn.Name = "updateBtn";
            this.updateBtn.Size = new System.Drawing.Size(75, 23);
            this.updateBtn.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.updateBtn.TabIndex = 7;
            this.updateBtn.Text = "修改";
            this.updateBtn.Click += new System.EventHandler(this.updateBtn_Click);
            // 
            // deleteBtn
            // 
            this.deleteBtn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.deleteBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.deleteBtn.BackColor = System.Drawing.Color.Transparent;
            this.deleteBtn.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.deleteBtn.Location = new System.Drawing.Point(679, 398);
            this.deleteBtn.Name = "deleteBtn";
            this.deleteBtn.Size = new System.Drawing.Size(75, 23);
            this.deleteBtn.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.deleteBtn.TabIndex = 8;
            this.deleteBtn.Text = "刪除";
            this.deleteBtn.Click += new System.EventHandler(this.deleteBtn_Click);
            // 
            // leaveBtn
            // 
            this.leaveBtn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.leaveBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.leaveBtn.BackColor = System.Drawing.Color.Transparent;
            this.leaveBtn.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.leaveBtn.Location = new System.Drawing.Point(760, 398);
            this.leaveBtn.Name = "leaveBtn";
            this.leaveBtn.Size = new System.Drawing.Size(75, 23);
            this.leaveBtn.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.leaveBtn.TabIndex = 9;
            this.leaveBtn.Text = "離開";
            this.leaveBtn.Click += new System.EventHandler(this.leaveBtn_Click);
            // 
            // cbxIdentity
            // 
            this.cbxIdentity.DisplayMember = "Text";
            this.cbxIdentity.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbxIdentity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxIdentity.FormattingEnabled = true;
            this.cbxIdentity.ItemHeight = 19;
            this.cbxIdentity.Location = new System.Drawing.Point(78, 9);
            this.cbxIdentity.Name = "cbxIdentity";
            this.cbxIdentity.Size = new System.Drawing.Size(121, 25);
            this.cbxIdentity.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbxIdentity.TabIndex = 10;
            this.cbxIdentity.Visible = false;
            this.cbxIdentity.SelectedIndexChanged += new System.EventHandler(this.identityCbx_SelectedIndexChanged);
            // 
            // lblSysAdminRole
            // 
            this.lblSysAdminRole.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblSysAdminRole.BackgroundStyle.Class = "";
            this.lblSysAdminRole.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblSysAdminRole.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblSysAdminRole.ForeColor = System.Drawing.Color.Blue;
            this.lblSysAdminRole.Location = new System.Drawing.Point(76, 9);
            this.lblSysAdminRole.Name = "lblSysAdminRole";
            this.lblSysAdminRole.Size = new System.Drawing.Size(121, 25);
            this.lblSysAdminRole.TabIndex = 11;
            this.lblSysAdminRole.Text = "會議室模組管理者";
            this.lblSysAdminRole.Visible = false;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "場地名稱";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "所屬大樓";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "場地狀態";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Width = 83;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "容納人數";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 83;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "特殊場地";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 83;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "建立者";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Column5.DefaultCellStyle = dataGridViewCellStyle1;
            this.Column5.HeaderText = "設備";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // MeetingRoomManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(847, 431);
            this.Controls.Add(this.lblSysAdminRole);
            this.Controls.Add(this.cbxIdentity);
            this.Controls.Add(this.leaveBtn);
            this.Controls.Add(this.deleteBtn);
            this.Controls.Add(this.updateBtn);
            this.Controls.Add(this.addBtn);
            this.Controls.Add(this.cbxUnit);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.dataGridViewX1);
            this.DoubleBuffered = true;
            this.MaximizeBox = true;
            this.MinimizeBox = true;
            this.Name = "MeetingRoomManagement";
            this.Text = "管理會議室";
            this.Load += new System.EventHandler(this.MeetingRoomManagement_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewX1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.DataGridViewX dataGridViewX1;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbxUnit;
        private DevComponents.DotNetBar.ButtonX addBtn;
        private DevComponents.DotNetBar.ButtonX updateBtn;
        private DevComponents.DotNetBar.ButtonX deleteBtn;
        private DevComponents.DotNetBar.ButtonX leaveBtn;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbxIdentity;
        private DevComponents.DotNetBar.LabelX lblSysAdminRole;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
    }
}