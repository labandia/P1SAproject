namespace NCR_system.View.Module
{
    partial class ShipRejected
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShipRejected));
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
            this.UploadImage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Edit = new System.Windows.Forms.DataGridViewImageColumn();
            this.Delete = new System.Windows.Forms.DataGridViewImageColumn();
            this.OpenReject = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.sectionfilter = new System.Windows.Forms.ComboBox();
            this.filteritems = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.cartesianChart1 = new LiveCharts.WinForms.CartesianChart();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.panel9 = new System.Windows.Forms.Panel();
            this.pieChart1 = new LiveCharts.WinForms.PieChart();
            this.panel10 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.RejectedGrid)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel10.SuspendLayout();
            this.SuspendLayout();
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
            this.RejectedGrid.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.RejectedGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle11;
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
            this.UploadImage,
            this.Edit,
            this.Delete});
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle19.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle19.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            dataGridViewCellStyle19.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle19.Padding = new System.Windows.Forms.Padding(6, 5, 6, 5);
            dataGridViewCellStyle19.SelectionBackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle19.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle19.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.RejectedGrid.DefaultCellStyle = dataGridViewCellStyle19;
            this.RejectedGrid.Location = new System.Drawing.Point(36, 361);
            this.RejectedGrid.Name = "RejectedGrid";
            this.RejectedGrid.ReadOnly = true;
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle20.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle20.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle20.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle20.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle20.SelectionForeColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle20.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.RejectedGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle20;
            this.RejectedGrid.RowHeadersVisible = false;
            this.RejectedGrid.RowTemplate.Height = 30;
            this.RejectedGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.RejectedGrid.Size = new System.Drawing.Size(1275, 395);
            this.RejectedGrid.TabIndex = 8;
            this.RejectedGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.RejectedGrid_CellClick);
            this.RejectedGrid.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.RejectedGrid_CellFormatting);
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
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RegNo.DefaultCellStyle = dataGridViewCellStyle12;
            this.RegNo.HeaderText = "Registration No.";
            this.RegNo.Name = "RegNo";
            this.RegNo.ReadOnly = true;
            this.RegNo.Width = 119;
            // 
            // DateIssued
            // 
            this.DateIssued.DataPropertyName = "DateIssued";
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.DateIssued.DefaultCellStyle = dataGridViewCellStyle13;
            this.DateIssued.HeaderText = "Date Issued";
            this.DateIssued.Name = "DateIssued";
            this.DateIssued.ReadOnly = true;
            this.DateIssued.Width = 97;
            // 
            // IssueGroup
            // 
            this.IssueGroup.DataPropertyName = "IssueGroup";
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.IssueGroup.DefaultCellStyle = dataGridViewCellStyle14;
            this.IssueGroup.HeaderText = "Issuing \nSection ";
            this.IssueGroup.Name = "IssueGroup";
            this.IssueGroup.ReadOnly = true;
            this.IssueGroup.Width = 85;
            // 
            // SectionID
            // 
            this.SectionID.DataPropertyName = "SectionID";
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.SectionID.DefaultCellStyle = dataGridViewCellStyle15;
            this.SectionID.HeaderText = "Main In-charge";
            this.SectionID.Name = "SectionID";
            this.SectionID.ReadOnly = true;
            this.SectionID.Visible = false;
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
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Quantity.DefaultCellStyle = dataGridViewCellStyle16;
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
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.DateCloseReg.DefaultCellStyle = dataGridViewCellStyle17;
            this.DateCloseReg.HeaderText = "Date Registered";
            this.DateCloseReg.Name = "DateCloseReg";
            this.DateCloseReg.ReadOnly = true;
            this.DateCloseReg.Width = 117;
            // 
            // Status
            // 
            this.Status.DataPropertyName = "Status";
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Status.DefaultCellStyle = dataGridViewCellStyle18;
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
            // UploadImage
            // 
            this.UploadImage.DataPropertyName = "UploadImage";
            this.UploadImage.HeaderText = "UploadImage";
            this.UploadImage.Name = "UploadImage";
            this.UploadImage.ReadOnly = true;
            this.UploadImage.Visible = false;
            this.UploadImage.Width = 114;
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
            // OpenReject
            // 
            this.OpenReject.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.OpenReject.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.OpenReject.Cursor = System.Windows.Forms.Cursors.Hand;
            this.OpenReject.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight;
            this.OpenReject.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.MenuHighlight;
            this.OpenReject.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.MenuHighlight;
            this.OpenReject.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OpenReject.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.OpenReject.ForeColor = System.Drawing.Color.White;
            this.OpenReject.Location = new System.Drawing.Point(1175, 299);
            this.OpenReject.Name = "OpenReject";
            this.OpenReject.Size = new System.Drawing.Size(136, 32);
            this.OpenReject.TabIndex = 21;
            this.OpenReject.Text = "Add Shipment Data";
            this.OpenReject.UseVisualStyleBackColor = false;
            this.OpenReject.Click += new System.EventHandler(this.OpenReject_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label9.Location = new System.Drawing.Point(208, 285);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(119, 15);
            this.label9.TabIndex = 31;
            this.label9.Text = "Filter by Open/Close";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label5.Location = new System.Drawing.Point(37, 285);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(97, 15);
            this.label5.TabIndex = 29;
            this.label5.Text = "Filter by Section";
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
            this.sectionfilter.Location = new System.Drawing.Point(40, 308);
            this.sectionfilter.MaxDropDownItems = 30;
            this.sectionfilter.Name = "sectionfilter";
            this.sectionfilter.Size = new System.Drawing.Size(153, 23);
            this.sectionfilter.TabIndex = 28;
            this.sectionfilter.SelectedIndexChanged += new System.EventHandler(this.sectionfilter_SelectedIndexChanged);
            // 
            // filteritems
            // 
            this.filteritems.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.filteritems.FormattingEnabled = true;
            this.filteritems.Items.AddRange(new object[] {
            "-- Filter Status --",
            "Open",
            "Close"});
            this.filteritems.Location = new System.Drawing.Point(211, 308);
            this.filteritems.Name = "filteritems";
            this.filteritems.Size = new System.Drawing.Size(182, 23);
            this.filteritems.TabIndex = 27;
            this.filteritems.SelectedIndexChanged += new System.EventHandler(this.filteritems_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(42)))), ((int)(((byte)(71)))));
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel8);
            this.panel1.Controls.Add(this.OpenReject);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.filteritems);
            this.panel1.Controls.Add(this.sectionfilter);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1352, 355);
            this.panel1.TabIndex = 32;
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(36)))), ((int)(((byte)(62)))));
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Controls.Add(this.panel7);
            this.panel4.Location = new System.Drawing.Point(555, 25);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(756, 246);
            this.panel4.TabIndex = 36;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.cartesianChart1);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 50);
            this.panel5.Name = "panel5";
            this.panel5.Padding = new System.Windows.Forms.Padding(30, 20, 30, 20);
            this.panel5.Size = new System.Drawing.Size(756, 196);
            this.panel5.TabIndex = 1;
            // 
            // cartesianChart1
            // 
            this.cartesianChart1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cartesianChart1.Location = new System.Drawing.Point(30, 20);
            this.cartesianChart1.Margin = new System.Windows.Forms.Padding(3, 30, 3, 3);
            this.cartesianChart1.Name = "cartesianChart1";
            this.cartesianChart1.Size = new System.Drawing.Size(696, 156);
            this.cartesianChart1.TabIndex = 2;
            this.cartesianChart1.Text = "QuantityChart";
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(76)))));
            this.panel7.Controls.Add(this.label7);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(756, 50);
            this.panel7.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(20, 14);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(200, 21);
            this.label7.TabIndex = 14;
            this.label7.Text = "Numbers Cases By Status";
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(36)))), ((int)(((byte)(62)))));
            this.panel8.Controls.Add(this.panel9);
            this.panel8.Controls.Add(this.panel10);
            this.panel8.Location = new System.Drawing.Point(40, 25);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(466, 246);
            this.panel8.TabIndex = 35;
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.pieChart1);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel9.Location = new System.Drawing.Point(0, 50);
            this.panel9.Name = "panel9";
            this.panel9.Padding = new System.Windows.Forms.Padding(30, 20, 30, 20);
            this.panel9.Size = new System.Drawing.Size(466, 196);
            this.panel9.TabIndex = 1;
            // 
            // pieChart1
            // 
            this.pieChart1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pieChart1.ForeColor = System.Drawing.Color.White;
            this.pieChart1.Location = new System.Drawing.Point(30, 20);
            this.pieChart1.Name = "pieChart1";
            this.pieChart1.Size = new System.Drawing.Size(406, 156);
            this.pieChart1.TabIndex = 1;
            this.pieChart1.Text = "pieChart1";
            // 
            // panel10
            // 
            this.panel10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(76)))));
            this.panel10.Controls.Add(this.label6);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel10.Location = new System.Drawing.Point(0, 0);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(466, 50);
            this.panel10.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(20, 14);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(187, 21);
            this.label6.TabIndex = 14;
            this.label6.Text = "Open Items per Section";
            // 
            // ShipRejected
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.RejectedGrid);
            this.Name = "ShipRejected";
            this.Size = new System.Drawing.Size(1352, 786);
            this.Load += new System.EventHandler(this.ShipRejected_Load);
            ((System.ComponentModel.ISupportInitialize)(this.RejectedGrid)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            this.panel10.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView RejectedGrid;
        private System.Windows.Forms.Button OpenReject;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox sectionfilter;
        private System.Windows.Forms.ComboBox filteritems;
        private System.Windows.Forms.Panel panel1;
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
        private System.Windows.Forms.DataGridViewTextBoxColumn UploadImage;
        private System.Windows.Forms.DataGridViewImageColumn Edit;
        private System.Windows.Forms.DataGridViewImageColumn Delete;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Label label6;
        private LiveCharts.WinForms.PieChart pieChart1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private LiveCharts.WinForms.CartesianChart cartesianChart1;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label label7;
    }
}
