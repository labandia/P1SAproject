namespace NCR_system.View.Module
{
    partial class Rejected
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Rejected));
            this.projectitle = new System.Windows.Forms.Label();
            this.OpenReject = new System.Windows.Forms.Button();
            this.RejectedGrid = new System.Windows.Forms.DataGridView();
            this.RecordID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RegNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateIssued = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IssueGroup = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SectionID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ModelNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Contents = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateCloseReg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Process = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Edit = new System.Windows.Forms.DataGridViewImageColumn();
            this.Delete = new System.Windows.Forms.DataGridViewImageColumn();
            this.sectionfilter = new System.Windows.Forms.ComboBox();
            this.filteritems = new System.Windows.Forms.ComboBox();
            this.cartesianChart1 = new LiveCharts.WinForms.CartesianChart();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.RejectedGrid)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // projectitle
            // 
            this.projectitle.AutoSize = true;
            this.projectitle.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold);
            this.projectitle.ForeColor = System.Drawing.Color.Black;
            this.projectitle.Location = new System.Drawing.Point(30, 37);
            this.projectitle.Name = "projectitle";
            this.projectitle.Size = new System.Drawing.Size(134, 30);
            this.projectitle.TabIndex = 5;
            this.projectitle.Text = "Rejected Lot";
            // 
            // OpenReject
            // 
            this.OpenReject.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.OpenReject.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(34)))), ((int)(((byte)(200)))));
            this.OpenReject.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(34)))), ((int)(((byte)(200)))));
            this.OpenReject.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(34)))), ((int)(((byte)(200)))));
            this.OpenReject.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(15)))), ((int)(((byte)(168)))));
            this.OpenReject.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OpenReject.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.OpenReject.ForeColor = System.Drawing.Color.White;
            this.OpenReject.Location = new System.Drawing.Point(1168, 37);
            this.OpenReject.Name = "OpenReject";
            this.OpenReject.Size = new System.Drawing.Size(136, 41);
            this.OpenReject.TabIndex = 11;
            this.OpenReject.Text = "OpenReject";
            this.OpenReject.UseVisualStyleBackColor = false;
            this.OpenReject.Click += new System.EventHandler(this.OpenReject_Click);
            // 
            // RejectedGrid
            // 
            this.RejectedGrid.AllowUserToAddRows = false;
            this.RejectedGrid.AllowUserToDeleteRows = false;
            this.RejectedGrid.AllowUserToResizeColumns = false;
            this.RejectedGrid.AllowUserToResizeRows = false;
            this.RejectedGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RejectedGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.RejectedGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.RejectedGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.RejectedGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.RecordID,
            this.RegNo,
            this.DateIssued,
            this.IssueGroup,
            this.SectionID,
            this.ModelNo,
            this.Quantity,
            this.Contents,
            this.DateCloseReg,
            this.Status,
            this.Process,
            this.Edit,
            this.Delete});
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle9.Padding = new System.Windows.Forms.Padding(6, 5, 6, 5);
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.RejectedGrid.DefaultCellStyle = dataGridViewCellStyle9;
            this.RejectedGrid.Location = new System.Drawing.Point(35, 352);
            this.RejectedGrid.Name = "RejectedGrid";
            this.RejectedGrid.ReadOnly = true;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.RejectedGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.RejectedGrid.RowHeadersVisible = false;
            this.RejectedGrid.RowTemplate.Height = 30;
            this.RejectedGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.RejectedGrid.Size = new System.Drawing.Size(1279, 414);
            this.RejectedGrid.TabIndex = 12;
            this.RejectedGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.RejectedGrid_CellClick);
            this.RejectedGrid.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.RejectedGrid_CellFormatting_1);
            // 
            // RecordID
            // 
            this.RecordID.DataPropertyName = "RecordID";
            this.RecordID.HeaderText = "RecordID";
            this.RecordID.Name = "RecordID";
            this.RecordID.ReadOnly = true;
            this.RecordID.Visible = false;
            this.RecordID.Width = 72;
            // 
            // RegNo
            // 
            this.RegNo.DataPropertyName = "RegNo";
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RegNo.DefaultCellStyle = dataGridViewCellStyle2;
            this.RegNo.HeaderText = "Registration No.";
            this.RegNo.Name = "RegNo";
            this.RegNo.ReadOnly = true;
            this.RegNo.Width = 119;
            // 
            // DateIssued
            // 
            this.DateIssued.DataPropertyName = "DateIssued";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.DateIssued.DefaultCellStyle = dataGridViewCellStyle3;
            this.DateIssued.HeaderText = "Date Issued";
            this.DateIssued.Name = "DateIssued";
            this.DateIssued.ReadOnly = true;
            this.DateIssued.Width = 97;
            // 
            // IssueGroup
            // 
            this.IssueGroup.DataPropertyName = "IssueGroup";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.IssueGroup.DefaultCellStyle = dataGridViewCellStyle4;
            this.IssueGroup.HeaderText = "Issuing\nSection ";
            this.IssueGroup.Name = "IssueGroup";
            this.IssueGroup.ReadOnly = true;
            this.IssueGroup.Width = 85;
            // 
            // SectionID
            // 
            this.SectionID.DataPropertyName = "SectionID";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.SectionID.DefaultCellStyle = dataGridViewCellStyle5;
            this.SectionID.HeaderText = "Main In-charge";
            this.SectionID.Name = "SectionID";
            this.SectionID.ReadOnly = true;
            this.SectionID.Width = 114;
            // 
            // ModelNo
            // 
            this.ModelNo.DataPropertyName = "ModelNo";
            this.ModelNo.HeaderText = "Model No. / Part No.";
            this.ModelNo.Name = "ModelNo";
            this.ModelNo.ReadOnly = true;
            this.ModelNo.Width = 103;
            // 
            // Quantity
            // 
            this.Quantity.DataPropertyName = "Quantity";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Quantity.DefaultCellStyle = dataGridViewCellStyle6;
            this.Quantity.HeaderText = "Quantity";
            this.Quantity.Name = "Quantity";
            this.Quantity.ReadOnly = true;
            this.Quantity.Width = 89;
            // 
            // Contents
            // 
            this.Contents.DataPropertyName = "Contents";
            this.Contents.HeaderText = "Contents";
            this.Contents.Name = "Contents";
            this.Contents.ReadOnly = true;
            this.Contents.Width = 91;
            // 
            // DateCloseReg
            // 
            this.DateCloseReg.DataPropertyName = "DateCloseReg";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.DateCloseReg.DefaultCellStyle = dataGridViewCellStyle7;
            this.DateCloseReg.HeaderText = "Date Registered";
            this.DateCloseReg.Name = "DateCloseReg";
            this.DateCloseReg.ReadOnly = true;
            this.DateCloseReg.Width = 117;
            // 
            // Status
            // 
            this.Status.DataPropertyName = "Status";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Status.DefaultCellStyle = dataGridViewCellStyle8;
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            this.Status.Width = 76;
            // 
            // Process
            // 
            this.Process.DataPropertyName = "Process";
            this.Process.HeaderText = "Process";
            this.Process.Name = "Process";
            this.Process.ReadOnly = true;
            this.Process.Visible = false;
            this.Process.Width = 83;
            // 
            // Edit
            // 
            this.Edit.HeaderText = "Edit";
            this.Edit.Image = ((System.Drawing.Image)(resources.GetObject("Edit.Image")));
            this.Edit.Name = "Edit";
            this.Edit.ReadOnly = true;
            this.Edit.Width = 45;
            // 
            // Delete
            // 
            this.Delete.HeaderText = "Delete";
            this.Delete.Image = ((System.Drawing.Image)(resources.GetObject("Delete.Image")));
            this.Delete.Name = "Delete";
            this.Delete.ReadOnly = true;
            this.Delete.Width = 58;
            // 
            // sectionfilter
            // 
            this.sectionfilter.DropDownHeight = 200;
            this.sectionfilter.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sectionfilter.FormattingEnabled = true;
            this.sectionfilter.IntegralHeight = false;
            this.sectionfilter.ItemHeight = 15;
            this.sectionfilter.Items.AddRange(new object[] {
            "-- Filter Section --",
            "Molding",
            "Press",
            "Rotor",
            "Winding",
            "Circuit"});
            this.sectionfilter.Location = new System.Drawing.Point(207, 44);
            this.sectionfilter.MaxDropDownItems = 30;
            this.sectionfilter.Name = "sectionfilter";
            this.sectionfilter.Size = new System.Drawing.Size(121, 23);
            this.sectionfilter.TabIndex = 20;
            // 
            // filteritems
            // 
            this.filteritems.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.filteritems.FormattingEnabled = true;
            this.filteritems.Items.AddRange(new object[] {
            "-- Filter Status --",
            "Open",
            "Close"});
            this.filteritems.Location = new System.Drawing.Point(355, 44);
            this.filteritems.Name = "filteritems";
            this.filteritems.Size = new System.Drawing.Size(121, 23);
            this.filteritems.TabIndex = 21;
            // 
            // cartesianChart1
            // 
            this.cartesianChart1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cartesianChart1.Location = new System.Drawing.Point(30, 30);
            this.cartesianChart1.Name = "cartesianChart1";
            this.cartesianChart1.Size = new System.Drawing.Size(1207, 179);
            this.cartesianChart1.TabIndex = 23;
            this.cartesianChart1.Text = "cartesianChart1";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.cartesianChart1);
            this.panel1.Location = new System.Drawing.Point(35, 92);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(30);
            this.panel1.Size = new System.Drawing.Size(1269, 241);
            this.panel1.TabIndex = 24;
            // 
            // Rejected
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.filteritems);
            this.Controls.Add(this.sectionfilter);
            this.Controls.Add(this.RejectedGrid);
            this.Controls.Add(this.OpenReject);
            this.Controls.Add(this.projectitle);
            this.Name = "Rejected";
            this.Size = new System.Drawing.Size(1349, 806);
            this.Load += new System.EventHandler(this.Rejected_Load);
            ((System.ComponentModel.ISupportInitialize)(this.RejectedGrid)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label projectitle;
        private System.Windows.Forms.Button OpenReject;
        private System.Windows.Forms.DataGridView RejectedGrid;
        private System.Windows.Forms.ComboBox sectionfilter;
        private System.Windows.Forms.ComboBox filteritems;
        private System.Windows.Forms.DataGridViewTextBoxColumn RecordID;
        private System.Windows.Forms.DataGridViewTextBoxColumn RegNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateIssued;
        private System.Windows.Forms.DataGridViewTextBoxColumn IssueGroup;
        private System.Windows.Forms.DataGridViewTextBoxColumn SectionID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ModelNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn Contents;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateCloseReg;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn Process;
        private System.Windows.Forms.DataGridViewImageColumn Edit;
        private System.Windows.Forms.DataGridViewImageColumn Delete;
        private LiveCharts.WinForms.CartesianChart cartesianChart1;
        private System.Windows.Forms.Panel panel1;
    }
}
