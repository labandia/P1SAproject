namespace NCR_system.View.Module
{
    partial class Customer_Complaint_user
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Customer_Complaint_user));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.CustomDatagrid = new System.Windows.Forms.DataGridView();
            this.RecordID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateCreated = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SectionID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ModelNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LotNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NGQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Details = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RegNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CustomerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CCtype = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Edit = new System.Windows.Forms.DataGridViewImageColumn();
            this.Delete = new System.Windows.Forms.DataGridViewImageColumn();
            this.CustSummaryGrid = new System.Windows.Forms.DataGridView();
            this.Section = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalOpen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OpenCC = new System.Windows.Forms.Button();
            this.SelectedProcess = new System.Windows.Forms.ComboBox();
            this.Externalbtn = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ExternalPanel = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pieChart2 = new LiveCharts.WinForms.PieChart();
            this.label2 = new System.Windows.Forms.Label();
            this.SDCPanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.filteritems = new System.Windows.Forms.ComboBox();
            this.sectionfilter = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.CustomDatagrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CustSummaryGrid)).BeginInit();
            this.panel1.SuspendLayout();
            this.ExternalPanel.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SDCPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // CustomDatagrid
            // 
            this.CustomDatagrid.AllowUserToAddRows = false;
            this.CustomDatagrid.AllowUserToDeleteRows = false;
            this.CustomDatagrid.AllowUserToResizeColumns = false;
            this.CustomDatagrid.AllowUserToResizeRows = false;
            this.CustomDatagrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CustomDatagrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.CustomDatagrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.CustomDatagrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CustomDatagrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.RecordID,
            this.DateCreated,
            this.SectionID,
            this.ModelNo,
            this.LotNo,
            this.NGQty,
            this.Details,
            this.RegNo,
            this.CustomerName,
            this.Status,
            this.CCtype,
            this.Edit,
            this.Delete});
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.CustomDatagrid.DefaultCellStyle = dataGridViewCellStyle7;
            this.CustomDatagrid.Location = new System.Drawing.Point(16, 69);
            this.CustomDatagrid.Name = "CustomDatagrid";
            this.CustomDatagrid.ReadOnly = true;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.CustomDatagrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.CustomDatagrid.RowHeadersVisible = false;
            this.CustomDatagrid.RowTemplate.Height = 30;
            this.CustomDatagrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.CustomDatagrid.Size = new System.Drawing.Size(986, 624);
            this.CustomDatagrid.TabIndex = 4;
            this.CustomDatagrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.CustomDatagrid_CellClick);
            this.CustomDatagrid.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.CustomDatagrid_CellFormatting);
            // 
            // RecordID
            // 
            this.RecordID.DataPropertyName = "RecordID";
            this.RecordID.HeaderText = "RecordID";
            this.RecordID.Name = "RecordID";
            this.RecordID.ReadOnly = true;
            this.RecordID.Visible = false;
            this.RecordID.Width = 62;
            // 
            // DateCreated
            // 
            this.DateCreated.DataPropertyName = "DateCreated";
            this.DateCreated.HeaderText = "Date";
            this.DateCreated.Name = "DateCreated";
            this.DateCreated.ReadOnly = true;
            this.DateCreated.Width = 57;
            // 
            // SectionID
            // 
            this.SectionID.DataPropertyName = "SectionID";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SectionID.DefaultCellStyle = dataGridViewCellStyle2;
            this.SectionID.HeaderText = "Section in charge";
            this.SectionID.Name = "SectionID";
            this.SectionID.ReadOnly = true;
            this.SectionID.Width = 116;
            // 
            // ModelNo
            // 
            this.ModelNo.DataPropertyName = "ModelNo";
            this.ModelNo.HeaderText = "Model No/ Part no.";
            this.ModelNo.Name = "ModelNo";
            this.ModelNo.ReadOnly = true;
            this.ModelNo.Width = 107;
            // 
            // LotNo
            // 
            this.LotNo.DataPropertyName = "LotNo";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.LotNo.DefaultCellStyle = dataGridViewCellStyle3;
            this.LotNo.HeaderText = "Lot No.";
            this.LotNo.Name = "LotNo";
            this.LotNo.ReadOnly = true;
            this.LotNo.Width = 64;
            // 
            // NGQty
            // 
            this.NGQty.DataPropertyName = "NGQty";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.NGQty.DefaultCellStyle = dataGridViewCellStyle4;
            this.NGQty.HeaderText = "NG Qty";
            this.NGQty.Name = "NGQty";
            this.NGQty.ReadOnly = true;
            this.NGQty.Width = 50;
            // 
            // Details
            // 
            this.Details.DataPropertyName = "Details";
            this.Details.HeaderText = "Details of Problem";
            this.Details.Name = "Details";
            this.Details.ReadOnly = true;
            this.Details.Width = 117;
            // 
            // RegNo
            // 
            this.RegNo.DataPropertyName = "RegNo";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RegNo.DefaultCellStyle = dataGridViewCellStyle5;
            this.RegNo.HeaderText = "Registration No.";
            this.RegNo.Name = "RegNo";
            this.RegNo.ReadOnly = true;
            this.RegNo.Width = 107;
            // 
            // CustomerName
            // 
            this.CustomerName.DataPropertyName = "CustomerName";
            this.CustomerName.HeaderText = "Customer Name";
            this.CustomerName.Name = "CustomerName";
            this.CustomerName.ReadOnly = true;
            this.CustomerName.Width = 107;
            // 
            // Status
            // 
            this.Status.DataPropertyName = "Status";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Status.DefaultCellStyle = dataGridViewCellStyle6;
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            this.Status.Width = 63;
            // 
            // CCtype
            // 
            this.CCtype.DataPropertyName = "CCtype";
            this.CCtype.HeaderText = "CCtype";
            this.CCtype.Name = "CCtype";
            this.CCtype.ReadOnly = true;
            this.CCtype.Visible = false;
            this.CCtype.Width = 71;
            // 
            // Edit
            // 
            this.Edit.DataPropertyName = "Edit";
            this.Edit.HeaderText = "Edit";
            this.Edit.Image = ((System.Drawing.Image)(resources.GetObject("Edit.Image")));
            this.Edit.Name = "Edit";
            this.Edit.ReadOnly = true;
            this.Edit.Width = 32;
            // 
            // Delete
            // 
            this.Delete.DataPropertyName = "Delete";
            this.Delete.HeaderText = "Delete";
            this.Delete.Image = ((System.Drawing.Image)(resources.GetObject("Delete.Image")));
            this.Delete.Name = "Delete";
            this.Delete.ReadOnly = true;
            this.Delete.Width = 48;
            // 
            // CustSummaryGrid
            // 
            this.CustSummaryGrid.AllowUserToAddRows = false;
            this.CustSummaryGrid.AllowUserToDeleteRows = false;
            this.CustSummaryGrid.AllowUserToResizeColumns = false;
            this.CustSummaryGrid.AllowUserToResizeRows = false;
            this.CustSummaryGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CustSummaryGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.Padding = new System.Windows.Forms.Padding(5);
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.CustSummaryGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.CustSummaryGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CustSummaryGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Section,
            this.TotalOpen});
            this.CustSummaryGrid.EnableHeadersVisualStyles = false;
            this.CustSummaryGrid.Location = new System.Drawing.Point(24, 22);
            this.CustSummaryGrid.Name = "CustSummaryGrid";
            this.CustSummaryGrid.ReadOnly = true;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.CustSummaryGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.CustSummaryGrid.RowHeadersVisible = false;
            this.CustSummaryGrid.Size = new System.Drawing.Size(278, 148);
            this.CustSummaryGrid.TabIndex = 5;
            // 
            // Section
            // 
            this.Section.DataPropertyName = "Section";
            this.Section.HeaderText = "Section";
            this.Section.Name = "Section";
            this.Section.ReadOnly = true;
            // 
            // TotalOpen
            // 
            this.TotalOpen.DataPropertyName = "TotalOpen";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.TotalOpen.DefaultCellStyle = dataGridViewCellStyle10;
            this.TotalOpen.HeaderText = "Total Open items";
            this.TotalOpen.Name = "TotalOpen";
            this.TotalOpen.ReadOnly = true;
            // 
            // OpenCC
            // 
            this.OpenCC.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OpenCC.Location = new System.Drawing.Point(17, 22);
            this.OpenCC.Name = "OpenCC";
            this.OpenCC.Size = new System.Drawing.Size(136, 41);
            this.OpenCC.TabIndex = 6;
            this.OpenCC.Text = "Add SDC";
            this.OpenCC.UseVisualStyleBackColor = true;
            this.OpenCC.Click += new System.EventHandler(this.OpenCC_Click);
            // 
            // SelectedProcess
            // 
            this.SelectedProcess.DisplayMember = "External";
            this.SelectedProcess.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.SelectedProcess.FormattingEnabled = true;
            this.SelectedProcess.ItemHeight = 13;
            this.SelectedProcess.Items.AddRange(new object[] {
            "External",
            "SDC"});
            this.SelectedProcess.Location = new System.Drawing.Point(184, 33);
            this.SelectedProcess.Name = "SelectedProcess";
            this.SelectedProcess.Size = new System.Drawing.Size(159, 21);
            this.SelectedProcess.TabIndex = 7;
            this.SelectedProcess.ValueMember = "External";
            this.SelectedProcess.SelectedIndexChanged += new System.EventHandler(this.SelectedProcess_SelectedIndexChanged);
            // 
            // Externalbtn
            // 
            this.Externalbtn.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Externalbtn.Location = new System.Drawing.Point(16, 22);
            this.Externalbtn.Name = "Externalbtn";
            this.Externalbtn.Size = new System.Drawing.Size(137, 41);
            this.Externalbtn.TabIndex = 8;
            this.Externalbtn.Text = "Add External";
            this.Externalbtn.UseVisualStyleBackColor = true;
            this.Externalbtn.Click += new System.EventHandler(this.Externalbtn_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.ExternalPanel);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.SDCPanel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(1019, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(330, 766);
            this.panel1.TabIndex = 9;
            // 
            // ExternalPanel
            // 
            this.ExternalPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ExternalPanel.Controls.Add(this.label3);
            this.ExternalPanel.Location = new System.Drawing.Point(3, 407);
            this.ExternalPanel.Name = "ExternalPanel";
            this.ExternalPanel.Size = new System.Drawing.Size(324, 319);
            this.ExternalPanel.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(18, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(123, 18);
            this.label3.TabIndex = 0;
            this.label3.Text = "External Details ";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.CustSummaryGrid);
            this.panel3.Controls.Add(this.pieChart2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(330, 200);
            this.panel3.TabIndex = 7;
            // 
            // pieChart2
            // 
            this.pieChart2.Location = new System.Drawing.Point(35, 0);
            this.pieChart2.Name = "pieChart2";
            this.pieChart2.Size = new System.Drawing.Size(249, 196);
            this.pieChart2.TabIndex = 4;
            this.pieChart2.Text = "pieChart2";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 540);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "label2";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // SDCPanel
            // 
            this.SDCPanel.Controls.Add(this.label1);
            this.SDCPanel.Location = new System.Drawing.Point(3, 206);
            this.SDCPanel.Name = "SDCPanel";
            this.SDCPanel.Size = new System.Drawing.Size(324, 195);
            this.SDCPanel.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(18, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "SDC Details ";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 699);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1019, 67);
            this.panel2.TabIndex = 10;
            // 
            // filteritems
            // 
            this.filteritems.FormattingEnabled = true;
            this.filteritems.Items.AddRange(new object[] {
            "Open",
            "Close"});
            this.filteritems.Location = new System.Drawing.Point(360, 33);
            this.filteritems.Name = "filteritems";
            this.filteritems.Size = new System.Drawing.Size(121, 21);
            this.filteritems.TabIndex = 11;
            // 
            // sectionfilter
            // 
            this.sectionfilter.FormattingEnabled = true;
            this.sectionfilter.Items.AddRange(new object[] {
            "Molding",
            "Press",
            "Rotor",
            "Winding",
            "Circuit"});
            this.sectionfilter.Location = new System.Drawing.Point(517, 33);
            this.sectionfilter.Name = "sectionfilter";
            this.sectionfilter.Size = new System.Drawing.Size(121, 21);
            this.sectionfilter.TabIndex = 12;
            // 
            // Customer_Complaint_user
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.sectionfilter);
            this.Controls.Add(this.filteritems);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.Externalbtn);
            this.Controls.Add(this.SelectedProcess);
            this.Controls.Add(this.OpenCC);
            this.Controls.Add(this.CustomDatagrid);
            this.Controls.Add(this.panel1);
            this.Name = "Customer_Complaint_user";
            this.Size = new System.Drawing.Size(1349, 766);
            this.Load += new System.EventHandler(this.Customer_Complaint_user_Load);
            ((System.ComponentModel.ISupportInitialize)(this.CustomDatagrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CustSummaryGrid)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ExternalPanel.ResumeLayout(false);
            this.ExternalPanel.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.SDCPanel.ResumeLayout(false);
            this.SDCPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.DataGridView CustomDatagrid;
        public System.Windows.Forms.DataGridView CustSummaryGrid;
        private System.Windows.Forms.Button OpenCC;
        private System.Windows.Forms.ComboBox SelectedProcess;
        private System.Windows.Forms.Button Externalbtn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Section;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalOpen;
        private System.Windows.Forms.DataGridViewTextBoxColumn RecordID;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateCreated;
        private System.Windows.Forms.DataGridViewTextBoxColumn SectionID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ModelNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn LotNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn NGQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn Details;
        private System.Windows.Forms.DataGridViewTextBoxColumn RegNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn CustomerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn CCtype;
        private System.Windows.Forms.DataGridViewImageColumn Edit;
        private System.Windows.Forms.DataGridViewImageColumn Delete;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private LiveCharts.WinForms.PieChart pieChart2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox filteritems;
        private System.Windows.Forms.Panel SDCPanel;
        private System.Windows.Forms.Panel ExternalPanel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ComboBox sectionfilter;
    }
}
