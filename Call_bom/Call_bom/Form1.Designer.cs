namespace Call_bom
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lstCallBom = new System.Windows.Forms.ListBox();
            this.cmsDelete = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.rightMouseDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.lblStation = new System.Windows.Forms.Label();
            this.lstCallCar = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgvShowBom = new System.Windows.Forms.DataGridView();
            this.lblLinkStatu = new System.Windows.Forms.Label();
            this.lblWorkStatu = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.btnSend = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.tmrCurrentTime = new System.Windows.Forms.Timer(this.components);
            this.panel4 = new System.Windows.Forms.Panel();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CallTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvStation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvWorkStation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvBomCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvBomDescribe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvStoreLocation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvPicking = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvPickingTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.cmsDelete.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvShowBom)).BeginInit();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(131)))), ((int)(((byte)(181)))));
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Location = new System.Drawing.Point(-2, -1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1386, 712);
            this.panel1.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lstCallBom);
            this.panel3.Controls.Add(this.lblStation);
            this.panel3.Controls.Add(this.lstCallCar);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Location = new System.Drawing.Point(3, 85);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(244, 616);
            this.panel3.TabIndex = 6;
            // 
            // lstCallBom
            // 
            this.lstCallBom.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lstCallBom.ContextMenuStrip = this.cmsDelete;
            this.lstCallBom.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lstCallBom.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lstCallBom.FormattingEnabled = true;
            this.lstCallBom.ItemHeight = 20;
            this.lstCallBom.Location = new System.Drawing.Point(8, 38);
            this.lstCallBom.Name = "lstCallBom";
            this.lstCallBom.Size = new System.Drawing.Size(228, 264);
            this.lstCallBom.TabIndex = 3;
            this.lstCallBom.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lstCallBom_DrawItem);
            // 
            // cmsDelete
            // 
            this.cmsDelete.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rightMouseDelete});
            this.cmsDelete.Name = "cmsDelete";
            this.cmsDelete.Size = new System.Drawing.Size(101, 26);
            // 
            // rightMouseDelete
            // 
            this.rightMouseDelete.Name = "rightMouseDelete";
            this.rightMouseDelete.Size = new System.Drawing.Size(100, 22);
            this.rightMouseDelete.Text = "删除";
            this.rightMouseDelete.Click += new System.EventHandler(this.rightMouseDelete_Click_1);
            // 
            // lblStation
            // 
            this.lblStation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(207)))), ((int)(((byte)(235)))));
            this.lblStation.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblStation.Location = new System.Drawing.Point(83, 3);
            this.lblStation.Name = "lblStation";
            this.lblStation.Size = new System.Drawing.Size(100, 30);
            this.lblStation.TabIndex = 2;
            this.lblStation.Text = "站点叫料";
            // 
            // lstCallCar
            // 
            this.lstCallCar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lstCallCar.ContextMenuStrip = this.cmsDelete;
            this.lstCallCar.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lstCallCar.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lstCallCar.FormattingEnabled = true;
            this.lstCallCar.ItemHeight = 20;
            this.lstCallCar.Location = new System.Drawing.Point(8, 340);
            this.lstCallCar.Name = "lstCallCar";
            this.lstCallCar.Size = new System.Drawing.Size(228, 264);
            this.lstCallCar.TabIndex = 3;
            this.lstCallCar.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lstCallCar_DrawItem);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(207)))), ((int)(((byte)(235)))));
            this.label1.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(73, 306);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 30);
            this.label1.TabIndex = 2;
            this.label1.Text = "站点叫车";
            // 
            // panel2
            // 
            this.panel2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel2.Controls.Add(this.dgvShowBom);
            this.panel2.Location = new System.Drawing.Point(240, 84);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1146, 617);
            this.panel2.TabIndex = 5;
            // 
            // dgvShowBom
            // 
            this.dgvShowBom.AllowUserToAddRows = false;
            this.dgvShowBom.AllowUserToDeleteRows = false;
            this.dgvShowBom.AllowUserToResizeColumns = false;
            this.dgvShowBom.AllowUserToResizeRows = false;
            this.dgvShowBom.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dgvShowBom.BackgroundColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(207)))), ((int)(((byte)(235)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Yellow;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvShowBom.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvShowBom.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvShowBom.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.CallTime,
            this.dgvStation,
            this.dgvWorkStation,
            this.dgvBomCode,
            this.dgvBomDescribe,
            this.dgvStoreLocation,
            this.dgvNumber,
            this.dgvPicking,
            this.dgvPickingTime});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvShowBom.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvShowBom.Location = new System.Drawing.Point(8, 2);
            this.dgvShowBom.Name = "dgvShowBom";
            this.dgvShowBom.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(207)))), ((int)(((byte)(235)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(207)))), ((int)(((byte)(235)))));
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvShowBom.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvShowBom.RowHeadersVisible = false;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            this.dgvShowBom.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvShowBom.RowTemplate.Height = 23;
            this.dgvShowBom.Size = new System.Drawing.Size(1130, 603);
            this.dgvShowBom.TabIndex = 4;
            this.dgvShowBom.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvShowBom_CellClick);
            this.dgvShowBom.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvShowBom_CellContentClick);
            // 
            // lblLinkStatu
            // 
            this.lblLinkStatu.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblLinkStatu.Location = new System.Drawing.Point(986, 11);
            this.lblLinkStatu.Name = "lblLinkStatu";
            this.lblLinkStatu.Size = new System.Drawing.Size(185, 30);
            this.lblLinkStatu.TabIndex = 2;
            this.lblLinkStatu.Text = "连接状态：未连接";
            // 
            // lblWorkStatu
            // 
            this.lblWorkStatu.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblWorkStatu.Location = new System.Drawing.Point(683, 11);
            this.lblWorkStatu.Name = "lblWorkStatu";
            this.lblWorkStatu.Size = new System.Drawing.Size(185, 30);
            this.lblWorkStatu.TabIndex = 2;
            this.lblWorkStatu.Text = "工作状态：";
            // 
            // lblTime
            // 
            this.lblTime.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTime.Location = new System.Drawing.Point(14, 1);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(171, 60);
            this.lblTime.TabIndex = 2;
            // 
            // btnSend
            // 
            this.btnSend.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(131)))), ((int)(((byte)(181)))));
            this.btnSend.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSend.Location = new System.Drawing.Point(468, 3);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(100, 45);
            this.btnSend.TabIndex = 1;
            this.btnSend.Text = "配送";
            this.btnSend.UseVisualStyleBackColor = false;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // btnExport
            // 
            this.btnExport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(131)))), ((int)(((byte)(181)))));
            this.btnExport.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnExport.Location = new System.Drawing.Point(267, 3);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(100, 45);
            this.btnExport.TabIndex = 0;
            this.btnExport.Text = "导出";
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // tmrCurrentTime
            // 
            this.tmrCurrentTime.Enabled = true;
            this.tmrCurrentTime.Interval = 1000;
            this.tmrCurrentTime.Tick += new System.EventHandler(this.tmrCurrentTime_Tick);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(131)))), ((int)(((byte)(181)))));
            this.panel4.Controls.Add(this.btnSend);
            this.panel4.Controls.Add(this.btnExport);
            this.panel4.Controls.Add(this.lblWorkStatu);
            this.panel4.Controls.Add(this.lblTime);
            this.panel4.Controls.Add(this.lblLinkStatu);
            this.panel4.Location = new System.Drawing.Point(22, 12);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1229, 71);
            this.panel4.TabIndex = 7;
            // 
            // ID
            // 
            this.ID.HeaderText = "编号";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Visible = false;
            // 
            // CallTime
            // 
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(205)))), ((int)(((byte)(235)))));
            this.CallTime.DefaultCellStyle = dataGridViewCellStyle2;
            this.CallTime.HeaderText = "叫料时间";
            this.CallTime.Name = "CallTime";
            this.CallTime.ReadOnly = true;
            this.CallTime.Width = 120;
            // 
            // dgvStation
            // 
            this.dgvStation.HeaderText = "站点";
            this.dgvStation.Name = "dgvStation";
            this.dgvStation.ReadOnly = true;
            this.dgvStation.Width = 80;
            // 
            // dgvWorkStation
            // 
            this.dgvWorkStation.HeaderText = "工位";
            this.dgvWorkStation.Name = "dgvWorkStation";
            this.dgvWorkStation.ReadOnly = true;
            this.dgvWorkStation.Width = 120;
            // 
            // dgvBomCode
            // 
            this.dgvBomCode.HeaderText = "料号编码";
            this.dgvBomCode.Name = "dgvBomCode";
            this.dgvBomCode.ReadOnly = true;
            this.dgvBomCode.Width = 135;
            // 
            // dgvBomDescribe
            // 
            this.dgvBomDescribe.HeaderText = "物料描述";
            this.dgvBomDescribe.Name = "dgvBomDescribe";
            this.dgvBomDescribe.ReadOnly = true;
            this.dgvBomDescribe.Width = 250;
            // 
            // dgvStoreLocation
            // 
            this.dgvStoreLocation.HeaderText = "存放地点";
            this.dgvStoreLocation.Name = "dgvStoreLocation";
            this.dgvStoreLocation.ReadOnly = true;
            this.dgvStoreLocation.Width = 120;
            // 
            // dgvNumber
            // 
            this.dgvNumber.HeaderText = "数量";
            this.dgvNumber.Name = "dgvNumber";
            this.dgvNumber.ReadOnly = true;
            // 
            // dgvPicking
            // 
            this.dgvPicking.FalseValue = "false";
            this.dgvPicking.HeaderText = "拣货";
            this.dgvPicking.Name = "dgvPicking";
            this.dgvPicking.ReadOnly = true;
            this.dgvPicking.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPicking.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dgvPicking.TrueValue = "true";
            this.dgvPicking.Width = 80;
            // 
            // dgvPickingTime
            // 
            this.dgvPickingTime.HeaderText = "拣货时间";
            this.dgvPickingTime.Name = "dgvPickingTime";
            this.dgvPickingTime.ReadOnly = true;
            this.dgvPickingTime.Width = 120;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1384, 712);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "深圳市米克力美科技有限公司      www.i-so.cn";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.cmsDelete.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvShowBom)).EndInit();
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvShowBom;
        private System.Windows.Forms.ListBox lstCallCar;
        private System.Windows.Forms.ListBox lstCallBom;
        private System.Windows.Forms.Label lblLinkStatu;
        private System.Windows.Forms.Label lblWorkStatu;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblStation;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Timer tmrCurrentTime;
        private System.Windows.Forms.ContextMenuStrip cmsDelete;
        private System.Windows.Forms.ToolStripMenuItem rightMouseDelete;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn CallTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvStation;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvWorkStation;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvBomCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvBomDescribe;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvStoreLocation;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvNumber;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvPicking;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvPickingTime;
    }
}

