namespace Ischool.Booking.Room
{
    partial class StatisticalReportForm
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
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.identifyLb = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.unitCbx = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.printBtn = new DevComponents.DotNetBar.ButtonX();
            this.leaveBtn = new DevComponents.DotNetBar.ButtonX();
            this.startTime = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.endTime = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.cbxIdentity = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            ((System.ComponentModel.ISupportInitialize)(this.startTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.endTime)).BeginInit();
            this.groupPanel1.SuspendLayout();
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
            this.labelX1.Location = new System.Drawing.Point(12, 12);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(60, 23);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "管理身分";
            // 
            // identifyLb
            // 
            this.identifyLb.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.identifyLb.BackgroundStyle.Class = "";
            this.identifyLb.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.identifyLb.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.identifyLb.Location = new System.Drawing.Point(73, 12);
            this.identifyLb.Name = "identifyLb";
            this.identifyLb.Size = new System.Drawing.Size(122, 23);
            this.identifyLb.TabIndex = 1;
            this.identifyLb.Text = "會議室模組管理者";
            this.identifyLb.Visible = false;
            // 
            // labelX3
            // 
            this.labelX3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelX3.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.Class = "";
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(206, 12);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(60, 23);
            this.labelX3.TabIndex = 2;
            this.labelX3.Text = "管理單位";
            // 
            // unitCbx
            // 
            this.unitCbx.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.unitCbx.DisplayMember = "Text";
            this.unitCbx.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.unitCbx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.unitCbx.FormattingEnabled = true;
            this.unitCbx.ItemHeight = 19;
            this.unitCbx.Location = new System.Drawing.Point(270, 11);
            this.unitCbx.Name = "unitCbx";
            this.unitCbx.Size = new System.Drawing.Size(115, 25);
            this.unitCbx.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.unitCbx.TabIndex = 3;
            // 
            // printBtn
            // 
            this.printBtn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.printBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.printBtn.BackColor = System.Drawing.Color.Transparent;
            this.printBtn.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.printBtn.Location = new System.Drawing.Point(229, 223);
            this.printBtn.Name = "printBtn";
            this.printBtn.Size = new System.Drawing.Size(75, 23);
            this.printBtn.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.printBtn.TabIndex = 4;
            this.printBtn.Text = "列印";
            this.printBtn.Click += new System.EventHandler(this.printBtn_Click);
            // 
            // leaveBtn
            // 
            this.leaveBtn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.leaveBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.leaveBtn.BackColor = System.Drawing.Color.Transparent;
            this.leaveBtn.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.leaveBtn.Location = new System.Drawing.Point(310, 223);
            this.leaveBtn.Name = "leaveBtn";
            this.leaveBtn.Size = new System.Drawing.Size(75, 23);
            this.leaveBtn.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.leaveBtn.TabIndex = 5;
            this.leaveBtn.Text = "離開";
            this.leaveBtn.Click += new System.EventHandler(this.leaveBtn_Click);
            // 
            // startTime
            // 
            this.startTime.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.startTime.BackgroundStyle.Class = "DateTimeInputBackground";
            this.startTime.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.startTime.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.startTime.ButtonDropDown.Visible = true;
            this.startTime.IsPopupCalendarOpen = false;
            this.startTime.Location = new System.Drawing.Point(139, 23);
            // 
            // 
            // 
            this.startTime.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.startTime.MonthCalendar.BackgroundStyle.BackColor = System.Drawing.SystemColors.Window;
            this.startTime.MonthCalendar.BackgroundStyle.Class = "";
            this.startTime.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.startTime.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.startTime.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.startTime.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.startTime.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.startTime.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.startTime.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.startTime.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.startTime.MonthCalendar.CommandsBackgroundStyle.Class = "";
            this.startTime.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.startTime.MonthCalendar.DisplayMonth = new System.DateTime(2018, 6, 1, 0, 0, 0, 0);
            this.startTime.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.startTime.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.startTime.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.startTime.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.startTime.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.startTime.MonthCalendar.NavigationBackgroundStyle.Class = "";
            this.startTime.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.startTime.MonthCalendar.TodayButtonVisible = true;
            this.startTime.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.startTime.Name = "startTime";
            this.startTime.Size = new System.Drawing.Size(161, 25);
            this.startTime.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.startTime.TabIndex = 7;
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
            this.endTime.Location = new System.Drawing.Point(139, 70);
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
            this.endTime.Size = new System.Drawing.Size(161, 25);
            this.endTime.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.endTime.TabIndex = 8;
            // 
            // labelX2
            // 
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.Class = "";
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(48, 24);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(60, 23);
            this.labelX2.TabIndex = 9;
            this.labelX2.Text = "開始時間";
            // 
            // labelX5
            // 
            this.labelX5.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.Class = "";
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Location = new System.Drawing.Point(48, 71);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(60, 23);
            this.labelX5.TabIndex = 10;
            this.labelX5.Text = "結束時間";
            // 
            // groupPanel1
            // 
            this.groupPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupPanel1.BackColor = System.Drawing.Color.Transparent;
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.labelX2);
            this.groupPanel1.Controls.Add(this.labelX5);
            this.groupPanel1.Controls.Add(this.startTime);
            this.groupPanel1.Controls.Add(this.endTime);
            this.groupPanel1.Location = new System.Drawing.Point(12, 57);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(373, 157);
            // 
            // 
            // 
            this.groupPanel1.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel1.Style.BackColorGradientAngle = 90;
            this.groupPanel1.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel1.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderBottomWidth = 1;
            this.groupPanel1.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel1.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderLeftWidth = 1;
            this.groupPanel1.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderRightWidth = 1;
            this.groupPanel1.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderTopWidth = 1;
            this.groupPanel1.Style.Class = "";
            this.groupPanel1.Style.CornerDiameter = 4;
            this.groupPanel1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel1.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel1.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseDown.Class = "";
            this.groupPanel1.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseOver.Class = "";
            this.groupPanel1.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel1.TabIndex = 11;
            this.groupPanel1.Text = "統計日期區間";
            // 
            // cbxIdentity
            // 
            this.cbxIdentity.DisplayMember = "Text";
            this.cbxIdentity.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbxIdentity.FormattingEnabled = true;
            this.cbxIdentity.ItemHeight = 19;
            this.cbxIdentity.Location = new System.Drawing.Point(74, 10);
            this.cbxIdentity.Name = "cbxIdentity";
            this.cbxIdentity.Size = new System.Drawing.Size(121, 25);
            this.cbxIdentity.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbxIdentity.TabIndex = 12;
            // 
            // StatisticalReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(397, 258);
            this.Controls.Add(this.cbxIdentity);
            this.Controls.Add(this.groupPanel1);
            this.Controls.Add(this.leaveBtn);
            this.Controls.Add(this.printBtn);
            this.Controls.Add(this.unitCbx);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.identifyLb);
            this.Controls.Add(this.labelX1);
            this.DoubleBuffered = true;
            this.Name = "StatisticalReportForm";
            this.Text = "統計場地使用狀況";
            this.Load += new System.EventHandler(this.StatisticalReportForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.startTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.endTime)).EndInit();
            this.groupPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX identifyLb;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.ComboBoxEx unitCbx;
        private DevComponents.DotNetBar.ButtonX printBtn;
        private DevComponents.DotNetBar.ButtonX leaveBtn;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput startTime;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput endTime;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbxIdentity;
    }
}