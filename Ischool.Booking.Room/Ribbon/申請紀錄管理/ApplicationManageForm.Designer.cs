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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridViewX1 = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new DevComponents.DotNetBar.Controls.DataGridViewButtonXColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.unitLb = new DevComponents.DotNetBar.LabelX();
            this.unitCbx = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.actorLb = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.roomCbx = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.leaveBtn = new DevComponents.DotNetBar.ButtonX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.starTime = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.endTime = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.errorLb = new DevComponents.DotNetBar.LabelX();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cancelBtn = new System.Windows.Forms.ToolStripMenuItem();
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
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewX1.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewX1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dataGridViewX1.Location = new System.Drawing.Point(12, 79);
            this.dataGridViewX1.Name = "dataGridViewX1";
            this.dataGridViewX1.RowTemplate.Height = 24;
            this.dataGridViewX1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewX1.Size = new System.Drawing.Size(847, 384);
            this.dataGridViewX1.TabIndex = 11;
            this.dataGridViewX1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewX1_CellClick);
            this.dataGridViewX1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewX1_CellDoubleClick);
            this.dataGridViewX1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dataGridViewX1_MouseDown);
            // 
            // Column3
            // 
            this.Column3.HeaderText = "申請時間";
            this.Column3.Name = "Column3";
            // 
            // Column1
            // 
            this.Column1.HeaderText = "場地名稱";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.FillWeight = 80F;
            this.Column2.HeaderText = "申請人";
            this.Column2.Name = "Column2";
            this.Column2.Width = 80;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "申請開始日期";
            this.Column5.Name = "Column5";
            this.Column5.Width = 110;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "申請結束日期";
            this.Column6.Name = "Column6";
            this.Column6.Width = 110;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "審核狀態";
            this.Column4.Name = "Column4";
            this.Column4.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column4.Text = null;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "申請狀態";
            this.Column7.Name = "Column7";
            this.Column7.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "申請結果";
            this.Column8.Name = "Column8";
            // 
            // unitLb
            // 
            this.unitLb.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.unitLb.BackgroundStyle.Class = "";
            this.unitLb.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.unitLb.Location = new System.Drawing.Point(255, 12);
            this.unitLb.Name = "unitLb";
            this.unitLb.Size = new System.Drawing.Size(132, 23);
            this.unitLb.TabIndex = 3;
            this.unitLb.Text = "labelX4";
            // 
            // unitCbx
            // 
            this.unitCbx.DisplayMember = "Text";
            this.unitCbx.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.unitCbx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.unitCbx.FormattingEnabled = true;
            this.unitCbx.ItemHeight = 19;
            this.unitCbx.Location = new System.Drawing.Point(255, 11);
            this.unitCbx.Name = "unitCbx";
            this.unitCbx.Size = new System.Drawing.Size(122, 25);
            this.unitCbx.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.unitCbx.TabIndex = 4;
            this.unitCbx.SelectedIndexChanged += new System.EventHandler(this.unitCbx_SelectedIndexChanged);
            // 
            // labelX3
            // 
            this.labelX3.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.Class = "";
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(189, 12);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(60, 23);
            this.labelX3.TabIndex = 2;
            this.labelX3.Text = "管理單位";
            // 
            // actorLb
            // 
            this.actorLb.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.actorLb.BackgroundStyle.Class = "";
            this.actorLb.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.actorLb.ForeColor = System.Drawing.Color.Blue;
            this.actorLb.Location = new System.Drawing.Point(71, 12);
            this.actorLb.Name = "actorLb";
            this.actorLb.Size = new System.Drawing.Size(92, 23);
            this.actorLb.TabIndex = 1;
            this.actorLb.Text = "actor";
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
            this.labelX6.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX6.BackgroundStyle.Class = "";
            this.labelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX6.Location = new System.Drawing.Point(655, 11);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(40, 23);
            this.labelX6.TabIndex = 0;
            this.labelX6.Text = "場地";
            // 
            // roomCbx
            // 
            this.roomCbx.DisplayMember = "Text";
            this.roomCbx.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.roomCbx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.roomCbx.FormattingEnabled = true;
            this.roomCbx.ItemHeight = 19;
            this.roomCbx.Location = new System.Drawing.Point(701, 10);
            this.roomCbx.Name = "roomCbx";
            this.roomCbx.Size = new System.Drawing.Size(158, 25);
            this.roomCbx.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.roomCbx.TabIndex = 1;
            this.roomCbx.SelectedIndexChanged += new System.EventHandler(this.roomCbx_SelectedIndexChanged);
            // 
            // leaveBtn
            // 
            this.leaveBtn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.leaveBtn.BackColor = System.Drawing.Color.Transparent;
            this.leaveBtn.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.leaveBtn.Location = new System.Drawing.Point(784, 469);
            this.leaveBtn.Name = "leaveBtn";
            this.leaveBtn.Size = new System.Drawing.Size(75, 23);
            this.leaveBtn.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.leaveBtn.TabIndex = 12;
            this.leaveBtn.Text = "離開";
            this.leaveBtn.Click += new System.EventHandler(this.leaveBtn_Click);
            // 
            // labelX5
            // 
            this.labelX5.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.Class = "";
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Location = new System.Drawing.Point(415, 49);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(139, 23);
            this.labelX5.TabIndex = 1;
            this.labelX5.Text = "查詢場地借用日期區間";
            // 
            // starTime
            // 
            this.starTime.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.starTime.BackgroundStyle.Class = "DateTimeInputBackground";
            this.starTime.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.starTime.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.starTime.ButtonDropDown.Visible = true;
            this.starTime.IsPopupCalendarOpen = false;
            this.starTime.Location = new System.Drawing.Point(560, 47);
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
            this.endTime.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.endTime.BackgroundStyle.Class = "DateTimeInputBackground";
            this.endTime.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.endTime.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.endTime.ButtonDropDown.Visible = true;
            this.endTime.IsPopupCalendarOpen = false;
            this.endTime.Location = new System.Drawing.Point(724, 47);
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
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.Class = "";
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(701, 49);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(17, 23);
            this.labelX2.TabIndex = 13;
            this.labelX2.Text = "至";
            // 
            // errorLb
            // 
            this.errorLb.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.errorLb.BackgroundStyle.Class = "";
            this.errorLb.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.errorLb.ForeColor = System.Drawing.Color.Red;
            this.errorLb.Location = new System.Drawing.Point(12, 469);
            this.errorLb.Name = "errorLb";
            this.errorLb.Size = new System.Drawing.Size(271, 23);
            this.errorLb.TabIndex = 14;
            this.errorLb.Text = "errorText";
            this.errorLb.Visible = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cancelBtn});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 48);
            // 
            // cancelBtn
            // 
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(152, 22);
            this.cancelBtn.Text = "取消申請紀錄";
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // ReviewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(871, 504);
            this.Controls.Add(this.errorLb);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.actorLb);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.endTime);
            this.Controls.Add(this.unitCbx);
            this.Controls.Add(this.leaveBtn);
            this.Controls.Add(this.unitLb);
            this.Controls.Add(this.starTime);
            this.Controls.Add(this.roomCbx);
            this.Controls.Add(this.labelX5);
            this.Controls.Add(this.labelX6);
            this.Controls.Add(this.dataGridViewX1);
            this.DoubleBuffered = true;
            this.Name = "ReviewForm";
            this.Text = "申請紀錄管理";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewX1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.starTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.endTime)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private DevComponents.DotNetBar.Controls.DataGridViewX dataGridViewX1;
        private DevComponents.DotNetBar.LabelX unitLb;
        private DevComponents.DotNetBar.Controls.ComboBoxEx unitCbx;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.LabelX actorLb;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.DotNetBar.Controls.ComboBoxEx roomCbx;
        private DevComponents.DotNetBar.ButtonX leaveBtn;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput endTime;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput starTime;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.LabelX labelX2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private DevComponents.DotNetBar.Controls.DataGridViewButtonXColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private DevComponents.DotNetBar.LabelX errorLb;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem cancelBtn;
    }
}