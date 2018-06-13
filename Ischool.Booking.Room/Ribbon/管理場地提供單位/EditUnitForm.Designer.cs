namespace Ischool.Booking.Room
{
    partial class EditUnitForm
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
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.unitNameTbx = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.unitBossTbx = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.dataGridViewX1 = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new DevComponents.DotNetBar.Controls.DataGridViewButtonXColumn();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.searchTbx = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.dateLb = new DevComponents.DotNetBar.LabelX();
            this.leaveBtn = new DevComponents.DotNetBar.ButtonX();
            this.saveBtn = new DevComponents.DotNetBar.ButtonX();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.errorLb = new DevComponents.DotNetBar.LabelX();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewX1)).BeginInit();
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
            this.labelX1.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelX1.Location = new System.Drawing.Point(12, 12);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(60, 23);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "單位名稱";
            // 
            // unitNameTbx
            // 
            // 
            // 
            // 
            this.unitNameTbx.Border.Class = "TextBoxBorder";
            this.unitNameTbx.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.unitNameTbx.Location = new System.Drawing.Point(78, 11);
            this.unitNameTbx.Name = "unitNameTbx";
            this.unitNameTbx.Size = new System.Drawing.Size(131, 25);
            this.unitNameTbx.TabIndex = 1;
            // 
            // labelX2
            // 
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.Class = "";
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelX2.Location = new System.Drawing.Point(12, 53);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(60, 23);
            this.labelX2.TabIndex = 2;
            this.labelX2.Text = "單位主管";
            // 
            // unitBossTbx
            // 
            // 
            // 
            // 
            this.unitBossTbx.Border.Class = "TextBoxBorder";
            this.unitBossTbx.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.unitBossTbx.Enabled = false;
            this.unitBossTbx.Location = new System.Drawing.Point(78, 52);
            this.unitBossTbx.Name = "unitBossTbx";
            this.unitBossTbx.Size = new System.Drawing.Size(131, 25);
            this.unitBossTbx.TabIndex = 3;
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
            this.Column3,
            this.Column2,
            this.Column4,
            this.Column5,
            this.Column6});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewX1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewX1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataGridViewX1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dataGridViewX1.Location = new System.Drawing.Point(0, 41);
            this.dataGridViewX1.Name = "dataGridViewX1";
            this.dataGridViewX1.RowTemplate.Height = 24;
            this.dataGridViewX1.Size = new System.Drawing.Size(620, 276);
            this.dataGridViewX1.TabIndex = 4;
            this.dataGridViewX1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewX1_CellClick);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "姓名";
            this.Column1.Name = "Column1";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "暱稱";
            this.Column3.Name = "Column3";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "性別";
            this.Column2.Name = "Column2";
            this.Column2.Width = 60;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "登入帳號";
            this.Column4.Name = "Column4";
            // 
            // Column5
            // 
            this.Column5.HeaderText = "科別";
            this.Column5.Name = "Column5";
            // 
            // Column6
            // 
            this.Column6.HeaderText = "";
            this.Column6.Name = "Column6";
            this.Column6.Text = null;
            // 
            // labelX3
            // 
            this.labelX3.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.Class = "";
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(408, 11);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(85, 23);
            this.labelX3.TabIndex = 5;
            this.labelX3.Text = "搜尋老師姓名";
            // 
            // searchTbx
            // 
            // 
            // 
            // 
            this.searchTbx.Border.Class = "TextBoxBorder";
            this.searchTbx.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.searchTbx.Location = new System.Drawing.Point(504, 10);
            this.searchTbx.Name = "searchTbx";
            this.searchTbx.Size = new System.Drawing.Size(110, 25);
            this.searchTbx.TabIndex = 6;
            this.searchTbx.TextChanged += new System.EventHandler(this.searchTbx_TextChanged);
            // 
            // dateLb
            // 
            this.dateLb.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.dateLb.BackgroundStyle.Class = "";
            this.dateLb.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dateLb.Location = new System.Drawing.Point(519, 12);
            this.dateLb.Name = "dateLb";
            this.dateLb.Size = new System.Drawing.Size(119, 23);
            this.dateLb.TabIndex = 7;
            this.dateLb.Text = "日期";
            // 
            // leaveBtn
            // 
            this.leaveBtn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.leaveBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.leaveBtn.BackColor = System.Drawing.Color.Transparent;
            this.leaveBtn.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.leaveBtn.Location = new System.Drawing.Point(563, 437);
            this.leaveBtn.Name = "leaveBtn";
            this.leaveBtn.Size = new System.Drawing.Size(75, 23);
            this.leaveBtn.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.leaveBtn.TabIndex = 8;
            this.leaveBtn.Text = "離開";
            this.leaveBtn.Click += new System.EventHandler(this.leaveBtn_Click);
            // 
            // saveBtn
            // 
            this.saveBtn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.saveBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveBtn.BackColor = System.Drawing.Color.Transparent;
            this.saveBtn.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.saveBtn.Location = new System.Drawing.Point(482, 437);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(75, 23);
            this.saveBtn.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.saveBtn.TabIndex = 9;
            this.saveBtn.Text = "儲存";
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // groupPanel1
            // 
            this.groupPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupPanel1.BackColor = System.Drawing.Color.Transparent;
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.labelX4);
            this.groupPanel1.Controls.Add(this.dataGridViewX1);
            this.groupPanel1.Controls.Add(this.searchTbx);
            this.groupPanel1.Controls.Add(this.labelX3);
            this.groupPanel1.Location = new System.Drawing.Point(12, 87);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(626, 344);
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
            this.groupPanel1.TabIndex = 10;
            this.groupPanel1.Text = "選擇單位主管";
            // 
            // labelX4
            // 
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.Class = "";
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Font = new System.Drawing.Font("微軟正黑體", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelX4.Location = new System.Drawing.Point(3, 11);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(75, 23);
            this.labelX4.TabIndex = 7;
            this.labelX4.Text = "老師清單";
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
            this.errorLb.Location = new System.Drawing.Point(12, 437);
            this.errorLb.Name = "errorLb";
            this.errorLb.Size = new System.Drawing.Size(137, 23);
            this.errorLb.TabIndex = 11;
            this.errorLb.Text = "errorText";
            this.errorLb.Visible = false;
            // 
            // EditUnitForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 472);
            this.Controls.Add(this.errorLb);
            this.Controls.Add(this.groupPanel1);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.leaveBtn);
            this.Controls.Add(this.dateLb);
            this.Controls.Add(this.unitBossTbx);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.unitNameTbx);
            this.Controls.Add(this.labelX1);
            this.DoubleBuffered = true;
            this.MaximumSize = new System.Drawing.Size(666, 511);
            this.MinimumSize = new System.Drawing.Size(666, 511);
            this.Name = "EditUnitForm";
            this.Text = "新增場地管理單位";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewX1)).EndInit();
            this.groupPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.TextBoxX unitNameTbx;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.TextBoxX unitBossTbx;
        private DevComponents.DotNetBar.Controls.DataGridViewX dataGridViewX1;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.TextBoxX searchTbx;
        private DevComponents.DotNetBar.LabelX dateLb;
        private DevComponents.DotNetBar.ButtonX leaveBtn;
        private DevComponents.DotNetBar.ButtonX saveBtn;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private DevComponents.DotNetBar.Controls.DataGridViewButtonXColumn Column6;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.LabelX errorLb;
    }
}