namespace Ischool.Booking.Room
{
    partial class ReviewForm
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridViewX1 = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new DevComponents.DotNetBar.Controls.DataGridViewButtonXColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cbxUnit = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.lbSysAdmin = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.cbxRoom = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.leaveBtn = new DevComponents.DotNetBar.ButtonX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.starTime = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.endTime = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cancelBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.ckbxSelectAll = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.cbxIdentity = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewX1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.starTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.endTime)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
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
            this.Column3,
            this.Column1,
            this.Column2,
            this.Column5,
            this.Column6,
            this.Column4,
            this.Column7,
            this.Column8});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewX1.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewX1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dataGridViewX1.Location = new System.Drawing.Point(12, 92);
            this.dataGridViewX1.MultiSelect = false;
            this.dataGridViewX1.Name = "dataGridViewX1";
            this.dataGridViewX1.ReadOnly = true;
            this.dataGridViewX1.RowTemplate.Height = 24;
            this.dataGridViewX1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewX1.Size = new System.Drawing.Size(847, 413);
            this.dataGridViewX1.TabIndex = 11;
            this.dataGridViewX1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewX1_CellClick);
            this.dataGridViewX1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewX1_CellDoubleClick);
            this.dataGridViewX1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dataGridViewX1_MouseDown);
            // 
            // Column3
            // 
            this.Column3.HeaderText = "申請時間";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.HeaderText = "會議室名稱";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.FillWeight = 80F;
            this.Column2.HeaderText = "申請人";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 80;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "申請開始日期";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 110;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "申請結束日期";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Width = 110;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "審核狀態";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column4.Text = null;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "審核結果";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "申請單狀態";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            // 
            // cbxUnit
            // 
            this.cbxUnit.DisplayMember = "Text";
            this.cbxUnit.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbxUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxUnit.FormattingEnabled = true;
            this.cbxUnit.ItemHeight = 19;
            this.cbxUnit.Location = new System.Drawing.Point(280, 11);
            this.cbxUnit.Name = "cbxUnit";
            this.cbxUnit.Size = new System.Drawing.Size(122, 25);
            this.cbxUnit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbxUnit.TabIndex = 4;
            this.cbxUnit.SelectedIndexChanged += new System.EventHandler(this.unitCbx_SelectedIndexChanged);
            // 
            // labelX3
            // 
            this.labelX3.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.Class = "";
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(214, 12);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(60, 23);
            this.labelX3.TabIndex = 2;
            this.labelX3.Text = "管理單位";
            // 
            // lbSysAdmin
            // 
            this.lbSysAdmin.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lbSysAdmin.BackgroundStyle.Class = "";
            this.lbSysAdmin.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lbSysAdmin.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbSysAdmin.Location = new System.Drawing.Point(71, 12);
            this.lbSysAdmin.Name = "lbSysAdmin";
            this.lbSysAdmin.Size = new System.Drawing.Size(120, 23);
            this.lbSysAdmin.TabIndex = 1;
            this.lbSysAdmin.Text = "會議室預約管理員";
            this.lbSysAdmin.Visible = false;
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(12, 12);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(60, 23);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "管理身分";
            // 
            // labelX6
            // 
            this.labelX6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelX6.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX6.BackgroundStyle.Class = "";
            this.labelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX6.Location = new System.Drawing.Point(641, 12);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(54, 23);
            this.labelX6.TabIndex = 0;
            this.labelX6.Text = "會議室";
            // 
            // cbxRoom
            // 
            this.cbxRoom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxRoom.DisplayMember = "Text";
            this.cbxRoom.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbxRoom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxRoom.FormattingEnabled = true;
            this.cbxRoom.ItemHeight = 19;
            this.cbxRoom.Location = new System.Drawing.Point(701, 11);
            this.cbxRoom.Name = "cbxRoom";
            this.cbxRoom.Size = new System.Drawing.Size(158, 25);
            this.cbxRoom.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbxRoom.TabIndex = 1;
            this.cbxRoom.SelectedIndexChanged += new System.EventHandler(this.roomCbx_SelectedIndexChanged);
            // 
            // leaveBtn
            // 
            this.leaveBtn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.leaveBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.leaveBtn.BackColor = System.Drawing.Color.Transparent;
            this.leaveBtn.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.leaveBtn.Location = new System.Drawing.Point(784, 521);
            this.leaveBtn.Name = "leaveBtn";
            this.leaveBtn.Size = new System.Drawing.Size(75, 23);
            this.leaveBtn.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.leaveBtn.TabIndex = 12;
            this.leaveBtn.Text = "離開";
            this.leaveBtn.Click += new System.EventHandler(this.leaveBtn_Click);
            // 
            // labelX5
            // 
            this.labelX5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelX5.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.Class = "";
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Location = new System.Drawing.Point(415, 60);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(139, 23);
            this.labelX5.TabIndex = 1;
            this.labelX5.Text = "查詢場地借用日期區間";
            // 
            // starTime
            // 
            this.starTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.starTime.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.starTime.BackgroundStyle.Class = "DateTimeInputBackground";
            this.starTime.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.starTime.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.starTime.ButtonDropDown.Visible = true;
            this.starTime.IsPopupCalendarOpen = false;
            this.starTime.Location = new System.Drawing.Point(560, 59);
            // 
            // 
            // 
            this.starTime.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.starTime.MonthCalendar.BackgroundStyle.BackColor = System.Drawing.SystemColors.Window;
            this.starTime.MonthCalendar.BackgroundStyle.Class = "";
            this.starTime.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.starTime.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.starTime.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.starTime.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.starTime.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.starTime.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.starTime.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.starTime.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.starTime.MonthCalendar.CommandsBackgroundStyle.Class = "";
            this.starTime.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.starTime.MonthCalendar.DisplayMonth = new System.DateTime(2018, 6, 1, 0, 0, 0, 0);
            this.starTime.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.starTime.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.starTime.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.starTime.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.starTime.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.starTime.MonthCalendar.NavigationBackgroundStyle.Class = "";
            this.starTime.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.starTime.MonthCalendar.TodayButtonVisible = true;
            this.starTime.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.starTime.Name = "starTime";
            this.starTime.Size = new System.Drawing.Size(135, 25);
            this.starTime.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.starTime.TabIndex = 2;
            this.starTime.ValueChanged += new System.EventHandler(this.starTime_ValueChanged);
            // 
            // endTime
            // 
            this.endTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.endTime.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.endTime.BackgroundStyle.Class = "DateTimeInputBackground";
            this.endTime.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.endTime.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.endTime.ButtonDropDown.Visible = true;
            this.endTime.IsPopupCalendarOpen = false;
            this.endTime.Location = new System.Drawing.Point(724, 59);
            // 
            // 
            // 
            this.endTime.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.endTime.MonthCalendar.BackgroundStyle.BackColor = System.Drawing.SystemColors.Window;
            this.endTime.MonthCalendar.BackgroundStyle.Class = "";
            this.endTime.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.endTime.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.endTime.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.endTime.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.endTime.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.endTime.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.endTime.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.endTime.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.endTime.MonthCalendar.CommandsBackgroundStyle.Class = "";
            this.endTime.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.endTime.MonthCalendar.DisplayMonth = new System.DateTime(2018, 6, 1, 0, 0, 0, 0);
            this.endTime.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.endTime.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.endTime.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.endTime.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.endTime.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.endTime.MonthCalendar.NavigationBackgroundStyle.Class = "";
            this.endTime.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.endTime.MonthCalendar.TodayButtonVisible = true;
            this.endTime.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.endTime.Name = "endTime";
            this.endTime.Size = new System.Drawing.Size(135, 25);
            this.endTime.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.endTime.TabIndex = 3;
            this.endTime.ValueChanged += new System.EventHandler(this.endTime_ValueChanged);
            // 
            // labelX2
            // 
            this.labelX2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.Class = "";
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(701, 60);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(17, 23);
            this.labelX2.TabIndex = 13;
            this.labelX2.Text = "至";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cancelBtn});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(147, 26);
            // 
            // cancelBtn
            // 
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(146, 22);
            this.cancelBtn.Text = "取消申請紀錄";
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // ckbxSelectAll
            // 
            this.ckbxSelectAll.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.ckbxSelectAll.BackgroundStyle.Class = "";
            this.ckbxSelectAll.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ckbxSelectAll.Location = new System.Drawing.Point(12, 60);
            this.ckbxSelectAll.Name = "ckbxSelectAll";
            this.ckbxSelectAll.Size = new System.Drawing.Size(154, 23);
            this.ckbxSelectAll.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ckbxSelectAll.TabIndex = 15;
            this.ckbxSelectAll.Text = "檢視所有申請紀錄";
            this.ckbxSelectAll.CheckedChanged += new System.EventHandler(this.conditionCbx_CheckedChanged);
            // 
            // labelX4
            // 
            this.labelX4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelX4.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.Class = "";
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Location = new System.Drawing.Point(12, 509);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(502, 42);
            this.labelX4.TabIndex = 16;
            this.labelX4.Text = "說明 : 畫面只會顯示須審核的申請紀錄，或者可勾選左上\r\n          方檢視所有申請紀錄按鈕，總覽會議室所有申請紀錄。";
            // 
            // cbxIdentity
            // 
            this.cbxIdentity.DisplayMember = "Text";
            this.cbxIdentity.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbxIdentity.FormattingEnabled = true;
            this.cbxIdentity.ItemHeight = 19;
            this.cbxIdentity.Location = new System.Drawing.Point(78, 11);
            this.cbxIdentity.Name = "cbxIdentity";
            this.cbxIdentity.Size = new System.Drawing.Size(102, 25);
            this.cbxIdentity.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbxIdentity.TabIndex = 17;
            this.cbxIdentity.Visible = false;
            // 
            // ReviewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(871, 556);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.ckbxSelectAll);
            this.Controls.Add(this.cbxUnit);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.endTime);
            this.Controls.Add(this.leaveBtn);
            this.Controls.Add(this.starTime);
            this.Controls.Add(this.cbxRoom);
            this.Controls.Add(this.labelX5);
            this.Controls.Add(this.labelX6);
            this.Controls.Add(this.dataGridViewX1);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.lbSysAdmin);
            this.Controls.Add(this.cbxIdentity);
            this.DoubleBuffered = true;
            this.Name = "ReviewForm";
            this.Text = "審核作業";
            this.Load += new System.EventHandler(this.ReviewForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewX1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.starTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.endTime)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private DevComponents.DotNetBar.Controls.DataGridViewX dataGridViewX1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbxUnit;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.LabelX lbSysAdmin;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbxRoom;
        private DevComponents.DotNetBar.ButtonX leaveBtn;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput endTime;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput starTime;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.LabelX labelX2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem cancelBtn;
        private DevComponents.DotNetBar.Controls.CheckBoxX ckbxSelectAll;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbxIdentity;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private DevComponents.DotNetBar.Controls.DataGridViewButtonXColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
    }
}